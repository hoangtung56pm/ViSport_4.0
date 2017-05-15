using ChargingGateway;
using SentMT;
using SMSManager_API.Library.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using WS_Music.Library;

/// <summary>
/// Summary description for Euro
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Euro : System.Web.Services.WebService {

    public Euro () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    log4net.ILog log = log4net.LogManager.GetLogger("File");
    #region Webservice
    [WebMethod]
    public string WSProcessMoSportGame(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        //return "1";
        return ExcecuteRequestMoSportGame(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }

    [WebMethod]
    public string WSProcessMoEuro_Wap(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        //return "1";
        return ExcecuteRequestMoEuro_Wap(User_ID, Service_ID, Command_Code, Message, Request_ID);
    }
    #endregion

    #region Method
    private string ExcecuteRequestMoSportGame(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        string responseValue = "1";
        int returnValue = 0;

        Message = Message.ToUpper();
        string subcode = "";
        if (Message.Trim().Length > Command_Code.Trim().Length)
        {
            subcode = Message.ToUpper().Substring(Command_Code.Length).Replace(" ", "");
        }

        Command_Code = Command_Code.ToUpper();
        Message = Message.ToUpper();

        try
        {
            log.Debug(" ");
            log.Debug(" ");
            log.Debug("-------------------- CHAY CUNG EURO 2016 -------------------------");
            log.Debug("User_ID: " + User_ID);
            log.Debug("Service_ID: " + Service_ID);
            log.Debug("Command_Code: " + Command_Code);
            log.Debug("Message: " + Message.ToUpper());
            log.Debug("Request_ID: " + Request_ID);
            log.Debug(" ");
            log.Debug(" ");

            #region Log MO Message Into Database (SMS_MO_Log)


            //if (AppEnv.GetSetting("TestFlag") == "0")
            //{
            var moInfo = new SMS_MOInfo();
            moInfo.User_ID = User_ID;
            moInfo.Service_ID = Service_ID;
            moInfo.Command_Code = Command_Code;
            moInfo.Message = Message;
            moInfo.Request_ID = Request_ID;
            moInfo.Operator = GetTelco(User_ID);
            SMS_MODB.InsertSportGameHeroMo(moInfo);
            //}


            #endregion

            string messageReturn = "";

            if (Command_Code == "EU" && subcode == "") //DK DICH VU TRIEU_PHU_BONG_DA
            {
                #region DK DICH VU

                var entity = new ViSport_S2_Registered_UsersInfo();
                entity.User_ID = User_ID;
                entity.Request_ID = Request_ID;
                entity.Service_ID = Service_ID;
                entity.Command_Code = Command_Code;
                entity.Service_Type = 1;
                entity.Charging_Count = 0;
                entity.FailedChargingTimes = 0;
                entity.RegisteredTime = DateTime.Now;
                entity.ExpiredTime = DateTime.Now.AddDays(1);
                entity.Registration_Channel = "SMS";
                entity.Status = 1;
                entity.Operator = GetTelco(User_ID);
                entity.Point = 2;

                string passWord = RandomActiveCode.RandomStringNumber(6);
                entity.Password = passWord;

                DataTable value = ViSport_S2_Registered_UsersController.InsertEuroRegisterUser(entity);

                if (value.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
                {

                    #region DK DV LAN DAU TIEN ==> KM 5 MDT

                    if (AppEnv.GetSetting("CTKM_Flag") == "1")
                    {
                        #region Sinh 5 mã dự thưởng khi đăng ký lần đầu
                        for (int i = 1; i <= 5; i++)
                        {
                            string code = RandomActiveCode.Generate(8);
                            ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(User_ID, code);
                        }
                        #endregion
                    }
                    messageReturn = AppEnv.GetSetting("AnhTaiBongDa_DangKyThanhCong");
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1 
                    
                    DataTable dtQuestion = ViSport_S2_Registered_UsersController.GetQuestionInfoEuro();
                    if (dtQuestion != null && dtQuestion.Rows.Count > 0)
                    {
                        messageReturn = dtQuestion.Rows[0]["Question"].ToString();

                        int questionId = ConvertUtility.ToInt32(dtQuestion.Rows[0]["Id"].ToString());
                        string answer = dtQuestion.Rows[0]["Answer"].ToString();

                        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 1); //SEND MT LAN 2 : GUI CAU HOI DAU TIEN
                        ViSport_S2_Registered_UsersController.InsertSportGameHeroAnswerLog(User_ID, questionId, messageReturn, answer, DateTime.Now, 0); // LUU LOG Question
                    }
                    #endregion

                }
                else if (value.Rows[0]["RETURN_ID"].ToString() == "2")
                {

                    //Check Đăng ký xong hủy trong ngày đầu tiên hay không
                    #region DA DK DV ROI XONG HUY DK LAI ==> TRA MT WELCOME                   
                       
                    DataTable dtCheck = ViSport_S2_Registered_UsersController.Euro_Check_Regiter_Cancel_Today(User_ID);
                    if (dtCheck != null && dtCheck.Rows.Count > 0)
                    {
                        //Đăng ký lại trong ngày đầu tiên
                        messageReturn = AppEnv.GetSetting("AnhTaiBongDa_DangKyThanhCongKM");
                        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1
                    }
                    else
                    {
                        //Đăng ký lại ngày thứ 2 trở đi 
                        string val_charge_return = ChuyenGiaBongDaCharged(User_ID, "5000",Request_ID);//Charge khi đăng ký lại ngày thứ 2 trở đi

                        if (val_charge_return == "1")
                        {
                            #region Charged THANHCONG ==> Trả MT

                            #region Sinh 5 mã dự thưởng khi đăng ký lại
                            for (int i = 1; i <= 5; i++)
                            {
                                string code = RandomActiveCode.Generate(8);
                                ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(User_ID, code);
                            }
                            #endregion

                            messageReturn = AppEnv.GetSetting("AnhTaiBongDa_DangKyThanhCongNotKM");
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi DUNG
                            DataTable dtQuestion = ViSport_S2_Registered_UsersController.GetQuestionInfoEuro();
                            if (dtQuestion != null && dtQuestion.Rows.Count > 0)
                            {
                                messageReturn = dtQuestion.Rows[0]["Question"].ToString();

                                int questionId = ConvertUtility.ToInt32(dtQuestion.Rows[0]["Id"].ToString());
                                string answer = dtQuestion.Rows[0]["Answer"].ToString();

                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 1); //SEND MT LAN 2 : GUI CAU HOI DAU TIEN
                                ViSport_S2_Registered_UsersController.InsertSportGameHeroAnswerLog(User_ID, questionId, messageReturn, answer, DateTime.Now, 0); // LUU LOG Question
                            }
                            

                            #endregion
                        }
                        else if (val_charge_return == "Result:12,Detail:Not enough money.")
                        {
                            #region Charged THATBAI ==> GUI ALERT THONG BAO NAP TIEN

                            messageReturn = "Thue bao khong du tien. Vui long nap tien de tiep tuc choi !";
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                            #endregion
                        }
                        else
                        {
                            #region Charged THATBAI ==> LOI SYSTEM

                            messageReturn = "He thong dang ban. Vui long tro lai sau it phut !";
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                            #endregion
                        }
                    }
                        //if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("AnhTaiBongDa_StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("AnhTaiBongDa_StartKM")))
                        //{
                        //    messageReturn = AppEnv.GetSetting("AnhTaiBongDa_DangKyThanhCongKM");
                        //}
                        //else
                        //{
                        //    messageReturn = AppEnv.GetSetting("AnhTaiBongDa_DangKyThanhCongNotKM");
                        //}                                         

                    

                    #endregion

                }
                else if (value.Rows[0]["RETURN_ID"].ToString() == "1")//DA DK DICH VU
                {
                    #region THUE BAO DANG ACTIVE DV
                    messageReturn = AppEnv.GetSetting("AnhTaiBongDa_DoubleDangKy");
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                    #endregion

                }

                #endregion
            }
            else if (Command_Code.ToUpper() == "DT" && subcode == "")
            {

                #region TRA CUU MADUTHUONG

                DataTable dtCount = ViSport_S2_Registered_UsersController.SportGameHeroCountLotteryCode(User_ID);
                string count = "0";
                if (dtCount != null && dtCount.Rows.Count > 0)
                {
                    count = dtCount.Rows[0]["Total"].ToString();
                }

                //messageReturn = "Quy khach dang co " + count + " ma du thuong de quay thuong CTKM Chuyen gia bong da cua Vietnamobile voi co hoi trung thuong 1 dien thoai Samsung Galaxy S4. " +
                //                "Chi tiet truy cap http://visport.vn. HT: 19001255";


                messageReturn = "Quy khach dang co " + count + " ma du thuong de quay thuong  CTKM Chay cung EURO cua Vietnamobile voi co hoi trung thuong 30 trieu tien mat. Chi tiet truy cap http://visport.vn. HT: 19001255";
                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);

                #endregion

            }

            else if (Command_Code.ToUpper() == "HDAN" && subcode == "") //HUONG DAN CHUONG TRINH
            {
                #region HUONGDAN DICHVU

                messageReturn = AppEnv.GetSetting("AnhTaiBongDa_HuongDanThanhCong");                
                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                return responseValue;
                #endregion
            }

            else if (Command_Code.ToUpper() == "HUY" && subcode.ToUpper() == "EU") //HUY DV TRIEU_PHU_BONG_DA
            {
                #region HUY DV Chay cung euro 

                DataTable dtUpdate = ViSport_S2_Registered_UsersController.UpdateEuroRegisterUser(User_ID, 0);
                //if (dtUpdate != null && dtUpdate.Rows.Count > 0)
                //{

                if (dtUpdate.Rows[0]["RETURN_ID"].ToString() == "1")
                {
                    messageReturn = AppEnv.GetSetting("AnhTaiBongDa_HuyDichVuThanhCong");   
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                }
                else
                {
                    messageReturn = AppEnv.GetSetting("AnhTaiBongDa_ChuaDangKy");                    
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                }
                //}
                //else
                //{

                //}

                #endregion
            }
           
            else if ((Command_Code.ToUpper() == "A" || Command_Code.ToUpper() == "B") && subcode == "")
            {
                //string today = DateTime.Now.DayOfWeek.ToString();

                //if(!CheckDayOfWeek(today))
                //{

                DataTable dtCount = ViSport_S2_Registered_UsersController.GetEuroUserInfoActive(User_ID);
                if (dtCount == null || dtCount.Rows.Count == 0)
                {
                    messageReturn = AppEnv.GetSetting("AnhTaiBongDa_ChuaDangKy");
                    //messageReturn = "Quy khach chua su dung dich vu Visport cua Vietnamobile. De dang ky su dung dich vu, soan TP gui 979. Chi tiet truy cap http://visport.vn. HT:19001255";
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                    return responseValue;
                }

                #region CAU HOI TPBD

                DataTable dtCountQues = ViSport_S2_Registered_UsersController.CountQuestionTodaySportGameHeroRegisterUser(User_ID);
                if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) == 0)
                {
                    messageReturn = "Chuong trinh hom nay sap bat dau. San sang san giai thuong Samsung Galaxy S4. chi tiet truy cap http://visport.vn. HT: 19001255";
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                    return responseValue;
                }

                #region PROCESS CAUHOI

                DataTable dtAnswer = ViSport_S2_Registered_UsersController.GetAnswerSportGameHero(User_ID);
                string answerMt = Convert_Answer(Command_Code.Trim().ToUpper());
                
                if (dtAnswer != null && dtAnswer.Rows.Count > 0)
                {
                    string answerDb = dtAnswer.Rows[0]["True_Answer"].ToString().Trim().ToUpper();
                    int questionId = ConvertUtility.ToInt32(dtAnswer.Rows[0]["Question_Id"].ToString());

                    if (answerMt == answerDb)//TRA LOI DUNG CAU HOI
                    {
                        DataTable randomMsg = ViSport_S2_Registered_UsersController.GetMessageRandomSportGameHero(3);
                        messageReturn = randomMsg.Rows[0]["Message"].ToString();
                        if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) == 5)
                        {
                            #region Du 5 CAUHOI TRONG NGAY

                            string code1 = RandomActiveCode.Generate(8);
                            ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(User_ID, code1);

                            messageReturn = messageReturn.Replace("xxx", code1);
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi DUNG

                            //CONG 1 MDT
                            ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUserTp(User_ID, questionId, 40, Request_ID, answerMt, 1);


                            messageReturn = "Quy khach da tra loi het 5 cau hoi mien phi hom nay. Nang cao co hoi trung thuong bang cach thu thach kien thuc cua minh voi cac cau hoi tiep theo (1.000d/cau hoi).";
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi Het 5 Cau FREE

                            SendContinueQuestion(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO

                            #endregion
                        }
                        else if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) < 5)
                        {
                            #region CHUA DU 5 CAUHOI TRONG NGAY

                            string code1 = RandomActiveCode.Generate(8);
                            ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(User_ID, code1);

                            messageReturn = messageReturn.Replace("xxx", code1);
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi DUNG

                            //CONG 1 MDT
                            ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUserTp(User_ID, questionId, 40, Request_ID, answerMt, 1);

                            SendContinueQuestionTpBd(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO

                            #endregion
                        }
                        else if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) > 5)
                        {
                            #region CAC CAU HOI TINHPHI

                            if (ChuyenGiaBongDaCharged(User_ID, "1000", Request_ID) == "1")
                            {
                                #region Charged THANHCONG ==> GHI NHAN DAPAN && TRA CAU HOI TIEP

                                string code1 = RandomActiveCode.Generate(8);
                                ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(User_ID, code1);

                                messageReturn = messageReturn.Replace("xxx", code1);
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi DUNG

                                SendContinueQuestionTpBd(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO

                                #endregion
                            }
                            else if (ChuyenGiaBongDaCharged(User_ID, "1000", Request_ID) == "Result:12,Detail:Not enough money.")
                            {
                                #region Charged THATBAI ==> GUI ALERT THONG BAO NAP TIEN

                                messageReturn = "Thue bao khong du tien. Vui long nap tien de tiep tuc choi !";
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                                #endregion
                            }
                            else
                            {
                                #region Charged THATBAI ==> LOI SYSTEM

                                messageReturn = "He thong dang ban. Vui long tro lai sau it phut !";
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                                #endregion
                            }

                            #endregion
                        }
                    }
                    else//TRA LOI SAI
                    {
                        DataTable randomMsg = ViSport_S2_Registered_UsersController.GetMessageRandomSportGameHero(4);
                        messageReturn = randomMsg.Rows[0]["Message"].ToString();

                        if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) == 5)
                        {
                            #region DU 5 CAUUHOI TRONG NGAY

                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi SAI
                            //CONG 0 MDT
                            ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUserTp(User_ID, questionId, 20, Request_ID, answerMt, 0);

                            //messageReturn = "Ban da tra loi het so cau hoi mien phi trong ngay (5 cau). Hay tiep tuc tham gia chuong trinh vao ngay mai de tich luy ma du thuong de SO HUU iPhone 5S.";

                            messageReturn = "Quy khach da tra loi het 5 cau hoi mien phi hom nay. Nang cao co hoi trung thuong bang cach thu thach kien thuc cua minh voi cac cau hoi tiep theo (1.000d/cau hoi).";
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi Het 5 Cau FREE
                            SendContinueQuestion(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO
                            #endregion
                        }
                        else if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) < 5)
                        {
                            #region CHUA DU 5 CAUHOI TRONG NGAY

                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi SAI
                            //CONG 0 MDT
                            ViSport_S2_Registered_UsersController.UpdatePointSportGameHeroRegisterUserTp(User_ID, questionId, 20, Request_ID, answerMt, 0);

                            SendContinueQuestionTpBd(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO

                            #endregion
                        }
                        else if (ConvertUtility.ToInt32(dtCountQues.Rows[0]["RETURN_ID"].ToString()) > 5)
                        {
                            #region CAC CAU HOI TINHPHI

                            if (ChuyenGiaBongDaCharged(User_ID, "1000", Request_ID) == "1")
                            {
                                #region Charged THANHCONG ==> GHI NHAN DAPAN && TRA CAU HOI TIEP

                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi SAI

                                SendContinueQuestionTpBd(User_ID, Service_ID, Command_Code, Request_ID);//GUI CAU HOI TIEP THEO

                                #endregion
                            }
                            else if (ChuyenGiaBongDaCharged(User_ID, "1000", Request_ID) == "Result:12,Detail:Not enough money.")
                            {
                                #region Charged THATBAI ==> GUI ALERT THONG BAO NAP TIEN

                                messageReturn = "Thue bao khong du tien. Vui long nap tien de tiep tuc choi !";
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                                #endregion
                            }
                            else
                            {
                                #region Charged THATBAI ==> LOI SYSTEM

                                messageReturn = "He thong dang ban. Vui long tro lai sau it phut !";
                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                                #endregion
                            }

                            #endregion
                        }
                    }

                }

                #endregion


                #endregion
                //}
            }

            else if (Command_Code.ToUpper() == "HD" && subcode.ToUpper() == "EU")
            {
                #region HDSD
                messageReturn = AppEnv.GetSetting("AnhTaiBongDa_HuongDanThanhCong");
                //messageReturn = "Dich vu/dau so 979 dang cung cap cac goi dich vu sau: Goi dich vu Anh tai bong da. Gia cuoc: 5000 dong/ngay. De dang ky dich vu, soan: BD gui 979. De huy dich vu, soan HUY BD gui 979. Truy cap http://visport.vn/ de biet them chi tiet. ";
                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);

                #endregion
            }
            
            else
            {
                //messageReturn = "Tin nhan sai cu phap. De nhan 2 MDT va co co hoi trung 30 trieu tien mat, iPhone 5S sanh dieu soan TP gui 979 (mien phi dang ky), de biet them thong tin soan: HD gui 979. HT: 19001255";
                messageReturn = "Tin nhan sai cu phap. Chi tiet truy cap http://visport.vn. HT: 19001255";
                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
            }
        }
        catch (Exception ex)
        {
            responseValue = "0";
            log.Debug("---------------Error sentMT Chay cung euro 2016----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }

        return responseValue;
    }


    private string ExcecuteRequestMoEuro_Wap(string User_ID, string Service_ID, string Command_Code, string Message, string Request_ID)
    {
        string responseValue = "1";
        int returnValue = 0;

        Message = Message.ToUpper();
        string subcode = "";
        if (Message.Trim().Length > Command_Code.Trim().Length)
        {
            subcode = Message.ToUpper().Substring(Command_Code.Length).Replace(" ", "");
        }

        Command_Code = Command_Code.ToUpper();
        Message = Message.ToUpper();

        try
        {
            log.Debug(" ");
            log.Debug(" ");
            log.Debug("-------------------- CHAY CUNG EURO 2016 -------------------------");
            log.Debug("User_ID: " + User_ID);
            log.Debug("Service_ID: " + Service_ID);
            log.Debug("Command_Code: " + Command_Code);
            log.Debug("Message: " + Message.ToUpper());
            log.Debug("Request_ID: " + Request_ID);
            log.Debug(" ");
            log.Debug(" ");

            #region Log MO Message Into Database (SMS_MO_Log)


            //if (AppEnv.GetSetting("TestFlag") == "0")
            //{
            var moInfo = new SMS_MOInfo();
            moInfo.User_ID = User_ID;
            moInfo.Service_ID = Service_ID;
            moInfo.Command_Code = Command_Code;
            moInfo.Message = Message;
            moInfo.Request_ID = Request_ID;
            moInfo.Operator = GetTelco(User_ID);
            SMS_MODB.InsertSportGameHeroMo(moInfo);
            //}


            #endregion

            string messageReturn = "";

            if (Command_Code == "EU" && subcode == "") //DK DICH VU TRIEU_PHU_BONG_DA
            {
                #region DK DICH VU

                var entity = new ViSport_S2_Registered_UsersInfo();
                entity.User_ID = User_ID;
                entity.Request_ID = Request_ID;
                entity.Service_ID = Service_ID;
                entity.Command_Code = Command_Code;
                entity.Service_Type = 1;
                entity.Charging_Count = 0;
                entity.FailedChargingTimes = 0;
                entity.RegisteredTime = DateTime.Now;
                entity.ExpiredTime = DateTime.Now.AddDays(1);
                entity.Registration_Channel = "WAP";
                entity.Status = 1;
                entity.Operator = GetTelco(User_ID);
                entity.Point = 2;

                string passWord = RandomActiveCode.RandomStringNumber(6);
                entity.Password = passWord;

                DataTable value = ViSport_S2_Registered_UsersController.InsertEuroRegisterUser(entity);

                if (value.Rows[0]["RETURN_ID"].ToString() == "0")//DK DICH VU LAN DAU
                {

                    #region DK DV LAN DAU TIEN ==> KM 5 MDT

                    if (AppEnv.GetSetting("CTKM_Flag") == "1")
                    {
                        #region Sinh 5 mã dự thưởng khi đăng ký lần đầu
                        for (int i = 1; i <= 5; i++)
                        {
                            string code = RandomActiveCode.Generate(8);
                            ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(User_ID, code);
                        }
                        #endregion
                    }
                    messageReturn = AppEnv.GetSetting("AnhTaiBongDa_DangKyThanhCong");
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1 

                    DataTable dtQuestion = ViSport_S2_Registered_UsersController.GetQuestionInfoEuro();
                    if (dtQuestion != null && dtQuestion.Rows.Count > 0)
                    {
                        messageReturn = dtQuestion.Rows[0]["Question"].ToString();

                        int questionId = ConvertUtility.ToInt32(dtQuestion.Rows[0]["Id"].ToString());
                        string answer = dtQuestion.Rows[0]["Answer"].ToString();

                        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 1); //SEND MT LAN 2 : GUI CAU HOI DAU TIEN
                        ViSport_S2_Registered_UsersController.InsertSportGameHeroAnswerLog(User_ID, questionId, messageReturn, answer, DateTime.Now, 0); // LUU LOG Question
                    }
                    #endregion

                }
                else if (value.Rows[0]["RETURN_ID"].ToString() == "2")
                {

                    //Check Đăng ký xong hủy trong ngày đầu tiên hay không
                    #region DA DK DV ROI XONG HUY DK LAI ==> TRA MT WELCOME

                    DataTable dtCheck = ViSport_S2_Registered_UsersController.Euro_Check_Regiter_Cancel_Today(User_ID);
                    if (dtCheck != null && dtCheck.Rows.Count > 0)
                    {
                        //Đăng ký lại trong ngày đầu tiên
                        messageReturn = AppEnv.GetSetting("AnhTaiBongDa_DangKyThanhCongKM");
                        SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT LAN 1
                    }
                    else
                    {
                        //Đăng ký lại ngày thứ 2 trở đi 
                        string val_charge_return = ChuyenGiaBongDaCharged(User_ID, "5000", Request_ID);//Charge khi đăng ký lại ngày thứ 2 trở đi

                        if (val_charge_return == "1")
                        {
                            #region Charged THANHCONG ==> Trả MT

                            #region Sinh 5 mã dự thưởng khi đăng ký lại
                            for (int i = 1; i <= 5; i++)
                            {
                                string code = RandomActiveCode.Generate(8);
                                ViSport_S2_Registered_UsersController.SportGameHeroLotteryCodeInsert(User_ID, code);
                            }
                            #endregion

                            messageReturn = AppEnv.GetSetting("AnhTaiBongDa_DangKyThanhCongNotKM");
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);// SEND MT GUI msg Thong bao Tra Loi DUNG
                            DataTable dtQuestion = ViSport_S2_Registered_UsersController.GetQuestionInfoEuro();
                            if (dtQuestion != null && dtQuestion.Rows.Count > 0)
                            {
                                messageReturn = dtQuestion.Rows[0]["Question"].ToString();

                                int questionId = ConvertUtility.ToInt32(dtQuestion.Rows[0]["Id"].ToString());
                                string answer = dtQuestion.Rows[0]["Answer"].ToString();

                                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 1); //SEND MT LAN 2 : GUI CAU HOI DAU TIEN
                                ViSport_S2_Registered_UsersController.InsertSportGameHeroAnswerLog(User_ID, questionId, messageReturn, answer, DateTime.Now, 0); // LUU LOG Question
                            }


                            #endregion
                        }
                        else if (val_charge_return == "Result:12,Detail:Not enough money.")
                        {
                            #region Charged THATBAI ==> GUI ALERT THONG BAO NAP TIEN

                            messageReturn = "Thue bao khong du tien. Vui long nap tien de tiep tuc choi !";
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                            #endregion
                        }
                        else
                        {
                            #region Charged THATBAI ==> LOI SYSTEM

                            messageReturn = "He thong dang ban. Vui long tro lai sau it phut !";
                            SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0); //SEND MT THONGBAO

                            #endregion
                        }
                    }
                    //if (DateTime.Now > Convert.ToDateTime(AppEnv.GetSetting("AnhTaiBongDa_StartKM")) && DateTime.Now < Convert.ToDateTime(AppEnv.GetSetting("AnhTaiBongDa_StartKM")))
                    //{
                    //    messageReturn = AppEnv.GetSetting("AnhTaiBongDa_DangKyThanhCongKM");
                    //}
                    //else
                    //{
                    //    messageReturn = AppEnv.GetSetting("AnhTaiBongDa_DangKyThanhCongNotKM");
                    //}                                         



                    #endregion

                }
                else if (value.Rows[0]["RETURN_ID"].ToString() == "1")//DA DK DICH VU
                {
                    #region THUE BAO DANG ACTIVE DV
                    messageReturn = AppEnv.GetSetting("AnhTaiBongDa_DoubleDangKy");
                    SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
                    #endregion

                }

                #endregion
            }
            

            else
            {
                //messageReturn = "Tin nhan sai cu phap. De nhan 2 MDT va co co hoi trung 30 trieu tien mat, iPhone 5S sanh dieu soan TP gui 979 (mien phi dang ky), de biet them thong tin soan: HD gui 979. HT: 19001255";
                messageReturn = "Tin nhan sai cu phap. Chi tiet truy cap http://visport.vn. HT: 19001255";
                SendMtSportGameHero(User_ID, messageReturn, Service_ID, Command_Code, Request_ID, 0);
            }
        }
        catch (Exception ex)
        {
            responseValue = "0";
            log.Debug("---------------Error sentMT Chay cung euro 2016----------------------");
            log.Debug("Get Error : " + ex.Message + ", returnValue: " + returnValue);
        }

        return responseValue;
    }
    #endregion

    #region Public Method
    private static string GetTelco(string mobile)
    {
        string prenumber = mobile.Substring(0, 5);

        string[] dfsplit = AppEnv.GetSetting("sfone").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "sfone";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("vnmobile").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "vnmobile";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("gtel").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "gtel";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("viettel").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "viettel";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("vms").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "vms";
                }
            }
        }

        dfsplit = AppEnv.GetSetting("gpc").Split('|');
        foreach (string s in dfsplit)
        {
            if (s != "")
            {
                if (prenumber.StartsWith(s))
                {
                    return "gpc";
                }
            }
        }

        return "";
    }

    #region Send MT
    public void SendMtSportGameHero(string userId, string mtMessage, string serviceId, string commandCode, string requestId, int isQuestion)
    {
        var objSentMt = new ServiceProviderService();

        const int msgType = (int)Constant.MessageType.NoCharge;

        if (AppEnv.GetSetting("TestFlag") == "0")
        {
            int result = objSentMt.sendMT(userId, mtMessage, serviceId, commandCode, msgType.ToString(), requestId, "1", "1", "0", "0");
            log.Debug("Send MT result : " + result);
            log.Debug("userId : " + userId);
            log.Debug("Noi dung MT : " + mtMessage);
            log.Debug("ServiceId : " + serviceId);
            log.Debug("commandCode : " + commandCode);
            log.Debug("requestId : " + requestId);
        }

        isQuestion = 0;// = 1 : CAU HOI GUI TU WinService

        var objMt = new ViSport_S2_SMS_MTInfo();
        objMt.User_ID = userId;
        objMt.Message = mtMessage;
        objMt.Service_ID = serviceId;
        objMt.Command_Code = commandCode;
        objMt.Message_Type = 1;
        objMt.Request_ID = requestId;
        objMt.Total_Message = 1;
        objMt.Message_Index = 0;
        objMt.IsMore = 0;
        objMt.Content_Type = 0;
        objMt.ServiceType = 0;
        objMt.ResponseTime = DateTime.Now;
        objMt.isLock = false;
        objMt.PartnerID = "VNM";
        objMt.Operator = GetTelco(userId);
        objMt.IsQuestion = isQuestion;

        ViSport_S2_SMS_MTController.InsertSportGameHeroMt(objMt);
    }
    #endregion
    private static bool CheckDayOfWeek(string inputDay)
    {

        if (inputDay == DayOfWeek.Tuesday.ToString() || inputDay == DayOfWeek.Thursday.ToString() || inputDay == DayOfWeek.Saturday.ToString())
        {
            return true;
        }

        return false;

        //return true;
    }

    public void SendContinueQuestion(string userId, string serviceId, string commandCode, string requestId)
    {
        DataTable dtQuestion = ViSport_S2_Registered_UsersController.GetQuestionInfoEuro();
        if (dtQuestion != null && dtQuestion.Rows.Count > 0)
        {
            string messageReturn = dtQuestion.Rows[0]["Question"].ToString();

            int questionIdnew = ConvertUtility.ToInt32(dtQuestion.Rows[0]["Id"].ToString());
            string answer = dtQuestion.Rows[0]["Answer"].ToString();

            SendMtSportGameHero(userId, messageReturn, serviceId, commandCode, requestId, 1);
            ViSport_S2_Registered_UsersController.InsertSportGameHeroAnswerLog(userId, questionIdnew, messageReturn, answer, DateTime.Now, 0); // LUU LOG Question
        }
    }
    public string Convert_Answer(string inp)
    {
        if (inp == "A")
        {
            return "1";
        }
        else
        {
            return "2";
        }
    }
    public void SendContinueQuestionTpBd(string userId, string serviceId, string commandCode, string requestId)
    {
        DataTable dtQuestion = ViSport_S2_Registered_UsersController.GetQuestionInfoEuro();
        if (dtQuestion != null && dtQuestion.Rows.Count > 0)
        {
            string messageReturn = dtQuestion.Rows[0]["Question"].ToString();
            messageReturn = messageReturn.Replace("P1", "1").Replace("P2", "2");

            int questionIdnew = ConvertUtility.ToInt32(dtQuestion.Rows[0]["Id"].ToString());
            string answer = dtQuestion.Rows[0]["Answer"].ToString();
            answer = answer.Replace("P1", "1").Replace("P2", "2");

            SendMtSportGameHero(userId, messageReturn, serviceId, commandCode, requestId, 1);
            ViSport_S2_Registered_UsersController.InsertSportGameHeroAnswerLog(userId, questionIdnew, messageReturn, answer, DateTime.Now, 0); // LUU LOG Question
        }
    }
    public string ChuyenGiaBongDaCharged(string userId, string price, string Request_ID)
    {
        var webServiceCharging3G = new WebServiceCharging3g();
        const string userName = "VMGViSport";
        const string userPass = "v@#port";
        const string cpId = "1930";
        //const string price = "1000";
        const string content = "Charged chay cung Euro @2016";
        const string serviceName = "CGBD_Euro_2016";
        string returnValue = webServiceCharging3G.PaymentVnmWithAccount(userId, price, content, serviceName, userName, userPass, cpId);

        //if (returnValue == "1")//CHARGED THANHCONG
        //{
            #region LOG DOANH THU

            //var logInfo = new ThanTaiChargedUserLogInfo();
            //logInfo.User_ID = userId;
            //logInfo.Request_ID = Request_ID;
            //logInfo.Service_ID = "979";
            //logInfo.Command_Code = "AB";
            //logInfo.Registration_Channel = "SMS";
            //logInfo.Status = 1;
            //logInfo.Operator = "vnmobile";
            //logInfo.Price = ConvertUtility.ToInt32(price);
            //logInfo.Reason = returnValue;

            //ViSport_S2_Registered_UsersController.Euro_ChargedUser_InsertLog(logInfo);
            var logInfo = new ThanTaiChargedUserLogInfo();
            logInfo.User_ID = userId;
            logInfo.Request_ID = Request_ID;
            logInfo.Service_ID = "979";
            logInfo.Command_Code = "TT";
            logInfo.Registration_Channel = "SMS";
            logInfo.Status = 1;
            logInfo.Operator = "vnmobile";
            logInfo.Price = ConvertUtility.ToInt32(price);
            logInfo.Reason = "Succ";

        ViSport_S2_Registered_UsersController.ThanTai_ChargedUser_InsertLog(logInfo);
            #endregion
        //}


        return returnValue;
    }
    #endregion

}
