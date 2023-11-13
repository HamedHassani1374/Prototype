using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Persistance.Extensions
{
    public static class ConvertExtention
    {
        public static DateTime AsDateTimeShamsi(this string persianDate)
        {
            string[] pd = persianDate.Split('/');
            return new DateTime(int.Parse(pd[0]), int.Parse(pd[1]), int.Parse(pd[2]), new PersianCalendar());

        }

        public static DateTime AsDateTimeShamsi(string persianDate, string time)
        {
            string[] pd = persianDate.Split('/');
            string[] ts = time.Split(':');

            return new DateTime(int.Parse(pd[0]), int.Parse(pd[1]), int.Parse(pd[2]), int.Parse(ts[0]), int.Parse(ts[0]), 0, 0, new PersianCalendar());
        }
        public static string GetPersianTime(this DateTime dateTime)
        {
            System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();
            string result = persianCalendar.GetHour(dateTime) + ":" + persianCalendar.GetMinute(dateTime);
            return result;
        }
        public static string GetPersianDate(this DateTime dateTime)
        {
            System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();
            return $"{persianCalendar.GetYear(dateTime):0000}/{persianCalendar.GetMonth(dateTime):00}/{persianCalendar.GetDayOfMonth(dateTime):00}";
        }
        public static string ConvertToStandardPDate(this string pdate)
        {
            if (string.IsNullOrEmpty(pdate) && pdate.Length != 0)
                return pdate.Substring(0, 4) + '/' + pdate.Substring(4, 2) + '/' + pdate.Substring(6);
            else
                return pdate;
        }
    }
}
