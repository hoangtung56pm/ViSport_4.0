using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VoteSmsMtInfo
/// </summary>
public class VoteSmsMtInfo
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

}