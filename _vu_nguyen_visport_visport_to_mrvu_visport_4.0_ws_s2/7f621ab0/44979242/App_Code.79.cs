#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\JobsWorldCupMatchUpdateStatus.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E7E6AC0209187A3EF8BC512F977AD268B58279F6"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\JobsWorldCupMatchUpdateStatus.cs"
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for JobsWorldCupMatchUpdateStatus
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class JobsWorldCupMatchUpdateStatus : System.Web.Services.WebService, IJobExecutorSoap
{

    public JobsWorldCupMatchUpdateStatus () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog log = log4net.LogManager.GetLogger(typeof(JobsWorldCupMatchUpdateStatus));

    [WebMethod]
    public int Execute(int jobID)
    {
        DataTable dt = ViSport_S2_Registered_UsersController.WorldCupMatchStatusUpdate(0);
        if(dt != null)
        {
            log.Debug(" ");
            log.Debug(" ");
            log.Debug("-----------World Cup Match Status Update ---------------");
            log.Debug("Return value : " + dt.Rows[0]["RETURN_ID"]);
            log.Debug(" ");
            log.Debug(" ");

        }
        return 1;
    }

}


#line default
#line hidden
