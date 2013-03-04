using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class ProductReworkQC2Model
    {

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

        public string emailTo { set; get; }

        public string submitRemark { set; get; }
    }
}