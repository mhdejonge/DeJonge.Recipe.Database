namespace DeJonge.Recipe.Database.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using DeJonge.Recipe.Database.Domain;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using static MongoDB.Driver.Builders<Domain.Recipe>;

    public class RecipeRepository : IRecipeRepository
    {
        public RecipeRepository(IMongoDatabase database)
        {
            Recipes = database.GetCollection<Recipe>();
        }

        private IMongoCollection<Recipe> Recipes { get; }

        public async Task<Recipe?> Get(ObjectId recipe)
        {
            return await Recipes.Find(FindById(recipe)).SingleOrDefaultAsync();
        }

        public async Task<List<Recipe>> Get(string? search, params Expression<Func<Recipe, object>>[] fields)
        {
            var filter = string.IsNullOrEmpty(search) ? Filter.Empty : Filter.Text(search);
            var projection = Projection.Combine(fields.Select(Projection.Include));
            return await Recipes.Find(filter).Project(projection).As<Recipe>().ToListAsync();
        }

        public async Task Insert(Recipe recipe)
        {
            await Recipes.InsertOneAsync(recipe);
        }

        public async Task Update(Recipe recipe)
        {
            await Recipes.ReplaceOneAsync(FindById(recipe.Id), recipe);
        }

        public async Task Delete(ObjectId recipe)
        {
            await Recipes.DeleteOneAsync(FindById(recipe));
        }

        private static FilterDefinition<Recipe> FindById(ObjectId recipe)
        {
            return Filter.Eq(document => document.Id, recipe);
        }

        public async Task CreateIndexes()
        {
            var definition = IndexKeys.Text(recipe => recipe.Name).Text(recipe => recipe.Tags);
            await Recipes.Indexes.CreateOneAsync(new CreateIndexModel<Recipe>(definition));
        }
    }
}
