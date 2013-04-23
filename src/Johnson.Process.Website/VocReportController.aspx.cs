using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Core;
using Johnson.Process.Website.Models;
using Newtonsoft.Json;
using Ultimus.WFServer;
using NPOI.HSSF.UserModel;
using System.IO;

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
            else if (action.Equals("Daochu", StringComparison.InvariantCultureIgnoreCase))
            {
                this.Daochu();
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

        private List<ProcessForm<VocForm>> Guolv(VocReportSearchModel searchModel, List<ProcessForm<VocForm>> forms)
        {
            List<ProcessForm<VocForm>> guolvhouForms = new List<ProcessForm<VocForm>>();
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

                    if (!string.IsNullOrEmpty(searchModel.vocCode))
                    {
                        if (form.Form.VocCode.IndexOf(searchModel.vocCode, StringComparison.InvariantCultureIgnoreCase) == -1)
                        {
                            continue;
                        }
                    }
                    guolvhouForms.Add(form);
                }
                catch (Exception ex)
                {
                    WebHelper.Logger.Error(ex.Message, ex);
                }
            }
            return guolvhouForms;
        }

        private void Search()
        {
            try
            {
                string formJson = Request["formJson"];
                VocReportSearchModel searchModel = JsonConvert.DeserializeObject<VocReportSearchModel>(formJson);

                List<VocReportModel> models = new List<VocReportModel>();
                List<ProcessForm<VocForm>> forms = WebHelper.VocProcess.Get();

                forms = Guolv(searchModel, forms);
                foreach (ProcessForm<VocForm> form in forms)
                {
                    models.Add(new VocReportModel(form));
                }

                Response.Write(JsonConvert.SerializeObject(models));
            }
            catch (Exception ex)
            {
                WebHelper.Logger.Error(ex.Message, ex);
            }
        }

        private void Daochu()
        {
            ActionResultModel actionModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                VocReportSearchModel searchModel = JsonConvert.DeserializeObject<VocReportSearchModel>(formJson);

                List<ProcessForm<VocForm>> forms = WebHelper.VocProcess.Get();

                forms = Guolv(searchModel, forms);
                string websiteBasePath = Server.MapPath("~/");
                string vocReportXlsPath = Path.Combine(websiteBasePath, "VOC REPORT.xls");
                FileStream stream = File.OpenRead(vocReportXlsPath);
                HSSFWorkbook workbook = new HSSFWorkbook(stream);
                HSSFSheet sheet = workbook.GetSheetAt(0);

                int rowCount = 0;
                foreach(ProcessForm<VocForm> form in forms)
                {
                    if(form.Form != null)
                    {
                        rowCount++;

                        HSSFRow row = sheet.CreateRow(rowCount);

                        row.CreateCell(0).SetCellValue(form.Form.VocCode);
                        row.CreateCell(1).SetCellValue(form.Form.ApplyUserName);
                        row.CreateCell(2).SetCellValue(form.Form.ApplyUserDepartmentName);
                        row.CreateCell(3).SetCellValue(form.Form.ApplyTime.ToString("yyyy-MM-dd"));
                        row.CreateCell(4).SetCellValue(form.Form.ProjectName);
                        row.CreateCell(5).SetCellValue(form.Form.MachineModel);
                        row.CreateCell(6).SetCellValue(form.Form.MachineCode);
                        row.CreateCell(7).SetCellValue(form.Form.FaultRemark);
                        row.CreateCell(8).SetCellValue(form.Form.FaultQuantity);
                        if (form.Form.Actions.Count > 0)
                        {
                            row.CreateCell(9).SetCellValue(form.Form.Actions[0].Remark);
                            if (form.Form.Actions[0].EndDate.HasValue)
                            {
                                row.CreateCell(10).SetCellValue(form.Form.Actions[0].EndDate.Value.ToString("yyyy-MM-dd"));
                            }
                        }
                        row.CreateCell(11).SetCellValue(form.Form.ResponsibleUserName);
                        row.CreateCell(12).SetCellValue(form.Form.Solutions);
                        if (form.Form.SolutionsStartTime.HasValue)
                        {
                            row.CreateCell(13).SetCellValue(form.Form.SolutionsStartTime.Value.ToString("yyyy-MM-dd"));
                        }
                        if (form.Form.SolutionsCompleteTime.HasValue)
                        {
                            row.CreateCell(14).SetCellValue(form.Form.SolutionsCompleteTime.Value.ToString("yyyy-MM-dd"));
                        }
                        row.CreateCell(15).SetCellValue(form.Form.Reason);
                        if (form.Form.ReasonWanchengShijian.HasValue)
                        {
                            row.CreateCell(16).SetCellValue(form.Form.ReasonWanchengShijian.Value.ToString("yyyy-MM-dd"));
                        }
                        row.CreateCell(17).SetCellValue("是");
                        row.CreateCell(18).SetCellValue(form.Form.Measures);
                        if (form.Form.WanchengShijian.HasValue)
                        {
                            row.CreateCell(19).SetCellValue(form.Form.WanchengShijian.Value.ToString("yyyy-MM-dd"));
                        }
                    }

                }
                string vocTempReportXlsPath = Server.MapPath(string.Format("~/Temp/{0}.xls", Guid.NewGuid().ToString()));
                if (!Directory.Exists(Path.GetDirectoryName(vocTempReportXlsPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(vocTempReportXlsPath));
                }
                Stream newStream = File.Open(vocTempReportXlsPath, FileMode.Create);
                workbook.Write(newStream);
                newStream.Close();

                stream.Close();
                workbook = null;
                sheet = null;
                actionModel.data = Path.GetFileName(vocTempReportXlsPath);
            }
            catch (Exception ex)
            {
                actionModel.result = ActionResult.Error;
                actionModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actionModel));
        }
    }
}