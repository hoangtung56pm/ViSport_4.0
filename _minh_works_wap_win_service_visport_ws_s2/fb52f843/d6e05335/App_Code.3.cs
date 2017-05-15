#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\WebService.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B6B92728755D5384198EEF01DB92CB15B1443914"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\WebService.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ChargingGateway;
using RingTone;
using SentMT;
using System.Data;
using SMSManager_API.Library.Utilities;
using ShotAndPrint;
using WS_Music.Library;
using WapJavaGame.Library.Utilities;
using vn;
using vn.vmgame;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

    }

    log4net.ILog log = log4net.LogManager.GetLogger("File");

    #region Web Service

    [WebMethod]
    public string WSProcessMo(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExcecuteRequestMo(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }

    [WebMethod]
    public string WSProcessMoVClip(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExcecuteRequestMovClip(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }

    [WebMethod]
    public string WSProcessMoSportGame(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExcecuteRequestMoSportGame(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }

    //[WebMethod]
    //public string WSProcessMoThanhNuGame(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    //{

    //    return ExcecuteRequestMoThanhNu(User_ID, Service_ID, Command_Code, Message, Request_ID);
    //}

    //[WebMethod]
    //public string WSThanhNuGameSubRegister(string msisdn)
    //{
    //    try
    //    {
    //        log.Debug(" ");
    //        log.Debug(" ");
    //        log.Debug("-------------------- GAME THANH NU DK SUBs Tu RS_Mobile -------------------------");
    //        log.Debug("User_ID: " + msisdn);
    //        log.Debug(" ");
    //        log.Debug(" ");

    //        string Service_ID = "2288";
    //        string Command_Code = "DK";
    //        string Request_ID = "0";
    //        string User_ID = msisdn;

    //        var entity = new ThanhNuRegisteredUsers();

    //        entity.UserId = msisdn;
    //        entity.RequestId = Request_ID;
    //        entity.ServiceId = Service_ID;
    //        entity.CommandCode = Command_Code;
    //        entity.ServiceType = 1;
    //        entity.ChargingCount = 0;
    //        entity.FailedChargingTimes = 0;
    //        entity.RegisteredTime = DateTime.Now;
    //        entity.ExpiredTime = DateTime.Now.AddDays(1);
    //        entity.RegistrationChannel = "rsmobile";
    //        entity.Status = 1;
    //        entity.Operator = GetTelco(User_ID);

    //        #region GOI HAM DK BEN DOI TAC

    //        string partnerResult = AppEnv.ThanhNuDangKy(User_ID);

    //        log.Debug(" ");
    //        log.Debug("**********");
    //        log.Debug("Partner_Thanh_Nu_smsKichHoat from Sub : " + partnerResult);
    //        log.Debug("**********");
    //        log.Debug(" ");

    //        string[] arrValue = partnerResult.Split('|');
    //        string messageReturn;
    //        if (arrValue[0].Trim() == "1")
    //        {
    //            ViSport_S2_Registered_UsersController.ThanhNuRegisterUserStatusUpdate(User_ID, 1);
    //            messageReturn = "Ban da la thanh vien cua Game Thanh Nu. Click vao link sau de dang nhap Game " + arrValue[1];
    //            SendMtThanhNu(User_ID, messageReturn, Service_ID, Command_Code, Request_ID); // LAN 1
    //        }
    //        else if (arrValue[0].Trim() == "0")
    //        {
    //            DataTable value = ViSport_S2_Registered_UsersController.ThanhNuRegisterUserInsert(entity);
    //            if (value.Rows[0]["RETURN_ID"].ToString() == "0")
    //            {
    //                messageReturn = "Chuc mung Quy Khach da dang ky thanh cong Game Thanh Nu Gia cuoc 1000d-ngay, Goi dich vu se duoc tu dong gia han hang ngay. De huy dang ky, Quy Khach soan HUY TN gui 2288.";
    //                SendMtThanhNu(User_ID, messageReturn, Service_ID, Command_Code, Request_ID); // LAN 1

    //                messageReturn = "Kich hoat tai khoan " + arrValue[1];
    //                SendMtThanhNu(User_ID, messageReturn, Service_ID, Command_Code, Request_ID); // LAN 2
    //            }
    //            else if (value.Rows[0]["RETURN_ID"].ToString() == "1")
    //            {
    //                ViSport_S2_Registered_UsersController.ThanhNuRegisterUserStatusUpdate(User_ID, 1);
    //                messageReturn = "Ban da la thanh vien cua Game Thanh Nu. Click vao link sau de dang nhap Game " + arrValue[1];
    //                SendMtThanhNu(User_ID, messageReturn, Service_ID, Command_Code, Request_ID); // LAN 1
    //            }
    //        }

    //        #endregion
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Debug("---------------Error From rsmobile subs regis----------------------");
    //        log.Debug("Get Error : " + ex.Message );
    //    }
        
    //    return "1";
    //}

    [WebMethod]
    public string WsMo949Process(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExecuteRequestMo949Process(User_ID,Service_ID,Command_Code,Message,Request_ID);
    }

    [WebMethod]
    public string WSWorldCupMoProcess(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExcuteRequestMoVtv6WorldCupProcess(User_ID,Service_ID,Command_Code,Message,Request_ID);
    }

    [WebMethod]
    public string LiveNewsWorldCupProcess(string User_ID, string Service_ID, string Command_Code,int matchId,string teamCode)
    {

        try
        {
            string message;
            if(Command_Code == "KQQ")
            {
                #region VOTE TRAN DAU

                if (teamCode != "HOA")
                    message = Command_Code + " " + matchId + " " + teamCode;
                else
                    message = Command_Code + " " + matchId;

                VoteRegisterController.WorldCupMatchInsert(User_ID, matchId,message , teamCode);

                #endregion
            }
            else if(Command_Code == "VDD")
            {
                #region VOTE DOI VO DICH

                message = Command_Code + " " + teamCode;
                VoteRegisterController.WorldCupRoundMatchInsert(User_ID, message, teamCode);

                #endregion
            }

            #region DK VTV6 World Cup

            var entity = new ViSport_S2_Registered_UsersInfo();
            entity.User_ID = User_ID;
            entity.Request_ID = "0";
            entity.Service_ID = Service_ID;
            entity.Command_Code = Command_Code;
            entity.Service_Type = 1;
            entity.Charging_Count = 0;
            entity.FailedChargingTimes = 0;
            entity.RegisteredTime = DateTime.Now;
            entity.ExpiredTime = DateTime.Now.AddDays(1);
            entity.Registration_Channel = "LIVE";
            entity.Status = 1;
            entity.Operator = GetTelco(User_ID);
            entity.Point = 0;
            ViSport_S2_Registered_UsersController.WorldCupRegisterUser94X(entity);

            #endregion

            #region LOG DOANH THU LIVE_NEWS

            var logInfo = new SportGameHeroChargedUserLogInfo();

            //logInfo.ID = ConvertUtility.ToInt32(drUser["ID"].ToString());
            logInfo.User_ID = User_ID;
            logInfo.Request_ID = "0";
            logInfo.Service_ID = "8579";
            logInfo.Command_Code = Command_Code;

            logInfo.Service_Type = 2;
            logInfo.Charging_Count = 0;
            logInfo.FailedChargingTime = 0;

            logInfo.RegisteredTime = DateTime.Now;
            logInfo.ExpiredTime = DateTime.Now.AddDays(1);

            logInfo.Registration_Channel = "LIVE";
            logInfo.Status = 1;
            logInfo.Operator = "vnmobile";
            logInfo.Price = ConvertUtility.ToInt32("5000");
            logInfo.Reason = "Succ";

            ViSport_S2_Registered_UsersController.WorldCupChargedUserLog(logInfo);

            #endregion

            return "1";

        }
        catch (Exception ex)
        {
            log.Debug("---------------Error VTV6 World Cup----------------------");
            log.Debug("Get Error : " + ex.Message);

            return "0";
        }
    }

    //[WebMethod]
    //public string Remind()
    //{

    //    //var post = new PostSubmitter();

    //    ////post.Url = "http://worldcup.visport.vn/TelcoApi/service.php?action=VMGsms&command=VMG&message=" + "dang-ky-vtv6-wc" + "&msisdn=" + "841888888869" + "&short_code=dau-SMS-VMG";

    //    //post.Url = "http://worldcup.visport.vn/TelcoApi/service.php?action=VMGgiahan&msisdn=841888888869&price=1000";

    //    //post.Type = PostSubmitter.PostTypeEnum.Get;
    //    //string result = post.Post();

    //    //const int msgType = (int)Constant.MessageType.NoCharge;

    //    //return msgType.ToString();

    //    //string today = DateTime.Now.DayOfWeek.ToString();
    //    //string monday = DayOfWeek.Monday.ToString();

    //    //return "today: " + today + " | monday: " + monday;

    //    DataTable dt = ViSport_S2_Registered_UsersController.SportGameHeroGetUser_Remind();
    //    if(dt != null && dt.Rows.Count > 0)
    //    {
    //        foreach(DataRow dr in dt.Rows)
    //        {
    //            string userId = dr["User_id"].ToString();
    //            DataTable info = ViSport_S2_Registered_UsersController.GetSportGameHeroUserInfo(userId);
    //            if(info != null && info.Rows.Count > 0)
    //            {
    //                int id = ConvertUtility.ToInt32(info.Rows[0]["Id"]);
    //                ViSport_S2_Registered_UsersController.SportGameHeroGetUserDelete_Remind(id);
    //            }
    //        }
    //    }

    //    return "1";

    //}

    [WebMethod]
    public string GenerateGiffCode(string userId,string userName,string password)
    {

        log.Debug(" ");
        log.Debug(" ");
        log.Debug("-------------------- BIG PROMOTION GenerateGiffCode CALL FROM  PARTNER-------------------------");
        log.Debug("User_ID: " + userId);
        log.Debug("userName: " + userName);
        log.Debug("password: " + password);
        log.Debug(" ");
        log.Debug(" ");

        userName = userName.Trim().ToLower();
        password = password.Trim().ToLower();

        if(userName == "vmgame" && password == "123!@#")
        {
            string vmCode = RandomActiveCode.RandomStringNumber(8);

            if(ViSport_S2_Registered_UsersController.ThanhNuRegisterUserCodeInsert(userId, vmCode, userName.ToUpper()))
            {
                return "1";
            }
        }
        if (userName == "shot" && password == "123!@#")
        {
            string shotCode = RandomActiveCode.RandomStringNumber(8);

            if (ViSport_S2_Registered_UsersController.ThanhNuRegisterUserCodeInsert(userId, shotCode, userName.ToUpper()))
            {
                return "1";
            }
        }
        if (userName == "nchuong" && password == "123!@#")
        {
            string nCode = RandomActiveCode.RandomStringNumber(8);

            if (ViSport_S2_Registered_UsersController.ThanhNuRegisterUserCodeInsert(userId, nCode, userName.ToUpper()))
            {
                return "1";
            }
        }

        if (userName == "import" && password == "123!@#")
        {
            try
            {
                #region IMPORT USER

                var entity = new ThanhNuRegisteredUsers();

                entity.UserId = userId;
                entity.RequestId = "0";
                entity.ServiceId = "949";
                entity.CommandCode = "GOI";
                entity.ServiceType = 1;
                entity.ChargingCount = 0;
                entity.FailedChargingTimes = 0;
                entity.RegisteredTime = DateTime.Now;
                entity.ExpiredTime = DateTime.Now.AddDays(5);
                entity.RegistrationChannel = "IMPORT";
                entity.Status = 1;
                entity.Operator = "vnmobile";

                DataTable value = ViSport_S2_Registered_UsersController.ThanhNuRegisterUserInsert(entity);

                string messageReturn;

                if (value.Rows[0]["RETURN_ID"].ToString() == "0")
                {

                    string code1 = RandomActiveCode.Generate(8);
                    string code2 = RandomActiveCode.Generate(8);

                    //messageReturn = "Chuc mung quy khach da dang ky CTKM trai nghiem dich vu GTGT, trung thuong SH sanh dieu va nhieu ipad mini. Qkhach duoc su dung mien phi 5 ngay goi dich vu ( bao gom game portal, shot and print, nhac chuong) va nhan 2 MDT: " + code1 + ", " + code2 + " de tham gia quay thuonng vao cuoi chuong trinh. Sau khi het khuyen mai 15 ngay, he thong se tu dong huy toan bo dich vu cho khach hang. De huy dich vu soan HUY GOI gui 949 ";
                    messageReturn = "Quy khach da dang ky thanh cong CTKM: trai nghiem dich vu MIEN PHI – CO HOI trung SH sanh dieu va nhieu iPad mini. Quy khach duoc nhan 2 MDT: " + code1 + ", " + code2 + " de tham gia quay thuong vao cuoi chuong trinh va su dung MIEN PHI 5 ngay cac dich vu: nhac chuong hay(2000d/ngay); Choi game hap dan – http://vmgame.vn (10,000d/7 ngay); De huy dich vu soan HUY GOI gui 949";

                    SendMtThanhNu(userId, messageReturn, "949", "GOI", "0");

                    #region LUU MDT

                    ViSport_S2_Registered_UsersController.ThanhNuRegisterUserCodeInsert(userId, code1, "GOI");
                    ViSport_S2_Registered_UsersController.ThanhNuRegisterUserCodeInsert(userId, code2, "GOI");

                    #endregion

                    #region DK VMGAME

                    var vmgame = new Service_RegisS2();
                    string vmGameResult = vmgame.BigPromotionRegis(userId, "BigPro123!@#Tqscd");

                    log.Debug(" ");
                    log.Debug(" ");
                    log.Debug("-------------------- BIG PROMOTION VmGameResult REGIS IMPORT -------------------------");
                    log.Debug("User_ID: " + userId);
                    log.Debug("vmGameResult: " + vmGameResult);
                    log.Debug(" ");
                    log.Debug(" ");


                    #endregion

                    //#region Shot And Print

                    //var shot = new S2Process();
                    //string shotResult = shot.BPRegister(userId, RandomActiveCode.Generate(10), "SMS", "4", "SMS");

                    //log.Debug(" ");
                    //log.Debug(" ");
                    //log.Debug("-------------------- BIG PROMOTION shotResult REGIS IMPORT -------------------------");
                    //log.Debug("User_ID: " + userId);
                    //log.Debug("shotResult: " + shotResult);
                    //log.Debug(" ");
                    //log.Debug(" ");

                    //#endregion

                    #region NC1

                    var ringTone = new NC1_Handler();
                    string ringToneRes = ringTone.SyncSubscriptionData("949", "DK", userId, "DK GOI", RandomActiveCode.Generate(10), "472", "0", "1", "DK GOI");

                    log.Debug(" ");
                    log.Debug(" ");
                    log.Debug("-------------------- BIG PROMOTION ringToneRes REGIS IMPORT -------------------------");
                    log.Debug("User_ID: " + userId);
                    log.Debug("ringToneRes: " + ringToneRes);
                    log.Debug(" ");
                    log.Debug(" ");

                    #endregion

                }
                else if (value.Rows[0]["RETURN_ID"].ToString() == "1")
                {
                    return "-1";
                }
                else if (value.Rows[0]["RETURN_ID"].ToString() == "2")
                {

                    #region DK VMGAME

                    var vmgame = new Service_RegisS2();
                    string vmGameResult = vmgame.BigPromotionRegis(userId, "BigPro123!@#Tqscd");

                    log.Debug(" ");
                    log.Debug(" ");
                    log.Debug("-------------------- BIG PROMOTION VmGameResult (Already) IMPORT -------------------------");
                    log.Debug("User_ID: " + userId);
                    log.Debug("vmGameResult: " + vmGameResult);
                    log.Debug(" ");
                    log.Debug(" ");


                    #endregion

                    //#region Shot And Print

                    //var shot = new S2Process();
                    //string shotResult = shot.BPRegister(userId, RandomActiveCode.Generate(10), "SMS", "4", "SMS");

                    //log.Debug(" ");
                    //log.Debug(" ");
                    //log.Debug("-------------------- BIG PROMOTION shotResult (Already) IMPORT -------------------------");
                    //log.Debug("User_ID: " + userId);
                    //log.Debug("shotResult: " + shotResult);
                    //log.Debug(" ");
                    //log.Debug(" ");

                    //#endregion

                    #region DK NC1

                    var ringTone = new NC1_Handler();
                    string ringToneRes = ringTone.SyncSubscriptionData("949", "DK", userId, "DK GOI", RandomActiveCode.Generate(10), "472", "0", "2", "DK GOI");

                    log.Debug(" ");
                    log.Debug(" ");
                    log.Debug("-------------------- BIG PROMOTION ringToneRes REGIS (Already) IMPORT -------------------------");
                    log.Debug("User_ID: " + userId);
                    log.Debug("ringToneRes: " + ringToneRes);
                    log.Debug(" ");
                    log.Debug(" ");

                    #endregion

                    //messageReturn = "Chuc mung quy khach da dang ky CTKM trai nghiem dich vu GTGT, trung thuong SH sanh dieu va nhieu ipad mini. Qkhach duoc su dung mien phi 5 ngay goi dich vu ( bao gom game portal, shot and print, nhac chuong). Sau khi het khuyen mai 15 ngay, he thong se tu dong huy toan bo dich vu cho khach hang. De huy dich vu soan HUY GOI gui 949 ";
                    messageReturn = "Quy khach da dang ky thanh cong CTKM: trai nghiem dich vu MIEN PHI – CO HOI trung SH sanh dieu va nhieu iPad mini. Su dung MIEN PHI 5 ngay cac dich vu: nhac chuong hay(2000d/ngay); Choi game hap dan – http://vmgame.vn (10,000d/7 ngay); De huy dich vu soan HUY GOI gui 949";

                    SendMtThanhNu(userId, messageReturn, "949", "GOI", "0");

                }


                #endregion

                return "1";
            }
            catch (Exception ex)
            {
                log.Debug(" ");
                log.Debug(" ");
                log.Debug("LOI IMPORT User: " + ex.Message);
                log.Debug(" ");
                log.Debug(" ");
                return "0";
            }
           
        }

        if (userName == "import_del" && password == "123!@#")
        {

            try
            {
                #region HUY DICH VU

                log.Debug(" ");
                log.Debug(" ");
                log.Debug("-------------------- BIG PROMOTION DELETE IMPORT -------------------------");
                log.Debug("User_ID: " + userId);
                log.Debug(" ");
                log.Debug(" ");

                string message;
                DataTable dt = ViSport_S2_Registered_UsersController.ThanhNuRegisterUserStatusUpdate(userId, 0);

                if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                {
                    //message = "Quy khach da huy thanh cong goi dich vu ( bao gom game portal, shot and print, nhac chuong). Ma du thuong cua Qkhach se khong duoc tham gia quay thuong. De dang ky lai dich vu soan GOI gui 949";

                    #region HUY VMGAME

                    var vmgame = new Service_RegisS2();
                    string vmRes = vmgame.BigPromotionDelete(userId, "BigPro123!@#Tqscd");

                    log.Debug(" ");
                    log.Debug(" ");
                    log.Debug("-------------------- BIG PROMOTION VmGameResult DELETE IMPORT -------------------------");
                    log.Debug("User_ID: " + userId);
                    log.Debug("vmGameResult: " + vmRes);
                    log.Debug(" ");
                    log.Debug(" ");

                    #endregion

                    //#region HUY SHOT and PRINT

                    //var shot = new S2Process();
                    //string shotRes = shot.BPCancel(userId, "4", "HUY GOI 949");

                    //log.Debug(" ");
                    //log.Debug(" ");
                    //log.Debug("-------------------- BIG PROMOTION shotResult DELETE IMPORT -------------------------");
                    //log.Debug("User_ID: " + userId);
                    //log.Debug("shotResult: " + shotRes);
                    //log.Debug(" ");
                    //log.Debug(" ");

                    //#endregion

                    #region HUY NC1

                    var ringTone = new NC1_Handler();
                    string ringToneRest = ringTone.SyncSubscriptionData("949", "DK", userId, "DK GOI", "0", "472", "0", "0", "HUY GOI");

                    log.Debug(" ");
                    log.Debug(" ");
                    log.Debug("-------------------- BIG PROMOTION ringToneRes DELETE IMPORT -------------------------");
                    log.Debug("User_ID: " + userId);
                    log.Debug("ringToneRest: " + ringToneRest);
                    log.Debug(" ");
                    log.Debug(" ");

                    #endregion

                    //SendMtThanhNu(userId, message, "949", "GOI", RandomActiveCode.Generate(10));

                    return "1";
                }
                else
                {
                    return "-1";
                }

                #endregion

                
            }
            catch (Exception ex)
            {
                log.Error("Loi Huy GOI 949 IMPORT : " + ex);
                return "-1";
            }

        }

        return "0";
    }

    [WebMethod]
    public string ImportUser(string userId,string userName,string passWord)
    {
        log.Debug(" ");
        log.Debug(" ");
        log.Debug("-------------------- IMPORT USER CALL FROM  PARTNER -------------------------");
        log.Debug("User_ID: " + userId);
        log.Debug("userName: " + userName);
        log.Debug("password: " + passWord);
        log.Debug(" ");
        log.Debug(" ");

        userName = userName.Trim().ToLower();
        passWord = passWord.Trim().ToLower();
        //string message;

        if (userName == "tpbd" && passWord == "tpbd!@#")
        {
            #region DK DICH VU

            var entity = new ViSport_S2_Registered_UsersInfo();
            entity.User_ID = userName;
            entity.Request_ID = "0";
            entity.Service_ID = "979";
            entity.Command_Code = "TP1";
            entity.Service_Type = 1;
            entity.Charging_Count = 0;
            entity.FailedChargingTimes = 0;
            entity.RegisteredTime = DateTime.Now;
            entity.ExpiredTime = DateTime.Now.AddDays(1);
            entity.Registration_Channel = "IMPORT";
            entity.Status = 1;
            entity.Operator = "vnmobile";
            entity.Point = 2;

            DataTable value = ViSport_S2_Registered_UsersController.InsertSportGameHeroRegisterUser(entity);

            if (value.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
            {

                #region SINH MA_DU_THUONG

                string code1 = RandomActiveCode.Generate(8);
                string code2 = RandomActiveCode.Generate(8);
                ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(userName, code1);
                ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(userName, code2);

                return "1";

                #endregion

                //message = "Chuc mung ban da tham gia Trieu phu bong da cua Vietnamobile. Ban co 2 ma du thuong de co co hoi trung thuong 30 trieu dong tien mat, iPhone 5S va nhieu giai thuong hap dan khac, moi ngay ban se duoc tra loi cau hoi va du doan ket qua de  nang cao co hoi trung thuong (5000d/ngay). De kiem tra MDT truy cap: http://visport.vn De huy dvu soan: HUY TP gui 979. HT: 19001255";
                //SendMtSportGameHero(userId, message, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1
            }

            if (value.Rows[0]["RETURN_ID"].ToString() == "1")//DA DK DICH VU
            {
                return "0";
                //message = "Ban da dk tham gia ctrinh Trieu phu bong da cua Vietnamobile. Hang ngay, ban se nhan duoc cau hoi de tich luy diem. De biet them thong tin soan: HD gui 979. HT: 19001255";
                //SendMtSportGameHero(userId, message, Service_ID, Command_Code, Request_ID, 0);
            }

            #endregion

        }

        if (userName == "tpbd_del" && passWord == "tpbd!@#")
        {
            #region HUY DV TRIEU_PHU_BONG_DA

            DataTable dtUpdate = ViSport_S2_Registered_UsersController.UpdateSportGameHeroRegisterUser(userId, 0);
            if (dtUpdate != null && dtUpdate.Rows.Count > 0)
            {
                if (dtUpdate.Rows[0]["RETURN_ID"].ToString() == "1")
                {
                    return "1";
                }
            }

            #endregion
        }

        return "0";
    }

    [WebMethod]
    public string WsBigPromotionMoProcess(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExecuteRequestMoBigPromotionProcess(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }

    #endregion


    #region Methods Process Mo

    private string ExecuteRequestMoBigPromotionProcess(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
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
            log.Debug("-------------------- BIG PROMOTION -------------------------");
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
            SMS_MODB.InsertThanhNuMo(moInfo);

            #endregion

            string messageReturn;

            if (Command_Code.Trim().ToUpper() == "GOI" && subcode == "")
            {

                #region DK DICH VU BIG PROMOTION

                var entity = new ThanhNuRegisteredUsers();

                entity.UserId = User_ID;
                entity.RequestId = Request_ID;
                entity.ServiceId = Service_ID;
                entity.CommandCode = Command_Code;
                entity.ServiceType = 1;
                entity.ChargingCount = 0;
                entity.FailedChargingTimes = 0;
                entity.RegisteredTime = DateTime.Now;
                entity.ExpiredTime = DateTime.Now.AddDays(5);
                entity.RegistrationChannel = "SMS";
                entity.Status = 1;
                entity.Operator = "vnmobile";

                DataTable value = ViSport_S2_Registered_UsersController.ThanhNuRegisterUserInsert(entity);

                if (value.Rows[0]["RETURN_ID"].ToString() == "0")
                {
                    
                    string code1 = RandomActiveCode.Generate(8);
                    string code2 = RandomActiveCode.Generate(8);

                    //messageReturn = "Chuc mung quy khach da dang ky CTKM trai nghiem dich vu GTGT, trung thuong SH sanh dieu va nhieu ipad mini. Qkhach duoc su dung mien phi 5 ngay goi dich vu ( bao gom game portal, shot and print, nhac chuong) va nhan 2 MDT: " + code1 + ", " + code2 + " de tham gia quay thuonng vao cuoi chuong trinh. Sau khi het khuyen mai 15 ngay, he thong se tu dong huy toan bo dich vu cho khach hang. De huy dich vu soan HUY GOI gui 949 ";

                    messageReturn = "Quy khach da dang ky thanh cong CTKM: trai nghiem dich vu MIEN PHI – CO HOI trung SH sanh dieu va nhieu iPad mini. Quy khach duoc nhan 2 MDT: " + code1 + ", " + code2 + " de tham gia quay thuong vao cuoi chuong trinh va su dung MIEN PHI 5 ngay cac dich vu: nhac chuong hay(2000d/ngay); Choi game hap dan – http://vmgame.vn (10,000d/7 ngay); De huy dich vu soan HUY GOI gui 949";

                    SendMtThanhNu(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);

                    #region LUU MDT

                    ViSport_S2_Registered_UsersController.ThanhNuRegisterUserCodeInsert(User_ID, code1, Command_Code.ToUpper());
                    ViSport_S2_Registered_UsersController.ThanhNuRegisterUserCodeInsert(User_ID, code2, Command_Code.ToUpper());

                    #endregion

                    #region DK VMGAME

                    var vmgame = new Service_RegisS2();
                    string vmGameResult = vmgame.BigPromotionRegis(User_ID, "BigPro123!@#Tqscd");

                    log.Debug(" ");
                    log.Debug(" ");
                    log.Debug("-------------------- BIG PROMOTION VmGameResult REGIS -------------------------");
                    log.Debug("User_ID: " + User_ID);
                    log.Debug("vmGameResult: " + vmGameResult);
                    log.Debug(" ");
                    log.Debug(" ");


                    #endregion

                    //#region Shot And Print

                    //var shot = new S2Process();
                    //string shotResult = shot.BPRegister(User_ID, Request_ID, "SMS", "4", "SMS");

                    //log.Debug(" ");
                    //log.Debug(" ");
                    //log.Debug("-------------------- BIG PROMOTION shotResult REGIS -------------------------");
                    //log.Debug("User_ID: " + User_ID);
                    //log.Debug("shotResult: " + shotResult);
                    //log.Debug(" ");
                    //log.Debug(" ");

                    //#endregion

                    #region NC1
                    
                    var ringTone = new NC1_Handler();
                    string ringToneRes = ringTone.SyncSubscriptionData("949","DK",User_ID,Message.ToUpper(),Request_ID,"472","0","1","DK GOI");

                    log.Debug(" ");
                    log.Debug(" ");
                    log.Debug("-------------------- BIG PROMOTION ringToneRes REGIS -------------------------");
                    log.Debug("User_ID: " + User_ID);
                    log.Debug("ringToneRes: " + ringToneRes);
                    log.Debug(" ");
                    log.Debug(" ");

                    #endregion

                }
                else if (value.Rows[0]["RETURN_ID"].ToString() == "1")
                {
                    messageReturn = "Ban da dang ky thanh cong dv Big Promotion. Xin cam on";
                    SendMtThanhNu(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                }
                else if (value.Rows[0]["RETURN_ID"].ToString() == "2")
                {

                    messageReturn = "Ban da dang ky thanh cong dv Big Promotion. Xin cam on";
                    SendMtThanhNu(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);

                    #region DK VMGAME

                    var vmgame = new Service_RegisS2();
                    string vmGameResult = vmgame.BigPromotionRegis(User_ID, "BigPro123!@#Tqscd");

                    log.Debug(" ");
                    log.Debug(" ");
                    log.Debug("-------------------- BIG PROMOTION VmGameResult (Already) -------------------------");
                    log.Debug("User_ID: " + User_ID);
                    log.Debug("vmGameResult: " + vmGameResult);
                    log.Debug(" ");
                    log.Debug(" ");


                    #endregion

                    //#region Shot And Print

                    //var shot = new S2Process();
                    //string shotResult = shot.BPRegister(User_ID, Request_ID, "SMS", "4", "SMS");

                    //log.Debug(" ");
                    //log.Debug(" ");
                    //log.Debug("-------------------- BIG PROMOTION shotResult (Already) -------------------------");
                    //log.Debug("User_ID: " + User_ID);
                    //log.Debug("shotResult: " + shotResult);
                    //log.Debug(" ");
                    //log.Debug(" ");

                    //#endregion

                    #region DK NC1

                    var ringTone = new NC1_Handler();
                    string ringToneRes = ringTone.SyncSubscriptionData("949", "DK", User_ID, Message.ToUpper(), Request_ID, "472", "0", "2", "DK GOI");

                    log.Debug(" ");
                    log.Debug(" ");
                    log.Debug("-------------------- BIG PROMOTION ringToneRes REGIS (Already) -------------------------");
                    log.Debug("User_ID: " + User_ID);
                    log.Debug("ringToneRes: " + ringToneRes);
                    log.Debug(" ");
                    log.Debug(" ");

                    #endregion

                }
                
                #endregion

            }
            else
            {
                #region SAI CU PHAP

                messageReturn = "Tin nhan sai cu phap, soan tin GOI gui 949. Xin cam on";
                SendMtThanhNu(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);

                #endregion
            }
        }
        catch (Exception ex)
        {
            log.Debug("--------------- ERROR LOG BIG PROMOTION ----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }

        return responseValue;
    }

    private string ExcuteRequestMoVtv6WorldCupProcess(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        string responseValue = "1";
        int returnValue = 0;

        Message = Message.Trim().ToUpper();
        string subcode = "";

        Command_Code = Command_Code.ToUpper();

        if (Message.Trim().Length > Command_Code.Trim().Length)
        {
            subcode = Message.ToUpper().Substring(Command_Code.Length).Trim();
        }

        try
        {
            log.Debug(" ");
            log.Debug(" ");
            log.Debug("-------------------- VTV6 World Cup Mo Log -------------------------");
            log.Debug("User_ID: " + User_ID);
            log.Debug("Service_ID: " + Service_ID);
            log.Debug("Command_Code: " + Command_Code);
            log.Debug("Message: " + Message.ToUpper());
            log.Debug("Request_ID: " + Request_ID);
            log.Debug(" ");
            log.Debug(" ");

            #region MO LOG

            Vtv6WorldCupLogMo(User_ID, Service_ID, Command_Code, Message, Request_ID, 0);

            #endregion

            string mtContent = string.Empty;

            int msgType = (int) Constant.MessageType.NoCharge;

            if(Command_Code == "VTV" && subcode == "")
            {
                #region DK VTV6 World Cup

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
                entity.Registration_Channel = "VTV";
                entity.Status = 1;
                entity.Operator = GetTelco(User_ID);
                entity.Point = 0;

                DataTable dtUser = ViSport_S2_Registered_UsersController.WorldCupRegisterUserVtv6(entity);

                if (dtUser.Rows[0]["RETURN_ID"].ToString() == "0")
                {
                    //GOI API lay PASS ben DOITAC
                    var post = new PostSubmitter();
                    post.Url = "http://worldcup.visport.vn/TelcoApi/service.php?action=VMGsms&command=VMG&message=" + "dang-ky-vtv6-wc" + "&msisdn=" + User_ID + "&short_code=dau-SMS-VMG";
                    post.Type = PostSubmitter.PostTypeEnum.Get;
                    string result = post.Post();

                    mtContent = result;
                }
                else if (dtUser.Rows[0]["RETURN_ID"].ToString() == "1")
                {
                    mtContent = "Ban da dang ky chuong trinh DONG HANH CUNG WORLD CUP truoc do";
                }

                Vtv6WorldCupSendMt(User_ID, Service_ID, Command_Code, mtContent, Request_ID, msgType.ToString());

                #endregion
            }
            else
            {
                mtContent = "Tin nhan sai cu phap. Soan VTV gui 979 de dang ky dich vu";
                Vtv6WorldCupSendMt(User_ID, Service_ID, Command_Code, mtContent, Request_ID, msgType.ToString());
            }

        }
        catch (Exception ex)
        {
            log.Debug("---------------Error VTV6 World Cup----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }

        return responseValue;

    }
    
    private string ExecuteRequestMo949Process(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        string responseValue = "1";
        int returnValue = 0;

        Message = Message.ToUpper();
        string subcode = "";

        Command_Code = Command_Code.ToUpper();

        if (Message.Trim().Length > Command_Code.Trim().Length)
        {
            subcode = Message.ToUpper().Substring(Command_Code.Length).Replace(" ", "");
        }

        try
        {
            log.Debug(" ");
            log.Debug(" ");
            log.Debug("--------------------MO 949 Log-------------------------");
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
            SMS_MODB.InsertMo949Mo(moInfo);

            #endregion

            string msgReturn;
            string messageContent;
            string msg = "";
            string price = "0";
            string url = "";

            if (Command_Code.ToUpper() == "GAMEHOT" && subcode == "")
            {
                msgReturn = PaymentVnmWapChargingOptimize("10000", User_ID, Command_Code);
                string[] msgResult = msgReturn.Split('|');
                msg = msgResult[0].Trim();
                price = msgResult[1].Trim();

                if(msg == "1")//CHARGED THANH CONG
                {
                    DataTable dtGame = VoteRegisterController.Mo949GetRandomGame();
                    url = "";
                    if(dtGame != null && dtGame.Rows.Count > 0)
                    {
                        try
                        {
                            var urlservice = new VMGGame.MOReceiver();
                            url = urlservice.VMG_ReturnUrlForGame(ConvertUtility.ToString(dtGame.Rows[0]["GID"]), 0, User_ID, ConvertUtility.ToInt32(dtGame.Rows[0]["Partner_ID"]), "XZONE", "WAP", "vnmobile", "WAP.XZONE.VN", "", "");
                            int indexofhttp = url.IndexOf("http://");
                            if (indexofhttp == -1)
                            {
                                url = "http://" + url;
                            }
                            else
                            {
                                url = url.Substring(indexofhttp);
                            }
                        }
                        catch (Exception ex) { url = ""; }
                    }

                    messageContent = "Ban da mua GAME thanh cong. Click vao link sau de tai ve may " + url;
                    SendMtMo949(User_ID,messageContent,Service_ID,Command_Code,Request_ID);
                }
            }
            else if(Command_Code.ToUpper() == "NCHAY" && subcode == "")
            {
                msgReturn = PaymentVnmWapChargingOptimize("10000", User_ID, Command_Code);
                string[] msgResult = msgReturn.Split('|');
                msg = msgResult[0].Trim();
                price = msgResult[1].Trim();

                if (msg == "1")//CHARGED THANH CONG
                {
                    DataTable dtMusic = VoteRegisterController.Mo949GetRandomMusic();
                    if (dtMusic != null && dtMusic.Rows.Count > 0)
                    {
                      url = GetVnmDownloadItem(GetTelco(User_ID), "22", dtMusic.Rows[0]["W_MItemID"].ToString(), AppEnv.MD5Encrypt(dtMusic.Rows[0]["W_MItemID"].ToString()));
                    }

                    messageContent = "Ban da mua Nhac Chuong thanh cong. Click vao link sau de tai ve may " + url;
                    SendMtMo949(User_ID, messageContent, Service_ID, Command_Code, Request_ID);
                }
            }
            else if(Command_Code.ToUpper() == "VIDEOHAY" && subcode == "")
            {
                msgReturn = PaymentVnmWapChargingOptimize("2000", User_ID, Command_Code);
                string[] msgResult = msgReturn.Split('|');
                msg = msgResult[0].Trim();
                price = msgResult[1].Trim();

                if (msg == "1")//CHARGED THANH CONG
                {
                    DataTable dtVideo = VoteRegisterController.Mo949GetRandomVideo();
                    if (dtVideo != null && dtVideo.Rows.Count > 0)
                    {
                        url = GetDownloadItem(GetTelco(User_ID), "5", dtVideo.Rows[0]["W_VItemID"].ToString(), AppEnv.MD5Encrypt(dtVideo.Rows[0]["W_VItemID"].ToString()));
                    }

                    messageContent = "Ban da mua Video hot thanh cong. Click vao link sau de tai ve may " + url;
                    SendMtMo949(User_ID, messageContent, Service_ID, Command_Code, Request_ID);
                }
            }
            else if(Command_Code.ToUpper() == "TRUYENHOT" && subcode == "")
            {
                msgReturn = PaymentVnmWapChargingOptimize("5000", User_ID, Command_Code);
                string[] msgResult = msgReturn.Split('|');
                msg = msgResult[0].Trim();
                price = msgResult[1].Trim();

                if (msg == "1")//CHARGED THANH CONG
                {
                    string key = DateTime.Now.ToString("yyyyMMdd");
                    string en = AppEnv.MD5Encrypt(key);
                    DataTable dtTruyen = VoteRegisterController.Mo949GetRandomVideo();
                    if (dtTruyen != null && dtTruyen.Rows.Count > 0)
                    {
                        url = "http://wap.vietnamobile.com.vn/thugian/truyenmoi.aspx?k=" + en;
                    }

                    messageContent = "Ban da mua Truyen Hot thanh cong. Click vao link sau de doc truyen " + url;
                    SendMtMo949(User_ID, messageContent, Service_ID, Command_Code, Request_ID);
                }
            }

            #region Log Doanh Thu

            var e = new VoteChargedUserLogInfo();
            e.User_ID = User_ID;
            e.Request_ID = Request_ID;
            e.Service_ID = Service_ID;
            e.Command_Code = Command_Code;
            e.Service_Type = 1;

            e.RegisteredTime = DateTime.Now;
            e.Registration_Channel = "SMS";
            e.Operator = GetTelco(User_ID);

            if(msg == "1")
            {
                e.Reason = "Succ";
            }
            else
            {
                e.Reason = msg;
            }
            e.Price = ConvertUtility.ToInt32(price);

            VoteRegisterController.Mo949ChargedUserLogInsert(e);

            #endregion

            #region Log De reCharged

            if (msg != "1")//GHI REGIS DE reCharged
            {
                var info = new VoteRegisteredInfo();
                info.User_ID = User_ID;
                info.Request_ID = Request_ID;
                info.Service_ID = Service_ID;
                info.Command_Code = Command_Code;
                info.Service_Type = 1;

                info.FailedChargingTime = 0;
                info.RegisteredTime = DateTime.Now;
                info.Registration_Channel = "SMS";
                info.Status = 1;
                info.Operator = GetTelco(User_ID);

                VoteRegisterController.Mo949RegisterInsert(info);

                messageContent = "Giao dich khong thanh cong hoac thue bao khong du tien. Vui long thu lai !";
                SendMtMo949(User_ID,messageContent,Service_ID,Command_Code,Request_ID);
            }

            #endregion
        }
        catch (Exception ex)
        {
            log.Debug("---------------Error sentMT----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }

        return responseValue;
    }

    private string ExcecuteRequestMoThanhNu(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
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
            log.Debug("--------------------GAME THANH NU-------------------------");
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
            SMS_MODB.InsertThanhNuMo(moInfo);

            #endregion

            string messageReturn;

            if(Command_Code.Trim().ToUpper() == "DK" && subcode.Trim().ToUpper() == "TN")
            {
                var entity = new ThanhNuRegisteredUsers();

                entity.UserId = User_ID;
                entity.RequestId = Request_ID;
                entity.ServiceId = Service_ID;
                entity.CommandCode = Command_Code;
                entity.ServiceType = 1;
                entity.ChargingCount = 0;
                entity.FailedChargingTimes = 0;
                entity.RegisteredTime = DateTime.Now;
                entity.ExpiredTime = DateTime.Now.AddDays(1);
                entity.RegistrationChannel = "SMS";
                entity.Status = 1;
                entity.Operator = GetTelco(User_ID);

                #region GOI HAM DK BEN DOI TAC

                string partnerResult = AppEnv.ThanhNuDangKy(User_ID);

                log.Debug(" ");
                log.Debug("**********");
                log.Debug("Partner_Thanh_Nu_smsKichHoat : " + partnerResult);
                log.Debug("**********");
                log.Debug(" ");

                string[] arrValue = partnerResult.Split('|');
                if(arrValue[0].Trim() == "1")
                {
                    ViSport_S2_Registered_UsersController.ThanhNuRegisterUserStatusUpdate(User_ID, 1);
                    messageReturn = "Ban da la thanh vien cua Game Thanh Nu. Click vao link sau de dang nhap Game " + arrValue[1];
                    SendMtThanhNu(User_ID, messageReturn, Service_ID, Command_Code, Request_ID); // LAN 1
                }
                else if(arrValue[0].Trim() == "0")
                {
                    DataTable value = ViSport_S2_Registered_UsersController.ThanhNuRegisterUserInsert(entity);
                    if (value.Rows[0]["RETURN_ID"].ToString() == "0")
                    {
                        messageReturn ="Chuc mung Quy Khach da dang ky thanh cong Game Thanh Nu Gia cuoc 1000d-ngay, Goi dich vu se duoc tu dong gia han hang ngay. De huy dang ky, Quy Khach soan HUY TN gui 2288.";
                        SendMtThanhNu(User_ID, messageReturn, Service_ID, Command_Code, Request_ID); // LAN 1

                        messageReturn = "Kich hoat tai khoan " + arrValue[1];
                        SendMtThanhNu(User_ID, messageReturn, Service_ID, Command_Code, Request_ID); // LAN 2
                    }
                    else if (value.Rows[0]["RETURN_ID"].ToString() == "1")
                    {
                        ViSport_S2_Registered_UsersController.ThanhNuRegisterUserStatusUpdate(User_ID, 1);
                        messageReturn = "Ban da la thanh vien cua Game Thanh Nu. Click vao link sau de dang nhap Game " + arrValue[1];
                        SendMtThanhNu(User_ID, messageReturn, Service_ID, Command_Code, Request_ID); // LAN 1
                    }
                }

                #endregion
            }
            else if(Command_Code.Trim().ToUpper() == "HUY" && subcode.Trim().ToUpper() == "TN")
            {

                #region GOI HAM HUY BEN DOI TAC

                string partnerResult = AppEnv.ThanhNuHuy(User_ID);
                if(partnerResult.Trim() == "1")
                {
                    DataTable value = ViSport_S2_Registered_UsersController.ThanhNuRegisterUserStatusUpdate(User_ID, 0);
                    if (value.Rows[0]["RETURN_ID"].ToString() == "1")
                    {
                        messageReturn ="Toan bo tai khoan Game Thanh Nu cua Quy khach se bi huy.De dang ki lai Qk vui long soan tin DK TN gui 2288";
                        SendMtThanhNu(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                    }
                }

                #endregion
            }
        }
        catch (Exception ex)
        {
            //responseValue = "1";
            log.Debug("---------------Error sentMT----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }

        return "1";
    }

    private string ExcecuteRequestMoSportGame(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        string responseValue = "1";
        int returnValue = 0;

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
            

            #endregion

            string messageReturn;

            if (Command_Code.ToUpper() == "BD" && subcode == "") //DK DICH VU ANH_TAI_BONG_DA
            {
                return responseValue;

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
                entity.Registration_Channel = "SMS";
                entity.Status = 1;
                entity.Operator = GetTelco(User_ID);
                entity.Point = 100;

                DataTable value = ViSport_S2_Registered_UsersController.InsertSportGameHeroRegisterUser(entity);

                if (value.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
                {
                    messageReturn = AppEnv.GetSetting("AnhTaiBongDa_DangKyThanhCong");
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1

                    DataTable dtQuestion = ViSport_S2_Registered_UsersController.GetQuestionInfoSportGameHero();
                    if (dtQuestion != null && dtQuestion.Rows.Count > 0)
                    {
                        messageReturn = dtQuestion.Rows[0]["Question"].ToString();

                        int questionId = ConvertUtility.ToInt32(dtQuestion.Rows[0]["Id"].ToString());
                        string answer = dtQuestion.Rows[0]["Answer"].ToString();

                        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 1); //SEND MT LAN 2 : GUI CAU HOI DAU TIEN
                        ViSport_S2_Registered_UsersController.InsertSportGameHeroAnswerLog(User_ID, questionId, messageReturn, answer, DateTime.Now, 0); // LUU LOG Question
                    }
                }
                else if (value.Rows[0]["RETURN_ID"].ToString() == "1")//DA DK DICH VU
                {
                    ViSport_S2_Registered_UsersController.UpdateSportGameHeroRegisterUser(User_ID, 1);//CAP NHAT TRANG THAI (Status = 1) NEU DANG O TRANG THAI HUY

                    messageReturn = AppEnv.GetSetting("AnhTaiBongDa_DoubleDangKy");
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                }

                #endregion

            }
            else if( (Command_Code == "TP" || Command_Code == "TP1" ) && subcode == "") //DK DICH VU TRIEU_PHU_BONG_DA
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
                entity.Registration_Channel = "SMS";
                entity.Status = 1;
                entity.Operator = GetTelco(User_ID);
                entity.Point = 2;
                
                DataTable value = ViSport_S2_Registered_UsersController.InsertSportGameHeroRegisterUser(entity);

                if (value.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
                {

                    #region SINH MA_DU_THUONG

                    string code1 = RandomActiveCode.Generate(8);
                    string code2 = RandomActiveCode.Generate(8);
                    ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(User_ID, code1);
                    ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(User_ID, code2);

                    #endregion

                    if(Command_Code == "TP")
                    {
                        //messageReturn = "Chuc mung ban da tham gia Trieu phu bong da cua Vietnamobile. Ban co 2 ma du thuong de co co hoi trung thuong 30 trieu dong tien mat, iPhone 5S va nhieu giai thuong hap dan khac, moi ngay ban se duoc tra loi cau hoi va du doan ket qua de  nang cao co hoi trung thuong (5000d/ngay). De kiem tra MDT truy cap: http://visport.vn De huy dvu soan: HUY TP gui 979. HT: 19001255";
                        messageReturn ="Chuc mung ban da tham gia Trieu phu bong da cua Vietnamobile. Ban co 2 ma du thuong de co co hoi trung thuong 1 dien thoai Samsung galaxy S4 , moi ngay ban se duoc tra loi cau hoi va du doan ket qua de nang cao co hoi trung thuong (5000d/ngay). De kiem tra MDT truy cap: http://visport.vn De huy dvu soan: HUY TP gui 979. HT: 19001255";
                    }
                    else
                    {
                        //messageReturn = "Ban la khach hang may may duoc tham gia CTKM Trieu phu bong da cua Vietnamobile. Ban se duoc trai nghiem mien phi dich vu trong 3 ngay va co 2 ma du thuong de co co hoi trung thuong 30 trieu dong tien mat, iPhone 5S va nhieu giai thuong hap dan khac, moi ngay ban se duoc tra loi cau hoi va du doan ket qua de  nang cao co hoi trung thuong (5000d/ngay). De kiem tra MDT truy cap: http://visport.vn. Sau khi het khuyen mai 15 ngay, he thong se tu dong huy toan bo dich vu cho khach hang. De huy dich vu soan: HUY TP1 gui 979. HT: 19001255";
                        messageReturn ="Ban la khach hang may may duoc tham gia CTKM Trieu phu bong da cua Vietnamobile. Ban se duoc trai nghiem mien phi dich vu trong 3 ngay va co 2 ma du thuong de co co hoi trung thuong samsung galaxy S4, moi ngay ban se duoc tra loi cau hoi va du doan ket qua de nang cao co hoi trung thuong (5000d/ngay). De kiem tra MDT truy cap: http://visport.vn. Sau khi het khuyen mai 3 ngay, he thong se tu dong huy toan bo dich vu cho khach hang. De huy dich vu soan: HUY TP1 gui 979. HT: 19001255";
                    }
                    
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1
                }
                else if (value.Rows[0]["RETURN_ID"].ToString() == "1")//DA DK DICH VU
                {
                    ViSport_S2_Registered_UsersController.UpdateSportGameHeroRegisterUser(User_ID, 1);//CAP NHAT TRANG THAI (Status = 1) NEU DANG O TRANG THAI HUY

                    messageReturn = "Ban da dk tham gia ctrinh Trieu phu bong da cua Vietnamobile. Hang ngay, ban se nhan duoc cau hoi de tich luy diem. De biet them thong tin soan: HD gui 979. HT: 19001255";
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                }

                #endregion
            }
            else if (Command_Code.ToUpper() == "DIEM" && subcode == "") //TRA MADUTHUONG
            {
                return responseValue;

                #region TRA CUU DIEM

                DataTable dt = ViSport_S2_Registered_UsersController.GetSportGameHeroUserInfo(User_ID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    int point = ConvertUtility.ToInt32(dt.Rows[0]["Point"].ToString());
                    int somaduthuong = point / 10;

                    messageReturn = AppEnv.GetSetting("AnhTaiBongDa_KiemTraMaDuThuongThanhCong").Replace("xxxx", point.ToString());
                    messageReturn = messageReturn.Replace("yyyy", somaduthuong.ToString());

                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                }

                #endregion
            }
            else if(Command_Code.ToUpper() == "HD" && subcode == "") //HUONG DAN CHUONG TRINH
            {
                //messageReturn = AppEnv.GetSetting("AnhTaiBongDa_HuongDanThanhCong");
                messageReturn =
                    "De dky tham gia chuong trinh trieu phu bong da soan: TP gui 979, huy dich vu soan: HUY TP gui 979, xem ma du thuong truy cap http://visport.vn";

                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
            }
            else if ((Command_Code.ToUpper() == "TC" && subcode.ToUpper() == "") || (Command_Code.ToUpper() == "HUY" && subcode.ToUpper() == "BD")) //HUY CHUONG TRINH
            {
                #region HUY DV ANH_TAI_BONG_DA

                DataTable dtUpdate = ViSport_S2_Registered_UsersController.UpdateSportGameHeroRegisterUser(User_ID, 0);
                if (dtUpdate != null && dtUpdate.Rows.Count > 0)
                {
                    if (dtUpdate.Rows[0]["RETURN_ID"].ToString() == "1")
                    {
                        messageReturn = AppEnv.GetSetting("AnhTaiBongDa_HuyDichVuThanhCong");
                        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                    }
                }

                #endregion
            }
            else if (Command_Code.ToUpper() == "HUY" && (subcode.ToUpper() == "TP" || subcode.ToUpper() == "TP1" )) //HUY DV TRIEU_PHU_BONG_DA
            {
                #region HUY DV TRIEU_PHU_BONG_DA

                DataTable dtUpdate = ViSport_S2_Registered_UsersController.UpdateSportGameHeroRegisterUser(User_ID, 0);
                if (dtUpdate != null && dtUpdate.Rows.Count > 0)
                {
                    if (dtUpdate.Rows[0]["RETURN_ID"].ToString() == "1")
                    {
                        messageReturn = "Ban da huy thanh cong dich vu Trieu phu bong da cua Vietnamobile. Ma du thuong cua ban se duoc bao luu va tham gia quay thuong khi ban dang ky lai. De dang ky soan TP gui 979. HT: 19001255";
                        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                    }
                }

                #endregion
            }
            else if( (Command_Code.ToUpper() == "P1" || Command_Code.ToUpper() == "P2") && subcode == "" ) //TRA LOI CAU HOI
            {
                return responseValue;

                #region TRA LOI CAU HOI

                DataTable dtCountQues = ViSport_S2_Registered_UsersController.CountQuestionTodaySportGameHeroRegisterUser(User_ID);
                DataTable dtAnswer = ViSport_S2_Registered_UsersController.GetAnswerSportGameHero(User_ID);
                string answerMt = Command_Code.Trim().ToUpper();

                if (dtAnswer != null && dtAnswer.Rows.Count > 0)
                {
                    string answerDb = dtAnswer.Rows[0]["True_Answer"].ToString().Trim().ToUpper();
                    int questionId = ConvertUtility.ToInt32(dtAnswer.Rows[0]["Question_Id"].ToString());

                    if (answerMt == answerDb)//TRA LOI DUNG CAU HOI
                    {
                        DataTable randomMsg = ViSport_S2_Registered_UsersController.GetMessageRandomSportGameHero(1);
                        messageReturn = randomMsg.Rows[0]["Message"].ToString();

                        if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) == 6)
                        {
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi DUNG
                            //CONG DIEM 40Diem
                            ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUser(User_ID, questionId, 40, Request_ID, answerMt, 1);

                            messageReturn = AppEnv.GetSetting("AnhTaiBongDa_TraLoiHet6CauMienPhi");
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi Het 6 Cau FREE

                            SendContinueQuestion(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO
                        }
                        else if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) < 6)
                        {
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi DUNG
                            //CONG DIEM 40Diem
                            ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUser(User_ID, questionId, 40, Request_ID, answerMt, 1);
                            SendContinueQuestion(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO
                        }
                        else if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) > 6)
                        {
                            if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) == 15)
                            {
                                #region HET 15 CAU HOI

                                //CHARGED TU CAU THU 7
                                string result = PaymentViSportChargingOptimize(User_ID);

                                if (result == "1")// CHARGED THANH CONG
                                {
                                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi DUNG

                                    //CONG DIEM 40x2 Diem
                                    ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUserAdvance(User_ID, questionId, 80, Request_ID, answerMt, 1);

                                    messageReturn = "Ban da tra loi het 15 cau hoi trong ngay. Xin cam on va hen gap lai vao ngay mai !";
                                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao THUE BAO TRA LOI HET 15 Cau Hoi
                                }
                                else if (result.Trim() == AppEnv.GetSetting("NotEnoughMoney").Trim())
                                {
                                    messageReturn = "Tai khoan khong du tien. Moi ban nap them tien de tiep tuc choi";
                                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao THUE BAO HET TIEN
                                }
                                else
                                {
                                    messageReturn = "He thong dang nang cap. Ban vui long thu lai sau";
                                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao THUE BAO HE THONG BAN
                                }

                                ChargedLogInsert(User_ID, Request_ID, Service_ID, Command_Code, result);// LOG DOANH THU

                                #endregion
                            }
                            else
                            {
                                #region CHUA HET 15 CAU HOI

                                //CHARGED TU CAU THU 7
                                string result = PaymentViSportChargingOptimize(User_ID);

                                if (result == "1")// CHARGED THANH CONG
                                {
                                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi DUNG

                                    //CONG DIEM 40x2 Diem
                                    //ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUser(User_ID, questionId, 40, Request_ID, answerMt, 1);
                                    ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUserAdvance(User_ID, questionId, 80, Request_ID, answerMt, 1);

                                    SendContinueQuestion(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO
                                }
                                else if (result.Trim() == AppEnv.GetSetting("NotEnoughMoney").Trim())
                                {
                                    messageReturn = "Tai khoan khong du tien. Moi ban nap them tien de tiep tuc choi";
                                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao THUE BAO HET TIEN
                                }
                                else
                                {
                                    messageReturn = "He thong dang nang cap. Ban vui long thu lai sau";
                                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao HE THONG DANG NANG CAP
                                }

                                ChargedLogInsert(User_ID, Request_ID, Service_ID, Command_Code, result);// LOG DOANH THU

                                #endregion
                            }
                        }
                    }
                    else//TRA LOI SAI
                    {
                        DataTable randomMsg = ViSport_S2_Registered_UsersController.GetMessageRandomSportGameHero(0);
                        messageReturn = randomMsg.Rows[0]["Message"].ToString();

                        if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) == 6)
                        {
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi SAI
                            //CONG DIEM 20Diem
                            ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUser(User_ID, questionId, 20, Request_ID, answerMt, 0);

                            messageReturn = AppEnv.GetSetting("AnhTaiBongDa_TraLoiHet6CauMienPhi");
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi Het 6 Cau FREE

                            SendContinueQuestion(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO
                        }
                        else if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) < 6)
                        {
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi SAI
                            //CONG DIEM 20Diem
                            ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUser(User_ID, questionId, 20, Request_ID, answerMt, 0);

                            SendContinueQuestion(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO
                        }
                        else if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) > 6)
                        {

                            if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) == 15)
                            {
                                #region HET 15 CAU HOI

                                //CHARGED TU CAU THU 7
                                string result = PaymentViSportChargingOptimize(User_ID);

                                if (result == "1")// CHARGED THANH CONG
                                {
                                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi SAI

                                    //CONG DIEM -20x2 Diem
                                    ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUserAdvance(User_ID, questionId, 40, Request_ID, answerMt, 0);

                                    messageReturn = "Ban da tra loi het 15 cau hoi trong ngay. Xin cam on va hen gap lai vao ngay mai !";
                                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao THUE BAO TRA LOI HET 15 Cau Hoi
                                }
                                else
                                {
                                    messageReturn = "Tai khoan khong du tien. Moi ban nap them tien de tiep tuc choi";
                                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao THUE BAO HET TIEN
                                }

                                ChargedLogInsert(User_ID, Request_ID, Service_ID, Command_Code, result);// LOG DOANH THU

                                #endregion
                            }
                            else
                            {

                                #region CHUA HET 15 CAU HOI

                                //CHARGED TU CAU THU 7
                                string result = PaymentViSportChargingOptimize(User_ID);

                                if (result == "1")// CHARGED THANH CONG
                                {
                                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi SAI

                                    //CONG DIEM -20x2 Diem
                                    ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUserAdvance(User_ID, questionId, 40, Request_ID, answerMt, 0);

                                    SendContinueQuestion(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO
                                }
                                else
                                {
                                    messageReturn = "Tai khoan khong du tien. Moi ban nap them tien de tiep tuc choi";
                                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao THUE BAO HET TIEN
                                }

                                ChargedLogInsert(User_ID, Request_ID, Service_ID, Command_Code, result);// LOG DOANH THU

                                #endregion

                            }
                        }
                    }
                }

                #endregion
            }
            else if ((Command_Code.ToUpper() == "1" || Command_Code.ToUpper() == "2") && subcode == "")
            {
                string today = DateTime.Now.DayOfWeek.ToString();

                if(!CheckDayOfWeek(today))
                {
                    #region CAU HOI TPBD

                    DataTable dtCountQues = ViSport_S2_Registered_UsersController.CountQuestionTodaySportGameHeroRegisterUser(User_ID);
                    DataTable dtAnswer = ViSport_S2_Registered_UsersController.GetAnswerSportGameHero(User_ID);
                    string answerMt = Command_Code.Trim().ToUpper();

                    if (dtAnswer != null && dtAnswer.Rows.Count > 0)
                    {
                        string answerDb = dtAnswer.Rows[0]["True_Answer"].ToString().Trim().ToUpper();
                        int questionId = ConvertUtility.ToInt32(dtAnswer.Rows[0]["Question_Id"].ToString());

                        if (answerMt == answerDb)//TRA LOI DUNG CAU HOI
                        {
                            DataTable randomMsg = ViSport_S2_Registered_UsersController.GetMessageRandomSportGameHero(3);
                            messageReturn = randomMsg.Rows[0]["Message"].ToString();
                            if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) == 5)
                            {
                                string code1 = RandomActiveCode.Generate(8);
                                ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(User_ID, code1);

                                messageReturn = messageReturn.Replace("xxx", code1);
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi DUNG

                                //CONG 1 MDT
                                ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUserTp(User_ID, questionId, 40, Request_ID, answerMt, 1);

                                //messageReturn = "Ban da tra loi het so cau hoi mien phi trong ngay (5 cau). Hay tiep tuc tham gia chuong trinh vao ngay mai de tich luy ma du thuong de SO HUU iPhone 5S.";

                                messageReturn = "Ban da tra loi het so cau hoi mien phi trong ngay (5 cau). Hay tiep tuc tham gia chuong trinh vao ngay mai de tich luy ma du thuong de SO HUU Samsung Galaxy S4";
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi Het 5 Cau FREE

                                //SendContinueQuestion(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO
                            }
                            else if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) < 5)
                            {

                                string code1 = RandomActiveCode.Generate(8);
                                ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(User_ID, code1);

                                messageReturn = messageReturn.Replace("xxx", code1);
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi DUNG

                                //CONG 1 MDT
                                ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUserTp(User_ID, questionId, 40, Request_ID, answerMt, 1);

                                SendContinueQuestionTpBd(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO
                            }
                        }
                        else//TRA LOI SAI
                        {
                            DataTable randomMsg = ViSport_S2_Registered_UsersController.GetMessageRandomSportGameHero(4);
                            messageReturn = randomMsg.Rows[0]["Message"].ToString();

                            if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) == 5)
                            {
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi SAI
                                //CONG 0 MDT
                                ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUserTp(User_ID, questionId, 20, Request_ID, answerMt, 0);

                                //messageReturn = "Ban da tra loi het so cau hoi mien phi trong ngay (5 cau). Hay tiep tuc tham gia chuong trinh vao ngay mai de tich luy ma du thuong de SO HUU iPhone 5S.";

                                messageReturn = "Ban da tra loi het so cau hoi mien phi trong ngay (5 cau). Hay tiep tuc tham gia chuong trinh vao ngay mai de tich luy ma du thuong de SO HUU Samsung Galaxy S4";
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi Het 5 Cau FREE
                            }
                            else if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) < 5)
                            {
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi SAI
                                //CONG 0 MDT
                                ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUserTp(User_ID, questionId, 20, Request_ID, answerMt, 0);

                                SendContinueQuestionTpBd(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO
                            }
                        }

                    }

                    #endregion
                }
            }
            else if (Command_Code.ToUpper() == "KTDV" && subcode == "")
            {
                #region KTDV

                DataTable dtChk = ViSport_S2_Registered_UsersController.CheckUserRegisterAllService(User_ID);
                string[] values = dtChk.Rows[0]["RETURN_ID"].ToString().Trim().Split(',');
                string services = string.Empty;

                if (!string.IsNullOrEmpty(values[0]))
                {
                    foreach (var value in values)
                    {
                        if (value.Trim() == "anhtaibongda")
                        {
                            services = "Anh tai bong da";
                        }
                    }

                    messageReturn = AppEnv.GetSetting("Ktdv_Mt_Content");
                    messageReturn = messageReturn.Replace("[dichvu]", services);

                }
                else
                {
                    messageReturn = "Quy khach khong su dung dich vu nao tren dau so nay. Tran trong cam on";
                }

                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);

                #endregion
            }
            else if (Command_Code.ToUpper() == "HUY" && subcode.ToUpper() == "TBDV")
            {

                #region HUY TBDV

                DataTable dtChk = ViSport_S2_Registered_UsersController.UnCheckUserRegisterAllService(User_ID);
                string[] values = dtChk.Rows[0]["RETURN_ID"].ToString().Trim().Split(',');
                //string services = string.Empty;
                if (!string.IsNullOrEmpty(values[0]))
                {
                    messageReturn = "Quy khach da huy thanh cong toan bo dich vu da dang ky tren dau so 979. Tran trong cam on";
                }
                else
                {
                    messageReturn = "Quy khach khong su dung dich vu nao tren dau so nay. Tran trong cam on";
                }

                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);

                #endregion

            }
            else if (Command_Code.ToUpper() == "HDSD" || Command_Code.ToUpper() == "HD")
            {
                #region HDSD

                messageReturn = "Dich vu/dau so 979 dang cung cap cac goi dich vu sau: Goi dich vu Anh tai bong da. Gia cuoc: 5000 dong/ngay. De dang ky dich vu, soan: BD gui 979. De huy dich vu, soan HUY BD gui 979. Truy cap http://visport.vn/ de biet them chi tiet. ";
                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);

                #endregion
            }
            else if(Command_Code == "KQ" && ( subcode == "1" || subcode == "2" || subcode == "3" ) )
            {
                #region DU DOAN KQ TRAN DAU

                string today = DateTime.Now.DayOfWeek.ToString();

                if(CheckDayOfWeek(today))
                {
                    DataTable dt = ViSport_S2_Registered_UsersController.SportGameHeroMatchGetByDay();
                    if(dt != null && dt.Rows.Count > 0)
                    {
                        int matchId = ConvertUtility.ToInt32(dt.Rows[0]["Id"].ToString());
                        string teamWin = "HOA";

                        if (subcode == "1")
                            teamWin = dt.Rows[0]["Team_A_Code"].ToString();
                        else if(subcode == "3")
                            teamWin = dt.Rows[0]["Team_B_Code"].ToString();

                        ViSport_S2_Registered_UsersController.SportGameHeroMatchVoteInsert(User_ID, matchId, Message.Trim(), teamWin, null, null, null, null,Command_Code);
                        messageReturn = "Ban da du doan ket qua tran dau thanh cong !";
                        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                    }
                    else
                    {
                        messageReturn = "Hom nay chua co Tran dau nao. Vui long quay lai sau !";
                        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                    }
                }
                else
                {
                    messageReturn = AppEnv.GetSetting("tpbd_saingaydudoan");
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                }

                #endregion
            }
            else if(Command_Code == "BT" && subcode != "")
            {
                #region DU DOAN TONG SO BAN THANG

                 string today = DateTime.Now.DayOfWeek.ToString();

                 if (CheckDayOfWeek(today))
                 {
                     DataTable dt = ViSport_S2_Registered_UsersController.SportGameHeroMatchGetByDay();
                     if (dt != null && dt.Rows.Count > 0)
                     {
                         int matchId = ConvertUtility.ToInt32(dt.Rows[0]["Id"].ToString());
                         int goal = ConvertUtility.ToInt32(subcode);

                         ViSport_S2_Registered_UsersController.SportGameHeroMatchVoteInsert(User_ID, matchId, Message.Trim(), null, goal, null, null, null,Command_Code);

                         messageReturn = "Ban da du doan tong so ban thang tran dau thanh cong !";
                         SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                     }
                     else
                     {
                         messageReturn = "Hom nay chua co Tran dau nao. Vui long quay lai sau !";
                         SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                     }
                 }
                 else
                 {
                     messageReturn = AppEnv.GetSetting("tpbd_saingaydudoan");
                     SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                 }

                #endregion
            }
            else if(Command_Code == "TS" && subcode != "")
            {
                #region DU DOAN TY SO TRAN DAU
                
                 string today = DateTime.Now.DayOfWeek.ToString();

                 if (CheckDayOfWeek(today))
                 {
                     DataTable dt = ViSport_S2_Registered_UsersController.SportGameHeroMatchGetByDay();
                     if (dt != null && dt.Rows.Count > 0)
                     {
                         int matchId = ConvertUtility.ToInt32(dt.Rows[0]["Id"].ToString());
                         ViSport_S2_Registered_UsersController.SportGameHeroMatchVoteInsert(User_ID, matchId, Message.Trim(), null, null, subcode.Trim().Replace(" ", "-"), null, null,Command_Code);

                         messageReturn = "Ban da du doan ty so tran dau thanh cong !";
                         SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                     }
                     else
                     {
                         messageReturn = "Hom nay chua co Tran dau nao. Vui long quay lai sau !";
                         SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                     }
                 }
                 else
                 {
                     messageReturn = AppEnv.GetSetting("tpbd_saingaydudoan");
                     SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                 }

                #endregion
            }
            else if (Command_Code == "GB" && (subcode == "1" || subcode == "2" || subcode == "3"))
            {
                #region DU DOAN POSSESSION
                
                 string today = DateTime.Now.DayOfWeek.ToString();

                 if (CheckDayOfWeek(today))
                 {
                      DataTable dt = ViSport_S2_Registered_UsersController.SportGameHeroMatchGetByDay();
                      if (dt != null && dt.Rows.Count > 0)
                      {
                          int matchId = ConvertUtility.ToInt32(dt.Rows[0]["Id"].ToString());

                          string teamPossession = "";

                          if (subcode == "2")
                              teamPossession = "HOA";
                          else if (subcode == "1")
                              teamPossession = dt.Rows[0]["Team_A_Code"].ToString();
                          else if (subcode == "3")
                              teamPossession = dt.Rows[0]["Team_B_Code"].ToString();

                          ViSport_S2_Registered_UsersController.SportGameHeroMatchVoteInsert(User_ID, matchId, Message.Trim(), null, null, null, teamPossession, null,Command_Code);

                          messageReturn = "Ban da du doan doi giu bong nhieu hon thanh cong !";
                          SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                      }
                      else
                      {
                          messageReturn = "Hom nay chua co Tran dau nao. Vui long quay lai sau !";
                          SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                      }
                 }
                 else
                 {
                     messageReturn = AppEnv.GetSetting("tpbd_saingaydudoan");
                     SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                 }

                #endregion
            }
            else if(Command_Code == "TV" && subcode != "")
            {
                #region DU DOAN SO THE VANG
                
                 string today = DateTime.Now.DayOfWeek.ToString();

                 if (CheckDayOfWeek(today))
                 {
                      DataTable dt = ViSport_S2_Registered_UsersController.SportGameHeroMatchGetByDay();
                      if (dt != null && dt.Rows.Count > 0)
                      {
                          int matchId = ConvertUtility.ToInt32(dt.Rows[0]["Id"].ToString());

                          int yellowCard = ConvertUtility.ToInt32(subcode);

                          ViSport_S2_Registered_UsersController.SportGameHeroMatchVoteInsert(User_ID, matchId, Message.Trim(), null, null, null, null, yellowCard,Command_Code);

                          messageReturn = "Ban da du doan tong so the vang thanh cong !";
                          SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                      }
                      else
                      {
                          messageReturn = "Hom nay chua co Tran dau nao. Vui long quay lai sau !";
                          SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                      }
                 }
                 else
                 {
                     messageReturn = AppEnv.GetSetting("tpbd_saingaydudoan");
                     SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                 }

                #endregion
            }
            else
            {
                //messageReturn = "Tin nhan sai cu phap. De nhan 2 MDT va co co hoi trung 30 trieu tien mat, iPhone 5S sanh dieu soan TP gui 979 (mien phi dang ky), de biet them thong tin soan: HD gui 979. HT: 19001255";
                messageReturn = "Tin nhan sai cu phap. De nhan 2 MDT va co co hoi trung Samsung Galaxy S4 soan TP gui 979 (mien phi dang ky), de biet them thong tin soan: HD gui 979. HT: 19001255";
                SendMtSportGameHero(User_ID,messageReturn,Service_ID,Command_Code,Request_ID,0);
            }
        }
        catch (Exception ex)
        {
            responseValue = "1";
            log.Debug("---------------Error sentMT----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }

        return responseValue;
    }

    private string ExcecuteRequestMo(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
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
            const int msgType = (int)Constant.MessageType.NoCharge;

            log.Debug(" ");
            log.Debug(" ");
            log.Debug("--------------------viSPORT-------------------------");
            log.Debug("User_ID: " + User_ID);
            log.Debug("Service_ID: " + Service_ID);
            log.Debug("Command_Code: " + Command_Code);
            log.Debug("Message: " + Message.ToUpper());
            log.Debug("Request_ID: " + Request_ID);
            log.Debug(" ");
            log.Debug(" ");

            #region Log MO Message Into Database (SMS_MO_Log)

            SMS_MOInfo moInfo = new SMS_MOInfo();

            moInfo.User_ID = User_ID;
            moInfo.Service_ID = Service_ID;
            moInfo.Command_Code = Command_Code;
            moInfo.Message = Message;
            moInfo.Request_ID = Request_ID;
            moInfo.Operator = GetTelco(User_ID);
            SMS_MODB.Insert(moInfo);

            #endregion

            
            #region DK & DANGKY
            
            if( Command_Code.ToUpper() == "DK" || Command_Code.ToUpper() == "DANGKY" )
            {
                string responseMsg = "";
                if(subcode.ToUpper() == "TT" || subcode.ToUpper() == "MU")
                {
                    var regObject = new ViSport_S2_Registered_SpamSms_UserInfo();

                    regObject.User_Id = User_ID;
                    regObject.Request_Id = User_ID;
                    regObject.Service_Id = Service_ID;
                    regObject.Command_Code = Command_Code;
                    regObject.Sub_Code = subcode;
                    regObject.Service_Type = GetServiceType(Command_Code);
                    regObject.Charging_Count = 0;
                    regObject.FailedChargingTimes = 0;
                    regObject.RegisteredTime = DateTime.Now;
                    //regObject.ExpiredTime = DateTime.Now.AddDays(7);
                    regObject.Registration_Channel = "SMS";
                    regObject.Status = 1;
                    regObject.Operator = moInfo.Operator;

                    DataTable value = ViSport_S2_Registered_UsersController.InsertSmsSpamUser(regObject);

                    if (value.Rows[0]["RETURN_ID"].ToString() == "1")//DA DK DICH Vu
                    {
                        if (subcode == "TT")
                            responseMsg = AppEnv.GetSetting("VNM_SpamSms_User_AlreadyRegister_TT");
                        else
                            responseMsg = AppEnv.GetSetting("VNM_SpamSms_User_AlreadyRegister_MU");
                    }
                    else if (value.Rows[0]["RETURN_ID"].ToString() == "0")//CHUA DK DICH VU
                    {
                        if (subcode == "TT")
                            responseMsg = AppEnv.GetSetting("VNM_SpamSms_User_SucessRegister_TT");
                        else
                            responseMsg = AppEnv.GetSetting("VNM_SpamSms_User_SucessRegister_MU");
                    }

                    #region MT Tra ve Thong Bao Sau Khi DANGKY

                    #region Send MT

                    var objSentMtRegister = new ServiceProviderService();

                    if (AppEnv.GetSetting("TestFlag") == "0")
                    {
                        returnValue = objSentMtRegister.sendMT(User_ID, responseMsg, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
                    }

                    #endregion

                    var objMt = new ViSport_S2_SMS_MTInfo();
                    objMt.User_ID = User_ID;
                    objMt.Message = responseMsg;
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
                    objMt.Operator = "ViSport_MT";

                    ViSport_S2_SMS_MTController.Insert(objMt);

                    #endregion
      
                }
                else
                {
                    responseMsg = AppEnv.GetSetting("VNM_SpamSms_User_ErrorSystax");

                    #region MT Tra ve Thong Bao TIN NHAN SAI CU PHAP

                    #region Send MT

                    var objSentMtRegister = new ServiceProviderService();

                    if (AppEnv.GetSetting("TestFlag") == "0")
                    {
                        returnValue = objSentMtRegister.sendMT(User_ID, responseMsg, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
                    }

                    #endregion

                    var objMt = new ViSport_S2_SMS_MTInfo();
                    objMt.User_ID = User_ID;
                    objMt.Message = responseMsg;
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
                    objMt.Operator = "ViSport_MT";

                    ViSport_S2_SMS_MTController.Insert(objMt);

                    #endregion

                }

                return responseValue;
            }

            #endregion

            #region HUY gui 979

            if (Command_Code.ToUpper() == "HUY")
            {
                if(subcode == "") //HUY Dich Vu ViSport SMS HANG NGAY FREE
                {
                    #region TAM STOP

                    //DataTable dt = ViSport_S2_Registered_UsersController.OffSmsSpam(User_ID, 0);
                    //if (dt != null && dt.Rows.Count > 0)
                    //{
                    //    if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                    //    {
                    //        string responseMessage = AppEnv.GetSetting("OffSpamSms_MT");

                    //        #region MT Tra ve Thong Bao da HUY

                    //        #region Send MT

                    //        var objSentMtHuy = new ServiceProviderService();

                    //        if (AppEnv.GetSetting("TestFlag") == "0")
                    //        {
                    //            returnValue = objSentMtHuy.sendMT(User_ID, responseMessage, Service_ID, Command_Code,
                    //                                              "1", Request_ID, "1", "1", "0", "0");
                    //        }

                    //        #endregion

                    //        var objMt = new ViSport_S2_SMS_MTInfo();
                    //        objMt.User_ID = User_ID;
                    //        objMt.Message = responseMessage;
                    //        objMt.Service_ID = Service_ID;
                    //        objMt.Command_Code = Command_Code;
                    //        objMt.Message_Type = 1;
                    //        objMt.Request_ID = Request_ID;
                    //        objMt.Total_Message = 1;
                    //        objMt.Message_Index = 0;
                    //        objMt.IsMore = 0;
                    //        objMt.Content_Type = 0;
                    //        objMt.ServiceType = 0;
                    //        objMt.ResponseTime = DateTime.Now;
                    //        objMt.isLock = false;
                    //        objMt.PartnerID = "Xzone";
                    //        objMt.Operator = "ViSport_MT";

                    //        ViSport_S2_SMS_MTController.Insert(objMt);

                    //        #endregion
                    //    }
                    //}

                    #endregion

                    #region HUY DV WORLD CUP VTV DIGITAL

                    var objSentMtHuy = new ServiceProviderService();
                    string msg;

                    DataTable dt = ViSport_S2_Registered_UsersController.WorldCupRegisteredUserDeleted(User_ID, "VTV");
                    if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                    {
                        msg = "Ban da HUY thanh cong dich vu Dong Hanh Cung World cup 2014.";
                        returnValue = objSentMtHuy.sendMT(User_ID, msg, Service_ID, Command_Code, msgType.ToString(), Request_ID, "1", "1", "0", "0");
                    }
                    else
                    {
                        msg = "Ban chua dang ky dich vu Dong Hanh Cung World cup 2014. De dang ky soan VTV gui 979";
                        returnValue = objSentMtHuy.sendMT(User_ID, msg, Service_ID, Command_Code, msgType.ToString(), Request_ID, "1", "1", "0", "0");
                    }

                    log.Debug(" ");
                    log.Debug(" ");
                    log.Debug("********** HUY DV VTV WC 2014 *************");
                    log.Debug("UserId : " + User_ID);
                    log.Debug("Message : " + msg);
                    log.Debug(" ");
                    log.Debug(" ");

                    #endregion
                   
                }
                else if(subcode == "TT")//HUY Dich Vu TinHangNgay
                {
                    DataTable dt = ViSport_S2_Registered_UsersController.DeleteSmsSpamUser(User_ID, subcode.ToUpper());
                    if(dt != null && dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                        {
                            string responseMessage = AppEnv.GetSetting("VNM_SpamSms_User_Delete_TT");

                            #region MT Tra ve Thong Bao da HUY

                            #region Send MT

                            ServiceProviderService objSentMtHuy = new ServiceProviderService();

                            if (AppEnv.GetSetting("TestFlag") == "0")
                            {
                                returnValue = objSentMtHuy.sendMT(User_ID, responseMessage, Service_ID, Command_Code,
                                                                  "1", Request_ID, "1", "1", "0", "0");
                            }

                            #endregion

                            var objMt = new ViSport_S2_SMS_MTInfo();
                            objMt.User_ID = User_ID;
                            objMt.Message = responseMessage;
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
                            objMt.Operator = "ViSport_MT";

                            ViSport_S2_SMS_MTController.Insert(objMt);

                            #endregion
                        }
                    }
                }
                else if(subcode == "MU")//HUY Dich Vu MU
                {
                    DataTable dt = ViSport_S2_Registered_UsersController.DeleteSmsSpamUser(User_ID, subcode.ToUpper());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                        {
                            string responseMessage = AppEnv.GetSetting("VNM_SpamSms_User_Delete_MU");

                            #region MT Tra ve Thong Bao da HUY

                            #region Send MT

                            ServiceProviderService objSentMtHuy = new ServiceProviderService();

                            if (AppEnv.GetSetting("TestFlag") == "0")
                            {
                                returnValue = objSentMtHuy.sendMT(User_ID, responseMessage, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
                            }
                            

                            #endregion

                            var objMt = new ViSport_S2_SMS_MTInfo();
                            objMt.User_ID = User_ID;
                            objMt.Message = responseMessage;
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
                            objMt.Operator = "ViSport_MT";

                            ViSport_S2_SMS_MTController.Insert(objMt);

                            #endregion
                        }
                    }
                }
                else if (subcode.ToUpper() == "BD" || subcode.ToUpper() == "TBDV" || subcode.ToUpper() == "TP")
                {
                    ExcecuteRequestMoSportGame(User_ID, Service_ID, Command_Code, Message, Request_ID);
                }
                else if (subcode.ToUpper() == "WC")
                {
                    #region HUY DV WORLD CUP VTV DIGITAL

                    var objSentMtHuy = new ServiceProviderService();
                    string msg;

                    DataTable dt = ViSport_S2_Registered_UsersController.WorldCupRegisteredUserDeleted(User_ID, "VTV");
                    if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                    {
                        msg = "Ban da HUY thanh cong dich vu Dong Hanh Cung World cup 2014.";
                        returnValue = objSentMtHuy.sendMT(User_ID, msg, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
                    }
                    else
                    {
                        msg = "Ban chua dang ky dich vu Dong Hanh Cung World cup 2014. De dang ky soan VTV gui 979";
                        returnValue = objSentMtHuy.sendMT(User_ID, msg, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
                    }

                    #endregion
                }
                else //TIN NHAN SAI CU PHAP
                {
                    string responseMsg = AppEnv.GetSetting("VNM_SpamSms_User_ErrorSystax");

                    #region MT Tra ve Thong Bao TIN NHAN SAI CU PHAP

                    #region Send MT

                    var objSentMtRegister = new ServiceProviderService();

                    if (AppEnv.GetSetting("TestFlag") == "0")
                    {
                        returnValue = objSentMtRegister.sendMT(User_ID, responseMsg, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
                    }

                    #endregion

                    var objMt = new ViSport_S2_SMS_MTInfo();
                    objMt.User_ID = User_ID;
                    objMt.Message = responseMsg;
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
                    objMt.Operator = "ViSport_MT";

                    ViSport_S2_SMS_MTController.Insert(objMt);

                    #endregion
                }
               
                return responseValue;
            }

            #endregion

            #region Execute MT

            //Message = Message.ToUpper();
            //string subcode = "";
            //if (Message.Trim().Length > Command_Code.Trim().Length)
            //{
            //    subcode = Message.ToUpper().Substring(Command_Code.Length).Replace(" ", "");
            //}

            if (subcode == "OFF")
            {
                var objCancel = new SMS_CancelInfo();

                objCancel.User_ID = User_ID;
                objCancel.Service_ID = Service_ID;
                objCancel.Command_Code = Command_Code;
                objCancel.Service_Type = GetServiceType(Command_Code);
                objCancel.Message = Message;
                objCancel.Request_ID = Request_ID;
                objCancel.Operator = GetTelco(User_ID);
                SMS_MODB.CancelInsert(objCancel);

                var regObject = new ViSport_S2_Registered_UsersInfo();

                regObject.User_ID = User_ID;
                regObject.Status = 0;
                regObject.Service_Type = objCancel.Service_Type;

                DataTable dt = ViSport_S2_Registered_UsersController.Update(regObject);

                string time = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    time = ConvertUtility.ToDateTime(dt.Rows[0]["ExpiredTime"]).ToString("dd/MM/yy");
                }
                var objSentMt = new ServiceProviderService();

                string message = "";
                if (regObject.Service_Type == 1)
                {
                    message = AppEnv.GetSetting("alert_cancel_success").Replace("dd/mm/yy", time);
                }
                else
                {
                    message = AppEnv.GetSetting("alert_SC_cancel_success").Replace("dd/mm/yy", time);
                }

                if (AppEnv.GetSetting("TestFlag") == "0")
                {
                    returnValue = objSentMt.sendMT(User_ID, message, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
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

                ViSport_S2_SMS_MTController.Insert(objMt);
            }
            else
            {
                if (subcode == "")
                {
                    ServiceProviderService objSentMT = new ServiceProviderService();
                    string message = string.Empty;

                    //Kiem tra da tung HUY trc day chua
                    int serviceType = GetServiceType(Command_Code);
                    DataTable dt = ViSport_S2_Registered_UsersController.CheckAlreadyOff(User_ID, serviceType);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string msgReturn = AppEnv.GetSetting("msgReturn");
                        if (AppEnv.GetSetting("TestFlag") == "0")
                        {
                            var webServiceCharging3G = new WebServiceCharging3g();
                            msgReturn = webServiceCharging3G.PaymentVnmWithAccount(User_ID, AppEnv.GetSetting("price_visport_charging"), "gia han goi tuan thue bao:" + User_ID,
                                                                                     "viSport", AppEnv.GetSetting("userName_3g_visport"), AppEnv.GetSetting("password_3g_visport"), AppEnv.GetSetting("cpId_3g_visport"));

                            log.Debug(" ");
                            log.Debug("VNM resp return value [viSport]: " + msgReturn);
                            log.Debug(" ");
                        }

                        if (msgReturn == "1")//Charging thanh cong
                        {
                            if (ViSport_S2_Registered_UsersController.UpdateAlreadyOff(User_ID, serviceType, DateTime.Now.AddDays(7)))
                            {
                                //Sent MT
                                if (serviceType == 1)
                                {
                                    message = AppEnv.GetSetting("DK_lai_khi_da_tung_HUY_dungcuphap_sm");
                                }
                                else
                                {
                                    message = AppEnv.GetSetting("DK_lai_khi_da_tung_HUY_dungcuphap_sc");
                                }

                                if (AppEnv.GetSetting("TestFlag") == "0")
                                {
                                    returnValue = objSentMT.sendMT(User_ID, message, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
                                }

                                ViSport_S2_SMS_MTInfo objMT = new ViSport_S2_SMS_MTInfo();
                                objMT.User_ID = User_ID;
                                objMT.Message = message;
                                objMT.Service_ID = Service_ID;
                                objMT.Command_Code = Command_Code;
                                objMT.Message_Type = 1;
                                objMT.Request_ID = Request_ID;
                                objMT.Total_Message = 1;
                                objMT.Message_Index = 0;
                                objMT.IsMore = 0;
                                objMT.Content_Type = 0;
                                objMT.ServiceType = 0;
                                objMT.ResponseTime = DateTime.Now;
                                objMT.isLock = false;
                                objMT.PartnerID = "Xzone";
                                objMT.Operator = GetTelco(User_ID);
                                ViSport_S2_SMS_MTController.Insert(objMT);
                            }
                        }
                        else
                        {
                            //Khong Du Tien msgReturn = resp: Result:12,Detail:Not enough money.
                            string[] arr = msgReturn.Split(',');
                            if (arr[0].Trim() == "Result:12")
                            {
                                message = AppEnv.GetSetting("DK_lai_khi_da_tung_HUY_thubaohettien");

                                if (AppEnv.GetSetting("TestFlag") == "0")
                                {
                                    returnValue = objSentMT.sendMT(User_ID, message, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
                                }

                                ViSport_S2_SMS_MTInfo objMT = new ViSport_S2_SMS_MTInfo();
                                objMT.User_ID = User_ID;
                                objMT.Message = message;
                                objMT.Service_ID = Service_ID;
                                objMT.Command_Code = Command_Code;
                                objMT.Message_Type = 1;
                                objMT.Request_ID = Request_ID;
                                objMT.Total_Message = 1;
                                objMT.Message_Index = 0;
                                objMT.IsMore = 0;
                                objMT.Content_Type = 0;
                                objMT.ServiceType = 0;
                                objMT.ResponseTime = DateTime.Now;
                                objMT.isLock = false;
                                objMT.PartnerID = "Xzone";
                                objMT.Operator = GetTelco(User_ID);
                                ViSport_S2_SMS_MTController.Insert(objMT);
                            }
                            else
                            {
                                log.Debug(" ");
                                log.Debug("VNM resp return value [viSport]: " + msgReturn);
                                log.Debug(" ");
                            }
                        }
                    }
                    else
                    {
                        ViSport_S2_Registered_UsersInfo regObject = new ViSport_S2_Registered_UsersInfo();

                        regObject.User_ID = User_ID;
                        regObject.Request_ID = Request_ID;
                        regObject.Service_ID = Service_ID;
                        regObject.Command_Code = Command_Code;
                        regObject.Service_Type = GetServiceType(Command_Code);
                        regObject.Charging_Count = 0;
                        regObject.FailedChargingTimes = 0;
                        regObject.RegisteredTime = DateTime.Now;
                        regObject.ExpiredTime = DateTime.Now.AddDays(7);
                        regObject.Registration_Channel = "SMS";
                        regObject.Status = 1;
                        regObject.Operator = moInfo.Operator;

                        DataTable value = ViSport_S2_Registered_UsersController.Insert(regObject);

                        log.Debug("Reg return value: " + value);


                        //string message = "";
                        if (regObject.Service_Type == 1)
                        {
                            message = AppEnv.GetSetting("alert_reg_success");
                        }
                        else
                        {
                            message = AppEnv.GetSetting("alert_SC_reg_success");
                        }


                        if (value.Rows[0]["RETURN_ID"].ToString() == "1")
                        {
                            string time = ConvertUtility.ToDateTime(value.Rows[0]["ExpiredTime"]).ToString("dd/MM/yyyy");
                            if (regObject.Service_Type == 1)
                            {
                                message = AppEnv.GetSetting("alert_Already_Reg").Replace("dd/mm/yy", time); //Replace here
                            }
                            else
                            {
                                message = AppEnv.GetSetting("alert_SC_Already_Reg").Replace("dd/mm/yy", time);
                            }
                        }

                        if (AppEnv.GetSetting("TestFlag") == "0")
                        {
                            returnValue = objSentMT.sendMT(User_ID, message, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
                        }

                        ViSport_S2_SMS_MTInfo objMT = new ViSport_S2_SMS_MTInfo();
                        objMT.User_ID = User_ID;
                        objMT.Message = message;
                        objMT.Service_ID = Service_ID;
                        objMT.Command_Code = Command_Code;
                        objMT.Message_Type = 1;
                        objMT.Request_ID = Request_ID;
                        objMT.Total_Message = 1;
                        objMT.Message_Index = 0;
                        objMT.IsMore = 0;
                        objMT.Content_Type = 0;
                        objMT.ServiceType = 0;
                        objMT.ResponseTime = DateTime.Now;
                        objMT.isLock = false;
                        objMT.PartnerID = "Xzone";
                        objMT.Operator = GetTelco(User_ID);
                        ViSport_S2_SMS_MTController.Insert(objMT);

                    }
                }
                else
                {
                    ServiceProviderService objSentMT = new ServiceProviderService();

                    //string message = "(092)Tin nhan sai, dang ky su dung dich vu viSport theo cu phap sau: SC gui 979 (3000vnd/7ngay). Quy khach se duoc su dung dich vu MIEN PHI trong 7 ngay dau tien.HT: 19001255";

                    //string message = "Tin nhan sai cu phap. De nhan 2 MDT va co co hoi trung 30 trieu tien mat, iPhone 5S sanh dieu soan TP gui 979 (mien phi dang ky), de biet them thong tin soan: HD gui 979. HT: 19001255";
                    string message = "Tin nhan sai cu phap. De nhan 2 MDT va co co hoi trung Samsung Galaxy S4 soan TP gui 979 (mien phi dang ky), de biet them thong tin soan: HD gui 979. HT: 19001255";

                    if (AppEnv.GetSetting("TestFlag") == "0")
                    {
                        returnValue = objSentMT.sendMT(User_ID, message, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
                    }

                    ViSport_S2_SMS_MTInfo objMT = new ViSport_S2_SMS_MTInfo();
                    objMT.User_ID = User_ID;
                    objMT.Message = message;
                    objMT.Service_ID = Service_ID;
                    objMT.Command_Code = Command_Code;
                    objMT.Message_Type = 1;
                    objMT.Request_ID = Request_ID;
                    objMT.Total_Message = 1;
                    objMT.Message_Index = 0;
                    objMT.IsMore = 0;
                    objMT.Content_Type = 0;
                    objMT.ServiceType = 0;
                    objMT.ResponseTime = DateTime.Now;
                    objMT.isLock = false;
                    objMT.PartnerID = "Xzone";
                    objMT.Operator = GetTelco(User_ID);
                    ViSport_S2_SMS_MTController.Insert(objMT);
                }
            }

            #region sentMT

            #endregion

            #endregion

            responseValue = "1";
        }
        catch (Exception ex)
        {
            responseValue = "1";
            log.Debug("---------------Error sentMT----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }

        return responseValue;
    }

    private string ExcecuteRequestMovClip(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        string responseValue = "1";
        int returnValue = 0;
        string message = string.Empty;
        
        if(Command_Code.ToUpper() == "CLIP")
        {
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

                Message = Message.ToUpper();
                string subcode = "";
                if (Message.Trim().Length > Command_Code.Trim().Length)
                {
                    subcode = Message.ToUpper().Substring(Command_Code.Length).Replace(" ", "");
                }

                if (subcode == "OFF")
                {

                    #region Huy DK USER

                    var objCancel = new SMS_CancelInfo();

                    objCancel.User_ID = User_ID;
                    objCancel.Service_ID = Service_ID;
                    objCancel.Command_Code = Command_Code;
                    objCancel.Service_Type = GetServiceTypeVClip(Command_Code);
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
                        string time;
                        //if (dt != null && dt.Rows.Count > 0)
                        //{
                            time = ConvertUtility.ToDateTime(dt.Rows[0]["ExpiredTime"]).ToString("dd/MM/yy");
                        //}
                        
                        //string message;
                        if (regObject.Service_Type == 1)
                        {
                            message = AppEnv.GetSetting("alert_cancel_success_vclip").Replace("dd/mm/yy", time);
                        }
                        else
                        {
                            message = AppEnv.GetSetting("alert_SC_cancel_success_vclip").Replace("dd/mm/yy", time);
                        }


                        if (AppEnv.GetSetting("TestFlag") == "0")
                        {
                            objSentMt.sendMT(User_ID, message, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
                        }
                    }
                    else
                    {
                        message = "Ban chua dk dich vu nay. Xin cam on";
                        objSentMt.sendMT(User_ID, message, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
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
                else if(subcode == "OFF1")
                {
                    #region Huy DK USER Free 5 Day

                    DataTable dt = ViSport_S2_Registered_UsersController.Free5DayClipOff(User_ID,0);
                    if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                    {
                        message = "QKhach da huy thanh cong DV VIDEO HOT hang ngay cua VNM.Truy cap: http://kho-clip.com de nhan huong dan dang ky lai dich vu Video cua Vietnamobile. HT: 19001255";
                        SendMtVmClip(User_ID,message,Service_ID,Command_Code,Request_ID);
                    }
                    else
                    {
                        message = "QKhach chua dang ky dich vu nay.De dang ky dich vu soan tin CLIP ON gui 949.HT: 19001255";
                        SendMtVmClip(User_ID, message, Service_ID, Command_Code, Request_ID);
                    }

                    #endregion
                }
                else
                {
                    if (subcode == "")
                    {

                        var objSentMt = new ServiceProviderService();

                        if (AppEnv.GetSetting("VClip_New") == "1")
                        {

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
                                message = "(092)Chuc mung! Quy khach da Dky thanh cong DV VMclip (2000vnd/ 1ngay). Truy cap ngay http://kho-clip.com/" + User_ID + ".aspx  de su dung dich vu. De huy DK, soan: CLIP OFF gui 949. HT: 19001255";
                            }
                            else if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                            {
                                message = "Quy khach da Dky dich vu VMclip truoc do. Xin cam on";
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
                        else
                        {
                            #region DK USER

                            
                            DataTable dt = ViSport_S2_Registered_UsersController.CheckAlreadyOffVClip(User_ID);
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                string msgReturn = AppEnv.GetSetting("msgReturn");
                                if (AppEnv.GetSetting("TestFlag") == "0")
                                {
                                    var webServiceCharging3G = new WebServiceCharging3g();
                                    msgReturn = webServiceCharging3G.PaymentVnmWithAccount(User_ID, AppEnv.GetSetting("price_vclip_charging"), "gia han goi tuan thue bao:" + User_ID,
                                                                                                  "VClip", AppEnv.GetSetting("userName_3g_vclip"), AppEnv.GetSetting("password_3g_vclip"), AppEnv.GetSetting("cpId_3g_vclip"));
                                    log.Debug(" ");
                                    log.Debug("VNM resp return value [VClip]: " + msgReturn);
                                    log.Debug(" ");
                                }

                                if (msgReturn == "1")//Thanh Cong
                                {
                                    if (ViSport_S2_Registered_UsersController.UpdateAlreadyOffVClip(User_ID, DateTime.Now.AddDays(7)))
                                    {
                                        message = AppEnv.GetSetting("DK_lai_khi_da_tung_HUY_dungcuphap_vclip");

                                        if (AppEnv.GetSetting("TestFlag") == "0")
                                        {
                                            objSentMt.sendMT(User_ID, message, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
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
                                    }
                                }
                                else
                                {
                                    //Khong Du Tien msgReturn = resp: Result:12,Detail:Not enough money.
                                    string[] arr = msgReturn.Split(',');
                                    if (arr[0].Trim() == "Result:12")
                                    {
                                        message = AppEnv.GetSetting("DK_lai_khi_da_tung_HUY_thubaohettien_vclip");

                                        if (AppEnv.GetSetting("TestFlag") == "0")
                                        {
                                            objSentMt.sendMT(User_ID, message, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
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
                                    }
                                    else
                                    {
                                        log.Debug(" ");
                                        log.Debug("VNM resp return value [VClip]: " + msgReturn);
                                        log.Debug(" ");
                                    }
                                }
                            }
                            else
                            {
                                var regObject = new ViSport_S2_Registered_UsersInfo();

                                regObject.User_ID = User_ID;
                                regObject.Request_ID = Request_ID;
                                regObject.Service_ID = Service_ID;
                                regObject.Command_Code = Command_Code;
                                regObject.Service_Type = GetServiceTypeVClip(Command_Code);
                                regObject.Charging_Count = 0;
                                regObject.FailedChargingTimes = 0;
                                regObject.RegisteredTime = DateTime.Now;
                                regObject.ExpiredTime = DateTime.Now.AddDays(3);
                                regObject.Registration_Channel = "SMS";
                                regObject.Status = 1;
                                regObject.Operator = moInfo.Operator;

                                DataTable value = ViSport_S2_Registered_UsersController.InsertVClip(regObject);

                                log.Debug("Reg return value: " + value);
                                if (regObject.Service_Type == 1)
                                {
                                    message = AppEnv.GetSetting("alert_reg_success_vclip");
                                }
                                else
                                {
                                    message = AppEnv.GetSetting("alert_SC_reg_success_vclip");
                                }


                                if (value.Rows[0]["RETURN_ID"].ToString() == "1")
                                {
                                    string time = ConvertUtility.ToDateTime(value.Rows[0]["ExpiredTime"]).ToString("dd/MM/yyyy");
                                    if (regObject.Service_Type == 1)
                                    {
                                        message = AppEnv.GetSetting("alert_Already_Reg_vclip").Replace("dd/mm/yy", time);
                                    }
                                    else
                                    {
                                        message = AppEnv.GetSetting("alert_SC_Already_Reg_vclip").Replace("dd/mm/yy", time);
                                    }
                                }

                                if (AppEnv.GetSetting("TestFlag") == "0")
                                {
                                    objSentMt.sendMT(User_ID, message, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");

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

                                    if (value.Rows[0]["RETURN_ID"].ToString() == "0")
                                    {
                                        string message1 = AppEnv.GetSetting("alert_reg_success_vclip1");
                                        objSentMt.sendMT(User_ID, message1, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");

                                        var objMt1 = new ViSport_S2_SMS_MTInfo();
                                        objMt1.User_ID = User_ID;
                                        objMt1.Message = message1;
                                        objMt1.Service_ID = Service_ID;
                                        objMt1.Command_Code = Command_Code;
                                        objMt1.Message_Type = 1;
                                        objMt1.Request_ID = Request_ID;
                                        objMt1.Total_Message = 1;
                                        objMt1.Message_Index = 0;
                                        objMt1.IsMore = 0;
                                        objMt1.Content_Type = 0;
                                        objMt1.ServiceType = 0;
                                        objMt1.ResponseTime = DateTime.Now;
                                        objMt1.isLock = false;
                                        objMt1.PartnerID = "Xzone";
                                        objMt1.Operator = GetTelco(User_ID);
                                        ViSport_S2_SMS_MTController.InsertVClip(objMt1);
                                    }
                                }
                            }

                            #endregion
                        }

                    }
                    else
                    {

                        #region SMS Sai Cu Phap

                        var objSentMt = new ServiceProviderService();

                        message = "(092)Tin nhan sai, dang ky su dung dich vu VMclip theo cu phap sau: CLIP gui 949 (2000vnd/ngay).HT: 19001255";

                        if (AppEnv.GetSetting("TestFlag") == "0")
                        {
                            objSentMt.sendMT(User_ID, message, Service_ID, Command_Code, "0", Request_ID, "1", "1", "0", "0");
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
                }


                #endregion

                responseValue = "1";
            }
            catch (Exception ex)
            {
                responseValue = "1";
                log.Debug("---------------Error sentMT----------------------");
                log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
            }

            #endregion
        }
        else if(Command_Code.ToUpper() == "DK1")
        {
            
            #region VNM Process Register

            try
            {
                log.Debug(" ");
                log.Debug(" ");
                log.Debug("-------------------VNM Mo Log DK1--------------------------");
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
                SMS_MODB.InsertVnmMo(moInfo);

                #endregion

                string sms = "Truy cap: http://wap.vietnamobile.com.vn de nhan huong dan dang ky cac dich vu cua Vietnamobile. HT: 19001255";
                SendMt(User_ID, sms, Service_ID, Command_Code, Request_ID);

                responseValue = "1";
            }
            catch (Exception ex)
            {
                responseValue = "1";
                log.Debug("---------------Error sentMT DK1----------------------");
                log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
            }

            #endregion
        }
        else if (Command_Code.ToUpper() == "HUY1")
        {
            #region VNM Process Delete

            try
            {
                log.Debug(" ");
                log.Debug(" ");
                log.Debug("-------------------VNM Mo Log HUY1--------------------------");
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
                SMS_MODB.InsertVnmMo(moInfo);

                #endregion

                string sms = "Truy cap: http://wap.vietnamobile.com.vn de nhan huong dan dang ky cac dich vu cua Vietnamobile. HT: 19001255";
                SendMt(User_ID, sms, Service_ID, Command_Code, Request_ID);

                responseValue = "1";
            }
            catch (Exception ex)
            {
                responseValue = "1";
                log.Debug("---------------Error----------------------");
                log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
            }

            #endregion
        }

        return responseValue;
    }

    #endregion

    #region Methods Public

    private static int GetServiceType(string subcode)
    {
        if (subcode == "SM")
        {
            return 1;
        }
        return 0;
    }

    private static int GetServiceTypeVClip(string subcode)
    {
        if (subcode == "CLIP")
        {
            return 1;
        }
        return 0;
    }

    private static string GetUserID(string userID)
    {
        if (userID.StartsWith("+"))
        {
            userID = userID.Replace("+", string.Empty);
        }
        if (userID.StartsWith("0") || userID.StartsWith("84"))
        {
            if (userID.StartsWith("0"))
            {
                userID = "84" + userID.Remove(0, 1);
            }
        }
        else
        {
            userID = "84" + userID;
        }

        return userID;
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

    public void SendMtThanhNu(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
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

        ViSport_S2_SMS_MTController.InsertThanhNuMt(objMt);
    }

    public void SendMtMo949(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
    {
        var objSentMt = new ServiceProviderService();

        if (AppEnv.GetSetting("TestFlag") == "0")
        {
            int result = objSentMt.sendMT(userId, mtMessage, serviceId, commandCode, "1", requestId, "1", "1", "0", "0");
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

        ViSport_S2_SMS_MTController.InsertMo949Mt(objMt);
    }

    public void SendMtVmClip(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
    {
        var objSentMt = new ServiceProviderService();

        if (AppEnv.GetSetting("TestFlag") == "0")
        {
            int result = objSentMt.sendMT(userId, mtMessage, serviceId, commandCode, "1", requestId, "1", "1", "0", "0");
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
        objMt.PartnerID = "Xzone";
        objMt.Operator = "vnmobile";

        ViSport_S2_SMS_MTController.InsertVClip(objMt);

    }

    public string GetVnmDownloadItem(string telco, string itemtype, string itemid, string encode)
    {
        //itemtype = 1 : hinh nen
        //itemtype = 2 : Nhac chuong
        //itemtype = 3: game
        //itemtype = 4: app
        //itemtype = 5: video
        //itemtype = 6: y kien chuyen gia
        //itemtype = 7: tip
        //itemtype = 8: ket qua cho
        //itemtype = 9: ket qua xo so
        //itemtype = 10: soi cau
        //itemtype = 11: ket qua xoso cho
        //itemtype = 12: ket qua xoso 20 ngay lien tiep
        //itemtype = 13: truyen cuoi
        return AppEnv.GetSetting("vnmdownload") + "?telco=" + telco + "&type=" + itemtype + "&id=" + itemid + "&code=" + encode;
    }

    public static string GetDownloadItem(string telco, string itemtype, string itemid, string encode)
    {
        //itemtype = 1 : hinh nen
        //itemtype = 2 : Nhac chuong
        //itemtype = 3: game
        return AppEnv.GetSetting("download") + "?telco=" + telco + "&type=" + itemtype + "&id=" + itemid + "&code=" + encode;
    }

    public string PaymentViSportChargingOptimize(string userId)
    {
        var webServiceCharging3G = new WebServiceCharging3g();

        string msgReturn = webServiceCharging3G.PaymentVnmWithAccount
            (
                userId, AppEnv.GetSetting("SportGamePrice"), "Sport_Game_Hero:" + userId,
                "viSport_Hero", AppEnv.GetSetting("userName_3g_visport"),
                AppEnv.GetSetting("password_3g_visport"), AppEnv.GetSetting("cpId_3g_visport")
            );

        return msgReturn;

    }

    public string PaymentViSportChargingOptimize1(string userId)
    {
        var webServiceCharging3G = new WebServiceCharging3g();

        string notEnoughMoney = AppEnv.GetSetting("NotEnoughMoney");

        string price = "3000";
        string msgReturn = webServiceCharging3G.PaymentVnmWithAccount(userId, price, "Sport_Game_Hero:" + userId, "viSport_Hero", AppEnv.GetSetting("userName_3g_visport"), AppEnv.GetSetting("password_3g_visport"), AppEnv.GetSetting("cpId_3g_visport"));
        if (msgReturn.Trim() == notEnoughMoney)
        {
            price = "2000";
            msgReturn = webServiceCharging3G.PaymentVnmWithAccount(userId, price, "Sport_Game_Hero:" + userId, "viSport_Hero", AppEnv.GetSetting("userName_3g_visport"), AppEnv.GetSetting("password_3g_visport"), AppEnv.GetSetting("cpId_3g_visport"));
            if (msgReturn.Trim() == notEnoughMoney)
            {
                price = "1000";
                msgReturn = webServiceCharging3G.PaymentVnmWithAccount(userId, price, "Sport_Game_Hero:" + userId, "viSport_Hero", AppEnv.GetSetting("userName_3g_visport"), AppEnv.GetSetting("password_3g_visport"), AppEnv.GetSetting("cpId_3g_visport"));
            }
        }

        return msgReturn + "|" + price;
    }

    public string PaymentVnmWapChargingOptimize(string price, string userId, string commandCode)
    {
        string notEnoughMoney = "Result:12,Detail:Not enough money.";
        commandCode = commandCode.ToUpper();
        var webServiceCharging3G = new WebServiceCharging3g();

        string msgReturn = "";

        if (AppEnv.GetSetting("Mo949Test") == "1")
        {
            price = "500";
            msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
        }
        else
        {
            if (price == "10000" && commandCode == "GAMEHOT")
            {
                msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                if (msgReturn == notEnoughMoney)
                {
                    price = "5000";
                    msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                }
            }
            else if (price == "10000" && commandCode == "NCHAY")
            {
                msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                if (msgReturn == notEnoughMoney)
                {
                    price = "5000";
                    msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                    if (msgReturn == notEnoughMoney)
                    {
                        price = "3000";
                        msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                        if (msgReturn == notEnoughMoney)
                        {
                            price = "2000";
                            msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                        }
                    }
                }
            }
            else if (price == "2000" && commandCode == "VIDEOHAY")
            {
                msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                if (msgReturn == notEnoughMoney)
                {
                    price = "1000";
                    msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                }
            }
            else if (price == "5000" && commandCode == "TRUYENHOT")
            {
                msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                if (msgReturn == notEnoughMoney)
                {
                    price = "3000";
                    msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                    if (msgReturn == notEnoughMoney)
                    {
                        price = "2000";
                        msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                        if (msgReturn == notEnoughMoney)
                        {
                            price = "1000";
                            msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                        }
                    }
                }
            }
        }


        return msgReturn + "|" + price;
    }

    public void SendContinueQuestion(string userId, string serviceId, string commandCode, string requestId)
    {
        DataTable dtQuestion = ViSport_S2_Registered_UsersController.GetQuestionInfoSportGameHero();
        if (dtQuestion != null && dtQuestion.Rows.Count > 0)
        {
            string messageReturn = dtQuestion.Rows[0]["Question"].ToString();

            int questionIdnew = ConvertUtility.ToInt32(dtQuestion.Rows[0]["Id"].ToString());
            string answer = dtQuestion.Rows[0]["Answer"].ToString();

            SendMtSportGameHero(userId, messageReturn, serviceId, commandCode, requestId, 1);
            ViSport_S2_Registered_UsersController.InsertSportGameHeroAnswerLog(userId, questionIdnew, messageReturn, answer, DateTime.Now, 0); // LUU LOG Question
        }
    }

    public void SendContinueQuestionTpBd(string userId, string serviceId, string commandCode, string requestId)
    {
        DataTable dtQuestion = ViSport_S2_Registered_UsersController.GetQuestionInfoSportGameHero();
        if (dtQuestion != null && dtQuestion.Rows.Count > 0)
        {
            string messageReturn = dtQuestion.Rows[0]["Question"].ToString();
            messageReturn = messageReturn.Replace("P1", "1").Replace("P2", "2");

            int questionIdnew = ConvertUtility.ToInt32(dtQuestion.Rows[0]["Id"].ToString());
            string answer = dtQuestion.Rows[0]["Answer"].ToString();
            answer = answer.Replace("P1", "1").Replace("P2", "2");

            SendMtSportGameHero(userId, messageReturn, serviceId, commandCode, requestId, 1);
            ViSport_S2_Registered_UsersController.InsertSportGameHeroAnswerLog(userId, questionIdnew, messageReturn, answer, DateTime.Now, 0); // LUU LOG Question
        }
    }

    public void ChargedLogInsert(string userId, string requestId, string serviceId, string commandCode, string result)
    {
        #region Log Doanh Thu

        var e = new SportGameHeroChargedUserLogInfo();

        e.User_ID = userId;
        e.Request_ID = requestId;
        e.Service_ID = serviceId;
        e.Command_Code = commandCode;
        e.Service_Type = 1;
        e.Charging_Count = 0;
        e.FailedChargingTime = 0;
        e.RegisteredTime = DateTime.Now;
        e.ExpiredTime = DateTime.Now.AddDays(1);
        e.Registration_Channel = "SMS";
        e.Status = 1;
        e.Operator = GetTelco(userId);

        if (result == "1")
        {
            e.Reason = "Succ";
        }
        else
        {
            e.Reason = result;
        }


        e.Price = ConvertUtility.ToInt32(AppEnv.GetSetting("SportGamePrice"));

        ViSport_S2_Registered_UsersController.InsertSportGameHeroChargedUserLog(e);

        #endregion
    }

    private void Vtv6WorldCupLogMo(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID, int isCharged)
    {
        var moInfo = new SMS_MOInfo();

        moInfo.User_ID = User_ID;
        moInfo.Service_ID = Service_ID;
        moInfo.Command_Code = Command_Code;
        moInfo.Message = Message;
        moInfo.Request_ID = Request_ID;
        moInfo.Operator = GetTelco(User_ID);
        moInfo.IsCharged = isCharged;

        SMS_MODB.WorldCupInsertMo(moInfo);
    }

    private static void Vtv6WorldCupSendMt(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID, string messageType)
    {
        var objSentMt = new ServiceProviderService();

        string message = Message;

        if (AppEnv.GetSetting("TestFlag") == "0")
        {
            objSentMt.sendMT(User_ID, message, Service_ID, Command_Code, messageType, Request_ID, "1", "1", "0", "0");
        }

        var objMt = new VoteSmsMtInfo();
        objMt.User_ID = User_ID;
        objMt.Message = message;
        objMt.Service_ID = Service_ID;
        objMt.Command_Code = Command_Code;
        objMt.Message_Type = ConvertUtility.ToInt32(messageType);
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

        VoteRegisterController.WorldCupSmsMtInsert(objMt);
    }

    private static bool CheckDayOfWeek(string inputDay)
    {

        if (inputDay == DayOfWeek.Tuesday.ToString() || inputDay == DayOfWeek.Thursday.ToString() || inputDay == DayOfWeek.Saturday.ToString())
        {
            return true;
        }

        return false;

        //return true;
    }

    #endregion

}


#line default
#line hidden
