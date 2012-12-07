using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.Core
{
    public class OrderEngFuZeRenPingShenInfo
    {
        public string TaskId { set; get; }

        public bool IsStandard { set; get; }

        public string EngEngineerAccount { set; get; }

        public string EngEngineerName { set; get; }

        public string DianQiEngineerAccount { set; get; }

        public string DianQiEngineerName { set; get; }

        public DateTime? WaiGouQingDanRiQi { set; get; }

        public DateTime? SheJiWanChengRiQi { set; get; }

        public TaskApproveInfo ApproveInfo { set; get; }
    }
}
