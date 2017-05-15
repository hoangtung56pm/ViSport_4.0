using Microsoft.ApplicationBlocks.Data;
using SMSManager_API.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DBController
/// </summary>
public class DBController
{
    public DBController()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable GetBySql(string commandText)
    {
        DataTable retVal = null;
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionStringVClip);
        SqlCommand sqlCommand = new SqlCommand(commandText, dbConn);
        try
        {
            retVal = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sqlCommand);
            da.Fill(retVal);
        }
        catch (Exception ex)
        {
            throw;
        }
        finally
        {
            dbConn.Close();
            sqlCommand.Dispose();
        }
        return retVal;
    }
    public static DataTable Hospital_User_GetUser_SendDailyMT(int id)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVClip, "Hospital_User_GetUser_SendDailyMT", id);
        if (ds != null && ds.Tables.Count > 0)
            return ds.Tables[0];
        return null;
    }
    public static DataTable Hospital_User_GetUser_SendAlertMT(int id)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringVClip, "Hospital_User_GetUser_SendAlertMT", id);
        if (ds != null && ds.Tables.Count > 0)
            return ds.Tables[0];
        return null;
    }
    public static int Send(string userId, string Content, string ServiceId, string CommandCode, string MessageType,
                       string RequestId, string TotalMessage, string MessageIndex, string IsMore, string ContentType)
    {
        if (userId.StartsWith("+"))
            userId = userId.Replace("+", string.Empty);
        if (userId.StartsWith("0"))
        {
            userId = "84" + userId.Remove(0, 1);
        }
        SMS_MTInfo objInfo = new SMS_MTInfo();
        objInfo.Command_Code = CommandCode;

        objInfo.Content_Type = ConvertUtility.ToInt32(ContentType);
        objInfo.IsMore = ConvertUtility.ToInt32(IsMore);
        objInfo.Message = Content;
        objInfo.Message_Index = ConvertUtility.ToInt32(MessageIndex);
        objInfo.Message_Type = ConvertUtility.ToInt32(MessageType);
        objInfo.Operator = GetTelco(userId);
        objInfo.PartnerID = "";
        objInfo.Request_ID = RequestId;
        objInfo.Service_ID = ServiceId;
        objInfo.ServiceType = 0;
        objInfo.Total_Message = ConvertUtility.ToInt32(TotalMessage);
        objInfo.User_ID = userId;
        int result = Insert(objInfo);
        return result;
    }
    public static int Insert(SMS_MTInfo _sMS_MTInfo)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionStringVClip);
        SqlCommand dbCmd = new SqlCommand("SMS_MT_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", _sMS_MTInfo.User_ID);
        dbCmd.Parameters.Add("@Message", _sMS_MTInfo.Message);
        dbCmd.Parameters.Add("@Service_ID", _sMS_MTInfo.Service_ID);
        dbCmd.Parameters.Add("@Command_Code", _sMS_MTInfo.Command_Code);
        dbCmd.Parameters.Add("@Message_Type", _sMS_MTInfo.Message_Type);
        dbCmd.Parameters.Add("@Request_ID", _sMS_MTInfo.Request_ID);
        dbCmd.Parameters.Add("@Total_Message", _sMS_MTInfo.Total_Message);
        dbCmd.Parameters.Add("@Message_Index", _sMS_MTInfo.Message_Index);
        dbCmd.Parameters.Add("@IsMore", _sMS_MTInfo.IsMore);
        dbCmd.Parameters.Add("@Content_Type", _sMS_MTInfo.Content_Type);
        dbCmd.Parameters.Add("@ServiceType", _sMS_MTInfo.ServiceType);
        dbCmd.Parameters.Add("@PartnerID", _sMS_MTInfo.PartnerID);
        dbCmd.Parameters.Add("@Operator", _sMS_MTInfo.Operator);
        dbCmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
            return (int)dbCmd.Parameters["@RETURN_VALUE"].Value;
        }
        finally
        {
            dbConn.Close();
        }
    }
    public static string GetTelco(string mobile)
    {
        string prenumber = mobile.Substring(0, 5);

        string[] dfsplit = AppEnv.GetSetting("sfone").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "sfone";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("vnmobile").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "vnmobile";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("gtel").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "gtel";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("viettel").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "viettel";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("vms").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "vms";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("gpc").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "gpc";
                }
            }
        }

        return "";
    }
}