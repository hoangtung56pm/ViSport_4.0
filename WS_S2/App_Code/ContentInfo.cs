using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WS_SDPGPC.Library.Content
{
    public class ContentInfo
    {
        private string _type;
        public string Type
        {
            get {return _type;}
            set { _type = value; }
        }
        private string _content;
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }
    }
}