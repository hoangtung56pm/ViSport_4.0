using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using ChargingGateway;
using SentMT;
using SMSManager_API.Library.Utilities;

/// <summary>
/// Summary description for JobsSubBiMatHotGirl
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class JobsSubBiMatHotGirl : System.Web.Services.WebService, IJobExecutorSoap
{

    public JobsSubBiMatHotGirl () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog log = log4net.LogManager.GetLogger(typeof(JobsSubBiMatHotGirl));

     [WebMethod]
     public int Execute(int jobId)
     {
         WebServiceCharging3g webServiceCharging3G = new WebServiceCharging3g();
         string userName = "VMGWAP3G";
         string userPass = "vmg@#3g";
         string cpId = "1928";
         string price = "5000";

         try
         {
             DataTable dtUsers = VoteRegisterController.SecretGetUserByType(false);
             if (dtUsers != null && dtUsers.Rows.Count > 0)
             {
                 string message = string.Empty;
                 string returnValue = string.Empty;
                 string notEnoughMoney = "Result:12,Detail:Not enough money.";

                 string serviceType = "BiMat_HotGirl";
                 string serviceName = "BiMat_HotGirl";
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
                             if(returnValue.Trim() == notEnoughMoney)
                             {
                                 price = "1000";
                                 returnValue = webServiceCharging3G.PaymentVnmWithAccount(userId, price, serviceType, serviceName, userName, userPass, cpId);
                             }
                         }
                     }

                     if(returnValue.Trim() == "1")//CHARGED THANH CONG
                     {
                         reasonLog = "Succ";
                     }
                     else
                     {
                         reasonLog = returnValue;
                     }

                     #region LOG DOANH THU

                     VoteChargedUserLogInfo logInfo = new VoteChargedUserLogInfo();

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

                     VoteRegisterController.SecretChargedUserLogInsertForSub(logInfo);

                     #endregion

                     #region GUI SMS CHO KHACH_HANG
                     
                     if(returnValue.Trim() == "1")
                     {
                         string serviceId = dr["Service_ID"].ToString();
                         string commandCode = dr["Command_Code"].ToString();
                         string requestId = dr["Request_ID"].ToString();

                         DataTable dt = VoteRegisterController.SecretGetCountByPersonId(userId, 1);
                         message = "So luot Dat gach cua ban: " + dt.Rows[0]["Count"] + ". Ban dang thuoc top " + dt.Rows[0]["Top"] + " nhung nguoi dat gach nhieu nhat. Dat gach cang nhieu ban cang co nhieu co hoi gap mat de biet BI MAT DONG TROI cua hot girl Mai Tho. De tiep tuc dat gach, Soan Gach gui 8379 hoac su dung 3G truy cap http://wap.vietnamobile.com.vn. HT: 19001255";

                         SendMtSecret(userId, serviceId, commandCode, message, requestId);

                         //GUI THEM TIN TUC BI_MAT_MAI_THO
                         DataTable dtSecretContent = VoteRegisterController.SecretGetRandomContent();
                         if(dtSecretContent != null && dtSecretContent.Rows.Count > 0)
                         {
                             message = dtSecretContent.Rows[0]["MT1"].ToString();
                             SendMtSecret(userId,serviceId,commandCode,message,requestId);
                         }

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

    #region PUBLIC METHODS

     private static void SendMtSecret(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
     {
         var objSentMt = new ServiceProviderService();

         string message = Message;

         if (AppEnv.GetSetting("TestFlag") == "0")
         {
             objSentMt.sendMT(User_ID, message, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
         }

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

         VoteRegisterController.SecretSmsMtInsert(objMt);

     }

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
