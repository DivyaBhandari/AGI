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
    public partial class ParametersRelationshipMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<ParameterDependency> param1List = DBAccess.getParameter1List("Master", "ParameterList");
                //Bind InputModule
                var inputModuleList = param1List.Select(x => x.InputModule).Distinct().ToList();
                ddlInputModule.DataSource = inputModuleList;
                ddlInputModule.DataBind();

                //Bind Dependency Parameter
                string inputModule = ddlInputModule.SelectedItem == null ? "" : ddlInputModule.SelectedItem.ToString();
                var param1= (from v in param1List where v.InputModule == inputModule select v.Parameter1);
                ddlIP1.DataSource = param1;
                ddlIP1.DataBind();

                //Bind Independent Parameter
                setParm2List();

                bindParameterDependency();
            }
        }

        protected void ddlIP1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setParm2List();
        }

        private void setParm2List()
        {
            string inputModule = ddlInputModule.SelectedItem.ToString();
            string param1 = ddlIP1.SelectedItem.ToString();
            List<ParameterDependency> param1List = DBAccess.getParameter1List("Master", "ParameterList");
            var param2List = (from v in param1List where v.InputModule == inputModule && v.Parameter1!= param1 select v.Parameter1);
            ddlIP2.DataSource = param2List;
            ddlIP2.DataBind();
        }

        protected void btnNewParameterDEpendency_Click(object sender, EventArgs e)
        {
            string param1 = ddlIP1.SelectedItem==null? "": ddlIP1.SelectedItem.ToString();
            string param2 = ddlIP2.SelectedItem == null ? "" : ddlIP2.SelectedItem.ToString() ;
            if(param1=="" || param2=="")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('Dependent and Independent parameters required.');", true);
                return;
            }
            int result= DBAccess.saveParameterDependency(param1, param2, ddlInputModule.SelectedItem == null? "" : ddlInputModule.SelectedItem.ToString());
            if (result.Equals(0))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);
                return;
            }
            bindParameterDependency();
            ddlInputModule_SelectedIndexChanged(sender, e);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('Record saved successfully!')</script>", false);
        }

        private void bindParameterDependency()
        {
            List<ParameterDependency> listPramDependency = DBAccess.getDependencyParameter();
            gvDependencyMaster.DataSource = listPramDependency;
            gvDependencyMaster.DataBind();
            int p = 0;
            foreach (ParameterDependency data in listPramDependency)
            {
                string param1 = (gvDependencyMaster.Rows[p].FindControl("lblParam1") as Label).Text;
                string inputModule= (gvDependencyMaster.Rows[p].FindControl("param1InputModule") as HiddenField).Value;
                DropDownList ddl1 = (gvDependencyMaster.Rows[p].FindControl("ddlParam2") as DropDownList);
                ddl1.DataSource = setParm2Listnew(param1, inputModule);
                ddl1.DataBind();
                ddl1.Items.Insert(0, new ListItem(data.Parameter2, ""));
                if (data.Parameter2 != "")
                {
                    ddl1.Text = data.Parameter2;
                }
                p++;
            }
        }

        private dynamic setParm2Listnew(string param1, string inputModule)
        {
            List<ParameterDependency> param1List = DBAccess.getParameter1List("Master", "ParameterList");
            var param2List = (from v in param1List where v.InputModule == inputModule && v.Parameter1 != param1 select v.Parameter1);
            //List<string> param2List = new List<string>();
            //for (int i = 0; i < param1List.Count; i++)
            //{
            //    string s = param1List[i].Parameter1;
            //    if (param1List[i].Parameter1 == param1)
            //    {
            //        continue;
            //    }
            //    else
            //    {
            //        param2List.Add(param1List[i].Parameter1);
            //    }
            //}
            return param2List;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ParameterDependency parameterDependency = new ParameterDependency();

            for (int i = 0; i < gvDependencyMaster.Rows.Count; i++)
            {
               string param1=  (gvDependencyMaster.Rows[i].FindControl("lblParam1") as Label).Text;
                string param2 = (gvDependencyMaster.Rows[i].FindControl("ddlParam2") as DropDownList).SelectedItem.ToString();
                if (param1 == "" || param2 == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('Dependent and Independent parameters required.');", true);
                    return;
                }
                int result=DBAccess.saveParameterDependency(param1, param2, ddlInputModule.SelectedItem == null ? "" : ddlInputModule.SelectedItem.ToString());
                if (result.Equals(0))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Records insertion failed.');", true);
                    return;

                }
            }
            bindParameterDependency();
            ddlInputModule_SelectedIndexChanged(sender, e);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('Records saved successfully!')</script>", false);
        }

        protected void ddlInputModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<ParameterDependency> param1List = DBAccess.getParameter1List("Master", "ParameterList");
            //Bind Dependency Parameter
            string inputModule = ddlInputModule.SelectedItem == null ? "" : ddlInputModule.SelectedItem.ToString();
            var param1 = (from v in param1List where v.InputModule == inputModule select v.Parameter1);
            ddlIP1.DataSource = param1;
            ddlIP1.DataBind();

            //Bind Independent Parameter
            setParm2List();
        }
    }
}