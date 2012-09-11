using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class ConsultationAndQuotationProductModel
    {
        public ConsultationAndQuotationProductModel()
        {
        }

        public ConsultationAndQuotationProductModel(ConsultationAndQuotationProductInfo info)
        {
            this.productModel = info.ProductModel;
            this.quantity = info.Quantity;
            this.remark = info.Remark;
            this.marketingWithoutSalesTP = info.MarketingWithoutSalesTP;
            this.marketingWithSalesTP = info.MarketingWithSalesTP;
            this.marketingTotalSalesTP = info.MarketingTotalSalesTP;
            this.csdWithoutSalesTP = info.CsdWithoutSalesTP;
            this.csdWithSalesTP = info.CsdWithSalesTP;
            this.csdTotalSalesTP = info.CsdTotalSalesTP;
        }

        public string productModel;
        public int quantity;
        public string remark;
        public float? marketingWithoutSalesTP;
        public float? marketingWithSalesTP;
        public float? marketingTotalSalesTP;

        public float? csdWithoutSalesTP;
        public float? csdWithSalesTP;
        public float? csdTotalSalesTP;

        public ConsultationAndQuotationProductInfo Map()
        {
            return new ConsultationAndQuotationProductInfo
            {
                ProductModel = this.productModel,
                Quantity = this.quantity,
                Remark = this.remark,
                MarketingWithoutSalesTP = this.marketingWithoutSalesTP,
                MarketingWithSalesTP = this.marketingWithSalesTP,
                MarketingTotalSalesTP = this.marketingTotalSalesTP,
                CsdWithoutSalesTP = this.csdWithoutSalesTP,
                CsdWithSalesTP = this.csdWithSalesTP,
                CsdTotalSalesTP = this.csdTotalSalesTP
            };
        }
    }
}