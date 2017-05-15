using System;
using System.Data;
using System.Web.Services;
using ChargingGateway;
using SentMT;
using SMSManager_API.Library.Utilities;
using WS_Music.Library;

/// <summary>
/// Summary description for JobsSubVClip
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class JobsSubVClip : System.Web.Services.WebService, IJobExecutorSoap
{

    public JobsSubVClip () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog log = log4net.LogManager.GetLogger(typeof(JobsSubVClip));

   
    [WebMethod]
    public int Execute(int jobId)
    {
        WebServiceCharging3g webServiceCharging3G = new WebServiceCharging3g();
        string userName = "VMGWAP3G";
        string userPass = "vmg@#3g";
        string cpId = "1928";
        string price;

        try
        {
            DataTable dt = ViSport_S2_Registered_UsersController.VClipGetMTByStatus(false);
            if(dt != null && dt.Rows.Count > 0)
            {
                string message = string.Empty;
                string returnValue = string.Empty;
                string notEnoughMoney = "Result:12,Detail:Not enough money.";

                string serviceType = "Charged Sub VClip";
                string serviceName = "VClip";
                string reasonLog = string.Empty;
                string status = "1";

                foreach (DataRow dr in dt.Rows)
                {
                    string userId = dr["User_ID"].ToString();
                    price = "2000";
                    returnValue = webServiceCharging3G.PaymentVnmWithAccount(userId, price, serviceType, serviceName, userName, userPass, cpId);
                    if (returnValue.Trim() == notEnoughMoney)
                    {
                        price = "1000";
                        returnValue = webServiceCharging3G.PaymentVnmWithAccount(userId, price, serviceType, serviceName, userName, userPass, cpId);
                    }

                    if (returnValue == "1")//CHARGED THANH CONG
                    {
                        #region Log Doanh Thu

                        var logInfo = new ViSport_S2_Charged_Users_LogInfo();

                        logInfo.ID = ConvertUtility.ToInt32(dr["Id"].ToString());
                        logInfo.User_ID = userId;
                        logInfo.Request_ID = dr["Request_ID"].ToString();
                        logInfo.Service_ID = dr["Service_ID"].ToString();
                        logInfo.Command_Code = dr["Command_Code"].ToString();
                        logInfo.Service_Type = ConvertUtility.ToInt32(dr["Service_Type"].ToString());
                        logInfo.Charging_Count = ConvertUtility.ToInt32(dr["Charging_Count"].ToString());
                        logInfo.FailedChargingTimes = ConvertUtility.ToInt32(dr["FailedChargingTimes"].ToString());
                        logInfo.RegisteredTime = ConvertUtility.ToDateTime(dr["RegisteredTime"].ToString());
                        logInfo.ExpiredTime = DateTime.Now;
                        logInfo.Registration_Channel = dr["Registration_Channel"].ToString();
                        logInfo.Status = ConvertUtility.ToInt32(dr["Status"].ToString());
                        logInfo.Operator = dr["Operator"].ToString();
                        logInfo.Price = ConvertUtility.ToInt32(price);
                        logInfo.Reason = "Succ";

                        ViSport_S2_Registered_UsersController.VClipInsertLog(logInfo);

                        #endregion
                    }
                    else
                    {
                        #region Log Doanh Thu

                        var logInfo = new ViSport_S2_Charged_Users_LogInfo();

                        logInfo.ID = ConvertUtility.ToInt32(dr["Id"].ToString());
                        logInfo.User_ID = userId;
                        logInfo.Request_ID = dr["Request_ID"].ToString();
                        logInfo.Service_ID = dr["Service_ID"].ToString();
                        logInfo.Command_Code = dr["Command_Code"].ToString();
                        logInfo.Service_Type = ConvertUtility.ToInt32(dr["Service_Type"].ToString());
                        logInfo.Charging_Count = ConvertUtility.ToInt32(dr["Charging_Count"].ToString());
                        logInfo.FailedChargingTimes = ConvertUtility.ToInt32(dr["FailedChargingTimes"].ToString());
                        logInfo.RegisteredTime = ConvertUtility.ToDateTime(dr["RegisteredTime"].ToString());
                        logInfo.ExpiredTime = DateTime.Now;
                        logInfo.Registration_Channel = dr["Registration_Channel"].ToString();
                        logInfo.Status = ConvertUtility.ToInt32(dr["Status"].ToString());
                        logInfo.Operator = dr["Operator"].ToString();
                        logInfo.Price = ConvertUtility.ToInt32(price);
                        logInfo.Reason = returnValue;

                        ViSport_S2_Registered_UsersController.VClipInsertLog(logInfo);

                        #endregion
                    }

                    if (returnValue == "1")//CHARGED THANH CONG
                    {
                        #region Gui MT cho khach hang thong bao gia han thanh cong

                        var objSentMt = new ServiceProviderService();
                        const int msgType = (int)Constant.MessageType.NoCharge;

                        message = "(092)Quy khach da gia han thanh cong DV VMclip cua Vietnamobile. Moi ban truy cap: http://kho-clip.com/" + userId + ".aspx de xem cac video HOT cap nhat 24/24 MIEN PHI. De huy DK, soan CLIP OFF gui 949. HT 19001255";

                        string serviceId = dr["Service_ID"].ToString();
                        string commandCode = dr["Command_Code"].ToString();
                        string requestId = dr["Request_ID"].ToString();

                        int value = objSentMt.sendMT(userId, message, serviceId, commandCode, msgType.ToString(), requestId, "1", "1", "0", "0");

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
                }
            }
        }
        catch (Exception ex)
        {
            log.Info(" ");
            log.Info("***** VClip Charged Error *****");
            log.Info("Error : " + ex);
            log.Info(" ");
            return 0;
        }

        return 1;
    }

}
