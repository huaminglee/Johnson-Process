using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using log4net;

namespace Johnson.Process.WindowsServiceManagementService
{
    public abstract class ServiceWorkMonitor
    {
        private const string STOP_CMD = "stop ";

        private const string START_CMD = "start ";

        private string _serviceName;

        private string[] _dependentServiceName;
        ILog logger;

        public ServiceWorkMonitor(string serviceName)
        {
            this._serviceName = serviceName;
            logger = log4net.LogManager.GetLogger("Johnson_Process_logger");
        }

        public ServiceWorkMonitor(string serviceName, string[] dependentServiceName)
        {
            this._serviceName = serviceName;
            this._dependentServiceName = dependentServiceName;
        }

        public void Restart()
        {
            Stop();
            Thread.Sleep(1000 * 60);
            Start();
        }

        public abstract bool AtWork();

        public void Monitor()
        {
            if (!this.AtWork())
            {
                this.Restart();
            }
        }

        public virtual void Stop()
        {
            NetProcess process = new NetProcess(STOP_CMD + this._serviceName);
            logger.Info("停止服务:" + this._serviceName);
            process.Run();
            if (this._dependentServiceName != null)
            {
                foreach (string servericeName in _dependentServiceName)
                {
                    process = new NetProcess(STOP_CMD + servericeName);
                    logger.Info("停止服务:" + servericeName);
                    process.Run();
                }

            }
        }

        public virtual void Start()
        {
            NetProcess process = new NetProcess(START_CMD + this._serviceName);
            logger.Info("启动服务:" + this._serviceName);
            process.Run();
            if (this._dependentServiceName != null)
            {
                foreach (string servericeName in _dependentServiceName)
                {
                    process = new NetProcess(START_CMD + servericeName);
                    logger.Info("启动服务:" + servericeName);
                    process.Run();
                }
            }
        }
    }
}
