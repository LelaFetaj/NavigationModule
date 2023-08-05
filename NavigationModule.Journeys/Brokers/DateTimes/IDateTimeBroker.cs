namespace NavigationModule.Journeys.Brokers.DateTimes
{
    public interface IDateTimeBroker
    {
        DateTimeOffset GetCurrentDateTime();
        DateOnly GetDateOnly();
    }
}
