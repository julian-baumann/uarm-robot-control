using UArmSDK;
using UArmSDK.Commands;

// var devices = UArmCommunication.GetDevices();
using var connection = UArmCommunication.Connect("/dev/tty.usbserial-AI04HYTO");

var response = await connection.Get<DeviceName>();
Console.WriteLine($"Name: {response.Name}");

var versionResponse = await connection.Get<SoftwareVersion>();
Console.WriteLine($"Version: {versionResponse.Version}");

var servoAngles = await connection.Get<CurrentServoAngles>();
Console.WriteLine($"Angles: Bottom {servoAngles.BottomServo}, Left: {servoAngles.LeftServo} Right: {servoAngles.RightServo} Height: {servoAngles.HeightServo}");

// Resetting position
await connection.QueryCommand(new MovePolar(Stretch: 100, Rotation: 0, Height: 80, Speed: 10));

Thread.Sleep(3000);
await connection.QueryCommand(new MovePolar(Stretch: 200, Rotation: 100, Height: 80, Speed: 10));

Thread.Sleep(1000);
await connection.QueryCommand(new MovePolar(Stretch: 200, Rotation: 100, Height: 200, Speed: 10));

// Wink
Thread.Sleep(1000);
await connection.QueryCommand(new MovePolar(Stretch: 200, Rotation: 100, Height: 200, Speed: 10));


Thread.Sleep(200);
await connection.QueryCommand(new MovePolar(Stretch: 200, Rotation: 100, Height: 250, Speed: 10));

Thread.Sleep(200);
await connection.QueryCommand(new MovePolar(Stretch: 200, Rotation: 100, Height: 200, Speed: 10));

Thread.Sleep(200);
await connection.QueryCommand(new MovePolar(Stretch: 200, Rotation: 100, Height: 250, Speed: 10));

Thread.Sleep(200);
await connection.QueryCommand(new MovePolar(Stretch: 200, Rotation: 100, Height: 200, Speed: 10));

Thread.Sleep(200);
await connection.QueryCommand(new MovePolar(Stretch: 200, Rotation: 100, Height: 250, Speed: 10));

Thread.Sleep(200);
await connection.QueryCommand(new MovePolar(Stretch: 200, Rotation: 100, Height: 200, Speed: 10));
