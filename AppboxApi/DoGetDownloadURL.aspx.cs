using System;
using System.Text;
using System.Web.UI;
using AppboxApi.Library;
using AppboxApi.Library.Entity;
using AppboxApi.Library.Utilities;
using log4net;

namespace AppboxApi
{
    public partial class DoGetDownloadURL : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ILog logger = LogManager.GetLogger("DoGetDownloadURL");

            try
            {
                string msisdn = Request.QueryString["Msisdn"];
                string reqTime = Request.QueryString["reqTime"];
                string shortCode = Request.QueryString["shortcode"];
                string reqId = Request.QueryString["reqId"];
                string username = Request.QueryString["username"];
                string password = Request.QueryString["password"];
                string gameId = Request.QueryString["GameID"];

                logger.Debug(" ");
                logger.Debug(" ");
                logger.Debug("----- VMS API CALL DoGetDownloadURL ----- :" + "msisdn : " + msisdn + " |reqTime : " + reqTime +
                                                " |shortCode : " + shortCode + " |reqId : " + reqId + " |userName : " + username +
                                                " |password : " + password + " |GameId : " + gameId);
                logger.Debug(" ");
                logger.Debug(" ");

                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    var item = new VmsAppboxGamelinkLog();
                    item.GameId = ConvertUtility.ToInt32(gameId);
                    item.Msisdn = msisdn;
                    item.ReqTime = reqTime;
                    item.ShortCode = shortCode;
                    item.ReqId = reqId;
                    item.UserName = username;
                    item.Password = password;

                    ApiController.ApiVmsAppboxGamelinkLog(item);

                    string key = DateTime.Now.ToString("ddMMyyyy") + gameId;
                    key = SecurityMethod.MD5Encrypt(key);

                    string strValue = string.Format("gameid={0}|reqid={1}|msisdn={2}|key={3}|source={4}|type={5}", gameId, reqId, msisdn, key, "WAP", "2");
                    byte[] dataEncode = Encoding.UTF8.GetBytes(strValue);
                    Base64Encoder myEncoder = new Base64Encoder(dataEncode);
                    StringBuilder encodevaulue = new StringBuilder();
                    encodevaulue.Append(myEncoder.GetEncoded());

                    string url = "http://vmgame.vn/wap/dlgame.ashx?value=" + encodevaulue;
                    logger.Debug("----- VMS API CALL DoGetDownloadURL URL RESPONSE ----- :" + url);

                    Response.Write(url);
                }
            }
            catch (Exception ex)
            {
                logger.Debug(" ");
                logger.Debug(" ");
                logger.Debug("----- VMS API CALL DoGetDownloadURL ----- :" + ex);
                logger.Debug(" ");
                logger.Debug(" ");
            }

        }
    }
}