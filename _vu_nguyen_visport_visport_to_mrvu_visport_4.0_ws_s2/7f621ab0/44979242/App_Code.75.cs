﻿#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\VclipNotification.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0C962F1D91FC02C78CABAF32358BCA7960679DC3"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\VclipNotification.cs"
using System;
using System.Web.Services;
using SMSManager_API.Library.Utilities;
using SentMT;
using WS_Music.Library;

/// <summary>
/// Summary description for VclipNotification
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class VclipNotification : System.Web.Services.WebService, IChargingNotificationSoap
{

    public VclipNotification () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog log = log4net.LogManager.GetLogger(typeof(VclipNotification));

    public string NotifyChargingInfo(string registeredId, string userId, string requestId, string serviceId, string serviceType, string chargingValue, string chargingAccount, string chargingTime, string chargingResponse)
    {

        log.Info(" ");
        log.Info("***** LOG VClip CHARGED NOTIFICATION From ANDY *****");

        log.Info("User_ID : " + userId);
        log.Info("chargingValue : " + chargingValue);
        log.Info("chargingAccount : " + chargingAccount);
        log.Info("chargingTime : " + chargingTime);
        log.Info("chargingResponse : " + chargingResponse);

        log.Info("****************************************");
        log.Info(" ");

        if (chargingResponse.Trim() == "1")//CHARGED THANH CONG
        {

            #region Log Doanh Thu

            var logInfo = new ViSport_S2_Charged_Users_LogInfo();

            logInfo.ID = ConvertUtility.ToInt32(registeredId);
            logInfo.User_ID = userId;
            logInfo.Request_ID = requestId;
            logInfo.Service_ID = serviceId;
            logInfo.Command_Code = "CLIP";
            logInfo.Service_Type = ConvertUtility.ToInt32(serviceType);
            logInfo.Charging_Count = 0;
            logInfo.FailedChargingTimes = 0;
            logInfo.RegisteredTime = DateTime.Now;
            logInfo.ExpiredTime = DateTime.Now.AddDays(1);
            logInfo.Registration_Channel = "SMS";
            logInfo.Status = 1;
            logInfo.Operator = "vnmobile";
            logInfo.Price = ConvertUtility.ToInt32(chargingValue);
            logInfo.Reason = "Succ";

            ViSport_S2_Registered_UsersController.VClipInsertLog(logInfo);

            #endregion

        }
        else //CHARGED THAT BAI
        {
            #region Log Doanh Thu

            var logInfo = new ViSport_S2_Charged_Users_LogInfo();

            logInfo.ID = ConvertUtility.ToInt32(registeredId);
            logInfo.User_ID = userId;
            logInfo.Request_ID = requestId;
            logInfo.Service_ID = serviceId;
            logInfo.Command_Code = "CLIP";
            logInfo.Service_Type = ConvertUtility.ToInt32(serviceType);
            logInfo.Charging_Count = 0;
            logInfo.FailedChargingTimes = 0;
            logInfo.RegisteredTime = DateTime.Now;
            logInfo.ExpiredTime = DateTime.Now.AddDays(1);
            logInfo.Registration_Channel = "SMS";
            logInfo.Status = 1;
            logInfo.Operator = "vnmobile";
            logInfo.Price = ConvertUtility.ToInt32(chargingValue);
            logInfo.Reason = chargingResponse;

            ViSport_S2_Registered_UsersController.VClipInsertLog(logInfo);

            #endregion
        }

        if (chargingResponse.Trim() == "1")
        {
            return "1";

            #region Gui MT cho khach hang thong bao gia han thanh cong

            var objSentMt = new ServiceProviderService();
            const int msgType = (int)Constant.MessageType.NoCharge;

            string message = "(092)Quy khach da gia han thanh cong DV VMclip cua Vietnamobile. Moi ban truy cap: http://kho-clip.com/" + userId + ".aspx de xem cac video HOT cap nhat 24/24 MIEN PHI. De huy DK, soan CLIP OFF gui 949. HT 19001255";

            const string commandCode = "CLIP";
            int value = objSentMt.sendMT(userId, message, "949", commandCode, msgType.ToString(), requestId, "1", "1", "0", "0");

            var objMt = new ViSport_S2_SMS_MTInfo();
            objMt.User_ID = userId;
            objMt.Message = message;
            objMt.Service_ID = serviceId;
            objMt.Command_Code = commandCode;
            objMt.Message_Type = 1;
            objMt.Request_ID = requestId;
            objMt.Total_Message = 1;
            objMt.Message_Index = 0;
            objMt.IsMore = 0;
            objMt.Content_Type = 0;
            objMt.ServiceType = 0;
            objMt.ResponseTime = DateTime.Now;
            objMt.isLock = false;
            objMt.PartnerID = "Xzone";
            objMt.Operator = "vnmobile";
            ViSport_S2_SMS_MTController.InsertVClip(objMt);

            log.Info(" ");
            log.Info("***** LOG SEND MT VCLIP *****");

            log.Info("User_ID : " + userId);
            log.Info("Message : " + message);
            log.Info("Service_ID : " + serviceId);
            log.Info("Command_Code : " + commandCode);
            log.Info("Send_MT : " + value);

            log.Info("****************************************");
            log.Info(" ");


            #endregion
        }

        return "1";
    }

    #region Advance methods

    public void SendMtSportGame(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
    {
        var objSentMt = new ServiceProviderService();

        const int msgType = (int)Constant.MessageType.NoCharge;

        if (AppEnv.GetSetting("TestFlag") == "0")
        {
            int result = objSentMt.sendMT(userId, mtMessage, serviceId, commandCode, msgType.ToString(), requestId, "1", "1", "0", "0");
            log.Debug("Send MT result : " + result);
            log.Debug("userId : " + userId);
            log.Debug("Noi dung MT : " + mtMessage);
            log.Debug("ServiceId : " + serviceId);
            log.Debug("commandCode : " + commandCode);
            log.Debug("requestId : " + requestId);
        }

        var objMt = new ViSport_S2_SMS_MTInfo();
        objMt.User_ID = userId;
        objMt.Message = mtMessage;
        objMt.Service_ID = serviceId;
        objMt.Command_Code = commandCode;
        objMt.Message_Type = 1;
        objMt.Request_ID = requestId;
        objMt.Total_Message = 1;
        objMt.Message_Index = 0;
        objMt.IsMore = 0;
        objMt.Content_Type = 0;
        objMt.ServiceType = 0;
        objMt.ResponseTime = DateTime.Now;
        objMt.isLock = false;
        objMt.PartnerID = "VNM";
        objMt.Operator = "vnmobile";
        objMt.IsQuestion = 1;

        ViSport_S2_SMS_MTController.InsertSportGameHeroMt(objMt);
    }

    private static bool CheckDayOfWeek(string inputDay)
    {

        if (inputDay == DayOfWeek.Tuesday.ToString() || inputDay == DayOfWeek.Thursday.ToString() || inputDay == DayOfWeek.Saturday.ToString())
        {
            return true;
        }

        return false;
    }

    #endregion
    
}


#line default
#line hidden
