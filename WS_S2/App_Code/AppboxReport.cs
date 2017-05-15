using System;
using System.Data;
using System.Web.Services;
using log4net;
using Microsoft.ApplicationBlocks.Data;
using SMSManager_API.Library.Utilities;

/// <summary>
/// Summary description for AppboxReport
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class AppboxReport : System.Web.Services.WebService, IJobExecutorSoap {

    public AppboxReport () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly ILog _log = LogManager.GetLogger(typeof(AppboxReport));

    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            _log.Debug("----------------------------------------------------");
            DataTable dt = AppboxGetPartners();
            if (dt.Rows.Count > 0)
            {
                _log.Debug("partner count: " + dt.Rows.Count);
                int day = DateTime.Now.Day;
                int month = DateTime.Now.Month;
                int year = DateTime.Now.Year;

                foreach (DataRow dr in dt.Rows)
                {
                    int partnerId = ConvertUtility.ToInt32(dr["Id"].ToString());
                    _log.Debug("partner: " + partnerId);
                    ExcuteReport(day, month, year, partnerId, 1);//TAI GAME
                    ExcuteReport(day, month, year, partnerId, 2);//GOI NGAY
                    ExcuteReport(day, month, year, partnerId, 3);//GOI TUAN
                }
            }
            else
            {
                _log.Debug("Partner null");            
            }
            _log.Debug("----------------------------------------------------");
        }
        catch (Exception ex)
        {
            _log.Error("Appbox Loi gui Report : " + ex);
            return 0;
        }
        return 1;
    }

    #region Get Data

    public DataTable AppboxGetPartners()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString56, "Vms_Appbox_Report");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return new DataTable();
    }

    public void ExcuteReport(int day,int month,int year,int partnerId,int serviceId)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString56, "CDR_MGame_ByDay", day, month, year, partnerId, serviceId);
    }

    #endregion

}
