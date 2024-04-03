using FlightPlanner.Core.Models;
using FlightPlanner.Core.Semaphore;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services;

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
        SemaphoreUtility.SharedSemaphore.Wait();
        try
        {
            return _context.Flights.Any(f =>
                f.From.AirportCode == request.From.AirportCode &&
                f.To.AirportCode == request.To.AirportCode &&
                f.Carrier == request.Carrier &&
                f.DepartureTime == request.DepartureTime &&
                f.ArrivalTime == request.ArrivalTime);
        }
        finally
        {
            SemaphoreUtility.SharedSemaphore.Release();
        }
    }

    public Flight CreateFlight(Flight flight)
    {
        SemaphoreUtility.SharedSemaphore.Wait();
        try
        {
            var fromAirport = _context.Airports
                .FirstOrDefault(a => a.AirportCode == flight.From.AirportCode);
            var toAirport = _context.Airports
                .FirstOrDefault(a => a.AirportCode == flight.To.AirportCode);

            if (fromAirport == null)
            {
                _context.Airports.Add(flight.From);
                fromAirport = flight.From;
            }

            if (toAirport == null)
            {
                _context.Airports.Add(flight.To);
                toAirport = flight.To;
            }

            flight.From = fromAirport;
            flight.To = toAirport;

            return Create(flight);
        }
        finally
        {
            SemaphoreUtility.SharedSemaphore.Release();
        }
    }
}
