using Microsoft.ApplicationBlocks.Data;
using SMSManager_API.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for VClip_Cancel_Import
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class VClip_Cancel_Import : System.Web.Services.WebService, IJobExecutorSoap
{
    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(AutoCancelUser_Import));

    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            DataTable dt = Vclip_GetAllUser_Tocancel();
            if (dt != null && dt.Rows.Count > 0)
            {

                foreach (DataRow _dr in dt.Rows)
                {
                    try
                    {
                        int ID = ConvertUtility.ToInt32(_dr["ID"]);
                        Vclip_CancelUser_Import(ID);

                    }
                    catch (Exception ex)
                    {
                        _log.Error("User_ID : " + _dr["User_ID"].ToString() + "--" + ex.ToString());
                    }
                }
            }
            _log.Info(" Visport Number of users to cancel : " + dt.Rows.Count);
            return 1;
        }
        catch (Exception ex)
        {
            _log.Error("***** Loi cancel User Visport Import : " + ex);
            return 0;
        }

    }
    #region Method
    public DataTable Vclip_GetAllUser_Tocancel()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Vclip_GetAllUser_Tocancel");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return new DataTable();
    }
    public void Vclip_CancelUser_Import(int ID)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Vclip_CancelUser_Import",
                           ID
                            );
    }
    #endregion

}
