using System;

namespace OkVip.Gamble
{
	public static class DateTimeExtensions
	{
		public static DateTime ToGmtTime(this DateTime dateTime)
		{
			return TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
		}

		public static long ToUnixTimestamp(this DateTime dateTime)
		{
			var timespan = dateTime - new DateTime(1970, 1, 1);
			return (long)timespan.TotalSeconds;
		}
	}
}