using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for CDR_VNP8979Process
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class CDR_VNP8979Process : System.Web.Services.WebService {

    public CDR_VNP8979Process () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    
    log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ThanTaiProcess));

    [WebMethod]
    public int Execute(int jobID)
    {
        try
        {
            DataTable dtPartner = GetAllPartner();
            if (dtPartner!=null && dtPartner.Rows.Count > 0)
            {
                foreach (DataRow _rowPartner in dtPartner.Rows)
                {
                    DataTable dtDichVu = GetAllDichVuByPartner(Convert.ToInt32(_rowPartner["PartnerID"]));
                    if (dtDichVu != null && dtDichVu.Rows.Count > 0)
                    {
                        foreach (DataRow _rowDV in dtDichVu.Rows)
                        {
                            DateTime now = DateTime.Now.AddDays(-1);
                            int day = now.Day;
                            int month = now.Month;
                            int year = now.Year;
                            Insert_CDR_VNP_8979_ByDay(day, month, year, Convert.ToInt32(_rowPartner["PartnerID"]), Convert.ToString(_rowDV["Service_ID"]));
                        }
                    }
                }
               
                               
            }
        }
        catch (Exception ex)
        {
            _log.Error("***** Loi CDR_VNP8979Process : " + ex);
            return 0;
        }
        return 1;
    }
    #region Methods
    public static string ConnTTNDService = AppEnv.GetConnectionString("localsqlttndservices");
    public DataTable GetAllPartner()
    {
        DataSet ds = SqlHelper.ExecuteDataset(ConnTTNDService, "CDR_GetAllPartner");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return new DataTable();
    }
    public DataTable GetAllDichVuByPartner(int PartnerID)
    {
        DataSet ds = SqlHelper.ExecuteDataset(ConnTTNDService, "CDR_VNP_GetAllDichVuByPartner", PartnerID);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return new DataTable();
    }
    public static void Insert_CDR_VNP_8979_ByDay(int day,int month, int year, int partnerid, string serviceid)
    {
        SqlHelper.ExecuteNonQuery(ConnTTNDService, "CDR_VNP_8979_ByDay",
                          day, month, year, partnerid, serviceid
                            );
    }
    #endregion
}
