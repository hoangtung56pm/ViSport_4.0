using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AppboxApi.Library
{
    public class AppEnv
    {

        public static string GetSetting(string key)
        {
            return WebConfigurationManager.AppSettings[key];
        }

    }
}