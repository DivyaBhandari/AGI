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
    public partial class DeleteSDoc : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //BindSDoclistForDelete();
                BindSDoclistForRestore();
                btnView_Click(sender, e);
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

        private void BindSDoclistForRestore()
        {
            List<string> SdocRestore = DBAccess.getSDocForDelete("Restore", "SDocList");
            ddlRestoreSdoc.DataSource = SdocRestore;
            ddlRestoreSdoc.DataBind();
        }

        protected void deleteSdocYes_ServerClick(object sender, EventArgs e)
        {
           int result= DBAccess.DeleteSDoc(ddlRestoreSdoc.SelectedItem==null ? "" : ddlRestoreSdoc.SelectedItem.ToString(), Session["EmpName"].ToString(), "Delete");
            if (result.Equals(0))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);
                return;
            }
            BindSDoclistForRestore();
            btnView_Click(sender, e);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('SDocID deleted successfully!')</script>", false);
        }

        protected void btnRestoreSDoc_Click(object sender, EventArgs e)
        {
            int result=DBAccess.DeleteSDoc(ddlRestoreSdoc.SelectedItem == null ? "" : ddlRestoreSdoc.SelectedItem.ToString(), Session["EmpName"].ToString(), "Restore");
            //BindSDoclistForDelete();
            if (result.Equals(0))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);
                return;
            }
            BindSDoclistForRestore();
            btnView_Click(sender, e);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('SDocID restored successfully!')</script>", false);
        }

    

        protected void btnView_Click(object sender, EventArgs e)
        {
            List<DeletedSDocDetails> listDeletedDetails = DBAccess.getDeletedSDocDetails(ddlRestoreSdoc.SelectedItem == null ? "" : ddlRestoreSdoc.SelectedItem.ToString(), Session["EmpName"].ToString(), "View");
            lvGeneralInfo.DataSource = listDeletedDetails;
            lvGeneralInfo.DataBind();
            if(lvGeneralInfo.Items.Count>0)
            {
                tbl.Visible = true;
            }
            else
            {
                tbl.Visible = false;
            }
            for(int i=0;i<listDeletedDetails.Count;i++)
            {
                username.InnerText = listDeletedDetails[i].DeletedBy;
                deletedDate.InnerText = listDeletedDetails[i].DeletedDate == "" ? "" : Convert.ToDateTime(listDeletedDetails[i].DeletedDate).ToString("dd/MM/yyyy HH:mm:ss");
            }
        }

        protected void restore_ServerClick(object sender, EventArgs e)
        {
           int result= DBAccess.DeleteSDoc(ddlRestoreSdoc.SelectedItem == null ? "" : ddlRestoreSdoc.SelectedItem.ToString(), Session["EmpName"].ToString(), "Restore");
            if (result.Equals(0))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);
                return;
            }
            //BindSDoclistForDelete();
            BindSDoclistForRestore();
            btnView_Click(sender, e);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('SDocID restored successfully!')</script>", false);
        }
    }
}