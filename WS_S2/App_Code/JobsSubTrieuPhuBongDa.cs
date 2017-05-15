using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Services;
using ChargingGateway;
using SMSManager_API.Library.Utilities;
using SentMT;
using WS_Music.Library;

/// <summary>
/// Summary description for JobsSubTrieuPhuBongDa
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class JobsSubTrieuPhuBongDa : System.Web.Services.WebService {

    public JobsSubTrieuPhuBongDa () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog log = log4net.LogManager.GetLogger(typeof(JobsSubSportGame));

    [WebMethod]
    public int Execute(int jobID)
    {
        WebServiceCharging3g webServiceCharging3G = new WebServiceCharging3g();
        string userName = "VMGViSport";
        string userPass = "v@#port";
        string cpId = "1930";
        string price;

        try
        {

            DataTable dtUsers = ViSport_S2_Registered_UsersController.GetSportGameUserByTypeTp(false);
            if (dtUsers != null && dtUsers.Rows.Count > 0)
            {
                string message = string.Empty;
                string returnValue = string.Empty;
                string notEnoughMoney = AppEnv.GetSetting("NotEnoughMoney");

                string serviceType = "Charge Hero";
                string serviceName = "ViSport_Hero";
                string reasonLog = string.Empty;

                int count = 0;

                for (int i = 0; i < dtUsers.Rows.Count; i++)
                {

                    if(count >= 3)
                    {
                        Thread.Sleep(1000);
                        count = 0;
                    }

                    try
                    {
                        string msisdn = dtUsers.Rows[i]["User_ID"].ToString();

                        #region TIEN HANH CHARGED

                        price = "5000";
                        returnValue = webServiceCharging3G.PaymentVnmWithAccount(dtUsers.Rows[i]["User_ID"].ToString(), price, "Charged Sub Trieu phu bong da ", "Trieuphu_Sub", userName, userPass, cpId);
                        if (returnValue.Trim() == notEnoughMoney)
                        {
                            price = "3000";
                            returnValue = webServiceCharging3G.PaymentVnmWithAccount(dtUsers.Rows[i]["User_ID"].ToString(), price, "Charged Sub Trieu phu bong da", "Trieuphu_Sub", userName, userPass, cpId);
                            if (returnValue.Trim() == notEnoughMoney)
                            {
                                price = "2000";
                                returnValue = webServiceCharging3G.PaymentVnmWithAccount(dtUsers.Rows[i]["User_ID"].ToString(), price, "Charged Sub Trieu phu bong da", "Trieuphu_Sub", userName, userPass, cpId);
                                if (returnValue.Trim() == notEnoughMoney)
                                {
                                    price = "1000";
                                    returnValue = webServiceCharging3G.PaymentVnmWithAccount(dtUsers.Rows[i]["User_ID"].ToString(), price, "Charged Sub Trieu phu bong da", "Trieuphu_Sub", userName, userPass, cpId);
                                }
                            }
                        }

                        if (returnValue == "1")
                        {
                            #region Sinh MDT

                            string code1 = RandomActiveCode.Generate(8);
                            string code2 = RandomActiveCode.Generate(8);
                            ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(msisdn,code1);
                            ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(msisdn, code2);

                            #endregion

                            #region LOG DOANH THU

                            var logInfo = new SportGameHeroChargedUserLogInfo();

                            logInfo.ID = ConvertUtility.ToInt32(dtUsers.Rows[i]["ID"].ToString());
                            logInfo.User_ID = dtUsers.Rows[i]["User_ID"].ToString();
                            logInfo.Request_ID = dtUsers.Rows[i]["Request_ID"].ToString();
                            logInfo.Service_ID = dtUsers.Rows[i]["Service_ID"].ToString();
                            logInfo.Command_Code = dtUsers.Rows[i]["Command_Code"].ToString();

                            logInfo.Service_Type = ConvertUtility.ToInt32(dtUsers.Rows[i]["Service_Type"].ToString());
                            logInfo.Charging_Count = ConvertUtility.ToInt32(dtUsers.Rows[i]["Charging_Count"].ToString());
                            logInfo.FailedChargingTime = ConvertUtility.ToInt32(dtUsers.Rows[i]["FailedChargingTimes"].ToString());

                            logInfo.RegisteredTime = ConvertUtility.ToDateTime(dtUsers.Rows[i]["RegisteredTime"].ToString());
                            logInfo.ExpiredTime = DateTime.Now.AddDays(1);

                            logInfo.Registration_Channel = dtUsers.Rows[i]["Registration_Channel"].ToString();
                            logInfo.Status = ConvertUtility.ToInt32(dtUsers.Rows[i]["Status"].ToString());
                            logInfo.Operator = dtUsers.Rows[i]["Operator"].ToString();
                            logInfo.Price = ConvertUtility.ToInt32(price);
                            logInfo.Reason = "Succ";

                            ViSport_S2_Registered_UsersController.InsertSportGameHeroChargedUserLogForSub(logInfo);

                            #endregion
                        }
                        else
                        {
                            #region LOG DOANH THU

                            var logInfo = new SportGameHeroChargedUserLogInfo();

                            logInfo.ID = ConvertUtility.ToInt32(dtUsers.Rows[i]["ID"].ToString());
                            logInfo.User_ID = dtUsers.Rows[i]["User_ID"].ToString();
                            logInfo.Request_ID = dtUsers.Rows[i]["Request_ID"].ToString();
                            logInfo.Service_ID = dtUsers.Rows[i]["Service_ID"].ToString();
                            logInfo.Command_Code = dtUsers.Rows[i]["Command_Code"].ToString();

                            logInfo.Service_Type = ConvertUtility.ToInt32(dtUsers.Rows[i]["Service_Type"].ToString());
                            logInfo.Charging_Count = ConvertUtility.ToInt32(dtUsers.Rows[i]["Charging_Count"].ToString());
                            logInfo.FailedChargingTime = ConvertUtility.ToInt32(dtUsers.Rows[i]["FailedChargingTimes"].ToString());

                            logInfo.RegisteredTime = ConvertUtility.ToDateTime(dtUsers.Rows[i]["RegisteredTime"].ToString());
                            logInfo.ExpiredTime = DateTime.Now.AddDays(1);

                            logInfo.Registration_Channel = dtUsers.Rows[i]["Registration_Channel"].ToString();
                            logInfo.Status = ConvertUtility.ToInt32(dtUsers.Rows[i]["Status"].ToString());
                            logInfo.Operator = dtUsers.Rows[i]["Operator"].ToString();
                            logInfo.Price = ConvertUtility.ToInt32(price);
                            logInfo.Reason = returnValue;

                            ViSport_S2_Registered_UsersController.InsertSportGameHeroChargedUserLogForSub(logInfo);

                            #endregion
                        }

                        if (returnValue == "1")//CHARGED THANH CONG
                        {
                            string today = DateTime.Now.DayOfWeek.ToString();

                            string serviceId = dtUsers.Rows[i]["Service_ID"].ToString();
                            string commandCode = dtUsers.Rows[i]["Command_Code"].ToString();
                            string requestId = dtUsers.Rows[i]["Request_ID"].ToString();

                            if (CheckDayOfWeek(today)) //Tra MT vao cac ngay 3,5,7
                            {

                                #region SEND MT THONG_TIN_TRAN_DAU

                                DataTable dtMtFootball = ViSport_S2_Registered_UsersController.SportGameHeroMatchGetByDay();
                                if (dtMtFootball != null && dtMtFootball.Rows.Count > 0)
                                {

                                    string teamA = UnicodeUtility.UnicodeToKoDau(dtMtFootball.Rows[0]["Team_A_Name"].ToString());
                                    string teamB = UnicodeUtility.UnicodeToKoDau(dtMtFootball.Rows[0]["Team_B_Name"].ToString());

                                    string message1 = "Tran dau du doan ngay hom nay la: " + teamA + " va " + teamB + ". De du doan " + teamA + " thang soan KQ 1, du doan " + teamB + " thang soan KQ 3, du doan 2 doi hoa soan KQ 2 gui 979";
                                    SendMtSportGame(dtUsers.Rows[i]["User_ID"].ToString(), message1, serviceId, commandCode, requestId); //MT1

                                    string message2 = "De du doan tong so ban thang soan BT G gui 979 (voi G la tong so ban thang 2 doi ghi trong thoi gian thi dau chinh thuc)";
                                    SendMtSportGame(dtUsers.Rows[i]["User_ID"].ToString(), message2, serviceId, commandCode, requestId); //MT2

                                    string message3 = "De du doan ti so trong thoi gian chinh thuc soan TS A B gui 979 trong do A la so ban thang doi " + teamA + " ghi duoc, B la so ban thang doi " + teamB + " ghi duoc.";
                                    SendMtSportGame(dtUsers.Rows[i]["User_ID"].ToString(), message3, serviceId, commandCode, requestId); //MT3

                                    string message4 = "De du doan " + teamA + " co ti le giu bong nhieu hon soan GB 1, du doan " + teamB + " co ti le giu bong nhieu hon soan GB 3, hai doi co ti le giu bong ngang nhau soan GB 2 gui 979";
                                    SendMtSportGame(dtUsers.Rows[i]["User_ID"].ToString(), message4, serviceId, commandCode, requestId); //MT4

                                    string message5 = "De du doan tong so the vang soan TV C gui 979 trong do C la tong so the vang trong tai rut ra cho 2 doi trong thoi gian thi dau chinh thuc ";
                                    SendMtSportGame(dtUsers.Rows[i]["User_ID"].ToString(), message5, serviceId, commandCode, requestId); //MT5

                                }


                                #endregion

                            }
                            else //Tra Cau hoi vao cac ngay 2,4,6,CN
                            {

                                #region SEND MT CAU_HOI_BONG_DA

                                //SEND MT CHO KHACH HANG

                                DataTable dtQuestion = ViSport_S2_Registered_UsersController.GetQuestionInfoSportGameHero();
                                if (dtQuestion != null && dtQuestion.Rows.Count > 0)
                                {
                                    message = dtQuestion.Rows[0]["Question"].ToString();
                                    message = message.Replace("P1", "1").Replace("P2", "2");

                                    int questionIdnew = ConvertUtility.ToInt32(dtQuestion.Rows[0]["Id"].ToString());
                                    string answer = dtQuestion.Rows[0]["Answer"].ToString();
                                    answer = answer.Replace("P1", "1").Replace("P2", "2");

                                    ViSport_S2_Registered_UsersController.InsertSportGameHeroAnswerLog(dtUsers.Rows[i]["User_ID"].ToString(), questionIdnew, message, answer, DateTime.Now, 0); // LUU LOG Question
                                    SendMtSportGame(dtUsers.Rows[i]["User_ID"].ToString(), message, serviceId, commandCode, requestId);

                                }

                                //END SEND MT CHO KHACH HANG

                                #endregion

                            }

                        }

                        #endregion

                        count = count + 1;

                    }
                    catch (Exception ex)
                    {
                        log.Error("Trieu phu bong da Loi charged : " + ex);
                    }

                }
            }

            return 1;
        }
        catch (Exception ex)
        {
            log.Error("Trieu phu bong da Loi lay tap User : " + ex);
            return 0;
        }
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
