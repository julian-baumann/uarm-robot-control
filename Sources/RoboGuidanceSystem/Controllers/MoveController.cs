using Microsoft.AspNetCore.Mvc;
using RoboGuidanceSystem.Services;
using UArmSDK.Commands;

namespace RoboGuidanceSystem.Controllers;

[ApiController]
[Route("move")]
public class MoveController(RobotCommunicationService robotCommunicationService) : ControllerBase
{
    [HttpPost]
    [Route("polar")]
    public async Task<ActionResult> Move([FromQuery] float stretch, [FromQuery] float rotation, [FromQuery] float height, [FromQuery] float speed)
    {
        await robotCommunicationService.ExecuteCommand(new MovePolar(
                Stretch: stretch,
                Rotation: rotation,
                Height: height,
                Speed: speed
            )
        );

        return Ok();
    }

    [HttpPost]
    [Route("absolute")]
    public async Task<ActionResult> MoveAbsolute([FromQuery] int x, [FromQuery] int y, [FromQuery] int z, [FromQuery] int speed)
    {
	    await robotCommunicationService.ExecuteCommand(new MoveAbsolute(x, y, z, speed));

	    return Ok();
    }

    [HttpGet]
    [Route("gripperStatus")]
    public async Task<ActionResult> Gripper()
    {
		var commandResult = await robotCommunicationService.QueryCommand<GripperStatus>();
		
		return Ok(commandResult.Grip);
    }
}
