using System.Net;
using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Dtos;
using MediatR;

namespace FlightPlanner.UseCases.MediationFlights.GetFlight;

public class GetFlightQueryHandler : IRequestHandler<GetFlightQuery, ServiceResult>
{
    private readonly IFlightService _flightService;
    private readonly IMapper _mapper;

    public GetFlightQueryHandler(IFlightService flightService, IMapper mapper)
    {
        _flightService = flightService;
        _mapper = mapper;
    }

    public Task<ServiceResult> Handle(GetFlightQuery request, CancellationToken cancellationToken)
    {
        var flight = _flightService.GetFullFlightById(request.Id);

        var response = new ServiceResult();
        if (flight == null)
        {
            response.ResultObject = new { ErrorMessage = $"Flight {request.Id} does not exist." };
            response.Status = HttpStatusCode.NotFound;
            return Task.FromResult(response);
        }

        response.ResultObject = _mapper.Map<FlightViewResponse>(flight);
        response.Status = HttpStatusCode.OK;
        return Task.FromResult(response);
    }
}