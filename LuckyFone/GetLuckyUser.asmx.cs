using System;
using System.Data;
using System.Web.Services;

namespace LuckyFone
{
    /// <summary>
    /// Summary description for GetLuckyUser
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class GetLuckyUser : WebService, IJobExecutorSoap
    {

        public GetLuckyUser()
        {

            //Uncomment the following line if using designed components 
            //InitializeComponent(); 
        }

        readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(GetLuckyUser));
        [WebMethod]
        public int Execute(int jobID)
        {
            try
            {
                DataTable dt = PublicController.LuckyfoneGetLuckyUser();
                if(dt != null && dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                    {
                        #region SEND MT

                        var mtInfo = new MTInfo();
                        var random = new Random();
                        mtInfo.User_ID = dr["User_Id"].ToString();
                        mtInfo.Service_ID = dr["Service_Id"].ToString();
                        mtInfo.Command_Code = dr["Command_Code"].ToString();
                        mtInfo.Message_Type = 0;
                        mtInfo.Request_ID = random.Next(100000000, 999999999).ToString();
                        mtInfo.Total_Message = 1;
                        mtInfo.Message_Index = 0;
                        mtInfo.IsMore = 0;
                        mtInfo.Content_Type = 0;

                        string msisdn = dr["True_User_Id"].ToString();
                        if(msisdn.Length > 10)
                        {
                            msisdn = msisdn.Substring(0, msisdn.Length - 2) + "xx";
                        }
                        else
                        {
                            msisdn = msisdn.Substring(0, msisdn.Length - 1) + "x";
                        }

                        mtInfo.Message = "Chuc mung thue bao " + msisdn + " da nhan duoc LOC may man dau nam cua 997 tri gia 200.000d. Soan: XS <ma tinh> gui 997 de tiep tuc nhan co hoi may man";

                        PublicController.SmsMtInsertNew(mtInfo);

                        PublicController.LuckyfoneMtInsert(dr["User_Id"].ToString(), mtInfo.Request_ID,
                                                    dr["Service_Id"].ToString(), dr["Command_Code"].ToString(),
                                                    mtInfo.Message, dr["Mobile_Operator"].ToString());

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                _log.Debug("Exception GetLuckyUser : " + ex);
            }

            return 1;
        }

    }
}
