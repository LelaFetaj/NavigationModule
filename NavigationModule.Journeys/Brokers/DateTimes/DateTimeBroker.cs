namespace NavigationModule.Journeys.Brokers.DateTimes
{
    sealed class DateTimeBroker : IDateTimeBroker
    {
        public DateTimeOffset GetCurrentDateTime() =>
            DateTimeOffset.UtcNow;

        public DateOnly GetDateOnly() =>
            DateOnly.FromDateTime(DateTimeOffset.UtcNow.Date);
    }
}
