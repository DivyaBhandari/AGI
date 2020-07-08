using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AGISoftware.DataBaseAccess;
using AGISoftware.Model;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using AGISoftware.Model;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Globalization;

namespace AGISoftware
{
    public partial class Contentpage : System.Web.UI.Page
    {

        List<DataInputModuleParameter> listParameter = new List<DataInputModuleParameter>();
        DataInputModuleParameter prameter = new DataInputModuleParameter();
        static string CutterWidth = "", InitialComponentDia = "", CurrentWheelDia = "", DresserDia="", InputComponentDiaOD="";
        static List<ParameterDependency> listparameterDependencie = new List<ParameterDependency>();
        static List<ParameterDetails> listinputParameters = new List<ParameterDetails>();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                bindSdocID();

                if (Session["ATKSDocID"] != null)
                {
                    BindData(Session["ATKSDocID"].ToString());

                    //bindData(Session["ATKSDocID"].ToString());
                }
                else
                {
                    BindData("");

                }
                DataTable dt = new DataTable();
                dt = DBAccess.GetInputModuleDetails();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][0].ToString() == "1")
                    {
                        generalInfo.InnerText = dt.Rows[i][2].ToString() == "" ? "General Information" : dt.Rows[i][2].ToString();
                    }
                    if (dt.Rows[i][0].ToString() == "2")
                    {
                        machineTool.InnerText = dt.Rows[i][2].ToString() == "" ? "Machine Tool" : dt.Rows[i][2].ToString();
                    }
                    if (dt.Rows[i][0].ToString() == "3")
                    {
                        consumables.InnerText = dt.Rows[i][2].ToString() == "" ? "Consumables" : dt.Rows[i][2].ToString();
                    }
                    if (dt.Rows[i][0].ToString() == "4")
                    {
                        workpiece.InnerText = dt.Rows[i][2].ToString() == "" ? "Workpiece" : dt.Rows[i][2].ToString();
                    }
                    if (dt.Rows[i][0].ToString() == "5")
                    {
                        operationalParam.InnerText = dt.Rows[i][2].ToString() == "" ? "Operational Parameters" : dt.Rows[i][2].ToString();
                    }
                    if (dt.Rows[i][0].ToString() == "6")
                    {
                        qualityParam.InnerText = dt.Rows[i][2].ToString() == "" ? "Quality Parameters" : dt.Rows[i][2].ToString();
                    }
                }

                listparameterDependencie = DBAccess.getDependencyParameterList();
               
                listinputParameters = DBAccess.GetParameters("","","");

            }
            if (Session["EmpName"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }


            //Page.Form.Attributes.Add("enctype", "multipart/form-data");
        }


        private void bindSdocID()
        {
            List<string> sdoclist = null;            sdoclist = DBAccess.getSDocForDelete("Delete", "SDocList");
            sdoclist.Insert(0, "");            var builder = new System.Text.StringBuilder();            if (sdoclist.Count > 0)            {                for (int i = 0; i < sdoclist.Count; i++)                {                    if (i == 0)                    {                        txtViewSdocid.Text = sdoclist[i].ToString();                    }                    builder.Append(String.Format("<option style='font-weight:unset' value='{0}'>", sdoclist[i].ToString()));                }            }            else            {                txtViewSdocid.Text = "";            }            SdocList.InnerHtml = builder.ToString();
        }



        private void BindData(string id)
        {


            bindDataInputModule(id);

            if (id != "")
            {
                string result = DBAccess.getSDocIdStatus(id);

                #region -----GI--------
                for (int i = 0; i < lvGeneralInfo.Items.Count; i++)
                {

                    if ((lvGeneralInfo.Items[i].FindControl("giobjectType") as Label).Text == "CheckBox")
                    {
                        (lvGeneralInfo.Items[i].FindControl("gicbvalue") as CheckBox).Attributes["onclick"] = "return false";

                    }
                    if ((lvGeneralInfo.Items[i].FindControl("giobjectType") as Label).Text == "Drop Down")
                    {
                        (lvGeneralInfo.Items[i].FindControl("giddlvalue") as DropDownList).Enabled = false;
                    }
                    if ((lvGeneralInfo.Items[i].FindControl("giobjectType") as Label).Text == "TextBox")
                    {
                        if ((lvGeneralInfo.Items[i].FindControl("giDateType") as Label).Text == "Date")
                        {
                            (lvGeneralInfo.Items[i].FindControl("gitxtDate") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvGeneralInfo.Items[i].FindControl("giDateType") as Label).Text == "Integer")
                        {
                            (lvGeneralInfo.Items[i].FindControl("gitxtallowNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvGeneralInfo.Items[i].FindControl("giDateType") as Label).Text == "Decimal")
                        {
                            (lvGeneralInfo.Items[i].FindControl("gitxtallowDecimal") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvGeneralInfo.Items[i].FindControl("giDateType") as Label).Text == "Alpha Numeric")
                        {
                            (lvGeneralInfo.Items[i].FindControl("gitxtallowalphaNumeric") as TextBox).ReadOnly = true;
                        }
                        else if ((lvGeneralInfo.Items[i].FindControl("gihdParameterName") as HiddenField).Value == "Comment")
                        {
                            (lvGeneralInfo.Items[i].FindControl("giComments") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).ReadOnly = true;
                        }

                    }




                }
                #endregion

                #region -----MT-----
                for (int i = 0; i < lvMachinetool.Items.Count; i++)
                {

                    if ((lvMachinetool.Items[i].FindControl("mtobjectType") as Label).Text == "CheckBox")
                    {
                        (lvMachinetool.Items[i].FindControl("mtcbvalue") as CheckBox).Attributes["onclick"] = "return false";

                    }
                    if ((lvMachinetool.Items[i].FindControl("mtobjectType") as Label).Text == "Drop Down")
                    {
                        (lvMachinetool.Items[i].FindControl("mtddlvalue") as DropDownList).Enabled = false;
                    }
                    if ((lvMachinetool.Items[i].FindControl("mtobjectType") as Label).Text == "TextBox")
                    {
                        if ((lvMachinetool.Items[i].FindControl("mtDateType") as Label).Text == "Date")
                        {
                            (lvMachinetool.Items[i].FindControl("mttxtDate") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvMachinetool.Items[i].FindControl("mtDateType") as Label).Text == "Integer")
                        {
                            (lvMachinetool.Items[i].FindControl("mttxtallowNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvMachinetool.Items[i].FindControl("mtDateType") as Label).Text == "Decimal")
                        {
                            (lvMachinetool.Items[i].FindControl("mttxtallowDecimal") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvMachinetool.Items[i].FindControl("mtDateType") as Label).Text == "Alpha Numeric")
                        {
                            (lvMachinetool.Items[i].FindControl("mttxtallowalphaNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvMachinetool.Items[i].FindControl("mttxtvalue") as TextBox).ReadOnly = true;
                        }

                    }
                }
                #endregion

                #region ----Consumable--
                for (int i = 0; i < lvConsumable.Items.Count; i++)
                {

                    if ((lvConsumable.Items[i].FindControl("cmobjectType") as Label).Text == "CheckBox")
                    {
                        (lvConsumable.Items[i].FindControl("cmcbvalue") as CheckBox).Attributes["onclick"] = "return false";

                    }
                    if ((lvConsumable.Items[i].FindControl("cmobjectType") as Label).Text == "Drop Down")
                    {
                        (lvConsumable.Items[i].FindControl("cmddlvalue") as DropDownList).Enabled = false;
                    }
                    if ((lvConsumable.Items[i].FindControl("cmobjectType") as Label).Text == "TextBox")
                    {
                        if ((lvConsumable.Items[i].FindControl("cmDateType") as Label).Text == "Date")
                        {
                            (lvConsumable.Items[i].FindControl("cmtxtDate") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvConsumable.Items[i].FindControl("cmDateType") as Label).Text == "Integer")
                        {
                            (lvConsumable.Items[i].FindControl("cmtxtallowNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvConsumable.Items[i].FindControl("cmDateType") as Label).Text == "Decimal")
                        {
                            (lvConsumable.Items[i].FindControl("cmtxtallowDecimal") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvConsumable.Items[i].FindControl("cmDateType") as Label).Text == "Alpha Numeric")
                        {
                            (lvConsumable.Items[i].FindControl("cmtxtallowalphaNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvConsumable.Items[i].FindControl("cmtxtvalue") as TextBox).ReadOnly = true;
                        }

                    }

                }
                #endregion


                #region -----Workpiece--
                for (int i = 0; i < lvWorkpiece.Items.Count; i++)
                {

                    if ((lvWorkpiece.Items[i].FindControl("wpobjectType") as Label).Text == "CheckBox")
                    {
                        (lvWorkpiece.Items[i].FindControl("wpcbvalue") as CheckBox).Attributes["onclick"] = "return false";

                    }
                    if ((lvWorkpiece.Items[i].FindControl("wpobjectType") as Label).Text == "Drop Down")
                    {
                        (lvWorkpiece.Items[i].FindControl("wpddlvalue") as DropDownList).Enabled = false;
                    }
                    if ((lvWorkpiece.Items[i].FindControl("wpobjectType") as Label).Text == "TextBox")
                    {
                        if ((lvWorkpiece.Items[i].FindControl("wpDateType") as Label).Text == "Date")
                        {
                            (lvWorkpiece.Items[i].FindControl("wptxtDate") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvWorkpiece.Items[i].FindControl("wpDateType") as Label).Text == "Integer")
                        {
                            (lvWorkpiece.Items[i].FindControl("wptxtallowNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvWorkpiece.Items[i].FindControl("wpDateType") as Label).Text == "Decimal")
                        {
                            (lvWorkpiece.Items[i].FindControl("wptxtallowDecimal") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvWorkpiece.Items[i].FindControl("wpDateType") as Label).Text == "Alpha Numeric")
                        {
                            (lvWorkpiece.Items[i].FindControl("wptxtallowalphaNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvWorkpiece.Items[i].FindControl("wphdParameterName") as HiddenField).Value == "Hardness")
                        {
                            (lvWorkpiece.Items[i].FindControl("txtHardness") as TextBox).ReadOnly = true;
                            (lvWorkpiece.Items[i].FindControl("ddlHardnessUnit") as DropDownList).Enabled = false;

                            continue;
                        }

                        else
                        {
                            (lvWorkpiece.Items[i].FindControl("wptxtvalue") as TextBox).ReadOnly = true;
                        }

                    }

                    if ((lvWorkpiece.Items[i].FindControl("wpobjectType") as Label).Text == "Image")
                    {

                        (lvWorkpiece.Items[i].FindControl("addnewImage") as LinkButton).Enabled = false;
                        (lvWorkpiece.Items[i].FindControl("addnewImage") as LinkButton).OnClientClick = null;
                        (lvWorkpiece.Items[i].FindControl("LinkButton1") as LinkButton).Enabled = false;
                        (lvWorkpiece.Items[i].FindControl("LinkButton1") as LinkButton).OnClientClick = null;
                        (lvWorkpiece.Items[i].FindControl("imageUpload1") as FileUpload).Enabled = false;
                        (lvWorkpiece.Items[i].FindControl("txtimageName1") as TextBox).ReadOnly = true;
                        (lvWorkpiece.Items[i].FindControl("imagedone") as Button).Enabled = false;
                        (lvWorkpiece.Items[i].FindControl("imagedone") as Button).OnClientClick = null;
                        (lvWorkpiece.Items[i].FindControl("dlImages") as DataList).Enabled = false;
                        DataList dl = (lvWorkpiece.Items[i].FindControl("dlImages") as DataList);
                        for (int d = 0; d < dl.Items.Count; d++)
                        {
                            (dl.Items[d].FindControl("removeImage") as LinkButton).OnClientClick = null;
                        }

                        //(lvWorkpiece.Items[i].FindControl("removeImage") as LinkButton).OnClientClick = null;

                        continue;
                    }

                }
                #endregion


                #region ----OP---
                for (int i = 0; i < lvOperationalParameter.Items.Count; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if(j==0)
                        {
                            (lvOperationalParameter.Items[i].FindControl("ddlopfor") as DropDownList).Enabled = false;
                        }
                        if (j == 1)
                        {
                            // (lvOperationalParameter.Items[i].FindControl("hdnParamIDopFeedRate") as HiddenField).Value;
                            (lvOperationalParameter.Items[i].FindControl("opFeedRateDecimal") as TextBox).ReadOnly = true;
                        }
                        else if (j == 2)
                        {
                            //dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopDOC") as HiddenField).Value;
                            (lvOperationalParameter.Items[i].FindControl("opDOCDecimal") as TextBox).ReadOnly = true;
                        }
                        else if (j == 3)
                        {
                            //dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopDOC") as HiddenField).Value;
                            (lvOperationalParameter.Items[i].FindControl("opFaceDecimal") as TextBox).ReadOnly = true;
                        }
                        else if (j == 4)
                        {
                            // dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopWorkRPM") as HiddenField).Value;
                            (lvOperationalParameter.Items[i].FindControl("opWorkRPMDecimal") as TextBox).ReadOnly = true;
                        }
                        else if (j == 5)
                        {
                            // dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopWheelms") as HiddenField).Value;
                            (lvOperationalParameter.Items[i].FindControl("opWheelmsDecimal") as TextBox).ReadOnly = true;
                        }
                        else if (j == 6)
                        {
                            //dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopWorkms") as HiddenField).Value;
                            (lvOperationalParameter.Items[i].FindControl("opWorkmsDecimal") as TextBox).ReadOnly = true;
                        }
                        else if (j == 7)
                        {
                            //  dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopWheelRPM") as HiddenField).Value;
                            (lvOperationalParameter.Items[i].FindControl("opWheelRPMDecimal") as TextBox).ReadOnly = true;
                        }

                    }

                }

                for (int i = 0; i < lvOperationalParameterGrind.Items.Count; i++)
                {


                    if ((lvOperationalParameterGrind.Items[i].FindControl("opobjectType") as Label).Text == "CheckBox")
                    {
                        (lvOperationalParameterGrind.Items[i].FindControl("opcbvalue") as CheckBox).Attributes["onclick"] = "return false";

                    }
                    if ((lvOperationalParameterGrind.Items[i].FindControl("opobjectType") as Label).Text == "Drop Down")
                    {
                        (lvOperationalParameterGrind.Items[i].FindControl("opddlvalue") as DropDownList).Enabled = false;
                    }
                    if ((lvOperationalParameterGrind.Items[i].FindControl("opobjectType") as Label).Text == "TextBox")
                    {
                        if ((lvOperationalParameterGrind.Items[i].FindControl("opDateType") as Label).Text == "Date")
                        {
                            (lvOperationalParameterGrind.Items[i].FindControl("optxtDate") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvOperationalParameterGrind.Items[i].FindControl("opDateType") as Label).Text == "Integer")
                        {
                            (lvOperationalParameterGrind.Items[i].FindControl("optxtallowNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvOperationalParameterGrind.Items[i].FindControl("opDateType") as Label).Text == "Decimal")
                        {
                            (lvOperationalParameterGrind.Items[i].FindControl("optxtallowDecimal") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvOperationalParameterGrind.Items[i].FindControl("opDateType") as Label).Text == "Alpha Numeric")
                        {
                            (lvOperationalParameterGrind.Items[i].FindControl("optxtallowalphaNumeric") as TextBox).ReadOnly = true;
                        }
                        //Add Hardness 
                        else
                        {
                            (lvOperationalParameterGrind.Items[i].FindControl("optxtvalue") as TextBox).ReadOnly = true;
                        }

                    }

                }



                //dreassing
                for (int i = 0; i < lvOPDressing.Items.Count; i++)
                {


                    if ((lvOPDressing.Items[i].FindControl("opobjectType") as Label).Text == "CheckBox")
                    {
                        (lvOPDressing.Items[i].FindControl("opcbvalue") as CheckBox).Attributes["onclick"] = "return false";

                    }
                    if ((lvOPDressing.Items[i].FindControl("opobjectType") as Label).Text == "Drop Down")
                    {
                        (lvOPDressing.Items[i].FindControl("opddlvalue") as DropDownList).Enabled = false;
                    }
                    if ((lvOPDressing.Items[i].FindControl("opobjectType") as Label).Text == "TextBox")
                    {
                        if ((lvOPDressing.Items[i].FindControl("opDateType") as Label).Text == "Date")
                        {
                            (lvOPDressing.Items[i].FindControl("optxtDate") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvOPDressing.Items[i].FindControl("opDateType") as Label).Text == "Integer")
                        {
                            (lvOPDressing.Items[i].FindControl("optxtallowNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvOPDressing.Items[i].FindControl("opDateType") as Label).Text == "Decimal")
                        {
                            (lvOPDressing.Items[i].FindControl("optxtallowDecimal") as TextBox).ReadOnly = true;
                        }
                        else
                        if ((lvOPDressing.Items[i].FindControl("opDateType") as Label).Text == "Alpha Numeric")
                        {
                            (lvOPDressing.Items[i].FindControl("optxtallowalphaNumeric") as TextBox).ReadOnly = true;
                        }
                        //Add Hardness 
                        else
                        {
                            (lvOPDressing.Items[i].FindControl("optxtvalue") as TextBox).ReadOnly = true;
                        }

                    }

                }

                #endregion


                #region ----QP---
                for (int i = 0; i < lvQualityParameter.Items.Count; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (j == 0)
                        {
                            //  dataipmodule.PrameterId = (lvQualityParameter.Items[i].FindControl("hdnParamIDqpTargetLower") as HiddenField).Value;
                            (lvQualityParameter.Items[i].FindControl("qpTargetLower") as TextBox).ReadOnly = true;
                        }
                        else if (j == 1)
                        {
                            //   dataipmodule.PrameterId = (lvQualityParameter.Items[i].FindControl("hdnParamIDqpTargetUppper") as HiddenField).Value;
                            (lvQualityParameter.Items[i].FindControl("qpTargetUppper") as TextBox).ReadOnly = true;
                        }
                        else if (j == 2)
                        {
                            //  dataipmodule.PrameterId = (lvQualityParameter.Items[i].FindControl("hdnParamIDqpAchievedLower") as HiddenField).Value;
                            (lvQualityParameter.Items[i].FindControl("qpAchievedLower") as TextBox).ReadOnly = true;
                        }
                        else if (j == 3)
                        {
                            // dataipmodule.PrameterId = (lvQualityParameter.Items[i].FindControl("hdnParamIDAchievedUppper") as HiddenField).Value;
                            (lvQualityParameter.Items[i].FindControl("qpAchievedUppper") as TextBox).ReadOnly = true;
                        }

                    }

                }
                #endregion

                if (id.Replace("SDoc", "").Split('_')[0] == "000000")
                {
                    //allowEdit.Visible = false;
                    //editInputModule.Visible = false;
                    //saveInputModule.Visible = true;
                    //btnDeleteSDoc.Visible = true;
                    cbTemplate.Visible = true;
                    cbTemplate.Checked = true;
                    cbTemplate.Attributes["onclick"] = "return false";
                   

                }
                else
                {
                    cbTemplate.Visible = true;
                    cbTemplate.Checked = false;
                    cbTemplate.Attributes["onclick"] = "return true";
                    
                }

                if (result == "Locked")
                {
                    allowEdit.Visible = false;
                    editInputModule.Visible = false;
                    lockstatus.Visible = true;
                }
                else
                {
                    allowEdit.Visible = true;
                    editInputModule.Visible = true;
                    lockstatus.Visible = false;
                }

                saveInputModule.Visible = true;
                btnDeleteSDoc.Visible = true;

            }
            else
            {
                allowEdit.Visible = false;
                editInputModule.Visible = true;
                saveInputModule.Visible = false;
                btnDeleteSDoc.Visible = false;
                cbTemplate.Visible = false;
                lockstatus.Visible = false;

            }

            if (Session["ATKSDocID"] != null)
            {
                txtViewSdocid.Text = Session["ATKSDocID"].ToString();
            }

        }

        private void bindDataInputModule(string id)
        {
            #region ---GI--

            List<DataInputModuleParameter> listParameter = new List<DataInputModuleParameter>();
            DataInputModuleParameter prameter = new DataInputModuleParameter();
            listParameter = DBAccess.getDataInputModuleData(id, "General Information", "");
            if (listParameter.Count % 2 != 0)
            {
                DataInputModuleParameter parameter = new DataInputModuleParameter();
                parameter.Prameter = "";
                parameter.CustomeName = "";
                parameter.PrameterId = "";
                parameter.Value = "";
                parameter.ObjectType = "";
                parameter.Datatype = "";
                parameter.LimitRange = "";
                parameter.Recommendation = "";
                parameter.Image = "";
                parameter.CalculatedFlag = "";
                parameter.DependancyFlag = "";
                parameter.Dependancy = "";
                parameter.IndependentParameter = "";
                parameter.Mandatory = "";
                listParameter.Add(parameter);
            }
            List<DataInputModuleParameter> GIlistParameter = new List<DataInputModuleParameter>();            decimal gidiv = Math.Ceiling(Convert.ToDecimal(listParameter.Count / 2));            int gik1 = Decimal.ToInt32(gidiv);            for (int k = 0; k < gidiv; k++)            {                GIlistParameter.Add(listParameter[k]);                if ((listParameter.Count % 2) != 0)                {                    if (k != gidiv - 1)                    {                        GIlistParameter.Add(listParameter[gik1]);                        gik1++;                    }                }                else                {                    GIlistParameter.Add(listParameter[gik1]);                    gik1++;                }            }
            lvGeneralInfo.DataSource = GIlistParameter;
            lvGeneralInfo.DataBind();
            //if (lvGeneralInfo.Items.Count == 0)
            //{
            //    optionsSaveBtn.Visible = false;
            //    optionTableHeader.Visible = false;
            //    optionsClearBtn.Visible = false;
            //}
            //  hiddenListviewLength.Value = optionslistview.Items.Count.ToString();
            int i = 0;
            foreach (DataInputModuleParameter data in GIlistParameter)
            {
                if (data.Mandatory == "1")
                {
                    (lvGeneralInfo.Items[i].FindControl("giMandatory") as Label).ForeColor = System.Drawing.Color.Red;
                }
                else if (data.Mandatory == "2")
                {
                    (lvGeneralInfo.Items[i].FindControl("giMandatory") as Label).ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    (lvGeneralInfo.Items[i].FindControl("giMandatory") as Label).Visible = false;
                }

                if (data.Image != "")
                {
                    (lvGeneralInfo.Items[i].FindControl("giimgRecommendation") as Image).ImageUrl = data.Image;
                }
                else
                {
                    (lvGeneralInfo.Items[i].FindControl("giimgRecommendation") as Image).Visible = false;
                }

                if (data.ObjectType == "CheckBox")
                {
                    if (data.Value == "No" || data.Value == "")
                    {
                        (lvGeneralInfo.Items[i].FindControl("gicbvalue") as CheckBox).Checked = false;

                    }
                    else
                    {
                        (lvGeneralInfo.Items[i].FindControl("gicbvalue") as CheckBox).Checked = true;
                    }
                  (lvGeneralInfo.Items[i].FindControl("giddlvalue") as DropDownList).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("gitxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("gitxtallowDecimal") as TextBox).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("gitxtallowNumeric") as TextBox).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("gitxtDate") as TextBox).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("giComments") as TextBox).Visible = false;
                }

                if (data.ObjectType == "TextBox")
                {
                    if (data.Datatype == "Date")
                    {
                        //if(data.Value=="")
                        // {
                        //     (lvGeneralInfo.Items[i].FindControl("gitxtDate") as TextBox).Text = data.Value;
                        // }
                        // else
                        // {
                        //     DateTime dt = DateTime.ParseExact(data.Value, "yyyy-MM-ddTHH:mm", null);
                        //     (lvGeneralInfo.Items[i].FindControl("gitxtDate") as TextBox).Text = dt.ToString();
                        // }
                        //string txtdate = "";
                        //if (!string.IsNullOrEmpty(data.Value))
                        //{

                        //    DateTime now = DateTime.ParseExact(data.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //    txtdate = now.ToString("yyyy-MM-ddTHH:mm");
                        //}
                        DateTime dt;
                        string txtdate = "";
                        if (data.Value != "")
                        {
                            if (DateTime.TryParseExact(data.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "yyyy-MM-ddTHH:mm", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd-MM-yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                            {
                                txtdate = dt.ToString("yyyy-MM-ddTHH:mm");
                            }
                        }


                        //  (lvGeneralInfo.Items[i].FindControl("gitxtDate") as TextBox).Text = string.IsNullOrEmpty(data.Value)?"":(DateTime.ParseExact(data.Value, "dd/MM/yyyy", CultureInfo.InvariantCulture)).ToString("yyyy-MM-ddTHH:mm");
                        (lvGeneralInfo.Items[i].FindControl("gitxtDate") as TextBox).Text = txtdate;

                        (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtallowNumeric") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtallowDecimal") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtallowalphaNumeric") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("giComments") as TextBox).Visible = false;
                    }
                    else if (data.Datatype == "Integer")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvGeneralInfo.Items[i].FindControl("gitxtallowNumeric") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvGeneralInfo.Items[i].FindControl("gitxtallowNumeric") as TextBox).ReadOnly = true;
                        }
                        (lvGeneralInfo.Items[i].FindControl("gitxtallowNumeric") as TextBox).Text = data.Value;
                        (lvGeneralInfo.Items[i].FindControl("gitxtDate") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtallowDecimal") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtallowalphaNumeric") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("giComments") as TextBox).Visible = false;
                    }
                    else if (data.Datatype == "Decimal")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvGeneralInfo.Items[i].FindControl("gitxtallowDecimal") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvGeneralInfo.Items[i].FindControl("gitxtallowDecimal") as TextBox).ReadOnly = true;
                        }
                        (lvGeneralInfo.Items[i].FindControl("gitxtallowDecimal") as TextBox).Text = data.Value;
                        (lvGeneralInfo.Items[i].FindControl("gitxtallowNumeric") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtDate") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtallowalphaNumeric") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("giComments") as TextBox).Visible = false;
                    }
                    else if (data.Datatype == "Alpha Numeric")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvGeneralInfo.Items[i].FindControl("gitxtallowalphaNumeric") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvGeneralInfo.Items[i].FindControl("gitxtallowalphaNumeric") as TextBox).ReadOnly = true;
                        }
                        (lvGeneralInfo.Items[i].FindControl("gitxtallowalphaNumeric") as TextBox).Text = data.Value;
                        (lvGeneralInfo.Items[i].FindControl("gitxtallowDecimal") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtallowNumeric") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtDate") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("giComments") as TextBox).Visible = false;

                    }
                    else if (data.Prameter == "Comment")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvGeneralInfo.Items[i].FindControl("giComments") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvGeneralInfo.Items[i].FindControl("giComments") as TextBox).ReadOnly = true;
                        }
                        (lvGeneralInfo.Items[i].FindControl("giComments") as TextBox).Text = data.Value;
                        (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtallowalphaNumeric") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtallowDecimal") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtallowNumeric") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtDate") as TextBox).Visible = false;

                    }
                    else
                    {
                        if (data.Prameter == "SDoc ID")
                        {
                            (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).ReadOnly = true;
                        }
                        if (data.CalculatedFlag == "True")
                        {
                            (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).ReadOnly = true;
                        }
                        (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).Text = data.Value;
                        (lvGeneralInfo.Items[i].FindControl("gitxtallowalphaNumeric") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtallowDecimal") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtallowNumeric") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("gitxtDate") as TextBox).Visible = false;
                        (lvGeneralInfo.Items[i].FindControl("giComments") as TextBox).Visible = false;
                    }


                   (lvGeneralInfo.Items[i].FindControl("giddlvalue") as DropDownList).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("gicbvalue") as CheckBox).Visible = false;
                }
                if (data.ObjectType == "Drop Down")
                {
                    //if (data.DependancyFlag == "1")
                    //{
                    //    DropDownList ddl1 = (lvGeneralInfo.Items[i].FindControl("giddlvalue") as DropDownList);
                    //    ddl1.DataSource = DBAccess.getInputModuleParameterDetails("General Information", data.PrameterId);
                    //    ddl1.DataTextField = "ParameterDetails";
                    //    ddl1.DataValueField = "ParameterDetails";
                    //    ddl1.DataBind();
                    //    ddl1.Items.Insert(0, new ListItem("Select " + data.Prameter, ""));
                    //    ddl1.Text = data.Value;

                    //    string param2Id = data.PrameterId;
                    //    string param2value = data.Value;
                    //    string param1Id, param1;
                    //    List<ParameterDependency> listParamDEpend = DBAccess.IMgetDependencyParametervalue(param2Id, param2value, out param1, out param1Id);

                    //    if (listParamDEpend.Count == 0)
                    //    {
                    //        listParamDEpend = DBAccess.IMgetDependencyParameter(param2Id, param2value);
                    //        if (listParamDEpend.Count > 0)
                    //        {
                    //            for (int pd = 0; pd < lvGeneralInfo.Items.Count; pd++)
                    //            {
                    //                if (listParamDEpend[0].ParameterId1 == (lvGeneralInfo.Items[pd].FindControl("giParameterID") as Label).Text)
                    //                {
                    //                    DropDownList ddl2 = (lvGeneralInfo.Items[pd].FindControl("giddlvalue") as DropDownList);
                    //                    ddl2.Items.Insert(0, new ListItem("Select " + listParamDEpend[0].Parameter1, ""));

                    //                }
                    //            }
                    //        }
                    //    }

                    //    for (int pd = 0; pd < lvGeneralInfo.Items.Count; pd++)
                    //    {
                    //        if (param1Id == (lvGeneralInfo.Items[pd].FindControl("giParameterID") as Label).Text)
                    //        {
                    //            DropDownList ddl2 = (lvGeneralInfo.Items[pd].FindControl("giddlvalue") as DropDownList);
                    //            ddl2.DataSource = listParamDEpend;
                    //            ddl2.DataTextField = "Parameter1Value";
                    //            ddl2.DataValueField = "Parameter1Value";
                    //            ddl2.DataBind();
                    //            ddl2.Items.Insert(0, new ListItem("Select " + param1, ""));
                    //            string value = (from v in GIlistParameter where v.PrameterId == param1Id select v.Value).First();
                    //            if (value != "")
                    //            {
                    //                ddl2.Text = value;
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (data.DependancyFlag == "2")
                    //    {
                    //        (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).Visible = false;
                    //        (lvGeneralInfo.Items[i].FindControl("gitxtallowalphaNumeric") as TextBox).Visible = false;
                    //        (lvGeneralInfo.Items[i].FindControl("gitxtallowDecimal") as TextBox).Visible = false;
                    //        (lvGeneralInfo.Items[i].FindControl("gitxtallowNumeric") as TextBox).Visible = false;
                    //        (lvGeneralInfo.Items[i].FindControl("gitxtDate") as TextBox).Visible = false;
                    //        (lvGeneralInfo.Items[i].FindControl("gicbvalue") as CheckBox).Visible = false;
                    //        (lvGeneralInfo.Items[i].FindControl("giComments") as TextBox).Visible = false;
                    //        i++;
                    //        continue;
                    //    }
                    DropDownList ddl1 = (lvGeneralInfo.Items[i].FindControl("giddlvalue") as DropDownList);
                    ddl1.DataSource = DBAccess.getInputModuleParameterDetails("General Information", data.PrameterId);
                    ddl1.DataTextField = "ParameterDetails";
                    ddl1.DataValueField = "ParameterDetails";
                    ddl1.DataBind();
                    ddl1.Items.Insert(0, new ListItem("Select " + data.Prameter, ""));
                    ddl1.Text = data.Value;
                    //}

                    (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("gitxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("gitxtallowDecimal") as TextBox).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("gitxtallowNumeric") as TextBox).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("gitxtDate") as TextBox).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("gicbvalue") as CheckBox).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("giComments") as TextBox).Visible = false;
                }
                if (data.ObjectType == "")
                {
                    (lvGeneralInfo.Items[i].FindControl("gicbvalue") as CheckBox).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("giddlvalue") as DropDownList).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("gitxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("gitxtallowDecimal") as TextBox).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("gitxtallowNumeric") as TextBox).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("gitxtDate") as TextBox).Visible = false;
                    (lvGeneralInfo.Items[i].FindControl("giComments") as TextBox).Visible = false;
                }
                i++;

            }
            #endregion


            #region ---MT--

            listParameter = DBAccess.getDataInputModuleData(id, "Machine Tool", "");
            if (listParameter.Count % 2 != 0)
            {
                DataInputModuleParameter parameter = new DataInputModuleParameter();
                parameter.Prameter = "";
                parameter.PrameterId = "";
                parameter.CustomeName = "";
                parameter.Value = "";
                parameter.ObjectType = "";
                parameter.Datatype = "";
                parameter.LimitRange = "";
                parameter.Recommendation = "";
                parameter.Image = "";
                parameter.CalculatedFlag = "";
                parameter.DependancyFlag = "";
                parameter.Dependancy = "";
                parameter.IndependentParameter = "";
                parameter.Mandatory = "";
                listParameter.Add(parameter);
            }
            List<DataInputModuleParameter> newlistParameter = new List<DataInputModuleParameter>();            decimal div = Math.Ceiling(Convert.ToDecimal(listParameter.Count / 2));            int k1 = Decimal.ToInt32(div);            for (int k = 0; k < div; k++)            {                newlistParameter.Add(listParameter[k]);                if ((listParameter.Count % 2) != 0)                {                    if (k != div - 1)                    {                        newlistParameter.Add(listParameter[k1]);                        k1++;                    }                }                else                {                    newlistParameter.Add(listParameter[k1]);                    k1++;                }            }            lvMachinetool.DataSource = newlistParameter;            lvMachinetool.DataBind();
            int m = 0;
            foreach (DataInputModuleParameter data in newlistParameter)
            {
                if (data.Mandatory == "1")
                {
                    (lvMachinetool.Items[m].FindControl("mtMandatory") as Label).ForeColor = System.Drawing.Color.Red;
                }
                else if (data.Mandatory == "2")
                {
                    (lvMachinetool.Items[m].FindControl("mtMandatory") as Label).ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    (lvMachinetool.Items[m].FindControl("mtMandatory") as Label).Visible = false;
                }
                if (data.Image != "")
                {
                    (lvMachinetool.Items[m].FindControl("mtimgRecommendation") as Image).ImageUrl = data.Image;
                }
                else
                {
                    (lvMachinetool.Items[m].FindControl("mtimgRecommendation") as Image).Visible = false;
                }
                if (data.ObjectType == "CheckBox")
                {
                    if (data.Value == "No" || data.Value == "")
                    {
                        (lvMachinetool.Items[m].FindControl("mtcbvalue") as CheckBox).Checked = false;

                    }
                    else
                    {
                        (lvMachinetool.Items[m].FindControl("mtcbvalue") as CheckBox).Checked = true;
                    }
                  (lvMachinetool.Items[m].FindControl("mtddlvalue") as DropDownList).Visible = false;
                    (lvMachinetool.Items[m].FindControl("mttxtvalue") as TextBox).Visible = false;
                    (lvMachinetool.Items[m].FindControl("mttxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvMachinetool.Items[m].FindControl("mttxtallowDecimal") as TextBox).Visible = false;
                    (lvMachinetool.Items[m].FindControl("mttxtallowNumeric") as TextBox).Visible = false;
                    (lvMachinetool.Items[m].FindControl("mttxtDate") as TextBox).Visible = false;
                }

                if (data.ObjectType == "TextBox")
                {

                    if (data.Datatype == "Date")
                    {
                        DateTime dt;
                        string txtdate = "";
                        if (data.Value != "")
                        {
                            if (DateTime.TryParseExact(data.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "yyyy-MM-ddTHH:mm", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd-MM-yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                            {
                                txtdate = dt.ToString("yyyy-MM-ddTHH:mm");
                            }
                        }
                        (lvMachinetool.Items[m].FindControl("mttxtDate") as TextBox).Text = txtdate;
                        //(lvMachinetool.Items[m].FindControl("mttxtDate") as TextBox).Text = data.Value == "" ? "" : Convert.ToDateTime(data.Value).ToString("yyyy-MM-ddTHH:mm");
                        (lvMachinetool.Items[m].FindControl("mttxtvalue") as TextBox).Visible = false;
                        (lvMachinetool.Items[m].FindControl("mttxtallowNumeric") as TextBox).Visible = false;
                        (lvMachinetool.Items[m].FindControl("mttxtallowDecimal") as TextBox).Visible = false;
                        (lvMachinetool.Items[m].FindControl("mttxtallowalphaNumeric") as TextBox).Visible = false;
                    }
                    else if (data.Datatype == "Integer")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvMachinetool.Items[m].FindControl("mttxtallowNumeric") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvMachinetool.Items[m].FindControl("mttxtallowNumeric") as TextBox).ReadOnly = true;
                        }
                        (lvMachinetool.Items[m].FindControl("mttxtallowNumeric") as TextBox).Text = data.Value;
                        (lvMachinetool.Items[m].FindControl("mttxtDate") as TextBox).Visible = false;
                        (lvMachinetool.Items[m].FindControl("mttxtvalue") as TextBox).Visible = false;
                        (lvMachinetool.Items[m].FindControl("mttxtallowDecimal") as TextBox).Visible = false;
                        (lvMachinetool.Items[m].FindControl("mttxtallowalphaNumeric") as TextBox).Visible = false;

                    }
                    else if (data.Datatype == "Decimal")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvMachinetool.Items[m].FindControl("mttxtallowDecimal") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvMachinetool.Items[m].FindControl("mttxtallowDecimal") as TextBox).ReadOnly = true;
                        }
                        (lvMachinetool.Items[m].FindControl("mttxtallowDecimal") as TextBox).Text = data.Value;
                        (lvMachinetool.Items[m].FindControl("mttxtallowNumeric") as TextBox).Visible = false;
                        (lvMachinetool.Items[m].FindControl("mttxtDate") as TextBox).Visible = false;
                        (lvMachinetool.Items[m].FindControl("mttxtvalue") as TextBox).Visible = false;
                        (lvMachinetool.Items[m].FindControl("mttxtallowalphaNumeric") as TextBox).Visible = false;
                    }
                    else if (data.Datatype == "Alpha Numeric")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvMachinetool.Items[m].FindControl("mttxtallowalphaNumeric") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvMachinetool.Items[m].FindControl("mttxtallowalphaNumeric") as TextBox).ReadOnly = true;
                        }
                        (lvMachinetool.Items[m].FindControl("mttxtallowalphaNumeric") as TextBox).Text = data.Value;
                        (lvMachinetool.Items[m].FindControl("mttxtallowDecimal") as TextBox).Visible = false;
                        (lvMachinetool.Items[m].FindControl("mttxtallowNumeric") as TextBox).Visible = false;
                        (lvMachinetool.Items[m].FindControl("mttxtDate") as TextBox).Visible = false;
                        (lvMachinetool.Items[m].FindControl("mttxtvalue") as TextBox).Visible = false;

                    }
                    else
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvMachinetool.Items[m].FindControl("mttxtvalue") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvMachinetool.Items[m].FindControl("mttxtvalue") as TextBox).ReadOnly = true;
                        }
                        (lvMachinetool.Items[m].FindControl("mttxtvalue") as TextBox).Text = data.Value;
                        (lvMachinetool.Items[m].FindControl("mttxtallowalphaNumeric") as TextBox).Visible = false;
                        (lvMachinetool.Items[m].FindControl("mttxtallowDecimal") as TextBox).Visible = false;
                        (lvMachinetool.Items[m].FindControl("mttxtallowNumeric") as TextBox).Visible = false;
                        (lvMachinetool.Items[m].FindControl("mttxtDate") as TextBox).Visible = false;
                    }


                    (lvMachinetool.Items[m].FindControl("mtddlvalue") as DropDownList).Visible = false;
                    (lvMachinetool.Items[m].FindControl("mtcbvalue") as CheckBox).Visible = false;
                }
                if (data.ObjectType == "Drop Down")
                {
                    //if (data.DependancyFlag == "1")
                    //{
                    //    DropDownList ddl1 = (lvMachinetool.Items[m].FindControl("mtddlvalue") as DropDownList);
                    //    ddl1.DataSource = DBAccess.getInputModuleParameterDetails("Machine Tool", data.PrameterId);
                    //    ddl1.DataTextField = "ParameterDetails";
                    //    ddl1.DataValueField = "ParameterDetails";
                    //    ddl1.DataBind();
                    //    ddl1.Items.Insert(0, new ListItem("Select " + data.Prameter, ""));
                    //    if (data.Value != "")
                    //    {
                    //        ddl1.Text = data.Value;
                    //    }


                    //    string param2Id = data.PrameterId;
                    //    string param2value = data.Value;
                    //    string param1Id, param1;
                    //    List<ParameterDependency> listParamDEpend = DBAccess.IMgetDependencyParametervalue(param2Id, param2value, out param1, out param1Id);

                    //    if(listParamDEpend.Count==0)
                    //    {
                    //        listParamDEpend = DBAccess.IMgetDependencyParameter(param2Id, param2value);
                    //        if(listParamDEpend.Count>0)
                    //        {
                    //            for (int pd = 0; pd < lvMachinetool.Items.Count; pd++)
                    //            {
                    //                if (listParamDEpend[0].ParameterId1 == (lvMachinetool.Items[pd].FindControl("mtParameterID") as Label).Text)
                    //                {
                    //                    DropDownList ddl2 = (lvMachinetool.Items[pd].FindControl("mtddlvalue") as DropDownList);
                    //                    ddl2.Items.Insert(0, new ListItem("Select " + listParamDEpend[0].Parameter1, ""));

                    //                }
                    //            }
                    //        }
                    //    }

                    //    for (int pd = 0; pd < lvMachinetool.Items.Count; pd++)
                    //    {
                    //        if (param1Id == (lvMachinetool.Items[pd].FindControl("mtParameterID") as Label).Text)
                    //        {
                    //            DropDownList ddl2 = (lvMachinetool.Items[pd].FindControl("mtddlvalue") as DropDownList);
                    //            ddl2.DataSource = listParamDEpend;
                    //            ddl2.DataTextField = "Parameter1Value";
                    //            ddl2.DataValueField = "Parameter1Value";
                    //            ddl2.DataBind();
                    //            ddl2.Items.Insert(0, new ListItem("Select " + param1, ""));
                    //            string value = (from v in newlistParameter where v.PrameterId == param1Id select v.Value).First();
                    //            if (value != "")
                    //            {
                    //                ddl2.Text = value;
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (data.DependancyFlag == "2")
                    //    {
                    //        (lvMachinetool.Items[m].FindControl("mttxtvalue") as TextBox).Visible = false;
                    //        (lvMachinetool.Items[m].FindControl("mttxtallowalphaNumeric") as TextBox).Visible = false;
                    //        (lvMachinetool.Items[m].FindControl("mttxtallowDecimal") as TextBox).Visible = false;
                    //        (lvMachinetool.Items[m].FindControl("mttxtallowNumeric") as TextBox).Visible = false;
                    //        (lvMachinetool.Items[m].FindControl("mttxtDate") as TextBox).Visible = false;
                    //        (lvMachinetool.Items[m].FindControl("mtcbvalue") as CheckBox).Visible = false;
                    //        m++;
                    //        continue;
                    //    }
                    DropDownList ddl1 = (lvMachinetool.Items[m].FindControl("mtddlvalue") as DropDownList);
                    ddl1.DataSource = DBAccess.getInputModuleParameterDetails("Machine Tool", data.PrameterId);
                    ddl1.DataTextField = "ParameterDetails";
                    ddl1.DataValueField = "ParameterDetails";
                    ddl1.DataBind();
                    ddl1.Items.Insert(0, new ListItem("Select " + data.Prameter, ""));
                    if (data.Value != "")
                    {
                        ddl1.Text = data.Value;
                    }
                    //}
                      (lvMachinetool.Items[m].FindControl("mttxtvalue") as TextBox).Visible = false;
                    (lvMachinetool.Items[m].FindControl("mttxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvMachinetool.Items[m].FindControl("mttxtallowDecimal") as TextBox).Visible = false;
                    (lvMachinetool.Items[m].FindControl("mttxtallowNumeric") as TextBox).Visible = false;
                    (lvMachinetool.Items[m].FindControl("mttxtDate") as TextBox).Visible = false;
                    (lvMachinetool.Items[m].FindControl("mtcbvalue") as CheckBox).Visible = false;
                }
                if (data.ObjectType == "")
                {
                    (lvMachinetool.Items[m].FindControl("mtddlvalue") as DropDownList).Visible = false;
                    (lvMachinetool.Items[m].FindControl("mttxtvalue") as TextBox).Visible = false;
                    (lvMachinetool.Items[m].FindControl("mttxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvMachinetool.Items[m].FindControl("mttxtallowDecimal") as TextBox).Visible = false;
                    (lvMachinetool.Items[m].FindControl("mttxtallowNumeric") as TextBox).Visible = false;
                    (lvMachinetool.Items[m].FindControl("mttxtDate") as TextBox).Visible = false;
                    (lvMachinetool.Items[m].FindControl("mtcbvalue") as CheckBox).Visible = false;
                }
                m++;

            }
            #endregion

            #region ---Consumable--

            listParameter = DBAccess.getDataInputModuleData(id, "Consumables Details", "");
            if (listParameter.Count % 2 != 0)
            {
                DataInputModuleParameter parameter = new DataInputModuleParameter();
                parameter.Prameter = "";
                parameter.PrameterId = "";
                parameter.CustomeName = "";
                parameter.Value = "";
                parameter.ObjectType = "";
                parameter.Datatype = "";
                parameter.LimitRange = "";
                parameter.Recommendation = "";
                parameter.Image = "";
                parameter.CalculatedFlag = "";
                parameter.DependancyFlag = "";
                parameter.Dependancy = "";
                parameter.IndependentParameter = "";
                parameter.Mandatory = "";
                listParameter.Add(parameter);
            }
            List<DataInputModuleParameter> consumlistParameter = new List<DataInputModuleParameter>();            decimal cdiv = Math.Ceiling(Convert.ToDecimal(listParameter.Count / 2));            int ck1 = Decimal.ToInt32(cdiv);            for (int k = 0; k < cdiv; k++)            {                consumlistParameter.Add(listParameter[k]);                if ((listParameter.Count % 2) != 0)                {                    if (k != cdiv - 1)                    {                        consumlistParameter.Add(listParameter[ck1]);                        ck1++;                    }                }                else                {                    consumlistParameter.Add(listParameter[ck1]);                    ck1++;                }            }            lvConsumable.DataSource = consumlistParameter;            lvConsumable.DataBind();
            int c = 0;
            foreach (DataInputModuleParameter data in consumlistParameter)
            {

                if (data.Mandatory == "1")
                {
                    (lvConsumable.Items[c].FindControl("cmMandatory") as Label).ForeColor = System.Drawing.Color.Red;
                }
                else if (data.Mandatory == "2")
                {
                    (lvConsumable.Items[c].FindControl("cmMandatory") as Label).ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    (lvConsumable.Items[c].FindControl("cmMandatory") as Label).Visible = false;
                }

                if (data.Image != "")
                {
                    (lvConsumable.Items[c].FindControl("cmimgRecommendation") as Image).ImageUrl = data.Image;
                }
                else
                {
                    (lvConsumable.Items[c].FindControl("cmimgRecommendation") as Image).Visible = false;
                }


                if (data.ObjectType == "CheckBox")
                {
                    if (data.Value == "No" || data.Value == "")
                    {
                        (lvConsumable.Items[c].FindControl("cmcbvalue") as CheckBox).Checked = false;

                    }
                    else
                    {
                        (lvConsumable.Items[c].FindControl("cmcbvalue") as CheckBox).Checked = true;
                    }
                  (lvConsumable.Items[c].FindControl("cmddlvalue") as DropDownList).Visible = false;
                    (lvConsumable.Items[c].FindControl("cmtxtvalue") as TextBox).Visible = false;
                    (lvConsumable.Items[c].FindControl("cmtxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvConsumable.Items[c].FindControl("cmtxtallowDecimal") as TextBox).Visible = false;
                    (lvConsumable.Items[c].FindControl("cmtxtallowNumeric") as TextBox).Visible = false;
                    (lvConsumable.Items[c].FindControl("cmtxtDate") as TextBox).Visible = false;
                }

                if (data.ObjectType == "TextBox")
                {
                    if (data.Datatype == "Date")
                    {
                        DateTime dt;
                        string txtdate = "";
                        if (data.Value != "")
                        {
                            if (DateTime.TryParseExact(data.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "yyyy-MM-ddTHH:mm", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd-MM-yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                            {
                                txtdate = dt.ToString("yyyy-MM-ddTHH:mm");
                            }
                        }
                        (lvConsumable.Items[c].FindControl("cmtxtDate") as TextBox).Text = txtdate;
                        //(lvConsumable.Items[c].FindControl("cmtxtDate") as TextBox).Text = data.Value == "" ? "" : Convert.ToDateTime(data.Value).ToString("yyyy-MM-ddTHH:mm");
                        (lvConsumable.Items[c].FindControl("cmtxtvalue") as TextBox).Visible = false;
                        (lvConsumable.Items[c].FindControl("cmtxtallowNumeric") as TextBox).Visible = false;
                        (lvConsumable.Items[c].FindControl("cmtxtallowDecimal") as TextBox).Visible = false;
                        (lvConsumable.Items[c].FindControl("cmtxtallowalphaNumeric") as TextBox).Visible = false;
                    }
                    else if (data.Datatype == "Integer")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvConsumable.Items[c].FindControl("cmtxtallowNumeric") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvConsumable.Items[c].FindControl("cmtxtallowNumeric") as TextBox).ReadOnly = true;
                        }
                        (lvConsumable.Items[c].FindControl("cmtxtallowNumeric") as TextBox).Text = data.Value;
                        (lvConsumable.Items[c].FindControl("cmtxtDate") as TextBox).Visible = false;
                        (lvConsumable.Items[c].FindControl("cmtxtvalue") as TextBox).Visible = false;
                        (lvConsumable.Items[c].FindControl("cmtxtallowDecimal") as TextBox).Visible = false;
                        (lvConsumable.Items[c].FindControl("cmtxtallowalphaNumeric") as TextBox).Visible = false;

                    }
                    else if (data.Datatype == "Decimal")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvConsumable.Items[c].FindControl("cmtxtallowDecimal") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvConsumable.Items[c].FindControl("cmtxtallowDecimal") as TextBox).ReadOnly = true;
                        }
                        (lvConsumable.Items[c].FindControl("cmtxtallowDecimal") as TextBox).Text = data.Value;
                        (lvConsumable.Items[c].FindControl("cmtxtallowNumeric") as TextBox).Visible = false;
                        (lvConsumable.Items[c].FindControl("cmtxtDate") as TextBox).Visible = false;
                        (lvConsumable.Items[c].FindControl("cmtxtvalue") as TextBox).Visible = false;
                        (lvConsumable.Items[c].FindControl("cmtxtallowalphaNumeric") as TextBox).Visible = false;
                    }
                    else if (data.Datatype == "Alpha Numeric")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvConsumable.Items[c].FindControl("cmtxtallowalphaNumeric") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvConsumable.Items[c].FindControl("cmtxtallowalphaNumeric") as TextBox).ReadOnly = true;
                        }
                        (lvConsumable.Items[c].FindControl("cmtxtallowalphaNumeric") as TextBox).Text = data.Value;
                        (lvConsumable.Items[c].FindControl("cmtxtallowDecimal") as TextBox).Visible = false;
                        (lvConsumable.Items[c].FindControl("cmtxtallowNumeric") as TextBox).Visible = false;
                        (lvConsumable.Items[c].FindControl("cmtxtDate") as TextBox).Visible = false;
                        (lvConsumable.Items[c].FindControl("cmtxtvalue") as TextBox).Visible = false;

                    }
                    else
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvConsumable.Items[c].FindControl("cmtxtvalue") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvConsumable.Items[c].FindControl("cmtxtvalue") as TextBox).ReadOnly = true;
                        }
                        (lvConsumable.Items[c].FindControl("cmtxtvalue") as TextBox).Text = data.Value;
                        (lvConsumable.Items[c].FindControl("cmtxtallowalphaNumeric") as TextBox).Visible = false;
                        (lvConsumable.Items[c].FindControl("cmtxtallowDecimal") as TextBox).Visible = false;
                        (lvConsumable.Items[c].FindControl("cmtxtallowNumeric") as TextBox).Visible = false;
                        (lvConsumable.Items[c].FindControl("cmtxtDate") as TextBox).Visible = false;
                    }
                    (lvConsumable.Items[c].FindControl("cmddlvalue") as DropDownList).Visible = false;
                    (lvConsumable.Items[c].FindControl("cmcbvalue") as CheckBox).Visible = false;
                }
                if (data.ObjectType == "Drop Down")
                {

                    //if (data.DependancyFlag == "1")
                    //{
                    //    DropDownList ddl1 = (lvConsumable.Items[c].FindControl("cmddlvalue") as DropDownList);
                    //    ddl1.DataSource = DBAccess.getInputModuleParameterDetails("Consumables Details", data.PrameterId);
                    //    ddl1.DataTextField = "ParameterDetails";
                    //    ddl1.DataValueField = "ParameterDetails";
                    //    ddl1.DataBind();
                    //    ddl1.Items.Insert(0, new ListItem("Select " + data.Prameter, ""));
                    //    if (data.Value != "")
                    //    {
                    //        ddl1.Text = data.Value;
                    //    }

                    //    string param2Id = data.PrameterId;
                    //    string param2value = data.Value;
                    //    string param1Id, param1;
                    //    List<ParameterDependency> listParamDEpend = DBAccess.IMgetDependencyParametervalue(param2Id, param2value, out param1, out param1Id);

                    //    if (listParamDEpend.Count == 0)
                    //    {
                    //        listParamDEpend = DBAccess.IMgetDependencyParameter(param2Id, param2value);
                    //        if (listParamDEpend.Count > 0)
                    //        {
                    //            for (int pd = 0; pd < lvConsumable.Items.Count; pd++)
                    //            {
                    //                if (listParamDEpend[0].ParameterId1 == (lvConsumable.Items[pd].FindControl("cmParameterID") as Label).Text)
                    //                {
                    //                    DropDownList ddl2 = (lvConsumable.Items[pd].FindControl("cmddlvalue") as DropDownList);
                    //                    ddl2.Items.Insert(0, new ListItem("Select " + listParamDEpend[0].Parameter1, ""));

                    //                }
                    //            }
                    //        }
                    //    }

                    //    for (int pd = 0; pd < lvConsumable.Items.Count; pd++)
                    //    {
                    //        if (param1Id == (lvConsumable.Items[pd].FindControl("cmParameterID") as Label).Text)
                    //        {
                    //            DropDownList ddl2 = (lvConsumable.Items[pd].FindControl("cmddlvalue") as DropDownList);
                    //            ddl2.DataSource = listParamDEpend;
                    //            ddl2.DataTextField = "Parameter1Value";
                    //            ddl2.DataValueField = "Parameter1Value";
                    //            ddl2.DataBind();
                    //            ddl2.Items.Insert(0, new ListItem("Select " + param1, ""));
                    //            string value = (from k in consumlistParameter where k.PrameterId == param1Id select k.Value).First();
                    //            if (value != "")
                    //            {
                    //                ddl2.Text = value;
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //if (data.DependancyFlag == "2")
                    //{

                    //    (lvConsumable.Items[c].FindControl("cmtxtvalue") as TextBox).Visible = false;
                    //    (lvConsumable.Items[c].FindControl("cmtxtallowalphaNumeric") as TextBox).Visible = false;
                    //    (lvConsumable.Items[c].FindControl("cmtxtallowDecimal") as TextBox).Visible = false;
                    //    (lvConsumable.Items[c].FindControl("cmtxtallowNumeric") as TextBox).Visible = false;
                    //    (lvConsumable.Items[c].FindControl("cmtxtDate") as TextBox).Visible = false;
                    //    (lvConsumable.Items[c].FindControl("cmcbvalue") as CheckBox).Visible = false;
                    //    c++;
                    //    continue;
                    //}
                    DropDownList ddl1 = (lvConsumable.Items[c].FindControl("cmddlvalue") as DropDownList);
                    ddl1.DataSource = DBAccess.getInputModuleParameterDetails("Consumables Details", data.PrameterId);
                    ddl1.DataTextField = "ParameterDetails";
                    ddl1.DataValueField = "ParameterDetails";
                    ddl1.DataBind();
                    ddl1.Items.Insert(0, new ListItem("Select " + data.Prameter, ""));
                    if (data.Value != "")
                    {
                        ddl1.Text = data.Value;
                    }

                    // }
                    (lvConsumable.Items[c].FindControl("cmtxtvalue") as TextBox).Visible = false;
                    (lvConsumable.Items[c].FindControl("cmtxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvConsumable.Items[c].FindControl("cmtxtallowDecimal") as TextBox).Visible = false;
                    (lvConsumable.Items[c].FindControl("cmtxtallowNumeric") as TextBox).Visible = false;
                    (lvConsumable.Items[c].FindControl("cmtxtDate") as TextBox).Visible = false;
                    (lvConsumable.Items[c].FindControl("cmcbvalue") as CheckBox).Visible = false;
                }
                if (data.ObjectType == "")
                {
                    (lvConsumable.Items[c].FindControl("cmcbvalue") as CheckBox).Visible = false;
                    (lvConsumable.Items[c].FindControl("cmddlvalue") as DropDownList).Visible = false;
                    (lvConsumable.Items[c].FindControl("cmtxtvalue") as TextBox).Visible = false;
                    (lvConsumable.Items[c].FindControl("cmtxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvConsumable.Items[c].FindControl("cmtxtallowDecimal") as TextBox).Visible = false;
                    (lvConsumable.Items[c].FindControl("cmtxtallowNumeric") as TextBox).Visible = false;
                    (lvConsumable.Items[c].FindControl("cmtxtDate") as TextBox).Visible = false;
                }
                c++;

            }
            #endregion

            #region ---Workpiece--

            listParameter = DBAccess.getDataInputModuleData(id, "Workpiece Details", "");
            int listOriginalCout = listParameter.Count;
            // listParameter.Remove(prameter.Prameter.ToString()=="Hardness Unit");
            if (listParameter.Count % 2 != 0)
            {
                DataInputModuleParameter parameter = new DataInputModuleParameter();
                parameter.Prameter = "";
                parameter.PrameterId = "";
                parameter.CustomeName = "";
                parameter.Value = "";
                parameter.ObjectType = "";
                parameter.Datatype = "";
                parameter.LimitRange = "";
                parameter.Recommendation = "";
                parameter.Image = "";
                parameter.CalculatedFlag = "";
                parameter.DependancyFlag = "";
                parameter.Dependancy = "";
                parameter.IndependentParameter = "";
                parameter.Mandatory = "";
                //  listParameter.Add(parameter);
                listParameter.Insert(listOriginalCout - 1, parameter);
            }

            List<DataInputModuleParameter> WPlistParameter = new List<DataInputModuleParameter>();            if (listOriginalCout % 2 == 0)
            {
                decimal wpdiv = Math.Ceiling(Convert.ToDecimal(listParameter.Count / 2));
                int wpk1 = Decimal.ToInt32(wpdiv);
                for (int k = 0; k < wpdiv; k++)
                {
                    WPlistParameter.Add(listParameter[k]);
                    if ((listParameter.Count % 2) != 0)
                    {
                        if (k != wpdiv - 1)
                        {
                            WPlistParameter.Add(listParameter[wpk1]);
                            wpk1++;
                        }
                    }
                    else
                    {
                        WPlistParameter.Add(listParameter[wpk1]);
                        wpk1++;
                    }
                }
            }
            else
            {
                decimal wpdiv = Math.Round(Convert.ToDecimal(listOriginalCout / 2));
                int wpk1 = Decimal.ToInt32(wpdiv);
                for (int k = 0; k < wpdiv; k++)
                {
                    WPlistParameter.Add(listParameter[k]);
                    if ((listParameter.Count % 2) != 0)
                    {
                        if (k != wpdiv - 1)
                        {
                            WPlistParameter.Add(listParameter[wpk1]);
                            wpk1++;
                        }
                    }
                    else
                    {
                        WPlistParameter.Add(listParameter[wpk1]);
                        wpk1++;
                    }
                }
                WPlistParameter.Add(listParameter[listParameter.Count - 2]);
                WPlistParameter.Add(listParameter[listParameter.Count - 1]);
            }                 lvWorkpiece.DataSource = WPlistParameter;            lvWorkpiece.DataBind();
            int w = 0;
            string hardnessUnit = "";
            //foreach (DataInputModuleParameter data in WPlistParameter)
            //{
            //    if (data.Prameter == "Hardness Unit")
            //    {
            //        hardnessUnit = data.Value;
            //    }
            //}


            foreach (DataInputModuleParameter data in WPlistParameter)
            {

                if (data.Mandatory == "1")
                {
                    (lvWorkpiece.Items[w].FindControl("wpMandatory") as Label).ForeColor = System.Drawing.Color.Red;
                }
                else if (data.Mandatory == "2")
                {
                    (lvWorkpiece.Items[w].FindControl("wpMandatory") as Label).ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    (lvWorkpiece.Items[w].FindControl("wpMandatory") as Label).Visible = false;
                }
                if (data.Image != "")
                {
                    (lvWorkpiece.Items[w].FindControl("wpimgRecommendation") as Image).ImageUrl = data.Image;
                }
                else
                {
                    (lvWorkpiece.Items[w].FindControl("wpimgRecommendation") as Image).Visible = false;
                }

                if (data.ObjectType == "CheckBox")
                {
                    if (data.Value == "No" || data.Value == "")
                    {
                        (lvWorkpiece.Items[w].FindControl("wpcbvalue") as CheckBox).Checked = false;

                    }
                    else
                    {
                        (lvWorkpiece.Items[w].FindControl("wpcbvalue") as CheckBox).Checked = true;
                    }
                  (lvWorkpiece.Items[w].FindControl("wpddlvalue") as DropDownList).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wptxtvalue") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wptxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wptxtallowDecimal") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wptxtallowNumeric") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wptxtDate") as TextBox).Visible = false;

                    //(lvWorkpiece.Items[w].FindControl("txtHardness") as TextBox).Visible = false;
                    //(lvWorkpiece.Items[w].FindControl("ddlHardnessUnit") as DropDownList).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("hardness") as HtmlGenericControl).Visible = false;

                    (lvWorkpiece.Items[w].FindControl("image") as HtmlGenericControl).Visible = false;
                }

                if (data.ObjectType == "TextBox")
                {
                    string s2 = data.Prameter;
                    if (data.Datatype == "Date")
                    {
                        DateTime dt;
                        string txtdate = "";
                        if (data.Value != "")
                        {
                            if (DateTime.TryParseExact(data.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "yyyy-MM-ddTHH:mm", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd-MM-yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                            {
                                txtdate = dt.ToString("yyyy-MM-ddTHH:mm");
                            }

                        }
                        (lvWorkpiece.Items[w].FindControl("wptxtDate") as TextBox).Text = txtdate;
                        //(lvWorkpiece.Items[w].FindControl("wptxtDate") as TextBox).Text = data.Value == "" ? "" : Convert.ToDateTime(data.Value).ToString("yyyy-MM-ddTHH:mm");
                        (lvWorkpiece.Items[w].FindControl("wptxtvalue") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtallowNumeric") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtallowDecimal") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtallowalphaNumeric") as TextBox).Visible = false;

                        //(lvWorkpiece.Items[w].FindControl("txtHardness") as TextBox).Visible = false;
                        //(lvWorkpiece.Items[w].FindControl("ddlHardnessUnit") as DropDownList).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("hardness") as HtmlGenericControl).Visible = false;
                    }
                    else if (data.Datatype == "Integer")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvWorkpiece.Items[w].FindControl("wptxtallowNumeric") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvWorkpiece.Items[w].FindControl("wptxtallowNumeric") as TextBox).ReadOnly = true;
                        }
                        (lvWorkpiece.Items[w].FindControl("wptxtallowNumeric") as TextBox).Text = data.Value;
                        (lvWorkpiece.Items[w].FindControl("wptxtDate") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtvalue") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtallowDecimal") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtallowalphaNumeric") as TextBox).Visible = false;

                        // (lvWorkpiece.Items[w].FindControl("txtHardness") as TextBox).Visible = false;
                        //(lvWorkpiece.Items[w].FindControl("ddlHardnessUnit") as DropDownList).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("hardness") as HtmlGenericControl).Visible = false;

                    }
                    else if (data.Datatype == "Decimal")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvWorkpiece.Items[w].FindControl("wptxtallowDecimal") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvWorkpiece.Items[w].FindControl("wptxtallowDecimal") as TextBox).ReadOnly = true;
                        }
                        (lvWorkpiece.Items[w].FindControl("wptxtallowDecimal") as TextBox).Text = data.Value;
                        (lvWorkpiece.Items[w].FindControl("wptxtallowNumeric") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtDate") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtvalue") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtallowalphaNumeric") as TextBox).Visible = false;

                        //(lvWorkpiece.Items[w].FindControl("txtHardness") as TextBox).Visible = false;
                        //(lvWorkpiece.Items[w].FindControl("ddlHardnessUnit") as DropDownList).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("hardness") as HtmlGenericControl).Visible = false;
                    }
                    else if (data.Datatype == "Alpha Numeric")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvWorkpiece.Items[w].FindControl("wptxtallowalphaNumeric") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvWorkpiece.Items[w].FindControl("wptxtallowalphaNumeric") as TextBox).ReadOnly = true;
                        }
                        (lvWorkpiece.Items[w].FindControl("wptxtallowalphaNumeric") as TextBox).Text = data.Value;
                        (lvWorkpiece.Items[w].FindControl("wptxtallowDecimal") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtallowNumeric") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtDate") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtvalue") as TextBox).Visible = false;

                        //(lvWorkpiece.Items[w].FindControl("txtHardness") as TextBox).Visible = false;
                        //(lvWorkpiece.Items[w].FindControl("ddlHardnessUnit") as DropDownList).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("hardness") as HtmlGenericControl).Visible = false;

                    }
                    else if (data.Prameter == "Hardness")
                    {

                        (lvWorkpiece.Items[w].FindControl("hardness") as HtmlGenericControl).Visible = true;
                        (lvWorkpiece.Items[w].FindControl("txtHardness") as TextBox).Text = data.Value;
                        (lvWorkpiece.Items[w].FindControl("ddlHardnessUnit") as DropDownList).Visible = true;

                        DropDownList ddl1 = new DropDownList();
                        ddl1 = (lvWorkpiece.Items[w].FindControl("ddlHardnessUnit") as DropDownList);
                        ddl1.DataSource = DBAccess.getInputModuleParameterDetails("Workpiece Details", "71");
                        ddl1.DataTextField = "ParameterDetails";
                        ddl1.DataValueField = "Limitrange";
                        string s = ddl1.DataValueField;
                        ddl1.DataBind();
                        string ss = Session["hardnessUnit"].ToString();
                        if (Session["hardnessUnit"].ToString() != "")
                        {
                            //ddl1.Text = Session["hardnessUnit"].ToString();
                            //Session["hardnessUnit"] = null;
                            var item = ddl1.Items.FindByText(Session["hardnessUnit"].ToString());                            if (item != null)                            {                                item.Selected = true;                            }                            Session["hardnessUnit"] = null;
                        }

                        ddl1.Items.Insert(0, new ListItem("Select Hardness Unit", ""));


                        (lvWorkpiece.Items[w].FindControl("wptxtallowalphaNumeric") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtallowDecimal") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtallowNumeric") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtDate") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtvalue") as TextBox).Visible = false;
                    }
                    else
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvWorkpiece.Items[w].FindControl("wptxtvalue") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvWorkpiece.Items[w].FindControl("wptxtvalue") as TextBox).ReadOnly = true;
                        }
                        (lvWorkpiece.Items[w].FindControl("wptxtvalue") as TextBox).Text = data.Value;
                        (lvWorkpiece.Items[w].FindControl("wptxtallowalphaNumeric") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtallowDecimal") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtallowNumeric") as TextBox).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("wptxtDate") as TextBox).Visible = false;

                        // (lvWorkpiece.Items[w].FindControl("txtHardness") as TextBox).Visible = false;
                        //(lvWorkpiece.Items[w].FindControl("ddlHardnessUnit") as DropDownList).Visible = false;
                        (lvWorkpiece.Items[w].FindControl("hardness") as HtmlGenericControl).Visible = false;
                    }
                    (lvWorkpiece.Items[w].FindControl("wpddlvalue") as DropDownList).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wpcbvalue") as CheckBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("image") as HtmlGenericControl).Visible = false;
                }
                if (data.ObjectType == "Drop Down")
                {
                    //if (data.DependancyFlag == "1")
                    //{
                    //    DropDownList ddl1 = (lvWorkpiece.Items[w].FindControl("wpddlvalue") as DropDownList);
                    //    ddl1.DataSource = DBAccess.getInputModuleParameterDetails("Workpiece Details", data.PrameterId);
                    //    ddl1.DataTextField = "ParameterDetails";
                    //    ddl1.DataValueField = "ParameterDetails";
                    //    ddl1.DataBind();
                    //    ddl1.Items.Insert(0, new ListItem("Select " + data.Prameter, ""));
                    //    if (data.Value != "")
                    //    {
                    //        ddl1.Text = data.Value;
                    //    }

                    //    string param2Id = data.PrameterId;
                    //    string param2value = data.Value;
                    //    string param1Id, param1;
                    //    List<ParameterDependency> listParamDEpend = DBAccess.IMgetDependencyParametervalue(param2Id, param2value, out param1, out param1Id);

                    //    if (listParamDEpend.Count == 0)
                    //    {
                    //        listParamDEpend = DBAccess.IMgetDependencyParameter(param2Id, param2value);
                    //        if (listParamDEpend.Count > 0)
                    //        {
                    //            for (int pd = 0; pd < lvWorkpiece.Items.Count; pd++)
                    //            {
                    //                if (listParamDEpend[0].ParameterId1 == (lvWorkpiece.Items[pd].FindControl("wpParameterID") as Label).Text)
                    //                {
                    //                    DropDownList ddl2 = (lvWorkpiece.Items[pd].FindControl("wpddlvalue") as DropDownList);
                    //                    ddl2.Items.Insert(0, new ListItem("Select " + listParamDEpend[0].Parameter1, ""));

                    //                }
                    //            }
                    //        }
                    //    }

                    //    for (int pd = 0; pd < lvWorkpiece.Items.Count; pd++)
                    //    {
                    //        if (param1Id == (lvWorkpiece.Items[pd].FindControl("wpParameterID") as Label).Text)
                    //        {
                    //            DropDownList ddl2 = (lvWorkpiece.Items[pd].FindControl("wpddlvalue") as DropDownList);
                    //            ddl2.DataSource = listParamDEpend;
                    //            ddl2.DataTextField = "Parameter1Value";
                    //            ddl2.DataValueField = "Parameter1Value";
                    //            ddl2.DataBind();
                    //            ddl2.Items.Insert(0, new ListItem("Select " + param1, ""));
                    //            string value = (from v in WPlistParameter where v.PrameterId == param1Id select v.Value).First();
                    //            if (value != "")
                    //            {
                    //                ddl2.Text = value;
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (data.DependancyFlag == "2")
                    //    {
                    //        (lvWorkpiece.Items[w].FindControl("wptxtvalue") as TextBox).Visible = false;
                    //        (lvWorkpiece.Items[w].FindControl("wptxtallowalphaNumeric") as TextBox).Visible = false;
                    //        (lvWorkpiece.Items[w].FindControl("wptxtallowDecimal") as TextBox).Visible = false;
                    //        (lvWorkpiece.Items[w].FindControl("wptxtallowNumeric") as TextBox).Visible = false;
                    //        (lvWorkpiece.Items[w].FindControl("wptxtDate") as TextBox).Visible = false;
                    //        (lvWorkpiece.Items[w].FindControl("wpcbvalue") as CheckBox).Visible = false;
                    //        (lvWorkpiece.Items[w].FindControl("hardness") as HtmlGenericControl).Visible = false;
                    //        (lvWorkpiece.Items[w].FindControl("image") as HtmlGenericControl).Visible = false;
                    //        w++;
                    //        continue;
                    //    }
                    DropDownList ddl1 = (lvWorkpiece.Items[w].FindControl("wpddlvalue") as DropDownList);
                    ddl1.DataSource = DBAccess.getInputModuleParameterDetails("Workpiece Details", data.PrameterId);
                    ddl1.DataTextField = "ParameterDetails";
                    ddl1.DataValueField = "ParameterDetails";
                    ddl1.DataBind();
                    ddl1.Items.Insert(0, new ListItem("Select " + data.Prameter, ""));
                    if (data.Value != "")
                    {
                        ddl1.Text = data.Value;
                    }
                    //}

                    (lvWorkpiece.Items[w].FindControl("wptxtvalue") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wptxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wptxtallowDecimal") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wptxtallowNumeric") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wptxtDate") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wpcbvalue") as CheckBox).Visible = false;

                    // (lvWorkpiece.Items[w].FindControl("txtHardness") as TextBox).Visible = false;
                    //(lvWorkpiece.Items[w].FindControl("ddlHardnessUnit") as DropDownList).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("hardness") as HtmlGenericControl).Visible = false;

                    (lvWorkpiece.Items[w].FindControl("image") as HtmlGenericControl).Visible = false;
                }
                if (data.ObjectType == "Image")
                {
                    //(lvWorkpiece.Items[w].FindControl("image") as HtmlGenericControl).Visible=false ;
                    List<WorkpieceImage> listImg = new List<WorkpieceImage>();
                    WorkpieceImage img = null;
                    if (data.Value != "")
                    {
                        string si = data.Value;
                        string[] imageName = data.ImageName.Split(';');
                        string[] imagePath = data.Value.Split(';');

                        for (int im = 0; im < imagePath.Length; im++)
                        {
                            try
                            {
                                if (imagePath[im].StartsWith("~/UploadImages"))
                                {
                                    try
                                    {
                                        img = new WorkpieceImage();
                                        img.wpImageName = imageName[im];
                                        img.wpImagePath = imagePath[im].Substring(2);
                                        listImg.Add(img);
                                    }catch(Exception ex)
                                    {

                                    }
                                   
                                }
                               
                            }
                            catch(Exception ex)
                            {

                            }
                          
                        }

                      (lvWorkpiece.Items[w].FindControl("dlImages") as DataList).DataSource = listImg;
                        (lvWorkpiece.Items[w].FindControl("dlImages") as DataList).DataBind();
                    }


                    //string[] filePaths = Directory.GetFiles(Server.MapPath("~/Images/"));
                    //List<ListItem> files = new List<ListItem>();
                    //foreach (string filePath in filePaths)
                    //{
                    //    string fileName = Path.GetFileName(filePath);
                    //    files.Add(new ListItem(fileName, "~/Images/" + fileName));
                    //}




                    (lvWorkpiece.Items[w].FindControl("wptxtvalue") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wptxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wptxtallowDecimal") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wptxtallowNumeric") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wptxtDate") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wpcbvalue") as CheckBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wpddlvalue") as DropDownList).Visible = false;

                    //(lvWorkpiece.Items[w].FindControl("txtHardness") as TextBox).Visible = false;
                    //(lvWorkpiece.Items[w].FindControl("ddlHardnessUnit") as DropDownList).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("hardness") as HtmlGenericControl).Visible = false;
                }

                if (data.ObjectType == "")
                {

                    (lvWorkpiece.Items[w].FindControl("wptxtvalue") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wptxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wptxtallowDecimal") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wptxtallowNumeric") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wptxtDate") as TextBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wpcbvalue") as CheckBox).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("wpddlvalue") as DropDownList).Visible = false;

                    //(lvWorkpiece.Items[w].FindControl("txtHardness") as TextBox).Visible = false;
                    //(lvWorkpiece.Items[w].FindControl("ddlHardnessUnit") as DropDownList).Visible = false;
                    (lvWorkpiece.Items[w].FindControl("hardness") as HtmlGenericControl).Visible = false;


                    (lvWorkpiece.Items[w].FindControl("image") as HtmlGenericControl).Visible = false;
                }
                w++;

            }
            #endregion

            #region ---Operation Parameter--
            List<DataInputModuleParameter> listParametergrinding = new List<DataInputModuleParameter>();
            listParametergrinding = DBAccess.getDataInputModuleOpearationalParamData(id, "Operational Parameters", "Grinding Parameters");
            lvOperationalParameter.DataSource = listParametergrinding;
            lvOperationalParameter.DataBind();
            int o = 0;
            foreach (DataInputModuleParameter data in listParametergrinding)
            {
                //    string s = data.Image;
                if (data.ImageFeedRate == "" || data.ImageFeedRate == null)
                {
                    (lvOperationalParameter.Items[o].FindControl("opimgRecommendationFeedRate") as Image).Visible = false;

                }
                else
                {

                    (lvOperationalParameter.Items[o].FindControl("opimgRecommendationFeedRate") as Image).ImageUrl = data.ImageFeedRate;
                }
                if (data.ParameterIDFeedRate == "" || data.ParameterIDFeedRate == null)
                {
                    (lvOperationalParameter.Items[o].FindControl("opFeedRatedecimal") as TextBox).Visible = false;
                }
                if (data.MandatoryFeedRate == "1")
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryFeedRate") as Label).ForeColor = System.Drawing.Color.Red;
                }
                else if (data.MandatoryFeedRate == "2")
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryFeedRate") as Label).ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryFeedRate") as Label).Visible = false;
                }


                if (data.ImageDOC == "" || data.ImageDOC == null)
                {
                    (lvOperationalParameter.Items[o].FindControl("opimgRecommendationDOC") as Image).Visible = false;

                }
                else
                {
                    (lvOperationalParameter.Items[o].FindControl("opimgRecommendationDOC") as Image).ImageUrl = data.ImageDOC;
                }
                if (data.ParameterIDDOC == "" || data.ParameterIDDOC == null)
                {
                    (lvOperationalParameter.Items[o].FindControl("opDOCDecimal") as TextBox).Visible = false;
                }
                if (data.MandatoryDoc == "1")
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryDOC") as Label).ForeColor = System.Drawing.Color.Red;
                }
                else if (data.MandatoryDoc == "2")
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryDOC") as Label).ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryDOC") as Label).Visible = false;
                }


                if (data.ImageFace == "" || data.ImageFace == null)
                {
                    (lvOperationalParameter.Items[o].FindControl("opimgRecommendationFace") as Image).Visible = false;

                }
                else
                {
                    (lvOperationalParameter.Items[o].FindControl("opimgRecommendationFace") as Image).ImageUrl = data.ImageFace;
                }
                if (data.ParameterIDFace == "" || data.ParameterIDFace == null)
                {
                    (lvOperationalParameter.Items[o].FindControl("opFaceDecimal") as TextBox).Visible = false;
                }
                if (data.MandatoryFace == "1")
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryFace") as Label).ForeColor = System.Drawing.Color.Red;
                }
                else if (data.MandatoryFace == "2")
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryFace") as Label).ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryFace") as Label).Visible = false;
                }


                if (data.ImageWorkRPM == "" || data.ImageWorkRPM == null)
                {
                    (lvOperationalParameter.Items[o].FindControl("opimgWorkRPM") as Image).Visible = false;

                }
                else
                {
                    (lvOperationalParameter.Items[o].FindControl("opimgWorkRPM") as Image).ImageUrl = data.ImageWorkRPM;
                }
                if (data.ParameterIDWorkRPM == "" || data.ParameterIDWorkRPM == null)
                {
                    (lvOperationalParameter.Items[o].FindControl("opWorkRPMDecimal") as TextBox).Visible = false;
                }
                if (data.MandatoryWorkRPM == "1")
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryWorkRPM") as Label).ForeColor = System.Drawing.Color.Red;
                }
                else if (data.MandatoryWorkRPM == "2")
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryWorkRPM") as Label).ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryWorkRPM") as Label).Visible = false;
                }



                if (data.ImageWheelms == "" || data.ImageWheelms == null)
                {
                    (lvOperationalParameter.Items[o].FindControl("opimgWheelms") as Image).Visible = false;

                }
                else
                {
                    (lvOperationalParameter.Items[o].FindControl("opimgWheelms") as Image).ImageUrl = data.ImageWheelms;
                }
                if (data.ParameterIDWheelms == "" || data.ParameterIDWheelms == null)
                {
                    (lvOperationalParameter.Items[o].FindControl("opWheelmsDEcimal") as TextBox).Visible = false;
                }
                if (data.MandatoryWheelms == "1")
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryWheelms") as Label).ForeColor = System.Drawing.Color.Red;
                }
                else if (data.MandatoryWheelms == "2")
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryWheelms") as Label).ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryWheelms") as Label).Visible = false;
                }


                if (data.ImageWorkms == "" || data.ImageWorkms == null)
                {
                    (lvOperationalParameter.Items[o].FindControl("opimgWorkms") as Image).Visible = false;

                }
                else
                {
                    (lvOperationalParameter.Items[o].FindControl("opimgWorkms") as Image).ImageUrl = data.ImageWorkms;
                }
                if (data.ParameterIDWorkms == "" || data.ParameterIDWorkms == null)
                {
                    (lvOperationalParameter.Items[o].FindControl("opWorkmsDEcimal") as TextBox).Visible = false;
                }
                if (data.MandatoryWorkms == "1")
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryWorkms") as Label).ForeColor = System.Drawing.Color.Red;
                }
                else if (data.MandatoryWorkms == "2")
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryWorkms") as Label).ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryWorkms") as Label).Visible = false;
                }



                if (data.ImageWheelRPM == "" || data.ImageWheelRPM == null)
                {
                    (lvOperationalParameter.Items[o].FindControl("opimgWheelRPM") as Image).Visible = false;

                }
                else
                {
                    (lvOperationalParameter.Items[o].FindControl("opimgWheelRPM") as Image).ImageUrl = data.ImageWheelRPM;
                }
                if (data.ParameterIDWheelRPM == "" || data.ParameterIDWheelRPM == null)
                {
                    (lvOperationalParameter.Items[o].FindControl("opWheelRPMDEcimal") as TextBox).Visible = false;
                }
                if (data.MandatoryWheelRPM == "1")
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryWheelRPM") as Label).ForeColor = System.Drawing.Color.Red;
                }
                else if (data.MandatoryWheelRPM == "2")
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryWheelRPM") as Label).ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    (lvOperationalParameter.Items[o].FindControl("opMandatoryWheelRPM") as Label).Visible = false;
                }

                if (data.GrindingProcess == null || data.GrindingProcess == "")
                {

                }
                else
                {
                    (lvOperationalParameter.Items[o].FindControl("ddlopfor") as DropDownList).Text = data.GrindingProcess;
                }
                o++;
            }





            List<DataInputModuleParameter> listParameterdresgrind = new List<DataInputModuleParameter>();
            listParameterdresgrind = DBAccess.getDataInputModuleData(id, "Operational Parameters", "Grinding Parameters");

            if (listParameterdresgrind.Count % 2 != 0)
            {
                DataInputModuleParameter parameter = new DataInputModuleParameter();
                parameter.Prameter = "";
                parameter.PrameterId = "";
                parameter.CustomeName = "";
                parameter.Value = "";
                parameter.ObjectType = "";
                parameter.Datatype = "";
                parameter.LimitRange = "";
                parameter.Recommendation = "";
                parameter.Image = "";
                parameter.CalculatedFlag = "";
                parameter.DependancyFlag = "";
                parameter.Dependancy = "";
                parameter.IndependentParameter = "";
                parameter.Mandatory = "";
                listParameterdresgrind.Add(parameter);
            }

            List<DataInputModuleParameter> opglistParameter = new List<DataInputModuleParameter>();            decimal opgdiv = Math.Ceiling(Convert.ToDecimal(listParameterdresgrind.Count / 2));            int opgk1 = Decimal.ToInt32(opgdiv);            for (int k = 0; k < opgdiv; k++)            {                opglistParameter.Add(listParameterdresgrind[k]);                if ((listParameterdresgrind.Count % 2) != 0)                {                    if (k != opgdiv - 1)                    {                        opglistParameter.Add(listParameterdresgrind[opgk1]);                        opgk1++;                    }                }                else                {                    opglistParameter.Add(listParameterdresgrind[opgk1]);                    opgk1++;                }            }

            lvOperationalParameterGrind.DataSource = opglistParameter;
            lvOperationalParameterGrind.DataBind();

            o = 0;
            foreach (DataInputModuleParameter data in opglistParameter)
            {

                if (data.Mandatory == "1")
                {
                    (lvOperationalParameterGrind.Items[o].FindControl("opMandatory") as Label).ForeColor = System.Drawing.Color.Red;
                }
                else if (data.Mandatory == "2")
                {
                    (lvOperationalParameterGrind.Items[o].FindControl("opMandatory") as Label).ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    (lvOperationalParameterGrind.Items[o].FindControl("opMandatory") as Label).Visible = false;
                }

                string s = data.Image;
                if (data.Image == "" || data.Image == null)
                {
                    (lvOperationalParameterGrind.Items[o].FindControl("opimgRecommendation") as Image).Visible = false;

                }
                else
                {
                    (lvOperationalParameterGrind.Items[o].FindControl("opimgRecommendation") as Image).ImageUrl = data.Image;
                }
                if (data.ObjectType == "CheckBox")
                {
                    if (data.Value == "No" || data.Value == "")
                    {
                        (lvOperationalParameterGrind.Items[o].FindControl("opcbvalue") as CheckBox).Checked = false;

                    }
                    else
                    {
                        (lvOperationalParameterGrind.Items[o].FindControl("opcbvalue") as CheckBox).Checked = true;
                    }
                  (lvOperationalParameterGrind.Items[o].FindControl("opddlvalue") as DropDownList).Visible = false;
                    (lvOperationalParameterGrind.Items[o].FindControl("optxtvalue") as TextBox).Visible = false;
                    (lvOperationalParameterGrind.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvOperationalParameterGrind.Items[o].FindControl("optxtallowDecimal") as TextBox).Visible = false;
                    (lvOperationalParameterGrind.Items[o].FindControl("optxtallowNumeric") as TextBox).Visible = false;
                    (lvOperationalParameterGrind.Items[o].FindControl("optxtDate") as TextBox).Visible = false;


                }

                if (data.ObjectType == "TextBox")
                {
                    if (data.Datatype == "Date")
                    {
                        DateTime dt;
                        string txtdate = "";
                        if (data.Value != "")
                        {
                            if (DateTime.TryParseExact(data.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "yyyy-MM-ddTHH:mm", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd-MM-yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                            {
                                txtdate = dt.ToString("yyyy-MM-ddTHH:mm");
                            }
                           
                        }
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtDate") as TextBox).Text = txtdate;
                        //(lvOperationalParameterGrind.Items[o].FindControl("optxtDate") as TextBox).Text = data.Value == "" ? "" : Convert.ToDateTime(data.Value).ToString("yyyy-MM-ddTHH:mm");
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtvalue") as TextBox).Visible = false;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtallowNumeric") as TextBox).Visible = false;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtallowDecimal") as TextBox).Visible = false;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).Visible = false;
                    }
                    else if (data.Datatype == "Integer")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvOperationalParameterGrind.Items[o].FindControl("optxtallowNumeric") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvOperationalParameterGrind.Items[o].FindControl("optxtallowNumeric") as TextBox).ReadOnly = true;
                        }
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtallowNumeric") as TextBox).Text = data.Value;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtDate") as TextBox).Visible = false;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtvalue") as TextBox).Visible = false;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtallowDecimal") as TextBox).Visible = false;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).Visible = false;

                    }
                    else if (data.Datatype == "Decimal")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvOperationalParameterGrind.Items[o].FindControl("optxtallowDecimal") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvOperationalParameterGrind.Items[o].FindControl("optxtallowDecimal") as TextBox).ReadOnly = true;
                        }
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtallowDecimal") as TextBox).Text = data.Value;
                        //(lvOperationalParameterGrind.Items[o].FindControl("optxtallowDecimal") as TextBox).BackColor = System.Drawing.Color.Red;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtallowNumeric") as TextBox).Visible = false;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtDate") as TextBox).Visible = false;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtvalue") as TextBox).Visible = false;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).Visible = false;
                    }
                    else if (data.Datatype == "Alpha Numeric")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvOperationalParameterGrind.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvOperationalParameterGrind.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).ReadOnly = true;
                        }
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).Text = data.Value;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtallowDecimal") as TextBox).Visible = false;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtallowNumeric") as TextBox).Visible = false;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtDate") as TextBox).Visible = false;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtvalue") as TextBox).Visible = false;

                    }
                    else
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvOperationalParameterGrind.Items[o].FindControl("optxtvalue") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvOperationalParameterGrind.Items[o].FindControl("optxtvalue") as TextBox).ReadOnly = true;
                        }
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtvalue") as TextBox).Text = data.Value;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).Visible = false;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtallowDecimal") as TextBox).Visible = false;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtallowNumeric") as TextBox).Visible = false;
                        (lvOperationalParameterGrind.Items[o].FindControl("optxtDate") as TextBox).Visible = false;
                    }
                    (lvOperationalParameterGrind.Items[o].FindControl("opddlvalue") as DropDownList).Visible = false;
                    (lvOperationalParameterGrind.Items[o].FindControl("opcbvalue") as CheckBox).Visible = false;
                }
                if (data.ObjectType == "Drop Down")
                {

                    //if (data.DependancyFlag == "1")
                    //{
                    //    DropDownList ddl1 = (lvOperationalParameterGrind.Items[o].FindControl("opddlvalue") as DropDownList);
                    //    ddl1.DataSource = DBAccess.getInputModuleParameterDetails("Operational Parameters", data.PrameterId);
                    //    ddl1.DataTextField = "ParameterDetails";
                    //    ddl1.DataValueField = "ParameterDetails";
                    //    ddl1.DataBind();
                    //    ddl1.Items.Insert(0, new ListItem("Select " + data.Prameter, ""));
                    //    if (data.Value != "")
                    //    {
                    //        ddl1.Text = data.Value;
                    //    }

                    //    string param2Id = data.PrameterId;
                    //    string param2value = data.Value;
                    //    string param1Id, param1;
                    //    List<ParameterDependency> listParamDEpend = DBAccess.IMgetDependencyParametervalue(param2Id, param2value, out param1, out param1Id);
                    //    if (listParamDEpend.Count == 0)
                    //    {
                    //        listParamDEpend = DBAccess.IMgetDependencyParameter(param2Id, param2value);
                    //        if (listParamDEpend.Count > 0)
                    //        {
                    //            for (int pd = 0; pd < lvOperationalParameterGrind.Items.Count; pd++)
                    //            {
                    //                if (listParamDEpend[0].ParameterId1 == (lvOperationalParameterGrind.Items[pd].FindControl("opParameterID") as Label).Text)
                    //                {
                    //                    DropDownList ddl2 = (lvOperationalParameterGrind.Items[pd].FindControl("opddlvalue") as DropDownList);
                    //                    ddl2.Items.Insert(0, new ListItem("Select " + listParamDEpend[0].Parameter1, ""));

                    //                }
                    //            }
                    //        }
                    //    }

                    //    for (int pd = 0; pd < lvOperationalParameterGrind.Items.Count; pd++)
                    //    {
                    //        if (param1Id == (lvOperationalParameterGrind.Items[pd].FindControl("opParameterID") as Label).Text)
                    //        {
                    //            DropDownList ddl2 = (lvOperationalParameterGrind.Items[pd].FindControl("opddlvalue") as DropDownList);
                    //            ddl2.DataSource = listParamDEpend;
                    //            ddl2.DataTextField = "Parameter1Value";
                    //            ddl2.DataValueField = "Parameter1Value";
                    //            ddl2.DataBind();
                    //            ddl2.Items.Insert(0, new ListItem("Select " + param1, ""));
                    //            string value = (from v in opglistParameter where v.PrameterId == param1Id select v.Value).First();
                    //            if (value != "")
                    //            {
                    //                ddl2.Text = value;
                    //            }
                    //        }
                    //    }

                    //}
                    //else
                    //{
                    //    if (data.DependancyFlag == "2")
                    //    {
                    //        (lvOperationalParameterGrind.Items[o].FindControl("optxtvalue") as TextBox).Visible = false;
                    //        (lvOperationalParameterGrind.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).Visible = false;
                    //        (lvOperationalParameterGrind.Items[o].FindControl("optxtallowDecimal") as TextBox).Visible = false;
                    //        (lvOperationalParameterGrind.Items[o].FindControl("optxtallowNumeric") as TextBox).Visible = false;
                    //        (lvOperationalParameterGrind.Items[o].FindControl("optxtDate") as TextBox).Visible = false;
                    //        (lvOperationalParameterGrind.Items[o].FindControl("opcbvalue") as CheckBox).Visible = false;
                    //        o++;
                    //        continue;
                    //    }
                    DropDownList ddl1 = (lvOperationalParameterGrind.Items[o].FindControl("opddlvalue") as DropDownList);
                    ddl1.DataSource = DBAccess.getInputModuleParameterDetails("Operational Parameters", data.PrameterId);
                    ddl1.DataTextField = "ParameterDetails";
                    ddl1.DataValueField = "ParameterDetails";
                    ddl1.DataBind();
                    ddl1.Items.Insert(0, new ListItem("Select " + data.Prameter, ""));
                    if (data.Value != "")
                    {
                        ddl1.Text = data.Value;
                    }
                    //  }

                    (lvOperationalParameterGrind.Items[o].FindControl("optxtvalue") as TextBox).Visible = false;
                    (lvOperationalParameterGrind.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvOperationalParameterGrind.Items[o].FindControl("optxtallowDecimal") as TextBox).Visible = false;
                    (lvOperationalParameterGrind.Items[o].FindControl("optxtallowNumeric") as TextBox).Visible = false;
                    (lvOperationalParameterGrind.Items[o].FindControl("optxtDate") as TextBox).Visible = false;
                    (lvOperationalParameterGrind.Items[o].FindControl("opcbvalue") as CheckBox).Visible = false;
                }
                if (data.ObjectType == "")
                {
                    (lvOperationalParameterGrind.Items[o].FindControl("opddlvalue") as DropDownList).Visible = false;
                    (lvOperationalParameterGrind.Items[o].FindControl("optxtvalue") as TextBox).Visible = false;
                    (lvOperationalParameterGrind.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvOperationalParameterGrind.Items[o].FindControl("optxtallowDecimal") as TextBox).Visible = false;
                    (lvOperationalParameterGrind.Items[o].FindControl("optxtallowNumeric") as TextBox).Visible = false;
                    (lvOperationalParameterGrind.Items[o].FindControl("optxtDate") as TextBox).Visible = false;
                    (lvOperationalParameterGrind.Items[o].FindControl("opcbvalue") as CheckBox).Visible = false;

                }

                o++;

            }


            List<DataInputModuleParameter> listParameterdress = new List<DataInputModuleParameter>();
            listParameterdress = DBAccess.getDataInputModuleData(id, "Operational Parameters", "Dressing Parameters");

            if (listParameterdress.Count % 2 != 0)
            {
                DataInputModuleParameter parameter = new DataInputModuleParameter();
                parameter.Prameter = "";
                parameter.PrameterId = "";
                parameter.CustomeName = "";
                parameter.Value = "";
                parameter.ObjectType = "";
                parameter.Datatype = "";
                parameter.LimitRange = "";
                parameter.Recommendation = "";
                parameter.Image = "";
                parameter.CalculatedFlag = "";
                parameter.DependancyFlag = "";
                parameter.Dependancy = "";
                parameter.IndependentParameter = "";
                parameter.Mandatory = "";
                listParameterdress.Add(parameter);
            }

            List<DataInputModuleParameter> oplistParameter = new List<DataInputModuleParameter>();            decimal opdiv = Math.Ceiling(Convert.ToDecimal(listParameterdress.Count / 2));            int opk1 = Decimal.ToInt32(opdiv);            for (int k = 0; k < opdiv; k++)            {                oplistParameter.Add(listParameterdress[k]);                if ((listParameterdress.Count % 2) != 0)                {                    if (k != opdiv - 1)                    {                        oplistParameter.Add(listParameterdress[opk1]);                        opk1++;                    }                }                else                {                    oplistParameter.Add(listParameterdress[opk1]);                    opk1++;                }            }

            lvOPDressing.DataSource = oplistParameter;
            lvOPDressing.DataBind();

            o = 0;
            foreach (DataInputModuleParameter data in oplistParameter)
            {
                if (data.Mandatory == "1")
                {
                    (lvOPDressing.Items[o].FindControl("opMandatory") as Label).ForeColor = System.Drawing.Color.Red;
                }
                else if (data.Mandatory == "2")
                {
                    (lvOPDressing.Items[o].FindControl("opMandatory") as Label).ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    (lvOPDressing.Items[o].FindControl("opMandatory") as Label).Visible = false;
                }
                string s = data.Image;
                if (data.Image == "" || data.Image == null)
                {
                    (lvOPDressing.Items[o].FindControl("opimgRecommendation") as Image).Visible = false;

                }
                else
                {
                    (lvOPDressing.Items[o].FindControl("opimgRecommendation") as Image).ImageUrl = data.Image;
                }
                if (data.ObjectType == "CheckBox")
                {
                    if (data.Value == "No" || data.Value == "")
                    {
                        (lvOPDressing.Items[o].FindControl("opcbvalue") as CheckBox).Checked = false;

                    }
                    else
                    {
                        (lvOPDressing.Items[o].FindControl("opcbvalue") as CheckBox).Checked = true;
                    }
                  (lvOPDressing.Items[o].FindControl("opddlvalue") as DropDownList).Visible = false;
                    (lvOPDressing.Items[o].FindControl("optxtvalue") as TextBox).Visible = false;
                    (lvOPDressing.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvOPDressing.Items[o].FindControl("optxtallowDecimal") as TextBox).Visible = false;
                    (lvOPDressing.Items[o].FindControl("optxtallowNumeric") as TextBox).Visible = false;
                    (lvOPDressing.Items[o].FindControl("optxtDate") as TextBox).Visible = false;


                }

                if (data.ObjectType == "TextBox")
                {
                    if (data.Datatype == "Date")
                    {
                        DateTime dt;
                        string txtdate = "";
                        if (data.Value != "")
                        {
                            if (DateTime.TryParseExact(data.Value, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "yyyy-MM-ddTHH:mm", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd/MM/yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt) || DateTime.TryParseExact(data.Value, "dd-MM-yyyy HH:mm", System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                            {
                                txtdate = dt.ToString("yyyy-MM-ddTHH:mm");
                            }
                           
                        }
                        (lvOPDressing.Items[o].FindControl("optxtDate") as TextBox).Text = txtdate;
                        //(lvOPDressing.Items[o].FindControl("optxtDate") as TextBox).Text = data.Value == "" ? "" : Convert.ToDateTime(data.Value).ToString("yyyy-MM-ddTHH:mm");
                        (lvOPDressing.Items[o].FindControl("optxtvalue") as TextBox).Visible = false;
                        (lvOPDressing.Items[o].FindControl("optxtallowNumeric") as TextBox).Visible = false;
                        (lvOPDressing.Items[o].FindControl("optxtallowDecimal") as TextBox).Visible = false;
                        (lvOPDressing.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).Visible = false;
                    }
                    else if (data.Datatype == "Integer")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvOPDressing.Items[o].FindControl("optxtallowNumeric") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvOPDressing.Items[o].FindControl("optxtallowNumeric") as TextBox).ReadOnly = true;
                        }
                        (lvOPDressing.Items[o].FindControl("optxtallowNumeric") as TextBox).Text = data.Value;
                        (lvOPDressing.Items[o].FindControl("optxtDate") as TextBox).Visible = false;
                        (lvOPDressing.Items[o].FindControl("optxtvalue") as TextBox).Visible = false;
                        (lvOPDressing.Items[o].FindControl("optxtallowDecimal") as TextBox).Visible = false;
                        (lvOPDressing.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).Visible = false;

                    }
                    else if (data.Datatype == "Decimal")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvOPDressing.Items[o].FindControl("optxtallowDecimal") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvOPDressing.Items[o].FindControl("optxtallowDecimal") as TextBox).ReadOnly = true;
                        }
                        (lvOPDressing.Items[o].FindControl("optxtallowDecimal") as TextBox).Text = data.Value;
                        (lvOPDressing.Items[o].FindControl("optxtallowNumeric") as TextBox).Visible = false;
                        (lvOPDressing.Items[o].FindControl("optxtDate") as TextBox).Visible = false;
                        (lvOPDressing.Items[o].FindControl("optxtvalue") as TextBox).Visible = false;
                        (lvOPDressing.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).Visible = false;
                    }
                    else if (data.Datatype == "Alpha Numeric")
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvOPDressing.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvOPDressing.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).ReadOnly = true;
                        }
                        (lvOPDressing.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).Text = data.Value;
                        (lvOPDressing.Items[o].FindControl("optxtallowDecimal") as TextBox).Visible = false;
                        (lvOPDressing.Items[o].FindControl("optxtallowNumeric") as TextBox).Visible = false;
                        (lvOPDressing.Items[o].FindControl("optxtDate") as TextBox).Visible = false;
                        (lvOPDressing.Items[o].FindControl("optxtvalue") as TextBox).Visible = false;

                    }
                    else
                    {
                        if (data.CalculatedFlag == "True")
                        {
                            (lvOPDressing.Items[o].FindControl("optxtvalue") as TextBox).BackColor = System.Drawing.Color.AliceBlue;
                            (lvOPDressing.Items[o].FindControl("optxtvalue") as TextBox).ReadOnly = true;
                        }
                        (lvOPDressing.Items[o].FindControl("optxtvalue") as TextBox).Text = data.Value;
                        (lvOPDressing.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).Visible = false;
                        (lvOPDressing.Items[o].FindControl("optxtallowDecimal") as TextBox).Visible = false;
                        (lvOPDressing.Items[o].FindControl("optxtallowNumeric") as TextBox).Visible = false;
                        (lvOPDressing.Items[o].FindControl("optxtDate") as TextBox).Visible = false;
                    }
                    (lvOPDressing.Items[o].FindControl("opddlvalue") as DropDownList).Visible = false;
                    (lvOPDressing.Items[o].FindControl("opcbvalue") as CheckBox).Visible = false;
                }
                if (data.ObjectType == "Drop Down")
                {

                    //if (data.DependancyFlag == "1")
                    //{
                    //    DropDownList ddl1 = (lvOPDressing.Items[o].FindControl("opddlvalue") as DropDownList);
                    //    ddl1.DataSource = DBAccess.getInputModuleParameterDetails("Operational Parameters", data.PrameterId);
                    //    ddl1.DataTextField = "ParameterDetails";
                    //    ddl1.DataValueField = "ParameterDetails";
                    //    ddl1.DataBind();
                    //    ddl1.Items.Insert(0, new ListItem("Select " + data.Prameter, ""));
                    //    if (data.Value != "")
                    //    {
                    //        ddl1.Text = data.Value;
                    //    }

                    //    string param2Id = data.PrameterId;
                    //    string param2value = data.Value;
                    //    string param1Id, param1;
                    //    List<ParameterDependency> listParamDEpend = DBAccess.IMgetDependencyParametervalue(param2Id, param2value, out param1, out param1Id);

                    //    if (listParamDEpend.Count == 0)
                    //    {
                    //        listParamDEpend = DBAccess.IMgetDependencyParameter(param2Id, param2value);
                    //        if (listParamDEpend.Count > 0)
                    //        {
                    //            for (int pd = 0; pd < lvOPDressing.Items.Count; pd++)
                    //            {
                    //                if (listParamDEpend[0].ParameterId1 == (lvOPDressing.Items[pd].FindControl("opParameterID") as Label).Text)
                    //                {
                    //                    DropDownList ddl2 = (lvOPDressing.Items[pd].FindControl("opddlvalue") as DropDownList);
                    //                    ddl2.Items.Insert(0, new ListItem("Select " + listParamDEpend[0].Parameter1, ""));

                    //                }
                    //            }
                    //        }
                    //    }

                    //    for (int pd = 0; pd < lvOPDressing.Items.Count; pd++)
                    //    {
                    //        if (param1Id == (lvOPDressing.Items[pd].FindControl("opParameterID") as Label).Text)
                    //        {
                    //            DropDownList ddl2 = (lvOPDressing.Items[pd].FindControl("opddlvalue") as DropDownList);
                    //            ddl2.DataSource = listParamDEpend;
                    //            ddl2.DataTextField = "Parameter1Value";
                    //            ddl2.DataValueField = "Parameter1Value";
                    //            ddl2.DataBind();
                    //            ddl2.Items.Insert(0, new ListItem("Select " + param1, ""));
                    //            string value = (from v in opglistParameter where v.PrameterId == param1Id select v.Value).First();
                    //            if (value != "")
                    //            {
                    //                ddl2.Text = value;
                    //            }
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    if (data.DependancyFlag == "2")
                    //    {
                    //        (lvOPDressing.Items[o].FindControl("optxtvalue") as TextBox).Visible = false;
                    //        (lvOPDressing.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).Visible = false;
                    //        (lvOPDressing.Items[o].FindControl("optxtallowDecimal") as TextBox).Visible = false;
                    //        (lvOPDressing.Items[o].FindControl("optxtallowNumeric") as TextBox).Visible = false;
                    //        (lvOPDressing.Items[o].FindControl("optxtDate") as TextBox).Visible = false;
                    //        (lvOPDressing.Items[o].FindControl("opcbvalue") as CheckBox).Visible = false;
                    //        o++;
                    //        continue;
                    //    }
                    DropDownList ddl1 = (lvOPDressing.Items[o].FindControl("opddlvalue") as DropDownList);
                    ddl1.DataSource = DBAccess.getInputModuleParameterDetails("Operational Parameters", data.PrameterId);
                    ddl1.DataTextField = "ParameterDetails";
                    ddl1.DataValueField = "ParameterDetails";
                    ddl1.DataBind();
                    ddl1.Items.Insert(0, new ListItem("Select " + data.Prameter, ""));
                    if (data.Value != "")
                    {
                        ddl1.Text = data.Value;
                    }

                    // }

                    (lvOPDressing.Items[o].FindControl("optxtvalue") as TextBox).Visible = false;
                    (lvOPDressing.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvOPDressing.Items[o].FindControl("optxtallowDecimal") as TextBox).Visible = false;
                    (lvOPDressing.Items[o].FindControl("optxtallowNumeric") as TextBox).Visible = false;
                    (lvOPDressing.Items[o].FindControl("optxtDate") as TextBox).Visible = false;
                    (lvOPDressing.Items[o].FindControl("opcbvalue") as CheckBox).Visible = false;
                }
                if (data.ObjectType == "")
                {
                    (lvOPDressing.Items[o].FindControl("opddlvalue") as DropDownList).Visible = false;
                    (lvOPDressing.Items[o].FindControl("optxtvalue") as TextBox).Visible = false;
                    (lvOPDressing.Items[o].FindControl("optxtallowalphaNumeric") as TextBox).Visible = false;
                    (lvOPDressing.Items[o].FindControl("optxtallowDecimal") as TextBox).Visible = false;
                    (lvOPDressing.Items[o].FindControl("optxtallowNumeric") as TextBox).Visible = false;
                    (lvOPDressing.Items[o].FindControl("optxtDate") as TextBox).Visible = false;
                    (lvOPDressing.Items[o].FindControl("opcbvalue") as CheckBox).Visible = false;

                }

                o++;

            }

            #endregion

            #region ---Quality Parameter--

            listParameter = DBAccess.getDataInputModuleQualityParamData(id, "Quality Parameters", "");
            lvQualityParameter.DataSource = listParameter;
            lvQualityParameter.DataBind();

            int q = 0;
            foreach (DataInputModuleParameter data in listParameter)
            {
                //    string s = data.Image;
                if (data.ImageRecommandationTL == "" || data.ImageRecommandationTL == null)
                {
                    (lvQualityParameter.Items[q].FindControl("qpLimitImgTargetLower") as Image).Visible = false;

                }
                else
                {

                    (lvQualityParameter.Items[q].FindControl("qpLimitImgTargetLower") as Image).ImageUrl = data.ImageRecommandationTL;
                }
                //if (data.ParamIdTargetLower == "190" || data.ParamIdTargetLower == "194" || data.ParamIdTargetLower == "198" || data.ParamIdTargetLower == "202" || data.ParamIdTargetLower == "206" || data.ParamIdTargetLower == "210")
                //{
                //    (lvQualityParameter.Items[q].FindControl("qpTargetLower") as TextBox).Visible = true;
                //}
                //else
                //{
                //    (lvQualityParameter.Items[q].FindControl("qpTargetLower") as TextBox).Visible = false;
                //}
                if (data.ParamIdTargetLower == "" || data.ParamIdTargetLower == null)
                {
                    (lvQualityParameter.Items[q].FindControl("qpTargetLower") as TextBox).Visible = false;
                }
                if (data.MandatoryTargetLower == "1")
                {
                    (lvQualityParameter.Items[q].FindControl("qpMandatoryTargetLower") as Label).ForeColor = System.Drawing.Color.Red;
                }
                else if (data.MandatoryTargetLower == "2")
                {
                    (lvQualityParameter.Items[q].FindControl("qpMandatoryTargetLower") as Label).ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    (lvQualityParameter.Items[q].FindControl("qpMandatoryTargetLower") as Label).Visible = false;
                }


                if (data.ImageRecommandationTU == "" || data.ImageRecommandationTU == null)
                {
                    (lvQualityParameter.Items[q].FindControl("qpLimitImgTargetUppper") as Image).Visible = false;

                }
                else
                {

                    (lvQualityParameter.Items[q].FindControl("qpLimitImgTargetUppper") as Image).ImageUrl = data.ImageRecommandationTU;
                }
                if (data.ParamIdTargetUpper == "" || data.ParamIdTargetUpper == null)
                {
                    (lvQualityParameter.Items[q].FindControl("qpTargetUppper") as TextBox).Visible = false;
                }
                if (data.MandatoryTargetUpper == "1")
                {
                    (lvQualityParameter.Items[q].FindControl("qpMandatoryTargetUppper") as Label).ForeColor = System.Drawing.Color.Red;
                }
                else if (data.MandatoryTargetUpper == "2")
                {
                    (lvQualityParameter.Items[q].FindControl("qpMandatoryTargetUppper") as Label).ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    (lvQualityParameter.Items[q].FindControl("qpMandatoryTargetUppper") as Label).Visible = false;
                }


                if (data.ImageRecommandationAL == "" || data.ImageRecommandationAL == null)
                {
                    (lvQualityParameter.Items[q].FindControl("qpLimitImgAchievedLower") as Image).Visible = false;

                }
                else
                {

                    (lvQualityParameter.Items[q].FindControl("qpLimitImgAchievedLower") as Image).ImageUrl = data.ImageRecommandationAL;
                }
                if (data.ParamIdActualLower == "" || data.ParamIdActualLower == null)
                {
                    (lvQualityParameter.Items[q].FindControl("qpAchievedLower") as TextBox).Visible = false;
                }
                if (data.MandatoryActualLower == "1")
                {
                    (lvQualityParameter.Items[q].FindControl("qpMandatoryAchievedLower") as Label).ForeColor = System.Drawing.Color.Red;
                }
                else if (data.MandatoryActualLower == "2")
                {
                    (lvQualityParameter.Items[q].FindControl("qpMandatoryAchievedLower") as Label).ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    (lvQualityParameter.Items[q].FindControl("qpMandatoryAchievedLower") as Label).Visible = false;
                }


                if (data.ImageRecommandationAU == "" || data.ImageRecommandationAU == null)
                {
                    (lvQualityParameter.Items[q].FindControl("qpLimitImgAchievedUppper") as Image).Visible = false;

                }
                else
                {

                    (lvQualityParameter.Items[q].FindControl("qpLimitImgAchievedUppper") as Image).ImageUrl = data.ImageRecommandationAU;
                }
                if (data.ParamIdActualUpper == "" || data.ParamIdActualUpper == null)
                {
                    (lvQualityParameter.Items[q].FindControl("qpAchievedUppper") as TextBox).Visible = false;
                }
                if (data.MandatoryActualUpper == "1")
                {
                    (lvQualityParameter.Items[q].FindControl("qpMandatoryAchievedUppper") as Label).ForeColor = System.Drawing.Color.Red;
                }
                else if (data.MandatoryActualUpper == "2")
                {
                    (lvQualityParameter.Items[q].FindControl("qpMandatoryAchievedUppper") as Label).ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    (lvQualityParameter.Items[q].FindControl("qpMandatoryAchievedUppper") as Label).Visible = false;
                }
                q++;
            }
            //int q = 0;
            //foreach (DataInputModuleParameter data in listParameter)
            //{
            //    if (data.ObjectType == "CheckBox")
            //    {
            //        if (data.Value == "No" || data.Value == "")
            //        {
            //            (lvQualityParameter.Items[q].FindControl("qpcbvalue") as CheckBox).Checked = false;

            //        }
            //        else
            //        {
            //            (lvQualityParameter.Items[q].FindControl("qpcbvalue") as CheckBox).Checked = true;
            //        }
            //      (lvQualityParameter.Items[q].FindControl("qpddlvalue") as DropDownList).Visible = false;
            //        (lvQualityParameter.Items[q].FindControl("qpTargetLower") as TextBox).Visible = false;
            //        (lvQualityParameter.Items[q].FindControl("qpTargetUppper") as TextBox).Visible = false;
            //        (lvQualityParameter.Items[q].FindControl("qpAchievedLower") as TextBox).Visible = false;
            //        (lvQualityParameter.Items[q].FindControl("qpAchievedUppper") as TextBox).Visible = false;
            //    }

            //    if (data.ObjectType == "TextBox")
            //    {
            //        (lvQualityParameter.Items[q].FindControl("qpTargetLower") as TextBox).Text = data.Value;
            //        (lvQualityParameter.Items[q].FindControl("qpTargetUppper") as TextBox).Text = data.Value;
            //        (lvQualityParameter.Items[q].FindControl("qpAchievedLower") as TextBox).Text = data.Value;
            //        (lvQualityParameter.Items[q].FindControl("qpAchievedUppper") as TextBox).Text = data.Value;
            //        (lvQualityParameter.Items[q].FindControl("qpddlvalue") as DropDownList).Visible = false;
            //        (lvQualityParameter.Items[q].FindControl("qpcbvalue") as CheckBox).Visible = false;
            //    }
            //    if (data.ObjectType == "Drop Down")
            //    {
            //        DropDownList ddl1 = (lvQualityParameter.Items[q].FindControl("qpddlvalue") as DropDownList);
            //        ddl1.DataSource = DBAccess.getInputModuleParameterDetails("Quality Parameters", data.Prameter);
            //        ddl1.DataTextField = "ParameterDetails";
            //        ddl1.DataValueField = "ParameterDetails";
            //        ddl1.DataBind();
            //        ddl1.Items.Insert(0, new ListItem("Select " + data.Prameter, ""));
            //        if (data.Value != "")
            //        {
            //            ddl1.Text = data.Value;
            //        }

            //        (lvQualityParameter.Items[q].FindControl("qpTargetLower") as TextBox).Visible = false;
            //        (lvQualityParameter.Items[q].FindControl("qpTargetUppper") as TextBox).Visible = false;
            //        (lvQualityParameter.Items[q].FindControl("qpAchievedLower") as TextBox).Visible = false;
            //        (lvQualityParameter.Items[q].FindControl("qpAchievedUppper") as TextBox).Visible = false;
            //        (lvQualityParameter.Items[q].FindControl("qpcbvalue") as CheckBox).Visible = false;
            //    }
            //    q++;

            //}
            #endregion
        }


        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string[] getRecommandation(string parameter, string ipModule)
        {
            string[] s = new string[3];
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = new SqlCommand("select Recommendation, LSL,USL from InputModuleParameterDetails where Parameter=@parameter and InputModule=@ipModule", con);
            cmd.Parameters.AddWithValue("@parameter", parameter);
            cmd.Parameters.AddWithValue("@ipModule", ipModule);
            cmd.CommandType = CommandType.Text;
            sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    s[0] = sdr["Recommendation"].ToString();
                    s[1] = sdr["LSL"].ToString();
                    s[2] = sdr["USL"].ToString();
                }
            }

            return s;
        }


        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string[] giObjective(string inputtext)
        {

            List<string> ouput = DBAccess.getGIObjective(inputtext);
            string[] s = new string[ouput.Count];
            for (int i = 0; i < ouput.Count; i++)
            {
                s[i] = ouput[i];
            }
            return s;
        }



        public void bindData(string id)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand("select * from System_Documents where ID=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {

                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog(ex.Message);
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog(ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
        }

       

        protected void viewInputModule_Click(object sender, EventArgs e)
        {
            // Session["ATKSDocID"] = null;


            string existOrnot = "Not Exists";
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand("select * from SystemDocTransaction where SDocId=@sdoc", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@sdoc", txtViewSdocid.Text);
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    existOrnot = "Exists";
                }
            }
            catch (Exception ex)
            {

            }

            if (existOrnot == "Exists")
            {
                Session["ATKSDocID"] = txtViewSdocid.Text;
                BindData(txtViewSdocid.Text.ToString());
                //saveInputModule.Visible = true;
                //allowEdit.Visible = true;
                //btnDeleteSDoc.Visible = true;
            }
            else
            {
                Session["ATKSDocID"] = null;
                BindData("");
                //saveInputModule.Visible = false;
                //btnDeleteSDoc.Visible = false;
                //allowEdit.Visible = false;
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('System Document ID not exists.');", true);
            }

        }

        protected void cycleDesignParameters_Click(object sender, EventArgs e)
        {
            Response.Redirect("DerivedParameters.aspx");
        }


        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void setBackColor(string color)
        {
            HttpContext.Current.Session["bg"] = color;
        }


        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void setActiveTabinSession(string tabname)
        {
            HttpContext.Current.Session["activeTabName"] = tabname;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string exitOrcreatenewSdocConfirmation(string SdocId)
        {
            string result = "NotExists";
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand("select * from SystemDocTransaction where SDocId=@docid", con);
                cmd.Parameters.AddWithValue("@docid", SdocId);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    result = "Exists";
                }
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog(ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return result;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]        public static string getBackColor()        {            if (HttpContext.Current.Session["bg"] == null)
            {
                HttpContext.Current.Session["bg"] = "black";
            }
            return HttpContext.Current.Session["bg"].ToString();        }

        [System.Web.Services.WebMethod(EnableSession = true)]        public static List<ParameterDependency> getDependencyValue(string param2Id, string param2selectedddlValue)        {
            string param1Id, param1;
            List<ParameterDependency> listParamDEpend = DBAccess.IMgetDependencyParametervalue(param2Id, param2selectedddlValue, out param1, out param1Id);
            if (listParamDEpend.Count == 0)
            {
                listParamDEpend = DBAccess.IMgetDependencyParameter(param2Id, param2selectedddlValue);
            }

            return listParamDEpend;
        }

        protected void saveASOK_ServerClick(object sender, EventArgs e)
        {

            try
            {
                string SDocName = "", Plunge = "", SubCategoryId = "", SaveAsSdoc = "";
                long docid = 0, plunge = 0, subcategory = 0;

                for (int i = 0; i < lvGeneralInfo.Items.Count; i++)
                {
                    // string parameterid = (lvGeneralInfo.Items[i].FindControl("giParameterID") as Label).Text;
                    //if (Convert.ToInt32(parameterid) == 1)
                    //{
                    //    SaveAsSdoc = (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).Text;
                    //}
                    string paramname = (lvGeneralInfo.Items[i].FindControl("gihdParameterName") as HiddenField).Value;
                    if (paramname == "SDoc ID")
                    {
                        SaveAsSdoc = (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).Text;
                    }
                }
                if (SaveAsSdoc == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('SDoc ID required');", true);
                    return;
                }

                string[] splitSDoc;
                splitSDoc = SaveAsSdoc.Split('_');
                docid = Convert.ToInt64(splitSDoc[0].Replace("SDoc", ""));
                plunge = Convert.ToInt64(splitSDoc[1]);
                subcategory = Convert.ToInt64(splitSDoc[2]);

                if (newSDocid.Checked == true)
                {
                    string SDoc = DBAccess.getSdocname("SDocName");
                    if (SDoc == "-1")
                    {
                        SDocName = "000001";
                    }
                    else
                    {
                        SDocName = (Convert.ToInt64(SDoc) + 1).ToString("000000");
                    }

                    Plunge = "01";

                    SubCategoryId = "0001";

                }
                else if (incrementPlunge.Checked == true)
                {
                    string Plungeid = DBAccess.getSystemDocumentName(docid, plunge, subcategory, "Plunge");
                    SDocName = docid.ToString("000000");
                    SubCategoryId = subcategory.ToString("0000");
                    if (Plungeid == "-1")
                    {
                        Plunge = "01";
                    }
                    else
                    {
                        Plunge = (Convert.ToInt64(Plungeid) + 1).ToString("00");
                    }
                }
                else if (incrementSdocSubCategory.Checked == true)
                {
                    string CategoryId = DBAccess.getSystemDocumentName(docid, plunge, subcategory, "SubCategory");
                    SDocName = docid.ToString("000000");
                    Plunge = plunge.ToString("00");
                    if (CategoryId == "-1")
                    {
                        SubCategoryId = "0001";
                    }
                    else
                    {
                        SubCategoryId = (Convert.ToInt64(CategoryId) + 1).ToString("0000");
                    }

                }

                string date = DateTime.Now.ToString("ddMMyy");
                string time = DateTime.Now.ToString("HH:mm");
                string SystemDoc = "SDoc" + SDocName + "_" + Plunge + "_" + SubCategoryId + "_" + date + "_" + time;


                DataInputModuleParameter dataipmodule = new DataInputModuleParameter();
                dataipmodule.SDOcName = SDocName;
                dataipmodule.PlungeId = Plunge;
                dataipmodule.SubcategoryId = SubCategoryId;
                Session["ATKSDocID"] = SystemDoc;

                dataipmodule.Username = Session["EmpName"].ToString();

                bool result = saveupdateInputModule(dataipmodule, "Save", SystemDoc);
                if (result == false)
                {
                    bindSdocID();
                    BindData(Session["ATKSDocID"].ToString());
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void saveConfirmYes_ServerClick(object sender, EventArgs e)
        {
            try
            {

                DataInputModuleParameter dataipmodule = new DataInputModuleParameter();
                dataipmodule.Username = Session["EmpName"].ToString();
                string Sdocid = "";
                for (int i = 0; i < lvGeneralInfo.Items.Count; i++)
                {
                    dataipmodule.Prameter = (lvGeneralInfo.Items[i].FindControl("gihdParameterName") as HiddenField).Value;
                    if (dataipmodule.Prameter == "SDoc ID")
                    {
                        Sdocid= (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).Text;
                        Session["ATKSDocID"] = Sdocid;
                        string[] splitSDoc;
                        splitSDoc = Session["ATKSDocID"].ToString().Split('_');
                        dataipmodule.SDOcName = splitSDoc[0].Replace("SDoc", "");
                        dataipmodule.PlungeId = splitSDoc[1];
                        dataipmodule.SubcategoryId = splitSDoc[2];
                    }
                }
                if (Sdocid == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('SDoc ID required');", true);
                    return;
                }
                bool result = saveupdateInputModule(dataipmodule, "Update", Session["ATKSDocID"].ToString());
                if (result == false)
                {
                    BindData(Session["ATKSDocID"].ToString());
                }

            }
            catch (Exception ex)
            {

            }
           

        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void SetSessionImageDetailsNull()
        {

            HttpContext.Current.Session["imagesDetails"] = null;
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void AddIamgeNameToSessionImageDetails(string imagename)
        {
            if (HttpContext.Current.Session["imagesDetails"] != null)
            {

                List<string> list = (List<string>)HttpContext.Current.Session["imagesDetails"];
                list.Add(imagename);
                HttpContext.Current.Session["imagesDetails"] = list;
            }
            else
            {
                List<string> list = new List<string>();
                list.Add(imagename);
                HttpContext.Current.Session["imagesDetails"] = list;
            }
        }

        public class parametervalue
        {
            public string parameter { get; set; }
            public string value { get; set; }
            public decimal lsl { get; set; }
            public decimal usl { get; set; }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static string getLimitRangeforParameterDependency(List<parametervalue> IndependentParameters, string DependentParameter)
        {
            // List<string> LSL = new List<string>(); 
            List<parametervalue> listparametervalue = new List<parametervalue>();
            try
            {
                
                parametervalue parametervalue = null;
                for (int i = 0; i < IndependentParameters.Count; i++)
                {
                    parametervalue = new parametervalue();
                    string lsl = listparameterDependencie.Where(x => x.Parameter1 == DependentParameter && x.Parameter2==IndependentParameters[i].parameter && x.Parameter2Value == IndependentParameters[i].value).Select(x => x.LSL).ToList().Count> 0 ?  listparameterDependencie.Where(x => x.Parameter1 == DependentParameter && x.Parameter2Value == IndependentParameters[i].value).Select(x => x.LSL).ToList()[0]:"";
                    string usl = listparameterDependencie.Where(x => x.Parameter1 == DependentParameter && x.Parameter2 == IndependentParameters[i].parameter && x.Parameter2Value == IndependentParameters[i].value).Select(x => x.USL).ToList().Count >0 ? listparameterDependencie.Where(x => x.Parameter1 == DependentParameter && x.Parameter2Value == IndependentParameters[i].value).Select(x => x.USL).ToList()[0]:"";
                    if (lsl != "")
                    {
                        parametervalue.lsl = Convert.ToDecimal(lsl);
                    }
                    if (usl != "")
                    {
                        parametervalue.usl = Convert.ToDecimal(usl);
                    }
                    if (lsl == "" && usl == "")
                    {

                    }
                    else
                    {
                        listparametervalue.Add(parametervalue);
                    }
                   
                }
               
            }
            catch(Exception ex)
            {

            }
            decimal flsl = 0;
            decimal fusl = 0;
            if(listparametervalue.Count>0)
            {
                 flsl = listparametervalue.Max(x => x.lsl);
                 fusl = listparametervalue.Min(x => x.usl);
            }
            //|| (IndependentParameters.Count != listparametervalue.Count)
            if ((flsl==0 && fusl==0) || listparametervalue==null )
            {
                return "";
            }
            else
            {
                return flsl + " , " + fusl;
            }
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<DependentparameterDetails> getDependentparameterDetails(string Independentparametervalue)
        {
            List<DependentparameterDetails> listdependentparameterDetails = new List<DependentparameterDetails>();
            DependentparameterDetails dependentparameterDetails = null;
            try
            {
                var dependentParameter = listparameterDependencie.Where(x => x.Parameter2Value == Independentparametervalue).Select(x => x.Parameter1).ToList();
                for(int i=0;i< dependentParameter.Count; i++)
                {
                    dependentparameterDetails = new DependentparameterDetails();
                    dependentparameterDetails.dependentParameter = dependentParameter[i].ToString();
                    //var list = listparameterDependencie.Where(x => x.Parameter1 == dependentParameter[i].ToString()).Select(x => new { x.LSL, x.USL, x.Parameter2, x.Parameter2Value}).ToList();

                    List<ParameterDependency> listparameterDependency = new List<ParameterDependency>();
                    ParameterDependency parameterDependency = null;
                    var listindependentparam = listinputParameters.Where(x => x.Parameter == dependentParameter[i].ToString()).Select(x => x.IndependentParameter).ToList()[0];
                    string[] independentparam;
                    independentparam = listindependentparam.Split(';');
                    for(int ij=0;ij<independentparam.Length;ij++)
                    {
                        var list = listparameterDependencie.Where(x => x.Parameter1 == dependentParameter[i].ToString() && x.Parameter2==independentparam[ij].ToString()).Select(x => new { x.LSL, x.USL, x.Parameter2, x.Parameter2Value }).ToList();
                        for (int j=0; j < list.Count;j++)
                        {
                            parameterDependency = new ParameterDependency();
                            parameterDependency.LSL = list[j].LSL;
                            parameterDependency.USL = list[j].USL;
                            parameterDependency.Parameter2 = list[j].Parameter2;
                            parameterDependency.Parameter2Value = list[j].Parameter2Value;
                            listparameterDependency.Add(parameterDependency);

                        }
                       
                    }
                    
                    dependentparameterDetails.parameterDependencies = listparameterDependency;
                    listdependentparameterDetails.Add(dependentparameterDetails);
                }

            }
            catch(Exception ex)
            {

            }
            return listdependentparameterDetails;
        }
            

        protected void CreatenewSDocYes1_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string SDocName = "", Plunge = "", SubCategoryId = "";
                string SDoc = DBAccess.getSdocname("SDocName");
                if (SDoc == "-1")
                {
                    SDocName = "000001";
                }
                else
                {
                    SDocName = (Convert.ToInt64(SDoc) + 1).ToString("000000");
                }
                Plunge = "01";
                SubCategoryId = "0001";
                string date = DateTime.Now.ToString("ddMMyy");
                string time = DateTime.Now.ToString("HH:mm");
                string SystemDoc = "SDoc" + SDocName + "_" + Plunge + "_" + SubCategoryId + "_" + date + "_" + time;
                Session["ATKSDocID"] = SystemDoc;

                DataInputModuleParameter dataipmodule = new DataInputModuleParameter();
                dataipmodule.SDOcName = SDocName;
                dataipmodule.PlungeId = Plunge;
                dataipmodule.SubcategoryId = SubCategoryId;

                dataipmodule.Username = Session["EmpName"].ToString();

                bool result = saveupdateInputModule(dataipmodule, "Save", SystemDoc);
                if (result == false)
                {
                    bindSdocID();
                    BindData(Session["ATKSDocID"].ToString());
                }
            }
            catch(Exception ex)
            {

            }
          
           
            //saveInputModule.Visible = true;
            //btnDeleteSDoc.Visible = true;
        }

        protected void removeImageConfimYes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                string[] removeImageValue;
                removeImageValue = removeImage.Value.Split(',');
                int success = DBAccess.RemoveImage(Session["ATKSDocID"].ToString(), removeImageValue[0], removeImageValue[1]);
                string Source = string.Empty;
                if (removeImageValue[1] != "")
                {
                    Source = GetImagePath(removeImageValue[1].Replace("~/UploadImages/", ""));
                    if (File.Exists(Source))
                    {
                        File.Delete(Source);
                        //Logger.WriteDebugLog("Derived Parameter Signal Process- \n " + Source);
                    }

                }

                removeImage.Value = "";
                saveConfirmYes_ServerClick(sender, e);
                string id = Session["ATKSDocID"].ToString();
                bindDataInputModule(id);
            }
            catch(Exception ex)
            {

            }
          
        }
        static string appPath = HttpContext.Current.Server.MapPath("");
        public static string GetImagePath(string imgName)
        {
            string src;
            if (HttpContext.Current.Session["Language"] == null)
                src = Path.Combine(appPath, "UploadImages", imgName);
            else
            {
                if (HttpContext.Current.Session["Language"].ToString() != "en")
                    src = Path.Combine(appPath, "UploadImages-" + HttpContext.Current.Session["Language"].ToString() + "", imgName);
                else
                    src = Path.Combine(appPath, "UploadImages", imgName);
            }
            return src;
        }

        protected void allowEdit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lvGeneralInfo.Items.Count; i++)
            {
                string calculateflag = (lvGeneralInfo.Items[i].FindControl("gicalculatedflag") as Label).Text;
                if ((lvGeneralInfo.Items[i].FindControl("giobjectType") as Label).Text == "CheckBox")
                {
                    (lvGeneralInfo.Items[i].FindControl("gicbvalue") as CheckBox).Attributes["onclick"] = "return true";

                }
                if ((lvGeneralInfo.Items[i].FindControl("giobjectType") as Label).Text == "Drop Down")
                {
                    (lvGeneralInfo.Items[i].FindControl("giddlvalue") as DropDownList).Enabled = true;
                }
                if ((lvGeneralInfo.Items[i].FindControl("giobjectType") as Label).Text == "TextBox")
                {
                    if ((lvGeneralInfo.Items[i].FindControl("giDateType") as Label).Text == "Date")
                    {
                        (lvGeneralInfo.Items[i].FindControl("gitxtDate") as TextBox).ReadOnly = false;
                    }
                    else
                    if ((lvGeneralInfo.Items[i].FindControl("giDateType") as Label).Text == "Integer")
                    {
                        if (calculateflag == "True")
                        {
                            (lvGeneralInfo.Items[i].FindControl("gitxtallowNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvGeneralInfo.Items[i].FindControl("gitxtallowNumeric") as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    if ((lvGeneralInfo.Items[i].FindControl("giDateType") as Label).Text == "Decimal")
                    {
                        if (calculateflag == "True")
                        {
                            (lvGeneralInfo.Items[i].FindControl("gitxtallowDecimal") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvGeneralInfo.Items[i].FindControl("gitxtallowDecimal") as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    if ((lvGeneralInfo.Items[i].FindControl("giDateType") as Label).Text == "Alpha Numeric")
                    {
                        if (calculateflag == "True")
                        {
                            (lvGeneralInfo.Items[i].FindControl("gitxtallowalphaNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvGeneralInfo.Items[i].FindControl("gitxtallowalphaNumeric") as TextBox).ReadOnly = false;
                        }
                    }
                    else if ((lvGeneralInfo.Items[i].FindControl("gihdParameterName") as HiddenField).Value == "Comment")
                    {
                        if (calculateflag == "True")
                        {
                            (lvGeneralInfo.Items[i].FindControl("giComments") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvGeneralInfo.Items[i].FindControl("giComments") as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    {


                        if ((lvGeneralInfo.Items[i].FindControl("gihdParameterName") as HiddenField).Value == "SDoc ID")
                        {
                            (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            if (calculateflag == "True")
                            {
                                (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).ReadOnly = true;
                            }
                            else
                            {
                                (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).ReadOnly = false;
                            }
                        }
                    }
                }
            }



            #region -----MT-----
            for (int i = 0; i < lvMachinetool.Items.Count; i++)
            {
                string calculateflag = (lvMachinetool.Items[i].FindControl("mtcalculatedflag") as Label).Text;

                if ((lvMachinetool.Items[i].FindControl("mtobjectType") as Label).Text == "CheckBox")
                {
                    (lvMachinetool.Items[i].FindControl("mtcbvalue") as CheckBox).Attributes["onclick"] = "return true";

                }
                if ((lvMachinetool.Items[i].FindControl("mtobjectType") as Label).Text == "Drop Down")
                {
                    (lvMachinetool.Items[i].FindControl("mtddlvalue") as DropDownList).Enabled = true;
                }
                if ((lvMachinetool.Items[i].FindControl("mtobjectType") as Label).Text == "TextBox")
                {
                    if ((lvMachinetool.Items[i].FindControl("mtDateType") as Label).Text == "Date")
                    {
                        (lvMachinetool.Items[i].FindControl("mttxtDate") as TextBox).ReadOnly = false;
                    }
                    else
                    if ((lvMachinetool.Items[i].FindControl("mtDateType") as Label).Text == "Integer")
                    {
                        if (calculateflag == "True")
                        {
                            (lvMachinetool.Items[i].FindControl("mttxtallowNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvMachinetool.Items[i].FindControl("mttxtallowNumeric") as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    if ((lvMachinetool.Items[i].FindControl("mtDateType") as Label).Text == "Decimal")
                    {
                        if (calculateflag == "True")
                        {
                            (lvMachinetool.Items[i].FindControl("mttxtallowDecimal") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvMachinetool.Items[i].FindControl("mttxtallowDecimal") as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    if ((lvMachinetool.Items[i].FindControl("mtDateType") as Label).Text == "Alpha Numeric")
                    {
                        if (calculateflag == "True")
                        {
                            (lvMachinetool.Items[i].FindControl("mttxtallowalphaNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvMachinetool.Items[i].FindControl("mttxtallowalphaNumeric") as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    {
                        if (calculateflag == "True")
                        {
                            (lvMachinetool.Items[i].FindControl("mttxtvalue") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvMachinetool.Items[i].FindControl("mttxtvalue") as TextBox).ReadOnly = false;
                        }
                    }

                }
            }
            #endregion

            #region ----Consumable--
            for (int i = 0; i < lvConsumable.Items.Count; i++)
            {
                string calculateflag = (lvConsumable.Items[i].FindControl("cmcalculatedflag") as Label).Text;
                if ((lvConsumable.Items[i].FindControl("cmobjectType") as Label).Text == "CheckBox")
                {
                    (lvConsumable.Items[i].FindControl("cmcbvalue") as CheckBox).Attributes["onclick"] = "return true";

                }
                if ((lvConsumable.Items[i].FindControl("cmobjectType") as Label).Text == "Drop Down")
                {
                    (lvConsumable.Items[i].FindControl("cmddlvalue") as DropDownList).Enabled = true;
                }
                if ((lvConsumable.Items[i].FindControl("cmobjectType") as Label).Text == "TextBox")
                {
                    if ((lvConsumable.Items[i].FindControl("cmDateType") as Label).Text == "Date")
                    {
                        (lvConsumable.Items[i].FindControl("cmtxtDate") as TextBox).ReadOnly = false;
                    }
                    else
                    if ((lvConsumable.Items[i].FindControl("cmDateType") as Label).Text == "Integer")
                    {
                        if (calculateflag == "True")
                        {
                            (lvConsumable.Items[i].FindControl("cmtxtallowNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvConsumable.Items[i].FindControl("cmtxtallowNumeric") as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    if ((lvConsumable.Items[i].FindControl("cmDateType") as Label).Text == "Decimal")
                    {
                        if (calculateflag == "True")
                        {
                            (lvConsumable.Items[i].FindControl("cmtxtallowDecimal") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvConsumable.Items[i].FindControl("cmtxtallowDecimal") as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    if ((lvConsumable.Items[i].FindControl("cmDateType") as Label).Text == "Alpha Numeric")
                    {
                        if (calculateflag == "True")
                        {
                            (lvConsumable.Items[i].FindControl("cmtxtallowalphaNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvConsumable.Items[i].FindControl("cmtxtallowalphaNumeric") as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    {
                        if (calculateflag == "True")
                        {
                            (lvConsumable.Items[i].FindControl("cmtxtvalue") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvConsumable.Items[i].FindControl("cmtxtvalue") as TextBox).ReadOnly = false;
                        }
                    }

                }

            }
            #endregion

            #region -----Workpiece--
            for (int i = 0; i < lvWorkpiece.Items.Count; i++)
            {
                string calculateflag = (lvWorkpiece.Items[i].FindControl("wpcalculatedflag") as Label).Text;
                if ((lvWorkpiece.Items[i].FindControl("wpobjectType") as Label).Text == "CheckBox")
                {
                    (lvWorkpiece.Items[i].FindControl("wpcbvalue") as CheckBox).Attributes["onclick"] = "return true";

                }
                if ((lvWorkpiece.Items[i].FindControl("wpobjectType") as Label).Text == "Drop Down")
                {
                    (lvWorkpiece.Items[i].FindControl("wpddlvalue") as DropDownList).Enabled = true;
                }
                if ((lvWorkpiece.Items[i].FindControl("wpobjectType") as Label).Text == "TextBox")
                {
                    if ((lvWorkpiece.Items[i].FindControl("wpDateType") as Label).Text == "Date")
                    {
                        (lvWorkpiece.Items[i].FindControl("wptxtDate") as TextBox).ReadOnly = false;
                    }
                    else
                    if ((lvWorkpiece.Items[i].FindControl("wpDateType") as Label).Text == "Integer")
                    {
                        if (calculateflag == "True")
                        {
                            (lvWorkpiece.Items[i].FindControl("wptxtallowNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvWorkpiece.Items[i].FindControl("wptxtallowNumeric") as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    if ((lvWorkpiece.Items[i].FindControl("wpDateType") as Label).Text == "Decimal")
                    {
                        if (calculateflag == "True")
                        {
                            (lvWorkpiece.Items[i].FindControl("wptxtallowDecimal") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvWorkpiece.Items[i].FindControl("wptxtallowDecimal") as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    if ((lvWorkpiece.Items[i].FindControl("wpDateType") as Label).Text == "Alpha Numeric")
                    {
                        if (calculateflag == "True")
                        {
                            (lvWorkpiece.Items[i].FindControl("wptxtallowalphaNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvWorkpiece.Items[i].FindControl("wptxtallowalphaNumeric") as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    if ((lvWorkpiece.Items[i].FindControl("wphdParameterName") as HiddenField).Value == "Hardness")
                    {
                        (lvWorkpiece.Items[i].FindControl("txtHardness") as TextBox).ReadOnly = false;
                        (lvWorkpiece.Items[i].FindControl("ddlHardnessUnit") as DropDownList).Enabled = true;

                        continue;
                    }

                    else
                    {
                        if (calculateflag == "True")
                        {
                            (lvWorkpiece.Items[i].FindControl("wptxtvalue") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvWorkpiece.Items[i].FindControl("wptxtvalue") as TextBox).ReadOnly = false;
                        }
                    }

                }

                if ((lvWorkpiece.Items[i].FindControl("wpobjectType") as Label).Text == "Image")
                {

                    //(lvWorkpiece.Items[i].FindControl("addnewImage") as LinkButton).Enabled = true;
                    //(lvWorkpiece.Items[i].FindControl("LinkButton1") as LinkButton).Enabled = true;
                    //(lvWorkpiece.Items[i].FindControl("imageUpload1") as FileUpload).Enabled = true;
                    //(lvWorkpiece.Items[i].FindControl("txtimageName1") as TextBox).ReadOnly = false;
                    (lvWorkpiece.Items[i].FindControl("addnewImage") as LinkButton).Enabled = true;
                    (lvWorkpiece.Items[i].FindControl("addnewImage") as LinkButton).OnClientClick = "return addNewFileUpload(this);";
                    (lvWorkpiece.Items[i].FindControl("LinkButton1") as LinkButton).Enabled = true;
                    (lvWorkpiece.Items[i].FindControl("LinkButton1") as LinkButton).OnClientClick = "return removeNewFileUpload(this);";
                    (lvWorkpiece.Items[i].FindControl("imageUpload1") as FileUpload).Enabled = true;
                    (lvWorkpiece.Items[i].FindControl("txtimageName1") as TextBox).ReadOnly = false;
                    (lvWorkpiece.Items[i].FindControl("imagedone") as Button).Enabled = true;
                    (lvWorkpiece.Items[i].FindControl("imagedone") as Button).OnClientClick = "return imageUpadateDone();";
                    DataList dl = (lvWorkpiece.Items[i].FindControl("dlImages") as DataList);
                    for (int d = 0; d < dl.Items.Count; d++)
                    {
                        (dl.Items[d].FindControl("removeImage") as LinkButton).OnClientClick = "return removeImageFun(this)";
                    }
                    continue;
                }

            }
            #endregion

            #region ----OP---
            for (int i = 0; i < lvOperationalParameter.Items.Count; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (j == 0)
                    {
                        (lvOperationalParameter.Items[i].FindControl("ddlopfor") as DropDownList).Enabled = true;
                    }
                    if (j == 1)
                    {
                        // (lvOperationalParameter.Items[i].FindControl("hdnParamIDopFeedRate") as HiddenField).Value;
                        (lvOperationalParameter.Items[i].FindControl("opFeedRateDecimal") as TextBox).ReadOnly = false;
                    }
                    else if (j == 2)
                    {
                        //dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopDOC") as HiddenField).Value;
                        (lvOperationalParameter.Items[i].FindControl("opDOCDecimal") as TextBox).ReadOnly = false;

                    }
                    else if (j == 3)
                    {
                        //dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopDOC") as HiddenField).Value;
                        (lvOperationalParameter.Items[i].FindControl("opFaceDecimal") as TextBox).ReadOnly = false;

                    }
                    else if (j == 4)
                    {
                        // dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopWorkRPM") as HiddenField).Value;
                        (lvOperationalParameter.Items[i].FindControl("opWorkRPMDecimal") as TextBox).ReadOnly = false;
                    }
                    else if (j == 5)
                    {
                        // dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopWheelms") as HiddenField).Value;
                        (lvOperationalParameter.Items[i].FindControl("opWheelmsDecimal") as TextBox).ReadOnly = false;
                    }
                    else if (j == 6)
                    {
                        //dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopWorkms") as HiddenField).Value;
                        (lvOperationalParameter.Items[i].FindControl("opWorkmsDecimal") as TextBox).ReadOnly = false;
                    }
                    else if (j == 7)
                    {
                        //  dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopWheelRPM") as HiddenField).Value;
                        (lvOperationalParameter.Items[i].FindControl("opWheelRPMDecimal") as TextBox).ReadOnly = false;
                    }

                }

            }

            for (int i = 0; i < lvOperationalParameterGrind.Items.Count; i++)
            {
                string calculateflag = (lvOperationalParameterGrind.Items[i].FindControl("opcalculatedflag") as Label).Text;

                if ((lvOperationalParameterGrind.Items[i].FindControl("opobjectType") as Label).Text == "CheckBox")
                {
                    (lvOperationalParameterGrind.Items[i].FindControl("opcbvalue") as CheckBox).Attributes["onclick"] = "return true";

                }
                if ((lvOperationalParameterGrind.Items[i].FindControl("opobjectType") as Label).Text == "Drop Down")
                {
                    (lvOperationalParameterGrind.Items[i].FindControl("opddlvalue") as DropDownList).Enabled = true;
                }
                if ((lvOperationalParameterGrind.Items[i].FindControl("opobjectType") as Label).Text == "TextBox")
                {
                    if ((lvOperationalParameterGrind.Items[i].FindControl("opDateType") as Label).Text == "Date")
                    {
                        (lvOperationalParameterGrind.Items[i].FindControl("optxtDate") as TextBox).ReadOnly = false;
                    }
                    else
                    if ((lvOperationalParameterGrind.Items[i].FindControl("opDateType") as Label).Text == "Integer")
                    {
                        if (calculateflag == "True")
                        {
                            (lvOperationalParameterGrind.Items[i].FindControl("optxtallowNumeric") as TextBox).ReadOnly = true; ;
                        }
                        else
                        {
                            (lvOperationalParameterGrind.Items[i].FindControl("optxtallowNumeric") as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    if ((lvOperationalParameterGrind.Items[i].FindControl("opDateType") as Label).Text == "Decimal")
                    {
                        if (calculateflag == "True")
                        {
                            (lvOperationalParameterGrind.Items[i].FindControl("optxtallowDecimal") as TextBox).ReadOnly = true; ;
                        }
                        else
                        {
                            (lvOperationalParameterGrind.Items[i].FindControl("optxtallowDecimal") as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    if ((lvOperationalParameterGrind.Items[i].FindControl("opDateType") as Label).Text == "Alpha Numeric")
                    {
                        if (calculateflag == "True")
                        {
                            (lvOperationalParameterGrind.Items[i].FindControl("optxtallowalphaNumeric") as TextBox).ReadOnly = true; ;
                        }
                        else
                        {
                            (lvOperationalParameterGrind.Items[i].FindControl("optxtallowalphaNumeric") as TextBox).ReadOnly = false;
                        }
                    }
                    //Add Hardness 
                    else
                    {
                        if (calculateflag == "True")
                        {
                            (lvOperationalParameterGrind.Items[i].FindControl("optxtvalue") as TextBox).ReadOnly = true; ;
                        }
                        else
                        {
                            (lvOperationalParameterGrind.Items[i].FindControl("optxtvalue") as TextBox).ReadOnly = false;
                        }
                    }

                }

            }



            //dreassing
            for (int i = 0; i < lvOPDressing.Items.Count; i++)
            {

                string calculateflag = (lvOPDressing.Items[i].FindControl("opcalculatedflag") as Label).Text;
                if ((lvOPDressing.Items[i].FindControl("opobjectType") as Label).Text == "CheckBox")
                {
                    (lvOPDressing.Items[i].FindControl("opcbvalue") as CheckBox).Attributes["onclick"] = "return true";

                }
                if ((lvOPDressing.Items[i].FindControl("opobjectType") as Label).Text == "Drop Down")
                {
                    (lvOPDressing.Items[i].FindControl("opddlvalue") as DropDownList).Enabled = true;
                }
                if ((lvOPDressing.Items[i].FindControl("opobjectType") as Label).Text == "TextBox")
                {
                    if ((lvOPDressing.Items[i].FindControl("opDateType") as Label).Text == "Date")
                    {
                        (lvOPDressing.Items[i].FindControl("optxtDate") as TextBox).ReadOnly = false;
                    }
                    else
                    if ((lvOPDressing.Items[i].FindControl("opDateType") as Label).Text == "Integer")
                    {
                        if (calculateflag == "True")
                        {
                            (lvOPDressing.Items[i].FindControl("optxtallowNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvOPDressing.Items[i].FindControl("optxtallowNumeric") as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    if ((lvOPDressing.Items[i].FindControl("opDateType") as Label).Text == "Decimal")
                    {
                        if (calculateflag == "True")
                        {
                            (lvOPDressing.Items[i].FindControl("optxtallowDecimal") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvOPDressing.Items[i].FindControl("optxtallowDecimal") as TextBox).ReadOnly = false;
                        }
                    }
                    else
                    if ((lvOPDressing.Items[i].FindControl("opDateType") as Label).Text == "Alpha Numeric")
                    {
                        if (calculateflag == "True")
                        {
                            (lvOPDressing.Items[i].FindControl("optxtallowalphaNumeric") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvOPDressing.Items[i].FindControl("optxtallowalphaNumeric") as TextBox).ReadOnly = false;
                        }
                    }
                    //Add Hardness 
                    else
                    {
                        if (calculateflag == "True")
                        {
                            (lvOPDressing.Items[i].FindControl("optxtvalue") as TextBox).ReadOnly = true;
                        }
                        else
                        {
                            (lvOPDressing.Items[i].FindControl("optxtvalue") as TextBox).ReadOnly = false;
                        }
                    }

                }

            }

            #endregion

            #region ----QP---
            for (int i = 0; i < lvQualityParameter.Items.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j == 0)
                    {
                        //  dataipmodule.PrameterId = (lvQualityParameter.Items[i].FindControl("hdnParamIDqpTargetLower") as HiddenField).Value;
                        (lvQualityParameter.Items[i].FindControl("qpTargetLower") as TextBox).ReadOnly = false;
                    }
                    else if (j == 1)
                    {
                        //   dataipmodule.PrameterId = (lvQualityParameter.Items[i].FindControl("hdnParamIDqpTargetUppper") as HiddenField).Value;
                        (lvQualityParameter.Items[i].FindControl("qpTargetUppper") as TextBox).ReadOnly = false;
                    }
                    else if (j == 2)
                    {
                        //  dataipmodule.PrameterId = (lvQualityParameter.Items[i].FindControl("hdnParamIDqpAchievedLower") as HiddenField).Value;
                        (lvQualityParameter.Items[i].FindControl("qpAchievedLower") as TextBox).ReadOnly = false;
                    }
                    else if (j == 3)
                    {
                        // dataipmodule.PrameterId = (lvQualityParameter.Items[i].FindControl("hdnParamIDAchievedUppper") as HiddenField).Value;
                        (lvQualityParameter.Items[i].FindControl("qpAchievedUppper") as TextBox).ReadOnly = false;
                    }

                }

            }
            #endregion
        }

        protected void templateCreate_ServerClick(object sender, EventArgs e)
        {
            string SDocName = "000000", Plunge = "01", SubCategoryId = "", SaveAsSdoc = "";
            long  subcategory = 0;
            string CategoryId = DBAccess.getSystemDocumentName(000000, 01, subcategory, "SubCategory");
            if (CategoryId == "-1")
            {
                SubCategoryId = "0001";
            }
            else
            {
                SubCategoryId = (Convert.ToInt64(CategoryId) + 1).ToString("0000");
            }
            string date = DateTime.Now.ToString("ddMMyy");
            string time = DateTime.Now.ToString("HH:mm");
            string SystemDoc = "SDoc000000_01_" + SubCategoryId + "_" + date + "_" + time;


            DataInputModuleParameter dataipmodule = new DataInputModuleParameter();
            dataipmodule.SDOcName = SDocName;
            dataipmodule.PlungeId = Plunge;
            dataipmodule.SubcategoryId = SubCategoryId;
            Session["ATKSDocID"] = SystemDoc;

            dataipmodule.Username = Session["EmpName"].ToString();
           bool result= saveupdateInputModule(dataipmodule, "Save", SystemDoc);
            if(result==false)
            {
                bindSdocID();
                BindData(Session["ATKSDocID"].ToString());
            }
            //saveInputModule.Visible = true;
            //btnDeleteSDoc.Visible = true;
        }

        private bool saveupdateInputModule(DataInputModuleParameter dataipmodule,string saveorupdate, string SystemDoc)
        {
            
            #region ----General Information-----
            for (int i = 0; i < lvGeneralInfo.Items.Count; i++)
            {
                //Pass SDoc,User,Datetime
                dataipmodule.CustomeName = (lvGeneralInfo.Items[i].FindControl("item") as Label).Text;
                dataipmodule.Prameter = (lvGeneralInfo.Items[i].FindControl("gihdParameterName") as HiddenField).Value;
                dataipmodule.PrameterId = (lvGeneralInfo.Items[i].FindControl("giParameterID") as Label).Text;
                if (dataipmodule.Prameter == "" && dataipmodule.PrameterId == "")
                {
                    continue;
                }
              
                if ((lvGeneralInfo.Items[i].FindControl("giobjectType") as Label).Text == "CheckBox")
                {
                    if ((lvGeneralInfo.Items[i].FindControl("gicbvalue") as CheckBox).Checked == true)
                        dataipmodule.Value = "Yes";
                    else
                        dataipmodule.Value = "No";
                }
                if ((lvGeneralInfo.Items[i].FindControl("giobjectType") as Label).Text == "Drop Down")
                {
                    //dataipmodule.Value = (lvGeneralInfo.Items[i].FindControl("giddlvalue") as DropDownList).Text;
                    dataipmodule.Value = (lvGeneralInfo.Items[i].FindControl("hfddlvalue") as HiddenField).Value;
                }
                if ((lvGeneralInfo.Items[i].FindControl("giobjectType") as Label).Text == "TextBox")
                {
                    if ((lvGeneralInfo.Items[i].FindControl("giDateType") as Label).Text == "Date")
                    {

                        dataipmodule.Value = (lvGeneralInfo.Items[i].FindControl("gitxtDate") as TextBox).Text ==""?"":Convert.ToDateTime((lvGeneralInfo.Items[i].FindControl("gitxtDate") as TextBox).Text).ToString("dd/MM/yyyy HH:mm");
                    }
                    else
                    if ((lvGeneralInfo.Items[i].FindControl("giDateType") as Label).Text == "Integer")
                    {
                        dataipmodule.Value = (lvGeneralInfo.Items[i].FindControl("gitxtallowNumeric") as TextBox).Text;
                    }
                    else
                    if ((lvGeneralInfo.Items[i].FindControl("giDateType") as Label).Text == "Decimal")
                    {
                        dataipmodule.Value = (lvGeneralInfo.Items[i].FindControl("gitxtallowDecimal") as TextBox).Text;
                    }
                    else
                    if ((lvGeneralInfo.Items[i].FindControl("giDateType") as Label).Text == "Alpha Numeric")
                    {
                        dataipmodule.Value = (lvGeneralInfo.Items[i].FindControl("gitxtallowalphaNumeric") as TextBox).Text;
                    }
                    else if ((lvGeneralInfo.Items[i].FindControl("gihdParameterName") as HiddenField).Value == "Comment")
                    {
                        dataipmodule.Value = (lvGeneralInfo.Items[i].FindControl("giComments") as TextBox).Text;
                    }
                    else
                    {
                        dataipmodule.Value = (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).Text;
                    }
                }
                if (saveorupdate == "Save")
                {
                    if (dataipmodule.Prameter == "SDoc ID")
                    {
                        dataipmodule.Value = SystemDoc;
                    }

                }
                int result = DBAccess.InsertUpdateInputModule(Session["ATKSDocID"].ToString(), dataipmodule, saveorupdate);
                if (result.Equals(0))
                {
                    if (saveorupdate == "Save")
                    {
                        DBAccess.DeleteSDocID(Session["ATKSDocID"].ToString());
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);
                    return true;

                }
                else
                if (result == -2)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('SDoc ID already exists.');", true);
                    return true;
                }
                else if (result == -1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('SDoc ID not exists.');", true);
                    return true;
                }

            }

            #endregion

            #region ----Machine Tool-----
            for (int i = 0; i < lvMachinetool.Items.Count; i++)
            {
                //Pass SDoc,User,Datetime
                dataipmodule.CustomeName = (lvMachinetool.Items[i].FindControl("mtItem") as Label).Text;
                dataipmodule.Prameter = (lvMachinetool.Items[i].FindControl("mthdParameterName") as HiddenField).Value;
                dataipmodule.PrameterId = (lvMachinetool.Items[i].FindControl("mtParameterID") as Label).Text;
                if (dataipmodule.Prameter == "" && dataipmodule.PrameterId == "")
                {
                    continue;
                }
                if ((lvMachinetool.Items[i].FindControl("mtobjectType") as Label).Text == "CheckBox")
                {
                    if ((lvMachinetool.Items[i].FindControl("mtcbvalue") as CheckBox).Checked == true)
                        dataipmodule.Value = "Yes";
                    else
                        dataipmodule.Value = "No";
                }
                if ((lvMachinetool.Items[i].FindControl("mtobjectType") as Label).Text == "Drop Down")
                {
                    dataipmodule.Value = (lvMachinetool.Items[i].FindControl("hfddlvalue") as HiddenField).Value;
                    //dataipmodule.Value = (lvMachinetool.Items[i].FindControl("mtddlvalue") as DropDownList).Text;
                }
                if ((lvMachinetool.Items[i].FindControl("mtobjectType") as Label).Text == "TextBox")
                {
                    if ((lvMachinetool.Items[i].FindControl("mtDateType") as Label).Text == "Date")
                    {
                        dataipmodule.Value = (lvMachinetool.Items[i].FindControl("mttxtDate") as TextBox).Text == "" ? "" : Convert.ToDateTime((lvMachinetool.Items[i].FindControl("mttxtDate") as TextBox).Text).ToString("dd/MM/yyyy HH:mm");
                    }
                    else
                    if ((lvMachinetool.Items[i].FindControl("mtDateType") as Label).Text == "Integer")
                    {
                        dataipmodule.Value = (lvMachinetool.Items[i].FindControl("mttxtallowNumeric") as TextBox).Text;
                    }
                    else
                    if ((lvMachinetool.Items[i].FindControl("mtDateType") as Label).Text == "Decimal")
                    {
                        dataipmodule.Value = (lvMachinetool.Items[i].FindControl("mttxtallowDecimal") as TextBox).Text;
                    }
                    else
                    if ((lvMachinetool.Items[i].FindControl("mtDateType") as Label).Text == "Alpha Numeric")
                    {
                        dataipmodule.Value = (lvMachinetool.Items[i].FindControl("mttxtallowalphaNumeric") as TextBox).Text;
                    }
                    else
                    {
                        dataipmodule.Value = (lvMachinetool.Items[i].FindControl("mttxtvalue") as TextBox).Text;
                    }

                }

                int result = DBAccess.InsertUpdateInputModule(Session["ATKSDocID"].ToString(), dataipmodule, saveorupdate);
                if (result.Equals(0))
                {
                    if (saveorupdate == "Save")
                    {
                        DBAccess.DeleteSDocID(Session["ATKSDocID"].ToString());
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);
                    return true;

                }
                else
                if (result == -2)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('SDoc ID already exists.');", true);
                    return true;
                }
                else if (result == -1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('SDoc ID not exists.');", true);
                    return true;
                }
            }

            #endregion

            #region ----Consumables-----
            for (int i = 0; i < lvConsumable.Items.Count; i++)
            {
                //Pass SDoc,User,Datetime
                dataipmodule.CustomeName = (lvConsumable.Items[i].FindControl("cmItem") as Label).Text;
                dataipmodule.Prameter = (lvConsumable.Items[i].FindControl("cmhdParameterName") as HiddenField).Value;
                dataipmodule.PrameterId = (lvConsumable.Items[i].FindControl("cmParameterID") as Label).Text;
                if (dataipmodule.Prameter == "" && dataipmodule.PrameterId == "")
                {
                    continue;
                }
                if ((lvConsumable.Items[i].FindControl("cmobjectType") as Label).Text == "CheckBox")
                {
                    if ((lvConsumable.Items[i].FindControl("cmcbvalue") as CheckBox).Checked == true)
                        dataipmodule.Value = "Yes";
                    else
                        dataipmodule.Value = "No";
                }
                if ((lvConsumable.Items[i].FindControl("cmobjectType") as Label).Text == "Drop Down")
                {
                    dataipmodule.Value = (lvConsumable.Items[i].FindControl("hfddlvalue") as HiddenField).Value;
                    //dataipmodule.Value = (lvConsumable.Items[i].FindControl("cmddlvalue") as DropDownList).Text;
                }
                if ((lvConsumable.Items[i].FindControl("cmobjectType") as Label).Text == "TextBox")
                {
                    if ((lvConsumable.Items[i].FindControl("cmDateType") as Label).Text == "Date")
                    {
                        dataipmodule.Value = (lvConsumable.Items[i].FindControl("cmtxtDate") as TextBox).Text == "" ? "" : Convert.ToDateTime((lvConsumable.Items[i].FindControl("cmtxtDate") as TextBox).Text).ToString("dd/MM/yyyy HH:mm");
                    }
                    else
                    if ((lvConsumable.Items[i].FindControl("cmDateType") as Label).Text == "Integer")
                    {
                        if (dataipmodule.Prameter == "Cutter Width (mm)")
                        {
                            CutterWidth = (lvConsumable.Items[i].FindControl("cmtxtallowNumeric") as TextBox).Text;
                        }
                        if (dataipmodule.Prameter == "Dresser Dia")
                        {
                            DresserDia = (lvConsumable.Items[i].FindControl("cmtxtallowNumeric") as TextBox).Text;
                        }
                        dataipmodule.Value = (lvConsumable.Items[i].FindControl("cmtxtallowNumeric") as TextBox).Text;
                    }
                    else
                    if ((lvConsumable.Items[i].FindControl("cmDateType") as Label).Text == "Decimal")
                    {
                        if (dataipmodule.Prameter == "Cutter Width (mm)")
                        {
                            CutterWidth = (lvConsumable.Items[i].FindControl("cmtxtallowDecimal") as TextBox).Text;
                        }

                        if (dataipmodule.Prameter == "Dresser Dia")
                        {
                            DresserDia = (lvConsumable.Items[i].FindControl("cmtxtallowDecimal") as TextBox).Text;
                        }
                        dataipmodule.Value = (lvConsumable.Items[i].FindControl("cmtxtallowDecimal") as TextBox).Text;
                    }
                    else
                    if ((lvConsumable.Items[i].FindControl("cmDateType") as Label).Text == "Alpha Numeric")
                    {
                        if (dataipmodule.Prameter == "Cutter Width (mm)")
                        {
                            CutterWidth = (lvConsumable.Items[i].FindControl("cmtxtallowalphaNumeric") as TextBox).Text;
                        }
                        if (dataipmodule.Prameter == "Dresser Dia")
                        {
                            DresserDia = (lvConsumable.Items[i].FindControl("cmtxtallowalphaNumeric") as TextBox).Text;
                        }
                        dataipmodule.Value = (lvConsumable.Items[i].FindControl("cmtxtallowalphaNumeric") as TextBox).Text;
                    }
                    else
                    {
                        if (dataipmodule.Prameter == "Cutter Width (mm)")
                        {
                            CutterWidth = (lvConsumable.Items[i].FindControl("cmtxtvalue") as TextBox).Text;
                        }
                        if (dataipmodule.Prameter == "Dresser Dia")
                        {
                            DresserDia = (lvConsumable.Items[i].FindControl("cmtxtvalue") as TextBox).Text;
                        }
                        dataipmodule.Value = (lvConsumable.Items[i].FindControl("cmtxtvalue") as TextBox).Text;
                    }

                }
                int result = DBAccess.InsertUpdateInputModule(Session["ATKSDocID"].ToString(), dataipmodule, saveorupdate);
                if (result.Equals(0))
                {
                    if (saveorupdate == "Save")
                    {
                        DBAccess.DeleteSDocID(Session["ATKSDocID"].ToString());
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);
                    return true;
                }
                else
                if (result == -2)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('SDoc ID already exists.');", true);
                    return true;
                }
                else if (result == -1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('SDoc ID not exists.');", true);
                    return true;
                }
            }

            #endregion


            #region ----Workpiece-----
            for (int i = 0; i < lvWorkpiece.Items.Count; i++)
            {
                //Pass SDoc,User,Datetime
                dataipmodule.CustomeName = (lvWorkpiece.Items[i].FindControl("wpItem") as Label).Text;
                dataipmodule.Prameter = (lvWorkpiece.Items[i].FindControl("wphdParameterName") as HiddenField).Value;
                dataipmodule.PrameterId = (lvWorkpiece.Items[i].FindControl("wpParameterID") as Label).Text;
                if (dataipmodule.Prameter == "" && dataipmodule.PrameterId == "")
                {
                    continue;
                }

                if ((lvWorkpiece.Items[i].FindControl("wpobjectType") as Label).Text == "CheckBox")
                {
                    if ((lvWorkpiece.Items[i].FindControl("wpcbvalue") as CheckBox).Checked == true)
                        dataipmodule.Value = "Yes";
                    else
                        dataipmodule.Value = "No";
                }
                if ((lvWorkpiece.Items[i].FindControl("wpobjectType") as Label).Text == "Drop Down")
                {
                    //dataipmodule.Value = (lvWorkpiece.Items[i].FindControl("wpddlvalue") as DropDownList).Text;
                    dataipmodule.Value = (lvWorkpiece.Items[i].FindControl("hfddlvalue") as HiddenField).Value;
                }
                if ((lvWorkpiece.Items[i].FindControl("wpobjectType") as Label).Text == "TextBox")
                {
                    if ((lvWorkpiece.Items[i].FindControl("wpDateType") as Label).Text == "Date")
                    {
                        dataipmodule.Value = (lvWorkpiece.Items[i].FindControl("wptxtDate") as TextBox).Text == "" ? "" : Convert.ToDateTime((lvWorkpiece.Items[i].FindControl("wptxtDate") as TextBox).Text).ToString("dd/MM/yyyy HH:mm");
                    }
                    else
                    if ((lvWorkpiece.Items[i].FindControl("wpDateType") as Label).Text == "Integer")
                    {
                        if (dataipmodule.Prameter == "Initial Component Dia (mm)")
                        {
                            InitialComponentDia = (lvWorkpiece.Items[i].FindControl("wptxtallowNumeric") as TextBox).Text;
                        }
                        if (dataipmodule.Prameter == "Input Component Dia-OD (mm)")
                        {
                            InputComponentDiaOD = (lvWorkpiece.Items[i].FindControl("wptxtallowNumeric") as TextBox).Text;
                        }
                        dataipmodule.Value = (lvWorkpiece.Items[i].FindControl("wptxtallowNumeric") as TextBox).Text;
                    }
                    else
                    if ((lvWorkpiece.Items[i].FindControl("wpDateType") as Label).Text == "Decimal")
                    {
                        if (dataipmodule.Prameter == "Initial Component Dia (mm)")
                        {
                            InitialComponentDia = (lvWorkpiece.Items[i].FindControl("wptxtallowDecimal") as TextBox).Text;
                        }
                        if (dataipmodule.Prameter == "Input Component Dia-OD (mm)")
                        {
                            InputComponentDiaOD = (lvWorkpiece.Items[i].FindControl("wptxtallowDecimal") as TextBox).Text;
                        }
                        dataipmodule.Value = (lvWorkpiece.Items[i].FindControl("wptxtallowDecimal") as TextBox).Text;
                    }
                    else
                    if ((lvWorkpiece.Items[i].FindControl("wpDateType") as Label).Text == "Alpha Numeric")
                    {
                        if (dataipmodule.Prameter == "Initial Component Dia (mm)")
                        {
                            InitialComponentDia = (lvWorkpiece.Items[i].FindControl("wptxtallowalphaNumeric") as TextBox).Text;
                        }
                        if (dataipmodule.Prameter == "Input Component Dia-OD (mm)")
                        {
                            InputComponentDiaOD = (lvWorkpiece.Items[i].FindControl("wptxtallowalphaNumeric") as TextBox).Text;
                        }
                        dataipmodule.Value = (lvWorkpiece.Items[i].FindControl("wptxtallowalphaNumeric") as TextBox).Text;
                    }
                    else
                    if ((lvWorkpiece.Items[i].FindControl("wphdParameterName") as HiddenField).Value == "Hardness")
                    {
                        dataipmodule.Value = (lvWorkpiece.Items[i].FindControl("txtHardness") as TextBox).Text;
                        int result1 = DBAccess.InsertUpdateInputModule(Session["ATKSDocID"].ToString(), dataipmodule, saveorupdate);
                        dataipmodule.PrameterId = "71";
                        //  dataipmodule.Value = (lvWorkpiece.Items[i].FindControl("ddlHardnessUnit") as DropDownList).Text;
                        if ((lvWorkpiece.Items[i].FindControl("ddlHardnessUnit") as DropDownList).SelectedIndex == 0)
                        {
                            dataipmodule.Value = "";
                        }
                        else
                        {
                            dataipmodule.Value = (lvWorkpiece.Items[i].FindControl("ddlHardnessUnit") as DropDownList).SelectedItem.Text;
                        }
                        int result2 = DBAccess.InsertUpdateInputModule(Session["ATKSDocID"].ToString(), dataipmodule, saveorupdate);
                        continue;
                    }

                    else
                    {
                        if (dataipmodule.Prameter == "Initial Component Dia (mm)")
                        {
                            InitialComponentDia = (lvWorkpiece.Items[i].FindControl("wptxtvalue") as TextBox).Text;
                        }
                         if (dataipmodule.Prameter == "Input Component Dia-OD (mm)")
                        {
                            InputComponentDiaOD = (lvWorkpiece.Items[i].FindControl("wptxtvalue") as TextBox).Text;
                        }
                        dataipmodule.Value = (lvWorkpiece.Items[i].FindControl("wptxtvalue") as TextBox).Text;
                    }

                }

                if ((lvWorkpiece.Items[i].FindControl("wpobjectType") as Label).Text == "Image")
                {

                    if (Session["imagesDetails"] != null)
                    {
                        List<string> list = new List<string>();
                        list = (List<string>)Session["imagesDetails"];
                        for (int k = 0; k < list.Count; k = k + 2)
                        {
                            dataipmodule.ImagePath = list[k];
                            dataipmodule.Value = list[k + 1];
                            int resul1t = DBAccess.InsertUpdateInputModuleForImage(Session["ATKSDocID"].ToString(), dataipmodule, "Save");

                        }
                        Session["imagesDetails"] = null;
                    }
                    if(saveorupdate=="Save")
                    {
                        DataList dl = (lvWorkpiece.Items[i].FindControl("dlImages") as DataList);
                        for (int j = 0; j < dl.Items.Count; j++)
                        {
                            dataipmodule.ImagePath = (dl.Items[j].FindControl("lblImageName") as Label).Text;
                            dataipmodule.Value = "~/" + (dl.Items[j].FindControl("img") as HtmlImage).Src;
                            int resul1t = DBAccess.InsertUpdateInputModuleForImage(Session["ATKSDocID"].ToString(), dataipmodule, saveorupdate);
                        }

                    }
                    continue;
                }
                int result = DBAccess.InsertUpdateInputModule(Session["ATKSDocID"].ToString(), dataipmodule, saveorupdate);


                if (result.Equals(0))
                {
                    if (saveorupdate == "Save")
                    {
                        DBAccess.DeleteSDocID(Session["ATKSDocID"].ToString());
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);
                    return true;

                }
                else
                 if (result == -2)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('SDoc ID already exists.');", true);
                    return true;
                }
                else if (result == -1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('SDoc ID not exists.');", true);
                    return true;
                }
            }

            #endregion

            #region ----Operational Parameters-----

            //Grinding

            for (int i = 0; i < lvOperationalParameter.Items.Count; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (j == 0)
                    {
                        continue;
                    }
                    if (j == 1)
                    {
                        dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopFeedRate") as HiddenField).Value;
                        dataipmodule.Value = (lvOperationalParameter.Items[i].FindControl("opFeedRateDecimal") as TextBox).Text;
                        dataipmodule.CalculatedFormula = "";
                        dataipmodule.GrindingProcess = "";
                    }
                    else if (j == 2)
                    {
                        dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopDOC") as HiddenField).Value;
                        dataipmodule.Value = (lvOperationalParameter.Items[i].FindControl("opDOCDecimal") as TextBox).Text;
                        dataipmodule.CalculatedFormula = "";
                        dataipmodule.GrindingProcess = "";
                    }
                    else if (j == 3)
                    {
                        dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopFace") as HiddenField).Value;
                        dataipmodule.Value = (lvOperationalParameter.Items[i].FindControl("opFaceDecimal") as TextBox).Text;
                        dataipmodule.CalculatedFormula = "";
                        dataipmodule.GrindingProcess = "";
                    }
                    else if (j == 4)
                    {
                        dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopWorkRPM") as HiddenField).Value;
                        dataipmodule.Value = (lvOperationalParameter.Items[i].FindControl("opWorkRPMDecimal") as TextBox).Text;
                        dataipmodule.CalculatedFormula = "";
                        dataipmodule.GrindingProcess = "";
                    }
                    else if (j == 5)
                    {
                        dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopWheelms") as HiddenField).Value;
                        dataipmodule.Value = (lvOperationalParameter.Items[i].FindControl("opWheelmsDecimal") as TextBox).Text;
                        dataipmodule.CalculatedFormula = "";
                        dataipmodule.GrindingProcess = "";
                    }
                    else if (j == 6)
                    {
                        dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopWorkms") as HiddenField).Value;
                        //dataipmodule.Value = (lvOperationalParameter.Items[i].FindControl("opWorkmsDecimal") as TextBox).Text;
                        if ((lvOperationalParameter.Items[i].FindControl("opWorkmsDecimal") as TextBox).ReadOnly)
                        {

                            dataipmodule.Value = Request.Form[(lvOperationalParameter.Items[i].FindControl("opWorkmsDecimal") as TextBox).UniqueID];
                        }
                        else
                        {
                            dataipmodule.Value = (lvOperationalParameter.Items[i].FindControl("opWorkmsDecimal") as TextBox).Text;
                        }
                        dataipmodule.GrindingProcess = (lvOperationalParameter.Items[i].FindControl("ddlopfor") as DropDownList).Text;
                        string parametername = (lvOperationalParameter.Items[i].FindControl("opWorkmsParameter") as Label).Text;
                        string workrpm = "", formulastring = "";
                        string formula = DBAccess.getFormulaList(parametername);
                        string[] parameter = formula.Split('*');
                        for (int f = 0; f < lvOperationalParameter.Items.Count; f++)
                        {
                            string param = (lvOperationalParameter.Items[f].FindControl("opWorkRPMParameter") as Label).Text;
                            string param1 = parameter[2].Split('/')[0].Trim();
                            if ((lvOperationalParameter.Items[f].FindControl("opWorkRPMParameter") as Label).Text.Trim() == parameter[2].Split(new string[] { ")/" }, StringSplitOptions.None)[0].Trim())
                            {
                                workrpm = (lvOperationalParameter.Items[f].FindControl("opWorkRPMDecimal") as TextBox).Text;
                            }
                        }
                        if (InputComponentDiaOD != "" && workrpm != "")
                        {
                            formulastring = "(" + Math.PI + " * " + InputComponentDiaOD + " * " + workrpm + ") / 60000";
                        }
                        else if (InputComponentDiaOD == "" && workrpm != "")
                        {
                            formulastring = "(" + Math.PI + " * " + 0 + " * " + workrpm + ") / 60000";
                        }
                        else if (InputComponentDiaOD != "" && workrpm == "")
                        {
                            formulastring = "(" + Math.PI + " * " + InputComponentDiaOD + " * " + 0 + ") / 60000";
                        }
                        else
                        {
                            formulastring = "(" + Math.PI + " * " + 0 + " * " + 0 + ") / 60000";
                        }
                        dataipmodule.CalculatedFormula = parametername + " ; " + Session["ATKSDocID"] + " ; " + formula + " ; " + formulastring;
                    }
                    else if (j == 7)
                    {
                        dataipmodule.PrameterId = (lvOperationalParameter.Items[i].FindControl("hdnParamIDopWheelRPM") as HiddenField).Value;
                        //  dataipmodule.Value = (lvOperationalParameter.Items[i].FindControl("opWheelRPMDecimal") as TextBox).Text;
                        if ((lvOperationalParameter.Items[i].FindControl("opWheelRPMDecimal") as TextBox).ReadOnly)
                        {

                            dataipmodule.Value = Request.Form[(lvOperationalParameter.Items[i].FindControl("opWheelRPMDecimal") as TextBox).UniqueID];
                        }
                        else
                        {
                            dataipmodule.Value = (lvOperationalParameter.Items[i].FindControl("opWheelRPMDecimal") as TextBox).Text;
                        }

                        string parametername = (lvOperationalParameter.Items[i].FindControl("opWheelRPMPrameter") as Label).Text;
                        string wheelms = "", formulastring = "", currentwheeldia = "";
                        string formula = DBAccess.getFormulaList(parametername);
                        string parameter = formula.Split(new string[] { " / " }, StringSplitOptions.None)[0].Split('*')[0].Substring(1);
                        for (int f = 0; f < lvOperationalParameter.Items.Count; f++)
                        {
                            string param = (lvOperationalParameter.Items[f].FindControl("opWheelmsParameter") as Label).Text;
                            if ((lvOperationalParameter.Items[f].FindControl("opWheelmsParameter") as Label).Text.Trim() == parameter.Trim())
                            {
                                wheelms = (lvOperationalParameter.Items[f].FindControl("opWheelmsDEcimal") as TextBox).Text;
                            }
                        }

                        for (int f = 0; f < lvOperationalParameterGrind.Items.Count; f++)
                        {
                            if ((lvOperationalParameterGrind.Items[f].FindControl("ophdParameterName") as HiddenField).Value.Trim() == "Current Wheel Diameter (mm)".Trim())
                            {
                                if ((lvOperationalParameterGrind.Items[f].FindControl("opDateType") as Label).Text == "Integer")
                                {
                                    currentwheeldia = (lvOperationalParameterGrind.Items[f].FindControl("optxtallowNumeric") as TextBox).Text;
                                }
                                else if ((lvOperationalParameterGrind.Items[f].FindControl("opDateType") as Label).Text == "Decimal")
                                {
                                    currentwheeldia = (lvOperationalParameterGrind.Items[f].FindControl("optxtallowDecimal") as TextBox).Text;
                                }
                                else if ((lvOperationalParameterGrind.Items[f].FindControl("opDateType") as Label).Text == "Alpha Numeric")
                                {
                                    currentwheeldia = (lvOperationalParameterGrind.Items[f].FindControl("optxtallowalphaNumeric") as TextBox).Text;
                                }
                                else
                                {
                                    currentwheeldia = (lvOperationalParameterGrind.Items[f].FindControl("optxtvalue") as TextBox).Text;
                                }
                            }
                        }

                        if (currentwheeldia != "" && wheelms != "")
                        {
                            formulastring = "(" + wheelms + " * 60) / (" + Math.PI + " * (" + currentwheeldia + " * 0.001))";
                        }
                        else if (currentwheeldia == "" && wheelms != "")
                        {
                            formulastring = "(" + wheelms + " * 60) / (" + Math.PI + " * (" + 0 + " * 0.001))";
                        }
                        else if (currentwheeldia != "" && wheelms == "")
                        {
                            formulastring = "(" + 0 + " * 60) / (" + Math.PI + " * (" + currentwheeldia + " * 0.001))";
                        }
                        else
                        {
                            formulastring = "(" + 0 + " * 60) / (" + Math.PI + " * (" + 0 + " * 0.001))";
                        }
                        dataipmodule.CalculatedFormula = parametername + " ; " + Session["ATKSDocID"] + " ; " + formula + " ; " + formulastring;
                        dataipmodule.GrindingProcess = "";
                    }
                    if (dataipmodule.PrameterId == "")
                    {
                        continue;
                    }
                    int result = DBAccess.InsertUpdateInputModule(Session["ATKSDocID"].ToString(), dataipmodule, saveorupdate);
                    if (result.Equals(0))
                    {
                        if (saveorupdate == "Save")
                        {
                            DBAccess.DeleteSDocID(Session["ATKSDocID"].ToString());
                        }
                        ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);
                        return true;

                    }
                    else
                  if (result == -2)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('SDoc ID already exists.');", true);
                        return true;
                    }
                    else if (result == -1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('SDoc ID not exists.');", true);
                        return true;
                    }
                }

            }


            for (int i = 0; i < lvOperationalParameterGrind.Items.Count; i++)
            {

                //Pass SDoc,User,Datetime
                dataipmodule.CustomeName = (lvOperationalParameterGrind.Items[i].FindControl("opItem") as Label).Text;
                dataipmodule.Prameter = (lvOperationalParameterGrind.Items[i].FindControl("ophdParameterName") as HiddenField).Value;
                dataipmodule.PrameterId = (lvOperationalParameterGrind.Items[i].FindControl("opParameterID") as Label).Text;
                if (dataipmodule.Prameter == "" && dataipmodule.PrameterId == "")
                {
                    continue;
                }
                if ((lvOperationalParameterGrind.Items[i].FindControl("opobjectType") as Label).Text == "CheckBox")
                {
                    if ((lvOperationalParameterGrind.Items[i].FindControl("opcbvalue") as CheckBox).Checked == true)
                        dataipmodule.Value = "Yes";
                    else
                        dataipmodule.Value = "No";
                }
                if ((lvOperationalParameterGrind.Items[i].FindControl("opobjectType") as Label).Text == "Drop Down")
                {
                    //dataipmodule.Value = (lvOperationalParameterGrind.Items[i].FindControl("opddlvalue") as DropDownList).Text;
                    dataipmodule.Value = (lvOperationalParameterGrind.Items[i].FindControl("hfddlvalue") as HiddenField).Value;
                }
                if ((lvOperationalParameterGrind.Items[i].FindControl("opobjectType") as Label).Text == "TextBox")
                {
                    if ((lvOperationalParameterGrind.Items[i].FindControl("opDateType") as Label).Text == "Date")
                    {

                        dataipmodule.Value = (lvOperationalParameterGrind.Items[i].FindControl("optxtDate") as TextBox).Text == "" ? "" : Convert.ToDateTime((lvOperationalParameterGrind.Items[i].FindControl("optxtDate") as TextBox).Text).ToString("dd/MM/yyyy HH:mm");
                    }
                    else
                    if ((lvOperationalParameterGrind.Items[i].FindControl("opDateType") as Label).Text == "Integer")
                    {
                        if (dataipmodule.Prameter == "Current Wheel Diameter (mm)")
                        {
                            CurrentWheelDia = (lvOperationalParameterGrind.Items[i].FindControl("optxtallowNumeric") as TextBox).Text;
                        }
                        dataipmodule.Value = (lvOperationalParameterGrind.Items[i].FindControl("optxtallowNumeric") as TextBox).Text;
                    }
                    else
                    if ((lvOperationalParameterGrind.Items[i].FindControl("opDateType") as Label).Text == "Decimal")
                    {
                        if (dataipmodule.Prameter == "Current Wheel Diameter (mm)")
                        {
                            CurrentWheelDia = (lvOperationalParameterGrind.Items[i].FindControl("optxtallowDecimal") as TextBox).Text;
                        }
                        dataipmodule.Value = (lvOperationalParameterGrind.Items[i].FindControl("optxtallowDecimal") as TextBox).Text;
                    }
                    else
                    if ((lvOperationalParameterGrind.Items[i].FindControl("opDateType") as Label).Text == "Alpha Numeric")
                    {
                        if (dataipmodule.Prameter == "Current Wheel Diameter (mm)")
                        {
                            CurrentWheelDia = (lvOperationalParameterGrind.Items[i].FindControl("optxtallowalphaNumeric") as TextBox).Text;
                        }
                        dataipmodule.Value = (lvOperationalParameterGrind.Items[i].FindControl("optxtallowalphaNumeric") as TextBox).Text;
                    }
                    //Add Hardness 
                    else
                    {
                        if (dataipmodule.Prameter == "Current Wheel Diameter (mm)")
                        {
                            CurrentWheelDia = (lvOperationalParameterGrind.Items[i].FindControl("optxtvalue") as TextBox).Text;
                        }
                        dataipmodule.Value = (lvOperationalParameterGrind.Items[i].FindControl("optxtvalue") as TextBox).Text;
                    }

                }
                int result = DBAccess.InsertUpdateInputModule(Session["ATKSDocID"].ToString(), dataipmodule, saveorupdate);
                if (result.Equals(0))
                {
                    if (saveorupdate == "Save")
                    {
                        DBAccess.DeleteSDocID(Session["ATKSDocID"].ToString());
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);
                    return true;

                }
                else
                if (result == -2)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('SDoc ID already exists.');", true);
                    return true;
                }
                else if (result == -1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('SDoc ID not exists.');", true);
                    return true;
                }
            }



            //dreassing
            for (int i = 0; i < lvOPDressing.Items.Count; i++)
            {

                //Pass SDoc,User,Datetime
                dataipmodule.CustomeName = (lvOPDressing.Items[i].FindControl("opItem") as Label).Text;
                dataipmodule.Prameter = (lvOPDressing.Items[i].FindControl("ophdParameterName") as HiddenField).Value;
                dataipmodule.PrameterId = (lvOPDressing.Items[i].FindControl("opParameterID") as Label).Text;
                dataipmodule.CalculatedFlag = (lvOPDressing.Items[i].FindControl("opcalculatedflag") as Label).Text;

                if (dataipmodule.Prameter == "" && dataipmodule.PrameterId == "")
                {
                    continue;
                }
                if ((lvOPDressing.Items[i].FindControl("opobjectType") as Label).Text == "CheckBox")
                {
                    if ((lvOPDressing.Items[i].FindControl("opcbvalue") as CheckBox).Checked == true)
                        dataipmodule.Value = "Yes";
                    else
                        dataipmodule.Value = "No";
                }
                if ((lvOPDressing.Items[i].FindControl("opobjectType") as Label).Text == "Drop Down")
                {
                    //dataipmodule.Value = (lvOPDressing.Items[i].FindControl("opddlvalue") as DropDownList).Text;
                    dataipmodule.Value = (lvOPDressing.Items[i].FindControl("hfddlvalue") as HiddenField).Value;
                }
                if ((lvOPDressing.Items[i].FindControl("opobjectType") as Label).Text == "TextBox")
                {
                    if ((lvOPDressing.Items[i].FindControl("opDateType") as Label).Text == "Date")
                    {
                        dataipmodule.Value = (lvOPDressing.Items[i].FindControl("optxtDate") as TextBox).Text == "" ? "" : Convert.ToDateTime((lvOPDressing.Items[i].FindControl("optxtDate") as TextBox).Text).ToString("dd/MM/yyyy HH:mm");
                    }
                    else
                    if ((lvOPDressing.Items[i].FindControl("opDateType") as Label).Text == "Integer")
                    {
                        dataipmodule.Value = (lvOPDressing.Items[i].FindControl("optxtallowNumeric") as TextBox).Text;
                    }
                    else
                    if ((lvOPDressing.Items[i].FindControl("opDateType") as Label).Text == "Decimal")
                    {

                        if (dataipmodule.CalculatedFlag == "True")
                        {
                            if (dataipmodule.Prameter == "Wheel Speed (Ns) (rpm)")
                            {
                                string Wheelspeedms = "";
                                string formula = "(Wheel Speed (Vs) (m/s) * 60000) / (π * Current Wheel Diameter (mm))";
                                for (int f = 0; f < lvOPDressing.Items.Count; f++)
                                {
                                    if ((lvOPDressing.Items[f].FindControl("ophdParameterName") as HiddenField).Value.Trim() == "Wheel Speed (Vs) (m/s)")
                                    {
                                        Wheelspeedms = (lvOPDressing.Items[f].FindControl("optxtallowDecimal") as TextBox).Text;
                                    }
                                }

                                string formulastring = "";
                                if (Wheelspeedms != "" && CurrentWheelDia != "")
                                {
                                    formulastring = "( " + Wheelspeedms + " * 60000) / (" + Math.PI + " * " + CurrentWheelDia + " )";
                                }
                                else if (Wheelspeedms == "" && CurrentWheelDia != "")
                                {
                                    formulastring = "( " + 0 + " * 60000) / (" + Math.PI + " * " + CurrentWheelDia + " )";
                                }
                                else
                               if (Wheelspeedms != "" && CurrentWheelDia == "")
                                {
                                    formulastring = "( " + Wheelspeedms + " * 60000) / (" + Math.PI + " * " + 0 + " )";
                                }
                                else
                                {
                                    formulastring = "( " + 0 + " * 60000) / (" + Math.PI + " * " + 0 + " )";
                                }

                                dataipmodule.CalculatedFormula = dataipmodule.CustomeName + " ; " + Session["ATKSDocID"] + " ; " + formula + " ; " + formulastring;
                            }
                            else if (dataipmodule.Prameter == "Dresser Speed (mps)")
                            {
                                string Dressspeedrpm = "";
                                string formula = "π * Dresser Dia * Dresser Speed (rpm)/60000";
                                for (int f = 0; f < lvOPDressing.Items.Count; f++)
                                {
                                    if ((lvOPDressing.Items[f].FindControl("ophdParameterName") as HiddenField).Value.Trim() == "Dresser Speed (rpm)")
                                    {
                                        Dressspeedrpm = (lvOPDressing.Items[f].FindControl("optxtallowDecimal") as TextBox).Text;
                                    }
                                }
                                string formulastring = "";
                                if (Dressspeedrpm != "" && DresserDia != "")
                                {
                                    formulastring = Math.PI + " * " + DresserDia + " * " + Dressspeedrpm + " / 60000";
                                }
                                else if (Dressspeedrpm == "" && DresserDia != "")
                                {
                                    formulastring = Math.PI + " * " + DresserDia + " * " + 0 + " / 60000";
                                }
                                else if (Dressspeedrpm != "" && DresserDia == "")
                                {
                                    formulastring = Math.PI + " * " + 0 + " * " + Dressspeedrpm + " / 60000";
                                }
                                else
                                {
                                    formulastring = Math.PI + " * " + 0 + " * " + 0 + " / 60000";
                                }


                                dataipmodule.CalculatedFormula = dataipmodule.CustomeName + " ; " + Session["ATKSDocID"] + " ; " + formula + " ; " + formulastring;
                            }
                            else if (dataipmodule.Prameter == "Crush Ratio")
                            {
                                string Dressspeedmps = "", Wheelspeed = "";
                                string formula = "Dresser Speed (mps)/Wheel Speed (Vs) (m/s)";
                                for (int f = 0; f < lvOPDressing.Items.Count; f++)
                                {
                                    if ((lvOPDressing.Items[f].FindControl("ophdParameterName") as HiddenField).Value.Trim() == "Dresser Speed (mps)")
                                    {
                                        Dressspeedmps = Request.Form[(lvOPDressing.Items[f].FindControl("optxtallowDecimal") as TextBox).UniqueID];
                                    }
                                    if ((lvOPDressing.Items[f].FindControl("ophdParameterName") as HiddenField).Value.Trim() == "Wheel Speed (Vs) (m/s)")
                                    {
                                        Wheelspeed = (lvOPDressing.Items[f].FindControl("optxtallowDecimal") as TextBox).Text;
                                    }
                                }
                                string formulastring = "";
                                if (Dressspeedmps != "" && Wheelspeed != "")
                                {
                                    formulastring = Dressspeedmps + " / " + Wheelspeed;
                                }
                                else if (Dressspeedmps == "" && Wheelspeed != "")
                                {
                                    formulastring = 0 + " / " + Wheelspeed;
                                }
                                else if (Dressspeedmps != "" && Wheelspeed == "")
                                {
                                    formulastring = Dressspeedmps + " / " + 0;
                                }
                                else
                                {
                                    formulastring = 0 + " / " + 0;
                                }


                                dataipmodule.CalculatedFormula = dataipmodule.CustomeName + " ; " + Session["ATKSDocID"] + " ; " + formula + " ; " + formulastring;
                            }
                            else
                            {

                                string formula = DBAccess.getFormulaList(dataipmodule.Prameter);
                                string[] parameter = formula.Split(new string[] { " / " }, StringSplitOptions.None);
                                string numerator = "", denominator = "", formulastring = "";
                                for (int f = 0; f < lvOPDressing.Items.Count; f++)
                                {
                                    if ((lvOPDressing.Items[f].FindControl("ophdParameterName") as HiddenField).Value.Trim() == parameter[0].Trim())
                                    {

                                        numerator = (lvOPDressing.Items[f].FindControl("optxtallowDecimal") as TextBox).Text;
                                    }

                                    if ((lvOPDressing.Items[f].FindControl("ophdParameterName") as HiddenField).Value == parameter[1])
                                    {
                                        if((lvOPDressing.Items[f].FindControl("optxtallowDecimal") as TextBox).ReadOnly)
                                        {
                                            denominator = Request.Form[(lvOPDressing.Items[f].FindControl("optxtallowDecimal") as TextBox).UniqueID];
                                        }
                                        else
                                        {
                                            denominator = (lvOPDressing.Items[f].FindControl("optxtallowDecimal") as TextBox).Text;
                                        }
                                       
                                    }
                                }
                                if (dataipmodule.Prameter == "Overlap Ratio - OD" || dataipmodule.Prameter == "Overlap Ratio - Face" || dataipmodule.Prameter == "Overlap Ratio - Radius")
                                {
                                    numerator = CutterWidth;
                                }
                                if (numerator != "" && denominator != "")
                                {
                                    formulastring = numerator + " / " + denominator;
                                }
                                else if (numerator == "" && denominator != "")
                                {
                                    formulastring = 0 + " / " + denominator;
                                }
                                else if (numerator != "" && denominator == "")
                                {
                                    formulastring = numerator + " / " + 0;
                                }
                                else
                                {
                                    formulastring = 0 + " / " + 0;
                                }

                                dataipmodule.CalculatedFormula = dataipmodule.CustomeName + " ; " + Session["ATKSDocID"] + " ; " + formula + " ; " + formulastring;
                            }
                        }
                        else
                        {
                            dataipmodule.CalculatedFormula = "";
                        }

                        //dataipmodule.Value = (lvOPDressing.Items[i].FindControl("optxtallowDecimal") as TextBox).Text;
                        if ((lvOPDressing.Items[i].FindControl("optxtallowDecimal") as TextBox).ReadOnly)
                        {

                            dataipmodule.Value = Request.Form[(lvOPDressing.Items[i].FindControl("optxtallowDecimal") as TextBox).UniqueID];
                        }
                        else
                        {
                            dataipmodule.Value = (lvOPDressing.Items[i].FindControl("optxtallowDecimal") as TextBox).Text;
                        }


                    }
                    else
                    if ((lvOPDressing.Items[i].FindControl("opDateType") as Label).Text == "Alpha Numeric")
                    {
                        dataipmodule.Value = (lvOPDressing.Items[i].FindControl("optxtallowalphaNumeric") as TextBox).Text;
                    }
                    //Add Hardness 
                    else
                    {
                        dataipmodule.Value = (lvOPDressing.Items[i].FindControl("optxtvalue") as TextBox).Text;
                    }

                }
                int result = DBAccess.InsertUpdateInputModule(Session["ATKSDocID"].ToString(), dataipmodule, saveorupdate);
                if (result.Equals(0))
                {
                    if (saveorupdate == "Save")
                    {
                        DBAccess.DeleteSDocID(Session["ATKSDocID"].ToString());
                    }
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);
                    return true;

                }
                else
                if (result == -2)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('SDoc ID already exists.');", true);
                    return true;
                }
                else if (result == -1)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('SDoc ID not exists.');", true);
                    return true;
                }
            }
            #endregion

            #region -- Quality Parameter --
            for (int i = 0; i < lvQualityParameter.Items.Count; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (j == 0)
                    {
                        dataipmodule.PrameterId = (lvQualityParameter.Items[i].FindControl("hdnParamIDqpTargetLower") as HiddenField).Value;
                        dataipmodule.Value = (lvQualityParameter.Items[i].FindControl("qpTargetLower") as TextBox).Text;
                    }
                    else if (j == 1)
                    {
                        dataipmodule.PrameterId = (lvQualityParameter.Items[i].FindControl("hdnParamIDqpTargetUppper") as HiddenField).Value;
                        dataipmodule.Value = (lvQualityParameter.Items[i].FindControl("qpTargetUppper") as TextBox).Text;
                    }
                    else if (j == 2)
                    {
                        dataipmodule.PrameterId = (lvQualityParameter.Items[i].FindControl("hdnParamIDqpAchievedLower") as HiddenField).Value;
                        dataipmodule.Value = (lvQualityParameter.Items[i].FindControl("qpAchievedLower") as TextBox).Text;
                    }
                    else if (j == 3)
                    {
                        dataipmodule.PrameterId = (lvQualityParameter.Items[i].FindControl("hdnParamIDAchievedUppper") as HiddenField).Value;
                        dataipmodule.Value = (lvQualityParameter.Items[i].FindControl("qpAchievedUppper") as TextBox).Text;
                    }
                    if (dataipmodule.PrameterId == "")
                    {
                        continue;
                    }
                    int result = DBAccess.InsertUpdateInputModule(Session["ATKSDocID"].ToString(), dataipmodule, saveorupdate);
                    if (result.Equals(0))
                    {
                        if (saveorupdate == "Save")
                        {
                            DBAccess.DeleteSDocID(Session["ATKSDocID"].ToString());
                        }
                        ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Record insertion failed.');", true);
                        return true;

                    }
                    else if (result == -2)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('SDoc ID already exists.');", true);
                        return true;
                    }
                    else if (result == -1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('SDoc ID not exists.');", true);
                        return true;
                    }
                }

            }
            #endregion
            return false;
        }

        protected void templateornewSdocOK_ServerClick(object sender, EventArgs e)
        {
            if (TemplatenewSdoc.Checked)
            {
                CreatenewSDocYes1_ServerClick(sender, e);
            } else if (newTemplate.Checked)
            {
                templateCreate_ServerClick(sender, e);
            }
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            BindData("");
            Session["ATKSDocID"] = null;
            txtViewSdocid.Text = "";
        }

        protected void leaveunsavedDataWarning_ServerClick(object sender, EventArgs e)
        {
            BindData("");
            Session["ATKSDocID"] = null;
            txtViewSdocid.Text = "";
        }

        protected void deleteSdocYes_ServerClick(object sender, EventArgs e)
        {

            string emprole = DBAccess.getRoleOfEmp(Session["EmpName"] == null ? "" : Session["EmpName"].ToString());
            if (emprole == "Admin")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('Admin not able to delete SDocID from Data Input Module Page.');", true);
            }
            else
            {
                string SaveAsSdoc = "";
                for (int i = 0; i < lvGeneralInfo.Items.Count; i++)
                {
                    string parameterid = (lvGeneralInfo.Items[i].FindControl("giParameterID") as Label).Text;
                    string parameter = (lvGeneralInfo.Items[i].FindControl("gihdParameterName") as HiddenField).Value;
                    if (parameter == "SDoc ID")
                    {
                        SaveAsSdoc = (lvGeneralInfo.Items[i].FindControl("gitxtvalue") as TextBox).Text;
                    }
                }
                int result = DBAccess.DeleteSDoc(SaveAsSdoc, Session["EmpName"] == null ? "" : Session["EmpName"].ToString(), "Delete");
                if (result == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Failed to delete SDocID');", true);
                    
                }
                else
                {
                    bindSdocID();
                    BindData("");
                    txtViewSdocid.Text = "";
                }
               
              
            }
           
           
        }
    }
}