using System;
using System.Threading;
using System.Data;
using System.Web.Configuration;
using VNM_VClip_SpamSms.ChargingGateway;
using VNM_VClip_SpamSms.SMS_MT;

namespace VNM_VClip_SpamSms.Library
{
    public class MSMProccess
    {
        public static Threaded.TQueue<ViSport_S2_Registered_UsersInfo> MT_PROC_QUE = new Threaded.TQueue<ViSport_S2_Registered_UsersInfo>(500);
        private static WebServiceCharging3g objCharge = new WebServiceCharging3g();
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(MSMProccess));

        #region base function

        public static string base64Encode(string data)
        {
            try
            {
                byte[] encData_byte = new byte[data.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(data);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception e)
            {
                _logger.Info(e.Message);
                throw new Exception("Error in base64Encode" + e.Message);
            }
        }

        public static void SentMtInfo(ViSport_S2_Registered_UsersInfo info)
        {

            DataTable dt = ViSport_S2_SMS_MTController.GetMtContent();
            //if(dt != null && dt.Rows.Count > 0)
            //{
            //    if(!string.IsNullOrEmpty(dt.Rows[0]["MT"].ToString()))
            //    {
                    try
                    {
                        string day = DateTime.Now.ToString("yyyyMMdd");
                        
                        DataTable dtMt = ViSport_S2_SMS_MTController.CheckAlreadySendMt(info.User_ID,0,day);
                        if(dtMt != null && dtMt.Rows.Count > 0)
                        {
                            _logger.Info("Da gui MT cho so : " + info.User_ID + "| day : " + day);
                        }
                        else
                        {
                            string message = string.Empty;
                            string isMt = dt.Rows[0]["IsMt"].ToString();

                            if(isMt == "1")
                            {
                                message = GetSetting("MessageTue");
                            }
                            else if(isMt == "2")
                            {
                                message = GetSetting("MessageThu");
                            }
                            else if(isMt == "3")
                            {
                                message = GetSetting("MessageSat");
                            }

                            _logger.Info(string.Format("Started Send MT User_ID= {0}, Request_ID = {1}, ID = {2}, Message = {3}", info.User_ID, info.Request_ID, info.ID, message));

                            //#region Gui MT cho khach hang

                            //var objSentMt = new ServiceProviderService();

                            //string serviceId = info.Service_ID;
                            //string commandCode = info.Command_Code;
                            //string requestId = info.Request_ID;

                            //objSentMt.sendMT(info.User_ID, message, serviceId, commandCode, "1", requestId, "1", "1", "0", "0");

                            //var objMt = new ViSport_S2_SMS_MTInfo();
                            //objMt.User_ID = info.User_ID;
                            //objMt.Message = message;
                            //objMt.Service_ID = serviceId;
                            //objMt.Command_Code = commandCode;
                            //objMt.Message_Type = 1;
                            //objMt.Request_ID = requestId;
                            //objMt.Total_Message = 1;
                            //objMt.Message_Index = 0;
                            //objMt.IsMore = 0;
                            //objMt.Content_Type = 0;
                            //objMt.ServiceType = 0;
                            //objMt.ResponseTime = DateTime.Now;
                            //objMt.isLock = false;
                            //objMt.PartnerID = "Xzone";
                            //objMt.Operator = "VClip_MT";
                            //ViSport_S2_SMS_MTController.InsertVClip(objMt);

                            //#endregion

                        }
                    }
                    catch (TimeoutException e)
                    {
                        _logger.Error(string.Concat("mySendMT - ", e.StackTrace));
                        _logger.Error(string.Concat("mySendMT - ", e.Message));
                        MT_PROC_QUE.Enqueue(info);

                    }
                    catch (Exception ex)
                    {
                        //neu phat sinh loi he thong thi Update Islock = 0, de lan sau co the charge lai
                        _logger.Info(string.Format("ERROR!Sending to {0} is error, Request_ID = {1}, Error mess = {2}", info.User_ID, info.Request_ID, ex.Message) + Environment.NewLine);
                        SMS_MTDB_SQL.MTUpdateFail(info.ID);
                    }
            //    }
            //}

            Thread.Sleep(100);
        }

        #endregion

        #region Public Methods

        public static string GetSetting(string key)
        {
            return WebConfigurationManager.AppSettings[key];
        }

        #endregion

    }
}
