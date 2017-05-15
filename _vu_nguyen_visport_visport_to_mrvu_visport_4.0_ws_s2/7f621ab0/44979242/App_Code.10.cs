#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\JobsSubBigSendMt.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9139BFF67BD03B576F3CF69B9D23375F7E45C053"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\JobsSubBigSendMt.cs"
using System;
using System.Data;
using System.Web.Services;

/// <summary>
/// Summary description for JobsSubBigSendMt
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class JobsSubBigSendMt : System.Web.Services.WebService, IJobExecutorSoap
{

    public JobsSubBigSendMt () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(JobsSubBigSendMt));

    [WebMethod]
    public int Execute(int jobID)
    {
        try
        {

           
                #region SEND MT MDT

                DataTable dt = ViSport_S2_Registered_UsersController.ThanhNuAllUserForSendMt();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        string userId = dr["User_Id"].ToString();
                        string message = "Ban da gia han thanh cong goi dich vu Big promotion cua Vietnamobile. Ban duoc them " + dr["Total"] + " MDT la: " + dr["Code"];

                        AppEnv.SendMtVmgPortal(userId, "949", "GOI", message);
                        ViSport_S2_Registered_UsersController.ThanhNuCodeTempDelete(userId);

                        #region LOG MT Send

                        var objMt = new ViSport_S2_SMS_MTInfo();
                        objMt.User_ID = userId;
                        objMt.Message = message;
                        objMt.Service_ID = "949";
                        objMt.Command_Code = "GOI";
                        objMt.Message_Type = 1;
                        objMt.Request_ID = "0";
                        objMt.Total_Message = 1;
                        objMt.Message_Index = 0;
                        objMt.IsMore = 0;
                        objMt.Content_Type = 0;
                        objMt.ServiceType = 0;
                        objMt.ResponseTime = DateTime.Now;
                        objMt.isLock = false;
                        objMt.PartnerID = "VNM";
                        objMt.Operator = "vnmobile";

                        ViSport_S2_SMS_MTController.InsertThanhNuMt(objMt);

                        #endregion

                        _log.Debug(" ");
                        _log.Debug(" ");
                        _log.Debug("-------------------- BIG PROMOTION SendMt to VMG-Portal -------------------------");
                        _log.Debug("User_ID: " + userId);
                        _log.Debug("Message: " + message);
                        _log.Debug(" ");
                        _log.Debug(" ");

                    }
                }

                #endregion
            
        }
        catch (Exception ex)
        {
            _log.Error("BIG PROMOTION Loi lay tap User : " + ex);
            return 0;
        }
        return 1;
    }

    
}


#line default
#line hidden
