using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;

/// <summary>
/// Summary description for ThanTaiProcess
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ThanTaiProcess : System.Web.Services.WebService, IJobExecutorSoap
{

    public ThanTaiProcess () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    log4net.ILog _log = log4net.LogManager.GetLogger(typeof(ThanTaiProcess));

    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            DataTable dt = GetLotteryResult();
            if(dt.Rows.Count > 0)
            {
                string value = string.Empty;

                string lot15 = dt.Rows[0]["db"].ToString();

                int len1 = lot15.Length - 2;
                int len2 = lot15.Length - (lot15.Length-2);

                lot15 = lot15.Substring(len1, len2);

                //value = lot15;

               string[] lots = dt.Rows[0]["lot_all"].ToString().Split('-');
               foreach(var lot in lots)
               {
                   int len3 = lot.Length - 2;
                   int len4 = lot.Length - (lot.Length - 2);
                   string subLot = lot.Substring(len3, len4);
                   value = value + "," + subLot;
               }
               DataTable dtUser = GetAllUser();
               if (dtUser != null && dtUser.Rows.Count > 0)
               {
                   
                   foreach (DataRow _rowUser in dtUser.Rows)
                   {
                       int point = 0;
                       DataTable dtCode = GetCodeByDay(Convert.ToString(_rowUser["User_ID"]));
                       foreach (DataRow dr in dtCode.Rows)
                       {
                           string capso = dr["CapSo"].ToString();
                           //if (value.Contains(capso))
                           //{
                           //    // UPDATE status: 0=khoong trung; 1 trung 1 diem; 2 trung giai dac biet
                           //    //UpdatePoint(dr["User_id"].ToString(), 1);

                           //    point = point + 1;
                           //    UpdateStatusManager(Convert.ToString(_rowUser["User_ID"]), 1);
                           //}
                           string[] Result = value.Split(',');
                           
                           foreach (string a in Result)
                           {
                               if (a == capso)
                               {
                                   point = point + 1;
                                   UpdateStatusManager(Convert.ToString(_rowUser["User_ID"]), 1);
                               }
                           }
                           if (lot15 == capso)//TRUNG GIAI DB
                           {
                               point = point + 10;
                               UpdateStatusManager(Convert.ToString(_rowUser["User_ID"]), 2);
                               //UPDATE 10diem theo User_ID
                               //UpdatePoint(dr["User_id"].ToString(), 10);
                           }

                       }
                       //int b = point;
                       UpdatePoint(Convert.ToString(_rowUser["User_ID"]), point);
                   }
               }
               //DataTable dtCode = GetCodeByDay();
               // if(dtCode.Rows.Count > 0)
               // {
               //     foreach(DataRow dr in dtCode.Rows)
               //     {
               //         string capso = dr["CapSo"].ToString();
               //         if(value.Contains(capso))
               //         {
               //             // UPDATE Theo User_id
               //             UpdatePoint(dr["User_id"].ToString(), 1);
               //         }

               //         if(lot15 == capso)//TRUNG GIAI DB
               //         {
               //             //UPDATE 10diem theo User_ID
               //             UpdatePoint(dr["User_id"].ToString(), 10);
               //         }

               //     }
               // }

            }
        }
        catch (Exception ex)
        {
            _log.Error("***** ThanTai Loi lay tap User : " + ex);
            return 0;
        }
        return 1;
    }

    #region Methods

    public static string Conn177 = AppEnv.GetConnectionString("localsqlVClip");
    public DataTable GetLotteryResult()
    {
        DataSet ds = SqlHelper.ExecuteDataset(Conn177, "ThanTai_GetLottery_Result");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return new DataTable();
    }

    public DataTable GetCodeByDay(string User_ID)
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "ThanTai_GetCodeByDay", User_ID);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return new DataTable();
    }
    public DataTable GetAllUser()
    {
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "ThanTai_GetAllUser");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return new DataTable();
    }
    public static void UpdatePoint(string User_ID, int Point)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "ThanTai_InsertPoint",
                          User_ID, Point
                            );
    }
    public static void UpdateStatusManager(string User_ID,int status)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionString, "ThanTai_UpdateStatusManager",
                          User_ID, status
                            );
    }
    #endregion

}

