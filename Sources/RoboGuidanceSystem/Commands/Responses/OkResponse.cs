namespace RoboGuidanceSystem.Commands.Responses;

public record OkResponse : IResponse
{
    public dynamic Parse(string rawResponse)
    {
        return this;
    }
}
