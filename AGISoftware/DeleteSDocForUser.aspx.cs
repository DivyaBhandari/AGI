using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AGISoftware.DataBaseAccess;
using AGISoftware.Model;

namespace AGISoftware
{
    public partial class DeleteSDocForUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindSDoclistForDelete();
            }
            if (Session["EmpName"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
        }
        private void BindSDoclistForDelete()
        {
            List<string> SdocDelete = DBAccess.getSDocForDelete("Delete", "SDocList");
            ddlDeleteSdoc.DataSource = SdocDelete;
            ddlDeleteSdoc.DataBind();
        }

        protected void deleteSdocYes_ServerClick(object sender, EventArgs e)
        {
            DBAccess.DeleteSDoc(ddlDeleteSdoc.SelectedItem == null ? "" : ddlDeleteSdoc.SelectedItem.ToString(), Session["EmpName"]==null?"" : Session["EmpName"].ToString(), "Delete");
            BindSDoclistForDelete();
        }
    }
}