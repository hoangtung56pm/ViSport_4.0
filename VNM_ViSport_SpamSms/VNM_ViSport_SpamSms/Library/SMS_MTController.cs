using System;
using System.Data;
using System.Threading;
using VNM_ViSport_SpamSms.SMS_MT;

namespace VNM_ViSport_SpamSms.Library
{
    public class SMS_MTController
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(SMS_MTController));
        private static bool isRun = true;

        #region Proccess MT
        public static ViSport_S2_Registered_UsersInfo initInfo(DataRow row)
        {
            ViSport_S2_Registered_UsersInfo info = new ViSport_S2_Registered_UsersInfo();
            try
            {

                info.ID = ConvertUtility.ToInt32(row["ID"]);
                info.User_ID = row["User_ID"].ToString().Trim();
                info.Request_ID = ConvertUtility.ToString(row["Request_ID"]).Trim();
                info.Service_ID = ConvertUtility.ToString(row["Service_ID"]).Trim();

                info.Command_Code = ConvertUtility.ToString(row["Command_Code"]).Trim();

                info.Sub_Code = ConvertUtility.ToString(row["Sub_Code"]).Trim();

                info.Service_Type = ConvertUtility.ToInt32(row["Service_Type"]);
                info.Request_ID = ConvertUtility.ToString(row["Request_ID"]).Trim();
                info.Charging_Count = ConvertUtility.ToInt32(row["Charging_Count"]);
                info.FailedChargingTimes = Convert.ToInt32(row["FailedChargingTimes"]);
                info.RegisteredTime = ConvertUtility.ToDateTime(row["RegisteredTime"]);
                info.ExpiredTime = ConvertUtility.ToDateTime(row["ExpiredTime"]);
                info.Registration_Channel = ConvertUtility.ToString(row["Registration_Channel"]);
                info.Status = ConvertUtility.ToInt32(row["Status"]);
                info.Operator = ConvertUtility.ToString(row["Operator"]);
                info.IsLock = ConvertUtility.ToInt32(row["IsLock"]);                
            }
            catch (Exception ex)
            {
                _logger.Info(string.Format("error! init info user_id = {0}, Request_ID = {1}, Reason = {2}", row["User_ID"].ToString().Trim(), row["Request_ID"].ToString().Trim(), ex.StackTrace) + Environment.NewLine);
                return null;
            }
            return info;
        }

        public void AddSMSToQueThread()
        {
            int LoopTimeInMiliSecound = SMS.Default.ProccessTimeLoop;
            
            while (isRun)
            {
                isRun = false;
                if (MSMProccess.MT_PROC_QUE.Count < 100)
                {
                    DataTable tbl = SMS_MTDB_SQL.GetMTByStatus(false);
                    EnQueue(tbl);
                }
                               
                isRun = true;
                Thread.Sleep(LoopTimeInMiliSecound); //1000 * 60 * 60 = 1h
            }
        }

        private void EnQueue(DataTable tbl)
        {
            int step = 1;

            if (tbl != null && tbl.Rows.Count > 0 && MSMProccess.MT_PROC_QUE.Count < 100)
            {
                _logger.Info(" ");
                _logger.Info(" ");
                _logger.Info(" ");
                _logger.Info(string.Format("MT_PROC_QUE count: {0}", MSMProccess.MT_PROC_QUE.Count));                
                _logger.Info(string.Format("******step: {0} at {1}", step++, DateTime.Now));
                _logger.Info(string.Format("******Number of User: {0}", tbl.Rows.Count));

                //Get list of MT id
                string lstId = "0";
                for (int i = 0; i < tbl.Rows.Count; i++)
                {
                    lstId += "," + Convert.ToString(tbl.Rows[i]["ID"]);
                }
                SMS_MTDB_SQL.MTUpdateByListId(lstId);

                foreach (DataRow row in tbl.Rows)
                {
                    try
                    {
                        ViSport_S2_Registered_UsersInfo info = initInfo(row);                        
                        MSMProccess.MT_PROC_QUE.Enqueue(info);                        
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(string.Format("error! {0}",  ex.Message + " -- " + ex.StackTrace) + Environment.NewLine);
                    }
                }

            }

            ViSport_S2_SMS_MTController.UpdateStatus(0); //UPDATE JobStatus Sau Khi Gui MT Xong;
        }
        #endregion
    }
}
