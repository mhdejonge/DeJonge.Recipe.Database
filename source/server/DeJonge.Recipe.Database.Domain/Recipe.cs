namespace DeJonge.Recipe.Database.Domain
{
    using System.Collections.Generic;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    public class Recipe
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired]
        public string Name { get; set; } = null!;

        [BsonIgnoreIfDefault]
        public List<Ingredient>? Ingredients { get; set; }

        [BsonIgnoreIfDefault]
        public List<string>? Instructions { get; set; }

        [BsonIgnoreIfDefault]
        public List<string>? Tags { get; set; }

        [BsonIgnoreIfDefault]
        public string? Source { get; set; }
    }
}
