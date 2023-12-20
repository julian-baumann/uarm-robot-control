using Microsoft.AspNetCore.Mvc;
using RoboGuidanceSystem.Services;
using UArmSDK.Commands;

namespace RoboGuidanceSystem.Controllers;

[Route("move")]
public class MoveController(RobotCommunicationService robotCommunicationService) : ControllerBase
{
    [HttpPost]
    [Route("polar")]
    public async Task<ActionResult> Move([FromQuery] float stretch, [FromQuery] float rotation, [FromQuery] float height, [FromQuery] float speed)
    {
        await robotCommunicationService.QueryCommand(new MovePolar(
                Stretch: stretch,
                Rotation: rotation,
                Height: height,
                Speed: speed
            )
        );

        return Ok();
    }
}
