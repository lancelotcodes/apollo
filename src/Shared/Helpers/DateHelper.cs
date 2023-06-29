using System;

namespace Shared.Helpers
{
    public static class DateHelper
    {
        public static DateTime? ToValidDate(this DateTime? dateTime)
        {
            return dateTime == null || dateTime.Value.Year < 1990 ? null : dateTime.Value;
        }
    }
}
