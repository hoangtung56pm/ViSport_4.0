using System;
using System.Collections.Generic;

using System.Web;
using System.Data;
using System.Data.SqlClient;
using log4net;
using Microsoft.ApplicationBlocks.Data;

public class SMS_MODB
{
    static log4net.ILog log = log4net.LogManager.GetLogger("File");

    public static int Insert(SMS_MOInfo _sMS_MOInfo)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("ViSport_S2_MO_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", _sMS_MOInfo.User_ID);
        dbCmd.Parameters.Add("@Request_ID", _sMS_MOInfo.Request_ID);
        dbCmd.Parameters.Add("@Service_ID", _sMS_MOInfo.Service_ID);
        dbCmd.Parameters.Add("@Command_Code", _sMS_MOInfo.Command_Code);
        dbCmd.Parameters.Add("@Message", _sMS_MOInfo.Message);       
        dbCmd.Parameters.Add("@Operator", _sMS_MOInfo.Operator);
        dbCmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
            return 1;
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error("Insert to MO  : " + ex.Message);
            }
            return 0;
        }
        finally
        {
            dbConn.Close();
        }
    }

    public static int CancelInsert(SMS_CancelInfo _sMS_MOInfo)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("ViSport_S2_MO_CancelInsert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", _sMS_MOInfo.User_ID);
        dbCmd.Parameters.Add("@Request_ID", _sMS_MOInfo.Request_ID);
        dbCmd.Parameters.Add("@Service_ID", _sMS_MOInfo.Service_ID);
        dbCmd.Parameters.Add("@Service_Type", _sMS_MOInfo.Service_Type);         
        dbCmd.Parameters.Add("@Command_Code", _sMS_MOInfo.Command_Code);
        dbCmd.Parameters.Add("@Message", _sMS_MOInfo.Message);
        dbCmd.Parameters.Add("@Operator", _sMS_MOInfo.Operator);
        dbCmd.Parameters.Add("@RETURN_ID", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
            return (int)dbCmd.Parameters["@RETURN_ID"].Value;
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error("Insert to MO  : " + ex.Message);
            }
            return 0;
        }
        finally
        {
            dbConn.Close();
        }
    }

    public static int InsertVClip(SMS_MOInfo _sMS_MOInfo)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("VClip_S2_MO_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", _sMS_MOInfo.User_ID);
        dbCmd.Parameters.Add("@Request_ID", _sMS_MOInfo.Request_ID);
        dbCmd.Parameters.Add("@Service_ID", _sMS_MOInfo.Service_ID);
        dbCmd.Parameters.Add("@Command_Code", _sMS_MOInfo.Command_Code);
        dbCmd.Parameters.Add("@Message", _sMS_MOInfo.Message);
        dbCmd.Parameters.Add("@Operator", _sMS_MOInfo.Operator);
        dbCmd.Parameters.Add("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
            return 1;
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error("Insert to MO  : " + ex.Message);
            }
            return 0;
        }
        finally
        {
            dbConn.Close();
        }
    }

    public static int CancelInsertVClip(SMS_CancelInfo _sMS_MOInfo)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("VClip_S2_MO_CancelInsert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", _sMS_MOInfo.User_ID);
        dbCmd.Parameters.Add("@Request_ID", _sMS_MOInfo.Request_ID);
        dbCmd.Parameters.Add("@Service_ID", _sMS_MOInfo.Service_ID);
        dbCmd.Parameters.Add("@Service_Type", _sMS_MOInfo.Service_Type);
        dbCmd.Parameters.Add("@Command_Code", _sMS_MOInfo.Command_Code);
        dbCmd.Parameters.Add("@Message", _sMS_MOInfo.Message);
        dbCmd.Parameters.Add("@Operator", _sMS_MOInfo.Operator);
        dbCmd.Parameters.Add("@RETURN_ID", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
            return (int)dbCmd.Parameters["@RETURN_ID"].Value;
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error("Insert to MO  : " + ex.Message);
            }
            return 0;
        }
        finally
        {
            dbConn.Close();
        }
    }

    #region VNM
    
    public static void InsertVnmMo(SMS_MOInfo entity)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("VNM_S2_MO_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", entity.User_ID);
        dbCmd.Parameters.Add("@Request_ID", entity.Request_ID);
        dbCmd.Parameters.Add("@Service_ID", entity.Service_ID);
        dbCmd.Parameters.Add("@Command_Code", entity.Command_Code);
        dbCmd.Parameters.Add("@Message", entity.Message);
        dbCmd.Parameters.Add("@Operator", entity.Operator);
        
        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error("Insert to MO VNM  : " + ex.Message);
            }
        }
        finally
        {
            dbConn.Close();
        }
    }

    public static void InsertSportGameHeroMo(SMS_MOInfo entity)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("Sport_Game_Hero_SMS_MO_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", entity.User_ID);
        dbCmd.Parameters.Add("@Request_ID", entity.Request_ID);
        dbCmd.Parameters.Add("@Service_ID", entity.Service_ID);
        dbCmd.Parameters.Add("@Command_Code", entity.Command_Code);
        dbCmd.Parameters.Add("@Message", entity.Message);
        dbCmd.Parameters.Add("@Operator", entity.Operator);

        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error("Insert to MO Sport_Game_Hero  : " + ex.Message);
            }
        }
        finally
        {
            dbConn.Close();
        }
    }

    public static void VnptMediaInsertMo(SMS_MOInfo entity)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("Vnpt_Media_SMS_MO_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", entity.User_ID);
        dbCmd.Parameters.Add("@Request_ID", entity.Request_ID);
        dbCmd.Parameters.Add("@Service_ID", entity.Service_ID);
        dbCmd.Parameters.Add("@Command_Code", entity.Command_Code);
        dbCmd.Parameters.Add("@Message", entity.Message);
        dbCmd.Parameters.Add("@Operator", entity.Operator);

        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error("Error Insert to MO Vnpt_media  : " + ex.Message);
            }
        }
        finally
        {
            dbConn.Close();
        }

    }


    public static void InsertThanhNuMo(SMS_MOInfo entity)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("Thanh_Nu_SMS_MO_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", entity.User_ID);
        dbCmd.Parameters.Add("@Request_ID", entity.Request_ID);
        dbCmd.Parameters.Add("@Service_ID", entity.Service_ID);
        dbCmd.Parameters.Add("@Command_Code", entity.Command_Code);
        dbCmd.Parameters.Add("@Message", entity.Message);
        dbCmd.Parameters.Add("@Operator", entity.Operator);

        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error("Insert to MO Thanh_Nu  : " + ex.Message);
            }
        }
        finally
        {
            dbConn.Close();
        }
    }

    public static void InsertMo949Mo(SMS_MOInfo entity)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("Mo949_SMS_MO_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", entity.User_ID);
        dbCmd.Parameters.Add("@Request_ID", entity.Request_ID);
        dbCmd.Parameters.Add("@Service_ID", entity.Service_ID);
        dbCmd.Parameters.Add("@Command_Code", entity.Command_Code);
        dbCmd.Parameters.Add("@Message", entity.Message);
        dbCmd.Parameters.Add("@Operator", entity.Operator);

        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error("Insert to MO Mo949  : " + ex.Message);
            }
        }
        finally
        {
            dbConn.Close();
        }
    }

    public static void WorldCupInsertMo(SMS_MOInfo entity)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("World_Cup_SMS_MO_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", entity.User_ID);
        dbCmd.Parameters.Add("@Request_ID", entity.Request_ID);
        dbCmd.Parameters.Add("@Service_ID", entity.Service_ID);
        dbCmd.Parameters.Add("@Command_Code", entity.Command_Code);
        dbCmd.Parameters.Add("@Message", entity.Message);
        dbCmd.Parameters.Add("@Operator", entity.Operator);
        dbCmd.Parameters.Add("@IsCharged", entity.IsCharged);

        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error("Insert to MO World_Cup  : " + ex.Message);
            }
        }
        finally
        {
            dbConn.Close();
        }
    }

    public static void InsertThanTaiMo(SMS_MOInfo entity)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("ThanTai_SMS_MO_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", entity.User_ID);
        dbCmd.Parameters.Add("@Request_ID", entity.Request_ID);
        dbCmd.Parameters.Add("@Service_ID", entity.Service_ID);
        dbCmd.Parameters.Add("@Command_Code", entity.Command_Code);
        dbCmd.Parameters.Add("@Message", entity.Message);
        dbCmd.Parameters.Add("@Operator", entity.Operator);

        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error("Insert to MO Than_Tai  : " + ex.Message);
            }
        }
        finally
        {
            dbConn.Close();
        }
    }
    #endregion

    #region Cau hoi may man
    public static void InsertCauHoiMayManMo(SMS_MOInfo entity)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("CauHoiMayMan_SMS_MO_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@User_ID", entity.User_ID);
        dbCmd.Parameters.Add("@Request_ID", entity.Request_ID);
        dbCmd.Parameters.Add("@Service_ID", entity.Service_ID);
        dbCmd.Parameters.Add("@Command_Code", entity.Command_Code);
        dbCmd.Parameters.Add("@Message", entity.Message);
        dbCmd.Parameters.Add("@Operator", entity.Operator);
        dbCmd.Parameters.Add("@Type", entity.Type);
        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            if (log.IsErrorEnabled)
            {
                log.Error("Insert to MO Cau_Hoi_May_Man  : " + ex.Message);
            }
        }
        finally
        {
            dbConn.Close();
        }
    }
    #endregion

    #region Confirm register
    public static int ViSport_Confirm_Register_Insert(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionString);
        SqlCommand dbCmd = new SqlCommand("ViSport_Confirm_Register_Insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.AddWithValue("@User_ID", User_ID);
        dbCmd.Parameters.AddWithValue("@Service_ID", Service_ID);
        dbCmd.Parameters.AddWithValue("@Command_Code", Command_Code);
        dbCmd.Parameters.AddWithValue("@Message", Message);
        dbCmd.Parameters.AddWithValue("@Request_ID", Request_ID);
        dbCmd.Parameters.AddWithValue("@RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();
            return (int)dbCmd.Parameters["@RETURN_VALUE"].Value;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dbConn.Close();
        }
    }
    public static DataTable GetUserRegister(string User_ID, string ShortCode)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Visport_GetUserRegister", User_ID, ShortCode);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return new DataTable();
    }
    public static void UpdateUser_Confirm(string User_ID, string ShortCode)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "Visport_UpdateUser_Confirm", User_ID, ShortCode);
    }
    #endregion
    public Int32 UnixTimeStampUTC()
    {
        Int32 unixTimeStamp;
        DateTime currentTime = DateTime.Now;
        DateTime zuluTime = currentTime.ToUniversalTime();
        DateTime unixEpoch = new DateTime(1970, 1, 1);
        unixTimeStamp = (Int32)(zuluTime.Subtract(unixEpoch)).TotalMilliseconds;
        return unixTimeStamp;
    }
    private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public long GetCurrentUnixTimestampMillis()
    {
        DateTime localDateTime, univDateTime;
        localDateTime = DateTime.Now;
        univDateTime = localDateTime.ToUniversalTime();
        return (long)(univDateTime - UnixEpoch).TotalMilliseconds;
    }
}

