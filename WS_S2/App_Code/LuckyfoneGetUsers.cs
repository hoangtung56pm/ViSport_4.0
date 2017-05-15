using System;
using System.Data;
using System.Web.Services;
using SentMT;
using WS_Music.Library;

/// <summary>
/// Summary description for LuckyfoneGetUsers
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class LuckyfoneGetUsers : System.Web.Services.WebService, IJobExecutorSoap
{

    public LuckyfoneGetUsers () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(LuckyfoneGetUsers));

    [WebMethod]
    public int Execute(int jobId)
    {

        ViSport_S2_Registered_UsersController.LuckyfoneCheckUserNew();
        return 1;

        //DataTable dt = ViSport_S2_Registered_UsersController.LuckyfoneGetUser();
        //if (dt != null && dt.Rows.Count > 0)
        //{
        //    foreach (DataRow dr in dt.Rows)
        //    {

        //        string userId = dr["USER_ID"].ToString();
        //        string serviceId = dr["SERVICE_ID"].ToString();
        //        string mobileOperator = dr["MOBILE_OPERATOR"].ToString();
        //        string commandCode = dr["COMMAND_CODE"].ToString();
        //        const string message = "Chuc mung ban da nhan duoc LOC may man dau nam cua 997 tri gia 200.000d. Soan: XS <ma tinh> gui 997 de tiep tuc nhan co hoi may man";
        //        DateTime submitDate = DateTime.Now;
        //        string requestId = dr["REQUEST_ID"].ToString();

        //        try
        //        {
        //            DataTable dtRes = ViSport_S2_Registered_UsersController.LuckyfoneCheckUser(userId, serviceId,
        //                                                                                       mobileOperator, commandCode,
        //                                                                                       message, submitDate, requestId);
        //            _log.Debug("********** LUCKYFONE LOG CHECK_USER **********");
        //            _log.Debug("userId : " + userId);
        //            _log.Debug("ServiceId : " + serviceId);
        //            _log.Debug("commandCode : " + commandCode);
        //            _log.Debug("mobileOperator : " + mobileOperator);
        //            _log.Debug("submitDate : " + submitDate);
        //            _log.Debug("DataTable Status : " + dtRes.Rows[0]["RETURN_ID"]);
        //            _log.Debug(" ");
        //            _log.Debug(" ");

        //            if (dtRes.Rows[0]["RETURN_ID"].ToString() == "2")//TRUNG THUONG
        //            {
        //                #region SEND MT TRUNG THUONG

        //                //SendMtLuckyFone(userId,message,serviceId,commandCode,requestId);

        //                #endregion
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            _log.Debug("********** LUCKYFONE LOG CHECK_USER ERROR **********");
        //            _log.Debug("userId : " + userId);
        //            _log.Debug("ServiceId : " + serviceId);
        //            _log.Debug("commandCode : " + commandCode);
        //            _log.Debug("mobileOperator : " + mobileOperator);
        //            _log.Debug("submitDate : " + submitDate);
        //            _log.Debug(" ");
        //            _log.Debug(" ");
        //            throw;
        //        }

        //    }
        //}
        
    }

    public void SendMtLuckyFone(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
    {
        var objSentMt = new ServiceProviderService();

        const int msgType = (int)Constant.MessageType.NoCharge;

        if (AppEnv.GetSetting("TestFlag") == "0")
        {
            int result = objSentMt.sendMT(userId, mtMessage, serviceId, commandCode, msgType.ToString(), requestId, "1", "1", "0", "0");
            _log.Debug("Send MT result : " + result);
            _log.Debug("userId : " + userId);
            _log.Debug("Noi dung MT : " + mtMessage);
            _log.Debug("ServiceId : " + serviceId);
            _log.Debug("commandCode : " + commandCode);
            _log.Debug("requestId : " + requestId);
        }
    }
    
}
