using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.Core
{
    public class ProcessFile
    {
        public int FileId { set; get; }

        public string FileName { set; get; }

        public DateTime CreateTime { set; get; }

        public string CreateUserName { set; get; }

        public string StepName { set; get; }
    }
}
