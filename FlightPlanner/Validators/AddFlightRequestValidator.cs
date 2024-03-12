using FlightPlanner.Api.Dtos;
using FluentValidation;

namespace FlightPlanner.Api.Validators
{
    public class AddFlightRequestValidator : AbstractValidator<AddFlightRequest>
    {
        public AddFlightRequestValidator()
        {
            RuleFor(request => request.From).SetValidator(new AirportViewModelValidator());
            RuleFor(request => request.To).SetValidator(new AirportViewModelValidator());
            RuleFor(request => request.Carrier)
                .NotEmpty()
                .WithMessage("Carrier required.");
            RuleFor(request => request.DepartureTime)
                .NotEmpty()
                .Must(DateValidationHelper.IsValidDateTime)
                .WithMessage("Invalid departure time data.");
            RuleFor(request => request.ArrivalTime)
                .NotEmpty()
                .Must(DateValidationHelper.IsValidDateTime)
                .WithMessage("Invalid arrival time data.");
            RuleFor(request => request)
                .Must(DateValidationHelper.DepartureIsBeforeArrival)
                .WithMessage("Departure must be before arrival.");
            RuleFor(request => request)
                .Must(FlightValidationHelper.AreAirportsDifferent)
                .WithMessage("From and To airports must be different.");
        }
    }
}
