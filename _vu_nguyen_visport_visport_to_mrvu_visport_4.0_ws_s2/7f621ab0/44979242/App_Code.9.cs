#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\Utilities\RegexUtility.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "269D37D28832E53005667F99B55319495A38A58E"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\Utilities\RegexUtility.cs"
using System;
using System.Text.RegularExpressions;

namespace SMSManager_API.Library.Utilities
{
    public class RegexUtility
    {
        public static bool IsAlphaNumeric(String strToCheck)
        {
            var objAlphaNumericPattern = new Regex("[^a-zA-Z0-9 ]");
            return !objAlphaNumericPattern.IsMatch(strToCheck);
        }
        // Function to test for Positive Integers.
        public static bool IsNaturalNumber(String strNumber)
        {
            Regex objNotNaturalPattern = new Regex("[^0-9]");
            Regex objNaturalPattern = new Regex("0*[1-9][0-9]*");
            return !objNotNaturalPattern.IsMatch(strNumber) &&
            objNaturalPattern.IsMatch(strNumber);
        }
        // Function to test for Positive Integers with zero inclusive
        public static bool IsWholeNumber(String strNumber)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strNumber);
        }
        // Function to Test for Integers both Positive & Negative
        public static bool IsInteger(String strNumber)
        {
            Regex objNotIntPattern = new Regex("[^0-9-]");
            Regex objIntPattern = new Regex("^-[0-9]+$|^[0-9]+$");
            return !objNotIntPattern.IsMatch(strNumber) && objIntPattern.IsMatch(strNumber);
        }
        // Function to Test for Positive Number both Integer & Real
        public static bool IsPositiveNumber(String strNumber)
        {
            Regex objNotPositivePattern = new Regex("[^0-9.]");
            Regex objPositivePattern = new Regex("^[.][0-9]+$|[0-9]*[.]*[0-9]+$");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            return !objNotPositivePattern.IsMatch(strNumber) &&
            objPositivePattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber);
        }
        // Function to test whether the string is valid number or not
        public static bool IsNumber(String strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
            String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");
            return !objNotNumberPattern.IsMatch(strNumber) &&
            !objTwoDotPattern.IsMatch(strNumber) &&
            !objTwoMinusPattern.IsMatch(strNumber) &&
            objNumberPattern.IsMatch(strNumber);
        }
        // Function To test for Alphabets.
        public static bool IsAlpha(String strToCheck)
        {
            Regex objAlphaPattern = new Regex("[^a-zA-Z]");
            return !objAlphaPattern.IsMatch(strToCheck);
        }

        public static bool IsEmail(string email)
        {
            const string matchEmailPattern =
                @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
            if (email != null)
            {
                return Regex.IsMatch(email, matchEmailPattern);
            }

            return false;
        }

        public static bool HasAlphabetAndNumeric(string strToCheck)
        {
            const string matchEmailPattern = @"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,20})$";

            if (strToCheck != null)
            {
                return Regex.IsMatch(strToCheck, matchEmailPattern,RegexOptions.IgnorePatternWhitespace);
            }

            return false;
        }

        
    }
}


#line default
#line hidden
