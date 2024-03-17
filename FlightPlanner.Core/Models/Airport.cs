﻿namespace FlightPlanner.Core.Models;

public class Airport : Entity
{
    public required string Country { get; set; }
    public required string City { get; set; }
    public required string AirportCode { get; set; }
}