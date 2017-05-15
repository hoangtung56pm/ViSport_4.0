#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\JobsSubSportGameAdvance.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "22F20F3CB100890C90BD663E2F143BABAFAB7354"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\JobsSubSportGameAdvance.cs"
using System;
using System.Data;
using System.Web.Services;
using SMSManager_API.Library.Utilities;
using ChargingGateway;
using SentMT;

/// <summary>
/// Summary description for JobsSubSportGameAdvance
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class JobsSubSportGameAdvance : System.Web.Services.WebService, IJobExecutorSoap
{

    public JobsSubSportGameAdvance () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog log = log4net.LogManager.GetLogger(typeof(JobsSubSportGameAdvance));


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

            DataTable dtUsers = ViSport_S2_Registered_UsersController.GetSportGameHeroUserInfo("841864925596");
            if (dtUsers != null && dtUsers.Rows.Count > 0)
            {
                string message = string.Empty;
                string returnValue = string.Empty;
                string notEnoughMoney = AppEnv.GetSetting("NotEnoughMoney");

                string serviceType = "Charge Hero";
                string serviceName = "ViSport_Hero";
                string reasonLog = string.Empty;

                for (int i = 0; i < dtUsers.Rows.Count; i++)
                {
                    try
                    {
                        #region TIEN HANH CHARGED

                        price = "5000";
                        returnValue = webServiceCharging3G.PaymentVnmWithAccount(dtUsers.Rows[i]["User_ID"].ToString(), price, "Charged Sub Anh Tai", "Anh_Tai_Sub", userName, userPass, cpId);
                        if (returnValue == "1")
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
                            logInfo.Reason = "Succ";

                            ViSport_S2_Registered_UsersController.InsertSportGameHeroChargedUserLogForSub(logInfo);

                            #endregion

                            returnValue = webServiceCharging3G.PaymentVnmWithAccount(dtUsers.Rows[i]["User_ID"].ToString(), price, "Charged Sub Anh Tai", "Anh_Tai_Sub", userName, userPass, cpId);

                            if (returnValue == "1")
                            {

                                #region LOG DOANH THU

                                var logInfo1 = new SportGameHeroChargedUserLogInfo();

                                logInfo1.ID = ConvertUtility.ToInt32(dtUsers.Rows[i]["ID"].ToString());
                                logInfo1.User_ID = dtUsers.Rows[i]["User_ID"].ToString();
                                logInfo1.Request_ID = dtUsers.Rows[i]["Request_ID"].ToString();
                                logInfo1.Service_ID = dtUsers.Rows[i]["Service_ID"].ToString();
                                logInfo1.Command_Code = dtUsers.Rows[i]["Command_Code"].ToString();

                                logInfo1.Service_Type = ConvertUtility.ToInt32(dtUsers.Rows[i]["Service_Type"].ToString());
                                logInfo1.Charging_Count = ConvertUtility.ToInt32(dtUsers.Rows[i]["Charging_Count"].ToString());
                                logInfo1.FailedChargingTime = ConvertUtility.ToInt32(dtUsers.Rows[i]["FailedChargingTimes"].ToString());

                                logInfo1.RegisteredTime = ConvertUtility.ToDateTime(dtUsers.Rows[i]["RegisteredTime"].ToString());
                                logInfo1.ExpiredTime = DateTime.Now.AddDays(1);

                                logInfo1.Registration_Channel = dtUsers.Rows[i]["Registration_Channel"].ToString();
                                logInfo1.Status = ConvertUtility.ToInt32(dtUsers.Rows[i]["Status"].ToString());
                                logInfo1.Operator = dtUsers.Rows[i]["Operator"].ToString();
                                logInfo1.Price = ConvertUtility.ToInt32(price);
                                logInfo1.Reason = "Succ";

                                ViSport_S2_Registered_UsersController.InsertSportGameHeroChargedUserLogForSub(logInfo1);

                                #endregion

                                returnValue = webServiceCharging3G.PaymentVnmWithAccount(dtUsers.Rows[i]["User_ID"].ToString(), price, "Charged Sub Anh Tai", "Anh_Tai_Sub", userName, userPass, cpId);

                                if (returnValue == "1")
                                {
                                    #region LOG DOANH THU

                                    var logInfo2 = new SportGameHeroChargedUserLogInfo();

                                    logInfo2.ID = ConvertUtility.ToInt32(dtUsers.Rows[i]["ID"].ToString());
                                    logInfo2.User_ID = dtUsers.Rows[i]["User_ID"].ToString();
                                    logInfo2.Request_ID = dtUsers.Rows[i]["Request_ID"].ToString();
                                    logInfo2.Service_ID = dtUsers.Rows[i]["Service_ID"].ToString();
                                    logInfo2.Command_Code = dtUsers.Rows[i]["Command_Code"].ToString();

                                    logInfo2.Service_Type = ConvertUtility.ToInt32(dtUsers.Rows[i]["Service_Type"].ToString());
                                    logInfo2.Charging_Count = ConvertUtility.ToInt32(dtUsers.Rows[i]["Charging_Count"].ToString());
                                    logInfo2.FailedChargingTime = ConvertUtility.ToInt32(dtUsers.Rows[i]["FailedChargingTimes"].ToString());

                                    logInfo2.RegisteredTime = ConvertUtility.ToDateTime(dtUsers.Rows[i]["RegisteredTime"].ToString());
                                    logInfo2.ExpiredTime = DateTime.Now.AddDays(1);

                                    logInfo2.Registration_Channel = dtUsers.Rows[i]["Registration_Channel"].ToString();
                                    logInfo2.Status = ConvertUtility.ToInt32(dtUsers.Rows[i]["Status"].ToString());
                                    logInfo2.Operator = dtUsers.Rows[i]["Operator"].ToString();
                                    logInfo2.Price = ConvertUtility.ToInt32(price);
                                    logInfo2.Reason = "Succ";

                                    ViSport_S2_Registered_UsersController.InsertSportGameHeroChargedUserLogForSub(logInfo2);

                                    #endregion
                                }
                            }
                        }
                        
                        
                        if (returnValue != "1")
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
                            //reasonLog = "Succ";

                            //SEND MT CHO KHACH HANG

                            //DataTable dtQuestion = ViSport_S2_Registered_UsersController.GetQuestionInfoSportGameHero();
                            //if (dtQuestion != null && dtQuestion.Rows.Count > 0)
                            //{
                                message = "Ban da duoc cong 960 diem cho game show Anh tai bong da.Soan DIEM gui 979 de xem so diem hien tai.";

                                

                                string serviceId = dtUsers.Rows[i]["Service_ID"].ToString();
                                string commandCode = dtUsers.Rows[i]["Command_Code"].ToString();
                                string requestId = dtUsers.Rows[i]["Request_ID"].ToString();

                                
                                SendMtSportGame(dtUsers.Rows[i]["User_ID"].ToString(), message, serviceId, commandCode, requestId);

                            //}

                            //END SEND MT CHO KHACH HANG

                        }
                        //else
                        //{
                        //    reasonLog = returnValue;
                        //}


                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Anh tai bong da Loi charged : " + ex);
                        continue;
                    }
                    //DataTable dt = new DataTable();
                    //dt = ViSport_S2_Registered_UsersController.SportGameHeroCheckUserChargedByDay(dtUsers.Rows[i]["User_ID"].ToString());
                    //if (dt.Rows[0]["RETURN_ID"].ToString().Trim() == "0")
                    //{

                    //}
                }

            }

            return 1;
        }
        catch (Exception ex)
        {
            log.Error("Anh tai bong da Loi lay tap User : " + ex);
            return 0;
        }
    }

    public void SendMtSportGame(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
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
        objMt.Operator = "vnmobile";
        objMt.IsQuestion = 1;

        ViSport_S2_SMS_MTController.InsertSportGameHeroMt(objMt);
    }

}


#line default
#line hidden
