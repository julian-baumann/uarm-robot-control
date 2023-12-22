namespace UArmSDK.Commands;

public record Pump(bool Active) : CommandBase
{
    internal override string Command => "M231 V" + (Active ? "1" : "0");
}
