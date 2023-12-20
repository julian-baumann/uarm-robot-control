namespace UArmSDK.Commands;

public record MoveRelative(int X, int Y, int Z, int Speed) : CommandBase
{
    internal override string Command => $"G204 X{X} Y{Y} Z{Z} F{Speed}";
}
