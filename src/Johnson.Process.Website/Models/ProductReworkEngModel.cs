using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Website.UserControls;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class ProductReworkEngModel
    {
        /// <summary>
        /// 返工所需物料
        /// </summary>
        public List<ProductReworkMaterials> Materials { set; get; }

        /// <summary>
        /// eng文件
        /// </summary>
        public List<ProcessFile> EngFiles { set; get; }

        public string submitRemark { set; get; }
    }
}