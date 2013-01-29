using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class OrderPingShenModel
    {

        public OrderPingShenModel()
        {

        }

        public OrderPingShenModel(OrderPingShenForm form)
        {
            this.banShiChu = form.BanShiChu;
            this.banShiChuLianXiRen = form.BanShiChuLianXiRen;
            this.beiZhu = form.BeiZhu;
            this.chanPinLeiXing = form.ChanPinLeiXing;
            this.isStandard = form.IsStandard;
            this.JDSNO = form.JDSNO;
            this.jiaoHuoRiQi = form.JiaoHuoRiQi.ToString("yyyy-MM-dd");
            this.jiShuYaoQiu = form.JiShuYaoQiu;
            this.level = form.Level;
            this.pmcEngineerAccount = form.PmcEngineerAccount;
            this.pmcEngineerName = form.PmcEngineerName;
            this.qiTaYaoQiuShuoMing = form.QiTaYaoQiuShuoMing;
            this.sapItem = form.SapItem;
            this.sapMaterial = form.SapMaterial;
            this.sheJiFuZeRenAccount = form.SheJiFuZeRenAccount;
            this.sheJiFuZeRenName = form.SheJiFuZeRenName;
            this.shuLiang = form.ShuLiang;
            this.SONO = form.SONO;
            this.startUserName = form.StartUserName;
            this.tuZiQueRen = form.TuZiQueRen;
            this.xiangMingCheng = form.XiangMingCheng;

            this.engEngineerAccount = form.EngEngineerAccount;
            this.engEngineerName = form.EngEngineerName;
            this.dianQiEngineerAccount = form.DianQiEngineerAccount;
            this.dianQiEngineerName = form.DianQiEngineerName;
            if (form.WaiGouWanChengRiQi.HasValue)
            {
                this.waiGouWanChengRiQi = form.WaiGouWanChengRiQi.Value.ToString("yyyy-MM-dd");
            }
            if (form.SheJiWanChengRiQi.HasValue)
            {
                this.sheJiWanChengRiQi = form.SheJiWanChengRiQi.Value.ToString("yyyy-MM-dd");
            }
            if (form.DianQiWanChengRiQi.HasValue)
            {
                this.dianQiWanChengRiQi = form.DianQiWanChengRiQi.Value.ToString("yyyy-MM-dd");
            }
            if (form.ShouCiWanChengRiQi.HasValue)
            {
                this.shouCiWanChengRiQi = form.ShouCiWanChengRiQi.Value.ToString("yyyy-MM-dd");
            }
            if (form.ZhengJiWanChengRiQi.HasValue)
            {
                this.zhengJiWanChengRiQi = form.ZhengJiWanChengRiQi.Value.ToString("yyyy-MM-dd");
            }
            if (form.WuLiaoDaoHuoRiQi.HasValue)
            {
                this.wuLiaoDaoHuoRiQi = form.WuLiaoDaoHuoRiQi.Value.ToString("yyyy-MM-dd");
            }
            if (form.JiZuWanGongRiQi.HasValue)
            {
                this.jiZuWanGongRiQi = form.JiZuWanGongRiQi.Value.ToString("yyyy-MM-dd");
            }
            this.fafangWancheng = form.FafangWancheng.ToString().ToLower();
            this.files = form.Files;
            this.items = form.Items;
            this.approves = form.Approves;
            this.hasXinWuLiao = form.HasXinWuLiao;
            this.jianChaEngineerAccount = form.JianChaEngineerAccount;
            this.jianChaEngineerName = form.JianChaEngineerAccount;
            this.zhuGuanAccount = form.ZhuGuanAccount;
            this.zhuGuanName = form.ZhuGuanName;
            this.sheJiShuoMing = form.SheJiShuoMing;
            this.sheJiZiLiao = form.SheJiZiLiao;
            this.cidZiLiao = form.CidZiLiao;
            this.qadZiLiao = form.QadZiLiao; 
            if(form.WenjianFafangForms != null)
            {
                this.wanjianFafangList = new List<OrderWenjianFafangReportModel>();
                foreach (OrderWenjianFafangForm wenjianFafangForm in form.WenjianFafangForms)
                {
                    this.wanjianFafangList.Add(new OrderWenjianFafangReportModel(wenjianFafangForm));
                }
            }
        }

        public string startUserName;
        public string level;
        public string jiaoHuoRiQi;
        public string SONO;
        public string JDSNO;
        public string tuZiQueRen;
        public string xiangMingCheng;
        public string banShiChu;
        public string banShiChuLianXiRen;
        public bool isStandard;
        public string chanPinLeiXing;
        public string sapItem;
        public string sapMaterial;
        public int? shuLiang;
        public string jiShuYaoQiu;
        public string beiZhu;
        public string qiTaYaoQiuShuoMing;
        public string sheJiFuZeRenAccount;
        public string sheJiFuZeRenName;
        public string pmcEngineerAccount;
        public string pmcEngineerName;
        public bool needPingShen;

        public string engEngineerAccount;
        public string engEngineerName;
        public string dianQiEngineerAccount;
        public string dianQiEngineerName;
        public string waiGouWanChengRiQi;
        public string sheJiWanChengRiQi;
        public string dianQiWanChengRiQi;
        public string wuLiaoDaoHuoRiQi;
        public string jiZuWanGongRiQi;

        public string shouCiWanChengRiQi;
        public string zhengJiWanChengRiQi;

        public List<ProcessFile> files;
        public List<OrderPingshenItemInfo> items;

        public bool hasXinWuLiao;
        public string jianChaEngineerAccount;
        public string jianChaEngineerName;
        public string zhuGuanAccount;
        public string zhuGuanName;
        public string sheJiShuoMing;
        public string fafangWancheng;
        public List<ProcessFile> sheJiZiLiao;
        public List<ProcessFile> cidZiLiao;
        public List<ProcessFile> qadZiLiao;

        public List<OrderWenjianFafangReportModel> wanjianFafangList { set; get; }
        public List<TaskApproveInfo> approves;
    }
}