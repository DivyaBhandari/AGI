using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AGISoftware.DataBaseAccess;
using System.Data;
using AGISoftware.Model;
using System.IO;
using System.Data.SqlClient;

namespace AGISoftware
{
    public partial class ParameterMaster : System.Web.UI.Page
    {
        List<string> allInputModules = new List<string>();
        List<string> allSubInputModules = new List<string>();
        List<string> allParameters = new List<string>();
        DataTable dt = new DataTable();
        static string deleteparameterName, deleteparameterID, deleteInputModule, deletecalculateflag, deletemandatoryflag;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindFilterValue();
            }
            if (Session["EmpName"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }

        }
        public void BindFilterValue()
        {
            string subinputmodule;
            allInputModules = DBAccess.GetAllInputModules_ParameterMaster();
            //allInputModules.Insert(0, "ALL");
            ddlInputModule.DataSource = allInputModules;
            ddlInputModule.DataBind();

            allSubInputModules = DBAccess.GetAllSubInputModules_ParameterMaster(ddlInputModule.SelectedItem.ToString());
            //allInputModules.Insert(0, "ALL");
            ddlSubInputModule.DataSource = allSubInputModules;
            ddlSubInputModule.DataBind();

            if (ddlSubInputModule.SelectedItem != null)
            {
                subinputmodule = ddlSubInputModule.SelectedItem.ToString();
            }
            else
            {
                subinputmodule = "";
            }
            allParameters = DBAccess.GetAllParameters_ParameterMaster(ddlInputModule.SelectedItem.ToString(), subinputmodule);
            allParameters.Insert(0, "All");
            ddlParameter.DataSource = allParameters;
            ddlParameter.DataBind();
            BindParameterList();
        }
        protected void ddlInputModule_SelectedIndexChanged(object sender, EventArgs e)
        {

            string ipModule = ddlInputModule.SelectedValue.ToString();
            allSubInputModules = DBAccess.GetAllSubInputModules_ParameterMaster(ipModule);
            //allSubInputModules.Insert(0, "");
            ddlSubInputModule.DataSource = allSubInputModules;
            ddlSubInputModule.DataBind();

            string sipModule = ddlSubInputModule.SelectedValue.ToString();
            allParameters = DBAccess.GetAllParameters_ParameterMaster(ipModule, sipModule);
            // allParameters.Insert(0, "");
            allParameters.Insert(0, "All");
            ddlParameter.DataSource = allParameters;
            ddlParameter.DataBind();
           
        }

        protected void ddlSubInputModule_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sipModule = ddlSubInputModule.SelectedValue.ToString();
            allParameters = DBAccess.GetAllParameters_ParameterMaster(ddlInputModule.SelectedValue.ToString(), sipModule);
            // allParameters.Insert(0, "");
            allParameters.Insert(0, "All");
            ddlParameter.DataSource = allParameters;
            ddlParameter.DataBind();
        }

        public void BindParameterList()
        {
            Session["ParamMasterInputModule"] = ddlInputModule.SelectedValue == null ? "" : ddlInputModule.SelectedItem.ToString();
            List<ParameterDetails> listParameter = new List<ParameterDetails>();
            listParameter = DBAccess.GetParameters(ddlInputModule.SelectedValue == null ? "" : ddlInputModule.SelectedItem.ToString(), ddlSubInputModule.SelectedItem == null ? "" : ddlSubInputModule.SelectedItem.ToString(), ddlParameter.SelectedItem == null || ddlParameter.SelectedValue.Equals("All") ? "" : ddlParameter.SelectedItem.ToString());
            gvParameterList.DataSource = listParameter;
            gvParameterList.DataBind();

            List<ParameterDetails> listParameters = new List<ParameterDetails>();
            listParameters = DBAccess.GetParameters("", "", "");
            List<string> IndependentParamlist = listParameters.Where(l => l.EntryType == "Drop Down").Select(l => l.Parameter).ToList();
            string OldCustomName = "";
            for (int i = 0; i < gvParameterList.Rows.Count; i++)
            {

                if (Convert.ToInt32(listParameter[i].IsNumeric) == 1)

                {
                    (gvParameterList.Rows[i].FindControl("edttxtUSL") as TextBox).Visible = true;
                    (gvParameterList.Rows[i].FindControl("edtlblUSL") as Label).Visible = false;
                    (gvParameterList.Rows[i].FindControl("edttxtLSL") as TextBox).Visible = true;
                    (gvParameterList.Rows[i].FindControl("edtlblLSL") as Label).Visible = false;
                    (gvParameterList.Rows[i].FindControl("edtcbDependentParam") as CheckBox).Visible = true;
                    (gvParameterList.Rows[i].FindControl("edtcbDependentParam") as CheckBox).Attributes["onclick"] = " return false";

                    (gvParameterList.Rows[i].FindControl("edtlbIndependentParam") as ListBox).Visible = true;
                    ListBox ddcb = new ListBox();
                    ddcb = (gvParameterList.Rows[i].FindControl("edtlbIndependentParam") as ListBox);
                    ddcb.DataSource = IndependentParamlist;
                    ddcb.DataBind();
                    string[] independentparamlist = listParameter[i].IndependentParameter.Split(';');
                    if (independentparamlist.Length > 0)
                    {
                        foreach (System.Web.UI.WebControls.ListItem item in ddcb.Items)
                        {
                            item.Selected = false;
                        }
                        foreach (System.Web.UI.WebControls.ListItem item in ddcb.Items)
                        {
                            for (int j = 0; j < independentparamlist.Length; j++)
                            {
                                if (item.Text == independentparamlist[j])
                                {
                                    item.Selected = true;
                                }
                            }
                        }
                    }
                    if ((gvParameterList.Rows[i].FindControl("edtcbDependentParam") as CheckBox).Checked)
                    {
                        (gvParameterList.Rows[i].FindControl("gotoParameterRelatioshipPage") as LinkButton).Visible = true;
                    }
                    else
                    {
                        (gvParameterList.Rows[i].FindControl("gotoParameterRelatioshipPage") as LinkButton).Visible = false;
                    }

                }
                else
                {
                    (gvParameterList.Rows[i].FindControl("edttxtUSL") as TextBox).Visible = false;
                    (gvParameterList.Rows[i].FindControl("edtlblUSL") as Label).Visible = true;
                    (gvParameterList.Rows[i].FindControl("edttxtLSL") as TextBox).Visible = false;
                    (gvParameterList.Rows[i].FindControl("edtlblLSL") as Label).Visible = true;
                    (gvParameterList.Rows[i].FindControl("edtcbDependentParam") as CheckBox).Visible = false;
                    //(gvParameterList.Rows[i].FindControl("edtdcIndependentParam") as Saplin.Controls.DropDownCheckBoxes).Visible = false;
                    (gvParameterList.Rows[i].FindControl("edtlbIndependentParam") as ListBox).Visible = false;
                }

                DropDownList ddl = (gvParameterList.Rows[i].FindControl("edtddlMandatory") as DropDownList);
                if (listParameter[i].Mandatoryflag == "1")
                {
                    ddl.Text = "Mandatory";
                }
                else if (listParameter[i].Mandatoryflag == "2")
                {
                    ddl.Text = "Important";
                }
                else
                {
                    ddl.Text = "Normal";
                }

                if (ddlInputModule.SelectedItem.ToString().Trim() == "Quality Parameters")
                {
                   // (gvParameterList.Rows[i].FindControl("edttxtCustomname") as TextBox).Visible = true;
                    string customname=(gvParameterList.Rows[i].FindControl("edttxtCustomname") as TextBox).Text;
                    if(customname.Contains(";Lower Target") || customname.Contains(";Upper Target") || customname.Contains(";Lower Actual") || customname.Contains(";Upper Actual"))
                    {
                        customname = customname.Replace(";Lower Target","");
                        customname = customname.Replace(";Upper Target", "");
                        customname = customname.Replace(";Lower Actual", "");
                        customname = customname.Replace(";Upper Actual", "");
                    }
                     (gvParameterList.Rows[i].FindControl("edttxtCustomname") as TextBox).Text = customname;
                   
                    if (OldCustomName!= customname)
                    {
                        (gvParameterList.Rows[i].FindControl("edttxtCustomname") as TextBox).ReadOnly = false;
                    }
                    else
                    {
                        (gvParameterList.Rows[i].FindControl("edttxtCustomname") as TextBox).ReadOnly = true;
                    }
                    OldCustomName = customname;

                }
                if ((gvParameterList.Rows[i].FindControl("imgRecommendation") as Image).ImageUrl == "")
                {
                    (gvParameterList.Rows[i].FindControl("imgRecommendation") as Image).Visible = false;
                    (gvParameterList.Rows[i].FindControl("removeRecommandationImage") as LinkButton).Visible = false;
                    
                }
                else
                {
                    (gvParameterList.Rows[i].FindControl("imgRecommendation") as Image).Visible = true;
                    (gvParameterList.Rows[i].FindControl("removeRecommandationImage") as LinkButton).Visible = true;
                }
            }

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                gvParameterList.ShowFooter = true;
                BindParameterList();
                string inputModule = Session["ParamMasterInputModule"].ToString();
                (gvParameterList.FooterRow.FindControl("lblNewInputModule") as Label).Text = inputModule;
                (gvParameterList.FooterRow.FindControl("lblNewSubInput") as Label).Text = (gvParameterList.Rows[0].FindControl("edtlblSubInputModule") as Label).Text;
               
                if (inputModule.Trim() == "Quality Parameters")
                {
                    (gvParameterList.FooterRow.FindControl("lblNewQPEntryType") as Label).Visible = true;
                    (gvParameterList.FooterRow.FindControl("ddlNewEntryType") as DropDownList).Visible = false;
                    (gvParameterList.FooterRow.FindControl("lblNewQPDataType") as Label).Visible = true;
                    (gvParameterList.FooterRow.FindControl("ddlNewDataType") as DropDownList).Visible = false;
                    //(gvParameterList.FooterRow.FindControl("txtnewCustomname") as TextBox).Visible = false;
                    //ScriptManager.RegisterStartupScript(this, GetType(), "hideDivas", "setScrollPosition();", true);
                }
                else
                {
                   
                    (gvParameterList.FooterRow.FindControl("lblNewQPEntryType") as Label).Visible = false;
                    (gvParameterList.FooterRow.FindControl("ddlNewEntryType") as DropDownList).Visible = true;
                    (gvParameterList.FooterRow.FindControl("lblNewQPDataType") as Label).Visible = false;
                    (gvParameterList.FooterRow.FindControl("ddlNewDataType") as DropDownList).Visible = true;
                    //(gvParameterList.FooterRow.FindControl("txtnewCustomname") as TextBox).Visible = true;
                    // ddlNewEntryType_SelectedIndexChanged(sender, e);
                    ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "ddlEntrytypechange();", true);
                    List<ParameterDetails> listParameters = new List<ParameterDetails>();
                    listParameters = DBAccess.GetParameters("", "", "");
                    List<string> IndependentParamlist = listParameters.Where(l => l.EntryType == "Drop Down").Select(l => l.Parameter).ToList();
                    ListBox ddcb = (gvParameterList.FooterRow.FindControl("newlbIndependentParam") as ListBox);
                    ddcb.DataSource = IndependentParamlist;
                    ddcb.DataBind();
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "hideDivas", "setScrollPosition();", true);                // ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "setScrollPosition();", true);
                btnCancel.Visible = true;
                btnNew.Visible = false;
                TextBox txt = (gvParameterList.FooterRow.FindControl("txtNewParameter") as TextBox);
                txt.Focus();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Visible = false;
            btnNew.Visible = true;
            gvParameterList.ShowFooter = false;
            BindParameterList();

        }
     
        protected void ddlNewEntryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = (gvParameterList.FooterRow.FindControl("ddlNewEntryType") as DropDownList);
            List<string> listDatatype = new List<string>();
            if (ddl.SelectedItem.ToString() == "TextBox")
            {
                listDatatype.Add("Alpha Numeric");
                listDatatype.Add("Date");
                listDatatype.Add("Decimal");
                listDatatype.Add("Integer");
                listDatatype.Add("Varchar");
            }
            else if (ddl.SelectedItem.ToString() == "Drop Down")
            {
                listDatatype.Add("Integer");
                listDatatype.Add("Varchar");
            }
            else if (ddl.SelectedItem.ToString() == "CheckBox")
            {
                listDatatype.Add("");
            }
            DropDownList ddl1 = (gvParameterList.FooterRow.FindControl("ddlNewDataType") as DropDownList);
            ddl1.DataSource = listDatatype;
            ddl1.DataBind();
            ddlNewDataType_SelectedIndexChanged(sender, e);
        }

        protected void ddlNewDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl1 = (gvParameterList.FooterRow.FindControl("ddlNewDataType") as DropDownList);
            if (ddl1.SelectedItem.ToString() == "Decimal" || ddl1.SelectedItem.ToString() == "Integer")
            {
                (gvParameterList.FooterRow.FindControl("txtNewLSL") as TextBox).ReadOnly = false;
                (gvParameterList.FooterRow.FindControl("txtNewUSL") as TextBox).ReadOnly = false;
                if ((gvParameterList.FooterRow.FindControl("txtNewLSL") as TextBox).Text == "NA")
                {
                    (gvParameterList.FooterRow.FindControl("txtNewLSL") as TextBox).Text = "";
                }
                if ((gvParameterList.FooterRow.FindControl("txtNewUSL") as TextBox).Text == "NA")
                {
                    (gvParameterList.FooterRow.FindControl("txtNewUSL") as TextBox).Text = "";
                }
                (gvParameterList.FooterRow.FindControl("chkNewDependentParam") as CheckBox).Visible = true;
                (gvParameterList.FooterRow.FindControl("newlbIndependentParam") as ListBox).Visible = true;
            }
            else
            {
                (gvParameterList.FooterRow.FindControl("txtNewLSL") as TextBox).Text = "NA";
                (gvParameterList.FooterRow.FindControl("txtNewLSL") as TextBox).ReadOnly = true;
                (gvParameterList.FooterRow.FindControl("txtNewUSL") as TextBox).Text = "NA";
                (gvParameterList.FooterRow.FindControl("txtNewUSL") as TextBox).ReadOnly = true;
                (gvParameterList.FooterRow.FindControl("chkNewDependentParam") as CheckBox).Visible = false;
                (gvParameterList.FooterRow.FindControl("newlbIndependentParam") as ListBox).Visible = false;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "setScrollPosition();", true);
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            if (gvParameterList.ShowFooter)
            {
                btnCancel.Visible = false;
                btnNew.Visible = true;
                gvParameterList.ShowFooter = false;
            }
            BindParameterList();
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static bool removeImage(string paramname, string paramid)
        {
            bool success = DBAccess.removeLimitImage(paramname, paramid);
            return success;
        }
       
        protected void Save_Click(object sender, EventArgs e)
        {
            string parameterName = "", customName = "";
            if (gvParameterList.ShowFooter)
            {
                ParameterDetails parameter = new ParameterDetails();
                //parameter.Id = (gvParameterList.FooterRow.FindControl("lblNewInputModule") as Label).Text;
                parameter.InputModule = (gvParameterList.FooterRow.FindControl("lblNewInputModule") as Label).Text;
                if (gvParameterList.FooterRow.FindControl("lblNewSubInput") != null)
                {
                    parameter.SubInputModule = (gvParameterList.FooterRow.FindControl("lblNewSubInput") as Label).Text;
                }
                else
                {
                    parameter.SubInputModule = "";
                }

                if (parameter.InputModule.Trim() == "Quality Parameters")
                {
                    string paramLHS = (gvParameterList.FooterRow.FindControl("txtNewParameter") as TextBox).Text;
                    parameter.Parameter = paramLHS;
                    parameter.Customname = (gvParameterList.FooterRow.FindControl("txtnewCustomname") as TextBox).Text;
                    parameter.EntryType = (gvParameterList.FooterRow.FindControl("lblNewQPEntryType") as Label).Text;
                    parameter.DataType = (gvParameterList.FooterRow.FindControl("lblNewQPDataType") as Label).Text;

                }
                else
                {
                    parameter.Parameter = (gvParameterList.FooterRow.FindControl("txtNewParameter") as TextBox).Text;
                    parameter.EntryType = (gvParameterList.FooterRow.FindControl("ddlNewEntryType") as DropDownList).Text;
                    //parameter.DataType = (gvParameterList.FooterRow.FindControl("ddlNewDataType") as DropDownList).Text;
                    parameter.DataType = (gvParameterList.FooterRow.FindControl("hdNewDatatype") as HiddenField).Value;
                    parameter.Customname = (gvParameterList.FooterRow.FindControl("txtnewCustomname") as TextBox).Text;
                }
                if (parameter.Parameter == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsModalUpdated", "openWarningModal('Parameter Name required.');", true);
                    return;
                }
                parameter.Reccomandation = (gvParameterList.FooterRow.FindControl("txtNewRecommendation") as TextBox).Text;
                if ((gvParameterList.FooterRow.FindControl("newRecommendationImage") as FileUpload).HasFile)
                {
                    FileUpload fileUpload1 = gvParameterList.FooterRow.FindControl("newRecommendationImage") as FileUpload;

                    if (fileUpload1.HasFile)
                    {
                        using (BinaryReader br = new BinaryReader(fileUpload1.PostedFile.InputStream))
                        {
                            parameter.ImageLimit = br.ReadBytes(fileUpload1.PostedFile.ContentLength);
                        }
                    }
                }
                parameter.LSL = (gvParameterList.FooterRow.FindControl("txtNewLSL") as TextBox).Text;
                parameter.USL = (gvParameterList.FooterRow.FindControl("txtNewUSL") as TextBox).Text;
                parameter.Enableflag = (gvParameterList.FooterRow.FindControl("chkNewEnable") as CheckBox).Checked;
                //parameter.SortOrder = (gvParameterList.FooterRow.FindControl("txtNewSortOrder") as TextBox).Text;
                parameter.DefaultParam = (gvParameterList.FooterRow.FindControl("chkNewDefaultParam") as CheckBox).Checked;

                if ((gvParameterList.FooterRow.FindControl("ddlNewMandatory") as DropDownList).SelectedItem.ToString() == "Normal")
                {
                    parameter.Mandatoryflag = "0";

                }
                else if ((gvParameterList.FooterRow.FindControl("ddlNewMandatory") as DropDownList).SelectedItem.ToString() == "Mandatory")
                {
                    parameter.Mandatoryflag = "1";

                }
                else if ((gvParameterList.FooterRow.FindControl("ddlNewMandatory") as DropDownList).SelectedItem.ToString() == "Important")
                {
                    parameter.Mandatoryflag = "2";

                }
                parameter.AdminName = Session["EmpName"].ToString();

                parameter.Dependencyflag = (gvParameterList.FooterRow.FindControl("chkNewDependentParam") as CheckBox).Checked;
                if ((gvParameterList.FooterRow.FindControl("chkNewDependentParam") as CheckBox).Checked)
                {
                    ListBox ddcb = (gvParameterList.FooterRow.FindControl("newlbIndependentParam") as ListBox);
                    string independentparamlist = "";
                    foreach (System.Web.UI.WebControls.ListItem item in ddcb.Items)
                    {
                        if (item.Selected)
                        {
                            if (independentparamlist == "")
                            {
                                independentparamlist = item.Text;
                            }
                            else
                            {
                                independentparamlist = independentparamlist + ";" + item.Text;
                            }
                        }
                    }
                    parameter.IndependentParameter = independentparamlist;
                }
                else
                {
                    parameter.IndependentParameter = "";
                }
                bool success;
                if (parameter.InputModule.Trim() == "Quality Parameters")
                {
                    string param = parameter.Parameter;
                    string custoname = parameter.Customname;
                    if (custoname == "")
                    {
                        custoname = param;
                    }
                    parameter.Parameter = param + ";Lower Target";
                    parameter.Customname = custoname + ";Lower Target";
                    success = DBAccess.SaveNewParameters(parameter);
                    parameter.Parameter = param + ";Upper Target";
                    parameter.Customname = custoname + ";Upper Target";
                    success = DBAccess.SaveNewParameters(parameter);
                    parameter.Parameter = param + ";Lower Actual";
                    parameter.Customname = custoname + ";Lower Actual";
                    success = DBAccess.SaveNewParameters(parameter);
                    parameter.Parameter = param + ";Upper Actual";
                    parameter.Customname = custoname + ";Upper Actual";
                    success = DBAccess.SaveNewParameters(parameter);
                }
                else
                {
                    success = DBAccess.SaveNewParameters(parameter);
                }


                if (success)
                {
                    gvParameterList.EditIndex = -1;
                    //  ScriptManager.RegisterStartupScript(this, GetType(), "RecordsModalUpdated", "openSuccessModal('Data Updated Successfully.');", true);
                    gvParameterList.ShowFooter = false;
                    BindParameterList();
                    btnCancel.Visible = false;
                    btnNew.Visible = true;
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('Record saved successfully!')</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);

                }
            }
            else
            {


                bool success = false;
                
                for (int i = 0; i < gvParameterList.Rows.Count; i++)
                {
                    byte[] imageRecommendation = null;
                    ParameterDetails parameter = new ParameterDetails();

                    parameter.Id = (gvParameterList.Rows[i].FindControl("edtlblID") as Label).Text;
                    parameter.InputModule = (gvParameterList.Rows[i].FindControl("edtlblInputModule") as Label).Text;
                    parameter.SubInputModule = (gvParameterList.Rows[i].FindControl("edtlblSubInputModule") as Label).Text;
                    parameter.Parameter = (gvParameterList.Rows[i].FindControl("edtlblParameter") as Label).Text;
                    parameter.Reccomandation = (gvParameterList.Rows[i].FindControl("edtlblRecommendation") as TextBox).Text;
                    parameter.LSL = (gvParameterList.Rows[i].FindControl("edttxtLSL") as TextBox).Text;
                    parameter.USL = (gvParameterList.Rows[i].FindControl("edttxtUSL") as TextBox).Text;
                    parameter.Enableflag = (gvParameterList.Rows[i].FindControl("edtcbEnable") as CheckBox).Checked;
                    // parameter.SortOrder = (gvParameterList.Rows[i].FindControl("edttxtSortOrder") as TextBox).Text;
                    parameter.DefaultParam = (gvParameterList.Rows[i].FindControl("edtcbDefaultParam") as CheckBox).Checked;
                    if ((gvParameterList.Rows[i].FindControl("edtddlMandatory") as DropDownList).SelectedItem.ToString() == "Normal")
                    {
                        parameter.Mandatoryflag = "0";

                    }
                    else if ((gvParameterList.Rows[i].FindControl("edtddlMandatory") as DropDownList).SelectedItem.ToString() == "Mandatory")
                    {
                        parameter.Mandatoryflag = "1";

                    }
                    else if ((gvParameterList.Rows[i].FindControl("edtddlMandatory") as DropDownList).SelectedItem.ToString() == "Important")
                    {
                        parameter.Mandatoryflag = "2";

                    }

                    parameter.AdminName = Session["EmpName"].ToString();

                    parameter.Dependencyflag = (gvParameterList.Rows[i].FindControl("edtcbDependentParam") as CheckBox).Checked;
                    if ((gvParameterList.Rows[i].FindControl("edtcbDependentParam") as CheckBox).Checked)
                    {
                        ListBox ddcb = (gvParameterList.Rows[i].FindControl("edtlbIndependentParam") as ListBox);
                        string independentparamlist = "";
                        foreach (System.Web.UI.WebControls.ListItem item in ddcb.Items)
                        {
                            if (item.Selected)
                            {
                                if (independentparamlist == "")
                                {
                                    independentparamlist = item.Text;
                                }
                                else
                                {
                                    independentparamlist = independentparamlist + ";" + item.Text;
                                }
                            }
                        }
                        parameter.IndependentParameter = independentparamlist;
                    }
                    else
                    {
                        parameter.IndependentParameter = "";
                    }

                    if ((gvParameterList.Rows[i].FindControl("RecommendationImage") as FileUpload).HasFile)
                    {
                        FileUpload fileUpload1 = gvParameterList.Rows[i].FindControl("RecommendationImage") as FileUpload;

                        if (fileUpload1.HasFile)
                        {
                            using (BinaryReader br = new BinaryReader(fileUpload1.PostedFile.InputStream))
                            {
                                parameter.ImageLimit = br.ReadBytes(fileUpload1.PostedFile.ContentLength);
                            }

                        }

                    }
                    else
                    {
                        SqlConnection conn = null;
                        try
                        {


                            conn = ConnectionManager.GetConnection();
                            SqlCommand cmd = null;
                            SqlDataReader rdr = null;
                            cmd = new SqlCommand("select LimitImage from InputModuleParameterDetails where InputModule=@inputmodule and ParameterID=@id and SubInputModule=@suninputmodule", conn);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@inputmodule", parameter.InputModule);
                            cmd.Parameters.AddWithValue("@id", parameter.Id);
                            cmd.Parameters.AddWithValue("@suninputmodule", parameter.SubInputModule);
                            rdr = cmd.ExecuteReader();
                            if (rdr.HasRows)
                            {
                                while (rdr.Read())
                                {
                                    if (rdr["LimitImage"] != DBNull.Value)
                                    {
                                        parameter.ImageLimit = (byte[])rdr["LimitImage"];
                                    }

                                }
                            }
                        }
                        catch (Exception ex)
                        {


                        }
                        finally
                        {
                            if (conn != null) conn.Close();
                        }
                    }

                   
                    if (parameter.InputModule.Trim() == "Quality Parameters")
                    {
                        string param = (parameter.Parameter).Substring(0, parameter.Parameter.LastIndexOf(';'));
                        if (parameterName != param)
                        {
                            customName = (gvParameterList.Rows[i].FindControl("edttxtCustomname") as TextBox).Text;
                            if (customName == "")
                            {
                                customName = param;
                            }
                            parameterName = param;
                        }
                        string rhs= (parameter.Parameter).Substring(parameter.Parameter.LastIndexOf(';'));
                        parameter.Customname = customName+rhs;
                    }
                    else
                    {
                        parameter.Customname = (gvParameterList.Rows[i].FindControl("edttxtCustomname") as TextBox).Text;
                    }


                    success = DBAccess.UpdateParameters(parameter, "");

                }
               
                if (success)
                {
                    gvParameterList.EditIndex = -1;
                    //  ScriptManager.RegisterStartupScript(this, GetType(), "RecordsModalUpdated", "openSuccessModal('Data Updated Successfully.');", true);
                    BindParameterList();
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('Records saved successfully!')</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Records insertion failed.');", true);

                }
            }

        }

        protected void removeParameterYes_ServerClick(object sender, EventArgs e)
        {
            if ((deletecalculateflag == "True") || (deletemandatoryflag == "1" || deletemandatoryflag == "2"))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('Mandatory / Important Parameters and Parameters used in calculation cannot be deleted');", true);
                return;
            }
            
            string success = DBAccess.DeleteParameter(deleteparameterName, deleteparameterID, deleteInputModule);
            if (success == "ParameterIsUsed")
            {

                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('Cannot delete the Parameter, exists in transaction table');", true);

            }
            else if (success == "Deleted")
            {
                BindParameterList();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('Record deleted successfully!')</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record deletion failed.');", true);
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void setParameterName(string Param)
        {

            HttpContext.Current.Session["ParameterNameandID"] = Param;
        }

        protected void gvParameterList_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            deleteparameterName=(gvParameterList.Rows[e.RowIndex].FindControl("edtlblParameter") as Label).Text;
            deleteparameterID= (gvParameterList.Rows[e.RowIndex].FindControl("hdParamID") as HiddenField).Value;
            deleteInputModule = (gvParameterList.Rows[e.RowIndex].FindControl("edtlblInputModule") as Label).Text;
            deletemandatoryflag = (gvParameterList.Rows[e.RowIndex].FindControl("edthdMandatory") as HiddenField).Value;
            deletecalculateflag = (gvParameterList.Rows[e.RowIndex].FindControl("edthdDeletable") as HiddenField).Value;
            ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openParameterRemoveConfirmModal();", true);
            
        }
    }
}