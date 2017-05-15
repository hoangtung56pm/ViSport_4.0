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
            SqlCommand dbCmd = new SqlCommand("ViSport_GetAllUserByType", dbConn);
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
            SqlCommand dbCmd = new SqlCommand("ViSport_UpdateByList", dbConn);
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
            SqlCommand dbCmd = new SqlCommand("ViSport_S2_Registered_Users_Update_Fail", dbConn);
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
        
        public static void InsertLog(ViSport_S2_Charged_Users_LogInfo _viSport_S2_Charged_Users_LogInfo)
        {
            SqlConnection dbConn = new SqlConnection(SMS.Default.cnn);
            SqlCommand dbCmd = new SqlCommand("ViSport_S2_Charged_Users_Log_Insert_New", dbConn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.AddWithValue("@ID", _viSport_S2_Charged_Users_LogInfo.ID);
            dbCmd.Parameters.AddWithValue("@User_ID", _viSport_S2_Charged_Users_LogInfo.User_ID);
            dbCmd.Parameters.AddWithValue("@Request_ID", _viSport_S2_Charged_Users_LogInfo.Request_ID);
            dbCmd.Parameters.AddWithValue("@Service_ID", _viSport_S2_Charged_Users_LogInfo.Service_ID);
            dbCmd.Parameters.AddWithValue("@Command_Code", _viSport_S2_Charged_Users_LogInfo.Command_Code);
            dbCmd.Parameters.AddWithValue("@Service_Type", _viSport_S2_Charged_Users_LogInfo.Service_Type);
            dbCmd.Parameters.AddWithValue("@Charging_Count", _viSport_S2_Charged_Users_LogInfo.Charging_Count);
            dbCmd.Parameters.AddWithValue("@FailedChargingTimes", _viSport_S2_Charged_Users_LogInfo.FailedChargingTimes);
            dbCmd.Parameters.AddWithValue("@RegisteredTime", _viSport_S2_Charged_Users_LogInfo.RegisteredTime);
            dbCmd.Parameters.AddWithValue("@ExpiredTime", _viSport_S2_Charged_Users_LogInfo.ExpiredTime);
            dbCmd.Parameters.AddWithValue("@Registration_Channel", _viSport_S2_Charged_Users_LogInfo.Registration_Channel);
            dbCmd.Parameters.AddWithValue("@Status", _viSport_S2_Charged_Users_LogInfo.Status);
            dbCmd.Parameters.AddWithValue("@Operator", _viSport_S2_Charged_Users_LogInfo.Operator);
            dbCmd.Parameters.AddWithValue("@Reason", _viSport_S2_Charged_Users_LogInfo.Reason);

            dbCmd.Parameters.AddWithValue("@Price", _viSport_S2_Charged_Users_LogInfo.Price);

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
