using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Website.Models;
using Johnson.Process.Core;
using Newtonsoft.Json;

namespace Johnson.Process.Website
{
    public partial class OrderPingshen_DaiFafangController : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request["action"];
            if (string.IsNullOrEmpty(action))
            {
                throw new ArgumentNullException("action");
            }
            Response.ContentType = "application/json";
            if (action.Equals("get", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Get();
            }
            this.Response.End();
        }

        private void Get()
        {
            try
            {
                List<DaiFafangWenjianOrderModel> models = new List<DaiFafangWenjianOrderModel>();
                List<ProcessForm<OrderPingShenForm>> forms = WebHelper.OrderPingShenProcess.Get();
                string startTaskId = WebHelper.OrderWenjianFafangProcess.GetStartTaskId(WebHelper.CurrentUserInfo.UserLoginName);
                foreach (ProcessForm<OrderPingShenForm> form in forms)
                {
                    try
                    {
                        if (form.Form == null || !form.Form.PingshenWancheng ||
                            form.Form.FafangWancheng ||
                            !WebHelper.CurrentUserInfo.UserLoginName.Equals(form.Form.EngEngineerAccount, StringComparison.InvariantCultureIgnoreCase))
                        {
                            continue;
                        }
                        DaiFafangWenjianOrderModel model = new DaiFafangWenjianOrderModel(form, startTaskId);
                        model.startTaskId = startTaskId;
                        models.Add(model);
                    }
                    catch (Exception ex)
                    {
                        WebHelper.Logger.Error(ex.Message, ex);
                    }
                }

                Response.Write(JsonConvert.SerializeObject(models));
            }
            catch (Exception ex)
            {
                WebHelper.Logger.Error(ex.Message, ex);
            }
        }
    }
}