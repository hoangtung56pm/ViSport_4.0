using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VoteSmsMoInfo
/// </summary>
public class VoteSmsMoInfo
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