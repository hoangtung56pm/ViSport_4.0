#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\JobSubBigSendMtRemind.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "05574652809CB0455693B01EBC73368452737F89"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\JobSubBigSendMtRemind.cs"
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using RingTone;
using ShotAndPrint;
using vn.vmgame;

/// <summary>
/// Summary description for JobSubBigSendMtRemind
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class JobSubBigSendMtRemind : System.Web.Services.WebService, IJobExecutorSoap
{

    public JobSubBigSendMtRemind () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(JobSubBigSendMtRemind));

    [WebMethod]
    public int Execute(int jobID)
    {
        try
        {

            #region SEND MT REMIND

            DataTable dtRemind = ViSport_S2_Registered_UsersController.ThanhNuAllUserForSendMtRemind(5);
            if (dtRemind != null && dtRemind.Rows.Count > 0)
            {
                foreach (DataRow dr in dtRemind.Rows)
                {

                    string userId = dr["User_Id"].ToString();
                    const string message = "QKhach da duoc trai nghiem dich vu gia tri gia tang trong 5 ngay. He thong se huy dich vu tu dong sau 15 ngay hoac qkhach dung dvu ngay lap tuc bang cach soan: HUY GOI gui 949";

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
                    _log.Debug("-------------------- BIG PROMOTION SendMt to VMG-Portal REMIND 5 DAY -------------------------");
                    _log.Debug("User_ID: " + userId);
                    _log.Debug("Message: " + message);
                    _log.Debug(" ");
                    _log.Debug(" ");

                }
            }

            DataTable dtDel = ViSport_S2_Registered_UsersController.ThanhNuAllUserForSendMtRemind(20);
            if (dtDel != null && dtDel.Rows.Count > 0)
            {
                foreach (DataRow dr in dtDel.Rows)
                {

                    string userId = dr["User_Id"].ToString();
                    #region HUY DICH VU

                    _log.Debug(" ");
                    _log.Debug(" ");
                    _log.Debug("-------------------- BIG PROMOTION DELETE AFTER 15 DAY -------------------------");
                    _log.Debug("User_ID: " + userId);
                    _log.Debug(" ");
                    _log.Debug(" ");

                    //string message;
                    DataTable dt = ViSport_S2_Registered_UsersController.ThanhNuRegisterUserStatusUpdate(userId, 0);

                    if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                    {
                        //message = "Quy khach da huy thanh cong goi dich vu ( bao gom game portal, shot and print, nhac chuong). Ma du thuong cua Qkhach se khong duoc tham gia quay thuong. De dang ky lai dich vu soan GOI gui 949";

                        #region HUY VMGAME

                        var vmgame = new Service_RegisS2();
                        string vmRes = vmgame.BigPromotionDelete(userId, "BigPro123!@#Tqscd");

                        _log.Debug(" ");
                        _log.Debug(" ");
                        _log.Debug("-------------------- BIG PROMOTION VmGameResult DELETE AFTER 15 DAY -------------------------");
                        _log.Debug("User_ID: " + userId);
                        _log.Debug("vmGameResult: " + vmRes);
                        _log.Debug(" ");
                        _log.Debug(" ");



                        #endregion

                        #region HUY SHOT and PRINT

                        var shot = new S2Process();
                        string shotRes = shot.BPCancel(userId, "4", "HUY GOI 949");

                        _log.Debug(" ");
                        _log.Debug(" ");
                        _log.Debug("-------------------- BIG PROMOTION shotResult DELETE AFTER 15 DAY -------------------------");
                        _log.Debug("User_ID: " + userId);
                        _log.Debug("shotResult: " + shotRes);
                        _log.Debug(" ");
                        _log.Debug(" ");

                        #endregion

                        #region HUY NC1

                        var ringTone = new NC1_Handler();
                        string ringToneRest = ringTone.SyncSubscriptionData("949", "DK", userId, "DK GOI", "0", "472", "0", "0", "HUY GOI");

                        _log.Debug(" ");
                        _log.Debug(" ");
                        _log.Debug("-------------------- BIG PROMOTION ringToneRes DELETE AFTER 15 DAY -------------------------");
                        _log.Debug("User_ID: " + userId);
                        _log.Debug("ringToneRest: " + ringToneRest);
                        _log.Debug(" ");
                        _log.Debug(" ");

                        #endregion

                        //SendMtThanhNu(userId, message, "949", "GOI", RandomActiveCode.Generate(10));
                       
                    }

                    #endregion
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
