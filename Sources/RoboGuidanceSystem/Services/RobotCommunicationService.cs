using System.IO.Ports;
using RoboGuidanceSystem.Commands;

namespace RoboGuidanceSystem.Services;

public class RobotCommunicationService
{
    private readonly Queue<CommandBase<dynamic>> _queue = new();

    private SerialPort _serialPort;
    private uint _currentCount = 0;
    private bool _isReading = true;

    public string[] GetDevices()
    {
        return SerialPort.GetPortNames();
    }

    public void Connect(string port)
    {
        _serialPort = new SerialPort(port);
        _serialPort.Open();
    }

    private void ReadThread()
    {
        while (_isReading)
        {
            if (_serialPort.IsOpen)
            {
                var currentReadLine = _serialPort.ReadLine();

            }
        }
    }

    private void WriteThread()
    {
        while (_isReading)
        {
            if (_serialPort.IsOpen)
            {
                foreach (var command in _queue)
                {
                    _serialPort.WriteLine($"{command.CommandId} {command.Code}");
                }
            }
        }
    }

    public Task<TResponse> ExecuteCommand<TResponse>(CommandBase<TResponse> commandBase)
    {
        lock (_queue)
        {
            commandBase.CommandId = _currentCount++;
            _queue.Enqueue(commandBase);
        }
    }
}
