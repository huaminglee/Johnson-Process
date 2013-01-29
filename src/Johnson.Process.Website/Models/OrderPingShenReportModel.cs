﻿using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;
using Ultimus.WFServer;

namespace Johnson.Process.Website.Models
{
    public class OrderPingShenReportModel
    {
        public OrderPingShenReportModel()
        {

        }

        public OrderPingShenReportModel(ProcessForm<OrderPingShenForm> processForm)
        {
            OrderPingShenForm form = processForm.Form;
            this.startUserName = form.StartUserName;
            this.startTime = form.StartTime.ToString("yyyy-MM-dd");

            this.banShiChu = form.BanShiChu;
            this.banShiChuLianXiRen = form.BanShiChuLianXiRen;
            this.beiZhu = form.BeiZhu;
            this.chanPinLeiXing = form.ChanPinLeiXing;
            this.isStandard = form.IsStandard ? "是" : "否";
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
#if DEBUG
            this.taskStatus = 1;
#else
            Task task = WebHelper.OrderPingShenProcess.GetStartTask(processForm.IncidentNo);
            this.taskId = task.strTaskId;
            this.incidentNo = task.nIncidentNo;
            this.taskStatus = WebHelper.OrderPingShenProcess.GetIncidentStatus(task.nIncidentNo);
#endif
        }

        public string startUserName;
        public string startTime;
        public string taskId;
        public int incidentNo;
        public int taskStatus;

        public string level;
        public string jiaoHuoRiQi;
        public string SONO;
        public string JDSNO;
        public string tuZiQueRen;
        public string xiangMingCheng;
        public string banShiChu;
        public string banShiChuLianXiRen;
        public string isStandard;
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
    }
}