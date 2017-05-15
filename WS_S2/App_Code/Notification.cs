using System;
using System.Data;
using System.Web.Services;
using SMSManager_API.Library.Utilities;
using SentMT;
using WS_Music.Library;

/// <summary>
/// Summary description for Notification
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Notification : System.Web.Services.WebService, IChargingNotificationSoap
{

    public Notification () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog log = log4net.LogManager.GetLogger(typeof(Notification));

    public string NotifyChargingInfo(string registeredId, string userId, string requestId, string serviceId, string serviceType, string chargingValue, string chargingAccount, string chargingTime, string chargingResponse)
    {

        log.Info(" ");
        log.Info("***** LOG TRIEU_PHU_BONG_DA CHARGED NOTIFICATION From ANDY *****");

        log.Info("User_ID : " + userId);
        log.Info("chargingValue : " + chargingValue);
        log.Info("chargingAccount : " + chargingAccount);
        log.Info("chargingTime : " + chargingTime);
        log.Info("chargingResponse : " + chargingResponse);

        log.Info("****************************************");
        log.Info(" ");

        if (chargingResponse.Trim() == "1")//CHARGED THANH CONG
        {
            if (AppEnv.GetSetting("CTKM_Flag") == "0")
            {

                #region Sinh MDT

                //string code1 = RandomActiveCode.Generate(8);
                //string code2 = RandomActiveCode.Generate(8);
                //ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(userId, code1);
                //ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(userId, code2);

                #endregion

            }

            #region LOG DOANH THU

            var logInfo = new SportGameHeroChargedUserLogInfo();

            logInfo.ID = ConvertUtility.ToInt32(registeredId);
            logInfo.User_ID = userId;
            logInfo.Request_ID = requestId;
            logInfo.Service_ID = serviceId;
            logInfo.Command_Code = "TP";

            logInfo.Service_Type = 1;
            logInfo.Charging_Count = 0;
            logInfo.FailedChargingTime = 0;

            logInfo.RegisteredTime = DateTime.Now;
            logInfo.ExpiredTime = DateTime.Now.AddDays(1);

            logInfo.Registration_Channel = "SMS";
            logInfo.Status = 1;
            logInfo.Operator = "vnmobile";
            logInfo.Price = ConvertUtility.ToInt32(chargingValue);
            logInfo.Reason = "Succ";

            ViSport_S2_Registered_UsersController.InsertSportGameHeroChargedUserLogForSub(logInfo);

            ViSport_S2_Registered_UsersController.Update_SportGameHeroChargedValue(userId, ConvertUtility.ToInt32(chargingValue));

            if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
            {
                ViSport_S2_Registered_UsersController.Update_ChargedValueCTKMvmgame_visport(userId, ConvertUtility.ToInt32(chargingValue));
            }
            #endregion

        }
        else //CHARGED THAT BAI
        {
            #region LOG DOANH THU

            var logInfo = new SportGameHeroChargedUserLogInfo();

            logInfo.ID = ConvertUtility.ToInt32(registeredId);
            logInfo.User_ID = userId;
            logInfo.Request_ID = requestId;
            logInfo.Service_ID = serviceId;
            logInfo.Command_Code = "TP";

            logInfo.Service_Type = 1;
            logInfo.Charging_Count = 0;
            logInfo.FailedChargingTime = 0;

            logInfo.RegisteredTime = DateTime.Now;
            logInfo.ExpiredTime = DateTime.Now.AddDays(1);

            logInfo.Registration_Channel = "SMS";
            logInfo.Status = 1;
            logInfo.Operator = "vnmobile";
            logInfo.Price = ConvertUtility.ToInt32(chargingValue);
            logInfo.Reason = chargingResponse;

            ViSport_S2_Registered_UsersController.InsertSportGameHeroChargedUserLogForSub(logInfo);

            //int chargingprice = ConvertUtility.ToInt32(chargingValue) > 1000;

            //if (chargingprice == 1000)
            //{
            if (DateTime.Now.Hour <= 22)
            {
                try
                {
                    DataTable dt = ViSport_S2_Registered_UsersController.ViSportGetInfo_UserCharg(userId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string userName = AppEnv.GetSetting("userName_3g_visport");
                        string userPass = AppEnv.GetSetting("password_3g_visport");
                        string cpId = AppEnv.GetSetting("cpId_3g_visport");
                        ViSport_S2_Registered_UsersController.Insert_CharingComand_Retry(
                            ConvertUtility.ToInt32(dt.Rows[0]["Id"].ToString()),
                            dt.Rows[0]["User_ID"].ToString(),
                            dt.Rows[0]["Request_Id"].ToString(),
                            ConvertUtility.ToInt32(dt.Rows[0]["Service_ID"].ToString()),
                            999,
                            1000,
                            cpId,
                            userName,
                            userPass,
                            "http://sv167.vmgmedia.vn:8000/JobSubs/Sport/Notification.asmx"
                            );
                    }
                }
                catch (Exception ex)
                {
                    log.Info("Loi charg retry visport "+ ex.ToString());
                }
                //}
            }
            #endregion
        }

        if (chargingResponse.Trim() == "1")
        {
            #region TRA MT
		    
            string today = DateTime.Now.DayOfWeek.ToString();
            const string commandCode = "TP";

            //if (CheckDayOfWeek(today)) //Tra MT vao cac ngay 3,5,7
            //{

            //    #region SEND MT THONG_TIN_TRAN_DAU

            //        DataTable dtMtFootball = ViSport_S2_Registered_UsersController.SportGameHeroMatchGetByDay();
            //        if (dtMtFootball != null && dtMtFootball.Rows.Count > 0)
            //        {

            //            string teamA = UnicodeUtility.UnicodeToKoDau(dtMtFootball.Rows[0]["Team_A_Name"].ToString());
            //            string teamB = UnicodeUtility.UnicodeToKoDau(dtMtFootball.Rows[0]["Team_B_Name"].ToString());

            //            string message1 = "Tran dau du doan ngay hom nay la: " + teamA + " va " + teamB + ". De du doan " + teamA + " thang soan KQ 1, du doan " + teamB + " thang soan KQ 3, du doan 2 doi hoa soan KQ 2 gui 979";
            //            SendMtSportGame(userId, message1, serviceId, commandCode, requestId); //MT1

            //            string message2 = "De du doan tong so ban thang soan BT G gui 979 (voi G la tong so ban thang 2 doi ghi trong thoi gian thi dau chinh thuc)";
            //            SendMtSportGame(userId, message2, serviceId, commandCode, requestId); //MT2

            //            string message3 = "De du doan ti so trong thoi gian chinh thuc soan TS A B gui 979 trong do A la so ban thang doi " + teamA + " ghi duoc, B la so ban thang doi " + teamB + " ghi duoc.";
            //            SendMtSportGame(userId, message3, serviceId, commandCode, requestId); //MT3

            //            string message4 = "De du doan " + teamA + " co ti le giu bong nhieu hon soan GB 1, du doan " + teamB + " co ti le giu bong nhieu hon soan GB 3, hai doi co ti le giu bong ngang nhau soan GB 2 gui 979";
            //            SendMtSportGame(userId, message4, serviceId, commandCode, requestId); //MT4

            //            string message5 = "De du doan tong so the vang soan TV C gui 979 trong do C la tong so the vang trong tai rut ra cho 2 doi trong thoi gian thi dau chinh thuc ";
            //            SendMtSportGame(userId, message5, serviceId, commandCode, requestId); //MT5

            //         }


            //    #endregion

            //}
            //else //Tra Cau hoi vao cac ngay 2,4,6,CN
            //{

            if (AppEnv.GetSetting("CTKM_Flag") == "0")
            {
                //#region SEND MT CAU_HOI_BONG_DA

                ////SEND MT CHO KHACH HANG

                //DataTable dtQuestion = ViSport_S2_Registered_UsersController.GetQuestionInfoSportGameHero();
                //if (dtQuestion != null && dtQuestion.Rows.Count > 0)
                //{

                //    string message = dtQuestion.Rows[0]["Question"].ToString();
                //    message = message.Replace("P1", "1").Replace("P2", "2");

                //    int questionIdnew = ConvertUtility.ToInt32(dtQuestion.Rows[0]["Id"].ToString());
                //    string answer = dtQuestion.Rows[0]["Answer"].ToString();
                //    answer = answer.Replace("P1", "1").Replace("P2", "2");

                //    ViSport_S2_Registered_UsersController.InsertSportGameHeroAnswerLog(userId, questionIdnew, message, answer, DateTime.Now, 0); // LUU LOG Question
                //    SendMtSportGame(userId, message, serviceId, commandCode, requestId);

                //}

                ////END SEND MT CHO KHACH HANG

                //#endregion
            }

                

            //}

	        #endregion
        }

        return "1";
    }

    #region Advance methods

    public void SendMtSportGame(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
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
        objMt.Operator = "vnmobile";
        objMt.IsQuestion = 1;

        ViSport_S2_SMS_MTController.InsertSportGameHeroMt(objMt);
    }

    private static bool CheckDayOfWeek(string inputDay)
    {

        if (inputDay == DayOfWeek.Tuesday.ToString() || inputDay == DayOfWeek.Thursday.ToString() || inputDay == DayOfWeek.Saturday.ToString())
        {
            return true;
        }

        return false;
    }

    #endregion
    
}
