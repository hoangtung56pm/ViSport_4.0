#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\ViSport_S2_Registered\SportGameHeroChargedUserLogInfo.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F993DD01FADDD8807DDB479EC507A96E28298227"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\ViSport_S2_Registered\SportGameHeroChargedUserLogInfo.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SportGameHeroChargedUserLogInfo
/// </summary>
public class SportGameHeroChargedUserLogInfo
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

    public string Reason { get; set; }

    public DateTime ChargingDate { get; set; }

    public int Price { get; set; }

}

public class ThanhNuChargedUserLogInfo
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

    public string Reason { get; set; }

    public DateTime ChargingDate { get; set; }

    public int Price { get; set; }

    public string PartnerResult { get; set; }

}

public class MoEntity997
{
    public Int64 ID { get; set; }

    public string UserID { get; set; }

    public string ServiceID { get; set; }

    public string MobileOperator { get; set; }

    public string CommandCode { get; set; }

    public string Info { get; set; }

    public DateTime Timestamp { get; set; }

    public int Responded { get; set; }

    public string RequestID { get; set; }

}

#line default
#line hidden
