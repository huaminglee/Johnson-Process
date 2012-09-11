using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class ConsultationAndQuotationModel
    {
        public ConsultationAndQuotationModel()
        {
        }

        public ConsultationAndQuotationModel(ConsultationAndQuotationForm form)
        {
            List<UploadFileModel> fileModels = new List<UploadFileModel>();
            if(form.Files != null)
            {
                foreach(ProcessFile file in form.Files)
                {
                    fileModels.Add(new UploadFileModel(file));
                }
            }

            List<ConsultationAndQuotationProductModel> products = new List<ConsultationAndQuotationProductModel>();
            if (form.Products != null)
            {
                foreach (ConsultationAndQuotationProductInfo product in form.Products)
                {
                    products.Add(new ConsultationAndQuotationProductModel(product));
                }
            }

            List<RemarkModel> remarks = new List<RemarkModel>();
            if (form.Approves != null)
            {
                foreach (TaskApproveInfo approveInfo in form.Approves)
                {
                    remarks.Add(new RemarkModel(approveInfo));
                }
            }

            this.applyTime = form.ApplyTime.ToString("yyyy-MM-dd");
            this.applyUserDepartmentName = form.ApplyUserDepartmentName;
            this.applyUserName = form.ApplyUserName;
            this.expectSignContact = form.ExpectSignContact.ToString("yyyy-MM-dd");
            this.files = fileModels;
            this.products = products;
            this.projectName = form.ProjectName;
            this.succeedProbability = form.SucceedProbability;
            this.remarks = remarks;
            this.marketingEngineer = form.MarketingEngineer;
            this.marketingReply = form.MarketingReply;
            this.engReply = form.EngReply;
            this.scmReply = form.ScmReply;
            this.logReply = form.LogReply;
            this.qadReply = form.QadReply;
            this.cidReply = form.CidReply;
            this.csdReply = form.CsdReply;
            this.csdEngineerAccount = form.CsdEngineerAccount;
            this.csdEngineerName = form.CsdEngineerName;
            this.csdTracerAccount = form.CsdTracerAccount;
            this.csdTracerName = form.CsdTracerName;
            this.leadTime = form.LeadTime;
            this.leadTimeRemark = form.LeadTimeRemark;
        }

        public string applyUserName;
        public string applyUserDepartmentName;
        public string applyTime;
        public string projectName;
        public string succeedProbability;
        public string expectSignContact;
        public string marketingEngineer;

        public string marketingReply;
        public string engReply;
        public string scmReply;
        public string logReply;
        public string qadReply;
        public string cidReply;
        public string csdReply;

        public string csdTracerName;
        public string csdTracerAccount;
        public string csdEngineerName;
        public string csdEngineerAccount;

        public int? leadTime;
        public string leadTimeRemark;

        public List<ConsultationAndQuotationProductModel> products;
        public List<UploadFileModel> files;
        public List<RemarkModel> remarks;

        public string submitRemark;

        public ConsultationAndQuotationForm Map()
        {
            List<ProcessFile> files = new List<ProcessFile>();
            if (this.files != null)
            {
                foreach (UploadFileModel fileModel in this.files)
                {
                    files.Add(fileModel.Map());
                }
            }
            List<ConsultationAndQuotationProductInfo> productList = new List<ConsultationAndQuotationProductInfo>();
            if (this.products != null)
            {
                foreach (ConsultationAndQuotationProductModel productModel in this.products)
                {
                    productList.Add(productModel.Map());
                }
            }

            ConsultationAndQuotationForm from = new ConsultationAndQuotationForm
            {
                ApplyTime = DateTime.Parse(this.applyTime),
                ApplyUserDepartmentName = this.applyUserDepartmentName,
                ApplyUserName = this.applyUserName,
                ExpectSignContact = DateTime.Parse(this.expectSignContact),
                Files = files,
                Products = productList,
                ProjectName = this.projectName,
                SucceedProbability = this.succeedProbability,
                MarketingEngineer = this.marketingEngineer,
                MarketingReply  = this.marketingReply,
                EngReply = this.engReply,
                ScmReply = this.scmReply,
                LogReply = this.logReply,
                QadReply = this.qadReply,
                CidReply = this.cidReply,
                CsdReply = this.csdReply,
                LeadTime = this.leadTime,
                CsdEngineerAccount = this.csdEngineerAccount,
                CsdEngineerName = this.csdEngineerName,
                CsdTracerAccount = this.csdTracerAccount,
                CsdTracerName = this.csdTracerName,
                LeadTimeRemark = this.leadTimeRemark
            };

            return from;
        }
    }

    public class ConsultationAndQuotationStartModel : ConsultationAndQuotationModel
    {
        
    }
}