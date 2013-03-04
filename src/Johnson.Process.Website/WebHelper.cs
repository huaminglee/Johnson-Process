using System;
using System.Collections.Generic;
using System.Web;
using log4net;
using Johnson.Process.Core;
using System.Configuration;
using EDoc2.Website;
using EDoc2.Organization;
using System.IO;

namespace Johnson.Process.Website
{
    public class WebHelper
    {
        static WebHelper()
        {
            log4net.Config.XmlConfigurator.Configure();
            Logger = log4net.LogManager.GetLogger("Johnson_Process_logger");
            TempFolderId = int.Parse(ConfigurationManager.AppSettings["tempFolderId"]);
            TempFolderAdminUserId = int.Parse(ConfigurationManager.AppSettings["TempFolderAdminUserId"]);
            EDoc2BaseUrl = ConfigurationManager.AppSettings["edoc2BaseUrl"];
            MarketingEngineerGroupId = int.Parse(ConfigurationManager.AppSettings["marketingEngineerGroupId"]);
            DeliveryProcess = new DeliveryProcess("货期管理");
            string consultationAndQuotationToCsdMailTemplatePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["JohnsonProcessPath"] + "/Template/ConsultationAndQuotationToCsdMailTemplate.htm");
            string consultationAndQuotationToCsdMailTemplate = File.ReadAllText(consultationAndQuotationToCsdMailTemplatePath);
            string consultationAndQuotation_TracerMailTemplatePath = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["JohnsonProcessPath"] + "/Template/ConsultationAndQuotation_TracerMailTemplate.htm");
            string consultationAndQuotation_TracerMailTemplate = File.ReadAllText(consultationAndQuotation_TracerMailTemplatePath);
            ConsultationAndQuotationProcess = new ConsultationAndQuotationProcess("技术咨询及报价", consultationAndQuotationToCsdMailTemplate, consultationAndQuotation_TracerMailTemplate);
            VocProcess = new VocProcess("VOC");

            string failureProductResultMailTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["JohnsonProcessPath"] + "/Template/FailureProductResultMailTemplate.htm"));
            string productReworkChaosongMailTemplate = File.ReadAllText(HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["JohnsonProcessPath"] + "/Template/ProductReworkChaosongMailTemplate.htm"));
            FailureProductProcess = new FailureProductProcess("不合格品处理", failureProductResultMailTemplate, productReworkChaosongMailTemplate);
            ProductReworkProcess = FailureProductProcess.ProductReworkProcess;
            OrderPingShenProcess = new OrderPingShenProcess("广州订单评审");
            OrderWenjianFafangProcess = new OrderWenjianFafangProcess("广州订单文件发放");
        }

        public static EDoc2UserInfo CurrentUserInfo
        {
            get
            {
#if DEBUG
                return new EDoc2UserInfo { DepartmentName ="d1", UserId = 1, UserRealName = "qi", UserLoginName = "qi" };
#endif
                if (WebsiteUtility.CurrentUser == null)
                {
                    WebsiteUtility.CheckAdAutoLogin();
                }
                return WebsiteUtility.CurrentUser;
            }
        }

        public static int MarketingEngineerGroupId { private set; get; }

        public static string EDoc2BaseUrl { private set; get; }

        public static int TempFolderAdminUserId { private set; get; }

        public static int TempFolderId { private set; get; }

        public static ILog Logger { private set; get; }

        public static DeliveryProcess DeliveryProcess { private set; get; }

        public static ConsultationAndQuotationProcess ConsultationAndQuotationProcess { private set; get; }

        public static FailureProductProcess FailureProductProcess { private set; get; }

        public static ProductReworkProcess ProductReworkProcess { private set; get; }

        public static OrderPingShenProcess OrderPingShenProcess { private set; get; }

        public static OrderWenjianFafangProcess OrderWenjianFafangProcess { private set; get; }

        public static VocProcess VocProcess { private set; get; }

        public static bool InDateRange(DateTime date, DateTime? startDate, DateTime? endDate)
        {
            if (endDate.HasValue && startDate.HasValue)
            {
                if (date < startDate || date > endDate)
                {
                    return false;
                }
            }
            else if (endDate.HasValue)
            {
                if (date > endDate)
                {
                    return false;
                }
            }
            else if (startDate.HasValue)
            {
                if (date < startDate)
                {
                    return false;
                }
            }
            return true;
        }
    }
}