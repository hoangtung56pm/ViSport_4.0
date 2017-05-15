#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\Utilities\UnicodeUtility.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "757EA73AEAA0CC1CF90A98B0733D4FCC7E026B60"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\Utilities\UnicodeUtility.cs"
using System;
using System.Collections;
using System.Text;

namespace SMSManager_API.Library.Utilities
{

    /**
     * \brief Provides services to translate ASCII (ISO-8859-1 / Latin 1) charset
     *        into GSM 03.38 character set
     */
    public class Gsm0338
    {
        /**
         * \brief Escape byte for the extended ISO
         */
        private static readonly Int16 ESC_BYTE = (Int16)27;

        /**
         * Mappings from GSM 03.38 to Unicode as per
         * http://unicode.org/Public/MAPPINGS/ETSI/GSM0338.TXT
         */
        private static readonly int[] gsmUtfMap = { 0x0040, 0x00A3, 0x0024, 0x00A5,
			0x00E8, 0x00E9, 0x00F9, 0x00EC, 0x00F2, 0x00E7, 0x000A, 0x00D8,
			0x00F8, 0x000D, 0x00C5, 0x00E5, 0x0394, 0x005F, 0x03A6, 0x0393,
			0x039B, 0x03A9, 0x03A0, 0x03A8, 0x03A3, 0x0398, 0x039E, 0x00A0,
			0x00C6, 0x00E6, 0x00DF, 0x00C9, 0x0020, 0x0021, 0x0022, 0x0023,
			0x00A4, 0x0025, 0x0026, 0x0027, 0x0028, 0x0029, 0x002A, 0x002B,
			0x002C, 0x002D, 0x002E, 0x002F, 0x0030, 0x0031, 0x0032, 0x0033,
			0x0034, 0x0035, 0x0036, 0x0037, 0x0038, 0x0039, 0x003A, 0x003B,
			0x003C, 0x003D, 0x003E, 0x003F, 0x00A1, 0x0041, 0x0042, 0x0043,
			0x0044, 0x0045, 0x0046, 0x0047, 0x0048, 0x0049, 0x004A, 0x004B,
			0x004C, 0x004D, 0x004E, 0x004F, 0x0050, 0x0051, 0x0052, 0x0053,
			0x0054, 0x0055, 0x0056, 0x0057, 0x0058, 0x0059, 0x005A, 0x00C4,
			0x00D6, 0x00D1, 0x00DC, 0x00A7, 0x00BF, 0x0061, 0x0062, 0x0063,
			0x0064, 0x0065, 0x0066, 0x0067, 0x0068, 0x0069, 0x006A, 0x006B,
			0x006C, 0x006D, 0x006E, 0x006F, 0x0070, 0x0071, 0x0072, 0x0073,
			0x0074, 0x0075, 0x0076, 0x0077, 0x0078, 0x0079, 0x007A, 0x00E4,
			0x00F6, 0x00F1, 0x00FC, 0x00E0 };

        /**
         * Extended GSM 03.38 to Unicode characters as per
         * http://unicode.org/Public/MAPPINGS/ETSI/GSM0338.TXT
         */
        private static int[][] extGsmUtfMap =  {
                                                                 new int[] { 0x0A, 0x000C }, new int[] { 0x14, 0x005E }, new int[] { 0x28, 0x007B }, new int[] { 0x29, 0x007D }, new int[] { 0x2F, 0x005C }, new int[] { 0x3C, 0x005B }, new int[] { 0x3D, 0x007E }, new int[] { 0x3E, 0x005D }, new int[] { 0x40, 0x007C }, new int[] { 0x65, 0x20AC }
                                               };

        public static bool isEncodeableInGsm0338(string utfString)
        {
            char[] utfChars = utfString.ToCharArray();
            int count = 0;
            for (int i = 0; i < utfChars.Length; i++)
            {
                int isExisted = 0;
                for (int j = 0; j < gsmUtfMap.Length; j++)
                {
                    if (gsmUtfMap[j] == utfChars[i])
                    {
                        count++;
                        isExisted = 1;
                        break;
                    }
                }
                if (isExisted == 0)
                {
                    for (int j = 0; j < extGsmUtfMap.Length; j++)
                    {
                        if (extGsmUtfMap[j][1] == utfChars[i])
                        {
                            count++;
                            break;
                        }
                    }
                }

            }
            if (count == utfChars.Length)
            {
                return true;
            }
            return false;
        }

    }

    public class UnicodeUtility
    {
        private const string uniChars =
            "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";
        private const string uniCharsNew = "áảãạâầấẩẫậăằắẳẵặẻẽẹêềếểễệđíỉĩịóỏõọôồốổỗộơờớởỡợúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ…";
        private const string validCharacter = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 ";
        private const string KoDauChars =
            "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";

        public static int UnicodeToUTF8(byte[] dest, int maxDestBytes, string source, int sourceChars)
        {
            int i, count;
            int c, result;

            result = 0;
            if ((source != null && source.Length == 0))
                return result;
            count = 0;
            i = 0;
            if (dest != null)
            {
                while ((i < sourceChars) && (count < maxDestBytes))
                {
                    c = (int)source[i++];
                    if (c <= 0x7F)
                        dest[count++] = (byte)c;
                    else if (c > 0x7FF)
                    {
                        if ((count + 3) > maxDestBytes)
                            break;
                        dest[count++] = (byte)(0xE0 | (c >> 12));
                        dest[count++] = (byte)(0x80 | ((c >> 6) & 0x3F));
                        dest[count++] = (byte)(0x80 | (c & 0x3F));
                    }
                    else
                    {
                        //  0x7F < source[i] <= 0x7FF
                        if ((count + 2) > maxDestBytes)
                            break;
                        dest[count++] = (byte)(0xC0 | (c >> 6));
                        dest[count++] = (byte)(0x80 | (c & 0x3F));
                    }
                }
                if (count >= maxDestBytes)
                    count = maxDestBytes - 1;
                dest[count] = (byte)(0);
            }
            else
            {
                while (i < sourceChars)
                {
                    c = (int)(source[i++]);
                    if (c > 0x7F)
                    {
                        if (c > 0x7FF)
                            count++;
                        count++;
                    }
                    count++;
                }
            }
            result = count + 1;
            return result;
        }


        public static int UTF8ToUnicode(char[] dest, int maxDestChars, byte[] source, int sourceBytes)
        {
            int i, count;
            int c, result;
            int wc;

            if (source == null)
            {
                result = 0;
                return result;
            }
            result = (int)(-1);
            count = 0;
            i = 0;
            if (dest != null)
            {
                while ((i < sourceBytes) && (count < maxDestChars))
                {
                    wc = (int)(source[i++]);
                    if ((wc & 0x80) != 0)
                    {
                        if (i >= sourceBytes)
                            return result;
                        wc = wc & 0x3F;
                        if ((wc & 0x20) != 0)
                        {
                            c = (byte)(source[i++]);
                            if ((c & 0xC0) != 0x80)
                                return result;
                            if (i >= sourceBytes)
                                return result;
                            wc = (wc << 6) | (c & 0x3F);
                        }
                        c = (byte)(source[i++]);
                        if ((c & 0xC0) != 0x80)
                            return result;
                        dest[count] = (char)((wc << 6) | (c & 0x3F));
                    }
                    else
                        dest[count] = (char)wc;
                    count++;
                }
                if (count > maxDestChars)
                    count = maxDestChars - 1;
                dest[count] = (char)(0);
            }
            else
            {
                while (i < sourceBytes)
                {
                    c = (byte)(source[i++]);
                    if ((c & 0x80) != 0)
                    {
                        if (i >= sourceBytes)
                            return result;
                        c = c & 0x3F;
                        if ((c & 0x20) != 0)
                        {
                            c = (byte)(source[i++]);
                            if ((c & 0xC0) != 0x80)
                                return result;
                            if (i >= sourceBytes)
                                return result;
                        }
                        c = (byte)(source[i++]);
                        if ((c & 0xC0) != 0x80)
                            return result;
                    }
                    count++;
                }
            }
            result = count + 1;
            return result;
        }


        public static byte[] UTF8Encode(string ws)
        {
            int l;
            byte[] temp, result;

            result = null;
            if ((ws != null && ws.Length == 0))
                return result;
            temp = new byte[ws.Length * 3];
            l = UnicodeToUTF8(temp, temp.Length + 1, ws, ws.Length);
            if (l > 0)
            {
                result = new byte[l - 1];
                Array.Copy(temp, 0, result, 0, l - 1);
            }
            else
            {
                result = new byte[ws.Length];
                for (int i = 0; i < result.Length; i++)
                    result[i] = (byte)(ws[i]);
            }
            return result;
        }


        public static string UTF8Decode(byte[] s)
        {
            int l;
            char[] temp;
            string result;

            result = String.Empty;
            if (s == null)
                return result;
            temp = new char[s.Length + 1];
            l = UTF8ToUnicode(temp, temp.Length, s, s.Length);
            if (l > 0)
            {
                result = "";
                for (int i = 0; i < l - 1; i++)
                    result += temp[i];
            }
            else
            {
                result = "";
                for (int i = 0; i < s.Length; i++)
                    result += (char)(s[i]);
            }
            return result;
        }

        public static string RemoveSpecialCharacter(string orig)
        {
            string rv;

            // replacing with space allows the camelcase to work a little better in most cases.
            rv = orig.Replace("\\", " ");
            rv = rv.Replace("(", " ");
            rv = rv.Replace(")", " ");
            rv = rv.Replace("/", " ");
            //rv = rv.Replace("-", " ");
            rv = rv.Replace(",", " ");
            rv = rv.Replace(">", " ");
            rv = rv.Replace("<", " ");
            rv = rv.Replace("&", " ");
            rv = rv.Replace("!", " ");
            rv = rv.Replace("@", " ");
            rv = rv.Replace("#", " ");
            rv = rv.Replace("$", " ");
            rv = rv.Replace("%", " ");
            rv = rv.Replace("^", " ");
            rv = rv.Replace("*", " ");
            rv = rv.Replace("+", "__");
            rv = rv.Replace("|", " ");
            rv = rv.Replace("[", " ");
            rv = rv.Replace("]", " ");
            rv = rv.Replace("{", " ");
            rv = rv.Replace("}", " ");
            rv = rv.Replace(":", " ");
            rv = rv.Replace(";", " ");
            rv = rv.Replace("?", " ");
            rv = rv.Replace("~", " ");
            rv = rv.Replace(",", " ");
            //rv = rv.Replace(".", " ");
            rv = rv.Replace("\"", "");
            // single quotes shouldn't result in CamelCase variables like Patient's -> PatientS
            // "smart" forward quote
            rv = rv.Replace("'", "");

            // make sure to get rid of double spaces.
            rv = rv.Replace("   ", " ");
            rv = rv.Replace("  ", " ");

            rv = rv.Trim(' '); // Remove leading and trailing spaces.

            return (rv);
        }

        public static string UnicodeToKoDau(string s)
        {
            string retVal = String.Empty;
            if (s == null)
                return retVal;
            int pos;
            for (int i = 0; i < s.Length; i++)
            {
                pos = uniChars.IndexOf(s[i].ToString());
                if (pos >= 0)
                    retVal += KoDauChars[pos];
                else
                    retVal += s[i];
            }
            return retVal;
        }
        public static bool CheckExistedUnicode(string s)
        {
            if (Gsm0338.isEncodeableInGsm0338(s))
            {
                return false;
            }
            return true;
            //bool retVal = false;
            //if (s == null)
            //    return retVal;
            //int pos;
            //for (int i = 0; i < s.Length; i++)
            //{
            //    pos = uniCharsNew.IndexOf(s[i].ToString());
            //    if (pos >= 0)
            //    {
            //        retVal = true;
            //        break;
            //    }
            //}
            //return retVal;
        }
        public static bool CheckExistedSpecialCharacter(string s)
        {
            bool retVal = false;
            if (s == null)
                return retVal;
            int pos;
            for (int i = 0; i < s.Length; i++)
            {
                pos = validCharacter.IndexOf(s[i].ToString());
                if (pos >= 0)
                {
                    retVal = true;
                }
                else
                {
                    retVal = false;
                    break;
                }
            }
            return retVal;
        }
        public static string UnicodeToWindows1252(string s)
        {
            string retVal = String.Empty;
            for (int i = 0; i < s.Length; i++)
            {
                int ord = (int)s[i];
                if (ord > 191)
                    retVal += "&#" + ord.ToString() + ";";
                else
                    retVal += s[i];
            }
            return retVal;
        }

        public static string UnicodeToISO8859(string src)
        {
            Encoding iso = Encoding.GetEncoding("iso8859-1");
            Encoding unicode = Encoding.UTF8;
            byte[] unicodeBytes = unicode.GetBytes(src);
            return iso.GetString(unicodeBytes);
        }

        public static string ISO8859ToUnicode(string src)
        {
            Encoding iso = Encoding.GetEncoding("iso8859-1");
            Encoding unicode = Encoding.UTF8;
            byte[] isoBytes = iso.GetBytes(src);
            return unicode.GetString(isoBytes);
        }
    }
}

#line default
#line hidden
