using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AGISoftware.DataBaseAccess;
using System.Text;
using AGISoftware.Model;
using System.Data;
using System.IO;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using OfficeOpenXml.Drawing.Chart;


namespace AGISoftware
{
    public partial class ApplicationToolKit : System.Web.UI.Page
    {
        DataTable dtGlobal = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                if (Session["NormalBind"] == null)
                {
                    Session["NormalBind"] = true;
                    cbSdocdrillOption.Checked = (bool)Session["NormalBind"];
                }
                else
                {
                    cbSdocdrillOption.Checked = (bool)Session["NormalBind"];
                }
                DataTable dt = DBAccess.getSystemDocumentData();
                // bindSDocList(dt);
                if (cbSdocdrillOption.Checked)
                {
                    bindNormalSdoclist(dt);
                    gvNormalSDocbind.Visible = true;
                    gvDisplayData.Visible = false;
                    noOfRows.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    bindSDocList(dt);
                    gvNormalSDocbind.Visible = false;
                    gvDisplayData.Visible = true;
                }
                bindDDLParameter();
                bindDDLParameterValue();
                txtQuery.Text = "select * from SystemDocTransaction";
                // bindParameterMinMaxAvg("SystemDoc");
                bindDDLGraphParameter();
                Session["ATKSDocID"] = null;
                Session["ATKPagination"] = "systemdoc";
                bindListBox();
                bindQueryList();////
                bindChkMinMaxParameter();
                Session["StatisticsColumns"] = "";
                Session["StatisticsConditions"] = "";
                bindParameterMinMaxAvg("");
                Session["ExecuteConditions"] = "";
                Session["APKDisplayCondition"] = null;
                Session["InputModule"] = null;
                DataTable dt1 = new DataTable();
                dt1 = DBAccess.GetInputModuleDetails();
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if (dt1.Rows[i][0].ToString() == "1")
                    {
                        systemDocBtn.Text = dt1.Rows[i][2].ToString() == "" ? "General Information" : dt1.Rows[i][2].ToString();
                    }
                    if (dt1.Rows[i][0].ToString() == "2")
                    {
                        machineToolBtn.Text = dt1.Rows[i][2].ToString() == "" ? "Machine Tool" : dt1.Rows[i][2].ToString();
                    }
                    if (dt1.Rows[i][0].ToString() == "3")
                    {
                        wheelBtn.Text = dt1.Rows[i][2].ToString() == "" ? "Consumables" : dt1.Rows[i][2].ToString();
                    }
                    if (dt1.Rows[i][0].ToString() == "4")
                    {
                        workPieceBtn.Text = dt1.Rows[i][2].ToString() == "" ? "Workpiece" : dt1.Rows[i][2].ToString();
                    }
                    if (dt1.Rows[i][0].ToString() == "5")
                    {
                        opeParBtn.Text = dt1.Rows[i][2].ToString() == "" ? "Operational Parameters" : dt1.Rows[i][2].ToString();
                    }
                    if (dt1.Rows[i][0].ToString() == "6")
                    {
                        targetQlyBtn.Text = dt1.Rows[i][2].ToString() == "" ? "Quality Parameters" : dt1.Rows[i][2].ToString();
                    }
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "loading", "loadingFun()", true);
            }
            //ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "bindInputModuleInStatistics()", true);
            if (Session["EmpName"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
            if (Session["NormalBind"] == null)
            {
                Session["NormalBind"] = true;
                cbSdocdrillOption.Checked = (bool)Session["NormalBind"];
            }
            //if (Response.Cookies["un"].Expires < DateTime.Now)
            //{
            //    Response.Redirect("LoginPage.aspx");
            //}

        }

        private void bindNormalSdoclist(DataTable dt)
        {
            removeTableColumn(dt);
            gvNormalSDocbind.DataSource = dt;
            gvNormalSDocbind.DataBind();

            for (int i = 0; i < gvNormalSDocbind.Rows.Count; i++)
            {
                for (int j = 0; j < gvNormalSDocbind.Rows[0].Cells.Count; j++)
                {

                    if (gvNormalSDocbind.Rows[i].Cells[0].Text.Split('_')[0].Contains("000000"))
                    {
                        gvNormalSDocbind.Rows[i].Cells[j].BackColor = System.Drawing.Color.FromArgb(222, 251, 249);
                    }
                 
                }

            }
        }
        private void bindSDocList(DataTable dt)
        {
            if(dt!=null)
            {
                dtGlobal = dt.Copy();
                DataView dv = new DataView(dt);
                DataTable SDocName = dv.ToTable(true, "SDocName");
                gvDisplayData.DataSource = SDocName;
                gvDisplayData.DataBind();
                noOfRows.Text = dtGlobal.Rows.Count.ToString();

            }
          
        }
        protected void gvDisplayData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            GridView nestgridview = (GridView)e.Row.FindControl("gvnested");
            string Sdoc = e.Row.Cells[1].Text;
            //DataTable dt = DBAccess.getSystemDocumentData();
            DataTable dt2 = new DataTable();
            dt2 = dtGlobal.Clone();
            foreach (DataRow dr in dtGlobal.Rows)
            {
                string s = dr[0].ToString().Split('_')[0];
                if (dr[0].ToString().Split('_')[0].Contains(Sdoc))
                {
                    //dt2.Rows.Add(dr.ItemArray);
                    dt2.ImportRow(dr);
                }
            }

            removeTableColumn(dt2);
            nestgridview.DataSource = dt2;
            nestgridview.DataBind();
            if (Sdoc == "000000")
            {
                for (int i = 0; i < nestgridview.Rows.Count; i++)
                {
                    for (int j = 0; j < nestgridview.Rows[0].Cells.Count; j++)
                    {
                        nestgridview.Rows[i].Cells[j].BackColor = System.Drawing.Color.FromArgb(222, 251, 249);
                    }

                }
            }
        }
        private void bindChkMinMaxParameter()
        {
            List<CustomColumn> listdata = new List<CustomColumn>();
            listdata = DBAccess.getATKTabParameter("Statistics");
            var distInputModule = listdata.Select(x => x.InputModule).Distinct().ToList();
            List<StatisticParamList> paramList = new List<StatisticParamList>();
            for (int i = 0; i < distInputModule.Count; i++)
            {
                StatisticParamList statData = new StatisticParamList();
                statData.InpputModule = distInputModule[i];
                // List<CustomColumn> lsCustomCol = new List<CustomColumn>();
                for (int j = 0; j < listdata.Count; j++)
                {
                    //if (distInputModule[i] == listdata[j].InputModule)
                    //{
                    //    CustomColumn data = new CustomColumn();
                    //    data.InputModule = distInputModule[i];
                    //    data.CustomName = listdata[j].CustomName;
                    //    data.ColumnName = listdata[j].ColumnName;
                    //    data.DerivedFlag = listdata[j].DerivedFlag;
                    //    statData.Values.Add(data);
                    //    //lsCustomCol.Add(data);
                    //}
                    if (distInputModule[i] == listdata[j].InputModule)
                    {
                        CustomColumn data = new CustomColumn();
                        data.InputModule = distInputModule[i];
                        data.CustomName = listdata[j].CustomName;
                        data.ColumnName = listdata[j].ColumnName;
                        data.DerivedFlag = listdata[j].DerivedFlag;
                        data.ColumnName = listdata[j].ColumnName + "," + data.DerivedFlag;
                        statData.Values.Add(data);
                    }
                }
                //
                paramList.Add(statData);
            }
            lvMinMaxParameterList.DataSource = paramList;
            lvMinMaxParameterList.DataBind();

            ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "lvminmaxActivation();", true);
        }
        private void bindListBox()
        {
            ddlMultiDownID.DataSource = DBAccess.getCustomColumnName();
            ddlMultiDownID.DataTextField = "CustomName";
            ddlMultiDownID.DataValueField = "CustomName";
            ddlMultiDownID.DataBind();
        }
        private void bindQueryList()
        {
            ddlQueryList.DataSource = DBAccess.getQueryList();
            ddlQueryList.DataBind();
        }
        private void bindDDLParameter()
        {
            //ddlParameter.DataSource = DBAccess.getATKTabParameter("");
            //ddlParameter.DataTextField = "ColumnName";
            //ddlParameter.DataValueField = "ColumnName";
            //ddlParameter.DataBind();
            List<CustomColumn> parameterlist = null;            parameterlist = DBAccess.getATKTabParameter("");
           // parameterlist.Insert(0, "");            var builder = new System.Text.StringBuilder();            if (parameterlist.Count > 0)            {                for (int i = 0; i < parameterlist.Count; i++)                {                    if (i == 0)                    {                        //txtParameternew.Text = parameterlist[i].ColumnName.ToString();                    }                    builder.Append(String.Format("<option style='font-weight:unset' value='{0}'>", parameterlist[i].ColumnName.ToString()));                }            }            else            {                txtParameternew.Text = "";            }            ddlParameternew.InnerHtml = builder.ToString();
        }
        private void bindDDLParameterValue()
        {
            // ddlParamValue.DataSource = DBAccess.getATKTabParameterValue(ddlParameter.SelectedValue.ToString());
            ddlParamValue.DataSource = DBAccess.getATKTabParameterValue(txtParameternew.Text);
            ddlParamValue.DataTextField = "Value";
            ddlParamValue.DataValueField = "Value";
            ddlParamValue.DataBind();
        }
        private void bindDDLGraphParameter()
        {
            //ddlGraphParam.DataSource = DBAccess.getATKDDLGraphParameter();
            //ddlGraphParam.DataTextField = "Parameter";
            //ddlGraphParam.DataValueField = "Parameter";
            //ddlGraphParam.DataBind();
            //ddlGraphParam2.DataSource = DBAccess.getATKDDLGraphParameter();
            //ddlGraphParam2.DataTextField = "Parameter";
            //ddlGraphParam2.DataValueField = "Parameter";
            //ddlGraphParam2.DataBind();
            //ddlGraphParam2.SelectedIndex = 1;
            //ddlGraphParam3.DataSource = DBAccess.getATKDDLGraphParameter();
            //ddlGraphParam3.DataTextField = "Parameter";
            //ddlGraphParam3.DataValueField = "Parameter";
            //ddlGraphParam3.DataBind();
            //ddlGraphParam3.SelectedIndex = 2;
            //ddlGraphParam4.DataSource = DBAccess.getATKDDLGraphParameter();
            //ddlGraphParam4.DataTextField = "Parameter";
            //ddlGraphParam4.DataValueField = "Parameter";
            //ddlGraphParam4.DataBind();
            //ddlGraphParam4.SelectedIndex = 3;
            //ddlGraphParam5.DataSource = DBAccess.getATKDDLGraphParameter();
            //ddlGraphParam5.DataTextField = "Parameter";
            //ddlGraphParam5.DataValueField = "Parameter";
            //ddlGraphParam5.DataBind();
            //ddlGraphParam5.SelectedIndex = 4;
        }
        private void setDropdownGraphParamColor(List<ADKParameterMinMaxAvg> listData, DropDownList ddl)        {            foreach (ListItem ddlData in ddl.Items)            {                foreach (ADKParameterMinMaxAvg data in listData)                {                    if (ddlData.Text == data.Parameter)                    {                        if (data.DerivedFlag.Trim() == "1")                        {                            ddlData.Attributes.Add("style", "color:#6f1616");                        }                        else                        {                            ddlData.Attributes.Add("style", "color:black");                        }                    }                }            }        }
        private void bindParameterMinMaxAvg(string sort)
        {
            int success;
            List<ADKParameterMinMaxAvg> listminmax = DBAccess.getATKParameterMinMaxAvg(Session["StatisticsConditions"] != null ? Session["StatisticsConditions"].ToString() : "", Session["StatisticsColumns"] != null ? Session["StatisticsColumns"].ToString() : "", sort, out success);
            gvMinMaxAvg.DataSource = listminmax;
            gvMinMaxAvg.DataBind();
            bindMinMaxTabletoParametrlist();
            gvminmaxLarge.DataSource = listminmax;
            gvminmaxLarge.DataBind();
            string ddllist = setdropdownvaluesforgraph(listminmax);
            try
            {
                if (listminmax.Count == 0)
                {
                    txtGraphParams1.Text = "";
                    //ddlGraphParam2.Items.Clear();
                    txtGraphParams2.Text = "";
                    // ddlGraphParam3.Items.Clear();
                    txtGraphParams3.Text = "";
                    txtGraphParams4.Text = "";
                    txtGraphParams5.Text = "";
                }
                else
                if (listminmax.Count == 1)
                {

                    // ddlGraphParams1.InnerHtml= setdropdownvaluesforgraph(listminmax, 0, txtGraphParams1, ddlGraphParams1);
                    ddlGraphParams1.InnerHtml = ddllist;
                    txtGraphParams1.Text = listminmax[0].Parameter.ToString();
                    txtGraphParams2.Text = "";
                    txtGraphParams3.Text = "";
                    txtGraphParams4.Text = "";
                    txtGraphParams5.Text = "";
                }
                else if (listminmax.Count == 2)
                {
                    //ddlGraphParams1.InnerHtml = setdropdownvaluesforgraph(listminmax, 0, txtGraphParams1, ddlGraphParams1);
                    //ddlGraphParams2.InnerHtml= setdropdownvaluesforgraph(listminmax, 1, txtGraphParams2, ddlGraphParams2);
                    ddlGraphParams1.InnerHtml = ddllist;
                    txtGraphParams1.Text = listminmax[0].Parameter.ToString();
                    ddlGraphParams2.InnerHtml = ddllist;
                    txtGraphParams2.Text = listminmax[1].Parameter.ToString();

                    txtGraphParams3.Text = "";
                    txtGraphParams4.Text = "";
                    txtGraphParams5.Text = "";
                }
                else if (listminmax.Count == 3)
                {
                    // ddlGraphParams1.InnerHtml = setdropdownvaluesforgraph(listminmax, 0, txtGraphParams1, ddlGraphParams1);
                    // ddlGraphParams2.InnerHtml = setdropdownvaluesforgraph(listminmax, 1, txtGraphParams2, ddlGraphParams2);
                    //ddlGraphParams3.InnerHtml= setdropdownvaluesforgraph(listminmax, 2, txtGraphParams3, ddlGraphParams3);
                    ddlGraphParams1.InnerHtml = ddllist;
                    txtGraphParams1.Text = listminmax[0].Parameter.ToString();
                    ddlGraphParams2.InnerHtml = ddllist;
                    txtGraphParams2.Text = listminmax[1].Parameter.ToString();
                    ddlGraphParams3.InnerHtml = ddllist;
                    txtGraphParams3.Text = listminmax[2].Parameter.ToString();

                    txtGraphParams4.Text = "";
                    txtGraphParams5.Text = "";
                }
                else if (listminmax.Count == 4)
                {
                    //ddlGraphParams1.InnerHtml = setdropdownvaluesforgraph(listminmax, 0, txtGraphParams1, ddlGraphParams1);
                    //ddlGraphParams2.InnerHtml = setdropdownvaluesforgraph(listminmax, 1, txtGraphParams2, ddlGraphParams2);
                    //ddlGraphParams3.InnerHtml = setdropdownvaluesforgraph(listminmax, 2, txtGraphParams3, ddlGraphParams3);
                    ddlGraphParams1.InnerHtml = ddllist;
                    txtGraphParams1.Text = listminmax[0].Parameter.ToString();
                    ddlGraphParams2.InnerHtml = ddllist;
                    txtGraphParams2.Text = listminmax[1].Parameter.ToString();
                    ddlGraphParams3.InnerHtml = ddllist;
                    txtGraphParams3.Text = listminmax[2].Parameter.ToString();
                    ddlGraphParams4.InnerHtml = ddllist;
                    txtGraphParams4.Text = listminmax[3].Parameter.ToString();
                    txtGraphParams5.Text = "";
                }
                else if (listminmax.Count > 4)
                {
                    //ddlGraphParam.DataSource = listminmax;
                    //ddlGraphParam.DataTextField = "Parameter";
                    //ddlGraphParam.DataValueField = "Avg";
                    //ddlGraphParam.DataBind();
                    //setDropdownGraphParamColor(listminmax, ddlGraphParam);

                    //ddlGraphParam2.DataSource = listminmax;
                    //ddlGraphParam2.DataTextField = "Parameter";
                    //ddlGraphParam2.DataValueField = "Avg";
                    //ddlGraphParam2.DataBind();
                    //setDropdownGraphParamColor(listminmax, ddlGraphParam2);
                    //ddlGraphParam2.SelectedIndex = 1;
                    //var builder = new System.Text.StringBuilder();
                    //if (listminmax.Count > 0)
                    //{
                    //    for (int i = 0; i < listminmax.Count; i++)
                    //    {
                    //        if (i == 1)
                    //        {
                    //            txtGraphParams2.Text = listminmax[i].Parameter.ToString();
                    //        }
                    //        // string str = "<option value='" + listminmax[i].Avg + "'>" + listminmax[i].Parameter + "</option>";
                    //        builder.Append(String.Format("<option>{0}</option><input type='hidden' value='{1}'/>",  listminmax[i].Parameter.ToString(), listminmax[i].Avg.ToString() ));
                    //       // builder.Append(str);
                    //    }
                    //}
                    //else
                    //{
                    //    txtGraphParams2.Text = "";
                    //}
                    //ddlGraphParams2.InnerHtml = builder.ToString();


                    //ddlGraphParam3.DataSource = listminmax;
                    //ddlGraphParam3.DataTextField = "Parameter";
                    //ddlGraphParam3.DataValueField = "Avg";
                    //ddlGraphParam3.DataBind();
                    //setDropdownGraphParamColor(listminmax, ddlGraphParam3);
                    //ddlGraphParam3.SelectedIndex = 2;

                    //builder = new System.Text.StringBuilder();
                    //if (listminmax.Count > 0)
                    //{
                    //    for (int i = 0; i < listminmax.Count; i++)
                    //    {
                    //        if (i == 1)
                    //        {
                    //            txtGraphParams3.Text = listminmax[i].Parameter.ToString();
                    //        }
                    //        string str = "<option value='" + listminmax[i].Avg + "'>" + listminmax[i].Parameter + "</option>";
                    //        builder.Append(str);
                    //    }
                    //}
                    //else
                    //{
                    //    txtGraphParams3.Text = "";
                    //}
                    //ddlGraphParams3.InnerHtml = builder.ToString();



                    //ddlGraphParam4.DataSource = listminmax;
                    //ddlGraphParam4.DataTextField = "Parameter";
                    //ddlGraphParam4.DataValueField = "Avg";
                    //ddlGraphParam4.DataBind();
                    //setDropdownGraphParamColor(listminmax, ddlGraphParam4);
                    //ddlGraphParam4.SelectedIndex = 3;
                    //ddlGraphParam5.DataSource = listminmax;
                    //ddlGraphParam5.DataTextField = "Parameter";
                    //ddlGraphParam5.DataValueField = "Avg";
                    //ddlGraphParam5.DataBind();
                    //setDropdownGraphParamColor(listminmax, ddlGraphParam5);
                    //ddlGraphParam5.SelectedIndex = 4;
                    ddlGraphParams1.InnerHtml = ddllist;
                    txtGraphParams1.Text = listminmax[0].Parameter.ToString();
                    ddlGraphParams2.InnerHtml = ddllist;
                    txtGraphParams2.Text = listminmax[1].Parameter.ToString();
                    ddlGraphParams3.InnerHtml = ddllist;
                    txtGraphParams3.Text = listminmax[2].Parameter.ToString();
                    ddlGraphParams4.InnerHtml = ddllist;
                    txtGraphParams4.Text = listminmax[3].Parameter.ToString();
                    ddlGraphParams5.InnerHtml = ddllist;
                    txtGraphParams5.Text = listminmax[4].Parameter.ToString();
                    //ddlGraphParams1.InnerHtml = setdropdownvaluesforgraph(listminmax, 0, txtGraphParams1, ddlGraphParams1);
                    //ddlGraphParams2.InnerHtml = setdropdownvaluesforgraph(listminmax, 1, txtGraphParams2, ddlGraphParams2);
                    //ddlGraphParams3.InnerHtml = setdropdownvaluesforgraph(listminmax, 2, txtGraphParams3, ddlGraphParams3);
                    //ddlGraphParams4.InnerHtml = setdropdownvaluesforgraph(listminmax, 3, txtGraphParams4, ddlGraphParams4);
                    //ddlGraphParams5.InnerHtml = setdropdownvaluesforgraph(listminmax, 4, txtGraphParams5, ddlGraphParams5);
                }

            }
            catch (Exception e)
            {

            }
            if (success == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('System Doc has incorrect datatype value. Please check input module data.');", true);
                return;
            }
            //gvMinMaxAvg.DataSource = DBAccess.getATKParameterMinMaxAvg(sort);
            //gvMinMaxAvg.DataBind();
        }

        private string setdropdownvaluesforgraph(List<ADKParameterMinMaxAvg> listminmax)
        {
            var builder = new System.Text.StringBuilder();
            if (listminmax.Count > 0)
            {
                for (int i = 0; i < listminmax.Count; i++)
                {

                    // string str = "<option>" + listminmax[i].Parameter.ToString() + "</option><input type='hidden' value='"+ listminmax[i].Avg.ToString() + "'/>";
                    //style = 'font-weight:unset;font-size: 2px'
                    string str = "<option style='font-weight:unset'>" + listminmax[i].Parameter.ToString() + "</option><input type='hidden' value='"+ listminmax[i].Avg.ToString() + "'/>";
                   // string str = "<option><span  style='font-weight:unset;font-size: 2px'>" + listminmax[i].Parameter.ToString() + "</span></option><input type='hidden' value='" + listminmax[i].Avg.ToString() + "'/>";
                    builder.Append(str);
                   
                }
            }
            return builder.ToString();
        }

        private void setQueryStringWhenClickBtn()
        {
            string querystring = txtQuery.Text;
            string querystringLower = txtQuery.Text.ToLower();
            if (querystringLower.Contains("where"))
            {
                if (Regex.Split(querystring, "where")[1] != "")
                {
                    string q = Regex.Split(querystring, "where")[1];
                    txtQuery.Text = "select * from SystemDocTransaction where " + q;
                }
            }
            else
            {
                txtQuery.Text = "select * from SystemDocTransaction";
            }
        }

        protected void allInfoBtn_Click(object sender, EventArgs e)
        {
            Session["ATKPagination"] = "allinformation";
            // DataTable dt = DBAccess.getSystemDocumentData();
            DataTable dt = DBAccess.getAllTabDetails("", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
            if (cbSdocdrillOption.Checked)
            {
                //removeTableColumn(dt);
                //gvNormalSDocbind.DataSource = dt;
                //gvNormalSDocbind.DataBind();
                bindNormalSdoclist(dt);
                gvNormalSDocbind.Visible = true;
                gvDisplayData.Visible = false;
                noOfRows.Text = dt.Rows.Count.ToString();
            }
            else
            {
                bindSDocList(dt);
                gvNormalSDocbind.Visible = false;
                gvDisplayData.Visible = true;
            }
            //gvDisplayData.DataSource = dt;
            //gvDisplayData.DataBind();
            //noOfRows.Text = dt.Rows.Count.ToString();
            bindListBox();
            setQueryStringWhenClickBtn();
        }
        protected void systemDocBtn_Click(object sender, EventArgs e)
        {
            Session["ATKPagination"] = "systemdoc";
            // DataTable dt = DBAccess.getSystemDocumentData();
            DataTable dt = DBAccess.getAllTabDetails("General Information", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
            if (cbSdocdrillOption.Checked)
            {
                //removeTableColumn(dt);
                //gvNormalSDocbind.DataSource = dt;
                //gvNormalSDocbind.DataBind();
                bindNormalSdoclist(dt);
                gvNormalSDocbind.Visible = true;
                gvDisplayData.Visible = false;
                noOfRows.Text = dt.Rows.Count.ToString();
            }
            else
            {
                bindSDocList(dt);
                gvNormalSDocbind.Visible = false;
                gvDisplayData.Visible = true;
            }
            //gvDisplayData.DataSource = dt;
            //gvDisplayData.DataBind();
            //noOfRows.Text = dt.Rows.Count.ToString();
            bindListBox();
            setQueryStringWhenClickBtn();
        }

        protected void machineToolBtn_Click(object sender, EventArgs e)
        {
            Session["ATKPagination"] = "machinetool";
            //  DataTable dt = DBAccess.getMachineToolData();
            DataTable dt = DBAccess.getAllTabDetails("Machine Tool", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
            if(cbSdocdrillOption.Checked)
            {
                //removeTableColumn(dt);
                //gvNormalSDocbind.DataSource = dt;
                //gvNormalSDocbind.DataBind();
                bindNormalSdoclist(dt);
                gvNormalSDocbind.Visible = true;
                gvDisplayData.Visible = false;
                noOfRows.Text = dt.Rows.Count.ToString();
            }
            else
            {
                bindSDocList(dt);
                gvNormalSDocbind.Visible = false;
                gvDisplayData.Visible = true;
            }
            // gvDisplayData.DataSource = dt;
            // gvDisplayData.DataBind();
            //noOfRows.Text = dt.Rows.Count.ToString();
            bindListBox();
            setQueryStringWhenClickBtn();
        }

        protected void wheelBtn_Click(object sender, EventArgs e)
        {
            Session["ATKPagination"] = "wheel";
            //DataTable dt = DBAccess.getWheelData();
            DataTable dt = DBAccess.getAllTabDetails("Consumables Details", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
            if (cbSdocdrillOption.Checked)
            {
                //removeTableColumn(dt);
                //gvNormalSDocbind.DataSource = dt;
                //gvNormalSDocbind.DataBind();
                bindNormalSdoclist(dt);
                gvNormalSDocbind.Visible = true;
                gvDisplayData.Visible = false;
                noOfRows.Text = dt.Rows.Count.ToString();
            }
            else
            {
                bindSDocList(dt);
                gvNormalSDocbind.Visible = false;
                gvDisplayData.Visible = true;
            }
            //gvDisplayData.DataSource = dt;
            //gvDisplayData.DataBind();
            //noOfRows.Text = dt.Rows.Count.ToString();
            bindListBox();
            setQueryStringWhenClickBtn();
        }

        protected void workPieceBtn_Click(object sender, EventArgs e)
        {
            Session["ATKPagination"] = "workpiece";
            //   DataTable dt = DBAccess.getWorkPieceData();
            DataTable dt = DBAccess.getAllTabDetails("Workpiece Details", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
            if (cbSdocdrillOption.Checked)
            {
                //removeTableColumn(dt);
                //gvNormalSDocbind.DataSource = dt;
                //gvNormalSDocbind.DataBind();
                bindNormalSdoclist(dt);
                gvNormalSDocbind.Visible = true;
                gvDisplayData.Visible = false;
                noOfRows.Text = dt.Rows.Count.ToString();
            }
            else
            {
                bindSDocList(dt);
                gvNormalSDocbind.Visible = false;
                gvDisplayData.Visible = true;
            }
            //gvDisplayData.DataSource = dt;
            //gvDisplayData.DataBind();
            //noOfRows.Text = dt.Rows.Count.ToString();
            bindListBox();
            setQueryStringWhenClickBtn();
        }

        protected void opeParBtn_Click(object sender, EventArgs e)
        {
            Session["ATKPagination"] = "operational";
            //  DataTable dt = DBAccess.getOperationalParameterData();
            DataTable dt = DBAccess.getAllTabDetails("Operational Parameters", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
            if (cbSdocdrillOption.Checked)
            {
                //removeTableColumn(dt);
                //gvNormalSDocbind.DataSource = dt;
                //gvNormalSDocbind.DataBind();
                bindNormalSdoclist(dt);
                gvNormalSDocbind.Visible = true;
                gvDisplayData.Visible = false;
                noOfRows.Text = dt.Rows.Count.ToString();
            }
            else
            {
                bindSDocList(dt);
                gvNormalSDocbind.Visible = false;
                gvDisplayData.Visible = true;
            }
            //gvDisplayData.DataSource = dt;
            //gvDisplayData.DataBind();
            //noOfRows.Text = dt.Rows.Count.ToString();
            bindListBox();
            setQueryStringWhenClickBtn();
        }

        protected void targetQlyBtn_Click(object sender, EventArgs e)
        {
            Session["ATKPagination"] = "quality";
            // DataTable dt = DBAccess.getTargetQualityData();
            DataTable dt = DBAccess.getAllTabDetails("Quality Parameters", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
            if (cbSdocdrillOption.Checked)
            {
                //removeTableColumn(dt);
                //gvNormalSDocbind.DataSource = dt;
                //gvNormalSDocbind.DataBind();
                bindNormalSdoclist(dt);
                gvNormalSDocbind.Visible = true;
                gvDisplayData.Visible = false;
                noOfRows.Text = dt.Rows.Count.ToString();
            }
            else
            {
                bindSDocList(dt);
                gvNormalSDocbind.Visible = false;
                gvDisplayData.Visible = true;
            }
            //gvDisplayData.DataSource = dt;
            //gvDisplayData.DataBind();
            //noOfRows.Text = dt.Rows.Count.ToString();
            bindListBox();
            setQueryStringWhenClickBtn();

        }

        protected void ddlParameter_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindDDLParameterValue();
        }

        protected void executeBtn_Click(object sender, EventArgs e)
        {

            if (hdnSearchToggle.Value == "Parameter")
            {
                searchIcon.InnerText = "Parameter";
                //ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", " document.getElementById('ddlParameter').style.display = 'inline-block'; document.getElementById('ddlParamValue').style.display = 'inline-block';document.getElementById('ddlSdocPlungeCatValues').style.display = 'none';document.getElementById('ddlSdocPlungeCat').style.display = 'none';", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "bindParameterValue();", true);
                //ddlParameter.Style.Add("display", "inline-block");
                txtParameternew.Style.Add("display", "inline-block");
                ddlParamValue.Style.Add("display", "inline-block");
                ddlSdocPlungeCatValues.Style.Add("display", "none");
                ddlSdocPlungeCat.Style.Add("display", "none");
            }
            else
            {
                searchIcon.InnerText = "System Doc";
                ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", "bindSdocPlungeCatValue();", true);
                //ScriptManager.RegisterStartupScript(this, GetType(), "hideDiv", " document.getElementById('ddlSdocPlungeCat').style.display = 'inline-block';document.getElementById('ddlSdocPlungeCatValues').style.display = 'inline-block';document.getElementById('ddlParameter').style.display = 'none';document.getElementById('ddlParamValue').style.display = 'none';", true);
                ddlSdocPlungeCatValues.Style.Add("display", "inline-block");
                ddlSdocPlungeCat.Style.Add("display", "inline-block");
                // ddlParameter.Style.Add("display", "none");
                txtParameternew.Style.Add("display", "none");
                ddlParamValue.Style.Add("display", "none");
            }
            string statement = txtQuery.Text;
            string statementLower = txtQuery.Text.ToLower();
            string statementLower1 = txtQuery.Text.ToLower();
            // string query = "";
            StringBuilder query = new StringBuilder();
            query.Append("");
            string column = "";
            List<string> listAndOr = new List<string>();
            try
            {
                if (!statementLower.Contains("select ") || !statementLower.Contains(" from "))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                    return;
                }
                string[] starsplit;
                if (statementLower.Contains("*"))
                {
                    starsplit = statementLower.Split('*');
                    if (starsplit[0].Contains("select") && starsplit[1].Contains("from"))
                    {
                        column = "";
                    }
                }
                if (!statementLower.Contains("*"))
                {
                    if (statementLower.Contains("select") && statementLower.Contains("from"))
                    {
                        string split1 = Regex.Split(statement, "select")[1];
                        column = Regex.Split(split1, "from")[0].Trim();
                    }

                }
                if (statementLower.Contains("where"))
                {
                    if (Regex.Split(statementLower, "where")[1] != "")
                    {
                        string q = Regex.Split(statementLower, "where")[1];

                        if (q.Contains("sdocname") || q.Contains("plungeid") || q.Contains("subcategoryid"))
                        {
                            query.Append(q);
                        }
                        else
                        {
                            string ss = q;
                            //Regex r = new Regex(@"'(.+?)'|in\(.*\)$");
                            Regex r = new Regex(@"'(.+?)'|in[ ]{0,}\(.+?\)");
                            MatchCollection mc = r.Matches(ss);
                            int len = mc.Count;
                            string replacestring = ss;
                            int jj = 0;
                            List<ListItem> listofvalues = new List<ListItem>();
                            foreach (Match data in mc)
                            {
                                if (ss.Contains(data.Value))
                                {

                                    // replacestring = replacestring.Replace(data.Value, "r" + jj);
                                    replacestring = ReplaceFirst(replacestring, data.Value, "r" + jj);
                                    listofvalues.Add(new ListItem("r" + jj, data.Value));
                                    jj++;
                                }
                            }
                            string finalstr = replacestring;
                            statementLower = finalstr;
                            // var arra = finalstr.Split(new string[] { " and ", " or " }, StringSplitOptions.RemoveEmptyEntries);
                            string[] splitcondition = finalstr.Split(new string[] { " and ", " or " }, StringSplitOptions.RemoveEmptyEntries);
                            do
                            {
                                int indexofAnd = statementLower.IndexOf(" and ");
                                int indexofOr = statementLower.IndexOf(" or ");
                                if (indexofAnd == indexofOr)
                                {
                                    break;
                                }
                                if (indexofOr == -1 && indexofAnd >= 0)
                                {
                                    listAndOr.Add("and");
                                    statementLower = statementLower.Substring(indexofAnd + 3);
                                }
                                else if (indexofAnd == -1 && indexofOr >= 0)
                                {
                                    listAndOr.Add("or");
                                    statementLower = statementLower.Substring(indexofOr + 2);
                                }
                                else
                                if (indexofAnd < indexofOr)
                                {
                                    listAndOr.Add("and");
                                    statementLower = statementLower.Substring(indexofAnd + 3);
                                }
                                else
                                {
                                    listAndOr.Add("or");
                                    statementLower = statementLower.Substring(indexofOr + 2);
                                }
                            } while (statementLower.Length > 0);
                            int indexAndOR = 0;
                            for (int i = 0; i < splitcondition.Length; i++)
                            {
                                //string condition = splitcondition[i].Trim().Replace("", string.Empty);
                                string param, value;
                                //string condition = Regex.Replace(splitcondition[i].ToString(), " ", string.Empty);
                                string condition = splitcondition[i].ToString().Trim();
                                //condition = Regex.Replace(condition, "\n", string.Empty);
                                if (!condition.Contains("="))
                                {
                                    string incondition = condition.Trim();

                                    value = incondition.Split(' ').Last().Trim();
                                    param = incondition.Remove(incondition.Length - value.Length).Trim();
                                    foreach (ListItem data in listofvalues)
                                    {
                                        if (value == data.Text)
                                        {
                                            value = data.Value;
                                        }
                                    }
                                    value = " " + value;
                                }
                                else
                                {
                                    try
                                    {
                                        param = Regex.Split(splitcondition[i], "=")[0].Trim();
                                        //value = "=" + Regex.Split(splitcondition[i], "=")[1];
                                        value = Regex.Split(splitcondition[i], "=")[1];
                                        foreach (ListItem data in listofvalues)
                                        {
                                            if (value == data.Text)
                                            {
                                                value = data.Value;
                                            }
                                        }
                                        value = "=" + value;
                                    }
                                    catch (Exception es)
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                                        return;
                                    }
                                }

                                if (i == 0)
                                {
                                    query.Append("(Parameter='" + param + "' And Value" + value + ") ");
                                }
                                else
                                {
                                    query.Append(listAndOr[indexAndOR] + " (Parameter='" + param + "' And Value" + value + ") ");
                                    indexAndOR++;
                                }
                            }
                        }
                    }
                }
                else
                {
                    string splitvalue = Regex.Split(statementLower1, "systemdoctransaction")[1];
                    if (splitvalue != "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                return;
            }
            string inputmodule = "";
            if (column == "")
            {
                Session["ATKPagination"] = hdnActiveBtn.Value;
                string sessionInputModuleValue = Session["ATKPagination"].ToString();
                if (sessionInputModuleValue == "systemdoc")
                {
                    inputmodule = "General Information";
                }
                else if (sessionInputModuleValue == "machinetool")
                {
                    inputmodule = "Machine Tool";
                }
                else if (sessionInputModuleValue == "wheel")
                {
                    inputmodule = "Consumables Details";
                }
                else if (sessionInputModuleValue == "workpiece")
                {
                    inputmodule = "Workpiece Details";
                }
                else if (sessionInputModuleValue == "operational")
                {
                    inputmodule = "Operational Parameters";
                }
                else if (sessionInputModuleValue == "quality")
                {
                    inputmodule = "Quality Parameters";
                }
                else if (sessionInputModuleValue == "allinformation")
                {
                    inputmodule = "";
                }
            }
            else
            {
                Session["ATKPagination"] = "execute";
            }

            string error = "";
            Session["ExecuteConditions"] = query.ToString();
            DataTable dt = DBAccess.getQueryData(query.ToString(), column, inputmodule, out error);
            if (error != "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                return;
            }
            Session["QueryForPageindex"] = txtQuery.Text;
            //  Session["ATKPagination"] = "execute";
            if(cbSdocdrillOption.Checked)
            {
                //removeTableColumn(dt);
                //gvNormalSDocbind.DataSource = dt;
                //gvNormalSDocbind.DataBind();
                bindNormalSdoclist(dt);
                gvNormalSDocbind.Visible = true;
                gvDisplayData.Visible = false;
                noOfRows.Text = dt.Rows.Count.ToString();
            }
            else
            {
                bindSDocList(dt);
                gvNormalSDocbind.Visible = false;
                gvDisplayData.Visible = true;
            }
            //gvDisplayData.DataSource = dt;
            //gvDisplayData.DataBind();
            //noOfRows.Text = dt.Rows.Count.ToString();
            DBAccess.saveQueryInfo(txtQuery.Text, Session["EmpName"].ToString());
            bindQueryList();


            //Statistics binding
            statistics_Click(sender, e);
        }
   
        public string ReplaceFirst(string text, string search, string replace)
        {
            int pos = text.IndexOf(search);
            if (pos < 0)
            {
                return text;
            }
            return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
        }
        protected void allBtn_Click(object sender, EventArgs e)
        {
            if (txtQuery.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Enter query.');", true);
            }
            List<string> conditionList = new List<string>();
            if (Session["APKDisplayCondition"] != null)
            {

                conditionList = (List<string>)Session["APKDisplayCondition"];
                conditionList.Add(" and ");
                // conditionList.Add(ddlParameter.Text);
                conditionList.Add(txtParameternew.Text);
                conditionList.Add(ddlParamValue.Text);
            }
            //    Session["APKDisplayCondition"] = conditionList;
            //    StringBuilder query = new StringBuilder();
            //    query.Append("select * from System_Documents_ where ");
            //    for(int i=0;i<conditionList.Count;i=i+2)
            //    {
            //        query.Append(conditionList[i]);
            //        query.Append("=");
            //        query.Append("'"+conditionList[i+1]+"'");
            //        if(i!=(conditionList.Count-2))
            //        {
            //            query.Append(" and ");
            //        }

            //    }
            //    txtQuery.Text = query.ToString();
            //}
            else
            {
                //List<string> conditionList = new List<string>();
                conditionList.Add("");
                //conditionList.Add(ddlParameter.Text);
                conditionList.Add(txtParameternew.Text);
                conditionList.Add(ddlParamValue.Text);
            }
            int flag = 0;
            for (int i = 0; i < conditionList.Count - 3; i = i + 3)
            {
                if (conditionList[conditionList.Count - 2] == conditionList[i + 1] && conditionList[conditionList.Count - 1] == conditionList[i + 2])
                {
                    flag = 1;
                    break;
                }
            }
            if (flag == 1)
            {
                conditionList.RemoveAt(conditionList.Count - 1);
                conditionList.RemoveAt(conditionList.Count - 1);
                conditionList.RemoveAt(conditionList.Count - 1);
            }
            Session["APKDisplayCondition"] = conditionList;
            StringBuilder query = new StringBuilder();
            string queryString = txtQuery.Text;
            if (queryString.Contains("*"))
            {
                query.Append("select * from SystemDocTransaction where ");
            }
            else
            {
                string str = queryString.Split(new string[] { "select" }, StringSplitOptions.None)[1].Split(new string[] { "from" }, StringSplitOptions.None)[0].Trim();
                query.Append("select " + str + " from SystemDocTransaction where ");
            }
            for (int i = 0; i < conditionList.Count; i = i + 3)
            {
                query.Append(conditionList[i]);
                query.Append(conditionList[i + 1]);
                query.Append("=");
                query.Append("'" + conditionList[i + 2] + "'");
                //if (i != (conditionList.Count - 2))
                //{
                //    query.Append(" and ");
                //}

            }
            txtQuery.Text = query.ToString();
        }

        protected void anyBtn_Click(object sender, EventArgs e)
        {
            if (txtQuery.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Enter query.');", true);
            }
            List<string> conditionList = new List<string>();
            if (Session["APKDisplayCondition"] != null)
            {

                conditionList = (List<string>)Session["APKDisplayCondition"];
                conditionList.Add(" or ");
                // conditionList.Add(ddlParameter.Text);
                conditionList.Add(txtParameternew.Text);
                conditionList.Add(ddlParamValue.Text);

            }
            else
            {
                //List<string> conditionList = new List<string>();
                conditionList.Add("");
                //conditionList.Add(ddlParameter.Text);
                conditionList.Add(txtParameternew.Text);
                conditionList.Add(ddlParamValue.Text);
            }
            int flag = 0;
            for (int i = 0; i < conditionList.Count - 3; i = i + 3)
            {
                if (conditionList[conditionList.Count - 2] == conditionList[i + 1] && conditionList[conditionList.Count - 1] == conditionList[i + 2])
                {
                    flag = 1;
                    break;
                }
            }
            if (flag == 1)
            {
                conditionList.RemoveAt(conditionList.Count - 1);
                conditionList.RemoveAt(conditionList.Count - 1);
                conditionList.RemoveAt(conditionList.Count - 1);
            }
            Session["APKDisplayCondition"] = conditionList;
            StringBuilder query = new StringBuilder();
            string queryString = txtQuery.Text;
            if (queryString.Contains("*"))
            {
                query.Append("select * from SystemDocTransaction where ");
            }
            else
            {
                string str = queryString.Split(new string[] { "select" }, StringSplitOptions.None)[1].Split(new string[] { "from" }, StringSplitOptions.None)[0].Trim();
                query.Append("select " + str + " from SystemDocTransaction where ");
            }
            for (int i = 0; i < conditionList.Count; i = i + 3)
            {
                query.Append(conditionList[i]);
                query.Append(conditionList[i + 1]);
                query.Append("=");
                query.Append("'" + conditionList[i + 2] + "'");
                //if (i != (conditionList.Count - 2))
                //{
                //    query.Append(" or ");
                //}

            }
            txtQuery.Text = query.ToString();
        }

        protected void clearBtn_Click(object sender, EventArgs e)
        {
            Session["APKDisplayCondition"] = null;
            Session["ExecuteConditions"] = "";
            DataTable dt = DBAccess.getSystemDocumentData();
            if(cbSdocdrillOption.Checked)
            {
                //removeTableColumn(dt);
                //gvNormalSDocbind.DataSource = dt;
                //gvNormalSDocbind.DataBind();
                bindNormalSdoclist(dt);
                gvNormalSDocbind.Visible = true;
                gvDisplayData.Visible = false;
                noOfRows.Text = dt.Rows.Count.ToString();
            }
            else
            {
                bindSDocList(dt);
                gvNormalSDocbind.Visible = false;
                gvDisplayData.Visible = true;
            }
          
            //gvDisplayData.DataSource = dt;
            //gvDisplayData.DataBind();
            //noOfRows.Text = dt.Rows.Count.ToString();
            Session["ATKPagination"] = "systemdoc";
            //bindDDLParameter();
            //bindDDLParameterValue();
            //bindListBox();
            txtQuery.Text = "select * from SystemDocTransaction";
            //systemDocBtn.Attributes.Add("class", "");
            systemDocBtn.CssClass = systemDocBtn.CssClass.Replace("inactive", "active").Trim();
            foreach (ListItem data in ddlMultiDownID.Items)            {                data.Selected = false;            }
            statistics_Click(sender, e);
            //string statement = txtQuery.Text.ToLower();
            //string query = "";
            //string column = "";
            //if (!statement.Contains("*"))
            //{
            //    if (statement.Contains("select") && statement.Contains("from"))
            //    {
            //        column = statement.Split(new string[] { "select" }, StringSplitOptions.None)[1].Split(new string[] { "from" }, StringSplitOptions.None)[0].Trim();
            //    }

            //}
            //if (statement.Contains("where"))
            //{
            //    if (statement.Split(new string[] { "where" }, StringSplitOptions.None)[1] != null)
            //    {
            //        query = statement.Split(new string[] { "where" }, StringSplitOptions.None)[1];
            //    }
            //}
            //string error = "";
            //DataTable dt = DBAccess.getQueryData(query,column, out error);
            //if (error != "")
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
            //    return;
            //}
            //Session["ATKPagination"] = "execute";
            //gvDisplayData.DataSource = dt;
            //gvDisplayData.DataBind();
            //noOfRows.Text = dt.Rows.Count.ToString();
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void setSDocIDInSession(string id)
        {
            HttpContext.Current.Session["ATKSDocID"] = id;
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void setInputModuletoSession(string inputModule)
        {
            HttpContext.Current.Session["InputModule"] = inputModule;
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string getInputModulefromSession()
        {
            if (HttpContext.Current.Session["InputModule"] != null)
            {
                return HttpContext.Current.Session["InputModule"].ToString();
            }
            else
            {
                return "All";
            }

        }

        protected void gvMinMaxAvg_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (e.SortExpression == "Item")
            {
                return;
            }
            else
            {
                SortDirection sortDirection = SortDirection.Ascending;
                string sortField = string.Empty;

                SortGridview((GridView)sender, e, out sortDirection, out sortField);
                string strSortDirection = sortDirection == SortDirection.Ascending ? "ASC" : "DESC";

                bindParameterMinMaxAvg(e.SortExpression + " " + strSortDirection);

            }
        }
        private void SortGridview(GridView gridView, GridViewSortEventArgs e, out SortDirection sortDirection, out string sortField)
        {
            sortField = e.SortExpression;
            sortDirection = e.SortDirection;

            if (gridView.Attributes["CurrentSortField"] != null && gridView.Attributes["CurrentSortDirection"] != null)
            {
                if (sortField == gridView.Attributes["CurrentSortField"])
                {
                    if (gridView.Attributes["CurrentSortDirection"] == "ASC")
                    {
                        sortDirection = SortDirection.Descending;
                    }
                    else
                    {
                        sortDirection = SortDirection.Ascending;
                    }
                }

                gridView.Attributes["CurrentSortField"] = sortField;
                gridView.Attributes["CurrentSortDirection"] = (sortDirection == SortDirection.Ascending ? "ASC" : "DESC");
            }
        }

        protected void gvMinMaxAvg_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (gvMinMaxAvg.Attributes["CurrentSortField"] != null && gvMinMaxAvg.Attributes["CurrentSortDirection"] != null)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    foreach (TableCell tableCell in e.Row.Cells)
                    {
                        if (tableCell.HasControls())
                        {
                            LinkButton sortLinkButton = null;
                            if (tableCell.Controls[0] is LinkButton)
                            {
                                sortLinkButton = (LinkButton)tableCell.Controls[0];
                            }

                            if (sortLinkButton != null && gvMinMaxAvg.Attributes["CurrentSortField"] == sortLinkButton.CommandArgument)
                            {
                                System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image();
                                if (gvMinMaxAvg.Attributes["CurrentSortDirection"] == "ASC")
                                {
                                    image.ImageUrl = "~/Images/UpArrow2.png";
                                    image.Width = 15;
                                }
                                else
                                {
                                    image.ImageUrl = "~/Images/UpArrow2.png";
                                    image.CssClass = "rotateImage";
                                    image.Width = 15;
                                }
                                tableCell.Controls.Add(new LiteralControl("&nbsp;"));
                                tableCell.Controls.Add(image);
                            }
                        }
                    }
                }
                //if (e.Row.RowType == DataControlRowType.DataRow)
                //{
                //	for (int i = 4; i < e.Row.Cells.Count; i++)
                //	{
                //		if(e.Row.Cells[i].HasControls())
                //		{
                //			string str = e.Row.Cells[i].Text;
                //		}
                //	}
                //}
            }
        }

        //protected void gvDisplayData_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {

        //        List<CustomColumn> list = DBAccess.getCustomColumnName();
        //        for (int i = 0; i < e.Row.Cells.Count; i++)
        //        {
        //            foreach (CustomColumn data in list)
        //            {
        //                if (e.Row.Cells[i].Text == data.ColumnName)
        //                {
        //                    //gvDisplayData.Columns[i].HeaderText = data.CustomName;
        //                    e.Row.Cells[i].Text = data.CustomName;
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}

        [System.Web.Services.WebMethod(EnableSession = true)]        public static string getDDLParamMeanValue(string param)        {            string mean = DBAccess.getATKDDLMeanValue(param);            return mean;        }

        protected void gvDisplayData_PageIndexChanging(object sender, GridViewPageEventArgs e)        {            gvDisplayData.PageIndex = e.NewPageIndex;            DataTable dt = null;
            if (Session["ATKPagination"].ToString() == "allinformation")
            {
                // dt = DBAccess.getSystemDocumentData();
                dt = DBAccess.getAllTabDetails("", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                //gvDisplayData.DataSource = dt;
                //gvDisplayData.DataBind();
                if (cbSdocdrillOption.Checked)
                {
                    //removeTableColumn(dt);
                    //gvNormalSDocbind.DataSource = dt;
                    //gvNormalSDocbind.DataBind();
                    bindNormalSdoclist(dt);
                    gvNormalSDocbind.Visible = true;
                    gvDisplayData.Visible = false;
                    noOfRows.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    bindSDocList(dt);
                    gvNormalSDocbind.Visible = false;
                    gvDisplayData.Visible = true;
                }
            }
            else if (Session["ATKPagination"].ToString() == "systemdoc")            {
                // dt = DBAccess.getSystemDocumentData();
                dt = DBAccess.getAllTabDetails("General Information", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                //gvDisplayData.DataSource = dt;
                //gvDisplayData.DataBind();
                if (cbSdocdrillOption.Checked)
                {
                    //removeTableColumn(dt);
                    //gvNormalSDocbind.DataSource = dt;
                    //gvNormalSDocbind.DataBind();
                    bindNormalSdoclist(dt);
                    gvNormalSDocbind.Visible = true;
                    gvDisplayData.Visible = false;
                    noOfRows.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    bindSDocList(dt);
                    gvNormalSDocbind.Visible = false;
                    gvDisplayData.Visible = true;
                }            }            else if (Session["ATKPagination"].ToString() == "machinetool")            {
                // dt = DBAccess.getMachineToolData();
                dt = DBAccess.getAllTabDetails("Machine Tool", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                //gvDisplayData.DataSource = dt;
                //gvDisplayData.DataBind();
                if (cbSdocdrillOption.Checked)
                {
                    //removeTableColumn(dt);
                    //gvNormalSDocbind.DataSource = dt;
                    //gvNormalSDocbind.DataBind();
                    bindNormalSdoclist(dt);
                    gvNormalSDocbind.Visible = true;
                    gvDisplayData.Visible = false;
                    noOfRows.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    bindSDocList(dt);
                    gvNormalSDocbind.Visible = false;
                    gvDisplayData.Visible = true;
                }            }            else if (Session["ATKPagination"].ToString() == "wheel")            {
                //  dt = DBAccess.getWheelData();
                dt = DBAccess.getAllTabDetails("Consumables Details", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                //gvDisplayData.DataSource = dt;
                //gvDisplayData.DataBind();
                if (cbSdocdrillOption.Checked)
                {
                    //removeTableColumn(dt);
                    //gvNormalSDocbind.DataSource = dt;
                    //gvNormalSDocbind.DataBind();
                    bindNormalSdoclist(dt);
                    gvNormalSDocbind.Visible = true;
                    gvDisplayData.Visible = false;
                    noOfRows.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    bindSDocList(dt);
                    gvNormalSDocbind.Visible = false;
                    gvDisplayData.Visible = true;
                }            }            else if (Session["ATKPagination"].ToString() == "workpiece")            {
                // dt = DBAccess.getWorkPieceData();
                dt = DBAccess.getAllTabDetails("Workpiece Details", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                //gvDisplayData.DataSource = dt;
                //gvDisplayData.DataBind();
                if (cbSdocdrillOption.Checked)
                {
                    //removeTableColumn(dt);
                    //gvNormalSDocbind.DataSource = dt;
                    //gvNormalSDocbind.DataBind();
                    bindNormalSdoclist(dt);
                    gvNormalSDocbind.Visible = true;
                    gvDisplayData.Visible = false;
                    noOfRows.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    bindSDocList(dt);
                    gvNormalSDocbind.Visible = false;
                    gvDisplayData.Visible = true;
                }            }            else if (Session["ATKPagination"].ToString() == "operational")            {
                //  dt = DBAccess.getOperationalParameterData();
                dt = DBAccess.getAllTabDetails("Operational Parameters", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                //gvDisplayData.DataSource = dt;
                //gvDisplayData.DataBind();
                if (cbSdocdrillOption.Checked)
                {
                    //removeTableColumn(dt);
                    //gvNormalSDocbind.DataSource = dt;
                    //gvNormalSDocbind.DataBind();
                    bindNormalSdoclist(dt);
                    gvNormalSDocbind.Visible = true;
                    gvDisplayData.Visible = false;
                    noOfRows.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    bindSDocList(dt);
                    gvNormalSDocbind.Visible = false;
                    gvDisplayData.Visible = true;
                }            }            else if (Session["ATKPagination"].ToString() == "quality")            {

                dt = DBAccess.getAllTabDetails("Quality Parameters", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                // dt = DBAccess.getTargetQualityData();
                //gvDisplayData.DataSource = dt;
                //gvDisplayData.DataBind();
                if (cbSdocdrillOption.Checked)
                {
                    //removeTableColumn(dt);
                    //gvNormalSDocbind.DataSource = dt;
                    //gvNormalSDocbind.DataBind();
                    bindNormalSdoclist(dt);
                    gvNormalSDocbind.Visible = true;
                    gvDisplayData.Visible = false;
                    noOfRows.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    bindSDocList(dt);
                    gvNormalSDocbind.Visible = false;
                    gvDisplayData.Visible = true;
                }            }            else if (Session["ATKPagination"].ToString() == "execute")            {                string statement = Session["QueryForPageindex"].ToString();                string statementLower = Session["QueryForPageindex"].ToString().ToLower();
                // string query = "";
                StringBuilder query = new StringBuilder();                query.Append("");                string column = "";                List<string> listAndOr = new List<string>();                try                {                    string[] starsplit;                    if (statementLower.Contains("*"))                    {                        starsplit = statementLower.Split('*');                        if (starsplit[0].Contains("select") && starsplit[1].Contains("from"))                        {                            column = "";                        }                    }                    if (!statementLower.Contains("*"))                    {                        if (statementLower.Contains("select") && statementLower.Contains("from"))                        {                            string split1 = Regex.Split(statement, "select")[1];                            column = Regex.Split(split1, "from")[0].Trim();                        }                    }                    if (statementLower.Contains("where"))
                    {
                        if (Regex.Split(statementLower, "where")[1] != "")
                        {
                            string q = Regex.Split(statementLower, "where")[1];

                            if (q.Contains("sdocname") || q.Contains("plungeid") || q.Contains("subcategoryid"))
                            {
                                query.Append(q);
                            }
                            else
                            {
                                string ss = q;
                                //Regex r = new Regex(@"'(.+?)'|in\(.*\)$");
                                Regex r = new Regex(@"'(.+?)'|in[ ]{0,}\(.+?\)");
                                MatchCollection mc = r.Matches(ss);
                                int len = mc.Count;
                                string replacestring = ss;
                                int jj = 0;
                                List<ListItem> listofvalues = new List<ListItem>();
                                foreach (Match data in mc)
                                {
                                    if (ss.Contains(data.Value))
                                    {

                                        //  replacestring = replacestring.Replace(data.Value, "r" + jj);
                                        replacestring = ReplaceFirst(replacestring, data.Value, "r" + jj);
                                        listofvalues.Add(new ListItem("r" + jj, data.Value));
                                        jj++;
                                    }
                                }
                                string finalstr = replacestring;
                                statementLower = finalstr;
                                // var arra = finalstr.Split(new string[] { " and ", " or " }, StringSplitOptions.RemoveEmptyEntries);
                                string[] splitcondition = finalstr.Split(new string[] { " and ", " or " }, StringSplitOptions.RemoveEmptyEntries);
                                do
                                {
                                    int indexofAnd = statementLower.IndexOf(" and ");
                                    int indexofOr = statementLower.IndexOf(" or ");
                                    if (indexofAnd == indexofOr)
                                    {
                                        break;
                                    }
                                    if (indexofOr == -1 && indexofAnd >= 0)
                                    {
                                        listAndOr.Add("and");
                                        statementLower = statementLower.Substring(indexofAnd + 3);
                                    }
                                    else if (indexofAnd == -1 && indexofOr >= 0)
                                    {
                                        listAndOr.Add("or");
                                        statementLower = statementLower.Substring(indexofOr + 2);
                                    }
                                    else
                                    if (indexofAnd < indexofOr)
                                    {
                                        listAndOr.Add("and");
                                        statementLower = statementLower.Substring(indexofAnd + 3);
                                    }
                                    else
                                    {
                                        listAndOr.Add("or");
                                        statementLower = statementLower.Substring(indexofOr + 2);
                                    }
                                } while (statementLower.Length > 0);
                                int indexAndOR = 0;
                                for (int i = 0; i < splitcondition.Length; i++)
                                {
                                    //string condition = splitcondition[i].Trim().Replace("", string.Empty);
                                    string param, value;
                                    //string condition = Regex.Replace(splitcondition[i].ToString(), " ", string.Empty);
                                    string condition = splitcondition[i].ToString().Trim();
                                    //condition = Regex.Replace(condition, "\n", string.Empty);
                                    if (!condition.Contains("="))
                                    {
                                        string incondition = condition.Trim();

                                        value = incondition.Split(' ').Last().Trim();
                                        param = incondition.Remove(incondition.Length - value.Length).Trim();
                                        foreach (ListItem data in listofvalues)
                                        {
                                            if (value == data.Text)
                                            {
                                                value = data.Value;
                                            }
                                        }
                                        value = " " + value;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            param = Regex.Split(splitcondition[i], "=")[0].Trim();
                                            //value = "=" + Regex.Split(splitcondition[i], "=")[1];
                                            value = Regex.Split(splitcondition[i], "=")[1];
                                            foreach (ListItem data in listofvalues)
                                            {
                                                if (value == data.Text)
                                                {
                                                    value = data.Value;
                                                }
                                            }
                                            value = "=" + value;
                                        }
                                        catch (Exception es)
                                        {
                                            ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                                            return;
                                        }
                                    }

                                    if (i == 0)
                                    {
                                        query.Append("(Parameter='" + param + "' And Value" + value + ") ");
                                    }
                                    else
                                    {
                                        query.Append(listAndOr[indexAndOR] + " (Parameter='" + param + "' And Value" + value + ") ");
                                        indexAndOR++;
                                    }
                                }
                            }
                        }
                    }                }                catch (Exception ex)                {                }                string inputmodule = "";                if (column == "")                {                    string sessionInputModuleValue = Session["ATKPagination"].ToString();                    if (sessionInputModuleValue == "systemdoc")                    {                        inputmodule = "General Information";                    }                    else if (sessionInputModuleValue == "machinetool")                    {                        inputmodule = "Machine Tool";                    }                    else if (sessionInputModuleValue == "wheel")                    {                        inputmodule = "Consumables Details";                    }                    else if (sessionInputModuleValue == "workpiece")                    {                        inputmodule = "Workpiece Details";                    }                    else if (sessionInputModuleValue == "operational")                    {                        inputmodule = "Operational Parameters";                    }                    else if (sessionInputModuleValue == "quality")                    {                        inputmodule = "Quality Parameters";                    }
                    else if (sessionInputModuleValue == "allinformation")
                    {
                        inputmodule = "";
                    }                }                string error = "";                dt = DBAccess.getQueryData(query.ToString(), column, inputmodule, out error);
                //if (error != "")
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                //    return;
                //}
                //gvDisplayData.DataSource = dt;
                //gvDisplayData.DataBind();
                if (cbSdocdrillOption.Checked)
                {
                    //removeTableColumn(dt);
                    //gvNormalSDocbind.DataSource = dt;
                    //gvNormalSDocbind.DataBind();
                    bindNormalSdoclist(dt);
                    gvNormalSDocbind.Visible = true;
                    gvDisplayData.Visible = false;
                    noOfRows.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    bindSDocList(dt);
                    gvNormalSDocbind.Visible = false;
                    gvDisplayData.Visible = true;
                }            }           // noOfRows.Text = dt.Rows.Count.ToString();        }
        protected void gvDisplayData_PreRender(object sender, EventArgs e)
        {
            GridView grid = (GridView)sender;            if (grid != null)            {                GridViewRow pagerRow = (GridViewRow)grid.BottomPagerRow;                if (pagerRow != null)                {                    pagerRow.Visible = true;                }            }
        }

        protected void Export_Click(object sender, EventArgs e)
        {

            string templatefile = string.Empty;
            string Filename = "ApplicationToolKit.xlsx";

            string Source = string.Empty;
            Source = GetReportPath(Filename);
            string Template = string.Empty;
            Template = "ApplicationToolKit_" + DateTime.Now + ".xlsx";
            string destination = string.Empty;
            destination = Path.Combine(appPath, "Temp", SafeFileName(Template));
            if (!File.Exists(Source))
            {
                Logger.WriteDebugLog("Application Tool Kit- \n " + Source);
            }

            List<ADKParameterMinMaxAvg> listminmaxavg = new List<ADKParameterMinMaxAvg>();
            ADKParameterMinMaxAvg minmaxavg = null;
            for (int i = 0; i < gvMinMaxAvg.Rows.Count; i++)
            {

                minmaxavg = new ADKParameterMinMaxAvg();
                minmaxavg.Parameter = ((Label)gvMinMaxAvg.Rows[i].FindControl("item")).Text;
                minmaxavg.Min = (gvMinMaxAvg.Rows[i].FindControl("min") as Label).Text;
                minmaxavg.Max = (gvMinMaxAvg.Rows[i].FindControl("max") as Label).Text;
                minmaxavg.Avg = (gvMinMaxAvg.Rows[i].FindControl("avg") as Label).Text;
                listminmaxavg.Add(minmaxavg);
            }

            if (cbSdocdrillOption.Checked)
            {
                if (gvNormalSDocbind.Rows.Count > 0)
                {
                    FileInfo newFile = new FileInfo(Source);
                    ExcelPackage Excel = new ExcelPackage(newFile, true);
                    Excel.Workbook.Worksheets.Delete("Sheet1");

                    var exelworksheet = Excel.Workbook.Worksheets.Add("Application Tool Kit");
                    int cellRow = 1;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, gvNormalSDocbind.HeaderRow.Cells.Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    exelworksheet.Cells[cellRow, cellRow, cellRow, gvNormalSDocbind.HeaderRow.Cells.Count].Value = "Automation of Grinding Process Intelligence (AGI)";
                    exelworksheet.Cells[cellRow, cellRow, cellRow, gvNormalSDocbind.HeaderRow.Cells.Count].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, gvNormalSDocbind.HeaderRow.Cells.Count].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(231, 231, 231));
                    exelworksheet.Cells[cellRow, cellRow, cellRow, gvNormalSDocbind.HeaderRow.Cells.Count].Merge = true;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, gvNormalSDocbind.HeaderRow.Cells.Count].Style.Font.Size = 18;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, gvNormalSDocbind.HeaderRow.Cells.Count].Style.Font.Bold = true;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, gvNormalSDocbind.HeaderRow.Cells.Count].Style.Font.Color.SetColor(Color.Red);


                    cellRow = cellRow + 1;
                    exelworksheet.Cells[cellRow, 1, cellRow, 1].Value = "Username: " + Session["EmpName"].ToString();
                    exelworksheet.Cells[cellRow, 2, cellRow, 2].Style.Font.Bold = true;
                    exelworksheet.Cells[cellRow, 1, cellRow, 1].Style.Font.Bold = true;
                    exelworksheet.Cells[cellRow, 2, cellRow, 2].Value = "DateTime: " + DateTime.Now;


                    cellRow = cellRow + 2;

                    exelworksheet.Cells[cellRow, 1, cellRow, 10].Value = "Aplication Tool Kit";
                    exelworksheet.Cells[cellRow, 1, cellRow, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    exelworksheet.Cells[cellRow, 1, cellRow, 10].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(209, 242, 208));
                    exelworksheet.Cells[cellRow, 1, cellRow, 10].Merge = true;
                    exelworksheet.Cells[cellRow, 1, cellRow, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    exelworksheet.Cells[cellRow, 1, cellRow, 10].Style.Font.Size = 14;
                    exelworksheet.Cells[cellRow, 1, cellRow, 10].Style.Font.Bold = true;
                    exelworksheet.Cells[cellRow, 1, cellRow, 10].Style.Font.Color.SetColor(Color.FromArgb(78, 73, 73));

                    cellRow++;

                    for (int i = 0; i < gvNormalSDocbind.HeaderRow.Cells.Count; i++)
                    {
                        exelworksheet.Cells[cellRow, i + 1].Value = gvNormalSDocbind.HeaderRow.Cells[i].Text;
                        exelworksheet.Cells[cellRow, i + 1].Style.Font.Bold = true;
                    }

                    cellRow++;
                    int a = gvNormalSDocbind.PageIndex;
                    for (int i = 0; i < gvNormalSDocbind.PageCount; i++)
                    {
                        gvNormalSDocbind.SetPageIndex(i);
                        foreach (GridViewRow row in gvNormalSDocbind.Rows)
                        {
                            int j = 0;
                            while (j < row.Cells.Count)
                            {
                                string s = row.Cells[j].Text;
                                if (row.Cells[j].Text == "&nbsp;")
                                {
                                    exelworksheet.Cells[cellRow, j + 1].Value = "";
                                }
                                else
                                {
                                    exelworksheet.Cells[cellRow, j + 1].Value = row.Cells[j].Text;
                                }

                                j++;

                            }
                            cellRow++;
                        }
                    }
                    gvNormalSDocbind.SetPageIndex(a);

                    for (int i = 1; i <= gvNormalSDocbind.HeaderRow.Cells.Count; i++)
                    {
                        exelworksheet.Cells[3, i, cellRow, i].AutoFitColumns();
                    }






                    //Garph Section


                    var exelworksheet1 = Excel.Workbook.Worksheets.Add("Application Tool Kit Graphs");
                    int sheet2Cellrow = 3, sheet2column = 1;
                    exelworksheet1.Cells[1, 1, 1, 22].Value = "Application Tool Kit Graphs";
                    exelworksheet1.Cells[1, 1, 1, 22].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    exelworksheet1.Cells[1, 1, 1, 22].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    exelworksheet1.Cells[1, 1, 1, 22].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(209, 242, 208));
                    exelworksheet1.Cells[1, 1, 1, 22].Merge = true;
                    //exelworksheet1.Cells[1, 1, 1, 22].Style = true;
                    exelworksheet1.Cells[1, 1, 1, 22].Style.Font.Size = 14;
                    exelworksheet1.Cells[1, 1, 1, 22].Style.Font.Bold = true;
                    exelworksheet1.Cells[1, 1, 1, 22].Style.Font.Color.SetColor(Color.FromArgb(78, 73, 73));

                    exelworksheet1.Cells[sheet2Cellrow, 1].Value = "Item";
                    exelworksheet1.Cells[sheet2Cellrow, 1].Style.Font.Bold = true;
                    exelworksheet1.Cells[sheet2Cellrow, 2].Value = "Min";
                    exelworksheet1.Cells[sheet2Cellrow, 2].Style.Font.Bold = true;
                    exelworksheet1.Cells[sheet2Cellrow, 3].Value = "Max";
                    exelworksheet1.Cells[sheet2Cellrow, 3].Style.Font.Bold = true;
                    exelworksheet1.Cells[sheet2Cellrow, 4].Value = "Avg";
                    exelworksheet1.Cells[sheet2Cellrow, 4].Style.Font.Bold = true;




                    sheet2Cellrow++;
                    if (listminmaxavg.Count > 0 || listminmaxavg != null)
                    {
                        foreach (ADKParameterMinMaxAvg data in listminmaxavg)
                        {
                            exelworksheet1.Cells[sheet2Cellrow, 1].Value = data.Parameter;
                            if (data.Min == "")
                            {
                                exelworksheet1.Cells[sheet2Cellrow, 2].Value = data.Min;
                            }
                            else
                            {
                                exelworksheet1.Cells[sheet2Cellrow, 2].Value = Convert.ToDecimal(data.Min);
                            }
                            if (data.Max == "")
                            {
                                exelworksheet1.Cells[sheet2Cellrow, 3].Value = data.Max;
                            }
                            else
                            {
                                exelworksheet1.Cells[sheet2Cellrow, 3].Value = Convert.ToDecimal(data.Max);
                            }
                            if (data.Avg == "")
                            {
                                exelworksheet1.Cells[sheet2Cellrow, 4].Value = data.Avg;
                            }
                            else
                            {
                                exelworksheet1.Cells[sheet2Cellrow, 4].Value = Convert.ToDecimal(data.Avg);
                            }


                            sheet2Cellrow++;
                        }
                    }

                    //graph1
                    string statement = txtQuery.Text;
                    string statementLower = txtQuery.Text.ToLower();
                    string statementLower1 = txtQuery.Text.ToLower();
                    // string query = "";
                    StringBuilder query = new StringBuilder();
                    query.Append("");
                    string column = "";
                    List<string> listAndOr = new List<string>();
                    try
                    {
                        if (!statementLower.Contains("select ") || !statementLower.Contains(" from "))
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                            return;
                        }
                        if (statementLower.Contains("where"))
                        {
                            if (Regex.Split(statementLower, "where")[1] != "")
                            {
                                string q = Regex.Split(statementLower, "where")[1];
                                if (q.Contains("sdocname") || q.Contains("plungeid") || q.Contains("subcategoryid"))
                                {
                                    query.Append(q);
                                }
                                else
                                {

                                    string[] splitcondition = q.Split(new string[] { " and ", " or " }, StringSplitOptions.RemoveEmptyEntries);
                                    do
                                    {
                                        int indexofAnd = statementLower.IndexOf(" and ");
                                        int indexofOr = statementLower.IndexOf(" or ");
                                        if (indexofAnd == indexofOr)
                                        {
                                            break;
                                        }
                                        if (indexofOr == -1 && indexofAnd >= 0)
                                        {
                                            listAndOr.Add("and");
                                            statementLower = statementLower.Substring(indexofAnd + 3);
                                        }
                                        else if (indexofAnd == -1 && indexofOr >= 0)
                                        {
                                            listAndOr.Add("or");
                                            statementLower = statementLower.Substring(indexofOr + 2);
                                        }
                                        else
                                        if (indexofAnd < indexofOr)
                                        {
                                            listAndOr.Add("and");
                                            statementLower = statementLower.Substring(indexofAnd + 3);
                                        }
                                        else
                                        {
                                            listAndOr.Add("or");
                                            statementLower = statementLower.Substring(indexofOr + 2);
                                        }
                                    } while (statementLower.Length > 0);
                                    int indexAndOR = 0;
                                    for (int i = 0; i < splitcondition.Length; i++)
                                    {
                                        //string condition = splitcondition[i].Trim().Replace("", string.Empty);
                                        string param, value;
                                        string condition = Regex.Replace(splitcondition[i].ToString(), " ", string.Empty);
                                        condition = Regex.Replace(condition, "\n", string.Empty);
                                        if (Regex.IsMatch(condition, @".*in\(.*\)$"))
                                        {
                                            string[] list = Regex.Split(splitcondition[i].ToString(), "in()");
                                            param = list[0].Trim();
                                            //string v = list[2].Remove(list[2].Length - 1, 1);
                                            //string v1=v.Remove(0, 1);
                                            value = " in " + list[2];
                                        }
                                        else
                                        {
                                            //return;
                                            try
                                            {
                                                param = Regex.Split(splitcondition[i], "=")[0].Trim();
                                                value = "=" + Regex.Split(splitcondition[i], "=")[1];
                                            }
                                            catch (Exception es)
                                            {
                                                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                                                return;
                                            }
                                        }
                                        if (i == 0)
                                        {
                                            query.Append("(Parameter='" + param + "' And Value" + value + ") ");
                                        }
                                        else
                                        {
                                            query.Append(listAndOr[indexAndOR] + " (Parameter='" + param + "' And Value" + value + ") ");
                                            indexAndOR++;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            string splitvalue = Regex.Split(statementLower1, "systemdoctransaction")[1];
                            if (splitvalue != "")
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                        return;
                    }


                    List<ListItem> listItems = new List<ListItem>();
                    // listItems = DBAccess.getATKGraphValueFrequency(ddlGraphParam.SelectedItem==null? "": ddlGraphParam.SelectedItem.Text,query.ToString());
                    listItems = DBAccess.getATKGraphValueFrequency(txtGraphParams1.Text, Session["StatisticsConditions"] != null ? Session["StatisticsConditions"].ToString() : "");
                    int sheet2GraphValueColumn = 6, count = 0;
                    if (listItems.Count >= 0)
                    {
                        foreach (ListItem item in listItems)
                        {
                            //if (item.Value != "")
                            //{
                            //    exelworksheet1.Cells[53, sheet2GraphValueColumn].Value = Convert.ToDecimal(item.Value);
                            //}
                            //else
                            //{
                            exelworksheet1.Cells[53, sheet2GraphValueColumn].Value = item.Value;
                            //}
                            if (item.Text != "")
                            {
                                exelworksheet1.Cells[54, sheet2GraphValueColumn].Value = Convert.ToDecimal(item.Text);
                            }
                            else
                            {
                                exelworksheet1.Cells[54, sheet2GraphValueColumn].Value = item.Text;
                            }

                            sheet2GraphValueColumn++;
                            count++;
                        }

                        ExcelChart chart = exelworksheet1.Drawings.AddChart("chart", eChartType.ColumnClustered);
                        ExcelChartSerie serie = chart.Series.Add(exelworksheet1.Cells[54, 6, 54, sheet2GraphValueColumn - 1], exelworksheet1.Cells[53, 6, 53, sheet2GraphValueColumn - 1]);
                        //exelworksheet1.Cells[53, 6, 53, sheet2GraphValueColumn - 1].Style.Hidden = true;
                        //exelworksheet1.Cells[54, 6, 54, sheet2GraphValueColumn - 1].Style.Hidden = true;
                        chart.SetPosition(2, 5, 5, 5);
                        chart.SetSize(500, 300);
                        chart.Title.Text =  txtGraphParams1.Text;
                        chart.Title.Font.Color = Color.FromArgb(89, 89, 89);
                        chart.Title.Font.Size = 15;
                        chart.Title.Font.Bold = true;
                        chart.Style = eChartStyle.Style15;
                        if (listItems.Count > 0)
                        {
                            exelworksheet1.Cells[54, 6, 54, sheet2GraphValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                            exelworksheet1.Cells[53, 6, 53, sheet2GraphValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                        }

                    }




                    //chart 2 
                    listItems = new List<ListItem>();
                    //  listItems = DBAccess.getATKGraphValueFrequency(ddlGraphParam2.SelectedItem==null?"": ddlGraphParam2.SelectedItem.Text, query.ToString());
                    listItems = DBAccess.getATKGraphValueFrequency(txtGraphParams2.Text, Session["StatisticsConditions"] != null ? Session["StatisticsConditions"].ToString() : "");
                    int sheet2Graph2ValueColumn = 6;

                    if (listItems.Count >= 0)
                    {
                        foreach (ListItem item in listItems)
                        {

                            exelworksheet1.Cells[56, sheet2Graph2ValueColumn].Value = item.Value;
                            if (item.Text != "")
                            {
                                exelworksheet1.Cells[57, sheet2Graph2ValueColumn].Value = Convert.ToDecimal(item.Text);
                            }
                            else
                            {
                                exelworksheet1.Cells[57, sheet2Graph2ValueColumn].Value = item.Text;
                            }

                            sheet2Graph2ValueColumn++;

                        }
                        ExcelChart chart2 = exelworksheet1.Drawings.AddChart("chart2", eChartType.ColumnClustered);
                        ExcelChartSerie serie2 = chart2.Series.Add(exelworksheet1.Cells[57, 6, 57, sheet2Graph2ValueColumn - 1], exelworksheet1.Cells[56, 6, 56, sheet2Graph2ValueColumn - 1]);
                        chart2.SetPosition(2, 5, 14, 5);
                        chart2.SetSize(500, 300);
                        chart2.Title.Text = txtGraphParams2.Text == null ? "" : txtGraphParams2.Text;
                        chart2.Title.Font.Color = Color.FromArgb(89, 89, 89);
                        chart2.Title.Font.Size = 15;
                        chart2.Title.Font.Bold = true;
                        chart2.Style = eChartStyle.Style15;
                        if (listItems.Count > 0)
                        {
                            exelworksheet1.Cells[57, 6, 57, sheet2Graph2ValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                            exelworksheet1.Cells[56, 6, 56, sheet2Graph2ValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                        }

                    }




                    //chart 3 
                    listItems = new List<ListItem>();
                    // listItems = DBAccess.getATKGraphValueFrequency(ddlGraphParam3.SelectedItem==null?"": ddlGraphParam3.SelectedItem.Text, query.ToString());
                    listItems = DBAccess.getATKGraphValueFrequency(txtGraphParams3.Text, Session["StatisticsConditions"] != null ? Session["StatisticsConditions"].ToString() : "");
                    int sheet2Graph3ValueColumn = 6;

                    if (listItems.Count >= 0)
                    {
                        foreach (ListItem item in listItems)
                        {

                            exelworksheet1.Cells[59, sheet2Graph3ValueColumn].Value = item.Value;
                            if (item.Text != "")
                            {
                                exelworksheet1.Cells[60, sheet2Graph3ValueColumn].Value = Convert.ToDecimal(item.Text);
                            }
                            else
                            {
                                exelworksheet1.Cells[60, sheet2Graph3ValueColumn].Value = item.Text;
                            }

                            sheet2Graph3ValueColumn++;

                        }
                        ExcelChart chart3 = exelworksheet1.Drawings.AddChart("chart3", eChartType.ColumnClustered);
                        ExcelChartSerie serie3 = chart3.Series.Add(exelworksheet1.Cells[60, 6, 60, sheet2Graph3ValueColumn - 1], exelworksheet1.Cells[59, 6, 59, sheet2Graph3ValueColumn - 1]);
                        chart3.SetPosition(18, 5, 5, 5);
                        chart3.SetSize(500, 300);
                        chart3.Title.Text = txtGraphParams3.Text;
                        chart3.Title.Font.Color = Color.FromArgb(89, 89, 89);
                        chart3.Title.Font.Size = 15;
                        chart3.Title.Font.Bold = true;
                        chart3.Style = eChartStyle.Style15;
                        if (listItems.Count > 0)
                        {
                            exelworksheet1.Cells[60, 6, 60, sheet2Graph3ValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                            exelworksheet1.Cells[59, 6, 59, sheet2Graph3ValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                        }

                    }


                    //chart 4 
                    listItems = new List<ListItem>();
                    //  listItems = DBAccess.getATKGraphValueFrequency(ddlGraphParam4.SelectedItem==null?"": ddlGraphParam4.SelectedItem.Text, query.ToString());
                    listItems = DBAccess.getATKGraphValueFrequency(txtGraphParams4.Text, Session["StatisticsConditions"] != null ? Session["StatisticsConditions"].ToString() : "");
                    int sheet2Graph4ValueColumn = 6;

                    if (listItems.Count >= 0)
                    {
                        foreach (ListItem item in listItems)
                        {

                            exelworksheet1.Cells[62, sheet2Graph4ValueColumn].Value = item.Value;
                            if (item.Text != "")
                            {
                                exelworksheet1.Cells[63, sheet2Graph4ValueColumn].Value = Convert.ToDecimal(item.Text);
                            }
                            else
                            {
                                exelworksheet1.Cells[63, sheet2Graph4ValueColumn].Value = item.Text;
                            }

                            sheet2Graph4ValueColumn++;

                        }
                        ExcelChart chart4 = exelworksheet1.Drawings.AddChart("chart4", eChartType.ColumnClustered);
                        ExcelChartSerie serie4 = chart4.Series.Add(exelworksheet1.Cells[63, 6, 63, sheet2Graph4ValueColumn - 1], exelworksheet1.Cells[62, 6, 62, sheet2Graph4ValueColumn - 1]);
                        chart4.SetPosition(18, 5, 14, 5);
                        chart4.SetSize(500, 300);
                        chart4.Title.Text = txtGraphParams4.Text;
                        chart4.Title.Font.Color = Color.FromArgb(89, 89, 89);
                        chart4.Title.Font.Size = 15;
                        chart4.Title.Font.Bold = true;
                        chart4.Style = eChartStyle.Style15;
                        if (listItems.Count > 0)
                        {
                            exelworksheet1.Cells[63, 6, 63, sheet2Graph4ValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                            exelworksheet1.Cells[62, 6, 62, sheet2Graph4ValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                        }

                    }


                    //chart 5 
                    listItems = new List<ListItem>();
                    //   listItems = DBAccess.getATKGraphValueFrequency(ddlGraphParam5.SelectedItem==null?"": ddlGraphParam5.SelectedItem.Text, query.ToString());
                    listItems = DBAccess.getATKGraphValueFrequency(txtGraphParams5.Text, Session["StatisticsConditions"] != null ? Session["StatisticsConditions"].ToString() : "");
                    int sheet2Graph5ValueColumn = 6;

                    if (listItems.Count >= 0)
                    {
                        foreach (ListItem item in listItems)
                        {

                            exelworksheet1.Cells[65, sheet2Graph5ValueColumn].Value = item.Value;
                            if (item.Text != "")
                            {
                                exelworksheet1.Cells[66, sheet2Graph5ValueColumn].Value = Convert.ToDecimal(item.Text);
                            }
                            else
                            {
                                exelworksheet1.Cells[66, sheet2Graph5ValueColumn].Value = item.Text;
                            }

                            sheet2Graph5ValueColumn++;

                        }
                        ExcelChart chart5 = exelworksheet1.Drawings.AddChart("chart5", eChartType.ColumnClustered);
                        ExcelChartSerie serie5 = chart5.Series.Add(exelworksheet1.Cells[66, 6, 66, sheet2Graph5ValueColumn - 1], exelworksheet1.Cells[65, 6, 65, sheet2Graph5ValueColumn - 1]);
                        chart5.SetPosition(34, 5, 5, 5);
                        chart5.SetSize(500, 300);
                        chart5.Title.Text = txtGraphParams5.Text;
                        chart5.Title.Font.Color = Color.FromArgb(89, 89, 89);
                        chart5.Title.Font.Size = 15;
                        chart5.Title.Font.Bold = true;
                        chart5.Style = eChartStyle.Style15;
                        if (listItems.Count > 0)
                        {
                            exelworksheet1.Cells[66, 6, 66, sheet2Graph5ValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                            exelworksheet1.Cells[65, 6, 65, sheet2Graph5ValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                        }
                    }




                    DownloadFile(destination, Excel.GetAsByteArray());
                }
            }
            else
            {

                if (gvDisplayData.Rows.Count > 0)
                {
                    try
                    {
                        FileInfo newFile = new FileInfo(Source);
                        ExcelPackage Excel = new ExcelPackage(newFile, true);
                        Excel.Workbook.Worksheets.Delete("Sheet1");

                        var exelworksheet = Excel.Workbook.Worksheets.Add("Application Tool Kit");
                        int cellRow = 1;
                        exelworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                        exelworksheet.Cells[cellRow, cellRow, cellRow, 10].Value = "Automation of Grinding Process Intelligence (AGI)";
                        exelworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        exelworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(231, 231, 231));
                        exelworksheet.Cells[cellRow, cellRow, cellRow, 10].Merge = true;
                        exelworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.Font.Size = 18;
                        exelworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.Font.Bold = true;
                        exelworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.Font.Color.SetColor(Color.Red);


                        cellRow = cellRow + 1;
                        exelworksheet.Cells[cellRow, 1, cellRow, 1].Value = "Username: " + Session["EmpName"].ToString();
                        exelworksheet.Cells[cellRow, 2, cellRow, 2].Style.Font.Bold = true;
                        exelworksheet.Cells[cellRow, 1, cellRow, 1].Style.Font.Bold = true;
                        exelworksheet.Cells[cellRow, 2, cellRow, 2].Value = "DateTime: " + DateTime.Now;


                        cellRow = cellRow + 2;

                        exelworksheet.Cells[cellRow, 1, cellRow, 10].Value = "Aplication Tool Kit";
                        exelworksheet.Cells[cellRow, 1, cellRow, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        exelworksheet.Cells[cellRow, 1, cellRow, 10].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(209, 242, 208));
                        exelworksheet.Cells[cellRow, 1, cellRow, 10].Merge = true;
                        exelworksheet.Cells[cellRow, 1, cellRow, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        exelworksheet.Cells[cellRow, 1, cellRow, 10].Style.Font.Size = 14;
                        exelworksheet.Cells[cellRow, 1, cellRow, 10].Style.Font.Bold = true;
                        exelworksheet.Cells[cellRow, 1, cellRow, 10].Style.Font.Color.SetColor(Color.FromArgb(78, 73, 73));

                        cellRow++;

                        GridView Gridview1=null;
                        for (int k=0;k<gvDisplayData.Rows.Count;k++)
                        {
                            if( ((GridView)gvDisplayData.Rows[k].FindControl("gvnested")).HeaderRow != null)
                            {
                                Gridview1 = (GridView)gvDisplayData.Rows[k].FindControl("gvnested");
                                break;
                            }
                        }

                       
                        for (int i = 0; i < Gridview1.HeaderRow.Cells.Count; i++)
                        {
                            exelworksheet.Cells[cellRow, i + 1].Value = Gridview1.HeaderRow.Cells[i].Text;
                            exelworksheet.Cells[cellRow, i + 1].Style.Font.Bold = true;
                        }

                        cellRow++;
                        int a = gvDisplayData.PageIndex;
                        for (int i = 0; i < gvDisplayData.PageCount; i++)
                        {
                            gvDisplayData.SetPageIndex(i);
                            foreach (GridViewRow row in gvDisplayData.Rows)
                            {
                                GridView nestedGrid = (GridView)row.FindControl("gvnested");
                                foreach (GridViewRow rowData in nestedGrid.Rows)
                                {
                                    int j = 0;
                                    while (j < rowData.Cells.Count)
                                    {
                                        string s = rowData.Cells[j].Text;
                                        if (rowData.Cells[j].Text == "&nbsp;")
                                        {
                                            exelworksheet.Cells[cellRow, j + 1].Value = "";
                                        }
                                        else
                                        {
                                            exelworksheet.Cells[cellRow, j + 1].Value = rowData.Cells[j].Text;
                                        }
                                        j++;
                                    }
                                    cellRow++;
                                }
                            }
                        }
                        gvDisplayData.SetPageIndex(a);

                        for (int i = 1; i <= Gridview1.HeaderRow.Cells.Count; i++)
                        {
                            exelworksheet.Cells[3, i, cellRow, i].AutoFitColumns();
                        }

                        #region ----- //Garph Section


                        var exelworksheet1 = Excel.Workbook.Worksheets.Add("Application Tool Kit Graphs");
                        int sheet2Cellrow = 3, sheet2column = 1;
                        exelworksheet1.Cells[1, 1, 1, 22].Value = "Application Tool Kit Graphs";
                        exelworksheet1.Cells[1, 1, 1, 22].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        exelworksheet1.Cells[1, 1, 1, 22].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        exelworksheet1.Cells[1, 1, 1, 22].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(209, 242, 208));
                        exelworksheet1.Cells[1, 1, 1, 22].Merge = true;
                        //exelworksheet1.Cells[1, 1, 1, 22].Style = true;
                        exelworksheet1.Cells[1, 1, 1, 22].Style.Font.Size = 14;
                        exelworksheet1.Cells[1, 1, 1, 22].Style.Font.Bold = true;
                        exelworksheet1.Cells[1, 1, 1, 22].Style.Font.Color.SetColor(Color.FromArgb(78, 73, 73));

                        exelworksheet1.Cells[sheet2Cellrow, 1].Value = "Item";
                        exelworksheet1.Cells[sheet2Cellrow, 1].Style.Font.Bold = true;
                        exelworksheet1.Cells[sheet2Cellrow, 2].Value = "Min";
                        exelworksheet1.Cells[sheet2Cellrow, 2].Style.Font.Bold = true;
                        exelworksheet1.Cells[sheet2Cellrow, 3].Value = "Max";
                        exelworksheet1.Cells[sheet2Cellrow, 3].Style.Font.Bold = true;
                        exelworksheet1.Cells[sheet2Cellrow, 4].Value = "Avg";
                        exelworksheet1.Cells[sheet2Cellrow, 4].Style.Font.Bold = true;




                        sheet2Cellrow++;
                        if (listminmaxavg.Count > 0 || listminmaxavg != null)
                        {
                            foreach (ADKParameterMinMaxAvg data in listminmaxavg)
                            {
                                exelworksheet1.Cells[sheet2Cellrow, 1].Value = data.Parameter;
                                if (data.Min == "")
                                {
                                    exelworksheet1.Cells[sheet2Cellrow, 2].Value = data.Min;
                                }
                                else
                                {
                                    exelworksheet1.Cells[sheet2Cellrow, 2].Value = Convert.ToDecimal(data.Min);
                                }
                                if (data.Max == "")
                                {
                                    exelworksheet1.Cells[sheet2Cellrow, 3].Value = data.Max;
                                }
                                else
                                {
                                    exelworksheet1.Cells[sheet2Cellrow, 3].Value = Convert.ToDecimal(data.Max);
                                }
                                if (data.Avg == "")
                                {
                                    exelworksheet1.Cells[sheet2Cellrow, 4].Value = data.Avg;
                                }
                                else
                                {
                                    exelworksheet1.Cells[sheet2Cellrow, 4].Value = Convert.ToDecimal(data.Avg);
                                }


                                sheet2Cellrow++;
                            }
                        }

                        //graph1
                        string statement = txtQuery.Text;
                        string statementLower = txtQuery.Text.ToLower();
                        string statementLower1 = txtQuery.Text.ToLower();
                        // string query = "";
                        StringBuilder query = new StringBuilder();
                        query.Append("");
                        string column = "";
                        List<string> listAndOr = new List<string>();
                        try
                        {
                            if (!statementLower.Contains("select ") || !statementLower.Contains(" from "))
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                                return;
                            }
                            if (statementLower.Contains("where"))
                            {
                                if (Regex.Split(statementLower, "where")[1] != "")
                                {
                                    string q = Regex.Split(statementLower, "where")[1];
                                    if (q.Contains("sdocname") || q.Contains("plungeid") || q.Contains("subcategoryid"))
                                    {
                                        query.Append(q);
                                    }
                                    else
                                    {

                                        string[] splitcondition = q.Split(new string[] { " and ", " or " }, StringSplitOptions.RemoveEmptyEntries);
                                        do
                                        {
                                            int indexofAnd = statementLower.IndexOf(" and ");
                                            int indexofOr = statementLower.IndexOf(" or ");
                                            if (indexofAnd == indexofOr)
                                            {
                                                break;
                                            }
                                            if (indexofOr == -1 && indexofAnd >= 0)
                                            {
                                                listAndOr.Add("and");
                                                statementLower = statementLower.Substring(indexofAnd + 3);
                                            }
                                            else if (indexofAnd == -1 && indexofOr >= 0)
                                            {
                                                listAndOr.Add("or");
                                                statementLower = statementLower.Substring(indexofOr + 2);
                                            }
                                            else
                                            if (indexofAnd < indexofOr)
                                            {
                                                listAndOr.Add("and");
                                                statementLower = statementLower.Substring(indexofAnd + 3);
                                            }
                                            else
                                            {
                                                listAndOr.Add("or");
                                                statementLower = statementLower.Substring(indexofOr + 2);
                                            }
                                        } while (statementLower.Length > 0);
                                        int indexAndOR = 0;
                                        for (int i = 0; i < splitcondition.Length; i++)
                                        {
                                            //string condition = splitcondition[i].Trim().Replace("", string.Empty);
                                            string param, value;
                                            string condition = Regex.Replace(splitcondition[i].ToString(), " ", string.Empty);
                                            condition = Regex.Replace(condition, "\n", string.Empty);
                                            if (Regex.IsMatch(condition, @".*in\(.*\)$"))
                                            {
                                                string[] list = Regex.Split(splitcondition[i].ToString(), "in()");
                                                param = list[0].Trim();
                                                //string v = list[2].Remove(list[2].Length - 1, 1);
                                                //string v1=v.Remove(0, 1);
                                                value = " in " + list[2];
                                            }
                                            else
                                            {
                                                //return;
                                                try
                                                {
                                                    param = Regex.Split(splitcondition[i], "=")[0].Trim();
                                                    value = "=" + Regex.Split(splitcondition[i], "=")[1];
                                                }
                                                catch (Exception es)
                                                {
                                                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                                                    return;
                                                }
                                            }
                                            if (i == 0)
                                            {
                                                query.Append("(Parameter='" + param + "' And Value" + value + ") ");
                                            }
                                            else
                                            {
                                                query.Append(listAndOr[indexAndOR] + " (Parameter='" + param + "' And Value" + value + ") ");
                                                indexAndOR++;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                string splitvalue = Regex.Split(statementLower1, "systemdoctransaction")[1];
                                if (splitvalue != "")
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                                    return;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                            return;
                        }


                        List<ListItem> listItems = new List<ListItem>();
                        // listItems = DBAccess.getATKGraphValueFrequency(ddlGraphParam.SelectedItem==null? "": ddlGraphParam.SelectedItem.Text,query.ToString());
                        listItems = DBAccess.getATKGraphValueFrequency(txtGraphParams1.Text, Session["StatisticsConditions"] != null ? Session["StatisticsConditions"].ToString() : "");
                        int sheet2GraphValueColumn = 6, count = 0;
                        if (listItems.Count >= 0)
                        {
                            foreach (ListItem item in listItems)
                            {
                                //if (item.Value != "")
                                //{
                                //    exelworksheet1.Cells[53, sheet2GraphValueColumn].Value = Convert.ToDecimal(item.Value);
                                //}
                                //else
                                //{
                                exelworksheet1.Cells[53, sheet2GraphValueColumn].Value = item.Value;
                                //}
                                if (item.Text != "")
                                {
                                    exelworksheet1.Cells[54, sheet2GraphValueColumn].Value = Convert.ToDecimal(item.Text);
                                }
                                else
                                {
                                    exelworksheet1.Cells[54, sheet2GraphValueColumn].Value = item.Text;
                                }

                                sheet2GraphValueColumn++;
                                count++;
                            }

                            ExcelChart chart = exelworksheet1.Drawings.AddChart("chart", eChartType.ColumnClustered);
                            ExcelChartSerie serie = chart.Series.Add(exelworksheet1.Cells[54, 6, 54, sheet2GraphValueColumn - 1], exelworksheet1.Cells[53, 6, 53, sheet2GraphValueColumn - 1]);
                            //exelworksheet1.Cells[53, 6, 53, sheet2GraphValueColumn - 1].Style.Hidden = true;
                            //exelworksheet1.Cells[54, 6, 54, sheet2GraphValueColumn - 1].Style.Hidden = true;
                            chart.SetPosition(2, 5, 5, 5);
                            chart.SetSize(500, 300);
                            chart.Title.Text = txtGraphParams1.Text;
                            chart.Title.Font.Color = Color.FromArgb(89, 89, 89);
                            chart.Title.Font.Size = 15;
                            chart.Title.Font.Bold = true;
                            chart.Style = eChartStyle.Style15;
                            if (listItems.Count > 0)
                            {
                                exelworksheet1.Cells[54, 6, 54, sheet2GraphValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                                exelworksheet1.Cells[53, 6, 53, sheet2GraphValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                            }

                        }




                        //chart 2 
                        listItems = new List<ListItem>();
                        //  listItems = DBAccess.getATKGraphValueFrequency(ddlGraphParam2.SelectedItem==null?"": ddlGraphParam2.SelectedItem.Text, query.ToString());
                        listItems = DBAccess.getATKGraphValueFrequency(txtGraphParams2.Text, Session["StatisticsConditions"] != null ? Session["StatisticsConditions"].ToString() : "");
                        int sheet2Graph2ValueColumn = 6;

                        if (listItems.Count >= 0)
                        {
                            foreach (ListItem item in listItems)
                            {

                                exelworksheet1.Cells[56, sheet2Graph2ValueColumn].Value = item.Value;
                                if (item.Text != "")
                                {
                                    exelworksheet1.Cells[57, sheet2Graph2ValueColumn].Value = Convert.ToDecimal(item.Text);
                                }
                                else
                                {
                                    exelworksheet1.Cells[57, sheet2Graph2ValueColumn].Value = item.Text;
                                }

                                sheet2Graph2ValueColumn++;

                            }
                            ExcelChart chart2 = exelworksheet1.Drawings.AddChart("chart2", eChartType.ColumnClustered);
                            ExcelChartSerie serie2 = chart2.Series.Add(exelworksheet1.Cells[57, 6, 57, sheet2Graph2ValueColumn - 1], exelworksheet1.Cells[56, 6, 56, sheet2Graph2ValueColumn - 1]);
                            chart2.SetPosition(2, 5, 14, 5);
                            chart2.SetSize(500, 300);
                            chart2.Title.Text = txtGraphParams2.Text;
                            chart2.Title.Font.Color = Color.FromArgb(89, 89, 89);
                            chart2.Title.Font.Size = 15;
                            chart2.Title.Font.Bold = true;
                            chart2.Style = eChartStyle.Style15;
                            if (listItems.Count > 0)
                            {
                                exelworksheet1.Cells[57, 6, 57, sheet2Graph2ValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                                exelworksheet1.Cells[56, 6, 56, sheet2Graph2ValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                            }

                        }




                        //chart 3 
                        listItems = new List<ListItem>();
                        // listItems = DBAccess.getATKGraphValueFrequency(ddlGraphParam3.SelectedItem==null?"": ddlGraphParam3.SelectedItem.Text, query.ToString());
                        listItems = DBAccess.getATKGraphValueFrequency(txtGraphParams3.Text, Session["StatisticsConditions"] != null ? Session["StatisticsConditions"].ToString() : "");
                        int sheet2Graph3ValueColumn = 6;

                        if (listItems.Count >= 0)
                        {
                            foreach (ListItem item in listItems)
                            {

                                exelworksheet1.Cells[59, sheet2Graph3ValueColumn].Value = item.Value;
                                if (item.Text != "")
                                {
                                    exelworksheet1.Cells[60, sheet2Graph3ValueColumn].Value = Convert.ToDecimal(item.Text);
                                }
                                else
                                {
                                    exelworksheet1.Cells[60, sheet2Graph3ValueColumn].Value = item.Text;
                                }

                                sheet2Graph3ValueColumn++;

                            }
                            ExcelChart chart3 = exelworksheet1.Drawings.AddChart("chart3", eChartType.ColumnClustered);
                            ExcelChartSerie serie3 = chart3.Series.Add(exelworksheet1.Cells[60, 6, 60, sheet2Graph3ValueColumn - 1], exelworksheet1.Cells[59, 6, 59, sheet2Graph3ValueColumn - 1]);
                            chart3.SetPosition(18, 5, 5, 5);
                            chart3.SetSize(500, 300);
                            chart3.Title.Text = txtGraphParams3.Text;
                            chart3.Title.Font.Color = Color.FromArgb(89, 89, 89);
                            chart3.Title.Font.Size = 15;
                            chart3.Title.Font.Bold = true;
                            chart3.Style = eChartStyle.Style15;
                            if (listItems.Count > 0)
                            {
                                exelworksheet1.Cells[60, 6, 60, sheet2Graph3ValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                                exelworksheet1.Cells[59, 6, 59, sheet2Graph3ValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                            }

                        }


                        //chart 4 
                        listItems = new List<ListItem>();
                        //  listItems = DBAccess.getATKGraphValueFrequency(ddlGraphParam4.SelectedItem==null?"": ddlGraphParam4.SelectedItem.Text, query.ToString());
                        listItems = DBAccess.getATKGraphValueFrequency(txtGraphParams4.Text, Session["StatisticsConditions"] != null ? Session["StatisticsConditions"].ToString() : "");
                        int sheet2Graph4ValueColumn = 6;

                        if (listItems.Count >= 0)
                        {
                            foreach (ListItem item in listItems)
                            {

                                exelworksheet1.Cells[62, sheet2Graph4ValueColumn].Value = item.Value;
                                if (item.Text != "")
                                {
                                    exelworksheet1.Cells[63, sheet2Graph4ValueColumn].Value = Convert.ToDecimal(item.Text);
                                }
                                else
                                {
                                    exelworksheet1.Cells[63, sheet2Graph4ValueColumn].Value = item.Text;
                                }

                                sheet2Graph4ValueColumn++;

                            }
                            ExcelChart chart4 = exelworksheet1.Drawings.AddChart("chart4", eChartType.ColumnClustered);
                            ExcelChartSerie serie4 = chart4.Series.Add(exelworksheet1.Cells[63, 6, 63, sheet2Graph4ValueColumn - 1], exelworksheet1.Cells[62, 6, 62, sheet2Graph4ValueColumn - 1]);
                            chart4.SetPosition(18, 5, 14, 5);
                            chart4.SetSize(500, 300);
                            chart4.Title.Text = txtGraphParams4.Text;
                            chart4.Title.Font.Color = Color.FromArgb(89, 89, 89);
                            chart4.Title.Font.Size = 15;
                            chart4.Title.Font.Bold = true;
                            chart4.Style = eChartStyle.Style15;
                            if (listItems.Count > 0)
                            {
                                exelworksheet1.Cells[63, 6, 63, sheet2Graph4ValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                                exelworksheet1.Cells[62, 6, 62, sheet2Graph4ValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                            }

                        }


                        //chart 5 
                        listItems = new List<ListItem>();
                        //   listItems = DBAccess.getATKGraphValueFrequency(ddlGraphParam5.SelectedItem==null?"": ddlGraphParam5.SelectedItem.Text, query.ToString());
                        listItems = DBAccess.getATKGraphValueFrequency(txtGraphParams5.Text, Session["StatisticsConditions"] != null ? Session["StatisticsConditions"].ToString() : "");
                        int sheet2Graph5ValueColumn = 6;

                        if (listItems.Count >= 0)
                        {
                            foreach (ListItem item in listItems)
                            {

                                exelworksheet1.Cells[65, sheet2Graph5ValueColumn].Value = item.Value;
                                if (item.Text != "")
                                {
                                    exelworksheet1.Cells[66, sheet2Graph5ValueColumn].Value = Convert.ToDecimal(item.Text);
                                }
                                else
                                {
                                    exelworksheet1.Cells[66, sheet2Graph5ValueColumn].Value = item.Text;
                                }

                                sheet2Graph5ValueColumn++;

                            }
                            ExcelChart chart5 = exelworksheet1.Drawings.AddChart("chart5", eChartType.ColumnClustered);
                            ExcelChartSerie serie5 = chart5.Series.Add(exelworksheet1.Cells[66, 6, 66, sheet2Graph5ValueColumn - 1], exelworksheet1.Cells[65, 6, 65, sheet2Graph5ValueColumn - 1]);
                            chart5.SetPosition(34, 5, 5, 5);
                            chart5.SetSize(500, 300);
                            chart5.Title.Text = txtGraphParams5.Text;
                            chart5.Title.Font.Color = Color.FromArgb(89, 89, 89);
                            chart5.Title.Font.Size = 15;
                            chart5.Title.Font.Bold = true;
                            chart5.Style = eChartStyle.Style15;
                            if (listItems.Count > 0)
                            {
                                exelworksheet1.Cells[66, 6, 66, sheet2Graph5ValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                                exelworksheet1.Cells[65, 6, 65, sheet2Graph5ValueColumn - 1].Style.Font.Color.SetColor(Color.White);
                            }
                        }

                        #endregion

                        DownloadFile(destination, Excel.GetAsByteArray());
                    }
                    catch (Exception ex)
                    {

                    }
                }
                
            }
        }
        static string appPath = HttpContext.Current.Server.MapPath("");
        public static string GetReportPath(string reportName)
        {
            string src;

            if (HttpContext.Current.Session["Language"] == null)
                src = Path.Combine(appPath, "ReportTemplate", reportName);
            else
            {
                if (HttpContext.Current.Session["Language"].ToString() != "en")
                    src = Path.Combine(appPath, "ReportTemplate-" + HttpContext.Current.Session["Language"].ToString() + "", reportName);
                else
                    src = Path.Combine(appPath, "ReportTemplate", reportName);
            }

            return src;
        }

        public static string SafeFileName(string name)
        {
            StringBuilder str = new StringBuilder(name);

            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                str = str.Replace(c, '_');
            }

            return str.ToString();
        }

        private static void DownloadFile(string filename, byte[] bytearray)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + Path.GetFileName(filename) + "\"");
            HttpContext.Current.Response.OutputStream.Write(bytearray, 0, bytearray.Length);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.SuppressContent = true;
            HttpContext.Current.ApplicationInstance.CompleteRequest();

        }

        protected void statistics_Click(object sender, EventArgs e)
        {
            Session["InputModule"] = null;
            if (txtQuery.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Enter query.');", true);
            }
            string statement = txtQuery.Text;
            string statementLower = txtQuery.Text.ToLower();
            string statementLower1 = txtQuery.Text.ToLower();
            // string query = "";
            StringBuilder query = new StringBuilder();
            query.Append("");
            StringBuilder column = new StringBuilder();
            column.Append("");
            List<string> listAndOr = new List<string>();
            try
            {
                if (!statementLower.Contains("select ") || !statementLower.Contains(" from "))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                    return;
                }
                string[] starsplit;
                if (statementLower.Contains("*"))
                {
                    starsplit = statementLower.Split('*');
                    if (starsplit[0].Contains("select") && starsplit[1].Contains("from"))
                    {
                        column.Append("");
                    }
                }

                if (statementLower.Contains("select") && statementLower.Contains("from"))
                {
                    string split1 = Regex.Split(statement, "select")[1];
                    string customColumn = Regex.Split(split1, "from")[0].Trim();
                    string[] splitColumn = Regex.Split(customColumn, ",");
                    List<CustomColumn> listdata = DBAccess.getATKTabParameter("");
                    for (int i = 0; i < splitColumn.Length; i++)
                    {

                        foreach (CustomColumn data in listdata)
                        {
                            if (splitColumn[i].Trim() == data.CustomName)
                            {
                                if (i == splitColumn.Length - 1)
                                {
                                    column.Append(data.ColumnName);
                                }
                                else
                                {
                                    column.Append(data.ColumnName + ",");
                                }
                            }
                        }
                    }
                }
                if (statementLower.Contains("where"))
                {
                    if (Regex.Split(statementLower, "where")[1] != "")
                    {
                        string q = Regex.Split(statementLower, "where")[1];

                        if (q.Contains("sdocname") || q.Contains("plungeid") || q.Contains("subcategoryid"))
                        {
                            query.Append(q);
                        }
                        else
                        {
                            string ss = q;
                            //Regex r = new Regex(@"'(.+?)'|in\(.*\)$");
                            Regex r = new Regex(@"'(.+?)'|in[ ]{0,}\(.+?\)");
                            MatchCollection mc = r.Matches(ss);
                            int len = mc.Count;
                            string replacestring = ss;
                            int jj = 0;
                            List<ListItem> listofvalues = new List<ListItem>();
                            foreach (Match data in mc)
                            {
                                if (ss.Contains(data.Value))
                                {

                                    //  replacestring = replacestring.Replace(data.Value, "r" + jj);
                                    replacestring = ReplaceFirst(replacestring, data.Value, "r" + jj);
                                    listofvalues.Add(new ListItem("r" + jj, data.Value));
                                    jj++;
                                }
                            }
                            string finalstr = replacestring;
                            statementLower = finalstr;
                            // var arra = finalstr.Split(new string[] { " and ", " or " }, StringSplitOptions.RemoveEmptyEntries);
                            string[] splitcondition = finalstr.Split(new string[] { " and ", " or " }, StringSplitOptions.RemoveEmptyEntries);
                            do
                            {
                                int indexofAnd = statementLower.IndexOf(" and ");
                                int indexofOr = statementLower.IndexOf(" or ");
                                if (indexofAnd == indexofOr)
                                {
                                    break;
                                }
                                if (indexofOr == -1 && indexofAnd >= 0)
                                {
                                    listAndOr.Add("and");
                                    statementLower = statementLower.Substring(indexofAnd + 3);
                                }
                                else if (indexofAnd == -1 && indexofOr >= 0)
                                {
                                    listAndOr.Add("or");
                                    statementLower = statementLower.Substring(indexofOr + 2);
                                }
                                else
                                if (indexofAnd < indexofOr)
                                {
                                    listAndOr.Add("and");
                                    statementLower = statementLower.Substring(indexofAnd + 3);
                                }
                                else
                                {
                                    listAndOr.Add("or");
                                    statementLower = statementLower.Substring(indexofOr + 2);
                                }
                            } while (statementLower.Length > 0);
                            int indexAndOR = 0;
                            for (int i = 0; i < splitcondition.Length; i++)
                            {
                                //string condition = splitcondition[i].Trim().Replace("", string.Empty);
                                string param, value;
                                //string condition = Regex.Replace(splitcondition[i].ToString(), " ", string.Empty);
                                string condition = splitcondition[i].ToString().Trim();
                                //condition = Regex.Replace(condition, "\n", string.Empty);
                                if (!condition.Contains("="))
                                {
                                    string incondition = condition.Trim();

                                    value = incondition.Split(' ').Last().Trim();
                                    param = incondition.Remove(incondition.Length - value.Length).Trim();
                                    foreach (ListItem data in listofvalues)
                                    {
                                        if (value == data.Text)
                                        {
                                            value = data.Value;
                                        }
                                    }
                                    value = " " + value;
                                }
                                else
                                {
                                    try
                                    {
                                        param = Regex.Split(splitcondition[i], "=")[0].Trim();
                                        //value = "=" + Regex.Split(splitcondition[i], "=")[1];
                                        value = Regex.Split(splitcondition[i], "=")[1];
                                        foreach (ListItem data in listofvalues)
                                        {
                                            if (value == data.Text)
                                            {
                                                value = data.Value;
                                            }
                                        }
                                        value = "=" + value;
                                    }
                                    catch (Exception es)
                                    {
                                        ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                                        return;
                                    }
                                }

                                if (i == 0)
                                {
                                    query.Append("(Parameter='" + param + "' And Value" + value + ") ");
                                }
                                else
                                {
                                    query.Append(listAndOr[indexAndOR] + " (Parameter='" + param + "' And Value" + value + ") ");
                                    indexAndOR++;
                                }
                            }
                        }
                    }
                }
                else
                {
                    string splitvalue = Regex.Split(statementLower1, "systemdoctransaction")[1];
                    if (splitvalue != "")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                return;
            }
            Session["StatisticsColumns"] = column.ToString();
            Session["StatisticsConditions"] = query.ToString();
            bindParameterMinMaxAvg("");
        }
        private void bindMinMaxTabletoParametrlist()
        {
            (lvMinMaxParameterList.FindControl("cbselectAllInputModule") as CheckBox).Checked = false;
            for (int j = 0; j < lvMinMaxParameterList.Items.Count; j++)
            {
                CheckBoxList checkBoxList = lvMinMaxParameterList.Items[j].FindControl("chkList") as CheckBoxList;
                for (int k = 0; k < checkBoxList.Items.Count; k++)
                {
                    checkBoxList.Items[k].Selected = false;
                }
                (lvMinMaxParameterList.Items[j].FindControl("cbSelectallInput") as CheckBox).Checked = false;

            }

            for (int i = 0; i < gvMinMaxAvg.Rows.Count; i++)
            {
                string parameter = (gvMinMaxAvg.Rows[i].FindControl("item") as Label).Text;
                for (int j = 0; j < lvMinMaxParameterList.Items.Count; j++)
                {
                    int count = 0;
                    CheckBoxList checkBoxList = lvMinMaxParameterList.Items[j].FindControl("chkList") as CheckBoxList;
                    for (int k = 0; k < checkBoxList.Items.Count; k++)
                    {
                        // if (parameter == checkBoxList.Items[k].Value)
                        if (parameter == checkBoxList.Items[k].Value.Split(',')[0])
                        {
                            checkBoxList.Items[k].Selected = true;
                        }
                    }
                }
            }

            for (int j = 0; j < lvMinMaxParameterList.Items.Count; j++)
            {
                int count = 0;
                CheckBoxList checkBoxList = lvMinMaxParameterList.Items[j].FindControl("chkList") as CheckBoxList;
                for (int k = 0; k < checkBoxList.Items.Count; k++)
                {
                    if (checkBoxList.Items[k].Selected)
                    {
                        count++;
                    }
                }
                if (count == checkBoxList.Items.Count)
                {
                    (lvMinMaxParameterList.Items[j].FindControl("cbSelectallInput") as CheckBox).Checked = true;
                }
            }
            int count1 = 0;
            for (int j = 0; j < lvMinMaxParameterList.Items.Count; j++)
            {
                if ((lvMinMaxParameterList.Items[j].FindControl("cbSelectallInput") as CheckBox).Checked)
                {
                    count1++;
                }
            }
            if (count1 == lvMinMaxParameterList.Items.Count)
            {
                (lvMinMaxParameterList.FindControl("cbselectAllInputModule") as CheckBox).Checked = true;
            }
        }
        protected void minMaxOk_ServerClick(object sender, EventArgs e)
        {
            txtsearch.Text = "";
            StringBuilder column = new StringBuilder();
            column.Append("");
            string sort = "";
            for (int i = 0; i < lvMinMaxParameterList.Items.Count; i++)
            {
                CheckBoxList checkBoxList = lvMinMaxParameterList.Items[i].FindControl("chkList") as CheckBoxList;
                if (checkBoxList.Items.Count > 0)
                {
                    for (int j = 0; j < checkBoxList.Items.Count; j++)
                    {
                        if (checkBoxList.Items[j].Selected)
                        {
                            //string parameter = checkBoxList.Items[j].Value;
                            string parameter = checkBoxList.Items[j].Value.Split(',')[0];
                            column.Append(parameter);
                            column.Append(",");
                        }
                    }

                }
            }
            int success;
            Session["StatisticsColumns"] = column.ToString();
            List<ADKParameterMinMaxAvg> listminmax = DBAccess.getATKParameterMinMaxAvg(Session["StatisticsConditions"] != null ? Session["StatisticsConditions"].ToString() : "", Session["StatisticsColumns"] != null ? Session["StatisticsColumns"].ToString() : "", sort, out success);
            gvMinMaxAvg.DataSource = listminmax;
            gvMinMaxAvg.DataBind();
            bindMinMaxTabletoParametrlist();
            gvminmaxLarge.DataSource = listminmax;
            gvminmaxLarge.DataBind();
            string ddllist = setdropdownvaluesforgraph(listminmax);
            try
            {
                if (listminmax.Count == 0)
                {
                    txtGraphParams1.Text = "";
                    txtGraphParams2.Text = "";
                    // ddlGraphParam3.Items.Clear();
                    txtGraphParams3.Text = "";
                    txtGraphParams4.Text = "";
                    txtGraphParams5.Text = "";
                }
                else
                 if (listminmax.Count == 1)
                {

                    // ddlGraphParams1.InnerHtml = setdropdownvaluesforgraph(listminmax, 0, txtGraphParams1, ddlGraphParams1);
                    ddlGraphParams1.InnerHtml = ddllist;
                    txtGraphParams1.Text = listminmax[0].Parameter.ToString();
                    txtGraphParams2.Text = "";
                    txtGraphParams3.Text = "";
                    txtGraphParams4.Text = "";
                    txtGraphParams5.Text = "";
                }
                else if (listminmax.Count == 2)
                {
                    //ddlGraphParams1.InnerHtml = setdropdownvaluesforgraph(listminmax, 0, txtGraphParams1, ddlGraphParams1);
                    //ddlGraphParams2.InnerHtml = setdropdownvaluesforgraph(listminmax, 1, txtGraphParams2, ddlGraphParams2);
                    ddlGraphParams1.InnerHtml = ddllist;
                    txtGraphParams1.Text = listminmax[0].Parameter.ToString();
                    ddlGraphParams2.InnerHtml = ddllist;
                    txtGraphParams2.Text = listminmax[1].Parameter.ToString();
                    txtGraphParams3.Text = "";
                    txtGraphParams4.Text = "";
                    txtGraphParams5.Text = "";
                }
                else if (listminmax.Count == 3)
                {

                    //ddlGraphParams1.InnerHtml = setdropdownvaluesforgraph(listminmax, 0, txtGraphParams1, ddlGraphParams1);
                    //ddlGraphParams2.InnerHtml = setdropdownvaluesforgraph(listminmax, 1, txtGraphParams2, ddlGraphParams2);
                    //ddlGraphParams3.InnerHtml = setdropdownvaluesforgraph(listminmax, 2, txtGraphParams3, ddlGraphParams3);
                    ddlGraphParams1.InnerHtml = ddllist;
                    txtGraphParams1.Text = listminmax[0].Parameter.ToString();
                    ddlGraphParams2.InnerHtml = ddllist;
                    txtGraphParams2.Text = listminmax[1].Parameter.ToString();
                    ddlGraphParams3.InnerHtml = ddllist;
                    txtGraphParams3.Text = listminmax[2].Parameter.ToString();
                    txtGraphParams4.Text = "";
                    txtGraphParams5.Text = "";
                }
                else if (listminmax.Count == 4)
                {

                    //ddlGraphParams1.InnerHtml = setdropdownvaluesforgraph(listminmax, 0, txtGraphParams1, ddlGraphParams1);
                    //ddlGraphParams2.InnerHtml = setdropdownvaluesforgraph(listminmax, 1, txtGraphParams2, ddlGraphParams2);
                    //ddlGraphParams3.InnerHtml = setdropdownvaluesforgraph(listminmax, 2, txtGraphParams3, ddlGraphParams3);
                    //ddlGraphParams4.InnerHtml = setdropdownvaluesforgraph(listminmax, 3, txtGraphParams4, ddlGraphParams4);
                    ddlGraphParams1.InnerHtml = ddllist;
                    txtGraphParams1.Text = listminmax[0].Parameter.ToString();
                    ddlGraphParams2.InnerHtml = ddllist;
                    txtGraphParams2.Text = listminmax[1].Parameter.ToString();
                    ddlGraphParams3.InnerHtml = ddllist;
                    txtGraphParams3.Text = listminmax[2].Parameter.ToString();
                    ddlGraphParams4.InnerHtml = ddllist;
                    txtGraphParams4.Text = listminmax[3].Parameter.ToString();
                    txtGraphParams5.Text = "";
                }
                else if (listminmax.Count > 4)
                {

                    //ddlGraphParams1.InnerHtml = setdropdownvaluesforgraph(listminmax, 0, txtGraphParams1, ddlGraphParams1);
                    //ddlGraphParams2.InnerHtml = setdropdownvaluesforgraph(listminmax, 1, txtGraphParams2, ddlGraphParams2);
                    //ddlGraphParams3.InnerHtml = setdropdownvaluesforgraph(listminmax, 2, txtGraphParams3, ddlGraphParams3);
                    //ddlGraphParams4.InnerHtml = setdropdownvaluesforgraph(listminmax, 3, txtGraphParams4, ddlGraphParams4);
                    //ddlGraphParams5.InnerHtml = setdropdownvaluesforgraph(listminmax, 4, txtGraphParams5, ddlGraphParams5);
                    ddlGraphParams1.InnerHtml = ddllist;
                    
                    txtGraphParams1.Text = listminmax[0].Parameter.ToString();
                    ddlGraphParams2.InnerHtml = ddllist;
                    txtGraphParams2.Text = listminmax[1].Parameter.ToString();
                    ddlGraphParams3.InnerHtml = ddllist;
                    txtGraphParams3.Text = listminmax[2].Parameter.ToString();
                    ddlGraphParams4.InnerHtml = ddllist;
                    txtGraphParams4.Text = listminmax[3].Parameter.ToString();
                    ddlGraphParams5.InnerHtml = ddllist;
                    txtGraphParams5.Text = listminmax[4].Parameter.ToString();

                }

            }
            catch (Exception ex)
            {

            }
            if (success == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('System Doc has incorrect datatype value. Please check input module data.');", true);
                return;
            }

            //ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "bindInputModuleInStatistics()", true);
        }


        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<ListItem> getDDLParamValueFrequency(string param, string querytxt)        {

            List<ListItem> listRes = DBAccess.getATKGraphValueFrequency(param, HttpContext.Current.Session["StatisticsConditions"] != null ? HttpContext.Current.Session["StatisticsConditions"].ToString() : "");            return listRes;        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string andBtnClick(string txtquery, string ddlparam, string ddlparamvalue)
        {

            List<string> conditionList = new List<string>();
            if (HttpContext.Current.Session["APKDisplayCondition"] != null)
            {

                conditionList = (List<string>)HttpContext.Current.Session["APKDisplayCondition"];
                conditionList.Add(" and ");
                conditionList.Add(ddlparam);
                conditionList.Add(ddlparamvalue);
            }
            else
            {
                //List<string> conditionList = new List<string>();
                conditionList.Add("");
                conditionList.Add(ddlparam);
                conditionList.Add(ddlparamvalue);
            }
            int flag = 0;
            for (int i = 0; i < conditionList.Count - 3; i = i + 3)
            {
                if (conditionList[conditionList.Count - 2] == conditionList[i + 1] && conditionList[conditionList.Count - 1] == conditionList[i + 2])
                {
                    flag = 1;
                    break;
                }
            }
            if (flag == 1)
            {
                conditionList.RemoveAt(conditionList.Count - 1);
                conditionList.RemoveAt(conditionList.Count - 1);
                conditionList.RemoveAt(conditionList.Count - 1);
            }
            HttpContext.Current.Session["APKDisplayCondition"] = conditionList;
            StringBuilder query = new StringBuilder();
            string queryString = txtquery;
            if (queryString.Contains("*"))
            {
                query.Append("select * from SystemDocTransaction where ");
            }
            else
            {
                string str = queryString.Split(new string[] { "select" }, StringSplitOptions.None)[1].Split(new string[] { "from" }, StringSplitOptions.None)[0].Trim();
                query.Append("select " + str + " from SystemDocTransaction where ");
            }
            for (int i = 0; i < conditionList.Count; i = i + 3)
            {
                query.Append(conditionList[i]);
                query.Append(conditionList[i + 1]);
                query.Append("=");
                query.Append("'" + conditionList[i + 2] + "'");
                //if (i != (conditionList.Count - 2))
                //{
                //    query.Append(" and ");
                //}

            }
            return query.ToString();
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string orBtnClick(string txtquery, string ddlparam, string ddlparamvalue)
        {

            List<string> conditionList = new List<string>();
            if (HttpContext.Current.Session["APKDisplayCondition"] != null)
            {

                conditionList = (List<string>)HttpContext.Current.Session["APKDisplayCondition"];
                conditionList.Add(" or ");
                conditionList.Add(ddlparam);
                conditionList.Add(ddlparamvalue);

            }
            else
            {
                //List<string> conditionList = new List<string>();
                conditionList.Add("");
                conditionList.Add(ddlparam);
                conditionList.Add(ddlparamvalue);
            }
            int flag = 0;
            for (int i = 0; i < conditionList.Count - 3; i = i + 3)
            {
                if (conditionList[conditionList.Count - 2] == conditionList[i + 1] && conditionList[conditionList.Count - 1] == conditionList[i + 2])
                {
                    flag = 1;
                    break;
                }
            }
            if (flag == 1)
            {
                conditionList.RemoveAt(conditionList.Count - 1);
                conditionList.RemoveAt(conditionList.Count - 1);
                conditionList.RemoveAt(conditionList.Count - 1);
            }
            HttpContext.Current.Session["APKDisplayCondition"] = conditionList;
            StringBuilder query = new StringBuilder();
            string queryString = txtquery;
            if (queryString.Contains("*"))
            {
                query.Append("select * from SystemDocTransaction where ");
            }
            else
            {
                string str = queryString.Split(new string[] { "select" }, StringSplitOptions.None)[1].Split(new string[] { "from" }, StringSplitOptions.None)[0].Trim();
                query.Append("select " + str + " from SystemDocTransaction where ");
            }
            for (int i = 0; i < conditionList.Count; i = i + 3)
            {
                query.Append(conditionList[i]);
                query.Append(conditionList[i + 1]);
                query.Append("=");
                query.Append("'" + conditionList[i + 2] + "'");
                //if (i != (conditionList.Count - 2))
                //{
                //    query.Append(" or ");
                //}

            }
            return query.ToString();
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<string> bindSdocPlungeCatValues(string parameter, string txtqueryvalue)
        {
            string statement = txtqueryvalue;
            string statementLower = txtqueryvalue.ToLower();
            string statementLower1 = txtqueryvalue.ToLower();
            // string query = "";
            StringBuilder query = new StringBuilder();
            query.Append("");
            string column = "";
            List<string> listAndOr = new List<string>();
            try
            {
                if (!statementLower.Contains("select ") || !statementLower.Contains(" from "))
                {
                    //   ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                    // return "Error";
                }
                string[] starsplit;
                //if (statementLower.Contains("*"))
                //{
                //    starsplit = statementLower.Split('*');
                //    if (starsplit[0].Contains("select") && starsplit[1].Contains("from"))
                //    {
                //        column = "";
                //    }
                //}
                //if (!statementLower.Contains("*"))
                //{
                //    if (statementLower.Contains("select") && statementLower.Contains("from"))
                //    {
                //        string split1 = Regex.Split(statement, "select")[1];
                //        column = Regex.Split(split1, "from")[0].Trim();
                //    }

                //}
                if (statementLower.Contains("where"))
                {
                    if (Regex.Split(statementLower, "where")[1] != "")
                    {
                        string q = Regex.Split(statementLower, "where")[1];
                        if (q.Contains("sdocname") || q.Contains("plungeid") || q.Contains("subcategoryid"))
                        {
                            query.Append(q);
                        }
                        //else
                        //{

                        //    string[] splitcondition = q.Split(new string[] { " and ", " or " }, StringSplitOptions.RemoveEmptyEntries);
                        //    do
                        //    {
                        //        int indexofAnd = statementLower.IndexOf(" and ");
                        //        int indexofOr = statementLower.IndexOf(" or ");
                        //        if (indexofAnd == indexofOr)
                        //        {
                        //            break;
                        //        }
                        //        if (indexofOr == -1 && indexofAnd >= 0)
                        //        {
                        //            listAndOr.Add("and");
                        //            statementLower = statementLower.Substring(indexofAnd + 3);
                        //        }
                        //        else if (indexofAnd == -1 && indexofOr >= 0)
                        //        {
                        //            listAndOr.Add("or");
                        //            statementLower = statementLower.Substring(indexofOr + 2);
                        //        }
                        //        else
                        //        if (indexofAnd < indexofOr)
                        //        {
                        //            listAndOr.Add("and");
                        //            statementLower = statementLower.Substring(indexofAnd + 3);
                        //        }
                        //        else
                        //        {
                        //            listAndOr.Add("or");
                        //            statementLower = statementLower.Substring(indexofOr + 2);
                        //        }
                        //    } while (statementLower.Length > 0);
                        //    int indexAndOR = 0;
                        //    for (int i = 0; i < splitcondition.Length; i++)
                        //    {
                        //        //string condition = splitcondition[i].Trim().Replace("", string.Empty);
                        //        string param = "", value = "";
                        //        string condition = Regex.Replace(splitcondition[i].ToString(), " ", string.Empty);
                        //        condition = Regex.Replace(condition, "\n", string.Empty);
                        //        if (Regex.IsMatch(condition, @".*in\(.*\)$"))
                        //        {
                        //            string[] list = Regex.Split(splitcondition[i].ToString(), "in()");
                        //            param = list[0].Trim();
                        //            //string v = list[2].Remove(list[2].Length - 1, 1);
                        //            //string v1=v.Remove(0, 1);
                        //            value = " in " + list[2];
                        //        }
                        //        else
                        //        {
                        //            //return;
                        //            try
                        //            {
                        //                param = Regex.Split(splitcondition[i], "=")[0].Trim();
                        //                value = "=" + Regex.Split(splitcondition[i], "=")[1];
                        //            }
                        //            catch (Exception es)
                        //            {
                        //                // ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                        //                // return;
                        //            }
                        //        }
                        //        if (i == 0)
                        //        {
                        //            query.Append("(Parameter='" + param + "' And Value" + value + ") ");
                        //        }
                        //        else
                        //        {
                        //            query.Append(listAndOr[indexAndOR] + " (Parameter='" + param + "' And Value" + value + ") ");
                        //            indexAndOR++;
                        //        }
                        //    }
                        //}
                    }
                }
                else
                {
                    string splitvalue = Regex.Split(statementLower1, "systemdoctransaction")[1];
                    if (splitvalue.Trim() != "")
                    {
                        //ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                        //  return "Error";
                    }
                }
            }
            catch (Exception ex)
            {
                // ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                //return "Error";
            }

            List<string> reslist = DBAccess.ddlSdocPlungeCatIndexChange(parameter, query.ToString());
            return reslist;
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void setSessionAPKDisplayCondition()
        {
            HttpContext.Current.Session["APKDisplayCondition"] = null;
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<string> bindParameterValues(string parameter, string txtqueryvalue)
        {
            string statement = txtqueryvalue;
            string statementLower = txtqueryvalue.ToLower();
            string statementLower1 = txtqueryvalue.ToLower();
            // string query = "";
            StringBuilder query = new StringBuilder();
            query.Append("");
            string column = "";
            List<string> listAndOr = new List<string>();
            try
            {
                if (!statementLower.Contains("select ") || !statementLower.Contains(" from "))
                {
                    //   ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                    // return "Error";
                }
                string[] starsplit;
                //if (statementLower.Contains("*"))
                //{
                //    starsplit = statementLower.Split('*');
                //    if (starsplit[0].Contains("select") && starsplit[1].Contains("from"))
                //    {
                //        column = "";
                //    }
                //}
                //if (!statementLower.Contains("*"))
                //{
                //    if (statementLower.Contains("select") && statementLower.Contains("from"))
                //    {
                //        string split1 = Regex.Split(statement, "select")[1];
                //        column = Regex.Split(split1, "from")[0].Trim();
                //    }

                //}
                if (statementLower.Contains("where"))
                {
                    if (Regex.Split(statementLower, "where")[1] != "")
                    {
                        string q = Regex.Split(statementLower, "where")[1];

                        if (q.Contains("sdocname") || q.Contains("plungeid") || q.Contains("subcategoryid"))
                        {
                            query.Append(q);
                        }
                        else
                        {
                            string ss = q;
                            //Regex r = new Regex(@"'(.+?)'|in\(.*\)$");
                            Regex r = new Regex(@"'(.+?)'|in[ ]{0,}\(.+?\)");
                            MatchCollection mc = r.Matches(ss);
                            int len = mc.Count;
                            string replacestring = ss;
                            int jj = 0;
                            List<ListItem> listofvalues = new List<ListItem>();
                            foreach (Match data in mc)
                            {
                                if (ss.Contains(data.Value))
                                {

                                    //  replacestring = replacestring.Replace(data.Value, "r" + jj);
                                    ApplicationToolKit atk = new ApplicationToolKit();
                                    replacestring = atk.ReplaceFirst(replacestring, data.Value, "r" + jj);
                                    listofvalues.Add(new ListItem("r" + jj, data.Value));
                                    jj++;
                                }
                            }
                            string finalstr = replacestring;
                            statementLower = finalstr;
                            // var arra = finalstr.Split(new string[] { " and ", " or " }, StringSplitOptions.RemoveEmptyEntries);
                            string[] splitcondition = finalstr.Split(new string[] { " and ", " or " }, StringSplitOptions.RemoveEmptyEntries);
                            do
                            {
                                int indexofAnd = statementLower.IndexOf(" and ");
                                int indexofOr = statementLower.IndexOf(" or ");
                                if (indexofAnd == indexofOr)
                                {
                                    break;
                                }
                                if (indexofOr == -1 && indexofAnd >= 0)
                                {
                                    listAndOr.Add("and");
                                    statementLower = statementLower.Substring(indexofAnd + 3);
                                }
                                else if (indexofAnd == -1 && indexofOr >= 0)
                                {
                                    listAndOr.Add("or");
                                    statementLower = statementLower.Substring(indexofOr + 2);
                                }
                                else
                                if (indexofAnd < indexofOr)
                                {
                                    listAndOr.Add("and");
                                    statementLower = statementLower.Substring(indexofAnd + 3);
                                }
                                else
                                {
                                    listAndOr.Add("or");
                                    statementLower = statementLower.Substring(indexofOr + 2);
                                }
                            } while (statementLower.Length > 0);
                            int indexAndOR = 0;
                            for (int i = 0; i < splitcondition.Length; i++)
                            {
                                //string condition = splitcondition[i].Trim().Replace("", string.Empty);
                                string param = "", value = "";
                                //string condition = Regex.Replace(splitcondition[i].ToString(), " ", string.Empty);
                                string condition = splitcondition[i].ToString().Trim();
                                //condition = Regex.Replace(condition, "\n", string.Empty);
                                if (!condition.Contains("="))
                                {
                                    string incondition = condition.Trim();

                                    value = incondition.Split(' ').Last().Trim();
                                    param = incondition.Remove(incondition.Length - value.Length).Trim();
                                    foreach (ListItem data in listofvalues)
                                    {
                                        if (value == data.Text)
                                        {
                                            value = data.Value;
                                        }
                                    }
                                    value = " " + value;
                                }
                                else
                                {
                                    try
                                    {
                                        param = Regex.Split(splitcondition[i], "=")[0].Trim();
                                        //value = "=" + Regex.Split(splitcondition[i], "=")[1];
                                        value = Regex.Split(splitcondition[i], "=")[1];
                                        foreach (ListItem data in listofvalues)
                                        {
                                            if (value == data.Text)
                                            {
                                                value = data.Value;
                                            }
                                        }
                                        value = "=" + value;
                                    }
                                    catch (Exception es)
                                    {
                                        //  ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                                        //  return;
                                    }
                                }

                                if (i == 0)
                                {
                                    query.Append("(Parameter='" + param + "' And Value" + value + ") ");
                                }
                                else
                                {
                                    query.Append(listAndOr[indexAndOR] + " (Parameter='" + param + "' And Value" + value + ") ");
                                    indexAndOR++;
                                }
                            }
                        }
                    }
                }
                else
                {
                    string splitvalue = Regex.Split(statementLower1, "systemdoctransaction")[1];
                    if (splitvalue.Trim() != "")
                    {
                        //ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                        //  return "Error";
                    }
                }
            }
            catch (Exception ex)
            {
                // ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                //return "Error";
            }
            //   List<string> reslist = DBAccess.getATKTabParameterValueScript(parameter, HttpContext.Current.Session["ExecuteConditions"]!=null ? HttpContext.Current.Session["ExecuteConditions"].ToString() : "" );
            List<string> reslist = DBAccess.getATKTabParameterValueScript(parameter, query.ToString());
            return reslist;
        }

        protected void ddlMinMaxInputModule_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void cbSdocdrillOption_CheckedChanged(object sender, EventArgs e)
        {
            Session["NormalBind"] = cbSdocdrillOption.Checked;
            DataTable dt = null;
            if (Session["ATKPagination"].ToString() == "allinformation")
            {
                // dt = DBAccess.getSystemDocumentData();
                dt = DBAccess.getAllTabDetails("", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                if (cbSdocdrillOption.Checked)
                {
                    //removeTableColumn(dt);
                    //gvNormalSDocbind.DataSource = dt;
                    //gvNormalSDocbind.DataBind();
                    bindNormalSdoclist(dt);
                    gvNormalSDocbind.Visible = true;
                    gvDisplayData.Visible = false;
                    noOfRows.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    bindSDocList(dt);
                    gvNormalSDocbind.Visible = false;
                    gvDisplayData.Visible = true;
                }
            }
            else if (Session["ATKPagination"].ToString() == "systemdoc")            {
                // dt = DBAccess.getSystemDocumentData();
                dt = DBAccess.getAllTabDetails("General Information", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                if (cbSdocdrillOption.Checked)
                {
                    //removeTableColumn(dt);
                    //gvNormalSDocbind.DataSource = dt;
                    //gvNormalSDocbind.DataBind();
                    bindNormalSdoclist(dt);
                    gvNormalSDocbind.Visible = true;
                    gvDisplayData.Visible = false;
                    noOfRows.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    bindSDocList(dt);
                    gvNormalSDocbind.Visible = false;
                    gvDisplayData.Visible = true;
                }            }            else if (Session["ATKPagination"].ToString() == "machinetool")            {
                // dt = DBAccess.getMachineToolData();
                dt = DBAccess.getAllTabDetails("Machine Tool", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                if (cbSdocdrillOption.Checked)
                {
                    //removeTableColumn(dt);
                    //gvNormalSDocbind.DataSource = dt;
                    //gvNormalSDocbind.DataBind();
                    bindNormalSdoclist(dt);
                    gvNormalSDocbind.Visible = true;
                    gvDisplayData.Visible = false;
                    noOfRows.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    bindSDocList(dt);
                    gvNormalSDocbind.Visible = false;
                    gvDisplayData.Visible = true;
                }            }            else if (Session["ATKPagination"].ToString() == "wheel")            {
                //  dt = DBAccess.getWheelData();
                dt = DBAccess.getAllTabDetails("Consumables Details", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                if (cbSdocdrillOption.Checked)
                {
                    //removeTableColumn(dt);
                    //gvNormalSDocbind.DataSource = dt;
                    //gvNormalSDocbind.DataBind();
                    bindNormalSdoclist(dt);
                    gvNormalSDocbind.Visible = true;
                    gvDisplayData.Visible = false;
                    noOfRows.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    bindSDocList(dt);
                    gvNormalSDocbind.Visible = false;
                    gvDisplayData.Visible = true;
                }            }            else if (Session["ATKPagination"].ToString() == "workpiece")            {
                // dt = DBAccess.getWorkPieceData();
                dt = DBAccess.getAllTabDetails("Workpiece Details", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                if (cbSdocdrillOption.Checked)
                {
                    //removeTableColumn(dt);
                    //gvNormalSDocbind.DataSource = dt;
                    //gvNormalSDocbind.DataBind();
                    bindNormalSdoclist(dt);
                    gvNormalSDocbind.Visible = true;
                    gvDisplayData.Visible = false;
                    noOfRows.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    bindSDocList(dt);
                    gvNormalSDocbind.Visible = false;
                    gvDisplayData.Visible = true;
                }            }            else if (Session["ATKPagination"].ToString() == "operational")            {
                //  dt = DBAccess.getOperationalParameterData();
                dt = DBAccess.getAllTabDetails("Operational Parameters", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                if (cbSdocdrillOption.Checked)
                {
                    //removeTableColumn(dt);
                    //gvNormalSDocbind.DataSource = dt;
                    //gvNormalSDocbind.DataBind();
                    bindNormalSdoclist(dt);
                    gvNormalSDocbind.Visible = true;
                    gvDisplayData.Visible = false;
                    noOfRows.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    bindSDocList(dt);
                    gvNormalSDocbind.Visible = false;
                    gvDisplayData.Visible = true;
                }            }            else if (Session["ATKPagination"].ToString() == "quality")            {

                dt = DBAccess.getAllTabDetails("Quality Parameters", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                if (cbSdocdrillOption.Checked)
                {
                    //removeTableColumn(dt);
                    //gvNormalSDocbind.DataSource = dt;
                    //gvNormalSDocbind.DataBind();
                    bindNormalSdoclist(dt);
                    gvNormalSDocbind.Visible = true;
                    gvDisplayData.Visible = false;
                    noOfRows.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    bindSDocList(dt);
                    gvNormalSDocbind.Visible = false;
                    gvDisplayData.Visible = true;
                }            }            else if (Session["ATKPagination"].ToString() == "execute")            {                string statement = Session["QueryForPageindex"].ToString();                string statementLower = Session["QueryForPageindex"].ToString().ToLower();
                // string query = "";
                StringBuilder query = new StringBuilder();                query.Append("");                string column = "";                List<string> listAndOr = new List<string>();                try                {                    string[] starsplit;                    if (statementLower.Contains("*"))                    {                        starsplit = statementLower.Split('*');                        if (starsplit[0].Contains("select") && starsplit[1].Contains("from"))                        {                            column = "";                        }                    }                    if (!statementLower.Contains("*"))                    {                        if (statementLower.Contains("select") && statementLower.Contains("from"))                        {                            string split1 = Regex.Split(statement, "select")[1];                            column = Regex.Split(split1, "from")[0].Trim();                        }                    }                    if (statementLower.Contains("where"))
                    {
                        if (Regex.Split(statementLower, "where")[1] != "")
                        {
                            string q = Regex.Split(statementLower, "where")[1];

                            if (q.Contains("sdocname") || q.Contains("plungeid") || q.Contains("subcategoryid"))
                            {
                                query.Append(q);
                            }
                            else
                            {
                                string ss = q;
                                //Regex r = new Regex(@"'(.+?)'|in\(.*\)$");
                                Regex r = new Regex(@"'(.+?)'|in[ ]{0,}\(.+?\)");
                                MatchCollection mc = r.Matches(ss);
                                int len = mc.Count;
                                string replacestring = ss;
                                int jj = 0;
                                List<ListItem> listofvalues = new List<ListItem>();
                                foreach (Match data in mc)
                                {
                                    if (ss.Contains(data.Value))
                                    {

                                        //  replacestring = replacestring.Replace(data.Value, "r" + jj);
                                        replacestring = ReplaceFirst(replacestring, data.Value, "r" + jj);
                                        listofvalues.Add(new ListItem("r" + jj, data.Value));
                                        jj++;
                                    }
                                }
                                string finalstr = replacestring;
                                statementLower = finalstr;
                                // var arra = finalstr.Split(new string[] { " and ", " or " }, StringSplitOptions.RemoveEmptyEntries);
                                string[] splitcondition = finalstr.Split(new string[] { " and ", " or " }, StringSplitOptions.RemoveEmptyEntries);
                                do
                                {
                                    int indexofAnd = statementLower.IndexOf(" and ");
                                    int indexofOr = statementLower.IndexOf(" or ");
                                    if (indexofAnd == indexofOr)
                                    {
                                        break;
                                    }
                                    if (indexofOr == -1 && indexofAnd >= 0)
                                    {
                                        listAndOr.Add("and");
                                        statementLower = statementLower.Substring(indexofAnd + 3);
                                    }
                                    else if (indexofAnd == -1 && indexofOr >= 0)
                                    {
                                        listAndOr.Add("or");
                                        statementLower = statementLower.Substring(indexofOr + 2);
                                    }
                                    else
                                    if (indexofAnd < indexofOr)
                                    {
                                        listAndOr.Add("and");
                                        statementLower = statementLower.Substring(indexofAnd + 3);
                                    }
                                    else
                                    {
                                        listAndOr.Add("or");
                                        statementLower = statementLower.Substring(indexofOr + 2);
                                    }
                                } while (statementLower.Length > 0);
                                int indexAndOR = 0;
                                for (int i = 0; i < splitcondition.Length; i++)
                                {
                                    //string condition = splitcondition[i].Trim().Replace("", string.Empty);
                                    string param, value;
                                    //string condition = Regex.Replace(splitcondition[i].ToString(), " ", string.Empty);
                                    string condition = splitcondition[i].ToString().Trim();
                                    //condition = Regex.Replace(condition, "\n", string.Empty);
                                    if (!condition.Contains("="))
                                    {
                                        string incondition = condition.Trim();

                                        value = incondition.Split(' ').Last().Trim();
                                        param = incondition.Remove(incondition.Length - value.Length).Trim();
                                        foreach (ListItem data in listofvalues)
                                        {
                                            if (value == data.Text)
                                            {
                                                value = data.Value;
                                            }
                                        }
                                        value = " " + value;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            param = Regex.Split(splitcondition[i], "=")[0].Trim();
                                            //value = "=" + Regex.Split(splitcondition[i], "=")[1];
                                            value = Regex.Split(splitcondition[i], "=")[1];
                                            foreach (ListItem data in listofvalues)
                                            {
                                                if (value == data.Text)
                                                {
                                                    value = data.Value;
                                                }
                                            }
                                            value = "=" + value;
                                        }
                                        catch (Exception es)
                                        {
                                            ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                                            return;
                                        }
                                    }

                                    if (i == 0)
                                    {
                                        query.Append("(Parameter='" + param + "' And Value" + value + ") ");
                                    }
                                    else
                                    {
                                        query.Append(listAndOr[indexAndOR] + " (Parameter='" + param + "' And Value" + value + ") ");
                                        indexAndOR++;
                                    }
                                }
                            }
                        }
                    }                }                catch (Exception ex)                {                }                string inputmodule = "";                if (column == "")                {                    string sessionInputModuleValue = Session["ATKPagination"].ToString();                    if (sessionInputModuleValue == "systemdoc")                    {                        inputmodule = "General Information";                    }                    else if (sessionInputModuleValue == "machinetool")                    {                        inputmodule = "Machine Tool";                    }                    else if (sessionInputModuleValue == "wheel")                    {                        inputmodule = "Consumables Details";                    }                    else if (sessionInputModuleValue == "workpiece")                    {                        inputmodule = "Workpiece Details";                    }                    else if (sessionInputModuleValue == "operational")                    {                        inputmodule = "Operational Parameters";                    }                    else if (sessionInputModuleValue == "quality")                    {                        inputmodule = "Quality Parameters";                    }
                    else if (sessionInputModuleValue == "allinformation")
                    {
                        inputmodule = "";
                    }                }                string error = "";                dt = DBAccess.getQueryData(query.ToString(), column, inputmodule, out error);
                //if (error != "")
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                //    return;
                //}
                if (cbSdocdrillOption.Checked)
                {
                    //removeTableColumn(dt);
                    //gvNormalSDocbind.DataSource = dt;
                    //gvNormalSDocbind.DataBind();
                    bindNormalSdoclist(dt);
                    gvNormalSDocbind.Visible = true;
                    gvDisplayData.Visible = false;
                    noOfRows.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    bindSDocList(dt);
                    gvNormalSDocbind.Visible = false;
                    gvDisplayData.Visible = true;
                }            }
        }
        private void removeTableColumn(DataTable dt)        {
            try
            {
                dt.Columns.Remove("SDocName");
                dt.Columns.Remove("PlungeId");
                dt.Columns.Remove("SubCategoryId");
            }
            catch (Exception e)
            {
            }        }

        protected void gvNormalSDocbind_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {

                gvNormalSDocbind.PageIndex = e.NewPageIndex;
                DataTable dt = null;
                if (Session["ATKPagination"].ToString() == "allinformation")
                {
                    // dt = DBAccess.getSystemDocumentData();
                    dt = DBAccess.getAllTabDetails("", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                    //gvDisplayData.DataSource = dt;
                    //gvDisplayData.DataBind();
                    if (cbSdocdrillOption.Checked)
                    {
                        //removeTableColumn(dt);
                        //gvNormalSDocbind.DataSource = dt;
                        //gvNormalSDocbind.DataBind();
                        bindNormalSdoclist(dt);
                        gvNormalSDocbind.Visible = true;
                        gvDisplayData.Visible = false;
                        noOfRows.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        bindSDocList(dt);
                        gvNormalSDocbind.Visible = false;
                        gvDisplayData.Visible = true;
                    }
                }
                else if (Session["ATKPagination"].ToString() == "systemdoc")
                {
                    // dt = DBAccess.getSystemDocumentData();
                    dt = DBAccess.getAllTabDetails("General Information", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                    //gvDisplayData.DataSource = dt;
                    //gvDisplayData.DataBind();
                    if (cbSdocdrillOption.Checked)
                    {
                        //removeTableColumn(dt);
                        //gvNormalSDocbind.DataSource = dt;
                        //gvNormalSDocbind.DataBind();
                        bindNormalSdoclist(dt);
                        gvNormalSDocbind.Visible = true;
                        gvDisplayData.Visible = false;
                        noOfRows.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        bindSDocList(dt);
                        gvNormalSDocbind.Visible = false;
                        gvDisplayData.Visible = true;
                    }
                }
                else if (Session["ATKPagination"].ToString() == "machinetool")
                {
                    // dt = DBAccess.getMachineToolData();
                    dt = DBAccess.getAllTabDetails("Machine Tool", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                    //gvDisplayData.DataSource = dt;
                    //gvDisplayData.DataBind();
                    if (cbSdocdrillOption.Checked)
                    {
                        //removeTableColumn(dt);
                        //gvNormalSDocbind.DataSource = dt;
                        //gvNormalSDocbind.DataBind();
                        bindNormalSdoclist(dt);
                        gvNormalSDocbind.Visible = true;
                        gvDisplayData.Visible = false;
                        noOfRows.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        bindSDocList(dt);
                        gvNormalSDocbind.Visible = false;
                        gvDisplayData.Visible = true;
                    }
                }
                else if (Session["ATKPagination"].ToString() == "wheel")
                {
                    //  dt = DBAccess.getWheelData();
                    dt = DBAccess.getAllTabDetails("Consumables Details", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                    //gvDisplayData.DataSource = dt;
                    //gvDisplayData.DataBind();
                    if (cbSdocdrillOption.Checked)
                    {
                        //removeTableColumn(dt);
                        //gvNormalSDocbind.DataSource = dt;
                        //gvNormalSDocbind.DataBind();
                        bindNormalSdoclist(dt);
                        gvNormalSDocbind.Visible = true;
                        gvDisplayData.Visible = false;
                        noOfRows.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        bindSDocList(dt);
                        gvNormalSDocbind.Visible = false;
                        gvDisplayData.Visible = true;
                    }
                }
                else if (Session["ATKPagination"].ToString() == "workpiece")
                {
                    // dt = DBAccess.getWorkPieceData();
                    dt = DBAccess.getAllTabDetails("Workpiece Details", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                    //gvDisplayData.DataSource = dt;
                    //gvDisplayData.DataBind();
                    if (cbSdocdrillOption.Checked)
                    {
                        //removeTableColumn(dt);
                        //gvNormalSDocbind.DataSource = dt;
                        //gvNormalSDocbind.DataBind();
                        bindNormalSdoclist(dt);
                        gvNormalSDocbind.Visible = true;
                        gvDisplayData.Visible = false;
                        noOfRows.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        bindSDocList(dt);
                        gvNormalSDocbind.Visible = false;
                        gvDisplayData.Visible = true;
                    }
                }
                else if (Session["ATKPagination"].ToString() == "operational")
                {
                    //  dt = DBAccess.getOperationalParameterData();
                    dt = DBAccess.getAllTabDetails("Operational Parameters", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                    //gvDisplayData.DataSource = dt;
                    //gvDisplayData.DataBind();
                    if (cbSdocdrillOption.Checked)
                    {
                        //removeTableColumn(dt);
                        //gvNormalSDocbind.DataSource = dt;
                        //gvNormalSDocbind.DataBind();
                        bindNormalSdoclist(dt);
                        gvNormalSDocbind.Visible = true;
                        gvDisplayData.Visible = false;
                        noOfRows.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        bindSDocList(dt);
                        gvNormalSDocbind.Visible = false;
                        gvDisplayData.Visible = true;
                    }
                }
                else if (Session["ATKPagination"].ToString() == "quality")
                {

                    dt = DBAccess.getAllTabDetails("Quality Parameters", Session["ExecuteConditions"] != null ? Session["ExecuteConditions"].ToString() : "");
                    // dt = DBAccess.getTargetQualityData();
                    //gvDisplayData.DataSource = dt;
                    //gvDisplayData.DataBind();
                    if (cbSdocdrillOption.Checked)
                    {
                        //removeTableColumn(dt);
                        //gvNormalSDocbind.DataSource = dt;
                        //gvNormalSDocbind.DataBind();
                        bindNormalSdoclist(dt);
                        gvNormalSDocbind.Visible = true;
                        gvDisplayData.Visible = false;
                        noOfRows.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        bindSDocList(dt);
                        gvNormalSDocbind.Visible = false;
                        gvDisplayData.Visible = true;
                    }
                }
                else if (Session["ATKPagination"].ToString() == "execute")
                {
                    string statement = Session["QueryForPageindex"].ToString();
                    string statementLower = Session["QueryForPageindex"].ToString().ToLower();
                    // string query = "";
                    StringBuilder query = new StringBuilder();
                    query.Append("");
                    string column = "";
                    List<string> listAndOr = new List<string>();
                    try
                    {
                        string[] starsplit;
                        if (statementLower.Contains("*"))
                        {
                            starsplit = statementLower.Split('*');
                            if (starsplit[0].Contains("select") && starsplit[1].Contains("from"))
                            {
                                column = "";
                            }
                        }
                        if (!statementLower.Contains("*"))
                        {
                            if (statementLower.Contains("select") && statementLower.Contains("from"))
                            {
                                string split1 = Regex.Split(statement, "select")[1];
                                column = Regex.Split(split1, "from")[0].Trim();
                            }

                        }
                        if (statementLower.Contains("where"))
                        {
                            if (Regex.Split(statementLower, "where")[1] != "")
                            {
                                string q = Regex.Split(statementLower, "where")[1];

                                if (q.Contains("sdocname") || q.Contains("plungeid") || q.Contains("subcategoryid"))
                                {
                                    query.Append(q);
                                }
                                else
                                {
                                    string ss = q;
                                    //Regex r = new Regex(@"'(.+?)'|in\(.*\)$");
                                    Regex r = new Regex(@"'(.+?)'|in[ ]{0,}\(.+?\)");
                                    MatchCollection mc = r.Matches(ss);
                                    int len = mc.Count;
                                    string replacestring = ss;
                                    int jj = 0;
                                    List<ListItem> listofvalues = new List<ListItem>();
                                    foreach (Match data in mc)
                                    {
                                        if (ss.Contains(data.Value))
                                        {

                                            //  replacestring = replacestring.Replace(data.Value, "r" + jj);
                                            replacestring = ReplaceFirst(replacestring, data.Value, "r" + jj);
                                            listofvalues.Add(new ListItem("r" + jj, data.Value));
                                            jj++;
                                        }
                                    }
                                    string finalstr = replacestring;
                                    statementLower = finalstr;
                                    // var arra = finalstr.Split(new string[] { " and ", " or " }, StringSplitOptions.RemoveEmptyEntries);
                                    string[] splitcondition = finalstr.Split(new string[] { " and ", " or " }, StringSplitOptions.RemoveEmptyEntries);
                                    do
                                    {
                                        int indexofAnd = statementLower.IndexOf(" and ");
                                        int indexofOr = statementLower.IndexOf(" or ");
                                        if (indexofAnd == indexofOr)
                                        {
                                            break;
                                        }
                                        if (indexofOr == -1 && indexofAnd >= 0)
                                        {
                                            listAndOr.Add("and");
                                            statementLower = statementLower.Substring(indexofAnd + 3);
                                        }
                                        else if (indexofAnd == -1 && indexofOr >= 0)
                                        {
                                            listAndOr.Add("or");
                                            statementLower = statementLower.Substring(indexofOr + 2);
                                        }
                                        else
                                        if (indexofAnd < indexofOr)
                                        {
                                            listAndOr.Add("and");
                                            statementLower = statementLower.Substring(indexofAnd + 3);
                                        }
                                        else
                                        {
                                            listAndOr.Add("or");
                                            statementLower = statementLower.Substring(indexofOr + 2);
                                        }
                                    } while (statementLower.Length > 0);
                                    int indexAndOR = 0;
                                    for (int i = 0; i < splitcondition.Length; i++)
                                    {
                                        //string condition = splitcondition[i].Trim().Replace("", string.Empty);
                                        string param, value;
                                        //string condition = Regex.Replace(splitcondition[i].ToString(), " ", string.Empty);
                                        string condition = splitcondition[i].ToString().Trim();
                                        //condition = Regex.Replace(condition, "\n", string.Empty);
                                        if (!condition.Contains("="))
                                        {
                                            string incondition = condition.Trim();

                                            value = incondition.Split(' ').Last().Trim();
                                            param = incondition.Remove(incondition.Length - value.Length).Trim();
                                            foreach (ListItem data in listofvalues)
                                            {
                                                if (value == data.Text)
                                                {
                                                    value = data.Value;
                                                }
                                            }
                                            value = " " + value;
                                        }
                                        else
                                        {
                                            try
                                            {
                                                param = Regex.Split(splitcondition[i], "=")[0].Trim();
                                                //value = "=" + Regex.Split(splitcondition[i], "=")[1];
                                                value = Regex.Split(splitcondition[i], "=")[1];
                                                foreach (ListItem data in listofvalues)
                                                {
                                                    if (value == data.Text)
                                                    {
                                                        value = data.Value;
                                                    }
                                                }
                                                value = "=" + value;
                                            }
                                            catch (Exception es)
                                            {
                                                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                                                return;
                                            }
                                        }

                                        if (i == 0)
                                        {
                                            query.Append("(Parameter='" + param + "' And Value" + value + ") ");
                                        }
                                        else
                                        {
                                            query.Append(listAndOr[indexAndOR] + " (Parameter='" + param + "' And Value" + value + ") ");
                                            indexAndOR++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                    string inputmodule = "";
                    if (column == "")
                    {
                        string sessionInputModuleValue = Session["ATKPagination"].ToString();
                        if (sessionInputModuleValue == "systemdoc")
                        {
                            inputmodule = "General Information";
                        }
                        else if (sessionInputModuleValue == "machinetool")
                        {
                            inputmodule = "Machine Tool";
                        }
                        else if (sessionInputModuleValue == "wheel")
                        {
                            inputmodule = "Consumables Details";
                        }
                        else if (sessionInputModuleValue == "workpiece")
                        {
                            inputmodule = "Workpiece Details";
                        }
                        else if (sessionInputModuleValue == "operational")
                        {
                            inputmodule = "Operational Parameters";
                        }
                        else if (sessionInputModuleValue == "quality")
                        {
                            inputmodule = "Quality Parameters";
                        }
                        else if (sessionInputModuleValue == "allinformation")
                        {
                            inputmodule = "";
                        }
                    }
                    string error = "";
                    dt = DBAccess.getQueryData(query.ToString(), column, inputmodule, out error);
                    //if (error != "")
                    //{
                    //    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Syntax Error.');", true);
                    //    return;
                    //}
                    //gvDisplayData.DataSource = dt;
                    //gvDisplayData.DataBind();
                    if (cbSdocdrillOption.Checked)
                    {
                        //removeTableColumn(dt);
                        //gvNormalSDocbind.DataSource = dt;
                        //gvNormalSDocbind.DataBind();
                        bindNormalSdoclist(dt);
                        gvNormalSDocbind.Visible = true;
                        gvDisplayData.Visible = false;
                        noOfRows.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        bindSDocList(dt);
                        gvNormalSDocbind.Visible = false;
                        gvDisplayData.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void gvNormalSDocbind_PreRender(object sender, EventArgs e)
        {
            GridView grid = (GridView)sender;            if (grid != null)            {                GridViewRow pagerRow = (GridViewRow)grid.BottomPagerRow;                if (pagerRow != null)                {                    pagerRow.Visible = true;                }            }
        }
    }
}