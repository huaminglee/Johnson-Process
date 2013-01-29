using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Ultimus.WFServer;
using System.Threading;
using Johnson.Process.Core;

namespace Johnson.ProcessTest
{
    public class ProcessTester
    {
        public void Test()
        {
            DeliveryProcessForm form = new DeliveryProcessForm { BookDate = DateTime.Now, ProjectName = "ssd" };
            UltimusFormProcess<DeliveryProcessForm> process = new UltimusFormProcess<DeliveryProcessForm>("MyTest");
            string qiAccount = "qi";
            string qiUltimusAccount = process.GetUltimusUserAccount("qi");
            string taskId = this.GetProcessStartTaskId("MyTest", qiUltimusAccount);
            TaskSendResult result = process.Start(qiAccount, taskId, null, "", "", form);
            Assert.IsTrue(result.IncidentNo > 0);
            Thread.Sleep(2000);

            taskId = this.GetIncidentTaskId("MyTest", qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, null, "", "", form);
            Thread.Sleep(2000);

            taskId = this.GetIncidentTaskId("MyTest", qiUltimusAccount, result.IncidentNo);
            process.Send(taskId, null, "", "", form);
            Thread.Sleep(2000);

            //taskId = this.GetIncidentTaskId("MyTest", qiUltimusAccount, result.IncidentNo);
            //process.Send(taskId, null, "", "", form);
            //Thread.Sleep(2000);
        }

        protected string GetProcessStartTaskId(string processName, string ultimusAccount)
        {
            Tasklist taskList = new Tasklist();
            TasklistFilter filter = new TasklistFilter { };
            filter.strArrUserName = new string[] { ultimusAccount };
            filter.nFiltersMask = Filters.nFilter_Initiate;
            filter.strProcessNameFilter = processName;
            Assert.IsTrue(taskList.LoadFilteredTasks(filter));
            Task task = taskList.GetAt(0);
            if (task == null)
            {
                throw new Exception("task 为空");
            }
            return task.strTaskId;
        }

        protected string GetIncidentTaskId(string processName, string ultimusAccount, int incidentNo)
        {
            Tasklist taskList = new Tasklist();
            TasklistFilter filter = new TasklistFilter { };
            filter.nFiltersMask = Filters.nFilter_Current | Filters.nFilter_Overdue | Filters.nFilter_Urgent;
            filter.strProcessNameFilter = processName;
            filter.nIncidentNo = incidentNo;
            filter.strArrUserName = new string[] { ultimusAccount };
            taskList.LoadFilteredTasks(filter);
            Task task = taskList.GetAt(0);
            if (task == null)
            {
                throw new Exception("task 为空");
            }
            return task.strTaskId;
        }

        protected string GetIncidentTaskId(string processName, string ultimusAccount, int incidentNo, string stepName)
        {
            Tasklist taskList = new Tasklist();
            TasklistFilter filter = new TasklistFilter { };
            filter.nFiltersMask = Filters.nFilter_Current | Filters.nFilter_Overdue | Filters.nFilter_Urgent;
            filter.strProcessNameFilter = processName;
            filter.nIncidentNo = incidentNo;
            filter.strStepLabelFilter = stepName;
            filter.strArrUserName = new string[] { ultimusAccount };
            taskList.LoadFilteredTasks(filter);
            Task task = taskList.GetAt(0);
            if (task == null)
            {
                throw new Exception("task 为空");
            }
            return task.strTaskId;
        }
    }
}
