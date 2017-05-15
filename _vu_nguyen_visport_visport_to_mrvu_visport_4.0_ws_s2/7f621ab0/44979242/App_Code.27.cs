#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\ZaloXoSoTuongThuat.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9C84D7827980E1108212D47D9DA3697EAEBD9F28"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\ZaloXoSoTuongThuat.cs"
using System;
using System.Data;
using System.Web.Services;
using SMSManager_API.Library.Utilities;

/// <summary>
/// Summary description for ZaloXoSoTuongThuat
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ZaloXoSoTuongThuat : System.Web.Services.WebService, IJobExecutorSoap
{

    public ZaloXoSoTuongThuat () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ZaloXoSoTuongThuat));

    [WebMethod]
    public int Execute(int jobID)
    {
        try
        {
            const string zaloPartner = "ZALO";
            DataTable dt = ZaloController.ZaloQuereGetUserXoSoTuongThuat();
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
                    int isDelete = ConvertUtility.ToInt32(dr["IsDelete"]);

                    int type = ZaloController.ApiZaloCallForSendZms(userId, message);
                    ZaloController.SaveMtLog(userId, serviceId, commandCode, message, requestId, telco, zaloPartner, type);
                    //if (type >= 0)//SEND TO Zalo Success
                    //{
                        long id = ConvertUtility.ToInt32(dr["Id"]);
                        long lotteryId = ConvertUtility.ToInt32(dr["Lottery_day_Id"]);
                        if(isDelete == 1)
                        {
                            ZaloController.ZaloQuereXoSoDelete(id, lotteryId);
                        }
                        else
                        {
                            ZaloController.ZaloQuereXoSoDelete(id, 0);
                        }
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("Zalo KETQUA XoSo TuongThuat Error : " + ex);
            return 0;
        }
        return 1;
    }
    
}


#line default
#line hidden
