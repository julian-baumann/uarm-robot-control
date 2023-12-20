
using UArmSDK;
using UArmSDK.Commands;

namespace RoboGuidanceSystem.Services;

public class RobotCommunicationService
{
    private UArmConnection? _connection;

    public string[] GetSerialPorts()
    {
        return UArmCommunication.GetDevices();
    }

    public void Connect(string port)
    {
        _connection = UArmCommunication.Connect(port);
    }

    public Task QueryCommand<TCommand>(TCommand command) where TCommand : CommandBase
    {
        return _connection.QueryCommand(command);
    }
}
