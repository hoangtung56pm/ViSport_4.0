using SMSManager_API.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for Visport_UpdateStatus
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Visport_UpdateStatus : System.Web.Services.WebService, IJobExecutorSoap
{

    public Visport_UpdateStatus () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog _log = log4net.LogManager.GetLogger(typeof(Visport_UpdateStatus));

    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            try
            {
                #region GET USER

                DataTable dtUsers = ViSport_S2_Registered_UsersController.Visport_getall_user_active();
                if (dtUsers != null && dtUsers.Rows.Count > 0)
                {
                   
                    foreach (DataRow dr in dtUsers.Rows)
                    {
                        ViSport_S2_Registered_UsersController.Update_ChargeStatus(
                           ConvertUtility.ToInt32(dr["Id"].ToString()),
                            dr["User_ID"].ToString()
                           
                            );
                    }
                }

                #endregion
            }
            catch (Exception)
            {
                
                
               
                
            }
        }
        catch (Exception ex)
        {
            _log.Error("update Charge status User : " + ex);
            return 0;
        }
        return 1;
    }

    
    
}
