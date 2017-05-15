using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using VNM_ViSport_Charging_SpamSms.Library;
namespace VNM_ViSport_Charging_SpamSms
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesSendMt;
            log4net.Config.XmlConfigurator.Configure();            
            ServicesSendMt = new ServiceBase[] { new SendMT() };
            ServiceBase.Run(ServicesSendMt);
        }
    }
}