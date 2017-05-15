using System;
using System.Data;
using System.Web.Configuration;
using System.Web.Services;
using ChargingGateway;
using log4net;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for VoicechatCharged
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class VoicechatCharged : System.Web.Services.WebService {

    public VoicechatCharged () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly ILog _log = LogManager.GetLogger(typeof(VoicechatGetChargedUser));

    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            DataTable dt = VoiceChatGetBillingUser();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    #region GOI SANG VNM Charged lui

                    string res = PaymentChargingOptimize(dr["ANI"].ToString());
                    string status = res.Split('|')[0];
                    string des = res.Split('|')[1];

                    #region GHI LOG vao bang : TBL_BILLING_YYYYMM
                    
                    #endregion

                    #region Xoa du lieu ban ghi trong bang : TBL_BILLING
                    
                    #endregion

                    #endregion
                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("***** VoicechatCharged Loi lay tap User charged : " + ex);
            return 0;
        }
        return 1;
    }

    #region DATA PROCESS

    public static DataTable VoiceChatGetBillingUser()
    {
        DataSet ds = SqlHelper.ExecuteDataset(
                                WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString,
                                "Select * From tbl_billing ");
        if (ds != null && ds.Tables.Count > 0)
            return ds.Tables[0];
        return new DataTable();
    }


    public string PaymentChargingOptimize(string userId)
    {
        var webServiceCharging3G = new WebServiceCharging3g();

        string voiceChatUserName = "";
        string voiceChatPassword = "";
        string voiceChatCpId = "";

        string notEnoughMoney = AppEnv.GetSetting("NotEnoughMoney");

        string price = "3000";
        string msgReturn = webServiceCharging3G.PaymentVnmWithAccount(userId, price, "voicechat:" + userId, "voicechat", voiceChatUserName, voiceChatPassword, voiceChatCpId);
        if (msgReturn.Trim() == notEnoughMoney)
        {
            price = "2000";
            msgReturn = webServiceCharging3G.PaymentVnmWithAccount(userId, price, "voicechat:" + userId, "voicechat", voiceChatUserName, voiceChatPassword, voiceChatCpId);
            if (msgReturn.Trim() == notEnoughMoney)
            {
                price = "1000";
                msgReturn = webServiceCharging3G.PaymentVnmWithAccount(userId, price, "voicechat:" + userId, "voicechat", voiceChatUserName, voiceChatPassword, voiceChatCpId);
            }
        }

        return msgReturn + "|" + price;
    }
    

    #endregion
    
}
