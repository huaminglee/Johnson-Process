using System;
using System.Collections.Generic;
using System.Web;
using EDoc2.Document;
using EDoc2.Website;

namespace Johnson.Process.Website
{
    public class ProcessPage : System.Web.UI.Page
    {
        protected string TaskId
        {
            get
            {
                return Request["taskId"];
            }
        }

        int _processFolderId;
        protected int ProcessFolderId
        {
            get
            {
#if DEBUG
                return 0;
#endif
                if (_processFolderId == 0)
                {
                    string token;
                    ApiManager.Api.OrgnizationManagement.Impersonate(WebHelper.TempFolderAdminUserId, "127.0.0.1", out token);
                    IEDoc2Folder folder;
                    int result = ApiManager.Api.DocumentManagement.CreateFolder(token,
                        WebHelper.TempFolderId, Guid.NewGuid().ToString(),
                        string.Empty, 0, 0, 0, string.Empty, string.Empty, 0, out folder);
                    if (result != 0)
                    {
                        throw new Exception("创建文件夹失败:"+result);
                    }
                    _processFolderId = folder.FolderId;
                }
                return _processFolderId;
            }
        }
    }
}