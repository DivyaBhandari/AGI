using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AGISoftware.Model;
using AGISoftware.DataBaseAccess;
using System.Data;

namespace AGISoftware
{
    public partial class InputModuleMasterView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindGridView();
            }
            if (Session["EmpName"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
        }
        private void BindGridView()
        {
            DataTable dt = new DataTable();
            dt = DBAccess.GetInputModuleDetails();
            gvInputModule.DataSource = dt;
            gvInputModule.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvInputModule.Rows.Count; i++)
            {
                string inputID = (gvInputModule.Rows[i].FindControl("hdfId") as HiddenField).Value;
                string input = (gvInputModule.Rows[i].FindControl("lblInputModule") as Label).Text;
                string rename = (gvInputModule.Rows[i].FindControl("txtRenameInputModule") as TextBox).Text;
                string sortOrder = (gvInputModule.Rows[i].FindControl("txtSortOrder") as TextBox).Text;
                int result=DBAccess.UpdateInputModuleRename(inputID, rename,sortOrder, "Update");
                if (result.Equals(0))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);
                    return;

                }
            }
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('Record saved successfully!')</script>", false);
            BindGridView();
        }
    }
}