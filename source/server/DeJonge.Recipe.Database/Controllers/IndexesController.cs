namespace DeJonge.Recipe.Database.Controllers
{
    using System.Threading.Tasks;
    using DeJonge.Recipe.Database.Domain;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route(Routes.Default)]
    public class IndexesController : ControllerBase
    {
        private readonly IRecipeRepository _recipes;

        public IndexesController(IRecipeRepository recipes)
        {
            _recipes = recipes;
        }

        [HttpPatch]
        public async Task CreateIndexes()
        {
            await _recipes.CreateIndexes();
        }
    }
}
