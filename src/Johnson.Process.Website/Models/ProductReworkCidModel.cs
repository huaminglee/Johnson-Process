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
        /// 工时计算
        /// </summary>
        public string GSJS { set; get; }

        /// <summary>
        /// 钣金
        /// </summary>
        public string BanJin { set; get; }

        /// <summary>
        /// 装配线
        /// </summary>
        public string ZPX { set; get; }

        /// <summary>
        /// 工艺备注
        /// </summary>
        public string HYBZ { set; get; }

        public string submitRemark { set; get; }
    }
}