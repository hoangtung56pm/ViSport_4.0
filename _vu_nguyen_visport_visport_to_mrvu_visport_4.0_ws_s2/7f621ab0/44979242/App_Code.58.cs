#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\Zalo_Registered\ZaloEntity.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0E6F8F54EE780731E399D0C7E0AFAE8A3B1C4E9E"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\Zalo_Registered\ZaloEntity.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ZaloEntity
/// </summary>
public class ZaloEntity
{

}

public class ZaloMoInfo
{
    public int ID { get; set; }

    public string User_ID { get; set; }

    public string Request_ID { get; set; }

    public string Service_ID { get; set; }

    public string Command_Code { get; set; }

    public string Message { get; set; }

    public string Operator { get; set; }

    public DateTime RequestDate { get; set; }
}

public class ZaloMtInfo
{
    public int ID { get; set; }

    public string User_ID { get; set; }

    public string Message { get; set; }

    public string Service_ID { get; set; }

    public string Command_Code { get; set; }

    public int Message_Type { get; set; }

    public string Request_ID { get; set; }

    public int Total_Message { get; set; }

    public int Message_Index { get; set; }

    public int IsMore { get; set; }

    public int Content_Type { get; set; }

    public int ServiceType { get; set; }

    public DateTime ResponseTime { get; set; }

    public int IsLock { get; set; }

    public string PartnerId { get; set; }

    public string Operator { get; set; }

    public int Type { get; set; }

}

public class MTInfo
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

}

public class ZaloLotteryDay
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public string RequestId { get; set; }

    public string ServiceId { get; set; }

    public string CommanCode { get; set; }

    public string Operator { get; set; }

    public string ServiceCode { get; set; }

    public int Type { get; set; }

    public int CompanyId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ExpiredDate { get; set; }

}

public class CallbackInfo
{

    public long Id { get; set; }

    public string Event { get; set; }

    public string Status { get; set; }

    public string MsgId { get; set; }

    public string TimeStamp { get; set; }

    public string Mac { get; set; }

}


#line default
#line hidden
