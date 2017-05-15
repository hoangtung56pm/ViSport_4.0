using System;
using System.Data;
using System.Web.Services;
using SMSManager_API.Library.Utilities;

/// <summary>
/// Summary description for ZaloMoProcess
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ZaloMoProcess : System.Web.Services.WebService {

    public ZaloMoProcess () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    static readonly log4net.ILog Log = log4net.LogManager.GetLogger("ZaloMoProcess");

    [WebMethod]
    public string MoProcess(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        Command_Code = Command_Code.ToUpper();
        if (Command_Code == "ZALOAG")
        {
            Command_Code = "ZALO";
        }

        string responseValue = "1";
        //int returnValue = 0;

        Message = Message.ToUpper().Trim();

        string subcode = "";
        if (Message.Trim().Length > Command_Code.Trim().Length)
        {
            subcode = Message.ToUpper().Substring(Command_Code.Length).Replace(" ", "");
        }
        else
        {
            subcode = Message.Replace("CAUZALO", "").Replace("ZALO", "").Replace(" ", "");
            Command_Code = Command_Code.Replace(subcode, "").Replace(" ", "");
        }

        //return responseValue;

        Log.Debug(" ");
        Log.Debug(" ");
        Log.Debug("-------------------- Zalo Mo Process -------------------------");
        Log.Debug("User_ID: " + User_ID);
        Log.Debug("Service_ID: " + Service_ID);
        Log.Debug("Command_Code: " + Command_Code);
        Log.Debug("Message: " + Message);
        Log.Debug("Request_ID: " + Request_ID);
        Log.Debug(" ");
        Log.Debug(" ");

        #region LOG SMS MO

        var moInfo = new ZaloMoInfo();
        moInfo.User_ID = User_ID;
        moInfo.Request_ID = Request_ID;
        moInfo.Service_ID = Service_ID;
        moInfo.Command_Code = Command_Code;
        moInfo.Message = Message;
        moInfo.Operator = GetTelco(User_ID);
        ZaloController.ZaloMoInsert(moInfo);

        #endregion

        try
        {
            string reMessage;
            DataTable dtService = ZaloController.ZaloGetServiceInfo(subcode);
            if (dtService != null && dtService.Rows.Count > 0)
            {
                //int typeId = ConvertUtility.ToInt32(dtService.Rows[0]["Type_Id"]);
                int companyId = ConvertUtility.ToInt32(dtService.Rows[0]["CompanyId"]);
                string companyName = UnicodeUtility.UnicodeToKoDau(dtService.Rows[0]["Name"].ToString());

                const string zaloPartner = "ZALO";
                //string vmgPartner = "VMG";

                DateTime nowTime = DateTime.Now;

                string lotTime = DateTime.Now.Year + "-" + DateTime.Now.Month;
                string ngay = DateTime.Now.Day + "/" + DateTime.Now.Month;
                var expiredDate1Day = DateTime.Now.AddDays(1).ToString("dd/MM/yyyy");
                var expiredDate7Day = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
                var expiredDate30Day = DateTime.Now.AddDays(30).ToString("dd/MM/yyyy");

                if (Command_Code == "ZALO")
                {
                    #region KQ XS

                    if (Message == "ZALOMN" && ("8179,8279").Contains(Service_ID))
                    {
                        #region KQ XOSO 1 NGAY

                        if (nowTime.Hour < 12) //TRA LUON KQ NGAY HOM TRUOC
                        {
                            lotTime = lotTime + "-" + DateTime.Now.AddDays(-1).Day;
                            DataTable dtCon = ZaloController.ZaloGetLotteryContent(companyId, lotTime);
                            if (dtCon != null && dtCon.Rows.Count > 0)
                            {

                                #region TRA MT WELCOME

                                reMessage = "Cam on ban da su dung dich vu KQXS VIP " + companyName + " 1 ngay cua Zalo. Ket qua xo so " + companyName + " se duoc gui den ban ngay khi co ket qua tren tin nhan Zalo.";
                                SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                                #endregion

                                #region TRA ZMS WElCOME

                                reMessage = "Cảm ơn bạn đã đăng ký thành công KQXS VIP " + companyName + " 1 ngày. Gói cước sẽ hết hạn đến hết ngày " + expiredDate1Day + ". Kết quả xổ số mới nhất sẽ được gửi đến bạn ngay sau khi có đầy đủ các giải.";

                                #region CALL Zalo_API

                                int reVal1 = ApiZaloCallForSendZms(User_ID, reMessage);

                                #endregion

                                //SAVE LOG AFTER CALL API
                                SaveMtLog(User_ID, Service_ID, Command_Code, reMessage, Request_ID, zaloPartner, reVal1);

                                #endregion

                                #region TRA KQ QUA ZALO

                                foreach (DataRow dr in dtCon.Rows)
                                {
                                    reMessage = ZaloController.OptimizeContent(dr["lot_content"].ToString());

                                    #region CALL Zalo_API

                                    int reVal = ApiZaloCallForSendZms(User_ID, reMessage);
                                    //SAVE LOG AFTER CALL API
                                    SaveMtLog(User_ID, Service_ID, Command_Code, reMessage, Request_ID, zaloPartner, reVal);

                                    #endregion

                                }

                                #endregion

                            }
                        }
                        if (nowTime.Hour > 12)
                        {
                            #region KIEM TRA XEM KQ HNAY DA CO CHUA

                            lotTime = lotTime + "-" + DateTime.Now.Day;
                            DataTable dtCon = ZaloController.ZaloGetLotteryContent(companyId, lotTime);
                            if (dtCon != null && dtCon.Rows.Count > 0)
                            {
                                #region TRA MT WELCOME

                                reMessage = "Cam on ban da su dung dich vu KQXS cua Zalo. Ket qua xo so " + companyName + " moi nhat se duoc gui den ban tren tin nhan cua Zalo.";
                                SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                                #endregion

                                #region CO = TRA KQ LUON

                                foreach (DataRow dr in dtCon.Rows)
                                {
                                    reMessage = ZaloController.OptimizeContent(dr["lot_content"].ToString());

                                    #region CALL Zalo_API

                                    int reVal = ApiZaloCallForSendZms(User_ID, reMessage);

                                    #endregion

                                    //SAVE LOG AFTER CALL API
                                    SaveMtLog(User_ID, Service_ID, Command_Code, reMessage, Request_ID, zaloPartner, reVal);
                                }

                                #endregion
                            }
                            else
                            {
                                #region KHONG = DUA VAO BANG Waiting

                                reMessage = "Ket qua xo so " + companyName + " ngay " + ngay + " se duoc he thong gui den ban ngay khi co ket qua tren tin nhan ZALO. ";
                                //GUI MT THONGBAO He Thong Da ghi Nhan SMS
                                SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                                //DUA VAO BANG DOI
                                SaveLotteryDay(User_ID, Request_ID, Service_ID, Command_Code, subcode, companyId);
                                //END DUA VAO BANG DOI

                                #endregion
                            }

                            #endregion
                        }

                        #endregion
                    }
                    else if ( (Service_ID == "8279" && Message != "ZALOMB") || (Message == "ZALOMT" && Service_ID == "8179") )
                    {
                        #region 1. Nhận kết quả xổ số mới nhất theo MIỀN

                        if (nowTime.Hour < 12) //TRA LUON KQ NGAY HOM TRUOC
                        {
                            lotTime = lotTime + "-" + DateTime.Now.AddDays(-1).Day;
                            DataTable dtCon = ZaloController.ZaloGetLotteryContent(companyId, lotTime);
                            if (dtCon != null && dtCon.Rows.Count > 0)
                            {

                                #region TRA MT WELCOME

                                reMessage = "Cam on ban da su dung dich vu KQXS VIP " + companyName + " 1 ngay cua Zalo. Ket qua xo so " + companyName + " se duoc gui den ban ngay khi co ket qua tren tin nhan Zalo.";
                                SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                                #endregion

                                #region TRA ZMS WElCOME

                                reMessage = "Cảm ơn bạn đã đăng ký thành công KQXS VIP " + companyName + " 1 ngày. Gói cước sẽ hết hạn đến hết ngày " + expiredDate1Day + ". Kết quả xổ số mới nhất sẽ được gửi đến bạn ngay sau khi có đầy đủ các giải.";

                                #region CALL Zalo_API

                                int reVal1 = ApiZaloCallForSendZms(User_ID, reMessage);

                                #endregion

                                //SAVE LOG AFTER CALL API
                                SaveMtLog(User_ID, Service_ID, Command_Code, reMessage, Request_ID, zaloPartner, reVal1);

                                #endregion

                                #region TRA KQ QUA ZALO

                                foreach (DataRow dr in dtCon.Rows)
                                {
                                    reMessage = ZaloController.OptimizeContent(dr["lot_content"].ToString());

                                    #region CALL Zalo_API

                                    int reVal = ApiZaloCallForSendZms(User_ID, reMessage);
                                    //SAVE LOG AFTER CALL API
                                    SaveMtLog(User_ID, Service_ID, Command_Code, reMessage, Request_ID, zaloPartner, reVal);

                                    #endregion

                                }

                                #endregion

                            }
                        }
                        if (nowTime.Hour > 12)
                        {
                            #region KIEM TRA XEM KQ HNAY DA CO CHUA

                            lotTime = lotTime + "-" + DateTime.Now.Day;
                            DataTable dtCon = ZaloController.ZaloGetLotteryContent(companyId, lotTime);
                            if (dtCon != null && dtCon.Rows.Count > 0)
                            {
                                #region TRA MT WELCOME

                                reMessage = "Cam on ban da su dung dich vu KQXS cua Zalo. Ket qua xo so " + companyName + " moi nhat se duoc gui den ban tren tin nhan cua Zalo.";
                                SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                                #endregion

                                #region CO = TRA KQ LUON

                                foreach (DataRow dr in dtCon.Rows)
                                {
                                    reMessage = ZaloController.OptimizeContent(dr["lot_content"].ToString());

                                    #region CALL Zalo_API

                                    int reVal = ApiZaloCallForSendZms(User_ID, reMessage);

                                    #endregion

                                    //SAVE LOG AFTER CALL API
                                    SaveMtLog(User_ID, Service_ID, Command_Code, reMessage, Request_ID, zaloPartner, reVal);
                                }

                                #endregion
                            }
                            else
                            {
                                #region KHONG = DUA VAO BANG Waiting

                                reMessage = "Ket qua xo so " + companyName + " ngay " + ngay + " se duoc he thong gui den ban ngay khi co ket qua tren tin nhan ZALO. ";
                                //GUI MT THONGBAO He Thong Da ghi Nhan SMS
                                SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                                //DUA VAO BANG DOI
                                SaveLotteryDay(User_ID, Request_ID, Service_ID, Command_Code, subcode, companyId);
                                //END DUA VAO BANG DOI

                                #endregion
                            }

                            #endregion
                        }

                        #endregion
                    }
                    else if(Service_ID == "8179" || (Message == "ZALOMB" && Service_ID == "8279") )
                    {
                        #region 1. Nhận kết quả xổ số mới nhất theo TỈNH

                        #region TRA MT WELCOME

                        reMessage = "Cam on ban da su dung dich vu KQXS VIP " + companyName + " 1 ngay cua Zalo. Ket qua xo so " + companyName + " moi nhat se duoc gui den ban ngay khi co ket qua tren tin nhan cua Zalo. ";
                        SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                        #endregion

                        #region TRA ZMS WElCOME

                        reMessage = "Cảm ơn bạn đã đăng ký thành công KQXS VIP " + companyName + " 1 ngày. Gói cước sẽ hết hạn đến hết ngày " + expiredDate1Day + ". Kết quả xổ số mới nhất sẽ được gửi đến bạn ngay sau khi có đầy đủ các giải.";

                        #region CALL Zalo_API

                        int reVal1 = ApiZaloCallForSendZms(User_ID, reMessage);

                        #endregion

                        //SAVE LOG AFTER CALL API
                        SaveMtLog(User_ID, Service_ID, Command_Code, reMessage, Request_ID, zaloPartner, reVal1);

                        #endregion

                        lotTime = lotTime + "-" + DateTime.Now.Day;
                        DataTable dtCon = ZaloController.ZaloGetLotteryContentToDay(companyId, lotTime);//KIEM TRA KQXS HOMNAY DA CO CHUA ?

                        if(dtCon != null && dtCon.Rows.Count > 0)
                        {
                            #region TRA KQXS Ngay Hom Nay

                            reMessage = ZaloController.OptimizeContent(dtCon.Rows[0]["lot_content"].ToString());

                            #region CALL Zalo_API

                            int reVal = ApiZaloCallForSendZms(User_ID, reMessage);

                            #endregion

                            //SAVE LOG AFTER CALL API
                            SaveMtLog(User_ID, Service_ID, Command_Code, reMessage, Request_ID, zaloPartner, reVal);

                            #endregion
                        }
                        else
                        {
                            #region TRA KQXS Ngay Gan Nhat

                            lotTime = DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.AddDays(-1).Day;
                            DataTable dtConPre = ZaloController.ZaloGetLotteryContent(companyId, lotTime);
                            if(dtConPre != null && dtConPre.Rows.Count > 0)
                            {
                                reMessage = ZaloController.OptimizeContent(dtConPre.Rows[0]["lot_content"].ToString());

                                //SEND MT KQXS Ngay Gan Nhat cho KH
                                //SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                                #region CALL Zalo_API

                                int reVal = ApiZaloCallForSendZms(User_ID, reMessage);

                                #endregion

                                //SAVE LOG AFTER CALL API
                                SaveMtLog(User_ID, Service_ID, Command_Code, reMessage, Request_ID, zaloPartner, reVal);

                            }

                            #endregion

                            #region DUA VAO BANG Waiting ==> DE TRA KQXS Ngay HOMNAY

                            //reMessage = "Ket qua xo so " + companyName + " moi nhat se duoc he thong gui den ban ngay khi co ket qua tren tin nhan ZALO. ";
                            ////GUI MT THONGBAO He Thong Da ghi Nhan SMS
                            //SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                            //DUA VAO BANG DOI
                            SaveLotteryDay(User_ID, Request_ID, Service_ID, Command_Code, subcode, companyId);
                            //END DUA VAO BANG DOI

                            #endregion
                        }

                        #endregion
                    }
                    else if ( (Service_ID == "8379" && Message != "ZALOMT") || (Message == "ZALOMB" && Service_ID == "8479") )
                    {
                        #region 2. Nhận kết quả xổ số mới nhất theo miền

                            #region TRA MT WELCOME

                            reMessage = "Cam on ban da su dung dich vu KQXS VIP " + companyName + " 7 ngay cua Zalo. Ket qua xo so " + companyName + " se duoc gui den ban ngay khi co ket qua tren tin nhan Zalo";

                            SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);
                            
                            #endregion

                            #region TRA ZMS WElCOME

                            reMessage = "Cảm ơn bạn đã đăng ký thành công KQXS VIP " + companyName + " 7 ngày. Gói cước sẽ hết hạn đến hết ngày " + expiredDate7Day + ". Kết quả xổ số mới nhất sẽ được gửi đến bạn ngay sau khi có đầy đủ các giải.";

                            #region CALL Zalo_API

                            int reVal1 = ApiZaloCallForSendZms(User_ID, reMessage);

                            #endregion

                            //SAVE LOG AFTER CALL API
                            SaveMtLog(User_ID, Service_ID, Command_Code, reMessage, Request_ID, zaloPartner, reVal1);

                            #endregion

                            #region KIEM TRA XEM DA CO KQXS HOMNAY CHUA

                            lotTime = DateTime.Now.Year + "-" + DateTime.Now.Month;
                            lotTime = lotTime + "-" + DateTime.Now.Day;
                            DataTable dtCon = ZaloController.ZaloGetLotteryContent(companyId, lotTime);//KIEM TRA KQXS HOMNAY DA CO CHUA ?

                            if(dtCon != null && dtCon.Rows.Count > 0)//ĐÃ CÓ KẾT QUẢ ==> TRẢ LUÔN
                            {
                                #region TRA KQ HOMNAY

                                foreach (DataRow dr in dtCon.Rows)
                                {
                                    reMessage = ZaloController.OptimizeContent(dr["lot_content"].ToString());
                                    int reVal = ApiZaloCallForSendZms(User_ID, reMessage);
                                    //SAVE LOG AFTER CALL API
                                    SaveMtLog(User_ID, Service_ID, Command_Code, reMessage, Request_ID, zaloPartner, reVal);
                                }

                                #endregion
                            }
                            else//CHƯA CÓ KẾT QUẢ ==> TRẢ TIN THÔNG BÁO
                            {
                                #region TRA MT THONGBAO qua ZMS

                                //reMessage = "Kết quả xổ số mới nhất sẽ được gửi đến bạn ngay sau khi có đầy đủ các giải. Điện thoại hỗ trợ - HOTLINE: 19001255.";

                                
                                reMessage = "Cảm ơn bạn đã đăng ký thành công kết quả xổ số " + subcode + ". Gói cước sẽ hết hạn đến hết ngày " + expiredDate7Day + ". Kết quả xổ số mới nhất sẽ được gửi đến bạn ngay sau khi có đầy đủ các giải.";
                                int reVal = ApiZaloCallForSendZmsAlert(User_ID, reMessage);

                                //SAVE LOG AFTER CALL API
                                SaveMtLog(User_ID, Service_ID, Command_Code, reMessage, Request_ID, zaloPartner, reVal);

                                #endregion

                                #region LUU VAO BANG DOI

                                SaveLotteryDay(User_ID, Request_ID, Service_ID, Command_Code, subcode, companyId);

                                #endregion
                            }
                            
                            #endregion

                        #endregion
                    }
                    else if (Service_ID == "8779" || (Message == "ZALOMB" && ("8579,8679").Contains(Service_ID) ) )
                    {
                        #region 3. Đăng ký nhận kết quả xổ số nhiều ngày

                        #region TRA MT WELCOME

                        reMessage = "Cam on ban da su dung dich vu KQXS VIP " + companyName + " 30 ngay cua Zalo. Ket qua xo so " + companyName + " se duoc gui den ban ngay khi co ket qua tren tin nhan Zalo.";
                        SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                        #endregion

                        #region TRA ZMS WElCOME

                        reMessage = "Cảm ơn bạn đã đăng ký thành công KQXS VIP " + companyName + " 30 ngày. Gói cước sẽ hết hạn đến hết ngày " + expiredDate30Day + ". Kết quả xổ số mới nhất sẽ được gửi đến bạn ngay sau khi có đầy đủ các giải.";

                        #region CALL Zalo_API

                        int reVal1 = ApiZaloCallForSendZms(User_ID, reMessage);

                        #endregion

                        //SAVE LOG AFTER CALL API
                        SaveMtLog(User_ID, Service_ID, Command_Code, reMessage, Request_ID, zaloPartner, reVal1);

                        #endregion

                        #region LUU VAO BANG DOI

                        SaveLotteryDay(User_ID, Request_ID, Service_ID, Command_Code, subcode, companyId);

                        #endregion

                        #endregion
                    }
                    else if (Service_ID == "8579" || (Message == "ZALOMT" && ("8379,8479,8679").Contains(Service_ID) ) || (Message == "ZALOMN" && ("8479,8679").Contains(Service_ID) ) )
                    {
                        #region 4. Tường thuật trực tiếp kết quả xổ số ==> Đổi Thành KQXS 30 Ngày

                        #region TRA MT WELCOME

                        reMessage = "Cam on ban da su dung dich vu KQXS VIP " + companyName + " 30 ngay cua Zalo. Ket qua xo so " + companyName + " se duoc gui den ban ngay khi co ket qua tren tin nhan Zalo.";
                        SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                        #endregion

                        #region TRA ZMS WElCOME

                        reMessage = "Cảm ơn bạn đã đăng ký thành công KQXS VIP " + companyName + " 30 ngày. Gói cước sẽ hết hạn đến hết ngày " + expiredDate30Day + ". Kết quả xổ số mới nhất sẽ được gửi đến bạn ngay sau khi có đầy đủ các giải.";

                        #region CALL Zalo_API

                        int reVal1 = ApiZaloCallForSendZms(User_ID, reMessage);

                        #endregion

                        //SAVE LOG AFTER CALL API
                        SaveMtLog(User_ID, Service_ID, Command_Code, reMessage, Request_ID, zaloPartner, reVal1);

                        #endregion

                        #region LUU VAO BANG DOI

                        SaveLotteryDay(User_ID, Request_ID, Service_ID, Command_Code, subcode, companyId);

                        #endregion

                        #endregion
                    }

                    #endregion
                }
                else if (Command_Code == "CAUZALO")
                {
                    #region SOI CAU

                    #region TRA MT WELCOME

                    reMessage = "Cam on ban da su dung dich vu Cap so may man cua Zalo. Cap so may man " + companyName + " ky toi se duoc gui den ban tren tin nhan cua Zalo. ";
                    //GUI SMS THONGBAO He Thong Da Ghi Nhan SMS Cua KH
                    SendMt(User_ID, Service_ID, Command_Code, reMessage, Request_ID);

                    #endregion

                    #region CALL ZALO API

                    DataTable dtCon = ZaloController.ZaloGetSoiCauContent(companyId);
                    if(dtCon != null && dtCon.Rows.Count > 0)
                    {
                        foreach(DataRow dr in dtCon.Rows)
                        {
                            reMessage = dr["Content"].ToString();
                            int reVal = ApiZaloCallForSendZms(User_ID,reMessage);
                            //SAVE LOG AFTER CALL API
                            SaveMtLog(User_ID, Service_ID, Command_Code, reMessage, Request_ID, zaloPartner,reVal);
                        }
                    }

                    #endregion

                    #endregion
                }
            }
            else
            {
                reMessage = "Tin nhan sai cu phap !";
                SendMt(User_ID,Service_ID,Command_Code,reMessage,Request_ID);
            }

        }
        catch (Exception ex)
        {
            Log.Debug(" ");
            Log.Debug(" ");
            Log.Debug("--------------- Zalo WS Process MO Error ----------------------");
            Log.Debug("Zalo Exception : " + ex.Message);
            Log.Debug(" ");
            Log.Debug(" ");
            throw;
        }

        return responseValue;
    }

    [WebMethod]
    public string SendMessageViaZaloApi(string userId,string message)
    {

        bool isSystaxCorrect = false;

        try
        {
            string zmsContent;
            int companyId = 0;
            message = message.Trim().ToUpper();
            //message = message.Replace(" ","");
            //message = message.Replace("  ","");


            if (message == "#THONGKEMB" || message == "#THONGKEMT" || message == "#THONGKEMN")
            {
                #region THONGKE KQXS FREE : #THONGKEMB, #THONGKEMN, #THONGKEMT

                if (message == "#THONGKEMB")
                {
                    companyId = 1;
                }
                else if (message == "#THONGKEMT")
                {
                    companyId = 22;
                }
                else if (message == "#THONGKEMN")
                {
                    companyId = 23;
                }

                DataTable dtCon = ZaloController.ZaloGetSoiCauContent(companyId);
                if (dtCon != null && dtCon.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCon.Rows)
                    {
                        zmsContent = dr["Content"].ToString();
                        ApiZaloCallForSendZms(userId, zmsContent);
                    }
                }

                #endregion
            }
            else if (message == "#HUONGDAN")
            {
                SendIntroduceAlert(userId);
            }
            else
            {

                #region Check now Time

                DateTime nowTime = DateTime.Now;

                bool isTrue = true;

                //bool isTrue = false;

                //if (nowTime.Hour <= 15)
                //{
                //    isTrue = true;
                //}
                //else if (nowTime.Hour == 16 && nowTime.Minute <= 30)
                //{
                //    isTrue = true;
                //}
                //else if (nowTime.Hour > 15 && nowTime.Hour < 18)
                //{
                //    zmsContent = "Để có kết quả xổ số mới nhất. Vui lòng sử dụng dịch vụ tính phí để nhận ngay !";
                //    ZaloController.ApiZaloCallForSendZmsAlert(userId, zmsContent);
                //}

                #endregion

                if(isTrue)
                {
                    #region TRA CUU KQXS FREE : #MN, #MB, #MT

                    message = message.Replace("#", "");
                    string[] arr = message.Split(' ');
                    //#hcm 2/8

                    if (arr.Length > 1)
                    {
                        #region TRA CUU KQXS Theo Ngay

                        string code = arr[0];

                        DataTable dtService = ZaloController.ZaloGetServiceInfo(code);
                        if (dtService != null && dtService.Rows.Count > 0)
                        {
                            string[] time = arr[1].Split('/');
                            string day = DateTime.Now.Year + "-" + time[1] + "-" + time[0];
                            companyId = ConvertUtility.ToInt32(dtService.Rows[0]["CompanyId"]);
                            string name = dtService.Rows[0]["Name"].ToString();

                            if (companyId == 1 || companyId == 22 || companyId == 23)
                            {
                                #region KQXS Theo MIEN : MB,MT,MN

                                DataTable dtCon = ZaloController.ZaloGetLotteryContent(companyId, day);//TRUY VAN KQXS THEO MIEN ?
                                foreach (DataRow dr in dtCon.Rows)
                                {
                                    zmsContent = ZaloController.OptimizeContent(dr["lot_content"].ToString());
                                    ApiZaloCallForSendZms(userId, zmsContent);
                                }

                                #endregion
                            }
                            else
                            {
                                #region KQXS Theo Tung TINH

                                DataTable dtCon = ZaloController.ZaloGetLotteryContentToDay(companyId, day);//TRUY VAN KQXS TINH THEO NGAY ?
                                if (dtCon != null && dtCon.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dtCon.Rows)
                                    {
                                        zmsContent = ZaloController.OptimizeContent(dr["lot_content"].ToString());
                                        ApiZaloCallForSendZms(userId, zmsContent);
                                    }
                                }
                                else
                                {
                                    zmsContent = name;
                                    ApiZaloCallForSendZms(userId, zmsContent);
                                }

                                #endregion
                            }
                        }
                        else
                        {
                           //SAI CU PHAP
                            isSystaxCorrect = true;
                        }

                        #endregion
                    }
                    else
                    {
                        #region TRA CUU KQXS Moi Nhat

                        DataTable dtService = ZaloController.ZaloGetServiceInfo(message);
                        if (dtService != null && dtService.Rows.Count > 0)
                        {
                            companyId = ConvertUtility.ToInt32(dtService.Rows[0]["CompanyId"]);
                            string lotTime = DateTime.Now.Year + "-" + DateTime.Now.Month;
                            lotTime = lotTime + "-" + DateTime.Now.Day;

                            DataTable dtCon = ZaloController.ZaloGetLotteryContent(companyId, lotTime);//KIEM TRA KQXS MOI NHAT ?

                            if (dtCon != null && dtCon.Rows.Count > 0)
                            {
                                #region KQXS Ngay HomNay

                                foreach (DataRow dr in dtCon.Rows)
                                {
                                    zmsContent = ZaloController.OptimizeContent(dr["lot_content"].ToString());
                                    ApiZaloCallForSendZms(userId, zmsContent);
                                }

                                #endregion
                            }
                            else
                            {
                                #region TRA KQXS Ngay Gan Nhat

                                DateTime preTime = DateTime.Now.AddDays(-1);

                                lotTime = preTime.Year + "-" + preTime.Month + "-" + preTime.Day;
                                DataTable dtConPre = ZaloController.ZaloGetLotteryContent(companyId, lotTime);
                                if (dtConPre != null && dtConPre.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dtConPre.Rows)
                                    {
                                        zmsContent = ZaloController.OptimizeContent(dr["lot_content"].ToString());
                                        ApiZaloCallForSendZms(userId, zmsContent);
                                    }
                                }

                                #endregion
                            }
                        }
                        else
                        {
                            #region KQXS TAT CA CAC DAI THEO NGAY

                            string[] time = arr[0].Split('/');
                            string day = DateTime.Now.Year + "-" + time[1] + "-" + time[0];
                            DataTable dtAll = ZaloController.ZaloGetLotteryContentAllByDay(day);
                            if(dtAll.Rows.Count > 0)
                            {
                                foreach (DataRow dt in dtAll.Rows)
                                {
                                    zmsContent = ZaloController.OptimizeContent(dt["lot_content"].ToString());
                                    ApiZaloCallForSendZms(userId, zmsContent);
                                }
                            }
                            else
                            {
                                isSystaxCorrect = true;
                            }
                            
                            #endregion
                        }

                        #endregion
                    }

                    #endregion
                }

            }
        }
        catch (Exception ex)
        {
            Log.Debug(" ");
            Log.Debug(" ");
            Log.Debug("-------------------- Zalo Events Exception -------------------------");
            Log.Debug("Exception: " + ex);
            Log.Debug(" ");
            Log.Debug(" ");
            isSystaxCorrect = true;
        }

        #region SAI CU PHAP
        
        if(isSystaxCorrect)
        {
            SendIntroduceAlert(userId);
        }

        #endregion

        return "1";
    }

    #region Methods

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

    private static void SaveMtLog(string User_ID, string Service_ID, string Command_Code, string message, string Request_ID,string Partner,int type)
    {
        var objMt = new ZaloMtInfo();
        objMt.User_ID = User_ID;
        objMt.Message = message;
        objMt.Service_ID = Service_ID;
        objMt.Command_Code = Command_Code;
        objMt.Message_Type = 1;
        objMt.Request_ID = Request_ID;
        objMt.Total_Message = 1;
        objMt.Message_Index = 0;
        objMt.IsMore = 0;
        objMt.Content_Type = 0;
        objMt.ServiceType = 0;
        objMt.ResponseTime = DateTime.Now;
        objMt.IsLock = 0;
        objMt.PartnerId = Partner;
        objMt.Operator = GetTelco(User_ID);
        objMt.Type = type;

        ZaloController.ZaloMtInsert(objMt);
    }

    private static void SendMt(string userId,string serviceId,string commandCode,string message,string requestId)
    {
        message = message.Replace("Đặc biệt", "Dac biet");
        message = message.Replace("?","");

        //message = message.Replace("", "DTHT-HOTLINE: 19001255");
        var mtInfo = new MTInfo();
        var random = new Random();
        mtInfo.User_ID = userId;
        mtInfo.Service_ID = serviceId;
        mtInfo.Command_Code = commandCode;
        mtInfo.Message_Type = 0;
        mtInfo.Request_ID = random.Next(100000000, 999999999).ToString();
        mtInfo.Total_Message = 1;
        mtInfo.Message_Index = 0;
        mtInfo.IsMore = 0;
        mtInfo.Content_Type = 0;
        mtInfo.Message = message;

        SaveMtLog(userId, serviceId, commandCode, message, requestId,"VMG",0);

        ZaloController.SmsMtInsertNew(mtInfo);
    }

    private static void SaveLotteryDay(string userId,string requestId,string serviceId,string commandCode,string subCode,int companyId)
    {
        //DUA VAO BANG DOI
        var item = new ZaloLotteryDay();
        item.UserId = userId;
        item.RequestId = requestId;
        item.ServiceId = serviceId;
        item.CommanCode = commandCode;
        item.Operator = GetTelco(userId);
        item.ServiceCode = subCode;
        item.CompanyId = companyId;
        ZaloController.ZaloLotteryDayInsert(item);
        //END DUA VAO BANG DOI
    }

    private static int ApiZaloCallForSendZms(string userId,string message)
    {
        //message = message + " \r\n DTHT - HOTLINE: 19001255.";
        int reValue = ZaloController.ApiZaloCallForSendZms(userId, message);
        return reValue;
    }

    private static int ApiZaloCallForSendZmsAlert(string userId, string message)
    {
        int reValue = ZaloController.ApiZaloCallForSendZmsAlert(userId, message);
        return reValue;
    }

    private void SendIntroduceAlert(string userId)
    {
        string zmsContent = "Cú pháp tra cứu Kết Quả Xổ Số như sau:\r\n" +
                               "- Cú pháp tra cứu kết quả xố số mới nhất theo MIỀN: " +
                               "\r\n miền Nam gửi #MN " +
                               "\r\n miền Bắc gửi #MB " +
                               "\r\n miền Trung gửi #MT";
        ZaloController.ApiZaloCallForSendZmsAlert(userId, zmsContent);


        zmsContent = "- Tra cứu kết quả xố số theo NGÀY, gửi tin nhắn với cú pháp: " +
                     " \r\n #NGÀY/THÁNG " +
                     "\r\n Ví dụ: để nhận kết quả xổ số ngày 31/12, gửi tin nhắn #31/12";
        ZaloController.ApiZaloCallForSendZmsAlert(userId, zmsContent);


        zmsContent = "- Tra cứu kết quả xổ số mới nhất theo TỈNH, gửi tin nhắn với cú pháp: " +
                     "\r\n #TÊNTỈNH " +
                     "\r\n Ví dụ: để tra cứu kết quả xổ số Cần Thơ mới nhất, gửi tin nhắn #CANTHO";
        ZaloController.ApiZaloCallForSendZmsAlert(userId, zmsContent);


        zmsContent = "- Tra cứu kết quả xổ số theo tỉnh trong ngày nhất định: " +
                     "\r\n #TÊNTỈNH NGÀY/THÁNG " +
                     "\r\n Ví dụ: để nhận kết quả xổ số ngày 31/12 của tỉnh Cần Thơ, gửi tin nhắn #CANTHO 31/12";
        ZaloController.ApiZaloCallForSendZmsAlert(userId, zmsContent);
    }

    #endregion


    #region DRAFT
    
    //try
    //        {
    //            string pageId = "2156820476252246890";
    //            string message = "vmg message";
    //            string smsMsg = "vmg - smsMsg";
    //            string secretKey = "LJ80A443RnVr3T8Ik18K";
    //            string pathPhoto = "";
    //            string pathVoice = "";
    //            long toUid = 84976245979;
    //            //long toUid = 84983042686;
    //            //long toUid = 84902220502;
    //            string templateid = "4069c383ffc616984fd7";
    //            ZaloServiceFactory factory = new ZaloServiceFactory(pageId, secretKey);

    //            //String photoId = factory.getZaloUploadService().uploadImage(pathPhoto);
    //            //String voiceId = factory.getZaloUploadService().uploadVoice(pathVoice);

    //            ZaloMessageService messageService = factory.getZaloMessageService();
    //            //ZaloPageResult objResult = messageService.sendTextMessage(toUid, message, message, true);
    //            Dictionary<string, string> data =
    //    new Dictionary<string, string>();

    //            data.Add("pageName", "Vmgmedia");
    //            ZaloPageResult objResult = messageService.sendTemplateTextMessageByPhoneNum(toUid, templateid, data, "fsfsfd", true);



    //            //int err1 = messageService.sendTextMessage(toUid, message, message, true);
    //            //if (err1 >= 0)
    //            //{
    //            //    Console.WriteLine("Pass send text message by page >>>>>>");
    //            //}

    //            //int err2 = messageService.sendImageMessage(toUid, photoId, pathPhoto, pathVoice, true);
    //            //if (err2 >= 0)
    //            //{
    //            //    Console.WriteLine("Pass send image message by page >>>>>>");
    //            //}

    //            string id = "b8e9d66f75aa87f4debb";


    //        }
    //        catch (ZaloSdkException ex)
    //        {
    //            Response.Write("Err = " + ex.getErrorCode());
    //            Response.Write("Mes = " + ex.getMessage());
    //        }
    //        catch (Exception ex)
    //        {
    //            Response.Write(ex.StackTrace);
    //        }

    #endregion

}
