using System.Net;
using AutoMapper;
using FlightPlanner.Core.Services;
using FlightPlanner.UseCases.Dtos;
using MediatR;

namespace FlightPlanner.UseCases.MediationAirport.SearchAirport;

public class SearchAirportQueryHandler : IRequestHandler<SearchAirportQuery, ServiceResult>
{
    private readonly IAirportService _airportService;
    private readonly IMapper _mapper;

    public SearchAirportQueryHandler(IAirportService airportService, IMapper mapper)
    {
        _airportService = airportService;
        _mapper = mapper;
    }

    public Task<ServiceResult> Handle(SearchAirportQuery request, CancellationToken cancellationToken)
    {
        var airports = _airportService.GetAirportByKeyword(request.Search);

        return Task.FromResult(new ServiceResult()
        {
            ResultObject = _mapper.Map<List<AirportViewModel>>(airports),
            Status = HttpStatusCode.OK
        });
    }
}