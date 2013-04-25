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

        private List<ProcessForm<ProductReworkForm>> Guolv(ProductReworkReportSearchModel searchModel, List<ProcessForm<ProductReworkForm>> forms)
        {
            List<ProcessForm<ProductReworkForm>> guolvhouForms = new List<ProcessForm<ProductReworkForm>>();
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
                    guolvhouForms.Add(form);
                }
                catch (Exception ex)
                {
                    WebHelper.Logger.Error(ex.Message, ex);
                }
            }
            return guolvhouForms;
        }

        private void Daochu()
        {
            ActionResultModel actionModel = new ActionResultModel();
            try
            {
                string formJson = Request["formJson"];
                ProductReworkReportSearchModel searchModel = JsonConvert.DeserializeObject<ProductReworkReportSearchModel>(formJson);

                List<ProcessForm<ProductReworkForm>> forms = WebHelper.ProductReworkProcess.Get();

                forms = Guolv(searchModel, forms);
                string websiteBasePath = Server.MapPath("~/");
                string vocReportXlsPath = Path.Combine(websiteBasePath, "Rework REPORT.xls");
                FileStream stream = File.OpenRead(vocReportXlsPath);
                HSSFWorkbook workbook = new HSSFWorkbook(stream);
                HSSFSheet sheet = workbook.GetSheetAt(0);

                int rowCount = 0;
                foreach (ProcessForm<ProductReworkForm> form in forms)
                {
                    if (form.Form != null)
                    {
                        rowCount++;

                        HSSFRow row = sheet.CreateRow(rowCount);

                        row.CreateCell(0).SetCellValue(form.Form.StartUserName);
                        row.CreateCell(1).SetCellValue(form.Form.StartTime.ToString("yyyy-MM-dd"));
                        row.CreateCell(2).SetCellValue(form.Form.FailureNo);
                        row.CreateCell(3).SetCellValue(ProductReworkFormHelper.Map(form.Form.ProductType));
                        row.CreateCell(4).SetCellValue(form.Form.XLH);
                        row.CreateCell(5).SetCellValue(form.Form.Name);
                        row.CreateCell(6).SetCellValue(form.Form.SapNo);
                        row.CreateCell(7).SetCellValue(form.Form.Quantity);
                        row.CreateCell(8).SetCellValue(form.Form.OrderNumber);
                        row.CreateCell(9).SetCellValue(form.Form.StartDepartment);
                        row.CreateCell(10).SetCellValue(form.Form.GS);
                        row.CreateCell(11).SetCellValue(form.Form.GSFY.ToString());
                        row.CreateCell(12).SetCellValue(form.Form.WLFY.ToString());
                        row.CreateCell(13).SetCellValue(form.Form.ZFY.ToString());
                        row.CreateCell(14).SetCellValue(ProductReworkFormHelper.MapFYCD(form.Form.FYCD));
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
    }
}