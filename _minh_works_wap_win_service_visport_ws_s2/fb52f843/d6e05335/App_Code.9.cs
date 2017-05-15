#pragma checksum "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\Utilities\LazyInitAttribute.cs" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E8FBFAEDB2550AF48B2362974E356313BB089D48"

#line 1 "E:\Minh_Works\Wap\WIN_SERVICE\ViSport\WS_S2\App_Code\Utilities\LazyInitAttribute.cs"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SMSManager_API.Library.Utilities
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class LazyInitAttribute : System.Attribute
    {
        private bool _isLazyInit;
        public bool IsLazyInit
        {
            get { return this._isLazyInit; }
        }
        public LazyInitAttribute(bool _lazyInit)
        {
            this._isLazyInit = _lazyInit;
        }
    }
}


#line default
#line hidden
