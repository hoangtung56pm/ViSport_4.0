using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for ThanTai_UpdateReport
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ThanTai_UpdateReport : System.Web.Services.WebService {

    public ThanTai_UpdateReport () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ThanTaiProcess));

    [WebMethod]
    public int Execute(int jobID)
    {
        try
        {
            DataTable dtUser = GetAllUser();
            if (dtUser != null && dtUser.Rows.Count > 0)
            {

                foreach (DataRow _rowUser in dtUser.Rows)
                {
                    UpdateReport(Convert.ToString(_rowUser["User_ID"]));
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("***** ThanTai Loi lay update report : " + ex);
            return 0;
        }
        return 1;
    }
    public static void UpdateReport(string UserID)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "ThanTai_UpdateReport", UserID
                          
                            );
    }
    public DataTable GetAllUser()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "ThanTai_GetAllUser");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return new DataTable();
    }
}
