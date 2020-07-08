using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace AGISoftware
{
    public class Utility
    {
        public static string SignalProcessPath = WebConfigurationManager.AppSettings["SignalProcessPath"];
        public static string ReportImageLHS = WebConfigurationManager.AppSettings["ReportImageLHS"];
        public static string ReportImageRHS = WebConfigurationManager.AppSettings["ReportImageRHS"];
        public static string ReportTableLHSHeader = WebConfigurationManager.AppSettings["ReportTableLHSHeader"];
        public static string ReportTableRHSHeader = WebConfigurationManager.AppSettings["ReportTableRHSHeader"];
    }
}