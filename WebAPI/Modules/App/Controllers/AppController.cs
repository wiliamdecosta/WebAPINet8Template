using Microsoft.AspNetCore.Mvc;

namespace InitProject.Modules.App.Controllers
{
    [Route("/")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AppController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Index()
        {
            return Ok("Welcome to Awk Web API");
        }
    }
}
