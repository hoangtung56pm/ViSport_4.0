using System;
using System.Data;
using System.Web.Configuration;
using LuckyFone.Helper;
using Microsoft.ApplicationBlocks.Data;
using Oracle.DataAccess.Client;

namespace LuckyFone
{
    public class PublicController
    {
        static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(PublicController));

        //public TransactionManager trans { get; set; }
        
       

        #region Luckyfone

        public static DataTable LuckyfoneGetMo()
        {
            try
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

                if (intCurrentDate > 0 && intCurrentDate < 6)
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

                Log.Debug("********** LUCKFONE LOG SQL **********");
                Log.Debug("SQL : " + sql);
                Log.Debug(" ");
                Log.Debug(" ");

                DataSet ds = OracleHelper.ExecuteDataset(AppEnv.LuckyFoneOracleVms, CommandType.Text, sql);
                //DataSet ds = OracleTransactionFilter.ExecuteDataset(trans, CommandType.Text, sql);
                

                if (ds != null && ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                return null;
            }
            catch (Exception ex)
            {
                Log.Debug("********** LUCKFONE LOG SQL ERROR **********");
                Log.Debug("Exception : " + ex);
                Log.Debug(" ");
                Log.Debug(" ");

                return null;
            }
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

        public static DataTable LuckyfoneCheckUser(string userId, string serviceId, string mobileOperator, string commandCode, string message, DateTime submitDate, string requestId)
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


        public static DataTable LuckyfoneGetLuckyUser()
        {
            DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "LKF_Lucky_User_GetForSendMt");
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }


        public static void LuckyfoneMtInsert(string userId,string requestId,string serviceId,string commandCode,string message,string telco)
        {
            SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "LKF_MT_Log_Insert"
                                         , userId
                                         , requestId
                                         , serviceId
                                         , commandCode
                                         , message
                                         , telco
                                     );
        }

        public static void SmsMtInsertNew(MTInfo obj)
        {
            SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "SMS_MT_Insert", obj.User_ID, obj.Message, obj.Service_ID, obj.Command_Code, obj.Message_Type, obj.Request_ID, obj.Total_Message, obj.Message_Index, obj.IsMore, obj.Content_Type, obj.ServiceType, obj.PartnerID, obj.Operator);
        }

        #endregion
    }

    public class AppEnv
    {
        public static string LuckyFoneOracleVms
        {
            get { return WebConfigurationManager.ConnectionStrings["localsqlLkf_Vms"].ConnectionString; }
        }

        public static string LuckyFoneOracleGpcViettel
        {
            get { return WebConfigurationManager.ConnectionStrings["localsqlLkf_Gpc_Viettel"].ConnectionString; }
        }

        public static string ConnectionString
        {
            get { return WebConfigurationManager.ConnectionStrings["localsqlVClip"].ConnectionString; }
        }
    }

    public class ConvertUtility
    {
        public static string FormatTimeVn(DateTime dt, string defaultText)
        {
            if (ToDateTime(dt) != new DateTime(1900, 1, 1))
                return dt.ToString("dd-mm-yy");
            else
                return defaultText;
        }
        public static double ToDouble1(string obj)
        {
            double retVal;
            try
            {
                obj = obj.Replace(",", "").Replace(".", ",").Replace(" ", "");

                retVal = Convert.ToDouble(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }
        public static int ToInt32(object obj)
        {
            int retVal = 0;

            try
            {
                retVal = Convert.ToInt32(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }

        public static int ToInt32(object obj, int defaultValue)
        {
            int retVal = defaultValue;

            try
            {
                retVal = Convert.ToInt32(obj);
            }
            catch
            {
                retVal = defaultValue;
            }

            return retVal;
        }

        public static string ToString(object obj)
        {
            string retVal;

            try
            {
                retVal = Convert.ToString(obj);
            }
            catch
            {
                retVal = String.Empty;
            }

            return retVal;
        }

        public static DateTime ToDateTime(object obj)
        {
            DateTime retVal;
            try
            {
                retVal = Convert.ToDateTime(obj);
            }
            catch
            {
                retVal = DateTime.Now;
            }
            if (retVal == new DateTime(1, 1, 1)) return DateTime.Now;

            return retVal;
        }

        public static DateTime ToDateTime(object obj, DateTime defaultValue)
        {
            DateTime retVal;
            try
            {
                retVal = Convert.ToDateTime(obj);
            }
            catch
            {
                retVal = DateTime.Now;
            }
            if (retVal == new DateTime(1, 1, 1)) return defaultValue;

            return retVal;
        }

        public static bool ToBoolean(object obj)
        {
            bool retVal;

            try
            {
                retVal = Convert.ToBoolean(obj);
            }
            catch
            {
                retVal = false;
            }

            return retVal;
        }

        public static double ToDouble(object obj)
        {
            double retVal;

            try
            {
                retVal = Convert.ToDouble(obj);
            }
            catch
            {
                retVal = 0;
            }

            return retVal;
        }

        public static double ToDouble(object obj, double defaultValue)
        {
            double retVal;

            try
            {
                retVal = Convert.ToDouble(obj);
            }
            catch
            {
                retVal = defaultValue;
            }

            return retVal;
        }

        public static String FormatValue(long number)
        {
            return number.ToString("#,##0");
        }

        public static DateTime ParseReportDate(String dateText)
        {
            try
            {
                return DateTime.ParseExact(dateText, "dd/MM/yyyy", null);
            }
            catch (Exception ex)
            {
                return DateTime.Today;
            }
        }
    }

    public class MoEntity997
    {
        public Int64 ID { get; set; }

        public string UserID { get; set; }

        public string ServiceID { get; set; }

        public string MobileOperator { get; set; }

        public string CommandCode { get; set; }

        public string Info { get; set; }

        public DateTime Timestamp { get; set; }

        public int Responded { get; set; }

        public string RequestID { get; set; }

    }

    public class MTInfo
    {
        private int _iD;
        public int ID
        {
            get { return _iD; }
            set { _iD = value; }
        }

        private string _user_ID;
        public string User_ID
        {
            get { return _user_ID; }
            set { _user_ID = value; }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        private string _service_ID;
        public string Service_ID
        {
            get { return _service_ID; }
            set { _service_ID = value; }
        }

        private string _command_Code;
        public string Command_Code
        {
            get { return _command_Code; }
            set { _command_Code = value; }
        }

        private int _message_Type;
        public int Message_Type
        {
            get { return _message_Type; }
            set { _message_Type = value; }
        }

        private string _request_ID;
        public string Request_ID
        {
            get { return _request_ID; }
            set { _request_ID = value; }
        }

        private int _total_Message;
        public int Total_Message
        {
            get { return _total_Message; }
            set { _total_Message = value; }
        }

        private int _message_Index;
        public int Message_Index
        {
            get { return _message_Index; }
            set { _message_Index = value; }
        }

        private int _isMore;
        public int IsMore
        {
            get { return _isMore; }
            set { _isMore = value; }
        }

        private int _content_Type;
        public int Content_Type
        {
            get { return _content_Type; }
            set { _content_Type = value; }
        }

        private int _serviceType;
        public int ServiceType
        {
            get { return _serviceType; }
            set { _serviceType = value; }
        }

        private string _partnerID;
        public string PartnerID
        {
            get { return _partnerID; }
            set { _partnerID = value; }
        }

        private string _operator;
        public string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

    }

}