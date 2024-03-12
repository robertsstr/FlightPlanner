﻿using FlightPlanner.Core.Services;
using FlightPlanner.Data;

namespace FlightPlanner.Services
{
    public class CleanupService : DbService, ICleanupService
    {
        public CleanupService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public void Cleanup()
        {
            _context.Airports.RemoveRange(_context.Airports);
            _context.Flights.RemoveRange(_context.Flights);
            _context.SaveChanges();
        }
    }
}
