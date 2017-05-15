#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\WsActiveJob.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ABD91B0A9A76972B388DA215AFF3E7046AF6F844"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\WsActiveJob.cs"
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


#line default
#line hidden
