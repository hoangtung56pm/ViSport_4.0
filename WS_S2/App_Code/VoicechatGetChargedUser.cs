using System;
using System.Data;
using System.Web.Configuration;
using System.Web.Services;
using log4net;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for VoicechatGetChargedUser
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class VoicechatGetChargedUser : System.Web.Services.WebService, IJobExecutorSoap
{

    public VoicechatGetChargedUser () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly ILog _log = LogManager.GetLogger(typeof(VoicechatGetChargedUser));

    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            DataTable dt = VoiceChatGetUser();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    tbl_billing_info item = new tbl_billing_info();

                    #region Gán Value cho Item
                    
                    #endregion

                    //Insert vao bang tbl_Billing
                    ApiVmsAppboxRegisteredUsersAdd(item);
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("***** VoicechatGetChargedUser Loi lay tap User : " + ex);
            return 0;
        }
        return 1;
    }

    #region DATA PROCESS

    public static DataTable VoiceChatGetUser()
    {
        DataSet ds = SqlHelper.ExecuteDataset(
                                WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString,
                                "Select * From tbl_voicechat ");
        if (ds != null && ds.Tables.Count > 0)
            return ds.Tables[0];
        return new DataTable();
    }

    public static bool ApiVmsAppboxRegisteredUsersAdd(tbl_billing_info e)
    {
        try
        {
            SqlHelper.ExecuteNonQuery(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, ""
                                     , e.Ani
                                     , e.TotalAmount
                                     , e.DeductedAmount
                                     , e.UserBalance
                                     , e.Isprepaid
                                     , e.Datetime
                                     , e.Recordstatus
                                     , e.Errordesc
                                     , e.Circleid
                                     , e.TypeEvent
                                     , e.IsEmm
                                     , e.Mode
                                     , e.ProcessDatetime
                                     , e.Src
                                     , e.Noofattempt
                                     , e.Servicename
                                     , e.NextProcessDate
                                     , e.Sysresponse
                                 );

            return true;
        }
        catch (Exception)
        {

            return false;
        }
    }

    #endregion

}
