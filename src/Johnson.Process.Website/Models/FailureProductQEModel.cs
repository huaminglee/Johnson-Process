using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class FailureProductQEModel
    {
        public FailureLevel Level { set; get; }

        public FailureResult QEResult { set; get; }

        public FailureSupplierDeal SupplierDeal { set; get; }

        public string SupplierDealBillNumber { set; get; }

        public FailureProduceDeal ProduceDeal { set; get; }

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

        public string ReworkPmcUserAccount { set; get; }

        public string ReworkPmcUserName { set; get; }

        public string FinUserAccount { set; get; }

        public string FinUserName { set; get; }

        public string QCUserAccount { set; get; }

        public string QCUserName { set; get; }

        public bool needMRB { set; get; }

        public string submitRemark { set; get; }
    }
}