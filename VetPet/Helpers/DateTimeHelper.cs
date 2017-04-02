using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VetPet.Helpers
{
    public class DateTimeHelper
    {
        public static string GetDateFromDatetime(DateTime? datetime)
        {
            if (!datetime.HasValue) return string.Empty;
            return datetime.Value.ToShortDateString();
        }

        public static string GetTimeFromDatetime(DateTime? datetime)
        {
            if (!datetime.HasValue) return string.Empty;
            return datetime.Value.ToShortTimeString();
        }
    }
}