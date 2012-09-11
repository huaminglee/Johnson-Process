using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Johnson.Process.Website.UserControls
{
    public partial class DeliveryDetails : System.Web.UI.UserControl
    {
        public bool ShowCsdAuditUser
        {
            set
            {
                this.td_CSDApp.Visible = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}