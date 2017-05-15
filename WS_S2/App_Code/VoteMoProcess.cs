using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using ChargingGateway;
using SentMT;
using SMSManager_API.Library.Utilities;

/// <summary>
/// Summary description for VoteMoProcess
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class VoteMoProcess : System.Web.Services.WebService {

    public VoteMoProcess () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog log = log4net.LogManager.GetLogger("File");

    #region Web Service

    //[WebMethod]
    //public string Ws8179MoProcess(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    //{
    //    return ExcecuteRequest8179Mo(User_ID, Service_ID, Command_Code, Message, Request_ID);
    //}

    [WebMethod]
    public string Ws8279MoProcess(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        string telco = GetTelco(User_ID);

        if (telco == "vnmobile")
        {
            return ExcecuteRequest8279MoVnm(User_ID, Service_ID, Command_Code, Message, Request_ID);
        }

        return ExcecuteRequest8279Mo(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }

    [WebMethod]
    public string Ws8279GirlSecretMoProcess(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        string telco = GetTelco(User_ID);

        if (telco == "vnmobile")
        {
            return ExcecuteRequest8279GirlSecretMoVnm(User_ID, Service_ID, Command_Code, Message, Request_ID);
        }

        return ExcecuteRequest8279GirlSecretMoOtherTelco(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }

    [WebMethod]
    public string WsVoteNew8579MoProcess(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        string telco = GetTelco(User_ID);

        if (telco == "vnmobile")
        {
            return ExcecuteRequestVoteNew8579MoVnm(User_ID, Service_ID, Command_Code, Message, Request_ID);
        }

        return ExcecuteRequestVoteNew8579OtherTelco(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }

    [WebMethod]
    public string WSProcessMoWorldCup(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExcecuteRequestMoWorldCup(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }

    #endregion

    #region Process MO Methods

    private string ExcecuteRequestMoWorldCup(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        string responseValue = "1";
        int returnValue = 0;

        Message = Message.Trim().ToUpper();
        string subcode = "";

        Command_Code = Command_Code.ToUpper();

        if (Message.Trim().Length > Command_Code.Trim().Length)
        {
            subcode = Message.ToUpper().Substring(Command_Code.Length).Trim();
        }

        try
        {

            log.Debug(" ");
            log.Debug(" ");
            log.Debug("--------------------World Cup Mo Log-------------------------");
            log.Debug("User_ID: " + User_ID);
            log.Debug("Service_ID: " + Service_ID);
            log.Debug("Command_Code: " + Command_Code);
            log.Debug("Message: " + Message.ToUpper());
            log.Debug("Request_ID: " + Request_ID);
            log.Debug(" ");
            log.Debug(" ");

            string mtContent = string.Empty;
            int matchId = 0;
            string teamCode = "HOA";

            #region KIEM TRA Ma_Tran_Dau OR Ma_Doi
            
            if(Command_Code == "KQ" && subcode != "")
            {

                var ws = new WebService();
                return ws.WSProcessMoSportGame(User_ID, Service_ID, Command_Code, Message, Request_ID);

                string[] arr = subcode.Split(' ');
                var dtMatch = new DataTable();

                if(arr.Length == 2)//DU DOAN THANG_THUA
                {
                    matchId = ConvertUtility.ToInt32(arr[0].Trim());
                    teamCode = arr[1].Trim();

                    #region CHECK Ma_Tran_Dau

                    dtMatch = VoteRegisterController.WorldCupMatchCheck(matchId);
                    if (dtMatch.Rows.Count == 0)
                    {
                        mtContent = "Ma tran dau chua dung. Vui long truy cap http://wap.vietnamobile.com.vn/ de biet them thong tin";

                        WorldCupLogMo(User_ID, Service_ID, Command_Code, Message, Request_ID, 0);
                        WorldCupSendMt(User_ID, Service_ID, Command_Code, mtContent, Request_ID, "2");

                        return responseValue;
                    }

                    #endregion

                    #region CHECK_Ma_Doi_Bong

                    DataTable dtRound = VoteRegisterController.WorldCupRoundCheck(teamCode);
                    if (dtRound.Rows.Count == 0)
                    {
                        mtContent = "Ma doi bong chua dung. Vui long truy cap http://wap.vietnamobile.com.vn/ de biet them thong tin";

                        WorldCupLogMo(User_ID, Service_ID, Command_Code, Message, Request_ID, 0);
                        WorldCupSendMt(User_ID, Service_ID, Command_Code, mtContent, Request_ID, "2");

                        return responseValue;
                    }

                    #endregion

                    #region CHECK_Finished_Vote_Time

                    if (dtMatch.Rows.Count > 0)
                    {
                        DateTime finishedVote = ConvertUtility.ToDateTime(dtMatch.Rows[0]["Finished_Vote"].ToString());
                        if (DateTime.Now > finishedVote)
                        {
                            mtContent = "Da het thoi gian du doan ket qua tran dau "
                                        + UnicodeUtility.UnicodeToKoDau(dtMatch.Rows[0]["Team_A_Name"].ToString())
                                        + " - "
                                        + UnicodeUtility.UnicodeToKoDau(dtMatch.Rows[0]["Team_B_Name"].ToString());

                            WorldCupLogMo(User_ID, Service_ID, Command_Code, Message, Request_ID, 0);
                            WorldCupSendMt(User_ID, Service_ID, Command_Code, mtContent, Request_ID, "2");

                            return responseValue;

                        }
                    }

                    #endregion

                }
                else if(arr.Length == 1)//DU DOAN KQ HOA
                {
                    matchId = ConvertUtility.ToInt32(arr[0]);

                    #region CHECK Ma_Tran_Dau

                    dtMatch = VoteRegisterController.WorldCupMatchCheck(matchId);
                    if (dtMatch.Rows.Count == 0)
                    {
                        mtContent = "Ma doi bong chua dung. Vui long truy cap http://wap.vietnamobile.com.vn/ de biet them thong tin";

                        WorldCupLogMo(User_ID, Service_ID, Command_Code, Message, Request_ID, 0);
                        WorldCupSendMt(User_ID, Service_ID, Command_Code, mtContent, Request_ID, "2");

                        return responseValue;
                    }

                    #endregion

                    #region CHECK_Finished_Vote_Time

                    if (dtMatch.Rows.Count > 0)
                    {
                        DateTime finishedVote = ConvertUtility.ToDateTime(dtMatch.Rows[0]["Finished_Vote"].ToString());
                        if (DateTime.Now > finishedVote)
                        {
                            mtContent = "Da het thoi gian du doan ket qua tran dau "
                                        + UnicodeUtility.UnicodeToKoDau(dtMatch.Rows[0]["Team_A_Name"].ToString())
                                        + " - "
                                        + UnicodeUtility.UnicodeToKoDau(dtMatch.Rows[0]["Team_B_Name"].ToString());

                            WorldCupLogMo(User_ID, Service_ID, Command_Code, Message, Request_ID, 0);
                            WorldCupSendMt(User_ID, Service_ID, Command_Code, mtContent, Request_ID, "2");

                            return responseValue;

                        }
                    }

                    #endregion

                }
                else
                {
                    mtContent = "Tin nhan sai cu phap. Vui long truy cap http://wap.vietnamobile.com.vn/ de biet them thong tin";

                    WorldCupLogMo(User_ID, Service_ID, Command_Code, Message, Request_ID, 0);
                    WorldCupSendMt(User_ID, Service_ID, Command_Code, mtContent, Request_ID, "2");

                    return responseValue;
                }
            }
            else if (Command_Code == "VD" && subcode != "")//DU DOAN DOI VO DICH
            {
                DataTable dtRound = VoteRegisterController.WorldCupRoundCheck(subcode);
                if(dtRound.Rows.Count == 0)
                {
                    mtContent = "Ma doi bong chua dung. Vui long truy cap http://wap.vietnamobile.com.vn/ de biet them thong tin";

                    WorldCupLogMo(User_ID, Service_ID, Command_Code, Message, Request_ID, 0);
                    WorldCupSendMt(User_ID, Service_ID, Command_Code, mtContent, Request_ID, "2");

                    return responseValue;
                }
            }
            else
            {
                mtContent = "Tin nhan sai cu phap. Vui long truy cap http://wap.vietnamobile.com.vn/ de biet them thong tin";

                WorldCupLogMo(User_ID, Service_ID, Command_Code, Message, Request_ID, 0);
                WorldCupSendMt(User_ID, Service_ID, Command_Code, mtContent, Request_ID, "2");

                return responseValue;
            }

            #endregion


            #region XU LY CHARGING & Log Mo & Log Doanh Thu

            string result = PaymentWorldCupChargingOptimize(User_ID, Command_Code);
            string[] arrResult = result.Split('|');

            string value = arrResult[0].Trim();
            string price = arrResult[1].Trim();
            int isCharged = 0;
            string reason;

            if (value == "1")
            {
                isCharged = 1;
                reason = "Succ";
            }
            else
            {
                reason = value;
            }

            WorldCupLogMo(User_ID,Service_ID,Command_Code,Message,Request_ID,isCharged);
            WorldCupLogDoanhThu(User_ID, Request_ID, Service_ID, Command_Code, price,reason);

            #endregion


            if(value == "1")//CHARGED $ THANH CONG
            {
                if(Command_Code == "KQ" && subcode != "")//DU DOAN KQ TRAN DAU
                {
                    #region DU DOAN KQ TRAN DAU

                    VoteRegisterController.WorldCupMatchInsert(User_ID,matchId,Message,teamCode);
                    mtContent = "Cam on ban da tham gia du doan cung chuong trinh Dong hanh cung World Cup 2014, ket qua se duoc gui toi ban sau khi tran dau ket thuc. Hay tiep tuc du doan de tang them co hoi trung thuong là chuyen du lich ThaiLan";

                    #endregion
                }
                else if(Command_Code == "VD" && subcode != "")//DU DOAN DOI VO DICH
                {
                    #region DU DOAN DOI VO DICH

                    VoteRegisterController.WorldCupRoundMatchInsert(User_ID, Message, subcode);
                    mtContent = "Cam on ban da tham gia du doan doi vo dich World Cup 2014. Ket qua se duoc gui toi ban sau khi giai dau ket thuc. Chuc ban may man!";

                    #endregion
                }

                #region DK SUB cho USER

                var entity = new ViSport_S2_Registered_UsersInfo();
                entity.User_ID = User_ID;
                entity.Request_ID = Request_ID;
                entity.Service_ID = Service_ID;
                entity.Command_Code = Command_Code;
                entity.Service_Type = 1;
                entity.Charging_Count = 0;
                entity.FailedChargingTimes = 0;
                entity.RegisteredTime = DateTime.Now;
                entity.ExpiredTime = DateTime.Now.AddDays(1);
                entity.Registration_Channel = "SMS";
                entity.Status = 1;
                entity.Operator = GetTelco(User_ID);
                entity.Point = 0;
                ViSport_S2_Registered_UsersController.WorldCupRegisterUser94X(entity);

                #endregion

                WorldCupSendMt(User_ID, Service_ID, Command_Code, mtContent, Request_ID, "1");
            }
            else if (value == AppEnv.GetSetting("NotEnoughMoney"))
            {
                mtContent = "Thue bao khong du tien. Vui long nap them tien de tiep tuc choi";
                WorldCupSendMt(User_ID, Service_ID, Command_Code, mtContent, Request_ID, "2");
            }
            else
            {
                mtContent = "He thong dang nang cap. Vui long thu lai sau";
                WorldCupSendMt(User_ID, Service_ID, Command_Code, mtContent, Request_ID, "2");
            }

        }
        catch (Exception ex)
        {
            log.Debug("---------------Error World Cup----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }

        return responseValue;
    }

    private string ExcecuteRequest8279GirlSecretMoVnm(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        string responseValue = "1";
        int returnValue = 0;

        Message = Message.ToUpper();
        string subcode = "";
        if (Message.Trim().Length > Command_Code.Trim().Length)
        {
            subcode = Message.ToUpper().Substring(Command_Code.Length).Replace(" ", "");
        }
        Command_Code = Command_Code.ToUpper();

        int servicePrice = 3000;

        try
        {
            log.Debug(" ");
            log.Debug(" ");
            log.Debug("--------------------VOTE SECRET VNM-------------------------");
            log.Debug("User_ID: " + User_ID);
            log.Debug("Service_ID: " + Service_ID);
            log.Debug("Command_Code: " + Command_Code);
            log.Debug("Message: " + Message.ToUpper());
            log.Debug("Request_ID: " + Request_ID);
            log.Debug(" ");
            log.Debug(" ");

            #region LOG SMS MO

            VoteSmsMoInfo moInfo = new VoteSmsMoInfo();
            moInfo.User_ID = User_ID;
            moInfo.Request_ID = Request_ID;
            moInfo.Service_ID = Service_ID;
            moInfo.Command_Code = Command_Code;
            moInfo.Message = Message;
            moInfo.Operator = GetTelco(User_ID);

            VoteRegisterController.SecretSmsMoInsert(moInfo);

            #endregion

            string messageReturn;

            if(Command_Code == "GACH" && subcode == "")//DK SUB VNM
            {
                #region DK USER

                var entity = new VoteRegisteredInfo();
                entity.User_ID = User_ID;
                entity.Request_ID = Request_ID;
                entity.Service_ID = Service_ID;
                entity.Command_Code = Command_Code;
                entity.Service_Type = 1;
                entity.Charging_Count = 0;
                entity.FailedChargingTime = 0;
                entity.RegisteredTime = DateTime.Now;
                entity.ExpiredTime = DateTime.Now.AddDays(1);
                entity.Registration_Channel = "SMS";
                entity.Status = 1;
                entity.Operator = GetTelco(User_ID);
                entity.Vote_Count = 1;

                entity.Vote_PersonId = 1;
                entity.IsDislike = 0;

                DataTable value = VoteRegisterController.SecretRegisterInsert(entity);

                int voteCount = ConvertUtility.ToInt32(value.Rows[0]["VOTE_SUM"]);
                string voteTop = GetTopVote(voteCount);

                if (value.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
                {
                    messageReturn = AppEnv.GetSetting("BiMatHotGirl_DangKyThanhCong").Replace("[SmsSub]", AppEnv.GetSetting("BiMatHotGirl_SmsSub_Vnm")).Replace("[WapAddress]", AppEnv.GetSetting("BiMatHotGirl_WapAddress_Vnm"));
                    SendMtSecret(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG LAN 1
                }
                else if (value.Rows[0]["RETURN_ID"].ToString() == "1")//DA DK DICH VU (UPDATE LUOT VOTE HIEN TAI)
                {
                    messageReturn = AppEnv.GetSetting("BiMatHotGirl_ThongBaoLuotVote")
                            .Replace("[SmsSub]", AppEnv.GetSetting("BiMatHotGirl_SmsSub_Vnm"))
                            .Replace("[WapAddress]", AppEnv.GetSetting("BiMatHotGirl_WapAddress_Vnm"));
                    messageReturn = messageReturn.Replace("[VoteCount]", voteCount.ToString());
                    messageReturn = messageReturn.Replace("[VoteTop]", voteTop);

                    SendMtSecret(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG
                }

                //GUI THEM TIN TUC BI_MAT_MAI_THO
                DataTable dtSecretContent = VoteRegisterController.SecretGetRandomContent();
                if(dtSecretContent != null && dtSecretContent.Rows.Count > 0)
                {
                    messageReturn = dtSecretContent.Rows[0]["MT1"].ToString();
                    SendMtSecret(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG
                }

                #region Log Doanh Thu

                var e = new VoteChargedUserLogInfo();

                e.User_ID = User_ID;
                e.Request_ID = Request_ID;
                e.Service_ID = Service_ID;
                e.Command_Code = Command_Code;
                e.Service_Type = 1;
                e.Charging_Count = 0;
                e.FailedChargingTime = 0;
                e.RegisteredTime = DateTime.Now;
                e.ExpiredTime = DateTime.Now.AddDays(1);
                e.Registration_Channel = "SMS";
                e.Status = 1;
                e.Operator = GetTelco(User_ID);
                e.Reason = "Succ";
                e.Price = servicePrice;
                e.Vote_PersonId = 1;

                VoteRegisterController.SecretChargedUserLogInsert(e);

                #endregion

                #endregion
            }
            else if(Command_Code == "HENHO" && subcode == "")//FAQ
            {
                #region FAQ

                messageReturn = AppEnv.GetSetting("BiMatHotGirl_HuongDan");
                messageReturn = messageReturn.Replace("[SmsSub]", AppEnv.GetSetting("BiMatHotGirl_SmsSub_Vnm"));
                messageReturn = messageReturn.Replace("[WapAddress]", AppEnv.GetSetting("BiMatHotGirl_WapAddress_Vnm"));


                #region Log Doanh Thu

                var e = new VoteChargedUserLogInfo();

                e.User_ID = User_ID;
                e.Request_ID = Request_ID;
                e.Service_ID = Service_ID;
                e.Command_Code = Command_Code;
                e.Service_Type = 1;
                e.Charging_Count = 0;
                e.FailedChargingTime = 0;
                e.RegisteredTime = DateTime.Now;
                e.ExpiredTime = DateTime.Now.AddDays(1);
                e.Registration_Channel = "SMS";
                e.Status = 1;
                e.Operator = GetTelco(User_ID);
                e.Reason = "Succ";
                e.Price = 2000;
                e.Vote_PersonId = 0;

                VoteRegisterController.SecretChargedUserLogInsert(e);

                #endregion


                SendMtSecret(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG

                #endregion
            }
            else if(Command_Code == "HUY" && subcode == "GACH")//HUY DICH VU
            {
                #region HUY DICH VU

                DataTable dt = VoteRegisterController.SecretRegisterUserLock(User_ID);
                string message = "";

                if (dt.Rows[0]["RETURN_ID"].ToString() == "0")//CHUA DK USER
                {
                    message = AppEnv.GetSetting("BiMatHotGirl_HuyDichVuChuaDangKy").Replace("[SmsSub]", AppEnv.GetSetting("BiMatHotGirl_SmsSub_Vnm"));
                }
                else if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                {
                    message = AppEnv.GetSetting("BiMatHotGirl_HuyDichVuDaDangKy").Replace("[SmsSub]", AppEnv.GetSetting("BiMatHotGirl_SmsSub_Vnm"));
                }

                #region Log Doanh Thu

                var e = new VoteChargedUserLogInfo();

                e.User_ID = User_ID;
                e.Request_ID = Request_ID;
                e.Service_ID = Service_ID;
                e.Command_Code = Command_Code;
                e.Service_Type = 1;
                e.Charging_Count = 0;
                e.FailedChargingTime = 0;
                e.RegisteredTime = DateTime.Now;
                e.ExpiredTime = DateTime.Now.AddDays(1);
                e.Registration_Channel = "SMS";
                e.Status = 1;
                e.Operator = GetTelco(User_ID);
                e.Reason = "Succ";
                e.Price = servicePrice;
                e.Vote_PersonId = 0;

                VoteRegisterController.SecretChargedUserLogInsert(e);

                #endregion

                SendMtSecret(User_ID, Service_ID, Command_Code, message, Request_ID);

                #endregion
            }
            else//SAI CU PHAP
            {
                #region SAI CU PHAP

                messageReturn = AppEnv.GetSetting("BiMatHotGirl_SaiCuPhap");
                messageReturn = messageReturn.Replace("[SmsSub]", AppEnv.GetSetting("BiMatHotGirl_SmsSub_Vnm"));
                messageReturn = messageReturn.Replace("[WapAddress]", AppEnv.GetSetting("BiMatHotGirl_WapAddress_Vnm"));

                SendMtSecret(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG

                #endregion
            }
        }
        catch (Exception ex)
        {
            responseValue = "1";
            log.Debug("---------------Error VOTE SECRET sentMT----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }

        return responseValue;

    }

    private string ExcecuteRequest8279GirlSecretMoOtherTelco(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        string responseValue = "1";
        int returnValue = 0;

        Message = Message.ToUpper();
        string subcode = "";
        if (Message.Trim().Length > Command_Code.Trim().Length)
        {
            subcode = Message.ToUpper().Substring(Command_Code.Length).Replace(" ", "");
        }
        Command_Code = Command_Code.ToUpper();

        int servicePrice = 3000;

        try
        {
            log.Debug(" ");
            log.Debug(" ");
            log.Debug("--------------------VOTE SECRET OTHER TELCO-------------------------");
            log.Debug("User_ID: " + User_ID);
            log.Debug("Service_ID: " + Service_ID);
            log.Debug("Command_Code: " + Command_Code);
            log.Debug("Message: " + Message.ToUpper());
            log.Debug("Request_ID: " + Request_ID);
            log.Debug(" ");
            log.Debug(" ");

            #region LOG SMS MO

            VoteSmsMoInfo moInfo = new VoteSmsMoInfo();
            moInfo.User_ID = User_ID;
            moInfo.Request_ID = Request_ID;
            moInfo.Service_ID = Service_ID;
            moInfo.Command_Code = Command_Code;
            moInfo.Message = Message;
            moInfo.Operator = GetTelco(User_ID);

            VoteRegisterController.SecretSmsMoInsert(moInfo);

            #endregion

            string messageReturn;

            if(Command_Code == "GACH" && subcode == "")//DAT GACH
            {
                #region DK USER

                var entity = new VoteRegisteredInfo();
                entity.User_ID = User_ID;
                entity.Request_ID = Request_ID;
                entity.Service_ID = Service_ID;
                entity.Command_Code = Command_Code;
                entity.Service_Type = 1;
                entity.Charging_Count = 0;
                entity.FailedChargingTime = 0;
                entity.RegisteredTime = DateTime.Now;
                entity.ExpiredTime = DateTime.Now.AddDays(1);
                entity.Registration_Channel = "SMS";
                entity.Status = 1;
                entity.Operator = GetTelco(User_ID);
                entity.Vote_Count = 1;

                entity.Vote_PersonId = 1;
                entity.IsDislike = 0;

                DataTable value = VoteRegisterController.SecretRegisterInsert(entity);

                int voteCount = ConvertUtility.ToInt32(value.Rows[0]["VOTE_SUM"]);
                string voteTop = GetTopVote(voteCount);

                messageReturn = AppEnv.GetSetting("BiMatHotGirl_ThongBaoLuotVote")
                            .Replace("[SmsSub]", AppEnv.GetSetting("BiMatHotGirl_SmsSub_Xzone"))
                            .Replace("[WapAddress]", AppEnv.GetSetting("BiMatHotGirl_WapAddress_Xzone"));
                messageReturn = messageReturn.Replace("[VoteCount]", voteCount.ToString());
                messageReturn = messageReturn.Replace("[VoteTop]", voteTop);

                SendMtSecret(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG LAN 1

                //GUI THEM TIN TUC BI_MAT_MAI_THO
                DataTable dtSecretContent = VoteRegisterController.SecretGetRandomContent();
                if (dtSecretContent != null && dtSecretContent.Rows.Count > 0)
                {
                    messageReturn = dtSecretContent.Rows[0]["MT1"].ToString();
                    SendMtSecret(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG
                }

                #region Log Doanh Thu

                var e = new VoteChargedUserLogInfo();

                e.User_ID = User_ID;
                e.Request_ID = Request_ID;
                e.Service_ID = Service_ID;
                e.Command_Code = Command_Code;
                e.Service_Type = 1;
                e.Charging_Count = 0;
                e.FailedChargingTime = 0;
                e.RegisteredTime = DateTime.Now;
                e.ExpiredTime = DateTime.Now.AddDays(1);
                e.Registration_Channel = "SMS";
                e.Status = 1;
                e.Operator = GetTelco(User_ID);
                e.Reason = "Succ";
                e.Price = servicePrice;
                e.Vote_PersonId = 1;

                VoteRegisterController.SecretChargedUserLogInsert(e);

                #endregion

                #endregion
            }
            else//SAI CU PHAP
            {
                #region SAI CU PHAP

                messageReturn = AppEnv.GetSetting("BiMatHotGirl_SaiCuPhap");
                messageReturn = messageReturn.Replace("[SmsSub]", AppEnv.GetSetting("BiMatHotGirl_SmsSub_Xzone"));
                messageReturn = messageReturn.Replace("[WapAddress]", AppEnv.GetSetting("BiMatHotGirl_WapAddress_Xzone"));

                SendMtSecret(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG

                #endregion
            }
        }
        catch (Exception ex)
        {
            responseValue = "1";
            log.Debug("---------------Error VOTE SECRET sentMT----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }

        return responseValue;

    }

    private string ExcecuteRequest8279MoVnm(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
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
            log.Debug("--------------------VOTE VNM-------------------------");
            log.Debug("User_ID: " + User_ID);
            log.Debug("Service_ID: " + Service_ID);
            log.Debug("Command_Code: " + Command_Code);
            log.Debug("Message: " + Message.ToUpper());
            log.Debug("Request_ID: " + Request_ID);
            log.Debug(" ");
            log.Debug(" ");

            #region LOG SMS MO
            
            VoteSmsMoInfo moInfo = new VoteSmsMoInfo();
            moInfo.User_ID = User_ID;
            moInfo.Request_ID = Request_ID;
            moInfo.Service_ID = Service_ID;
            moInfo.Command_Code = Command_Code;
            moInfo.Message = Message;
            moInfo.Operator = GetTelco(User_ID);

            VoteRegisterController.VoteSmsMoInsert(moInfo);

            #endregion

            //if (Command_Code.ToUpper() != "HUY" || Command_Code.ToUpper() != "VOTE1" || Command_Code.ToUpper() != "VOTE2" || Command_Code.ToUpper() != "GACH1" || Command_Code.ToUpper() != "GACH2")
            //{

            //    string message = AppEnv.GetSetting("Vote_Sms_ErrorSystax");

            //    SendMt(User_ID,Service_ID,Command_Code,message,Request_ID);

            //    return responseValue;
            //}
            if(Command_Code.ToUpper() == "HUY")
            {
                DataTable dt = VoteRegisterController.VoteRegisterUserLock(User_ID,1);
                string message = "";

                if (dt.Rows[0]["RETURN_ID"].ToString() == "0")//CHUA DK USER
                {
                    message = AppEnv.GetSetting("Vote_Sms_LockUserError");
                }
                else if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                {
                    message = AppEnv.GetSetting("Vote_Sms_LockUserSuccess");
                }

                #region Log Doanh Thu

                var e = new VoteChargedUserLogInfo();

                e.User_ID = User_ID;
                e.Request_ID = Request_ID;
                e.Service_ID = Service_ID;
                e.Command_Code = Command_Code;
                e.Service_Type = 1;
                e.Charging_Count = 0;
                e.FailedChargingTime = 0;
                e.RegisteredTime = DateTime.Now;
                e.ExpiredTime = DateTime.Now.AddDays(1);
                e.Registration_Channel = "SMS";
                e.Status = 1;
                e.Operator = GetTelco(User_ID);
                e.Reason = "Succ";
                e.Price = 2000;
                e.Vote_PersonId = 0;

                VoteRegisterController.VoteChargedUserLogInsert(e);

                #endregion

                SendMt(User_ID, Service_ID, Command_Code, message, Request_ID);
            }
            else if( Command_Code.ToUpper() == "HUY" && (subcode.ToUpper() == "VOTE1" || subcode.ToUpper() == "VOTE2") )
            {
                //HUY DICH VU
                int votePersonId = 1;
                if (subcode.ToUpper() == "VOTE2")
                    votePersonId = 2;

                DataTable dt = VoteRegisterController.VoteRegisterUserLock(User_ID, votePersonId);
                string message = "";

                if (dt.Rows[0]["RETURN_ID"].ToString() == "0")//CHUA DK USER
                {
                    message = AppEnv.GetSetting("Vote_Sms_LockUserError");
                }
                else if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                {
                    message = AppEnv.GetSetting("Vote_Sms_LockUserSuccess");
                }

                #region Log Doanh Thu

                var e = new VoteChargedUserLogInfo();

                e.User_ID = User_ID;
                e.Request_ID = Request_ID;
                e.Service_ID = Service_ID;
                e.Command_Code = Command_Code;
                e.Service_Type = 1;
                e.Charging_Count = 0;
                e.FailedChargingTime = 0;
                e.RegisteredTime = DateTime.Now;
                e.ExpiredTime = DateTime.Now.AddDays(1);
                e.Registration_Channel = "SMS";
                e.Status = 1;
                e.Operator = GetTelco(User_ID);
                e.Reason = "Succ";
                e.Price = 2000;
                e.Vote_PersonId = 0;

                VoteRegisterController.VoteChargedUserLogInsert(e);

                #endregion

                SendMt(User_ID,Service_ID,Command_Code,message,Request_ID);
            }
            else if (Command_Code.ToUpper() == "HUY" && (subcode.ToUpper() == "GACH1" || subcode.ToUpper() == "GACH2"))
            {
                //HUY DICH VU
                int dislikePersonId = 1;
                if (subcode.ToUpper() == "GACH2")
                    dislikePersonId = 2;

                DataTable dt = VoteRegisterController.VoteRegisterUserDislikeLock(User_ID, dislikePersonId);
                string message = "";

                if (dt.Rows[0]["RETURN_ID"].ToString() == "0")//CHUA DK USER
                {
                    message = AppEnv.GetSetting("Vote_Sms_LockUserError");
                }
                else if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                {
                    message = AppEnv.GetSetting("Vote_Sms_LockUserSuccess");
                }

                #region Log Doanh Thu

                var e = new VoteChargedUserLogInfo();

                e.User_ID = User_ID;
                e.Request_ID = Request_ID;
                e.Service_ID = Service_ID;
                e.Command_Code = Command_Code;
                e.Service_Type = 1;
                e.Charging_Count = 0;
                e.FailedChargingTime = 0;
                e.RegisteredTime = DateTime.Now;
                e.ExpiredTime = DateTime.Now.AddDays(1);
                e.Registration_Channel = "SMS";
                e.Status = 1;
                e.Operator = GetTelco(User_ID);
                e.Reason = "Succ";
                e.Price = 2000;
                e.Vote_PersonId = 0;

                VoteRegisterController.VoteChargedUserLogInsert(e);

                #endregion

                SendMt(User_ID, Service_ID, Command_Code, message, Request_ID);
            }
            else if( (Command_Code.ToUpper() == "VOTE1" || Command_Code.ToUpper() == "VOTE2") && subcode == "" )
            {
                int votePersonId = 1;
                string personName = "Mai Tho";

                if (Command_Code.ToUpper() == "VOTE2")
                {
                    votePersonId = 2;
                    personName = "Linh Miu";
                }
                    
                var entity = new VoteRegisteredInfo();
                entity.User_ID = User_ID;
                entity.Request_ID = Request_ID;
                entity.Service_ID = Service_ID;
                entity.Command_Code = Command_Code;
                entity.Service_Type = 1;
                entity.Charging_Count = 0;
                entity.FailedChargingTime = 0;
                entity.RegisteredTime = DateTime.Now;
                entity.ExpiredTime = DateTime.Now.AddDays(1);
                entity.Registration_Channel = "SMS";
                entity.Status = 1;
                entity.Operator = GetTelco(User_ID);
                entity.Vote_Count = 1;

                entity.Vote_PersonId = votePersonId;
                entity.IsDislike = 0;

                DataTable value = VoteRegisterController.VoteRegisterInsert(entity);
                string messageReturn;
                int voteCount = ConvertUtility.ToInt32(value.Rows[0]["VOTE_SUM"]);
                string voteTop = GetTopVote(voteCount);

                if (value.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
                {
                    messageReturn = AppEnv.GetSetting("Vote_Sms_RegisterSucess").Replace("PersonName", personName);
                    SendMt(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG LAN 1

                    string contentMt2 = AppEnv.GetSetting("Vote_Sms_RegisterSucess_Mt2_Like").Replace("PersonName", personName);
                    contentMt2 = contentMt2.Replace("VoteCount", voteCount.ToString());
                    contentMt2 = contentMt2.Replace("VoteTop", voteTop);
                    SendMt(User_ID, Service_ID, Command_Code, contentMt2, Request_ID);//SENT MT CHO KHACH HANG LAN 2
                }
                else if (value.Rows[0]["RETURN_ID"].ToString() == "1")//DA DK DICH VU (UPDATE LUOT VOTE HIEN TAI)
                {
                    messageReturn = AppEnv.GetSetting("Vote_Sms_RegisterSucess_Mt2_Like").Replace("PersonName", personName);
                    messageReturn = messageReturn.Replace("VoteCount", voteCount.ToString());
                    messageReturn = messageReturn.Replace("VoteTop", voteTop);

                    SendMt(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG
                }

                #region Log Doanh Thu

                var e = new VoteChargedUserLogInfo();

                e.User_ID = User_ID;
                e.Request_ID = Request_ID;
                e.Service_ID = Service_ID;
                e.Command_Code = Command_Code;
                e.Service_Type = 1;
                e.Charging_Count = 0;
                e.FailedChargingTime = 0;
                e.RegisteredTime = DateTime.Now;
                e.ExpiredTime = DateTime.Now.AddDays(1);
                e.Registration_Channel = "SMS";
                e.Status = 1;
                e.Operator = GetTelco(User_ID);
                e.Reason = "Succ";
                e.Price = 2000;
                e.Vote_PersonId = votePersonId;

                VoteRegisterController.VoteChargedUserLogInsert(e);

                #endregion

            }
            else if ( (Command_Code.ToUpper() == "GACH1" || Command_Code.ToUpper() == "GACH2") && subcode == "")
            {
                int votePersonId = 0;
                int dislikePersonId = 0;
                string personName = "";

                if (Command_Code.ToUpper() == "GACH1")
                {
                    votePersonId = 2;
                    dislikePersonId = 1;
                    personName = "Mai Tho";
                }
                else if (Command_Code.ToUpper() == "GACH2")
                {
                    votePersonId = 1;
                    dislikePersonId = 2;
                    personName = "Linh Miu";
                }

                
                #region LUU USER DK NEM GACH

                var entity = new VoteRegisteredInfo();
                entity.User_ID = User_ID;
                entity.Request_ID = Request_ID;
                entity.Service_ID = Service_ID;
                entity.Command_Code = Command_Code;
                entity.Service_Type = 1;
                entity.Charging_Count = 0;
                entity.FailedChargingTime = 0;
                entity.RegisteredTime = DateTime.Now;
                entity.ExpiredTime = DateTime.Now.AddDays(1);
                entity.Registration_Channel = "SMS";
                entity.Status = 1;
                entity.Operator = GetTelco(User_ID);

                entity.Vote_Count = 1;
                entity.Vote_PersonId = votePersonId;

                entity.IsDislike = 1;

                entity.Dislike_Count = 1;
                entity.Dislike_PersonId = dislikePersonId;

                DataTable dt = VoteRegisterController.VoteRegisterInsert(entity);
                DataTable dtDislike = VoteRegisterController.VoteRegisterDislikeInsert(entity);
                string messageReturn;

                int dislikeCount = ConvertUtility.ToInt32(dtDislike.Rows[0]["DISLIKE_SUM"]);
                string dislikeTop = GetTopVote(dislikeCount);

                if (dt.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
                {
                    messageReturn = AppEnv.GetSetting("Vote_Sms_RegisterSucess_Dislike").Replace("PersonName", personName);
                    SendMt(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG LAN 1

                    string contentMt2 = AppEnv.GetSetting("Vote_Sms_RegisterSucess_Mt2_UnLike").Replace("PersonName", personName);
                    contentMt2 = contentMt2.Replace("DislikeCount", dislikeCount.ToString());
                    contentMt2 = contentMt2.Replace("DislikeTop", dislikeTop);
                    SendMt(User_ID, Service_ID, Command_Code, contentMt2, Request_ID);//SENT MT CHO KHACH HANG LAN 2
                }
                else if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                {
                    messageReturn = AppEnv.GetSetting("Vote_Sms_RegisterSucess_Mt2_UnLike").Replace("PersonName", personName);
                    messageReturn = messageReturn.Replace("DislikeCount", dislikeCount.ToString());
                    messageReturn = messageReturn.Replace("DislikeTop", dislikeTop);

                    SendMt(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG
                }

                #region Log Doanh Thu

                var e = new VoteChargedUserLogInfo();

                e.User_ID = User_ID;
                e.Request_ID = Request_ID;
                e.Service_ID = Service_ID;
                e.Command_Code = Command_Code;
                e.Service_Type = 1;
                e.Charging_Count = 0;
                e.FailedChargingTime = 0;
                e.RegisteredTime = DateTime.Now;
                e.ExpiredTime = DateTime.Now.AddDays(1);
                e.Registration_Channel = "SMS";
                e.Status = 1;
                e.Operator = GetTelco(User_ID);
                e.Reason = "Succ";
                e.Price = 2000;
                e.Vote_PersonId = votePersonId;

                VoteRegisterController.VoteChargedUserLogInsert(e);

                #endregion

                #endregion
            }
            else if(Command_Code.ToUpper() == "HENHO" && subcode == "")
            {
                string messageReturn = AppEnv.GetSetting("Vote_Sms_Faq");

                #region Log Doanh Thu

                var e = new VoteChargedUserLogInfo();

                e.User_ID = User_ID;
                e.Request_ID = Request_ID;
                e.Service_ID = Service_ID;
                e.Command_Code = Command_Code;
                e.Service_Type = 1;
                e.Charging_Count = 0;
                e.FailedChargingTime = 0;
                e.RegisteredTime = DateTime.Now;
                e.ExpiredTime = DateTime.Now.AddDays(1);
                e.Registration_Channel = "SMS";
                e.Status = 1;
                e.Operator = GetTelco(User_ID);
                e.Reason = "Succ";
                e.Price = 2000;
                e.Vote_PersonId = 0;

                VoteRegisterController.VoteChargedUserLogInsert(e);

                #endregion

                SendMt(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG
            }
            else//TIN NHAN SAI CU PHAP
            {
                string message = AppEnv.GetSetting("Vote_Sms_ErrorSystax");
                SendMt(User_ID,Service_ID,Command_Code,message,Request_ID);
            }

            responseValue = "1";
        }
        catch (Exception ex)
        {
            responseValue = "1";
            log.Debug("---------------Error sentMT----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }

        return responseValue;
    }

    private string ExcecuteRequest8279Mo(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
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
            log.Debug("--------------------VOTE OTHER TELCO-------------------------");
            log.Debug("User_ID: " + User_ID);
            log.Debug("Service_ID: " + Service_ID);
            log.Debug("Command_Code: " + Command_Code);
            log.Debug("Message: " + Message.ToUpper());
            log.Debug("Request_ID: " + Request_ID);
            log.Debug(" ");
            log.Debug(" ");

            #region LOG SMS MO

            VoteSmsMoInfo moInfo = new VoteSmsMoInfo();
            moInfo.User_ID = User_ID;
            moInfo.Request_ID = Request_ID;
            moInfo.Service_ID = Service_ID;
            moInfo.Command_Code = Command_Code;
            moInfo.Message = Message;
            moInfo.Operator = GetTelco(User_ID);

            VoteRegisterController.VoteSmsMoInsert(moInfo);

            #endregion

            string returnMessage;
            if( (Command_Code.ToUpper() == "VOTE1" || Command_Code.ToUpper() == "VOTE2") && subcode == "" )
            {
                int votePersonId = 1;
                string personName = "Mai Tho";

                if (Command_Code.ToUpper() == "VOTE2")
                {
                    votePersonId = 2;
                    personName = "Linh Miu";
                }

                var entity = new VoteRegisteredInfo();
                entity.User_ID = User_ID;
                entity.Request_ID = Request_ID;
                entity.Service_ID = Service_ID;
                entity.Command_Code = Command_Code;
                entity.Service_Type = 1;
                entity.Charging_Count = 0;
                entity.FailedChargingTime = 0;
                entity.RegisteredTime = DateTime.Now;
                entity.ExpiredTime = DateTime.Now.AddDays(1);
                entity.Registration_Channel = "SMS";
                entity.Status = 1;
                entity.Operator = GetTelco(User_ID);
                entity.Vote_Count = 1;

                entity.Vote_PersonId = votePersonId;

                DataTable value = VoteRegisterController.VoteRegisterInsert(entity);

                int voteCount = ConvertUtility.ToInt32(value.Rows[0]["VOTE_SUM"]);
                string voteTop = GetTopVote(voteCount);

                string messageReturn = AppEnv.GetSetting("Vote_Sms_RegisterSucess_OtherTelco").Replace("PersonName", personName);
                messageReturn = messageReturn.Replace("VoteCount", voteCount.ToString());
                messageReturn = messageReturn.Replace("VoteTop", voteTop);

                #region Log Doanh Thu

                var e = new VoteChargedUserLogInfo();

                e.User_ID = User_ID;
                e.Request_ID = Request_ID;
                e.Service_ID = Service_ID;
                e.Command_Code = Command_Code;
                e.Service_Type = 1;
                e.Charging_Count = 0;
                e.FailedChargingTime = 0;
                e.RegisteredTime = DateTime.Now;
                e.ExpiredTime = DateTime.Now.AddDays(1);
                e.Registration_Channel = "SMS";
                e.Status = 1;
                e.Operator = GetTelco(User_ID);
                e.Reason = "Succ";
                e.Price = 2000;
                e.Vote_PersonId = votePersonId;

                VoteRegisterController.VoteChargedUserLogInsert(e);

                #endregion

                SendMt(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG
            }
            else if ( (Command_Code.ToUpper() == "GACH1" || Command_Code.ToUpper() == "GACH2") && subcode == "" )
            {
                int votePersonId = 0;
                int dislikePersonId = 0;
                string personName = "";

                if (Command_Code.ToUpper() == "GACH1")
                {
                    votePersonId = 2;
                    dislikePersonId = 1;
                    personName = "Mai Tho";
                }
                else if (Command_Code.ToUpper() == "GACH2")
                {
                    votePersonId = 1;
                    dislikePersonId = 2;
                    personName = "Linh Miu";
                }

                #region LUU USER DK NEM GACH

                var entity = new VoteRegisteredInfo();
                entity.User_ID = User_ID;
                entity.Request_ID = Request_ID;
                entity.Service_ID = Service_ID;
                entity.Command_Code = Command_Code;
                entity.Service_Type = 1;
                entity.Charging_Count = 0;
                entity.FailedChargingTime = 0;
                entity.RegisteredTime = DateTime.Now;
                entity.ExpiredTime = DateTime.Now.AddDays(1);
                entity.Registration_Channel = "SMS";
                entity.Status = 1;
                entity.Operator = GetTelco(User_ID);

                entity.Vote_Count = 1;
                entity.Vote_PersonId = votePersonId;

                entity.IsDislike = 1;

                entity.Dislike_Count = 1;
                entity.Dislike_PersonId = dislikePersonId;

                DataTable dt = VoteRegisterController.VoteRegisterInsert(entity);
                DataTable dtDislike = VoteRegisterController.VoteRegisterDislikeInsert(entity);

                int dislikeCount = ConvertUtility.ToInt32(dtDislike.Rows[0]["DISLIKE_SUM"]);
                string dislikeTop = GetTopVote(dislikeCount);

                string messageReturn = AppEnv.GetSetting("Vote_Sms_RegisterSucessDislike_OtherTelco").Replace("PersonName", personName);
                messageReturn = messageReturn.Replace("DislikeCount", dislikeCount.ToString());
                messageReturn = messageReturn.Replace("DislikeTop", dislikeTop);

                //if (dt.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
                //{
                //    messageReturn = AppEnv.GetSetting("Vote_Sms_RegisterSucess_Dislike").Replace("PersonName", personName);
                //}
                //else if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                //{
                //    messageReturn = AppEnv.GetSetting("Vote_Sms_AlreadyRegister_Dislike").Replace("PersonName", personName);
                //}

                #region Log Doanh Thu

                var e = new VoteChargedUserLogInfo();

                e.User_ID = User_ID;
                e.Request_ID = Request_ID;
                e.Service_ID = Service_ID;
                e.Command_Code = Command_Code;
                e.Service_Type = 1;
                e.Charging_Count = 0;
                e.FailedChargingTime = 0;
                e.RegisteredTime = DateTime.Now;
                e.ExpiredTime = DateTime.Now.AddDays(1);
                e.Registration_Channel = "SMS";
                e.Status = 1;
                e.Operator = GetTelco(User_ID);
                e.Reason = "Succ";
                e.Price = 2000;
                e.Vote_PersonId = votePersonId;

                VoteRegisterController.VoteChargedUserLogInsert(e);

                #endregion

                SendMt(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG

                #endregion
            }
            else if(Command_Code.ToUpper() == "HENHO" && subcode == "")
            {
                string messageReturn = AppEnv.GetSetting("Vote_Sms_Faq_OtherTelco");

                #region Log Doanh Thu

                var e = new VoteChargedUserLogInfo();

                e.User_ID = User_ID;
                e.Request_ID = Request_ID;
                e.Service_ID = Service_ID;
                e.Command_Code = Command_Code;
                e.Service_Type = 1;
                e.Charging_Count = 0;
                e.FailedChargingTime = 0;
                e.RegisteredTime = DateTime.Now;
                e.ExpiredTime = DateTime.Now.AddDays(1);
                e.Registration_Channel = "SMS";
                e.Status = 1;
                e.Operator = GetTelco(User_ID);
                e.Reason = "Succ";
                e.Price = 2000;
                e.Vote_PersonId = 0;

                VoteRegisterController.VoteChargedUserLogInsert(e);

                #endregion

                SendMt(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG
            }
            else//Tin Nhan Sai Cu Phap
            {
                returnMessage = AppEnv.GetSetting("Vote_Sms_ErrorSystax_OtherTelco");
                SendMt(User_ID, Service_ID, Command_Code, returnMessage, Request_ID);
            }
        }
        catch (Exception ex)
        {
            responseValue = "1";
            log.Debug("---------------Error sentMT----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }

        return responseValue;
    }

    private string ExcecuteRequestVoteNew8579MoVnm(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        string responseValue = "1";
        int returnValue = 0;

        Message = Message.ToUpper();
        string subcode = "";
        if (Message.Trim().Length > Command_Code.Trim().Length)
        {
            subcode = Message.ToUpper().Substring(Command_Code.Length).Replace(" ", "");
        }

        Command_Code = Command_Code.ToUpper();

        try
        {
            log.Debug(" ");
            log.Debug(" ");
            log.Debug("--------------------VOTE NEW VNM-------------------------");
            log.Debug("User_ID: " + User_ID);
            log.Debug("Service_ID: " + Service_ID);
            log.Debug("Command_Code: " + Command_Code);
            log.Debug("Message: " + Message.ToUpper());
            log.Debug("Request_ID: " + Request_ID);
            log.Debug(" ");
            log.Debug(" ");

            #region LOG SMS MO

            VoteSmsMoInfo moInfo = new VoteSmsMoInfo();
            moInfo.User_ID = User_ID;
            moInfo.Request_ID = Request_ID;
            moInfo.Service_ID = Service_ID;
            moInfo.Command_Code = Command_Code;
            moInfo.Message = Message;
            moInfo.Operator = GetTelco(User_ID);

            VoteRegisterController.NewVoteSmsMoInsert(moInfo);

            #endregion

            if (Command_Code.ToUpper() == "HUY1" && subcode.ToUpper() == "VOTE1")
            {
                //HUY DICH VU
                DataTable dt = VoteRegisterController.NewVoteRegisterUserLock(User_ID);
                string message = "";

                if (dt.Rows[0]["RETURN_ID"].ToString() == "0")//CHUA DK USER
                {
                    message = AppEnv.GetSetting("New_Vote_HuyDichVuChuaDangKy");
                }
                else if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                {
                    message = AppEnv.GetSetting("New_Vote_HuyDichVuDaDangKy");
                }

                #region Log Doanh Thu

                NewVoteLogDoanhThu(User_ID, Request_ID, Service_ID, Command_Code);

                #endregion

                SendMtNewVote(User_ID, Service_ID, Command_Code, message, Request_ID);
            }
            else if ( Command_Code.ToUpper() == "VOTE1" && subcode == "")
            {
                var entity = new VoteRegisteredInfo();
                entity.User_ID = User_ID;
                entity.Request_ID = Request_ID;
                entity.Service_ID = Service_ID;
                entity.Command_Code = Command_Code;
                entity.Service_Type = 1;
                entity.Charging_Count = 0;
                entity.FailedChargingTime = 0;
                entity.RegisteredTime = DateTime.Now;
                entity.ExpiredTime = DateTime.Now.AddDays(1);
                entity.Registration_Channel = "SMS";
                entity.Status = 1;
                entity.Operator = GetTelco(User_ID);
                entity.Vote_Count = 1;

                entity.Vote_PersonId = 0;
                entity.IsDislike = 0;

                DataTable value = VoteRegisterController.NewVoteRegisterInsert(entity);
                string messageReturn;
                int voteCount = ConvertUtility.ToInt32(value.Rows[0]["VOTE_SUM"]);
                string voteTop = GetTopVote(voteCount);

                if (value.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
                {
                    messageReturn = AppEnv.GetSetting("New_Vote_DangKyThanhCong");
                    SendMtNewVote(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG LAN 1

                    string contentMt2 = AppEnv.GetSetting("New_Vote_ThongBaoLuotVote");
                    contentMt2 = contentMt2.Replace("VoteCount", voteCount.ToString());
                    contentMt2 = contentMt2.Replace("VoteTop", voteTop);
                    SendMtNewVoteNoCharged(User_ID, Service_ID, Command_Code, contentMt2, Request_ID);//SENT MT CHO KHACH HANG LAN 2
                }
                else if (value.Rows[0]["RETURN_ID"].ToString() == "1")//DA DK DICH VU (UPDATE LUOT VOTE HIEN TAI)
                {
                    messageReturn = AppEnv.GetSetting("New_Vote_ThongBaoLuotVote");
                    messageReturn = messageReturn.Replace("VoteCount", voteCount.ToString());
                    messageReturn = messageReturn.Replace("VoteTop", voteTop);

                    SendMtNewVote(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG
                }

                #region Log Doanh Thu

                NewVoteLogDoanhThu(User_ID, Request_ID, Service_ID, Command_Code);

                #endregion
                
            }
            else if (Command_Code.ToUpper() == "HENHO" && subcode == "")
            {
                string messageReturn = AppEnv.GetSetting("New_Vote_HuongDan");

                SendMtNewVote(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG

                #region Log Doanh Thu

                NewVoteLogDoanhThu(User_ID, Request_ID, Service_ID, Command_Code);

                #endregion
            }
            else//TIN NHAN SAI CU PHAP
            {
                string messageReturn = AppEnv.GetSetting("New_Vote_SaiCuPhap");
                SendMtNewVote(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG
            }
        }
        catch (Exception ex)
        {
            responseValue = "1";
            log.Debug("---------------Error VOTE NEW----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }

        return responseValue;
    }

    private string ExcecuteRequestVoteNew8579OtherTelco(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        string responseValue = "1";
        int returnValue = 0;

        Message = Message.ToUpper();
        string subcode = "";
        if (Message.Trim().Length > Command_Code.Trim().Length)
        {
            subcode = Message.ToUpper().Substring(Command_Code.Length).Replace(" ", "");
        }

        Command_Code = Command_Code.ToUpper();

        try
        {
            log.Debug(" ");
            log.Debug(" ");
            log.Debug("--------------------VOTE OTHER TELCO-------------------------");
            log.Debug("User_ID: " + User_ID);
            log.Debug("Service_ID: " + Service_ID);
            log.Debug("Command_Code: " + Command_Code);
            log.Debug("Message: " + Message.ToUpper());
            log.Debug("Request_ID: " + Request_ID);
            log.Debug(" ");
            log.Debug(" ");

            #region LOG SMS MO

            VoteSmsMoInfo moInfo = new VoteSmsMoInfo();
            moInfo.User_ID = User_ID;
            moInfo.Request_ID = Request_ID;
            moInfo.Service_ID = Service_ID;
            moInfo.Command_Code = Command_Code;
            moInfo.Message = Message;
            moInfo.Operator = GetTelco(User_ID);

            VoteRegisterController.VoteSmsMoInsert(moInfo);

            #endregion

            if (Command_Code.ToUpper() == "VOTE1" && subcode == "")
            {
                var entity = new VoteRegisteredInfo();
                entity.User_ID = User_ID;
                entity.Request_ID = Request_ID;
                entity.Service_ID = Service_ID;
                entity.Command_Code = Command_Code;
                entity.Service_Type = 1;
                entity.Charging_Count = 0;
                entity.FailedChargingTime = 0;
                entity.RegisteredTime = DateTime.Now;
                entity.ExpiredTime = DateTime.Now.AddDays(1);
                entity.Registration_Channel = "SMS";
                entity.Status = 1;
                entity.Operator = GetTelco(User_ID);
                entity.Vote_Count = 1;

                entity.Vote_PersonId = 0;
                entity.IsDislike = 0;

                DataTable value = VoteRegisterController.NewVoteRegisterInsert(entity);
                int voteCount = ConvertUtility.ToInt32(value.Rows[0]["VOTE_SUM"]);
                string voteTop = GetTopVote(voteCount);

                string messageReturn = AppEnv.GetSetting("New_Vote_ThongBaoLuotVote_OtherTelco");
                messageReturn = messageReturn.Replace("VoteCount", voteCount.ToString());
                messageReturn = messageReturn.Replace("VoteTop", voteTop);

                SendMtNewVote(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG

                #region Log Doanh Thu

                NewVoteLogDoanhThu(User_ID, Request_ID, Service_ID, Command_Code);

                #endregion

            }
            else if(Command_Code == "HENHO")
            {
                string messageReturn = AppEnv.GetSetting("New_Vote_HuongDan_OtherTelco");

                SendMtNewVote(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG

                #region Log Doanh Thu

                NewVoteLogDoanhThu(User_ID, Request_ID, Service_ID, Command_Code);

                #endregion
            }
            else//SAI CU PHAP
            {
                string messageReturn = AppEnv.GetSetting("New_Vote_SaiCuPhap_OtherTelco");

                SendMtNewVote(User_ID, Service_ID, Command_Code, messageReturn, Request_ID);//SENT MT CHO KHACH HANG
            }
        }
        catch (Exception ex)
        {
            responseValue = "1";
            log.Debug("---------------Error VOTE NEW----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }

        return responseValue;
    }

    #endregion

    #region Public Methods

    public string PaymentWorldCupChargingOptimize(string userId,string commandCode)
    {

        //return "1|500";

        var webServiceCharging3G = new WebServiceCharging3g();

        string notEnoughMoney = AppEnv.GetSetting("NotEnoughMoney");
        string serviceName = "World_Cup";

        string price = "5000";
        string msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, serviceName);

        if (msgReturn.Trim() == notEnoughMoney)
        {
            price = "3000";
            msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, serviceName);

            if (msgReturn.Trim() == notEnoughMoney)
            {
                price = "2000";
                msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, serviceName);

                if (msgReturn.Trim() == notEnoughMoney)
                {
                    price = "1000";
                    msgReturn = webServiceCharging3G.PaymentVnm(userId, price, commandCode, serviceName);
                }

            }
        }

        return msgReturn + "|" + price;
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

    private static void SendMt(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        var objSentMt = new ServiceProviderService();

        string message = Message;

        if (AppEnv.GetSetting("TestFlag") == "0")
        {
            objSentMt.sendMT(User_ID, message, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
        }

        var objMt = new VoteSmsMtInfo();
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
        objMt.PartnerId = "Xzone";
        objMt.Operator = GetTelco(User_ID);
        
        VoteRegisterController.VoteSmsMtInsert(objMt);

    }

    private static void SendMtSecret(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        var objSentMt = new ServiceProviderService();

        string message = Message;

        if (AppEnv.GetSetting("TestFlag") == "0")
        {
            objSentMt.sendMT(User_ID, message, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
        }

        var objMt = new VoteSmsMtInfo();
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
        objMt.PartnerId = "Xzone";
        objMt.Operator = GetTelco(User_ID);

        VoteRegisterController.SecretSmsMtInsert(objMt);

    }

    private static void SendMtNewVote(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        var objSentMt = new ServiceProviderService();

        string message = Message;

        if (AppEnv.GetSetting("TestFlag") == "0")
        {
            objSentMt.sendMT(User_ID, message, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
        }

        var objMt = new VoteSmsMtInfo();
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
        objMt.PartnerId = "Xzone";
        objMt.Operator = GetTelco(User_ID);

        VoteRegisterController.NewVoteSmsMtInsert(objMt);

    }

    private static void SendMtNewVoteNoCharged(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        var objSentMt = new ServiceProviderService();

        string message = Message;

        if (AppEnv.GetSetting("TestFlag") == "0")
        {
            objSentMt.sendMT(User_ID, message, Service_ID, Command_Code, "0", Request_ID, "1", "1", "0", "0");
        }

        var objMt = new VoteSmsMtInfo();
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
        objMt.PartnerId = "Xzone";
        objMt.Operator = GetTelco(User_ID);

        VoteRegisterController.NewVoteSmsMtInsert(objMt);
    }

    private static string GetTopVote(int vote)
    {
        string str = "";

        if(vote >= 1 && vote <= 10)
        {
            str = "100";
        }
        else if(vote >= 11 && vote <= 50)
        {
            str = "50";
        }
        else if(vote >= 51)
        {
            str = "10";
        }

        return str;
    }

    private void NewVoteLogDoanhThu(string user_Id,string request_Id,string service_Id,string command_Code)
    {
        #region Log Doanh Thu

        var e = new VoteChargedUserLogInfo();

        e.User_ID = user_Id;
        e.Request_ID = request_Id;
        e.Service_ID = service_Id;
        e.Command_Code = command_Code;
        e.Service_Type = 1;
        e.Charging_Count = 0;
        e.FailedChargingTime = 0;
        e.RegisteredTime = DateTime.Now;
        e.ExpiredTime = DateTime.Now.AddDays(1);
        e.Registration_Channel = "SMS";
        e.Status = 1;
        e.Operator = GetTelco(user_Id);
        e.Reason = "Succ";
        e.Price = 5000;
        e.Vote_PersonId = 0;

        VoteRegisterController.NewVoteChargedUserLogInsert(e);

        #endregion
    }

    private void WorldCupLogDoanhThu(string user_Id, string request_Id, string service_Id, string command_Code,string price,string reason)
    {
        #region Log Doanh Thu

        var e = new VoteChargedUserLogInfo();

        e.User_ID = user_Id;
        e.Request_ID = request_Id;
        e.Service_ID = service_Id;
        e.Command_Code = command_Code;
        e.Service_Type = 1;
        e.Charging_Count = 0;
        e.FailedChargingTime = 0;
        e.RegisteredTime = DateTime.Now;
        e.ExpiredTime = DateTime.Now.AddDays(1);
        e.Registration_Channel = "SMS";
        e.Status = 1;
        e.Operator = GetTelco(user_Id);
        e.Reason = reason;

        e.Price = ConvertUtility.ToInt32(price);

        e.Vote_PersonId = 0;

        VoteRegisterController.WorldCupChargedUserLogInsert(e);

        #endregion
    }

    private static void WorldCupSendMt(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID,string messageType)
    {
        var objSentMt = new ServiceProviderService();

        string message = Message;

        if (AppEnv.GetSetting("TestFlag") == "0")
        {
            objSentMt.sendMT(User_ID, message, Service_ID, Command_Code, messageType, Request_ID, "1", "1", "0", "0");
        }

        var objMt = new VoteSmsMtInfo();
        objMt.User_ID = User_ID;
        objMt.Message = message;
        objMt.Service_ID = Service_ID;
        objMt.Command_Code = Command_Code;
        objMt.Message_Type = ConvertUtility.ToInt32(messageType);
        objMt.Request_ID = Request_ID;
        objMt.Total_Message = 1;
        objMt.Message_Index = 0;
        objMt.IsMore = 0;
        objMt.Content_Type = 0;
        objMt.ServiceType = 0;
        objMt.ResponseTime = DateTime.Now;
        objMt.IsLock = 0;
        objMt.PartnerId = "Xzone";
        objMt.Operator = GetTelco(User_ID);

        VoteRegisterController.WorldCupSmsMtInsert(objMt);

    }

    private void WorldCupLogMo(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID, int isCharged)
    {
        var moInfo = new SMS_MOInfo();

        moInfo.User_ID = User_ID;
        moInfo.Service_ID = Service_ID;
        moInfo.Command_Code = Command_Code;
        moInfo.Message = Message;
        moInfo.Request_ID = Request_ID;
        moInfo.Operator = GetTelco(User_ID);
        moInfo.IsCharged = isCharged;

        SMS_MODB.WorldCupInsertMo(moInfo);
    }

    #endregion

}
