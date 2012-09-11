using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.Core
{
    [Flags]
    public enum FailureSource
    {
        /// <summary>
        /// 制程不合格
        /// </summary>
        Manufacture,

        /// <summary>
        /// 来料不合格
        /// </summary>
        Material,

        /// <summary>
        /// 成品不合格
        /// </summary>
        Product,

        /// <summary>
        /// 客户退回
        /// </summary>
        Return,
    }

    [Flags]
    public enum FailureReason 
    {
        /// <summary>
        /// 设计
        /// </summary>
        A,

        /// <summary>
        /// 工艺
        /// </summary>
        B,

        /// <summary>
        /// 合同变更
        /// </summary>
        C,

        /// <summary>
        /// 采购计划
        /// </summary>
        D,

        /// <summary>
        /// 生产计划
        /// </summary>
        E,

        /// <summary>
        /// 图纸计划
        /// </summary>
        F,

        /// <summary>
        /// 来料质量
        /// </summary>
        G,

        /// <summary>
        /// 工装设备
        /// </summary>
        H,

        /// <summary>
        /// 操作失误
        /// </summary>
        I,

        /// <summary>
        /// 厂内搬运存储
        /// </summary>
        J,

        /// <summary>
        /// 场外运输
        /// </summary>
        K,

        /// <summary>
        /// 其他
        /// </summary>
        L
    }

    public enum FailureLevel
    {
        Critical,

        Major,

        Minor
    }

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
        Pick
    }

    public enum FailureSupplierDeal
    {
        ReportOf8D,

        ClaimIndemnity
    }

    public enum FailureProduceDeal
    {
        Rework,

        ReportOf8D
    }

    public class MrbFailureResult
    {
        public string UserAccount { set; get; }

        public string UserName { set; get; }

        public FailureResult Result { set; get; }
    }

    public class FailureProductForm
    {
        public string ComponentCode { set; get; }

        public string ComponentName { set; get; }

        public string OrderCode { set; get; }

        public string FailurePlace { set; get; }

        public string Supplier { set; get; }

        public FailureSource Source { set; get; }

        public string Quantity { set; get; }

        public FailureReason Reason { set;get; }

        public string ReasonRemark { set; get; }
        /// <summary>
        /// 不合格品现象
        /// </summary>
        public string Remark { set; get; }

        public string PmcOpinion { set; get; }

        public FailureLevel Level { set; get; }

        public FailureResult QEResult { set; get; }

        public FailureSupplierDeal SupplierDeal { set; get; }

        public string SupplierDealBillNumber { set; get; }

        public FailureProduceDeal ProduceDeal { set; get; }

        public string ProduceDealNumber { set; get; }

        public string Analysis { set; get; }

        public List<MrbFailureResult> MrbResults { set; get; }

        public FailureResult QAResult { set; get; }

        public string ReceiveQARemark { set; get; }

        public string QCValidateResult { set; get; }

        public string StorehouseUserAccount { set; get; }

        public string StorehouseUserName { set; get; }

        public string CidUserAccount { set; get; }

        public string CidUserName { set; get; }

        public string EngUserAccount { set; get; }

        public string EngUserName { set; get; }

        public string CsdUserAccount { set; get; }

        public string CsdUserName { set; get; }

        public string PmcUserAccount { set; get; }

        public string PmcUserName { set; get; }

        public string ReworkPmcUserAccount { set; get; }

        public string ReworkPmcUserName { set; get; }

        public string FinUserAccount { set; get; }

        public string FinUserName { set; get; }

        public string QCUserAccount { set; get; }

        public string QCUserName { set; get; }

        public string QEUserAccount { set; get; }

        public string QEUserName { set; get; }

        public List<TaskApproveInfo> Approves { set; get; }
    }
}
