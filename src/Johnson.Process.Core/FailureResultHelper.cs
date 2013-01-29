using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.Core
{
    public class FailureResultHelper
    {
        public static string MapName(FailureResult result)
        {
            switch (result)
            {
                case FailureResult.MRB: return "MRB会议";
                case FailureResult.Pick: return "挑选";
                case FailureResult.Receive: return "让步接收";
                case FailureResult.Return: return "退回供应商";
                case FailureResult.Rework: return "返工/返修";
                case FailureResult.Scrap: return "报废";
            }
            return "";
        }
    }
}
