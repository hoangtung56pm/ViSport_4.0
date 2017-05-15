#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\ZaloLotteryReciever.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8CE45543842D22155213649B25029A46E5B16521"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\ZaloLotteryReciever.cs"
using System;
using System.Web.Services;
using log4net;

/// <summary>
/// Summary description for ZaloLotteryReciever
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class ZaloLotteryReciever : System.Web.Services.WebService {

    public ZaloLotteryReciever () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    private static readonly ILog Log = LogManager.GetLogger(typeof(ZaloLotteryReciever));

    [WebMethod]
    public string PushLotteryResult(int companyID, string lotteryResult, int status)
    {
        try
        {
            Log.Info(" ");
            Log.Info(" ");
            Log.Info("*** Received lottery result from MrT");
            Log.Info("Company ID: " + companyID);
            Log.Info("Result: " + lotteryResult);
            Log.Info("status: " + status);
            Log.Info(" ");
            Log.Info(" ");

            if (status == 1)
            {
                //IsDelete = 1 : Xoa DuLieu o 2 Bang : Zalo_Lottery_Day va Zalo_Quere_Mt
                ZaloController.ZaloQuereAdd(companyID,lotteryResult,1,0);
            }
            else
            {

                #region KQ CHO

                //-- Type = 1 : 1. Nhận kết quả xổ số mới nhất theo tỉnh (8179,8279)
                //--      = 2 : 2. Nhận kết quả xổ số mới nhất theo miền (8379)
                //--      = 3 : 3. Đăng ký nhận kết quả xổ số nhiều ngày (8779)
                //--      = 4 : 4. Tường thuật trực tiếp kết quả xổ số (8579)

                //IsDelete = 0 : Xoa DuLieu o 1 Bang : Zalo_Quere_Mt (Chua xoa du lieu o bang : Zalo_Lottery_Day)
                ZaloController.ZaloQuereAdd(companyID, lotteryResult, 0, 4);

                #endregion

            }

            return "Received !";
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            return "Exception";
        }
    }
    
}


#line default
#line hidden
