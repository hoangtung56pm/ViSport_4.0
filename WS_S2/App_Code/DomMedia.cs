using System.Data;
using System.Web.Services;
using log4net;
using Microsoft.ApplicationBlocks.Data;
using SentMT;
using SMSManager_API.Library.Utilities;
using WS_Music.Library;

/// <summary>
/// Summary description for DomMedia
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class DomMedia : System.Web.Services.WebService {

    public DomMedia () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly ILog _log = LogManager.GetLogger("File");

    [WebMethod]
    public string MoProcess(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExcecuteMo(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }

    private string ExcecuteMo(string userId, string serviceId, string commandCode, string message, string requestId)
    {

        _log.Debug("***** DOM_Media MO *****");
        _log.Debug("userId : " + userId);
        _log.Debug("serviceId : " + serviceId);
        _log.Debug("commandCode : " + commandCode);
        _log.Debug("message : " + message);
        _log.Debug("requestId : " + requestId);
        _log.Debug("*****");

        message = message.ToUpper();
        string subcode = "";
        if (message.Trim().Length > commandCode.Trim().Length)
        {
            subcode = message.ToUpper().Substring(commandCode.Length).Replace(" ", "");
        }
        commandCode = commandCode.ToUpper();

        if (commandCode == "LO" || commandCode == "DE")
        {
            DataTable dt = GetCompanyId(subcode);
            if (dt.Rows.Count > 0)
            {
                int companyId = ConvertUtility.ToInt32(dt.Rows[0]["company_id"].ToString());
                DataTable dtContent = GetContent(companyId);
                if (dtContent.Rows.Count > 0)
                {
                    string content = dtContent.Rows[0]["Content"].ToString();
                    if (!string.IsNullOrEmpty(content))
                    {
                        message = content;
                        SendMtDomMedia(userId,message,serviceId,commandCode,requestId);
                    }
                    else
                    {
                        message = "Du lieu chua duoc cap nhat. Xin cam on !";
                        SendMtDomMedia(userId, message, serviceId, commandCode, requestId);
                    }
                }
            }
            else
            {
                message = "Tin nhan sai cu phap. Xin thu lai !";
                SendMtDomMedia(userId,message,serviceId,commandCode,requestId);
            }
        }

        return "1";
    }

    #region Public methods

    public void SendMtDomMedia(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
    {
        var objSentMt = new ServiceProviderService();

        const int msgType = (int)Constant.MessageType.NoCharge;

        int result = objSentMt.sendMT(userId, mtMessage, serviceId, commandCode, msgType.ToString(), requestId, "1", "1", "0", "0");
        _log.Debug("***** DOM_Media Send MT *****");
        _log.Debug("Send MT result : " + result);
        _log.Debug("userId : " + userId);
        _log.Debug("Noi dung MT : " + mtMessage);
        _log.Debug("ServiceId : " + serviceId);
        _log.Debug("commandCode : " + commandCode);
        _log.Debug("requestId : " + requestId);
        _log.Debug("*****");
    }

    public DataTable GetCompanyId(string code)
    {
        code = code.Replace(" ", "");
        code = code.Trim();

        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVClip, "Dom_Service_Code_GetInfo", code);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return new DataTable();
    }

    public DataTable GetContent(int companyId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVClip, "SoicauDom_GetByCompanyId", companyId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return new DataTable();
    }

    #endregion
    
}
