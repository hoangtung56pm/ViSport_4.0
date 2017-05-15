using System;
using System.Data;
using System.Web.Services;
using SMSManager_API.Library.Utilities;

/// <summary>
/// Summary description for ZaloXoSo
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ZaloXoSo : System.Web.Services.WebService, IJobExecutorSoap
{

    public ZaloXoSo () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ZaloXoSo));

    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            const string zaloPartner = "ZALO";
            DataTable dt = ZaloController.ZaloQuereGetUserXoso();
            if(dt != null && dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    string userId = dr["User_Id"].ToString();
                    string serviceId = dr["Service_Id"].ToString();
                    string commandCode = ConvertUtility.ToString(dr["Command_Code"].ToString());
                    string message = dr["Mt_Content"].ToString();
                    string requestId = ConvertUtility.ToString(dr["Request_Id"].ToString());
                    string telco = ConvertUtility.ToString(dr["Operator"].ToString());

                    int type = ZaloController.ApiZaloCallForSendZms(userId,message);
                    ZaloController.SaveMtLog(userId,serviceId,commandCode,message,requestId,telco,zaloPartner,type);
                    //if(type >= 0)//SEND TO Zalo Success
                    //{
                        long id = ConvertUtility.ToInt32(dr["Id"]);
                        long lotteryId = ConvertUtility.ToInt32(dr["Lottery_day_Id"]);
                        ZaloController.ZaloQuereXoSoDelete(id,lotteryId);
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("Zalo KETQUA XoSo Error : " + ex);
            return 0;
        }
        return 1;
    }
    
}
