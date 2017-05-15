#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\AppEnv.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "734E83D6504531BFACE9F432C327E6F583D7B63F"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\AppEnv.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using S2VNM;
using WS_Music.Library;
using vn.thanhnu;

/// <summary>
/// Summary description for AppEnv
/// </summary>
public class AppEnv
{
	
    static readonly S2Vnm S2Vnm = new S2Vnm();

    public static bool CheckMbCode(string input)
    {
        string code = AppEnv.GetSetting("MB_Code");
        string[] codes = code.Split(',');

        bool re = false;

        foreach (var s in codes)
        {
            if (s == input.ToUpper())
                re = true;
        }

        return re;
    }

    public static string ThanhNuDangKy(string userId)
    {
        vn.thanhnu.Service thanhnuService = new vn.thanhnu.Service();
        string value = thanhnuService.smsKichHoat(userId);

        return value;

    }

    public static string ThanhNuHuy(string userId)
    {
        var thanhnuService = new Service();

        string value = thanhnuService.smsHuy(userId);

        return value;
    }

    public static string RegisterService(string shortCode, string requestId, string msisdn, string commandcode, string message)
    {
        if (GetSetting("TestFlag") == "0")
        {
            string reString = S2Vnm.SyncSubWapVnmData(shortCode, requestId, msisdn, commandcode, message);

            return reString;
        }

        return "1|Thanh cong";
    }

    public static string DeleteService94X(string shortCode, string requestId, string msisdn, string commandcode, string message)
    {
        if (GetSetting("TestFlag") == "0")
        {
            string value = S2Vnm.SyncSubWapVnmData(shortCode, requestId, msisdn, commandcode, message);//ANDY Service S2_94x DELETE
            string[] res = value.Split('|');
            if (res.Length > 0)
            {
                if (res[0] == "1")//DK THANH CONG
                {
                    return "1";
                }
                return "0";
            }
        }

        return "1|Success";
    }

    public static string RegisterService94X(string shortCode, string requestId, string msisdn, string commandcode, string message)
    {
        if (GetSetting("TestFlag") == "0")
        {
            string value = S2Vnm.RegisterService(shortCode, requestId, msisdn, commandcode, message);//ANDY Service S2_94x
            string[] res = value.Split('|');
            if (res.Length > 0)
            {
                if (res[0] == "1")//DK THANH CONG
                {
                    return "1";
                }

                if (res[1].Trim() == "DoubleRegister")
                {
                    return "2";
                }

                return "0";
            }
        }

        return "1|Success";
    }

    public static string DeleteService(string shortCode,string requestId,string msisdn,string commandCode,string message)
    {
        if(GetSetting("TestFlag")=="0")
        {
            string reString = S2Vnm.SyncSubWapVnmData(shortCode, requestId, msisdn, commandCode, message);

            return reString;
        }

        return "1|Thanh cong";
    }

    public static string ConnectionStringLuckyFone
    {
        get { return WebConfigurationManager.ConnectionStrings["localsqlLkf"].ConnectionString; }
    }

    public static string ConnectionString
    {
        get { return WebConfigurationManager.ConnectionStrings["localsql"].ConnectionString; }
    }

     public static string ConnectionStringVClip
    {
        get { return WebConfigurationManager.ConnectionStrings["localsqlVClip"].ConnectionString; }
    }

     public static string ConnectionStringTtndService
     {
         get { return WebConfigurationManager.ConnectionStrings["localsqlttndservices"].ConnectionString; }
     }

     public static string ConnectionStringVms
     {
         get { return WebConfigurationManager.ConnectionStrings["localsqlVms"].ConnectionString; }
     }
    

    public static string GetConnectionString(string name)
    {
        return WebConfigurationManager.ConnectionStrings[name].ConnectionString;
    }
                 
    public static string GetSetting(string key)
    {
        return WebConfigurationManager.AppSettings[key];
    }

    public static double ConvertToTimestamp(DateTime value)
    {
        //create Timespan by subtracting the value provided from
        //the Unix Epoch
        TimeSpan span = (value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());

        //return the total seconds (which is a UNIX timestamp)
        return (double)span.TotalSeconds;
    }

    public static string MD5Encrypt(string plainText)
    {
        byte[] data, output;
        UTF8Encoding encoder = new UTF8Encoding();
        MD5CryptoServiceProvider hasher = new MD5CryptoServiceProvider();

        data = encoder.GetBytes(plainText);
        output = hasher.ComputeHash(data);

        return BitConverter.ToString(output).Replace("-", "").ToLower();
    }

    public static void SendMtVmgPortal(string userId, string serviceId, string commandCode, string message)
    {
        var mtInfo = new VoteSmsMtInfo();
        var random = new Random();
        mtInfo.User_ID = userId;
        mtInfo.Service_ID = serviceId;
        mtInfo.Command_Code = commandCode;
        mtInfo.Message_Type = (int)Constant.MessageType.NoCharge;
        mtInfo.Request_ID = random.Next(100000000, 999999999).ToString();
        mtInfo.Total_Message = 1;
        mtInfo.Message_Index = 0;
        mtInfo.IsMore = 0;
        mtInfo.Content_Type = 0;
        mtInfo.Message = message;
        mtInfo.PartnerId = "";
        mtInfo.Operator = "vnmobile";

        ViSport_S2_Registered_UsersController.SmsMtInsertVmgPortal(mtInfo);
    }

}

#line default
#line hidden
