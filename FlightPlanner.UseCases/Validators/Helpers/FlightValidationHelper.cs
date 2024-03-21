using FlightPlanner.UseCases.Dtos;

namespace FlightPlanner.UseCases.Validators.Helpers;

public static class FlightValidationHelper
{
    public static bool AreAirportsDifferent(AddFlightRequest request)
    {
        return !string.Equals(request.From.Airport.Trim(), request.To.Airport.Trim(), StringComparison.OrdinalIgnoreCase);
    }
}