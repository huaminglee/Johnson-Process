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
    public partial class ProductRework_ReportController : System.Web.UI.Page
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
                List<ProductReworkReportModel> models = new List<ProductReworkReportModel>();
                List<ProcessForm<ProductReworkForm>> forms = WebHelper.ProductReworkProcess.Get();

                foreach (ProcessForm<ProductReworkForm> form in forms)
                {
                    try
                    {
                        if (form.Form == null)
                        {
                            continue;
                        }
                        ProductReworkReportModel model = new ProductReworkReportModel(form);
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

        private void Search()
        {
            try
            {
                string formJson = Request["formJson"];
                ProductReworkReportSearchModel searchModel = JsonConvert.DeserializeObject<ProductReworkReportSearchModel>(formJson);

                List<ProductReworkReportModel> models = new List<ProductReworkReportModel>();
                List<ProcessForm<ProductReworkForm>> forms = WebHelper.ProductReworkProcess.Get();

                foreach (ProcessForm<ProductReworkForm> form in forms)
                {
                    try
                    {
                        ProductReworkReportModel model = new ProductReworkReportModel(form);
                        if (form.Form == null)
                        {
                            continue;
                        }

                        if (!WebHelper.InDateRange(form.Form.StartTime, searchModel.startTimeStart, searchModel.startTimeEnd))
                        {
                            continue;
                        }

                        if (!string.IsNullOrEmpty(searchModel.startUserName))
                        {
                            if (form.Form.StartUserName.IndexOf(searchModel.startUserName, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.FailureNo))
                        {
                            if (form.Form.FailureNo.IndexOf(searchModel.FailureNo, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (searchModel.ProductType.HasValue && form.Form.ProductType != searchModel.ProductType.Value)
                        {
                            continue;
                        }

                        if (!string.IsNullOrEmpty(searchModel.XLH))
                        {
                            if (form.Form.XLH.IndexOf(searchModel.XLH, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.Name))
                        {
                            if (form.Form.Name.IndexOf(searchModel.Name, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.SapNo))
                        {
                            if (form.Form.SapNo.IndexOf(searchModel.SapNo, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.Quantity))
                        {
                            if (form.Form.Quantity.IndexOf(searchModel.Quantity, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.OrderNumber))
                        {
                            if (form.Form.OrderNumber.IndexOf(searchModel.OrderNumber, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.StartDepartment))
                        {
                            if (form.Form.StartDepartment.IndexOf(searchModel.StartDepartment, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

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