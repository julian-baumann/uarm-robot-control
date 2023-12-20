
using UArmSDK;
using UArmSDK.Commands;

namespace RoboGuidanceSystem.Services;

public class RobotCommunicationService
{
    private UArmConnection _connection;

    public RobotCommunicationService(IConfiguration configuration)
    {
        var serialPort = configuration["SerialPort"];

        if (string.IsNullOrEmpty(serialPort))
        {
            throw new NullReferenceException("SerialPort configuration is not set.");
        }

        _connection = UArmCommunication.Connect(serialPort);
    }

    public string[] GetSerialPorts()
    {
        return UArmCommunication.GetDevices();
    }

    public Task QueryCommand<TCommand>(TCommand command) where TCommand : CommandBase
    {
        return _connection.QueryCommand(command);
    }
}
