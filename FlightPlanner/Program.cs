using FlightPlanner.Api.Handler;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using FlightPlanner.Services;
using FlightPlanner.UseCases;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Api;

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

        builder.Services.AddTransient<IFlightPlannerDbContext, FlightPlannerDbContext>();
        builder.Services.AddTransient<IDbService, DbService>();
        builder.Services.AddTransient<IEntityService<Flight>, EntityService<Flight>>();
        builder.Services.AddTransient<IEntityService<Airport>, EntityService<Airport>>();
        builder.Services.AddTransient<IFlightService, FlightService>();
        builder.Services.AddTransient<IAirportService, AirportService>();
        builder.Services.AddTransient<ICleanupService, CleanupService>();
        builder.Services.AddTransient<ISearchFlightsService, SearchFlightsService>();

        builder.Services.AddServices();

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