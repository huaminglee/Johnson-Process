using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Johnson.Process.WindowsServiceManagementService
{
    class NetProcess
    {
        System.Diagnostics.Process cmdProcess;
        public NetProcess(string cmd)
        {
            cmdProcess = new System.Diagnostics.Process();
            cmdProcess.StartInfo.FileName = "net ";
            cmdProcess.StartInfo.UseShellExecute = false;
            cmdProcess.StartInfo.Arguments = cmd;
            cmdProcess.StartInfo.CreateNoWindow = true;
            
        }

        public void Run()
        {
            this.cmdProcess.Start();
            this.cmdProcess.Close();
        }
    }
}
