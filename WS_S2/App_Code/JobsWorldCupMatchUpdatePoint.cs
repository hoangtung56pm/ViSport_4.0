using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using ChargingGateway;
using SMSManager_API.Library.Utilities;
using SentMT;
using WS_Music.Library;


/// <summary>
/// Summary description for JobsWorldCupMatchUpdatePoint
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class JobsWorldCupMatchUpdatePoint : System.Web.Services.WebService, IJobExecutorSoap
{

    public JobsWorldCupMatchUpdatePoint()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog _log = log4net.LogManager.GetLogger(typeof(JobsWorldCupMatchUpdatePoint));

    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            var webServiceCharging3G = new WebServiceCharging3g();
            string userName = AppEnv.GetSetting("userName_3g_WapVnm");
            string userPass = AppEnv.GetSetting("password_3g_WapVnm");
            string cpId = AppEnv.GetSetting("cpId_3g_WapVnm");
            string price;

            string message = string.Empty;
            string returnValue = string.Empty;
            string notEnoughMoney = AppEnv.GetSetting("NotEnoughMoney");

            string serviceType = "Charged Sub World Cup";
            string serviceName = "World_Cup";
            string reasonLog = string.Empty;

            DataTable dtPlayed = ViSport_S2_Registered_UsersController.WorldCupGetMatchPlayed();
            if(dtPlayed != null && dtPlayed.Rows.Count > 0)
            {
                foreach(DataRow drPlayed in dtPlayed.Rows)
                {
                    int matchId = ConvertUtility.ToInt32(drPlayed["Id"].ToString());
                    string winner = drPlayed["Winner"].ToString();

                    #region XU LY CONG DIEM

                    DataSet ds = ViSport_S2_Registered_UsersController.WorldCupGetMatchVoteByMatchIdRightAndWrong(matchId, winner);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dtRight = ds.Tables[0];
                        DataTable dtWrong = ds.Tables[1];

                        #region UPDATE RIGHT POINT

                        if (dtRight != null && dtRight.Rows.Count > 0)
                        {
                            foreach (DataRow drR in dtRight.Rows)
                            {
                                ViSport_S2_Registered_UsersController.WorldCupRegisteredUserUpdatePoint(drR["User_Id"].ToString(), 5);
                            }
                        }

                        #endregion

                        #region UPDATE WRONG POINT

                        if (dtWrong != null && dtWrong.Rows.Count > 0)
                        {
                            foreach (DataRow drW in dtWrong.Rows)
                            {
                                ViSport_S2_Registered_UsersController.WorldCupRegisteredUserUpdatePoint(drW["User_Id"].ToString(), 1);
                            }
                        }

                        #endregion

                    }

                    #endregion

                    

                    #region UPDATE Trang Thai TranDau

                    ViSport_S2_Registered_UsersController.WorldCupMatchStatusUpdate(ConvertUtility.ToInt32(drPlayed["Id"].ToString()));

                    #endregion

                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("WC Loi lay Tran Dau vua da xong : " + ex);
            return 0;
        }
        return 1;
    }

    public void SendMtWorldCup(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
    {
        var objSentMt = new ServiceProviderService();

        const int msgType = (int)Constant.MessageType.NoCharge;

        //if (AppEnv.GetSetting("TestFlag") == "0")
        //{
            int result = objSentMt.sendMT(userId, mtMessage, serviceId, commandCode, msgType.ToString(), requestId, "1", "1", "0", "0");
            _log.Debug("Send MT result : " + result);
            _log.Debug("userId : " + userId);
            _log.Debug("Noi dung MT : " + mtMessage);
            _log.Debug("ServiceId : " + serviceId);
            _log.Debug("commandCode : " + commandCode);
            _log.Debug("requestId : " + requestId);
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
        objMt.IsQuestion = 1;

        ViSport_S2_Registered_UsersController.WorldCupInsertMtLog(objMt);
    }

    public void SendMtWorldCupLiveNews(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
    {
        var objSentMt = new ServiceProviderService();

        const int msgType = (int)Constant.MessageType.NoCharge;

        //if (AppEnv.GetSetting("TestFlag") == "0")
        //{
        serviceId = "949";
        int result = objSentMt.sendMT(userId, mtMessage, serviceId, commandCode, msgType.ToString(), requestId, "1", "1", "0", "0");
        _log.Debug("Send MT result : " + result);
        _log.Debug("userId : " + userId);
        _log.Debug("Noi dung MT : " + mtMessage);
        _log.Debug("ServiceId : " + serviceId);
        _log.Debug("commandCode : " + commandCode);
        _log.Debug("requestId : " + requestId);
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
        objMt.IsQuestion = 1;

        ViSport_S2_Registered_UsersController.WorldCupInsertMtLog(objMt);
    }
    
}
