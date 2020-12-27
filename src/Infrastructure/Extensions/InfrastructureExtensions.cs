using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using src.Core;
using src.Core.Interfaces.Todo;
using src.Core.Services;

namespace src.Infrastructure.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<ITodoService, TodoService>();

            services.AddAutoMapperSetup();

            services.AddCompression();

            return services;
        }
    }
}