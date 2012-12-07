using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class OrderPingShenStartModel
    {
        public OrderPingShenStartModel()
        {
        }

        public OrderPingShenStartModel(OrderPingShenForm form)
        {
            this.banShiChu = form.BanShiChu;
            this.banShiChuLianXiRen = form.BanShiChuLianXiRen;
            this.beiZhu = form.BeiZhu;
            this.chanPinLeiXing = form.ChanPinLeiXing;
            this.isStandard = form.IsStandard;
            this.JDSNO = form.JDSNO;
            this.jiaoHuoRiQi = form.JiaoHuoRiQi;
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
            this.files = form.Files;
        }

        public string taskId;

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

        public string csdPingShenRenAccounts;
        public string csdPingShenRenNames;
        public string engPingShenRenAccounts;
        public string engPingShenRenNames;
        public string scmPingShenRenAccounts;
        public string scmPingShenRenNames;
        public string qadPingShenRenAccounts;
        public string qadPingShenRenNames;
        public string cidPingShenRenAccounts;
        public string cidPingShenRenNames;
        public string pmcPingShenRenAccounts;
        public string pmcPingShenRenNames;

        public string pingShenStartDate;
        public string pingShenStartHours;
        public string pingShenStartMinutes;
        public string pingShenEndDate;
        public string pingShenEndHours;
        public string pingShenEndMinutes;
        public string pingShenPalce;
        public string pingShenContent;

        public List<ProcessFile> files;

        public string submitRemark;
    }
}