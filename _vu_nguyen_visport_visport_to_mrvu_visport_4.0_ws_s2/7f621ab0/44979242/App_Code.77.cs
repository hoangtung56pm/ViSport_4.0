#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\ZaloExpiredAlert.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "49CFC9BBF984299B62C37311CD9CC80902F2931F"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\ZaloExpiredAlert.cs"
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
    public int Execute(int jobID)
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


#line default
#line hidden
