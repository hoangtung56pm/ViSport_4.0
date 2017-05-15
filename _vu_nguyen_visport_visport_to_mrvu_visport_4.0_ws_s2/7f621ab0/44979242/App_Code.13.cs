#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\SMS_MO\SMS_CancelInfo.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7CF767FCED3A424DB72563C4582E1083F96BB6CB"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\App_Code\SMS_MO\SMS_CancelInfo.cs"
using System;
using System.Collections.Generic;

using System.Web;


public class SMS_CancelInfo
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

    private int _Service_Type;
    public int Service_Type
    {
        get { return _Service_Type; }
        set { _Service_Type = value; }
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

}



#line default
#line hidden
