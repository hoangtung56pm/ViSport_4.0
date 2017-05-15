using SMSManager_API.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for EuroCharging
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class EuroCharging : System.Web.Services.WebService, IJobExecutorSoap
{

    public EuroCharging () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog _log = log4net.LogManager.GetLogger(typeof(GetUsers));

    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            try
            {
                #region GET USER

                DataTable dtUsers = ViSport_S2_Registered_UsersController.GetEuroUserByTypeTp();
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
                            5000,
                            cpId,
                            userName,
                            userPass,
                            "http://sv167.vmgmedia.vn:8000/JobSubs/Sport/EuroCheck_Charging.asmx"
                            );
                    }
                }

                #endregion
            }
            catch (Exception)
            {
                //Retry lai khi co loi
                #region GET USER

                DataTable dtUsers = ViSport_S2_Registered_UsersController.GetEuroUserByTypeTp();
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
                            5000,
                            cpId,
                            userName,
                            userPass,
                            "http://sv167.vmgmedia.vn:8000/JobSubs/Sport/EuroCheck_Charging.asmx"
                            );
                    }
                }

                #endregion

                //Gui Email Alert
                DoSendMail("hoangtung.ngo@vmgmedia.vn", "", "LOI CHARGED VISPORT", "Visport xuat hien loi charged he thong da retry lai. Vui long kiem tra lai doanh thu");
            }
        }
        catch (Exception ex)
        {
            _log.Error("Euro Loi lay tap User : " + ex);
            return 0;
        }
        return 1;
    }

    public static bool DoSendMail(string toList, string ccList, string subject, string content)
    {

        //<add key="Mail_Server" value="smtp.gmail.com"/>   
        //<add key="Mail_From" value="alertit@vmgmedia.vn"/>
        //<add key="Mail_UserName" value="alertit@vmgmedia.vn"/>    
        //<add key="Mail_Password" value="truongphongvu"/>
        //<add key="Mail_Port" value="587"/>

        const string userName = "alertit@vmgmedia.vn";
        const string @from = "alertit@vmgmedia.vn";
        const string password = "truongphongvu";
        const string server = "smtp.gmail.com";
        const int port = 587;

        MailMessage message = new MailMessage();
        SmtpClient smtpClient = new SmtpClient();
        string msg = string.Empty;
        try
        {
            MailAddress fromAddress = new MailAddress(from);
            message.From = fromAddress;
            message.To.Add(toList);
            if (ccList != null && ccList != string.Empty)
            {
                message.CC.Add(ccList);
            }
            message.Subject = subject;
            message.IsBodyHtml = true;
            message.Body = content;
            smtpClient.Host = server;
            smtpClient.Port = port;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential(userName, password);

            smtpClient.Send(message);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
}
