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
        public bool needPingShen;

        public string csdPingShenRenAccounts;
        public string csdPingShenRenNames;
        public string engPingShenRenAccounts;
        public string engPingShenRenNames;
        public string scmPingShenRenAccounts;
        public string scmPingShenRenNames;
        public string qadPingShenRenAccounts;
        public string qadPingShenRenNames;
        public string cidPingShenRenAccounts;
        public string cidPingShenRenNames;
        public string pmcPingShenRenAccounts;
        public string pmcPingShenRenNames;

        public string pingShenStartDate;
        public string pingShenStartHours;
        public string pingShenStartMinutes;
        public string pingShenEndDate;
        public string pingShenEndHours;
        public string pingShenEndMinutes;
        public string pingShenPalce;
        public string pingShenContent;
    }
}