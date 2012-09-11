using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.Core
{
    public class ProcessEmailEntity
    {
        public int ID { set; get; } 
        public string Email{set;get;} 
        public string Subject{set;get;}
        public string Content { set; get; }
        public int Status { set; get; }
    }
}
