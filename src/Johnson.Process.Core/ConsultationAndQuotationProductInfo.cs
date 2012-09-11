using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.Core
{
    public class ConsultationAndQuotationProductInfo
    {
        public string ProductModel { set; get; }
        public int Quantity { set; get; }
        public string Remark { set; get; }
        public float? MarketingWithoutSalesTP { set; get; }
        public float? MarketingWithSalesTP { set; get; }
        public float? MarketingTotalSalesTP { set; get; }
        public float? CsdWithoutSalesTP { set; get; }
        public float? CsdWithSalesTP { set; get; }
        public float? CsdTotalSalesTP { set; get; }
    }
}
