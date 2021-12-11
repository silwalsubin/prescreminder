using services.UserPrescriptions.Domain;
using services.UserPrescriptions.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeZoneConverter;

namespace services.UserPrescriptions
{
    public static class PrescriptionExpirationCalculator
    {
        public static DateTime GetExpirationTimeUtc(this UserPrescriptionsTableSchema.UserPrescriptionRecord record, string timeZone, List<TimeOfDay> timeOfDays)
        {
            var timeZoneInfo = TZConvert.GetTimeZoneInfo(timeZone);
            var localFromDate = record.StartDateUtc > record.ModifiedDateUtc
                ? record.StartDateUtc.Add(timeZoneInfo.BaseUtcOffset)
                : record.ModifiedDateUtc.Add(timeZoneInfo.BaseUtcOffset);

            for (int i = 0; i < record.TotalQuantity; i++)
            {
                localFromDate = localFromDate.IncrementTime(timeOfDays);
            }

            return localFromDate.Subtract(timeZoneInfo.BaseUtcOffset);
        }

        public static int QuantityRemaining(this UserPrescriptionsTableSchema.UserPrescriptionRecord record, string timeZone, List<TimeOfDay> timeOfDays)
        {
            var timeZoneInfo = TZConvert.GetTimeZoneInfo(timeZone);
            var localFromDate = record.StartDateUtc > record.ModifiedDateUtc
                                ? record.StartDateUtc.Add(timeZoneInfo.BaseUtcOffset)
                                : record.ModifiedDateUtc.Add(timeZoneInfo.BaseUtcOffset);
            var localTimeNow = DateTime.UtcNow.Add(timeZoneInfo.BaseUtcOffset);

            int quantitySpent = 0;
            localFromDate = localFromDate.IncrementTime(timeOfDays);
            while (localFromDate < localTimeNow)
            {
                localFromDate = localFromDate.IncrementTime(timeOfDays);
                quantitySpent++;
            }

            return quantitySpent >= record.TotalQuantity ? 0 : record.TotalQuantity - quantitySpent;
        }

        private static DateTime IncrementTime(this DateTime dateTime, List<TimeOfDay> timeOfDays)
        {
            var timeOfDaysOrdered = timeOfDays.OrderBy(x => new TimeSpan(x.Hour, x.Minute, 0)).ToList();
            foreach (var timeOfDay in timeOfDaysOrdered)
            {
                if (new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second) < new TimeSpan(timeOfDay.Hour, timeOfDay.Minute, 0))
                {
                    return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, timeOfDay.Hour, timeOfDay.Minute, 0);
                }
            }

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, timeOfDaysOrdered[0].Hour, timeOfDaysOrdered[0].Minute, 0).AddDays(1);
        }
    }
}
