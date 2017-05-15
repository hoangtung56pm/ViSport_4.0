#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\ViSport_S2_Registered\VnmS2RegisterUserInfo.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C786FE1DF3A1D2EDEAC2583A9F69CF00663ECEA3"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\ViSport_S2_Registered\VnmS2RegisterUserInfo.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VnmS2RegisterUserInfo
/// </summary>
public class VnmS2RegisterUserInfo
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public string RequestId { get; set; }

    public string ServiceId { get; set; }

    public string CommandCode { get; set; }

    public string SubCode { get; set; }

    public string Operator { get; set; }

    public string RegisteredChannel { get; set; }

    public int Status { get; set; }

    public DateTime RegisteredTime { get; set; }
}

public class ThanhNuRegisteredUsers
{
    public int Id { get; set; }

    public string UserId { get; set; }

    public string RequestId { get; set; }

    public string ServiceId { get; set; }

    public string CommandCode { get; set; }

    public int ServiceType { get; set; }

    public int ChargingCount { get; set; }

    public int FailedChargingTimes { get; set; }

    public DateTime RegisteredTime { get; set; }

    public DateTime ExpiredTime { get; set; }

    public string RegistrationChannel { get; set; }

    public int Status { get; set; }

    public string Operator { get; set; }

    public int IsLock { get; set; }
}

#line default
#line hidden
