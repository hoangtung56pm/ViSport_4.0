﻿#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\Jobs.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "D0D427EA105B15E2FF708EAECE5DCF052117F39F"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\Jobs.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Summary description for Jobs
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Jobs : System.Web.Services.WebService, IJobExecutorSoap
{

    public Jobs () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    public int Execute(int jobID)
    {
        return jobID;
    }
    
}


#line default
#line hidden