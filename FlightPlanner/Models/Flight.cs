﻿using System.Text.Json.Serialization;

namespace FlightPlanner.Models
{
    public class Flight
    {
        public int Id { get; set; }
        [JsonIgnore]
        public int FromId { get; set; }
        [JsonIgnore]
        public int ToId { get; set; }
        public Airport From { get; set; }
        public Airport To { get; set; }
        public string Carrier { get; set; }
        public string DepartureTime { get; set; }
        public string ArrivalTime { get; set; }
    }
}
