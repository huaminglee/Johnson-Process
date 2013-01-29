using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.Core
{
    public class OrderWenjianFafangForm
    {
        public int IncidentNo { set; get; }

        public string StartUserName { set; get; }

        public string StartUserAccount { set; get; }

        public DateTime StartTime { set; get; }

        public int OrderPingshenIncidentNo { set; get; }

        public string JianChaEngineerAccount { set; get; }

        public string JianChaEngineerName { set; get; }

        public string ZhuGuanAccount { set; get; }

        public string ZhuGuanName { set; get; }

        public string SheJiShuoMing { set; get; }

        public bool FafangWancheng { set; get; }

        public bool HasXinWuLiao { set; get; }

        public List<ProcessFile> SheJiZiLiao { set; get; }

        public List<TaskApproveInfo> Approves { set; get; }
    }
}
