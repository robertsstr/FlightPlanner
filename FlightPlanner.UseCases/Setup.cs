using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FlightPlanner.UseCases;

public static class Setup
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddValidatorsFromAssembly(assembly);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
            services.AddAutoMapper(assembly);

            return services;
    }
}