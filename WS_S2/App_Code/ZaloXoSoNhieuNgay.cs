using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using SMSManager_API.Library.Utilities;

/// <summary>
/// Summary description for ZaloXoSoNhieuNgay
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ZaloXoSoNhieuNgay : System.Web.Services.WebService, IJobExecutorSoap
{

    public ZaloXoSoNhieuNgay () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ZaloXoSoNhieuNgay));

    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            const string zaloPartner = "ZALO";
            DataTable dt = ZaloController.ZaloQuereGetUserXosoNhieuNgay();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string userId = dr["User_Id"].ToString();
                    string serviceId = dr["Service_Id"].ToString();
                    string commandCode = dr["Command_Code"].ToString();
                    string message = dr["Mt_Content"].ToString();
                    string requestId = dr["Request_Id"].ToString();
                    string telco = dr["Operator"].ToString();

                    int type = ZaloController.ApiZaloCallForSendZms(userId, message);
                    ZaloController.SaveMtLog(userId, serviceId, commandCode, message, requestId, telco, zaloPartner, type);
                    //if (type >= 0)//SEND TO Zalo Success
                    //{
                        long id = ConvertUtility.ToInt32(dr["Id"]);
                        //long lotteryId = 0;
                        ZaloController.ZaloQuereXoSoDelete(id, 0);
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("Zalo KETQUA XoSo NhieuNgay Error : " + ex);
            return 0;
        }
        return 1;
    }
    
}
