using Microsoft.AspNetCore.Mvc;
using UArmSDK;

namespace RoboGuidanceSystem.Controllers;

[ApiController]
[Route("config")]
public class ConfigurationController : ControllerBase
{
    [HttpGet]
    [Route("ports")]
    public ActionResult<IEnumerable<string>> GetPorts()
    {
        return Ok(UArmCommunication.GetDevices());
    }
}
