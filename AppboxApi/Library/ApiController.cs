using System;
using System.Data;
using System.Net;
using System.Web;
using System.Web.Configuration;
using AppboxApi.Library.Entity;
using AppboxApi.Library.SQLHelper;
using AppboxApi.Library.Utilities;
using log4net;

namespace AppboxApi.Library
{
    public class ApiController
    {
        static readonly ILog Logger = LogManager.GetLogger("ApiController");

        #region DATA PROCESS

        public static DataTable ApiReceiverRegisterLogAdd(VmsReceiverRegisterLog e)
        {
            DataSet ds = SqlHelper.ExecuteDataset(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, "Vms_Receiver_Register_Log_Insert",
                            e.UserName,
                            e.Password,
                            e.ServiceId,
                            e.Msisdn,
                            e.SubsTime,
                            e.Params,
                            e.Mo);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return null;
        }

        public static DataTable ApiVmsAppboxRegisteredUsersInfo(string userId)
        {
            DataSet ds = SqlHelper.ExecuteDataset(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, 
                "Vms_Appbox_Registered_Users_Info", userId);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            return new DataTable();
        }

        public static bool ApiVmsAppboxRegisteredUsersAdd(VmsAppboxRegisteredUser e)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, "Vms_Appbox_Registered_Users_Add"
                                         , e.UserId
                                         , e.RequestId
                                         , e.ServiceId
                                         , e.CommandCode
                                         , e.ChargingCount
                                         , e.FailedChargingTimes
                                         , e.RegisteredTime
                                         , e.ExpiredTime
                                         , e.RegistrationChannel
                                         , e.Status
                                         , e.Password
                                         , e.PartnerId
                                     );

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public static bool ApiVmsAppboxRegisteredUsersUnScribe(string userId, string unChannel)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, "Vms_Appbox_Registered_Users_UnSubcribe"
                                         , userId
                                         , unChannel
                                     );

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public static bool ApiVmsAppboxRegisteredUsersRenewal(string userId, int type)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, "Vms_Appbox_Registered_Users_Renewal"
                                         , userId
                                         , type
                                     );

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public static bool ApiVmsReceiverRegisterLogInsert(VmsReceiverRegisterLog item)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, "Vms_Receiver_Register_Log_Insert"
                                         , item.UserName
                                         , item.Password
                                         , item.ServiceId
                                         , item.Msisdn
                                         , item.SubsTime
                                         , item.Params
                                         , item.Mo
                                     );

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public static bool ApiVmsAppboxMoLog(VmsAppboxMoLog item)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, "Vms_Appbox_Mo_Insert"
                                         , item.UserId
                                         , item.ShortCode
                                         , item.Moseq
                                         , item.CmdCode
                                         , item.MsgBody
                                         , item.UserName
                                         , item.Password
                                         , item.Status
                                     );

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public static bool ApiVmsAppboxMtLog(VmsAppboxMtLog item)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, "Vms_Appbox_Mt_Insert"
                                         , item.UserId
                                         , item.ShortCode
                                         , item.MtSeq
                                         , item.MsgType
                                         , item.MsgTitle
                                         , item.MsgBody.Replace("+"," ")
                                         , item.MoSeq
                                         , item.ProcResult
                                         , item.CpId
                                         , item.ServiceId
                                         , item.ContentId
                                         , item.Price
                                         , item.Channel
                                         , item.SrcPort
                                         , item.DestPort
                                         , item.UserName
                                         , item.Password
                                     );
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public static bool ApiVmsAppboxGamelinkLog(VmsAppboxGamelinkLog item)
        {
            try
            {
                SqlHelper.ExecuteNonQuery(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, "Vms_Appbox_GameLink_Log_Add"
                                         , item.GameId
                                         , item.Msisdn
                                         , item.ReqTime
                                         , item.ShortCode
                                         , item.ReqId
                                         , item.UserName
                                         , item.Password
                                     );

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public static void ApiVmsAppboxBillingLog(VmsAppboxBillingLog item)
        {
            SqlHelper.ExecuteNonQuery(WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString, "Vms_Appbox_Billing_Log_Add"
                                         , item.CpRequestId
                                         , item.Mobile
                                         , item.ChargeType
                                         , item.ResponseCode
                                         , item.Price
                                         , item.Type
                                         , item.Description
                                     );
        }

        #endregion

        #region PUBLIC METHOD

        public static void MtApi(string userId, string title, string body, string moSeq, int procResult, int price)
        {
            #region XU LY GUI MT PHAN HOI

            var mt = new VmsAppboxMtLog();
            mt.ShortCode = "9210"; //src : ex 8008
            mt.UserId = userId; //dest
            mt.MtSeq = ConvertUtility.ToInt32(SecurityMethod.RandomStringNumber(6));
            mt.MsgType = "Text";

            //mt.MsgTitle = HttpContext.Current.Server.UrlEncode(title);
            mt.MsgTitle = title;

            //mt.MsgBody = HttpContext.Current.Server.UrlEncode(body);
            mt.MsgBody = body;

            mt.MoSeq = moSeq;

            //* 0 = the MO message is not charged 
            //* 1 = the MO message is charged 
            mt.ProcResult = procResult;

            mt.CpId = AppEnv.GetSetting("cpId");
            mt.ServiceId = "26";
            mt.ContentId = "26";

            mt.Price = price;
            mt.Channel = "SMS";
            mt.SrcPort = 1;
            mt.DestPort = 1;
            mt.UserName = "boxapp";
            mt.Password = "Centech12boxapp34";

            string url = "http://113.187.31.26:8016/smsmt/boxapp" +
                         "?src=" + mt.ShortCode + 
                         "&dest=" + mt.UserId + 
                         "&mtseq=" + mt.MtSeq + 
                         "&msgtype=" + mt.MsgType + 
                         "&msgtitle=" + mt.MsgTitle + 
                         "&msgbody=" + mt.MsgBody + 
                         "&moseq=" + mt.MoSeq + 
                         "&procresult=" + mt.ProcResult + 
                         "&cpid=" + mt.CpId + 
                         "&serviceid=" + mt.ServiceId + 
                         "&contentid=" + mt.ContentId + 
                         "&price=" + mt.Price + 
                         "&channel=" + mt.Channel + 
                         "&username=" + mt.UserName + 
                         "&password=" + mt.Password;

            Logger.Debug("----- VMG SEND MT URL : " + url);

            var httpReq = (HttpWebRequest)WebRequest.Create(url);
            httpReq.AllowAutoRedirect = false;
            httpReq.Method = "GET";
            var httpRes = (HttpWebResponse)httpReq.GetResponse();
            int status = (int)httpRes.StatusCode;

            Logger.Debug("----- STATUS_CODE FROM MGAME : " + status);

            if (status == 200)//GUI THANH CONG TOI MGAME Platform
            {
                ApiVmsAppboxMtLog(mt);
            }

            httpRes.Close();


            #endregion
        }

        #endregion

    }
}