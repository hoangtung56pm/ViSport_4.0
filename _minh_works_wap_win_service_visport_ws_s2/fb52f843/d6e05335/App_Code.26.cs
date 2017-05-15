#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\ZaloMoProcess.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5FCACF2654F58426F3CB7D60E849BB7148888BDE"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\ZaloMoProcess.cs"
using System;
using System.Data;
using System.Web.Services;
using SMSManager_API.Library.Utilities;
using ZaloPageSDK.com.vng.zalosdk.entity;
using ZaloPageSDK.com.vng.zalosdk.exceptions;
using ZaloPageSDK.com.vng.zalosdk.service;

/// <summary>
/// Summary description for ZaloMoProcess
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ZaloMoProcess : System.Web.Services.WebService {

    public ZaloMoProcess () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    static readonly log4net.ILog Log = log4net.LogManager.GetLogger("ZaloMoProcess");

    [WebMethod]
    public string MoProcess(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        Command_Code = Command_Code.ToUpper();
        Message = Message.ToUpper();

        string responseValue = "1";
        //int returnValue = 0;

        Message = Message.ToUpper();
        string subcode = "";
        if (Message.Trim().Length > Command_Code.Trim().Length)
        {
            subcode = Message.ToUpper().Substring(Command_Code.Length).Replace(" ", "");
        }
        else
        {
            subcode = Message.Replace("ZALO", "").Replace("CAUZALO", "").Replace(" ","");
            Command_Code = Command_Code.Replace(subcode, "").Replace(" ", "");
        }

        //return responseValue;

        Log.Debug(" ");
        Log.Debug(" ");
        Log.Debug("-------------------- Zalo Mo Process -------------------------");
        Log.Debug("User_ID: " + User_ID);
        Log.Debug("Service_ID: " + Service_ID);
        Log.Debug("Command_Code: " + Command_Code);
        Log.Debug("Message: " + Message);
        Log.Debug("Request_ID: " + Request_ID);
        Log.Debug(" ");
        Log.Debug(" ");

        #region LOG SMS MO

        var moInfo = new ZaloMoInfo();
        moInfo.User_ID = User_ID;
        moInfo.Request_ID = Request_ID;
        moInfo.Service_ID = Service_ID;
        moInfo.Command_Code = Command_Code;
        moInfo.Message = Message;
        moInfo.Operator = GetTelco(User_ID);
        ZaloController.ZaloMoInsert(moInfo);

        #endregion

        try
        {
            string reMessage;
            DataTable dtService = ZaloController.ZaloGetServiceInfo(subcode);
            if (dtService != null && dtService.Rows.Count > 0)
            {
                //int typeId = ConvertUtility.ToInt32(dtService.Rows[0]["Type_Id"]);
                int companyId = ConvertUtility.ToInt32(dtService.Rows[0]["CompanyId"]);
                string companyName = UnicodeUtility.UnicodeToKoDau(dtService.Rows[0]["Name"].ToString());

                const string zaloPartner = "ZALO";
                //string vmgPartner = "VMG";

                DateTime nowTime = DateTime.Now;

                string lotTime = DateTime.Now.Year + "-" + DateTime.Now.Month;
                string ngay = DateTime.Now.Day + "/" + DateTime.Now.Month;

                if (Command_Code == "ZALO")
                {
                    #region KQ XS

                    if (Service_ID == "8179" || Service_ID == "8279")
                    {
                        #region 1. Nhận kết quả xổ số mới nhất theo tỉnh

                        if(nowTime.Hour < 12) //TRA LUON KQ NGAY HOM TRUOC
                        {
                            lotTime = lotTime + "-" + DateTime.Now.AddDays(-1).Day;
                            DataTable dtCon = ZaloController.ZaloGetLotteryContent(companyId, lotTime);
                            if(dtCon != null && dtCon.Rows.Count > 0)
                            {

                                #region TRA MT WELCOME

                                reMessage = "Cam on ban da su dung dich vu KQXS cua Zalo. Ket qua xo so " + companyName + " se duoc gui den ban tren tin nhan cua Zalo. DTHT: 19001255";
                                SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                                #endregion
                                
                                #region TRA KQ QUA ZALO

                                foreach (DataRow dr in dtCon.Rows)
                                {
                                    reMessage = dr["lot_content"].ToString();

                                    #region CALL Zalo_API

                                    int reVal = ApiZaloCallForSendZms(User_ID, reMessage);
                                    //SAVE LOG AFTER CALL API
                                    SaveMtLog(User_ID, Service_ID, Command_Code, reMessage, Request_ID, zaloPartner, reVal);

                                    #endregion

                                }

                                #endregion

                            }
                        }
                        if(nowTime.Hour > 12)
                        {
                            #region KIEM TRA XEM KQ HNAY DA CO CHUA

                            lotTime = lotTime + "-" + DateTime.Now.Day;
                            DataTable dtCon = ZaloController.ZaloGetLotteryContent(companyId, lotTime);
                            if (dtCon != null && dtCon.Rows.Count > 0)
                            {
                                #region TRA MT WELCOME

                                reMessage = "Cam on ban da su dung dich vu KQXS cua Zalo. Ket qua xo so " + companyName + " ngay " + ngay + " se duoc gui den ban tren tin nhan cua Zalo. DTHT: 19001255";
                                SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                                #endregion

                                #region CO = TRA KQ LUON

                                    foreach (DataRow dr in dtCon.Rows)
                                    {
                                        reMessage = dr["lot_content"].ToString();

                                        #region CALL Zalo_API

                                        int reVal = ApiZaloCallForSendZms(User_ID,reMessage);

                                        #endregion

                                        //SAVE LOG AFTER CALL API
                                        SaveMtLog(User_ID, Service_ID, Command_Code, reMessage, Request_ID, zaloPartner, reVal);
                                    }

                                #endregion
                            }
                            else
                            {
                                #region KHONG = DUA VAO BANG Waiting

                                reMessage = "Ket qua xo so " + companyName + " ngay " + ngay + " se duoc he thong gui den ban ngay khi co ket qua tren tin nhan ZALO. DT ho tro: 19001255";
                                //GUI MT THONGBAO He Thong Da ghi Nhan SMS
                                SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                                //DUA VAO BANG DOI
                                SaveLotteryDay(User_ID, Request_ID, Service_ID, Command_Code, subcode, companyId);
                                //END DUA VAO BANG DOI

                                #endregion
                            }
                           
                            #endregion
                        }

                        #endregion
                    }
                    else if (Service_ID == "8379")
                    {
                        #region 2. Nhận kết quả xổ số mới nhất theo miền

                            #region TRA MT WELCOME

                                reMessage = "Cam on ban da su dung dich vu KQXS cua Zalo. Ket qua xo so ban yeu cau se duoc gui den ban tren tin nhan cua Zalo. DTHT: 19001255";
                                SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                            #endregion

                            #region LUU VAO BANG DOI
                            
                            SaveLotteryDay(User_ID,Request_ID,Service_ID,Command_Code,subcode,companyId);

                            #endregion

                        #endregion
                    }
                    else if (Service_ID == "8779")
                    {
                        #region 3. Đăng ký nhận kết quả xổ số nhiều ngày

                        #region TRA MT WELCOME

                        reMessage = "Ban da dang ky su dung dich vu Xo so " + companyName + " trong 30 ngay lien tiep cua ZALO. Chuc ban may man! DT ho tro: 19001255";
                        SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                        #endregion

                        #region LUU VAO BANG DOI

                        SaveLotteryDay(User_ID, Request_ID, Service_ID, Command_Code, subcode, companyId);

                        #endregion

                        #endregion
                    }
                    else if (Service_ID == "8579")
                    {
                        #region 4. Tường thuật trực tiếp kết quả xổ số

                        #region TRA MT WELCOME

                        reMessage = "Tuong thuat truc tiep KQXS " + companyName + " ngay " + ngay + " se duoc gui den ban tren tin nhan ZALO ngay khi co ket qua. Xin vui long doi! DTHT:19001255";
                        SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                        #endregion

                        #region LUU VAO BANG DOI

                        SaveLotteryDay(User_ID, Request_ID, Service_ID, Command_Code, subcode, companyId);

                        #endregion

                        #endregion
                    }

                    #endregion
                }
                else if (Command_Code == "CAUZALO")
                {
                    #region SOI CAU

                    #region TRA MT WELCOME

                    reMessage = "Cam on ban da su dung dich vu Cap so may man cua Zalo. Cap so may man " + companyName + " ky toi se duoc gui den ban tren tin nhan cua Zalo. DTHT: 19001255";
                    //GUI SMS THONGBAO He Thong Da Ghi Nhan SMS Cua KH
                    SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                    #endregion

                    #region CALL ZALO API

                    DataTable dtCon = ZaloController.ZaloGetSoiCauContent(companyId);
                    if(dtCon != null && dtCon.Rows.Count > 0)
                    {
                        foreach(DataRow dr in dtCon.Rows)
                        {
                            reMessage = dr["Content"].ToString();
                            int reVal = ApiZaloCallForSendZms(User_ID,reMessage);
                            //SAVE LOG AFTER CALL API
                            SaveMtLog(User_ID, Service_ID, Command_Code, reMessage, Request_ID, zaloPartner,reVal);
                        }
                    }

                    #endregion

                    #endregion
                }
            }
            else
            {
                reMessage = "Tin nhan sai cu phap !";
                SendMt(User_ID,Service_ID,Command_Code,reMessage,Request_ID);
            }

        }
        catch (Exception ex)
        {
            Log.Debug(" ");
            Log.Debug(" ");
            Log.Debug("--------------- Zalo WS Process MO Error ----------------------");
            Log.Debug("Zalo Exception : " + ex.Message);
            Log.Debug(" ");
            Log.Debug(" ");
            throw;
        }

        return responseValue;
    }

    #region Methods

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

    private static void SaveMtLog(string User_ID, string Service_ID, string Command_Code, string message, string Request_ID,string Partner,int type)
    {
        var objMt = new ZaloMtInfo();
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
        objMt.PartnerId = Partner;
        objMt.Operator = GetTelco(User_ID);
        objMt.Type = type;

        ZaloController.ZaloMtInsert(objMt);
    }

    private static void SendMt(string userId,string serviceId,string commandCode,string message,string requestId)
    {
        var mtInfo = new MTInfo();
        var random = new Random();
        mtInfo.User_ID = userId;
        mtInfo.Service_ID = serviceId;
        mtInfo.Command_Code = commandCode;
        mtInfo.Message_Type = 0;
        mtInfo.Request_ID = random.Next(100000000, 999999999).ToString();
        mtInfo.Total_Message = 1;
        mtInfo.Message_Index = 0;
        mtInfo.IsMore = 0;
        mtInfo.Content_Type = 0;
        mtInfo.Message = message;

        SaveMtLog(userId, serviceId, commandCode, message, requestId,"VMG",0);

        ZaloController.SmsMtInsertNew(mtInfo);
    }

    private static void SaveLotteryDay(string userId,string requestId,string serviceId,string commandCode,string subCode,int companyId)
    {
        //DUA VAO BANG DOI
        var item = new ZaloLotteryDay();
        item.UserId = userId;
        item.RequestId = requestId;
        item.ServiceId = serviceId;
        item.CommanCode = commandCode;
        item.Operator = GetTelco(userId);
        item.ServiceCode = subCode;
        item.CompanyId = companyId;
        ZaloController.ZaloLotteryDayInsert(item);
        //END DUA VAO BANG DOI
    }

    private static int ApiZaloCallForSendZms(string userId,string message)
    {
        int reValue = ZaloController.ApiZaloCallForSendZms(userId, message);
        return reValue;
    }

    #endregion


    #region DRAFT
    
    //try
    //        {
    //            string pageId = "2156820476252246890";
    //            string message = "vmg message";
    //            string smsMsg = "vmg - smsMsg";
    //            string secretKey = "LJ80A443RnVr3T8Ik18K";
    //            string pathPhoto = "";
    //            string pathVoice = "";
    //            long toUid = 84976245979;
    //            //long toUid = 84983042686;
    //            //long toUid = 84902220502;
    //            string templateid = "4069c383ffc616984fd7";
    //            ZaloServiceFactory factory = new ZaloServiceFactory(pageId, secretKey);

    //            //String photoId = factory.getZaloUploadService().uploadImage(pathPhoto);
    //            //String voiceId = factory.getZaloUploadService().uploadVoice(pathVoice);

    //            ZaloMessageService messageService = factory.getZaloMessageService();
    //            //ZaloPageResult objResult = messageService.sendTextMessage(toUid, message, message, true);
    //            Dictionary<string, string> data =
    //    new Dictionary<string, string>();

    //            data.Add("pageName", "Vmgmedia");
    //            ZaloPageResult objResult = messageService.sendTemplateTextMessageByPhoneNum(toUid, templateid, data, "fsfsfd", true);



    //            //int err1 = messageService.sendTextMessage(toUid, message, message, true);
    //            //if (err1 >= 0)
    //            //{
    //            //    Console.WriteLine("Pass send text message by page >>>>>>");
    //            //}

    //            //int err2 = messageService.sendImageMessage(toUid, photoId, pathPhoto, pathVoice, true);
    //            //if (err2 >= 0)
    //            //{
    //            //    Console.WriteLine("Pass send image message by page >>>>>>");
    //            //}

    //            string id = "b8e9d66f75aa87f4debb";


    //        }
    //        catch (ZaloSdkException ex)
    //        {
    //            Response.Write("Err = " + ex.getErrorCode());
    //            Response.Write("Mes = " + ex.getMessage());
    //        }
    //        catch (Exception ex)
    //        {
    //            Response.Write(ex.StackTrace);
    //        }

    #endregion

}


#line default
#line hidden
