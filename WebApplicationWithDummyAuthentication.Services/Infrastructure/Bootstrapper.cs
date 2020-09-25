using Microsoft.Extensions.DependencyInjection;
using WebApplicationWithDummyAuthentication.Services.Interfaces;

namespace WebApplicationWithDummyAuthentication.Services.Infrastructure
{
    public static class Bootstrapper
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
        }
    }
}