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

    public int Execute(int jobId)
    {
        return jobId;
    }
    
}
