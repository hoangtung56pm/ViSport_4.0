#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\SamGpcCdrUpdate.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5B49CF032331F80F51CB0B0009F1BEF6CEE30451"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\SamGpcCdrUpdate.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for SamGpcCdrUpdate
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SamGpcCdrUpdate : System.Web.Services.WebService, IJobExecutorSoap
{

    public SamGpcCdrUpdate () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(CdrGpc));

    [WebMethod]
    public int Execute(int jobID)
    {
        try
        {
            ViSport_S2_Registered_UsersController.SamGpcUniqueIdUpdate();
        }
        catch (Exception ex)
        {
            _log.Error("CDR Loi UPDATE Unique_Id SAM : " + ex);
            return 0;
        }
        return 1;

    }

}


#line default
#line hidden
