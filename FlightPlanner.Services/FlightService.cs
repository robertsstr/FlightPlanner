using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlannerDbContext context) : base(context)
        {
        }

        public Flight? GetFullFlightById(int id)
        {
            return _context.Flights
                .Include(flight => flight.To)
                .Include(flight => flight.From)
                .SingleOrDefault(flight => flight.Id == id);
        }

        public bool HasDuplicateFlight(Flight request)
        {
            return _context.Flights.Any(f =>
                f.From.AirportCode == request.From.AirportCode &&
                f.To.AirportCode == request.To.AirportCode &&
                f.Carrier == request.Carrier &&
                f.DepartureTime == request.DepartureTime &&
                f.ArrivalTime == request.ArrivalTime);
        }
    }
}
