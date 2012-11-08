using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.Core
{
    public class DeliveryProcessForm
    {
        public string ApplyUserName { set; get; }

        public DateTime ApplyTime { set; get; }

        public string OrderNumber { set; get; }

        public string ProjectName { set; get; }

        public string SaleEngineer { set; get; }

        public string SaleOffice { set; get; }
        
        public string SaleGroup { set; get; }

        public string CsdReply { set; get; }

        public string LogReply { set; get; }

        public string CsdEngineerName { set; get; }

        public string CsdEngineerId { set; get; }

        public string LogEngineerName { set; get; }

        public string LogEngineerId { set; get; }

        public DateTime BookDate { set; get; }

        public DateTime RequestOutDate { set; get; }

        public List<Material> Materials { set; get; }

        public List<ProcessFile> Files { set; get; }

        public List<TaskApproveInfo> Approves { set; get; }
    }
}
