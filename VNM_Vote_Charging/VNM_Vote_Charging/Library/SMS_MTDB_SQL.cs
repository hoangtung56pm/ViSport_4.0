using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace VNM_ViSport_Charging.Library
{
    public class SMS_MTDB_SQL
    {

        public static DataTable GetMTByStatus(bool isLock)
        {
            DataTable retVal = null;
            SqlConnection dbConn = new SqlConnection(SMS.Default.cnn);
            SqlCommand dbCmd = new SqlCommand("Vote_GetAllUserByType", dbConn);
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
        public static void MTUpdateByListId(string ids)
        {
            SqlConnection dbConn = new SqlConnection(SMS.Default.cnn);
            SqlCommand dbCmd = new SqlCommand("Vote_UpdateByList", dbConn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", ids);
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
        public static void MTUpdateFail(int id)
        {
            SqlConnection dbConn = new SqlConnection(SMS.Default.cnn);
            SqlCommand dbCmd = new SqlCommand("Vote_Registered_Users_Update_Fail", dbConn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@id", id);
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
        
        public static void InsertLogLike(ViSport_S2_Charged_Users_LogInfo viSportS2ChargedUsersLogInfo)
        {
            SqlConnection dbConn = new SqlConnection(SMS.Default.cnn);
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

        public static void InsertLogDisLike(ViSport_S2_Charged_Users_LogInfo viSportS2ChargedUsersLogInfo)
        {
            SqlConnection dbConn = new SqlConnection(SMS.Default.cnn);
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

        public static DataTable GetVoteAccountInfo(string userId,string commandCode)
        {
            DataTable retVal = new DataTable();
            SqlConnection dbConn = new SqlConnection(SMS.Default.cnn);
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

    }
}
