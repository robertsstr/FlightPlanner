using FlightPlanner.Api.Validators.Helpers;
using FlightPlanner.Core.Models;
using FluentValidation;

namespace FlightPlanner.Api.Validators.Validations
{
    public class SearchFlightRequestValidator : AbstractValidator<SearchFlightsRequest>
    {
        public SearchFlightRequestValidator()
        {
            RuleFor(request => request.From)
                .NotEmpty()
                .WithMessage("Departure airport required.");
            RuleFor(request => request.To)
                .NotEmpty()
                .WithMessage("Arrival airport required.");
            RuleFor(request => request.DepartureDate)
                .NotEmpty()
                .Must(DateValidationHelper.IsValidDateTime)
                .WithMessage("Invalid departure date data.");
            RuleFor(request => request)
                .Must(request => request.From != request.To)
                .WithMessage("Departure and arrival airports cannot be the same.");
        }
    }
}
