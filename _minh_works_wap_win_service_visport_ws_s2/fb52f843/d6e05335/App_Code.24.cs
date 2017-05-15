#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\JobsWorldCupSubVtvGetUsers.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1DCA66B12033AAE2F99F297D066E23A87DEBBDEE"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\JobsWorldCupSubVtvGetUsers.cs"
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using SMSManager_API.Library.Utilities;

/// <summary>
/// Summary description for JobsWorldCupSubVtvGetUsers
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class JobsWorldCupSubVtvGetUsers : System.Web.Services.WebService, IJobExecutorSoap
{

    public JobsWorldCupSubVtvGetUsers () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog _log = log4net.LogManager.GetLogger(typeof(JobsWorldCupSubVtvGetUsers));

     [WebMethod]
     public int Execute(int jobID)
     {
         try
         {
             DataTable dtUsers = ViSport_S2_Registered_UsersController.WorldCupGetRegisterUserForChargedVtv();
             if (dtUsers != null && dtUsers.Rows.Count > 0)
             {
                  string userName = AppEnv.GetSetting("userName_3g_visport");
                  string userPass = AppEnv.GetSetting("password_3g_visport");
                  string cpId = AppEnv.GetSetting("cpId_3g_visport");

                 foreach (DataRow dr in dtUsers.Rows)
                 {
                     ViSport_S2_Registered_UsersController.WorldCupUserInsertToAndy(
                         ConvertUtility.ToInt32(dr["Id"].ToString()),
                         dr["User_ID"].ToString(),
                         dr["Request_Id"].ToString(),
                         ConvertUtility.ToInt32(dr["Service_ID"].ToString()),
                         999,
                         3000,
                         cpId, 
                         userName,
                         userPass, 
                         "http://123.29.67.168:8000/JobsWorldCupSubVtvNotification.asmx"
                         );
                 }
             }
         }
         catch (Exception ex)
         {
             _log.Error("WC Loi lay Tap User VTV : " + ex);
             return 0;
         }
         return 1;
     }

}


#line default
#line hidden
