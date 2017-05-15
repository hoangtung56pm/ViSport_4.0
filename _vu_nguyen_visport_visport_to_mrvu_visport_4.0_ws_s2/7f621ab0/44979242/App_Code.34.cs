#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\JobsSubSportGameBonus.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "38B3B12852E174801571A35E3EAF84454265AAC2"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\JobsSubSportGameBonus.cs"
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using ChargingGateway;
using SMSManager_API.Library.Utilities;

/// <summary>
/// Summary description for JobSubSportGameBonus
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class JobsSubSportGameBonus : System.Web.Services.WebService, IJobExecutorSoap
{

    public JobsSubSportGameBonus()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog log = log4net.LogManager.GetLogger(typeof(JobsSubSportGameBonus));

    [WebMethod]
    public int Execute(int jobID)
    {
        var webServiceCharging3G = new WebServiceCharging3g();
        string userName = "VMGViSport";
        string userPass = "v@#port";
        string cpId = "1930";
        string price;

        try
        {
            DataTable dtUsers = ViSport_S2_Registered_UsersController.GetSportGameUserByTypeBonus();
            if (dtUsers != null && dtUsers.Rows.Count > 0)
            {
                for (int i = 0; i < dtUsers.Rows.Count; i++)
                {
                    try
                    {
                        #region TIEN HANH CHARGED

                        price = "1000";
                        string returnValue = webServiceCharging3G.PaymentVnmWithAccount(dtUsers.Rows[i]["User_ID"].ToString(), price, "Charged Sub Anh Tai", "Anh_Tai_Sub", userName, userPass, cpId);
                       
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

                            ViSport_S2_Registered_UsersController.InsertSportGameHeroChargedUserLogForSubBonus(logInfo);

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

                            ViSport_S2_Registered_UsersController.InsertSportGameHeroChargedUserLogForSubBonus(logInfo);

                            #endregion
                        }

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        log.Error("Anh tai bong da Loi charged : " + ex);
                    }
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

}


#line default
#line hidden
