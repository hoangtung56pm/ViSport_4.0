#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\Truyencuoi.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1C54348537E2D094992426BF53F5A22170FA58C0"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\Truyencuoi.cs"
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using ChargingGateway;

/// <summary>
/// Summary description for Truyencuoi
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Truyencuoi : System.Web.Services.WebService, IJobExecutorSoap
{

    public Truyencuoi()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(Truyencuoi));

    [WebMethod]
    public int Execute(int jobID)
    {

        try
        {
            var webServiceCharging3G = new WebServiceCharging3g();
            string userName = AppEnv.GetSetting("userName_3g_WapVnm");
            string userPass = AppEnv.GetSetting("password_3g_WapVnm");
            string cpId = AppEnv.GetSetting("cpId_3g_WapVnm");
            string price;

            string returnValue = string.Empty;

            string serviceType = "Charged Sub S2_94x Bonus";
            string serviceName = "S2_94x Bonus";


            DataTable dtUser = ViSport_S2_Registered_UsersController.S294XGetUserRegisteredByServiceId(12);
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                foreach (DataRow dr in dtUser.Rows)
                {
                    string userId = dr["User_ID"].ToString();
                    price = "1000";
                    returnValue = webServiceCharging3G.PaymentVnmWithAccount(userId, price, serviceType, serviceName, userName, userPass, cpId);

                    if (returnValue == "1")//CHARGED THANH CONG
                    {
                        #region GHI LOG DOANH THU

                        ViSport_S2_Registered_UsersController.S294XChargedUserLog3G(
                                                        userId, dr["Request_ID"].ToString(), dr["Service_Type"].ToString(),
                                                        dr["Service_ID"].ToString(), dr["Id"].ToString(), dr["Short_Code"].ToString(),
                                                        dr["Command_Code"].ToString(), "1000", "Charged 12 Shit", "1", userName);

                        #endregion
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("S2 94x Lay tap User Loi : " + ex);
            return 0;
        }
        return 1;

    }
    
}


#line default
#line hidden
