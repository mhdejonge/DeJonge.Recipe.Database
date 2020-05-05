namespace DeJonge.Recipe.Database
{
    using DeJonge.Recipe.Database.DependencyInjection;
    using DeJonge.Recipe.Database.Domain;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using SimpleInjector;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            AppSettings = configuration.Get<AppSettings>();
            Container = new Container();
        }

        public AppSettings AppSettings { get; }

        public Container Container { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.Integrate(Container, AppSettings);
        }

        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            Container.Verify();
            if (environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }
            application.UseHttpsRedirection();
            application.UseRouting();
            application.UseAuthorization();
            application.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToFile(Routes.Index);
            });
        }
    }
}
