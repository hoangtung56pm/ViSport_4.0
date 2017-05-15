using SMSManager_API.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for VclipUpdateStatus
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class VclipUpdateStatus : System.Web.Services.WebService, IJobExecutorSoap
{

    public VclipUpdateStatus()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog _log = log4net.LogManager.GetLogger(typeof(VclipUpdateStatus));

    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            try
            {
                #region GET USER
                ViSport_S2_Registered_UsersController.VClip_UpdateStatusCharging();
                #endregion
            }
            catch (Exception)
            {
                
            }
        }
        catch (Exception ex)
        {
            _log.Error("update Charge status User Vclip : " + ex);
            return 0;
        }
        return 1;
    }



}
