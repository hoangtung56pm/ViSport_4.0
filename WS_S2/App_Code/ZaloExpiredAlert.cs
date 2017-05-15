using System;
using System.Data;
using System.Web.Services;
using log4net;
using SMSManager_API.Library.Utilities;

/// <summary>
/// Summary description for ZaloExpiredAlert
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ZaloExpiredAlert : System.Web.Services.WebService, IJobExecutorSoap
{

    public ZaloExpiredAlert () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly ILog _log = LogManager.GetLogger(typeof(ZaloExpiredAlert));

    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            const string zaloPartner = "ZALO";
            DataTable dt = ZaloController.ZaloQuereGetUserExpiredXosoNhieuNgay();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string userId = dr["User_Id"].ToString();
                    string serviceId = dr["Service_Id"].ToString();
                    string commandCode = ConvertUtility.ToString(dr["Command_Code"].ToString());
                    string message = "Bạn đã hết hạn sử dụng dịch vụ xổ số 30 ngày của Zalo. Vui lòng đăng ký tiếp bằng cách chọn menu SMS.";
                    string requestId = ConvertUtility.ToString(dr["Request_Id"].ToString());
                    string telco = ConvertUtility.ToString(dr["Operator"].ToString());

                    int type = ZaloController.ApiZaloCallForSendZmsAlert(userId, message);
                    ZaloController.SaveMtLog(userId, serviceId, commandCode, message, requestId, telco, zaloPartner, type);
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("Zalo Expired Alert Error : " + ex);
            return 0;
        }
        return 1;
    }

}
