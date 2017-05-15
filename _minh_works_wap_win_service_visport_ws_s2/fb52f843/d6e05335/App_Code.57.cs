#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\CdrGpc.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "118DA2C8F145D9E8B90DC8DAB2309AECAA74955E"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\CdrGpc.cs"
using System;
using System.Data;
using System.IO;
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
    public int Execute(int jobID)
    {
        try
        {
            DateTime dataDate = DateTime.Today.AddDays(-1); 
            DataTable dtUsers = ViSport_S2_Registered_UsersController.SamCdrGpc();
            if (dtUsers != null && dtUsers.Rows.Count > 0)
            {
                foreach (DataRow dr in dtUsers.Rows)
                {
                     //create a writer and open the file
                    var tw = new StreamWriter(Server.MapPath(AppEnv.GetSetting("SamCdr") + "/Cdr_Gpc_" + dataDate.Year + "" + dataDate.Month + "" + dataDate.Day + ".txt"), true);

                    string cdrData = "SUB"
                                     + "|" + "GPC"
                                     + "|" + dr["Service_Name"]
                                     + "|" + dr["shortCode"]
                                     + "|" + dr["msisdn"]
                                     + "|" + dr["cost"]
                                     + "|" + "1"
                                     + "|" + dr["TimeStamp"];

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
            _log.Error("CDR_GPC Loi lay Tap User : " + ex);
            return 0;
        }
        return 1;
    }
    
}


#line default
#line hidden
