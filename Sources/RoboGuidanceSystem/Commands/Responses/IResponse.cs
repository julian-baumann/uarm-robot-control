namespace RoboGuidanceSystem.Commands.Responses;

public interface IResponse
{
    public dynamic Parse(string rawResponse);
}
