#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\Utilities\ConvertUtility.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E511BB4CDBAE7425B5DA31370CCDDDFAF087FE40"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\Utilities\ConvertUtility.cs"
using System;

namespace SMSManager_API.Library.Utilities
{
	public class ConvertUtility
	{
		public static string FormatTimeVn(DateTime dt, string defaultText)
		{
			if (ToDateTime(dt) != new DateTime(1900, 1, 1))
				return dt.ToString("dd-mm-yy");
			else
				return defaultText;
		}
        public static double ToDouble1(string obj)
		{
			double retVal;
            try
			{
            obj = obj.Replace(",","").Replace(".", ",").Replace(" ","");
			
				retVal = Convert.ToDouble(obj);
			}
			catch
			{
				retVal = 0;
			}

			return retVal;
		}
		public static int ToInt32(object obj)
		{
			int retVal = 0;

			try
			{
				retVal = Convert.ToInt32(obj);
			}
			catch
			{
				retVal = 0;
			}

			return retVal;
		}

		public static int ToInt32(object obj, int defaultValue)
		{
			int retVal = defaultValue;

			try
			{
				retVal = Convert.ToInt32(obj);
			}
			catch
			{
				retVal = defaultValue;
			}

			return retVal;
		}

		public static string ToString(object obj)
		{
			string retVal;

			try
			{
				retVal = Convert.ToString(obj);
			}
			catch
			{
				retVal = String.Empty;
			}

			return retVal;
		}

		public static DateTime ToDateTime(object obj)
		{
			DateTime retVal;
			try
			{
				retVal = Convert.ToDateTime(obj);
			}
			catch
			{
				retVal = DateTime.Now;
			}
			if (retVal == new DateTime(1, 1, 1)) return DateTime.Now;

			return retVal;
		}

		public static DateTime ToDateTime(object obj, DateTime defaultValue)
		{
			DateTime retVal;
			try
			{
				retVal = Convert.ToDateTime(obj);
			}
			catch
			{
				retVal = DateTime.Now;
			}
			if (retVal == new DateTime(1, 1, 1)) return defaultValue;

			return retVal;
		}

		public static bool ToBoolean(object obj)
		{
			bool retVal;

			try
			{
				retVal = Convert.ToBoolean(obj);
			}
			catch
			{
				retVal = false;
			}

			return retVal;
		}

		public static double ToDouble(object obj)
		{
			double retVal;

			try
			{
				retVal = Convert.ToDouble(obj);
			}
			catch
			{
				retVal = 0;
			}

			return retVal;
		}

		public static double ToDouble(object obj, double defaultValue)
		{
			double retVal;

			try
			{
				retVal = Convert.ToDouble(obj);
			}
			catch
			{
				retVal = defaultValue;
			}

			return retVal;
		}

        
	}
}

#line default
#line hidden
