using SMSManager_API.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for Teramobile
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Teramobile : System.Web.Services.WebService, IJobExecutorSoap
{

    public Teramobile () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(CdrGpc));

    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            int PartnerID = 47;
            DataTable dtPartner = ViSport_S2_Registered_UsersController.PartnerInfo(PartnerID);
            if (dtPartner != null && dtPartner.Rows.Count > 0)
            {
                int SubViettel_PartnerID = ConvertUtility.ToInt32(dtPartner.Rows[0]["SubViettel_PartnerID"].ToString());
                //int partnerId = ConvertUtility.ToInt32(dtPartner.Rows[0]["PartnerId"].ToString());
                string folderName = dtPartner.Rows[0]["FolderName"].ToString();

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
                DataTable dtGpc = ViSport_S2_Registered_UsersController.CdrGpc_ByPartnerID(PartnerID);
                if (dtGpc != null && dtGpc.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtGpc.Rows)
                    {
                        //var contentId = !string.IsNullOrEmpty(dr["Unique_Id"].ToString()) ? dr["Unique_Id"].ToString() 
                        //    : ViSport_S2_Registered_UsersController.SamGetContentId(dr["Register_Syntax"].ToString());
                        string contentId = "";

                        DataTable dtContent = ViSport_S2_Registered_UsersController.CdrSam_GetContentId_ByUserID_date(dr["msisdn"].ToString(), dataDate);
                        if (dtContent != null && dtContent.Rows.Count > 0)
                        {
                            contentId = dtContent.Rows[0]["ContentId"].ToString();
                        }


                        //string contentId = string.Empty;
                        var output = "=\"" + dr["msisdn"].ToString() + "\"";
                        var outputtime = "=\"" + dr["TimeStamp"] + "\"";
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

                #region VMS
                DataTable dtVms = ViSport_S2_Registered_UsersController.CdrVms_ByPartnerID(PartnerID);
                if (dtVms != null && dtVms.Rows.Count > 0)
                {
                    const string shortCode = "8979";
                    foreach (DataRow dr in dtVms.Rows)
                    {
                        //var contentId = !string.IsNullOrEmpty(dr["Unique_Id"].ToString()) ? dr["Unique_Id"].ToString()
                        //    : ViSport_S2_Registered_UsersController.SamGetContentId(dr["Register_Systax"].ToString());
                        string contentId = "";

                        DataTable dtContent = ViSport_S2_Registered_UsersController.CdrSam_GetContentId_ByUserID_date(dr["msisdn"].ToString(), dataDate);
                        if (dtContent != null && dtContent.Rows.Count > 0)
                        {
                            contentId = dtContent.Rows[0]["ContentId"].ToString();
                        }

                        var output = "=\"" + dr["msisdn"].ToString() + "\"";
                        var outputtime = "=\"" + dr["TimeStamp"] + "\"";
                        //string contentId = string.Empty;
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

                #region VNM

                DataTable dtVnm = ViSport_S2_Registered_UsersController.CdrVnm_ByDate(PartnerID, dataDate);
                if (dtVnm != null && dtVnm.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtVnm.Rows)
                    {
                        //string contentId = ViSport_S2_Registered_UsersController.SamGetContentId(dr["Register_Syntax"].ToString());
                        string contentId = "";
                        DataTable dtContent = ViSport_S2_Registered_UsersController.CdrSam_GetContentId_ByUserID_date(dr["User_Id"].ToString(), dataDate);
                        if (dtContent != null && dtContent.Rows.Count > 0)
                        {
                            contentId = dtContent.Rows[0]["ContentId"].ToString();
                        }
                        //var output = "=TEXT(" + dr["User_Id"].ToString() + ", \"0\")";

                        var output = "=\"" + dr["User_Id"].ToString() + "\"";
                        var outputtime = "=\"" + string.Format("{0:yyyyMMddHHmmss}", ConvertUtility.ToDateTime(dr["Charging_Time"])) + "\"";
                        csv.AppendLine("SUB"
                                    + "," + "Vnm"
                                    + "," + dr["Product_Name"]
                                    + "," + dr["shortCode"]
                                    //+ "," + dr["User_Id"].ToString()
                                    + "," + output
                                    + "," + contentId
                                    + "," + dr["Charging_Price"]
                                    + "," + dr["Charging_Status"]
                                    + "," + dr["Charging_Response"].ToString().Replace(",", "-")
                                     //+ "," + ConvertUtility.ToDateTime(dr["Charging_Time"]));
                                     + "," + outputtime);

                    }
                    File.WriteAllText(filePath, csv.ToString());
                }

                #endregion

                #region VNM 1119
                DataTable dtVnm1119 = ViSport_S2_Registered_UsersController.CdrVnm1119_ByDate(PartnerID, dataDate);
                if (dtVnm1119 != null && dtVnm1119.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtVnm1119.Rows)
                    {
                        //string contentId =ViSport_S2_Registered_UsersController.SamGetContentId(dr["SubscriptionKeyword"].ToString());
                        string contentId = "";
                        DataTable dtContent = ViSport_S2_Registered_UsersController.CdrSam_GetContentId_ByUserID_date(dr["User_Id"].ToString(), dataDate);
                        if (dtContent != null && dtContent.Rows.Count > 0)
                        {
                            contentId = dtContent.Rows[0]["ContentId"].ToString();
                        }
                        var output = "=\"" + dr["User_Id"].ToString() + "\"";
                        var outputtime = "=\"" + string.Format("{0:yyyyMMddHHmmss}", ConvertUtility.ToDateTime(dr["Charging_Time"])) + "\"";
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

                //#region VIETTEL SUB 9029
                //DataTable dtViettel = ViSport_S2_Registered_UsersController.CdrViettel9029(SubViettel_PartnerID);
                //if (dtViettel != null && dtViettel.Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dtViettel.Rows)
                //    {
                //        //string contentId = ViSport_S2_Registered_UsersController.SamGetContentId(dr["Register_Syntax"].ToString());
                //        string contentId = "";
                //        //DataTable dtContent = ViSport_S2_Registered_UsersController.CdrGpcSam_GetContentId_ByUserID(dr["User_Id"].ToString());
                //        //if (dtContent != null && dtContent.Rows.Count > 0)
                //        //{
                //        //    contentId = dtContent.Rows[0]["ContentId"].ToString();
                //        //}
                //        csv.AppendLine("SUB"
                //                    + "," + "Viettel"
                //                    + "," + dr["Product_Name"]
                //                    + "," + dr["shortCode"]
                //                    + "," + dr["User_Id"]
                //                    + "," + contentId
                //                    + "," + dr["Charging_Price"]
                //                    + "," + dr["Charging_Status"]
                //                    + "," + dr["Charging_Response"]
                //                    + "," + ConvertUtility.ToDateTime(dr["Charging_Time"]));

                //    }
                //    File.WriteAllText(filePath, csv.ToString());
                //}
                //#endregion
                _log.Debug("Teramobile CDR - jobID :" + jobId);
            }
        }
        catch (Exception ex)
        {
            _log.Error("Teramobile CDR Error : " + ex);
            return 0;
        }
        return 1;
    }
    
}
