using FlightPlanner.Api.Dtos;
using FluentValidation;

namespace FlightPlanner.Api.Validators.Validations
{
    public class AirportViewModelValidator : AbstractValidator<AirportViewModel>
    {
        public AirportViewModelValidator()
        {
            RuleFor(airport => airport.Airport)
                .NotEmpty()
                .WithMessage("Airport required.");
            RuleFor(airport => airport.City)
                .NotEmpty()
                .WithMessage("City required.");
            RuleFor(airport => airport.Country)
                .NotEmpty()
                .WithMessage("Country required.");
        }
    }
}
