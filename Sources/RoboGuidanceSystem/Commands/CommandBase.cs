namespace RoboGuidanceSystem.Commands;

public abstract record CommandBase<TResponse>
{
    public uint CommandId { get; set; } = 0;
    public abstract string Code { get; }
    public abstract TResponse ParseResponse(string response);
}
