using System.ComponentModel;
using System.ServiceProcess;

namespace VNM_VClip_SpamSms
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

            serviceInstaller.DisplayName = "VNM_VClip_SpamSms";
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "VNM_VClip_SpamSms";

            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}

