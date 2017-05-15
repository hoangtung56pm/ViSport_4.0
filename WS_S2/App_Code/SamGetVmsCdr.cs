using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using SMSManager_API.Library.Utilities;

/// <summary>
/// Summary description for SamGetVmsCdr
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SamGetVmsCdr : System.Web.Services.WebService, IJobExecutorSoap
{

    public SamGetVmsCdr () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(CdrGpc));


    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            const int partnerId = 21;
            DataTable dtVms = ViSport_S2_Registered_UsersController.CdrPartnerGetServiceId(partnerId);
            if (dtVms != null && dtVms.Rows.Count > 0)
            {
                ViSport_S2_Registered_UsersController.SamVmsCdrReset();//RESET

                foreach (DataRow drSv in dtVms.Rows)
                {
                    string serviceName = drSv["Service_Name"].ToString();
                    string serviceId = drSv["Service_Id"].ToString();
                    const string shortCode = "8979";
                    string registerSystax = drSv["Register_Syntax"].ToString();

                    DataTable dtUsers = ViSport_S2_Registered_UsersController.SamVmsGetCdrByServiceId(serviceId);
                    if (dtUsers != null && dtUsers.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtUsers.Rows)
                        {
                            ViSport_S2_Registered_UsersController.SamVmsCdrAdd(
                                dr["msisdn"].ToString(),
                                serviceId,
                                ConvertUtility.ToInt32(dr["cost"].ToString()),
                                serviceName,
                                shortCode,
                                registerSystax, dr["TimeStamp"].ToString(),
                                ConvertUtility.ToInt32(dr["ChargeResult"])
                                );
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("CDR Loi UPDATE Unique_Id SAM VMS : " + ex);
            return 0;
        }
        return 1;

    }
    
}
