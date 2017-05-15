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
