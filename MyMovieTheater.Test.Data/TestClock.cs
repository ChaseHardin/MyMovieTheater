using System;
using System.Globalization;

namespace MyMovieTheater.Test.Data
{
    public class TestClock
    {
        private static DateTime _current = DateTime.MinValue;

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
                if (_current.Equals(DateTime.MinValue))
                {
                    _current = DateTime.ParseExact("1970-01-01 00:00:00", "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);
                }
                _current = _current.AddSeconds(1);
                return _current;
            }
        }

        public static DateTime Date(string yyyyMMdd)
        {
            var date = yyyyMMdd + " 00:00:00";
            return DateTime.SpecifyKind(DateTime.ParseExact(date, "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture),
                DateTimeKind.Utc);
        }
    }
}