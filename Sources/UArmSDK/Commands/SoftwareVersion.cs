namespace UArmSDK.Commands;

public record SoftwareVersion : CommandBase, IWithResponse
{
    public string Version { get; private set; } = null!;

    internal override string Command => "P203";

    public void ParseResult(string rawResponse)
    {
        Version = rawResponse.Replace("V", "").Trim();
    }
}
