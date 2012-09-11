using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class VocCompletedSolutionsModel
    {
        public string taskId { set; get; }

        public DateTime solutionsCompleteTime { set; get; }

        public string submitRemark { set; get; }
    }
}