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
    public partial class AssignValueForDependency : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                bindParameter1List();
                bindParameterDependencyValue(ddlIP1.SelectedItem==null? "" : ddlIP1.SelectedItem.ToString());
            }
        }
        private void bindParameter1List()
        {
            List<ParameterDependency> parameterDependency = new List<ParameterDependency>();
            List<string> listParam1= DBAccess.TgetParameter1List("Detail", "ParameterList", out parameterDependency);
            ddlIP1.DataSource = listParam1;
            ddlIP1.DataBind();
            string param1 = ddlIP1.SelectedItem ==null? "" : ddlIP1.SelectedItem.ToString();
            foreach(ParameterDependency d in parameterDependency)
            {
                if(d.Parameter1==param1)
                {
                    txtIP2.Text = d.Parameter2;
                    return;
                }
                else
                {
                    txtIP2.Text = "";
                }
            }
        }

        protected void ddlIP1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ParameterDependency> parameterDependency = new List<ParameterDependency>();
            List<string> listParam1 = DBAccess.TgetParameter1List("Detail", "ParameterList", out parameterDependency);
            string param1 = ddlIP1.SelectedItem == null ? "" : ddlIP1.SelectedItem.ToString();
            foreach (ParameterDependency d in parameterDependency)
            {
                if (d.Parameter1 == param1)
                {
                    txtIP2.Text = d.Parameter2;
                    return;
                }
                else
                {
                    txtIP2.Text = "";
                }
            }
        }

        private void bindParameterDependencyValue(string param1)
        {
            List<ParameterDependency> parameterDependency = new List<ParameterDependency>();
            List<string> param2ddllist;
            parameterDependency = DBAccess.getParameterDependencyValues(param1, out param2ddllist);
            gvDependencyTransaction.DataSource = parameterDependency;
            gvDependencyTransaction.DataBind();
            int t = 0;
            foreach(ParameterDependency data in parameterDependency)
            {
                DropDownList ddl1 = (gvDependencyTransaction.Rows[t].FindControl("ddlParam2Value") as DropDownList);
                ddl1.DataSource = param2ddllist;
                ddl1.DataBind();
                ddl1.Items.Insert(0, new ListItem("", ""));
                if (data.Parameter2Value != "" || data.Parameter2Value !=null)
                {
                    ddl1.Text = data.Parameter2Value;
                }
                t++;
            }

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            bindParameterDependencyValue(ddlIP1.SelectedItem == null ? "" : ddlIP1.SelectedItem.ToString());
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ParameterDependency parameterDependency = new ParameterDependency();
            for(int i=0;i<gvDependencyTransaction.Rows.Count;i++)
            {
                parameterDependency.Parameter1 = (gvDependencyTransaction.Rows[i].FindControl("lblParam1") as Label).Text;
                parameterDependency.Parameter1Value = (gvDependencyTransaction.Rows[i].FindControl("lblParam1Value") as Label).Text;
                parameterDependency.Parameter2 = (gvDependencyTransaction.Rows[i].FindControl("lblParam2") as Label).Text;
                parameterDependency.Parameter2Value = (gvDependencyTransaction.Rows[i].FindControl("ddlParam2Value") as DropDownList).Text;
                int result=DBAccess.saveParameterDependencyValue(parameterDependency);
                if (result.Equals(0))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Records insertion failed.');", true);
                    return;

                }
            }
            bindParameterDependencyValue(ddlIP1.SelectedItem == null ? "" : ddlIP1.SelectedItem.ToString());
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('Records saved successfully!')</script>", false);
        }
    }
}