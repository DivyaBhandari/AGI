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
    public partial class AssignPagesforUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindEmployeeID();
                bindPages();
                ddlEmployeeId_SelectedIndexChanged(sender, e);
               // bindRoleAccessRight(ddlEmployeeId.SelectedItem==null? "" : ddlEmployeeId.SelectedItem.ToString());

                //ddlRole_SelectedIndexChanged(sender, e);
            }
        }
        private void bindEmployeeID()
        {
            List<string> EmpIdlist = DBAccess.GetEmployeeID();
            ddlEmployeeId.DataSource = EmpIdlist;
            //ddlEmployeeId.DataTextField = "EmpId";
            //ddlEmployeeId.DataValueField = "EmpId";
            ddlEmployeeId.DataBind();
        }
        private void bindPages()
        {
            List<RoleAccessRight> listPages = new List<RoleAccessRight>();

            listPages.Add(assignPageName("Transactions", "Transactions"));
            listPages.Add(assignPageName("Application Tool Kit", "ApplicationToolKit"));
            listPages.Add(assignPageName("Data Input Module", "DataInputModule"));
            listPages.Add(assignPageName("Derived Parameters", "DerivedParameters"));
            listPages.Add(assignPageName("Output Module", "OutputModules"));
            listPages.Add(assignPageName("Signal Processing", "SignalProcess"));
            cblTransactionPages.DataSource = listPages;
            cblTransactionPages.DataTextField = "PageNameForText";
            cblTransactionPages.DataValueField = "PageNameForValue";
            cblTransactionPages.DataBind();

            List<RoleAccessRight> listMasterPages = new List<RoleAccessRight>();

            listMasterPages.Add(assignPageName("Masters", "Masters"));
            listMasterPages.Add(assignPageName("Parameter List", "MasterData"));
            listMasterPages.Add(assignPageName("Parameter Data", "ParameterMaster"));
            listMasterPages.Add(assignPageName("Employee Details", "OperatorDetailsMaster"));
            listMasterPages.Add(assignPageName("Parameter Dependency", "ParametersRelationshipMaster"));
            listMasterPages.Add(assignPageName("Assign Value For Dependency", "AssignValueForDependency"));
            listMasterPages.Add(assignPageName("Delete SDocID", "DeleteSDoc"));
            listMasterPages.Add(assignPageName("Unlock SdocID", "UnlockSdocID"));
            listMasterPages.Add(assignPageName("Input Module", "InputModuleMasterView"));
            listMasterPages.Add(assignPageName("User Access Rights", "AssignPagesforUser"));
            listMasterPages.Add(assignPageName("Parameter Relationship", "ParameterDependenacyList"));
            
            cblMasterPages.DataSource = listMasterPages;
            cblMasterPages.DataTextField = "PageNameForText";
            cblMasterPages.DataValueField = "PageNameForValue";
            cblMasterPages.DataBind();
        }

        private RoleAccessRight assignPageName(string text,string value)
        {
            RoleAccessRight roleAccessRight = new RoleAccessRight();
            roleAccessRight.PageNameForText = text;
            roleAccessRight.PageNameForValue = value;
            return roleAccessRight;
        }
        private void bindRoleAccessRight(string empid)
        {
            List<RoleAccessRight> listroleAccessRights = DBAccess.GetRoleAccessRight(empid);
            gvRoleAccessRight.DataSource = listroleAccessRights;
            gvRoleAccessRight.DataBind();
            //if( listroleAccessRights.Count== cblTransactionPages.Items.Count-1)
            //{
            //    cblTransactionPages.Items[0].Selected = true;
            //}
            int tcount = 0, mcount=0;
            foreach (System.Web.UI.WebControls.ListItem item in cblTransactionPages.Items)
            {
                if (item.Selected)
                {
                    tcount++;
                }
            }
            if(tcount== cblTransactionPages.Items.Count - 1)
            {
                cblTransactionPages.Items[0].Selected = true;
            }
            foreach (System.Web.UI.WebControls.ListItem item in cblMasterPages.Items)
            {
                if (item.Selected)
                {
                    mcount++;
                }
            }
            if (mcount == cblMasterPages.Items.Count - 1)
            {
                cblMasterPages.Items[0].Selected = true;
            }

        }

        protected void Save_Click(object sender, EventArgs e)
        {
            string empid = ddlEmployeeId.SelectedItem==null? "" : ddlEmployeeId.SelectedItem.ToString();
            foreach (System.Web.UI.WebControls.ListItem item in cblTransactionPages.Items)
            {
                if(item.Value== "Transactions")
                {
                    continue;
                }
                string page = item.Value;
                int visibility = 0;
                if (item.Selected)
                {
                    visibility = 1;
                }
                int result = DBAccess.InsertUpdateRoleAccessRight(empid, page, visibility);
                if (result.Equals(0))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);
                    return;

                }
            }
            foreach (System.Web.UI.WebControls.ListItem item in cblMasterPages.Items)
            {
                if (item.Value == "Masters")
                {
                    continue;
                }
                string page = item.Value;
                int visibility = 0;
                if (item.Selected)
                {
                    visibility = 1;
                }
               int result= DBAccess.InsertUpdateRoleAccessRight(empid, page, visibility);
                if (result.Equals(0))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);
                    return;

                }
            }
           bindRoleAccessRight(empid);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('Record saved successfully!')</script>", false);
        }

        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            //foreach (System.Web.UI.WebControls.ListItem item in dcPages.Items)
            //{
            //        item.Selected = false;
            //}

            //List<string> listPageList = DBAccess.GetPageListForRole(ddlRole.SelectedItem==null? "": ddlRole.SelectedItem.ToString());
            //foreach (string ls in listPageList)
            //{
            //    foreach (System.Web.UI.WebControls.ListItem item in dcPages.Items)
            //    {
            //        if(item.Text==ls)
            //        {
            //            item.Selected = true;
            //        }
            //        //item.Selected = listPageList.where(s => s.)
            //    }

            //}
        }

        protected void ddlEmployeeId_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string empname;
                string empid = ddlEmployeeId.SelectedItem == null ? "" : ddlEmployeeId.SelectedItem.ToString();
                List<RoleAccessRight> Pagelist = DBAccess.GetPageListForRole(empid, out empname);
                txtempname.Text = empname;
                //if (Pagelist.Count > 0)
                //{
                foreach (System.Web.UI.WebControls.ListItem item in cblTransactionPages.Items)
                {
                    item.Selected = Pagelist.Where(s => s.Page.Equals(item.Value, StringComparison.OrdinalIgnoreCase)).Select(s => s.visibilty).FirstOrDefault();

                }
                foreach (System.Web.UI.WebControls.ListItem item in cblMasterPages.Items)
                {
                    item.Selected = Pagelist.Where(s => s.Page.Equals(item.Value, StringComparison.OrdinalIgnoreCase)).Select(s => s.visibilty).FirstOrDefault();

                }
                bindRoleAccessRight(empid);
                //}

            }
            catch (Exception ex)
            {

            }
        }
    }
}