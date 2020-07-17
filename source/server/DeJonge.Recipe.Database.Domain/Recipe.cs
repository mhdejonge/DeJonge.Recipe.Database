namespace DeJonge.Recipe.Database.Domain
{
    using System.Collections.Generic;
    using MongoDB.Bson;

    public class Recipe
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; } = null!;

        public List<Ingredient> Ingredients { get; set; } = null!;

        public List<string> Instructions { get; set; } = null!;
    }
}
