namespace FlightPlanner.Core.Semaphore
{
    public static class SemaphoreUtility
    {
        public static readonly SemaphoreSlim SharedSemaphore = new(1, 1);
    }
}
