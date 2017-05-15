using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.ServiceProcess;

namespace VNM_ViSport_SpamSms
{
    [RunInstallerAttribute(true)]
    public class MyWinServiceInstaller : System.Configuration.Install.Installer
    {
        public MyWinServiceInstaller()
        {

            ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
            ServiceInstaller serviceInstaller = new ServiceInstaller();

            //# Service Account Information

            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
            serviceProcessInstaller.Username = null;
            serviceProcessInstaller.Password = null;

            serviceInstaller.DisplayName = "VNM_ViSport_SpamSMS";
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "VNM_ViSport_SpamSMS";

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}

