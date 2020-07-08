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
    public partial class OperatorDetailsMaster : System.Web.UI.Page
    {
        List<EmployeeDetails> listEmp = new List<EmployeeDetails>();
        static string empid = "";
       // EmployeeDetails emp = new EmployeeDetails();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindEmployeeDetails("");
            }
            if (Session["EmpName"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
        }
        private void BindEmployeeDetails(string empid)
        {
            listEmp = DBAccess.getEmployeeDetails(empid);
            gvEmpDetails.DataSource = listEmp;
            gvEmpDetails.DataBind();
            int i = 0;
            foreach (EmployeeDetails data in listEmp)
            {
                (gvEmpDetails.Rows[i].FindControl("txtpassword") as TextBox).Attributes.Add("value", data.password);
                (gvEmpDetails.Rows[i].FindControl("ddlrole") as DropDownList).Text=data.role;

                i++;

            }
        }

        protected void gvEmpDetails_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmpDetails.EditIndex = e.NewEditIndex;
            listEmp = DBAccess.getEmployeeDetails("");

            string role = "Admin", password="";
            for (int i = 0; i < listEmp.Count; i++)
            {
                if (i == e.NewEditIndex)
                {
                    role = listEmp[i].role;
                    password= listEmp[i].password;
                }
            }
            gvEmpDetails.DataSource = listEmp;
            gvEmpDetails.DataBind();
            (gvEmpDetails.Rows[e.NewEditIndex].FindControl("ddlrole") as DropDownList).Text = role;
            (gvEmpDetails.Rows[e.NewEditIndex].FindControl("txtpassword") as TextBox).Attributes.Add("value", password);
           
        }

        protected void gvEmpDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            EmployeeDetails emp = new EmployeeDetails();
            emp.id = (gvEmpDetails.Rows[e.RowIndex].FindControl("lblid") as Label).Text;
            emp.Name = (gvEmpDetails.Rows[e.RowIndex].FindControl("txtname") as TextBox).Text;
            emp.role = (gvEmpDetails.Rows[e.RowIndex].FindControl("ddlrole") as DropDownList).SelectedItem.ToString();
            emp.email = (gvEmpDetails.Rows[e.RowIndex].FindControl("txtemail") as TextBox).Text;
            emp.mblno = (gvEmpDetails.Rows[e.RowIndex].FindControl("txtmblno") as TextBox).Text;
            emp.password = (gvEmpDetails.Rows[e.RowIndex].FindControl("txtpassword") as TextBox).Text;
            emp.AdminName = Session["EmpName"].ToString();
            int success = DBAccess.saveEmployeeDetails(emp, "Update");
            gvEmpDetails.EditIndex = -1;
            BindEmployeeDetails("");
        }

        protected void gvEmpDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            gvEmpDetails.EditIndex = -1;
            BindEmployeeDetails("");
        }

        protected void gvEmpDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            //GridViewRow row = gvEmpDetails.Rows[rowIndex];
            //string empid = (row.FindControl("txtid") as Label).Text;
             empid = (gvEmpDetails.Rows[e.RowIndex].FindControl("lblid") as Label).Text;
            ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openConfirmModal('Are you sure, you want to delete this record?');", true);
        }

      

        protected void newEmp_Click(object sender, EventArgs e)
        {
            gvEmpDetails.ShowFooter = true;
            BindEmployeeDetails("");
            Button1.Visible = true;
            newEmp.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "setScrollPosition();", true);
            (gvEmpDetails.FooterRow.FindControl("newtxtid") as TextBox).Focus();
        }

        protected void addNewEmp_Click(object sender, EventArgs e)
        {
            EmployeeDetails emp = new EmployeeDetails();
            emp.id = (gvEmpDetails.FooterRow.FindControl("newtxtid") as TextBox).Text;
            if (emp.id=="")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('Employee Id required.');", true);
                return;

            }

            emp.AdminName = Session["EmpName"].ToString();
            emp.Name = (gvEmpDetails.FooterRow.FindControl("newtxtname") as TextBox).Text;
            emp.role = (gvEmpDetails.FooterRow.FindControl("newddlrole") as DropDownList).SelectedItem.ToString();
            emp.email = (gvEmpDetails.FooterRow.FindControl("newtxtemail") as TextBox).Text;
            emp.mblno = (gvEmpDetails.FooterRow.FindControl("newtxtmblno") as TextBox).Text;
            emp.password = (gvEmpDetails.FooterRow.FindControl("newtxtpassword") as TextBox).Text;

            int success = DBAccess.saveEmployeeDetails(emp, "New");
            if (success.Equals(0))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);
                return;

            }
            else  if (success < 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('Employee ID already exists.');", true);
                return;
            }


            gvEmpDetails.ShowFooter = false;
            BindEmployeeDetails("");
            Button1.Visible = false;
            newEmp.Visible = true;
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('Record saved successfully!')</script>", false);
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvEmpDetails.Rows[rowIndex];
            string empid = (row.FindControl("txtid") as Label).Text;
            int success = DBAccess.deleteEmployeeDetails(empid);
            gvEmpDetails.EditIndex = -1;
            BindEmployeeDetails("");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            gvEmpDetails.ShowFooter = false;
            BindEmployeeDetails("");
            Button1.Visible = false;
            newEmp.Visible = true;
        }

        protected void saveConfirmYes_ServerClick(object sender, EventArgs e)
        {
            int success = DBAccess.deleteEmployeeDetails(empid);
            gvEmpDetails.EditIndex = -1;
            BindEmployeeDetails("");
        }

        protected void save_Click(object sender, EventArgs e)
        {
            if(gvEmpDetails.ShowFooter)
            {
                EmployeeDetails emp = new EmployeeDetails();
                emp.id = (gvEmpDetails.FooterRow.FindControl("newtxtid") as TextBox).Text;
                if (emp.id == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('Employee Id required.');", true);
                    return;

                }

                emp.AdminName = Session["EmpName"].ToString();
                emp.Name = (gvEmpDetails.FooterRow.FindControl("newtxtname") as TextBox).Text;
                emp.role = (gvEmpDetails.FooterRow.FindControl("newddlrole") as DropDownList).SelectedItem.ToString();
                emp.email = (gvEmpDetails.FooterRow.FindControl("newtxtemail") as TextBox).Text;
                emp.mblno = (gvEmpDetails.FooterRow.FindControl("newtxtmblno") as TextBox).Text;
                emp.password = (gvEmpDetails.FooterRow.FindControl("newtxtpassword") as TextBox).Text;

                int success = DBAccess.saveEmployeeDetails(emp, "New");
                if (success.Equals(0))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);
                    return;

                }
                else if (success < 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('Employee ID already exists.');", true);
                    return;
                }


                gvEmpDetails.ShowFooter = false;
                BindEmployeeDetails("");
                Button1.Visible = false;
                newEmp.Visible = true;
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('Record saved successfully!')</script>", false);
            }
            else
            {
                for (int i = 0; i < gvEmpDetails.Rows.Count; i++)
                {
                    EmployeeDetails emp = new EmployeeDetails();
                    emp.id = (gvEmpDetails.Rows[i].FindControl("lblid") as Label).Text;
                    emp.Name = (gvEmpDetails.Rows[i].FindControl("txtname") as TextBox).Text;
                    emp.role = (gvEmpDetails.Rows[i].FindControl("ddlrole") as DropDownList).SelectedItem.ToString();
                    emp.email = (gvEmpDetails.Rows[i].FindControl("txtemail") as TextBox).Text;
                    emp.mblno = (gvEmpDetails.Rows[i].FindControl("txtmblno") as TextBox).Text;
                    emp.password = (gvEmpDetails.Rows[i].FindControl("txtpassword") as TextBox).Text;
                    emp.AdminName = Session["EmpName"].ToString();
                    int success = DBAccess.saveEmployeeDetails(emp, "Update");
                    if (success.Equals(0))
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Records insertion failed.');", true);
                        return;

                    }
                    //gvEmpDetails.EditIndex = -1;

                }
                BindEmployeeDetails("");
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('Records saved successfully!')</script>", false);
            }
          
        }
    }
}