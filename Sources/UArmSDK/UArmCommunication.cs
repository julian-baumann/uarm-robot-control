using System.IO.Ports;

namespace UArmSDK;

public class UArmCommunication
{
    public static string[] GetDevices()
    {
        return SerialPort.GetPortNames();
    }

    private static SerialPort InitializeSerialPort(string port)
    {
        var serialPort = new SerialPort(
            portName: port,
            baudRate: 115200,
            parity: Parity.None,
            dataBits: 8,
            stopBits: StopBits.One
        );

        serialPort.NewLine = "\r\n";
        serialPort.ReadTimeout = 5000;

        serialPort.Open();

        return serialPort;
    }

    public static UArmConnection Connect(string port)
    {
        var serialPort = InitializeSerialPort(port);
        serialPort.Dispose();

        serialPort = InitializeSerialPort(port);

        return new UArmConnection(serialPort);
    }

}
