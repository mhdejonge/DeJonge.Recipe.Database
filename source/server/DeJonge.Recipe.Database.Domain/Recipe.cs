namespace DeJonge.Recipe.Database.Domain
{
    using System.Collections.Generic;
    using MongoDB.Bson;

    public class Recipe
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public List<Ingredient> Ingredients { get; set; }

        public List<string> Instructions { get; set; }
    }
}
