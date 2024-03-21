using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Dtos;
using MediatR;

namespace FlightPlanner.UseCases.MediationFlights.DeleteFlight;

public class DeleteFlightCommandHandler : IRequestHandler<DeleteFlightCommand, ServiceResult>
{
    private readonly IFlightService _flightService;
    private readonly IAirportService _airportService;

    public DeleteFlightCommandHandler(IFlightService flightService, IAirportService airportService)
    {
        _flightService = flightService;
        _airportService = airportService;
    }

    public Task<ServiceResult> Handle(DeleteFlightCommand request, CancellationToken cancellationToken)
    {
        {
            var flight = _flightService.GetById(request.Id);
            if (flight != null)
            {
                _flightService.Delete(flight);
                _airportService.DeleteUnusedAirports();
            }

            return Task.FromResult(new ServiceResult());
        }
    }
}