using Microsoft.ApplicationBlocks.Data;
using SMSManager_API.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for SmsLtdkqtn
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SmsLtdkqtn : System.Web.Services.WebService
{

    public SmsLtdkqtn()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    log4net.ILog _log = log4net.LogManager.GetLogger(typeof(SmsLtdkqtn));
    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            DataTable dtLeague = SqlHelper.ExecuteDataset(Connttnd, CommandType.StoredProcedure, "API_TTS_Competitions").Tables[0];
            SmsMT_Delete();
            SmsMtLtd item = new SmsMtLtd();
            item.Mt1 = string.Empty;
            item.Mt2 = string.Empty;
            item.Mt3 = string.Empty;
            item.Mt4 = string.Empty;
            item.Mt5 = string.Empty;
            item.Mt6 = string.Empty;
            if (dtLeague != null && dtLeague.Rows.Count > 0)
            {
                item.Mt1 = DateTime.Now.AddDays(-1).ToString("dd/MM") + " ";
                foreach (DataRow drL in dtLeague.Rows)
                {
                    string id = drL["id"].ToString();
                    DataSet dsLeague = GetDataByCompetition(ConvertUtility.ToInt32(id));                    
                    DataTable dtMatch = dsLeague.Tables[2];
                    if (dtMatch != null && dtMatch.Rows.Count > 0)
                    {
                       
                        item.Mt1 = item.Mt1 + UnicodeUtility.UnicodeToKoDau(drL["title"].ToString());
                        foreach (DataRow drMatch in dtMatch.Rows)
                        {
                            #region UPDATE MATCH
                                item.Mt1 = item.Mt1 + "  " + "+" + " " + UnicodeUtility.UnicodeToKoDau(UnicodeUtility.UnicodeToKoDau(drMatch["home_name"].ToString())) + " " + drMatch["score_a"] + "-" + drMatch["score_b"] + " " + UnicodeUtility.UnicodeToKoDau(UnicodeUtility.UnicodeToKoDau(drMatch["away_name"].ToString()));
                            
                            #endregion
                        }
                        item.Mt1 = item.Mt1 + " ";
                    }
                }
                if(item.Mt1.Length>160){
                    item.Mt2 = item.Mt1.Substring(160, item.Mt1.Length-160);
                    item.Mt1 = item.Mt1.Substring(0,159);
                    
                    if (item.Mt2.Length > 160)
                    {
                        item.Mt3 = item.Mt2.Substring(160, item.Mt2.Length-160);
                        item.Mt2 = item.Mt2.Substring(0, 159);
                        
                        if (item.Mt3.Length > 160)
                        {
                            item.Mt4 = item.Mt3.Substring(160, item.Mt3.Length-160);
                            item.Mt3 = item.Mt3.Substring(0, 159);
                            
                            if (item.Mt4.Length > 160)
                            {
                                item.Mt5 = item.Mt4.Substring(160, item.Mt4.Length-160);
                                item.Mt4 = item.Mt4.Substring(0, 159);
                                
                                if (item.Mt5.Length > 160)
                                {
                                    item.Mt6 = item.Mt5.Substring(160, item.Mt5.Length-160);
                                    item.Mt5 = item.Mt5.Substring(0, 159);
                                    
                                    if (item.Mt6.Length > 160)
                                    {
                                        item.Mt6 = item.Mt1.Substring(0, 159);
                                        

                                    }
                                }
                            }
                        }
                    }
                }
                string a = item.Mt1 + item.Mt2 + item.Mt3 + item.Mt4 + item.Mt5 + item.Mt6;
                item.WapContent = string.Empty;
                item.Status = 1;
                item.ModifyUser = 1;

                item.ModifyDate = DateTime.Now;

                //INSERT dsg_SMS_MT_LTD
                //SmsMtInsert(item);
            }
        }
        catch (Exception ex)
        {
            _log.Error("***** SmsLtdkqtn Loi  : " + ex);
            return 0;
        }
        return 1;
    }

    #region Methods

    public static string Connttnd = AppEnv.GetConnectionString("localsql");
    public void SmsMtInsert(SmsMtLtd item)
    {
        SqlHelper.ExecuteNonQuery(Connttnd, "dsg_sms_mt_ltdkqtn_insert",
            //item.CompetitionId,
            item.Mt1.Trim(),
            item.Mt2.Trim(),
            item.Mt3.Trim(),
            item.Mt4.Trim(),
            item.Mt5.Trim(),
            item.Mt6.Trim(),
            item.WapContent,
            item.Status,
            item.ModifyUser);
    }
    public void SmsMT_Delete()
    {
        SqlHelper.ExecuteNonQuery(Connttnd, "dsg_sms_mt_ltdkqtn_delete");
    }
    public DataSet GetDataByCompetition(int competitionId)
    {
        try
        {
            DataSet ds = SqlHelper.ExecuteDataset(Connttnd, "API_SMS_LTD_KQTN", competitionId);
            if (ds != null)
            {
                return ds;
            }
        }
        catch (Exception)
        {
            return null;
        }

        return null;
    }
    public class SmsMtLtd
    {
        public int Id { get; set; }
        //public int CompetitionId { get; set; }
        public string Mt1 { get; set; }
        public string Mt2 { get; set; }
        public string Mt3 { get; set; }
        public string Mt4 { get; set; }
        public string Mt5 { get; set; }
        public string Mt6 { get; set; }
        public string WapContent { get; set; }
        public int Status { get; set; }
        public int ModifyUser { get; set; }
        public DateTime ModifyDate { get; set; }
    }
    #endregion

}
