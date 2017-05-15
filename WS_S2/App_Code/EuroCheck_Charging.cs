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
/// Summary description for EuroCheck_Charging
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class EuroCheck_Charging : System.Web.Services.WebService, IChargingNotificationSoap
{

    public EuroCheck_Charging () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog log = log4net.LogManager.GetLogger(typeof(Notification));

    public string NotifyChargingInfo(string registeredId, string userId, string requestId, string serviceId, string serviceType, string chargingValue, string chargingAccount, string chargingTime, string chargingResponse)
    {

        log.Info(" ");
        log.Info("***** LOG Euro NOTIFICATION From Tung *****");

        log.Info("User_ID : " + userId);
        log.Info("chargingValue : " + chargingValue);
        log.Info("chargingAccount : " + chargingAccount);
        log.Info("chargingTime : " + chargingTime);
        log.Info("chargingResponse : " + chargingResponse);

        log.Info("****************************************");
        log.Info(" ");

        if (chargingResponse.Trim() == "1")//CHARGED THANH CONG
        {
            #region Sinh mã dự thưởng
            int price = ConvertUtility.ToInt32(chargingValue);
            if (price == 5000)
            {
                #region Sinh 5 mã dự thưởng
                for (int i = 1; i <= 5; i++)
                {
                    string code = RandomActiveCode.Generate(8);
                    ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(userId, code);
                }
                #endregion
            }
            else if (price == 3000)
            {
                #region Sinh 5 mã dự thưởng
                for (int i = 1; i <= 3; i++)
                {
                    string code = RandomActiveCode.Generate(8);
                    ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(userId, code);
                }
                #endregion
            }
            else if (price == 2000)
            {
                #region Sinh 5 mã dự thưởng
                for (int i = 1; i <= 2; i++)
                {
                    string code = RandomActiveCode.Generate(8);
                    ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(userId, code);
                }
                #endregion
            }
            else if (price == 1000)
            {
                string code = RandomActiveCode.Generate(8);
                ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(userId, code);
            }
            #endregion

            #region LOG DOANH THU

            var logInfo = new SportGameHeroChargedUserLogInfo();

            logInfo.ID = ConvertUtility.ToInt32(registeredId);
            logInfo.User_ID = userId;
            logInfo.Request_ID = requestId;
            logInfo.Service_ID = serviceId;
            logInfo.Command_Code = "EU";

            logInfo.Service_Type = ConvertUtility.ToInt32(serviceType);
            logInfo.Charging_Count = 0;
            logInfo.FailedChargingTime = 0;

            logInfo.RegisteredTime = DateTime.Now;
            logInfo.ExpiredTime = DateTime.Now.AddDays(1);

            logInfo.Registration_Channel = "SMS";
            logInfo.Status = 1;
            logInfo.Operator = "vnmobile";
            logInfo.Price = ConvertUtility.ToInt32(chargingValue);
            logInfo.Reason = "Succ";

            ViSport_S2_Registered_UsersController.InsertEuroChargedUserLogForSub(logInfo);

            //ViSport_S2_Registered_UsersController.Update_SportGameHeroChargedValue(userId, ConvertUtility.ToInt32(chargingValue));

            //if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("EndKM")))
            //{
            //    ViSport_S2_Registered_UsersController.Update_ChargedValueCTKMvmgame_visport(userId, ConvertUtility.ToInt32(chargingValue));
            //}
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
            logInfo.Command_Code = "EU";

            logInfo.Service_Type = ConvertUtility.ToInt32(serviceType);
            logInfo.Charging_Count = 0;
            logInfo.FailedChargingTime = 0;

            logInfo.RegisteredTime = DateTime.Now;
            logInfo.ExpiredTime = DateTime.Now.AddDays(1);

            logInfo.Registration_Channel = "SMS";
            logInfo.Status = 1;
            logInfo.Operator = "vnmobile";
            logInfo.Price = ConvertUtility.ToInt32(chargingValue);
            logInfo.Reason = chargingResponse;

            ViSport_S2_Registered_UsersController.InsertEuroChargedUserLogForSub(logInfo);

            #endregion
        }

        if (chargingResponse.Trim() == "1")
        {
            #region TRA MT

            string today = DateTime.Now.DayOfWeek.ToString();
            const string commandCode = "EU";


            if (AppEnv.GetSetting("CTKM_Flag") == "0")
            {
                #region SEND MT CAU_HOI_BONG_DA

                //SEND MT CHO KHACH HANG

                DataTable dtQuestion = ViSport_S2_Registered_UsersController.GetQuestionInfoEuro();
                if (dtQuestion != null && dtQuestion.Rows.Count > 0)
                {

                    string message = dtQuestion.Rows[0]["Question"].ToString();
                    message = message.Replace("P1", "1").Replace("P2", "2");

                    int questionIdnew = ConvertUtility.ToInt32(dtQuestion.Rows[0]["Id"].ToString());
                    string answer = dtQuestion.Rows[0]["Answer"].ToString();
                    answer = answer.Replace("P1", "1").Replace("P2", "2");

                    ViSport_S2_Registered_UsersController.InsertSportGameHeroAnswerLog(userId, questionIdnew, message, answer, DateTime.Now, 0); // LUU LOG Question
                    SendMtSportGame(userId, message, serviceId, commandCode, requestId);

                }

                //END SEND MT CHO KHACH HANG

                #endregion
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
    
    

