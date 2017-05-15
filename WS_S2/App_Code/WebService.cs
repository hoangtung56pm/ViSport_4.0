using System;
using System.Web.Services;
using ChargingGateway;
using RingTone;
using SentMT;
using System.Data;
using SMSManager_API.Library.Utilities;
using WS_Music.Library;
using WapJavaGame.Library.Utilities;
using vn.vmgame;
using System.Text.RegularExpressions;

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
    //log4net.ILog log = log4net.LogManager.GetLogger("WebService");   

    #region Web Service

    [WebMethod]
    public string WSProcessMo(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExcecuteRequestMo(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }
    [WebMethod]
    public string WSProcessMoThanTai(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExcecuteRequestMoThanTai(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }

    [WebMethod]
    public string WSProcessMoVClip(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExcecuteRequestMovClip(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }

    [WebMethod]
    public string WSProcessMoSportGame(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        //return "1";
        return ExcecuteRequestMoSportGame(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }

    [WebMethod]
    public string WsMo949Process(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExecuteRequestMo949Process(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }

    [WebMethod]
    public string WSWorldCupMoProcess(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExcuteRequestMoVtv6WorldCupProcess(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }

    [WebMethod]
    public string LiveNewsWorldCupProcess(string User_ID, string Service_ID, string Command_Code, int matchId, string teamCode)
    {

        try
        {
            string message;
            if (Command_Code == "KQQ")
            {
                #region VOTE TRAN DAU

                if (teamCode != "HOA")
                    message = Command_Code + " " + matchId + " " + teamCode;
                else
                    message = Command_Code + " " + matchId;

                VoteRegisterController.WorldCupMatchInsert(User_ID, matchId, message, teamCode);

                #endregion
            }
            else if (Command_Code == "VDD")
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

    [WebMethod]
    public string GenerateGiffCode(string userId, string userName, string password)
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

        if (userName == "vmgame" && password == "123!@#")
        {
            string vmCode = RandomActiveCode.RandomStringNumber(8);

            if (ViSport_S2_Registered_UsersController.ThanhNuRegisterUserCodeInsert(userId, vmCode, userName.ToUpper()))
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
    public string ImportUser(string userId, string userName, string passWord, string mtContent, int chargedDay)
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

        if (userName == "tpbd" && passWord == "tpbd!@#")
        {

            #region DK DICH VU

            var entity = new ViSport_S2_Registered_UsersInfo();
            entity.User_ID = userId;
            entity.Request_ID = "0";
            entity.Service_ID = "979";
            entity.Command_Code = "TP1";
            entity.Service_Type = 1;
            entity.Charging_Count = 0;
            entity.FailedChargingTimes = 0;
            entity.RegisteredTime = DateTime.Now;
            entity.ExpiredTime = DateTime.Now.AddDays(1);
            entity.Registration_Channel = "wap";
            entity.Status = 1;
            entity.Operator = "vnmobile";
            entity.Point = 2;
            entity.CountTo_Cancel = chargedDay + 1;


            DataTable value = ViSport_S2_Registered_UsersController.Visport_Import_User(entity);

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
        else if (userName == "kmbd" && passWord == "tpbd!@#")
        {

            #region KM BD

            var entity = new ViSport_S2_Registered_UsersInfo();
            entity.User_ID = userId;
            entity.Request_ID = "0";
            entity.Service_ID = "979";
            entity.Command_Code = "DK KM";
            entity.Service_Type = 1;
            entity.Charging_Count = 0;
            entity.FailedChargingTimes = 0;
            entity.RegisteredTime = DateTime.Now;
            entity.ExpiredTime = DateTime.Now.AddDays(1);
            entity.Registration_Channel = "OTHER";
            entity.Status = 1;
            entity.Operator = "vnmobile";
            entity.Point = 2;
            entity.CountTo_Cancel = chargedDay + 1;
            DataTable value = ViSport_S2_Registered_UsersController.Visport_Import_User(entity);
            VisportSaveMtContent(userId, mtContent, "DK KM");

            return "1";

            #endregion

        }
        else if (userName == "dktp" && passWord == "tpbd!@#")
        {

            #region DK TP

            var entity = new ViSport_S2_Registered_UsersInfo();
            entity.User_ID = userId;
            entity.Request_ID = "0";
            entity.Service_ID = "979";
            entity.Command_Code = "DK TP";
            entity.Service_Type = 1;
            entity.Charging_Count = 0;
            entity.FailedChargingTimes = 0;
            entity.RegisteredTime = DateTime.Now;
            entity.ExpiredTime = DateTime.Now.AddDays(1);
            entity.Registration_Channel = "wap";
            entity.Status = 1;
            entity.Operator = "vnmobile";
            entity.Point = 2;
            entity.CountTo_Cancel = chargedDay + 1;
            DataTable value = ViSport_S2_Registered_UsersController.Visport_Import_User(entity);
            VisportSaveMtContent(userId, mtContent, "DK TP");

            return "1";

            #endregion

        }
        else if (userName == "clip" && passWord == "clip!@#")
        {

            #region LOG DANG KY

            var regObject = new ViSport_S2_Registered_UsersInfo();

            regObject.User_ID = userId;
            regObject.Request_ID = "0";
            regObject.Service_ID = "949";
            regObject.Command_Code = "CLIP";
            regObject.Service_Type = 0;
            regObject.Charging_Count = 0;
            regObject.FailedChargingTimes = 0;
            regObject.RegisteredTime = DateTime.Now;
            regObject.ExpiredTime = DateTime.Now.AddDays(1);
            regObject.Registration_Channel = "other";
            regObject.Status = 1;
            regObject.Operator = "VNM";
            regObject.CountTo_Cancel = chargedDay + 1;
            ViSport_S2_Registered_UsersController.InsertVClip(regObject);

            #endregion

            #region MT Log

            var objMt = new ViSport_S2_SMS_MTInfo();
            objMt.User_ID = userId;
            objMt.Message = mtContent;
            objMt.Service_ID = "949";
            objMt.Command_Code = "CLIP";
            objMt.Message_Type = 1;
            objMt.Request_ID = "0";
            objMt.Total_Message = 1;
            objMt.Message_Index = 0;
            objMt.IsMore = 0;
            objMt.Content_Type = 0;
            objMt.ServiceType = 0;
            objMt.ResponseTime = DateTime.Now;
            objMt.isLock = false;
            objMt.PartnerID = "Xzone";
            objMt.Operator = "VNM";

            ViSport_S2_SMS_MTController.InsertVClip(objMt);

            #endregion

            return "1";

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

    public void VisportSaveMtContent(string userId, string mtMessage, string commandCode)
    {
        var objMt = new ViSport_S2_SMS_MTInfo();
        objMt.User_ID = userId;
        objMt.Message = mtMessage;
        objMt.Service_ID = "979";
        objMt.Command_Code = commandCode;
        objMt.Message_Type = 1;
        objMt.Request_ID = "0";
        objMt.Total_Message = 1;
        objMt.Message_Index = 0;
        objMt.IsMore = 0;
        objMt.Content_Type = 0;
        objMt.ServiceType = 0;
        objMt.ResponseTime = DateTime.Now;
        objMt.isLock = false;
        objMt.PartnerID = "VNM";
        objMt.Operator = GetTelco(userId);
        objMt.IsQuestion = 0;

        ViSport_S2_SMS_MTController.InsertSportGameHeroMt(objMt);
    }

    [WebMethod]
    public string WsBigPromotionMoProcess(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExecuteRequestMoBigPromotionProcess(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }
    [WebMethod]
    public string WSProcessMoViSportWap(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExcecuteRequestMoViSportWap(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }

    [WebMethod]
    public string WSProcessMoViSportWap_New(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExcecuteRequestMoViSportWap_New(User_ID, Service_ID, Command_Code, Message, Request_ID);
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

            //if (!filterMsisdn(User_ID))
            //{
            //    return "0";
            //}

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
                    string ringToneRes = ringTone.SyncSubscriptionData("949", "DK", User_ID, Message.ToUpper(), Request_ID, "472", "0", "1", "DK GOI");

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

            if (!filterMsisdn(User_ID))
            {
                return "0";
            }

            #region MO LOG

            Vtv6WorldCupLogMo(User_ID, Service_ID, Command_Code, Message, Request_ID, 0);

            #endregion

            string mtContent = string.Empty;

            int msgType = (int)Constant.MessageType.NoCharge;

            if (Command_Code == "VTV" && subcode == "")
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

            //if (!filterMsisdn(User_ID))
            //{
            //    return "0";
            //}

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

                if (msg == "1")//CHARGED THANH CONG
                {
                    DataTable dtGame = VoteRegisterController.Mo949GetRandomGame();
                    url = "";
                    if (dtGame != null && dtGame.Rows.Count > 0)
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
                    SendMtMo949(User_ID, messageContent, Service_ID, Command_Code, Request_ID);
                }
            }
            else if (Command_Code.ToUpper() == "NCHAY" && subcode == "")
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
            else if (Command_Code.ToUpper() == "VIDEOHAY" && subcode == "")
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
            else if (Command_Code.ToUpper() == "TRUYENHOT" && subcode == "")
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

            if (msg == "1")
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
                SendMtMo949(User_ID, messageContent, Service_ID, Command_Code, Request_ID);
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

        Command_Code = Command_Code.Trim().ToUpper();
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

            //if (!filterMsisdn(User_ID))
            //{
            //    return "0";
            //}

            #region Log MO Message Into Database (SMS_MO_Log)


            //if (AppEnv.GetSetting("TestFlag") == "0")
            //{
            var moInfo = new SMS_MOInfo();
            moInfo.User_ID = User_ID;
            moInfo.Service_ID = Service_ID;
            moInfo.Command_Code = Command_Code;
            moInfo.Message = Message;
            moInfo.Request_ID = Request_ID;
            moInfo.Operator = GetTelco(User_ID);
            SMS_MODB.InsertSportGameHeroMo(moInfo);
            //}


            #endregion

            string messageReturn = "";

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
            //else if ((Command_Code == "TP" || Command_Code == "TP1") && subcode == "") //DK DICH VU TRIEU_PHU_BONG_DA
            //{
            //    #region DK DICH VU

            //    var entity = new ViSport_S2_Registered_UsersInfo();
            //    entity.User_ID = User_ID;
            //    entity.Request_ID = Request_ID;
            //    entity.Service_ID = Service_ID;
            //    entity.Command_Code = Command_Code;
            //    entity.Service_Type = 1;
            //    entity.Charging_Count = 0;
            //    entity.FailedChargingTimes = 0;
            //    entity.RegisteredTime = DateTime.Now;
            //    entity.ExpiredTime = DateTime.Now.AddDays(1);
            //    entity.Registration_Channel = "SMS";
            //    entity.Status = 1;
            //    entity.Operator = GetTelco(User_ID);
            //    entity.Point = 2;

            //    string passWord = RandomActiveCode.RandomStringNumber(6);
            //    entity.Password = passWord;

            //    DataTable value = ViSport_S2_Registered_UsersController.InsertSportGameHeroRegisterUser(entity);

            //    if (value.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
            //    {

            //        #region DK DV LAN DAU TIEN ==> KM 2 MDT

            //        if (AppEnv.GetSetting("CTKM_Flag") == "0")
            //        {
            //            #region SINH MA_DU_THUONG

            //            string code1 = RandomActiveCode.Generate(8);
            //            string code2 = RandomActiveCode.Generate(8);
            //            ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(User_ID, code1);
            //            ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(User_ID, code2);

            //            #endregion
            //        }


            //        if (Command_Code == "TP")
            //        {
            //            messageReturn = "Chuc mung ban da dang ky thanh cong dich vu Visport cua Vietnamobile. ";
            //            if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
            //            {
            //                messageReturn = messageReturn + "Ban co 5.000 diem voi co hoi trung thuong 1 dien thoai Samsung Galaxy A5. ";
            //            }
            //            messageReturn = messageReturn + "Mien phi ngay dau tien cho thue bao lan dau dang ky. Dang ky dich vu, ban duoc xem, nghe, tai toan bo noi dung mien phi. " +
            //                            "Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han).  Huy dvu soan: HUY TP gui 979. HT: 19001255.";
            //            if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
            //            {
            //                messageReturn = messageReturn + "De biet chi tiet CTKM, soan HD gui 979, xem diem va so diem cao nhat hien tai soan TOP gui 979";
            //            }

            //        }
            //        else if (Command_Code == "TP1")
            //        {
            //            messageReturn = "Ban la khach hang may may duoc tham gia CTKM Trieu phu bong da cua Vietnamobile. " +
            //                            "Ban se duoc trai nghiem mien phi dich vu trong 3 ngay va duoc tang 5000 diem voi co hoi so huu samsung galaxy A5, " +
            //                            "moi ngay ban se nhan duoc tin tuc bong da nong hoi trong nuoc va ngoai nuoc (5000d/ngay). " +
            //                            "De kiem tra diem truy cap: http://visport.vn." +
            //                            "Sau khi het khuyen mai 3 ngay, he thong se tu dong huy toan bo dich vu cho khach hang. De huy dich vu soan: HUY TP1 gui 979. HT: 19001255";
            //        }
            //        else
            //        {
            //            messageReturn = string.Empty;
            //        }

            //        if (!string.IsNullOrEmpty(messageReturn))
            //        {
            //            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1
            //        }

            //        #endregion

            //    }
            //    else if (value.Rows[0]["RETURN_ID"].ToString() == "2")
            //    {

            //        #region DA DK DV ROI XONG HUY DK LAI ==> TRA MT WELCOME

            //        if (Command_Code == "TP")
            //        {
            //            messageReturn = "Chuc mung ban da dang ky thanh cong dich vu Visport cua Vietnamobile. Dang ky dich vu, ban duoc xem, nghe, tai toan bo noi dung mien phi. ";
            //            if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
            //            {
            //                messageReturn = messageReturn + "  Ban co 5.000 diem voi co hoi trung thuong 1 dien thoai Samsung Galaxy A5.";
            //            }
            //            messageReturn = messageReturn + "Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han). Huy dvu soan: HUY TP gui 979. HT: 19001255";
            //        }
            //        else
            //        {
            //            messageReturn = "Ban la khach hang may may duoc tham gia CTKM Trieu phu bong da cua Vietnamobile. Ban se duoc trai nghiem mien phi dich vu trong 3 ngay va co 2 ma du thuong de co co hoi trung thuong samsung galaxy S4, moi ngay ban se duoc tra loi cau hoi va du doan ket qua de nang cao co hoi trung thuong (5000d/ngay). De kiem tra MDT truy cap: http://visport.vn. Sau khi het khuyen mai 3 ngay, he thong se tu dong huy toan bo dich vu cho khach hang. De huy dich vu soan: HUY TP1 gui 979. HT: 19001255";
            //        }

            //        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1

            //        #endregion

            //    }
            //    else if (value.Rows[0]["RETURN_ID"].ToString() == "1")//DA DK DICH VU
            //    {

            //        #region THUE BAO DANG ACTIVE DV

            //        messageReturn = "Ban dang su dung dich vu Visport cua Vietnamobile.";
            //        if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
            //        {
            //            //messageReturn = messageReturn + " Ban co 5.000 diem voi co hoi trung thuong 1 dien thoai Samsung Galaxy A5. ";
            //        }
            //        messageReturn = messageReturn + " Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han). Huy dvu soan: HUY TP gui 979. HT: 19001255";
            //        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);

            //        #endregion

            //    }

            //    #endregion
            //}
            else if (Command_Code.ToUpper() == "MDT" && subcode == "")
            {

                #region TRA CUU MADUTHUONG

                DataTable dtCount = ViSport_S2_Registered_UsersController.SportGameHeroCountLotteryCode(User_ID);
                string count = "0";
                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    count = dtCount.Rows[0]["Total"].ToString();
                }

                messageReturn = "Quy khach dang co " + count + " ma du thuong de quay thuong CTKM Chuyen gia bong da cua Vietnamobile voi co hoi trung thuong 1 dien thoai Samsung Galaxy S4. " +
                                "Chi tiet truy cap http://visport.vn. HT: 19001255";

                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);

                #endregion

            }
            else if (Command_Code.ToUpper() == "DIEM" && subcode == "") //TRA DIEM TRIEU PHU BONG DA
            {


                #region TRA CUU DIEM

                //DataTable dt = ViSport_S2_Registered_UsersController.GetSportGameHeroUserInfo(User_ID);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    int point = ConvertUtility.ToInt32(dt.Rows[0]["Point"].ToString());
                //    int somaduthuong = point / 10;

                //    messageReturn = AppEnv.GetSetting("AnhTaiBongDa_KiemTraMaDuThuongThanhCong").Replace("xxxx", point.ToString());
                //    messageReturn = messageReturn.Replace("yyyy", somaduthuong.ToString());

                //    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                //}
                string mtx = "";
                string sodiem = "0";
                DataSet dset = ViSport_S2_Registered_UsersController.SportGameHero_GetInfoUserTPBD(User_ID);
                if (dset.Tables[0] != null && dset.Tables[0].Rows.Count > 0)
                {
                    sodiem = ConvertUtility.ToString(dset.Tables[0].Rows[0]["Total"]);
                }
                if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                {
                    mtx = "Ban dang co <DIEM> diem trong cuoc dua den giai thuong " + AppEnv.GetSetting("GiaithuongCTKM") + ". Soan TOP gui 979 de biet vi tri cua minh tren bang xep hang";
                    SendMtSportGameHero(User_ID, mtx.Replace("<DIEM>", sodiem), Service_ID, Command_Code, Request_ID, 0);
                }

                return responseValue;

                #endregion
            }
            else if (Command_Code.ToUpper() == "TOP" && subcode == "") //TRA VI TRI TRONG BXH DICH VU TRIEU PHU BONG DA
            {
                if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                {
                    #region TRA CUU TOP

                    string mtx = "";
                    string sodiem = "0";
                    int thutu = 0;
                    var webServiceCharging3G = new WebServiceCharging3g();
                    const string userName = "VMGViSport";
                    const string userPass = "v@#port";
                    const string cpId = "1930";
                    string price = "2000";
                    const string content = "Charged trieu phu bong da";
                    const string serviceName = "TPBD";
                    string returnchargValue = webServiceCharging3G.PaymentVnmWithAccount(User_ID, price, content, serviceName, userName, userPass, cpId);
                    //string returnchargValue = "1";
                    if (returnchargValue == "1")
                    {
                        #region LOG DOANH THU

                        DataTable dtUsers = ViSport_S2_Registered_UsersController.GetSportGameHeroUserInfoActive(User_ID, 1);
                        DataRow dr = dtUsers.Rows[0];

                        var logInfo = new SportGameHeroChargedUserLogInfo();

                        logInfo.ID = ConvertUtility.ToInt32(dr["ID"].ToString());
                        logInfo.User_ID = User_ID;
                        logInfo.Request_ID = dr["Request_ID"].ToString();
                        logInfo.Service_ID = dr["Service_ID"].ToString();
                        logInfo.Command_Code = dr["Command_Code"].ToString();

                        logInfo.Service_Type = ConvertUtility.ToInt32(dr["Service_Type"].ToString());
                        logInfo.Charging_Count = ConvertUtility.ToInt32(dr["Charging_Count"].ToString());
                        logInfo.FailedChargingTime = ConvertUtility.ToInt32(dr["FailedChargingTimes"].ToString());

                        logInfo.RegisteredTime = ConvertUtility.ToDateTime(dr["RegisteredTime"].ToString());
                        logInfo.ExpiredTime = DateTime.Now.AddDays(1);

                        logInfo.Registration_Channel = dr["Registration_Channel"].ToString();
                        logInfo.Status = ConvertUtility.ToInt32(dr["Status"].ToString());
                        logInfo.Operator = dr["Operator"].ToString();
                        logInfo.Price = ConvertUtility.ToInt32(price);
                        logInfo.Reason = "Succ";

                        ViSport_S2_Registered_UsersController.InsertSportGameHeroChargedUserLogForSub(logInfo);

                        #endregion
                        ViSport_S2_Registered_UsersController.Update_SportGameHeroChargedValue(User_ID, ConvertUtility.ToInt32(price));
                        DataSet dset = ViSport_S2_Registered_UsersController.SportGameHero_GetInfoUserTPBD(User_ID);
                        if (dset.Tables[0] != null && dset.Tables[0].Rows.Count > 0)
                        {
                            sodiem = ConvertUtility.ToString(dset.Tables[0].Rows[0]["Total"]);
                            thutu = ConvertUtility.ToInt32(dset.Tables[0].Rows[0]["row"]);
                        }
                        if (thutu > 0)
                        {
                            if (thutu <= 5)
                            {
                                mtx = "Ban dang co <DIEM> diem va nam trong top 5 diem cao nhat. Giai thuong " + AppEnv.GetSetting("GiaithuongCTKM") + " dang nam trong tam tay ban. Co gang len.";
                            }
                            else if (thutu > 5 && thutu <= 10)
                            {
                                mtx = "Ban dang co <DIEM> diem va nam trong top 10 diem cao nhat. Giai thuong " + AppEnv.GetSetting("GiaithuongCTKM") + " dang nam trong tam tay ban. Co gang len.";
                            }
                            else if (thutu > 10 && thutu < 100)
                            {
                                mtx = "Ban dang co <DIEM> diem va nam trong top 100 diem cao nhat. Giai thuong " + AppEnv.GetSetting("GiaithuongCTKM") + " dang nam trong tam tay ban. Co gang len.";
                            }
                            else if (thutu >= 100)
                            {
                                mtx = "Ban dang co <DIEM> diem. Co gang len de lot vao top 5 voi co hoi trung thuong " + AppEnv.GetSetting("GiaithuongCTKM") + " danh cho KH co diem so cao nhat.";
                            }
                            SendMtSportGameHero(User_ID, mtx.Replace("<DIEM>", sodiem), Service_ID, Command_Code, Request_ID, 0);

                        }

                    }
                    else
                    {
                        price = "1000";
                        returnchargValue = webServiceCharging3G.PaymentVnmWithAccount(User_ID, price, content, serviceName, userName, userPass, cpId);
                        if (returnchargValue == "1")
                        {
                            #region LOG DOANH THU

                            DataTable dtUsers = ViSport_S2_Registered_UsersController.GetSportGameHeroUserInfoActive(User_ID, 1);
                            DataRow dr = dtUsers.Rows[0];

                            var logInfo = new SportGameHeroChargedUserLogInfo();

                            logInfo.ID = ConvertUtility.ToInt32(dr["ID"].ToString());
                            logInfo.User_ID = User_ID;
                            logInfo.Request_ID = dr["Request_ID"].ToString();
                            logInfo.Service_ID = dr["Service_ID"].ToString();
                            logInfo.Command_Code = dr["Command_Code"].ToString();

                            logInfo.Service_Type = ConvertUtility.ToInt32(dr["Service_Type"].ToString());
                            logInfo.Charging_Count = ConvertUtility.ToInt32(dr["Charging_Count"].ToString());
                            logInfo.FailedChargingTime = ConvertUtility.ToInt32(dr["FailedChargingTimes"].ToString());

                            logInfo.RegisteredTime = ConvertUtility.ToDateTime(dr["RegisteredTime"].ToString());
                            logInfo.ExpiredTime = DateTime.Now.AddDays(1);

                            logInfo.Registration_Channel = dr["Registration_Channel"].ToString();
                            logInfo.Status = ConvertUtility.ToInt32(dr["Status"].ToString());
                            logInfo.Operator = dr["Operator"].ToString();
                            logInfo.Price = ConvertUtility.ToInt32(price);
                            logInfo.Reason = "Succ";

                            ViSport_S2_Registered_UsersController.InsertSportGameHeroChargedUserLogForSub(logInfo);

                            #endregion
                            ViSport_S2_Registered_UsersController.Update_SportGameHeroChargedValue(User_ID, ConvertUtility.ToInt32(price));
                            DataSet dset = ViSport_S2_Registered_UsersController.SportGameHero_GetInfoUserTPBD(User_ID);
                            if (dset.Tables[0] != null && dset.Tables[0].Rows.Count > 0)
                            {
                                sodiem = ConvertUtility.ToString(dset.Tables[0].Rows[0]["Total"]);
                                thutu = ConvertUtility.ToInt32(dset.Tables[0].Rows[0]["row"]);
                            }
                            if (thutu > 0)
                            {
                                if (thutu <= 5)
                                {
                                    mtx = "Ban dang co <DIEM> diem va nam trong top 5 diem cao nhat. Giai thuong " + AppEnv.GetSetting("GiaithuongCTKM") + " dang nam trong tam tay ban. Co gang len.";
                                }
                                else if (thutu > 5 && thutu <= 10)
                                {
                                    mtx = "Ban dang co <DIEM> diem va nam trong top 10 diem cao nhat. Giai thuong " + AppEnv.GetSetting("GiaithuongCTKM") + " dang nam trong tam tay ban. Co gang len.";
                                }
                                else if (thutu > 10 && thutu < 100)
                                {
                                    mtx = "Ban dang co <DIEM> diem va nam trong top 100 diem cao nhat. Giai thuong " + AppEnv.GetSetting("GiaithuongCTKM") + " dang nam trong tam tay ban. Co gang len.";
                                }
                                else if (thutu >= 100)
                                {
                                    mtx = "Ban dang co <DIEM> diem. Co gang len de lot vao top 5 voi co hoi trung thuong " + AppEnv.GetSetting("GiaithuongCTKM") + " danh cho KH co diem so cao nhat.";
                                }
                                SendMtSportGameHero(User_ID, mtx.Replace("<DIEM>", sodiem), Service_ID, Command_Code, Request_ID, 0);

                            }
                        }
                        else
                        {
                            mtx = "Tai khoan cua ban khong du, vui long nap them tien.";
                            SendMtSportGameHero(User_ID, mtx, Service_ID, Command_Code, Request_ID, 0);
                        }
                    }

                    #endregion
                }
                return responseValue;
            }
            else if (Command_Code.ToUpper() == "HD" && subcode == "") //HUONG DAN CHUONG TRINH
            {
                #region HUONGDAN DICHVU

                //messageReturn = AppEnv.GetSetting("AnhTaiBongDa_HuongDanThanhCong");
                messageReturn =
                    "Dich vu Visport dang cung cap thong tin the thao. Gia cuoc:5000 dong/ngay. De dang ky dich vu, soan: DK TP gui 979. De huy dich vu, soan HUY TP gui 979";

                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);

                return responseValue;
                #endregion
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
            else if (Command_Code.ToUpper() == "HUY" && (subcode.ToUpper() == "TP" || subcode.ToUpper() == "TP1")) //HUY DV TRIEU_PHU_BONG_DA
            {
                #region HUY DV TRIEU_PHU_BONG_DA

                DataTable dtUpdate = ViSport_S2_Registered_UsersController.UpdateSportGameHeroRegisterUser(User_ID, 0);
                //if (dtUpdate != null && dtUpdate.Rows.Count > 0)
                //{

                if (dtUpdate.Rows[0]["RETURN_ID"].ToString() == "1")
                {
                    if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                    {
                        messageReturn = AppEnv.GetSetting("HuyMT_KM");
                    }
                    else
                    {
                        messageReturn = AppEnv.GetSetting("HuyMT_notKM");
                    }

                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                }
                else
                {
                    if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                    {
                        messageReturn = AppEnv.GetSetting("HuyMTnotRegis_KM");
                    }
                    else
                    {
                        messageReturn = AppEnv.GetSetting("HuyMTnotRegis_notKM");
                    }
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                }
                //}
                //else
                //{

                //}

                #endregion
            }
            else if ((Command_Code.ToUpper() == "P1" || Command_Code.ToUpper() == "P2") && subcode == "") //TRA LOI CAU HOI
            {
                return responseValue;
            }
            else if ((Command_Code.ToUpper() == "1" || Command_Code.ToUpper() == "2") && subcode == "")
            {
                //string today = DateTime.Now.DayOfWeek.ToString();

                //if(!CheckDayOfWeek(today))
                //{

                DataTable dtCount = ViSport_S2_Registered_UsersController.GetSportGameHeroUserInfoActive(User_ID, 1);
                if (dtCount == null || dtCount.Rows.Count == 0)
                {
                    messageReturn = "Quy khach chua su dung dich vu Visport cua Vietnamobile. De dang ky su dung dich vu, soan TP gui 979. Chi tiet truy cap http://visport.vn. HT:19001255";
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                    return responseValue;
                }

                #region CAU HOI TPBD

                DataTable dtCountQues = ViSport_S2_Registered_UsersController.CountQuestionTodaySportGameHeroRegisterUser(User_ID);
                if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) == 0)
                {
                    messageReturn = "Chuong trinh hom nay sap bat dau. San sang san giai thuong Samsung Galaxy S4. chi tiet truy cap http://visport.vn. HT: 19001255";
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                    return responseValue;
                }

                #region PROCESS CAUHOI

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
                            #region Du 5 CAUHOI TRONG NGAY

                            string code1 = RandomActiveCode.Generate(8);
                            ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(User_ID, code1);

                            messageReturn = messageReturn.Replace("xxx", code1);
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi DUNG

                            //CONG 1 MDT
                            ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUserTp(User_ID, questionId, 40, Request_ID, answerMt, 1);


                            messageReturn = "Quy khach da tra loi het 5 cau hoi mien phi hom nay. Nang cao co hoi trung thuong bang cach thu thach kien thuc cua minh voi cac cau hoi tiep theo (1000d/cau hoi).";
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi Het 5 Cau FREE

                            SendContinueQuestion(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO

                            #endregion
                        }
                        else if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) < 5)
                        {
                            #region CHUA DU 5 CAUHOI TRONG NGAY

                            string code1 = RandomActiveCode.Generate(8);
                            ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(User_ID, code1);

                            messageReturn = messageReturn.Replace("xxx", code1);
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi DUNG

                            //CONG 1 MDT
                            ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUserTp(User_ID, questionId, 40, Request_ID, answerMt, 1);

                            SendContinueQuestionTpBd(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO

                            #endregion
                        }
                        else if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) > 5)
                        {
                            #region CAC CAU HOI TINHPHI

                            if (ChuyenGiaBongDaCharged(User_ID) == "1")
                            {
                                #region Charged THANHCONG ==> GHI NHAN DAPAN && TRA CAU HOI TIEP

                                string code1 = RandomActiveCode.Generate(8);
                                ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(User_ID, code1);

                                messageReturn = messageReturn.Replace("xxx", code1);
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi DUNG

                                SendContinueQuestionTpBd(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO

                                #endregion
                            }
                            else if (ChuyenGiaBongDaCharged(User_ID) == "Result:12,Detail:Not enough money.")
                            {
                                #region Charged THATBAI ==> GUI ALERT THONG BAO NAP TIEN

                                messageReturn = "Thue bao khong du tien. Vui long nap tien de tiep tuc choi !";
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                                #endregion
                            }
                            else
                            {
                                #region Charged THATBAI ==> LOI SYSTEM

                                messageReturn = "He thong dang ban. Vui long tro lai sau it phut !";
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                                #endregion
                            }

                            #endregion
                        }
                    }
                    else//TRA LOI SAI
                    {
                        DataTable randomMsg = ViSport_S2_Registered_UsersController.GetMessageRandomSportGameHero(4);
                        messageReturn = randomMsg.Rows[0]["Message"].ToString();

                        if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) == 5)
                        {
                            #region DU 5 CAUUHOI TRONG NGAY

                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi SAI
                            //CONG 0 MDT
                            ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUserTp(User_ID, questionId, 20, Request_ID, answerMt, 0);

                            //messageReturn = "Ban da tra loi het so cau hoi mien phi trong ngay (5 cau). Hay tiep tuc tham gia chuong trinh vao ngay mai de tich luy ma du thuong de SO HUU iPhone 5S.";

                            messageReturn = "Ban da tra loi het so cau hoi mien phi trong ngay (5 cau). Hay tiep tuc tham gia chuong trinh vao ngay mai de tich luy ma du thuong de SO HUU Samsung Galaxy S4";
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi Het 5 Cau FREE

                            #endregion
                        }
                        else if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) < 5)
                        {
                            #region CHUA DU 5 CAUHOI TRONG NGAY

                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi SAI
                            //CONG 0 MDT
                            ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUserTp(User_ID, questionId, 20, Request_ID, answerMt, 0);

                            SendContinueQuestionTpBd(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO

                            #endregion
                        }
                        else if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) > 5)
                        {
                            #region CAC CAU HOI TINHPHI

                            if (ChuyenGiaBongDaCharged(User_ID) == "1")
                            {
                                #region Charged THANHCONG ==> GHI NHAN DAPAN && TRA CAU HOI TIEP

                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi SAI

                                SendContinueQuestionTpBd(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO

                                #endregion
                            }
                            else if (ChuyenGiaBongDaCharged(User_ID) == "Result:12,Detail:Not enough money.")
                            {
                                #region Charged THATBAI ==> GUI ALERT THONG BAO NAP TIEN

                                messageReturn = "Thue bao khong du tien. Vui long nap tien de tiep tuc choi !";
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                                #endregion
                            }
                            else
                            {
                                #region Charged THATBAI ==> LOI SYSTEM

                                messageReturn = "He thong dang ban. Vui long tro lai sau it phut !";
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                                #endregion
                            }

                            #endregion
                        }
                    }

                }

                #endregion


                #endregion
                //}
            }
            else if (Command_Code.ToUpper() == "KTDV" && subcode == "")
            {
                #region KTDV

                DataTable dtChk = ViSport_S2_Registered_UsersController.CheckUserRegisterAllService(User_ID);

                if (dtChk != null && dtChk.Rows.Count > 0)
                {
                    DateTime date = ConvertUtility.ToDateTime(dtChk.Rows[0]["RegisteredTime"].ToString());
                    string d1 = date.ToString("dd/MM/yyyy HH:mm");
                    messageReturn = AppEnv.GetSetting("Ktdv_Mt_Content").Replace("dd/mm/yyyy HH:MM", date.ToString("dd/mm/yyyy HH:MM")).Replace("SMS(/USSD/WAP)", dtChk.Rows[0]["Registration_Channel"].ToString());
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
            else if (Command_Code == "KQ" && (subcode == "1" || subcode == "2" || subcode == "3"))
            {
                return responseValue;

                #region DU DOAN KQ TRAN DAU

                string today = DateTime.Now.DayOfWeek.ToString();

                if (CheckDayOfWeek(today))
                {
                    DataTable dt = ViSport_S2_Registered_UsersController.SportGameHeroMatchGetByDay();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int matchId = ConvertUtility.ToInt32(dt.Rows[0]["Id"].ToString());
                        string teamWin = "HOA";

                        if (subcode == "1")
                            teamWin = dt.Rows[0]["Team_A_Code"].ToString();
                        else if (subcode == "3")
                            teamWin = dt.Rows[0]["Team_B_Code"].ToString();

                        ViSport_S2_Registered_UsersController.SportGameHeroMatchVoteInsert(User_ID, matchId, Message.Trim(), teamWin, null, null, null, null, Command_Code);
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
            else if (Command_Code == "BT" && subcode != "")
            {
                return responseValue;

                #region DU DOAN TONG SO BAN THANG

                string today = DateTime.Now.DayOfWeek.ToString();

                if (CheckDayOfWeek(today))
                {
                    DataTable dt = ViSport_S2_Registered_UsersController.SportGameHeroMatchGetByDay();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int matchId = ConvertUtility.ToInt32(dt.Rows[0]["Id"].ToString());
                        int goal = ConvertUtility.ToInt32(subcode);

                        ViSport_S2_Registered_UsersController.SportGameHeroMatchVoteInsert(User_ID, matchId, Message.Trim(), null, goal, null, null, null, Command_Code);

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
            else if (Command_Code == "TS" && subcode != "")
            {
                return responseValue;

                #region DU DOAN TY SO TRAN DAU

                string today = DateTime.Now.DayOfWeek.ToString();

                if (CheckDayOfWeek(today))
                {
                    DataTable dt = ViSport_S2_Registered_UsersController.SportGameHeroMatchGetByDay();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int matchId = ConvertUtility.ToInt32(dt.Rows[0]["Id"].ToString());
                        ViSport_S2_Registered_UsersController.SportGameHeroMatchVoteInsert(User_ID, matchId, Message.Trim(), null, null, subcode.Trim().Replace(" ", "-"), null, null, Command_Code);

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
                return responseValue;

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

                        ViSport_S2_Registered_UsersController.SportGameHeroMatchVoteInsert(User_ID, matchId, Message.Trim(), null, null, null, teamPossession, null, Command_Code);

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
            else if (Command_Code == "TV" && subcode != "")
            {
                return responseValue;

                #region DU DOAN SO THE VANG

                string today = DateTime.Now.DayOfWeek.ToString();

                if (CheckDayOfWeek(today))
                {
                    DataTable dt = ViSport_S2_Registered_UsersController.SportGameHeroMatchGetByDay();
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int matchId = ConvertUtility.ToInt32(dt.Rows[0]["Id"].ToString());

                        int yellowCard = ConvertUtility.ToInt32(subcode);

                        ViSport_S2_Registered_UsersController.SportGameHeroMatchVoteInsert(User_ID, matchId, Message.Trim(), null, null, null, null, yellowCard, Command_Code);

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
            else if (Command_Code == "MK" && subcode == "")
            {
                return responseValue;

                #region GUI LAI MATKHAU CHO USER

                DataTable dt = ViSport_S2_Registered_UsersController.GetSportGameHeroUserInfo(User_ID);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string passWord = dt.Rows[0]["PassWord"].ToString();
                    if (string.IsNullOrEmpty(passWord))
                    {
                        passWord = RandomActiveCode.RandomStringNumber(6);
                        ViSport_S2_Registered_UsersController.UpdateSportGameHeroRegisterUserPassWord(User_ID, passWord);//UPDATE Password moi cho USER
                    }
                    messageReturn = "Tai khoan va mat khau su dung dich vu của Quy Khach la:  username: " + User_ID + ", password: " + passWord + ". Vui long truy cap website hoac wapsite http://visport.vn de su dung dich vu";
                }
                else
                {
                    messageReturn = "Quy khach chua dang ky su dung dich vu Visport cua Vietnamibile. Soan tin TP gui 979 de dang ky voi co hoi so huu giai thuong dien thoai Samsung Galaxy S4.  Chi tiet truy cap http://visport.vn. HT: 19001255";
                }
                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT GUI PassWord;

                #endregion
            }
            else
            {
                //messageReturn = "Tin nhan sai cu phap. De nhan 2 MDT va co co hoi trung 30 trieu tien mat, iPhone 5S sanh dieu soan TP gui 979 (mien phi dang ky), de biet them thong tin soan: HD gui 979. HT: 19001255";
                messageReturn = "Tin nhan sai cu phap. Chi tiet truy cap http://visport.vn. HT: 19001255";
                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
            }
        }
        catch (Exception ex)
        {
            responseValue = "1";
            log.Debug("---------------Error sentMT ChuyenGiaBongDa----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }

        return responseValue;
    }

    private string ExcecuteRequestMo(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        string responseValue = "1";
        int returnValue = 0;
        string syntax = AppEnv.GetSetting("regisVisport");
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

            //if (!filterMsisdn(User_ID))
            //{
            //    return "0";
            //}

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

            #region Check user Confirm 
            if (Command_Code.ToUpper() == "Y")
            {
                DataTable dtuser = SMS_MODB.GetUserRegister(User_ID, Service_ID);
                if (dtuser != null && dtuser.Rows.Count > 0)
                {
                    Message = dtuser.Rows[0]["Message"].ToString();
                    Command_Code = dtuser.Rows[0]["Command_Code"].ToString();
                    if (Message.Trim().Length > Command_Code.Trim().Length)
                    {
                        subcode = Message.ToUpper().Substring(Command_Code.Length).Replace(" ", "");
                    }
                    
                    SMS_MODB.UpdateUser_Confirm(User_ID, Service_ID);
                    #region DK & DANGKY

                    if (Command_Code.ToUpper() == "DK" && (subcode.ToUpper() == "TP" || subcode.ToUpper() == "KM"))
                    {
                        if (subcode.ToUpper() == "TP" && DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                        {
                            return responseValue;
                        }
                        string messageReturn = "";
                        #region DK DICH VU

                        var entity = new ViSport_S2_Registered_UsersInfo();
                        entity.User_ID = User_ID;
                        entity.Request_ID = Request_ID;
                        entity.Service_ID = Service_ID;
                        if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                        {
                            entity.Command_Code = "DK KM";
                        }
                        else if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM2")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM2")))
                        {
                            entity.Command_Code = "DK KM";
                        }
                        else
                        {
                            entity.Command_Code = Command_Code;
                        }
                        entity.Service_Type = 1;
                        entity.Charging_Count = 0;
                        entity.FailedChargingTimes = 0;
                        entity.RegisteredTime = DateTime.Now;
                        entity.ExpiredTime = DateTime.Now.AddDays(1);
                        entity.Registration_Channel = "SMS";
                        entity.Status = 1;
                        entity.Operator = GetTelco(User_ID);
                        entity.Point = 2;

                        string passWord = RandomActiveCode.RandomStringNumber(6);
                        entity.Password = passWord;

                        DataTable value = ViSport_S2_Registered_UsersController.InsertSportGameHeroRegisterUser(entity);
                        if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                        {
                            ViSport_S2_Registered_UsersController.InsertUserTo56(User_ID);
                        }
                        else if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM2")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM2")))
                        {
                            ViSport_S2_Registered_UsersController.InsertUserTo56(User_ID);
                        }

                        if (value.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
                        {

                            #region DK DV LAN DAU TIEN ==> KM 2 MDT

                            if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                            {
                                messageReturn = AppEnv.GetSetting("RegisMT_KM");
                            }
                            else if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM2")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM2")))
                            {
                                //messageReturn = AppEnv.GetSetting("RegisMT_KM2");
                                //messageReturn = "Chuc mung ban da dang ky thanh cong dich vu Visport cua Vietnamobile. Mien phi ngay dau tien cho thue bao lan dau dang ky. Dang ky dich vu, ban duoc xem, nghe, tai toan bo noi dung mien phi. Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han). Huy dvu soan: HUY TP gui 979. HT: 19001255";
                                messageReturn = "Ban da dang ky thanh cong CT iPhone 6s trao tay TRI AN khach hang Vietnamobile cua dv Visport . KH co so diem cao nhat se so huu iPhone 6s vao cuoi CT. Duy tri dich vu de nhan diem hang ngay. Truy cap bang 3g vao trang http://visport.vn  de su dung dvu (5000d/ngay). De biet ro hon ve chuong trinh soan HD gui 2288. De huy dich vu soan HUY KM gui 979";
                            }
                            else
                            {
                                //messageReturn = AppEnv.GetSetting("RegisMT_notKM");
                                messageReturn = "Chuc mung ban da dang ky thanh cong dich vu Visport cua Vietnamobile. Mien phi ngay dau tien cho thue bao lan dau dang ky. Dang ky dich vu, ban duoc xem, nghe, tai toan bo noi dung mien phi. Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han). Huy dvu soan: HUY TP gui 979. HT: 19001255";
                            }

                            if (!string.IsNullOrEmpty(messageReturn))
                            {
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1
                            }

                            #endregion

                        }
                        else if (value.Rows[0]["RETURN_ID"].ToString() == "2")
                        {

                            #region DA DK DV ROI XONG HUY DK LAI ==> TRA MT WELCOME

                            if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                            {
                                messageReturn = AppEnv.GetSetting("RegisMT_KM");
                            }
                            else if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM2")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM2")))
                            {
                                //messageReturn = AppEnv.GetSetting("RegisMT_KM2");
                                //messageReturn = "Chuc mung ban da dang ky thanh cong dich vu Visport cua Vietnamobile. Dang ky dich vu, ban duoc xem, nghe, tai toan bo noi dung mien phi. Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han). Huy dvu soan: HUY TP gui 979. HT: 19001255";
                                messageReturn = "Ban da dang ky thanh cong CT iPhone 6s trao tay TRI AN khach hang Vietnamobile cua dv Visport . KH co so diem cao nhat se so huu iPhone 6s vao cuoi CT. Duy tri dich vu de nhan diem hang ngay. Truy cap bang 3g vao trang http://visport.vn  de su dung dvu (5000d/ngay). De biet ro hon ve chuong trinh soan HD gui 2288. De huy dich vu soan HUY KM gui 979";
                            }
                            else
                            {
                                //messageReturn = AppEnv.GetSetting("RegisMT_notKM");
                                messageReturn = "Chuc mung ban da dang ky thanh cong dich vu Visport cua Vietnamobile. Dang ky dich vu, ban duoc xem, nghe, tai toan bo noi dung mien phi. Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han). Huy dvu soan: HUY TP gui 979. HT: 19001255";
                            }
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1

                            #endregion

                        }
                        else if (value.Rows[0]["RETURN_ID"].ToString() == "1")//DA DK DICH VU
                        {

                            #region THUE BAO DANG ACTIVE DV

                            if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                            {
                                messageReturn = AppEnv.GetSetting("DoubleRegisMT_KM");
                            }
                            else if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM2")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM2")))
                            {
                                //messageReturn = AppEnv.GetSetting("DoubleRegisMT_KM");
                                messageReturn = "Ban dang su dung dich vu Visport cua Vietnamobile. Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han). Huy dvu soan: HUY TP gui 979. HT: 19001255";
                            }
                            else
                            {
                                //messageReturn = AppEnv.GetSetting("DoubleRegisMT_notKM");
                                messageReturn = "Ban dang su dung dich vu Visport cua Vietnamobile. Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han). Huy dvu soan: HUY TP gui 979. HT: 19001255";
                            }
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);

                            #endregion

                        }

                        #endregion
                        return responseValue;
                    }
                    #region dịch vụ thông tin thể thao(DK PZSP tới 979)
                    else if (Command_Code.ToUpper() == "DK" && subcode.ToUpper() == "PZSP")
                    {
                        string messageReturn = "";
                        #region DK DICH VU

                        var entity = new ViSport_S2_Registered_UsersInfo();
                        entity.User_ID = User_ID;
                        entity.Request_ID = Request_ID;
                        entity.Service_ID = Service_ID;
                        entity.Command_Code = "DK PZSP";
                        entity.Service_Type = 2;
                        entity.Charging_Count = 0;
                        entity.FailedChargingTimes = 0;
                        entity.RegisteredTime = DateTime.Now;
                        entity.ExpiredTime = DateTime.Now.AddDays(7);
                        entity.Registration_Channel = "SMS";
                        entity.Status = 1;
                        entity.Operator = GetTelco(User_ID);
                        entity.Point = 2;

                        string passWord = RandomActiveCode.RandomStringNumber(6);
                        entity.Password = passWord;

                        DataTable value = ViSport_S2_Registered_UsersController.Visport_PZSP_222_Insert(entity);

                        if (value.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
                        {

                            #region DK DV LAN DAU TIEN 
                            messageReturn = "Quy khach duoc tang 07 ngay mien phi dich vu The Thao. Cap nhat cac tin tuc the thao moi nhat tai: http://visport.vn .Phi dich vu: 10,000d/30 ngay duoc tinh tu ngay " + DateTime.Now.AddDays(7).ToString("dd/MM/yyyy") + " va se tu dong gia han. De huy dich vu, soan HUY SP gui 222. Chi tiet lien he 19001255";

                            if (!string.IsNullOrEmpty(messageReturn))
                            {
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1
                            }
                            #endregion

                        }
                        else if (value.Rows[0]["RETURN_ID"].ToString() == "2")
                        {
                            #region DA DK DV ROI XONG HUY DK LAI ==> TRA MT WELCOME  
                            string val_charge_return = VisportCharged(User_ID, "10000", Request_ID);//Charge khi đăng ký lại ngày thứ 2 trở đi

                            if (val_charge_return == "1")
                            {
                                #region Charged THANHCONG ==> Trả MT

                                messageReturn = "Cap nhat cac tin tuc the thao moi nhat tai: http://visport.vn .Phi dich vu: 10,000d/30 ngay duoc tinh tu ngay " + DateTime.Now.ToString("dd/MM/yyyy") + " va se tu dong gia han. De huy dich vu, soan HUY SP gui 222. Chi tiet lien he 19001255";
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao

                                #endregion
                            }
                            else if (val_charge_return == "Result:12,Detail:Not enough money.")
                            {
                                #region Charged THATBAI ==> GUI ALERT THONG BAO NAP TIEN

                                messageReturn = "Thue bao khong du tien. Vui long nap tien de tiep tuc choi !";
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                                #endregion
                            }
                            else
                            {
                                #region Charged THATBAI ==> LOI SYSTEM

                                messageReturn = "He thong dang ban. Vui long tro lai sau it phut !";
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                                #endregion
                            }
                            #endregion


                        }
                        else if (value.Rows[0]["RETURN_ID"].ToString() == "1")//DA DK DICH VU
                        {
                            #region THUE BAO DANG ACTIVE DV  
                            messageReturn = "Quy khach hien dang su dung goi dich vụ The Thao cua Vietnamobile. De biet them chi tiet truy cap  http://visport.vn hoac lien he 19001255";

                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                            #endregion

                        }

                        #endregion
                        return responseValue;
                    }
                    #endregion

                    else if (Command_Code.ToUpper() == "DK" || Command_Code.ToUpper() == "DANGKY")
                    {
                        string responseMsg = "";
                        if (subcode.ToUpper() == "TT" || subcode.ToUpper() == "MU")
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
                }
                return "1";
            }
            #endregion

            #region HUY gui 979

            if (Command_Code.ToUpper() == "HUY")
            {
                if (subcode == "") //HUY Dich Vu ViSport SMS HANG NGAY FREE
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
                else if (subcode == "TT")//HUY Dich Vu TinHangNgay
                {
                    DataTable dt = ViSport_S2_Registered_UsersController.DeleteSmsSpamUser(User_ID, subcode.ToUpper());
                    if (dt != null && dt.Rows.Count > 0)
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
                else if (subcode == "MU")//HUY Dich Vu MU
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
                else if (subcode.ToUpper() == "EU")
                {
                    #region HUY DV Chay cung euro

                    DataTable dtUpdate = ViSport_S2_Registered_UsersController.UpdateEuroRegisterUser(User_ID, 0);
                    //if (dtUpdate != null && dtUpdate.Rows.Count > 0)
                    //{

                    if (dtUpdate.Rows[0]["RETURN_ID"].ToString() == "1")
                    {
                        string messageReturn = AppEnv.GetSetting("AnhTaiBongDa_HuyDichVuThanhCong");
                        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                    }
                    else
                    {
                        string messageReturn = AppEnv.GetSetting("AnhTaiBongDa_ChuaDangKy");
                        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                    }
                    //}
                    //else
                    //{

                    //}

                    #endregion
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
                else if (subcode.ToUpper() == "KM")
                {
                    #region HUY DV vmgame + visport
                    string messageReturn = "";
                    DataTable dtUpdate = ViSport_S2_Registered_UsersController.UpdateSportGameHeroRegisterUser(User_ID, 0);


                    //if (dtUpdate != null && dtUpdate.Rows.Count > 0)
                    //{

                    if (dtUpdate.Rows[0]["RETURN_ID"].ToString() == "1")
                    {
                        //ViSport_S2_Registered_UsersController.Users_CTKM_VMGAME_VISPORT_Delete(User_ID);

                        if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                        {
                            ViSport_S2_Registered_UsersController.Users_CTKM_VMGAME_VISPORT_Delete(User_ID);
                            messageReturn = AppEnv.GetSetting("HuyMT_KM");
                        }
                        else if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM2")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM2")))
                        {
                            messageReturn = AppEnv.GetSetting("HuyMT_KM2");
                        }
                        else
                        {
                            messageReturn = AppEnv.GetSetting("HuyMT_notKM");
                        }

                        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                    }
                    else
                    {
                        if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                        {
                            messageReturn = AppEnv.GetSetting("HuyMTnotRegis_KM");
                        }
                        else if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM2")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM2")))
                        {
                            messageReturn = AppEnv.GetSetting("HuyMTnotRegis_KM");
                        }
                        else
                        {
                            messageReturn = AppEnv.GetSetting("HuyMTnotRegis_notKM");
                        }
                        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                    }
                    //}
                    //else
                    //{

                    //}

                    #endregion
                }
                else if (subcode.ToUpper() == "PZSP")
                {// Hủy dịch vụ thể thao 
                    #region PZSP 222
                    string messageReturn = "";
                    DataTable dtUpdate = ViSport_S2_Registered_UsersController.Visport_Thethao222_Update_Status(User_ID, 0);
                    //if (dtUpdate != null && dtUpdate.Rows.Count > 0)
                    //{

                    if (dtUpdate.Rows[0]["RETURN_ID"].ToString() == "1")
                    {
                        messageReturn = "Quy khach da huy thanh cong goi Thong tin The Thao cua Vietnamobile. Tran trong cam on.";
                        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                    }
                    else
                    {

                        messageReturn = "Quy Khach chua su dung dich vu nay.LH 19001255";

                        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
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
            #region DK & DANGKY

            if (AppEnv.IsRightSyntax(syntax, Message) && Command_Code.ToUpper() != "HUY")
            {
                if (AppEnv.GetSetting("flagconfirm") == "1")
                {
                    #region Dang ky co confirm
                    //if (Command_Code.ToUpper() != "HUY")
                    //{
                        string mt = "Quy khach vui long soan Y gui 979 de xac nhan dong y dang ky va tu dong gia han hang ngay dich vu Visport voi gia dich vu:5.000d/ngay. Tran trong cam on";
                        SendMtSportGameHero(User_ID, mt, Service_ID, Command_Code, Request_ID, 0);
                        SMS_MODB.ViSport_Confirm_Register_Insert(User_ID, Service_ID, Command_Code, Message, Request_ID);
                    //}
                    return "1";
                    #endregion
                }
                else
                {
                    #region Dang ky ko confirm 
                    //if (Command_Code.ToUpper() == "DK" && (subcode.ToUpper() == "TP" || subcode.ToUpper() == "KM"))
                    //{
                    if (Command_Code.ToUpper() == "DK" && subcode.ToUpper() == "TP")
                    {
                        if (subcode.ToUpper() == "TP" && DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                        {
                            return responseValue;
                        }
                        string messageReturn = "";
                        #region DK DICH VU

                        var entity = new ViSport_S2_Registered_UsersInfo();
                        entity.User_ID = User_ID;
                        entity.Request_ID = Request_ID;
                        entity.Service_ID = Service_ID;
                        if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                        {
                            entity.Command_Code = "DK KM";
                        }
                        else if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM2")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM2")))
                        {
                            entity.Command_Code = "DK KM";
                        }
                        else
                        {
                            entity.Command_Code = Command_Code;
                        }
                        entity.Service_Type = 1;
                        entity.Charging_Count = 0;
                        entity.FailedChargingTimes = 0;
                        entity.RegisteredTime = DateTime.Now;
                        entity.ExpiredTime = DateTime.Now.AddDays(1);
                        entity.Registration_Channel = "SMS";
                        entity.Status = 1;
                        entity.Operator = GetTelco(User_ID);
                        entity.Point = 2;

                        string passWord = RandomActiveCode.RandomStringNumber(6);
                        entity.Password = passWord;

                        DataTable value = ViSport_S2_Registered_UsersController.InsertSportGameHeroRegisterUser(entity);
                        if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                        {
                            ViSport_S2_Registered_UsersController.InsertUserTo56(User_ID);
                        }
                        else if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM2")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM2")))
                        {
                            ViSport_S2_Registered_UsersController.InsertUserTo56(User_ID);
                        }

                        if (value.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
                        {

                            #region DK DV LAN DAU TIEN ==> KM 2 MDT

                            if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                            {
                                messageReturn = AppEnv.GetSetting("RegisMT_KM");
                            }
                            else if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM2")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM2")))
                            {
                                //messageReturn = AppEnv.GetSetting("RegisMT_KM2");
                                //messageReturn = "Chuc mung ban da dang ky thanh cong dich vu Visport cua Vietnamobile. Mien phi ngay dau tien cho thue bao lan dau dang ky. Dang ky dich vu, ban duoc xem, nghe, tai toan bo noi dung mien phi. Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han). Huy dvu soan: HUY TP gui 979. HT: 19001255";
                                messageReturn = "Ban da dang ky thanh cong CT iPhone 6s trao tay TRI AN khach hang Vietnamobile cua dv Visport . KH co so diem cao nhat se so huu iPhone 6s vao cuoi CT. Duy tri dich vu de nhan diem hang ngay. Truy cap bang 3g vao trang http://visport.vn  de su dung dvu (5000d/ngay). De biet ro hon ve chuong trinh soan HD gui 2288. De huy dich vu soan HUY KM gui 979";
                            }
                            else
                            {
                                //messageReturn = AppEnv.GetSetting("RegisMT_notKM");
                                messageReturn = "Chuc mung ban da dang ky thanh cong dich vu Visport cua Vietnamobile. Mien phi ngay dau tien cho thue bao lan dau dang ky. Dang ky dich vu, ban duoc xem, nghe, tai toan bo noi dung mien phi. Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han). Huy dvu soan: HUY TP gui 979. HT: 19001255";
                            }

                            if (!string.IsNullOrEmpty(messageReturn))
                            {
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1
                            }

                            #endregion

                        }
                        else if (value.Rows[0]["RETURN_ID"].ToString() == "2")
                        {

                            #region DA DK DV ROI XONG HUY DK LAI ==> TRA MT WELCOME

                            if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                            {
                                messageReturn = AppEnv.GetSetting("RegisMT_KM");
                            }
                            else if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM2")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM2")))
                            {
                                //messageReturn = AppEnv.GetSetting("RegisMT_KM2");
                                //messageReturn = "Chuc mung ban da dang ky thanh cong dich vu Visport cua Vietnamobile. Dang ky dich vu, ban duoc xem, nghe, tai toan bo noi dung mien phi. Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han). Huy dvu soan: HUY TP gui 979. HT: 19001255";
                                messageReturn = "Ban da dang ky thanh cong CT iPhone 6s trao tay TRI AN khach hang Vietnamobile cua dv Visport . KH co so diem cao nhat se so huu iPhone 6s vao cuoi CT. Duy tri dich vu de nhan diem hang ngay. Truy cap bang 3g vao trang http://visport.vn  de su dung dvu (5000d/ngay). De biet ro hon ve chuong trinh soan HD gui 2288. De huy dich vu soan HUY KM gui 979";
                            }
                            else
                            {
                                //messageReturn = AppEnv.GetSetting("RegisMT_notKM");
                                messageReturn = "Chuc mung ban da dang ky thanh cong dich vu Visport cua Vietnamobile. Dang ky dich vu, ban duoc xem, nghe, tai toan bo noi dung mien phi. Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han). Huy dvu soan: HUY TP gui 979. HT: 19001255";
                            }
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1

                            #endregion

                        }
                        else if (value.Rows[0]["RETURN_ID"].ToString() == "1")//DA DK DICH VU
                        {

                            #region THUE BAO DANG ACTIVE DV

                            if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                            {
                                messageReturn = AppEnv.GetSetting("DoubleRegisMT_KM");
                            }
                            else if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM2")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM2")))
                            {
                                //messageReturn = AppEnv.GetSetting("DoubleRegisMT_KM");
                                messageReturn = "Ban dang su dung dich vu Visport cua Vietnamobile. Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han). Huy dvu soan: HUY TP gui 979. HT: 19001255";
                            }
                            else
                            {
                                //messageReturn = AppEnv.GetSetting("DoubleRegisMT_notKM");
                                messageReturn = "Ban dang su dung dich vu Visport cua Vietnamobile. Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han). Huy dvu soan: HUY TP gui 979. HT: 19001255";
                            }
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);

                            #endregion

                        }

                        #endregion
                        return responseValue;
                    }
                    #region dịch vụ thông tin thể thao(DK PZSP tới 979)
                    else if (Command_Code.ToUpper() == "DK" && subcode.ToUpper() == "PZSP")
                    {
                        string messageReturn = "";
                        #region DK DICH VU

                        var entity = new ViSport_S2_Registered_UsersInfo();
                        entity.User_ID = User_ID;
                        entity.Request_ID = Request_ID;
                        entity.Service_ID = Service_ID;
                        entity.Command_Code = "DK PZSP";
                        entity.Service_Type = 2;
                        entity.Charging_Count = 0;
                        entity.FailedChargingTimes = 0;
                        entity.RegisteredTime = DateTime.Now;
                        entity.ExpiredTime = DateTime.Now.AddDays(7);
                        entity.Registration_Channel = "SMS";
                        entity.Status = 1;
                        entity.Operator = GetTelco(User_ID);
                        entity.Point = 2;

                        string passWord = RandomActiveCode.RandomStringNumber(6);
                        entity.Password = passWord;

                        DataTable value = ViSport_S2_Registered_UsersController.Visport_PZSP_222_Insert(entity);

                        if (value.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
                        {

                            #region DK DV LAN DAU TIEN 
                            messageReturn = "Quy khach duoc tang 07 ngay mien phi dich vu The Thao. Cap nhat cac tin tuc the thao moi nhat tai: http://visport.vn .Phi dich vu: 10,000d/30 ngay duoc tinh tu ngay " + DateTime.Now.AddDays(7).ToString("dd/MM/yyyy") + " va se tu dong gia han. De huy dich vu, soan HUY SP gui 222. Chi tiet lien he 19001255";

                            if (!string.IsNullOrEmpty(messageReturn))
                            {
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1
                            }
                            #endregion

                        }
                        else if (value.Rows[0]["RETURN_ID"].ToString() == "2")
                        {
                            #region DA DK DV ROI XONG HUY DK LAI ==> TRA MT WELCOME  
                            string val_charge_return = VisportCharged(User_ID, "10000", Request_ID);//Charge khi đăng ký lại ngày thứ 2 trở đi

                            if (val_charge_return == "1")
                            {
                                #region Charged THANHCONG ==> Trả MT

                                messageReturn = "Cap nhat cac tin tuc the thao moi nhat tai: http://visport.vn .Phi dich vu: 10,000d/30 ngay duoc tinh tu ngay " + DateTime.Now.ToString("dd/MM/yyyy") + " va se tu dong gia han. De huy dich vu, soan HUY SP gui 222. Chi tiet lien he 19001255";
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao

                                #endregion
                            }
                            else if (val_charge_return == "Result:12,Detail:Not enough money.")
                            {
                                #region Charged THATBAI ==> GUI ALERT THONG BAO NAP TIEN

                                messageReturn = "Thue bao khong du tien. Vui long nap tien de tiep tuc choi !";
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                                #endregion
                            }
                            else
                            {
                                #region Charged THATBAI ==> LOI SYSTEM

                                messageReturn = "He thong dang ban. Vui long tro lai sau it phut !";
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                                #endregion
                            }
                            #endregion


                        }
                        else if (value.Rows[0]["RETURN_ID"].ToString() == "1")//DA DK DICH VU
                        {
                            #region THUE BAO DANG ACTIVE DV  
                            messageReturn = "Quy khach hien dang su dung goi dich vụ The Thao cua Vietnamobile. De biet them chi tiet truy cap  http://visport.vn hoac lien he 19001255";

                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                            #endregion

                        }

                        #endregion
                        return responseValue;
                    }
                    #endregion

                    else if (Command_Code.ToUpper() == "DK" || Command_Code.ToUpper() == "DANGKY")
                    {
                        string responseMsg = "";
                        if (subcode.ToUpper() == "TT" || subcode.ToUpper() == "MU")
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
                }
            }
            else
            {
                //tin nhan dk sai cu phap
                string mt = AppEnv.GetSetting("AnhTaiBongDa_SaiCuPhap");
                SendMtSportGameHero(User_ID, mt, Service_ID, Command_Code, Request_ID, 0);
                SMS_MODB.ViSport_Confirm_Register_Insert(User_ID, Service_ID, Command_Code, Message, Request_ID);
                responseValue = "1";
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


                        //if (value.Rows[0]["RETURN_ID"].ToString() == "1")
                        //{
                        //    string time = ConvertUtility.ToDateTime(value.Rows[0]["ExpiredTime"]).ToString("dd/MM/yyyy");
                        //    if (regObject.Service_Type == 1)
                        //    {
                        //        message = AppEnv.GetSetting("alert_Already_Reg").Replace("dd/mm/yy", time); //Replace here
                        //    }
                        //    else
                        //    {
                        //        message = AppEnv.GetSetting("alert_SC_Already_Reg").Replace("dd/mm/yy", time);
                        //    }
                        //}

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
                    string message = "Tin nhan sai cu phap. Chi tiet truy cap http://visport.vn. HT: 19001255";

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

        //if (!filterMsisdn(User_ID))
        //{
        //    return "0";
        //}

        if (Command_Code.ToUpper() == "CLIP")
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
                else if (subcode == "OFF1")
                {
                    #region Huy DK USER Free 5 Day

                    DataTable dt = ViSport_S2_Registered_UsersController.Free5DayClipOff(User_ID, 0);
                    if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                    {
                        message = "QKhach da huy thanh cong DV VIDEO HOT hang ngay cua VNM.Truy cap: http://kho-clip.com de nhan huong dan dang ky lai dich vu Video cua Vietnamobile. HT: 19001255";
                        SendMtVmClip(User_ID, message, Service_ID, Command_Code, Request_ID);
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
                                message = "Chuc mung! Quy khach da Dky thanh cong DV VMclip," +
                                          "QKhach duoc mien phi ngay dau tien su dung dich vu, sau KM 2.000d/ngay va dvu duoc tu dong gia han. " +
                                          "Moi QK truy cap http://kho-clip.com/ de su dung dvu. De huy DK, soan: CLIP OFF gui 949. HT: 19001255.";
                            }
                            else if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                            {
                                message = "Quy Khach da dang ky dich vu VMclip truoc do. Moi QK truy cap http://kho-clip.com/ de su dung dvu.. HT: 19001255";
                            }
                            else if (dt.Rows[0]["RETURN_ID"].ToString() == "2")
                            {
                                message = "Chuc mung! Quy khach da Dky thanh cong DV VMclip. Moi QK truy cap http://kho-clip.com/ de su dung dvu (2.000d/ngay),dvu duoc tu dong gia han. De huy DK, soan: CLIP OFF gui 949. HT: 19001255.";
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
        else if (Command_Code.ToUpper() == "DK1")
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

    private string ExcecuteRequestMoThanTai(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
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
            log.Debug("-------------------- CAP SO THAN TAI -------------------------");
            log.Debug("User_ID: " + User_ID);
            log.Debug("Service_ID: " + Service_ID);
            log.Debug("Command_Code: " + Command_Code);
            log.Debug("Message: " + Message.ToUpper());
            log.Debug("Request_ID: " + Request_ID);
            log.Debug(" ");
            log.Debug(" ");

            //if (!filterMsisdn(User_ID))
            //{
            //    return "0";
            //}

            #region Log MO Message Into Database (SMS_MO_Log)

            var moInfo = new SMS_MOInfo();

            moInfo.User_ID = User_ID;
            moInfo.Service_ID = Service_ID;
            moInfo.Command_Code = Command_Code;
            moInfo.Message = Message;
            moInfo.Request_ID = Request_ID;
            moInfo.Operator = GetTelco(User_ID);
            SMS_MODB.InsertThanTaiMo(moInfo);

            #endregion

            string messageReturn;
            if (Command_Code.ToUpper() == "TT")
            {
                DateTime nowTime = DateTime.Now;
                if (nowTime.Hour > 17 && nowTime.Hour < 24)
                {
                    messageReturn = "Co hoi ngay hom nay cua ban da het roi. Quay lai voi chung toi vao ngay mai va lua chon cap so tu 00h den 17h hang ngay nhe";
                    SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                }
                else
                {
                    DataTable dt = ViSport_S2_Registered_UsersController.ThanTai_DemLuot(User_ID);
                    if (ConvertUtility.ToInt32(dt.Rows[0]["RETURN_ID"].ToString()) > 10)
                    {
                        messageReturn = "Ban da lua chon 10 cap so may man va het luot lua chon trong hom nay. Cung cho don ket qua nhe, may man dang cho ban.";
                        SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                    }

                    else
                    {
                        //string numberregex = "[0-9]{2}";

                        string numberregex = "^[0-9]+$";
                        if (Regex.IsMatch(subcode, numberregex) && subcode.Length == 2)
                        {
                            //if (ViSport_S2_Registered_UsersController.ThanTai_CheckExistSubRegister(User_ID))
                            //{
                            if (ViSport_S2_Registered_UsersController.ThanTai_CheckExistMng(User_ID))
                            {
                                if (ConvertUtility.ToInt32(dt.Rows[0]["RETURN_ID"].ToString()) < 5)
                                {
                                    messageReturn = "Ban da lua chon cap so " + subcode + ". May man dang cho ban. TT <cặp số> de lua chon cap so tiep theo nang cao co hoi trung thuong.";
                                    SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                                    ThanTai_MT_Controller.Insert_CapSo(User_ID, subcode);
                                }
                                else if (ConvertUtility.ToInt32(dt.Rows[0]["RETURN_ID"].ToString()) == 5)
                                {
                                    messageReturn = "Ban da lua chon cap so " + subcode + ".Ban da het 5 luot lua chon mien phi. TT <cặp số> de nang cao diem va co hoi trung thuong (1000d/tin, toi da duọc chon them 5 lan)";
                                    SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                                    ThanTai_MT_Controller.Insert_CapSo(User_ID, subcode);
                                }
                                else
                                {
                                    //Charged                                   

                                    #region Tinh tien tin nhan le

                                    if (CapSoThanTaiCharged(User_ID, Request_ID, Service_ID) == "1")
                                    {
                                        #region Charged THANHCONG

                                        messageReturn = "Ban da lua chon cap so " + subcode + ". May man dang cho ban. TT <cặp số> de lua chon cap so tiep theo nang cao co hoi trung thuong.";
                                        SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                                        ThanTai_MT_Controller.Insert_CapSo(User_ID, subcode);
                                        #endregion
                                    }
                                    else if (CapSoThanTaiCharged(User_ID, Request_ID, Service_ID) == "Result:12,Detail:Not enough money.")
                                    {
                                        #region Charged THATBAI ==> GUI ALERT THONG BAO NAP TIEN

                                        messageReturn = "Thue bao khong du tien. Vui long nap tien de tiep tuc choi !";
                                        SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);    //SEND MT THONGBAO

                                        #endregion
                                    }
                                    else
                                    {
                                        #region Charged THATBAI ==> LOI SYSTEM

                                        messageReturn = "He thong dang ban. Vui long tro lai sau it phut !";
                                        SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);    //SEND MT THONGBAO

                                        #endregion
                                    }

                                    #endregion
                                }
                            }
                            else
                            {
                                //Charged
                                #region Tinh tien tin nhan le

                                if (CapSoThanTaiCharged(User_ID, Request_ID, Service_ID) == "1")
                                {
                                    #region Charged THANHCONG

                                    messageReturn = "Ban da lua chon cap so " + subcode + ". May man dang cho ban. TT <cặp số> de lua chon cap so tiep theo nang cao co hoi trung thuong.";
                                    SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                                    ThanTai_MT_Controller.Insert_CapSo(User_ID, subcode);
                                    #endregion
                                }
                                else if (CapSoThanTaiCharged(User_ID, Request_ID, Service_ID) == "Result:12,Detail:Not enough money.")
                                {
                                    #region Charged THATBAI ==> GUI ALERT THONG BAO NAP TIEN

                                    messageReturn = "Thue bao khong du tien. Vui long nap tien de tiep tuc choi !";
                                    SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);    //SEND MT THONGBAO

                                    #endregion
                                }
                                else
                                {
                                    #region Charged THATBAI ==> LOI SYSTEM

                                    messageReturn = "He thong dang ban. Vui long tro lai sau it phut !";
                                    SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);    //SEND MT THONGBAO

                                    #endregion
                                }

                                #endregion
                            }
                            //}
                            //else
                            //{
                            //    #region Tinh tien tin nhan le

                            //    if (CapSoThanTaiCharged(User_ID, Request_ID, Service_ID) == "1")
                            //    {
                            //        #region Charged THANHCONG

                            //        messageReturn = "Ban da lua chon cap so " + subcode + ". May man dang cho ban. TT <cặp số> de lua chon cap so tiep theo nang cao co hoi trung thuong.";
                            //        SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                            //        ThanTai_MT_Controller.Insert_CapSo(User_ID, subcode);
                            //        #endregion
                            //    }
                            //    else if (CapSoThanTaiCharged(User_ID, Request_ID, Service_ID) == "Result:12,Detail:Not enough money.")
                            //    {
                            //        #region Charged THATBAI ==> GUI ALERT THONG BAO NAP TIEN

                            //        messageReturn = "Thue bao khong du tien. Vui long nap tien de tiep tuc choi !";
                            //        SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);    //SEND MT THONGBAO

                            //        #endregion
                            //    }
                            //    else
                            //    {
                            //        #region Charged THATBAI ==> LOI SYSTEM

                            //        messageReturn = "He thong dang ban. Vui long tro lai sau it phut !";
                            //        SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);    //SEND MT THONGBAO

                            //        #endregion
                            //    }

                            //    #endregion
                            //}
                        }
                        else
                        {
                            messageReturn = "Tin nhan sai cu phap. HT: 19001255";
                            SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                        }

                    }
                }

            }
            else if (Command_Code.ToUpper() == "DIEM")
            {
                DataTable dtDiem = ThanTai_MT_Controller.SumPointByDay(User_ID);
                int diem = 0;
                int stt = 0;
                if (dtDiem != null && dtDiem.Rows.Count > 0)
                {
                    diem = ConvertUtility.ToInt32(dtDiem.Rows[0]["Point"].ToString());
                    //stt = ConvertUtility.ToInt32(dtDiem.Rows[0]["Stt"].ToString());
                }


                messageReturn = "Ban dang co " + diem + " điểm và xếp thứ " + stt + ". Co gang len nao. De xem so diem tong dang co, soan TONG gui 949";
                SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
            }
            else if (Command_Code.ToUpper() == "TONG")
            {
                DataTable dtDiem = ThanTai_MT_Controller.SumPoint(User_ID);
                int tong = 0;
                int stt = 0;
                if (dtDiem != null && dtDiem.Rows.Count > 0)
                {
                    tong = ConvertUtility.ToInt32(dtDiem.Rows[0]["Point"].ToString());
                    //stt = ConvertUtility.ToInt32(dtDiem.Rows[0]["Stt"].ToString());
                }
                messageReturn = "Ban dang co " + tong + " điểm và xếp thứ " + stt + ". Co gang len de gianh 30 trieu dong.";
                SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
            }
            else
            {
                messageReturn = "Tin nhan sai cu phap. HT: 19001255";
                SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
            }
        }
        catch (Exception ex)
        {
            log.Debug("--------------- CAP SO THAN TAI ----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }
        return responseValue;
    }

    private string ExcecuteRequestMoViSportWap(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
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
            log.Debug("--------------------viSPORT Wap-------------------------");
            log.Debug("User_ID: " + User_ID);
            log.Debug("Service_ID: " + Service_ID);
            log.Debug("Command_Code: " + Command_Code);
            log.Debug("Message: " + Message.ToUpper());
            log.Debug("Request_ID: " + Request_ID);
            log.Debug(" ");
            log.Debug(" ");

            //if (!filterMsisdn(User_ID))
            //{
            //    return "0";
            //}

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

            if (Command_Code.ToUpper() == "DK" && (subcode.ToUpper() == "TP" || subcode.ToUpper() == "KM"))
            {
                if (subcode.ToUpper() == "TP" && DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                {
                    return responseValue;
                }
                string messageReturn = "";
                #region DK DICH VU

                var entity = new ViSport_S2_Registered_UsersInfo();
                entity.User_ID = User_ID;
                entity.Request_ID = Request_ID;
                entity.Service_ID = Service_ID;
                if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                {
                    entity.Command_Code = "DK KM";
                }
                else if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM2")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM2")))
                {
                    entity.Command_Code = "DK KM";
                }
                else
                {
                    entity.Command_Code = Command_Code;
                }
                entity.Service_Type = 1;
                entity.Charging_Count = 0;
                entity.FailedChargingTimes = 0;
                entity.RegisteredTime = DateTime.Now;
                entity.ExpiredTime = DateTime.Now.AddDays(1);
                entity.Registration_Channel = "WAP";
                entity.Status = 1;
                entity.Operator = GetTelco(User_ID);
                entity.Point = 2;

                string passWord = RandomActiveCode.RandomStringNumber(6);
                entity.Password = passWord;

                DataTable value = ViSport_S2_Registered_UsersController.InsertSportGameHeroRegisterUser(entity);
                if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                {
                    ViSport_S2_Registered_UsersController.InsertUserTo56(User_ID);
                }
                else if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM2")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM2")))
                {
                    ViSport_S2_Registered_UsersController.InsertUserTo56(User_ID);
                }

                if (value.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
                {

                    #region DK DV LAN DAU TIEN ==> KM 2 MDT

                    if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                    {
                        messageReturn = AppEnv.GetSetting("RegisMT_KM");
                    }
                    else if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM2")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM2")))
                    {
                        //messageReturn = AppEnv.GetSetting("RegisMT_KM2");
                        //messageReturn = "Chuc mung ban da dang ky thanh cong dich vu Visport cua Vietnamobile. Mien phi ngay dau tien cho thue bao lan dau dang ky. Dang ky dich vu, ban duoc xem, nghe, tai toan bo noi dung mien phi. Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han). Huy dvu soan: HUY TP gui 979. HT: 19001255";
                        messageReturn = "Ban da dang ky thanh cong CT iPhone 6s trao tay TRI AN khach hang Vietnamobile cua dv Visport . KH co so diem cao nhat se so huu iPhone 6s vao cuoi CT. Duy tri dich vu de nhan diem hang ngay. Truy cap bang 3g vao trang http://visport.vn  de su dung dvu (5000d/ngay). De biet ro hon ve chuong trinh soan HD gui 2288. De huy dich vu soan HUY KM gui 979";
                    }
                    else
                    {
                        //messageReturn = AppEnv.GetSetting("RegisMT_notKM");
                        messageReturn = "Chuc mung ban da dang ky thanh cong dich vu Visport cua Vietnamobile. Mien phi ngay dau tien cho thue bao lan dau dang ky. Dang ky dich vu, ban duoc xem, nghe, tai toan bo noi dung mien phi. Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han). Huy dvu soan: HUY TP gui 979. HT: 19001255";
                    }

                    if (!string.IsNullOrEmpty(messageReturn))
                    {
                        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1
                    }

                    #endregion

                }
                else if (value.Rows[0]["RETURN_ID"].ToString() == "2")
                {

                    #region DA DK DV ROI XONG HUY DK LAI ==> TRA MT WELCOME

                    if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                    {
                        messageReturn = AppEnv.GetSetting("RegisMT_KM");
                    }
                    else if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM2")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM2")))
                    {
                        //messageReturn = AppEnv.GetSetting("RegisMT_KM2");
                        //messageReturn = "Chuc mung ban da dang ky thanh cong dich vu Visport cua Vietnamobile. Dang ky dich vu, ban duoc xem, nghe, tai toan bo noi dung mien phi. Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han). Huy dvu soan: HUY TP gui 979. HT: 19001255";
                        messageReturn = "Ban da dang ky thanh cong CT iPhone 6s trao tay TRI AN khach hang Vietnamobile cua dv Visport . KH co so diem cao nhat se so huu iPhone 6s vao cuoi CT. Duy tri dich vu de nhan diem hang ngay. Truy cap bang 3g vao trang http://visport.vn  de su dung dvu (5000d/ngay). De biet ro hon ve chuong trinh soan HD gui 2288. De huy dich vu soan HUY KM gui 979";
                    }
                    else
                    {
                        //messageReturn = AppEnv.GetSetting("RegisMT_notKM");
                        messageReturn = "Chuc mung ban da dang ky thanh cong dich vu Visport cua Vietnamobile. Dang ky dich vu, ban duoc xem, nghe, tai toan bo noi dung mien phi. Truy cap bang 3g vao dia chi http://visport.vn de su dung (5000d/ngay, dvu duoc tu dong gia han). Huy dvu soan: HUY TP gui 979. HT: 19001255";
                    }
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1

                    #endregion

                }


                #endregion
                return responseValue;
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

        return responseValue;
    }

    private string ExcecuteRequestMoViSportWap_New(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
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
            log.Debug("--------------------viSPORT Wap TP1-------------------------");
            log.Debug("User_ID: " + User_ID);
            log.Debug("Service_ID: " + Service_ID);
            log.Debug("Command_Code: " + Command_Code);
            log.Debug("Message: " + Message.ToUpper());
            log.Debug("Request_ID: " + Request_ID);
            log.Debug(" ");
            log.Debug(" ");

            //if (!filterMsisdn(User_ID))
            //{
            //    return "0";
            //}

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

            //if (Command_Code.ToUpper() == "DK" && (subcode.ToUpper() == "TP" || subcode.ToUpper() == "KM"))
            //{
            if (Command_Code.ToUpper() == "TP1")
            {
                //if (subcode.ToUpper() == "TP" && DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
                //{
                //    return responseValue;
                //}
                string messageReturn = "";
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
                entity.Registration_Channel = "WAP";
                entity.Status = 1;
                entity.Operator = GetTelco(User_ID);
                entity.Point = 2;

                string passWord = RandomActiveCode.RandomStringNumber(6);
                entity.Password = passWord;

                DataTable value = ViSport_S2_Registered_UsersController.InsertSportGameHeroRegisterUser(entity);


                if (value.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
                {

                    #region DK DV LAN DAU TIEN ==> KM 2 MDT

                    //messageReturn = AppEnv.GetSetting("RegisMT_notKM");
                    messageReturn = "Dich vu cap nhat cac tin tuc va video moi nhat duoc update lien tuc Visport chuc mung quy khach dang ky thanh cong. Quy khach duoc mien phi ngay dau tien trong lan dau dang ky. Tu ngay sau dich vu tu dong gia han voi goi cuoc 5000d/ngay. De huy dich vu soan HUY TP1 gui 979. LH 19001255 de biet chi tiet";


                    if (!string.IsNullOrEmpty(messageReturn))
                    {
                        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1
                    }

                    #endregion

                }
                else if (value.Rows[0]["RETURN_ID"].ToString() == "2")
                {

                    #region DA DK DV ROI XONG HUY DK LAI ==> TRA MT WELCOME                  
                    //messageReturn = AppEnv.GetSetting("RegisMT_notKM");
                    messageReturn = "Dich vu cap nhat cac tin tuc va video moi nhat duoc update lien tuc Visport chuc mung quy khach dang ky thanh cong. Quy khach duoc mien phi ngay dau tien trong lan dau dang ky. Tu ngay sau dich vu tu dong gia han voi goi cuoc 5000d/ngay. De huy dich vu soan HUY TP1 gui 979. LH 19001255 de biet chi tiet";

                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1

                    #endregion

                }


                #endregion
                return responseValue;
            }
            #endregion

            responseValue = "1";
        }
        catch (Exception ex)
        {
            responseValue = "1";
            log.Debug("---------------Error sentMT TP1----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
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
    public void SendMtThanTai(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
    {
        //var objSentMt = new ServiceProviderService();

        //const int msgType = (int)Constant.MessageType.NoCharge;

        // int result = objSentMt.sendMT(userId, mtMessage, serviceId, commandCode, msgType.ToString(), requestId, "1", "1", "0", "0");
        //    log.Debug("Send MT result : " + result);
        //    log.Debug("userId : " + userId);
        //    log.Debug("Noi dung MT : " + mtMessage);
        //    log.Debug("ServiceId : " + serviceId);
        //    log.Debug("commandCode : " + commandCode);
        //    log.Debug("requestId : " + requestId);

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

        ThanTai_MT_Controller.Insert_MT(objMt);
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

    #region ChuyenGiaBongDa

    public static bool filterMsisdn(string msisdn)
    {
        if (msisdn.IndexOf("000") > -1 || msisdn.IndexOf("111") > -1 || msisdn.IndexOf("222") > -1 || msisdn.IndexOf("333") > -1 || msisdn.IndexOf("444") > -1 || msisdn.IndexOf("555") > -1 || msisdn.IndexOf("666") > -1 || msisdn.IndexOf("777") > -1 || msisdn.IndexOf("888") > -1 || msisdn.IndexOf("999") > -1)
        {
            return false;
        }
        if (msisdn.IndexOf("123") > -1 || msisdn.IndexOf("234") > -1 || msisdn.IndexOf("345") > -1 || msisdn.IndexOf("456") > -1 || msisdn.IndexOf("567") > -1 || msisdn.IndexOf("678") > -1 || msisdn.IndexOf("789") > -1 || msisdn.IndexOf("012") > -1)
        {
            return false;
        }
        if (msisdn.IndexOf("987") > -1 || msisdn.IndexOf("876") > -1 || msisdn.IndexOf("765") > -1 || msisdn.IndexOf("654") > -1 || msisdn.IndexOf("543") > -1 || msisdn.IndexOf("432") > -1 || msisdn.IndexOf("321") > -1 || msisdn.IndexOf("210") > -1)
        {
            return false;
        }
        return true;
    }

    public string ChuyenGiaBongDaCharged(string userId)
    {
        var webServiceCharging3G = new WebServiceCharging3g();
        const string userName = "VMGViSport";
        const string userPass = "v@#port";
        const string cpId = "1930";
        const string price = "1000";
        const string content = "Charged cau hoi CGBD";
        const string serviceName = "CGBD_CauHoi";
        string returnValue = webServiceCharging3G.PaymentVnmWithAccount(userId, price, content, serviceName, userName, userPass, cpId);

        if (returnValue == "1")//CHARGED THANHCONG
        {
            #region LOG DOANH THU

            DataTable dtUsers = ViSport_S2_Registered_UsersController.GetSportGameHeroUserInfoActive(userId, 1);
            DataRow dr = dtUsers.Rows[0];

            var logInfo = new SportGameHeroChargedUserLogInfo();

            logInfo.ID = ConvertUtility.ToInt32(dr["ID"].ToString());
            logInfo.User_ID = userId;
            logInfo.Request_ID = dr["Request_ID"].ToString();
            logInfo.Service_ID = dr["Service_ID"].ToString();
            logInfo.Command_Code = dr["Command_Code"].ToString();

            logInfo.Service_Type = ConvertUtility.ToInt32(dr["Service_Type"].ToString());
            logInfo.Charging_Count = ConvertUtility.ToInt32(dr["Charging_Count"].ToString());
            logInfo.FailedChargingTime = ConvertUtility.ToInt32(dr["FailedChargingTimes"].ToString());

            logInfo.RegisteredTime = ConvertUtility.ToDateTime(dr["RegisteredTime"].ToString());
            logInfo.ExpiredTime = DateTime.Now.AddDays(1);

            logInfo.Registration_Channel = dr["Registration_Channel"].ToString();
            logInfo.Status = ConvertUtility.ToInt32(dr["Status"].ToString());
            logInfo.Operator = dr["Operator"].ToString();
            logInfo.Price = ConvertUtility.ToInt32(price);
            logInfo.Reason = "Succ";

            ViSport_S2_Registered_UsersController.InsertSportGameHeroChargedUserLogForSub(logInfo);

            #endregion
        }


        return returnValue;
    }

    public string CapSoThanTaiCharged(string userId, string Request_ID, string Service_ID)
    {
        var webServiceCharging3G = new WebServiceCharging3g();
        const string userName = "VMGViSport";
        const string userPass = "v@#port";
        const string cpId = "1930";
        const string price = "1000";
        const string content = "Charged CSTT";
        const string serviceName = "CSTT_DuDoan";
        string returnValue = webServiceCharging3G.PaymentVnmWithAccount(userId, price, content, serviceName, userName, userPass, cpId);

        if (returnValue == "1")//CHARGED THANHCONG
        {
            #region LOG DOANH THU

            //DataTable dtUsers = ViSport_S2_Registered_UsersController.GetSportGameHeroUserInfoActive(userId);
            //DataRow dr = dtUsers.Rows[0];

            var logInfo = new ThanTaiChargedUserLogInfo();
            logInfo.User_ID = userId;
            logInfo.Request_ID = Request_ID;
            logInfo.Service_ID = Service_ID;
            logInfo.Command_Code = "TT";
            logInfo.Registration_Channel = "SMS";
            logInfo.Status = 1;
            logInfo.Operator = "vnmobile";
            logInfo.Price = ConvertUtility.ToInt32(price);
            logInfo.Reason = "Succ";

            ViSport_S2_Registered_UsersController.ThanTai_ChargedUser_InsertLog(logInfo);

            #endregion
        }


        return returnValue;
    }
    public string VisportCharged(string userId, string price, string Request_ID)
    {
        var webServiceCharging3G = new WebServiceCharging3g();
        const string userName = "VMGViSport";
        const string userPass = "v@#port";
        const string cpId = "1930";
        //const string price = "1000";
        const string content = "Charged Dich vu the thao";
        const string serviceName = "DK_PZSP_222";
        string returnValue = webServiceCharging3G.PaymentVnmWithAccount(userId, price, content, serviceName, userName, userPass, cpId);


        #region LOG DOANH THU

        if (returnValue == "1")//CHARGED THANHCONG
        {
            #region LOG DOANH THU

            DataTable dtUsers = ViSport_S2_Registered_UsersController.GetSportGameHeroUserInfoActive(userId, 2);
            DataRow dr = dtUsers.Rows[0];

            var logInfo = new SportGameHeroChargedUserLogInfo();

            logInfo.ID = ConvertUtility.ToInt32(dr["ID"].ToString());
            logInfo.User_ID = userId;
            logInfo.Request_ID = dr["Request_ID"].ToString();
            logInfo.Service_ID = dr["Service_ID"].ToString();
            logInfo.Command_Code = dr["Command_Code"].ToString();

            logInfo.Service_Type = ConvertUtility.ToInt32(dr["Service_Type"].ToString());
            logInfo.Charging_Count = ConvertUtility.ToInt32(dr["Charging_Count"].ToString());
            logInfo.FailedChargingTime = ConvertUtility.ToInt32(dr["FailedChargingTimes"].ToString());

            logInfo.RegisteredTime = ConvertUtility.ToDateTime(dr["RegisteredTime"].ToString());
            logInfo.ExpiredTime = DateTime.Now.AddDays(1);

            logInfo.Registration_Channel = dr["Registration_Channel"].ToString();
            logInfo.Status = ConvertUtility.ToInt32(dr["Status"].ToString());
            logInfo.Operator = dr["Operator"].ToString();
            logInfo.Price = ConvertUtility.ToInt32(price);
            logInfo.Reason = "Succ";

            ViSport_S2_Registered_UsersController.InsertSportGameHeroChargedUserLogForSub(logInfo);

            #endregion
        }
        #endregion
        //}


        return returnValue;
    }
    #endregion

}
