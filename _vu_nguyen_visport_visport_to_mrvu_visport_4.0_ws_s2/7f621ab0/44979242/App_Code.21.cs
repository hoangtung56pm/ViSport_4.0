#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\JobsSubHotGirlQuanTu.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2890BBD95E52E9510382A1693CB673D4D36968FC"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\JobsSubHotGirlQuanTu.cs"
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using ChargingGateway;
using SentMT;
using SMSManager_API.Library.Utilities;
using WS_Music.Library;

/// <summary>
/// Summary description for JobsSubHotGirlQuanTu
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class JobsSubHotGirlQuanTu : System.Web.Services.WebService, IJobExecutorSoap
{

    public JobsSubHotGirlQuanTu () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog log = log4net.LogManager.GetLogger(typeof(JobsSubHotGirlQuanTu));

    [WebMethod]
    public int Execute(int jobID)
    {
        WebServiceCharging3g webServiceCharging3G = new WebServiceCharging3g();
        string userName = "VMGWAP3G";
        string userPass = "vmg@#3g";
        string cpId = "1928";
        string price = "5000";

        try
        {
            DataTable dtUsers = VoteRegisterController.NewVoteGetUserByType(false);
            if(dtUsers != null && dtUsers.Rows.Count > 0)
            {
                string message = string.Empty;
                string returnValue = string.Empty;
                string notEnoughMoney = "Result:12,Detail:Not enough money.";

                string serviceType = "HotGirl_QuanTu";
                string serviceName = "HotGirl_QuanTu";
                string reasonLog = string.Empty;
                foreach (DataRow dr in dtUsers.Rows)
                {
                    string userId = dr["User_ID"].ToString();
                    returnValue = webServiceCharging3G.PaymentVnmWithAccount(userId, price, serviceType, serviceName, userName, userPass, cpId);
                    if (returnValue.Trim() == notEnoughMoney)
                    {
                        price = "3000";
                        returnValue = webServiceCharging3G.PaymentVnmWithAccount(userId, price, serviceType, serviceName, userName, userPass, cpId);
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
                    }

                    if (returnValue.Trim() == "1")//CHARGED THANH CONG
                    {
                        reasonLog = "Succ";
                    }
                    else
                    {
                        reasonLog = returnValue;
                    }

                    #region LOG DOANH THU

                    var logInfo = new VoteChargedUserLogInfo();

                    logInfo.ID = ConvertUtility.ToInt32(dr["ID"].ToString());
                    logInfo.User_ID = userId;
                    logInfo.Request_ID = dr["Request_ID"].ToString();
                    logInfo.Service_ID = dr["Service_ID"].ToString();
                    logInfo.Command_Code = dr["Command_Code"].ToString();
                    logInfo.Service_Type = 0;//Charged Sub Service_Type
                    logInfo.Charging_Count = ConvertUtility.ToInt32(dr["Charging_Count"].ToString());
                    logInfo.FailedChargingTime = ConvertUtility.ToInt32(dr["FailedChargingTimes"].ToString());
                    logInfo.RegisteredTime = DateTime.Now;
                    logInfo.ExpiredTime = DateTime.Now.AddDays(1);
                    logInfo.Registration_Channel = dr["Registration_Channel"].ToString();
                    logInfo.Status = ConvertUtility.ToInt32(dr["Status"].ToString());
                    logInfo.Operator = dr["Operator"].ToString();
                    logInfo.Price = ConvertUtility.ToInt32(price);
                    logInfo.Vote_PersonId = 1;
                    logInfo.Reason = reasonLog;

                    VoteRegisterController.NewVoteChargedUserLogInsertForSub(logInfo);

                    #endregion

                    #region GUI SMS CHO KHACH_HANG

                    if (returnValue.Trim() == "1")
                    {
                        string serviceId = dr["Service_ID"].ToString();
                        string commandCode = dr["Command_Code"].ToString();
                        string requestId = dr["Request_ID"].ToString();

                        DataTable dt = VoteRegisterController.NewVoteRegisterUserGetInfo(userId);
                        int voteCount = ConvertUtility.ToInt32(dt.Rows[0]["Vote_Count"]);
                        string voteTop = GetTopVote(voteCount);

                        message = "So luot vote cua ban: " + voteCount + ".Ban dang thuoc top: " + voteTop + " nhung nguoi Vote nhieu nhat.Soan: Vote1 gui 8579 de Hen Ho voi 1 trong 5 Hot Girl Xinh Dep.Chi tiet truy cap: http://wap.vietnamobile.com.vn. HT: 19001255";

                        SendMtNewVote(userId, serviceId, commandCode, message, requestId);
                    }

                    #endregion

                }

            }

            return 1;
        }
        catch (Exception ex)
        {
             log.Error(ex.ToString());
             return 0;
        }

    }

    #region METHODS

    private static string GetTopVote(int vote)
    {
        string str = "";

        if (vote >= 1 && vote <= 10)
        {
            str = "100";
        }
        else if (vote >= 11 && vote <= 50)
        {
            str = "50";
        }
        else if (vote >= 51)
        {
            str = "10";
        }

        return str;
    }

    private static void SendMtNewVote(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        var objSentMt = new ServiceProviderService();

        string message = Message;
        const int msgType = (int)Constant.MessageType.NoCharge;

        //if (AppEnv.GetSetting("TestFlag") == "0")
        //{
            objSentMt.sendMT(User_ID, message, Service_ID, Command_Code, msgType.ToString(), Request_ID, "1", "1", "0", "0");
        //}

        var objMt = new VoteSmsMtInfo();
        objMt.User_ID = User_ID;
        objMt.Message = message;
        objMt.Service_ID = Service_ID;
        objMt.Command_Code = Command_Code;
        objMt.Message_Type = 1;
        objMt.Request_ID = Request_ID;
        objMt.Total_Message = 1;
        objMt.Message_Index = 0;
        objMt.IsMore = 0;
        objMt.Content_Type = 0;
        objMt.ServiceType = 0;
        objMt.ResponseTime = DateTime.Now;
        objMt.IsLock = 0;
        objMt.PartnerId = "Xzone";
        objMt.Operator = GetTelco(User_ID);

        VoteRegisterController.NewVoteSmsMtInsert(objMt);

    }

    private static string GetTelco(string mobile)
    {
        string prenumber = mobile.Substring(0, 5);

        string[] dfsplit = AppEnv.GetSetting("sfone").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "sfone";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("vnmobile").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "vnmobile";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("gtel").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "gtel";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("viettel").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "viettel";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("vms").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "vms";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("gpc").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "gpc";
                }
            }
        }

        return "";
    }

    #endregion
}


#line default
#line hidden
