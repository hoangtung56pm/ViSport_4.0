using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace VNM_ViSport_Charging.Library
{
    public static class SecurityUtils
    {
        public static string MD5Encrypt(string plainText)
        {
            byte[] data, output;
            UTF8Encoding encoder = new UTF8Encoding();
            MD5CryptoServiceProvider hasher = new MD5CryptoServiceProvider();

            data = encoder.GetBytes(plainText);
            output = hasher.ComputeHash(data);

            return BitConverter.ToString(output).Replace("-", "").ToLower();
        }
        public static string SHA1Encrypt(string plainTextString)
        {
            UTF8Encoding enc = new UTF8Encoding();

            SHA1 sha = new SHA1CryptoServiceProvider();

            byte[] shaHash = sha.ComputeHash(
                enc.GetBytes(plainTextString));

            return Convert.ToBase64String(shaHash);
        }
        public static string GetMD5Hash(string input)
        {
            MD5CryptoServiceProvider hasher = new MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = hasher.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string password = s.ToString();
            return password;
        }

    }
}
