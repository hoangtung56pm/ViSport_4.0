using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for WsActiveJob
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WsActiveJob : System.Web.Services.WebService {

    public WsActiveJob () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public bool WsActiveJobSchedule(int status)
    {
        return UpdateJobStatus(status);
    }

    private static bool UpdateJobStatus(int status)
    {
        return ViSport_S2_SMS_MTController.UpdateStatus(status);
    }
    
}
