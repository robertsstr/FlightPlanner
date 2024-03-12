using FlightPlanner.Api.Dtos;

namespace FlightPlanner.Api.Validators
{
    public static class DateValidationHelper
    {
        public static bool IsValidDateTime(string dateString)
        {
            return DateTime.TryParse(dateString, out _);
        }

        public static bool DepartureIsBeforeArrival(AddFlightRequest request)
        {
            return DateTime.Parse(request.ArrivalTime) <= DateTime.Parse(request.DepartureTime);
        }
    }
}
