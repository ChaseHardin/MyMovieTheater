using System;
using System.Globalization;

namespace MyMovieTheater.Test.Data
{
    public class TestClock
    {
        private static DateTime _curent = DateTime.MaxValue;

        public DateTime CurrentTime
        {
            get { return Now; }
        }

        public DateTime CurrentIfDefault(DateTime value)
        {
            return Now;
        }

        public static DateTime Now
        {
            get
            {
                if (_curent.Equals(DateTime.MinValue))
                {
                    _curent = DateTime.ParseExact("1970-01-01 00:00:00", "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);
                }

                _curent = _curent.AddSeconds(1);
                return _curent;
            }
        }

        public static DateTime Date(string yyyMMdd)
        {
            var date = yyyMMdd + " 00:00:00";
            return DateTime.SpecifyKind(DateTime.ParseExact(date, "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture), DateTimeKind.Utc);
        }
    }
}