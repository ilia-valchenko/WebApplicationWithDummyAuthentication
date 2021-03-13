using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApplicationWithDummyAuthentication.Handlers;
using WebApplicationWithDummyAuthentication.Options;

namespace WebApplicationWithDummyAuthentication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        // ASP .NET Core has built-in dependency injection functionality.
        // It has built-in IoC container.
        // If you want more features such as auto-registration, scanning, interceptors, or decorators
        // then you may replace built-in IoC container with a third party container.
        public void ConfigureServices(IServiceCollection services)
        {
            // NOTE: I'm not sure I need to use AddMvc here. It's a REST API service. AddControllers should be enough.
            //services.AddControllers();
            services.AddMvc();

            // .AddAuthentication() adds authentication middleware.
            // That being said, you probably don't want to create your own middleware: you probably want to create
            // a new authentication handler that plays nicely with the ASP.NET authentication framework
            // (so that you use the [Authorize] attribute on controllers).

            //services.AddAuthentication(options => { options.DefaultScheme = "testAuthScheme"; })
            services.AddAuthentication()
                .AddScheme<DummyAuthenticationOptions, DummyAuthenticationHandler>("testAuthScheme", authOptions => { })
                .AddScheme<TokenBasedAuthenticationOptions, TokenBasedAuthenticationHandler>("tokenBasedAuthScheme", authOptions => { });

            Services.Infrastructure.Bootstrapper.ConfigureServices(services);
        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
        // Don't forget that you can pass the ILoggerFactory.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory factory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // NOTE: The order is important!

            app.UseRouting();
            //app.UseCors();

            // .AddAuthentication() adds the auth services to the service collection
            // whereas .UseAuthentication() adds the .NET Core's authentication middleware to the pipeline.
            // If you have your own custom middleware, you don't need .UseAuthentication().

            // .AddAuthentication() and .UseAuthentication() isn't enough for activating your authentication even if you
            // implemented your custom authentication handler.
            //app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // MapControllers is enough for RESP API service.
                // endpoints.MapControllers();

                endpoints.MapControllerRoute("default", "{controller=Person}/{action=GetAsync}");
            });
        }
    }
}