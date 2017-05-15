#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\CdrVms.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B5E59E0EFD5240DE56C8433149EDEA5F1E3A1AE6"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\CdrVms.cs"
using System;
using System.Data;
using System.IO;
using System.Web.Services;

/// <summary>
/// Summary description for CdrVms
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class CdrVms : System.Web.Services.WebService, IJobExecutorSoap
{

    public CdrVms () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(CdrVnm));

    [WebMethod]
    public int Execute(int jobID)
    {
        try
        {

            DateTime dataDate = DateTime.Today.AddDays(-1); 

            DataTable dtSvId = ViSport_S2_Registered_UsersController.SamVmsGetServiceId();
            if(dtSvId != null && dtSvId.Rows.Count > 0)
            {
                foreach(DataRow drSv in dtSvId.Rows)
                {
                    string serviceName = drSv["Service_Name"].ToString();
                    string serviceId = drSv["Service_Id"].ToString();
                    const string shortCode = "8979";

                    DataTable dtUsers = ViSport_S2_Registered_UsersController.SamVmsGetCdrByServiceId(serviceId);
                    if(dtUsers != null && dtUsers.Rows.Count > 0)
                    {
                        foreach(DataRow dr in dtUsers.Rows)
                        {
                            // create a writer and open the file
                            var tw = new StreamWriter(Server.MapPath(AppEnv.GetSetting("SamCdr") + "/Cdr_Vms_" + dataDate.Year + "" + dataDate.Month + "" + dataDate.Day + ".txt"), true);

                            string cdrData = "SUB"
                                             + "|" + "VMS"
                                             + "|" + serviceName
                                             + "|" + shortCode
                                             + "|" + dr["MSISDN"]
                                             + "|" + dr["Cost"]
                                             + "|" + dr["ChargeResult"]
                                             + "|" + dr["TimeStamp"];

                            tw.WriteLine();
                            // write a line of text to the file
                            tw.WriteLine(cdrData);

                            // close the stream
                            tw.Close();
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("CDR_VMS Loi lay Tap User : " + ex);
            return 0;
        }
        return 1;
    }
    
    
}


#line default
#line hidden
