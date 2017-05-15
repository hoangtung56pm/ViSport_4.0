using Microsoft.ApplicationBlocks.Data;
using SentMT;
using SMSManager_API.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using WS_Music.Library;

/// <summary>
/// Summary description for Hospital
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Hospital : System.Web.Services.WebService
{

    log4net.ILog log = log4net.LogManager.GetLogger("File");
    [WebMethod]
    public string WSProcessMoHospital(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        return ExcecuteRequestMoHospital(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }
    #region Methods Process Mo
    private string ExcecuteRequestMoHospital(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        //string responseValue = "1";
        string returnValue = "0";

        //Message = Message.ToUpper();
        //string subcode = "";
        string mo = Normalize(Message).ToUpper();
        Command_Code = Command_Code.ToUpper();
        User_ID = GetNormalPhonenumber(User_ID);
        Service_Info matchService = null;
        string subcode = "";
        string mt = "";
        int msgType = (int)Constant.MessageType.NoCharge;
        if (Message.Trim().Length > Command_Code.Trim().Length)
        {
            subcode = Message.ToUpper().Substring(Command_Code.Length).Replace(" ", "");
        }
        try
        {
            //log.Debug(" ");
            //log.Debug(" ");
            //log.Debug("-------------------- Hospital Service mo  -------------------------");
            //log.Debug("User_ID: " + User_ID);
            //log.Debug("Service_ID: " + Service_ID);
            //log.Debug("Command_Code: " + Command_Code);
            //log.Debug("Message: " + Message.ToUpper());
            //log.Debug("Request_ID: " + Request_ID);
            //log.Debug(" ");
            //log.Debug(" ");

            #region Log MO Message Into Database (SMS_MO_Log)

            var moInfo = new SMS_MOInfo();

            moInfo.User_ID = User_ID;
            moInfo.Service_ID = Service_ID;
            moInfo.Command_Code = Command_Code;
            moInfo.Message = Message;
            moInfo.Request_ID = Request_ID;
            moInfo.Operator = DBController.GetTelco(User_ID);
            InsertMo(moInfo);

            #endregion
            #region Process
            List<Service_Info> listService = Services_ListAll();
            foreach (Service_Info s2Service in listService)
            {
                if (IsRightSyntax(s2Service.Service_Code, Command_Code))
                {
                    matchService = s2Service;
                    break;
                }

            }

            if (matchService == null) // => Sai cú pháp
            {
                mt = "Tin nhan cua quy khach sai cu phap. Tran trong cam on";
                //gửi MT

            }
            else
            {
                // xử lý nó thôi
                // check subcode phải dạng 010517 or 01052017
                int ck_ngay, ck_thang, ck_nam;
                int ngay = ck_ngay = DateTime.Now.Day;
                int thang = ck_thang = DateTime.Now.Month;
                int nam = ck_nam = DateTime.Now.Year;
                if (subcode.Length == 6 || subcode.Length == 8)
                {
                    ngay = ConvertUtility.ToInt32(subcode.Substring(0, 2));
                    thang = ConvertUtility.ToInt32(subcode.Substring(2, 2));
                    nam = subcode.Length == 6 ? ConvertUtility.ToInt32("20" + subcode.Substring(4, 2)) : ConvertUtility.ToInt32(subcode.Substring(4, 4));
                    DateTime AlertDate = new DateTime(nam, thang, ngay);
                    DateTime ck_AlertDate = new DateTime(ck_nam, ck_thang, ck_ngay);
                    if (AlertDate >= ck_AlertDate)
                    {
                       
                        try
                        {
                            Hospital_User_insert(User_ID, Command_Code, matchService.ID, matchService.Service_Type, AlertDate);
                            Hospital_UserLog_insert(User_ID, Command_Code, matchService.ID, matchService.Service_Type, AlertDate);
                            mt = matchService.WelcomeMT;
                            msgType = (int)Constant.MessageType.Charge;
                            returnValue = "1";
                        }
                        catch(Exception ex)
                        {
                            mt = "He thong dang ban. De duoc ho tro xin lien he 19001255. Cam on quy khach.";
                        }
                    }
                    else
                    {
                        //ngay hẹn phải lớn hơn hoặc bằng ngày hiện tại
                        mt = "Ngay hen tai kham phai sau ngay hien tai, vui long kiem tra lai. DTHT 19001255";
                    }
                }
                else
                {
                    //sai định dạng thời gian
                    mt = "Ban da nhan sai dinh dang ngay. DTHT 19001255";
                }

            }
            #endregion

        }
        catch (Exception ex)
        {
            log.Debug("--------------- Hospital Service mo ----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
            mt = "He thong dang ban. De duoc ho tro xin lien he 19001255. Cam on quy khach.";
        }
        //send MT to MT queue
        DBController.Send(User_ID, mt, "8779", Command_Code, msgType.ToString() , DateTime.Now.Ticks.ToString(), "1", "1", "0", ((int)Constant.ContentType.Text).ToString());

        return returnValue;
    }
    #endregion    
    
    #region Method
    public class Service_Info
    {
        public int ID { get; set; }
        public string Service_Code { get; set; }
        public string Service_Name { get; set; }
        public string TimesMT { get; set; }
        public string TimesAlert { get; set; }
        public string WelcomeMT { get; set; }
        public string DailyMT { get; set; }
        public string WaitingMT { get; set; }
        public string CronTime { get; set; }
        public int Status { get; set; }
        public int Service_Type { get; set; }
        public int TimelineFirst { get; set; }
        public int TimelineSecond { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int ModifiedBy { get; set; }


    }

    public static bool IsRightSyntax(string template, string input)
    {
        string[] templateItems = template.Split('|');

        foreach (string item in templateItems)
        {
            string keyword = item.Trim();
            if (keyword != String.Empty && keyword.ToUpper() == input.ToUpper()) return true;
        }

        return false;
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
    public static List<Service_Info> Service_Info_GetBySql(string commandText)
    {
        List<Service_Info> list = new List<Service_Info>();

        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionStringVClip);
        SqlCommand sqlCommand = new SqlCommand(commandText, dbConn);
        try
        {
            dbConn.Open();
            SqlDataReader dataReader = sqlCommand.ExecuteReader();

            list = PopulateObjectsFromReader(dataReader);
            dataReader.Close();
        }
        finally
        {
            dbConn.Close();
            sqlCommand.Dispose();
        }

        return list;
    }
    private static List<Service_Info> PopulateObjectsFromReader(IDataReader dataReader)
    {
        List<Service_Info> list = new List<Service_Info>();

        while (dataReader.Read())
        {
            Service_Info service = new Service_Info();
            service.ID = ConvertUtility.ToInt32(dataReader["ID"]);
            service.Service_Name = ConvertUtility.ToString(dataReader["Service_Name"]);
            service.Service_Code = ConvertUtility.ToString(dataReader["Service_Code"]);
            service.Service_Type = ConvertUtility.ToInt32(dataReader["Service_Type"]);
            service.TimesMT = ConvertUtility.ToString(dataReader["TimesMT"]);
            service.TimesAlert = ConvertUtility.ToString(dataReader["TimesAlert"]);
            service.WelcomeMT = ConvertUtility.ToString(dataReader["WelcomeMT"]);
            service.DailyMT = ConvertUtility.ToString(dataReader["DailyMT"]);
            service.WaitingMT = ConvertUtility.ToString(dataReader["WaitingMT"]);
            service.CronTime = ConvertUtility.ToString(dataReader["CronTime"]);
            service.Status = ConvertUtility.ToInt32(dataReader["Status"]);
            service.TimelineFirst = ConvertUtility.ToInt32(dataReader["TimelineFirst"]);
            service.TimelineSecond = ConvertUtility.ToInt32(dataReader["TimelineSecond"]);
            service.CreatedDate = ConvertUtility.ToDateTime(dataReader["CreatedDate"]);
            service.CreatedBy = ConvertUtility.ToInt32(dataReader["CreatedBy"]);
            service.ModifiedDate = ConvertUtility.ToDateTime(dataReader["ModifiedDate"]);
            service.ModifiedBy = ConvertUtility.ToInt32(dataReader["ModifiedBy"]);
            list.Add(service);
        }

        return list;
    }
    public static List<Service_Info> Services_ListAll()
    {
        string sql = String.Format("Select * from [Hospital_Service] where  Status={0} order by ID desc", 1);
        return Service_Info_GetBySql(sql);
    }
    public static string Normalize(string _message)
    {
        String strTmp = _message.Trim();
        strTmp = strTmp.Replace('/', ' ');
        strTmp = strTmp.Replace(',', ' ');
        strTmp = strTmp.Replace('<', ' ');
        strTmp = strTmp.Replace('>', ' ');
        strTmp = strTmp.Replace('[', ' ');
        strTmp = strTmp.Replace(']', ' ');
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
    public static string GetNormalPhonenumber(string userId)
    {
        string retVal = userId;

        if (retVal.StartsWith("+"))
            retVal = retVal.Replace("+", string.Empty);
        if (retVal.StartsWith("0"))
        {
            retVal = "84" + retVal.Remove(0, 1);
        }

        return retVal;
    }
    public static void Hospital_User_insert(string UserID,string Command_Code, int Service_ID, int Service_Type, DateTime AlertDate)
    {
        SqlHelper.ExecuteNonQuery(AppEnv.ConnectionStringVClip, "Hospital_User_insert",
                             UserID, Command_Code, Service_ID, Service_Type, AlertDate

                            );
    }
    //public static void Hospital_UserLog_insert(string UserID,string Command_Code, int Service_ID, int Service_Type, DateTime AlertDate)
    //{
    //    SqlHelper.ExecuteNonQuery(AppEnv.ConnectionStringVClip, "Hospital_UserLog_insert",
    //                         UserID, Command_Code, Service_ID, Service_Type, AlertDate

    //                        );
    //}
    public static void Hospital_UserLog_insert(string UserID, string Command_Code, int Service_ID, int Service_Type, DateTime AlertDate)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionStringVClip);
        SqlCommand dbCmd = new SqlCommand("Hospital_UserLog_insert", dbConn);
        dbCmd.CommandType = CommandType.StoredProcedure;
        dbCmd.Parameters.Add("@UserID", UserID);
        dbCmd.Parameters.Add("@Command_Code", Command_Code);
        dbCmd.Parameters.Add("@Service_ID", Service_ID);
        dbCmd.Parameters.Add("@Service_Type", Service_Type);
        dbCmd.Parameters.Add("@AlertDate", AlertDate);
        try
        {
            dbConn.Open();
            dbCmd.ExecuteNonQuery();          
        }
        finally
        {
            dbConn.Close();
        }
    }
    public static void InsertMo(SMS_MOInfo entity)
    {
        SqlConnection dbConn = new SqlConnection(AppEnv.ConnectionStringVClip);
        SqlCommand dbCmd = new SqlCommand("Hospital_SMS_MO_Insert", dbConn);
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
        }
        finally
        {
            dbConn.Close();
        }
    }    
    #endregion

}
