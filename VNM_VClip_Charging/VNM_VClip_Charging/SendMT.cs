using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using VNM_VClip_Charging.Library;
using System.Threading;
using Threaded;
using Amib.Threading;


namespace VNM_VClip_Charging
{
    public partial class SendMT : ServiceBase
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(SendMT));
        private TQueue<ViSport_S2_Charged_Users_LogInfo> queue = new TQueue<ViSport_S2_Charged_Users_LogInfo>();        
        private SmartThreadPool _smartThreadPool;
        private IWorkItemsGroup _workItemsGroup;
        private Thread workItemsProducerThread;        
        
        public SendMT()
        {
            InitializeComponent();            
        }


        protected override void OnStart(string[] args)
        {
            //Load MO to Queue
            //tinnv 2012-09-19 Viet rieng de chay NON VMG Route <Runon_Route <> 5>
            Thread worker1 = new Thread(AddtoQueue);
            worker1.Name = "MyWorker";
            worker1.IsBackground = false;
            worker1.Start();

            int concurentThreads = 50;
            int maxThread = 55;
            try
            {
                concurentThreads = Convert.ToInt32(SMS.Default.ConcurentThread);
            }
            catch { concurentThreads = 50; }

            try
            {
                maxThread = Convert.ToInt32(SMS.Default.MaxThread);
            }
            catch { maxThread = 55; }

            try
            {
                STPStartInfo stpStartInfo = new STPStartInfo
                {
                    ThreadPriority = ThreadPriority.Normal,
                    WorkItemPriority = WorkItemPriority.Normal,
                    IdleTimeout = 60000,
                    MaxWorkerThreads = maxThread,
                    MinWorkerThreads = 0,
                    EnableLocalPerformanceCounters = false
                };
                this._smartThreadPool = new SmartThreadPool(stpStartInfo);
                this._smartThreadPool.WaitForIdle();

                this._workItemsGroup = this._smartThreadPool.CreateWorkItemsGroup(concurentThreads);
                this._workItemsGroup.WaitForIdle();

                this.workItemsProducerThread = new Thread(new ThreadStart(this.WorkItemsProducer));
                this.workItemsProducerThread.IsBackground = false;
                this.workItemsProducerThread.Start();
                
            }
            catch (Exception ex)
            {
                _logger.Error(string.Concat("Error - VNM_VClip_Charging.SendMT.Onstart: ", ex.Message));
                _logger.Error(string.Concat("Error - VNM_VClip_Charging.SendMT.Onstart: ", ex.StackTrace));
            }            
        }        

        private void AddtoQueue()
        {
            // Insert To Queue
            try
            {
                SMS_MTController objmtctl = new SMS_MTController();
                objmtctl.AddSMSToQueThread();                            
            }
            catch (Exception ex)
            {
                _logger.Error(string.Concat("Error - ADDTOQUEUE: ",ex.Message));
                _logger.Error(string.Concat("Error - ADDTOQUEUE: ", ex.StackTrace));
            }  

        }
        
        private void WorkItemsProducer()
        {
            IWorkItemsGroup workItemsGroup = _workItemsGroup;
            if (null == workItemsGroup)
            {
                _logger.Info("workItemsGroup null mat roi");
                return;
            }

            try
            {
                while (true)
                {
                    if (MSMProccess.MT_PROC_QUE.Count > 0)
                    {
                        ViSport_S2_Registered_UsersInfo info = MSMProccess.MT_PROC_QUE.Dequeue();
                        if (info != null)
                        {
                            WorkItemCallback workItemCallback = new WorkItemCallback(this.DoSomeWork1);
                            workItemsGroup.QueueWorkItem(workItemCallback, info);
                        }
                        else
                        {
                            _logger.Error("Dequeue ViSport_S2_Registered_UsersInfo IS NULL");
                        }
                    }
                    else
                    {
                        Thread.Sleep(50);
                    }
                }
            }
            catch (ObjectDisposedException e)
            {
                e.GetHashCode();
            }
            catch (Exception ex)
            {
                _logger.Error(string.Concat("WorkItemsProducer - ", ex.StackTrace));
                _logger.Error(string.Concat("WorkItemsProducer - ", ex.Message));
            }

        }
        
        private object DoSomeWork1(object obj)
        {
            try
            {
                MSMProccess.ChargeUser((ViSport_S2_Registered_UsersInfo)obj);               
            }
            catch (Exception ex)
            {
                _logger.Error(string.Concat("DoSomeWork1 - ",ex.StackTrace));
                _logger.Error(string.Concat("DoSomeWork1 - ", ex.Message));
            }
            return null;
        }
        
        protected override void OnStop()
        {
            this._smartThreadPool.Shutdown(false, 10000);         
        }
    }
}
