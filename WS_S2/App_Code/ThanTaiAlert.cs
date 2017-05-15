using Microsoft.ApplicationBlocks.Data;
using SentMT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using WS_Music.Library;

/// <summary>
/// Summary description for ThanTaiAlert
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ThanTaiAlert : System.Web.Services.WebService, IJobExecutorSoap
{

    public ThanTaiAlert()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ThanTaiAlert));
    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            DataTable dt = GetLotteryResult();
            if (dt != null && dt.Rows.Count > 0)
            {
                int diem = Convert.ToInt32(dt.Rows[0]["Point"].ToString());
                if (diem > 0)
                {
                    string UserID = dt.Rows[0]["User_ID"].ToString();
                    string messageReturn = "Chuc mung ban da danh duoc the cao 100k cua ngay hom nay tu chuong trinh Cap So Than Tai. Lien he 19001255 de nhan giai";
                    SendMtThanTai(UserID, messageReturn, "949", "TT", "0");
                }
                
            }
        }
        catch (Exception ex)
        {
            _log.Error("***** ThanTai alert Loi lay tap User : " + ex);
            return 0;
        }
        return 1;
    }
    #region Methods
    public void SendMtThanTai(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
    {
        var objSentMt = new ServiceProviderService();

        const int msgType = (int)Constant.MessageType.NoCharge;

        int result = objSentMt.sendMT(userId, mtMessage, serviceId, commandCode, msgType.ToString(), requestId, "1", "1", "0", "0");
        _log.Debug("Send MT result : " + result);
        _log.Debug("userId : " + userId);
        _log.Debug("Noi dung MT : " + mtMessage);
        _log.Debug("ServiceId : " + serviceId);
        _log.Debug("commandCode : " + commandCode);
        _log.Debug("requestId : " + requestId);

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

    public DataTable GetLotteryResult()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "ThanTai_Result_ByDay");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return new DataTable();
    }
    #endregion
}
