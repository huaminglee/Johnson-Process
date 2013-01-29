using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class OrderWenjianFafangModel
    {
        public OrderWenjianFafangModel()
        {

        }

        public OrderWenjianFafangModel(OrderPingShenForm form)
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
            this.files = form.Files;
            this.items = form.Items;
        }

        public OrderWenjianFafangModel(OrderPingShenForm orderPingShenForm, OrderWenjianFafangForm orderWenjianFafangForm)
            :this(orderPingShenForm)
        {
            this.startUserName = orderWenjianFafangForm.StartUserName;
            this.approves = orderWenjianFafangForm.Approves;
            this.jianChaEngineerAccount = orderWenjianFafangForm.JianChaEngineerAccount;
            this.jianChaEngineerName = orderWenjianFafangForm.JianChaEngineerAccount;
            this.zhuGuanAccount = orderWenjianFafangForm.ZhuGuanAccount;
            this.zhuGuanName = orderWenjianFafangForm.ZhuGuanName;
            this.sheJiShuoMing = orderWenjianFafangForm.SheJiShuoMing;
            this.fafangWancheng = orderWenjianFafangForm.FafangWancheng.ToString().ToLower();
            this.sheJiZiLiao = orderWenjianFafangForm.SheJiZiLiao;
            this.hasXinWuLiao = orderWenjianFafangForm.HasXinWuLiao;
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

        public List<TaskApproveInfo> approves;
    }
}