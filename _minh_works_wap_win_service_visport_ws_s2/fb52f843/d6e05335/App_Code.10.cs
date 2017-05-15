#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\JobsWorldCupSubVtv.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "69430051052AD55051E3ED93412DFA5A747D692F"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\JobsWorldCupSubVtv.cs"
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using ChargingGateway;
using SMSManager_API.Library.Utilities;
using SentMT;
using WS_Music.Library;
using WapJavaGame.Library.Utilities;

/// <summary>
/// Summary description for JobsWorldCupSubVtv
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class JobsWorldCupSubVtv : System.Web.Services.WebService, IJobExecutorSoap
{

    public JobsWorldCupSubVtv()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog _log = log4net.LogManager.GetLogger(typeof(JobsWorldCupSubVtv));

    [WebMethod]
    public int Execute(int jobID)
    {
        try
        {
            var webServiceCharging3G = new WebServiceCharging3g();

            //string userName = AppEnv.GetSetting("userName_3g_WapVnm");
            //string userPass = AppEnv.GetSetting("password_3g_WapVnm");
            //string cpId = AppEnv.GetSetting("cpId_3g_WapVnm");

            string userName = AppEnv.GetSetting("userName_3g_visport");
            string userPass = AppEnv.GetSetting("password_3g_visport");
            string cpId = AppEnv.GetSetting("cpId_3g_visport");

            string price;

            string notEnoughMoney = AppEnv.GetSetting("NotEnoughMoney");

            const string serviceType = "Charged Sub World Cup VTV";
            const string serviceName = "World_Cup_VTV";

            DataTable dtUsers = ViSport_S2_Registered_UsersController.WorldCupGetRegisterUserForChargedVtv();
            if(dtUsers != null && dtUsers.Rows.Count > 0)
            {
                foreach (DataRow dr in dtUsers.Rows)
                {
                    string userId = dr["User_ID"].ToString();

                    //price = "3000";
                    //string returnValue = webServiceCharging3G.PaymentVnmWithAccount(userId, price, serviceType, serviceName, userName, userPass, cpId);

                    price = "3000";
                    string returnValue = webServiceCharging3G.PaymentVnmWithAccount(userId, price, serviceType, serviceName, userName, userPass, cpId);
                    if (returnValue.Trim() == notEnoughMoney)
                    {
                        price = "2000";
                        returnValue = webServiceCharging3G.PaymentVnmWithAccount(userId, price, serviceType, serviceName, userName, userPass, cpId);
                        if (returnValue.Trim() == notEnoughMoney)
                        {
                            price = "1000";
                            returnValue = webServiceCharging3G.PaymentVnmWithAccount(userId, price, serviceType, serviceName, userName, userPass, cpId);
                        }
                    }

                    _log.Debug(" ");
                    _log.Debug(" ");
                    _log.Debug("UserId : " + userId);
                    _log.Debug("Price : " + price);
                    _log.Debug("ReturnValue : " + returnValue);
                    _log.Debug("UserName : " + userName + " | UserPass : " + userPass + " | CpId : " + cpId);
                    _log.Debug(" ");
                    _log.Debug(" ");

                    if(returnValue == "1")//CHARGED THANH_CONG
                    {
                        #region GOI API sang VTV

                        string url = "http://worldcup.visport.vn/TelcoApi/service.php?action=VMGgiahan&msisdn=" + userId + "&price=" + price;

                        var post = new PostSubmitter();
                        post.Url = url;
                        post.Type = PostSubmitter.PostTypeEnum.Get;
                        string message = post.Post();

                        _log.Debug(" ");
                        _log.Debug(" ");
                        _log.Debug("API Call : " + url);
                        _log.Debug("UserId : " + userId);
                        _log.Debug("Content From VTV : " + message);
                        _log.Debug(" ");
                        _log.Debug(" ");

                        //SendMtWorldCup(userId, message, dr["Service_ID"].ToString(), dr["Command_Code"].ToString(),dr["User_ID"].ToString());

                        #endregion

                        #region LOG DOANH THU

                        const string reasonLog = "Succ";

                        var logInfo = new SportGameHeroChargedUserLogInfo();

                        logInfo.ID = ConvertUtility.ToInt32(dr["ID"].ToString());
                        logInfo.User_ID = dr["User_ID"].ToString();
                        logInfo.Request_ID = dr["Request_ID"].ToString();
                        logInfo.Service_ID = dr["Service_ID"].ToString();
                        logInfo.Command_Code = dr["Command_Code"].ToString();

                        logInfo.Service_Type = ConvertUtility.ToInt32(dr["Service_Type"].ToString());
                        logInfo.Charging_Count = ConvertUtility.ToInt32(dr["Charging_Count"].ToString());
                        logInfo.FailedChargingTime = ConvertUtility.ToInt32(dr["FailedChargingTimes"].ToString());

                        logInfo.RegisteredTime = ConvertUtility.ToDateTime(dr["RegisteredTime"].ToString());
                        logInfo.ExpiredTime = DateTime.Now.AddDays(1);

                        logInfo.Registration_Channel = dr["Registration_Channel"].ToString();
                        logInfo.Status = ConvertUtility.ToInt32(dr["Status"].ToString());
                        logInfo.Operator = dr["Operator"].ToString();
                        logInfo.Price = ConvertUtility.ToInt32(price);
                        logInfo.Reason = reasonLog;

                        ViSport_S2_Registered_UsersController.WorldCupChargedUserLogForSubVtv6(logInfo);

                        #endregion
                    }

                }
            }


        }
        catch (Exception ex)
        {
            _log.Error("WC Loi lay Tap User VTV : " + ex);
            return 0;
        }
        return 1;
    }

    public void SendMtWorldCup(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
    {
        var objSentMt = new ServiceProviderService();

        const int msgType = (int)Constant.MessageType.NoCharge;

        //if (AppEnv.GetSetting("TestFlag") == "0")
        //{
        int result = objSentMt.sendMT(userId, mtMessage, serviceId, commandCode,msgType.ToString(), requestId, "1", "1", "0", "0");
        _log.Debug("Send MT result : " + result);
        _log.Debug("userId : " + userId);
        _log.Debug("Noi dung MT : " + mtMessage);
        _log.Debug("ServiceId : " + serviceId);
        _log.Debug("commandCode : " + commandCode);
        _log.Debug("requestId : " + requestId);
        //}

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

        ViSport_S2_Registered_UsersController.WorldCupInsertMtLog(objMt);
    }

}


#line default
#line hidden
