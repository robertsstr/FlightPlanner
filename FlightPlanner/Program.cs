using System.Reflection;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Handler;
using FlightPlanner.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<FlightPlannerDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("FlightPlanner")));

            builder.Services.AddScoped<IFlightPlannerDbContext, FlightPlannerDbContext>();
            builder.Services.AddScoped<IDbService, DbService>();
            builder.Services.AddScoped<IEntityService<Flight>, EntityService<Flight>>();
            builder.Services.AddScoped<IEntityService<Airport>, EntityService<Airport>>();
            builder.Services.AddScoped<IFlightService, FlightService>();
            builder.Services.AddScoped<ICleanupService, CleanupService>();
            builder.Services.AddScoped<ISearchFlightsService, SearchFlightsService>();

            var assembly = Assembly.GetExecutingAssembly();
            builder.Services.AddAutoMapper(assembly);
            builder.Services.AddValidatorsFromAssembly(assembly);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
