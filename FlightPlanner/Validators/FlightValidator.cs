using FlightPlanner.Models;

namespace FlightPlanner.Validators
{
    public static class FlightValidator
    {
        public static bool IsFlightDataValid(Flight flight)
        {
             return string.IsNullOrEmpty(flight.From.Country) ||
                    string.IsNullOrEmpty(flight.From.City) ||
                    string.IsNullOrEmpty(flight.From.AirportCode) ||
                    string.IsNullOrEmpty(flight.To.Country) ||
                    string.IsNullOrEmpty(flight.To.City) ||
                    string.IsNullOrEmpty(flight.To.AirportCode) ||
                    string.IsNullOrEmpty(flight.Carrier) ||
                    string.IsNullOrEmpty(flight.DepartureTime) ||
                    string.IsNullOrEmpty(flight.ArrivalTime);
        }

        public static bool AreAirportsDifferent(Flight flight)
        {
            return string.Equals(flight.From.AirportCode.Trim(), flight.To.AirportCode.Trim(), StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsDateValid(Flight flight)
        {
            return DateTime.Parse(flight.ArrivalTime) <= DateTime.Parse(flight.DepartureTime);
        }
    }
}
