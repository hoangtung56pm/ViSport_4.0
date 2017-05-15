using System;
using System.Threading;
using System.Data;
using VNM_ViSport_SpamSms.ChargingGateway;
using VNM_ViSport_SpamSms.SentMT;
using VNM_ViSport_SpamSms.SMS_MT;

namespace VNM_ViSport_SpamSms.Library
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

            DataTable dt = ViSport_S2_SMS_MTController.GetMtContent(info.Sub_Code.ToUpper());
            if(dt != null && dt.Rows.Count > 0)
            {
                //if(!string.IsNullOrEmpty(dt.Rows[0]["MT"].ToString()))
                //{
                    try
                    {
                        int hour = ConvertUtility.ToInt32(dt.Rows[0]["Hour"].ToString());
                        string day = dt.Rows[0]["Day"].ToString();

                        //DataTable dtMt = ViSport_S2_SMS_MTController.CheckAlreadySendMt(info.User_ID,hour,day,info.Sub_Code);
                        //_logger.Info("Tham So Check CheckAlreadySendMt : " + info.User_ID + "| hour : " + hour + "| day : " + day);

                        //if (dtMt != null && dtMt.Rows[0]["RETURN_ID"].ToString() == "1")
                        //{
                        //    _logger.Info("Da gui MT cho so : " + info.User_ID + "| hour : " + hour + "| day : " + day);
                        //}
                        //else
                        //{
                            //string message = dt.Rows[0]["MT"].ToString(); //Message lay tu DB;

                            //string messageKd = UnicodeUtility.UnicodeToKoDau(message); //Loai bo dau;

                            
                            #region Gui MT cho khach hang

                            var objSentMt = new ServiceProviderService();

                            string serviceId = info.Service_ID;
                            string commandCode = info.Command_Code;
                            string requestId = info.Request_ID;

                            //for (int i = 0; i < 3;i++ )
                            //{
                                const int mtOrder = 1;
                                string mtContent = string.Empty;

                                if (!string.IsNullOrEmpty(dt.Rows[0]["MT1"].ToString()))
                                {
                                    mtContent = dt.Rows[0]["MT1"].ToString();
                                }
                                else if(!string.IsNullOrEmpty(dt.Rows[0]["MT2"].ToString()))
                                {
                                    mtContent = dt.Rows[0]["MT2"].ToString();
                                }
                                else if (!string.IsNullOrEmpty(dt.Rows[0]["MT3"].ToString()))
                                {
                                    mtContent = dt.Rows[0]["MT3"].ToString();
                                }

                                //if(i==0)
                                //{
                                //    mtOrder = 1;
                                //    mtContent = dt.Rows[0]["MT1"].ToString();
                                //}
                                //else if(i == 1)
                                //{
                                //    mtOrder = 2;
                                //    mtContent = dt.Rows[0]["MT2"].ToString();
                                //}
                                //else
                                //{
                                //    mtOrder = 3;
                                //    mtContent = dt.Rows[0]["MT3"].ToString();
                                //}

                                DataTable dtMt = ViSport_S2_SMS_MTController.CheckAlreadySendMt(info.User_ID, hour, day, info.Sub_Code,mtOrder);
                                _logger.Info("Tham So Check CheckAlreadySendMt : " + info.User_ID + " | hour : " + hour + " | day : " + day + " | sub_code :" + info.Sub_Code + " | mt_order :" + mtOrder);

                                if(dtMt != null && dtMt.Rows.Count > 0)
                                {
                                    _logger.Info("Da gui MT cho so : " + info.User_ID + " | hour : " + hour + " | day : " + day + " | sub_code :" + info.Sub_Code + " | mt_order :" + mtOrder);
                                }
                                else
                                {
                                    _logger.Info(string.Format("Started Send MT User_ID= {0}, Request_ID = {1}, ID = {2}, Message = {3}", info.User_ID, info.Request_ID, info.ID, mtContent));

                                    if (!string.IsNullOrEmpty(mtContent))
                                    {
                                        int sentMt = objSentMt.sendMT(info.User_ID, mtContent, serviceId, commandCode, "1", requestId, "1", "1", "0", "0");
                                        _logger.Info("SentMt return: " + sentMt);
                                    }

                                    #region Log Sms MT

                                    var objMt = new ViSport_S2_SMS_MTInfo();
                                    objMt.User_ID = info.User_ID;
                                    objMt.Message = mtContent;
                                    objMt.Service_ID = serviceId;
                                    objMt.Command_Code = commandCode;
                                    objMt.Sub_Code = info.Sub_Code;
                                    objMt.Message_Type = 1;
                                    objMt.Request_ID = requestId;
                                    objMt.Total_Message = 1;
                                    objMt.Message_Index = 0;
                                    objMt.IsMore = 0;
                                    objMt.Content_Type = 0;
                                    objMt.ServiceType = 0;
                                    objMt.ResponseTime = DateTime.Now;
                                    objMt.isLock = false;
                                    objMt.PartnerID = "Xzone";
                                    objMt.Operator = "ViSport_MT";

                                    objMt.MtOrder = mtOrder;

                                    ViSport_S2_SMS_MTController.InsertMtSpamSmsUser(objMt);

                                    #endregion
                                }

                            //}

                            #endregion
                        //}
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
                //}
            }

            Thread.Sleep(100);
        }

        #endregion
    }
}
