using Microsoft.AspNetCore.Mvc;

namespace prescreminder.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeartBeatController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Is Alive";
        }
    }
}
