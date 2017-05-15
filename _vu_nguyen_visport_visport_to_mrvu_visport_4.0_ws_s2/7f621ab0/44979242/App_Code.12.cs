﻿#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\Report.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0B2A617A0417122DB45CD8F7BF574F615E072897"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\Report.cs"
using System;
using System.Data;
using System.Net.Mail;
using System.Web.Services;

/// <summary>
/// Summary description for Report
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Report : System.Web.Services.WebService, IJobExecutorSoap
{

    public Report () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(GetUsers));

    [WebMethod]
    public int Execute(int jobID)
    {
        try
        {

            #region GUI EMAIL THONGKE NGAY HOM TRUOC

            string fromDate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.AddDays(-1).Day;
            string toDate = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
            DataTable dt = ViSport_S2_Registered_UsersController.SportGameHeroThongKe(fromDate, toDate);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                string content = "DV Visport ngày : " + dr["NgayThongKe"] + " | " + "Đăng ký:" + dr["DangKy"] + " | " + "Hủy:" + dr["Huy"] + " | " +
                                 "Tổng charged : " + dr["TongCharged"] + " | " + "Thành công :" + dr["ThanhCong"] + " | " +
                                 "Thất bại : " + dr["ThatBai"];

                DoSendMail("khanhvu.kieu@vmgmedia.vn", "", "THỐNG KÊ VISPORT", content);
            }

            #endregion

            #region Calcel Sub

            ViSport_S2_Registered_UsersController.SportGameHeroSubCancel();

            #endregion

        }
        catch (Exception ex)
        {
            _log.Error("Visport Loi gui Report : " + ex);
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


#line default
#line hidden
