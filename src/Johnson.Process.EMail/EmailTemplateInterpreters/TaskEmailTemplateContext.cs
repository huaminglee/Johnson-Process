using System;
using System.Collections.Generic;
using System.Text;
using Ultimus.WFServer;

namespace Johnson.Process.EMail
{
    public class TaskEmailTemplateContext
    {
        public string Template { set; get; }

        public Task Task { set; get; }
    }
}
