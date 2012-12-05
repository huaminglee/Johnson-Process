using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Core;
using Johnson.Process.Website.Models;
using Newtonsoft.Json;
using Ultimus.WFServer;

namespace Johnson.Process.Website
{
    public partial class VocReportController : System.Web.UI.Page
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
                List<VocReportModel> models = new List<VocReportModel>();
                List<ProcessForm<VocForm>> forms = WebHelper.VocProcess.Get();

                foreach (ProcessForm<VocForm> form in forms)
                {
                    try
                    {
                        if (form.Form == null)
                        {
                            continue;
                        }
                        VocReportModel model = new VocReportModel(form);
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
                VocReportSearchModel searchModel = JsonConvert.DeserializeObject<VocReportSearchModel>(formJson);

                List<VocReportModel> models = new List<VocReportModel>();
                List<ProcessForm<VocForm>> forms = WebHelper.VocProcess.Get();

                foreach (ProcessForm<VocForm> form in forms)
                {
                    try
                    {
                        VocReportModel model = new VocReportModel(form);
                        if (form.Form == null)
                        {
                            continue;
                        }
                        if (searchModel.applyTimeEnd.HasValue && searchModel.applyTimeStart.HasValue)
                        {
                            if (form.Form.ApplyTime < searchModel.applyTimeStart || form.Form.ApplyTime > searchModel.applyTimeEnd)
                            {
                                continue;
                            }
                        }
                        else if (searchModel.applyTimeEnd.HasValue)
                        {
                            if (form.Form.ApplyTime > searchModel.applyTimeEnd)
                            {
                                continue;
                            }
                        }
                        else if (searchModel.applyTimeStart.HasValue)
                        {
                            if (form.Form.ApplyTime < searchModel.applyTimeStart)
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

                        if (!string.IsNullOrEmpty(searchModel.applyUserName))
                        {
                            if (form.Form.ApplyUserName.IndexOf(searchModel.applyUserName, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.faultRemark))
                        {
                            if (form.Form.FaultRemark.IndexOf(searchModel.faultRemark, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.machineCode))
                        {
                            if (form.Form.MachineCode.IndexOf(searchModel.machineCode, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.machineModel))
                        {
                            if (form.Form.MachineModel.IndexOf(searchModel.machineModel, StringComparison.InvariantCultureIgnoreCase) == -1)
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

                        if (model.taskStatus != searchModel.taskStatus)
                        {
                            continue;
                        }

                        if (!string.IsNullOrEmpty(searchModel.vocCode))
                        {
                            if (form.Form.VocCode.IndexOf(searchModel.vocCode, StringComparison.InvariantCultureIgnoreCase) == -1)
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