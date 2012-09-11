using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EDoc2.Website;
using EDoc2.Organization;

namespace Johnson.Process.Website.UserControls
{
    public partial class ConsultationAndQuotationDetails : System.Web.UI.UserControl
    {
        protected List<EDoc2UserInfo> MarketingEngineers
        {
            get
            {
                List<EDoc2UserInfo> userInfos;
#if DEBUG
                userInfos = new List<EDoc2UserInfo>();
                userInfos.Add(new EDoc2UserInfo{ UserLoginName = "t1", UserRealName = "t1"});
                userInfos.Add(new EDoc2UserInfo{ UserLoginName = "t2", UserRealName = "t2"});
                return userInfos;
#endif
                ApiManager.Api.OrgnizationManagement.GetChildUsersInUserGroup(ApiManager.CurrentUserToken, WebHelper.MarketingEngineerGroupId, out userInfos);
                return userInfos;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.marketingEngineerRepeater.DataSource = MarketingEngineers;
            this.marketingEngineerRepeater.DataBind();
        }
    }
}