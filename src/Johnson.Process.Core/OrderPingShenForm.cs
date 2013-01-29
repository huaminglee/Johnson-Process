using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.Core
{
    public class OrderPingShenForm
    {
        public string StartUserName { set; get; }

        public string StartUserAccount { set; get; }

        public DateTime StartTime { set; get; }

        public string DeptPingShenResult { set; get; }

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

        public string EngEngineerAccount { set; get; }

        public string EngEngineerName { set; get; }

        public string DianQiEngineerAccount { set; get; }

        public string DianQiEngineerName { set; get; }

        public string ScmEngineerAccount { set; get; }

        public DateTime? WaiGouWanChengRiQi { set; get; }

        public DateTime? SheJiWanChengRiQi { set; get; }

        public DateTime? DianQiWanChengRiQi { set; get; }

        public DateTime? ShouCiWanChengRiQi { set; get; }

        public DateTime? ZhengJiWanChengRiQi { set; get; }

        public DateTime? WuLiaoDaoHuoRiQi { set; get; }

        public DateTime? JiZuWanGongRiQi { set; get; }

        public List<OrderPingshenItemInfo> Items { set; get; }

        public List<ProcessFile> Files { set; get; }

        public List<TaskApproveInfo> Approves { set; get; }

        public bool PingshenWancheng { set; get; }

        public bool HasXinWuLiao { set; get; }

        public string JianChaEngineerAccount { set; get; }

        public string JianChaEngineerName { set; get; }

        public string ZhuGuanAccount { set; get; }

        public string ZhuGuanName { set; get; }

        public string SheJiShuoMing{set;get;}

        public bool FafangWancheng { set; get; }

        public List<ProcessFile> SheJiZiLiao { set; get; }

        public List<ProcessFile> CidZiLiao { set; get; }

        public List<ProcessFile> QadZiLiao { set; get; }

        public List<OrderWenjianFafangForm> WenjianFafangForms { set; get; }
    }
}
