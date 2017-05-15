#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\HandlerBigDelete.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C575BB6C0D1CCD0D3B0F6443C157A7CD3249FDFC"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\HandlerBigDelete.cs"
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using RingTone;
using SentMT;
using ShotAndPrint;
using Subscription_Services.ServiceHandlers;
using WS_Music.Library;
using vn.vmgame;

/// <summary>
/// Summary description for HandlerBigDelete
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class HandlerBigDelete : System.Web.Services.WebService, IServiceHandlerSoap
{

    public HandlerBigDelete () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog log = log4net.LogManager.GetLogger(typeof(HandlerBigDelete));

    public string SyncSubscriptionData(string ShortCode, string CommandCode, string UserID, string Message, string RequestID, string ServiceID, string RefID, string UpdateType, string UpdateDescription)
    {

        try
        {
            #region HUY DICH VU

            log.Debug(" ");
            log.Debug(" ");
            log.Debug("-------------------- BIG PROMOTION DELETE-------------------------");
            log.Debug("User_ID: " + UserID);
            log.Debug("Service_ID: " + ServiceID);
            log.Debug("Command_Code: " + CommandCode);
            log.Debug("Message: " + Message.ToUpper());
            log.Debug("Request_ID: " + RequestID);
            log.Debug(" ");
            log.Debug(" ");

            string message;
            DataTable dt = ViSport_S2_Registered_UsersController.ThanhNuRegisterUserStatusUpdate(UserID, 0);

            if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
            {
                message = "Quy khach da huy thanh cong goi dich vu ( bao gom game portal, shot and print, nhac chuong). Ma du thuong cua Qkhach se khong duoc tham gia quay thuong. De dang ky lai dich vu soan GOI gui 949";

                #region HUY VMGAME
                
                var vmgame = new Service_RegisS2();
                string vmRes = vmgame.BigPromotionDelete(UserID, "BigPro123!@#Tqscd");

                log.Debug(" ");
                log.Debug(" ");
                log.Debug("-------------------- BIG PROMOTION VmGameResult DELETE -------------------------");
                log.Debug("User_ID: " + UserID);
                log.Debug("vmGameResult: " + vmRes);
                log.Debug(" ");
                log.Debug(" ");

                #endregion

                #region HUY SHOT and PRINT
                
                var shot = new S2Process();
                string shotRes = shot.BPCancel(UserID, "4", "HUY GOI 949");

                log.Debug(" ");
                log.Debug(" ");
                log.Debug("-------------------- BIG PROMOTION shotResult DELETE -------------------------");
                log.Debug("User_ID: " + UserID);
                log.Debug("shotResult: " + shotRes);
                log.Debug(" ");
                log.Debug(" ");

                #endregion

                #region HUY NC1

                var ringTone = new NC1_Handler();
                string ringToneRest = ringTone.SyncSubscriptionData("949", "DK", UserID, Message.ToUpper(), "0", "472", "0", "0", "HUY GOI");

                log.Debug(" ");
                log.Debug(" ");
                log.Debug("-------------------- BIG PROMOTION ringToneRes DELETE -------------------------");
                log.Debug("User_ID: " + UserID);
                log.Debug("ringToneRest: " + ringToneRest);
                log.Debug(" ");
                log.Debug(" ");

                #endregion

                SendMtThanhNu(UserID,message,"949",CommandCode,RequestID);
            }
            else if (dt.Rows[0]["RETURN_ID"].ToString() == "0")
            {
                message = "Ban chua dang ky dich vu nay. De dang ky dich vu soan tin GOI gui 949";
                SendMtThanhNu(UserID, message, ServiceID, CommandCode, RequestID);
            }

            #endregion
        }
        catch (Exception ex)
        {
            log.Error("Loi Huy GOI 949 : " + ex);
            return "-1";
        }

        return "-1";
    }

    public void SendMtThanhNu(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
    {
        var objSentMt = new ServiceProviderService();

        const int msgType = (int)Constant.MessageType.NoCharge;

        //if (AppEnv.GetSetting("TestFlag") == "0")
        //{
            int result = objSentMt.sendMT(userId, mtMessage, serviceId, commandCode, msgType.ToString(), requestId, "1", "1", "0", "0");
            log.Debug("Send MT result : " + result);
            log.Debug("userId : " + userId);
            log.Debug("Noi dung MT : " + mtMessage);
            log.Debug("ServiceId : " + serviceId);
            log.Debug("commandCode : " + commandCode);
            log.Debug("requestId : " + requestId);
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

        ViSport_S2_SMS_MTController.InsertThanhNuMt(objMt);
    }

}


#line default
#line hidden
