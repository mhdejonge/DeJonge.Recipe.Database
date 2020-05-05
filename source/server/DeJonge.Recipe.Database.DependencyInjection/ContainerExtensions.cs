namespace DeJonge.Recipe.Database.DependencyInjection
{
    using System.Linq;
    using DeJonge.Recipe.Database.Domain;
    using DeJonge.Recipe.Database.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using MongoDB.Driver;
    using SimpleInjector;

    public static class ContainerExtensions
    {
        public static void Integrate(this IServiceCollection services, Container container, AppSettings appSettings)
        {
            services.AddSimpleInjector(container, options => options.AddAspNetCore().AddControllerActivation());
            container.RegisterDatabase(appSettings);
            container.RegisterApplicationTypes();
        }

        private static void RegisterDatabase(this Container container, AppSettings appSettings)
        {
            var url = new MongoUrl(appSettings.ConnectionUrl);
            var client = new MongoClient(url);
            var database = client.GetDatabase(url.DatabaseName);
            container.RegisterInstance(database);
        }

        private static void RegisterApplicationTypes(this Container container)
        {
            var services = from type in typeof(RecipeRepository).Assembly.GetExportedTypes()
                           where type.IsClass && !type.IsAbstract && type.GetInterfaces().Any()
                           let service = type.GetInterfaces().Single()
                           select (type, service);
            foreach (var (type, service) in services)
            {
                container.Register(service, type, Lifestyle.Singleton);
            }
        }
    }
}
