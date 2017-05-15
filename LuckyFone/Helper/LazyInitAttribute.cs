using System;
using System.Collections.Generic;
using System.Web;

namespace LuckyFone.Helper
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