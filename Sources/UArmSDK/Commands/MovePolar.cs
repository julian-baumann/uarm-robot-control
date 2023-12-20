using UArmSDK.Extensions;

namespace UArmSDK.Commands;

public record MovePolar(float Stretch, float Rotation, float Height, float Speed) : CommandBase
{
    internal override string Command => $"G201 S{Stretch.ToInvariantString()} R{Rotation.ToInvariantString()} H{Height.ToInvariantString()} F{Speed.ToInvariantString()}";
}
