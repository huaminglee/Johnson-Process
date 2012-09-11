using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.EMail
{
    public abstract class TaskEmailTemplateInterpreter
    {
        public abstract void Interpret(TaskEmailTemplateContext context);
    }
}
