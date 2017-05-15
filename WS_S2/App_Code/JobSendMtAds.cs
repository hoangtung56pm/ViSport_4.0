using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using SentMT;

/// <summary>
/// Summary description for JobSendMtAds
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class JobSendMtAds : System.Web.Services.WebService, IJobExecutorSoap
{

    public JobSendMtAds () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog log = log4net.LogManager.GetLogger(typeof(JobSendMtAds));

     [WebMethod]
     public int Execute(int jobId)
     {
         try
         {
             DataTable dtActiveUser = VoteRegisterController.VoteGetVnmUserActive();
             if(dtActiveUser != null && dtActiveUser.Rows.Count > 0)
             {
                 foreach(DataRow dr in dtActiveUser.Rows)
                 {
                     string userId = dr["User_ID"].ToString();
                     string serviceId = dr["Service_ID"].ToString();
                     string requestId = dr["Request_ID"].ToString();
                     string commandCode = dr["Command_Code"].ToString();
                     string message = "Thong bao, het ngay 30/11/2013 Chuong trinh 'Hen ho hot girl' se ket thuc. Tiep ngay sau la Gameshow con HOT hon 'Bi mat HOT girl'. Ngoai viec gap go HOT girl Sieu Vong 1 - Mai Tho, Quy khach con co co hoi thuong thuc mot clip 102 doc quyen va biet duoc bi mat Dong Troi an chua ben trong Mai Tho. He thong se tu dong chuyen thue bao cua QK sang chuong trinh moi ke tu ngay 01/12/2013, neu khong muon tham gia chuong trinh, soan HUY gui 8279. Hoac truy cap http://wap.vietnamobile.com.vn/bi-mat-hot-girl/mai-tho.aspx de biet thong tin. HT 19001255";

                     log.Debug(" ");
                     log.Debug(" ");
                     log.Debug("--------------------VOTE VNM SEND MT HUY-------------------------");
                     log.Debug("User_ID: " + userId);
                     log.Debug("Service_ID: " + serviceId);
                     log.Debug("Command_Code: " + commandCode);
                     log.Debug("Message: " + message.ToUpper());
                     log.Debug("Request_ID: " + requestId);
                     log.Debug(" ");
                     log.Debug(" ");

                     SendMt(userId,serviceId,commandCode,message,requestId);
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

     private static void SendMt(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
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

         VoteRegisterController.VoteSmsMtInsert(objMt);

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

}
