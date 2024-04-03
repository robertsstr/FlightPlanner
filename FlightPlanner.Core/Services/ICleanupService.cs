namespace FlightPlanner.Core.Services;

public interface ICleanupService : IDbService
{
    void Cleanup();
}