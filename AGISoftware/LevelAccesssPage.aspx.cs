using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGISoftware
{
    public partial class LevelAccesssPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string level1name = WebConfigurationManager.ConnectionStrings["level1"].ToString();
                    string level2name = WebConfigurationManager.ConnectionStrings["level2"].ToString();
                    string level3name = WebConfigurationManager.ConnectionStrings["level3"].ToString();
                    if (level1name == "")
                    {
                        level1.Visible = false;
                    }
                    if (level2name == "")
                    {
                        level2.Visible = false;
                    }
                    if (level3name == "")
                    {
                        level3.Visible = false;
                    }
                }
                catch ( Exception ex)
                {

                }
               
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void setLevelType(string leveltype, string leveltypename)
        {
            HttpContext.Current.Session["LevelType"] = leveltype;
            HttpContext.Current.Session["LevelTypeName"] = leveltypename;
        }

    }
}