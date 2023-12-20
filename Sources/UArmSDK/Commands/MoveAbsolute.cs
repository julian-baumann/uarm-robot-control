namespace UArmSDK.Commands;

public record MoveAbsolute(int X, int Y, int Z, int Speed) : CommandBase
{
    internal override string Command => $"G0 X{X} Y{Y} Z{Z} F{Speed}";
}
