using SMSManager_API.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using WS_Music.Library;

/// <summary>
/// Summary description for Hospital_SendDailyMT
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Hospital_SendDailyMT : System.Web.Services.WebService, IJobExecutorSoap
{
    
    public int Execute(int jobId)
    {
        DataTable dt = DBController.GetBySql("Select * from Hospital_Service where Status = 1 order by ID desc");
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow row in dt.Rows)
            {
                DataTable dtUser = DBController.Hospital_User_GetUser_SendDailyMT(ConvertUtility.ToInt32(row["ID"]));
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    foreach (DataRow item in dtUser.Rows)
                    {
                        //đẩy vào Mt queue
                        string mt = row["DailyMT"].ToString();                       
                        string User_ID = item["User_ID"].ToString();
                        string Command_Code = item["Command_Code"].ToString();
                        DBController.Send(User_ID, mt, "8779", Command_Code, ((int)Constant.MessageType.NoCharge).ToString(), DateTime.Now.Ticks.ToString(), "1", "1", "0", ((int)Constant.ContentType.Text).ToString());
                    }
                }
            }
        }
        return 1;
    }    
}
