using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AGISoftware
{
    public partial class SignalProcess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // signalProcessFame.Src = Utility.SignalProcessPath;
                signalProcessFame.Attributes.Add("src", Utility.SignalProcessPath);
            }
        }
    }
}