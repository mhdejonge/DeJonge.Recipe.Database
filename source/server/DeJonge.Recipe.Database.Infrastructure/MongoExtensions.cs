namespace DeJonge.Recipe.Database.Infrastructure
{
    using MongoDB.Driver;
    using Pluralize.NET;

    public static class MongoExtensions
    {
        public static IMongoCollection<TDocument> GetCollection<TDocument>(this IMongoDatabase database)
        {
            return database.GetCollection<TDocument>(new Pluralizer().Pluralize(typeof(TDocument).Name).ToLower());
        }
    }
}
