#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\JobsWorldCupSubVtvNotification.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1A4270793217F77FB368D1D8109B5AE59018B29E"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\JobsWorldCupSubVtvNotification.cs"
using System;
using System.Web.Services;
using SMSManager_API.Library.Utilities;
using WapJavaGame.Library.Utilities;

/// <summary>
/// Summary description for JobsWorldCupSubVtvNotification
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class JobsWorldCupSubVtvNotification : System.Web.Services.WebService, IChargingNotificationSoap
{

    public JobsWorldCupSubVtvNotification () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog log = log4net.LogManager.GetLogger(typeof(JobsWorldCupSubVtvNotification));
   
    public string NotifyChargingInfo(string registeredId, string userId, string requestId, string serviceId, string serviceType, string chargingValue, string chargingAccount, string chargingTime, string chargingResponse)
    {

        log.Info(" ");
        log.Info("***** LOG VTV CHARGED NOTIFICATION From ANDY *****");

        log.Info("User_ID : " + userId);
        log.Info("chargingValue : " + chargingValue);
        log.Info("chargingAccount : " + chargingAccount);
        log.Info("chargingTime : " + chargingTime);
        log.Info("chargingResponse : " + chargingResponse);

        log.Info("****************************************");
        log.Info(" ");

        if(chargingResponse.Trim() == "1")//CHARGED THANH CONG
        {

            #region GOI API sang VTV

            //string url = "http://worldcup.visport.vn/TelcoApi/service.php?action=VMGgiahan&msisdn=" + userId + "&price=" + chargingValue;

            //var post = new PostSubmitter();
            //post.Url = url;
            //post.Type = PostSubmitter.PostTypeEnum.Get;
            //string message = post.Post();

            //log.Debug(" ");
            //log.Debug(" ");
            //log.Debug("API Call : " + url);
            //log.Debug("UserId : " + userId);
            //log.Debug("Content From VTV : " + message);
            //log.Debug(" ");
            //log.Debug(" ");

            //SendMtWorldCup(userId, message, dr["Service_ID"].ToString(), dr["Command_Code"].ToString(),dr["User_ID"].ToString());

            #endregion

            #region LOG DOANH THU

            const string reasonLog = "Succ";

            var logInfo = new SportGameHeroChargedUserLogInfo();

            logInfo.ID = ConvertUtility.ToInt32(registeredId);
            logInfo.User_ID = userId;
            logInfo.Request_ID = requestId;
            logInfo.Service_ID = serviceId;
            logInfo.Command_Code = "VTV";

            logInfo.Service_Type = ConvertUtility.ToInt32(0);
            logInfo.Charging_Count = ConvertUtility.ToInt32(0);
            logInfo.FailedChargingTime = ConvertUtility.ToInt32(0);

            logInfo.RegisteredTime = ConvertUtility.ToDateTime(chargingTime);
            logInfo.ExpiredTime = DateTime.Now.AddDays(1);

            logInfo.Registration_Channel = "VTV";
            logInfo.Status = ConvertUtility.ToInt32(1);
            logInfo.Operator = "vnmobile";
            logInfo.Price = ConvertUtility.ToInt32(chargingValue);
            logInfo.Reason = reasonLog;

            ViSport_S2_Registered_UsersController.WorldCupChargedUserLogForSubVtv6(logInfo);

            #endregion

        }
        else //CHARGED THAT BAI
        {
            #region LOG DOANH THU

            var logInfo = new SportGameHeroChargedUserLogInfo();

            logInfo.ID = ConvertUtility.ToInt32(registeredId);
            logInfo.User_ID = userId;
            logInfo.Request_ID = requestId;
            logInfo.Service_ID = serviceId;
            logInfo.Command_Code = "VTV";

            logInfo.Service_Type = ConvertUtility.ToInt32(0);
            logInfo.Charging_Count = ConvertUtility.ToInt32(0);
            logInfo.FailedChargingTime = ConvertUtility.ToInt32(0);

            logInfo.RegisteredTime = ConvertUtility.ToDateTime(chargingTime);
            logInfo.ExpiredTime = DateTime.Now.AddDays(1);

            logInfo.Registration_Channel = "VTV";
            logInfo.Status = ConvertUtility.ToInt32(1);
            logInfo.Operator = "vnmobile";
            logInfo.Price = ConvertUtility.ToInt32(chargingValue);
            logInfo.Reason = chargingResponse.Trim();

            ViSport_S2_Registered_UsersController.WorldCupChargedUserLogForSubVtv6(logInfo);

            #endregion
        }

        return "1";
    }
}


#line default
#line hidden
