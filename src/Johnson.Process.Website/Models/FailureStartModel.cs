using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class FailureStartModel
    {
        public ProductType ProductType { set; get; }

        public string No { set; get; }
        /// <summary>
        /// 部件系列号
        /// </summary>
        public string BJXLH { set; get; }

        /// <summary>
        /// 机组系列号
        /// </summary>
        public string JZXLH { set; get; }

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

        public string ComponentCode { set; get; }

        public string ComponentName { set; get; }

        public string OrderCode { set; get; }

        public string FailurePlace { set; get; }

        public string Source { set; get; }

        public string Quantity { set; get; }

        public string Reason { set; get; }

        public string ReasonRemark { set; get; }

        public string Remark { set; get; }

        public string PmcUserAccount { set; get; }

        public string PmcUserName { set; get; }

        public string QEUserAccount { set; get; }

        public string QEUserName { set; get; }

        public string submitRemark { set; get; }

        public string emailTo { set; get; }
    }
}