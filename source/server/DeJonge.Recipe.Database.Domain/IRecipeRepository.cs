namespace DeJonge.Recipe.Database.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MongoDB.Bson;

    public interface IRecipeRepository
    {
        Task<Recipe?> Get(ObjectId recipe);

        Task<List<Recipe>> Get(params Expression<Func<Recipe, object>>[] fields);

        Task Insert(Recipe recipe);

        Task Update(Recipe recipe);

        Task Delete(ObjectId recipe);
    }
}
