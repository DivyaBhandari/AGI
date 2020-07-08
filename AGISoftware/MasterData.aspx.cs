using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using AGISoftware.DataBaseAccess;

namespace AGISoftware
{
    public partial class MasterData : System.Web.UI.Page
    {
        List<string> allInputModules = new List<string>();
        List<string> allSubInputModules = new List<string>();
        List<string> allParameters = new List<string>();
        DataTable dt = new DataTable();
        static string DeleteParameterID ;
        static string Deletevalue;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ////////////////
                // BindFilterList();
                bindTopDDL();
                bindNewDDL();
                btnView_Click(null, EventArgs.Empty);
            }
            if (Session["EmpName"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }

        }

        private void BindGridView()
        {
            dt = DBAccess.GetParameterListValue(ddlInputModule.SelectedValue.Equals("ALL") ? string.Empty : ddlInputModule.SelectedValue, ddlSubInputModule.SelectedValue, ddlParameter.SelectedValue);
            gvParameterList.DataSource = dt;
            gvParameterList.DataBind();
            for(int i=0;i<gvParameterList.Rows.Count;i++)
            {
                string paramdt = (gvParameterList.Rows[i].FindControl("hiddenParamDataType") as HiddenField).Value;
                if(paramdt== "Integer")
                {
                    (gvParameterList.Rows[i].FindControl("txtListValue") as TextBox).CssClass += " allowDecimal ";

                }
               
            }
        }

        //private void BindFilterList()
        //{
        //    bindTopDDL();

        //    allInputModules = DBAccess.GetAllInputModules();
        //    allInputModules.Insert(0, "ALL");
        //    ddlInputModule.DataSource = allInputModules;
        //    ddlInputModule.DataBind();
        //    allInputModules = DBAccess.GetAllNewInputModules();
        //    ddlIpModule.DataSource = allInputModules;
        //    ddlIpModule.DataBind();

        //    //allSubInputModules.Add("");
        //    //ddlSubInputModule.DataSource = allSubInputModules;
        //    //ddlSubInputModule.DataBind();
        //    List<string> allSipModules = DBAccess.GetAllSipModules();
        //    ddlSipModule.DataSource = allSipModules;


        //    allParameters.Add("");
        //    ddlParameter.DataSource = allParameters;
        //    ddlParameter.DataBind();


        //    string ipModule = ddlIpModule.SelectedValue.ToString();
        //    allSubInputModules = DBAccess.GetAllSubInputModules_MasterData(ipModule);
        //    ddlSipModule.DataSource = allSubInputModules;
        //    ddlSipModule.DataBind();

        //    string sipModule = ddlSipModule.SelectedValue.ToString();
        //    allParameters = DBAccess.GetAllParameters_MasterData(ipModule, sipModule);
        //    //allParameters.Insert(0, "All");
        //    ddlParam.DataSource = allParameters;
        //    ddlParam.DataBind();

        //}
        private void bindNewDDL()
        {
            allInputModules = DBAccess.GetAllNewInputModules();
            ddlIpModule.DataSource = allInputModules;
            ddlIpModule.DataBind();
            string ipModule = ddlIpModule.SelectedValue.ToString();
            allSubInputModules = DBAccess.GetAllSubInputModules_MasterData(ipModule);
            ddlSipModule.DataSource = allSubInputModules;
            ddlSipModule.DataBind();
            string sipModule = ddlSipModule.SelectedValue.ToString();
            //allParameters = DBAccess.GetAllParameters_MasterData(ipModule, sipModule);
            //allParameters.Insert(0, "All");
            ddlParam.DataSource = DBAccess.GetAllParameters_MasterData(ipModule, sipModule);
            ddlParam.DataTextField = "Parameter";
            ddlParam.DataValueField = "ParameterID";
            ddlParam.DataBind();
            addorremoceDecimalclass(ddlParam.SelectedValue.Split(';')[1]);
        }
        private void bindTopDDL()
        {
            /////////////////
            try
            {
                allInputModules = DBAccess.GetAllInputModules();
                allInputModules.Insert(0, "ALL");
                ddlInputModule.DataSource = allInputModules;
                ddlInputModule.DataBind();
                ddlSubInputModule.DataSource = DBAccess.GetSubInputModule(ddlInputModule.SelectedItem.Text);
                ddlSubInputModule.DataBind();
                ddlParameter.DataSource = DBAccess.getParameterDetails(ddlInputModule.SelectedItem.Text, ddlSubInputModule.SelectedValue);
                ddlParameter.DataTextField = "Parameter";
                ddlParameter.DataValueField = "ParameterID";
                ddlParameter.DataBind();
                ddlParameter.Items.Insert(0, new ListItem("", ""));
            }
            catch (Exception ex)
            { }
        }

        protected void ddlInputModule_SelectedIndexChanged(object sender, EventArgs e)
        {

            string ipModule = ddlInputModule.SelectedValue.ToString();
            if (ipModule.Equals("ALL"))
            {
                ddlSubInputModule.DataSource = DBAccess.GetSubInputModule(ddlInputModule.SelectedItem.Text);
                ddlSubInputModule.DataBind();
                ddlParameter.DataSource = DBAccess.getParameterDetails(ddlInputModule.SelectedItem.Text, ddlSubInputModule.SelectedValue);
                ddlParameter.DataTextField = "Parameter";
                ddlParameter.DataValueField = "ParameterID";
                ddlParameter.DataBind();
                ddlParameter.Items.Insert(0, new ListItem("", ""));

                //allInputModules = DBAccess.GetAllInputModules();
                //allInputModules.Insert(0, "ALL");
                //allSubInputModules.Add("");
                //ddlSubInputModule.DataSource = allSubInputModules;
                //ddlSubInputModule.DataBind();
                //allParameters.Add("");
                //List<ListItem> list = new List<ListItem>();
                //list.Add(new ListItem("", ""));
                //ddlParameter.DataSource = list;
                //ddlParameter.DataTextField = "Text";
                //ddlParameter.DataValueField = "Value";
                //ddlParameter.DataBind();
            }
            else
            {
                allSubInputModules = DBAccess.GetAllSubInputModules(ipModule);
                allSubInputModules.Insert(0, "");
                ddlSubInputModule.DataSource = allSubInputModules;
                ddlSubInputModule.DataBind();

                string sipModule = ddlSubInputModule.SelectedValue.ToString();
                //allParameters = DBAccess.GetAllParameters(ipModule, sipModule);
                //allParameters.Insert(0, "");
                //ddlParameter.DataSource = allParameters;
                //ddlParameter.DataBind();
                ddlParameter.DataSource = DBAccess.getParameterDetails(ddlInputModule.SelectedItem.Text, ddlSubInputModule.SelectedValue);
                ddlParameter.DataTextField = "Parameter";
                ddlParameter.DataValueField = "ParameterID";
                ddlParameter.DataBind();
                ddlParameter.Items.Insert(0, new ListItem("", ""));
            }
            if (btnNew.Enabled)
                ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "document.getElementById('divInput').style.display = 'none';", true);
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "showDiv", "document.getElementById('divInput').style.display = 'block';", true);

        }

        protected void ddlSubInputModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ipModule = ddlInputModule.SelectedValue.ToString();
            string sipModule = ddlSubInputModule.SelectedValue.ToString();
            //allParameters = DBAccess.GetAllParameters(ipModule, sipModule);
            //allParameters.Insert(0, "");
            //ddlParameter.DataSource = allParameters;
            //ddlParameter.DataBind();
            ddlParameter.DataSource = DBAccess.getParameterDetails(ddlInputModule.SelectedItem.Text, ddlSubInputModule.SelectedValue);
            ddlParameter.DataTextField = "Parameter";
            ddlParameter.DataValueField = "ParameterID";
            ddlParameter.DataBind();
            ddlParameter.Items.Insert(0, new ListItem("", ""));
            //BindGridView();
            if (btnNew.Enabled)
                ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "document.getElementById('divInput').style.display = 'none';", true);
        }

        protected void ddlParameter_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (btnNew.Enabled)
                ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "document.getElementById('divInput').style.display = 'none';", true);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string ipModule = string.Empty;
            string sipModule = string.Empty;
            string param = string.Empty;
            string paramid = string.Empty;
            if (ddlIpModule.Visible)
                ipModule = ddlIpModule.SelectedValue;
            else
                ipModule = lblIpModule.Text;

            if (ddlSipModule.Visible)
                sipModule = ddlSipModule.SelectedValue;
            else
                sipModule = lblSipModule.Text;
            try
            {
                if (ddlParam.Visible)
                {
                    param = ddlParam.SelectedItem.Text;
                    paramid = ddlParam.SelectedValue.Split(';')[0];
                }
                else
                {
                    param = lblParam.Text;
                    paramid = hiddenlblParamId.Value;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('For this Sub-Input Module, there is no Parameter.');", true);
                return;
            }
            string lstValue = ftxtListValue.Text;
            if (DBAccess.InsertParameterListValue(ipModule, sipModule, param, lstValue, paramid, Session["EmpName"].ToString()))
            {
                // ScriptManager.RegisterStartupScript(this, GetType(), "RecordsModaladded", "openSuccessModal('Data Saved Successfully.');", true);
                ddlIpModule.ClearSelection();
                ddlIpModule_SelectedIndexChanged(null, EventArgs.Empty);
                ftxtListValue.Text = string.Empty;
                btnNew.Enabled = true;
                ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "document.getElementById('divInput').style.display = 'none';", true);
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('Record saved successfully!')</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('List Value already exists,Can not Add!');", true);
            }
            BindGridView();
        }

        protected void lbtnupdate_Click(object sender, EventArgs e)
        {

        }

        protected void gvParameterList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvParameterList.EditIndex = e.NewEditIndex;
            BindGridView();
            ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "document.getElementById('divInput').style.display = 'none';", true);
        }

        protected void gvParameterList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            ////////
            int id = Convert.ToInt32((gvParameterList.Rows[e.RowIndex].FindControl("hdfIDEdit") as HiddenField).Value);
            string listValue = (gvParameterList.Rows[e.RowIndex].FindControl("txtListValue") as TextBox).Text;
            bool success = DBAccess.UpdateParameterListValue(id, listValue, Session["EmpName"].ToString());
            if (success)
            {
                gvParameterList.EditIndex = -1;
                //  ScriptManager.RegisterStartupScript(this, GetType(), "RecordsModalUpdated", "openSuccessModal('Data Updated Successfully.');", true);
                BindGridView();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Failed to Upadate, Please Try Again.');", true);
            }
            BindGridView();
            ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "document.getElementById('divInput').style.display = 'none';", true);
        }

        protected void ddlIpModule_SelectedIndexChanged(object sender, EventArgs e)
        {

            string ipModule = ddlIpModule.SelectedValue.ToString();
            allSubInputModules = DBAccess.GetAllSubInputModules_MasterData(ipModule);
            ddlSipModule.DataSource = allSubInputModules;
            ddlSipModule.DataBind();

            string sipModule = ddlSipModule.SelectedValue.ToString();
            //allParameters = DBAccess.GetAllParameters_MasterData(ipModule, sipModule);
            //allParameters.Insert(0, "All");
            ddlParam.DataSource = DBAccess.GetAllParameters_MasterData(ipModule, sipModule);
            ddlParam.DataTextField = "Parameter";
            ddlParam.DataValueField = "ParameterID";
            ddlParam.DataBind();
            addorremoceDecimalclass(ddlParam.SelectedValue.Split(';')[1]);
            if (ddlSipModule.Items.Count == 0)
            {
                lblSipModule.Visible = true;
                ddlSipModule.Visible = false;
            }
            else
            {
                lblSipModule.Visible = false;
                ddlSipModule.Visible = true;
            }
        }

        protected void ddlSipModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            string inputmodule;
            if (ddlIpModule.Visible)
            {
                inputmodule = ddlIpModule.SelectedValue.ToString();
            }
            else
            {
                inputmodule = lblIpModule.Text;
            }
            string sipModule = ddlSipModule.SelectedValue.ToString();
            // allParameters = DBAccess.GetAllParameters_MasterData(ddlIpModule.SelectedValue.ToString(), sipModule);
            ddlParam.DataSource = DBAccess.GetAllParameters_MasterData(inputmodule, sipModule);
            ddlParam.DataTextField = "Parameter";
            ddlParam.DataValueField = "ParameterID";
            ddlParam.DataBind();
            addorremoceDecimalclass(ddlParam.SelectedValue.Split(';')[1]);
            //string ipModule = string.Empty;
            //if (ddlIpModule.Visible)
            //{
            //    ipModule = ddlIpModule.SelectedValue.ToString();
            //}
            //else
            //{
            //    ipModule = lblIpModule.Text;
            //}
            //string sipModule = string.Empty;
            //if (ddlSipModule.Visible)
            //{
            //    sipModule = ddlSipModule.SelectedValue.ToString();
            //    ftxtListValue.Text = string.Empty;
            //}
            //else
            //{
            //    sipModule = lblSipModule.Text;
            //}
            //allParameters = DBAccess.GetAllParameters(ipModule, sipModule);
            //allParameters.Insert(0, "");
            //ddlParam.DataSource = allParameters;
            //ddlParam.DataBind();
        }
        protected void gvParameterList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvParameterList.EditIndex = -1;
            BindGridView();
            if (btnNew.Enabled)
                ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "document.getElementById('divInput').style.display = 'none';", true);
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "showDiv", "document.getElementById('divInput').style.display = 'block';", true);
        }
        protected void gvParameterList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvParameterList.PageIndex = e.NewPageIndex;
            BindGridView();
            if (btnNew.Enabled)
                ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "document.getElementById('divInput').style.display = 'none';", true);
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "showDiv", "document.getElementById('divInput').style.display = 'block';", true);

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (ddlIpModule.Visible)
            {
                ddlIpModule.ClearSelection();
                ddlIpModule_SelectedIndexChanged(null, EventArgs.Empty);
            }
            ftxtListValue.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "document.getElementById('divInput').style.display = 'none';", true);
            btnNew.Enabled = true;
        }
        protected void btnNew_Click(object sender, EventArgs e)
        {
            btnNew.Enabled = false;
            if (ddlInputModule.SelectedValue.Equals("ALL"))            {                lblIpModule.Visible = false;                lblSipModule.Visible = false;                lblParam.Visible = false;                ddlIpModule.Visible = true;                ddlSipModule.Visible = true;                ddlParam.Visible = true;
                if (ddlSipModule.Items.Count == 0)
                {
                    lblSipModule.Visible = true;
                    ddlSipModule.Visible = false;
                }
                else
                {
                    lblSipModule.Visible = false;
                    ddlSipModule.Visible = true;
                }            }            else            {                if (ddlSubInputModule.SelectedValue.Equals("") && ddlParameter.SelectedValue.Equals(""))                {                    lblSipModule.Visible = false;                    lblParam.Visible = false;                    ddlSipModule.Visible = true;                    ddlParam.Visible = true;                    ddlIpModule.Visible = false;                    lblIpModule.Visible = true;                    lblIpModule.Text = ddlInputModule.SelectedValue;                    allSubInputModules = DBAccess.GetAllSubInputModules_MasterData(lblIpModule.Text);                    ddlSipModule.DataSource = allSubInputModules;                    ddlSipModule.DataBind();                    string sipModule = ddlSipModule.SelectedValue.ToString();                    ddlParam.DataSource = DBAccess.GetAllParameters_MasterData(lblIpModule.Text, ddlSipModule.SelectedValue);                    ddlParam.DataTextField = "Parameter";                    ddlParam.DataValueField = "ParameterID";                    ddlParam.DataBind();
                    addorremoceDecimalclass(ddlParam.SelectedValue.Split(';')[1]);
                    if (ddlSipModule.Items.Count == 0)
                    {
                        lblSipModule.Visible = true;
                        ddlSipModule.Visible = false;
                    }
                    else
                    {
                        lblSipModule.Visible = false;
                        ddlSipModule.Visible = true;
                    }                }                else if (ddlParameter.SelectedValue.Equals(""))                {                    ddlIpModule.Visible = false;                    lblIpModule.Visible = true;                    lblIpModule.Text = ddlInputModule.SelectedValue;                    ddlSipModule.Visible = false;                    lblSipModule.Visible = true;                    lblSipModule.Text = ddlSubInputModule.SelectedValue;                    ddlParam.Visible = true;                    lblParam.Visible = false;                    ddlParam.DataSource = DBAccess.GetAllParameters_MasterData(lblIpModule.Text, lblSipModule.Text);                    ddlParam.DataTextField = "Parameter";                    ddlParam.DataValueField = "ParameterID";                    ddlParam.DataBind();
                    addorremoceDecimalclass(ddlParam.SelectedValue.Split(';')[1]);
                }                else                {                    ddlIpModule.Visible = false;                    lblIpModule.Visible = true;                    lblIpModule.Text = ddlInputModule.SelectedValue;                    ddlSipModule.Visible = false;                    lblSipModule.Visible = true;                    lblSipModule.Text = ddlSubInputModule.SelectedValue;                    ddlParam.Visible = false;                    lblParam.Visible = true;                    lblParam.Text = ddlParameter.SelectedItem.Text;                    hiddenlblParamId.Value = ddlParameter.SelectedValue;
                    
                    addorremoceDecimalclass(DBAccess.getDatatype(hiddenlblParamId.Value));                }
               
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "showDiv", "document.getElementById('divInput').style.display = 'block';", true);
        }

        private void addorremoceDecimalclass(string datatype)
        {
            if (datatype == "Integer")
            {
                ftxtListValue.CssClass += " allowDecimal ";
            }
            else
            {
                ftxtListValue.CssClass = ftxtListValue.CssClass.Replace("allowDecimal", "");
            }
        }


        protected void btnView_Click(object sender, EventArgs e)
        {
            if (ddlInputModule.SelectedValue.Equals("ALL"))
            {
                lblIpModule.Visible = false;
                lblSipModule.Visible = false;
                lblParam.Visible = false;

                ddlIpModule.Visible = true;
                ddlSipModule.Visible = true;
                ddlParam.Visible = true;
                if (ddlSipModule.Items.Count == 0)
                {
                    lblSipModule.Visible = true;
                    ddlSipModule.Visible = false;
                }
                else
                {
                    lblSipModule.Visible = false;
                    ddlSipModule.Visible = true;
                }
            }
            else
            {
                if (ddlSubInputModule.SelectedValue.Equals("") && ddlParameter.SelectedValue.Equals(""))
                {
                    lblSipModule.Visible = false;
                    lblParam.Visible = false;

                    ddlSipModule.Visible = true;
                    ddlParam.Visible = true;

                    ddlIpModule.Visible = false;
                    lblIpModule.Visible = true;
                    lblIpModule.Text = ddlInputModule.SelectedValue;

                    allSubInputModules = DBAccess.GetAllSubInputModules_MasterData(lblIpModule.Text);
                    ddlSipModule.DataSource = allSubInputModules;
                    ddlSipModule.DataBind();
                    string sipModule = ddlSipModule.SelectedValue.ToString();
                    ddlParam.DataSource = DBAccess.GetAllParameters_MasterData(lblIpModule.Text, ddlSipModule.SelectedValue);
                    ddlParam.DataTextField = "Parameter";
                    ddlParam.DataValueField = "ParameterID";
                    ddlParam.DataBind();
                    addorremoceDecimalclass(ddlParam.SelectedValue.Split(';')[1]);
                    if (ddlSipModule.Items.Count == 0)
                    {
                        lblSipModule.Visible = true;
                        ddlSipModule.Visible = false;
                    }
                    else
                    {
                        lblSipModule.Visible = false;
                        ddlSipModule.Visible = true;
                    }
                }
                else if (ddlParameter.SelectedValue.Equals(""))
                {
                    ddlIpModule.Visible = false;
                    lblIpModule.Visible = true;
                    lblIpModule.Text = ddlInputModule.SelectedValue;

                    ddlSipModule.Visible = false;
                    lblSipModule.Visible = true;
                    lblSipModule.Text = ddlSubInputModule.SelectedValue;



                    ddlParam.Visible = true;
                    lblParam.Visible = false;

                    ddlParam.DataSource = DBAccess.GetAllParameters_MasterData(lblIpModule.Text, lblSipModule.Text);
                    ddlParam.DataTextField = "Parameter";
                    ddlParam.DataValueField = "ParameterID";
                    ddlParam.DataBind();
                    addorremoceDecimalclass(ddlParam.SelectedValue.Split(';')[1]);
                    //ddlParam.DataSource = DBAccess.GetAllParameters_MasterData(ddlIpModule.SelectedValue.ToString(), lblSipModule.Text);
                    //ddlParam.DataTextField = "Parameter";
                    //ddlParam.DataValueField = "ParameterID";
                    //ddlParam.DataBind();
                }
                else
                {
                    ddlIpModule.Visible = false;
                    lblIpModule.Visible = true;
                    lblIpModule.Text = ddlInputModule.SelectedValue;

                    ddlSipModule.Visible = false;
                    lblSipModule.Visible = true;
                    lblSipModule.Text = ddlSubInputModule.SelectedValue;

                    ddlParam.Visible = false;
                    lblParam.Visible = true;
                    lblParam.Text = ddlParameter.SelectedItem.Text;
                    hiddenlblParamId.Value = ddlParameter.SelectedValue;
                    addorremoceDecimalclass(DBAccess.getDatatype(hiddenlblParamId.Value));
                    // hiddenlblParamDatatype.Value= ddlParameter.SelectedValue.Split(';')[1];
                }
                //ddlIpModule_SelectedIndexChanged(null, EventArgs.Empty);
                //if (ddlSubInputModule.SelectedValue.Equals("") && allSubInputModules.Count > 1)
                //{
                //    ddlSipModule.Visible = true;
                //    lblSipModule.Visible = false;
                //    lblParam.Visible = false;
                //    ddlParam.Visible = true;
                //}
                //else
                //{
                //    ddlSipModule.Visible = false;
                //    lblSipModule.Visible = true;
                //    lblSipModule.Text = ddlSubInputModule.SelectedValue;

                //}
                //ddlSipModule_SelectedIndexChanged(null, EventArgs.Empty);
                //if (ddlParameter.SelectedValue.Equals(""))
                //{
                //    ddlParam.Visible = true;
                //    lblParam.Visible = false;
                //    ddlSipModule_SelectedIndexChanged(null, EventArgs.Empty);
                //}
                //else
                //{
                //    ddlParam.Visible = false;
                //    lblParam.Visible = true;
                //    lblParam.Text = ddlParameter.SelectedValue;
                //}
            }

            BindGridView();
            if (btnNew.Enabled)
                ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "document.getElementById('divInput').style.display = 'none';", true);
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "showDiv", "document.getElementById('divInput').style.display = 'block';", true);
        }

        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            /////
            int rowIndex = Convert.ToInt32(((sender as LinkButton).NamingContainer as GridViewRow).RowIndex);
            GridViewRow row = gvParameterList.Rows[rowIndex];
            int id = Convert.ToInt32((row.FindControl("hdfID") as HiddenField).Value);
             DeleteParameterID = (row.FindControl("hiddenParamID") as HiddenField).Value;
           // string parameter = (row.FindControl("lblParameter") as Label).Text;
             Deletevalue = (row.FindControl("lblListValue") as Label).Text;
            //string colName = (row.FindControl("hdfColName") as HiddenField).Value;
            ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openConfirmModal('Are you sure, you want to delete this record?');", true);
           
        }

        protected void lbnReload_Click(object sender, EventArgs e)
        {
            //BindFilterList();
            bindTopDDL();
            bindNewDDL();

            btnView_Click(null, EventArgs.Empty);

            ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "document.getElementById('divInput').style.display = 'none';", true);
            btnNew.Enabled = true;
        }

        protected void ddlParam_SelectedIndexChanged(object sender, EventArgs e)
        {
            ftxtListValue.Text = string.Empty;
            if (btnNew.Enabled)
                ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "document.getElementById('divInput').style.display = 'none';", true);
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showDiv", "document.getElementById('divInput').style.display = 'block';", true);
                string datatype = ddlParam.SelectedValue.Split(';')[1];
                addorremoceDecimalclass(datatype);
                //if(datatype== "Integer")
                //{
                //    ftxtListValue.CssClass += " allowDecimal ";
                //}
                //else
                //{
                //    ftxtListValue.CssClass = ftxtListValue.CssClass.Replace("allowDecimal", "");
                //}
            }
                
        }

        protected void deleteConfirmYes_ServerClick(object sender, EventArgs e)
        {
            if (DBAccess.DeleteParameterListValue(DeleteParameterID, Deletevalue))
            {
                //ScriptManager.RegisterStartupScript(this, GetType(), "RecordsDeleted", "openSuccessModal('Record Deleted Successfully.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsDeletedFailed", "openWarningModal('Cannot Delete Parameter Value, exists in the Transaction table');", true);
            }
            BindGridView();
            if (btnNew.Enabled)
                ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "document.getElementById('divInput').style.display = 'none';", true);
            else
                ScriptManager.RegisterStartupScript(this, GetType(), "showDiv", "document.getElementById('divInput').style.display = 'block';", true);
        }

        protected void gvParameterList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            
            DeleteParameterID=(gvParameterList.Rows[e.RowIndex].FindControl("hiddenParamID") as HiddenField).Value;
             Deletevalue = (gvParameterList.Rows[e.RowIndex].FindControl("txtListValue") as TextBox).Text;
            ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openConfirmModal('Are you sure, you want to delete this record?');", true);
        }

        protected void save_Click(object sender, EventArgs e)
        {
            bool success = false;
            for (int i=0;i<gvParameterList.Rows.Count;i++)
            {
                int id = Convert.ToInt32((gvParameterList.Rows[i].FindControl("hdfIDEdit") as HiddenField).Value);
                string listValue = (gvParameterList.Rows[i].FindControl("txtListValue") as TextBox).Text;
                success = DBAccess.UpdateParameterListValue(id, listValue, Session["EmpName"].ToString());
            }
            
            if (success)
            {
                gvParameterList.EditIndex = -1;
                //  ScriptManager.RegisterStartupScript(this, GetType(), "RecordsModalUpdated", "openSuccessModal('Data Updated Successfully.');", true);
                BindGridView();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('Records saved successfully!')</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Records insertion failed.');", true);
            }
            BindGridView();
            ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "document.getElementById('divInput').style.display = 'none';", true);
        }
    }
}