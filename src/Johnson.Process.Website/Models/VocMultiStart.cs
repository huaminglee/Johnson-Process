using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class VocMultiStart
    {
        public string applyUserName { set; get; }

        public string applyUserDepartmentName { set; get; }

        public string applyTime { set; get; }

        public string projectName { set; get; }

        public List<VocStartModel> complaints { set; get; }

        public string submitRemark { set; get; }
    }
}