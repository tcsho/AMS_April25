using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for ImpromptuHelper
/// </summary>
namespace ADG.JQueryExtenders.Impromptu
{
    public static class ImpromptuHelper
    {
        public static void ShowPrompt(string message)
        {
            message = message.Replace("'", "");
            var page = HttpContext.Current.CurrentHandler as Page;
            if (page != null)
            {
                ScriptManager.RegisterStartupScript(page, page.GetType(), "text", "CallToastr('" + message + "','true')\n", true);
            }
        }
    }
}