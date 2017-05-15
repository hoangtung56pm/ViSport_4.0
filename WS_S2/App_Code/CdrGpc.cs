using SMSManager_API.Library.Utilities;
using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Web.Services;

/// <summary>
/// Summary description for CdrGpc
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class CdrGpc : System.Web.Services.WebService, IJobExecutorSoap
{

    public CdrGpc () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(CdrGpc));
    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            DataTable dtPartner = ViSport_S2_Registered_UsersController.PartnerGetAll_Active();
            //const int partnerId = 21;
            ViSport_S2_Registered_UsersController.GpcCdrReset();//RESET
            foreach (DataRow drPartner in dtPartner.Rows)
            {
                int partnerId = ConvertUtility.ToInt32(drPartner["PartnerID"].ToString());
                DataTable dtVms = ViSport_S2_Registered_UsersController.GPC_CdrPartnerGetServiceId(partnerId);
                if (dtVms != null && dtVms.Rows.Count > 0)
                {
                    

                    foreach (DataRow drSv in dtVms.Rows)
                    {
                        string serviceName = drSv["Service_Name"].ToString();
                        string serviceId = drSv["Service_Id"].ToString();
                        const string shortCode = "8979";
                        string registerSystax = drSv["Register_Syntax"].ToString();
                        try
                        {
                            DataTable dtUsers = ViSport_S2_Registered_UsersController.GPCGetCdrByServiceId(serviceId);
                            if (dtUsers != null && dtUsers.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dtUsers.Rows)
                                {
                                    ViSport_S2_Registered_UsersController.GPCCdrAdd(
                                        dr["msisdn"].ToString(),
                                        serviceId,
                                        ConvertUtility.ToInt32(dr["cost"].ToString()),
                                        serviceName,
                                        shortCode,
                                        registerSystax, dr["TimeStamp"].ToString(),
                                        //ConvertUtility.ToInt32(dr["ChargeResult"]),
                                        partnerId
                                        );
                                }
                            }
                            Thread.Sleep(5000);
                        }
                        catch(Exception ex)
                        {

                        }
                    }
                }
            }
            //Insert log chung Ok
            _log.Debug("CDR GPC update");
        }
        catch (Exception ex)
        {
            _log.Error("CDR Loi UPDATE GPC : " + ex);
            return 0;
        }
        return 1;
    }
    //[WebMethod]
    //public int Execute(int jobId)
    //{
    //    try
    //    {
    //        DateTime dataDate = DateTime.Today.AddDays(-1); 
    //        DataTable dtUsers = ViSport_S2_Registered_UsersController.SamCdrGpc();
    //        if (dtUsers != null && dtUsers.Rows.Count > 0)
    //        {
    //            foreach (DataRow dr in dtUsers.Rows)
    //            {
    //                 //create a writer and open the file
    //                var tw = new StreamWriter(Server.MapPath(AppEnv.GetSetting("SamCdr") + "/Cdr_Gpc_" + dataDate.Year + "" + dataDate.Month + "" + dataDate.Day + ".txt"), true);

    //                string cdrData = "SUB"
    //                                 + "|" + "GPC"
    //                                 + "|" + dr["Service_Name"]
    //                                 + "|" + dr["shortCode"]
    //                                 + "|" + dr["msisdn"]
    //                                 + "|" + dr["cost"]
    //                                 + "|" + "1"
    //                                 + "|" + dr["TimeStamp"];

    //                tw.WriteLine();
    //                // write a line of text to the file
    //                tw.WriteLine(cdrData);

    //                // close the stream
    //                tw.Close();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        _log.Error("CDR_GPC Loi lay Tap User : " + ex);
    //        return 0;
    //    }
    //    return 1;
    //}
    
}
