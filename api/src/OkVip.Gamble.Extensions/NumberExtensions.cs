using System;
using System.Linq;
using System.Text;

namespace OkVip.Gamble
{
	public static class NumberExtensions
	{
		public static string CreateRandom(this object obj, int length = 10)
		{
			int count = 1;
			StringBuilder results = new StringBuilder();

			while (count <= length)
			{
				var guidstring = Guid.NewGuid().ToString("N");
				var getNumbers = (from t in guidstring
								  where char.IsDigit(t)
								  select t).LastOrDefault();

				results.Append(getNumbers.ToString());

				count++;
			}

			return results.ToString();
		}
	}
}
