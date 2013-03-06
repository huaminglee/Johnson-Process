using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class ProductReworkQcModel
    {
        public string FailureNo { set; get; }

        public string EngUserAccount { set; get; }

        public string EngUserName { set; get; }

        public string PmcUserAccount { set; get; }

        public string PmcUserName { set; get; }

        public string FinUserAccount { set; get; }

        public string FinUserName { set; get; }

        public string QEUserAccount { set; get; }

        public string QEUserName { set; get; }

        public string CidUserAccount { set; get; }

        public string CidUserName { set; get; }

        /// <summary>
        /// 原因描述
        /// </summary>
        public string PZZTMS { set; get; }

        public string submitRemark { set; get; }
    }
}