using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services;

public class CleanupService : DbService, ICleanupService
{
    public CleanupService(IFlightPlannerDbContext context) : base(context)
    {
    }

    public void Cleanup()
    {
        DeleteAll<Flight>();
        DeleteAll<Airport>();
    }
}