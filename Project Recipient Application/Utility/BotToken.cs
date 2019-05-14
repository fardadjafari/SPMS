using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Recipient_Application.Utility
{
    public class BotToken
    {
        public static string Api { get { return ""; } }

        public static string FileUrl { get { return System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/IMG/")); } }
    }
}