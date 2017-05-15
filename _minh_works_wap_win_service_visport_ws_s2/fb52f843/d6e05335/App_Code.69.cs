#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\ViSport_S2_Registered\ViSport_S2_Registered_UsersController.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C21EBDFF7BCBA0456AE0AA5DF0FB39856C715BC3"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\ViSport_S2_Registered\ViSport_S2_Registered_UsersController.cs"
using System;
using System.Collections.Generic;

using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using log4net;
using Microsoft.ApplicationBlocks.Data;
using SMSManager_API.Library.Utilities;

/// <summary>
/// Summary description for ViSport_S2_Registered_UsersController
/// </summary>
public class ViSport_S2_Registered_UsersController
{
    public ViSport_S2_Registered_UsersController()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region viSport

    public static DataTable Insert(ViSport_S2_Registered_UsersInfo _viSport_S2_Registered_UsersInfo)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "ViSport_S2_Registered_Users_Insert", _viSport_S2_Registered_UsersInfo.User_ID
        , _viSport_S2_Registered_UsersInfo.Request_ID
       , _viSport_S2_Registered_UsersInfo.Service_ID
       , _viSport_S2_Registered_UsersInfo.Command_Code
        , _viSport_S2_Registered_UsersInfo.Service_Type
        , _viSport_S2_Registered_UsersInfo.Charging_Count
        , _viSport_S2_Registered_UsersInfo.FailedChargingTimes
        , _viSport_S2_Registered_UsersInfo.RegisteredTime
       , _viSport_S2_Registered_UsersInfo.ExpiredTime
        , _viSport_S2_Registered_UsersInfo.Registration_Channel
       , _viSport_S2_Registered_UsersInfo.Status
        , _viSport_S2_Registered_UsersInfo.Operator);
        if (ds != null && ds.Tables.Count > 0)
        {
            //return ConvertUtility.ToInt32(ds.Tables[0].Rows[0]["RETURN_ID"]);
            return ds.Tables[0];
        }
        return null;

    }

    public static DataTable InsertSmsSpamUser(ViSport_S2_Registered_SpamSms_UserInfo entity)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "ViSport_S2_SpamSMS_Users_Insert"
            , entity.User_Id
            , entity.Request_Id
            , entity.Service_Id
            , entity.Command_Code
            , entity.Sub_Code
            , entity.Service_Type
            , entity.Charging_Count
            , entity.FailedChargingTimes
            , entity.RegisteredTime
            //, entity.ExpiredTime
            , entity.Registration_Channel
            , entity.Status
            , entity.Operator);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable Update(ViSport_S2_Registered_UsersInfo _viSport_S2_Registered_UsersInfo)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "ViSport_S2_Registered_Users_Update", _viSport_S2_Registered_UsersInfo.User_ID
        , _viSport_S2_Registered_UsersInfo.Status
       , _viSport_S2_Registered_UsersInfo.Service_Type);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable OffSmsSpam(string userId,int status)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "ViSport_S2_SpamSMS_User_UpdateStatus", userId, status);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable DeleteSmsSpamUser(string userId,string subCode)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "ViSport_SpamSMS_Users_Delete", userId, subCode);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static bool UpdateAlreadyOff(string userId, int serviceType, DateTime expiredTime)
    {
        try
        {
            SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "ViSport_S2_Registered_User_Already_OFF_Update", userId,
                                      serviceType, expiredTime);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public static DataTable CheckAlreadyOff(string userId, int serviceType)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "ViSport_S2_Registered_Users_OFF_Already", userId, serviceType);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }
        
    #endregion

    #region VClip

    public static DataTable InsertVClip(ViSport_S2_Registered_UsersInfo viSportS2RegisteredUsersInfo)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "VClip_S2_Registered_Users_Insert", viSportS2RegisteredUsersInfo.User_ID
        , viSportS2RegisteredUsersInfo.Request_ID
       , viSportS2RegisteredUsersInfo.Service_ID
       , viSportS2RegisteredUsersInfo.Command_Code
        , viSportS2RegisteredUsersInfo.Service_Type
        , viSportS2RegisteredUsersInfo.Charging_Count
        , viSportS2RegisteredUsersInfo.FailedChargingTimes
        , viSportS2RegisteredUsersInfo.RegisteredTime
       , viSportS2RegisteredUsersInfo.ExpiredTime
        , viSportS2RegisteredUsersInfo.Registration_Channel
       , viSportS2RegisteredUsersInfo.Status
        , viSportS2RegisteredUsersInfo.Operator);
        if (ds != null && ds.Tables.Count > 0)
        {
            //return ConvertUtility.ToInt32(ds.Tables[0].Rows[0]["RETURN_ID"]);
            return ds.Tables[0];
        }
        return null;

    }

    public static DataTable InsertVClipNew(ViSport_S2_Registered_UsersInfo viSportS2RegisteredUsersInfo)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "VClip_S2_Registered_Users_Insert_New", viSportS2RegisteredUsersInfo.User_ID
        , viSportS2RegisteredUsersInfo.Request_ID
       , viSportS2RegisteredUsersInfo.Service_ID
       , viSportS2RegisteredUsersInfo.Command_Code
        , viSportS2RegisteredUsersInfo.Service_Type
        , viSportS2RegisteredUsersInfo.Charging_Count
        , viSportS2RegisteredUsersInfo.FailedChargingTimes
        , viSportS2RegisteredUsersInfo.RegisteredTime
       , viSportS2RegisteredUsersInfo.ExpiredTime
        , viSportS2RegisteredUsersInfo.Registration_Channel
       , viSportS2RegisteredUsersInfo.Status
        , viSportS2RegisteredUsersInfo.Operator);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable UpdateVClip(ViSport_S2_Registered_UsersInfo _viSport_S2_Registered_UsersInfo)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "VClip_S2_Registered_Users_Update", _viSport_S2_Registered_UsersInfo.User_ID
        , _viSport_S2_Registered_UsersInfo.Status);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable CheckAlreadyOffVClip(string userId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "VClip_S2_Registered_Users_OFF_Already", userId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static bool UpdateAlreadyOffVClip(string userId, DateTime expiredTime)
    {
        try
        {
            SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "VClip_S2_Registered_Users_Already_OFF_Update", userId, expiredTime);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public static DataTable VClipGetMTByStatus(bool isLock)
    {
        DataTable retVal = null;
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("VClip_GetAllUserByType", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.AddWithValue("@isLock", isLock);
        try
        {
            retVal = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(dbCmd);
            da.Fill(retVal);
        }
        finally
        {
            dbConn.Close();
        }
        return retVal;
    }

    public static void VClipInsertLog(ViSport_S2_Charged_Users_LogInfo viSportS2ChargedUsersLogInfo)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("VClip_S2_Charged_Users_Log_Insert_New", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.AddWithValue("@ID", viSportS2ChargedUsersLogInfo.ID);
        dbCmd.Parameters.AddWithValue("@User_ID", viSportS2ChargedUsersLogInfo.User_ID);
        dbCmd.Parameters.AddWithValue("@Request_ID", viSportS2ChargedUsersLogInfo.Request_ID);
        dbCmd.Parameters.AddWithValue("@Service_ID", viSportS2ChargedUsersLogInfo.Service_ID);
        dbCmd.Parameters.AddWithValue("@Command_Code", viSportS2ChargedUsersLogInfo.Command_Code);
        dbCmd.Parameters.AddWithValue("@Service_Type", viSportS2ChargedUsersLogInfo.Service_Type);
        dbCmd.Parameters.AddWithValue("@Charging_Count", viSportS2ChargedUsersLogInfo.Charging_Count);
        dbCmd.Parameters.AddWithValue("@FailedChargingTimes", viSportS2ChargedUsersLogInfo.FailedChargingTimes);
        dbCmd.Parameters.AddWithValue("@RegisteredTime", viSportS2ChargedUsersLogInfo.RegisteredTime);
        dbCmd.Parameters.AddWithValue("@ExpiredTime", viSportS2ChargedUsersLogInfo.ExpiredTime);
        dbCmd.Parameters.AddWithValue("@Registration_Channel", viSportS2ChargedUsersLogInfo.Registration_Channel);
        dbCmd.Parameters.AddWithValue("@Status", viSportS2ChargedUsersLogInfo.Status);
        dbCmd.Parameters.AddWithValue("@Operator", viSportS2ChargedUsersLogInfo.Operator);
        dbCmd.Parameters.AddWithValue("@Reason", viSportS2ChargedUsersLogInfo.Reason);

        dbCmd.Parameters.AddWithValue("@Price", viSportS2ChargedUsersLogInfo.Price);

        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
        }
        finally
        {
            dbConn.Close();
        }
    }

    #endregion

    #region VClip Free5Day

    public static DataTable Free5DayClipOff(string msisdn,int status)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "VClip_S2_Registered_Users_Update_Free5Day", msisdn, status);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    #endregion

    #region VNM

    public static void InsertVnmRegisterUser(VnmS2RegisterUserInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "VNM_S2_Registered_Users_Insert",
            entity.UserId,
            entity.RequestId,
            entity.ServiceId,
            entity.CommandCode,
            entity.SubCode,
            entity.Operator,
            entity.RegisteredChannel,
            entity.Status);
    }

    public static DataTable GetExistUser(string userId,string subCode)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "VNM_S2_Registered_Users_CheckExist", userId,
                                              subCode);
        if(ds != null)
        {
            return ds.Tables[0];
        }

        return null;
    }

    public static void DeleteVnmRegisterUser(string userId,string subCode)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "VNM_Registered_Users_Delete", userId, subCode);
    }

    public static DataTable GetCompanyCode(string companyCode)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVClip, "VNM_Wap_GetCompanyForS2", companyCode);
        if (ds != null)
        {
            return ds.Tables[0];
        }

        return null;
    }

    #endregion

    #region Sport Game Hero

    public static DataTable InsertSportGameHeroRegisterUser(ViSport_S2_Registered_UsersInfo entity)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_Registered_Users_Insert"

            , entity.User_ID
            , entity.Request_ID
            , entity.Service_ID
            , entity.Command_Code
            , entity.Service_Type
            , entity.Charging_Count
            , entity.FailedChargingTimes
            , entity.RegisteredTime
            , entity.ExpiredTime
            , entity.Registration_Channel
            , entity.Status
            , entity.Operator
            , entity.Point
            
            );

        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;

    }

    public static void InsertSportGameHeroChargedUserLog(SportGameHeroChargedUserLogInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Sport_Game_Hero_Charged_Users_Log_Insert"
                                     , entity.User_ID
                                     , entity.Request_ID
                                     , entity.Service_ID
                                     , entity.Command_Code
                                     , entity.Service_Type
                                     , entity.Charging_Count
                                     , entity.FailedChargingTime
                                     , entity.RegisteredTime
                                     , entity.ExpiredTime
                                     , entity.Registration_Channel
                                     , entity.Status
                                     , entity.Operator
                                     , entity.Reason
                                     , entity.Price
                                 );
    }

    public static void InsertSportGameHeroChargedUserLogForSub(SportGameHeroChargedUserLogInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Sport_Game_Hero_Charged_Users_Log_Insert_ForSub"
                                     , entity.ID
                                     , entity.User_ID
                                     , entity.Request_ID
                                     , entity.Service_ID
                                     , entity.Command_Code
                                     , entity.Service_Type
                                     , entity.Charging_Count
                                     , entity.FailedChargingTime
                                     , entity.RegisteredTime
                                     , entity.ExpiredTime
                                     , entity.Registration_Channel
                                     , entity.Status
                                     , entity.Operator
                                     , entity.Reason
                                     , entity.Price
                                 );
    }

    public static void InsertSportGameHeroChargedUserLogForSubBonus(SportGameHeroChargedUserLogInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Sport_Game_Hero_Charged_Users_Log_Insert_ForSub_Bonus"
                                     , entity.ID
                                     , entity.User_ID
                                     , entity.Request_ID
                                     , entity.Service_ID
                                     , entity.Command_Code
                                     , entity.Service_Type
                                     , entity.Charging_Count
                                     , entity.FailedChargingTime
                                     , entity.RegisteredTime
                                     , entity.ExpiredTime
                                     , entity.Registration_Channel
                                     , entity.Status
                                     , entity.Operator
                                     , entity.Reason
                                     , entity.Price
                                 );
    }

    public static DataTable GetSportGameHeroUserInfo(string userId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_GetUserInfo", userId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable GetQuestionInfoSportGameHero()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_Question_GetRandomInfo");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable CheckUserRegisterAllService(string userId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_Register_Check_All", userId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable UnCheckUserRegisterAllService(string userId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_Register_UnCheck_All", userId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static void InsertSportGameHeroAnswerLog(string userId,int questionId,string  question,string trueAnswer,DateTime sendTime,int status)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Sport_Game_Hero_Answer_Log_Insert"
                                    , userId
                                    , questionId
                                    , question
                                    , trueAnswer
                                    , sendTime
                                    , status
                                );
    }

    public static DataTable UpdateSportGameHeroRegisterUser(string userId,int status)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_Registered_Users_Update"

            , userId
            , status

            );

        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;

    }

    public static DataTable GetAnswerSportGameHero(string userId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_Answer_Log_GetAnswerByUserId", userId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable SportGameHeroCheckUserCharged(string userId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_CheckUserCharged", userId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable SportGameHeroCheckUserChargedByDay(string userId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_CheckUserChargedByDay", userId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable GetMessageRandomSportGameHero(int status)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_Message_GetRandomInfo",status);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable UpdatePointSportGameHeroRegisterUser(string userId, int questionId,int point,string requestId,string  answer,int status)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_Registered_Users_Update_Point"

            ,userId
            ,questionId
            ,point
            ,requestId
            ,answer
            ,status

            );

        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;

    }

    public static DataTable UpdatePointSportGameHeroRegisterUserTp(string userId, int questionId, int point, string requestId, string answer, int status)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_Registered_Users_Update_Point_Tp"

            , userId
            , questionId
            , point
            , requestId
            , answer
            , status

            );

        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;

    }

    public static DataTable UpdatePointSportGameHeroRegisterUserAdvance(string userId, int questionId, int point, string requestId, string answer, int status)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_Registered_Users_Update_Point_Advance"

            , userId
            , questionId
            , point
            , requestId
            , answer
            , status

            );

        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;

    }

    public static DataTable CountQuestionTodaySportGameHeroRegisterUser(string userId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_CountQuestionToday", userId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable GetSportGameUserByType(bool isLock)
    {
        DataTable retVal = null;
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("Sport_Game_Hero_Registered_Users_GetAllUserByType", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.AddWithValue("@isLock", isLock);
        try
        {
            retVal = new DataTable();
            var da = new SqlDataAdapter(dbCmd);
            da.Fill(retVal);
        }
        finally
        {
            dbConn.Close();
        }
        return retVal;
    }

    public static DataTable GetSportGameUserByTypeTp(bool isLock)
    {
        DataTable retVal = null;
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("Sport_Game_Hero_Registered_Users_GetAllUserByType_Tp", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.AddWithValue("@isLock", isLock);
        try
        {
            retVal = new DataTable();
            var da = new SqlDataAdapter(dbCmd);
            da.Fill(retVal);
        }
        finally
        {
            dbConn.Close();
        }
        return retVal;
    }

    public static DataTable GetSportGameUserByTypeBonus()
    {
        //DataTable retVal = null;
        //SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        //SqlCommand dbCmd = new SqlCommand("Sport_Game_Hero_Registered_Users_GetAllUserByTypeBonus", dbConn);
        //dbCmd.CommandType = CommandType.StoredProcedure;

        //try
        //{
        //    retVal = new DataTable();
        //    var da = new SqlDataAdapter(dbCmd);
        //    da.Fill(retVal);
        //}
        //finally
        //{
        //    dbConn.Close();
        //}
        //return retVal;

        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_Registered_Users_GetAllUserByTypeBonus");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;

    }

    public static void SportGameHeroInsertLog(SportGameHeroChargedUserLogInfo entity)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("Sport_Game_Hero_Charged_Users_Log_Insert_ForSub", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.AddWithValue("@ID", entity.ID);
        dbCmd.Parameters.AddWithValue("@User_ID", entity.User_ID);
        dbCmd.Parameters.AddWithValue("@Request_ID", entity.Request_ID);
        dbCmd.Parameters.AddWithValue("@Service_ID", entity.Service_ID);
        dbCmd.Parameters.AddWithValue("@Command_Code", entity.Command_Code);
        dbCmd.Parameters.AddWithValue("@Service_Type", entity.Service_Type);
        dbCmd.Parameters.AddWithValue("@Charging_Count", entity.Charging_Count);
        dbCmd.Parameters.AddWithValue("@FailedChargingTimes", entity.FailedChargingTime);
        dbCmd.Parameters.AddWithValue("@RegisteredTime", entity.RegisteredTime);
        dbCmd.Parameters.AddWithValue("@ExpiredTime", entity.ExpiredTime);
        dbCmd.Parameters.AddWithValue("@Registration_Channel", entity.Registration_Channel);
        dbCmd.Parameters.AddWithValue("@Status", entity.Status);
        dbCmd.Parameters.AddWithValue("@Operator", entity.Operator);
        dbCmd.Parameters.AddWithValue("@Reason", entity.Reason);

        dbCmd.Parameters.AddWithValue("@Price", entity.Price);

        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
        }
        finally
        {
            dbConn.Close();
        }
    }

    public static DataTable CheckExistSendMt(string userId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_SMS_MT_CheckExist", userId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static void SportGameHeroLotteryCodeInsert(string userId, string code)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Sport_Game_Hero_Lottery_Code_Insert"
                                    , userId
                                    , code
                                );
    }

    public static void SportGameHeroMatchVoteInsert(string userId,int matchId,string message,
                                                    string teamWin,int? totalGoal,string score,
                                                    string possession,int? yellowCard,string commandCode)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Sport_Game_Hero_Match_Vote_Insert"
                                   , userId
                                   , matchId
                                   , message
                                   , teamWin
                                   , totalGoal
                                   , score
                                   , possession
                                   , yellowCard
                                   , commandCode
                               );
    }

    public static DataTable SportGameHeroMatchGetByDay()
    {
        int day = 3;

        if (DateTime.Now.DayOfWeek.ToString() == DayOfWeek.Thursday.ToString())
            day = 5;
        else if (DateTime.Now.DayOfWeek.ToString() == DayOfWeek.Saturday.ToString())
            day = 7;

        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_Match_GetByDay", day);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;

    }

    public static DataTable SportGameHeroGetMtContentFootball(int type)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_MT_Content_GetTop", type);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    #endregion

    #region Remind

    public static DataTable SportGameHeroGetUser_Remind()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Sport_Game_Hero_GetUser_Remind");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static void SportGameHeroGetUserDelete_Remind(int id)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Sport_Game_Hero_DeleteUser_Remind", id);
    }

    #endregion

    #region Thanh Nu GAME

    public static DataTable ThanhNuGameGetUserByType()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Thanh_Nu_Registered_Users_GetAllUserByType",0);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable ThanhNuGetUserInfo(string userId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Thanh_Nu_Registered_Users_GetInfo", userId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }



    public static DataTable ThanhNuRegisterUserInsert(ThanhNuRegisteredUsers entity)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Thanh_Nu_Registered_Users_Insert"

            , entity.UserId
            , entity.RequestId
            , entity.ServiceId
            , entity.CommandCode
            , entity.ServiceType
            , entity.ChargingCount
            , entity.FailedChargingTimes
            , entity.RegisteredTime
            , entity.ExpiredTime
            , entity.RegistrationChannel
            , entity.Status
            , entity.Operator

            );

        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;

    }

    public static DataTable ThanhNuRegisterUserStatusUpdate(string userId,int status)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Thanh_Nu_Registered_Users_Update_Status"

            , userId
            , status

            );

        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static void ThanhNuChargedUserLog(ThanhNuChargedUserLogInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Thanh_Nu_Charged_Users_Log_Insert_ForSub"
                                     , entity.ID
                                     , entity.User_ID
                                     , entity.Request_ID
                                     , entity.Service_ID
                                     , entity.Command_Code
                                     , entity.Service_Type
                                     , entity.Charging_Count
                                     , entity.FailedChargingTime
                                     , entity.RegisteredTime
                                     , entity.ExpiredTime
                                     , entity.Registration_Channel
                                     , entity.Status
                                     , entity.Operator
                                     , entity.Reason
                                     , entity.Price
                                     , entity.PartnerResult
                                 );
    }

    public static bool ThanhNuRegisterUserCodeInsert(string userId,string code,string channel)
    {
        try
        {
            SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Thanh_Nu_Registered_User_Code_Insert", userId, code, channel);
        }
        catch (Exception)
        {
            return false;
        }

        return true;

    }

    public static bool ThanhNuCodeTempInsert(string userId, string code)
    {
        try
        {
            SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Thanh_Nu_User_Code_Temp_Insert", userId, code);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public static DataTable ThanhNuAllUserForSendMt()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Thanh_Nu_User_Code_Temp_GetAll");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable ThanhNuAllUserForSendMtRemind(int day)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Thanh_Nu_Registered_Users_Remind", day);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static void SmsMtInsertVmgPortal(VoteSmsMtInfo obj)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionStringVClip, "SMS_MT_Insert", obj.User_ID, obj.Message, obj.Service_ID, obj.Command_Code, obj.Message_Type, obj.Request_ID, obj.Total_Message, obj.Message_Index, obj.IsMore, obj.Content_Type, obj.ServiceType, obj.PartnerId, obj.Operator);
    }

    public static bool ThanhNuCodeTempDelete(string userId)
    {
        try
        {
            SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Thanh_Nu_User_Code_Temp_DelByMsisdn", userId);
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    #endregion

    #region WORLD CUP

    public static DataTable WorldCupGetRegisterUserForCharged()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "World_Cup_Get_Registered_Users_For_Charged");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable WorldCupGetRegisterUserForChargedVtv()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "World_Cup_Get_Registered_Users_For_Charged_Vtv");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable WorldCupMatchStatusUpdate(int matchId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "World_Cup_Match_Update_Status",matchId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable WorldCupGetMatchPlayed()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "World_Cup_Get_Match_Played");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataSet WorldCupGetMatchVoteByMatchIdRightAndWrong(int matchId,string teamCode)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "World_Cup_Get_Match_Vote_By_MatchId_RightAndWrong", matchId, teamCode);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds;
        }
        return null;
    }

    public static DataTable WorldCupRegisteredUserGetPoint(int matchId, string teamCode,string userId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "World_Cup_Get_Match_Vote_By_MatchIdAndUserId_RightAndWrong", matchId, teamCode, userId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable WorldCupRegisteredUserDeleted(string userId,string commandCode)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "World_Cup_Registered_Users_Delete", userId,commandCode);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static void WorldCupRegisteredUserUpdatePoint(string userId,int point)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "World_Cup_Registered_Users_Update_Point", userId, point);
    }

    public static void WorldCupRegisteredUserUpdateExpiredTime(int id)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "World_Cup_Registered_User_Update_ExpiredTime", id);
    }

    public static void WorldCupChargedUserLogForSub(SportGameHeroChargedUserLogInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "World_Cup_Charged_Users_Log_Insert_ForSub"
                                     , entity.ID
                                     , entity.User_ID
                                     , entity.Request_ID
                                     , entity.Service_ID
                                     , entity.Command_Code
                                     , entity.Service_Type
                                     , entity.Charging_Count
                                     , entity.FailedChargingTime
                                     , entity.RegisteredTime
                                     , entity.ExpiredTime
                                     , entity.Registration_Channel
                                     , entity.Status
                                     , entity.Operator
                                     , entity.Reason
                                     , entity.Price
                                 );
    }

    public static void WorldCupChargedUserLog(SportGameHeroChargedUserLogInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "World_Cup_Charged_Users_Log_Insert"
                                     , entity.User_ID
                                     , entity.Request_ID
                                     , entity.Service_ID
                                     , entity.Command_Code
                                     , entity.Service_Type
                                     , entity.Charging_Count
                                     , entity.FailedChargingTime
                                     , entity.RegisteredTime
                                     , entity.ExpiredTime
                                     , entity.Registration_Channel
                                     , entity.Status
                                     , entity.Operator
                                     , entity.Reason
                                     , entity.Price
                                 );
    }

    public static int WorldCupInsertMtLog(ViSport_S2_SMS_MTInfo entity)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("World_Cup_SMS_MT_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", entity.User_ID);
        dbCmd.Parameters.Add("@Message", entity.Message);
        dbCmd.Parameters.Add("@Service_ID", entity.Service_ID);
        dbCmd.Parameters.Add("@Command_Code", entity.Command_Code);
        dbCmd.Parameters.Add("@Message_Type", entity.Message_Type);
        dbCmd.Parameters.Add("@Request_ID", entity.Request_ID);
        dbCmd.Parameters.Add("@Total_Message", entity.Total_Message);
        dbCmd.Parameters.Add("@Message_Index", entity.Message_Index);
        dbCmd.Parameters.Add("@IsMore", entity.IsMore);
        dbCmd.Parameters.Add("@Content_Type", entity.Content_Type);
        dbCmd.Parameters.Add("@ServiceType", entity.ServiceType);
        dbCmd.Parameters.Add("@ResponseTime", entity.ResponseTime);
        dbCmd.Parameters.Add("@isLock", entity.isLock);
        dbCmd.Parameters.Add("@PartnerID", entity.PartnerID);
        dbCmd.Parameters.Add("@Operator", entity.Operator);

        dbCmd.Parameters.Add("@RETURN_ID", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
            return (int)dbCmd.Parameters["@RETURN_ID"].Value;
        }
        finally
        {
            dbConn.Close();
        }
    }

    public static DataTable WorldCupGetMtContent()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "World_Cup_MT_Content_GetTop");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable WorldCupRegisterUser94X(ViSport_S2_Registered_UsersInfo entity)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "World_Cup_Registered_Users_Insert"

            , entity.User_ID
            , entity.Request_ID
            , entity.Service_ID
            , entity.Command_Code
            , entity.Service_Type
            , entity.Charging_Count
            , entity.FailedChargingTimes
            , entity.RegisteredTime
            , entity.ExpiredTime
            , entity.Registration_Channel
            , entity.Status
            , entity.Operator
            , entity.Point

            );

        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;

    }

    public static DataTable WorldCupRegisterUserVtv6(ViSport_S2_Registered_UsersInfo entity)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "World_Cup_Registered_Users_Insert_Vtv"

            , entity.User_ID
            , entity.Request_ID
            , entity.Service_ID
            , entity.Command_Code
            , entity.Service_Type
            , entity.Charging_Count
            , entity.FailedChargingTimes
            , entity.RegisteredTime
            , entity.ExpiredTime
            , entity.Registration_Channel
            , entity.Status
            , entity.Operator
            , entity.Point

            );

        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;

    }

    public static void WorldCupChargedUserLogForSubVtv6(SportGameHeroChargedUserLogInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "World_Cup_Charged_Users_Log_Insert_ForSub_Vtv"
                                     , entity.ID
                                     , entity.User_ID
                                     , entity.Request_ID
                                     , entity.Service_ID
                                     , entity.Command_Code
                                     , entity.Service_Type
                                     , entity.Charging_Count
                                     , entity.FailedChargingTime
                                     , entity.RegisteredTime
                                     , entity.ExpiredTime
                                     , entity.Registration_Channel
                                     , entity.Status
                                     , entity.Operator
                                     , entity.Reason
                                     , entity.Price
                                 );
    }

    public static void WorldCupUserInsertToAndy(int registeredID,
			string userID,
			string requestID,
            int serviceID,
            int serviceType,
            int chargingValue,
			string cpId,
			string username,
			string userpass, 
			string notificationEndpoint )
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionStringTtndService, "World_Cup_Vtv_ChargingCommand_Insert"
                                     , registeredID
                                     , userID
                                     , requestID
                                     , serviceID
                                     , serviceType
                                     , chargingValue
                                     , cpId
                                     , username
                                     , userpass
                                     , notificationEndpoint
                                 );
    }

    #endregion

    #region S2_94x

    public static void S294XChargedUserLog3G(string userId,string requestId,string serviceType,
                                               string serviceId,string registeredId,string shortCode,
                                               string commandCode,string chargingPrice,string chargingResponse,
                                               string chargingStatus,string chargingAccount)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionStringTtndService, "S2_TTND_Log_3gCharging"
                                     , userId
                                     , requestId
                                     , serviceType
                                     , serviceId
                                     , registeredId
                                     , shortCode
                                     , commandCode
                                     , chargingPrice
                                     , chargingResponse
                                     , chargingStatus
                                     , chargingAccount
                                 );
    }

    public static DataTable S294XGetUserRegistered()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringTtndService, "S2_TTND_GetUserRegister_94x");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable S294XGetUserRegisteredByServiceId(int serviceId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringTtndService, "S2_TTND_GetUserRegister_94x_ByServiceId",serviceId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    #endregion

    #region PARTNER_CDR

    public static DataTable PartnerGetAll()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringTtndService, "S2_TTND_Partners_CDR_GetAll");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable CdrGpc(int partnerId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringTtndService, "Vinaphone_Crd_ByPartner",partnerId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable CdrVnm(int partnerId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringTtndService, "Vnm_Partner_Crd", partnerId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable CdrVnm1119(int partnerId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVms, "Vnm_Partner_Crd_1119", partnerId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable CdrPartnerGetServiceId(int partnerId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVms, "Vms_Partner_GetServiceId",partnerId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }




    public static DataTable SamCdrGpc()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringTtndService, "Vinaphone_Sam_Crd");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable SamCdrVnm()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringTtndService, "Vnm_Sam_Crd");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable SamVmsGetServiceId()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVms, "Vms_Sam_GetServiceId");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable SamVmsGetCdrByServiceId(string serviceId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVms, "Vms_Sam_Crd", serviceId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    #endregion

    #region Luckyfone 

    public static DataTable LuckyfoneGetMo()
    {
        string year = DateTime.Now.Year.ToString();
        string month = DateTime.Now.Month.ToString();
        string day = (DateTime.Now.Day - 1).ToString();

        if (month.Length == 1)
        {
            month = "0" + month;
        }

        if (day.Length == 1)
        {
            day = "0" + day;
        }

        string oldParameter = year + month + day;

        string tableMoLog = "sms_receive_log PARTITION (P_" + oldParameter.Substring(0, 4) + "_" + oldParameter.Substring(4, 2);
        int intCurrentDate = ConvertUtility.ToInt32(oldParameter.Substring(6, 2));

        if(intCurrentDate > 0 && intCurrentDate < 6)
        {
            tableMoLog = tableMoLog + "_1)";
        }
        else if (intCurrentDate > 5 && intCurrentDate < 11)
        {
            tableMoLog = tableMoLog + "_2)";
        }
        else if (intCurrentDate > 10 && intCurrentDate < 16)
        {
            tableMoLog = tableMoLog + "_3)";
        }
        else if (intCurrentDate > 15 && intCurrentDate < 21)
        {
            tableMoLog = tableMoLog + "_4)";
        }
        else if (intCurrentDate > 20 && intCurrentDate < 26)
        {
            tableMoLog = tableMoLog + "_5)";
        }
        else if (intCurrentDate > 25 && intCurrentDate < 32)
        {
            tableMoLog = tableMoLog + "_6)";
        }

        string sql = "Select TOP 10 USER_ID, " +
                     "SERVICE_ID, " +
                     "MOBILE_OPERATOR, " +
                     "COMMAND_CODE, " +
                     "INFO, " +
                     "TIMESTAMP, " +
                     "RESPONDED, " +
                     "REQUEST_ID " +
                     " From " + tableMoLog + " Where To_Char(TIMESTAMP,'YYYYMMDD')='" + oldParameter + "'";

        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, CommandType.Text,sql);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static void LuckyfoneMoInsert(MoEntity997 entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "LKF_Mo_Insert"
                                     , entity.UserID
                                     , entity.ServiceID
                                     , entity.MobileOperator
                                     , entity.CommandCode
                                     , entity.Info
                                     , entity.Timestamp
                                     , entity.Responded
                                     , entity.RequestID
                                 );
    }

    public static DataTable LuckyfoneCheckUser(string userId,string serviceId,string mobileOperator,string commandCode,string message,DateTime submitDate,string requestId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "LKF_User_Check_Exist",
                                                userId,
                                                serviceId,
                                                mobileOperator,
                                                commandCode,
                                                message,
                                                submitDate,
                                                requestId
                                                    );
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable LuckyfoneCheckUserNew()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "LKF_User_Check_Exist_New");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable LuckyfoneGetUser()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "LKF_Mo_GetUser");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    #endregion

}


#line default
#line hidden
