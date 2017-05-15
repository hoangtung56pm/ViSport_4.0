using System;
using System.Collections.Generic;
using System.Text;

namespace VNM_VClip_SpamSms.Library
{
    class SMSEnum
    {

        public enum SMS_MT_Type : int
        {
            //Text
            Text = 0,

            Ringtone = 1,

            Logo = 2,

            PictureMessage = 4,

            Wappush = 8
        }
    }
}
