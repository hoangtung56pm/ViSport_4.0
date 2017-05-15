#pragma checksum "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\111.aspx.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "504C3A10EB197CBC370851D1AEA6F9822CD029B5"

#line 1 "D:\VU_NGUYEN\ViSport\VISPORT_TO_MrVU\ViSport_4.0\WS_S2\111.aspx.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _111 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        WebService ws = new WebService();
        ws.WSProcessMoSportGame("849785518", "979", "DIEM", "DIEM", "36234636");
    }
}

#line default
#line hidden
