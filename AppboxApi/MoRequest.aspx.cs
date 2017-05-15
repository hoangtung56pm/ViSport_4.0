using System;
using System.Data;
using System.Web.UI;
using AppboxApi.Library;
using AppboxApi.Library.Entity;
using AppboxApi.Library.Utilities;
using log4net;

namespace AppboxApi
{
    public partial class MoRequest : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ILog logger = LogManager.GetLogger("MoRequest");

            try
            {

                string src = Request.QueryString["src"].Trim(); //MSISDN
                string dest = Request.QueryString["dest"].Trim(); //Short code (e.g. “8008”)
                string moseq = Request.QueryString["moseq"].Trim();
                string cmdcode = Request.QueryString["cmdcode"].Trim();
                string msgbody = Request.QueryString["msgbody"].Trim();
                string userName = Request.QueryString["username"].Trim();
                string passWord = Request.QueryString["password"].Trim();

                logger.Debug(" ");
                logger.Debug(" ");
                logger.Debug("----- VMS API CALL MoRequest ----- :" + "UserName : " + userName + " |Pass : " + passWord +
                                                " |src : " + src + " |dest : " + dest + " |moseq : " + moseq +
                                                " |cmdcode : " + cmdcode + " |msgbody: " + msgbody);
                logger.Debug(" ");
                logger.Debug(" ");

                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(passWord))
                {
                    cmdcode = cmdcode.ToUpper();

                    var item = new VmsAppboxMoLog();
                    item.UserId = src;
                    item.ShortCode = dest;
                    item.Moseq = moseq;
                    item.CmdCode = cmdcode;
                    item.MsgBody = msgbody;
                    item.UserName = userName;
                    item.Password = passWord;

                    bool isTrue = ApiController.ApiVmsAppboxMoLog(item);

                    if (isTrue)
                    {
                        Response.StatusCode = 200; //RESPONSE StatusCode = 200 THINKNET

                        #region XU LY GUI MT PHAN HOI

                        var mt = new VmsAppboxMtLog();
                        mt.UserId = item.UserId; //dest

                        string title = string.Empty;
                        string body = string.Empty;

                        if (cmdcode == "HD")
                        {
                            #region HUONG DAN

                            title = "Huong dan";
                            //body = "Chao mung Quy khach den voi su tro giup cua dich vu APPBOX cua MobiFone. " +
                            //       "De dang ky goi ngay soan: DK AG, de dang ky goi tuan soan: DK AG7 gui 9210. " +
                            //       "De kiem tra goi cuoc dang su dung soan KT gui 9210. De biet gia cuoc soan GIA gui 9210. " +
                            //       "De huy soan HUY Tengoi gui 9210. " +
                            //       "De su dung dich vu Appbox, Quy khach truy cap dia chi http://appbox.vn. " +
                            //       "Lien he ho tro 19001255. Tran trong cam on!";

                            body = "Chao mung Quy khach den voi su tro giup cua dich vu APPBOX cua MobiFone. " +
                                   "De dang ky goi ngay soan: DK AG, de dang ky goi tuan soan: DK AG7 gui 9210. " +
                                   "De kiem tra goi cuoc dang su dung soan KT gui 9210. " +
                                   "De biet gia cuoc soan GIA gui 9210. " +
                                   "Quen mat khau, soan MK gui 9210. " +
                                   "De huy soan HUY Tengoi gui 9210. " +
                                   "De su dung dich vu Appbox, Quy khach truy cap dia chi http://appbox.vn. " +
                                   "Lien he ho tro 19001255. Tran trong cam on!";

                            #endregion
                        }
                        else if (cmdcode == "KT")
                        {
                            #region KIEM TRA DICH VU

                            DataTable dt = ApiController.ApiVmsAppboxRegisteredUsersInfo(src);
                            title = "Kiem tra";

                            if (dt.Rows.Count > 0)
                            {
                                DataRow dr = dt.Rows[0];
                                string tenGoi = "Goi ngay";
                                string hanSd = ConvertUtility.ToDateTime(dr["ExpiredTime"]).ToString("dd/MM/yyyy");
                                string giaCuoc = "2.000/ngay";

                                if (dr["Service_ID"].ToString() == "27")
                                {
                                    tenGoi = "Goi Tuan";
                                    giaCuoc = "1.0000/tuan";
                                }

                                body = "Quy khach hien dang su dung goi cuoc " + tenGoi + " thuoc dich vu Appbox cua MobiFone. " +
                                       "Thoi han su dung den " + hanSd + ". " +
                                       "Truy cap http://appbox.vn (Mien phi cuoc DATA) de su dung dich vu. " +
                                       "Cuoc dich vu: " + giaCuoc + ". " +
                                       "Lien he ho tro 19001255. Tran trong cam on!";
                            }
                            else
                            {
                                body = "Quy khach chua dang ky dich vu Appbox cua MobiFone. De dang ky dich vu,Quy khach vui long soan: DK Tengoi gui 9210 (Tengoi: AG – goi ngay, AG7 - goi tuan). " +
                                       "De xem huong dan soan HD gui 9210. " +
                                       "Chi tiet truy cap http://appbox.vn hoac lien he ho tro 19001255.Tran trong cam on!";
                            }

                            #endregion
                        }
                        else if (cmdcode == "MK")
                        {
                            #region LAY LAI MATKHAU

                            DataTable dt = ApiController.ApiVmsAppboxRegisteredUsersInfo(item.UserId);
                            if (dt.Rows.Count > 0)
                            {
                                title = "Mat khau";
                                body = "Mat khau de su dung dich vu Appbox cua Quy khach la: " + dt.Rows[0]["Password"] + ". Truy cap http://appbox.vn de su dung dich vu. Lien he ho tro 19001255. Tran trong cam on!";
                            }
                            else
                            {
                                title = "Mat khau";
                                body = "Quy khach chua dang ky dich vu Appbox cua MobiFone. De dang ky dich vu, Quy khach vui long soan: DK Tengoi gui 9210 (Tengoi: AG - goi ngay, AG7 - goi tuan). De xem huong dan soan HD gui 9210. Chi tiet truy cap http://appbox.vn hoac lien he ho tro 19001255. Tran trong cam on!";
                            }

                            #endregion
                        }
                        else if (cmdcode == "GIA")
                        {
                            #region GIA

                            title = "Gia cuoc";
                            body = "Gia goi cuoc dich vu Appbox cua MobiFone: AG: 2.000d/ngay (mien phi cho thue bao dang ky lan dau), " +
                                   "AG7: 10.000d/tuan (mien phi cho thue bao dang ky lan dau). " +
                                   "Chi tiet truy cap http://appbox.vn hoac lien he 19001255." +
                                   "Tran trong cam on!";

                            #endregion
                        }

                        //mt.MsgTitle = Server.UrlEncode(title);
                        //mt.MsgBody = Server.UrlEncode(body);

                        mt.MsgTitle = title;
                        mt.MsgBody = body;


                        mt.MoSeq = item.Moseq;

                        //* 0 = the MO message is not charged 
                        //* 1 = the MO message is charged 
                        mt.ProcResult = 0;

                        mt.Price = 0;
                        ApiController.MtApi(mt.UserId, mt.MsgTitle, "(ND)" + mt.MsgBody, mt.MoSeq, mt.ProcResult, mt.Price);


                        #endregion
                    }
                    else
                    {
                        Response.StatusCode = 204;
                    }
                }

            }
            catch (Exception ex)
            {
                logger.Debug(" ");
                logger.Debug(" ");
                logger.Debug("----- VMS API CALL MoRequest ERROR ----- :" + ex);
                logger.Debug(" ");
                logger.Debug(" ");
            }

        }
    }
}