#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\SMS_MT\ViSport_S2_SMS_MTController.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "008865FE90429906DC34612A97FDAE37560E9FD6"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\SMS_MT\ViSport_S2_SMS_MTController.cs"
using System;
using System.Collections.Generic;

using System.Web;
using System.Data;
using System.Data.SqlClient;
using log4net;

/// <summary>
/// Summary description for ViSport_S2_SMS_MTController
/// </summary>
public class ViSport_S2_SMS_MTController
{

    public static bool UpdateStatus(int status)
    {
        try
        {
            SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
            SqlCommand dbCmd = new SqlCommand("ViSport_S2_JobStatus_Update", dbConn);
            dbCmd.CommandType = CommandType.StoredProcedure;
            dbCmd.Parameters.Add("@Status", status);

            dbConn.Open();
            dbCmd.ExecuteNonQuery();
            dbConn.Close();
        }
        catch (Exception)
        {
            return false;
        }

        return true;
    }

    public static int Insert(ViSport_S2_SMS_MTInfo _viSport_S2_SMS_MTInfo)
    {

        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("ViSport_S2_SMS_MT_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", _viSport_S2_SMS_MTInfo.User_ID);
        dbCmd.Parameters.Add("@Message", _viSport_S2_SMS_MTInfo.Message);
        dbCmd.Parameters.Add("@Service_ID", _viSport_S2_SMS_MTInfo.Service_ID);
        dbCmd.Parameters.Add("@Command_Code", _viSport_S2_SMS_MTInfo.Command_Code);
        dbCmd.Parameters.Add("@Message_Type", _viSport_S2_SMS_MTInfo.Message_Type);
        dbCmd.Parameters.Add("@Request_ID", _viSport_S2_SMS_MTInfo.Request_ID);
        dbCmd.Parameters.Add("@Total_Message", _viSport_S2_SMS_MTInfo.Total_Message);
        dbCmd.Parameters.Add("@Message_Index", _viSport_S2_SMS_MTInfo.Message_Index);
        dbCmd.Parameters.Add("@IsMore", _viSport_S2_SMS_MTInfo.IsMore);
        dbCmd.Parameters.Add("@Content_Type", _viSport_S2_SMS_MTInfo.Content_Type);
        dbCmd.Parameters.Add("@ServiceType", _viSport_S2_SMS_MTInfo.ServiceType);
        dbCmd.Parameters.Add("@ResponseTime", _viSport_S2_SMS_MTInfo.ResponseTime);
        dbCmd.Parameters.Add("@isLock", _viSport_S2_SMS_MTInfo.isLock);
        dbCmd.Parameters.Add("@PartnerID", _viSport_S2_SMS_MTInfo.PartnerID);
        dbCmd.Parameters.Add("@Operator", _viSport_S2_SMS_MTInfo.Operator);
        dbCmd.Parameters.Add("@RETURN_ID", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
            return (int)dbCmd.Parameters["@RETURN_ID"].Value;
        }
        finally
        {
            dbConn.Close();
        }
    }
    public static int InsertVClip(ViSport_S2_SMS_MTInfo _viSport_S2_SMS_MTInfo)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("VClip_S2_SMS_MT_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", _viSport_S2_SMS_MTInfo.User_ID);
        dbCmd.Parameters.Add("@Message", _viSport_S2_SMS_MTInfo.Message);
        dbCmd.Parameters.Add("@Service_ID", _viSport_S2_SMS_MTInfo.Service_ID);
        dbCmd.Parameters.Add("@Command_Code", _viSport_S2_SMS_MTInfo.Command_Code);
        dbCmd.Parameters.Add("@Message_Type", _viSport_S2_SMS_MTInfo.Message_Type);
        dbCmd.Parameters.Add("@Request_ID", _viSport_S2_SMS_MTInfo.Request_ID);
        dbCmd.Parameters.Add("@Total_Message", _viSport_S2_SMS_MTInfo.Total_Message);
        dbCmd.Parameters.Add("@Message_Index", _viSport_S2_SMS_MTInfo.Message_Index);
        dbCmd.Parameters.Add("@IsMore", _viSport_S2_SMS_MTInfo.IsMore);
        dbCmd.Parameters.Add("@Content_Type", _viSport_S2_SMS_MTInfo.Content_Type);
        dbCmd.Parameters.Add("@ServiceType", _viSport_S2_SMS_MTInfo.ServiceType);
        dbCmd.Parameters.Add("@ResponseTime", _viSport_S2_SMS_MTInfo.ResponseTime);
        dbCmd.Parameters.Add("@isLock", _viSport_S2_SMS_MTInfo.isLock);
        dbCmd.Parameters.Add("@PartnerID", _viSport_S2_SMS_MTInfo.PartnerID);
        dbCmd.Parameters.Add("@Operator", _viSport_S2_SMS_MTInfo.Operator);
        dbCmd.Parameters.Add("@RETURN_ID", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
            return (int)dbCmd.Parameters["@RETURN_ID"].Value;
        }
        finally
        {
            dbConn.Close();
        }
    }

    #region VNM
    
    public static int InsertVnmMt(ViSport_S2_SMS_MTInfo viSportS2SmsMtInfo)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("VNM_S2_SMS_MT_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", viSportS2SmsMtInfo.User_ID);
        dbCmd.Parameters.Add("@Message", viSportS2SmsMtInfo.Message);
        dbCmd.Parameters.Add("@Service_ID", viSportS2SmsMtInfo.Service_ID);
        dbCmd.Parameters.Add("@Command_Code", viSportS2SmsMtInfo.Command_Code);
        dbCmd.Parameters.Add("@Message_Type", viSportS2SmsMtInfo.Message_Type);
        dbCmd.Parameters.Add("@Request_ID", viSportS2SmsMtInfo.Request_ID);
        dbCmd.Parameters.Add("@Total_Message", viSportS2SmsMtInfo.Total_Message);
        dbCmd.Parameters.Add("@Message_Index", viSportS2SmsMtInfo.Message_Index);
        dbCmd.Parameters.Add("@IsMore", viSportS2SmsMtInfo.IsMore);
        dbCmd.Parameters.Add("@Content_Type", viSportS2SmsMtInfo.Content_Type);
        dbCmd.Parameters.Add("@ServiceType", viSportS2SmsMtInfo.ServiceType);
        dbCmd.Parameters.Add("@ResponseTime", viSportS2SmsMtInfo.ResponseTime);
        dbCmd.Parameters.Add("@isLock", viSportS2SmsMtInfo.isLock);
        dbCmd.Parameters.Add("@PartnerID", viSportS2SmsMtInfo.PartnerID);
        dbCmd.Parameters.Add("@Operator", viSportS2SmsMtInfo.Operator);
        dbCmd.Parameters.Add("@RETURN_ID", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
            return (int)dbCmd.Parameters["@RETURN_ID"].Value;
        }
        finally
        {
            dbConn.Close();
        }
    }

    public static int InsertSportGameHeroMt(ViSport_S2_SMS_MTInfo entity)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("Sport_Game_Hero_SMS_MT_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", entity.User_ID);
        dbCmd.Parameters.Add("@Message", entity.Message);
        dbCmd.Parameters.Add("@Service_ID", entity.Service_ID);
        dbCmd.Parameters.Add("@Command_Code", entity.Command_Code);
        dbCmd.Parameters.Add("@Message_Type", entity.Message_Type);
        dbCmd.Parameters.Add("@Request_ID", entity.Request_ID);
        dbCmd.Parameters.Add("@Total_Message", entity.Total_Message);
        dbCmd.Parameters.Add("@Message_Index", entity.Message_Index);
        dbCmd.Parameters.Add("@IsMore", entity.IsMore);
        dbCmd.Parameters.Add("@Content_Type", entity.Content_Type);
        dbCmd.Parameters.Add("@ServiceType", entity.ServiceType);
        dbCmd.Parameters.Add("@ResponseTime", entity.ResponseTime);
        dbCmd.Parameters.Add("@isLock", entity.isLock);
        dbCmd.Parameters.Add("@PartnerID", entity.PartnerID);
        dbCmd.Parameters.Add("@Operator", entity.Operator);
        dbCmd.Parameters.Add("@IsQuestion", entity.IsQuestion);


        dbCmd.Parameters.Add("@RETURN_ID", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
            return (int)dbCmd.Parameters["@RETURN_ID"].Value;
        }
        finally
        {
            dbConn.Close();
        }
    }

    public static int InsertThanhNuMt(ViSport_S2_SMS_MTInfo entity)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("Thanh_Nu_SMS_MT_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", entity.User_ID);
        dbCmd.Parameters.Add("@Message", entity.Message);
        dbCmd.Parameters.Add("@Service_ID", entity.Service_ID);
        dbCmd.Parameters.Add("@Command_Code", entity.Command_Code);
        dbCmd.Parameters.Add("@Message_Type", entity.Message_Type);
        dbCmd.Parameters.Add("@Request_ID", entity.Request_ID);
        dbCmd.Parameters.Add("@Total_Message", entity.Total_Message);
        dbCmd.Parameters.Add("@Message_Index", entity.Message_Index);
        dbCmd.Parameters.Add("@IsMore", entity.IsMore);
        dbCmd.Parameters.Add("@Content_Type", entity.Content_Type);
        dbCmd.Parameters.Add("@ServiceType", entity.ServiceType);
        dbCmd.Parameters.Add("@ResponseTime", entity.ResponseTime);
        dbCmd.Parameters.Add("@isLock", entity.isLock);
        dbCmd.Parameters.Add("@PartnerID", entity.PartnerID);
        dbCmd.Parameters.Add("@Operator", entity.Operator);


        dbCmd.Parameters.Add("@RETURN_ID", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
            return (int)dbCmd.Parameters["@RETURN_ID"].Value;
        }
        finally
        {
            dbConn.Close();
        }
    }

    public static int InsertMo949Mt(ViSport_S2_SMS_MTInfo entity)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("Mo949_SMS_MT_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", entity.User_ID);
        dbCmd.Parameters.Add("@Message", entity.Message);
        dbCmd.Parameters.Add("@Service_ID", entity.Service_ID);
        dbCmd.Parameters.Add("@Command_Code", entity.Command_Code);
        dbCmd.Parameters.Add("@Message_Type", entity.Message_Type);
        dbCmd.Parameters.Add("@Request_ID", entity.Request_ID);
        dbCmd.Parameters.Add("@Total_Message", entity.Total_Message);
        dbCmd.Parameters.Add("@Message_Index", entity.Message_Index);
        dbCmd.Parameters.Add("@IsMore", entity.IsMore);
        dbCmd.Parameters.Add("@Content_Type", entity.Content_Type);
        dbCmd.Parameters.Add("@ServiceType", entity.ServiceType);
        dbCmd.Parameters.Add("@ResponseTime", entity.ResponseTime);
        dbCmd.Parameters.Add("@isLock", entity.isLock);
        dbCmd.Parameters.Add("@PartnerID", entity.PartnerID);
        dbCmd.Parameters.Add("@Operator", entity.Operator);


        dbCmd.Parameters.Add("@RETURN_ID", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
            return (int)dbCmd.Parameters["@RETURN_ID"].Value;
        }
        finally
        {
            dbConn.Close();
        }
    }

    #endregion
}

#line default
#line hidden
