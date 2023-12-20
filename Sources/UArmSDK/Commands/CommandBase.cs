namespace UArmSDK.Commands;

public abstract record CommandBase
{
    internal abstract string Command { get; }
}

public interface IWithResponse
{
    void ParseResult(string rawResponse);
}
