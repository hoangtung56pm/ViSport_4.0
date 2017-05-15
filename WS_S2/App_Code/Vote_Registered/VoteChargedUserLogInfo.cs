using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VoteChargedUserLogInfo
/// </summary>
public class VoteChargedUserLogInfo
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

    public int Vote_PersonId { get; set; }
}