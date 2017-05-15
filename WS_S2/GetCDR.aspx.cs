using Microsoft.ApplicationBlocks.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SMSManager_API.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GetCDR : System.Web.UI.Page
{
    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(GetCDR));
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetDate(DateTime.Now);
            DataTable dtPartner = ViSport_S2_Registered_UsersController.PartnerGetAll_Retry();
            DropDoiTac.DataTextField = "Name";
            DropDoiTac.DataValueField = "partnerID";
            DropDoiTac.DataSource = dtPartner;
            DropDoiTac.DataBind();
            lblUpdateStatus.Text = ConvertUtility.ToString(DateTime.Now.DayOfWeek);
        }
    }
    public void SetDate(DateTime date)
    {
        MiscUtility.FillIndex(dgrThang, 1, 12, date.Month);
        MiscUtility.FillIndex(dgrNam, date.Year - 5, date.Year + 5, date.Year);
        MiscUtility.FillIndex(dgrNgay, 1, 31, date.Day);
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int partnerId = ConvertUtility.ToInt32(DropDoiTac.SelectedValue);
        DateTime dataDate = new DateTime(ConvertUtility.ToInt32(dgrNam.SelectedValue), ConvertUtility.ToInt32(dgrThang.SelectedValue), ConvertUtility.ToInt32(dgrNgay.SelectedValue));
        #region Log chung GPC
        try
        {
            //DataTable dtPartner = ViSport_S2_Registered_UsersController.PartnerGetAll_Active();
            //const int partnerId = 21;
            ViSport_S2_Registered_UsersController.GpcCdrReset();//RESET

            DataTable dtVms = ViSport_S2_Registered_UsersController.GPC_CdrPartnerGetServiceId(partnerId);
            if (dtVms != null && dtVms.Rows.Count > 0)
            {


                foreach (DataRow drSv in dtVms.Rows)
                {
                    string serviceName = drSv["Service_Name"].ToString();
                    string serviceId = drSv["Service_Id"].ToString();
                    const string shortCode = "8979";
                    string registerSystax = drSv["Register_Syntax"].ToString();

                    try
                    {
                        DataTable dtUsers = ViSport_S2_Registered_UsersController.GPCGetCdrByServiceId_ByDate(serviceId, dataDate);
                        if (dtUsers != null && dtUsers.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtUsers.Rows)
                            {
                                ViSport_S2_Registered_UsersController.GPCCdrAdd(
                                    dr["msisdn"].ToString(),
                                    serviceId,
                                    ConvertUtility.ToInt32(dr["cost"].ToString()),
                                    serviceName,
                                    shortCode,
                                    registerSystax, dr["TimeStamp"].ToString(),
                                    //ConvertUtility.ToInt32(dr["ChargeResult"]),
                                    partnerId
                                    );
                            }
                        }
                        _log.Debug("GPC Service_Name" + serviceName + "--Count user :" + dtUsers.Rows.Count);
                    }
                    catch (Exception ex)
                    {
                        _log.Error("CDR Loi UPDATE GPC -" + serviceId + "- : " + ex);
                    }

                }

            }
            //Insert log chung Ok
            _log.Debug("CDR GPC update");
        }
        catch (Exception ex)
        {
            _log.Error("CDR Loi UPDATE GPC : " + ex);

        }

        #endregion

        #region Log chung VMS
        try
        {
            //DataTable dtPartner = ViSport_S2_Registered_UsersController.PartnerGetAll_Active();
            //const int partnerId = 21;
            ViSport_S2_Registered_UsersController.VmsCdrReset();//RESET


            DataTable dtVms = ViSport_S2_Registered_UsersController.CdrPartnerGetServiceId(partnerId);
            if (dtVms != null && dtVms.Rows.Count > 0)
            {


                foreach (DataRow drSv in dtVms.Rows)
                {
                    string serviceName = drSv["Service_Name"].ToString();
                    string serviceId = drSv["Service_Id"].ToString();
                    const string shortCode = "8979";
                    string registerSystax = drSv["Register_Syntax"].ToString();
                    try
                    {
                        DataTable dtUsers = ViSport_S2_Registered_UsersController.SamVmsGetCdrByServiceId_ByDate(serviceId, dataDate);
                        if (dtUsers != null && dtUsers.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtUsers.Rows)
                            {
                                ViSport_S2_Registered_UsersController.VmsCdrAdd(
                                    dr["msisdn"].ToString(),
                                    serviceId,
                                    ConvertUtility.ToInt32(dr["cost"].ToString()),
                                    serviceName,
                                    shortCode,
                                    registerSystax, dr["TimeStamp"].ToString(),
                                    ConvertUtility.ToInt32(dr["ChargeResult"]),
                                    partnerId
                                    );
                            }
                        }
                        _log.Debug("VMS Service_Name" + serviceName + "--Count user :" + dtUsers.Rows.Count);
                    }
                    catch (Exception ex)
                    {
                        _log.Error("CDR Loi UPDATE GPC -" + serviceId + "- : " + ex);
                    }

                }

            }
            //Insert log chung Ok
            _log.Debug("CDR VMS update");
        }
        catch (Exception ex)
        {
            _log.Error("CDR Loi UPDATE VMS : " + ex);

        }
        #endregion

        #region Generate
        try
        {
            int PartnerID = partnerId;
            DataTable dtPartner = ViSport_S2_Registered_UsersController.PartnerInfo(PartnerID);
            if (dtPartner != null && dtPartner.Rows.Count > 0)
            {
                int SubViettel_PartnerID = ConvertUtility.ToInt32(dtPartner.Rows[0]["SubViettel_PartnerID"].ToString());
                //int partnerId = ConvertUtility.ToInt32(dtPartner.Rows[0]["PartnerId"].ToString());
                string folderName = dtPartner.Rows[0]["FolderName"].ToString();

                folderName = "/Sam_Cdr"; //Forder for re push CDR

                if (!Directory.Exists(Server.MapPath(folderName)))
                {
                    Directory.CreateDirectory(Server.MapPath(folderName));
                }

                var csv = new StringBuilder();
                //DateTime dataDate = DateTime.Today.AddDays(-1);
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
                        //var output = "=\"" + dr["msisdn"].ToString() + "\"";
                        //var outputtime = "=\"" + dr["TimeStamp"] + "\"";
                        var output = dr["msisdn"].ToString();
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

                        //var output = "=\"" + dr["msisdn"].ToString() + "\"";
                        //var outputtime = "=\"" + dr["TimeStamp"] + "\"";
                        var output = dr["msisdn"].ToString();
                        var outputtime = dr["TimeStamp"];
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

                        //var output = "=\"" + dr["User_Id"].ToString() + "\"";
                        //var outputtime = "=\"" + string.Format("{0:yyyyMMddHHmmss}", ConvertUtility.ToDateTime(dr["Charging_Time"])) + "\"";
                        var output = dr["User_Id"].ToString();
                        var outputtime = string.Format("{0:yyyyMMddHHmmss}", ConvertUtility.ToDateTime(dr["Charging_Time"]));
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
                        //var output = "=\"" + dr["User_Id"].ToString() + "\"";
                        //var outputtime = "=\"" + string.Format("{0:yyyyMMddHHmmss}", ConvertUtility.ToDateTime(dr["Charging_Time"])) + "\"";
                        var output = dr["User_Id"].ToString();
                        var outputtime = string.Format("{0:yyyyMMddHHmmss}", ConvertUtility.ToDateTime(dr["Charging_Time"]));
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

            }
            lblUpdateStatus.Text = "Thành công";
        }
        catch (Exception ex)
        {
            _log.Error("CDR Error : " + ex);
            lblUpdateStatus.Text = ex.ToString();
        }
        #endregion
    }
    protected void btnUpdateLog_Click(object sender, EventArgs e)
    {
        int partnerId = ConvertUtility.ToInt32(DropDoiTac.SelectedValue);
        DateTime dataDate = new DateTime(ConvertUtility.ToInt32(dgrNam.SelectedValue), ConvertUtility.ToInt32(dgrThang.SelectedValue), ConvertUtility.ToInt32(dgrNgay.SelectedValue));
        #region Log chung GPC
        try
        {
            //DataTable dtPartner = ViSport_S2_Registered_UsersController.PartnerGetAll_Active();
            //const int partnerId = 21;
            ViSport_S2_Registered_UsersController.GpcCdrReset();//RESET

            DataTable dtVms = ViSport_S2_Registered_UsersController.GPC_CdrPartnerGetServiceId(partnerId);
            if (dtVms != null && dtVms.Rows.Count > 0)
            {


                foreach (DataRow drSv in dtVms.Rows)
                {
                    string serviceName = drSv["Service_Name"].ToString();
                    string serviceId = drSv["Service_Id"].ToString();
                    const string shortCode = "8979";
                    string registerSystax = drSv["Register_Syntax"].ToString();

                    DataTable dtUsers = ViSport_S2_Registered_UsersController.GPCGetCdrByServiceId_ByDate(serviceId, dataDate);
                    if (dtUsers != null && dtUsers.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtUsers.Rows)
                        {
                            ViSport_S2_Registered_UsersController.GPCCdrAdd(
                                dr["msisdn"].ToString(),
                                serviceId,
                                ConvertUtility.ToInt32(dr["cost"].ToString()),
                                serviceName,
                                shortCode,
                                registerSystax, dr["TimeStamp"].ToString(),
                                //ConvertUtility.ToInt32(dr["ChargeResult"]),
                                partnerId
                                );
                        }
                    }
                }

            }
            //Insert log chung Ok
            _log.Debug("CDR GPC update");
        }
        catch (Exception ex)
        {
            _log.Error("CDR Loi UPDATE GPC : " + ex);

        }

        #endregion

        #region Log chung VMS
        try
        {
            //DataTable dtPartner = ViSport_S2_Registered_UsersController.PartnerGetAll_Active();
            //const int partnerId = 21;
            ViSport_S2_Registered_UsersController.VmsCdrReset();//RESET


            DataTable dtVms = ViSport_S2_Registered_UsersController.CdrPartnerGetServiceId(partnerId);
            if (dtVms != null && dtVms.Rows.Count > 0)
            {


                foreach (DataRow drSv in dtVms.Rows)
                {
                    string serviceName = drSv["Service_Name"].ToString();
                    string serviceId = drSv["Service_Id"].ToString();
                    const string shortCode = "8979";
                    string registerSystax = drSv["Register_Syntax"].ToString();

                    DataTable dtUsers = ViSport_S2_Registered_UsersController.SamVmsGetCdrByServiceId_ByDate(serviceId, dataDate);
                    if (dtUsers != null && dtUsers.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtUsers.Rows)
                        {
                            ViSport_S2_Registered_UsersController.VmsCdrAdd(
                                dr["msisdn"].ToString(),
                                serviceId,
                                ConvertUtility.ToInt32(dr["cost"].ToString()),
                                serviceName,
                                shortCode,
                                registerSystax, dr["TimeStamp"].ToString(),
                                ConvertUtility.ToInt32(dr["ChargeResult"]),
                                partnerId
                                );
                        }
                    }
                }

            }
            //Insert log chung Ok
            _log.Debug("CDR VMS update");
        }
        catch (Exception ex)
        {
            _log.Error("CDR Loi UPDATE VMS : " + ex);

        }
        #endregion
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        #region Generate
        int partnerId = ConvertUtility.ToInt32(DropDoiTac.SelectedValue);
        DateTime dataDate = new DateTime(ConvertUtility.ToInt32(dgrNam.SelectedValue), ConvertUtility.ToInt32(dgrThang.SelectedValue), ConvertUtility.ToInt32(dgrNgay.SelectedValue));
        try
        {
            int PartnerID = partnerId;
            DataTable dtPartner = ViSport_S2_Registered_UsersController.PartnerInfo(PartnerID);
            if (dtPartner != null && dtPartner.Rows.Count > 0)
            {
                int SubViettel_PartnerID = ConvertUtility.ToInt32(dtPartner.Rows[0]["SubViettel_PartnerID"].ToString());
                //int partnerId = ConvertUtility.ToInt32(dtPartner.Rows[0]["PartnerId"].ToString());
                string folderName = dtPartner.Rows[0]["FolderName"].ToString();

                folderName = "/Sam_Cdr"; //Forder for re push CDR

                if (!Directory.Exists(Server.MapPath(folderName)))
                {
                    Directory.CreateDirectory(Server.MapPath(folderName));
                }

                var csv = new StringBuilder();
                //DateTime dataDate = DateTime.Today.AddDays(-1);
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
                        //var output = "=\"" + dr["msisdn"].ToString() + "\"";
                        //var outputtime = "=\"" + dr["TimeStamp"] + "\"";
                        var output = dr["msisdn"].ToString();
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

                        //var output = "=\"" + dr["msisdn"].ToString() + "\"";
                        //var outputtime = "=\"" + dr["TimeStamp"] + "\"";
                        var output = dr["msisdn"].ToString();
                        var outputtime = dr["TimeStamp"];
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
                        //var output = "=\"" + dr["User_Id"].ToString() + "\"";
                        //var outputtime = "=\"" + string.Format("{0:yyyyMMddHHmmss}", ConvertUtility.ToDateTime(dr["Charging_Time"])) + "\"";
                        var output = dr["User_Id"].ToString();
                        var outputtime = string.Format("{0:yyyyMMddHHmmss}", ConvertUtility.ToDateTime(dr["Charging_Time"]));
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
                                    + "," + outputtime);
                        //csv.AppendFormat("{0},{1},{2},{3},\"{4}\",{5},{6},{7},{8}", "SUB", "Vnm", dr["Product_Name"], dr["shortCode"], output, "0", dr["Charging_Price"], dr["Charging_Status"], dr["Charging_Response"], dr["Charging_Time"]);

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
                        //var output = "=\"" + dr["User_Id"].ToString() + "\"";
                        //var outputtime = "=\"" + string.Format("{0:yyyyMMddHHmmss}", ConvertUtility.ToDateTime(dr["Charging_Time"])) + "\"";
                        var output = dr["User_Id"].ToString();
                        var outputtime = string.Format("{0:yyyyMMddHHmmss}", ConvertUtility.ToDateTime(dr["Charging_Time"]));
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

            }
            lblUpdateStatus.Text = "Thành công";
        }
        catch (Exception ex)
        {
            _log.Error("CDR Error : " + ex);
            lblUpdateStatus.Text = ex.ToString();
        }
        #endregion
    }

    protected void btUpdateLog_Click(object sender, EventArgs e)
    {
        DataTable dtLogcharg_old = S294x_GetLogCharg_old(1006);
        foreach (DataRow row in dtLogcharg_old.Rows)
        {
            try
            {
                S2_949_UpdateLogCharg(
                 row["User_ID"].ToString()
               , row["Request_ID"].ToString()
               , ConvertUtility.ToInt32(row["Service_Type"].ToString())
               , ConvertUtility.ToInt32(row["Service_ID"].ToString())
               , ConvertUtility.ToInt32(row["Registered_ID"].ToString())
               , row["Short_Code"].ToString()
               , row["Command_Code"].ToString()
               , ConvertUtility.ToInt32(row["Charging_Price"].ToString())
               , ConvertUtility.ToDateTime(row["Charging_Time"].ToString())
               , row["Charging_Response"].ToString()
               , ConvertUtility.ToInt32(row["Charging_Status"].ToString())
               , row["Charging_Account"].ToString()
               , ConvertUtility.ToInt32(row["PartnerID"].ToString())
               );
            }
            catch (Exception ex)
            {
                lblUpdateStatus.Text = ex.ToString();
            }
        }
        lblUpdateStatus.Text = dtLogcharg_old.Rows.Count.ToString();
    }
    protected void btnUpdate_User_Click(object sender, EventArgs e)
    {
        //string Message_confirm = "y ";
        //Message_confirm = Message_confirm.Substring(1, Message_confirm.Length-1).Trim();
        //if (string.IsNullOrEmpty(Message_confirm))
        //{
        //    lblUpdateStatus.Text = "a";
        //}
        //Getdata();
        //DataTable dtService_1119 = S2_All_Service_1119();
        //int Service_Type = ConvertUtility.ToInt32(drop_Service_type.SelectedValue);
        DataTable dtService_949 = User_log_VNM_New_GetAll(1006);//Gameportal_GetActive_User(0);
        foreach (DataRow _row in dtService_949.Rows)
        {
            try
            {



                #region Log User wap
                //SMS_MODB un = new SMS_MODB();
                //string TRANSACTION_ID = un.GetCurrentUnixTimestampMillis().ToString();
                //string MSISDN = _row["User_ID"].ToString();
                //string SERVICE_NAME = "Dịch vụ Gameportal";//_row["Service_Name"].ToString();
                //string SHORTCODE = "2288";//_row["Short_Code"].ToString();
                //int PRICE = 10000;// ConvertUtility.ToInt32(_row["Charging_Price"].ToString());
                //string DESC = "Dịch vụ Gameportal";// _row["Service_Name"].ToString();
                //int VALIDITY = 7;//ConvertUtility.ToInt32(_row["PeriodLength"].ToString());
                //int FREETRIAL = 1;//ConvertUtility.ToInt32(_row["NoChargeLength"].ToString());
                //string BASE_URL = "http://vmgame.vn/";
                //string CALLBACK_URL = "http://wap.vietnamobile.com.vn/Wap/RegisterAll.aspx?transactionID=" + TRANSACTION_ID;
                //string CONFIRM = "YES";
                //int SERVICE_ID = 0;// ConvertUtility.ToInt32(_row["Service_ID"].ToString());
                //DateTime INSERT_DATE = ConvertUtility.ToDateTime(_row["RegisteredTime"].ToString());
                //S2_UpdateUser_ToLogConfirm(MSISDN, SERVICE_NAME, SHORTCODE, PRICE, DESC, VALIDITY, FREETRIAL, BASE_URL, CALLBACK_URL, TRANSACTION_ID, CONFIRM, 2, SERVICE_ID, INSERT_DATE);
                #endregion

                #region Log user sms
                //int price = ConvertUtility.ToInt32(_row["Charging_Price"].ToString());
                int Service_ID = ConvertUtility.ToInt32(_row["Service_ID"].ToString());
                string Service_Name =  _row["Service_Name"].ToString();
                //int PeriodLength = ConvertUtility.ToInt32(_row["PeriodLength"].ToString());
                string Cancel_Syntax = _row["Cancel_Syntax"].ToString();
                string cancel_MT = _row["cancel_MT"].ToString();
                string Right_Syntax_MT = _row["Right_Syntax_MT"].ToString();
                string A_Number, B_Number, Msg_Content;
                DateTime Send_Time = ConvertUtility.ToDateTime(_row["CancelTime_rd"].ToString());
                Random rnd = new Random();
                //SMS nhắn lên
                //A_Number = _row["User_ID"].ToString();
                //B_Number = "2288";// _row["Short_Code"].ToString();
                ////Send_Time = ConvertUtility.ToDateTime(_row["RegisteredTime"].ToString());
                //Msg_Content = GetMo(Register_Syntax);
                //TB_CP_VAS_SMS_insert(A_Number, B_Number, Send_Time, Msg_Content, Service_ID, Service_Name, 1, 2);
                //// trả confirm 
                //B_Number = _row["User_ID"].ToString();
                //A_Number = "2288";//_row["Short_Code"].ToString();
                //Send_Time = Send_Time.AddSeconds(rnd.Next(7, 15));
                ////Msg_Content = "Quy khach vui long soan Y gui " + A_Number + " de xac nhan dong y dang ky va tu dong gia han hang ngay " + Service_Name + " voi gia dich vu: " + price + "d /" + PeriodLength + " ngay.Tran trong cam on";
                //Msg_Content = "Quy khach vui long soan Y gui 2288 de xac nhan dong y dang ky va tu dong gia han hang ngay dich vu GamePortal voi gia dich vu:10.000d/tuan. Tran trong cam on!";
                //TB_CP_VAS_SMS_insert(A_Number, B_Number, Send_Time, Msg_Content, Service_ID, Service_Name, 2, 2);
                ////SMS xác nhận
                A_Number = _row["User_ID"].ToString();
                B_Number = "949";//_row["Short_Code"].ToString();
                Send_Time = Send_Time.AddSeconds(-(rnd.Next(10, 20)));
                Msg_Content = GetMo(Cancel_Syntax);
                TB_CP_VAS_WAP_New1_Insert(A_Number, B_Number, Send_Time, Msg_Content, Service_ID, Service_Name, 1, 2);
                //trả Mt đăng ký
                B_Number = _row["User_ID"].ToString();
                A_Number = "949";//_row["Short_Code"].ToString();
                Send_Time = Send_Time.AddSeconds(rnd.Next(10, 20));
                Msg_Content = cancel_MT;//Right_Syntax_MT.Replace("Shortcode", _row["Short_Code"].ToString());
                //Msg_Content = "Chuc mung QK da dang ky thanh cong dich vu Game portal cua Vietnamobile, (phi dich vu 10.000 d/tuan,dvu se duoc tu dong gia han). Moi ban truy cap de tai game : http://vmgame.vn. Hang tuan ban se duoc tai 2 game MIEN PHI khi truy cap vao link. De huy dich vu soan HUY gui 2288";
                TB_CP_VAS_WAP_New1_Insert(A_Number, B_Number, Send_Time, Msg_Content, Service_ID, Service_Name, 3, 1006);

                #endregion

                #region Check trung ma
                //if (NormalizeSyntax(row["SubscriptionKeyword"].ToString()).Contains("|"))
                //{
                //    string[] lines = NormalizeSyntax(row["SubscriptionKeyword"].ToString()).Split('|');
                //    foreach (string line in lines)
                //    {

                //        foreach (DataRow _dr in dtService_949.Rows)
                //        {
                //            string return_value = Check_exist_keyword(line, _dr["Register_syntax"].ToString());
                //            if (return_value != "")
                //            {
                //                check = true;
                //                Matrung = Matrung + return_value + ",";
                //                update_exist1119vs949(ConvertUtility.ToInt32(row["ServiceId"].ToString()), Matrung);
                //            }

                //        }
                //    }
                //}
                //else
                //{
                //    foreach (DataRow _dr in dtService_949.Rows)
                //    {
                //        string return_value = Check_exist_keyword(NormalizeSyntax(row["SubscriptionKeyword"].ToString()), _dr["Register_syntax"].ToString());
                //        if (return_value != "")
                //        {
                //            check = true;
                //            Matrung = Matrung + return_value + ",";
                //            update_exist1119vs949(ConvertUtility.ToInt32(row["ServiceId"].ToString()), Matrung);
                //        }
                //    }
                //}

                //JobExecutor serviceNotification = new JobExecutor("http://101.99.16.163:9036/S2Jobs/JobGeneral.asmx");
                //serviceNotification.Execute(ConvertUtility.ToInt32(row["ID"].ToString()));
                //Thread.Sleep(10000);
                //int dem = S2_GetActive_User(ConvertUtility.ToInt32(row["Loai_Goi"]));
                //S2_Update_TB_LuyKe(ConvertUtility.ToInt32(row["Loai_Goi"]), dem);

                //DataTable dt_info = S2_AllUsser_loop_user_info(ConvertUtility.ToInt32(row["Service_ID"].ToString()), row["User_ID"].ToString());
                ////DataTable dt_info = S2_AllUsser_loop_user_info(665, "841882021669");
                //if (dt_info != null && dt_info.Rows.Count > 0)
                ////if (dt_info != null && dt_info.Rows.Count > 0)
                //{
                //    string a = "";
                //   for(int i=1;i<= dt_info.Rows.Count - 1; i++)
                //    {
                //        //a = a + dt_info.Rows[i]["user_id"].ToString();
                //        S2_AllUsser_loop_delete(ConvertUtility.ToInt32(dt_info.Rows[i]["id"].ToString()), dt_info.Rows[i]["user_id"].ToString());
                //    }
                //}
                //S2_UpdateUser_dbMoi(
                //    row["User_ID"].ToString()
                //    , row["Request_ID"].ToString()
                //    , ConvertUtility.ToInt32(row["Service_ID"].ToString())
                //    , row["Short_Code"].ToString()
                //    , row["Command_Code"].ToString()
                //    , row["Reference_ID"].ToString()
                //    , ConvertUtility.ToInt32(row["PeriodLength"].ToString())
                //    , ConvertUtility.ToInt32(row["NoChargeLength"].ToString())
                //    , ConvertUtility.ToInt32(row["Service_Type"].ToString())
                //    , ConvertUtility.ToInt32(row["Charging_Count"].ToString())
                //    , ConvertUtility.ToInt32(row["FailedChargingTimes"].ToString())
                //    , ConvertUtility.ToDateTime(row["RegisteredTime"].ToString())
                //    , ConvertUtility.ToDateTime(row["ExpiredTime"].ToString())
                //    , ConvertUtility.ToInt32(row["Status"].ToString())
                //    , row["Registration_Channel"].ToString()
                //    , ConvertUtility.ToDateTime(row["CancelTime"].ToString())
                //    , row["Cancel_Channel"].ToString()
                //    , ConvertUtility.ToInt32(row["Flag"].ToString())
                //    , ConvertUtility.ToInt32(row["ChargedFee"].ToString())
                //    , ConvertUtility.ToInt32(row["CountTo_Cancel"].ToString())
                //    , ConvertUtility.ToInt32(row["Charging_Price"].ToString())
                //    , ConvertUtility.ToInt32(row["Charging_Status"].ToString())
                //    );

                #endregion

                //DateTime CancelTime = ConvertUtility.ToDateTime(_row["CancelTime"]);
                //int id = ConvertUtility.ToInt32(_row["ID"]);
                ////DateTime startDate = StartDay(CancelTime);
                ////DateTime endDate = EndDay(RegisteredTime);
                //DateTime startDate = StartDay(CancelTime);
                //Random rnd = new Random();
                //DateTime endDate = EndDay(startDate.AddDays(rnd.Next(1, 5)));

                ////TimeSpan timeSpan = endDate - startDate;
                ////var randomTest = new Random();
                ////TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
                ////DateTime newDate = startDate + newSpan;

                //Random r = new Random();
                //long rand62bit = (((long)r.Next()) << 31) + r.Next();
                //// 62bits suffices for random datetimes, 31 does not!
                //DateTime newDate = startDate + new TimeSpan(rand62bit % (endDate - startDate).Ticks);
                //S2_Updatengay_huy(id, newDate);
            }
            catch (Exception ex)
            {
                //_log.Error("loi user "+ row["User_ID"].ToString() +ex.Message.ToString());
            }
        }
        //lblUpdateStatus.Text = dtUser_old.Rows.Count.ToString();
    }
    #region 94x get user
    public static DateTime EndDay(DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
    }

    public static DateTime StartDay(DateTime date)
    {
        return new DateTime(date.Year, date.Month, date.Day, 7, 0, 0, 0);
    }
    public static DataTable S2_All_userlog()
    {
        DataSet ds = SqlHelper.ExecuteDataset(CnnStrTtndService_new, CommandType.Text,
                    String.Format("SELECT * from User_log_VNM_New where Registration_Channel='other'"));
        if (ds != null && ds.Tables.Count > 0)
            return ds.Tables[0];
        return null;
    }
    //public static void S2_Updatengaydk(int id, DateTime ngay)
    //{
    //    SqlHelper.ExecuteNonQuery(CnnStrTtndService_new, CommandType.Text,
    //                //String.Format("Select * from S2_TTND_Registered_Users where  Service_Type = " + Service_Type))
    //                String.Format("Update User_log_VNM set RegisteredTime_rd = '" + ngay + "' where  ID = " + id + ""));

    //}
    public static void S2_Updatengay_huy(int id, DateTime ngay)
    {
        SqlHelper.ExecuteNonQuery(CnnStrTtndService_new, CommandType.Text,
                    //String.Format("Select * from S2_TTND_Registered_Users where  Service_Type = " + Service_Type))
                    String.Format("Update User_log_VNM_New set CancelTime_rd = '" + ngay + "' where  ID = " + id + ""));

    }
    public static DataTable User_log_VNM_New_GetAll(int Service_Type)
    {

        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringTtndService, "User_log_VNM_New_GetAll", Service_Type);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
        
    }
    public static void TB_CP_VAS_WAP_New1_Insert(
      string A_Number
    , string B_Number
    , DateTime Send_Time
    , string Msg_Content
    , int Service_ID
    , string Service_Name
    , int Type
   , int Service_Type
    )
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionStringTtndService
                                                           , "TB_CP_VAS_WAP_New1_Insert",
         A_Number
   , B_Number
   , Send_Time
   , Msg_Content
   , Service_ID
   , Service_Name
   , Type
   , Service_Type
       );
    }
    #endregion
    #region MyRegion
    public static string CnnStrTtndService_old = "server=db168.vmgmedia.vn;database=TTND_Services;uid=ttndservices;pwd=ttND53r7vice$";
    public static string CnnStrTtndService_new = "server=dbttnd.vmgmedia.vn;database=TTND_Services;uid=ttndservices;pwd=ttND53r7vice$";
    public static DataTable S2_GetLogCharg_old(int Service_Type)
    {
        DataSet ds = SqlHelper.ExecuteDataset(CnnStrTtndService_old, CommandType.Text,
                    String.Format("Select * from S2_TTND_VnmChargingLog_201608 where day(charging_time) = 18 and Service_Type = " + Service_Type));
        if (ds != null && ds.Tables.Count > 0)
            return ds.Tables[0];
        return null;
    }
    public static DataTable S294x_GetLogCharg_old(int Service_Type)
    {
        DataSet ds = SqlHelper.ExecuteDataset(CnnStrTtndService_old, CommandType.Text,
                    String.Format("Select * from S2_TTND_3gChargingLog_201608 where day(charging_time) = 18 and Charging_Status = 1 and Service_Type = " + Service_Type));
        if (ds != null && ds.Tables.Count > 0)
            return ds.Tables[0];
        return null;
    }

    public static int S2_STK_UpdateLogCharg(
          string User_ID
        , string Request_ID
        , int Service_Type
        , int Service_ID
        , int Registered_ID
        , string Short_Code
        , string Command_Code
        , int Charging_Price
        , DateTime Charging_Time
        , string Charging_Response
        , int Charging_Status
        , string Charging_Account
        , int PartnerID
                  )
    {
        return ConvertUtility.ToInt32(SqlHelper.ExecuteScalar(CnnStrTtndService_new
                                                            , "S2_STK_UpdateLogCharg",
       User_ID
, Request_ID
, Service_Type
, Service_ID
, Registered_ID
, Short_Code
, Command_Code
, Charging_Price
, Charging_Time
, Charging_Response
, Charging_Status
, Charging_Account
, PartnerID
));
    }

    public static int S2_949_UpdateLogCharg(
          string User_ID
        , string Request_ID
        , int Service_Type
        , int Service_ID
        , int Registered_ID
        , string Short_Code
        , string Command_Code
        , int Charging_Price
        , DateTime Charging_Time
        , string Charging_Response
        , int Charging_Status
        , string Charging_Account
        , int PartnerID
                  )
    {
        return ConvertUtility.ToInt32(SqlHelper.ExecuteScalar(CnnStrTtndService_new
                                                            , "S2_949_UpdateLogCharg",
       User_ID
, Request_ID
, Service_Type
, Service_ID
, Registered_ID
, Short_Code
, Command_Code
, Charging_Price
, Charging_Time
, Charging_Response
, Charging_Status
, Charging_Account
, PartnerID
));
    }
    #endregion

    #region s2949
   

    public static DataTable S2_All_Service_KPI()
    {
        DataSet ds = SqlHelper.ExecuteDataset(CnnStrTtndService_new, CommandType.Text,
                    String.Format("SELECT * FROM [Report_VNM_94x] where 1 = 1 and year(NGAY) = 2016 and month(NGAY) = 9 and day(NGAY) = 18 and thue_bao_luy_ke < 0"));
        if (ds != null && ds.Tables.Count > 0)
            return ds.Tables[0];
        return null;
    }

    public static DataTable S294x_GetActive_User(int Service_Type)
    {

        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringTtndService, "S294x_GetActive_User", Service_Type);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
        //return ConvertUtility.ToInt32(SqlHelper.ExecuteScalar(CnnStrTtndService_new, "Report_94x_Count_User", service_id));
        //string sqlqr = "select * from S2_TTND_Registered_Users where service_type = " + Service_Type + " and registeredtime >='2016-08-09' and registeredtime <='2016-11-21' order by id asc";
        //DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringTtndService, CommandType.Text,
        //           String.Format(sqlqr));
        //if (ds != null && ds.Tables.Count > 0)
        //    return ds.Tables[0];
        //return null;
    }
    public static DataTable Visport_GetActive_User()
    {

        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString, "Visport_GetActive_User");
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;
       
    }
    public static DataTable Gameportal_GetActive_User(int type)
    {

        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionString56, "Gameportal_GetActive_User",type);
        if (ds != null && ds.Tables.Count > 0)
        {
            return ds.Tables[0];
        }
        return null;

    }
    public static DataTable S294x_GetInActive_User(int Service_Type)
    {
        //return ConvertUtility.ToInt32(SqlHelper.ExecuteScalar(CnnStrTtndService_new, "Report_94x_Count_User", service_id));
        string sqlqr = "select * from S2_TTND_InActive_Users where service_type = " + Service_Type + " and registeredtime >='2016-08-09' and registeredtime <='2016-11-21' order by id asc";
        DataSet ds = SqlHelper.ExecuteDataset(AppEnv.ConnectionStringTtndService, CommandType.Text,
                   String.Format(sqlqr));
        if (ds != null && ds.Tables.Count > 0)
            return ds.Tables[0];
        return null;
    }

    public static void S2_Update_TB_LuyKe(int service_id, int TBLuyKe)
    {
        SqlHelper.ExecuteNonQuery(CnnStrTtndService_new, CommandType.Text,
                    //String.Format("Select * from S2_TTND_Registered_Users where  Service_Type = " + Service_Type))
                    String.Format("Update Report_VNM_94x set THUE_BAO_LUY_KE = " + TBLuyKe + "where year(NGAY) = 2016 and month(NGAY) = 9 and day(NGAY) = 18 and Loai_Goi = " + service_id + ""));

    }

    public static DataTable S2_AllUsser_loop()
    {
        DataSet ds = SqlHelper.ExecuteDataset(CnnStrTtndService_new, CommandType.Text,
                    //String.Format("Select * from S2_TTND_Registered_Users where  Service_Type = " + Service_Type))
                    String.Format("select user_id,count(1),service_id from S2_TTND_Registered_Users where 1=1 and service_type in (1006) group by user_id,service_id having count(1) > 1"));
        if (ds != null && ds.Tables.Count > 0)
            return ds.Tables[0];
        return null;
    }
    public static DataTable S2_AllUsser_loop_user_info(int Service_id, string user_id)
    {
        DataSet ds = SqlHelper.ExecuteDataset(CnnStrTtndService_new, CommandType.Text,
                    //String.Format("Select * from S2_TTND_Registered_Users where  Service_Type = " + Service_Type))
                    String.Format("select * from S2_TTND_Registered_Users where user_id = '" + user_id + "' and Service_id = " + Service_id + ""));
        if (ds != null && ds.Tables.Count > 0)
            return ds.Tables[0];
        return null;
    }
    public static void S2_AllUsser_loop_delete(int id, string user_id)
    {
        SqlHelper.ExecuteNonQuery(CnnStrTtndService_new, CommandType.Text,
                    //String.Format("Select * from S2_TTND_Registered_Users where  Service_Type = " + Service_Type))
                    String.Format("delete from S2_TTND_Registered_Users where id = " + id + " and user_id = '" + user_id + "'"));

    }

    public static DataTable S2_AllUsser_old(int Service_Type)
    {
        DataSet ds = SqlHelper.ExecuteDataset(CnnStrTtndService_old, CommandType.Text,
                    //String.Format("Select * from S2_TTND_Registered_Users where  Service_Type = " + Service_Type))
                    String.Format("Select * from S2_TTND_Registered_Users where  user_id = '84928483503' "));
        if (ds != null && ds.Tables.Count > 0)
            return ds.Tables[0];
        return null;
    }
    public static int S2_UpdateUser_dbMoi(
         string User_ID
, string Request_ID
, int Service_ID
, string Short_Code
, string Command_Code
, string Reference_ID
, int PeriodLength
, int NoChargeLength
, int Service_Type
, int Charging_Count
, int FailedChargingTimes
, DateTime RegisteredTime
, DateTime ExpiredTime
, int Status
, string Registration_Channel
, DateTime CancelTime
, string Cancel_Channel
, int Flag
, int ChargedFee
, int CountTo_Cancel
, int Charging_Price
, int Charging_Status
                  )
    {
        return ConvertUtility.ToInt32(SqlHelper.ExecuteScalar(CnnStrTtndService_new
                                                            , "S2_TTND_Registered_Users_1_Insert",
        User_ID
, Request_ID
, Service_ID
, Short_Code
, Command_Code
, Reference_ID
, PeriodLength
, NoChargeLength
, Service_Type
, Charging_Count
, FailedChargingTimes
, RegisteredTime
, ExpiredTime
, Status
, Registration_Channel
, CancelTime
, Cancel_Channel
, Flag
, ChargedFee
, CountTo_Cancel
, Charging_Price
, Charging_Status
        ));
    }

    

    public static int S2_UpdateUser_ToLogConfirm(
      string MSISDN
    , string SERVICE_NAME
    , string SHORTCODE
    , int PRICE
    , string DESC
    , int VALIDITY
    , int FREETRIAL
    , string BASE_URL
    , string CALLBACK_URL
    , string TRANSACTION_ID
    , string CONFIRM
    , int TYPE
    , int SERVICE_ID    
    ,DateTime INSERT_DATE
    )
    {
        return ConvertUtility.ToInt32(SqlHelper.ExecuteScalar(AppEnv.ConnectionStringTtndService
                                                            , "TB_CP_VAS_Insert",
         MSISDN
    ,  SERVICE_NAME
    ,  SHORTCODE
    ,  PRICE
    ,  DESC
    ,  VALIDITY
    ,  FREETRIAL
    ,  BASE_URL
    ,  CALLBACK_URL
    ,  TRANSACTION_ID
    ,  CONFIRM
    ,  TYPE
    ,  SERVICE_ID
    , INSERT_DATE
        ));
    }

    public static void TB_CP_VAS_SMS_insert(
      string A_Number
    , string B_Number
    , DateTime Send_Time
    , string Msg_Content
    , int Service_ID
    , string Service_Name
    , int Type
   ,int Service_Type
    )
    {
         SqlHelper.ExecuteNonQuery(AppEnv.ConnectionStringTtndService
                                                            , "TB_CP_VAS_SMS_insert",
          A_Number
    ,  B_Number
    ,  Send_Time
    ,  Msg_Content
    ,  Service_ID
    ,  Service_Name
    , Type
    , Service_Type
        );
    }
    public static string GetMo(string register_syntax)
    {
        string mo;
        if (register_syntax.IndexOf("|")>-1)
        {
            string[] Items = register_syntax.Split('|');
            mo = Items[1];

        }
        else
        {
            mo = register_syntax;
        }
        return mo;
    }
    #endregion

    #region GPC
    public static DataTable S2_All_Service_GPC()
    {
        DataSet ds = SqlHelper.ExecuteDataset(CnnStrTtndService_new, CommandType.Text,
                    String.Format("select  * from dbo.S2_GPC_TTND_Jobs where JobExecutorEndpoint = 'http://101.99.16.163:9036/S2Jobs/JobGeneral.asmx' and id <> 66 and status = 6"));
        if (ds != null && ds.Tables.Count > 0)
            return ds.Tables[0];
        return null;
    }
    #endregion

    #region Test API
    public void Getdata()
    {
        var httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.36.25:9091/api.php");
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
            string json = "{\"CMD_CODE\":\"get_banner\"," +
                           "\"code_portal\":\"tts_livescore\"," +
                          "\"position_code\":\"TTS_LiVESCORE_TOP\"}";

            streamWriter.Write(json);
            streamWriter.Flush();
            streamWriter.Close();
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        string result;
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
            result = streamReader.ReadToEnd();
        }
        if (!string.IsNullOrEmpty(result))
        {
            AdsInfo _AdsInfo = new JavaScriptSerializer().Deserialize<AdsInfo>(result);


            foreach (var item in _AdsInfo.RESULT)
            {
                string name = item.name;
                string width = item.width;
                string height = item.height;
                foreach (var item_img in item.item)
                {
                    string id = item_img.id;
                    string url = item_img.url;
                    string urlImage = item_img.urlImage;
                }
            }

        }
    }
    public class AdsInfo
    {
        private string _CMD_CODE;
        public string CMD_CODE
        {
            get { return _CMD_CODE; }
            set { _CMD_CODE = value; }
        }

        private int _ERR_CODE;
        public int ERR_CODE
        {
            get { return _ERR_CODE; }
            set { _ERR_CODE = value; }
        }

        private string _ERR_MSG;
        public string ERR_MSG
        {
            get { return _ERR_MSG; }
            set { _ERR_MSG = value; }
        }

        private List<result> _RESULT;
        public List<result> RESULT
        {
            get { return _RESULT; }
            set { _RESULT = value; }
        }
    }
    public class result
    {
        public string name { get; set; }
        public string height { get; set; }
        public string width { get; set; }
        public string type { get; set; }
        public string code { get; set; }
        public List<ImagesInfo> item { get; set; }

    }
    public class ImagesInfo
    {
        public string id { get; set; }
        public string url { get; set; }
        public string urlImage { get; set; }
    }
    #endregion

    #region Check 1119 not in 949
    public static DataTable S2_All_Service_1119()
    {
        DataSet ds = SqlHelper.ExecuteDataset(CnnStrTtndService_new, CommandType.Text,
                    String.Format("select * from S2_Vnm_Services_check"));
        if (ds != null && ds.Tables.Count > 0)
            return ds.Tables[0];
        return null;
    }

    public static DataTable S2_All_Service_949()
    {
        DataSet ds = SqlHelper.ExecuteDataset(CnnStrTtndService_new, CommandType.Text,
                    String.Format("select * from S2_TTND_Subscription_Services"));
        if (ds != null && ds.Tables.Count > 0)
            return ds.Tables[0];
        return null;
    }
    public static void update_exist1119vs949(int service_id, string syntax)
    {
        SqlHelper.ExecuteNonQuery(CnnStrTtndService_new, CommandType.Text,
                    String.Format("Update S2_Vnm_Services_check set syntax = '" + syntax + "',In94x=1 where ServiceId = " + service_id + ""));

    }
    public static string Check_exist_keyword(string keywword, string syntax)
    {
        string return_string = "";
        if (syntax.Contains("|"))
        {
            string[] lines = syntax.Split('|');
            foreach (string line in lines)
            {
                if (keywword == line)
                {
                    return_string = return_string + line;
                }

            }
            return return_string;
        }
        else
        {
            if (keywword == syntax)
            {
                return_string = keywword;
            }
            return return_string;
        }

    }
    public static string NormalizeSyntax(string _message)
    {
        String strTmp = _message.Trim();
        strTmp = strTmp.Replace('/', ' ');
        strTmp = strTmp.Replace(',', ' ');
        strTmp = strTmp.Replace('.', ' ');
        strTmp = strTmp.Replace('<', ' ');
        strTmp = strTmp.Replace('>', ' ');
        strTmp = strTmp.Replace('\r', ' ');
        strTmp = strTmp.Replace('\n', ' ');

        String strResult = "";
        for (int i = 0; i < strTmp.Length; i++)
        {
            // char ch = strTmp.charAt(i);
            char ch = strTmp[i];
            if (ch == ' ')
            {
                for (int j = i; j < strTmp.Length; j++)
                {
                    //char ch2 = strTmp.charAt(j);
                    char ch2 = strTmp[j];
                    if (ch2 != ' ')
                    {
                        i = j;
                        strResult = strResult + ' ' + ch2;
                        break;
                    }
                }

            }
            else
            {
                strResult = strResult + ch;
            }
        }
        return strResult;
    }
    #endregion
}

