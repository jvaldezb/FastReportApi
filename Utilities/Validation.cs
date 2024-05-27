using System;
using System.Text.RegularExpressions;

namespace Utilities
{
	public class Validation
	{
		public static bool isDecimal(object test_number)
		{
			try
			{
				decimal num = Convert.ToDecimal(test_number);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool isInteger(object test_number)
		{
			try
			{
				long num = Convert.ToInt64(test_number);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool isBoolean(object test_number)
		{
			try
			{
				bool num = Convert.ToBoolean(test_number);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public static bool ValidateMail(string mail)
		{
			Regex regex = new Regex("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$");
			Match match = regex.Match(mail);
			if (match.Success)
			{
				return true;
			}
			return false;
		}

		public static decimal Coalesce(string num, decimal value)
        {
			return isDecimal(num) ? Convert.ToDecimal(num) : value;
        }
	}
}
