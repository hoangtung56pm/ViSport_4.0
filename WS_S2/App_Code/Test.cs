using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using ChargingGateway;
using SentMT;

/// <summary>
/// Summary description for Test
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Test : System.Web.Services.WebService, IJobExecutorSoap
{

    public Test () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog log = log4net.LogManager.GetLogger(typeof(Test));

    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            DataTable dt = ViSport_S2_Registered_UsersController.GetQuestionInfoSportGameHero();
            if (dt != null && dt.Rows.Count > 0)
            {
                var webServiceCharging3G = new WebServiceCharging3g();
                string returnValue = webServiceCharging3G.PaymentVnmWithAccount("9999999", "1000", "Charged Sub Anh Tai", "Anh_Tai_Sub", "", "", "");
                log.Error("****");
                log.Error("****");
                log.Error("163 Call 139 : Sucess");
                log.Error("Call charging Service : " + returnValue);
                log.Error("****");
                log.Error("****");
            }
        }
        catch (Exception ex)
        {
            log.Error("****");
            log.Error("****");
            log.Error("163 Call 139 : " + ex);
            log.Error("****");
            log.Error("****");
        }
        
        return 1;
    }

}
