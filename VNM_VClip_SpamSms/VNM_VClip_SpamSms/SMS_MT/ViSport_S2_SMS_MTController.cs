using System.Data;
using System.Data.SqlClient;
using VNM_VClip_SpamSms.Library;

namespace VNM_VClip_SpamSms.SMS_MT
{
    public class ViSport_S2_SMS_MTController
    {
        public static DataTable CheckAlreadySendMt(string msisdn,int hour,string day)
        {
            DataSet ds = SqlHelper.ExecuteDataset(SMS.Default.cnn, "VClip_S2_SMS_MT_CheckAlreadySend", msisdn, hour, day);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public static DataTable GetMtContent()
        {
            DataSet ds = SqlHelper.ExecuteDataset(SMS.Default.cnn, "VClip_S2_GetMTContent");
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public static void UpdateStatus(int status)
        {
            SqlConnection dbConn = new SqlConnection(SMS.Default.cnn);
            SqlCommand dbCmd = new SqlCommand("VClip_S2_JobStatus_Update", dbConn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.Add("@Status", status);
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

        public static int Insert(ViSport_S2_SMS_MTInfo _viSport_S2_SMS_MTInfo)
        {
            SqlConnection dbConn = new SqlConnection(SMS.Default.cnn);
            SqlCommand dbCmd = new SqlCommand("VClip_S2_SMS_MT_Insert", dbConn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.Add("@User_ID", _viSport_S2_SMS_MTInfo.User_ID);
            dbCmd.Parameters.Add("@Message", _viSport_S2_SMS_MTInfo.Message);
            dbCmd.Parameters.Add("@Service_ID", _viSport_S2_SMS_MTInfo.Service_ID);
            dbCmd.Parameters.Add("@Command_Code", _viSport_S2_SMS_MTInfo.Command_Code);
            dbCmd.Parameters.Add("@Message_Type", _viSport_S2_SMS_MTInfo.Message_Type);
            dbCmd.Parameters.Add("@Request_ID", _viSport_S2_SMS_MTInfo.Request_ID);
            dbCmd.Parameters.Add("@Total_Message", _viSport_S2_SMS_MTInfo.Total_Message);
            dbCmd.Parameters.Add("@Message_Index", _viSport_S2_SMS_MTInfo.Message_Index);
            dbCmd.Parameters.Add("@IsMore", _viSport_S2_SMS_MTInfo.IsMore);
            dbCmd.Parameters.Add("@Content_Type", _viSport_S2_SMS_MTInfo.Content_Type);
            dbCmd.Parameters.Add("@ServiceType", _viSport_S2_SMS_MTInfo.ServiceType);
            dbCmd.Parameters.Add("@ResponseTime", _viSport_S2_SMS_MTInfo.ResponseTime);
            dbCmd.Parameters.Add("@isLock", _viSport_S2_SMS_MTInfo.isLock);
            dbCmd.Parameters.Add("@PartnerID", _viSport_S2_SMS_MTInfo.PartnerID);
            dbCmd.Parameters.Add("@Operator", _viSport_S2_SMS_MTInfo.Operator);
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
        public static int InsertVClip(ViSport_S2_SMS_MTInfo _viSport_S2_SMS_MTInfo)
        {
            SqlConnection dbConn = new SqlConnection(SMS.Default.cnn);
            SqlCommand dbCmd = new SqlCommand("VClip_S2_SMS_MT_Insert", dbConn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.Add("@User_ID", _viSport_S2_SMS_MTInfo.User_ID);
            dbCmd.Parameters.Add("@Message", _viSport_S2_SMS_MTInfo.Message);
            dbCmd.Parameters.Add("@Service_ID", _viSport_S2_SMS_MTInfo.Service_ID);
            dbCmd.Parameters.Add("@Command_Code", _viSport_S2_SMS_MTInfo.Command_Code);
            dbCmd.Parameters.Add("@Message_Type", _viSport_S2_SMS_MTInfo.Message_Type);
            dbCmd.Parameters.Add("@Request_ID", _viSport_S2_SMS_MTInfo.Request_ID);
            dbCmd.Parameters.Add("@Total_Message", _viSport_S2_SMS_MTInfo.Total_Message);
            dbCmd.Parameters.Add("@Message_Index", _viSport_S2_SMS_MTInfo.Message_Index);
            dbCmd.Parameters.Add("@IsMore", _viSport_S2_SMS_MTInfo.IsMore);
            dbCmd.Parameters.Add("@Content_Type", _viSport_S2_SMS_MTInfo.Content_Type);
            dbCmd.Parameters.Add("@ServiceType", _viSport_S2_SMS_MTInfo.ServiceType);
            dbCmd.Parameters.Add("@ResponseTime", _viSport_S2_SMS_MTInfo.ResponseTime);
            dbCmd.Parameters.Add("@isLock", _viSport_S2_SMS_MTInfo.isLock);
            dbCmd.Parameters.Add("@PartnerID", _viSport_S2_SMS_MTInfo.PartnerID);
            dbCmd.Parameters.Add("@Operator", _viSport_S2_SMS_MTInfo.Operator);
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
