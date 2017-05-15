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
    public int Execute(int jobId)
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

                    //folderName = "~/Sam_Cdr"; //Forder for re push CDR

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

                    if (partnerId == 21)
                    {
                        #region CDR for SAM

                        DataTable dtGpc = ViSport_S2_Registered_UsersController.CdrGpcSam();
                        if (dtGpc != null && dtGpc.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtGpc.Rows)
                            {
                                //var contentId = !string.IsNullOrEmpty(dr["Unique_Id"].ToString()) ? dr["Unique_Id"].ToString() 
                                //    : ViSport_S2_Registered_UsersController.SamGetContentId(dr["Register_Syntax"].ToString());
                                string contentId = "";
                                DataTable dtContent = ViSport_S2_Registered_UsersController.CdrGpcSam_GetContentId_ByUserID(dr["msisdn"].ToString());
                                if (dtContent != null && dtContent.Rows.Count > 0)
                                {
                                    contentId = dtContent.Rows[0]["ContentId"].ToString();
                                }

                                //var output = "=\"" + dr["msisdn"].ToString() + "\"";
                                //var outputtime = "=\"" + dr["TimeStamp"] + "\"";
                                var output =  dr["msisdn"].ToString();
                                var outputtime = dr["TimeStamp"];
                                csv.AppendLine("SUB"
                                            + "," + "Gpc"
                                            + "," + dr["Service_Name"]
                                            + "," + dr["shortCode"]
                                            + "," + output
                                            + "," + contentId
                                            + "," + dr["cost"]
                                            + "," + "1"
                                            + "," + "Succ"
                                            + "," + outputtime);
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        #region PARTNER

                        DataTable dtGpc = ViSport_S2_Registered_UsersController.CdrGpc(partnerId);
                        if (dtGpc != null && dtGpc.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtGpc.Rows)
                            {
                                string contentId = string.Empty;
                                //Type,TelcoCode,ServiceName,ShortCode,Msisdn,Unique_id,Price,ChargedStatus,Detail,Created
                                //var output = "=\"" + dr["msisdn"].ToString() + "\"";
                                //var outputtime = "=\"" + dr["TimeStamp"] + "\"";
                                var output =  dr["msisdn"].ToString();
                                var outputtime =  dr["TimeStamp"];
                                csv.AppendLine("SUB"
                                            + "," + "Gpc"
                                            + "," + dr["Service_Name"]
                                            + "," + dr["shortCode"]
                                            + "," + output
                                            + "," + contentId
                                            + "," + dr["cost"]
                                            + "," + "1"
                                            + "," + "Succ"
                                            + "," + outputtime);
                            }
                        }

                        #endregion
                    }

                    #endregion

                    #region VMS

                    if(partnerId == 21)
                    {
                        #region SAM

                        DataTable dtVms = ViSport_S2_Registered_UsersController.CdrVmsSam();
                        if (dtVms != null && dtVms.Rows.Count > 0)
                        {
                            const string shortCode = "8979";
                            foreach (DataRow dr in dtVms.Rows)
                            {
                                //var contentId = !string.IsNullOrEmpty(dr["Unique_Id"].ToString()) ? dr["Unique_Id"].ToString()
                                //    : ViSport_S2_Registered_UsersController.SamGetContentId(dr["Register_Systax"].ToString());
                                string contentId = "";
                                DataTable dtContent = ViSport_S2_Registered_UsersController.CdrGpcSam_GetContentId_ByUserID(dr["msisdn"].ToString());
                                if (dtContent != null && dtContent.Rows.Count > 0)
                                {
                                    contentId = dtContent.Rows[0]["ContentId"].ToString();
                                }
                                //var output = "=\"" + dr["msisdn"].ToString() + "\"";
                                //var outputtime = "=\"" + dr["TimeStamp"] + "\"";
                                var output =  dr["msisdn"].ToString();
                                var outputtime =  dr["TimeStamp"] ;
                                csv.AppendLine("SUB"
                                            + "," + "Vms"
                                            + "," + dr["Service_Name"]
                                            + "," + shortCode
                                            + "," + output
                                            + "," + contentId
                                            + "," + dr["cost"]
                                            + "," + dr["ChargeResult"]
                                            + "," + " "
                                            + "," + outputtime);

                            }
                            File.WriteAllText(filePath, csv.ToString());
                        }

                        #endregion
                    }
                    else
                    {
                        #region OTHER

                        DataTable dtVms = ViSport_S2_Registered_UsersController.CdrPartnerGetServiceId(partnerId);
                        if (dtVms != null && dtVms.Rows.Count > 0)
                        {
                            foreach (DataRow drSv in dtVms.Rows)
                            {
                                string serviceName = drSv["Service_Name"].ToString();
                                string serviceId = drSv["Service_Id"].ToString();
                                const string shortCode = "8979";

                                string contentId = ViSport_S2_Registered_UsersController.SamGetContentId(drSv["Register_Syntax"].ToString());

                                DataTable dtUsers = ViSport_S2_Registered_UsersController.SamVmsGetCdrByServiceId(serviceId);
                                if (dtUsers != null && dtUsers.Rows.Count > 0)
                                {
                                    foreach (DataRow dr in dtUsers.Rows)
                                    {
                                        //Type,TelcoCode,ServiceName,ShortCode,Msisdn,Unique_id,Price,ChargedStatus,Detail,Created
                                        //var output = "=\"" + dr["msisdn"].ToString() + "\"";
                                        //var outputtime = "=\"" + dr["TimeStamp"] + "\"";
                                        var output =  dr["msisdn"].ToString();
                                        var outputtime =  dr["TimeStamp"];
                                        csv.AppendLine("SUB"
                                            + "," + "Vms"
                                            + "," + serviceName
                                            + "," + shortCode
                                            + "," + output
                                            + "," + contentId
                                            + "," + dr["cost"]
                                            + "," + dr["ChargeResult"]
                                            + "," + " "
                                            + "," + outputtime);
                                    }
                                    File.WriteAllText(filePath, csv.ToString());
                                }

                            }
                        }

                        #endregion
                    }

                    #endregion

                    #region VNM

                    DataTable dtVnm = ViSport_S2_Registered_UsersController.CdrVnm(partnerId);
                    if (dtVnm != null && dtVnm.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtVnm.Rows)
                        {
                            //string contentId = ViSport_S2_Registered_UsersController.SamGetContentId(dr["Register_Syntax"].ToString());
                            string contentId = "";
                            DataTable dtContent = ViSport_S2_Registered_UsersController.CdrGpcSam_GetContentId_ByUserID(dr["User_Id"].ToString());
                            if (dtContent != null && dtContent.Rows.Count > 0)
                            {
                                contentId = dtContent.Rows[0]["ContentId"].ToString();
                            }
                            //var output = "=\"" + dr["User_Id"].ToString() + "\"";
                            //var outputtime = "=\"" + string.Format("{0:yyyyMMddHHmmss}", ConvertUtility.ToDateTime(dr["Charging_Time"])) + "\"";
                            var output = dr["User_Id"].ToString();
                            var outputtime =  string.Format("{0:yyyyMMddHHmmss}", ConvertUtility.ToDateTime(dr["Charging_Time"])) ;
                            csv.AppendLine("SUB"
                                        + "," + "Vnm"
                                        + "," + dr["Product_Name"]
                                        + "," + dr["shortCode"]
                                        + "," + output
                                        + "," + contentId
                                        + "," + dr["Charging_Price"]
                                        + "," + dr["Charging_Status"]
                                        + "," + dr["Charging_Response"].ToString().Replace(",", "-")
                                        + "," + outputtime);

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
                            //string contentId =ViSport_S2_Registered_UsersController.SamGetContentId(dr["SubscriptionKeyword"].ToString());
                            string contentId = "";
                            DataTable dtContent = ViSport_S2_Registered_UsersController.CdrGpcSam_GetContentId_ByUserID(dr["User_Id"].ToString());
                            if (dtContent != null && dtContent.Rows.Count > 0)
                            {
                                contentId = dtContent.Rows[0]["ContentId"].ToString();
                            }
                            //var output = "=\"" + dr["User_Id"].ToString() + "\"";
                            //var outputtime = "=\"" + string.Format("{0:yyyyMMddHHmmss}", ConvertUtility.ToDateTime(dr["Charging_Time"])) + "\"";
                            var output =  dr["User_Id"].ToString();
                            var outputtime =  string.Format("{0:yyyyMMddHHmmss}", ConvertUtility.ToDateTime(dr["Charging_Time"]));
                            csv.AppendLine("SUB"
                                        + "," + "Vnm"
                                        + "," + dr["Product_Name"]
                                        + "," + dr["shortCode"]
                                        + "," + output
                                        + "," + contentId
                                        + "," + dr["Charging_Price"]
                                        + "," + dr["Charging_Status"]
                                        + "," + dr["Charging_Response"].ToString().Replace(",","-")
                                        + "," + outputtime);
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
