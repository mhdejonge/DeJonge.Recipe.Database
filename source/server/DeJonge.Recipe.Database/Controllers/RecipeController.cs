namespace DeJonge.Recipe.Database.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DeJonge.Recipe.Database.Domain;
    using Microsoft.AspNetCore.Mvc;
    using MongoDB.Bson;

    [ApiController]
    [Route(Routes.Default)]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipes;

        public RecipeController(IRecipeRepository recipes)
        {
            _recipes = recipes;
        }

        [HttpGet(Routes.Id)]
        public async Task<ActionResult<List<Recipe>>> Get()
        {
            return await _recipes.Get(recipe => recipe.Name);
        }

        [HttpGet(Routes.Id)]
        public async Task<ActionResult<Recipe>> Get([FromRoute] string id)
        {
            if (!ObjectId.TryParse(id, out var recipeId))
            {
                return BadRequest();
            }
            var recipe = await _recipes.Get(recipeId);
            if (recipe != null)
            {
                return recipe;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Recipe? recipe)
        {
            if (recipe == null)
            {
                return BadRequest();
            }
            await _recipes.Insert(recipe);
            return CreatedAtAction(nameof(Get), new { id = recipe.Id }, recipe);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Recipe? recipe)
        {
            if (recipe == null)
            {
                return BadRequest();
            }
            await _recipes.Update(recipe);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] string? id = null)
        {
            if (ObjectId.TryParse(id, out var recipeId))
            {
                await _recipes.Delete(recipeId);
                return NoContent();
            }
            return BadRequest();
        }
    }
}
