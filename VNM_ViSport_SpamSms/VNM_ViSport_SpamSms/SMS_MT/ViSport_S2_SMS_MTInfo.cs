using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VNM_ViSport_SpamSms.SMS_MT
{
    public class ViSport_S2_SMS_MTInfo
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

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; }
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

        public string Sub_Code { get; set; }

        private int _message_Type;
        public int Message_Type
        {
            get { return _message_Type; }
            set { _message_Type = value; }
        }

        private string _request_ID;
        public string Request_ID
        {
            get { return _request_ID; }
            set { _request_ID = value; }
        }

        private int _total_Message;
        public int Total_Message
        {
            get { return _total_Message; }
            set { _total_Message = value; }
        }

        private int _message_Index;
        public int Message_Index
        {
            get { return _message_Index; }
            set { _message_Index = value; }
        }

        private int _isMore;
        public int IsMore
        {
            get { return _isMore; }
            set { _isMore = value; }
        }

        private int _content_Type;
        public int Content_Type
        {
            get { return _content_Type; }
            set { _content_Type = value; }
        }

        private int _serviceType;
        public int ServiceType
        {
            get { return _serviceType; }
            set { _serviceType = value; }
        }

        private DateTime _responseTime;
        public DateTime ResponseTime
        {
            get { return _responseTime; }
            set { _responseTime = value; }
        }

        private bool _isLock;
        public bool isLock
        {
            get { return _isLock; }
            set { _isLock = value; }
        }

        private string _partnerID;
        public string PartnerID
        {
            get { return _partnerID; }
            set { _partnerID = value; }
        }

        private string _operator;
        public string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }

        public int MtOrder { get; set; }

    }
}
