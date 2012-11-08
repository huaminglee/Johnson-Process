using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class DeliveryModel
    {
        public DeliveryModel()
        {
        }

        public DeliveryModel(DeliveryProcessForm form)
        {
            List<UploadFileModel> fileModels = new List<UploadFileModel>();
            if(form.Files != null)
            {
                foreach(ProcessFile file in form.Files)
                {
                    fileModels.Add(new UploadFileModel(file));
                }
            }
            this.files = fileModels;

            List<MaterialModel> materialModels = new List<MaterialModel>();
            if (form.Materials != null)
            {
                foreach (Material material in form.Materials)
                {
                    materialModels.Add(new MaterialModel(material));
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
            this.remarks = remarks;

            this.bookDate = form.BookDate.ToString("yyyy-MM-dd");
            this.materials = materialModels;
            this.orderNumber = form.OrderNumber;
            this.projectName = form.ProjectName;
            this.requestOutDate = form.RequestOutDate.ToString("yyyy-MM-dd");
            this.saleEngineerYT = form.SaleEngineer;
            this.saleGroup = form.SaleGroup;
            this.saleOffice = form.SaleOffice;
            this.csdReply = form.CsdReply;
            this.logReply = form.LogReply;
            this.csdEngineer = form.CsdEngineerId;
            this.csdEngineerName = form.CsdEngineerName;
            this.logEngineer = form.LogEngineerId;
            this.logEngineerName = form.LogEngineerName;
        }

        public string orderNumber;
        public string projectName;
        public string saleOffice;
        public string saleGroup;
        public string saleEngineerYT;
        public string bookDate;
        public string requestOutDate;
        public string submitRemark;
        public string csdReply;
        public string logReply;

        public string csdEngineer;
        public string csdEngineerName;
        public string logEngineer;
        public string logEngineerName;

        public List<MaterialModel> materials;
        public List<UploadFileModel> files;
        public List<RemarkModel> remarks;

        public DeliveryProcessForm Map()
        {
            List<ProcessFile> files = new List<ProcessFile>();
            if(this.files != null)
            {
                foreach(UploadFileModel fileModel in this.files)
                {
                    files.Add(fileModel.Map());
                }
            }
            List<Material> materialList = new List<Material>();
            if(this.materials != null)
            {
                foreach(MaterialModel materialModel in this.materials)
                {
                    materialList.Add(materialModel.Map());
                }
            }

            DeliveryProcessForm task = new DeliveryProcessForm 
            { 
                BookDate = DateTime.Parse(this.bookDate),
                Files = files,
                Materials = materialList,
                OrderNumber = this.orderNumber,
                ProjectName = this.projectName,
                RequestOutDate = DateTime.Parse(this.requestOutDate),
                SaleGroup = this.saleGroup,
                SaleOffice = this.saleOffice,
                SaleEngineer = this.saleEngineerYT,
                CsdReply = this.csdReply,
                LogReply = this.logReply,
                CsdEngineerId = this.csdEngineer,
                CsdEngineerName = this.csdEngineerName,
                LogEngineerId = this.logEngineer,
                LogEngineerName = this.logEngineerName
            };

            return task;
        }
    }

    public class DeliveryStartModel : DeliveryModel
    {
        public DeliveryStartModel()
        {
        }

        public DeliveryStartModel(DeliveryProcessForm form)
            : base(form)
        {
            
        }
    }

    public class DeliveryCustomerServiceSendModel : DeliveryModel
    {
        public DeliveryCustomerServiceSendModel()
        {
        }

        public DeliveryCustomerServiceSendModel(DeliveryProcessForm form)
            : base(form)
        {

        }
        public bool needLogisticsReply;
    }
}