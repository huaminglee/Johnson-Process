using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.EMail
{
    public class TaskFullUserInterpreter : TaskEmailTemplateInterpreter
    {
        public override void Interpret(TaskEmailTemplateContext context)
        {
            context.Template = context.Template.Replace("${TaskFullUser}", context.Task.strUserFullName);
        }
    }
}
