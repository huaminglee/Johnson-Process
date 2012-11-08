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
    public partial class DeliveryReportController : System.Web.UI.Page
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
            else if (action.Equals("Search", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Search();
            }
            this.Response.End();
        }
        private void Get()
        {
            try
            {
                List<DeliveryReportModel> models = new List<DeliveryReportModel>();
                List<ProcessForm<DeliveryProcessForm>> forms = WebHelper.DeliveryProcess.Get();

                foreach (ProcessForm<DeliveryProcessForm> form in forms)
                {
                    if (form.Form == null)
                    {
                        continue;
                    }
                    DeliveryReportModel model = new DeliveryReportModel(form);
                    if (model.taskStatus != 1)
                    {
                        continue;
                    }
                    models.Add(model);
                }

                Response.Write(JsonConvert.SerializeObject(models));
            }
            catch (Exception ex)
            {
                WebHelper.Logger.Error(ex.Message, ex);
            }
        }

        private void Search()
        {
            try
            {
                string formJson = Request["formJson"];
                DeliveryReportSearchModel searchModel = JsonConvert.DeserializeObject<DeliveryReportSearchModel>(formJson);

                List<DeliveryReportModel> models = new List<DeliveryReportModel>();
                List<ProcessForm<DeliveryProcessForm>> forms = WebHelper.DeliveryProcess.Get();

                foreach (ProcessForm<DeliveryProcessForm> form in forms)
                {
                    DeliveryReportModel model = new DeliveryReportModel(form);
                    if (form.Form == null)
                    {
                        continue;
                    }

                    if (!WebHelper.InDateRange(form.Form.ApplyTime, searchModel.applyTimeStart, searchModel.applyTimeEnd))
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(searchModel.applyUserName))
                    {
                        if (form.Form.ApplyUserName.IndexOf(searchModel.applyUserName, StringComparison.InvariantCultureIgnoreCase) == -1)
                        {
                            continue;
                        }
                    }

                    if (model.taskStatus != searchModel.taskStatus)
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(searchModel.orderNumber))
                    {
                        if (form.Form.OrderNumber.IndexOf(searchModel.orderNumber, StringComparison.InvariantCultureIgnoreCase) == -1)
                        {
                            continue;
                        }
                    }

                    if (!string.IsNullOrEmpty(searchModel.projectName))
                    {
                        if (form.Form.ProjectName.IndexOf(searchModel.projectName, StringComparison.InvariantCultureIgnoreCase) == -1)
                        {
                            continue;
                        }
                    }

                    if (!string.IsNullOrEmpty(searchModel.saleOffice))
                    {
                        if (form.Form.SaleOffice.IndexOf(searchModel.saleOffice, StringComparison.InvariantCultureIgnoreCase) == -1)
                        {
                            continue;
                        }
                    }

                    if (!string.IsNullOrEmpty(searchModel.saleGroup))
                    {
                        if (form.Form.SaleGroup.IndexOf(searchModel.saleGroup, StringComparison.InvariantCultureIgnoreCase) == -1)
                        {
                            continue;
                        }
                    }

                    if (!string.IsNullOrEmpty(searchModel.saleEngineerYT))
                    {
                        if (form.Form.SaleEngineer.IndexOf(searchModel.saleEngineerYT, StringComparison.InvariantCultureIgnoreCase) == -1)
                        {
                            continue;
                        }
                    }

                    if (!WebHelper.InDateRange(form.Form.BookDate, searchModel.bookDateStart, searchModel.bookDateEnd))
                    {
                        continue;
                    }

                    if (!WebHelper.InDateRange(form.Form.RequestOutDate, searchModel.requestOutDateStart, searchModel.requestOutDateEnd))
                    {
                        continue;
                    }
                    models.Add(model);
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