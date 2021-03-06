﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for SamGpcCdrUpdate
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SamGpcCdrUpdate : System.Web.Services.WebService, IJobExecutorSoap
{

    public SamGpcCdrUpdate () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(CdrGpc));

    [WebMethod]
    public int Execute(int jobId)
    {
        try
        {
            ViSport_S2_Registered_UsersController.SamGpcUniqueIdUpdate();
        }
        catch (Exception ex)
        {
            _log.Error("CDR Loi UPDATE Unique_Id SAM : " + ex);
            return 0;
        }
        return 1;

    }

}
