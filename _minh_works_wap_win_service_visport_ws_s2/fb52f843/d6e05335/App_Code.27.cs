#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\SMS_MO\SMS_MOInfo.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "56E93A37D2A08A8394FD4C559E0BDEE336F78E03"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\SMS_MO\SMS_MOInfo.cs"
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

}



#line default
#line hidden
