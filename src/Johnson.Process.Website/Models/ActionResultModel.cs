using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public enum ActionResult
    {
        Succeed,
        Error
    }

    public class ActionResultModel
    {
        public ActionResult result;

        public string message;

        public object data;
    }
}