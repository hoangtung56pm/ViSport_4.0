using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ThanTai_MT_Info
/// </summary>
public class ThanTai_MT_Info
{
    private int _ID;

    public int ID
    {
        get { return _ID; }
        set { _ID = value; }
    }
    private string _User_ID;

    public string User_ID
    {
        get { return _User_ID; }
        set { _User_ID = value; }
    }
    private string _Message;

    public string Message
    {
        get { return _Message; }
        set { _Message = value; }
    }

    private string _Service_ID;

    public string Service_ID
    {
        get { return _Service_ID; }
        set { _Service_ID = value; }
    }
    private string _Command_Code;

    public string Command_Code
    {
        get { return _Command_Code; }
        set { _Command_Code = value; }
    }
    private int _Message_Type;

    public int Message_Type
    {
        get { return _Message_Type; }
        set { _Message_Type = value; }
    }
    private string _Request_ID;

    public string Request_ID
    {
        get { return _Request_ID; }
        set { _Request_ID = value; }
    }
    private int _Total_Message;

    public int Total_Message
    {
        get { return _Total_Message; }
        set { _Total_Message = value; }
    }
    private int _Message_Index;

    public int Message_Index
    {
        get { return _Message_Index; }
        set { _Message_Index = value; }
    }
    private int _IsMore;

    public int IsMore
    {
        get { return _IsMore; }
        set { _IsMore = value; }
    }
    private int _Content_Type;

    public int Content_Type
    {
        get { return _Content_Type; }
        set { _Content_Type = value; }
    }
    private int _ServiceType;

    public int ServiceType
    {
        get { return _ServiceType; }
        set { _ServiceType = value; }
    }
    private DateTime _ResponseTime;

    public DateTime ResponseTime
    {
        get { return _ResponseTime; }
        set { _ResponseTime = value; }
    }
    private string _PartnerID;

    public string PartnerID
    {
        get { return _PartnerID; }
        set { _PartnerID = value; }
    }
    private string _Operator;

    public string Operator
    {
        get { return _Operator; }
        set { _Operator = value; }
    }
    private int _Type;

    public int Type
    {
        get { return _Type; }
        set { _Type = value; }
    }
}