#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\JobsSubMo949.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "101270029B1C42AFFFAF162428D4BD95E00E4272"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\JobsSubMo949.cs"
using System;
using System.Data;
using System.Web.Services;
using ChargingGateway;
using SentMT;
using SMSManager_API.Library.Utilities;
using WS_Music.Library;

/// <summary>
/// Summary description for JobsSubMo949
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class JobsSubMo949 : System.Web.Services.WebService, IJobExecutorSoap
{

    public JobsSubMo949()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog log = log4net.LogManager.GetLogger(typeof(JobsSubMo949));

    [WebMethod]
    public int Execute(int jobID)
    {
        //var webServiceCharging3G = new WebServiceCharging3g();
        //string userName = "VMGWAP3G";
        //string userPass = "vmg@#3g";
        //string cpId = "1928";
        string price = "0";

        DataTable dtUser = VoteRegisterController.Mo949GetUserForReCharged();
        if (dtUser != null && dtUser.Rows.Count > 0)
        {
            string userId;
            string commandCode;
            string requestId;
            string serviceId;

            string msg;

            foreach (DataRow dr in dtUser.Rows)
            {
                userId = dr["User_ID"].ToString();
                commandCode = dr["Command_Code"].ToString().ToUpper();
                requestId = dr["Request_ID"].ToString();
                serviceId = dr["Service_ID"].ToString();

                if (commandCode == "GAMEHOT" || commandCode == "NCHAY")
                {
                    price = "10000";
                }
                else if (commandCode == "VIDEOHAY")
                {
                    price = "2000";
                }
                else if (commandCode == "TRUYENHOT")
                {
                    price = "5000";
                }
                string msgReturn = PaymentVnmWapChargingOptimize(price, userId, commandCode);
                string[] msgResult = msgReturn.Split('|');
                msg = msgResult[0].Trim();
                price = msgResult[1].Trim();

                #region Log Doanh Thu

                var e = new VoteChargedUserLogInfo();
                e.ID = ConvertUtility.ToInt32(dr["ID"].ToString());
                e.User_ID = userId;
                e.Request_ID = requestId;
                e.Service_ID = serviceId;
                e.Command_Code = commandCode;
                e.Service_Type = 0;

                e.RegisteredTime = DateTime.Now;
                e.Registration_Channel = "SMS";
                e.Operator = dr["Operator"].ToString();

                e.Reason = msg == "1" ? "Succ" : msg;
                e.Price = ConvertUtility.ToInt32(price);

                VoteRegisterController.Mo949ChargedUserLogInsertForSub(e);

                #endregion

                if (msg == "1")
                {
                    string url = string.Empty;
                    string messageContent;

                    if (commandCode == "GAMEHOT")
                    {
                        #region GAME

                        DataTable dtGame = VoteRegisterController.Mo949GetRandomGame();
                        url = "";
                        if (dtGame != null && dtGame.Rows.Count > 0)
                        {
                            try
                            {
                                var urlservice = new VMGGame.MOReceiver();
                                url = urlservice.VMG_ReturnUrlForGame(ConvertUtility.ToString(dtGame.Rows[0]["GID"]), 0, userId, ConvertUtility.ToInt32(dtGame.Rows[0]["Partner_ID"]), "XZONE", "WAP", "vnmobile", "WAP.XZONE.VN", "", "");
                                int indexofhttp = url.IndexOf("http://");
                                if (indexofhttp == -1)
                                {
                                    url = "http://" + url;
                                }
                                else
                                {
                                    url = url.Substring(indexofhttp);
                                }
                            }
                            catch (Exception ex) { url = ""; }
                        }

                        messageContent = "Ban da mua GAME thanh cong. Click vao link sau de tai ve may " + url;
                        SendMtMo949(userId, messageContent, serviceId, commandCode, requestId);

                        #endregion
                    }
                    else if (commandCode == "NCHAY")
                    {
                        #region MUSIC

                        DataTable dtMusic = VoteRegisterController.Mo949GetRandomMusic();
                        if (dtMusic != null && dtMusic.Rows.Count > 0)
                        {
                            url = GetVnmDownloadItem(GetTelco(userId), "22", dtMusic.Rows[0]["W_MItemID"].ToString(), AppEnv.MD5Encrypt(dtMusic.Rows[0]["W_MItemID"].ToString()));
                        }

                        messageContent = "Ban da mua Nhac Chuong thanh cong. Click vao link sau de tai ve may " + url;
                        SendMtMo949(userId, messageContent, serviceId, commandCode, requestId);

                        #endregion
                    }
                    else if (commandCode == "VIDEOHAY")
                    {
                        #region VIDEO

                        DataTable dtVideo = VoteRegisterController.Mo949GetRandomVideo();
                        if (dtVideo != null && dtVideo.Rows.Count > 0)
                        {
                            url = GetDownloadItem(GetTelco(userId), "5", dtVideo.Rows[0]["W_VItemID"].ToString(), AppEnv.MD5Encrypt(dtVideo.Rows[0]["W_VItemID"].ToString()));
                        }

                        messageContent = "Ban da mua Video hot thanh cong. Click vao link sau de tai ve may " + url;
                        SendMtMo949(userId, messageContent, serviceId, commandCode, requestId);

                        #endregion
                    }
                    else if (commandCode == "TRUYENHOT")
                    {
                        #region TRUYEN HOT

                        string key = DateTime.Now.ToString("yyyyMMdd");
                        string en = AppEnv.MD5Encrypt(key);
                        DataTable dtTruyen = VoteRegisterController.Mo949GetRandomVideo();
                        if (dtTruyen != null && dtTruyen.Rows.Count > 0)
                        {
                            url = "http://wap.vietnamobile.com.vn/thugian/truyenmoi.aspx?k=" + en;
                        }

                        messageContent = "Ban da mua Truyen Hot thanh cong. Click vao link sau de doc truyen " + url;
                        SendMtMo949(userId, messageContent, serviceId, commandCode, requestId);

                        #endregion
                    }
                }
            }
        }

        //{"List":[{"Type":"text","Content":"noi dung text 1"},{"Type":"wappush","Content":"http://xzone.vn"}]}

        return 1;
    }

    public string PaymentVnmWapChargingOptimize(string price, string userId, string commandCode)
    {
        const string notEnoughMoney = "Result:12,Detail:Not enough money.";
        commandCode = commandCode.ToUpper();
        var webServiceCharging3G = new WebServiceCharging3g();

        string msgReturn = "";

        if (AppEnv.GetSetting("Mo949Test") == "1")
        {
            price = "500";
            msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
        }
        else
        {
            if (price == "10000" && commandCode == "GAMEHOT")
            {
                msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                if (msgReturn == notEnoughMoney)
                {
                    price = "5000";
                    msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                }
            }
            else if (price == "10000" && commandCode == "NCHAY")
            {
                msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                if (msgReturn == notEnoughMoney)
                {
                    price = "5000";
                    msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                    if (msgReturn == notEnoughMoney)
                    {
                        price = "3000";
                        msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                        if (msgReturn == notEnoughMoney)
                        {
                            price = "2000";
                            msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                        }
                    }
                }
            }
            else if (price == "2000" && commandCode == "VIDEOHAY")
            {
                msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                if (msgReturn == notEnoughMoney)
                {
                    price = "1000";
                    msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                }
            }
            else if (price == "5000" && commandCode == "TRUYENHOT")
            {
                msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                if (msgReturn == notEnoughMoney)
                {
                    price = "3000";
                    msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                    if (msgReturn == notEnoughMoney)
                    {
                        price = "2000";
                        msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                        if (msgReturn == notEnoughMoney)
                        {
                            price = "1000";
                            msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, "Mo949_Charging");
                        }
                    }
                }
            }
        }


        return msgReturn + "|" + price;
    }

    public void SendMtMo949(string userId, string mtMessage, string serviceId, string commandCode, string requestId)
    {
        var objSentMt = new ServiceProviderService();

        const int msgType = (int)Constant.MessageType.NoCharge;

        if (AppEnv.GetSetting("TestFlag") == "0")
        {
            int result = objSentMt.sendMT(userId, mtMessage, serviceId, commandCode, msgType.ToString(), requestId, "1", "1", "0", "0");
            log.Debug(" ");
            log.Debug(" ");
            log.Debug("************ Mo 949 Log reCharged ************************");
            log.Debug("Send MT result : " + result);
            log.Debug("userId : " + userId);
            log.Debug("Noi dung MT : " + mtMessage);
            log.Debug("ServiceId : " + serviceId);
            log.Debug("commandCode : " + commandCode);
            log.Debug("requestId : " + requestId);
            log.Debug(" ");
            log.Debug(" ");
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
        objMt.Operator = GetTelco(userId);

        ViSport_S2_SMS_MTController.InsertMo949Mt(objMt);
    }

    public string GetVnmDownloadItem(string telco, string itemtype, string itemid, string encode)
    {
        //itemtype = 1 : hinh nen
        //itemtype = 2 : Nhac chuong
        //itemtype = 3: game
        //itemtype = 4: app
        //itemtype = 5: video
        //itemtype = 6: y kien chuyen gia
        //itemtype = 7: tip
        //itemtype = 8: ket qua cho
        //itemtype = 9: ket qua xo so
        //itemtype = 10: soi cau
        //itemtype = 11: ket qua xoso cho
        //itemtype = 12: ket qua xoso 20 ngay lien tiep
        //itemtype = 13: truyen cuoi
        return AppEnv.GetSetting("vnmdownload") + "?telco=" + telco + "&type=" + itemtype + "&id=" + itemid + "&code=" + encode;
    }

    public static string GetDownloadItem(string telco, string itemtype, string itemid, string encode)
    {
        //itemtype = 1 : hinh nen
        //itemtype = 2 : Nhac chuong
        //itemtype = 3: game
        return AppEnv.GetSetting("download") + "?telco=" + telco + "&type=" + itemtype + "&id=" + itemid + "&code=" + encode;
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

}


#line default
#line hidden
