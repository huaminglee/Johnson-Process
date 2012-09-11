using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.Core
{
    public class VocAction
    {
        public string Code { set; get; }

        public string Remark { set; get; }

        public string Result { set; get; }

        public string ResultFileName { set; get; }

        public int ResultFileId { set; get; }

        public DateTime? StartDate { set; get; }

        public DateTime? EndDate { set; get; }

        public string UserAccount { set; get; }

        public string UserName { set; get; }
    }
}
