#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\Zalo\Callback.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D1982C0B0335E78A74E4C01A37D5AF302D8B0249"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\Zalo\Callback.aspx.cs"
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using ZaloPageSDK.com.vng.zalosdk.entity;
using ZaloPageSDK.com.vng.zalosdk.exceptions;
using ZaloPageSDK.com.vng.zalosdk.service;

public partial class Zalo_Callback : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!Page.IsPostBack)
        {
            //Response.AppendHeader("receivedCode", "200");

            //?event=msgstt&status=delivered&msgid={1}&timestamp={2}&mac={3} 

            //log4net.ILog _log = log4net.LogManager.GetLogger("Zalo_Callback");

            //string events = Request.QueryString["event"];
            //string status = Request.QueryString["status"];
            //string msgId = Request.QueryString["msgid"];
            //string timeStamp = Request.QueryString["timestamp"];
            //string mac = Request.QueryString["mac"];

            //_log.Debug(" ");
            //_log.Debug(" ");
            //_log.Debug("--------------- Zalo Call Back URL ----------------------");
            //_log.Debug("Zalo Callback ===> events:" + events + "|status:" + status + "|msgId:" + msgId + "|Mac:" + mac);
            //_log.Debug(" ");
            //_log.Debug(" ");

            //if (!string.IsNullOrEmpty(events) && 
            //    !string.IsNullOrEmpty(status) && 
            //    !string.IsNullOrEmpty(msgId) && 
            //    !string.IsNullOrEmpty(timeStamp) && 
            //    !string.IsNullOrEmpty(mac))
            //{
            //    try
            //    {
            //        var item = new CallbackInfo();
            //        item.Event = events;
            //        item.Mac = mac;
            //        item.MsgId = msgId;
            //        item.Status = status;
            //        item.TimeStamp = timeStamp;

            //        ZaloController.ZaloCallbackNotificationInsert(item);
            //        Response.AddHeader("receivedCode", "200");
            //    }
            //    catch (Exception ex)
            //    {
            //        _log.Debug("--------------- Zalo Call Back URL ERROR ----------------------");
            //        _log.Debug("Error : " + ex);
            //        _log.Debug(" ");
            //    }
            //}
        }
    }
    protected void btnTest_Click(object sender, EventArgs e)
    {
        string lotTime = DateTime.Now.Year + "-" + DateTime.Now.Month;
        lotTime = lotTime + "-" + DateTime.Now.AddDays(-1).Day;

        DataTable dtCon = ZaloController.ZaloGetLotteryContent(1, lotTime);
        ApiZaloCallForSendZms("84987765522", dtCon.Rows[0]["lot_content"].ToString());
    }

    private static int ApiZaloCallForSendZms(string userId, string message)
    {
        log4net.ILog Log = log4net.LogManager.GetLogger("ApiZaloCallForSendZms");
        int reValue = -1;
        try
        {
            const string pageId = "998973463693604902";
            const string secretKey = "yjODClYNJUSJH8Qt20vT";

            long toUid = long.Parse(userId);
            var factory = new ZaloServiceFactory(pageId, secretKey);

            ZaloMessageService messageService = factory.getZaloMessageService();
            //ZaloPageResult objResult = messageService.sendTextMessage(toUid, message, message, false);

            //<pageName> Gui ban <header>
            //<Content>

            Dictionary<string, string> data = new Dictionary<string, string>();

            data.Add("pageName", "VMG Media");
            data.Add("header", "KQXS");
            data.Add("Content", message);

            ZaloPageResult objResult = messageService.sendTemplateTextMessageByPhoneNum(toUid, "602ed6c4ea8103df5a90", data, "Zms Test", true);

            reValue = objResult.getError();

            Log.Debug(" ");
            Log.Debug(" ");
            Log.Debug("--------------- Zalo CALL API RESULT ----------------------");
            Log.Debug("Zalo API RESULT Err : " + objResult.getError());
            Log.Debug("Zalo API RESULT Id : " + objResult.getId());
            Log.Debug(" ");
            Log.Debug(" ");
        }
        catch (ZaloSdkException ex)
        {
            Log.Debug(" ");
            Log.Debug(" ");
            Log.Debug("--------------- Zalo CALL API Error ----------------------");
            Log.Debug("Zalo API Err : " + ex.getErrorCode());
            Log.Debug("Zalo API Mes : " + ex.getMessage());
            Log.Debug(" ");
            Log.Debug(" ");
        }
        catch (Exception ex)
        {
            Log.Debug("Zalo API StackTrace : " + ex.StackTrace);
        }

        return reValue;
    }
}

#line default
#line hidden
