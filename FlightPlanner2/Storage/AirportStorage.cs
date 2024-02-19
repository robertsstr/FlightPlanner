using FlightPlanner.Models;

namespace FlightPlanner.Storage
{
    public class AirportStorage
    {
        private static readonly Dictionary<Airport, int> _airportsUsage = new Dictionary<Airport, int>();

        public static void AddAirport(Airport departure, Airport arrival)
        {
            {
                if (!_airportsUsage.ContainsKey(departure))
                {
                    _airportsUsage[departure] = 1;
                }
                else
                {
                    _airportsUsage[departure]++;
                }

                if (!_airportsUsage.ContainsKey(arrival))
                {
                    _airportsUsage[arrival] = 1;
                }
                else
                {
                    _airportsUsage[arrival]++;
                }
            }
        }

        public static List<Airport>? GetAirport(string search)
        {
            string trimmedSearch = search.Trim();

            return _airportsUsage.Keys.Where(airport =>
                    airport.AirportCode.Contains(trimmedSearch, StringComparison.OrdinalIgnoreCase) ||
                    airport.City.Contains(trimmedSearch, StringComparison.OrdinalIgnoreCase) ||
                    airport.Country.Contains(trimmedSearch, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public static void RemoveAirportUsage(Airport airport)
        {
            if (_airportsUsage.ContainsKey(airport))
            {
                _airportsUsage[airport]--;
                if (_airportsUsage[airport] <= 0)
                {
                    _airportsUsage.Remove(airport);
                }
            }
        }

        public static void Clear()
        {
            _airportsUsage.Clear();
        }
    }
}
