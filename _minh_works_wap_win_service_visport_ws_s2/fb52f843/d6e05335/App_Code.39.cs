#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\Vote_Registered\VoteRegisterController.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8EBA948C8B2DE29D8A1173CABDB45082D67AB189"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\Vote_Registered\VoteRegisterController.cs"
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for VoteRegisterController
/// </summary>
public class VoteRegisterController
{

    #region World Cup

    public static void WorldCupSmsMtInsert(VoteSmsMtInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "World_Cup_SMS_MT_Insert"
                                     , entity.User_ID
                                     , entity.Message
                                     , entity.Service_ID
                                     , entity.Command_Code
                                     , entity.Message_Type
                                     , entity.Request_ID
                                     , entity.Total_Message
                                     , entity.Message_Index
                                     , entity.IsMore
                                     , entity.Content_Type
                                     , entity.ServiceType
                                     , entity.ResponseTime
                                     , entity.IsLock
                                     , entity.PartnerId
                                     , entity.Operator
                                 );
    }

    public static void WorldCupRoundMatchInsert(string userId,string message,string teamCode)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "World_Cup_Round_Match_Add"
                                     , userId
                                     , message
                                     , teamCode
                                 );
    }

    public static DataTable WorldCupRoundCheck(string teamCode)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "World_Cup_Round_Check", teamCode);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable WorldCupMatchCheck(int matchId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "World_Cup_Match_Check", matchId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static void WorldCupRoundInsert(string userId,string message,string teamCode)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "World_Cup_Round_Match_Add"
                                    , userId
                                    , message
                                    , teamCode
                                );
    }

    public static void WorldCupMatchInsert(string userId,int matchId, string message, string teamCode)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "World_Cup_Match_Vote_Add"
                                    
                                    , userId
                                    , matchId
                                    , message
                                    , teamCode
                                );
    }

    #endregion

    public static DataTable VoteRegisterInsert(VoteRegisteredInfo entity)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Vote_Registered_Users_Insert"
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
            ,entity.Vote_Count
            ,entity.Vote_PersonId
            ,entity.IsDislike

            );

        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable VoteRegisterDislikeInsert(VoteRegisteredInfo entity)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Vote_Registered_Users_Dislike_Insert"

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
            , entity.Dislike_Count
            , entity.Dislike_PersonId

            );

        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static void VoteChargedUserLogInsert(VoteChargedUserLogInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Vote_Charged_Users_Log_Insert"
                                     ,entity.User_ID
                                     ,entity.Request_ID
                                     ,entity.Service_ID
                                     ,entity.Command_Code
                                     ,entity.Service_Type
                                     ,entity.Charging_Count
                                     ,entity.FailedChargingTime
                                     ,entity.RegisteredTime
                                     ,entity.ExpiredTime
                                     ,entity.Registration_Channel
                                     ,entity.Status
                                     ,entity.Operator
                                     ,entity.Reason
                                     ,entity.Price
                                     ,entity.Vote_PersonId
                                 );
    }

    public static void VoteSmsMoInsert(VoteSmsMoInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Vote_SMS_MO_Insert"
                                     , entity.User_ID
                                     , entity.Request_ID
                                     , entity.Service_ID
                                     , entity.Command_Code
                                     , entity.Message
                                     , entity.Operator
                                 );
    }

    public static void VoteSmsMtInsert(VoteSmsMtInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Vote_SMS_MT_Insert"
                                     , entity.User_ID
                                     , entity.Message
                                     , entity.Service_ID
                                     , entity.Command_Code
                                     , entity.Message_Type
                                     , entity.Request_ID
                                     , entity.Total_Message
                                     , entity.Message_Index
                                     , entity.IsMore
                                     , entity.Content_Type
                                     , entity.ServiceType
                                     , entity.ResponseTime
                                     , entity.IsLock
                                     , entity.PartnerId
                                     , entity.Operator
                                     
                                 );
    }

    
    public static DataTable VoteRegisterUserLock(string userId,int votePersonId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Vote_Registered_Users_Lock",userId,votePersonId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable VoteRegisterUserDislikeLock(string userId, int dislikePersonId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Vote_Registered_Users_Dislike_Lock", userId, dislikePersonId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static void VoteUpdateCount(string userId,string commandCode)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Vote_Count_Update", userId, commandCode);
    }

    public static DataTable VoteGetCount(string userId,string commandCode)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Vote_GetCount_Info", userId, commandCode);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable VoteGetVnmUserActive()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Vote_GetVnmUserActice");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable VoteGetUserInfo(string userId,int votePersonId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Vote_Registered_Users_GetInfo",userId,votePersonId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable NewVoteGetUserInfo(string userId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Vote_New_Registered_Users_GetInfo", userId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static void InsertLogLike(ViSport_S2_Charged_Users_LogInfo viSportS2ChargedUsersLogInfo)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("Vote_Charged_Users_Log_Insert_ForSub_Like", dbConn);
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
        dbCmd.Parameters.AddWithValue("@Vote_PersonId", viSportS2ChargedUsersLogInfo.Vote_PersonId);

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

    public static void NewInsertLogLike(ViSport_S2_Charged_Users_LogInfo viSportS2ChargedUsersLogInfo)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("Vote_New_Charged_Users_Log_Insert_ForSub_Like", dbConn);
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
        dbCmd.Parameters.AddWithValue("@Vote_PersonId", viSportS2ChargedUsersLogInfo.Vote_PersonId);

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

    public static void InsertLogDisLike(ViSport_S2_Charged_Users_LogInfo viSportS2ChargedUsersLogInfo)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("Vote_Charged_Users_Log_Insert_ForSub_DisLike", dbConn);
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
        dbCmd.Parameters.AddWithValue("@Dislike_PersonId", viSportS2ChargedUsersLogInfo.Vote_PersonId);

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

    public static DataTable GetVoteAccountInfo(string userId, string commandCode)
    {
        DataTable retVal = new DataTable();
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("Vote_GetCount_Info", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.AddWithValue("@User_ID", userId);
        dbCmd.Parameters.AddWithValue("@Command_Code", commandCode);
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

    #region SECRET VOTE

    public static void SecretSmsMoInsert(VoteSmsMoInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Secret_SMS_MO_Insert"
                                     , entity.User_ID
                                     , entity.Request_ID
                                     , entity.Service_ID
                                     , entity.Command_Code
                                     , entity.Message
                                     , entity.Operator
                                 );
    }

    public static void SecretSmsMtInsert(VoteSmsMtInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Secret_SMS_MT_Insert"
                                     , entity.User_ID
                                     , entity.Message
                                     , entity.Service_ID
                                     , entity.Command_Code
                                     , entity.Message_Type
                                     , entity.Request_ID
                                     , entity.Total_Message
                                     , entity.Message_Index
                                     , entity.IsMore
                                     , entity.Content_Type
                                     , entity.ServiceType
                                     , entity.ResponseTime
                                     , entity.IsLock
                                     , entity.PartnerId
                                     , entity.Operator

                                 );
    }

    public static DataTable SecretRegisterInsert(VoteRegisteredInfo entity)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Secret_Registered_Users_Insert"
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
            , entity.Vote_Count
            , entity.Vote_PersonId
            , entity.IsDislike

            );

        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static void SecretChargedUserLogInsert(VoteChargedUserLogInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Secret_Charged_Users_Log_Insert"
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
                                     , entity.Vote_PersonId
                                 );
    }

    public static DataTable SecretRegisterUserLock(string userId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Secret_Registered_Users_Lock", userId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable SecretGetUserByType(bool isLock)
    {
        DataTable retVal = null;
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("Secret_GetAllUserByType", dbConn);
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

    public static void SecretChargedUserLogInsertForSub(VoteChargedUserLogInfo e)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("Secret_Charged_Users_Log_Insert_ForSub", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.AddWithValue("@ID", e.ID);
        dbCmd.Parameters.AddWithValue("@User_ID", e.User_ID);
        dbCmd.Parameters.AddWithValue("@Request_ID", e.Request_ID);
        dbCmd.Parameters.AddWithValue("@Service_ID", e.Service_ID);
        dbCmd.Parameters.AddWithValue("@Command_Code", e.Command_Code);
        dbCmd.Parameters.AddWithValue("@Service_Type", e.Service_Type);
        dbCmd.Parameters.AddWithValue("@Charging_Count", e.Charging_Count);
        dbCmd.Parameters.AddWithValue("@FailedChargingTimes", e.FailedChargingTime);
        dbCmd.Parameters.AddWithValue("@RegisteredTime", e.RegisteredTime);
        dbCmd.Parameters.AddWithValue("@ExpiredTime", e.ExpiredTime);
        dbCmd.Parameters.AddWithValue("@Registration_Channel", e.Registration_Channel);
        dbCmd.Parameters.AddWithValue("@Status", e.Status);
        dbCmd.Parameters.AddWithValue("@Operator", e.Operator);
        dbCmd.Parameters.AddWithValue("@Reason", e.Reason);

        dbCmd.Parameters.AddWithValue("@Price", e.Price);
        dbCmd.Parameters.AddWithValue("@Vote_PersonId", e.Vote_PersonId);

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

    public static DataTable SecretGetCountByPersonId(string userId, int personId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Secret_GetCount_Info_ByPersonId", userId, personId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable SecretGetRandomContent()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVClip, "Wap_Vnm_Secret_GetRandomContent");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    #endregion

    #region Mo949

    public static DataTable Mo949RegisterInsert(VoteRegisteredInfo entity)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Mo949_Registered_Users_Insert"
            , entity.User_ID
            , entity.Request_ID
            , entity.Service_ID
            , entity.Command_Code
            , entity.Service_Type
            
            , entity.FailedChargingTime
            , entity.RegisteredTime
            , entity.Registration_Channel
            , entity.Status
            , entity.Operator

            );

        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static void Mo949ChargedUserLogInsert(VoteChargedUserLogInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Mo949_Charged_Users_Log_Insert"
                                     , entity.User_ID
                                     , entity.Request_ID
                                     , entity.Service_ID
                                     , entity.Command_Code
                                     , entity.Service_Type
                                     
                                     , entity.RegisteredTime
                                     , entity.Registration_Channel
                                     , entity.Operator
                                     , entity.Reason
                                     , entity.Price
                                 );
    }

    public static void Mo949ChargedUserLogInsertForSub(VoteChargedUserLogInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Mo949_Charged_Users_Log_Insert_ForSub"
                                     , entity.ID
                                     , entity.User_ID
                                     , entity.Request_ID
                                     , entity.Service_ID
                                     , entity.Command_Code
                                     , entity.Service_Type

                                     , entity.RegisteredTime
                                     , entity.Registration_Channel
                                     , entity.Operator
                                     , entity.Reason
                                     , entity.Price
                                 );
    }

    public static DataTable Mo949GetRandomGame()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVClip, "Mo949_Vnm_GetRandomGame");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable Mo949GetRandomMusic()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVClip, "Mo949_Vnm_GetRandomMusic");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable Mo949GetRandomVideo()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVClip, "Mo949_Vnm_GetRandomVideo");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable Mo949GetUserForReCharged()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Mo949_Registered_Users_GetForReCharged");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    #endregion

    #region VOTE NEW

    public static void NewVoteSmsMoInsert(VoteSmsMoInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Vote_New_SMS_MO_Insert"
                                     , entity.User_ID
                                     , entity.Request_ID
                                     , entity.Service_ID
                                     , entity.Command_Code
                                     , entity.Message
                                     , entity.Operator
                                 );
    }

    public static DataTable NewVoteRegisterUserLock(string userId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Vote_New_Registered_Users_Lock", userId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static void NewVoteChargedUserLogInsert(VoteChargedUserLogInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Vote_New_Charged_Users_Log_Insert"
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
                                     , entity.Vote_PersonId
                                 );
    }

    public static void WorldCupChargedUserLogInsert(VoteChargedUserLogInfo entity)
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

    public static void NewVoteSmsMtInsert(VoteSmsMtInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Vote_New_SMS_MT_Insert"
                                     , entity.User_ID
                                     , entity.Message
                                     , entity.Service_ID
                                     , entity.Command_Code
                                     , entity.Message_Type
                                     , entity.Request_ID
                                     , entity.Total_Message
                                     , entity.Message_Index
                                     , entity.IsMore
                                     , entity.Content_Type
                                     , entity.ServiceType
                                     , entity.ResponseTime
                                     , entity.IsLock
                                     , entity.PartnerId
                                     , entity.Operator

                                 );
    }

    public static DataTable NewVoteRegisterInsert(VoteRegisteredInfo entity)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Vote_New_Registered_Users_Insert"
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
            , entity.Vote_Count
            , entity.Vote_PersonId
            , entity.IsDislike

            );

        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable NewVoteGetUserByType(bool isLock)
    {
        DataTable retVal = null;
        var dbConn = new SqlConnection(AppEnv.ConnectionString);
        var dbCmd = new SqlCommand("Vote_New_Registered_Users_GetAllUserByType", dbConn);
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

    public static void NewVoteChargedUserLogInsertForSub(VoteChargedUserLogInfo e)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("Vote_New_Charged_Users_Log_Insert_ForSub_Like", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.AddWithValue("@ID", e.ID);
        dbCmd.Parameters.AddWithValue("@User_ID", e.User_ID);
        dbCmd.Parameters.AddWithValue("@Request_ID", e.Request_ID);
        dbCmd.Parameters.AddWithValue("@Service_ID", e.Service_ID);
        dbCmd.Parameters.AddWithValue("@Command_Code", e.Command_Code);
        dbCmd.Parameters.AddWithValue("@Service_Type", e.Service_Type);
        dbCmd.Parameters.AddWithValue("@Charging_Count", e.Charging_Count);
        dbCmd.Parameters.AddWithValue("@FailedChargingTimes", e.FailedChargingTime);
        dbCmd.Parameters.AddWithValue("@RegisteredTime", e.RegisteredTime);
        dbCmd.Parameters.AddWithValue("@ExpiredTime", e.ExpiredTime);
        dbCmd.Parameters.AddWithValue("@Registration_Channel", e.Registration_Channel);
        dbCmd.Parameters.AddWithValue("@Status", e.Status);
        dbCmd.Parameters.AddWithValue("@Operator", e.Operator);
        dbCmd.Parameters.AddWithValue("@Reason", e.Reason);

        dbCmd.Parameters.AddWithValue("@Price", e.Price);
        dbCmd.Parameters.AddWithValue("@Vote_PersonId", e.Vote_PersonId);

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

    public static DataTable NewVoteRegisterUserGetInfo(string userId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Vote_New_Registered_Users_GetInfo", userId);
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
