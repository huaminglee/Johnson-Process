using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.WindowsServiceManagementService
{
    class JohnsonProcessWindowsServiceRestartor : ServiceWorkMonitor
    {
        public JohnsonProcessWindowsServiceRestartor()
            :base("Johnson.Process.WindowsService")
        {

        }

        public override bool AtWork()
        {
            if (DateTime.Now.Hour == 7 || DateTime.Now.Hour == 12 || DateTime.Now.Hour == 24)
            {
                return false;
            }
            return true;
        }
    }
}
