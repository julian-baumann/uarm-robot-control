namespace RoboGuidanceSystem.Commands;

public record GetNameCommand : CommandBase<GetNameCommand.Response>
{
    public override string Code => "P201\\n";

    public override Response ParseResponse(string response)
    {
        return new Response(response.Replace("V", "").Trim());
    }

    public record Response(string Version);
}
