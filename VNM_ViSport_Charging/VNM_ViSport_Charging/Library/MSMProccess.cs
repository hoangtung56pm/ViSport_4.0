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
using VNM_ViSport_Charging.ChargingGateway;
using VNM_ViSport_Charging.SentMT;
namespace VNM_ViSport_Charging.Library
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

        public static void ChargeUser(ViSport_S2_Registered_UsersInfo info)
        {
            int reval = -1;
            try
            {
                _logger.Info(string.Format("Started charging User_ID= {0}, Request_ID = {1}, ID = {2}", info.User_ID, info.Request_ID, info.ID.ToString()));

                if (info == null)
                {
                    return;
                }

                #region charging here

                string userName = SMS.Default.UserName;
                string userPass = SMS.Default.Password;
                string cpId = SMS.Default.CpID;
                string price = SMS.Default.PriceSC;
                string serviceType = "Charge goi ";
                string serviceName = "ViSport";
                if (info.Service_Type == 1)
                {
                    price = SMS.Default.PriceSM;
                    serviceType += "SM";
                }
                else
                {
                    serviceType += "SC";
                }

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
                        returnValue = objCharge.PaymentVnmWithAccount(info.User_ID, price, serviceType, serviceName, userName, userPass, cpId);
                        if (returnValue.Trim() == "Result:12,Detail:Not enough money.")
                        {
                            price = "1000";
                            returnValue = objCharge.PaymentVnmWithAccount(info.User_ID, price, serviceType, serviceName, userName, userPass, cpId);
                            status = "2";
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

                ViSport_S2_Charged_Users_LogInfo logInfo = new ViSport_S2_Charged_Users_LogInfo();

                logInfo.ID = info.ID;
                logInfo.User_ID = info.User_ID;
                logInfo.Request_ID = info.Request_ID;
                logInfo.Service_ID = info.Service_ID;
                logInfo.Command_Code = info.Command_Code;
                logInfo.Service_Type = info.Service_Type;
                logInfo.Charging_Count = info.Charging_Count;
                logInfo.FailedChargingTimes = info.FailedChargingTimes;

                logInfo.RegisteredTime = DateTime.Now;
                if(status == "1")
                {
                    logInfo.ExpiredTime = info.ExpiredTime.AddDays(7);
                }
                else
                {
                    logInfo.ExpiredTime = info.ExpiredTime.AddDays(3);
                }
                    

                logInfo.Registration_Channel = info.Registration_Channel;
                logInfo.Status = info.Status;
                logInfo.Operator = info.Operator;
                logInfo.Price = ConvertUtility.ToInt32(price);

                if (reval > 0)
                {
                    //neu charge tien thanh cong thi tu dong gia han expiretime cho user them 7 ngay, dong thoi insert vao bang log charging, cong Charging_Count len 1, islock = 0  de lan sau charge tiep
                    _logger.Info(string.Format("Succ!Charging to {0} is succ with return value {1}, Request_ID = {2}", info.User_ID, reval, info.Request_ID));
                    logInfo.Reason = "Succ";
                    SMS_MTDB_SQL.InsertLog(logInfo);

                    #region Gui MT cho khach hang thong bao gia han thanh cong

                    ServiceProviderService objSentMT = new ServiceProviderService();

                    string message = SMS.Default.Message;
                    if (info.Service_Type == 1)
                    {
                        message = SMS.Default.Message;
                    }
                    else
                    {
                        message = SMS.Default.MessageSC;
                    }
                    string Service_ID = info.Service_ID;
                    string Command_Code = info.Command_Code;
                    string Request_ID = info.Request_ID;

                    objSentMT.sendMT(logInfo.User_ID, message, Service_ID, Command_Code, "1", Request_ID, "1", "1", "0", "0");
                           
                    #endregion
                }

                else
                {
                    //neu charge tien khong thanh cong thi ghi lai log loi, dong thoi tang FailedChargingTimes len 1.IsLock set = 0 de tien hanh charge lai
                    _logger.Info(string.Format("ERROR !Charging to {0} is fail, Request_ID = {1}", info.User_ID, info.Request_ID));
                    logInfo.ExpiredTime = info.ExpiredTime;
                    if (returnValue == "Exceed")
                    {
                        logInfo.Reason = "Exceed";
                    }
                    else
                    {
                        logInfo.Reason = "Error charging: " + returnValue;

                    }
                    SMS_MTDB_SQL.InsertLog(logInfo);
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
                _logger.Info(string.Format("ERROR!Sending to {0} is error, Request_ID = {1}, Error mess = {2}", info.User_ID, info.Request_ID, ex.Message) + Environment.NewLine);
                SMS_MTDB_SQL.MTUpdateFail(info.ID);
            }

            Thread.Sleep(100);
        }

        #endregion
    }
}
