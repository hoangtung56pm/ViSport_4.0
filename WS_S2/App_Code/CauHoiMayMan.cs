using SentMT;
using SMSManager_API.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using WS_Music.Library;

/// <summary>
/// Summary description for CauHoiMayMan
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class CauHoiMayMan : System.Web.Services.WebService {

    public CauHoiMayMan () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    log4net.ILog log = log4net.LogManager.GetLogger("File");
    [WebMethod]
    public string WSProcessMoCauHoiMayMan(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExcecuteRequestMoCauHoiMayMan(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }

    #region Methods Process Mo
    
    private string ExcecuteRequestMoCauHoiMayMan(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        string responseValue = "1";
        int returnValue = 0;

        Message = Message.ToUpper();
        string subcode = "";
        if (Message.Trim().Length > Command_Code.Trim().Length)
        {
            subcode = Message.ToUpper().Substring(Command_Code.Length).Replace(" ", "");
        }
        try
        {
            log.Debug(" ");
            log.Debug(" ");
            log.Debug("-------------------- CAU HOI MAY MAN -------------------------");
            log.Debug("User_ID: " + User_ID);
            log.Debug("Service_ID: " + Service_ID);
            log.Debug("Command_Code: " + Command_Code);
            log.Debug("Message: " + Message.ToUpper());
            log.Debug("Request_ID: " + Request_ID);
            log.Debug(" ");
            log.Debug(" ");

            #region Log MO Message Into Database (SMS_MO_Log)

            var moInfo = new SMS_MOInfo();

            moInfo.User_ID = User_ID;
            moInfo.Service_ID = Service_ID;
            moInfo.Command_Code = Command_Code;
            moInfo.Message = Message;
            moInfo.Request_ID = Request_ID;
            moInfo.Operator = GetTelco(User_ID);
            moInfo.Type = 2;
            SMS_MODB.InsertCauHoiMayManMo(moInfo);

            #endregion
             string messageReturn;
             if (Command_Code.ToUpper() == "HD")
             {
                 if (subcode == "MM")
                 {
                     messageReturn = "Dich vu Quay So May Man cua Vietnamobile tang ban co hoi so huu iPhone 6s sanh dieu hang quy. Dong thoi moi tuan, KH dat diem so cao nhat se gianh giai thuong 1 trieu dong tien mat. KH tham gia bang cach soan DK MM gui 949 (2000d/ngay), moi ngay KH se nhan 5 cau hoi mien phi. KH truy cap http://visport.vn/Wap/cauhoi.aspx de tra loi cau hoi va nhan diem. Tra loi dung duoc 1 diem, tra loi sai khong duoc diem. Mua them cau hoi voi gia 1000d/cau hoi.";
                     SendMtCauHoiMayMan(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                 }else{
                     messageReturn = "Tin nhan sai cu phap. HT: 19001255";
                     SendMtCauHoiMayMan(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                 }
             }
             //else if (Command_Code.ToUpper() == "DIEM")
             //{
                 
             //    DataTable dtDiem = ThanTai_MT_Controller.Diem_CauHoiMayMan(User_ID);
             //    int diem = 0;
             //    int stt = 0;
                 
             //    if (dtDiem != null && dtDiem.Rows.Count > 0)
             //    {
             //        diem = ConvertUtility.ToInt32(dtDiem.Rows[0]["Diem"].ToString());
             //        stt = ConvertUtility.ToInt32(dtDiem.Rows[0]["Stt"].ToString());
             //        if (diem > 0)
             //        {
             //            messageReturn = "Ban dang co " + diem + " diem va xep thu " + stt + " trong bang xep hang de nhan giai thuong 1 trieu dong tuan nay va iPhone 6s cua quy. Chi tiet soan HD MM (MIEN PHI) gui 949 hoac truy cap <landing page>";
             //            SendMtCauHoiMayMan(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
             //        }
             //        else
             //        {
             //            messageReturn = "Ban chua danh duoc diem nao. Co gang len !.";
             //            SendMtCauHoiMayMan(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
             //        }
             //    }
             //    else
             //    {
             //        messageReturn = "Ban chua dang ky dich vu, KH tham gia bang cach soan DK MM gui 949 (2000d/ngay) !";
             //        SendMtCauHoiMayMan(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
             //    }
             //}
             else{
                 messageReturn = "Tin nhan sai cu phap. HT: 19001255";
                 SendMtCauHoiMayMan(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
             }
        }
        catch (Exception ex)
        {
            log.Debug("--------------- CAU HOI MAY MAN ----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }
        return responseValue;
    }

    #endregion

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

    public void SendMtCauHoiMayMan(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
    {
        var objSentMt = new ServiceProviderService();

        const int msgType = (int)Constant.MessageType.NoCharge;

        int result = objSentMt.sendMT(userId, mtMessage, serviceId, commandCode, msgType.ToString(), requestId, "1", "1", "0", "0");
        log.Debug("Send MT result : " + result);
        log.Debug("userId : " + userId);
        log.Debug("Noi dung MT : " + mtMessage);
        log.Debug("ServiceId : " + serviceId);
        log.Debug("commandCode : " + commandCode);
        log.Debug("requestId : " + requestId);

        var objMt = new ThanTai_MT_Info();
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

        objMt.PartnerID = "VMG";
        objMt.Operator = "vnmobile";
        objMt.Type = 2;
        ThanTai_MT_Controller.Insert_CauHoiMayMan_MT(objMt);
    }
}
