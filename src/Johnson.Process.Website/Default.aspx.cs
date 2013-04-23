using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDoc2.Website;
using EDoc2.Organization;

namespace Johnson.Process.Website
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("token:" + ApiManager.CurrentUserToken);

            EDoc2UserInfo userInfo;
            ApiManager.Api.OrgnizationManagement.GetCurrentUser(ApiManager.CurrentUserToken, out userInfo);
            Response.Write("user name:" + userInfo.UserRealName);
        }
    }
}