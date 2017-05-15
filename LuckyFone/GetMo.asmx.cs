using System;
using System.Web.Services;
using Oracle.DataAccess.Client;

namespace LuckyFone
{
    /// <summary>
    /// Summary description for GetMo
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class GetMo : WebService, IJobExecutorSoap
    {

        public GetMo()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(GetMo));

        [WebMethod]
        public int Execute(int jobID)
        {
            try
            {
                string connstring = AppEnv.LuckyFoneOracleVms;
                #region Process

                using (var conn = new OracleConnection(connstring))
                {
                    conn.Open();

                    string year = DateTime.Now.Year.ToString();
                    string month = DateTime.Now.Month.ToString();
                    string day = (DateTime.Now.Day).ToString();


                    DateTime beforeTime = DateTime.Now.AddMinutes(-10);
                    string hour = beforeTime.Hour.ToString();
                    string minute = beforeTime.Minute.ToString();
                    string second = beforeTime.Second.ToString();

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

                    string tableMoLog = " VMSGW1.SMS_RECEIVE_LOG PARTITION (P_" + oldParameter.Substring(0, 4) + "_" +
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

                    string sql = "Select USER_ID, " +
                               "SERVICE_ID, " +
                               "MOBILE_OPERATOR, " +
                               "COMMAND_CODE, " +
                               "INFO, " +
                               "TIMESTAMP, " +
                               "RESPONDED, " +
                               "REQUEST_ID " +
                               " From " + tableMoLog + " Where To_Char(TIMESTAMP,'YYYYMMDD')='" + oldParameter + "' " +
                               "and TIMESTAMP > to_date('" + oldParameterTime + "', 'mm-dd-yyyy hh24:mi:ss' )"
                               ;

                    Log.Debug("********** LUCKFONE LOG SQL VMS **********");
                    Log.Debug("SQL : " + sql);
                    Log.Debug(" ");
                    Log.Debug(" ");

                    using (var comm = new OracleCommand(sql, conn))
                    {
                        using (OracleDataReader rdr = comm.ExecuteReader())
                        {
                            int count = 0;

                            while (rdr.Read())
                            {

                                count = count + 1;
                                var item = new MoEntity997();
                                item.CommandCode = rdr["COMMAND_CODE"].ToString();
                                item.Info = string.Empty;
                                item.MobileOperator = rdr["MOBILE_OPERATOR"].ToString();
                                item.RequestID = rdr["REQUEST_ID"].ToString();
                                item.Responded = ConvertUtility.ToInt32(rdr["RESPONDED"].ToString());
                                item.ServiceID = rdr["SERVICE_ID"].ToString();
                                item.Timestamp = ConvertUtility.ToDateTime(rdr["TIMESTAMP"].ToString());
                                item.UserID = rdr["USER_ID"].ToString();

                                //CHECK Exist User
                                PublicController.LuckyfoneCheckUser(item.UserID,
                                                                    item.ServiceID,
                                                                    item.MobileOperator,
                                                                    item.CommandCode,
                                                                    item.Info,
                                                                    DateTime.Now,
                                                                    item.RequestID);
                            }

                            Log.Debug("Count : " + count);
                        }
                    }

                    conn.Close();
                }

                #endregion
            }
            catch (Exception ex)
            {

                Log.Debug("Exception : " + ex);
            }

            return 1;
        }
    }
}
