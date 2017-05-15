using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace VNM_ViSport_Charging_SpamSms.Library
{
    public class SMS_MTDB_SQL
    {

        public static DataTable GetMTByStatus(bool isLock)
        {
            DataTable retVal = null;
            SqlConnection dbConn = new SqlConnection(SMS.Default.cnn);
            SqlCommand dbCmd = new SqlCommand("ViSport_GetAllUserByType_SpamSms_New", dbConn);
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
            SqlCommand dbCmd = new SqlCommand("ViSport_UpdateByList_SpamSms", dbConn);
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
            SqlCommand dbCmd = new SqlCommand("ViSport_S2_Registered_Users_Update_Fail_SpamSms", dbConn);
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

        public static void InsertLog(ViSport_S2_Registered_SpamSms_UserInfo viSportS2ChargedUsersLogInfo)
        {
            SqlConnection dbConn = new SqlConnection(SMS.Default.cnn);
            SqlCommand dbCmd = new SqlCommand("ViSport_S2_Charged_Users_Log_Insert_New_SpamSms", dbConn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@ID", viSportS2ChargedUsersLogInfo.Id);
            dbCmd.Parameters.AddWithValue("@User_ID", viSportS2ChargedUsersLogInfo.User_Id);
            dbCmd.Parameters.AddWithValue("@Request_ID", viSportS2ChargedUsersLogInfo.Request_Id);
            dbCmd.Parameters.AddWithValue("@Service_ID", viSportS2ChargedUsersLogInfo.Service_Id);
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


        public static void ViSportSpamSmsUserUpdateExpiredTime(int id)
        {
            SqlConnection dbConn = new SqlConnection(SMS.Default.cnn);
            SqlCommand dbCmd = new SqlCommand("ViSport_S2_SpamSMS_Users_UpdateExpiredTime", dbConn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@Id", id);
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
    }
}
