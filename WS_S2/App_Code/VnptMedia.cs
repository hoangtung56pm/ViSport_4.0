using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ApiVnptMedia;
using SentMT;
using WS_Music.Library;

/// <summary>
/// Summary description for VnptMedia
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class VnptMedia : System.Web.Services.WebService {

    public VnptMedia () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog _log = log4net.LogManager.GetLogger("File");

    [WebMethod]
    public string MoProcess(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExcecuteMo(User_ID,Service_ID,Command_Code,Message,Request_ID);
    }

    private string ExcecuteMo(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {

        Message = Message.ToUpper();
        string subcode = "";
        if (Message.Trim().Length > Command_Code.Trim().Length)
        {
            subcode = Message.ToUpper().Substring(Command_Code.Length).Replace(" ", "");
        }

        Command_Code = Command_Code.ToUpper();
        Message = Message.ToUpper();

        #region Log MO Message Into Database (SMS_MO_Log)

        var moInfo = new SMS_MOInfo();
        moInfo.User_ID = User_ID;
        moInfo.Service_ID = Service_ID;
        moInfo.Command_Code = Command_Code;
        moInfo.Message = Message;
        moInfo.Request_ID = Request_ID;
        moInfo.Operator = "VNM";
        SMS_MODB.InsertSportGameHeroMo(moInfo);


        #endregion

        #region CALL to Partner WS
        
        RegGate ws = new RegGate();
        string resVal;
        string res;
        string message;

        string packageName = subcode;
        const string promotion = "0";
        const string trial = "0";
        const string bundle = "0";
        string note = Message;
        const string application = "VMGPORTAL";
        const string channel = "SMS";
        const string userName = "VMG";
        const string userId = "sv167.vmgmedia.vn";
        const string policy = "0";

        if (Command_Code == "DK")
        {
            #region DK

            //103|INVALID_IP
            resVal = ws.register(Request_ID, User_ID, packageName, promotion, trial, bundle, note, application, channel,userName, userId);
            res = resVal.Split('|')[0].Trim();
            _log.Debug("Vnpt_media response ws register : " + resVal + "; Message : " + Message);

            if (res == "0")
            {
                #region "Đăng ký thành công dịch vụ"

                message = "Chuc mung, Quy khach dang ky thanh cong goi xem " + subcode + " va duoc " + GetDay(subcode) + " ngay xem MIEN PHI cac kenh truyen hinh cap VTV tren dich vu Mobile TV cua VNM. " +
                          "Goi cuoc se duoc tu dong gia han theo <Ngay/Tuan> " +
                          "khi het mien phi, cuoc gia han: (Giá gói DV), soan HUY " + subcode + " gui 444 de huy dich vu. " +
                          "Truy cap http://mtv.vietnamobile.com.vn/ de xem ngay cac kenh truyen hinh Cap VTV dac sac nhat hien nay. " +
                          "Dac biet, dich vu hoan toan mien phi cuoc GPRS/3G. Tran trong cam on!";
                
                SendMtVnptMedia(User_ID,message,Service_ID,Command_Code,Request_ID);

                #endregion
            }
            else if (res == "1")
            {
                #region Thuê bao này đã tồn tại

                message = "Dang ky khong thanh cong do Quy khach dang su dung goi ngay cua dich vu MobileTV. Truy cap http://mtv.vietnamobile.com.vn/ de su dung dich vu. Chi tiet lien he 19001255. Tran trong cam on!";
                SendMtVnptMedia(User_ID, message, Service_ID, Command_Code, Request_ID);

                #endregion
            }
            else if(res == "2")
            {
                #region Đăng ký rồi và đăng ký lại dịch vụ

                message = "Dang ky khong thanh cong do Quy khach dang su dung goi ngay cua dich vu MobileTV. Truy cap http://mtv.vietnamobile.com.vn/ de su dung dich vu. Chi tiet lien he 19001255. Tran trong cam on!";
                SendMtVnptMedia(User_ID, message, Service_ID, Command_Code, Request_ID);

                #endregion
            }
            else if (res == "3")
            {
                #region Đăng ký thành công dịch vụ và không bị trừ cước đăng ký
                
                #endregion
            }
            else if (res == "4")
            {
                #region Đăng ký thành công dịch vụ và bị trừ cước đăng ký
                
                #endregion
            }
            else if (res == "5")
            {
                #region Đăng ký không thành công do không đủ tiền trong tài khoản

                message = "Dang ky khong thanh cong do tai khoan khong du tien. Xin cam on";
                SendMtVnptMedia(User_ID, message, Service_ID, Command_Code, Request_ID);

                #endregion
            }
            else if (res == "101")
            {
                #region Sai cu PHAP

                message = "Tin nhan sai cu phap. xin cam on";
                SendMtVnptMedia(User_ID, message, Service_ID, Command_Code, Request_ID);

                #endregion
            }
            else if (res == "103")
            {
                #region IP Khong duoc phep

                message = "IP khong duoc phep. xin cam on";
                SendMtVnptMedia(User_ID, message, Service_ID, Command_Code, Request_ID);

                #endregion
            }
            else if (res == "100")
            {
                #region Loi khong xac dinh

                message = "Loi khong xac dinh. xin cam on";
                SendMtVnptMedia(User_ID, message, Service_ID, Command_Code, Request_ID);

                #endregion
            }
            else
            {
                #region Đều là đăng ký không thành công

                message = "He thong dang ban. xin cam on";
                //SendMtVnptMedia(User_ID, message, Service_ID, Command_Code, Request_ID);
                SendMtVnptMedia(User_ID, res, Service_ID, Command_Code, Request_ID);

                #endregion
            }

            #endregion
        }
        else if (Command_Code == "HUY")
        {
            #region HUY

            resVal = ws.cancelService(Request_ID, User_ID, packageName,policy, promotion,note, application, channel, userName, userId);
            res = resVal.Split('|')[0].Trim();
            _log.Debug("Vnpt_media response ws cancel : " + resVal + "; Message : " + Message);

            if (res == "0")
            {
                #region "Success"

                message = "Quy khach da huy thanh cong goi " + subcode + " cua dich vu MobileTV. De dang ky lai soan DK " + subcode + " gui 444. " +
                          "Cac noi dung Quy khach da mua van duoc tiep tuc su dung. Cam on Quy khach da su dung dich vu. " +
                          "Chi tiet truy cap http://mtv.vietnamobile.com.vn/ hoac lien he ho tro 19001255. Tran trong cam on!";

                SendMtVnptMedia(User_ID, message, Service_ID, Command_Code, Request_ID);

                #endregion
            }
            else if (res == "1")
            {
                #region Thuê bao này không tồn tại

                message = "Quy khach chua dang ky dich vu MobileTV cua VNM. " +
                          "De dang ky dich vu, Quy khach vui long soan: PHIM cho goi ngay hoac PHIM PHIM cho goi tuan gui 444. " +
                          "Xem huong dan soan HD gui 444. " +
                          "Chi tiet truy cap http://mtv.vietnamobile.com.vn/ hoac lien he ho tro 19001255. Tran trong cam on!";
                SendMtVnptMedia(User_ID, message, Service_ID, Command_Code, Request_ID);

                #endregion
            }
            else
            {
                #region Đều là đăng ký không thành công

                message = "He thong dang ban. xin cam on";
                //SendMtVnptMedia(User_ID, message, Service_ID, Command_Code, Request_ID);
                SendMtVnptMedia(User_ID, res, Service_ID, Command_Code, Request_ID);
                #endregion
            }

            #endregion
        }
        else if (Command_Code == "MK")
        {
            #region MAT KHAU

            resVal = ws.getPassOtp(User_ID, "Lay pass nguoi dung");
            res = resVal.Split('|')[0].Trim();
            _log.Debug("Vnpt_media response ws password : " + resVal + "; Message : " + Message);
            if (res == "0")
            {
                string pass = resVal.Split('|')[1].Trim();
                message = "Mat khau de su dung dich vu MobileTV cua Quy khach la: " + pass + ".Truy cap http://mtv.vietnamobile.com.vn/ de su dung dich vu. Lien he ho tro 19001255. Tran trong cam on!";
                SendMtVnptMedia(User_ID, message, Service_ID, Command_Code, Request_ID);
            }
            else
            {
                message = "Quy khach chua dang ky dich vu MobileTV cua VNM. De dang ky dich vu, Quy khach vui long soan: DK TenGoi gui 444 hoac truy cap dia chi http://mtv.vietnamobile.com.vn/ va lam theo huong dan. Lien he ho tro 19001255. Tran trong cam on!";
                SendMtVnptMedia(User_ID, message, Service_ID, Command_Code, Request_ID);
            }

            #endregion
        }


        #endregion

        return "1";
    }

    #region Public methods

    public void SendMtVnptMedia(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
    {
        var objSentMt = new ServiceProviderService();

        const int msgType = (int)Constant.MessageType.NoCharge;

        //if (AppEnv.GetSetting("TestFlag") == "0")
        //{
        int result = objSentMt.sendMT(userId, mtMessage, serviceId, commandCode, msgType.ToString(), requestId, "1", "1", "0", "0");
        _log.Debug("***** VNPT_Media Send MT *****");
        _log.Debug("Send MT result : " + result);
        _log.Debug("userId : " + userId);
        _log.Debug("Noi dung MT : " + mtMessage);
        _log.Debug("ServiceId : " + serviceId);
        _log.Debug("commandCode : " + commandCode);
        _log.Debug("requestId : " + requestId);
        _log.Debug("*****");
        //}

        const int isQuestion = 0; // = 1 : CAU HOI GUI TU WinService

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
        objMt.Operator = "VNM";
        objMt.IsQuestion = isQuestion;

        ViSport_S2_SMS_MTController.VnptInsertMt(objMt);
    }

    public string GetDay(string input)
    {
        string day = string.Empty;

        if (input == "MTV1")
        {
            day = "1";
        }
        else if (input == "MTV7")
        {
            day = "7";
        }
        else if (input == "MTV30")
        {
            day = "30";
        }
        else if (input == "VTC1")
        {
            day = "1";
        }
        else if (input == "VTC7")
        {
            day = "7";
        }
        else if (input == "VTC30")
        {
            day = "30";
        }
        else if (input == "CAB1")
        {
            day = "1";
        }
        else if (input == "CAB7")
        {
            day = "7";
        }
        else if (input == "CAB30")
        {
            day = "30";
        }
        else if (input == "TT1")
        {
            day = "1";
        }
        else if (input == "TT7")
        {
            day = "7";
        }
        else if (input == "TT30")
        {
            day = "30";
        }
        else if (input == "PHIM1")
        {
            day = "1";
        }
        else if (input == "PHIM7")
        {
            day = "7";
        }
        else if (input == "PHIM30")
        {
            day = "30";
        }
        else if (input == "VOD" || input == "MOD ")
        {
            day = "1";
        }
        else if (input == "CLIP1")
        {
            day = "1";
        }
        else if (input == "CLIP7")
        {
            day = "7";
        }
        else if (input == "CLIP30")
        {
            day = "30";
        }
        else if (input == "SD")
        {
            day = "1";
        }

        return day;
    }

    #endregion

    

}
