#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\SMS_MO\SMS_MODB.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6CB70E8F4F18B1BBA966622BAB26CB5A0C84AC32"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\SMS_MO\SMS_MODB.cs"
using System;
using System.Collections.Generic;

using System.Web;
using System.Data;
using System.Data.SqlClient;
using log4net;

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

    #endregion
}



#line default
#line hidden
