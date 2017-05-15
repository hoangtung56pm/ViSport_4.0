#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\VclipGetUsers.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "21003FE66E5D3E1B813B1BF590AD615B31727A06"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\VclipGetUsers.cs"
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using SMSManager_API.Library.Utilities;

/// <summary>
/// Summary description for VclipGetUsers
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class VclipGetUsers : System.Web.Services.WebService, IJobExecutorSoap
{

    public VclipGetUsers () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(GetUsers));

    [WebMethod]
    public int Execute(int jobID)
    {
        try
        {
            DataTable dt = ViSport_S2_Registered_UsersController.VClipGetMTByStatus(false);
            if (dt != null && dt.Rows.Count > 0)
            {
                const string userName = "VMGWAP3G";
                const string userPass = "vmg@#3g";
                const string cpId = "1928";

                foreach (DataRow dr in dt.Rows)
                {
                    ViSport_S2_Registered_UsersController.WorldCupUserInsertToAndy(
                        ConvertUtility.ToInt32(dr["Id"].ToString()),
                        dr["User_ID"].ToString(),
                        dr["Request_Id"].ToString(),
                        ConvertUtility.ToInt32(dr["Service_ID"].ToString()),
                        999,
                        2000,
                        cpId,
                        userName,
                        userPass,
                        "http://123.29.67.168:8000/JobSubs/VClip/VclipNotification.asmx"
                        );
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("VClip Loi lay tap User : " + ex);
            return 0;
        }
        return 1;
    }
    
}


#line default
#line hidden
