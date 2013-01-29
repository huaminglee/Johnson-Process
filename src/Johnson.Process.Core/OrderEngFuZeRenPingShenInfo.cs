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

        public string QiTaYaoQiuShuoMing { set; get; }

        public string SheJiFuZeRenAccount { set; get; }

        public string SheJiFuZeRenName { set; get; }

        public string PmcEngineerAccount { set; get; }

        public string PmcEngineerName { set; get; }

        public string CsdPingShenRenAccounts { set; get; }

        public string EngPingShenRenAccounts { set; get; }

        public string ScmPingShenRenAccounts { set; get; }

        public string QadPingShenRenAccounts { set; get; }

        public string CidPingShenRenAccounts { set; get; }

        public string PmcPingShenRenAccounts { set; get; }

        public string PingShenRenAccounts
        {
            get
            {
                if (CsdPingShenRenAccounts == null)
                {
                    CsdPingShenRenAccounts = "";
                }
                if (EngPingShenRenAccounts == null)
                {
                    EngPingShenRenAccounts = "";
                }
                if (ScmPingShenRenAccounts == null)
                {
                    ScmPingShenRenAccounts = "";
                }
                if (QadPingShenRenAccounts == null)
                {
                    QadPingShenRenAccounts = "";
                }
                if (CidPingShenRenAccounts == null)
                {
                    CidPingShenRenAccounts = "";
                }
                if (PmcPingShenRenAccounts == null)
                {
                    PmcPingShenRenAccounts = "";
                }
                return CsdPingShenRenAccounts.TrimEnd(';') + EngPingShenRenAccounts.TrimEnd(';') +
                    ScmPingShenRenAccounts.TrimEnd(';') + QadPingShenRenAccounts.TrimEnd(';') + CidPingShenRenAccounts.TrimEnd(';') + PmcPingShenRenAccounts.TrimEnd(';');
            }
        }
    }
}
