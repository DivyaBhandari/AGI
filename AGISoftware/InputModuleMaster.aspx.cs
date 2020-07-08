using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AGISoftware.DataBaseAccess;
using System.Data;

namespace AGISoftware
{
    public partial class InputModuleMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        private void BindGridView()
        {
            DataTable dt = new DataTable();
            dt = DBAccess.GetInputModuleDetails();
            gvInputModule.DataSource = dt;
            gvInputModule.DataBind();
        }

        protected void gvInputModule_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvInputModule.Rows[rowIndex];
            int id = Convert.ToInt32((row.FindControl("hdfId") as HiddenField).Value);
            if (DBAccess.DeleteInputModuleData(id))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsDeleted", "openSuccessModal('Record Deleted Successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsDeletedFailed", "openWarningModal('Can not Delete Record!');", true);
            }
            BindGridView();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            gvInputModule.ShowFooter = true;
            BindGridView();
            btnNew.Enabled = false;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string inputModule = (gvInputModule.FooterRow.FindControl("txtInputModule") as TextBox).Text;
            string subInputModule = (gvInputModule.FooterRow.FindControl("txtSubInputModule") as TextBox).Text;
            if (DBAccess.SaveInputModuleData(inputModule, subInputModule))
            {
                gvInputModule.ShowFooter = false;
                BindGridView();
                btnNew.Enabled = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsDeleted", "openSuccessModal('Record Saved Successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsDeletedFailed", "openWarningModal('Can not Add row!');", true);
            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            gvInputModule.ShowFooter = false;
            BindGridView();
            btnNew.Enabled = true;
        }
    }
}