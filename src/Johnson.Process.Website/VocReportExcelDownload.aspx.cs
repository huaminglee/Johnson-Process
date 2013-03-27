using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Johnson.Process.Website
{
    public partial class VocReportExcelDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AddHeader("Content-Disposition", "attachment;filename=\"VOC REPORT.xls\";");//设置下载文件名
            Response.ContentType = "application/octet-stream"; 
            Response.WriteFile(Server.MapPath("~/Temp/"+Request["file"]));
            Response.End();
        }
    }
}