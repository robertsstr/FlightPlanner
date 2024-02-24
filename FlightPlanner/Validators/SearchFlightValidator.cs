using FlightPlanner.Models;

namespace FlightPlanner.Validators
{
    public static class SearchFlightValidator
    {
        public static bool IsSearchFlightDataValid(SearchFlightsRequest request)
        {
            return string.IsNullOrEmpty(request.From) ||
                   string.IsNullOrEmpty(request.To) ||
                   string.IsNullOrEmpty(request.DepartureDate);
        } 

        public static bool AreAirportsDifferent(SearchFlightsRequest request)
        {
            return string.Equals(request.From, request.To, StringComparison.OrdinalIgnoreCase);
        }
    }
}
