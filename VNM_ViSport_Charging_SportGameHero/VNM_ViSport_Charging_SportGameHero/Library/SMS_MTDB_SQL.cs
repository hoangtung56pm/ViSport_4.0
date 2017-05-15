using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

namespace VNM_ViSport_Charging_SpamSms.Library
{
    public class SMS_MTDB_SQL
    {

        public static DataTable GetMTByStatus(bool isLock)
        {
            DataTable retVal = null;
            SqlConnection dbConn = new SqlConnection(SMS.Default.cnn);
            SqlCommand dbCmd = new SqlCommand("Sport_Game_Hero_Registered_Users_GetAllUserByType", dbConn);
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
            SqlCommand dbCmd = new SqlCommand("Sport_Game_Hero_UpdateByList", dbConn);
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
            SqlCommand dbCmd = new SqlCommand("Sport_Game_Hero_Registered_Users_Update_Fail", dbConn);
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
            SqlCommand dbCmd = new SqlCommand("Sport_Game_Hero_Charged_Users_Log_Insert_ForSub", dbConn);
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

        public static DataTable GetQuestionInfoSportGameHero()
        {
            DataSet ds = SqlHelper.ExecuteDataset(SMS.Default.cnn, "Sport_Game_Hero_Question_GetRandomInfo");
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public static DataTable CheckExistSendMt(string userId)
        {
            DataSet ds = SqlHelper.ExecuteDataset(SMS.Default.cnn, "Sport_Game_Hero_SMS_MT_CheckExist",userId);
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        public static void InsertSportGameHeroAnswerLog(string userId, int questionId, string question, string trueAnswer, DateTime sendTime, int status)
        {
            SqlHelper.ExecuteNonQuery(SMS.Default.cnn, "Sport_Game_Hero_Answer_Log_Insert"
                                        , userId
                                        , questionId
                                        , question
                                        , trueAnswer
                                        , sendTime
                                        , status
                                    );

           // DataSet ds = SqlHelper.ExecuteDataset(SMS.Default.cnn, "Sport_Game_Hero_Answer_Log_Insert"

           //                                 , userId
           //                                 , questionId
           //                                 , question
           //                                 , trueAnswer
           //                                 , sendTime
           //                                 , status

           //);

           // if (ds != null && ds.Tables.Count > 0)
           // {
           //     return ds.Tables[0];
           // }
           // return null;

        }

        public static int InsertSportGameHeroMt(ViSport_S2_SMS_MTInfo entity)
        {
            SqlConnection dbConn = new SqlConnection(SMS.Default.cnn);
            SqlCommand dbCmd = new SqlCommand("Sport_Game_Hero_SMS_MT_Insert", dbConn);
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
            dbCmd.Parameters.Add("@IsQuestion", entity.IsQuestion);


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

    }
}
