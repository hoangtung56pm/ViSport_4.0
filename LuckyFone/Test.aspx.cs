using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OracleCommand = Oracle.DataAccess.Client.OracleCommand;
using OracleConnection = Oracle.DataAccess.Client.OracleConnection;
using OracleDataReader = Oracle.DataAccess.Client.OracleDataReader;


namespace LuckyFone
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
           
            log4net.ILog Log = log4net.LogManager.GetLogger(typeof(PublicController));

            string connstring = AppEnv.LuckyFoneOracleGpcViettel;
            #region Process

            using (var conn = new OracleConnection(connstring))
            {
                conn.Open();

                string year = DateTime.Now.Year.ToString();
                string month = DateTime.Now.Month.ToString();
                string day = (DateTime.Now.Day).ToString();
                string hour = DateTime.Now.Hour.ToString();
                string minute = (DateTime.Now.AddMinutes(-10).Minute).ToString();
                string second = DateTime.Now.Second.ToString();

                string oldParameterTime = month + "-" + day + "-" + year + " " + hour + ":" + minute + ":" + second;

                if (month.Length == 1)
                {
                    month = "0" + month;
                }

                if (day.Length == 1)
                {
                    day = "0" + day;
                }

                string oldParameter = year + month + day;

                string tableMoLog = " SMSDT.SMS_RECEIVE_LOG PARTITION (P_" + oldParameter.Substring(0, 4) + "_" +
                                    oldParameter.Substring(4, 2);
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

                Log.Debug("SQL : " + oldParameterTime);
                Log.Debug(" ");

                string sql = "Select USER_ID, " +
                           "SERVICE_ID, " +
                           "MOBILE_OPERATOR, " +
                           "COMMAND_CODE, " +
                           "INFO, " +
                           "TIMESTAMP, " +
                           "RESPONDED, " +
                           "REQUEST_ID " +
                           " From " + tableMoLog + " Where To_Char(TIMESTAMP,'YYYYMMDD')='" + oldParameter + "' " +
                           "and TIMESTAMP > to_date('" + oldParameterTime + "', 'mm-dd-yyyy hh24:mi:ss' ) AND MOBILE_OPERATOR = 'GPC' "

                           ;

                Log.Debug("********** LUCKFONE LOG SQL VINA-VIETTEL **********");
                Log.Debug("SQL : " + sql);
                Log.Debug(" ");
                Log.Debug(" ");

                using (var comm = new OracleCommand(sql, conn))
                {
                    using (OracleDataReader rdr = comm.ExecuteReader())
                    {
                        int count = 0;
                        //SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
                        //dbConn.Open();

                        while (rdr.Read())
                        {
                            count = count + 1;
                            Log.Debug("VALUE : " + rdr["User_ID"] + "|" + rdr["MOBILE_OPERATOR"]);

                            //var item = new MoEntity997();
                            //item.CommandCode = rdr["COMMAND_CODE"].ToString();
                            ////item.Info = rdr["INFO"].ToString();
                            //item.Info = string.Empty;
                            //item.MobileOperator = rdr["MOBILE_OPERATOR"].ToString();
                            //item.RequestID = rdr["REQUEST_ID"].ToString();
                            //item.Responded = ConvertUtility.ToInt32(rdr["RESPONDED"].ToString());
                            //item.ServiceID = rdr["SERVICE_ID"].ToString();
                            //item.Timestamp = ConvertUtility.ToDateTime(rdr["TIMESTAMP"].ToString());
                            //item.UserID = rdr["USER_ID"].ToString();
                            //PublicController.LuckyfoneMoInsert(item);

                        }

                        Log.Debug("Count : " + count);
                    }
                }
            }

            #endregion
           
        }


    }
}