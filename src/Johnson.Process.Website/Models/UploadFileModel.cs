using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class UploadFileModel
    {
        public UploadFileModel()
        {
        }

        public UploadFileModel(ProcessFile file)
        {
            this.fileId = file.FileId;
            this.fileName = file.FileName;
        }

        public int fileId;
        public string fileName;

        public ProcessFile Map()
        {
            return new ProcessFile
            {
                FileId = this.fileId,
                FileName = this.fileName
            };
        }
    }
}