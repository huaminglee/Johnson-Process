﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.Core
{
    public enum FailureResult
    {
        None,

        /// <summary>
        /// 退货
        /// </summary>
        Return,

        /// <summary>
        /// 让步接收
        /// </summary>
        Receive,

        /// <summary>
        /// 返工
        /// </summary>
        Rework,

        /// <summary>
        /// 报废
        /// </summary>
        Scrap,

        /// <summary>
        /// 挑选
        /// </summary>
        Pick,

        /// <summary>
        /// MRB
        /// </summary>
        MRB,

        /// <summary>
        /// 其它
        /// </summary>
        OTHER
    }

    public class MrbFailureResult
    {
        public string UserAccount { set; get; }

        public string UserName { set; get; }

        public FailureResult Result { set; get; }

        public string ResultName
        {
            get
            {
                return FailureResultHelper.MapName(Result);
            }
        }
    }

    public class FailureProductForm
    {
        public string StartUserAccount { set; get; }

        public string StartUserName { set; get; }

        public DateTime StartTime { set; get; }

        public string StartDepartment { set; get; }

        public ProductType ProductType { set; get; }

        /// <summary>
        /// 不合品编号
        /// </summary>
        public string No { set; get; }
        /// <summary>
        /// 零件号
        /// </summary>
        public string ComponentCode { set; get; }

        /// <summary>
        /// 部件系列号
        /// </summary>
        public string BJXLH { set; get; }

        /// <summary>
        /// 机组系列号
        /// </summary>
        public string JZXLH { set; get; }

        /// <summary>
        /// 零件名称
        /// </summary>
        public string ComponentName { set; get; }

        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { set; get; }

        /// <summary>
        /// 责任部门
        /// </summary>
        public string ZRBM { set; get; }

        public string UM { set; get; }

        public string MO { set; get; }

        /// <summary>
        /// 供应商代码
        /// </summary>
        public string GYSDM { set; get; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string GYSMC { set; get; }

        public string FailurePlace { set; get; }

        public string Source { set; get; }

        public string Quantity { set; get; }

        public string Reason { set;get; }

        public string ReasonRemark { set; get; }
        /// <summary>
        /// 不合格品现象
        /// </summary>
        public string Remark { set; get; }

        public string PmcOpinion { set; get; }

        public string Level { set; get; }

        public FailureResult QEResult { set; get; }

        public FailureResult Result
        {
            get
            {
                if (this.QEResult != FailureResult.None && this.QEResult != FailureResult.MRB)
                {
                    return QEResult;
                }
                if (this.MrbResults != null)
                {
                    FailureResult finalResult = this.MrbResults[0].Result;
                    foreach (MrbFailureResult mrbResult in this.MrbResults)
                    {
                        if (mrbResult.Result != finalResult)
                        {
                            finalResult = FailureResult.None;
                            break;
                        }
                    }
                    if (finalResult != FailureResult.None)
                    {
                        return finalResult;
                    }
                }
                if (this.QAResult != FailureResult.None)
                {
                    return this.QAResult;
                }
                return FailureResult.None;
            }
        }

        public string SupplierDeal { set; get; }

        public string SupplierDealBillNumber { set; get; }

        public string ProduceDeal { set; get; }

        public string ProduceDealNumber { set; get; }

        public string Analysis { set; get; }

        public List<MrbFailureResult> MrbResults { set; get; }

        public FailureResult QAResult { set; get; }

        public string ReceiveQARemark { set; get; }

        public string QCValidateResult { set; get; }

        public string CidUserAccount { set; get; }

        public string CidUserName { set; get; }

        public string EngUserAccount { set; get; }

        public string EngUserName { set; get; }

        public string CsdUserAccount { set; get; }

        public string CsdUserName { set; get; }

        public string PmcUserAccount { set; get; }

        public string PmcUserName { set; get; }

        public string FinUserAccount { set; get; }

        public string FinUserName { set; get; }

        public string QCUserAccount { set; get; }

        public string QEUserAccount { set; get; }

        public string QEUserName { set; get; }

        public List<TaskApproveInfo> Approves { set; get; }
    }
}
