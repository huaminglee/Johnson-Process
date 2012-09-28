using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class ProductReworkCidModel
    {

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
        /// 工时
        /// </summary>
        public string GS { set; get; }

        public string submitRemark { set; get; }
    }
}