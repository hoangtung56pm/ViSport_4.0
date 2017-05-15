using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using SentMT;
using Subscription_Services.ServiceHandlers;
using WS_Music.Library;

/// <summary>
/// Summary description for VclipWebservice
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class VclipWebservice : System.Web.Services.WebService, IServiceHandlerSoap
{
    log4net.ILog log = log4net.LogManager.GetLogger("VclipWebservice");
    public VclipWebservice () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 

    }
       
    public string SyncSubscriptionData(string Service_ID, string Command_Code, string User_ID, string Message, string Request_ID, string ServiceID, string RefID, string UpdateType, string UpdateDescription)
    {
        string message = "";
        Message = Message.ToUpper();
        string subcode = "";
        if (Message.Trim().Length > Command_Code.Trim().Length)
        {
            subcode = Message.ToUpper().Substring(Command_Code.Length).Replace(" ", "");
        }

        #region VCLIP

        try
        {
            log.Debug(" ");
            log.Debug(" ");
            log.Debug("-------------------VCLIP--------------------------");
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
            SMS_MODB.InsertVClip(moInfo);

            #endregion

            #region Execute MT



            if (Message.StartsWith("HUY"))
            {
                //return "-5";
                #region Huy DK USER

                var objCancel = new SMS_CancelInfo();

                objCancel.User_ID = User_ID;
                objCancel.Service_ID = Service_ID;
                objCancel.Command_Code = Command_Code;
                objCancel.Service_Type = GetServiceTypeVClip(subcode);
                objCancel.Message = Message;
                objCancel.Request_ID = Request_ID;
                objCancel.Operator = GetTelco(User_ID);
                SMS_MODB.CancelInsert(objCancel);

                var regObject = new ViSport_S2_Registered_UsersInfo();

                regObject.User_ID = User_ID;
                regObject.Status = 0;
                regObject.Service_Type = objCancel.Service_Type;

                DataTable dt = ViSport_S2_Registered_UsersController.UpdateVClip(regObject);

                var objSentMt = new ServiceProviderService();

                if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                {
                    message = AppEnv.GetSetting("alert_cancel_success_vclip");
                    objSentMt.sendMT(User_ID, message, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
                    //return "-5";
                }
                else
                {
                    message = "Ban chua dk dich vu nay. Xin cam on";
                    objSentMt.sendMT(User_ID, message, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
                    //return "-6";
                }



                var objMt = new ViSport_S2_SMS_MTInfo();
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
                objMt.isLock = false;
                objMt.PartnerID = "Xzone";
                objMt.Operator = GetTelco(User_ID);

                ViSport_S2_SMS_MTController.InsertVClip(objMt);

                #endregion

            }
            else
            {
                var objSentMt = new ServiceProviderService();

                if (AppEnv.GetSetting("VClip_New") == "1")
                {
                    //Đăng ký kịch bản mới
                    #region Dang Ky USER (Kich ban moi)

                    var regObject = new ViSport_S2_Registered_UsersInfo();

                    regObject.User_ID = User_ID;
                    regObject.Request_ID = Request_ID;
                    regObject.Service_ID = Service_ID;
                    regObject.Command_Code = Command_Code;
                    regObject.Service_Type = GetServiceTypeVClip(Command_Code);
                    regObject.Charging_Count = 0;
                    regObject.FailedChargingTimes = 0;
                    regObject.RegisteredTime = DateTime.Now;
                    regObject.ExpiredTime = DateTime.Now.AddDays(1);
                    regObject.Registration_Channel = "SMS";
                    regObject.Status = 1;
                    regObject.Operator = moInfo.Operator;

                    DataTable dt = ViSport_S2_Registered_UsersController.InsertVClipNew(regObject);
                    if (dt.Rows[0]["RETURN_ID"].ToString() == "0")
                    {
                        //Đăng ký lần đầu
                        message = "Chuc mung! Quy khach da Dky thanh cong DV VClip. QK duoc mien phi ngay dau tien trong lan dau dang ky. Moi QK truy cap http://kho-clip.com/ de su dung dvu (2.000d/ngay),dvu duoc tu dong gia han. De huy DK, soan:HUY VCLIP gui 949. HT: 19001255.";
                    }
                    else if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                    {
                        //Double đăng ký
                        message = "Quy Khach da dang ky dich vu VClip truoc do. Moi QK truy cap http://kho-clip.com/ de su dung dvu.. HT: 19001255";
                    }
                    else if (dt.Rows[0]["RETURN_ID"].ToString() == "2")
                    {
                        //Hủy đi đăng ký lại
                        message = "Chuc mung! Quy khach da Dky thanh cong DV VClip. Moi QK truy cap http://kho-clip.com/ de su dung dvu (2.000d/ngay),dvu duoc tu dong gia han. De huy DK, soan:HUY VCLIP gui 949. HT: 19001255.";
                    }

                    #region SEND_MT

                    objSentMt.sendMT(User_ID, message, Service_ID, Command_Code, "0", Request_ID, "1", "1", "0", "0");
                    var objMt = new ViSport_S2_SMS_MTInfo();
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
                    objMt.isLock = false;
                    objMt.PartnerID = "Xzone";
                    objMt.Operator = GetTelco(User_ID);
                    ViSport_S2_SMS_MTController.InsertVClip(objMt);

                    #endregion


                    #endregion

                }

            }


            #endregion

            return "-10";
        }
        catch (Exception ex)
        {
            log.Debug("---------------Error sentMT----------------------");
            log.Debug("Get Error : " + ex.Message);
            return "-20";
        }

        #endregion

        //return "-1";
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

    public void SendMt(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
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
        objMt.Operator = GetTelco(userId);

        ViSport_S2_SMS_MTController.InsertVnmMt(objMt);
    }

    private static int GetServiceTypeVClip(string subcode)
    {
        if (subcode == "VCLIP")
        {
            return 1;
        }
        return 0;
    }
    
    [WebMethod]
    public string SynchronizeUser(string Shortcode, string RequestID, string Msisdn, string Commandcode, string Message, int SyncType, string content, int chargedDay)
    {
        string retVal = "0|Unidentified";
        var objSentMt = new ServiceProviderService();
        try
        {                
                //  Add
                if (SyncType == 1)
            {
                #region Dang Ky USER (Kich ban moi)

                var regObject = new ViSport_S2_Registered_UsersInfo();

                regObject.User_ID = Msisdn;
                regObject.Request_ID = RequestID;
                regObject.Service_ID = Shortcode;
                regObject.Command_Code = Commandcode;
                regObject.Service_Type = 0;
                regObject.Charging_Count = 0;
                regObject.FailedChargingTimes = 0;
                regObject.RegisteredTime = DateTime.Now;
                regObject.ExpiredTime = DateTime.Now.AddDays(1);
                regObject.Registration_Channel = "wap";
                regObject.Status = 1;
                regObject.Operator = "vnmobile";
                regObject.CountTo_Cancel = chargedDay + 1;

                DataTable dt = ViSport_S2_Registered_UsersController.ImportVClip(regObject);               

                #region SEND_MT

                objSentMt.sendMT(Msisdn, content, Shortcode, Commandcode, "0", RequestID, "1", "1", "0", "0");
                var objMt = new ViSport_S2_SMS_MTInfo();
                objMt.User_ID = Msisdn;
                objMt.Message = content;
                objMt.Service_ID = Shortcode;
                objMt.Command_Code = Commandcode;
                objMt.Message_Type = 1;
                objMt.Request_ID = RequestID;
                objMt.Total_Message = 1;
                objMt.Message_Index = 0;
                objMt.IsMore = 0;
                objMt.Content_Type = 0;
                objMt.ServiceType = 0;
                objMt.ResponseTime = DateTime.Now;
                objMt.isLock = false;
                objMt.PartnerID = "Xzone";
                objMt.Operator = GetTelco(Msisdn);
                ViSport_S2_SMS_MTController.InsertVClip(objMt);

                #endregion


                #endregion
                retVal = "1";
                    
                }
                else if (SyncType == 0) // Delete
                {

                var objCancel = new SMS_CancelInfo();

                objCancel.User_ID = Msisdn;
                objCancel.Service_ID = Shortcode;
                objCancel.Command_Code = Commandcode;
                objCancel.Service_Type = 0;
                objCancel.Message = Message;
                objCancel.Request_ID = RequestID;
                objCancel.Operator = "vnmobile";
                SMS_MODB.CancelInsert(objCancel);

                retVal = "1";
                   
                }
          
        }
        catch (Exception ex)
        {
            log.Error(ex.ToString());
            retVal = "0|" + ex.Message;
        }

        return retVal;
    }
}
