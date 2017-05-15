using System;
using System.Web.UI;
using AppboxApi.Library;
using AppboxApi.Library.Entity;
using AppboxApi.Library.Utilities;
using log4net;

namespace AppboxApi
{
    public partial class Subrequest : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ILog logger = LogManager.GetLogger("Subrequest");

            try
            {
                string userName = Request.QueryString["username"].Trim();
                string passWord = Request.QueryString["password"].Trim();
                string serviceid = Request.QueryString["serviceid"].Trim();
                string msisdn = Request.QueryString["msisdn"].Trim();
                string substime = Request.QueryString["substime"].Trim();
                string param = Request.QueryString["params"].Trim();
                string mo = Request.QueryString["mo"].Trim();

                logger.Debug(" ");
                logger.Debug(" ");
                logger.Debug("----- VMS API CALL Subrequest ----- :" + "UserName : " + userName + " |Pass : " + passWord +
                                                " |ServiceId : " + serviceid + " |Msisdn : " + msisdn + " |Substime : " + substime +
                                                " |Param : " + param + " |Mo: " + mo);
                logger.Debug(" ");
                logger.Debug(" ");

                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(passWord))
                {

                    int val = ConvertUtility.ToInt32(param);
                    bool isTrue = false;

                    if (val == 0)
                    {
                        #region Subscribe

                        var item = new VmsAppboxRegisteredUser();
                        item.ChargingCount = 0;
                        item.CommandCode = "DK";

                        if (ConvertUtility.ToInt32(serviceid) == 26)//GOI NGAY
                            item.ExpiredTime = DateTime.Now.AddDays(1);
                        else if (ConvertUtility.ToInt32(serviceid) == 27)//GOI TUAN
                            item.ExpiredTime = DateTime.Now.AddDays(7);

                        item.UserId = msisdn;
                        item.FailedChargingTimes = 0;
                        item.RegisteredTime = DateTime.Now;
                        item.RegistrationChannel = "SMS";
                        item.RequestId = SecurityMethod.RandomStringNumber(9);
                        item.ServiceId = serviceid;
                        item.Status = 1;
                        item.Password = SecurityMethod.RandomStringNumber(6);
                        item.PartnerId = 1; //VMG

                        if (ApiController.ApiVmsAppboxRegisteredUsersAdd(item))
                        {
                            isTrue = true;

                            #region MT Tra MatKhau
                            ApiController.MtApi(msisdn, "Mat khau", "(ND)Moi Quy khach truy cap http://appbox.vn/ de tai cac game hap dan cua dich vu Appbox. Mat khau de su dung dich vu cua Quy khach la: " + item.Password + ". Chi tiet: 19001255", "0", 0, 0);
                            #endregion
                        }

                        #endregion
                    }
                    else if (val == 1)
                    {
                        #region Unsubscribe

                        if (ApiController.ApiVmsAppboxRegisteredUsersUnScribe(msisdn, "SMS"))
                        {
                            isTrue = true;
                        }

                        #endregion
                    }
                    else if (val == 2)
                    {
                        #region Pedding

                        #endregion
                    }
                    else if (val == 3)
                    {
                        #region Renewal Successed

                        //TYPE = 2 : GOI NGAY
                        //TYPE = 3 : GOI TUAN

                        VmsAppboxBillingLog item = new VmsAppboxBillingLog();

                        int type = 0;
                        if (ConvertUtility.ToInt32(serviceid) == 26) //GOI NGAY
                        {
                            type = 2;
                            item.Price = 2000;
                            item.Type = type;
                        }
                        else if (ConvertUtility.ToInt32(serviceid) == 27) //GOI TUAN
                        {
                            type = 3;
                            item.Price = 10000;
                            item.Type = type;
                        }

                        if (ApiController.ApiVmsAppboxRegisteredUsersRenewal(msisdn, type))
                        {
                            isTrue = true;
                        }

                        #region BILLING LOG

                        item.CpRequestId = "0";
                        item.Mobile = msisdn;
                        item.ChargeType = "mobile";
                        item.ResponseCode = 0;
                        item.Description = mo;

                        ApiController.ApiVmsAppboxBillingLog(item);

                        #endregion


                        #endregion
                    }
                    else if (val == 4)
                    {
                        #region Change MSISDN

                        #endregion
                    }

                    #region LOG ALL

                    var itemLog = new VmsReceiverRegisterLog();
                    itemLog.UserName = userName;
                    itemLog.Password = passWord;
                    itemLog.ServiceId = serviceid;
                    itemLog.Msisdn = msisdn;
                    itemLog.SubsTime = substime;
                    itemLog.Params = ConvertUtility.ToInt32(param);
                    itemLog.Mo = mo;

                    ApiController.ApiVmsReceiverRegisterLogInsert(itemLog);

                    #endregion

                    if (isTrue)
                        Response.StatusCode = 200;
                    else
                        Response.StatusCode = 204;
                }
            }
            catch (Exception ex)
            {
                logger.Debug(" ");
                logger.Debug(" ");
                logger.Debug("----- VMS API CALL Subrequest ERROR ----- :" + ex);
                logger.Debug(" ");
                logger.Debug(" ");
            }

        }
    }
}