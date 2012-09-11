using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.EMail.Exceptions
{
    public class TaskEmailNotifySerivceException: ApplicationException
    {
        string _message;

        public TaskEmailNotifySerivceException(string message)
        {
            this._message = message;
        }

        public override string Message
        {
            get
            {
                return this._message;
            }
        }
    }
}
