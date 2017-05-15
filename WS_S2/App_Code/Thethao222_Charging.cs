using SMSManager_API.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for Thethao222_Charging
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Thethao222_Charging : System.Web.Services.WebService, IJobExecutorSoap
{

    public Thethao222_Charging()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog _log = log4net.LogManager.GetLogger(typeof(Thethao222_Charging));

    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            try
            {
                #region GET USER

                DataTable dtUsers = ViSport_S2_Registered_UsersController.Thethao222_GetUserByTypeTp(false);
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
                            (5000 - ConvertUtility.ToInt32(dr["charging_price"].ToString())),
                            cpId,
                            userName,
                            userPass,
                            "http://sv167.vmgmedia.vn:8000/JobSubs/Sport/Thethao222_Notification.asmx"
                            );
                    }
                    _log.Debug("Thethao222_Charging get user :" + dtUsers.Rows.Count);
                }

                #endregion
            }
            catch (Exception)
            {
                //Retry lai khi co loi
                #region GET USER

                DataTable dtUsers = ViSport_S2_Registered_UsersController.GetSportGameUserByTypeTp(false);
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
                            (10000 - ConvertUtility.ToInt32(dr["charging_price"].ToString())),
                            cpId,
                            userName,
                            userPass,
                            "http://sv167.vmgmedia.vn:8000/JobSubs/Sport/Thethao222_Notification.asmx"
                            );
                    }
                }

                #endregion

                //Gui Email Alert
                //DoSendMail("hoangtung.ngo@vmgmedia.vn", "", "LOI CHARGED VISPORT", "Visport xuat hien loi charged he thong da retry lai. Vui long kiem tra lai doanh thu");
            }
        }
        catch (Exception ex)
        {
            _log.Error("Thethao222_Charging Loi lay tap User : " + ex);
            return 0;
        }
        return 1;
    }

}
