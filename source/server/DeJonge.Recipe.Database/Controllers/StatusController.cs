namespace DeJonge.Recipe.Database.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route(Routes.Default)]
    public class StatusController : ControllerBase
    {
        public string Status()
        {
            return "Site is running...";
        }
    }
}
