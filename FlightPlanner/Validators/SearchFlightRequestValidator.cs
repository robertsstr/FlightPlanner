using FlightPlanner.Core.Models;
using FluentValidation;

namespace FlightPlanner.Api.Validators
{
    public class SearchFlightRequestValidator : AbstractValidator<SearchFlightsRequest>
    {
        public SearchFlightRequestValidator()
        {
            RuleFor(request => request.From)
                .NotEmpty()
                .WithMessage("Departure airport required.");
            RuleFor(request => request.From)
                .NotEmpty()
                .WithMessage("Arrival airport required.");
            RuleFor(request => request.DepartureDate)
                .NotEmpty()
                .Must(DateValidationHelper.IsValidDateTime)
                .WithMessage("Invalid departure date data.");
        }
    }
}
