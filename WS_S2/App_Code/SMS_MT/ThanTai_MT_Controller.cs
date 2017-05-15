using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ThanTai_MT_Controller
/// </summary>
public class ThanTai_MT_Controller
{
    
    public static void Insert_MT(ThanTai_MT_Info _MTInfo)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "ThanTai_SMS_MT_Insert", _MTInfo.User_ID, _MTInfo.Message,
                           _MTInfo.Service_ID, _MTInfo.Command_Code, _MTInfo.Message_Type, _MTInfo.Request_ID,_MTInfo.Total_Message,
                           _MTInfo.Message_Index,_MTInfo.IsMore,_MTInfo.Content_Type,_MTInfo.ServiceType,_MTInfo.ResponseTime,
                           _MTInfo.PartnerID,_MTInfo.Operator
                          

                            );
    }
   
    public static void Insert_CapSo(string UserID, string CapSo)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "ThanTai_Manager_Insert", UserID, CapSo


                            );
    }
    
    public static DataTable SumPointByDay(string UserID)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "ThanTai_SumPointByDay", UserID);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }
    public static DataTable SumPoint(string UserID)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "ThanTai_SumPoint", UserID);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }
    #region Cau hoi may man
    public static void Insert_CauHoiMayMan_MT(ThanTai_MT_Info _MTInfo)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "CauHoiMayMan_SMS_MT_Insert", _MTInfo.User_ID, _MTInfo.Message,
                           _MTInfo.Service_ID, _MTInfo.Command_Code, _MTInfo.Message_Type, _MTInfo.Request_ID, _MTInfo.Total_Message,
                           _MTInfo.Message_Index, _MTInfo.IsMore, _MTInfo.Content_Type, _MTInfo.ServiceType, _MTInfo.ResponseTime,
                           _MTInfo.PartnerID, _MTInfo.Operator, _MTInfo.Type


                            );
    }
    public static DataTable Diem_CauHoiMayMan(string UserID)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "CauHoiMayMan_SumPoint", UserID);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
    }
    #endregion
}