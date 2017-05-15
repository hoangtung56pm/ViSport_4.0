using System;
using System.Collections.Generic;
using System.Text;

using System.ComponentModel;
using System.ServiceProcess;

namespace VNM_VClip_Charging
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

            serviceInstaller.DisplayName = "VNM_VClip_Charging";
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "VNM_VClip_Charging";

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}

