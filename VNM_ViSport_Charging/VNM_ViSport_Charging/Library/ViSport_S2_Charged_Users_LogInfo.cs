using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VNM_ViSport_Charging.Library
{
    public class ViSport_S2_Charged_Users_LogInfo
    {
        private int _iD;
        public int ID
        {
            get { return _iD; }
            set { _iD = value; }
        }

        private string _user_ID;
        public string User_ID
        {
            get { return _user_ID; }
            set { _user_ID = value; }
        }

        private string _request_ID;
        public string Request_ID
        {
            get { return _request_ID; }
            set { _request_ID = value; }
        }

        private string _service_ID;
        public string Service_ID
        {
            get { return _service_ID; }
            set { _service_ID = value; }
        }

        private string _command_Code;
        public string Command_Code
        {
            get { return _command_Code; }
            set { _command_Code = value; }
        }

        private int _service_Type;
        public int Service_Type
        {
            get { return _service_Type; }
            set { _service_Type = value; }
        }

        private int _charging_Count;
        public int Charging_Count
        {
            get { return _charging_Count; }
            set { _charging_Count = value; }
        }

        private int _failedChargingTimes;
        public int FailedChargingTimes
        {
            get { return _failedChargingTimes; }
            set { _failedChargingTimes = value; }
        }

        private DateTime _registeredTime;
        public DateTime RegisteredTime
        {
            get { return _registeredTime; }
            set { _registeredTime = value; }
        }

        private DateTime _expiredTime;
        public DateTime ExpiredTime
        {
            get { return _expiredTime; }
            set { _expiredTime = value; }
        }

        private string _registration_Channel;
        public string Registration_Channel
        {
            get { return _registration_Channel; }
            set { _registration_Channel = value; }
        }

        private int _status;
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private string _operator;
        public string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

        private string _reason;
        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }

        public int Price { get; set;}

    }

}
