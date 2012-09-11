using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class VocActionModel
    {
        public VocActionModel()
        {

        }

        public VocActionModel(VocAction act)
        {
            this.code = act.Code;
            this.remark = act.Remark;
            this.result = act.Result;
            this.resultFileName = act.ResultFileName;
            this.resultFileId = act.ResultFileId;
            if (act.StartDate.HasValue)
            {
                this.startDate = act.StartDate.Value.ToString("yyyy-MM-dd");
            }
            if (act.EndDate.HasValue)
            {
                this.endDate = act.EndDate.Value.ToString("yyyy-MM-dd");
            }
            this.userAccount = act.UserAccount;
            this.userName = act.UserName;
        }

        public string code { set; get; }

        public string remark { set; get; }

        public string result { set; get; }

        public string resultFileName { set; get; }

        public int resultFileId { set; get; }

        public string startDate { set; get; }

        public string endDate { set; get; }

        public string userAccount { set; get; }

        public string userName { set; get; }

        public VocAction Map()
        {
            DateTime? _startDate, _endDate;
            _startDate = _endDate = null;
            DateTime dateOutput;
            if (DateTime.TryParse(this.startDate, out dateOutput))
            {
                _startDate = dateOutput;
            }
            if (DateTime.TryParse(this.endDate, out dateOutput))
            {
                _endDate = dateOutput;
            }
            return new VocAction 
            { 
                Code = this.code,
                EndDate = _endDate,
                Remark = this.remark,
                Result = this.result,
                ResultFileId = this.resultFileId,
                ResultFileName = this.resultFileName,
                StartDate = _startDate,
                UserAccount = userAccount,
                UserName = userName
            };
        }
    }
}