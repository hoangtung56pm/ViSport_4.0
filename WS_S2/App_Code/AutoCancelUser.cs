using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for AutoCancelUser
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class AutoCancelUser : System.Web.Services.WebService, IJobExecutorSoap {

    public AutoCancelUser () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(AutoCancelUser));

    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            //UpdateReport();
            ViSport_S2_Registered_UsersController.SportGameHeroSubCancel();
        }
        catch (Exception ex)
        {
            _log.Error("***** Loi cancel DK TP 979 report : " + ex);
            return 0;
        }
        return 1;
    }
    //public static void UpdateReport()
    //{
    //    SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Sport_Game_Hero_Registered_Users_AutoCancel_User"
                          
    //                        );
    //}
   
}
    

