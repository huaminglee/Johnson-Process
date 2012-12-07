using System;
using System.Collections.Generic;
using System.Web;
using Johnson.Process.Core;

namespace Johnson.Process.Website.Models
{
    public class SheJiTiJiaoSubmitModel
    {
        public string taskId;
        public string sheJiShuoMing;
        public string ziLiaoWanZhengDu;
        public bool hasXinWuLiao;
        public string jianChaEngineerAccount;
        public string jianChaEngineerName;
        public string zhuGuanAccount;
        public string zhuGuanName;
        public string submitRemark;
        public List<ProcessFile> sheJiZiLiao;
    }
}