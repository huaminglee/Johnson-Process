using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.WFServer;

namespace Johnson.Process.Core
{
    public class UltimusHelper
    {
        public static Task GetTask(string taskId)
        {
            Task task = new Task();
            if (!task.InitializeFromTaskId(taskId))
            {
                throw new Exception(string.Format("初始化taskId为{0}的Task失败", taskId));
            }
            return task;
        }
    }
}
