namespace UArmSDK.Commands;

public record DeviceName : CommandBase, IWithResponse
{
    public string Name { get; private set; } = null!;

    internal override string Command => "P201";

    public void ParseResult(string rawResponse)
    {
        Name = rawResponse.Replace("V", "").Trim();
    }
}
