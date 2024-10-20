using Microsoft.EntityFrameworkCore;
using RegistroTecnicos.Components;
using RegistroTecnicos.Models;
using RegistroTecnicos.DAL;
using RegistroTecnicos.Service;

namespace RegistroTecnicos
{ 
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            //Inyeccion de la base de datos (SqLite)
            var ConStr = builder.Configuration.GetConnectionString("ConStr");
            builder.Services.AddDbContext<Context>(c => c.UseSqlite(ConStr));

            //Inyeccion del servicio (service)
            builder.Services.AddScoped<TecnicoService>();
            builder.Services.AddScoped<TiposTecnicoService>();
			builder.Services.AddScoped<ClienteService>();
			builder.Services.AddScoped<TrabajoService>();
            builder.Services.AddScoped<PrioridadesService>();
            builder.Services.AddScoped<TrabajosDetalleService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }   
    }
}
