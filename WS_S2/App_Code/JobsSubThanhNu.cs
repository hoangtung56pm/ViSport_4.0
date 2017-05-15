using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Description;
using ChargingGateway;
using SentMT;
using SMSManager_API.Library.Utilities;
using vn.thanhnu;

/// <summary>
/// Summary description for JobsSubThanhNu
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class JobsSubThanhNu : System.Web.Services.WebService, IJobExecutorSoap
{

    public JobsSubThanhNu () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog log = log4net.LogManager.GetLogger(typeof(JobsSubThanhNu));

    public int Execute(int jobId)
    {

        //log.Error(" ");
        //log.Error("****Game Thanh Nu Sub Error******");
        //log.Error("LOG Start");
        //log.Error("**********");
        //log.Error(" ");
        //return 1;

        WebServiceCharging3g webServiceCharging3G = new WebServiceCharging3g();
        vn.thanhnu.Service partnerService = new vn.thanhnu.Service();

        string userName = "GamePortal2";
        string passWord = "g@e@vmg2";
        string cpId = "1937";

        try
        {
            DataTable dtUser = ViSport_S2_Registered_UsersController.ThanhNuGameGetUserByType();
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                string message = string.Empty;
                string partnerResult = string.Empty;

                int price = 1000;

                foreach (DataRow dr in dtUser.Rows)
                {
                    string vnmChargedResult = webServiceCharging3G.PaymentVnmWithAccount(dr["User_ID"].ToString(), "1000","Charged Sub Thanh Nu","Thanh_Nu_Sub",userName,passWord,cpId);

                    if(vnmChargedResult == "1")//CHARGED THANH CONG
                    {
                        partnerResult = partnerService.smsGiaHan(dr["User_ID"].ToString(), "1");
                        if(partnerResult.Trim() == "1")
                        {
                            message = "Goi dich vu Game Thanh Nu  cua Quy Khach da duoc gia han thanh cong. Quy khach duoc cong 110 G_Coin vao tk. Cam on Quy Khach da su dung goi dich vu .";
                            SendMtThanhNu(dr["User_ID"].ToString(),message,dr["Service_ID"].ToString(),dr["Command_Code"].ToString(),dr["Request_ID"].ToString());
                        }
                    }
                    else
                    {
                        partnerResult = partnerService.smsGiaHan(dr["User_ID"].ToString(), "0");
                        if(partnerResult == "1")// CHARGED G_Coint thanh cong
                        {
                            message = "Quy Khach da gia han thanh cong Game Thanh Nu";
                            price = 0;
                            vnmChargedResult = "1";
                        }
                        else if (partnerResult == "0")
                        {
                            message = "Tk cua Quy Khach khong du de gia han Game Thanh Nu ";
                        }

                        SendMtThanhNu(dr["User_ID"].ToString(), message, dr["Service_ID"].ToString(), dr["Command_Code"].ToString(), dr["Request_ID"].ToString());
                    }

                    //LOG DOANH THU
                    var e = new ThanhNuChargedUserLogInfo();

                    e.ID = ConvertUtility.ToInt32(dr["ID"].ToString());
                    e.User_ID = dr["User_ID"].ToString();
                    e.Request_ID = dr["Request_ID"].ToString();
                    e.Service_ID = dr["Service_ID"].ToString();
                    e.Command_Code = dr["Command_Code"].ToString();
                    e.Service_Type = 0;
                    e.Charging_Count = 0;
                    e.FailedChargingTime = 0;
                    e.RegisteredTime = DateTime.Now;
                    e.ExpiredTime = DateTime.Now.AddDays(1);
                    e.Registration_Channel = "SMS";
                    e.Status = 1;
                    e.Operator = "vnmobile";

                    if(vnmChargedResult == "1")
                    {
                        e.Reason = "Succ";
                    }
                    else
                    {
                        e.Reason = vnmChargedResult;
                    }
                    
                    e.Price = price;
                    e.PartnerResult = partnerResult;

                    ViSport_S2_Registered_UsersController.ThanhNuChargedUserLog(e);
                }
            }

            return 1;
        }
        catch (Exception ex)
        {
            log.Error(" ");
            log.Error("****Game Thanh Nu Sub Error******");
            log.Error(ex.ToString());
            log.Error("**********");
            log.Error(" ");

            return 0;
        }
    }

    public void SendMtThanhNu(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
    {
        var objSentMt = new ServiceProviderService();

        if (AppEnv.GetSetting("TestFlag") == "0")
        {
            int result = objSentMt.sendMT(userId, mtMessage, serviceId, commandCode, "1", requestId, "1", "1", "0", "0");
            log.Debug("Send MT result : " + result);
            log.Debug("userId : " + userId);
            log.Debug("Noi dung MT : " + mtMessage);
            log.Debug("ServiceId : " + serviceId);
            log.Debug("commandCode : " + commandCode);
            log.Debug("requestId : " + requestId);
        }

        var objMt = new ViSport_S2_SMS_MTInfo();
        objMt.User_ID = userId;
        objMt.Message = mtMessage;
        objMt.Service_ID = serviceId;
        objMt.Command_Code = commandCode;
        objMt.Message_Type = 1;
        objMt.Request_ID = requestId;
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
    }
    
}
