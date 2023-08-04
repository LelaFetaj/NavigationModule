namespace NavigationModule.Authentication.Brokers.DateTimes
{
    sealed class DateTimeBroker : IDateTimeBroker
    {
        public DateTimeOffset GetCurrentDateTime() =>
            DateTimeOffset.UtcNow;
    }
}
