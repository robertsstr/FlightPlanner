using FlightPlanner.Models;

namespace FlightPlanner.Storage
{
    public class FlightStorage
    {
        private static List<Flight> _flights = new List<Flight>(); 
        private static int _id = 1;

        public static void AddFlight(Flight flight)
        {
            flight.Id = _id++;
            _flights.Add(flight);
            AirportStorage.AddAirport(flight.From, flight.To);
        }

        public static Flight? GetFlightById(int id)
        {
            return _flights.FirstOrDefault(flight => flight.Id == id);
        }

        public static IEnumerable<Flight> GetFlights(SearchFlightsRequest request)
        {
            return _flights.Where(f =>
                DateTime.Parse(f.DepartureTime).Date == DateTime.Parse(request.DepartureDate).Date &&
                f.From.AirportCode == request.From &&
                f.To.AirportCode == request.To);
        }

        public static bool HasDuplicateFlight(Flight flight)
        {
            return _flights.Any(f =>
                DateTime.Parse(f.DepartureTime) == DateTime.Parse(flight.DepartureTime) &&
                DateTime.Parse(f.ArrivalTime) == DateTime.Parse(flight.ArrivalTime) &&
                f.From.AirportCode == flight.From.AirportCode &&
                f.To.AirportCode == flight.To.AirportCode);
        }

        public static void RemoveFlight(int id)
        {
            Flight flightToRemove = _flights.FirstOrDefault(flight => flight.Id == id);
            if (flightToRemove != null)
            {
                _flights.Remove(flightToRemove);
                AirportStorage.RemoveAirportUsage(flightToRemove.From);
                AirportStorage.RemoveAirportUsage(flightToRemove.To);
            }
        }

        public static void Clear()
        {
            _flights.Clear();
        }
    }
}
