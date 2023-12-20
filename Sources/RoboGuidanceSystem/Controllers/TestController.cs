using Microsoft.AspNetCore.Mvc;

namespace RoboGuidanceSystem.Controllers;

[Route("test")]
public class TestController : ControllerBase
{
    [HttpGet]
    public ActionResult Get()
    {
        return Ok("Hello, World");
    }
}
