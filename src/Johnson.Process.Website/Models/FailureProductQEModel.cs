using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class FailureProductQEModel
    {
        public string Level { set; get; }

        public FailureResult QEResult { set; get; }

        public string SupplierDeal { set; get; }

        public string SupplierDealBillNumber { set; get; }

        public string ProduceDeal { set; get; }

        public string ProduceDealNumber { set; get; }

        public string Analysis { set; get; }

        public string StorehouseUserAccount { set; get; }

        public string StorehouseUserName { set; get; }

        public string CidUserAccount { set; get; }

        public string CidUserName { set; get; }

        public string EngUserAccount { set; get; }

        public string EngUserName { set; get; }

        public string CsdUserAccount { set; get; }

        public string CsdUserName { set; get; }

        public string FinUserAccount { set; get; }

        public string FinUserName { set; get; }

        public string emailTo { set; get; }

        public string submitRemark { set; get; }
    }
}