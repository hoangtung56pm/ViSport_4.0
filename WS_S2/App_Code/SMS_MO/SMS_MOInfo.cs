using System;
using System.Collections.Generic;

using System.Web;


public class SMS_MOInfo
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

    private string _message;
    public string Message
    {
        get { return _message; }
        set { _message = value; }
    }

    private string _Operator;
    public string Operator
    {
        get { return _Operator; }
        set { _Operator = value; }
    }

    public int IsCharged { get; set; }
    private int _Type;

    public int Type
    {
        get { return _Type; }
        set { _Type = value; }
    }

}

