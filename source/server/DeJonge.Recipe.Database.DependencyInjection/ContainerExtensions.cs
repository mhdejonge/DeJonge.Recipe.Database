namespace DeJonge.Recipe.Database.DependencyInjection
{
    using System;
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
            container.RegisterRepositories();
        }

        private static void RegisterDatabase(this Container container, AppSettings appSettings)
        {
            var url = new MongoUrl(appSettings.ConnectionUrl);
            var client = new MongoClient(url);
            var database = client.GetDatabase(url.DatabaseName);
            container.RegisterInstance(database);
        }

        private static void RegisterRepositories(this Container container)
        {
            var services = from type in typeof(RecipeRepository).Assembly.GetExportedTypes()
                           where type.IsClass && !type.IsAbstract && type.GetInterfaces().Any()
                           select type;
            foreach (var implementationType in services)
            {
                container.Register(implementationType.ServiceType(), implementationType, Lifestyle.Singleton);
            }
        }

        private static Type ServiceType(this Type implementationType)
        {
            var allInterfaces = implementationType.GetInterfaces();
            var inheritedInterfaces = allInterfaces.SelectMany(service => service.GetInterfaces());
            var baseTypeInterfaces = implementationType.BaseType?.GetInterfaces() ?? Enumerable.Empty<Type>();
            var directInterfaces = allInterfaces.Except(inheritedInterfaces).Except(baseTypeInterfaces);
            return directInterfaces.Single();
        }
    }
}
