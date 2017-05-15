#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\Utilities\Constant.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "881A604D11438F9021C598B59E78D7B0B73EDE30"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\Utilities\Constant.cs"
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace WS_Music.Library
{
    public class Constant
    {
        public enum MessageType : int
        {
            NoCharge = 0,
            Charge = 1,
            Refund = 2,
        }

        public enum ContentType : int
        {
            Text = 0,
            RingTone = 1,
            Logo = 2,
            Binary = 3,
            PictureMessage = 4,
            Wappush = 8,
        }

        public enum DayNum : int
        {
            Monday = 0,
            Tuesday = 1,
            Wednesday = 2,
            Thursday = 3,
            Friday = 4,
            Saturday = 5,
            Sunday = 6,
        }

    }
}


#line default
#line hidden
