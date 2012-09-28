using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.Core
{
    public class ProcessForm<T>
    {
        public int ID { set; get; }

        public string ProcessName { set; get; }

        public int IncidentNo { set; get; }

        public T Form { set; get; }

        public string ProcessType { set; get; }
    }
}
