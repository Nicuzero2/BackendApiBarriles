using BackendApiBarriles.DAL.Accounts;
using BackendApiBarriles.LayerBLAppBarriles.Accounts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BackendApiBarriles
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configura tus servicios aquí
            services.AddSingleton(Configuration); // Inyecta IConfiguration como un servicio
            services.AddTransient<AccountsBL>(); // Configura AccountsBL para la inyección de dependencias
            services.AddTransient<AccountsDAL>(); // Configura AccountsBL para la inyección de dependencias
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // Configuración para entorno de producción
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                // Configuración de rutas y controladores
            });
        }
    }
}
