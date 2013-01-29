using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.Core
{
    public class OrderPingShenStartInfo
    {
        public string StartUserAccount { set; get; }

        public string StartUserName { set; get; }

        public string Level { set; get; }

        public DateTime JiaoHuoRiQi { set; get; }

        public string SONO { set; get; }

        public string JDSNO { set; get; }

        public string TuZiQueRen { set; get; }

        public string XiangMingCheng { set; get; }

        public string BanShiChu { set; get; }

        public string BanShiChuLianXiRen { set; get; }

        public bool IsStandard { set; get; }

        public string ChanPinLeiXing { set; get; }

        public string SapItem { set; get; }

        public string SapMaterial { set; get; }

        public int ShuLiang { set; get; }

        public string JiShuYaoQiu { set; get; }

        public string BeiZhu { set; get; }

        public string QiTaYaoQiuShuoMing { set; get; }

        public string SheJiFuZeRenAccount { set; get; }

        public string SheJiFuZeRenName { set; get; }

        public string PmcEngineerAccount { set; get; }

        public string PmcEngineerName { set; get; }

        public string TaskId { set; get; }

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

        public List<ProcessFile> Files { set; get; }

        public List<OrderPingshenItemInfo> Items { set; get; }

        public TaskApproveInfo ApproveInfo { set; get; }
    }
}
