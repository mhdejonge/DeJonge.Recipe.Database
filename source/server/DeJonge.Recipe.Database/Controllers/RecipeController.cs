namespace DeJonge.Recipe.Database.Controllers
{
    using DeJonge.Recipe.Database.Domain;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route(Routes.Default)]
    public class RecipeController : ControllerBase
    {
        [HttpGet]
        public void Get() { }

        [HttpPost]
        public void Post() { }

        [HttpPut]
        public void Put() { }

        [HttpDelete]
        public void Delete() { }
    }
}
