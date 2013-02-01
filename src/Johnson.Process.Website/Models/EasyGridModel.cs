using System;
using System.Collections.Generic;
using System.Web;

namespace Johnson.Process.Website.Models
{
    public class EasyGridModel<T>
    {
        public EasyGridModel()
        {

        }

        public int total;
        public List<T> rows;
    }
}