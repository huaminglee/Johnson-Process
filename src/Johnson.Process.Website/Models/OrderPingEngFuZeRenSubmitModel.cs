using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class OrderPingEngFuZeRenSubmitModel
    {
        public string taskId;
        public bool isStandard;
        public bool needEngineerPingShen;
        public string engEngineerAccount;
        public string engEngineerName;
        public string dianQiEngineerAccount;
        public string dianQiEngineerName;
        public DateTime? waiGouQingDanRiQi;
        public DateTime? sheJiWanChengRiQi;
        public string submitRemark;
    }
}