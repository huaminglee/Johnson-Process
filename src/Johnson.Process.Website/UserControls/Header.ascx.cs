using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Johnson.Process.Website.UserControls
{
    public partial class Header : System.Web.UI.UserControl
    {
        string _headerTitle;
        public string HeaderTitle
        {
            set
            {
                _headerTitle = value;
            }
            get
            {
                if (string.IsNullOrEmpty(_headerTitle))
                {
                    return "货期管理";
                }
                return _headerTitle;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}