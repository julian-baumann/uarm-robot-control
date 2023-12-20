using System.IO.Ports;
using UArmSDK.Commands;

namespace UArmSDK;

internal record QueueItem(CommandBase Command, uint RequestId, Action<IWithResponse?> SetResult, Action<Exception> SetError);

public class UArmConnection : IDisposable
{
    private readonly SerialPort _serialPort;
    private readonly Queue<QueueItem> _writeQueue = new();
    private readonly HashSet<QueueItem> _readQueue = [];
    private readonly Thread _writeThread;
    private readonly Thread _readThread;
    private bool _readActive = false;
    private bool _writeActive = false;
    private bool _isReady = false;

    private uint _currentRequestId = 0;

    public UArmConnection(SerialPort serialPort)
    {
        _serialPort = serialPort;

        _readActive = true;
        _readThread = new Thread(ReadThread);
        _readThread.Start();

        _writeActive = true;
        _writeThread = new Thread(WriteThread);
        _writeThread.Start();
    }

    public Task<TResponse> Get<TResponse>() where TResponse : CommandBase, IWithResponse
    {
        var taskCompletionSource = new TaskCompletionSource<TResponse>();

        _writeQueue.Enqueue(new QueueItem(
                Command: Activator.CreateInstance<TResponse>(),
                RequestId: _currentRequestId++,
                SetResult: response =>
                {
                    if (response is TResponse typedResponse)
                    {
                        taskCompletionSource.SetResult(typedResponse);
                    }
                },
                SetError: error => taskCompletionSource.SetException(error)
            )
        );

        return taskCompletionSource.Task;
    }

    public Task QueryCommand<TResponse>(TResponse command) where TResponse : CommandBase
    {
        var taskCompletionSource = new TaskCompletionSource();

        _writeQueue.Enqueue(new QueueItem(
                Command: command,
                RequestId: _currentRequestId++,
                SetResult: _ =>
                {
                    taskCompletionSource.SetResult();
                },
                SetError: error => taskCompletionSource.SetException(error)
            )
        );

        return taskCompletionSource.Task;
    }

    public void Dispose()
    {
        _readActive = false;
        _writeActive = false;
        _readThread.Interrupt();
        _writeThread.Interrupt();
        _serialPort.Dispose();
    }

    private void WriteThread()
    {
        while (_writeActive)
        {
            while (_writeQueue.Count > 0 && _isReady)
            {
                var queueItem = _writeQueue.Dequeue();
                _readQueue.Add(queueItem);
                var command = $"#{queueItem.RequestId} {queueItem.Command.Command}\n";
                lock (_serialPort)
                {
                    _serialPort.Write(command);
                }
            }
        }
    }


    private void ReadThread()
    {
        while (_readActive)
        {
            try
            {
                var answer = _serialPort.ReadLine();
                if (answer == "@1")
                {
                    _isReady = true;
                }

                if (!answer.StartsWith('$'))
                {
                    continue;
                }

                var rawRequestId = answer.Split(" ")
                    .FirstOrDefault()
                    ?.Replace("$", "")
                    .Trim();

                if (string.IsNullOrEmpty(rawRequestId))
                {
                    continue;
                }

                var requestId = ushort.Parse(rawRequestId);

                var requestResponse = _readQueue.FirstOrDefault(element => element.RequestId == requestId);

                if (requestResponse == null)
                {
                    continue;
                }

                var response = answer.Replace($"${requestId} ", "");

                if (response.StartsWith("OK"))
                {
                    try
                    {
                        if (requestResponse.Command is IWithResponse requestResponseCommand)
                        {
                            requestResponseCommand.ParseResult(response.Replace("OK ", ""));
                            requestResponse.SetResult.Invoke(requestResponseCommand);
                        }

                        requestResponse.SetResult.Invoke(null);
                    }
                    catch (Exception exception)
                    {
                        requestResponse.SetError.Invoke(exception);
                    }
                }
                else if (response.StartsWith('E'))
                {
                    var exception = new Exception("Unknown error");

                    if (response.StartsWith("E20"))
                    {
                        exception = new CommandDoesNotExistException();
                    }
                    else if (response.StartsWith("E21"))
                    {
                        exception = new InvalidParameterException();
                    }
                    else if (response.StartsWith("E22"))
                    {
                        exception = new AddressOutOfRangeException();
                    }

                    requestResponse.SetError(exception);
                }

                _readQueue.Remove(requestResponse);
            }
            catch (TimeoutException)
            {
                // ignore and continue
            }
        }
    }
}
