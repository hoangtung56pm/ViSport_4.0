#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\Handler.ashx" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BE26E12495C94BC843A2623C63A5E748AE476A5D"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\Handler.ashx"


using System;
using System.Data;
using System.Web;
using System.Web.Script.Serialization;
using SMSManager_API.Library.Utilities;
using WS_SDPGPC.Library.Content;

public class Handler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        //context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        
        string serviceCode = context.Request.QueryString["serviceCode"];
        
        if(!string.IsNullOrEmpty(serviceCode))
        {
            serviceCode = serviceCode.ToUpper();
        }
        
        string msisdn = context.Request.QueryString["msisdn"];
        string type = context.Request.QueryString["Type"];

        string serviceId = "8979";
        
        const string reType = "text";
        string reContent = string.Empty;

        log4net.ILog log = log4net.LogManager.GetLogger("File");
        log.Debug(" ");
        log.Debug(" ");
        log.Debug("--------------------VOTE MOBI FROM Mr.T-------------------------");
        log.Debug("User_ID: " + msisdn);
        log.Debug("Command_Code: " + serviceCode);
        log.Debug("Type: " + type);
        log.Debug(" ");
        log.Debug(" ");
        
        
        if(serviceCode == "V1" || serviceCode == "V2" || serviceCode == "G1" || serviceCode == "G2" )//DV VOTE1 & VOTE2
        {
            int votePersonId = 1;
            string personName = "Mai Tho";
            string commandCode = "VOTE1";

            int dislikePersonId = 0;

            if (serviceCode == "V2")
            {
                votePersonId = 2;
                personName = "Linh Miu";
                commandCode = "VOTE2";
            }
            else if (serviceCode == "G1")
            {
                votePersonId = 2;
                dislikePersonId = 1;
                personName = "Mai Tho";
                commandCode = "GACH1";
            }
            else if (serviceCode == "G2")
            {
                votePersonId = 1;
                dislikePersonId = 2;
                personName = "Linh Miu";
                commandCode = "GACH2";
            }
            
            if(type == "1")//DK DV LAN DAU
            {
                var entity = new VoteRegisteredInfo();
                entity.User_ID = msisdn;
                entity.Request_ID = "0";
                entity.Service_ID = serviceId;
                entity.Command_Code = commandCode;
                entity.Service_Type = 1;
                entity.Charging_Count = 0;
                entity.FailedChargingTime = 0;
                entity.RegisteredTime = DateTime.Now;
                entity.ExpiredTime = DateTime.Now.AddDays(1);
                entity.Registration_Channel = "SMS";
                entity.Status = 1;
                entity.Operator = "vms";
                entity.Vote_Count = 1;

                entity.Vote_PersonId = votePersonId;
                entity.IsDislike = dislikePersonId;

                if(serviceCode == "V1" || serviceCode == "V2")
                {
                    DataTable dt = VoteRegisterController.NewVoteRegisterInsert(entity);
                    
                    if (dt.Rows[0]["RETURN_ID"].ToString() == "0")//CHUA DK USER
                    {
                        reContent = AppEnv.GetSetting("Vote_Sms_RegisterSucess_Mt2_Like_Mobi").Replace("VoteCount", "1").Replace("VoteTop", "100");
                    }
                    else if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                    {
                        int voteCount = ConvertUtility.ToInt32(dt.Rows[0]["VOTE_SUM"]);
                        string voteTop = GetTopVote(voteCount);
                        reContent = AppEnv.GetSetting("Vote_Sms_RegisterSucess_Mt2_Like_Mobi").Replace("VoteCount", voteCount.ToString()).Replace("VoteTop", voteTop);
                    }

                    #region LOG DOANH THU

                    NewVoteLogDoanhThu(msisdn, "0", serviceId, "VOTE1");
                    
                    #endregion
                    
                }
                //else
                //{
                //    DataTable dt = VoteRegisterController.VoteRegisterInsert(entity);
                //    DataTable dtDislike = VoteRegisterController.VoteRegisterDislikeInsert(entity);

                //    if (dt.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
                //    {
                //        reContent = AppEnv.GetSetting("Vote_Sms_RegisterSucess_Mt2_Like_Mobi").Replace("PersonName", personName);
                //    }
                //    else if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                //    {
                //        reContent = AppEnv.GetSetting("Vote_Sms_AlreadyRegister_Mobi");
                //    }
                //}
            }
            else if(type == "2")//UPDATE LUOT VOTE (SUB HANG NGAY)
            {
                DataTable info = VoteRegisterController.NewVoteGetUserInfo(msisdn);
                if (info != null && info.Rows.Count > 0)
                {
                    var logInfo = new ViSport_S2_Charged_Users_LogInfo();

                    logInfo.ID = ConvertUtility.ToInt32(info.Rows[0]["ID"].ToString());
                    logInfo.User_ID = msisdn;
                    logInfo.Request_ID = info.Rows[0]["Request_ID"].ToString();
                    logInfo.Service_ID = info.Rows[0]["Service_ID"].ToString();
                    logInfo.Command_Code = info.Rows[0]["Command_Code"].ToString();
                    logInfo.Service_Type = 0;//Charged Sub Service_Type
                    logInfo.Charging_Count = ConvertUtility.ToInt32(info.Rows[0]["Charging_Count"].ToString());
                    logInfo.FailedChargingTimes = ConvertUtility.ToInt32(info.Rows[0]["FailedChargingTimes"].ToString());
                    logInfo.RegisteredTime = DateTime.Now;
                    logInfo.ExpiredTime = DateTime.Now.AddDays(1);
                    logInfo.Registration_Channel = info.Rows[0]["Registration_Channel"].ToString();
                    logInfo.Status = ConvertUtility.ToInt32(info.Rows[0]["Status"].ToString());
                    logInfo.Operator = info.Rows[0]["Operator"].ToString();
                    logInfo.Price = 2000;
                    logInfo.Vote_PersonId = ConvertUtility.ToInt32(info.Rows[0]["Vote_PersonId"].ToString());
                    logInfo.Reason = "Succ";
                    
                    if(serviceCode == "V1" || serviceCode == "V2")
                    {
                        VoteRegisterController.NewInsertLogLike(logInfo);

                        DataTable dt = VoteRegisterController.NewVoteGetUserInfo(msisdn);
                        int voteCount = ConvertUtility.ToInt32(dt.Rows[0]["Vote_Count"]);
                        string voteTop = GetTopVote(voteCount);

                        reContent = AppEnv.GetSetting("Vote_Sms_RegisterSucess_Mt2_Like_Mobi").Replace("VoteCount", voteCount.ToString()).Replace("VoteTop", voteTop);
                        
                        //DataTable dt = VoteRegisterController.GetVoteAccountInfo(msisdn, info.Rows[0]["Command_Code"].ToString());
                        //reContent = AppEnv.GetSetting("Vote_Sms_ChargedSubSucess_Like").Replace("PersonName", dt.Rows[0]["Name"].ToString());
                        //reContent = reContent.Replace("VoteCount", dt.Rows[0]["Count"].ToString());
                        //reContent = reContent.Replace("VoteTop", dt.Rows[0]["Top"].ToString());
                        
                    }
                    //else if(serviceCode == "G1" || serviceCode == "G2")
                    //{
                    //    if (info.Rows[0]["Vote_PersonId"].ToString() == "1")
                    //    {
                    //        logInfo.Vote_PersonId = 2;
                    //    }
                    //    else if (info.Rows[0]["Vote_PersonId"].ToString() == "2")
                    //    {
                    //        logInfo.Vote_PersonId = 1;
                    //    }
                    //    VoteRegisterController.InsertLogDisLike(logInfo);
                    //    DataTable dt = VoteRegisterController.GetVoteAccountInfo(msisdn, info.Rows[0]["Command_Code"].ToString());

                    //    reContent = AppEnv.GetSetting("Vote_Sms_ChargedSubSucess_UnLike").Replace("PersonName", dt.Rows[0]["Name"].ToString());
                    //    reContent = reContent.Replace("DislikeCount", dt.Rows[0]["Count"].ToString());
                    //    reContent = reContent.Replace("DislikeTop", dt.Rows[0]["Top"].ToString());
                    //}
                }
            }
            else if(type == "3")//HUY DICH VU
            {
                
                DataTable dt = VoteRegisterController.NewVoteRegisterUserLock(msisdn);

                if (dt.Rows[0]["RETURN_ID"].ToString() == "0")//CHUA DK USER
                {
                    reContent = AppEnv.GetSetting("Vote_Sms_LockUserError_Mobi");
                }
                else if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                {
                    reContent = AppEnv.GetSetting("Vote_Sms_LockUserSuccess_Mobi");
                }
                //DataTable dt = VoteRegisterController.VoteRegisterUserLock(msisdn, 1);

                //if (dt.Rows[0]["RETURN_ID"].ToString() == "0")//CHUA DK USER
                //{
                //    reContent = AppEnv.GetSetting("Vote_Sms_LockUserError_Mobi");
                //}
                //else if (dt.Rows[0]["RETURN_ID"].ToString() == "1")
                //{
                //    reContent = AppEnv.GetSetting("Vote_Sms_LockUserSuccess_Mobi");
                //}
                
            }
        }
        
        if(serviceCode == "T1")//DV GAME THANH_NU
        {
            if(type == "1")//DK DV LAN DAU
            {
                var entity = new ThanhNuRegisteredUsers();
                entity.UserId = msisdn;
                entity.RequestId = "0";
                entity.ServiceId = "2288";
                entity.CommandCode = "DK";
                entity.ServiceType = 1;
                entity.ChargingCount = 0;
                entity.FailedChargingTimes = 0;
                entity.RegisteredTime = DateTime.Now;
                entity.ExpiredTime = DateTime.Now.AddDays(1);
                entity.RegistrationChannel = "SMS";
                entity.Status = 1;
                entity.Operator = GetTelco(msisdn);

                #region GOI HAM DK BEN DOI TAC

                string partnerResult = AppEnv.ThanhNuDangKy(msisdn);

                log.Debug(" ");
                log.Debug("**********");
                log.Debug("Partner_Thanh_Nu_smsKichHoat : " + partnerResult);
                log.Debug("**********");
                log.Debug(" ");

                string[] arrValue = partnerResult.Split('|');
                if (arrValue[0].Trim() == "1")
                {
                    ViSport_S2_Registered_UsersController.ThanhNuRegisterUserStatusUpdate(msisdn, 1);
                    reContent = "Ban da la thanh vien cua Game Thanh Nu. Click vao link sau de dang nhap Game " + arrValue[1];
                }
                else if (arrValue[0].Trim() == "0")
                {
                    DataTable value = ViSport_S2_Registered_UsersController.ThanhNuRegisterUserInsert(entity);
                    if (value.Rows[0]["RETURN_ID"].ToString() == "0")
                    {
                        reContent = "Chuc mung Quy Khach da dang ky thanh cong Game Thanh Nu Gia cuoc 1000d-ngay, Goi dich vu se duoc tu dong gia han hang ngay.Kich hoat tai khoan " + arrValue[1] + " .De huy dang ky, Quy Khach soan HUY TN gui 2288.";
                    }
                    else if (value.Rows[0]["RETURN_ID"].ToString() == "1")
                    {
                        ViSport_S2_Registered_UsersController.ThanhNuRegisterUserStatusUpdate(msisdn, 1);
                        reContent = "Ban da la thanh vien cua Game Thanh Nu. Click vao link sau de dang nhap Game " + arrValue[1];
                    }
                }

                #endregion
            }
            else if(type == "2")//CHARGED SUB
            {
                var partnerService = new vn.thanhnu.Service();

                DataTable dtUser = ViSport_S2_Registered_UsersController.ThanhNuGetUserInfo(msisdn);
                if(dtUser != null && dtUser.Rows.Count > 0)
                {
                    string partnerResult = partnerService.smsGiaHan(msisdn, "1");
                    if (partnerResult.Trim() == "1")
                    {
                        reContent = "Goi dich vu Game Thanh Nu  cua Quy Khach da duoc gia han thanh cong. Quy khach duoc cong 110 G_Coin vao tk. Cam on Quy Khach da su dung goi dich vu .";
                    }

                    #region LOG DOANH THU

                    //LOG DOANH THU
                    var e = new ThanhNuChargedUserLogInfo();

                    e.ID = ConvertUtility.ToInt32(dtUser.Rows[0]["ID"].ToString());
                    e.User_ID = msisdn;
                    e.Request_ID = "0";
                    e.Service_ID = serviceId;
                    e.Command_Code = "DK";
                    e.Service_Type = 0;
                    e.Charging_Count = 0;
                    e.FailedChargingTime = 0;
                    e.RegisteredTime = DateTime.Now;
                    e.ExpiredTime = DateTime.Now.AddDays(1);
                    e.Registration_Channel = "SMS";
                    e.Status = 1;
                    e.Operator = GetTelco(msisdn);

                    e.Reason = "Succ";

                    e.Price = 1000;
                    e.PartnerResult = partnerResult;

                    ViSport_S2_Registered_UsersController.ThanhNuChargedUserLog(e);

                    #endregion                    
                }

            }
            else if(type == "3")//HUY DICH VU
            {
                #region GOI HAM HUY BEN DOI TAC

                string partnerResult = AppEnv.ThanhNuHuy(msisdn);
                if (partnerResult.Trim() == "1")
                {
                    DataTable value = ViSport_S2_Registered_UsersController.ThanhNuRegisterUserStatusUpdate(msisdn, 0);
                    if (value.Rows[0]["RETURN_ID"].ToString() == "1")
                    {
                        reContent = "Toan bo tai khoan Game Thanh Nu cua Quy khach se bi huy.De dang ki lai Qk vui long soan tin DK TN gui 2288";
                    }
                }

                #endregion
            }
        }

        
        
        var aSerializer = new JavaScriptSerializer();
        
        var obj = new MT();
        var ct1 = new ContentInfo();
        obj.List = new ContentInfo[1];
        
        ct1.Type = reType;
        ct1.Content = reContent;
        obj.List[0] = ct1;
        
        string strReturn = aSerializer.Serialize(obj);

        log.Debug(" ");
        log.Debug(" ");
        log.Debug("strReturn: " + strReturn);
        log.Debug(" ");
        log.Debug(" ");
        
        context.Response.ContentType = "text/html";
        context.Response.Write(strReturn);
        
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    private void NewVoteLogDoanhThu(string user_Id, string request_Id, string service_Id, string command_Code)
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
        e.Price = 2000;
        e.Vote_PersonId = 0;

        VoteRegisterController.NewVoteChargedUserLogInsert(e);

        #endregion
    }

    private static string GetTopVote(int vote)
    {
        string str = "";

        if (vote >= 1 && vote <= 10)
        {
            str = "100";
        }
        else if (vote >= 11 && vote <= 50)
        {
            str = "50";
        }
        else if (vote >= 51)
        {
            str = "10";
        }

        return str;
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
