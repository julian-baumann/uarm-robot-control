using System.Globalization;

namespace UArmSDK.Commands;

public record CurrentServoAngles : CommandBase, IWithResponse
{
    public float BottomServo { get; private set; }
    public float LeftServo { get; private set; }
    public float RightServo { get; private set; }
    public float HeightServo { get; private set; }

    internal override string Command => "P200";

    public void ParseResult(string rawResponse)
    {
        var splitResponse = rawResponse.Split(" ");

        if (splitResponse.Length != 4)
        {
            throw new OutputParseException();
        }

        BottomServo = float.Parse(splitResponse[0].Remove(0, 1).Trim(), CultureInfo.InvariantCulture);
        LeftServo = float.Parse(splitResponse[1].Remove(0, 1).Trim(), CultureInfo.InvariantCulture);
        RightServo = float.Parse(splitResponse[2].Remove(0, 1).Trim(), CultureInfo.InvariantCulture);
        HeightServo = float.Parse(splitResponse[3].Remove(0, 1).Trim(), CultureInfo.InvariantCulture);
    }
}
