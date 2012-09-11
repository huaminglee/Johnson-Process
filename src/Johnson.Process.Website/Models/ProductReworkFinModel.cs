using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class ProductReworkFinModel
    {

        /// <summary>
        /// 工时费用
        /// </summary>
        public double GSFY { set; get; }

        /// <summary>
        /// 物料费用
        /// </summary>
        public double WLFY { set; get; }

        /// <summary>
        /// 总费用
        /// </summary>
        public double ZFY { set; get; }

        public string submitRemark { set; get; }
    }
}