using System;
using System.Web.Services;
using ChargingGateway;
using RingTone;
using SentMT;
using System.Data;
using SMSManager_API.Library.Utilities;
using WS_Music.Library;
using WapJavaGame.Library.Utilities;
using vn.vmgame;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for ThanTai
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ThanTai : System.Web.Services.WebService
{

    public ThanTai()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    log4net.ILog log = log4net.LogManager.GetLogger("File");
    [WebMethod]
    public string WSProcessMoThanTai(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExcecuteRequestMoThanTai(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }
    #region Methods Process Mo
    private string ExcecuteRequestMoThanTai(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        string responseValue = "1";
        int returnValue = 0;

        Message = Message.ToUpper();
        string subcode = "";
        if (Message.Trim().Length > Command_Code.Trim().Length)
        {
            subcode = Message.ToUpper().Substring(Command_Code.Length).Replace(" ", "");
        }
        try
        {
            log.Debug(" ");
            log.Debug(" ");
            log.Debug("-------------------- CAP SO THAN TAI -------------------------");
            log.Debug("User_ID: " + User_ID);
            log.Debug("Service_ID: " + Service_ID);
            log.Debug("Command_Code: " + Command_Code);
            log.Debug("Message: " + Message.ToUpper());
            log.Debug("Request_ID: " + Request_ID);
            log.Debug(" ");
            log.Debug(" ");

            #region Log MO Message Into Database (SMS_MO_Log)

            var moInfo = new SMS_MOInfo();

            moInfo.User_ID = User_ID;
            moInfo.Service_ID = Service_ID;
            moInfo.Command_Code = Command_Code;
            moInfo.Message = Message;
            moInfo.Request_ID = Request_ID;
            moInfo.Operator = GetTelco(User_ID);
            SMS_MODB.InsertThanTaiMo(moInfo);

            #endregion

            string messageReturn;
            if (Command_Code.ToUpper() == "TT")
            {
                DateTime nowTime = DateTime.Now;
                if (nowTime.Hour >= 17 && nowTime.Hour <= 24)
                {
                    messageReturn = "Co hoi ngay hom nay cua ban da het roi. Quay lai voi chung toi vao ngay mai va lua chon cap so tu 00h den 17h hang ngay nhe";
                    SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                }
                else
                {
                    //DataTable dt = ViSport_S2_Registered_UsersController.ThanTai_DemLuot(User_ID);
                    //if (ConvertUtility.ToInt32(dt.Rows[0]["RETURN_ID"].ToString()) >= 10)
                    //{
                    //    messageReturn = "Ban da lua chon 10 cap so may man va het luot lua chon trong hom nay. Cung cho don ket qua nhe, may man dang cho ban.";
                    //    SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                    //}

                    //else
                    //{
                        //string numberregex = "[0-9]{2}";

                        string numberregex = "^[0-9]+$";
                        if (Regex.IsMatch(subcode, numberregex) && subcode.Length == 2)
                        {
                            DataTable dtCheckSub = ViSport_S2_Registered_UsersController.ThanTai_CheckExistSubRegister(User_ID);
                            if (dtCheckSub!=null && dtCheckSub.Rows.Count>0)
                            {
                                DataTable dt = ViSport_S2_Registered_UsersController.ThanTai_DemLuot(User_ID);
                                if (ConvertUtility.ToInt32(dt.Rows[0]["RETURN_ID"].ToString()) >= 10)
                                {
                                    messageReturn = "Ban da lua chon 10 cap so may man va het luot lua chon trong hom nay. Cung cho don ket qua nhe, may man dang cho ban.";
                                    SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                                }

                                else
                                {
                                //if (ViSport_S2_Registered_UsersController.ThanTai_CheckExistMng(User_ID))
                                //{
                                    if (ConvertUtility.ToInt32(dt.Rows[0]["RETURN_ID"].ToString()) < 4)
                                    {
                                        messageReturn = "Ban da lua chon cap so " + subcode + ". May man dang cho ban. TT <cap so> de lua chon cap so tiep theo nang cao co hoi trung thuong.";
                                        SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                                        ThanTai_MT_Controller.Insert_CapSo(User_ID, subcode);
                                    }
                                    else if (ConvertUtility.ToInt32(dt.Rows[0]["RETURN_ID"].ToString()) == 4)
                                    {
                                        messageReturn = "Ban da lua chon cap so " + subcode + ".Ban da het 5 luot lua chon mien phi. TT <cap so> de nang cao diem va co hoi trung thuong (1000d/tin, toi da duọc chon them 5 lan)";
                                        SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                                        ThanTai_MT_Controller.Insert_CapSo(User_ID, subcode);
                                    }
                                    else
                                    {
                                        //Charged                                   

                                        #region Tinh tien tin nhan le

                                        if (CapSoThanTaiCharged(User_ID, Request_ID, Service_ID) == "1")
                                        {
                                            #region Charged THANHCONG

                                            messageReturn = "Ban da lua chon cap so " + subcode + ". May man dang cho ban. TT <cap so> de lua chon cap so tiep theo nang cao co hoi trung thuong.";
                                            SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                                            ThanTai_MT_Controller.Insert_CapSo(User_ID, subcode);
                                            #endregion
                                        }
                                        else if (CapSoThanTaiCharged(User_ID, Request_ID, Service_ID) == "Result:12,Detail:Not enough money.")
                                        {
                                            #region Charged THATBAI ==> GUI ALERT THONG BAO NAP TIEN

                                            messageReturn = "Thue bao khong du tien. Vui long nap tien de tiep tuc choi !";
                                            SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);    //SEND MT THONGBAO

                                            #endregion
                                        }
                                        else
                                        {
                                            #region Charged THATBAI ==> LOI SYSTEM

                                            messageReturn = "He thong dang ban. Vui long tro lai sau it phut !";
                                            SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);    //SEND MT THONGBAO

                                            #endregion
                                        }

                                        #endregion
                                    }
                                }
                            }
                            else
                            {
                                #region Tinh tien tin nhan le

                                if (CapSoThanTaiCharged(User_ID, Request_ID, Service_ID) == "1")
                                {
                                    #region Charged THANHCONG

                                    messageReturn = "Ban da lua chon cap so " + subcode + ". May man dang cho ban. TT <cặp số> de lua chon cap so tiep theo nang cao co hoi trung thuong.";
                                    SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                                    ThanTai_MT_Controller.Insert_CapSo(User_ID, subcode);
                                    #endregion
                                }
                                else if (CapSoThanTaiCharged(User_ID, Request_ID, Service_ID) == "Result:12,Detail:Not enough money.")
                                {
                                    #region Charged THATBAI ==> GUI ALERT THONG BAO NAP TIEN

                                    messageReturn = "Thue bao khong du tien. Vui long nap tien de tiep tuc choi !";
                                    SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);    //SEND MT THONGBAO

                                    #endregion
                                }
                                else
                                {
                                    #region Charged THATBAI ==> LOI SYSTEM

                                    messageReturn = "He thong dang ban. Vui long tro lai sau it phut !";
                                    SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);    //SEND MT THONGBAO

                                    #endregion
                                }

                                #endregion
                            }
                        }
                        else
                        {
                            messageReturn = "Tin nhan sai cu phap. HT: 19001255";
                            SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                        }

                    //}
                }

            }
            else if (Command_Code.ToUpper() == "DIEM")
            {
                DataTable dtDiem = ThanTai_MT_Controller.SumPointByDay(User_ID);
                int diem = 0;
                int stt = 0;
                DateTime nowTime = DateTime.Now;
                if (nowTime.Hour >= 19 && nowTime.Hour <= 24)
                {
                    if (dtDiem != null && dtDiem.Rows.Count > 0)
                    {
                        diem = ConvertUtility.ToInt32(dtDiem.Rows[0]["Point"].ToString());
                        stt = ConvertUtility.ToInt32(dtDiem.Rows[0]["Top"].ToString());
                        if (diem > 0)
                        {
                            messageReturn = "Ban dang co " + diem + " diem va nam trong top " + stt + " diem cao nhat. Co gang len nao. De xem so diem tong dang co, soan TONG gui 949";
                            SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                        }
                        else
                        {
                            messageReturn = "Ban chua danh duoc diem nao. Co gang len nao.";
                            SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                        }
                    }
                }
                
                else
                {
                    messageReturn = "Ket qua xo so se duoc cap nhat vao toi nay. Ban hay theo doi de biet so diem cua minh. Soan tin DK XS MB de theo doi ket qua xo so";
                    SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                }



            }
            else if (Command_Code.ToUpper() == "TONG")
            {
                DataTable dtDiem = ThanTai_MT_Controller.SumPoint(User_ID);
                int tong = 0;
                int stt = 0;
                 DateTime nowTime = DateTime.Now;
                
                if (dtDiem != null && dtDiem.Rows.Count > 0)
                {
                    tong = ConvertUtility.ToInt32(dtDiem.Rows[0]["Tong"].ToString());
                    stt = ConvertUtility.ToInt32(dtDiem.Rows[0]["Top"].ToString());
                    if (tong > 0)
                    {
                        messageReturn = "Ban dang co " + tong + " diem va nam trong top " + stt + " diem cao nhat. Co gang len de gianh 30 trieu dong.";
                        SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                    }
                    else
                    {
                        messageReturn = "Ban chua danh duoc diem nao. Co gang len de gianh 30 trieu dong.";
                        SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
                    }
                }
               
            }
            else
            {
                messageReturn = "Tin nhan sai cu phap. HT: 19001255";
                SendMtThanTai(User_ID, messageReturn, Service_ID, Command_Code, Request_ID);
            }
        }
        catch (Exception ex)
        {
            log.Debug("--------------- CAP SO THAN TAI ----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }
        return responseValue;
    }
    #endregion
    public void SendMtThanTai(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
    {
        var objSentMt = new ServiceProviderService();

        const int msgType = (int)Constant.MessageType.NoCharge;

        int result = objSentMt.sendMT(userId, mtMessage, serviceId, commandCode, msgType.ToString(), requestId, "1", "1", "0", "0");
        log.Debug("Send MT result : " + result);
        log.Debug("userId : " + userId);
        log.Debug("Noi dung MT : " + mtMessage);
        log.Debug("ServiceId : " + serviceId);
        log.Debug("commandCode : " + commandCode);
        log.Debug("requestId : " + requestId);

        var objMt = new ThanTai_MT_Info();
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

        objMt.PartnerID = "VMG";
        objMt.Operator = "vnmobile";

        ThanTai_MT_Controller.Insert_MT(objMt);
    }
    private static string GetTelco(string mobile)
    {
        string prenumber = mobile.Substring(0, 5);

        string[] dfsplit = AppEnv.GetSetting("sfone").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "sfone";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("vnmobile").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "vnmobile";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("gtel").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "gtel";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("viettel").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "viettel";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("vms").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "vms";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("gpc").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "gpc";
                }
            }
        }

        return "";
    }
    #region MyRegion
    public string CapSoThanTaiCharged(string userId, string Request_ID, string Service_ID)
    {
        var webServiceCharging3G = new WebServiceCharging3g();
        const string userName = "VMGViSport";
        const string userPass = "v@#port";
        const string cpId = "1930";
        const string price = "1000";
        const string content = "Charged CSTT";
        const string serviceName = "CSTT_DuDoan";
        string returnValue = webServiceCharging3G.PaymentVnmWithAccount(userId, price, content, serviceName, userName, userPass, cpId);

        if (returnValue == "1")//CHARGED THANHCONG
        {
            #region LOG DOANH THU

            //DataTable dtUsers = ViSport_S2_Registered_UsersController.GetSportGameHeroUserInfoActive(userId);
            //DataRow dr = dtUsers.Rows[0];

            var logInfo = new ThanTaiChargedUserLogInfo();
            logInfo.User_ID = userId;
            logInfo.Request_ID = Request_ID;
            logInfo.Service_ID = Service_ID;
            logInfo.Command_Code = "TT";
            logInfo.Registration_Channel = "SMS";
            logInfo.Status = 1;
            logInfo.Operator = "vnmobile";
            logInfo.Price = ConvertUtility.ToInt32(price);
            logInfo.Reason = "Succ";

            ViSport_S2_Registered_UsersController.ThanTai_ChargedUser_InsertLog(logInfo);

            #endregion
        }


        return returnValue;
    }
    #endregion

}
