using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class VocActinCompletedModel
    {
        public string taskId;

        public List<VocActionModel> actions;

        public string submitRemark;
    }
}