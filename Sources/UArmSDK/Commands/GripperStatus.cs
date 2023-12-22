namespace UArmSDK.Commands;


public record GripperStatus : CommandBase, IWithResponse
{
    public bool Grip { get; private set; }
    internal override string Command => "P233";
    public void ParseResult(string rawResponse)
    {
        var rawState = rawResponse.Remove(0, 1).Trim();
        Grip = rawState == "0";
    }
}
