using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Website.Models;
using Johnson.Process.Core;
using Newtonsoft.Json;
using System.IO;
using NPOI.HSSF.UserModel;

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
                        if (!WebHelper.InDateRange(form.Form.StartTime, DateTime.Now.AddDays(-20), DateTime.Now))
                        {
                            continue;
                        }
                        FailureProductReportModel model = new FailureProductReportModel(form);
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
                forms = Guolv(searchModel, forms);

                foreach (ProcessForm<FailureProductForm> form in forms)
                {
                    try
                    {
                        FailureProductReportModel model = new FailureProductReportModel(form);

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

        private void Daochu()
        {
            ActionResultModel actionModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                FailureProductReportSearchModel searchModel = JsonConvert.DeserializeObject<FailureProductReportSearchModel>(formJson);

                List<ProcessForm<FailureProductForm>> forms = WebHelper.FailureProductProcess.Get();

                forms = Guolv(searchModel, forms);
                string websiteBasePath = Server.MapPath("~/");
                string vocReportXlsPath = Path.Combine(websiteBasePath, "FailureProduct REPORT.xls");
                FileStream stream = File.OpenRead(vocReportXlsPath);
                HSSFWorkbook workbook = new HSSFWorkbook(stream);
                HSSFSheet sheet = workbook.GetSheetAt(0);

                int rowCount = 0;
                foreach (ProcessForm<FailureProductForm> form in forms)
                {
                    if (form.Form != null)
                    {
                        rowCount++;

                        HSSFRow row = sheet.CreateRow(rowCount);

                        row.CreateCell(0).SetCellValue(form.Form.StartUserName);
                        row.CreateCell(1).SetCellValue(form.Form.StartTime.ToString("yyyy-MM-dd"));
                        row.CreateCell(2).SetCellValue(form.Form.No);
                        row.CreateCell(3).SetCellValue(form.Form.ComponentCode);
                        row.CreateCell(4).SetCellValue(form.Form.ComponentName);
                        row.CreateCell(5).SetCellValue(form.Form.BJXLH);
                        row.CreateCell(6).SetCellValue(form.Form.JZXLH);
                        row.CreateCell(7).SetCellValue(form.Form.GYSMC);
                        row.CreateCell(8).SetCellValue(form.Form.ZRBM);
                        row.CreateCell(9).SetCellValue(FailureResultHelper.MapName(form.Form.Result));
                    }

                }
                string tempReportXlsPath = Server.MapPath(string.Format("~/Temp/{0}.xls", Guid.NewGuid().ToString()));
                if (!Directory.Exists(Path.GetDirectoryName(tempReportXlsPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(tempReportXlsPath));
                }
                Stream newStream = File.Open(tempReportXlsPath, FileMode.Create);
                workbook.Write(newStream);
                newStream.Close();

                stream.Close();
                workbook = null;
                sheet = null;
                actionModel.data = Path.GetFileName(tempReportXlsPath);
            }
            catch (Exception ex)
            {
                actionModel.result = ActionResult.Error;
                actionModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(actionModel));
        }


        private List<ProcessForm<FailureProductForm>> Guolv(FailureProductReportSearchModel searchModel, List<ProcessForm<FailureProductForm>> forms)
        {
            List<ProcessForm<FailureProductForm>> guolvhouForms = new List<ProcessForm<FailureProductForm>>();
            foreach (ProcessForm<FailureProductForm> form in forms)
            {
                try
                {
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
                        if (form.Form.GYSMC.IndexOf(searchModel.GYSMC, StringComparison.InvariantCultureIgnoreCase) == -1)
                        {
                            continue;
                        }
                    }
                    if (searchModel.Result != FailureResult.None)
                    {
                        if (form.Form.Result != searchModel.Result)
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
    }
}