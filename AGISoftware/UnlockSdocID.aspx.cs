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
    public partial class UnlockSdocID : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindSDoclistForUnlock();
                btnView_Click(sender, e);
            }

            if (Session["EmpName"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
        }
        private void BindSDoclistForUnlock()
        {
            List<string> SdocUnlock = DBAccess.getSDocForUnlock("SDocList");
            ddlUnlockSdoc.DataSource = SdocUnlock;
            ddlUnlockSdoc.DataBind();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            List<DeletedSDocDetails> listDeletedDetails = DBAccess.getLockedSDocDetails(ddlUnlockSdoc.SelectedItem == null ? "":ddlUnlockSdoc.SelectedItem.ToString(), Session["EmpName"].ToString(), "View");
            lvGeneralInfo.DataSource = listDeletedDetails;
            lvGeneralInfo.DataBind();
            if (lvGeneralInfo.Items.Count > 0)
            {
                tbl.Visible = true;
            }
            else
            {
                tbl.Visible = false;
            }
            for (int i = 0; i < listDeletedDetails.Count; i++)
            {
                username.InnerText = listDeletedDetails[i].DeletedBy;
                lockedDate.InnerText = listDeletedDetails[i].DeletedDate == "" ? "" : Convert.ToDateTime(listDeletedDetails[i].DeletedDate).ToString("dd/MM/yyyy HH:mm:ss");
            }
        }

        protected void unlockSdocYes_ServerClick(object sender, EventArgs e)
        {
           int result= DBAccess.UnlockSDoc(ddlUnlockSdoc.SelectedItem == null ? "" : ddlUnlockSdoc.SelectedItem.ToString(), Session["EmpName"].ToString(), "Save");
            if (result.Equals(0))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);
                return;
            }
            BindSDoclistForUnlock();
            btnView_Click(sender, e);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('SDocID unlocked successfully!')</script>", false);
        }
    }
}