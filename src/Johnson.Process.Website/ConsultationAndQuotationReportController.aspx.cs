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
    public partial class ConsultationAndQuotationReportController : System.Web.UI.Page
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
                List<ConsultationAndQuotationReportModel> models = new List<ConsultationAndQuotationReportModel>();
                List<ProcessForm<ConsultationAndQuotationForm>> forms = WebHelper.ConsultationAndQuotationProcess.Get();

                foreach (ProcessForm<ConsultationAndQuotationForm> form in forms)
                {
                    try
                    {
                        if (form.Form == null)
                        {
                            continue;
                        }
                        if (!WebHelper.InDateRange(form.Form.ApplyTime, DateTime.Now.AddDays(-20), DateTime.Now))
                        {
                            continue;
                        }
                        ConsultationAndQuotationReportModel model = new ConsultationAndQuotationReportModel(form);
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
                ConsultationAndQuotationReportSearchModel searchModel = JsonConvert.DeserializeObject<ConsultationAndQuotationReportSearchModel>(formJson);

                List<ConsultationAndQuotationReportModel> models = new List<ConsultationAndQuotationReportModel>();
                List<ProcessForm<ConsultationAndQuotationForm>> forms = WebHelper.ConsultationAndQuotationProcess.Get();

                foreach (ProcessForm<ConsultationAndQuotationForm> form in forms)
                {
                    try
                    {
                        ConsultationAndQuotationReportModel model = new ConsultationAndQuotationReportModel(form);
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

                        if (!string.IsNullOrEmpty(searchModel.applyUserDepartmentName))
                        {
                            if (form.Form.ApplyUserDepartmentName.IndexOf(searchModel.applyUserDepartmentName, StringComparison.InvariantCultureIgnoreCase) == -1)
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

                        if (!string.IsNullOrEmpty(searchModel.marketingEngineer))
                        {
                            if (form.Form.MarketingEngineer.IndexOf(searchModel.marketingEngineer, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!WebHelper.InDateRange(form.Form.ExpectSignContact, searchModel.expectSignContactDateStart, searchModel.expectSignContactDateEnd))
                        {
                            continue;
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