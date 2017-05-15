using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Threading;
using System.Data;
using System.Configuration;
using System.Net;
using Threaded;
using System.IO;
using System.Web;
using VNM_ViSport_Charging_SpamSms.ChargingGateway;
using VNM_ViSport_Charging_SpamSms.SentMT;
namespace VNM_ViSport_Charging_SpamSms.Library
{
    public class MSMProccess
    {
        public static Threaded.TQueue<ViSport_S2_Registered_SpamSms_UserInfo> MT_PROC_QUE = new Threaded.TQueue<ViSport_S2_Registered_SpamSms_UserInfo>(500);
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

        public static void ChargeUser(ViSport_S2_Registered_SpamSms_UserInfo info)
        {
            int reval = -1;
            try
            {
                _logger.Info(string.Format("Started charging User_ID= {0}, Request_ID = {1}, ID = {2}, IsTest = {3}", info.User_Id, info.Request_Id, info.Id, SMS.Default.IsTest));

                //if (info == null)
                //{
                //    return;
                //}

                #region charging here

                string userName = SMS.Default.UserName;
                string userPass = SMS.Default.Password;
                string cpId = SMS.Default.CpID;
                string price= string.Empty;
                string serviceType = "Charge Sub Sport Game ";
                string serviceName = "ViSport_Hero";

                string notEnoughMoney = "Result:12,Detail:Not enough money.";

                string status = "1";
                string returnValue;
                if (SMS.Default.IsTest == "1")
                {
                    returnValue = "1";
                }
                else
                {
                    if (info.FailedChargingTimes <= ConvertUtility.ToInt32(SMS.Default.FailCharge))
                    {

                        price = "4000";
                        returnValue = objCharge.PaymentVnmWithAccount(info.User_Id, price, serviceType, serviceName, userName, userPass, cpId);

                        if (returnValue.Trim() == notEnoughMoney)
                        {

                            price = "3000";
                            returnValue = objCharge.PaymentVnmWithAccount(info.User_Id, price, serviceType, serviceName, userName, userPass, cpId);

                            if (returnValue.Trim() == notEnoughMoney)
                            {

                                price = "2000";
                                returnValue = objCharge.PaymentVnmWithAccount(info.User_Id, price, serviceType, serviceName, userName, userPass, cpId);

                                if (returnValue.Trim() == notEnoughMoney)
                                {

                                    price = "1000";
                                    returnValue = objCharge.PaymentVnmWithAccount(info.User_Id, price, serviceType, serviceName, userName, userPass, cpId);

                                }

                            }
                        }
                    }
                    else
                    {
                        returnValue = "Exceed";
                        reval = -1;
                    }
                }

                if (returnValue == "1")
                {
                    reval = 1;
                }

                #endregion

                #region Ghi log vao bang ViSport_S2_Charged_Users_Log

                var logInfo = new ViSport_S2_Registered_SpamSms_UserInfo();

                logInfo.Id = info.Id;
                logInfo.User_Id = info.User_Id;
                logInfo.Request_Id = info.Request_Id;
                logInfo.Service_Id = info.Service_Id;
                logInfo.Command_Code = info.Command_Code;

                logInfo.Service_Type = info.Service_Type;
                logInfo.Charging_Count = info.Charging_Count;
                logInfo.FailedChargingTimes = info.FailedChargingTimes;

                logInfo.RegisteredTime = info.RegisteredTime;
                logInfo.ExpiredTime = DateTime.Now.AddDays(1);

                logInfo.Registration_Channel = info.Registration_Channel;
                logInfo.Status = info.Status;
                logInfo.Operator = info.Operator;
                logInfo.Price = ConvertUtility.ToInt32(price);

                if (reval > 0)
                {
                    _logger.Info(string.Format("Succ!Charging to {0} is succ with return value {1}, Request_ID = {2}", info.User_Id, reval, info.Request_Id));
                    logInfo.Reason = "Succ";


                    if (SMS.Default.IsTest == "0")
                    {
                        #region Gui MT cho khach hang thong bao gia han thanh cong

                        ServiceProviderService objSentMT = new ServiceProviderService();

                        string message = string.Empty;

                        DataTable dtQuestion = SMS_MTDB_SQL.GetQuestionInfoSportGameHero();
                        if (dtQuestion != null && dtQuestion.Rows.Count > 0)
                        {
                            message = dtQuestion.Rows[0]["Question"].ToString();

                            int questionIdnew = ConvertUtility.ToInt32(dtQuestion.Rows[0]["Id"].ToString());
                            string answer = dtQuestion.Rows[0]["Answer"].ToString();

                            SMS_MTDB_SQL.InsertSportGameHeroAnswerLog(info.User_Id, questionIdnew, message, answer, DateTime.Now, 0); // LUU LOG Question

                            string serviceId = info.Service_Id;
                            string commandCode = info.Command_Code;
                            string requestId = info.Request_Id;

                            DataTable dtMt = SMS_MTDB_SQL.CheckExistSendMt(info.User_Id);

                            if (dtMt != null && dtMt.Rows.Count > 0)
                            {
                                _logger.Info("Da gui MT cho so : " + info.User_Id);
                            }
                            else
                            {
                                if(!string.IsNullOrEmpty(message))
                                {
                                    int sentMt = objSentMT.sendMT(logInfo.User_Id, message, serviceId, commandCode, "1", requestId, "1", "1", "0", "0");
                                    _logger.Info("SentMt return: " + sentMt + "| UserId:" + logInfo.User_Id);
                                }

                                #region LOG SEND MT

                                var objMt = new ViSport_S2_SMS_MTInfo();
                                objMt.User_ID = info.User_Id;
                                objMt.Message = message;
                                objMt.Service_ID = serviceId;
                                objMt.Command_Code = commandCode;
                                objMt.Message_Type = 1;
                                objMt.Request_ID = requestId;
                                objMt.Total_Message = 1;
                                objMt.Message_Index = 0;
                                objMt.IsMore = 0;
                                objMt.Content_Type = 0;
                                objMt.ServiceType = 0;
                                objMt.ResponseTime = DateTime.Now;
                                objMt.isLock = false;
                                objMt.PartnerID = "VNM";
                                objMt.Operator = "vnmobile";
                                objMt.IsQuestion = 1;

                                SMS_MTDB_SQL.InsertSportGameHeroMt(objMt);

                                #endregion

                            }
                        }
                        
                        #endregion
                    }

                    SMS_MTDB_SQL.InsertLog(logInfo); //LOG DOANH THU
                }
                else
                {
                    //neu charge tien khong thanh cong thi ghi lai log loi, dong thoi tang FailedChargingTimes len 1.IsLock set = 0 de tien hanh charge lai
                    _logger.Info(string.Format("ERROR !Charging to {0} is fail, Request_ID = {1}", info.User_Id, info.Request_Id));
                    logInfo.ExpiredTime = info.ExpiredTime;
                    if (returnValue == "Exceed")
                    {
                        logInfo.Reason = "Exceed";
                    }
                    else
                    {
                        logInfo.Reason = returnValue;

                    }

                    if (SMS.Default.IsTest == "0")
                    {
                        SMS_MTDB_SQL.InsertLog(logInfo); //LOG DOANH THU
                    }
                }

                #endregion

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
                _logger.Info(string.Format("ERROR!Sending to {0} is error, Request_ID = {1}, Error mess = {2}", info.User_Id, info.Request_Id, ex.Message) + Environment.NewLine);
                SMS_MTDB_SQL.MTUpdateFail(info.Id);
            }

            Thread.Sleep(100);
        }

        #endregion
    }
}
