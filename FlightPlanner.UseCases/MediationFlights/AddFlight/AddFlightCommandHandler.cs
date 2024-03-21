using System.Net;
using AutoMapper;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Dtos;
using FluentValidation;
using MediatR;

namespace FlightPlanner.UseCases.MediationFlights.AddFlight;

public class AddFlightCommandHandler : IRequestHandler<AddFlightCommand, ServiceResult>
{
    private readonly IFlightService _flightService;
    private readonly IValidator<AddFlightRequest> _validator;
    private readonly IMapper _mapper;

    public AddFlightCommandHandler(IFlightService flightService,
        IValidator<AddFlightRequest> validator, IMapper mapper)
    {
        _flightService = flightService;
        _validator = validator;
        _mapper = mapper;
    }

    public Task<ServiceResult> Handle(AddFlightCommand request, CancellationToken cancellationToken)
    {
        var response = new ServiceResult();

        var validationResult = _validator.Validate(request.AddFlightRequest);
        if (!validationResult.IsValid)
        {
            response.ResultObject = validationResult.Errors;
            response.Status = HttpStatusCode.BadRequest;
            return Task.FromResult(response);
        }

        var flight = _mapper.Map<Flight>(request.AddFlightRequest);
        if (_flightService.HasDuplicateFlight(flight))
        {
            response.ResultObject = new { ErrorMessage = "Duplicate flight." };
            response.Status = HttpStatusCode.Conflict;
            return Task.FromResult(response);
        }

        _flightService.CreateFlight(flight);

        response.ResultObject = _mapper.Map<FlightViewResponse>(flight);
        response.Status = HttpStatusCode.Created;
        return Task.FromResult(response);
    }
}