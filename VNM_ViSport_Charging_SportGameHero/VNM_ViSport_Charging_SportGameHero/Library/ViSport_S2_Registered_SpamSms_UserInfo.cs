using System;

namespace VNM_ViSport_Charging_SpamSms.Library
{
    public class ViSport_S2_Registered_SpamSms_UserInfo
    {
        public int Id { get; set; }

        public string User_Id { get; set; }

        public string Request_Id { get; set; }

        public string Service_Id { get; set; }

        public string Command_Code { get; set; }

        public string Sub_Code { get; set; }

        public int Service_Type { get; set; }

        public int Charging_Count { get; set; }

        public int FailedChargingTimes { get; set; }

        public DateTime RegisteredTime { get; set; }

        public DateTime ChargingDay { get; set; }

        public DateTime ExpiredTime { get; set; }

        public string Registration_Channel { get; set; }

        public int Status { get; set; }

        public string Operator { get; set; }

        public int IsLock { get; set; }

        public string Reason { get; set; }

        public int Price { get; set; }
    }
}
