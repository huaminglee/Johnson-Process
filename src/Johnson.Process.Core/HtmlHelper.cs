using System;
using System.Collections.Generic;
using System.Text;

namespace Johnson.Process.Core
{
    public class HtmlHelper
    {
        public static string GetTextareaHtml(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            return value.Replace("\r\n", "<br />");
        }
    }
}
