﻿#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\Vote_Registered\VoteRegisteredInfo.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "B8246FA6236B56258F7FD106FB8E460F3615B726"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\Vote_Registered\VoteRegisteredInfo.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VoteRegisteredInfo
/// </summary>
public class VoteRegisteredInfo
{
    public int ID { get; set; }

    public string User_ID { get; set; }

    public string Request_ID { get; set; }

    public string Service_ID { get; set; }

    public string Command_Code { get; set; }

    public int Service_Type { get; set; }

    public int Charging_Count { get; set; }

    public int FailedChargingTime { get; set; }

    public DateTime RegisteredTime { get; set; }

    public DateTime ExpiredTime { get; set; }

    public string Registration_Channel { get; set; }

    public int Status { get; set; }

    public string Operator { get; set; }

    public int IsLock { get; set; }

    public int Vote_Count { get; set; }

    public int Vote_PersonId { get; set; }

    public int IsDislike { get; set; }

    public int Dislike_Count { get; set; }

    public int Dislike_PersonId { get; set; }

}

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

    public int Price { get; set; }

    public int Vote_PersonId { get; set; }


}

#line default
#line hidden
