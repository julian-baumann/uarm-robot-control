namespace UArmSDK.Commands;

public enum PumpState
{
    Stopped,
    Working,
    Grabbing
}

public record PumpStatus : CommandBase, IWithResponse
{
    public PumpState State { get; private set; }

    internal override string Command => "P231";

    public void ParseResult(string rawResponse)
    {
        var rawState = rawResponse.Remove(0, 1).Trim();

        if (rawState == "0")
        {
            State = PumpState.Stopped;
        }
        else if (rawState == "1")
        {
            State = PumpState.Working;
        }
        else if (rawState == "2")
        {
            State = PumpState.Grabbing;
        }
    }
}
