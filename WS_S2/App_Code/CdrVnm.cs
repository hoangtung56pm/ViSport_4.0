using System;
using System.Data;
using System.IO;
using System.Web.Services;
using SMSManager_API.Library.Utilities;

/// <summary>
/// Summary description for CdrVnm
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class CdrVnm : System.Web.Services.WebService, IJobExecutorSoap
{

    public CdrVnm () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(CdrVnm));

    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            DateTime dataDate = DateTime.Today.AddDays(-1); 
            DataTable dtUsers = ViSport_S2_Registered_UsersController.SamCdrVnm();
            if (dtUsers != null && dtUsers.Rows.Count > 0)
            {
                foreach (DataRow dr in dtUsers.Rows)
                {
                    // create a writer and open the file
                    var tw = new StreamWriter(Server.MapPath(AppEnv.GetSetting("SamCdr") + "/Cdr_Vnm_" + dataDate.Year + "" + dataDate.Month + "" + dataDate.Day + ".txt"), true);

                    string cdrData = "SUB"
                                     + "|" + "VNM"
                                     + "|" + dr["Product_Name"]
                                     + "|" + dr["shortCode"]
                                     + "|" + dr["User_Id"]
                                     + "|" + dr["Charging_Price"]
                                     + "|" + dr["Charging_Response"]
                                     + "|" + ConvertUtility.ToDateTime(dr["Charging_Time"]).ToString("yyyy/MM/dd : hh:mm:ss");

                    tw.WriteLine();
                    // write a line of text to the file
                    tw.WriteLine(cdrData);

                    // close the stream
                    tw.Close();
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("CDR_VNM Loi lay Tap User : " + ex);
            return 0;
        }
        return 1;
    }
    
}
