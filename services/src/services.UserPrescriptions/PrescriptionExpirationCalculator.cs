using System;

namespace services.UserPrescriptions
{
    public static class PrescriptionExpirationCalculator
    {
        public static DateTime GetExpirationTimeUtc(int quantityAvailable, DateTime startTimeUtc, int occurrenceInDay)
        {
            var days = quantityAvailable / occurrenceInDay;
            return startTimeUtc.AddDays(days);
        }
    }
}
