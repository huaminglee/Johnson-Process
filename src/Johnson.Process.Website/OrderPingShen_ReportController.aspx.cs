using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using Johnson.Process.Website.Models;
using Johnson.Process.Core;

namespace Johnson.Process.Website
{
    public partial class OrderPingShen_ReportController : System.Web.UI.Page
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
                List<OrderPingShenReportModel> models = new List<OrderPingShenReportModel>();
                List<ProcessForm<OrderPingShenForm>> forms = WebHelper.OrderPingShenProcess.Get();

                foreach (ProcessForm<OrderPingShenForm> form in forms)
                {
                    try
                    {
                        if (form.Form == null)
                        {
                            continue;
                        }
                        OrderPingShenReportModel model = new OrderPingShenReportModel(form);
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
                OrderPingShenReportSearchModel searchModel = JsonConvert.DeserializeObject<OrderPingShenReportSearchModel>(formJson);

                List<OrderPingShenReportModel> models = new List<OrderPingShenReportModel>();
                List<ProcessForm<OrderPingShenForm>> forms = WebHelper.OrderPingShenProcess.Get();

                foreach (ProcessForm<OrderPingShenForm> form in forms)
                {
                    try
                    {
                        OrderPingShenReportModel model = new OrderPingShenReportModel(form);
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

                        if (!WebHelper.InDateRange(form.Form.JiaoHuoRiQi, searchModel.jiaoHuoRiQiStart, searchModel.jiaoHuoRiQiEnd))
                        {
                            continue;
                        }

                        if (!string.IsNullOrEmpty(searchModel.SONO))
                        {
                            if (form.Form.SONO.IndexOf(searchModel.SONO, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.JDSNO))
                        {
                            if (form.Form.JDSNO.IndexOf(searchModel.JDSNO, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.tuZiQueRen))
                        {
                            if (form.Form.TuZiQueRen.IndexOf(searchModel.tuZiQueRen, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.xiangMingCheng))
                        {
                            if (form.Form.XiangMingCheng.IndexOf(searchModel.xiangMingCheng, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.banShiChu))
                        {
                            if (form.Form.BanShiChu.IndexOf(searchModel.banShiChu, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.banShiChuLianXiRen))
                        {
                            if (form.Form.BanShiChuLianXiRen.IndexOf(searchModel.banShiChuLianXiRen, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (searchModel.isStandard.HasValue)
                        {
                            if (form.Form.IsStandard != searchModel.isStandard.Value)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.chanPinLeiXing))
                        {
                            if (form.Form.ChanPinLeiXing.IndexOf(searchModel.chanPinLeiXing, StringComparison.InvariantCultureIgnoreCase) == -1)
                            {
                                continue;
                            }
                        }

                        if (!string.IsNullOrEmpty(searchModel.sapItem))
                        {
                            if (form.Form.SapItem.IndexOf(searchModel.sapItem, StringComparison.InvariantCultureIgnoreCase) == -1)
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