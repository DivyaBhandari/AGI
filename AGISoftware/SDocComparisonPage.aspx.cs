using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AGISoftware.DataBaseAccess;
using AGISoftware.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;

namespace AGISoftware
{
    public partial class SDocComparisonPage : System.Web.UI.Page
    {
        static List<string> SDocid1list = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["SDocid2"] = null;
                Session["CUMIGeneralInfoParameters"] = null;
                Session["CUMIQualityParameters"] = null;
                bindSDocId1list();
                bindCheckListParameter();
                bindQualityParameterCheckListParameter();
                bindGeneralInfo();
                bindQualityParameter();
                bindInferenceSignal();
                bindImages();
            }
        }
        private void bindSDocId1list()
        {
            //SDocid1list = DBAccess.getSDocForDelete("Delete", "SDocList");
            //ddlSodcId1.DataSource = SDocid1list;
            //ddlSodcId1.DataBind();
            //bindSDocId2list();

            // List<string> sdocid1list = null;
            SDocid1list = DBAccess.getSDocForDelete("Delete", "SDocList");
            //SDocid1list.Insert(0, "");
            var builder = new System.Text.StringBuilder();            if (SDocid1list.Count > 0)            {                for (int i = 0; i < SDocid1list.Count; i++)                {                    if (i == 0)                    {                        if (Session["ATKSDocID"] == null)
                        {
                            txtSDocId1.Text = SDocid1list[i].ToString();
                        }
                        else
                        {
                            txtSDocId1.Text = Session["ATKSDocID"].ToString();
                        }                                           }                    builder.Append(String.Format("<option style='font-weight:unset' value='{0}'>", SDocid1list[i].ToString()));                }            }            else            {                txtSDocId1.Text = "";            }                        SDocid1lists.InnerHtml = builder.ToString();
            bindSDocId2list();
        }
        private void bindSDocId2list()
        {
            string SDocid = txtSDocId1.Text;
            List<string> SDocid2list = SDocid1list.Where(x => x.ToString() != SDocid).Select(x => x.ToString()).ToList();
            //ddlSDocId2.DataSource = SDocid2list;
            //ddlSDocId2.DataBind();
            var builder = new System.Text.StringBuilder();            if (SDocid2list.Count > 0)            {                for (int i = 0; i < SDocid2list.Count; i++)                {                    if (i == 0)                    {                        if (Session["SDocid2"] == null)
                        {
                            txtSDocId2.Text = SDocid2list[i].ToString();
                            Session["SDocid2"] = txtSDocId2.Text;
                        }
                        else 
                        {
                            if (Session["SDocid2"].ToString() == SDocid)
                            {
                                txtSDocId2.Text = SDocid2list[i].ToString();
                            }
                            else
                            {
                                txtSDocId2.Text = Session["SDocid2"].ToString();
                            }
                           
                        }                    }                    builder.Append(String.Format("<option style='font-weight:unset' value='{0}'>", SDocid2list[i].ToString()));                }            }            else            {                txtSDocId2.Text = "";            }

            SDocid2lists.InnerHtml = builder.ToString();

        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static void setSDocID2(string SDcoid2)
        {
            HttpContext.Current.Session["SDocid2"] = SDcoid2;
        }
        protected void ddlSodcId1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindSDocId2list();
        }
        private void bindGeneralInfo()
        {
            // gvGeneralInfo.DataSource = DBAccess.cumiBindGeneralInfo(ddlSodcId1.SelectedItem==null? "" : ddlSodcId1.SelectedItem.ToString() , ddlSDocId2.SelectedItem==null? "" : ddlSDocId2.SelectedItem.ToString(),"", "GeneralParm");
            DataTable dt;
            if (Session["CUMIGeneralInfoParameters"] != null)
            {
                dt = DBAccess.cumiBindGeneralInfo(txtSDocId1.Text, txtSDocId2.Text, Session["CUMIGeneralInfoParameters"].ToString(), "GeneralParmNonDefault");
            }
            else
            {
               dt = DBAccess.cumiBindGeneralInfo(txtSDocId1.Text, txtSDocId2.Text, "", "GeneralParm");
            }
           
            DataTable dtoutput= removeBlankValuesFromGeneralInfoGrid(dt);
            gvGeneralInfo.DataSource = dtoutput;
            gvGeneralInfo.DataBind();
            checkParameterToPanelList(); 
            //List<string> Sdocid = new List<string>();
            //if (gvGeneralInfo.Rows.Count > 0)
            //{
            //    for (int i = 3; i < gvGeneralInfo.Rows[0].Cells.Count - 3; i++)
            //    {
            //        string sdoc = gvGeneralInfo.HeaderRow.Cells[i].Text;
            //        Sdocid.Add(sdoc);
            //    }
            //    dcSdoclist.DataSource = Sdocid;
            //    dcSdoclist.DataBind();
            //    foreach (System.Web.UI.WebControls.ListItem item in dcSdoclist.Items)
            //    {
            //        item.Selected = true;
            //    }
            //    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "btnSDocGraphClick();", true);
            //}
        }
        private DataTable removeBlankValuesFromGeneralInfoGrid(DataTable dt)
        {
            DataTable newdt = new DataTable();
            newdt = dt.Clone();
           
            try
            {
                if(dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow drtableOld in dt.Rows)
                    {
                        bool blankvalue = false;
                        //bool samevalue = false;
                        //string oldvalue = "";
                        for (int j = 3; j < (dt.Columns.Count - 3); j++)
                        {
                            if ((drtableOld[j].ToString() == "" || drtableOld[j].ToString() == "&nbsp;" || drtableOld[j].ToString() == null))
                            {
                                blankvalue = true;
                            }
                            //string value = drtableOld[j].ToString() == "&nbsp;" ? "" : drtableOld[j].ToString();
                            //if (j == 3)
                            //{
                            //    oldvalue = value;
                            //}
                            //else
                            //{
                            //    if (oldvalue == value)
                            //    {
                            //        samevalue = true;
                            //    }
                            //    else
                            //    {
                            //        samevalue = false;
                            //    }
                            //}
                        }
                        if (!blankvalue)
                        {
                            newdt.ImportRow(drtableOld);
                            //newdt["ColorFlag"] = samevalue;
                        }
                    }
                    //DataColumn colorflag = new DataColumn("ColorFlag", typeof(System.String));
                    //newdt.Columns.Add(colorflag);
                    //if (newdt!=null && newdt.Rows.Count > 0)
                    //{
                    //    foreach (DataRow drtablenew in newdt.Rows)
                    //    {
                    //        bool samevalue = false;
                    //        string oldvalue = "";
                    //        for (int j = 3; j < (newdt.Columns.Count - 4); j++)
                    //        {
                    //            string value = drtablenew[j].ToString() == "&nbsp;" ? "" : drtablenew[j].ToString();
                    //            if (j == 3)
                    //            {
                    //                oldvalue = value;
                    //            }
                    //            else
                    //            {
                    //                if (oldvalue == value)
                    //                {
                    //                    samevalue = true;
                    //                }
                    //                else
                    //                {
                    //                    samevalue = false;
                    //                }
                    //            }
                    //        }
                    //        if (samevalue)
                    //        {
                    //            drtablenew["ColorFlag"] = true;
                    //        }
                    //        else
                    //        {
                    //            drtablenew["ColorFlag"] = false;
                    //        }
                    //    }

                    //}
                  
                }
            }
            catch(Exception ex)
            {

            }
            return newdt;
        }
        private void bindQualityParameter()
        {
            List<QualityParam> listqualityParams;
            if (Session["CUMIQualityParameters"] != null)
            {
                listqualityParams = DBAccess.cumiBindQualityParam(txtSDocId1.Text, txtSDocId2.Text, Session["CUMIQualityParameters"].ToString(), "QualityParmNonDefault");
            }
            else
            {
                listqualityParams = DBAccess.cumiBindQualityParam(txtSDocId1.Text, txtSDocId2.Text, "", "QualityParm");
            }
           
            lvQualityParam.DataSource = removeBlankValuesFromQualityGrid(listqualityParams);
            lvQualityParam.DataBind();
            checkQualityParameterToPanelList();
        }
        private List<QualityParam> removeBlankValuesFromQualityGrid(List<QualityParam> qualityParams)
        {
            List<QualityParam> newListQualityParam = new List<QualityParam>();
            QualityParam newqualityParam = null;
            try
            {
                bool[] blankrowno = new bool[4] ;
                bool[] blankrowno1 = new bool[4];
                List<string> blankParameterName= new List<string>();
                List<string> nonblankParameterName = new List<string>();
                int k = 0,n=0;
                for (int i = 0; i < qualityParams.Count; i++)
                {

                    for (int j = 0; j < qualityParams[i].Values.Count; j++)
                    {
                        bool blankvalue = false;
                        string parameterName = qualityParams[i].Values[j].Prameter;
                      
                        string tl = qualityParams[i].Values[j].TargetLower;
                        if (String.IsNullOrEmpty(qualityParams[i].Values[j].TargetLower) && String.IsNullOrEmpty(qualityParams[i].Values[j].TargetUpper) && String.IsNullOrEmpty(qualityParams[i].Values[j].ActualLower) && String.IsNullOrEmpty(qualityParams[i].Values[j].ActualUpper))
                        {
                            blankvalue = true;
                        }

                        if (blankvalue)
                        {
                           
                            if (nonblankParameterName.Contains(parameterName))
                            {
                                if (blankParameterName.Contains(parameterName))
                                {
                                    blankParameterName.Remove(parameterName);
                                }
                            }
                            else
                            {
                                if (!blankParameterName.Contains(parameterName))
                                {
                                    blankParameterName.Add(parameterName);
                                }
                            }
                        }
                        else
                        {
                            if (blankParameterName.Contains(parameterName))
                            {
                                blankParameterName.Remove(parameterName);

                            }
                            else
                            {
                                if (!nonblankParameterName.Contains(parameterName))
                                {
                                    nonblankParameterName.Add(parameterName);
                                }
                            }
                          
                        }
                      
                    }
                }

                for (int i = 0; i < qualityParams.Count; i++)
                {
                    newqualityParam = new QualityParam();
                    newqualityParam.SdocName = qualityParams[i].SdocName;
                    List<DataInputModuleParameter> newListIP = new List<DataInputModuleParameter>();
                    DataInputModuleParameter newIP = null;
                    for (int j = 0; j < qualityParams[i].Values.Count; j++)
                    {
                        if (!blankParameterName.Contains(qualityParams[i].Values[j].Prameter))
                        {
                            // qualityParams[i].Values.RemoveAt(j);
                            newIP = new DataInputModuleParameter();
                            newIP.Prameter = qualityParams[i].Values[j].Prameter;
                            newIP.TargetLower = qualityParams[i].Values[j].TargetLower;
                            newIP.TargetUpper = qualityParams[i].Values[j].TargetUpper;
                            newIP.ActualLower = qualityParams[i].Values[j].ActualLower;
                            newIP.ActualUpper = qualityParams[i].Values[j].ActualUpper;
                            newListIP.Add(newIP);
                        }
                    }
                    newqualityParam.Values = newListIP;
                    newListQualityParam.Add(newqualityParam);
                }

            }
            catch (Exception ex)
            {

            }
            return newListQualityParam;
        }

        private void bindInferenceSignal()        {
            //gvInferenceSignal.DataSource = DBAccess.cumiBindInferenceSignal(ddlSodcId1.SelectedItem == null ? "" : ddlSodcId1.SelectedItem.ToString(), ddlSDocId2.SelectedItem == null ? "" : ddlSDocId2.SelectedItem.ToString());
            gvInferenceSignal.DataSource = DBAccess.cumiBindInferenceSignal(txtSDocId1.Text, txtSDocId2.Text);
            gvInferenceSignal.DataBind();        }
        private void bindImages()
        {
            //lvImages.DataSource = DBAccess.cumiBindImages(ddlSodcId1.SelectedItem == null ? "" : ddlSodcId1.SelectedItem.ToString(), ddlSDocId2.SelectedItem == null ? "" : ddlSDocId2.SelectedItem.ToString());
            List<SdocImages> sdocImages = new List<SdocImages>();
            sdocImages= DBAccess.cumiBindImages(txtSDocId1.Text, txtSDocId2.Text);
            Session["CUMISdocImages"] = sdocImages;
            lvImages.DataSource = sdocImages;
            lvImages.DataBind();
        }
        private void bindCheckListParameter()
        {
            chkParameter.DataSource = DBAccess.getOMGeneralInfoParamForFilter();
            chkParameter.DataTextField = "CustomName";
            chkParameter.DataValueField = "ColumnName";
            chkParameter.DataBind();
            chkParameter.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select All", "Select All"));
        }
        private void bindQualityParameterCheckListParameter()
        {
            chkFilterQlyParam.DataSource = DBAccess.getOMQualityParamForFilter();
            chkFilterQlyParam.DataTextField = "CustomName";
            chkFilterQlyParam.DataValueField = "ColumnName";
            chkFilterQlyParam.DataBind();
            chkFilterQlyParam.Items.Insert(0, new System.Web.UI.WebControls.ListItem("Select All", "Select All"));
        }
      
        private void checkParameterToPanelList()
        {
            try
            {
                for (int j = 0; j < chkParameter.Items.Count; j++)
                {
                    chkParameter.Items[j].Selected = false;
                }
                int totalchecked = 0;

                for (int i = 0; i < gvGeneralInfo.Rows.Count; i++)
                {
                    string s = gvGeneralInfo.Rows[i].Cells[0].Text;
                    for (int j = 1; j < chkParameter.Items.Count; j++)
                    {
                        if (gvGeneralInfo.Rows[i].Cells[0].Text == chkParameter.Items[j].Text)
                        {
                            chkParameter.Items[j].Selected = true;
                            totalchecked++;
                        }
                    }
                }
                if (totalchecked == (chkParameter.Items.Count - 1))
                {
                    chkParameter.Items[0].Selected = true;
                }
            }
            catch(Exception ex)
            {

            }
           
        }
        private void checkQualityParameterToPanelList()
        {
            try
            {
                for (int j = 0; j < chkFilterQlyParam.Items.Count; j++)
                {
                    chkFilterQlyParam.Items[j].Selected = false;
                }
                int totalchecked = 0;
                if (lvQualityParam.Items.Count > 0)
                {
                    ListView listView = (lvQualityParam.Items[0].FindControl("lvinnerQly") as ListView);
                    for (int i = 0; i < listView.Items.Count; i++)
                    {
                        string qulityParam = (listView.Items[i].FindControl("Qlyparam") as Label).Text;
                        for (int j = 1; j < chkFilterQlyParam.Items.Count; j++)
                        {
                            if (qulityParam == chkFilterQlyParam.Items[j].Text)
                            {
                                chkFilterQlyParam.Items[j].Selected = true;
                            }
                        }
                    }
                }
                if (totalchecked == (chkFilterQlyParam.Items.Count - 1))
                {
                    chkFilterQlyParam.Items[0].Selected = true;
                }
            }
            catch (Exception ex)
            {

            }

           

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            bindGeneralInfo();
            bindQualityParameter();
            bindInferenceSignal();
            bindImages();
        }

        protected void generalInfoOk_ServerClick(object sender, EventArgs e)
        {
            txtsearchqly.Text = "";
            txtsearch.Text = "";
            int count = 0;
            StringBuilder parameter = new StringBuilder();
            parameter.Append("");
            if (chkParameter.Items[0].Selected)
            {
                //gvGeneralInfo.DataSource = DBAccess.cumiBindGeneralInfo(ddlSodcId1.SelectedItem==null ? "" : ddlSodcId1.SelectedItem.ToString(), ddlSDocId2.SelectedItem == null ? "" : ddlSDocId2.SelectedItem.ToString(), parameter.ToString(), "GeneralParmNonDefault");
                DataTable dt = DBAccess.cumiBindGeneralInfo(txtSDocId1.Text, txtSDocId2.Text, parameter.ToString(), "GeneralParmNonDefault");
                DataTable dtoutput = removeBlankValuesFromGeneralInfoGrid(dt);
                gvGeneralInfo.DataSource = dtoutput;
                gvGeneralInfo.DataBind();
                checkParameterToPanelList();
            }
            else
            {
                for (int i = 1; i < chkParameter.Items.Count; i++)
                {
                    if (chkParameter.Items[i].Selected)
                    {
                        if (count == 0)
                        {
                            parameter.Append("'" + chkParameter.Items[i].Value + "'");
                            count++;
                        }
                        else
                        {
                            parameter.Append("," + "'" + chkParameter.Items[i].Value + "'");

                        }
                    }
                }
                //gvGeneralInfo.DataSource = DBAccess.cumiBindGeneralInfo(ddlSodcId1.SelectedItem == null ? "" : ddlSodcId1.SelectedItem.ToString(), ddlSDocId2.SelectedItem == null ? "" : ddlSDocId2.SelectedItem.ToString(), parameter.ToString(), "GeneralParmNonDefault");
                 DataTable dt= DBAccess.cumiBindGeneralInfo(txtSDocId1.Text, txtSDocId2.Text, parameter.ToString(), "GeneralParmNonDefault");
                DataTable dtoutput = removeBlankValuesFromGeneralInfoGrid(dt);
                gvGeneralInfo.DataSource = dtoutput;
                gvGeneralInfo.DataBind();
                checkParameterToPanelList();
            }
            Session["CUMIGeneralInfoParameters"] = parameter.ToString();
        }

        protected void qltFilterOk_ServerClick(object sender, EventArgs e)
        {
            txtsearchqly.Text = "";
            txtsearch.Text = "";
            int count = 0;
            StringBuilder parameter = new StringBuilder();
            parameter.Append("");
            if (chkFilterQlyParam.Items[0].Selected)
            {
                //lvQualityParam.DataSource = DBAccess.cumiBindQualityParam(ddlSodcId1.SelectedItem == null ? "" : ddlSodcId1.SelectedItem.ToString(), ddlSDocId2.SelectedItem == null ? "" : ddlSDocId2.SelectedItem.ToString(), parameter.ToString(), "QualityParmNonDefault");
                List<QualityParam> listqualityParams = DBAccess.cumiBindQualityParam(txtSDocId1.Text, txtSDocId2.Text, parameter.ToString(), "QualityParmNonDefault");
                lvQualityParam.DataSource=removeBlankValuesFromQualityGrid(listqualityParams);
                lvQualityParam.DataBind();
                checkQualityParameterToPanelList();
                // bindQualityParameter(setSdocPlungeCategory(), parameter.ToString());
            }
            else
            {
                for (int i = 1; i < chkFilterQlyParam.Items.Count; i++)
                {
                    if (chkFilterQlyParam.Items[i].Selected)
                    {
                        if (count == 0)
                        {
                            parameter.Append(chkFilterQlyParam.Items[i].Value);
                            count++;
                        }
                        else
                        {
                            parameter.Append("," + chkFilterQlyParam.Items[i].Value);

                        }
                    }
                }
                //lvQualityParam.DataSource = DBAccess.cumiBindQualityParam(ddlSodcId1.SelectedItem == null ? "" : ddlSodcId1.SelectedItem.ToString(), ddlSDocId2.SelectedItem == null ? "" : ddlSDocId2.SelectedItem.ToString(), parameter.ToString(), "QualityParmNonDefault");
                List<QualityParam> listqualityParams = DBAccess.cumiBindQualityParam(txtSDocId1.Text, txtSDocId2.Text, parameter.ToString(), "QualityParmNonDefault");
                lvQualityParam.DataSource = removeBlankValuesFromQualityGrid(listqualityParams);
                lvQualityParam.DataBind();
                checkQualityParameterToPanelList();
            }
            Session["CUMIQualityParameters"] = parameter.ToString();
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<string> getMRRGraphData(string SDocid1, string SDocid2)
        {
            List<string> ouput = DBAccess.cumiBindCalculatedParam(SDocid1, SDocid2);
            return ouput;
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<TotalCycleTimeGrpah> getTotalCycleTimeGraphData(string SDocid1, string SDocid2)
        {
            List<TotalCycleTimeGrpah> ouput = DBAccess.cumiBindTotalCycleTime(SDocid1, SDocid2);
           // HttpContext.Current.Session["TotalCycleTime"] = ouput;
            return ouput;
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<TotalCycleTimeGrpah> getGrindingTime(string SDocid1, string SDocid2)
        {
            List<TotalCycleTimeGrpah> ouput = DBAccess.cumiBindGrindingTime(SDocid1, SDocid2);
            return ouput;
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<TotalCycleTimeGrpah> getNonGrindingTime(string SDocid1, string SDocid2)
        {
            List<TotalCycleTimeGrpah> ouput = DBAccess.cumiBindNonGrindingTime(SDocid1, SDocid2);
            return ouput;
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        public static DrillChart getDrilldownTimeData(string Sdocid)
        {
            DrillChart drillChart = new DrillChart();

            try
            {
                List<ChartSeries> listChartSeries = new List<ChartSeries>();
                ChartSeries chartSeries = null;
                // List<string> ouput = DBAccess.omBindTotalCycleTime(Sdoc, Plunge, SubCat);
                DataTable totalgycletime = (DataTable)HttpContext.Current.Session["TotalCycleTime"];

                string[] SDoctotalTimeDataColor = { "#000090", "#8fff6f" };
                int k = 0;
                for (int i = 2; i < totalgycletime.Columns.Count; i++)
                {
                    if(totalgycletime.Columns[i].ColumnName==Sdocid)
                    {
                        for (int j = 0; j < totalgycletime.Rows.Count; j++)
                        {
                            string parameter = totalgycletime.Rows[j][1].ToString();
                            string value= totalgycletime.Rows[j][i].ToString();
                            if (parameter == "Grinding Time (sec)" || parameter == "Non Grinding Time (sec)")
                            {
                                chartSeries = new ChartSeries();
                                chartSeries.name = parameter;
                                chartSeries.y = value == "" || value == null ? 0 : Convert.ToDecimal(value);
                                chartSeries.drilldown = parameter;
                                chartSeries.color = SDoctotalTimeDataColor[k];
                                listChartSeries.Add(chartSeries);
                                k++;
                            }
                        }
                    }
                }
                drillChart.listChartSeries = listChartSeries;

                //get grinding data
                List<DrildownSeries> listDrillownSeries = new List<DrildownSeries>();
                DrildownSeries drildownSeries = null;

                string[] SDocgrindTimeDataColor = { "#000090", "#8fff6f", "#800001", "#e67312", "#1c1c21", "#056aad" };
                DataTable grinding =(DataTable)HttpContext.Current.Session["GrindingTime"];
                drildownSeries = new DrildownSeries();
                drildownSeries.name = "Grinding Time (sec)";
                drildownSeries.id = "Grinding Time (sec)";
                List<DrildownData> listDrilldownData = new List<DrildownData>();
                DrildownData drildownData = null;
                k = 0;
                for (int i = 2; i < grinding.Columns.Count; i++)
                {
                    if (grinding.Columns[i].ColumnName == Sdocid)
                    {
                        for (int j = 0; j < grinding.Rows.Count; j++)
                        {
                            drildownData = new DrildownData();
                            string parameter = grinding.Rows[j][1].ToString();
                            string value = grinding.Rows[j][i].ToString();
                            drildownData.name = parameter;
                            drildownData.y = value == null || value == "" ? 0 : Convert.ToDecimal(value);
                            drildownData.color = SDocgrindTimeDataColor[k];
                            listDrilldownData.Add(drildownData);
                            k++;
                        }
                    }
                }
                drildownSeries.data = listDrilldownData;
                listDrillownSeries.Add(drildownSeries);

                //get Nongrinding data
                string[] SDocnongrindTimeDataColor = { "#000090", "#8fff6f", "#800001", "#e67312", "#1c1c21", "#056aad" };
                DataTable nongrinding = (DataTable)HttpContext.Current.Session["NonGrindingTime"];
                drildownSeries = new DrildownSeries();
                drildownSeries.name = "Non Grinding Time (sec)";
                drildownSeries.id = "Non Grinding Time (sec)";
                listDrilldownData = new List<DrildownData>();
                drildownData = null;
                k = 0;
                for (int i = 0; i < nongrinding.Columns.Count; i++)
                {
                    if (nongrinding.Columns[i].ColumnName == Sdocid)
                    {
                        for (int j = 0; j < nongrinding.Rows.Count; j++)
                        {
                            drildownData = new DrildownData();
                            string parameter = nongrinding.Rows[j][1].ToString();
                            string value = nongrinding.Rows[j][i].ToString();
                            drildownData.name = parameter;
                            drildownData.y = value == null || value == "" ? 0 : Convert.ToDecimal(value);
                            drildownData.color = SDocnongrindTimeDataColor[k];
                            listDrilldownData.Add(drildownData);
                            k++;
                        }
                    }
                }
                drildownSeries.data = listDrilldownData;
                listDrillownSeries.Add(drildownSeries);

                drillChart.listDrilldownSeries = listDrillownSeries;

            }
            catch (Exception ex)
            {

            }
            return drillChart;
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                //if (hdnTotalCycleTimeGraph.Value != "")
                //{
                //    string str = hdnTotalCycleTimeGraph.Value.ToString();
                //    string base64 = Request.Form[hdnTotalCycleTimeGraph.UniqueID].Split(',')[1];
                //}
                //if (hdnCalculatedTimeGraph.Value != "")
                //{
                //    string str = hdnCalculatedTimeGraph.Value.ToString();
                //    string base64 = Request.Form[hdnCalculatedTimeGraph.UniqueID].Split(',')[1];
                //}

                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                pdfDoc.Open();

                

                //byte[] file1;
                //file = System.IO.File.ReadAllBytes(Server.MapPath(Utility.ReportImageRHS));//ImagePath  
                //iTextSharp.text.Image jpg2 = iTextSharp.text.Image.GetInstance(file);
                //jpg2.ScaleToFit(50f, 50f);

                PdfPTable pdfTable1 = new PdfPTable(5);
                pdfTable1.WidthPercentage = 100;
                pdfTable1.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable1.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                pdfTable1.DefaultCell.BorderWidth = 0;

                try
                {
                    byte[] file;
                    file = System.IO.File.ReadAllBytes(Server.MapPath(Utility.ReportImageLHS));//ImagePath  
                    iTextSharp.text.Image jpg1 = iTextSharp.text.Image.GetInstance(file);
                    jpg1.ScaleToFit(50f, 40f);


                    PdfPCell cell = new PdfPCell(jpg1, false);
                    cell.BorderWidth = 0;
                    cell.Padding = 1;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    pdfTable1.AddCell(cell);
                }
                catch(Exception ex)
                {
                    PdfPCell cell = new PdfPCell();
                    cell.BorderWidth = 0;
                    cell.Padding = 1;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    pdfTable1.AddCell(cell);
                }
              

                iTextSharp.text.Font font = FontFactory.GetFont("Calibri (Body)", 10, iTextSharp.text.Font.BOLD);
                Chunk c1 = new Chunk("Data Collection / Trial Wheel Documentation", font);
                c1.Font.Color = new BaseColor(0, 0, 0);
                c1.Font.SetStyle(0);
                c1.Font.Size = 16;
              
                Phrase p1 = new Phrase();
                p1.Add(c1);
                PdfPCell cellHeader = new PdfPCell(p1);
                cellHeader.Colspan = 3;
                cellHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                cellHeader.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellHeader.BorderWidth = 0;
                pdfTable1.AddCell(cellHeader);

                try
                {
                    byte[] file;
                    file = System.IO.File.ReadAllBytes(Server.MapPath(Utility.ReportImageRHS));//ImagePath  
                    iTextSharp.text.Image jpg2 = iTextSharp.text.Image.GetInstance(file);
                    jpg2.ScaleToFit(50f, 50f);

                    PdfPCell rimgcell = new PdfPCell(jpg2, false);
                    rimgcell.BorderWidth = 0;
                    rimgcell.Padding = 1;
                    rimgcell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    pdfTable1.AddCell(rimgcell);
                    pdfDoc.Add(pdfTable1);

                }
                catch(Exception ex)
                {
                    PdfPCell rimgcell = new PdfPCell();
                    rimgcell.BorderWidth = 0;
                    rimgcell.Padding = 1;
                    rimgcell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    pdfTable1.AddCell(rimgcell);
                    pdfDoc.Add(pdfTable1);
                }
            

            
                List<string> sdocDetails = new List<string>();
                int noOfSdoc = gvGeneralInfo.HeaderRow.Cells.Count - 6;
                if (noOfSdoc > 0)
                {
                    for (int i = 3; i < gvGeneralInfo.HeaderRow.Cells.Count - 3; i++)
                    {
                        sdocDetails.Add(gvGeneralInfo.HeaderRow.Cells[i].Text);
                    }
                }
                else
                {
                    return;
                }
                int generalInfoCount = 3, inferenceCount = 1, imageCount = 0, qlyCount = 0, calParamCount = 1, totalcycletimeCount=2, grindingtimeCount=2;
                DataTable calcParamTable = (DataTable)Session["CalcParamGraphData"];
                string calParamStr = hdnCalcParamGraph.Value.Trim().Remove(hdnCalcParamGraph.Value.Trim().Length - 1);
                List<string> calcParamGraph = calParamStr.Split('$').ToList();
                string s1 = hdnCalcParamGraph.Value;
                List<string> calcParamGraphData = new List<string>();
                for (int i = 0; i < calcParamGraph.Count; i++)
                {
                    calcParamGraphData.Add(calcParamGraph[i].Split(',')[1]);
                }

                DataTable totalcycletimeTable = (DataTable)Session["TotalCycleTime"];
                string totalcycletimeStr = hdnTotalCycleTimeGraph.Value.Trim().Remove(hdnTotalCycleTimeGraph.Value.Trim().Length - 1);
                List<string> totalcycletimeGraph = totalcycletimeStr.Split('$').ToList();
                s1 = hdnTotalCycleTimeGraph.Value;
                List<string> totalcycletimeGraphData = new List<string>();
                for (int i = 0; i < totalcycletimeGraph.Count; i++)
                {
                    totalcycletimeGraphData.Add(totalcycletimeGraph[i].Split(',')[1]);
                }

                string totalcycletimedrillStr = hdnDrillChart.Value.Trim().Remove(hdnDrillChart.Value.Trim().Length - 1);
                List<string> totalcycletimedrillGraph = totalcycletimedrillStr.Split('$').ToList();
                s1 = hdnDrillChart.Value;
                List<string> totalcycletimedrillGraphData = new List<string>();
                for (int i = 0; i < totalcycletimedrillGraph.Count; i++)
                {
                    totalcycletimedrillGraphData.Add(totalcycletimedrillGraph[i].Split(',')[1]);
                }

                DataTable grindingtimeTable = (DataTable)Session["GrindingTime"];
                string grindingtimeStr = hdnGrindingTimeGraph.Value.Trim().Remove(hdnGrindingTimeGraph.Value.Trim().Length - 1);
                List<string> grindingtimeGraph = grindingtimeStr.Split('$').ToList();
                s1 = hdnGrindingTimeGraph.Value;
                List<string> grindingtimeGraphData = new List<string>();
                for (int i = 0; i < grindingtimeGraph.Count; i++)
                {
                    grindingtimeGraphData.Add(grindingtimeGraph[i].Split(',')[1]);
                }

                PdfPTable finalTbl = new PdfPTable(2);
                finalTbl.WidthPercentage = 100;
                finalTbl.SpacingBefore = 7;
                finalTbl.SpacingAfter = 7;
                finalTbl.SplitLate = false;

                for (int sdocCount = 0; sdocCount < sdocDetails.Count; sdocCount++)
                {
                    PdfPTable mainTbl = new PdfPTable(1);
                    mainTbl.WidthPercentage = 100;
                    mainTbl.SpacingBefore = 7;
                    mainTbl.SpacingAfter = 7;

                    if(sdocCount==0)
                    {
                        c1 = new Chunk(Utility.ReportTableLHSHeader, FontFactory.GetFont("Times New Roman"));
                        c1.Font.Color = new BaseColor(255, 0, 0);
                        c1.Font.Size = 12;
                        p1 = new Phrase();
                        p1.Add(c1);
                        PdfPCell sdocnamecell1 = new PdfPCell(p1);
                        sdocnamecell1.Border = 0;
                        sdocnamecell1.BackgroundColor = new BaseColor(230, 184, 183);
                        sdocnamecell1.HorizontalAlignment = Element.ALIGN_CENTER;
                        mainTbl.AddCell(sdocnamecell1);
                    }
                    else
                    {
                        c1 = new Chunk(Utility.ReportTableRHSHeader, FontFactory.GetFont("Times New Roman"));
                        c1.Font.Color = new BaseColor(255, 0, 0);
                        c1.Font.Size = 12;
                        p1 = new Phrase();
                        p1.Add(c1);
                        PdfPCell sdocnamecell1 = new PdfPCell(p1);
                        sdocnamecell1.Border = 0;
                        sdocnamecell1.BackgroundColor = new BaseColor(230, 184, 183);
                        sdocnamecell1.HorizontalAlignment = Element.ALIGN_CENTER;
                        mainTbl.AddCell(sdocnamecell1);
                    }
                    c1 = new Chunk(sdocDetails[sdocCount], FontFactory.GetFont("Times New Roman"));
                    c1.Font.Color = new BaseColor(0, 0, 0);
                    c1.Font.Size = 6;
                    p1 = new Phrase();
                    p1.Add(c1);
                    PdfPCell sdocnamecell = new PdfPCell(p1);
                    sdocnamecell.Border = 0;
                    sdocnamecell.HorizontalAlignment = Element.ALIGN_CENTER;
                    mainTbl.AddCell(sdocnamecell);

                    #region  ---- General Info -----
                    List<string> generalDetails = new List<string>();
                    List<string> machineDetails = new List<string>();
                    List<string> consumableDetails = new List<string>();
                    List<string> workpieceDetails = new List<string>();
                    List<string> operationalDetails = new List<string>();
                    int gCount = 0, mCount = 0, cCount = 0, wCount = 0, oCount = 0;
                    int mColorCount=0;

                    for (int i = 0; i < gvGeneralInfo.Rows.Count; i++)
                    {
                        string inputModule = gvGeneralInfo.Rows[i].Cells[1].Text;
                        if (inputModule == "General Information")
                        {
                            if (gCount == 0)
                            {
                                generalDetails.Add("General Information");
                                gCount++;
                            }
                            if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                            {
                                gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text = "";
                                //continue;
                            }
                            generalDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                            generalDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                        }
                        else if (inputModule == "Machine Tool")
                        {
                            if (mCount == 0)
                            {
                                machineDetails.Add("Machine Tool");
                                mCount++;
                            }
                            if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                            {
                                gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text = "";
                                // continue;
                            }
                            machineDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                            machineDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                            //  machineDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text + "~" + gvGeneralInfo.Rows[i].Cells[gvGeneralInfo.Rows[i].Cells.Count-1].Text);
                        }
                        else if (inputModule == "Consumables Details")
                        {
                            if (cCount == 0)
                            {
                                consumableDetails.Add("Consumables Details");
                                cCount++;
                            }
                            if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                            {
                                gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text = "";
                                //continue;
                            }
                            consumableDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                            consumableDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                        }
                        else if (inputModule == "Workpiece Details")
                        {
                            if (wCount == 0)
                            {
                                workpieceDetails.Add("Workpiece Details");
                                wCount++;
                            }
                            if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                            {
                                gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text = "";
                                //continue;
                            }
                            if (gvGeneralInfo.Rows[i].Cells[2].Text == "Hardness")
                            {
                                string hardnessUnit = string.Empty;
                                hardnessUnit = DBAccess.getHardnessUnit(sdocDetails[sdocCount]);
                                workpieceDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                                workpieceDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text + "  " + hardnessUnit);
                                continue;
                            }
                            if (gvGeneralInfo.Rows[i].Cells[2].Text == "Hardness Unit")
                            {

                                continue;
                            }
                            workpieceDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                            workpieceDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                        }
                        else if (inputModule == "Operational Parameters")
                        {
                            if (oCount == 0)
                            {
                                operationalDetails.Add("Operational Parameters");
                                oCount++;
                            }
                            if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                            {
                                gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text = "";
                                //continue;
                            }
                            operationalDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                            operationalDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                        }
                    }
                    generalInfoCount++;



                    if (generalDetails.Count > 0)
                    {
                        PdfPCell mcell = new PdfPCell();
                        PdfPTable generalDetailsTbl = new PdfPTable(2);
                        generalDetailsTbl.WidthPercentage = 100;
                        generalDetailsTbl.SpacingBefore = 7;
                        generalDetailsTbl.SpacingAfter = 7;
                        for (int i = 0; i < generalDetails.Count; i++)
                        {
                            if (i == 0)
                            {
                                PdfPCell headerCell = getPdfCellWithBoldText(generalDetails[i]);
                                headerCell.Colspan = 2;
                                headerCell.BackgroundColor = new BaseColor(230, 184, 183);
                                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                generalDetailsTbl.AddCell(headerCell);
                            }
                            else if ((i % 2) == 0)
                            {
                                generalDetailsTbl.AddCell(getPdfCell(generalDetails[i].Replace("&amp;", "&")));
                            }
                            else
                            {
                                generalDetailsTbl.AddCell(getPdfCell(generalDetails[i].Replace("&amp;", "&")));
                            }
                        }
                        mcell.AddElement(generalDetailsTbl);
                        mcell.Border = 0;
                        mainTbl.AddCell(mcell);
                    }
                    if (machineDetails.Count > 0)
                    {
                        //PdfPTable machineDetailsTbl1 = new PdfPTable(1);
                        PdfPCell mcell = new PdfPCell();
                        PdfPTable machineDetailsTbl = new PdfPTable(2);
                        machineDetailsTbl.WidthPercentage = 100;
                        machineDetailsTbl.SpacingBefore = 7;
                        machineDetailsTbl.SpacingAfter = 7;
                        for (int i = 0; i < machineDetails.Count; i++)
                        {
                            if (i == 0)
                            {
                                PdfPCell headerCell = getPdfCellWithBoldText(machineDetails[i]);
                                headerCell.Colspan = 2;
                                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                headerCell.BackgroundColor = new BaseColor(230, 184, 183);
                                machineDetailsTbl.AddCell(headerCell);
                            }
                            else if ((i % 2) == 0)
                            {
                                machineDetailsTbl.AddCell(getPdfCell(machineDetails[i].Replace("&amp;", "&")));
                                //string[] value = machineDetails[i].Split('~');
                                //if (value[1] == "True")
                                //{
                                //    machineDetailsTbl.AddCell(getPdfCell(value[0].Replace("&amp;", "&")));
                                //}
                                //else
                                //{
                                //    machineDetailsTbl.AddCell(getPdfCell(value[0].Replace("&amp;", "&"))).BackgroundColor = new BaseColor(222, 222, 222);
                                //}

                            }
                            else
                            {

                                machineDetailsTbl.AddCell(getPdfCell(machineDetails[i].Replace("&amp;", "&")));
                            }
                        }

                        //PdfPCell p = new PdfPCell();
                        //p.Border = 0;
                        //p.AddElement(machineDetailsTbl);
                        //machineDetailsTbl1.AddCell(p);

                        //mcell.AddElement(machineDetailsTbl1);
                        //mcell.Border = 0;
                        //mainTbl.AddCell(mcell);
                        mcell.AddElement(machineDetailsTbl);
                        mcell.Border = 0;
                        mainTbl.AddCell(mcell);
                    }
                    if (consumableDetails.Count > 0)
                    {
                        PdfPCell mcell = new PdfPCell();
                        PdfPTable DetailsTbl = new PdfPTable(2);
                        DetailsTbl.WidthPercentage = 100;
                        DetailsTbl.SpacingBefore = 7;
                        DetailsTbl.SpacingAfter = 7;
                        for (int i = 0; i < consumableDetails.Count; i++)
                        {
                            if (i == 0)
                            {
                                PdfPCell headerCell = getPdfCellWithBoldText(consumableDetails[i]);
                                headerCell.Colspan = 2;
                                headerCell.BackgroundColor = new BaseColor(230, 184, 183);
                                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                DetailsTbl.AddCell(headerCell);
                            }
                            else if ((i % 2) == 0)
                            {
                                DetailsTbl.AddCell(getPdfCell(consumableDetails[i].Replace("&amp;", "&")));
                            }
                            else
                            {
                                DetailsTbl.AddCell(getPdfCell(consumableDetails[i].Replace("&amp;", "&")));
                            }
                        }
                        mcell.AddElement(DetailsTbl);
                        mcell.Border = 0;
                        mainTbl.AddCell(mcell);
                    }
                    if (workpieceDetails.Count > 0)
                    {
                        PdfPCell mcell = new PdfPCell();
                        PdfPTable DetailsTbl = new PdfPTable(2);
                        DetailsTbl.WidthPercentage = 100;
                        DetailsTbl.SpacingBefore = 7;
                        DetailsTbl.SpacingAfter = 7;
                        for (int i = 0; i < workpieceDetails.Count; i++)
                        {
                            if (i == 0)
                            {
                                PdfPCell headerCell = getPdfCellWithBoldText(workpieceDetails[i]);
                                headerCell.Colspan = 2;
                                headerCell.BackgroundColor = new BaseColor(230, 184, 183);
                                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                DetailsTbl.AddCell(headerCell);
                            }
                            else if ((i % 2) == 0)
                            {
                                DetailsTbl.AddCell(getPdfCell(workpieceDetails[i].Replace("&amp;", "&")));
                            }
                            else
                            {
                                DetailsTbl.AddCell(getPdfCell(workpieceDetails[i].Replace("&amp;", "&")));
                            }
                        }
                        mcell.AddElement(DetailsTbl);
                        mcell.Border = 0;
                        mainTbl.AddCell(mcell);
                    }
                    if (operationalDetails.Count > 0)
                    {
                        PdfPTable DetailsTbl2 = new PdfPTable(1);
                        DetailsTbl2.WidthPercentage = 100;
                        PdfPCell mcell = new PdfPCell();
                        PdfPTable DetailsTbl = new PdfPTable(2);
                        DetailsTbl.WidthPercentage = 100;
                        DetailsTbl.SpacingBefore = 7;
                        DetailsTbl.SpacingAfter = 7;
                        for (int i = 0; i < operationalDetails.Count; i++)
                        {
                            if (i == 0)
                            {
                                PdfPCell headerCell = getPdfCellWithBoldText(operationalDetails[i]);
                                headerCell.Colspan = 2;
                                headerCell.BackgroundColor = new BaseColor(230, 184, 183);
                                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                DetailsTbl.AddCell(headerCell);
                            }
                            else if ((i % 2) == 0)
                            {
                                DetailsTbl.AddCell(getPdfCell(operationalDetails[i].Replace("&amp;", "&")));
                            }
                            else
                            {
                                DetailsTbl.AddCell(getPdfCell(operationalDetails[i].Replace("&amp;", "&")));
                            }
                        }
                        //PdfPCell p = new PdfPCell();
                        //p.AddElement(DetailsTbl1);
                        //p.Border = 0;
                        //DetailsTbl2.AddCell(p);
                        //mcell1.AddElement(DetailsTbl2);
                        //mcell1.Border = 0;
                        //mainTbl.AddCell(mcell1);
                        mcell.AddElement(DetailsTbl);
                        mcell.Border = 0;
                        mainTbl.AddCell(mcell);
                    }
                    #endregion

                    #region  ----- Output ----------
                    if (lvQualityParam.Items.Count > 0)
                    {
                        int noOfQltItem = lvQualityParam.Items.Count;

                        PdfPCell qlyCell = new PdfPCell();
                        PdfPTable qlyDetailsFinalTbl = new PdfPTable(1);
                        qlyDetailsFinalTbl.WidthPercentage = 100;
                        qlyDetailsFinalTbl.SpacingBefore = 7;
                        qlyDetailsFinalTbl.SpacingAfter = 7;
                        PdfPCell qlyDetailsFinalCell = new PdfPCell();
                        PdfPTable qlyDetailsTbl = new PdfPTable(5);
                        qlyDetailsTbl.WidthPercentage = 100;
                        qlyDetailsTbl.SpacingBefore = 7;
                        qlyDetailsTbl.SpacingAfter = 7;
                        ListView qlyListview = (lvQualityParam.Items[qlyCount].FindControl("lvinnerQly") as ListView);
                        qlyCount++;
                        ListView dlyFinallistview = new ListView();
                        for (int i = 0; i < qlyListview.Items.Count; i++)
                        {
                            if (string.IsNullOrEmpty((qlyListview.Items[i].FindControl("lblTargetLower") as Label).Text) && string.IsNullOrEmpty((qlyListview.Items[i].FindControl("lblTargetUpper") as Label).Text) && string.IsNullOrEmpty((qlyListview.Items[i].FindControl("lblActualLower") as Label).Text) && string.IsNullOrEmpty((qlyListview.Items[i].FindControl("lblActualUpper") as Label).Text))
                            {
                                dlyFinallistview.Items.Add(qlyListview.Items[i]);
                                //  continue;
                            }
                            else
                            {
                                dlyFinallistview.Items.Add(qlyListview.Items[i]);
                            }
                        }

                        if (dlyFinallistview.Items.Count > 0)
                        {
                            PdfPCell qlyheaderCell = getPdfCellWithBoldText("Quality Parameters");
                            qlyheaderCell.Colspan = 2;
                            qlyheaderCell.BackgroundColor = new BaseColor(230, 184, 183);
                            qlyheaderCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            qlyDetailsFinalTbl.AddCell(qlyheaderCell);
                            for (int i = 0; i < 3; i++)
                            {
                                string name = "";
                                int colspanValue = 1;
                                if (i == 1)
                                {
                                    name = "Target";
                                    colspanValue = 2;
                                }
                                else if (i == 2)
                                {
                                    name = "Achieved";
                                    colspanValue = 2;
                                }
                                PdfPCell qlyHeadercell = getPdfCellWithBoldText(name);
                                qlyHeadercell.Colspan = colspanValue;
                                qlyHeadercell.HorizontalAlignment = Element.ALIGN_CENTER;
                                qlyDetailsTbl.AddCell(qlyHeadercell);
                            }
                            for (int i = 0; i < 5; i++)
                            {
                                string name = "";
                                if (i == 0)
                                {
                                    name = "Item";
                                }
                                else if (i == 1 || i == 3)
                                {
                                    name = "Lower";
                                }
                                else if (i == 2 || i == 4)
                                {
                                    name = "Upper";
                                }
                                PdfPCell qlyHeadercell = getPdfCellWithBoldText(name);
                                qlyHeadercell.HorizontalAlignment = Element.ALIGN_CENTER;
                                qlyDetailsTbl.AddCell(qlyHeadercell);
                            }
                        }
                        for (int j = 0; j < dlyFinallistview.Items.Count; j++)
                        {

                            qlyDetailsTbl.AddCell(getPdfCellWithBoldText(((dlyFinallistview.Items[j].FindControl("Qlyparam") as Label).Text).Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                            qlyDetailsTbl.AddCell(getPdfCell(((dlyFinallistview.Items[j].FindControl("lblTargetLower") as Label).Text).Replace("&amp;", "&"))).VerticalAlignment=Element.ALIGN_MIDDLE;
                            qlyDetailsTbl.AddCell(getPdfCell(((dlyFinallistview.Items[j].FindControl("lblTargetUpper") as Label).Text).Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                            qlyDetailsTbl.AddCell(getPdfCell(((dlyFinallistview.Items[j].FindControl("lblActualLower") as Label).Text).Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                            qlyDetailsTbl.AddCell(getPdfCell(((dlyFinallistview.Items[j].FindControl("lblActualUpper") as Label).Text).Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                        }
                        qlyDetailsFinalCell.AddElement(qlyDetailsTbl);
                        qlyDetailsFinalTbl.AddCell(qlyDetailsFinalCell);
                        PdfPCell qCell = new PdfPCell();
                        qCell.AddElement(qlyDetailsFinalTbl);
                        qCell.Border = 0;
                        mainTbl.AddCell(qCell);
                    }
                    #endregion

                    #region  ----- Calculated Param Graph ---

                    if (calcParamTable.Rows.Count > 0)
                    {
                        PdfPTable calPramTbl = new PdfPTable(1);
                        calPramTbl.WidthPercentage = 100;
                        //calPramTbl.SpacingAfter = 7;
                        calPramTbl.SpacingBefore = 7;
                        PdfPTable calPramgraphTbl = new PdfPTable(1);
                        calPramgraphTbl.WidthPercentage = 100;
                        calPramgraphTbl.SpacingAfter = 7;
                        //calPramgraphTbl.SpacingBefore = 7;
                        PdfPCell calParamCell1 = getPdfCellWithBoldText("Calculated Parameters");
                        calParamCell1.Colspan = 2;
                        calParamCell1.BackgroundColor = new BaseColor(230, 184, 183);
                        calParamCell1.HorizontalAlignment = Element.ALIGN_CENTER;
                        calPramTbl.AddCell(calParamCell1);
                        PdfPTable innerTbl = new PdfPTable(2);
                        innerTbl.AddCell(getPdfCellWithBoldText("Item"));
                        innerTbl.AddCell(getPdfCellWithBoldText("Value"));
                        for (int i = 0; i < calcParamTable.Rows.Count; i++)
                        {
                            innerTbl.AddCell(getPdfCell(calcParamTable.Rows[i][0].ToString()));
                            innerTbl.AddCell(getPdfCell(calcParamTable.Rows[i][calParamCount].ToString()));
                        }
                        calPramTbl.AddCell(innerTbl);
                        PdfPCell calculateparamCell = new PdfPCell();
                        calculateparamCell.AddElement(calPramTbl);
                        calculateparamCell.Border = 0;
                        mainTbl.AddCell(calculateparamCell);

                        //graph
                        byte[] calcbytes = Convert.FromBase64String(calcParamGraphData[sdocCount]);
                        iTextSharp.text.Image calcimggraph = iTextSharp.text.Image.GetInstance(calcbytes);
                        calcimggraph.ScaleToFit(1000f, 1000f);
                        PdfPCell calParamCell2 = new PdfPCell(calcimggraph, true);
                        calPramgraphTbl.AddCell(calParamCell2);
                        calParamCount++;

                        PdfPCell calculateparamgraphCell = new PdfPCell();
                        calculateparamgraphCell.AddElement(calPramgraphTbl);
                        calculateparamgraphCell.Border = 0;
                        mainTbl.AddCell(calculateparamgraphCell);

                    }
                    #endregion

                    #region  ----- Total Cycle Time Graph ---

                    if (totalcycletimeTable.Rows.Count > 0)
                    {
                        PdfPTable tctTbl = new PdfPTable(1);
                        tctTbl.WidthPercentage = 100;
                        //  tctTbl.SpacingAfter = 7;
                        tctTbl.SpacingBefore = 7;
                        PdfPTable tctgraphTbl = new PdfPTable(1);
                        tctgraphTbl.WidthPercentage = 100;
                        // tctgraphTbl.SpacingAfter = 7;
                        // tctgraphTbl.SpacingBefore = 7;
                        PdfPTable tctdrillTbl = new PdfPTable(1);
                        tctdrillTbl.WidthPercentage = 100;
                        tctdrillTbl.SpacingAfter = 7;
                        // tctdrillTbl.SpacingBefore = 7;

                        PdfPCell tctcell = getPdfCellWithBoldText("Total Cycle Time");
                        tctcell.Colspan = 2;
                        tctcell.BackgroundColor = new BaseColor(230, 184, 183);
                        tctcell.HorizontalAlignment = Element.ALIGN_CENTER;
                        tctTbl.AddCell(tctcell);
                        PdfPTable innerTbl = new PdfPTable(2);
                        innerTbl.AddCell(getPdfCellWithBoldText("Item"));
                        innerTbl.AddCell(getPdfCellWithBoldText("Value"));
                        for (int i = 0; i < totalcycletimeTable.Rows.Count; i++)
                        {
                            innerTbl.AddCell(getPdfCell(totalcycletimeTable.Rows[i][1].ToString()));
                            innerTbl.AddCell(getPdfCell(totalcycletimeTable.Rows[i][totalcycletimeCount].ToString()));
                        }
                        tctTbl.AddCell(innerTbl);
                        PdfPCell totalcycletimeCell = new PdfPCell();
                        totalcycletimeCell.AddElement(tctTbl);
                        totalcycletimeCell.Border = 0;
                        mainTbl.AddCell(totalcycletimeCell);

                        //graph
                        byte[] calcbytes = Convert.FromBase64String(totalcycletimeGraphData[sdocCount]);
                        iTextSharp.text.Image tctimggraph = iTextSharp.text.Image.GetInstance(calcbytes);
                       // tctimggraph.ScaleToFit(350f, 500f);
                        PdfPCell tctcell2 = new PdfPCell(tctimggraph, true);
                        tctgraphTbl.AddCell(tctcell2);

                        PdfPCell tctgraphCell = new PdfPCell();
                        tctgraphCell.AddElement(tctgraphTbl);
                        tctgraphCell.Border = 0;
                        mainTbl.AddCell(tctgraphCell);

                        //drill chart
                        byte[] drillbytes = Convert.FromBase64String(totalcycletimedrillGraphData[sdocCount]);
                        iTextSharp.text.Image tctdrillimggraph = iTextSharp.text.Image.GetInstance(drillbytes);
                        PdfPCell tctdrillcell2 = new PdfPCell(tctdrillimggraph, true);
                        tctdrillTbl.AddCell(tctdrillcell2);
                        totalcycletimeCount++;

                        PdfPCell tctdrillCell = new PdfPCell();
                        tctdrillCell.AddElement(tctdrillTbl);
                        tctdrillCell.Border = 0;
                        mainTbl.AddCell(tctdrillCell);
                        //pdfDoc.Add(calPramTbl);
                    }
                    #endregion
                    #region  ----- Grinding Time Graph ---

                    if (grindingtimeTable.Rows.Count > 0)
                    {
                        PdfPTable gtTbl = new PdfPTable(1);
                        gtTbl.WidthPercentage = 100;
                        // gtTbl.SpacingAfter = 7;
                        gtTbl.SpacingBefore = 7;
                        PdfPTable gtgraphTbl = new PdfPTable(1);
                        gtgraphTbl.WidthPercentage = 100;
                        gtgraphTbl.SpacingAfter = 7;
                        // gtgraphTbl.SpacingBefore = 7;
                        PdfPCell tctcell = getPdfCellWithBoldText("Calculated Time");
                        tctcell.Colspan = 2;
                        tctcell.BackgroundColor = new BaseColor(230, 184, 183);
                        tctcell.HorizontalAlignment = Element.ALIGN_CENTER;
                        gtTbl.AddCell(tctcell);
                        PdfPTable innerTbl = new PdfPTable(2);
                        innerTbl.AddCell(getPdfCellWithBoldText("Item"));
                        innerTbl.AddCell(getPdfCellWithBoldText("Value"));
                        for (int i = 0; i < grindingtimeTable.Rows.Count; i++)
                        {
                            innerTbl.AddCell(getPdfCell(grindingtimeTable.Rows[i][1].ToString()));
                            innerTbl.AddCell(getPdfCell(grindingtimeTable.Rows[i][grindingtimeCount].ToString()));
                        }
                        gtTbl.AddCell(innerTbl);
                        PdfPCell grindingtimeCell = new PdfPCell();
                        grindingtimeCell.AddElement(gtTbl);
                        grindingtimeCell.Border = 0;
                        mainTbl.AddCell(grindingtimeCell);

                        //graph
                        byte[] calcbytes = Convert.FromBase64String(grindingtimeGraphData[sdocCount]);
                        iTextSharp.text.Image tctimggraph = iTextSharp.text.Image.GetInstance(calcbytes);
                      // tctimggraph.ScaleAbsolute(290f, 280f);
                        //PdfPTable imgtable = new PdfPTable(1);
                        //imgtable.AddCell(tctimggraph);
                       // tctimggraph.ScaleToFitHeight = true;

                        PdfPCell tctcell2 = new PdfPCell(tctimggraph,true);
                        //tctcell2.FixedHeight = 600f;
                      //  tctcell2.AddElement(new Chunk(tctimggraph, 0, 0));
                        gtgraphTbl.AddCell(tctcell2);
                        PdfPCell grindingtimegraphCell = new PdfPCell();
                        grindingtimegraphCell.AddElement(gtgraphTbl);
                        grindingtimegraphCell.Border = 0;
                        mainTbl.AddCell(grindingtimegraphCell);
                        //pdfDoc.Add(calPramTbl);
                    }
                    #endregion


                    finalTbl.AddCell(mainTbl);
                }


                #region ------- Remarks------
                if (txtRemarks.Text != "")
                {
                    if(sdocDetails.Count==1)
                    {
                        PdfPTable mainTbl = new PdfPTable(1);
                        mainTbl.WidthPercentage = 100;
                        mainTbl.SpacingBefore = 7;
                        mainTbl.SpacingAfter = 7;
                        PdfPCell remarlcell = new PdfPCell();
                        remarlcell = getPdfCellWithBoldText("Remarks");
                        remarlcell.BackgroundColor = new BaseColor(230, 184, 183);
                        remarlcell.HorizontalAlignment = Element.ALIGN_LEFT;
                        mainTbl.AddCell(remarlcell);
                        remarlcell = new PdfPCell();
                        remarlcell = getPdfCell(txtRemarks.Text);
                        mainTbl.AddCell(remarlcell);
                        finalTbl.AddCell(mainTbl);
                    }
                    else
                    {
                        PdfPCell remarlcell = new PdfPCell();
                        remarlcell = getPdfCellWithBoldText("Remarks");
                        remarlcell.Colspan = 2;
                        remarlcell.BackgroundColor = new BaseColor(230, 184, 183);
                        remarlcell.HorizontalAlignment = Element.ALIGN_LEFT;
                        finalTbl.AddCell(remarlcell);
                        remarlcell = new PdfPCell();
                        remarlcell = getPdfCell(txtRemarks.Text);
                        remarlcell.Colspan = 2;
                        finalTbl.AddCell(remarlcell);

                    }
                   

                }
                #endregion

                for (int sdocCount = 0; sdocCount < sdocDetails.Count; sdocCount++)
                {
                    PdfPTable mainTbl = new PdfPTable(1);
                    mainTbl.WidthPercentage = 100;
                    mainTbl.SpacingBefore = 7;
                    mainTbl.SpacingAfter = 7;

                    //mainTbl.SplitLate = false;
                    #region  ------- Images --------------
                    //PdfPTable imageDetails = new PdfPTable(1);
                    //imageDetails.WidthPercentage = 100;
                    //imageDetails.SpacingAfter = 7;
                    //imageDetails.SpacingBefore = 7;
                    //imageDetails.SplitLate = false;
                    if (lvImages.Items.Count > 0)
                    {
                        try
                        {
                            PdfPCell imgheaderCell = getPdfCellWithBoldText("Process");
                            imgheaderCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            imgheaderCell.BackgroundColor = new BaseColor(230, 184, 183);
                            mainTbl.AddCell(imgheaderCell);
                            //for(int imgC = 0; imgC < lvImages.Items.Count; imgC++)
                            //{
                                string s = (lvImages.Items[imageCount].FindControl("imgSdoc") as HiddenField).Value;
                                string s4 = sdocDetails[sdocCount].ToString();
                                if ((lvImages.Items[imageCount].FindControl("imgSdoc") as HiddenField).Value == sdocDetails[sdocCount].ToString())
                                {
                                    ListView imageListview = lvImages.Items[imageCount].FindControl("lvImageDetails") as ListView;
                                    imageCount++;
                                    for (int i = 0; i < imageListview.Items.Count; i++)
                                    {
                                        try
                                        {
                                            PdfPCell imgCell = new PdfPCell();
                                            PdfPTable imageDetails1 = new PdfPTable(1);
                                            imageDetails1.WidthPercentage = 100;
                                            imageDetails1.SpacingBefore = 3;
                                            imageDetails1.SpacingAfter = 3;
                                          //  imageDetails1.SplitLate = true;
                                            imageDetails1.AddCell(getPdfCell((imageListview.Items[i].FindControl("imgName") as HtmlGenericControl).InnerText));
                                            string path = (imageListview.Items[i].FindControl("hdnImagePath") as HiddenField).Value;
                                            byte[] imgfile;
                                            imgfile = System.IO.File.ReadAllBytes(Server.MapPath(path));//ImagePath  
                                            iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgfile);
                                            PdfPCell imgCell1 = new PdfPCell(img, true);
                                            imageDetails1.AddCell(imgCell1);
                                            //if (img.ScaledHeight > 200)
                                            //{
                                            //    imgCell.FixedHeight = 200;
                                            //}
                                            //img.ScaleToFit(50f, 30f);
                                            imgCell.AddElement(imageDetails1);
                                            mainTbl.AddCell(imgCell);
                                        }
                                        catch (Exception ex1)
                                        {
                                            // k++;
                                        }
                                    }

                                }
                                else
                                {
                                    // imageCount++;
                                    PdfPCell dummyimgCell = new PdfPCell();
                                    mainTbl.AddCell(dummyimgCell);
                                }
                          //  }
                           
                        }
                        catch (Exception ex)
                        {
                            PdfPCell dummyimgCell = new PdfPCell();
                            mainTbl.AddCell(dummyimgCell);
                        }

                    }

                    //PdfPCell imageCell = new PdfPCell();
                    //imageCell.AddElement(imageDetails);
                    //imageCell.Border = 0;
                    //mainTbl.AddCell(imageCell);


                    #endregion
                    PdfPCell pdfPCell = new PdfPCell();
                    finalTbl.AddCell(mainTbl);
                    //pdfDoc.Add(finalTbl);
                }

                #region ---- Footer-----
                PdfPTable customerTbl = new PdfPTable(1);
                customerTbl.SpacingBefore=20;
                //customerTbl.SpacingAfter = 10;
                PdfPCell customercell = new PdfPCell();
                customercell = getPdfCell("Customer");
                customercell.Border = 0;
                customerTbl.AddCell(customercell);
                PdfPCell finalcustomercell = new PdfPCell();
                finalcustomercell.AddElement(customerTbl);
                finalTbl.AddCell(finalcustomercell);

                PdfPTable cumiTbl = new PdfPTable(1);
                //cumiTbl.SpacingAfter = 10;
                cumiTbl.SpacingBefore = 20;
                PdfPCell cumilcell = new PdfPCell();
                cumilcell = getPdfCell("Cumi");
                cumilcell.Border = 0;
                cumiTbl.AddCell(cumilcell);
                PdfPCell finalcumicell = new PdfPCell();
                finalcumicell.AddElement(cumiTbl);
                finalTbl.AddCell(finalcumicell);
                #endregion

                pdfDoc.Add(finalTbl);
                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=Comparison_Report"+DateTime.Now+".pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Write(pdfDoc);
                //Response.End();
                Response.Flush();
                //HttpContext.Current.Response.SuppressContent = true;
                //HttpContext.Current.ApplicationInstance.CompleteRequest();

            }
            catch (Exception ex)
            {

            }

        }

        private PdfPCell getPdfCell(string value)
        {
            Chunk chunk = new Chunk(value);
            chunk.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
            chunk.Font.SetStyle(0);
            chunk.Font.Size = 7;
            Phrase phrase = new Phrase();
            phrase.Add(chunk);
            PdfPCell cell = new PdfPCell(phrase);
            cell.ExtraParagraphSpace = 3;
            cell.BorderColor = new BaseColor(70, 70, 70);
            return cell;
        }
        private PdfPCell getPdfCellWithBoldText(string value)
        {
            iTextSharp.text.Font font = FontFactory.GetFont("Calibri (Body)", 10, iTextSharp.text.Font.BOLD);
            iTextSharp.text.Font boldFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD);
            Chunk chunk = new Chunk(value, font);
            chunk.Font.Color = new iTextSharp.text.BaseColor(0, 0, 0);
            //chunk.Font.SetStyle(0);
            chunk.Font.Size = 9;
            Phrase phrase = new Phrase();
            phrase.Add(chunk);
            PdfPCell cell = new PdfPCell(phrase);
            cell.ExtraParagraphSpace = 3;
            cell.BorderColor = new BaseColor(70, 70, 70);
            return cell;
        }

        protected void txtSDocId1_TextChanged(object sender, EventArgs e)
        {
             bindSDocId2list();
        }

        protected void gvGeneralInfo_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType != DataControlRowType.DataRow) return;
            if ((e.Row.Cells[3].Text=="" || e.Row.Cells[3].Text=="&nbsp;") && (e.Row.Cells[4].Text == "" || e.Row.Cells[4].Text == "&nbsp;"))
            {
                gvGeneralInfo.DeleteRow(e.Row.RowIndex);
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
        private void setWorkSheetSetting(ExcelWorksheet wksheet)        {            wksheet.PrinterSettings.Orientation = eOrientation.Landscape;            wksheet.PrinterSettings.FitToPage = true;            wksheet.PrinterSettings.FitToWidth = 1;            wksheet.PrinterSettings.FitToHeight = 0;

        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {


                List<string> sdocDetails = new List<string>();
                string Sdocid = "";
                int noOfSdoc = gvGeneralInfo.HeaderRow.Cells.Count - 6;
                if (noOfSdoc > 0)
                {
                    for (int i = 3; i < gvGeneralInfo.HeaderRow.Cells.Count - 3; i++)
                    {
                        sdocDetails.Add(gvGeneralInfo.HeaderRow.Cells[i].Text);
                        if (sdocDetails.Count == 1)
                        {
                            Sdocid = gvGeneralInfo.HeaderRow.Cells[i].Text;
                        }
                    }
                }
                else
                {
                    return;
                }

                string templatefile = string.Empty;
                string Filename = "SDocComparison.xlsx";

                string Source = string.Empty;
                Source = GetReportPath(Filename);
                string Template = string.Empty;
                Template = Sdocid + "_" + DateTime.Now + ".xlsx";
                string destination = string.Empty;
                destination = Path.Combine(appPath, "Temp", SafeFileName(Template));
                if (!File.Exists(Source))
                {
                    Logger.WriteDebugLog("SDoc Comparison Report- \n " + Source);
                }

                FileInfo newFile = new FileInfo(Source);
                ExcelPackage Excel = new ExcelPackage(newFile, true);
                Excel.Workbook.Worksheets.Delete("Sheet1");
                #region ----General Information-----
                if (gvGeneralInfo != null)
                {
                    DataTable dtGenearalInfo = new DataTable();
                    //Forloop for header
                    for (int i = 0; i < gvGeneralInfo.HeaderRow.Cells.Count; i++)
                    {
                        dtGenearalInfo.Columns.Add(gvGeneralInfo.HeaderRow.Cells[i].Text);
                    }
                    //foreach for datarow
                    foreach (GridViewRow row in gvGeneralInfo.Rows)
                    {
                        DataRow dr = dtGenearalInfo.NewRow();
                        for (int j = 0; j < row.Cells.Count; j++)
                        {
                            dr[gvGeneralInfo.HeaderRow.Cells[j].Text] = row.Cells[j].Text == "&nbsp;" ? "" : row.Cells[j].Text;
                        }
                        dtGenearalInfo.Rows.Add(dr);
                    }

                    var exelworksheet = Excel.Workbook.Worksheets.Add("General Informations");
                    setWorkSheetSetting(exelworksheet);

                    int cellRow = 1;
                    exelworksheet.Row(1).Height = 30;
                    System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath(Utility.ReportImageLHS));
                    ExcelPicture pic = exelworksheet.Drawings.AddPicture("ImgLHS", img);
                    pic.SetPosition(0, 0, 0, 0);
                    pic.SetSize(40,40);
                    exelworksheet.Protection.IsProtected = false;
                    exelworksheet.Protection.AllowSelectLockedCells = false;
                    System.Drawing.Image imgs = System.Drawing.Image.FromFile(Server.MapPath(Utility.ReportImageRHS));
                    ExcelPicture pics = exelworksheet.Drawings.AddPicture("ImgLHSs", imgs);
                    pics.SetPosition(0, 0,3, 150);
                    pics.SetSize(40, 40);
                    exelworksheet.Protection.IsProtected = false;
                    exelworksheet.Protection.AllowSelectLockedCells = false;

                    exelworksheet.Cells[cellRow, cellRow, cellRow, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, 4].Value = "Data Collection / Trial Wheel Documentation";
                    exelworksheet.Cells[cellRow, cellRow, cellRow, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, 4].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(231, 231, 231));
                    exelworksheet.Cells[cellRow, cellRow, cellRow, 4].Merge = true;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, 4].Style.Font.Size = 18;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, 4].Style.Font.Bold = true;
                    //exelworksheet.Cells[cellRow, cellRow, cellRow, 4].Style.Border;
                    exelworksheet.Cells[cellRow, cellRow, cellRow, 4].Style.Font.Color.SetColor(Color.Red);
                    cellRow = cellRow + 1;
                    int cellColumn = 1;
                    for (int sdoccount = 0; sdoccount < sdocDetails.Count; sdoccount++)
                    {
                        cellRow = 3;
                        exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Value = sdoccount == 0 ? Utility.ReportTableLHSHeader : Utility.ReportTableRHSHeader;
                        exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(230, 184, 183));
                        exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Merge = true;
                        exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Style.Font.Size = 14;
                        exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Style.Font.Bold = true;
                        exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Style.Font.Color.SetColor(Color.Red);
                        exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        cellRow++;
                        exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Value = sdocDetails[sdoccount];
                        exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Merge = true;
                        exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Style.Font.Size = 10;
                        exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        string inputModuleName = "";
                        for (int i = 0; i < dtGenearalInfo.Rows.Count; i++)
                        {
                            for (int j = 0; j < dtGenearalInfo.Columns.Count; j++)
                            {
                                if (inputModuleName != dtGenearalInfo.Rows[i]["InputModule"].ToString())
                                {
                                    cellRow++;
                                    exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Value = dtGenearalInfo.Rows[i]["InputModule"].ToString();
                                    exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(230, 184, 183));
                                    exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Merge = true;
                                    exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Style.Font.Bold = true;
                                    exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Style.Font.Color.SetColor(Color.Black);
                                    exelworksheet.Cells[cellRow, cellColumn, cellRow, cellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                    cellRow++;
                                    inputModuleName = dtGenearalInfo.Rows[i]["InputModule"].ToString();
                                }

                                exelworksheet.Cells[cellRow, cellColumn].Value = dtGenearalInfo.Rows[i]["Items"].ToString();
                                exelworksheet.Cells[cellRow, cellColumn + 1].Value = dtGenearalInfo.Rows[i][sdocDetails[sdoccount]].ToString();
                                exelworksheet.Cells[cellRow, cellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                exelworksheet.Column(cellColumn).AutoFit();

                            }
                            cellRow++;
                        }
                        cellColumn = cellColumn + 2;
                    }

                    for (int i = 1; i <= dtGenearalInfo.Columns.Count; i++)
                    {
                        exelworksheet.Cells[3, i, dtGenearalInfo.Rows.Count + 1, i].AutoFitColumns();
                    }
                }
                #endregion

                #region ----Quality Parameter----
                if (lvQualityParam != null)
                {
                    var exelQlyworksheet = Excel.Workbook.Worksheets.Add("Quality Parameters");
                    int cellRow = 1;
                    exelQlyworksheet.Row(1).Height = 30;
                    System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath(Utility.ReportImageLHS));
                    ExcelPicture pic = exelQlyworksheet.Drawings.AddPicture("ImgLHS", img);
                    pic.SetPosition(0, 0, 0, 0);
                    pic.SetSize(40, 40);
                    exelQlyworksheet.Protection.IsProtected = false;
                    exelQlyworksheet.Protection.AllowSelectLockedCells = false;
                    System.Drawing.Image imgrhs = System.Drawing.Image.FromFile(Server.MapPath(Utility.ReportImageRHS));
                    ExcelPicture picrhs = exelQlyworksheet.Drawings.AddPicture("ImgRHS", imgrhs);
                    picrhs.SetPosition(0, 0, 9,30);
                    picrhs.SetSize(40, 40);
                    exelQlyworksheet.Protection.IsProtected = false;
                    exelQlyworksheet.Protection.AllowSelectLockedCells = false;

                    exelQlyworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    exelQlyworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    exelQlyworksheet.Cells[cellRow, cellRow, cellRow, 10].Value = "Data Collection / Trial Wheel Documentation";
                    exelQlyworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    exelQlyworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(231, 231, 231));
                    exelQlyworksheet.Cells[cellRow, cellRow, cellRow, 10].Merge = true;
                    exelQlyworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.Font.Size = 18;
                    exelQlyworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.Font.Bold = true;
                    //exelworksheet.Cells[cellRow, cellRow, cellRow, 4].Style.Border;
                    exelQlyworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.Font.Color.SetColor(Color.Red);
                    cellRow++;
                    //exelQlyworksheet.Cells[4, 1, 4, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //exelQlyworksheet.Cells[4, 1, 4, 8].Value = "Quality Parameters";
                    //exelQlyworksheet.Cells[4, 1, 4, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //exelQlyworksheet.Cells[4, 1, 4, 8].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(209, 242, 208));
                    //exelQlyworksheet.Cells[4, 1, 4, 8].Merge = true;
                    //exelQlyworksheet.Cells[4, 1, 4, 8].Style.Font.Size = 14;
                    //exelQlyworksheet.Cells[4, 1, 4, 8].Style.Font.Bold = true;
                    //exelQlyworksheet.Cells[4, 1, 4, 8].Style.Font.Color.SetColor(Color.FromArgb(78, 73, 73));

                    int totalSDocCount = lvQualityParam.Items.Count;
                    int qcol = 1;
                    for (int i = 0; i < lvQualityParam.Items.Count; i++)
                    {
                        int qrow = 3;

                        exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Value = i == 0 ? Utility.ReportTableLHSHeader : Utility.ReportTableRHSHeader;
                        exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(230, 184, 183));
                        exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Merge = true;
                        exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Style.Font.Size = 14;
                        exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Style.Font.Bold = true;
                        exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Style.Font.Color.SetColor(Color.Red);
                        exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        qrow++;
                        exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Value = (lvQualityParam.Items[i].FindControl("SDocId") as HtmlGenericControl).InnerText;
                        exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Merge = true;
                        exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Style.Font.Size = 10;
                        exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        qrow++;
                        exelQlyworksheet.Cells[qrow, qcol, qrow, qcol].Value = "";
                        exelQlyworksheet.Cells[qrow, qcol + 1, qrow, qcol + 2].Value = "Target";
                        exelQlyworksheet.Cells[qrow, qcol + 1, qrow, qcol + 2].Style.Font.Bold = true;
                        exelQlyworksheet.Cells[qrow, qcol + 1, qrow, qcol + 2].Merge = true;
                        exelQlyworksheet.Cells[qrow, qcol + 1, qrow, qcol + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        exelQlyworksheet.Cells[qrow, qcol + 3, qrow, qcol + 4].Value = "Achieved";
                        exelQlyworksheet.Cells[qrow, qcol + 3, qrow, qcol + 4].Style.Font.Bold = true;
                        exelQlyworksheet.Cells[qrow, qcol + 3, qrow, qcol + 4].Merge = true;
                        exelQlyworksheet.Cells[qrow, qcol + 3, qrow, qcol + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        exelQlyworksheet.Cells[qrow, qcol + 3, qrow, qcol + 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        qrow++;
                        exelQlyworksheet.Cells[qrow, qcol, qrow, qcol].Value = "Item";
                        exelQlyworksheet.Cells[qrow, qcol, qrow, qcol].Style.Font.Bold = true;
                        exelQlyworksheet.Cells[qrow, qcol + 1, qrow, qcol + 1].Value = "Lower";
                        exelQlyworksheet.Cells[qrow, qcol + 1, qrow, qcol + 1].Style.Font.Bold = true;
                        exelQlyworksheet.Cells[qrow, qcol + 2, qrow, qcol + 2].Value = "Upper";
                        exelQlyworksheet.Cells[qrow, qcol + 2, qrow, qcol + 2].Style.Font.Bold = true;
                        exelQlyworksheet.Cells[qrow, qcol + 3, qrow, qcol + 3].Value = "Lower";
                        exelQlyworksheet.Cells[qrow, qcol + 3, qrow, qcol + 3].Style.Font.Bold = true;
                        exelQlyworksheet.Cells[qrow, qcol + 4, qrow, qcol + 4].Value = "Upper";
                        exelQlyworksheet.Cells[qrow, qcol + 4, qrow, qcol + 4].Style.Font.Bold = true;
                        exelQlyworksheet.Cells[qrow, qcol + 4, qrow, qcol + 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                        qrow++;
                        ListView lvSdocContent = (lvQualityParam.Items[i].FindControl("lvinnerQly") as ListView);
                        for (int j = 0; j < lvSdocContent.Items.Count; j++)
                        {
                            string parameter = (lvSdocContent.Items[j].FindControl("Qlyparam") as Label).Text;
                            string targetLower = (lvSdocContent.Items[j].FindControl("lblTargetLower") as Label).Text;
                            string targetUpper = (lvSdocContent.Items[j].FindControl("lblTargetUpper") as Label).Text;
                            string actualLower = (lvSdocContent.Items[j].FindControl("lblActualLower") as Label).Text;
                            string actualUpper = (lvSdocContent.Items[j].FindControl("lblActualUpper") as Label).Text;
                            exelQlyworksheet.Cells[qrow, qcol, qrow, qcol].Value = parameter;
                            if (targetLower == "&nbsp;" || targetLower == "")
                            {
                                exelQlyworksheet.Cells[qrow, qcol + 1, qrow, qcol + 1].Value = targetLower;
                            }
                            else
                            {
                                exelQlyworksheet.Cells[qrow, qcol + 1, qrow, qcol + 1].Value = Convert.ToDecimal(targetLower);
                            }
                            if (targetUpper == "" || targetUpper == "&nbsp;")
                            {
                                exelQlyworksheet.Cells[qrow, qcol + 2, qrow, qcol + 2].Value = targetUpper;
                            }
                            else
                            {
                                exelQlyworksheet.Cells[qrow, qcol + 2, qrow, qcol + 2].Value = Convert.ToDecimal(targetUpper);
                            }
                            if (actualLower == "" || actualLower == "&nbsp;")
                            {
                                exelQlyworksheet.Cells[qrow, qcol + 3, qrow, qcol + 3].Value = actualLower;
                            }
                            else
                            {
                                exelQlyworksheet.Cells[qrow, qcol + 3, qrow, qcol + 3].Value = Convert.ToDecimal(actualLower);
                            }
                            if (actualUpper == "" || actualUpper == "&nbsp;")
                            {
                                exelQlyworksheet.Cells[qrow, qcol + 4, qrow, qcol + 4].Value = actualUpper;
                            }
                            else
                            {
                                exelQlyworksheet.Cells[qrow, qcol + 4, qrow, qcol + 4].Value = Convert.ToDecimal(actualUpper);
                            }
                            exelQlyworksheet.Cells[qrow, qcol + 4, qrow, qcol + 4].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            qrow++;
                        }
                        exelQlyworksheet.Column(qcol).AutoFit();
                        qcol = qcol + 5;

                    }
                }
                #endregion

                #region -- Calculated Parameters
               
                DataTable dtcalculateParam = new DataTable();
                if (Session["CalcParamGraphData"] == null)
                {
                    List<string> ouput = DBAccess.cumiBindCalculatedParam(txtSDocId1.Text, txtSDocId2.Text);
                    dtcalculateParam = (DataTable)Session["CalcParamGraphData"];
                   
                }
                else
                {
                    dtcalculateParam = (DataTable)Session["CalcParamGraphData"];
                }
                List<string> sdocid = new List<string>();

                if (dtcalculateParam.Rows.Count > 0)
                {
                    var exelCalcParamworksheet = Excel.Workbook.Worksheets.Add("Calulated Parameter");
                    setWorkSheetSetting(exelCalcParamworksheet);
                    int cpcellRow = 1;
                    exelCalcParamworksheet.Row(1).Height = 30;
                    System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath(Utility.ReportImageLHS));
                    ExcelPicture pic = exelCalcParamworksheet.Drawings.AddPicture("ImgLHS", img);
                    pic.SetPosition(0, 0, 0, 0);
                    pic.SetSize(40, 40);
                    exelCalcParamworksheet.Protection.IsProtected = false;
                    exelCalcParamworksheet.Protection.AllowSelectLockedCells = false;
                    System.Drawing.Image imgrhs = System.Drawing.Image.FromFile(Server.MapPath(Utility.ReportImageRHS));
                    ExcelPicture picrhs = exelCalcParamworksheet.Drawings.AddPicture("ImgRHS", imgrhs);
                    picrhs.SetPosition(0, 0, 3, 30);
                    picrhs.SetSize(40, 40);
                    exelCalcParamworksheet.Protection.IsProtected = false;
                    exelCalcParamworksheet.Protection.AllowSelectLockedCells = false;

                    exelCalcParamworksheet.Cells[cpcellRow, cpcellRow, cpcellRow, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    exelCalcParamworksheet.Cells[cpcellRow, cpcellRow, cpcellRow, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    exelCalcParamworksheet.Cells[cpcellRow, cpcellRow, cpcellRow, 4].Value = "Data Collection / Trial Wheel Documentation";
                    exelCalcParamworksheet.Cells[cpcellRow, cpcellRow, cpcellRow, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    exelCalcParamworksheet.Cells[cpcellRow, cpcellRow, cpcellRow, 4].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(231, 231, 231));
                    exelCalcParamworksheet.Cells[cpcellRow, cpcellRow, cpcellRow, 4].Merge = true;
                    exelCalcParamworksheet.Cells[cpcellRow, cpcellRow, cpcellRow, 4].Style.Font.Size = 18;
                    exelCalcParamworksheet.Cells[cpcellRow, cpcellRow, cpcellRow, 4].Style.Font.Bold = true;
                    //exelworksheet.Cells[cellRow, cellRow, cellRow, 4].Style.Border;
                    exelCalcParamworksheet.Cells[cpcellRow, cpcellRow, cpcellRow, 4].Style.Font.Color.SetColor(Color.Red);
                    cpcellRow++;
                    int cpcellColumn = 1;
                    for (int i = 1; i < dtcalculateParam.Columns.Count; i++)
                    {
                        sdocid.Add(dtcalculateParam.Columns[i].ColumnName.ToString());
                    }
                    for (int sodccount = 0; sodccount < sdocid.Count; sodccount++)
                    {
                        cpcellRow = 3;
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Value = sodccount == 0 ? Utility.ReportTableLHSHeader : Utility.ReportTableRHSHeader;
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(230, 184, 183));
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Merge = true;
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Font.Size = 14;
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Font.Bold = true;
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Font.Color.SetColor(Color.Red);
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        cpcellRow++;

                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Value = sdocid[sodccount];
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Merge = true;
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Font.Size = 10;
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        cpcellRow++;

                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Value = "Calculated Parameter";
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(230, 184, 183));
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Merge = true;
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Font.Bold = true;
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Font.Color.SetColor(Color.Black);
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        cpcellRow++;

                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn].Value = "Item";
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn].Style.Font.Bold = true;
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn + 1].Value = "Value";
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn + 1].Style.Font.Bold = true;
                        exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        cpcellRow++;
                        for (int cprowcount = 0; cprowcount < dtcalculateParam.Rows.Count; cprowcount++)
                        {
                            for (int cpcolcount = 0; cpcolcount < dtcalculateParam.Columns.Count; cpcolcount++)
                            {
                                exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn].Value = dtcalculateParam.Rows[cprowcount]["Parameter"].ToString();
                                if (dtcalculateParam.Rows[cprowcount][sdocid[sodccount]].ToString() == "")
                                {
                                    exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn + 1].Value = dtcalculateParam.Rows[cprowcount][sdocid[sodccount]].ToString();
                                }
                                else
                                {
                                    exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn + 1].Value = Convert.ToDecimal(dtcalculateParam.Rows[cprowcount][sdocid[sodccount]].ToString());

                                }
                                exelCalcParamworksheet.Cells[cpcellRow, cpcellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                exelCalcParamworksheet.Column(cpcellColumn).Width = 50;
                                exelCalcParamworksheet.Column(cpcellColumn + 1).AutoFit();
                            }
                            cpcellRow++;
                        }
                        cpcellRow++;
                        ExcelPieChart pieChartCalcParam = exelCalcParamworksheet.Drawings.AddChart("CalculatedParameterPieChart" + sodccount, eChartType.Pie3D) as ExcelPieChart;
                        pieChartCalcParam.Title.Text = "Calculated Parameter";
                        pieChartCalcParam.Title.Font.Size = 11;
                        //ExcelRange yvalues = exelCalcParamworksheet.Cells["B6,B9"];
                        //ExcelRange xvalues = exelCalcParamworksheet.Cells["A7,A9"];
                        //pieChartCalcParam.Series.Add(yvalues, xvalues);

                        pieChartCalcParam.Series.Add(ExcelRange.GetAddress(7, cpcellColumn + 1, 9, cpcellColumn + 1), ExcelRange.GetAddress(7, cpcellColumn, 9, cpcellColumn));
                        pieChartCalcParam.Legend.Position = eLegendPosition.Bottom;
                        pieChartCalcParam.DataLabel.ShowPercent = true;
                        pieChartCalcParam.SetSize(400, 250);
                        pieChartCalcParam.SetPosition(cpcellRow, 0, cpcellColumn - 1, 0);

                        cpcellColumn = cpcellColumn + 2;

                    }
                }

                #endregion

                #region -- Total Cycle Time
               
                DataTable dttotalcycletimeParam = new DataTable();
                if (Session["CalcParamGraphData"] == null)
                {
                    List<TotalCycleTimeGrpah> ouput = DBAccess.cumiBindTotalCycleTime(txtSDocId1.Text, txtSDocId2.Text);
                    dttotalcycletimeParam = (DataTable)Session["TotalCycleTime"];
                    
                }
                else
                {
                    dttotalcycletimeParam = (DataTable)Session["TotalCycleTime"];
                }
                sdocid = new List<string>();

                if (dttotalcycletimeParam.Rows.Count > 0)
                {
                    var exelTotalCycleTimeworksheet = Excel.Workbook.Worksheets.Add("Total Cycle Time");
                    setWorkSheetSetting(exelTotalCycleTimeworksheet);
                    int tctcellRow = 1;
                    exelTotalCycleTimeworksheet.Row(1).Height = 30;
                    System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath(Utility.ReportImageLHS));
                    ExcelPicture pic = exelTotalCycleTimeworksheet.Drawings.AddPicture("ImgLHS", img);
                    pic.SetPosition(0, 0, 0, 0);
                    pic.SetSize(40, 40);
                    exelTotalCycleTimeworksheet.Protection.IsProtected = false;
                    exelTotalCycleTimeworksheet.Protection.AllowSelectLockedCells = false;
                    System.Drawing.Image imgrhs = System.Drawing.Image.FromFile(Server.MapPath(Utility.ReportImageRHS));
                    ExcelPicture picrhs = exelTotalCycleTimeworksheet.Drawings.AddPicture("ImgRHS", imgrhs);
                    picrhs.SetPosition(0, 0, 3, 30);
                    picrhs.SetSize(40, 40);
                    exelTotalCycleTimeworksheet.Protection.IsProtected = false;
                    exelTotalCycleTimeworksheet.Protection.AllowSelectLockedCells = false;

                    exelTotalCycleTimeworksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    exelTotalCycleTimeworksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    exelTotalCycleTimeworksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 4].Value = "Data Collection / Trial Wheel Documentation";
                    exelTotalCycleTimeworksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    exelTotalCycleTimeworksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 4].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(231, 231, 231));
                    exelTotalCycleTimeworksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 4].Merge = true;
                    exelTotalCycleTimeworksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 4].Style.Font.Size = 18;
                    exelTotalCycleTimeworksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 4].Style.Font.Bold = true;
                    //exelworksheet.Cells[cellRow, cellRow, cellRow, 4].Style.Border;
                    exelTotalCycleTimeworksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 4].Style.Font.Color.SetColor(Color.Red);
                    tctcellRow++;

                    int cpcellColumn = 1;
                    for (int i = 2; i < dttotalcycletimeParam.Columns.Count; i++)
                    {
                        sdocid.Add(dttotalcycletimeParam.Columns[i].ColumnName.ToString());
                    }
                    for (int sodccount = 0; sodccount < sdocid.Count; sodccount++)
                    {
                        int cpcellRow = 3;
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Value = sodccount == 0 ? Utility.ReportTableLHSHeader : Utility.ReportTableRHSHeader;
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(230, 184, 183));
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Merge = true;
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Font.Size = 14;
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Font.Bold = true;
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Font.Color.SetColor(Color.Red);
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        cpcellRow++;

                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Value = sdocid[sodccount];
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Merge = true;
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Font.Size = 10;
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        cpcellRow++;

                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Value = "Total Cycle Time";
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(230, 184, 183));
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Merge = true;
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Font.Bold = true;
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Font.Color.SetColor(Color.Black);
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        cpcellRow++;

                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn].Value = "Item";
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn].Style.Font.Bold = true;
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn + 1].Value = "Value";
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn + 1].Style.Font.Bold = true;
                        exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        cpcellRow++;
                        for (int cprowcount = 0; cprowcount < dttotalcycletimeParam.Rows.Count; cprowcount++)
                        {
                            for (int cpcolcount = 0; cpcolcount < dttotalcycletimeParam.Columns.Count; cpcolcount++)
                            {
                                exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn].Value = dttotalcycletimeParam.Rows[cprowcount]["Parameter"].ToString();
                                if (dttotalcycletimeParam.Rows[cprowcount][sdocid[sodccount]].ToString() == "")
                                {
                                    exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn + 1].Value = dttotalcycletimeParam.Rows[cprowcount][sdocid[sodccount]].ToString();
                                }
                                else
                                {
                                    exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn + 1].Value = Convert.ToDecimal(dttotalcycletimeParam.Rows[cprowcount][sdocid[sodccount]].ToString());

                                }
                                exelTotalCycleTimeworksheet.Cells[cpcellRow, cpcellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                exelTotalCycleTimeworksheet.Column(cpcellColumn).Width = 50;
                                exelTotalCycleTimeworksheet.Column(cpcellColumn + 1).AutoFit();
                            }
                            cpcellRow++;
                        }
                        cpcellRow++;
                        ExcelPieChart pieChartActualandGrinding = exelTotalCycleTimeworksheet.Drawings.AddChart("TotalCycleTimePieChart" + sodccount, eChartType.Pie3D) as ExcelPieChart;
                        pieChartActualandGrinding.Title.Text = "Total Cycle Time";
                        pieChartActualandGrinding.Title.Font.Size = 11;
                        if (sodccount == 0)
                        {
                            ExcelRange yvalues = exelTotalCycleTimeworksheet.Cells["B7,B10"];
                            ExcelRange xvalues = exelTotalCycleTimeworksheet.Cells["A7,A10"];
                            pieChartActualandGrinding.Series.Add(yvalues, xvalues);
                        }
                        else
                        {
                            ExcelRange yvalues = exelTotalCycleTimeworksheet.Cells["D7,D10"];
                            ExcelRange xvalues = exelTotalCycleTimeworksheet.Cells["c7,C10"];
                            pieChartActualandGrinding.Series.Add(yvalues, xvalues);
                        }
                        //pieChartActualandGrinding.Series.Add(ExcelRange.GetAddress(7, cpcellColumn + 1, 9, cpcellColumn + 1), ExcelRange.GetAddress(7, cpcellColumn, 9, cpcellColumn));
                        pieChartActualandGrinding.Legend.Position = eLegendPosition.Bottom;
                        pieChartActualandGrinding.DataLabel.ShowPercent = true;
                        pieChartActualandGrinding.SetSize(400, 250);
                        pieChartActualandGrinding.SetPosition(cpcellRow, 0, cpcellColumn - 1, 0);

                        cpcellRow = cpcellRow + 14;
                        ExcelPieChart pieChartGrindandNon = exelTotalCycleTimeworksheet.Drawings.AddChart("GrindingAndNongrindingTimePieChart" + sodccount, eChartType.Pie3D) as ExcelPieChart;
                        pieChartGrindandNon.Title.Text = "Total Cycle Time";
                        pieChartGrindandNon.Title.Font.Size = 11;
                        if (sodccount == 0)
                        {
                            ExcelRange yvalues = exelTotalCycleTimeworksheet.Cells["B7,B8"];
                            ExcelRange xvalues = exelTotalCycleTimeworksheet.Cells["A7,A8"];
                            pieChartGrindandNon.Series.Add(yvalues, xvalues);
                        }
                        else
                        {
                            ExcelRange yvalues = exelTotalCycleTimeworksheet.Cells["D7,D8"];
                            ExcelRange xvalues = exelTotalCycleTimeworksheet.Cells["c7,C8"];
                            pieChartGrindandNon.Series.Add(yvalues, xvalues);
                        }
                        //pieChartActualandGrinding.Series.Add(ExcelRange.GetAddress(7, cpcellColumn + 1, 9, cpcellColumn + 1), ExcelRange.GetAddress(7, cpcellColumn, 9, cpcellColumn));
                        pieChartGrindandNon.Legend.Position = eLegendPosition.Bottom;
                        pieChartGrindandNon.DataLabel.ShowPercent = true;
                        pieChartGrindandNon.SetSize(400, 250);
                        pieChartGrindandNon.SetPosition(cpcellRow, 0, cpcellColumn - 1, 0);

                        cpcellColumn = cpcellColumn + 2;

                    }
                }

                #endregion


                #region -----Calculated Time-----
              

                DataTable dtcalctimeParam = new DataTable();
                if (Session["GrindingTime"] == null)
                {
                    List<TotalCycleTimeGrpah> ouput = DBAccess.cumiBindGrindingTime(txtSDocId1.Text, txtSDocId2.Text);
                    dtcalctimeParam = (DataTable)Session["GrindingTime"];
                   
                }
                else
                {
                    dtcalctimeParam = (DataTable)Session["GrindingTime"];
                }
                sdocid = new List<string>();

                if (dtcalctimeParam.Rows.Count > 0)
                {
                    var exelCalculatedTimeworksheetrksheet = Excel.Workbook.Worksheets.Add("Calculated Time");
                    setWorkSheetSetting(exelCalculatedTimeworksheetrksheet);
                    int tctcellRow = 1;
                    exelCalculatedTimeworksheetrksheet.Row(1).Height = 30;
                    System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath(Utility.ReportImageLHS));
                    ExcelPicture pic = exelCalculatedTimeworksheetrksheet.Drawings.AddPicture("ImgLHS", img);
                    pic.SetPosition(0, 0, 0, 0);
                    pic.SetSize(40, 40);
                    exelCalculatedTimeworksheetrksheet.Protection.IsProtected = false;
                    exelCalculatedTimeworksheetrksheet.Protection.AllowSelectLockedCells = false;
                    System.Drawing.Image imgrhs = System.Drawing.Image.FromFile(Server.MapPath(Utility.ReportImageRHS));
                    ExcelPicture picrhs = exelCalculatedTimeworksheetrksheet.Drawings.AddPicture("ImgRHS", imgrhs);
                    picrhs.SetPosition(0, 0, 3, 30);
                    picrhs.SetSize(40, 40);
                    exelCalculatedTimeworksheetrksheet.Protection.IsProtected = false;
                    exelCalculatedTimeworksheetrksheet.Protection.AllowSelectLockedCells = false;

                    exelCalculatedTimeworksheetrksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 4].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    exelCalculatedTimeworksheetrksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    exelCalculatedTimeworksheetrksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 4].Value = "Data Collection / Trial Wheel Documentation";
                    exelCalculatedTimeworksheetrksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    exelCalculatedTimeworksheetrksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 4].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(231, 231, 231));
                    exelCalculatedTimeworksheetrksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 4].Merge = true;
                    exelCalculatedTimeworksheetrksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 4].Style.Font.Size = 18;
                    exelCalculatedTimeworksheetrksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 4].Style.Font.Bold = true;
                    //exelworksheet.Cells[cellRow, cellRow, cellRow, 4].Style.Border;
                    exelCalculatedTimeworksheetrksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 4].Style.Font.Color.SetColor(Color.Red);
                    tctcellRow++;
                    int cpcellColumn = 1;
                    for (int i = 2; i < dtcalctimeParam.Columns.Count; i++)
                    {
                        sdocid.Add(dtcalctimeParam.Columns[i].ColumnName.ToString());
                    }
                    for (int sodccount = 0; sodccount < sdocid.Count; sodccount++)
                    {
                       int cpcellRow = 3;
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Value = sodccount == 0 ? Utility.ReportTableLHSHeader : Utility.ReportTableRHSHeader;
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(230, 184, 183));
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Merge = true;
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Font.Size = 14;
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Font.Bold = true;
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Font.Color.SetColor(Color.Red);
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        cpcellRow++;

                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Value = sdocid[sodccount];
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Merge = true;
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Font.Size = 10;
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        cpcellRow++;

                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Value = "Calculated Time";
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(230, 184, 183));
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Merge = true;
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Font.Bold = true;
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Font.Color.SetColor(Color.Black);
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        cpcellRow++;

                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn].Value = "Item";
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn].Style.Font.Bold = true;
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn + 1].Value = "Value";
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn + 1].Style.Font.Bold = true;
                        exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        cpcellRow++;
                        for (int cprowcount = 0; cprowcount < dtcalctimeParam.Rows.Count; cprowcount++)
                        {
                            for (int cpcolcount = 0; cpcolcount < dtcalctimeParam.Columns.Count; cpcolcount++)
                            {
                                exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn].Value = dtcalctimeParam.Rows[cprowcount]["Parameter"].ToString();
                                if (dtcalctimeParam.Rows[cprowcount][sdocid[sodccount]].ToString() == "")
                                {
                                    exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn + 1].Value = dtcalctimeParam.Rows[cprowcount][sdocid[sodccount]].ToString();
                                }
                                else
                                {
                                    exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn + 1].Value = Convert.ToDecimal(dtcalctimeParam.Rows[cprowcount][sdocid[sodccount]].ToString());

                                }
                                exelCalculatedTimeworksheetrksheet.Cells[cpcellRow, cpcellColumn + 1].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                exelCalculatedTimeworksheetrksheet.Column(cpcellColumn).Width = 50;
                                exelCalculatedTimeworksheetrksheet.Column(cpcellColumn + 1).AutoFit();
                            }
                            cpcellRow++;
                        }
                        cpcellRow++;
                        ExcelPieChart pieChartCalculatedTime = exelCalculatedTimeworksheetrksheet.Drawings.AddChart("CalculatedTimepiechart" + sodccount, eChartType.Pie3D) as ExcelPieChart;
                        pieChartCalculatedTime.Title.Text = "";
                        pieChartCalculatedTime.Title.Font.Size = 11;
                        pieChartCalculatedTime.Series.Add(ExcelRange.GetAddress(7, cpcellColumn + 1, 12, cpcellColumn + 1), ExcelRange.GetAddress(7, cpcellColumn, 12, cpcellColumn));
                        pieChartCalculatedTime.Legend.Position = eLegendPosition.Bottom;
                        pieChartCalculatedTime.DataLabel.ShowPercent = true;
                        pieChartCalculatedTime.SetSize(400, 250);
                        pieChartCalculatedTime.SetPosition(cpcellRow, 0, cpcellColumn - 1, 0);

                        cpcellColumn = cpcellColumn + 2;

                    }
                }
                #endregion


                #region ----Images----
               

                List<SdocImages> listImage = new List<SdocImages>();
                if (Session["CUMISdocImages"] == null)
                {
                    listImage = DBAccess.cumiBindImages(txtSDocId1.Text, txtSDocId2.Text);
                    Session["CUMISdocImages"] = listImage;
                   
                }
                else
                {
                    listImage = (List<SdocImages>)Session["CUMISdocImages"];
                }
                if (listImage.Count > 0)
                {
                    var exelImageworksheet = Excel.Workbook.Worksheets.Add("Images");
                    setWorkSheetSetting(exelImageworksheet);
                    int tctcellRow = 1;
                    exelImageworksheet.Row(1).Height = 30;
                    System.Drawing.Image imglhs = System.Drawing.Image.FromFile(Server.MapPath(Utility.ReportImageLHS));
                    ExcelPicture piclhs = exelImageworksheet.Drawings.AddPicture("ImgLHS", imglhs);
                    piclhs.SetPosition(0, 0, 0, 0);
                    piclhs.SetSize(40, 40);
                    exelImageworksheet.Protection.IsProtected = false;
                    exelImageworksheet.Protection.AllowSelectLockedCells = false;
                    System.Drawing.Image imgrhs = System.Drawing.Image.FromFile(Server.MapPath(Utility.ReportImageRHS));
                    ExcelPicture picrhs = exelImageworksheet.Drawings.AddPicture("ImgRHS", imgrhs);
                    picrhs.SetPosition(0, 0, 11, 30);
                    picrhs.SetSize(40, 40);
                    exelImageworksheet.Protection.IsProtected = false;
                    exelImageworksheet.Protection.AllowSelectLockedCells = false;

                    exelImageworksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 12].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    exelImageworksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    exelImageworksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 12].Value = "Data Collection / Trial Wheel Documentation";
                    exelImageworksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    exelImageworksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 12].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(231, 231, 231));
                    exelImageworksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 12].Merge = true;
                    exelImageworksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 12].Style.Font.Size = 18;
                    exelImageworksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 12].Style.Font.Bold = true;
                    //exelworksheet.Cells[cellRow, cellRow, cellRow, 4].Style.Border;
                    exelImageworksheet.Cells[tctcellRow, tctcellRow, tctcellRow, 12].Style.Font.Color.SetColor(Color.Red);
                    tctcellRow++;
                    int cellRowImages = 3;
                    try
                    {
                        int k = 0;
                        int cpcellColumn = 1;
                        foreach (SdocImages data in listImage)
                        {
                           int cpcellRow = 3;
                            exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Value = k == 0 ? Utility.ReportTableLHSHeader : Utility.ReportTableRHSHeader;
                            exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(230, 184, 183));
                            exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Merge = true;
                            exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Style.Font.Size = 14;
                            exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Style.Font.Bold = true;
                            exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Style.Font.Color.SetColor(Color.Red);
                            //exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            cpcellRow++;

                            exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Value = data.SdocName;
                            exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Merge = true;
                            exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Style.Font.Size = 10;
                            //exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                            cpcellRow++;

                            for (int j = 0; j < data.Values.Count; j++)
                            {
                                try
                                {
                                    exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Value = data.Values[j].wpImageName;
                                    exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                                    exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Merge = true;
                                    //  exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;

                                    cpcellRow++;
                                    string path = data.Values[j].wpImagePath;
                                    System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath(path));
                                    ExcelPicture pic = exelImageworksheet.Drawings.AddPicture(data.Values[j].wpImageName + "_" + k + j, img);
                                    pic.SetPosition(cpcellRow, 0, cpcellColumn, 0);
                                    pic.SetSize(200, 200);
                                    exelImageworksheet.Protection.IsProtected = false;
                                    exelImageworksheet.Protection.AllowSelectLockedCells = false;
                                    cpcellRow = cpcellRow + 12;
                                    //exelImageworksheet.Cells[cpcellRow, cpcellColumn, cpcellRow, cpcellColumn + 5].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                                }
                                catch (Exception e1)
                                {
                                }
                            }
                            cpcellColumn = cpcellColumn + 6;
                            k++;
                        }

                    }
                    catch (Exception ex)
                    {

                    }

                }

                #endregion
                DownloadFile(destination, Excel.GetAsByteArray());
            }
            catch (Exception ex)
            {

            }

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
    }
}