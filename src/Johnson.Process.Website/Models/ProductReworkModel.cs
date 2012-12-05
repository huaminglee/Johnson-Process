using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class ProductReworkModel
    {
        public ProductReworkModel()
        {

        }

        public ProductReworkModel(FailureProductForm form)
        {
            this.FailureNo = form.No;
            this.Name = form.ComponentName;
            this.SapNo = form.ComponentCode;
            this.ProductType = form.ProductType;
            if (form.ProductType == Core.ProductType.LJ)
            {
                this.XLH = form.BJXLH;
            }
            else
            {
                this.XLH = form.JZXLH;
            }
            this.Quantity = form.Quantity;
            this.OrderNumber = form.OrderCode;
            this.StartDepartment = form.StartDepartment;
            this.ProductArea = form.FailurePlace;
            this.Approves = form.Approves;
        }

        public ProductReworkModel(ProductReworkForm form)
        {
            this.FailureNo = form.FailureNo;
            this.StartDepartment = form.StartDepartment;
            this.Approves = form.Approves;
            this.CBCLYJ = form.CBCLYJ;
            this.CBCLYJBZ = form.CBCLYJBZ;
            this.CidUserAccount = form.CidUserAccount;
            this.CidUserName = form.CidUserName;
            this.Code = form.Code;
            this.CompletedTime = form.CompletedTime.ToString("yyyy-MM-dd");
            this.EngUserAccount = form.EngUserAccount;
            this.EngUserName = form.EngUserName;
            this.FGJG = form.FGJG;
            this.FGMD = form.FGMD;
            this.FinUserAccount = form.FinUserAccount;
            this.FinUserName = form.FinUserName;
            this.FYCD = form.FYCD;
            this.FYCDZ = form.FYCDZ;
            this.FYQRR = form.FYQRR;
            if (form.GSFY > 0)
            {
                this.GSFY = form.GSFY.ToString();
            }
            this.GS = form.GS;
            this.GSLX = form.GSLX;
            this.GYFA = form.GYFA;
            this.Materials = form.Materials;
            this.Name = form.Name;
            this.OrderNumber = form.OrderNumber;
            this.PmcUserAccount = form.PmcUserAccount;
            this.PmcUserName = form.PmcUserName;
            this.ProductArea = form.ProductArea;
            this.ProductType = form.ProductType;
            this.PZZTMS = form.PZZTMS;
            this.QCUserAccount = form.QCUserAccount;
            this.QCUserName = form.QCUserName;
            this.QEUserAccount = form.QEUserAccount;
            this.QEUserName = form.QEUserName;
            this.Quantity = form.Quantity;
            this.SapNo = form.SapNo;
            this.Source = form.Source;
            this.SPDH = form.SPDH;
            if (form.WLFY > 0)
            {
                this.WLFY = form.WLFY.ToString();
            }
            this.XLH = form.XLH;
            this.YYMS = form.YYMS;
            if (form.ZFY > 0)
            {
                this.ZFY = form.ZFY.ToString();
            }
            this.Files = form.Files;
            this.CidFiles = form.CidFiles;
            this.EngFiles = form.EngFiles;
            this.QADFAQR = form.QADFAQR;
            this.WLJHAP = form.WLJHAP;
            this.SCJHAP = form.SCJHAP;
            this.FGJGBZ = form.FGJGBZ;
            this.XGCLDH = form.XGCLDH;
        }

        public ProductType ProductType { set; get; }

        public string FailureNo { set; get; }

        /// <summary>
        /// 系列号
        /// </summary>
        public string XLH { set; get; }

        /// <summary>
        /// 返工返修单号
        /// </summary>
        public string Code { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// sap no
        /// </summary>
        public string SapNo { set; get; }

        /// <summary>
        /// 数量
        /// </summary>
        public string Quantity { set; get; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNumber { set; get; }

        /// <summary>
        /// 发出部门
        /// </summary>
        public string StartDepartment { set; get; }

        /// <summary>
        /// 产品所在地
        /// </summary>
        public string ProductArea { set; get; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public string CompletedTime { set; get; }

        /// <summary>
        /// 返工品来源
        /// </summary>
        public string Source { set; get; }

        /// <summary>
        /// 费用承担
        /// </summary>
        public string FYCD { set; get; }

        /// <summary>
        /// 费用承担者
        /// </summary>
        public string FYCDZ { set; get; }

        /// <summary>
        /// 索赔单号
        /// </summary>
        public string SPDH { set; get; }

        /// <summary>
        /// 确认人
        /// </summary>
        public string FYQRR { set; get; }

        /// <summary>
        /// 原因描述
        /// </summary>
        public string YYMS { set; get; }

        /// <summary>
        /// 返工目的
        /// </summary>
        public string FGMD { set; get; }

        /// <summary>
        /// 品质状态描述
        /// </summary>
        public string PZZTMS { set; get; }

        /// <summary>
        /// 初步处理意见
        /// </summary>
        public string CBCLYJ { set; get; }

        /// <summary>
        /// 初步处理意见备注
        /// </summary>
        public string CBCLYJBZ { set; get; }

        /// <summary>
        /// 返工所需物料
        /// </summary>
        public List<ProductReworkMaterials> Materials { set; get; }

        /// <summary>
        /// eng文件
        /// </summary>
        public List<ProcessFile> EngFiles { set; get; }

        /// <summary>
        /// 工艺方案
        /// </summary>
        public List<ProductReworkFGFA> GYFA { set; get; }

        /// <summary>
        /// cid文件
        /// </summary>
        public List<ProcessFile> CidFiles { set; get; }

        /// <summary>
        /// 工时类型
        /// </summary>
        public string GSLX { set; get; }

        /// <summary>
        /// 工时计算
        /// </summary>
        public string GS { set; get; }

        /// <summary>
        /// QAD方案确认
        /// </summary>
        public string QADFAQR { set; get; }

        /// <summary>
        /// 物料计划安排
        /// </summary>
        public string WLJHAP { set; get; }

        /// <summary>
        /// 生产计划安排
        /// </summary>
        public string SCJHAP { set; get; }

        /// <summary>
        /// 返工结果
        /// </summary>
        public string FGJG { set; get; }

        /// <summary>
        /// 返工结果备注
        /// </summary>
        public string FGJGBZ { set; get; }

        /// <summary>
        /// 相关处理单号
        /// </summary>
        public string XGCLDH { set; get; }

        /// <summary>
        /// 工时费用
        /// </summary>
        public string GSFY { set; get; }

        /// <summary>
        /// 物料费用
        /// </summary>
        public string WLFY { set; get; }

        /// <summary>
        /// 总费用
        /// </summary>
        public string ZFY { set; get; }

        public string EngUserAccount { set; get; }

        public string EngUserName { set; get; }

        public string PmcUserAccount { set; get; }

        public string PmcUserName { set; get; }

        public string FinUserAccount { set; get; }

        public string FinUserName { set; get; }

        public string QCUserAccount { set; get; }

        public string QCUserName { set; get; }

        public string QEUserAccount { set; get; }

        public string QEUserName { set; get; }

        public string CidUserAccount { set; get; }

        public string CidUserName { set; get; }

        public List<TaskApproveInfo> Approves { set; get; }

        /// <summary>
        /// 附件
        /// </summary>
        public List<ProcessFile> Files { set; get; }
    }
}