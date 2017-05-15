#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\Zalo_Registered\ZaloController.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7B7CCB7892E67E7C322D68FA6EB4F19EBA66A01B"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\Zalo_Registered\ZaloController.cs"
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.ApplicationBlocks.Data;
using ZaloPageSDK.com.vng.zalosdk.entity;
using ZaloPageSDK.com.vng.zalosdk.exceptions;
using ZaloPageSDK.com.vng.zalosdk.service;

/// <summary>
/// Summary description for ZaloController
/// </summary>
public class ZaloController
{
	public ZaloController()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    #region Get Data Methods

    public static void ZaloMoInsert(ZaloMoInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionStringVClip, "Zalo_Mo_Insert"
                                     , entity.User_ID
                                     , entity.Request_ID
                                     , entity.Service_ID
                                     , entity.Command_Code
                                     , entity.Message
                                     , entity.Operator
                                 );
    }

    public static DataTable ZaloGetServiceInfo(string code)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVClip, "Zalo_Service_Code_GetInfo",code);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable ZaloGetLotteryContent(int companyId, string day)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVClip, "Zalo_Get_Lottery",companyId,day);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable ZaloGetSoiCauContent(int companyId)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVClip, "Zalo_SoiCau_GetContent",companyId);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static void ZaloMtInsert(ZaloMtInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionStringVClip, "Zalo_Mt_Insert"
                                     , entity.User_ID
                                     , entity.Message
                                     , entity.Service_ID
                                     , entity.Command_Code
                                     , entity.Message_Type
                                     , entity.Request_ID
                                     , entity.Total_Message
                                     , entity.Message_Index
                                     , entity.IsMore
                                     , entity.Content_Type
                                     , entity.ServiceType
                                     , entity.PartnerId
                                     , entity.Operator
                                     , entity.Type
                                 );
    }

    public static void ZaloLotteryDayInsert(ZaloLotteryDay entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionStringVClip, "Zalo_Lottery_Day_Add"
                                     , entity.UserId
                                     , entity.RequestId
                                     , entity.ServiceId
                                     , entity.CommanCode
                                     , entity.Operator
                                     , entity.ServiceCode
                                     , entity.CompanyId
                                 );
    }

    public static void ZaloCallbackNotificationInsert(CallbackInfo entity)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionStringVClip, "Zalo_CallBack_Notification_Insert"
                                     , entity.Event
                                     , entity.Status
                                     , entity.MsgId
                                     , entity.TimeStamp
                                     , entity.Mac
                                 );
    }

    public static void ZaloQuereAdd(int companyId, string content, int isDelete, int type)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionStringVClip, "Zalo_Quere_Mt_AddByCompanyId"
                                     , companyId
                                     , content
                                     , isDelete
                                     , type
                                 );
    }

    public static DataTable ZaloQuereGetUserXoso()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVClip, "Zalo_Quere_Mt_GetUser_XoSo");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable ZaloQuereGetUserXosoNhieuNgay()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVClip, "Zalo_Quere_Mt_GetUser_XoSoNhieuNgay");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }

    public static DataTable ZaloQuereGetUserXoSoTuongThuat()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVClip, "Zalo_Quere_Mt_GetUser_XoSoTuongThuat");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }


    public static void ZaloQuereXoSoDelete(long id, long lotteryId)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionStringVClip, "Zalo_Quere_Mt_GetUser_XoSo_Delete"
                                     , id
                                     , lotteryId
                                 );
    }



    public static void SmsMtInsertNew(MTInfo obj)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionStringVClip, "SMS_MT_Insert", obj.User_ID, obj.Message, obj.Service_ID, obj.Command_Code, obj.Message_Type, obj.Request_ID, obj.Total_Message, obj.Message_Index, obj.IsMore, obj.Content_Type, obj.ServiceType, obj.PartnerID, obj.Operator);
    }
    
    #endregion

    #region ZALO API CALL

    public static int ApiZaloCallForSendZms(string userId, string message)
    {
        log4net.ILog log = log4net.LogManager.GetLogger("ZaloController");
        int reValue = -1;
        try
        {
            const string pageId = "998973463693604902";
            const string secretKey = "yjODClYNJUSJH8Qt20vT";

            long toUid = long.Parse(userId);
            var factory = new ZaloServiceFactory(pageId, secretKey);

            ZaloMessageService messageService = factory.getZaloMessageService();

            //ZaloPageResult objResult = messageService.sendTextMessage(toUid, message, message, true);
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add("pageName", "VMG Media");
            data.Add("header", "KQXS");
            data.Add("Content", message);

            ZaloPageResult objResult = messageService.sendTemplateTextMessageByPhoneNum(toUid, "602ed6c4ea8103df5a90", data, "VMG Zms", true);


            reValue = objResult.getError();

            log.Debug(" ");
            log.Debug(" ");
            log.Debug("--------------- Zalo CALL API RESULT ----------------------");
            log.Debug("Zalo API RESULT Err : " + objResult.getError());
            log.Debug("Zalo API RESULT Id : " + objResult.getId());
            log.Debug(" ");
            log.Debug(" ");
        }
        catch (ZaloSdkException ex)
        {
            log.Debug(" ");
            log.Debug(" ");
            log.Debug("--------------- Zalo CALL API Error ----------------------");
            log.Debug("Zalo API Err : " + ex.getErrorCode());
            log.Debug("Zalo API Mes : " + ex.getMessage());
            log.Debug(" ");
            log.Debug(" ");
        }
        catch (Exception ex)
        {
            log.Debug("Zalo API StackTrace : " + ex.StackTrace);
        }

        return reValue;
    }

    public static void SaveMtLog(string User_ID, string Service_ID, string Command_Code, string message, string Request_ID,string Operator, string Partner, int type)
    {
        var objMt = new ZaloMtInfo();
        objMt.User_ID = User_ID;
        objMt.Message = message;
        objMt.Service_ID = Service_ID;
        objMt.Command_Code = Command_Code;
        objMt.Message_Type = 1;
        objMt.Request_ID = Request_ID;
        objMt.Total_Message = 1;
        objMt.Message_Index = 0;
        objMt.IsMore = 0;
        objMt.Content_Type = 0;
        objMt.ServiceType = 0;
        objMt.ResponseTime = DateTime.Now;
        objMt.IsLock = 0;
        objMt.PartnerId = Partner;
        objMt.Operator = Operator;
        objMt.Type = type;

        ZaloMtInsert(objMt);
    }

    #endregion

}

#line default
#line hidden
