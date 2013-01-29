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
    public partial class FailureProduct_ReportController : System.Web.UI.Page
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
                List<FailureProductReportModel> models = new List<FailureProductReportModel>();
                List<ProcessForm<FailureProductForm>> forms = WebHelper.FailureProductProcess.Get();

                foreach (ProcessForm<FailureProductForm> form in forms)
                {
                    try
                    {
                        if (form.Form == null)
                        {
                            continue;
                        }
                        FailureProductReportModel model = new FailureProductReportModel(form);
                        if (model.taskStatus != 1)
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

        private void Search()
        {
            try
            {
                string formJson = Request["formJson"];
                FailureProductReportSearchModel searchModel = JsonConvert.DeserializeObject<FailureProductReportSearchModel>(formJson);

                List<FailureProductReportModel> models = new List<FailureProductReportModel>();
                List<ProcessForm<FailureProductForm>> forms = WebHelper.FailureProductProcess.Get();

                foreach (ProcessForm<FailureProductForm> form in forms)
                {
                    try
                    {
                        FailureProductReportModel model = new FailureProductReportModel(form);
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

                        if (model.taskStatus != searchModel.taskStatus)
                        {
                            continue;
                        }

                        if (!string.IsNullOrEmpty(searchModel.No))
                        {
                            if (form.Form.No.IndexOf(searchModel.No, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.ComponentCode))
                        {
                            if (form.Form.ComponentCode.IndexOf(searchModel.ComponentCode, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.ComponentName))
                        {
                            if (form.Form.ComponentName.IndexOf(searchModel.ComponentName, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.BJXLH))
                        {
                            if (form.Form.BJXLH.IndexOf(searchModel.BJXLH, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.JZXLH))
                        {
                            if (form.Form.JZXLH.IndexOf(searchModel.JZXLH, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.GYSMC))
                        {
                            if (form.Form.JZXLH.IndexOf(searchModel.GYSMC, StringComparison.InvariantCultureIgnoreCase) == -1)
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