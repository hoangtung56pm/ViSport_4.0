using SMSManager_API.Library.Utilities;
using System;
using System.Data;
using System.IO;
using System.Threading;
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
    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(CdrVms));


    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            DataTable dtPartner = ViSport_S2_Registered_UsersController.PartnerGetAll_Active();
            //const int partnerId = 21;
            ViSport_S2_Registered_UsersController.VmsCdrReset();//RESET
            foreach (DataRow drPartner in dtPartner.Rows)
            {
                int partnerId = ConvertUtility.ToInt32(drPartner["PartnerID"].ToString());
                DataTable dtVms = ViSport_S2_Registered_UsersController.CdrPartnerGetServiceId(partnerId);
                if (dtVms != null && dtVms.Rows.Count > 0)
                {
                    

                    foreach (DataRow drSv in dtVms.Rows)
                    {
                        string serviceName = drSv["Service_Name"].ToString();
                        string serviceId = drSv["Service_Id"].ToString();
                        const string shortCode = "8979";
                        string registerSystax = drSv["Register_Syntax"].ToString();

                        try {
                        DataTable dtUsers = ViSport_S2_Registered_UsersController.SamVmsGetCdrByServiceId(serviceId);
                        if (dtUsers != null && dtUsers.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtUsers.Rows)
                            {
                                ViSport_S2_Registered_UsersController.VmsCdrAdd(
                                    dr["msisdn"].ToString(),
                                    serviceId,
                                    ConvertUtility.ToInt32(dr["cost"].ToString()),
                                    serviceName,
                                    shortCode,
                                    registerSystax, dr["TimeStamp"].ToString(),
                                    ConvertUtility.ToInt32(dr["ChargeResult"]),
                                    partnerId
                                    );
                            }
                        }
                            Thread.Sleep(5000);
                        } catch(Exception ex)
                        {

                        }
                    }
                }
            }
            //Insert log chung Ok
            _log.Debug("CDR VMS update");
        }
        catch (Exception ex)
        {
            _log.Error("CDR Loi UPDATE VMS : " + ex);
            return 0;
        }
        return 1;

    }
    //readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(CdrVnm));

    //[WebMethod]
    //public int Execute(int jobId)
    //{
    //    try
    //    {

    //        DateTime dataDate = DateTime.Today.AddDays(-1); 

    //        DataTable dtSvId = ViSport_S2_Registered_UsersController.SamVmsGetServiceId();
    //        if(dtSvId != null && dtSvId.Rows.Count > 0)
    //        {
    //            foreach(DataRow drSv in dtSvId.Rows)
    //            {
    //                string serviceName = drSv["Service_Name"].ToString();
    //                string serviceId = drSv["Service_Id"].ToString();
    //                const string shortCode = "8979";

    //                DataTable dtUsers = ViSport_S2_Registered_UsersController.SamVmsGetCdrByServiceId(serviceId);
    //                if(dtUsers != null && dtUsers.Rows.Count > 0)
    //                {
    //                    foreach(DataRow dr in dtUsers.Rows)
    //                    {
    //                        // create a writer and open the file
    //                        var tw = new StreamWriter(Server.MapPath(AppEnv.GetSetting("SamCdr") + "/Cdr_Vms_" + dataDate.Year + "" + dataDate.Month + "" + dataDate.Day + ".txt"), true);

    //                        string cdrData = "SUB"
    //                                         + "|" + "VMS"
    //                                         + "|" + serviceName
    //                                         + "|" + shortCode
    //                                         + "|" + dr["MSISDN"]
    //                                         + "|" + dr["Cost"]
    //                                         + "|" + dr["ChargeResult"]
    //                                         + "|" + dr["TimeStamp"];

    //                        tw.WriteLine();
    //                        // write a line of text to the file
    //                        tw.WriteLine(cdrData);

    //                        // close the stream
    //                        tw.Close();
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        _log.Error("CDR_VMS Loi lay Tap User : " + ex);
    //        return 0;
    //    }
    //    return 1;
    //}
    
    
}
