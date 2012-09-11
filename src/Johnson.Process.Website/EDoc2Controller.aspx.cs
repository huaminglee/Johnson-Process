using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Johnson.Process.Website.Models;
using Newtonsoft.Json;
using EDoc2.Website;

namespace Johnson.Process.Website
{
    public partial class EDoc2Controller : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string action = Request["action"];
            if (string.IsNullOrEmpty(action))
            {
                throw new ArgumentNullException("action");
            }
            Response.ContentType = "application/json";

            if (action.Equals("DeleteFile", StringComparison.InvariantCultureIgnoreCase))
            {
                this.DeleteFile();
            }
        }

        private void DeleteFile()
        {
            ActionResultModel resultModel = new ActionResultModel();
            try
            {
                string token;
                ApiManager.Api.OrgnizationManagement.Impersonate(WebHelper.TempFolderAdminUserId, "127.0.0.1", out token);
                int fileId = int.Parse(Request["fileId"]);
                int result = ApiManager.Api.DocumentManagement.DeleteFile(token, fileId, false);
                if (result != 0)
                {
                    throw new Exception("删除文件失败:" + result);
                }
            }
            catch (Exception ex)
            {
                resultModel.result = ActionResult.Error;
                resultModel.message = ex.Message;
                WebHelper.Logger.Error(ex.Message, ex);
            }
            Response.Write(JsonConvert.SerializeObject(resultModel));
        }
    }
}