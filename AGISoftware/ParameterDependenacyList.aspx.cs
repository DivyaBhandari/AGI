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
    public partial class ParameterDependenacyList : System.Web.UI.Page
    {
        static List<ParameterDependency> listparameterDependencies;
        static string ParameterName = "", ParameterID="";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                //Response.Write(Request.QueryString["param"]);

                if (Session["ParameterNameandID"] != null)
                {
                    // ParameterName = Request.QueryString["param"].Split(',')[0];
                    //ParameterID = Request.QueryString["param"].Split(',')[1];
                    ParameterName = Session["ParameterNameandID"].ToString().Split(',')[0];
                    ParameterID = Session["ParameterNameandID"].ToString().Split(',')[1];
                    Session["ParameterNameandID"] = null;
                }
                else
                {
                    ParameterName = "";
                    ParameterID = "";
                }
              
                BindDropdownValue();
                btnView_Click(sender, e);
            }
        }
        private void BindDropdownValue()
        {
            listparameterDependencies = DBAccess.getParameterRelationData();
            if(listparameterDependencies.Count>0)
            {
                var distinctDependentParam = listparameterDependencies.Select(x =>new { x.Parameter1, x.ParameterId1 }).Distinct().ToList();
                //var distinctDependentParamID = listparameterDependencies.Select(x => x.Parameter1).Distinct().ToList();
                ddlDependentParam.DataSource = distinctDependentParam;
                ddlDependentParam.DataTextField = "Parameter1";
                ddlDependentParam.DataValueField = "ParameterId1";
                ddlDependentParam.DataBind();

               
                if (ParameterName == "" || ParameterName == null)
                {
                   
                }
                else
                {
                    try
                    {
                        ddlDependentParam.ClearSelection();
                        ddlDependentParam.Items.FindByValue(ParameterID).Selected = true;
                    }
                    catch(Exception ex)
                    {

                    }
                   
                }

                var independentParam = listparameterDependencies.Where(x => x.Parameter1 == ddlDependentParam.SelectedItem.Text).Select(x => x.Parameter2Value).ToList();
                ddlIndependentParam.DataSource = independentParam;
                ddlIndependentParam.DataBind();
               
            }
           
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void setParameterNameNull()
        {

            HttpContext.Current.Session["ParameterNameandID"] = null;
        }

        protected void ddlDependentParam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listparameterDependencies.Count > 0)
            {
                var independentParam = listparameterDependencies.Where(x => x.Parameter1 == ddlDependentParam.SelectedItem.Text).Select(x => x.Parameter2Value).ToList();
                ddlIndependentParam.DataSource = independentParam;
                ddlIndependentParam.DataBind();
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            List<ParameterDependency> listparameterDependency = DBAccess.getParameterRelationList(ddlDependentParam.SelectedItem == null ? "": ddlDependentParam.SelectedItem.Text, ddlDependentParam.SelectedValue==null?"":ddlDependentParam.SelectedValue, ddlIndependentParam.SelectedItem==null?"":ddlIndependentParam.SelectedItem.Text);
            gvDependencyTransaction.DataSource = listparameterDependency;
            gvDependencyTransaction.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                for (int i = 0; i < gvDependencyTransaction.Rows.Count; i++)
                {
                    ParameterDependency parameterDependency = new ParameterDependency();
                    parameterDependency.Parameter1 = (gvDependencyTransaction.Rows[i].FindControl("lblDependentParam") as Label).Text;
                    parameterDependency.ParameterId1 = (gvDependencyTransaction.Rows[i].FindControl("hdDependentParamID") as HiddenField).Value;
                    parameterDependency.Parameter2 = (gvDependencyTransaction.Rows[i].FindControl("hdIndependentParam") as HiddenField).Value;
                    parameterDependency.ParameterId2 = (gvDependencyTransaction.Rows[i].FindControl("hdIndependentParamID") as HiddenField).Value;
                    parameterDependency.Parameter2Value = (gvDependencyTransaction.Rows[i].FindControl("lblIndependentParamValue") as Label).Text;
                    parameterDependency.LSL = (gvDependencyTransaction.Rows[i].FindControl("txtLSL") as TextBox).Text;
                    parameterDependency.USL = (gvDependencyTransaction.Rows[i].FindControl("txtUSL") as TextBox).Text;
                    if (parameterDependency.LSL == "" && parameterDependency.USL == "")
                    {
                        continue;
                    }
                    string result = DBAccess.saveUpdateParameterRelationshipData(parameterDependency);
                    if(result== "Saved")
                    {

                    }else if(result== "Not Saved")
                    {

                        ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningrModal();", true);
                        return;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Records insertion failed.');", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            btnView_Click(sender, e);
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('Records saved successfully!')</script>", false);
        }
    }
}