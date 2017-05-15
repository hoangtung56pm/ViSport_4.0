#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\LuckyfoneGetMo.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B56CA11C2C83BD7C3C3E478FE02C23586F2A992A"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\LuckyfoneGetMo.cs"
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using SMSManager_API.Library.Utilities;

/// <summary>
/// Summary description for LuckyfoneGetMo
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class LuckyfoneGetMo : System.Web.Services.WebService, IJobExecutorSoap
{

    public LuckyfoneGetMo () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(LuckyfoneGetMo));

    [WebMethod]
    public int Execute(int jobID)
    {

        DataTable dt = ViSport_S2_Registered_UsersController.LuckyfoneGetMo();
        if (dt != null && dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                var item = new MoEntity997();
                item.CommandCode = dr["COMMAND_CODE"].ToString();
                item.Info = dr["INFO"].ToString();
                item.MobileOperator = dr["MOBILE_OPERATOR"].ToString();
                item.RequestID = dr["REQUEST_ID"].ToString();
                item.Responded = ConvertUtility.ToInt32(dr["RESPONDED"].ToString());
                item.ServiceID = dr["SERVICE_ID"].ToString();
                item.Timestamp = ConvertUtility.ToDateTime(dr["TIMESTAMP"].ToString());
                item.UserID = dr["USER_ID"].ToString();

                try
                {
                    ViSport_S2_Registered_UsersController.LuckyfoneMoInsert(item);
                }
                catch (Exception)
                {
                    _log.Debug("********** LUCKFONE LOG GETMO ERROR **********");
                    _log.Debug("userId : " + item.UserID);
                    _log.Debug("ServiceId : " + item.ServiceID);
                    _log.Debug("commandCode : " + item.CommandCode);
                    _log.Debug("mobileOperator : " + item.MobileOperator);
                    _log.Debug("submitDate : " + item.Timestamp);
                    _log.Debug(" ");
                    _log.Debug(" ");
                }

                _log.Debug("********** LUCKFONE LOG GETMO **********");
                _log.Debug("userId : " + item.UserID);
                _log.Debug("ServiceId : " + item.ServiceID);
                _log.Debug("commandCode : " + item.CommandCode);
                _log.Debug("mobileOperator : " + item.MobileOperator);
                _log.Debug("submitDate : " + item.Timestamp);
                _log.Debug(" ");
                _log.Debug(" ");

            }
        }

        return 1;
    }
    
}


#line default
#line hidden
