using FlightPlanner.UseCases.Dtos;

namespace FlightPlanner.UseCases.Validators.Helpers;

public static class DateValidationHelper
{
    public static bool IsValidDateTime(string dateString)
    {
        return DateTime.TryParse(dateString, out _);
    }

    public static bool DepartureIsBeforeArrival(AddFlightRequest request)
    {
        return DateTime.Parse(request.DepartureTime) < DateTime.Parse(request.ArrivalTime);
    }
}