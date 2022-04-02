using Microsoft.Extensions.DependencyInjection;
using FazendaFeliz.ApplicationCore.Interfaces;
using FazendaFeliz.ApplicationCore.Interfaces.Repository;
using FazendaFeliz.Infrastructure.Data.Context;
using FazendaFeliz.Infrastructure.Data.Repositories;
using FazendaFeliz.Infrastructure.Services;
using FazendaFeliz.ApplicationCore.Interfaces.Service;

namespace FazendaFeliz.Infrastructure.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // DbContexts
            services.AddScoped<AppDbContext>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IAnuncioRepository, AnuncioRepository>();

            // Services
            services.AddTransient<IIdentityService, IdentityService>();

            return services;
        }
    }
}
