#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\CdrGenerate.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2BBDFDBB07451ABD7F030EA4D3363E76277AB4C1"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\CdrGenerate.cs"
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.Services;
using SMSManager_API.Library.Utilities;

/// <summary>
/// Summary description for CdrGenerate
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class CdrGenerate : System.Web.Services.WebService, IJobExecutorSoap
{

    public CdrGenerate () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(CdrGpc));

    [WebMethod]
    public int Execute(int jobID)
    {
        try
        {
            //string filePath1 = @"C:\test.csv";
            //string delimiter = ",";

            //string[][] output = new string[][]{  
            //    new string[]{"Col 1 Row 1", "Col 2 Row 1", "Col 3 Row 1"},                   
            //    new string[]{"Col1 Row 2", "Col2 Row 2", "Col3 Row 2"}  
            //};
            //int length = output.GetLength(0);
            //StringBuilder sb = new StringBuilder();
            //for (int index = 0; index < length; index++)
            //    sb.AppendLine(string.Join(delimiter, output[index]));

            //File.WriteAllText(filePath1, sb.ToString());

            DataTable dtPartner = ViSport_S2_Registered_UsersController.PartnerGetAll();
            if(dtPartner != null && dtPartner.Rows.Count > 0)
            {
                foreach(DataRow drPartner in dtPartner.Rows)
                {
                    int partnerId = ConvertUtility.ToInt32(drPartner["PartnerId"].ToString());
                    string folderName = drPartner["FolderName"].ToString();

                    if (!Directory.Exists(Server.MapPath(folderName)))
                    {
                        Directory.CreateDirectory(Server.MapPath(folderName));
                    }

                    var csv = new StringBuilder();
                    DateTime dataDate = DateTime.Today.AddDays(-1);
                    string filePath = Server.MapPath(folderName + "/vmg_dn_vn_" + dataDate.Year + "" + dataDate.Month + "" + dataDate.Day + ".csv");

                    csv.AppendLine("Type"
                                        + "," + "TelcoCode"
                                        + "," + "ServiceName"
                                        + "," + "ShortCode"
                                        + "," + "Msisdn"
                                        + "," + "Unique_id"
                                        + "," + "Price"
                                        + "," + "ChargedStatus"
                                        + "," + "Detail"
                                        + "," + " Created");

                    #region GPC

                    DataTable dtGpc = ViSport_S2_Registered_UsersController.CdrGpc(partnerId);
                    if (dtGpc != null && dtGpc.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtGpc.Rows)
                        {
                            //Type,TelcoCode,ServiceName,ShortCode,Msisdn,Unique_id,Price,ChargedStatus,Detail,Created
                            csv.AppendLine("SUB"
                                        + "," + "Gpc"
                                        + "," + dr["Service_Name"]
                                        + "," + dr["shortCode"]
                                        + "," + dr["msisdn"]
                                        + "," + " "
                                        + "," + dr["cost"]
                                        + "," + "1"
                                        + "," + "Succ"
                                        + "," + dr["TimeStamp"]);
                        }
                    }

                    #endregion

                    #region VMS

                    DataTable dtVms = ViSport_S2_Registered_UsersController.CdrPartnerGetServiceId(partnerId);
                    if (dtVms != null && dtVms.Rows.Count > 0)
                    {
                        foreach (DataRow drSv in dtVms.Rows)
                        {
                            string serviceName = drSv["Service_Name"].ToString();
                            string serviceId = drSv["Service_Id"].ToString();
                            const string shortCode = "8979";

                            DataTable dtUsers = ViSport_S2_Registered_UsersController.SamVmsGetCdrByServiceId(serviceId);
                            if (dtUsers != null && dtUsers.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dtUsers.Rows)
                                {
                                    //Type,TelcoCode,ServiceName,ShortCode,Msisdn,Unique_id,Price,ChargedStatus,Detail,Created
                                    csv.AppendLine("SUB"
                                        + "," + "Vms"
                                        + "," + serviceName
                                        + "," + shortCode
                                        + "," + dr["msisdn"]
                                        + "," + " "
                                        + "," + dr["cost"]
                                        + "," + dr["ChargeResult"]
                                        + "," + " "
                                        + "," + dr["TimeStamp"]);
                                }
                                File.WriteAllText(filePath, csv.ToString());
                            }
                        }
                    }

                    #endregion

                    #region VNM

                    DataTable dtVnm = ViSport_S2_Registered_UsersController.CdrVnm(partnerId);
                    if (dtVnm != null && dtVnm.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtVnm.Rows)
                        {
                            csv.AppendLine("SUB"
                                        + "," + "Vnm"
                                        + "," + dr["Product_Name"]
                                        + "," + dr["shortCode"]
                                        + "," + dr["User_Id"]
                                        + "," + " "
                                        + "," + dr["Charging_Price"]
                                        + "," + dr["Charging_Status"]
                                        + "," + dr["Charging_Response"]
                                        + "," + ConvertUtility.ToDateTime(dr["Charging_Time"]));

                        }
                        File.WriteAllText(filePath, csv.ToString());
                    }

                    #endregion

                    #region VNM 1119

                    DataTable dtVnm1119 = ViSport_S2_Registered_UsersController.CdrVnm1119(partnerId);
                    if (dtVnm1119 != null && dtVnm1119.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtVnm1119.Rows)
                        {
                            csv.AppendLine("SUB"
                                        + "," + "Vnm"
                                        + "," + dr["Product_Name"]
                                        + "," + dr["shortCode"]
                                        + "," + dr["User_Id"]
                                        + "," + " "
                                        + "," + dr["Charging_Price"]
                                        + "," + dr["Charging_Status"]
                                        + "," + dr["Charging_Response"]
                                        + "," + ConvertUtility.ToDateTime(dr["Charging_Time"]));
                        }
                        File.WriteAllText(filePath, csv.ToString());
                    }

                    #endregion

                }
            }
        }
        catch (Exception ex)
        {
            _log.Error("CDR Loi lay Tap User : " + ex);
            return 0;
        }
        return 1;
    }
    
}


#line default
#line hidden
