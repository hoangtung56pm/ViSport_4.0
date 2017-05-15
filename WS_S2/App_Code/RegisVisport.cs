using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using SentMT;
using SMSManager_API.Library.Utilities;
using WS_Music.Library;

/// <summary>
/// Summary description for RegisVisport
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class RegisVisport : System.Web.Services.WebService {

    public RegisVisport () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    log4net.ILog log = log4net.LogManager.GetLogger(typeof(Notification));
    [WebMethod]
    public string MOreceiver_VoiceChat(String Command_Code, String Service_ID, String User_ID, String Message, String Request_ID, String Channel)
    {
        string messageReturn = "";
        string responseValue = "";

        Message = Message.ToUpper();
        string subcode = "";
        if (Message.Trim().Length > Command_Code.Trim().Length)
        {
            subcode = Message.ToUpper().Substring(Command_Code.Length).Replace(" ", "");
        }

        Command_Code = Command_Code.ToUpper();
        Message = Message.ToUpper();
        try
        {
            log.Debug(" ");
            log.Debug(" ");
            log.Debug("-------------------- TRIEU PHU BONG DA -------------------------");
            log.Debug("User_ID: " + User_ID);
            log.Debug("Service_ID: " + Service_ID);
            log.Debug("Command_Code: " + Command_Code);
            log.Debug("Message: " + Message.ToUpper());
            log.Debug("Request_ID: " + Request_ID);
            log.Debug(" ");
            log.Debug(" ");

            #region Log MO Message Into Database (SMS_MO_Log)


            if (AppEnv.GetSetting("TestFlag") == "0")
            {
                var moInfo = new SMS_MOInfo();
                moInfo.User_ID = User_ID;
                moInfo.Service_ID = Service_ID;
                moInfo.Command_Code = Command_Code;
                moInfo.Message = Message;
                moInfo.Request_ID = Request_ID;
                moInfo.Operator = GetTelco(User_ID);
                SMS_MODB.InsertSportGameHeroMo(moInfo);
            }

            if (Command_Code == "TP" && subcode == "" && Service_ID == "979") //DK DICH VU TRIEU_PHU_BONG_DA
            {
                #region DK DICH VU

                var entity = new ViSport_S2_Registered_UsersInfo();
                entity.User_ID = User_ID;
                entity.Request_ID = Request_ID;
                entity.Service_ID = Service_ID;
                entity.Command_Code = Command_Code;
                entity.Service_Type = 1;
                entity.Charging_Count = 0;
                entity.FailedChargingTimes = 0;
                entity.RegisteredTime = DateTime.Now;
                entity.ExpiredTime = DateTime.Now.AddDays(1);
                entity.Registration_Channel = Channel;
                entity.Status = 1;
                entity.Operator = GetTelco(User_ID);
                entity.Point = 2;

                string passWord = RandomActiveCode.RandomStringNumber(6);
                entity.Password = passWord;

                DataTable value = ViSport_S2_Registered_UsersController.InsertSportGameHeroRegisterUser(entity);

                if (value.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
                {

                    #region DK DV LAN DAU TIEN                     

                    messageReturn = "Chuc mung ban da tham gia CTKM Trieu phu bong da cua Vietnamobile. ";
                    if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                    {
                        messageReturn = messageReturn + "Ban co 5.000 diem voi co hoi trung thuong 1 dien thoai Samsung Galaxy A5. ";
                    }
                    messageReturn = messageReturn + "Moi ngay ban se nhan duoc nhung tin tuc the thao nong hoi (5000d/ngay). " +
                                    "Truy cap: http://visport.vn de su dung dich vu. " +
                                    "De huy dvu soan: HUY TP gui 979, xem diem va so diem cao nhat hien tai soan TOP gui 979 HT: 19001255";

                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);    

                    responseValue = "1|Success";
                    #endregion

                }
                else if (value.Rows[0]["RETURN_ID"].ToString() == "2")
                {

                    #region DA DK DV ROI XONG HUY DK LAI ==> TRA MT WELCOME

                    messageReturn = "Chuc mung ban da tham gia  CTKM Trieu phu bong da cua Vietnamobile.";
                    if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                    {
                        messageReturn = messageReturn + "  Ban co 5.000 diem voi co hoi trung thuong 1 dien thoai Samsung Galaxy A5.";
                    }
                    messageReturn = messageReturn + "Moi ngay ban se nhan duoc nhung tin tuc the thao nong hoi. Truy cap: http://visport.vn de su dung dvu. De huy dvu soan: HUY TP gui 979. HT: 19001255";
                                            
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1

                    responseValue = "1|Success";
                    #endregion

                }
                else if (value.Rows[0]["RETURN_ID"].ToString() == "1")//DA DK DICH VU
                {

                    #region THUE BAO DANG ACTIVE DV

                    messageReturn = "Ban da tham gia  CTKM Trieu phu bong da cua Vietnamobile.";
                    if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                    {
                        messageReturn = messageReturn + " Ban co 5.000 diem voi co hoi trung thuong 1 dien thoai Samsung Galaxy A5.";
                    }
                    messageReturn = messageReturn + "Moi ngay ban se nhan duoc nhung tin tuc the thao nong hoi. Truy cap: http://visport.vn de su dung dvu. De huy dvu soan: HUY TP gui 979. HT: 19001255";
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);

                    responseValue = "0|DoubleRegister";
                    #endregion

                }

                #endregion
            }
            else
            {
                messageReturn = "Tin nhan sai cu phap.";
                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                responseValue = "-1|WrongSyntax";
            }

            #endregion
        }
        catch (Exception ex)
        {
            log.Error(ex.ToString());
            responseValue = "-2|System busy";
        }

        return responseValue;
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

    public void SendMtSportGameHero(string userId, string mtMessage, string serviceId, string commandCode, string requestId, int isQuestion)
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

        isQuestion = 0;// = 1 : CAU HOI GUI TU WinService

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
        objMt.Operator = GetTelco(userId);
        objMt.IsQuestion = isQuestion;

        ViSport_S2_SMS_MTController.InsertSportGameHeroMt(objMt);
    }
    
}
