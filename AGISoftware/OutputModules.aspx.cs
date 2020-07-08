using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AGISoftware.DataBaseAccess;
using AGISoftware.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using OfficeOpenXml.Drawing.Chart;
using System.IO;
using System.Text;
using System.Data;
using OfficeOpenXml.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Threading;

namespace AGISoftware
{
    public partial class OutputModules : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["GeneralInfoParameters"] = null;
                Session["QualityParameters"] = null;
                bindCheckListParameter();
                bindQualityParameterCheckListParameter();
                bindSdonName();
                if (Session["ATKSDocID"] != null)
                {
                    //bindGeneralInfo(Session["ATKSDocID"].ToString());
                    //bindQualityParam(Session["ATKSDocID"].ToString());
                    //bindGrindingTime(Session["ATKSDocID"].ToString());
                    //bindCalculatedParam(Session["ATKSDocID"].ToString());
                    //bindCheckListParameter();
                    //bindQualityParameterCheckListParameter();

                }
               
            }
            if (Session["EmpName"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
        }
        private void bindSdonName()
        {

            ddlSdocID.DataSource = DBAccess.getOMSdocname();
            ddlSdocID.DataBind();
            if (Session["ATKSDocID"] != null)
            {
                string s = Session["ATKSDocID"].ToString();
                string[] SdocPlungCat = Session["ATKSDocID"].ToString().Trim().Split('_');
                ddlSdocID.SelectedValue = SdocPlungCat[0].Trim().Remove(0, 4);
                ddlChkPlunges.DataSource = DBAccess.getOMPlunges(ddlSdocID.SelectedItem.Text);                ddlChkPlunges.DataBind();                ddlChkPlunges.SelectedValue = SdocPlungCat[1];                ddlChkSubCatogery.DataSource = DBAccess.getOMSubCategoryBasedOnPlunges(ddlSdocID.SelectedItem.Text, ddlChkPlunges.SelectedItem.Value);                ddlChkSubCatogery.DataBind();                ddlChkSubCatogery.SelectedValue = SdocPlungCat[2];            }            else            {
                try
                {
                  
                    ddlChkPlunges.DataSource = DBAccess.getOMPlunges(ddlSdocID.SelectedItem.Text);
                    ddlChkPlunges.DataBind();
                    if(ddlChkPlunges.Items.Count>0)
                    {
                        ddlChkPlunges.SelectedIndex = 0;
                    }
                    ddlChkSubCatogery.DataSource = DBAccess.getOMSubCategoryBasedOnPlunges(ddlSdocID.SelectedItem.Text, ddlChkPlunges.SelectedItem.Value);
                    ddlChkSubCatogery.DataBind();
                    if(ddlChkSubCatogery.Items.Count>0)
                    {
                        ddlChkSubCatogery.SelectedIndex = 0;
                    }
                }
                catch (Exception e)
                { }
            }
            bindGeneralInfo(setSdocPlungeCategory(), "");
            bindQualityParameter(setSdocPlungeCategory(), "");
            bindDerivedParameters(setSdocPlungeCategory(), "");
            bindInferenceSignal(setSdocPlungeCategory());
            bindImages(setSdocPlungeCategory());

        }

        private SdocPlungCat setSdocPlungeCategory()
        {
            SdocPlungCat name = new SdocPlungCat();
            if (ddlSdocID.SelectedItem.Text != "")
            {
                name.Sdocname = ddlSdocID.SelectedItem.Text;
            }

            List<string> listPlunge = new List<string>();
            foreach (System.Web.UI.WebControls.ListItem data in ddlChkPlunges.Items)
            {
                if (data.Selected)
                {
                    listPlunge.Add(data.Text);
                }
            }
            StringBuilder strPlunge = new StringBuilder();
            strPlunge.Append("");
            for (int i = 0; i < listPlunge.Count; i++)
            {
                if (i == listPlunge.Count - 1)
                {
                    strPlunge.Append("'" + listPlunge[i] + "'");
                }
                else
                {
                    strPlunge.Append("'" + listPlunge[i] + "',");
                }
            }
            name.Plunge = strPlunge.ToString();

            List<string> listCat = new List<string>();
            foreach (System.Web.UI.WebControls.ListItem data in ddlChkSubCatogery.Items)
            {
                if (data.Selected)
                {
                    listCat.Add(data.Text);
                }
            }
            StringBuilder strCat = new StringBuilder();
            strCat.Append("");
            for (int i = 0; i < listCat.Count; i++)
            {
                if (i == listCat.Count - 1)
                {
                    strCat.Append("'" + listCat[i] + "'");
                }
                else
                {
                    strCat.Append("'" + listCat[i] + "',");
                }
            }
            name.Category = strCat.ToString();
            return name;
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

        private void bindGeneralInfo(SdocPlungCat sdocid, string Parameter)
        {
            DataTable dt;
            if (Session["GeneralInfoParameters"] != null)
            {
                 dt = DBAccess.omBindGeneralInfo(sdocid, Session["GeneralInfoParameters"].ToString(), "GeneralParmNonDefault");
            }
            else
            {
                 dt = DBAccess.omBindGeneralInfo(sdocid, Parameter, "GeneralParm");
            }
           
            gvGeneralInfo.DataSource = removeBlankValuesFromGeneralInfoGrid(dt);
            gvGeneralInfo.DataBind();
            checkParameterToPanelList();
            List<string> Sdocid = new List<string>();
            if(gvGeneralInfo.Rows.Count>0)
            {
                for (int i = 3; i < gvGeneralInfo.Rows[0].Cells.Count - 3; i++)
                {
                    string sdoc = gvGeneralInfo.HeaderRow.Cells[i].Text;
                    Sdocid.Add(sdoc);
                }
                dcSdoclist.DataSource = Sdocid;
                dcSdoclist.DataBind();
                foreach (System.Web.UI.WebControls.ListItem item in dcSdoclist.Items)
                {
                    item.Selected = true;
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "btnSDocGraphClick();", true);
            }
        }
        private DataTable removeBlankValuesFromGeneralInfoGrid(DataTable dt)
        {
            DataTable newdt = new DataTable();
            newdt = dt.Clone();
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow drtableOld in dt.Rows)
                    {
                        bool blankvalue = false;
                        for (int j = 3; j < (dt.Columns.Count - 3); j++)
                        {
                            if ((drtableOld[j].ToString() == "" || drtableOld[j].ToString() == "&nbsp;" || drtableOld[j].ToString() == null))
                            {
                                blankvalue = true;
                            }
                        }
                        if (!blankvalue)
                        {
                            //newdt.Rows.Remove(dt.Rows[i]);
                            newdt.ImportRow(drtableOld);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return newdt;
        }
        private void bindDerivedParameters(SdocPlungCat sdocid, string Parameter)
        {
            List<OMDerivedParameter> listoMDerivedParameters = new List<OMDerivedParameter>();
            OMDerivedParameter omderivedParameter=DBAccess.omBindDerivedParameter(sdocid, Parameter, "DerivedParameter");
            if(omderivedParameter!=null)
            { 
            var SdocId = omderivedParameter.derivedInputParameters.Select(s => s.SDocId).Distinct().ToList();
            OMDerivedParameter finalomderivedParameter = null;
            for (int i=0;i<SdocId.Count;i++)
            {
                finalomderivedParameter = new OMDerivedParameter();
                finalomderivedParameter.SDocName = SdocId[i];
          

                List<DerivedInputParameter> listderivedInputParameters = new List<DerivedInputParameter>();
                DerivedInputParameter derivedInputParameter = null;
                for (int j = 0; j < omderivedParameter.derivedInputParameters.Count; j++)
                {
                        if (SdocId[i] == omderivedParameter.derivedInputParameters[j].SDocId)
                        {
                            derivedInputParameter = new DerivedInputParameter();
                            derivedInputParameter.Parameter = omderivedParameter.derivedInputParameters[j].Parameter;
                            derivedInputParameter.Diameter = omderivedParameter.derivedInputParameters[j].Diameter;
                            derivedInputParameter.DOC = omderivedParameter.derivedInputParameters[j].DOC;
                            derivedInputParameter.StockonFace = omderivedParameter.derivedInputParameters[j].StockonFace;
                            derivedInputParameter.InFeed = omderivedParameter.derivedInputParameters[j].InFeed;
                           // derivedInputParameter.WorkSpeed = omderivedParameter.derivedInputParameters[j].WorkSpeed;
                            derivedInputParameter.ODWidth = omderivedParameter.derivedInputParameters[j].ODWidth;
                            derivedInputParameter.FeedAngle = omderivedParameter.derivedInputParameters[j].FeedAngle;
                            derivedInputParameter.WorkSpeedOD = omderivedParameter.derivedInputParameters[j].WorkSpeedOD;
                            derivedInputParameter.WorkSpeedFace = omderivedParameter.derivedInputParameters[j].WorkSpeedFace;
                            derivedInputParameter.XFeed = omderivedParameter.derivedInputParameters[j].XFeed;
                            derivedInputParameter.ZFeed = omderivedParameter.derivedInputParameters[j].ZFeed;
                            derivedInputParameter.TangoColor = omderivedParameter.derivedInputParameters[j].TangoColor;
                            derivedInputParameter.TangoFlagOD = omderivedParameter.derivedInputParameters[j].TangoFlagOD;
                            derivedInputParameter.TangoFlagFace = omderivedParameter.derivedInputParameters[j].TangoFlagFace;
                            listderivedInputParameters.Add(derivedInputParameter);
                        }
                }
                finalomderivedParameter.derivedInputParameters = listderivedInputParameters;

                for (int j = 0; j < omderivedParameter.derivedInputParametersOutput.Count; j++)
                {
                        if (SdocId[i] == omderivedParameter.derivedInputParametersOutput[j].SDocId)
                        {
                            finalomderivedParameter.Sparkouttime = omderivedParameter.derivedInputParametersOutput[j].Sparkouttime;
                            finalomderivedParameter.Targetrelieftime = omderivedParameter.derivedInputParametersOutput[j].Targetrelieftime;
                            finalomderivedParameter.TraverseSpeed = omderivedParameter.derivedInputParametersOutput[j].TraverseSpeed;
                            finalomderivedParameter.SlideForward = omderivedParameter.derivedInputParametersOutput[j].SlideForward;
                            finalomderivedParameter.ProgramRead = omderivedParameter.derivedInputParametersOutput[j].ProgramRead;
                            finalomderivedParameter.Flagging = omderivedParameter.derivedInputParametersOutput[j].Flagging;
                            finalomderivedParameter.SlideReturn = omderivedParameter.derivedInputParametersOutput[j].SlideReturn;
                            finalomderivedParameter.Others = omderivedParameter.derivedInputParametersOutput[j].Others;
                            finalomderivedParameter.LoadUnloadTime = omderivedParameter.derivedInputParametersOutput[j].LoadUnloadTime;
                            finalomderivedParameter.Chipwidthratio = omderivedParameter.derivedInputParametersOutput[j].Chipwidthratio;
                            finalomderivedParameter.Wheeltiltangle = omderivedParameter.derivedInputParametersOutput[j].Wheeltiltangle;
                            finalomderivedParameter.ManualLoadingUnloading= omderivedParameter.derivedInputParametersOutput[j].ManualLoadingUnloading;
                            finalomderivedParameter.remarks = omderivedParameter.derivedInputParametersOutput[j].remarks;
                        }

                }

                    List<DerivedCalculateParameter> listderivedCalculateParameters = new List<DerivedCalculateParameter>();
                DerivedCalculateParameter derivedCalculateParameter = null;
                for (int j = 0; j < omderivedParameter.derivedcalculatedtParameters.Count; j++)
                {
                        if (SdocId[i] == omderivedParameter.derivedcalculatedtParameters[j].SDocId)
                        {
                            derivedCalculateParameter = new DerivedCalculateParameter();
                            derivedCalculateParameter.Parameter = omderivedParameter.derivedcalculatedtParameters[j].Parameter;
                            derivedCalculateParameter.RadialDOCX = omderivedParameter.derivedcalculatedtParameters[j].RadialDOCX;
                            derivedCalculateParameter.RadialDOCZ = omderivedParameter.derivedcalculatedtParameters[j].RadialDOCZ;
                            derivedCalculateParameter.MRRX = omderivedParameter.derivedcalculatedtParameters[j].MRRX;
                            derivedCalculateParameter.MRRZ = omderivedParameter.derivedcalculatedtParameters[j].MRRZ;
                            derivedCalculateParameter.TotalMRRX = omderivedParameter.derivedcalculatedtParameters[j].TotalMRRX;
                            derivedCalculateParameter.ToralMRR = omderivedParameter.derivedcalculatedtParameters[j].ToralMRR;
                            derivedCalculateParameter.GritPenetrationDepthX = omderivedParameter.derivedcalculatedtParameters[j].GritPenetrationDepthX;
                            derivedCalculateParameter.GritPenetrationDepthZ = omderivedParameter.derivedcalculatedtParameters[j].GritPenetrationDepthZ;
                            derivedCalculateParameter.Time = omderivedParameter.derivedcalculatedtParameters[j].Time;
                            derivedCalculateParameter.WorkRPMRatio = omderivedParameter.derivedcalculatedtParameters[j].WorkRPMRatio;
                            derivedCalculateParameter.WorkSpeedRatio = omderivedParameter.derivedcalculatedtParameters[j].WorkSpeedRatio;
                            derivedCalculateParameter.TangoFlagOD = omderivedParameter.derivedcalculatedtParameters[j].TangoFlagOD;
                            derivedCalculateParameter.TangoFlagFace = omderivedParameter.derivedcalculatedtParameters[j].TangoFlagFace;
                            derivedCalculateParameter.TangoColor = omderivedParameter.derivedcalculatedtParameters[j].TangoColor;
                            listderivedCalculateParameters.Add(derivedCalculateParameter);
                        }
                }
                finalomderivedParameter.derivedcalculatedtParameters = listderivedCalculateParameters;

              
                for (int j = 0; j < omderivedParameter.derivedCalculateParametersOutput.Count; j++)
                {
                   
                        if (SdocId[i] == omderivedParameter.derivedCalculateParametersOutput[j].SDocId)
                        {
                            finalomderivedParameter.EquivalentDia = omderivedParameter.derivedCalculateParametersOutput[j].EquivalentDia;
                            finalomderivedParameter.GrindingCycletime = omderivedParameter.derivedCalculateParametersOutput[j].GrindingCycletime;
                            finalomderivedParameter.NongrindingCycleTime = omderivedParameter.derivedCalculateParametersOutput[j].NongrindingCycleTime;
                            finalomderivedParameter.TotalGrindingTime = omderivedParameter.derivedCalculateParametersOutput[j].TotalGrindingTime;
                        finalomderivedParameter.EquivalentDiaFace = omderivedParameter.derivedCalculateParametersOutput[j].EquivalentDiaFace;
                        finalomderivedParameter.CuttingEdgeDensity = omderivedParameter.derivedCalculateParametersOutput[j].CuttingEdgeDensity;
                            finalomderivedParameter.TotalCycletime = omderivedParameter.derivedCalculateParametersOutput[j].TotalCycletime;
                            finalomderivedParameter.FloorToFloor = omderivedParameter.derivedCalculateParametersOutput[j].FloorToFloor;
                            finalomderivedParameter.SparkOutRevolutions = omderivedParameter.derivedCalculateParametersOutput[j].SparkOutRevolutions;
                        }
                }

                listoMDerivedParameters.Add(finalomderivedParameter);
            }

            }

            lvderivedParameter.DataSource = listoMDerivedParameters;
            lvderivedParameter.DataBind();

        }
        private void checkParameterToPanelList()
        {
            try
            {

                string s1 = chkParameter.Items[0].Text;
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
            catch (Exception ex)
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
                                totalchecked++;
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
        private void bindQualityParameter(SdocPlungCat sdocid, string Parameter)
        {
            List<QualityParam> listQuality;
            if (Session["QualityParameters"] != null)
            {
                listQuality = DBAccess.omBindQualityParam(sdocid, Session["QualityParameters"].ToString(), "QualityParmNonDefault");
            }
            else
            {
               listQuality = DBAccess.omBindQualityParam(sdocid, Parameter, "QualityParm");
            }
            
            lvQualityParam.DataSource = removeBlankValuesFromQualityGrid(listQuality);
            lvQualityParam.DataBind();
            checkQualityParameterToPanelList();
        }
        private List<QualityParam> removeBlankValuesFromQualityGrid(List<QualityParam> qualityParams)
        {
            List<QualityParam> newListQualityParam = new List<QualityParam>();
            QualityParam newqualityParam = null;
            try
            {
                bool[] blankrowno = new bool[4];
                bool[] blankrowno1 = new bool[4];
                List<string> blankParameterName = new List<string>();
                List<string> nonblankParameterName = new List<string>();
                int k = 0, n = 0;
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
        private void bindInferenceSignal(SdocPlungCat sdocid)        {            gvInferenceSignal.DataSource = DBAccess.omBindInferenceSignal(sdocid);            gvInferenceSignal.DataBind();        }
        private void bindImages(SdocPlungCat sdocid)
        {
            lvImages.DataSource = DBAccess.omBindImages(sdocid);
            lvImages.DataBind();
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<string> getGrindingTime(string Sdoc, string Plunge, string SubCat)
        {
            List<string> ouput = DBAccess.omBindGrindingTime(Sdoc, Plunge, SubCat);
            HttpContext.Current.Session["GrindingTime"] = ouput;
            return ouput;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<string> getNonGrindingTime(string Sdoc, string Plunge, string SubCat)
        {
            List<string>  ouput = DBAccess.omBindNonGrindingTime(Sdoc, Plunge, SubCat);
            HttpContext.Current.Session["NonGrindingTime"] = ouput;
            return ouput;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<string> getTotalCycleTime(string Sdoc, string Plunge, string SubCat)
        {
            List<string> ouput = DBAccess.omBindTotalCycleTime(Sdoc, Plunge, SubCat);
            HttpContext.Current.Session["TotalCycleTime"] = ouput;
            return ouput;
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static DrillChart getDrilldownTimeData(string Sdoc, string Plunge, string SubCat)
        {
            DrillChart drillChart = new DrillChart();
          
            try
            {


                List<ChartSeries> listChartSeries = new List<ChartSeries>();
                ChartSeries chartSeries = null;
                List<string> ouput = DBAccess.omBindTotalCycleTime(Sdoc, Plunge, SubCat);
                string[] SDoctotalTimeDataColor = { "#000090", "#8fff6f"};
                int j = 0;
                for (int i = 0; i < ouput.Count; i = i + 2)
                {
                    if (ouput[i] == "Grinding Time (sec)" || ouput[i] == "Non Grinding Time (sec)")
                    {
                        chartSeries = new ChartSeries();
                        chartSeries.name = ouput[i];
                        chartSeries.y = ouput[i + 1]=="" || ouput[i+1]==null? 0: Convert.ToDecimal(ouput[i+1]);
                        chartSeries.drilldown = ouput[i];
                        chartSeries.color= SDoctotalTimeDataColor[j];
                        listChartSeries.Add(chartSeries);
                        j++;
                    }
                }
                drillChart.listChartSeries = listChartSeries;

                //get grinding data
                List<DrildownSeries> listDrillownSeries = new List<DrildownSeries>();
                DrildownSeries drildownSeries = null;

                string[] SDocgrindTimeDataColor = { "#000090", "#8fff6f", "#800001", "#e67312", "#1c1c21", "#056aad" };
                List<string> grinding = DBAccess.omBindGrindingTime(Sdoc, Plunge, SubCat);
                drildownSeries = new DrildownSeries();
                drildownSeries.name = "Grinding Time (sec)";
                drildownSeries.id = "Grinding Time (sec)";
                List<DrildownData> listDrilldownData = new List<DrildownData>();
                DrildownData drildownData = null;
                j = 0;
                for (int i = 0; i < grinding.Count; i = i + 2)
                {
                    drildownData = new DrildownData();
                    drildownData.name = grinding[i];
                    drildownData.y = grinding[i + 1] == null || grinding[i + 1] == "" ? 0 : Convert.ToDecimal(grinding[i + 1]);
                    drildownData.color = SDocgrindTimeDataColor[j];
                    listDrilldownData.Add(drildownData);
                    j++;
                }
                drildownSeries.data = listDrilldownData;
                listDrillownSeries.Add(drildownSeries);

                //get Nongrinding data
                string[] SDocnongrindTimeDataColor = { "#000090", "#8fff6f", "#800001", "#e67312", "#1c1c21", "#056aad" };
                List<string> nongrinding = DBAccess.omBindNonGrindingTime(Sdoc, Plunge, SubCat);
                drildownSeries = new DrildownSeries();
                drildownSeries.name = "Non Grinding Time (sec)";
                drildownSeries.id = "Non Grinding Time (sec)";
                listDrilldownData = new List<DrildownData>();
                drildownData = null;
                j = 0;
                for (int i = 0; i < nongrinding.Count; i = i + 2)
                {
                    drildownData = new DrildownData();
                    drildownData.name = nongrinding[i];
                    drildownData.y = nongrinding[i + 1] == null || nongrinding[i + 1] == "" ? 0 : Convert.ToDecimal(nongrinding[i + 1]);
                    drildownData.color = SDocnongrindTimeDataColor[j];
                    listDrilldownData.Add(drildownData);
                    j++;
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

        [System.Web.Services.WebMethod(EnableSession = true)]
        public static List<string> getCalculatedGraphData(string Sdoc, string Plunge, string SubCat)
        {
            List<string> ouput = DBAccess.omBindCalculatedParam(Sdoc, Plunge, SubCat);
            return ouput;
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
      

        protected void exportBtn_Click(object sender, EventArgs e)
        {
            SdocPlungCat sdocPlungeCat = new SdocPlungCat();
            sdocPlungeCat = setSdocPlungeCategory();
            List<string> listGIParam = new List<string>();
            foreach (GridViewRow row in gvGeneralInfo.Rows)
            {
                listGIParam.Add(row.Cells[0].Text);
            }
            List<CustomColumn> listGICustomParam = new List<CustomColumn>();
            listGICustomParam = DBAccess.getOMGeneralInfoParamForFilter();
            int gicount = 0;
            StringBuilder giSelectedParam = new StringBuilder();
            giSelectedParam.Append("");
            for (int i = 0; i < listGIParam.Count; i++)
            {
                for (int j = 0; j < listGICustomParam.Count; j++)
                {
                    if (listGIParam[i] == listGICustomParam[j].CustomName)
                    {
                        if (gicount == 0)
                        {
                            giSelectedParam.Append("'" + listGICustomParam[j].ColumnName + "'");
                            gicount++;
                        }
                        else
                        {
                            giSelectedParam.Append(",'" + listGICustomParam[j].ColumnName + "'");
                        }
                    }
                }
            }

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
            string Filename = "OutputModule.xlsx";

            string Source = string.Empty;
            Source = GetReportPath(Filename);
            string Template = string.Empty;
            Template = Sdocid+"_" + DateTime.Now + ".xlsx";
            string destination = string.Empty;
            destination = Path.Combine(appPath, "Temp", SafeFileName(Template));
            if (!File.Exists(Source))
            {
                Logger.WriteDebugLog("Output Module Report- \n " + Source);
            }

            FileInfo newFile = new FileInfo(Source);
            ExcelPackage Excel = new ExcelPackage(newFile, true);
            Excel.Workbook.Worksheets.Delete("Sheet1");

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
                        dr[gvGeneralInfo.HeaderRow.Cells[j].Text] = row.Cells[j].Text== "&nbsp;"? "" : row.Cells[j].Text;
                    }
                    dtGenearalInfo.Rows.Add(dr);
                }

                var exelworksheet = Excel.Workbook.Worksheets.Add("General Informations");
                setWorkSheetSetting(exelworksheet);
                int cellRow = 1;
                exelworksheet.Cells[cellRow, cellRow, cellRow, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //Color backcolor = Color.FromArgb(224, 76, 76);
                exelworksheet.Cells[cellRow, cellRow, cellRow, 8].Value = "Automation of Grinding Process Intelligence (AGI)";
                exelworksheet.Cells[cellRow, cellRow, cellRow, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                exelworksheet.Cells[cellRow, cellRow, cellRow, 8].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(231, 231, 231));
                exelworksheet.Cells[cellRow, cellRow, cellRow, 8].Merge = true;
                exelworksheet.Cells[cellRow, cellRow, cellRow, 8].Style.Font.Size = 18;
                exelworksheet.Cells[cellRow, cellRow, cellRow, 8].Style.Font.Bold = true;
                exelworksheet.Cells[cellRow, cellRow, cellRow, 8].Style.Font.Color.SetColor(Color.Red);


                cellRow = cellRow + 1;
                exelworksheet.Cells[cellRow, 1, cellRow, 1].Value = "Username: " + Session["EmpName"].ToString();
                exelworksheet.Cells[cellRow, 2, cellRow, 2].Style.Font.Bold = true;
                exelworksheet.Cells[cellRow, 1, cellRow, 1].Style.Font.Bold = true;
                exelworksheet.Cells[cellRow, 2, cellRow, 2].Value = "DateTime: " + DateTime.Now;

                cellRow = 4;
                exelworksheet.Cells[cellRow, 1, cellRow, 8].Value = "General Information";
                exelworksheet.Cells[cellRow, 1, cellRow, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                exelworksheet.Cells[cellRow, 1, cellRow, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                exelworksheet.Cells[cellRow, 1, cellRow, 8].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(209, 242, 208));
                exelworksheet.Cells[cellRow, 1, cellRow, 8].Merge = true;
                exelworksheet.Cells[cellRow, 1, cellRow, 8].Style.Font.Size = 14;
                exelworksheet.Cells[cellRow, 1, cellRow, 8].Style.Font.Bold = true;
                exelworksheet.Cells[cellRow, 1, cellRow, 8].Style.Font.Color.SetColor(Color.FromArgb(78, 73, 73));
                cellRow++;
                for (int i = 0; i < dtGenearalInfo.Columns.Count; i++)
                {
                    exelworksheet.Cells[cellRow, i + 1].Value = dtGenearalInfo.Columns[i].ColumnName.ToString();
                    exelworksheet.Cells[cellRow, i + 1].Style.Font.Bold = true;
                }
                cellRow++;
                for (int i = 0; i < dtGenearalInfo.Rows.Count; i++)
                {
                    for (int j = 0; j < dtGenearalInfo.Columns.Count; j++)
                    {
                        exelworksheet.Cells[cellRow, j + 1].Value = dtGenearalInfo.Rows[i][j].ToString();
                    }
                    cellRow++;
                }
                for (int i = 1; i <= dtGenearalInfo.Columns.Count; i++)
                {
                    exelworksheet.Cells[4, i, dtGenearalInfo.Rows.Count + 1, i].AutoFitColumns();
                }
            }


            #region ----Quality Parameter----
            if (lvQualityParam != null)
            {
                var exelQlyworksheet = Excel.Workbook.Worksheets.Add("Quality Parameters");
                exelQlyworksheet.Cells[1, 1, 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //Color backcolor = Color.FromArgb(224, 76, 76);
                exelQlyworksheet.Cells[1, 1, 1, 8].Value = "Automation of Grinding Process Intelligence (AGI)";
                exelQlyworksheet.Cells[1, 1, 1, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                exelQlyworksheet.Cells[1, 1, 1, 8].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(231, 231, 231));
                exelQlyworksheet.Cells[1, 1, 1, 8].Merge = true;
                exelQlyworksheet.Cells[1, 1, 1, 8].Style.Font.Size = 18;
                exelQlyworksheet.Cells[1, 1, 1, 8].Style.Font.Bold = true;
                exelQlyworksheet.Cells[1, 1, 1, 8].Style.Font.Color.SetColor(Color.Red);


                exelQlyworksheet.Cells[2, 1, 2, 1].Value = "Username: " + Session["EmpName"].ToString();
                exelQlyworksheet.Cells[2, 2, 2, 2].Style.Font.Bold = true;
                exelQlyworksheet.Cells[2, 1, 2, 1].Style.Font.Bold = true;
                exelQlyworksheet.Cells[2, 2, 2, 2].Value = "DateTime: " + DateTime.Now;

                exelQlyworksheet.Cells[4, 1, 4, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                exelQlyworksheet.Cells[4, 1, 4, 8].Value = "Quality Parameters";
                exelQlyworksheet.Cells[4, 1, 4, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                exelQlyworksheet.Cells[4, 1, 4, 8].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(209, 242, 208));
                exelQlyworksheet.Cells[4, 1, 4, 8].Merge = true;
                exelQlyworksheet.Cells[4, 1, 4, 8].Style.Font.Size = 14;
                exelQlyworksheet.Cells[4, 1, 4, 8].Style.Font.Bold = true;
                exelQlyworksheet.Cells[4, 1, 4, 8].Style.Font.Color.SetColor(Color.FromArgb(78, 73, 73));

                int totalSDocCount = lvQualityParam.Items.Count;
                int qcol = 1;
                for (int i = 0; i < lvQualityParam.Items.Count; i++)
                {
                    int qrow = 5;
                    string Sdoc = (lvQualityParam.Items[i].FindControl("SDocId") as HtmlGenericControl).InnerText;
                    exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Value = Sdoc;
                    exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Merge = true;
                    exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Style.Font.Size = 13;
                    exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Style.Font.Bold = true;
                    exelQlyworksheet.Cells[qrow, qcol, qrow, qcol + 4].Style.Font.Color.SetColor(Color.OrangeRed);

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
                        qrow++;
                    }
                    exelQlyworksheet.Column(qcol).AutoFit();
                    qcol = qcol + 6;
                    
                }
                //for (int i = 1; i <= lvQualityParam.Items.Count; i++)
                //{
                //    exelQlyworksheet.Cells[7, i, 7 + lvSdocContent.Items.Count, i].AutoFitColumns();
                //}
                //exelQlyworksheet.Cells[exelQlyworksheet.Dimension.Address].AutoFitColumns();
            }

            #endregion

            #region  -------Derived Parameters----
            if(lvderivedParameter.Items.Count>0)
            {
                var exelDerivedParamworksheet = Excel.Workbook.Worksheets.Add("Derived Parameters");
                exelDerivedParamworksheet.Cells[1, 1, 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //Color backcolor = Color.FromArgb(224, 76, 76);
                exelDerivedParamworksheet.Cells[1, 1, 1, 8].Value = "Automation of Grinding Process Intelligence (AGI)";
                exelDerivedParamworksheet.Cells[1, 1, 1, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                exelDerivedParamworksheet.Cells[1, 1, 1, 8].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(231, 231, 231));
                exelDerivedParamworksheet.Cells[1, 1, 1, 8].Merge = true;
                exelDerivedParamworksheet.Cells[1, 1, 1, 8].Style.Font.Size = 18;
                exelDerivedParamworksheet.Cells[1, 1, 1, 8].Style.Font.Bold = true;
                exelDerivedParamworksheet.Cells[1, 1, 1, 8].Style.Font.Color.SetColor(Color.Red);


                exelDerivedParamworksheet.Cells[2, 1, 2, 1].Value = "Username: " + Session["EmpName"].ToString();
                exelDerivedParamworksheet.Cells[2, 2, 2, 2].Style.Font.Bold = true;
                exelDerivedParamworksheet.Cells[2, 1, 2, 1].Style.Font.Bold = true;
                exelDerivedParamworksheet.Cells[2, 2, 2, 2].Value = "DateTime: " + DateTime.Now;

                exelDerivedParamworksheet.Cells[4, 1, 4, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                exelDerivedParamworksheet.Cells[4, 1, 4, 8].Value = "Derived Parameters";
                exelDerivedParamworksheet.Cells[4, 1, 4, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                exelDerivedParamworksheet.Cells[4, 1, 4, 8].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(209, 242, 208));
                exelDerivedParamworksheet.Cells[4, 1, 4, 8].Merge = true;
                exelDerivedParamworksheet.Cells[4, 1, 4, 8].Style.Font.Size = 14;
                exelDerivedParamworksheet.Cells[4, 1, 4, 8].Style.Font.Bold = true;
                exelDerivedParamworksheet.Cells[4, 1, 4, 8].Style.Font.Color.SetColor(Color.FromArgb(78, 73, 73));

                int dcol,drow=5;
                for(int i=0;i<lvderivedParameter.Items.Count;i++)
                {
                    dcol = 1;
                    string SDocid = (lvderivedParameter.Items[i].FindControl("lblSdocId") as Label).Text;
                    exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol + 4].Value = SDocid;
                    exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol + 4].Merge = true;
                    exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol + 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol + 4].Style.Font.Size = 13;
                    exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol + 4].Style.Font.Bold = true;
                    exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol + 4].Style.Font.Color.SetColor(Color.OrangeRed);

                    drow++;
                    dcol = 1;
                    ListView lvderivedInput=lvderivedParameter.Items[i].FindControl("lvInputParam") as ListView;
                    //Header name
                  
                    for (int j = 0; j < lvderivedInput.Items.Count; j++)
                    {
                        dcol = 1;
                        bool tanggoCologFlag = false;
                        if(j==0)
                        {
                           exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Parameter";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Dia (x) (mm)";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Stock Diametrically (mm)";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Stock on Face (mm)";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "In Feed (mm/min)";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Grinding OD Width (mm)";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Feed Angle";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Work Speed (m/min) OD";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Work Speed (m/min) Face";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            //exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Work Speed (m/sec)";
                            //exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            //dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "X-Feed (mm/min)";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Z-Feed (mm/min)";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            drow++;
                           
                        }
                        dcol = 1;
                        string parameter = (lvderivedInput.Items[j].FindControl("identifier") as Label).Text;
                        string dia = (lvderivedInput.Items[j].FindControl("diameter") as Label).Text;
                        string doc = (lvderivedInput.Items[j].FindControl("doc") as Label).Text;
                        string face = (lvderivedInput.Items[j].FindControl("face") as Label).Text;
                        string infeed = (lvderivedInput.Items[j].FindControl("infeed") as Label).Text;
                       // string workspeed = (lvderivedInput.Items[j].FindControl("workspeed") as Label).Text;
                        string odwidth = (lvderivedInput.Items[j].FindControl("odwidth") as Label).Text;
                        string feedangle = (lvderivedInput.Items[j].FindControl("feedangle") as Label).Text;
                        string workspeedOD = (lvderivedInput.Items[j].FindControl("workspeedOD") as Label).Text;
                        string workspeedface = (lvderivedInput.Items[j].FindControl("workspeedface") as Label).Text;
                        string xfeed = (lvderivedInput.Items[j].FindControl("xfeed") as Label).Text;
                        string zfeed = (lvderivedInput.Items[j].FindControl("zfeed") as Label).Text;
                        string tangoOD= (lvderivedInput.Items[j].FindControl("TangoFlagOD") as HiddenField).Value;
                        string tangoFace = (lvderivedInput.Items[j].FindControl("TangoFlagFace") as HiddenField).Value;
                        if (tangoOD=="1" || tangoFace=="1")
                        {
                            tanggoCologFlag = true;
                        }
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = parameter;
                        dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = dia;
                        dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = doc;
                        dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = face;
                        dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = infeed;
                        dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = odwidth;
                        dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = feedangle;
                        dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = workspeedOD;
                        dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = workspeedface;
                        dcol++;
                        //exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = workspeed;
                        //dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = xfeed;
                        dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = zfeed;
                        if (tanggoCologFlag)
                        {
                            exelDerivedParamworksheet.Row(drow).Style.Font.Color.SetColor(Color.Red);
                        }
                        drow++;
                    }

                    drow++;
                    string sparkOutTime= (lvderivedParameter.Items[i].FindControl("txtSparkOutTime") as Label).Text;
                    string tango = (lvderivedParameter.Items[i].FindControl("txtTangotTime") as Label).Text;
                    string feedgrindtime = (lvderivedParameter.Items[i].FindControl("txtFeedGrindTime") as Label).Text;
                    string slideforware = (lvderivedParameter.Items[i].FindControl("txtSlideForward") as Label).Text;
                    string prgmread = (lvderivedParameter.Items[i].FindControl("txtPrgmRead") as Label).Text;
                    string flag = (lvderivedParameter.Items[i].FindControl("txtFlagging") as Label).Text;
                    string sldereturn = (lvderivedParameter.Items[i].FindControl("txtSlideRetuen") as Label).Text;
                    string other = (lvderivedParameter.Items[i].FindControl("txtOther") as Label).Text;
                    string loadunload = (lvderivedParameter.Items[i].FindControl("txtLoadUnload") as Label).Text;
                    string Chipwidthratio = (lvderivedParameter.Items[i].FindControl("txtChipwidthratio") as Label).Text;
                    string Wheeltiltangle = (lvderivedParameter.Items[i].FindControl("txtWheeltiltangle") as Label).Text;
                    string manualLoadUnload = (lvderivedParameter.Items[i].FindControl("txtManualLoadUnload") as Label).Text;

                    exelDerivedParamworksheet.Cells[drow, 1, drow, 1].Value = "Chip width / thickness ratio";
                    exelDerivedParamworksheet.Cells[drow, 2, drow, 2].Value = Chipwidthratio;
                    exelDerivedParamworksheet.Cells[drow, 4, drow, 4].Value = "Wheel Tilt Angle (Deg)";
                    exelDerivedParamworksheet.Cells[drow, 5, drow, 5].Value = Wheeltiltangle;

                    drow = drow + 2;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 5].Value = "Grinding Time (sec)";
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 5].Merge = true;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 5].Style.Font.Bold = true;
                    drow++;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 1].Value = "Spark Out Time (sec)";
                    exelDerivedParamworksheet.Cells[drow, 2, drow, 2].Value = sparkOutTime;
                    exelDerivedParamworksheet.Cells[drow, 4, drow, 4].Value = "Tango / Relief Time (sec)";
                    exelDerivedParamworksheet.Cells[drow, 5, drow, 5].Value = tango;
                    drow++;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 1].Value = "Traverse Grinding Time (sec)";
                    exelDerivedParamworksheet.Cells[drow, 2, drow, 2].Value = feedgrindtime;
                    drow=drow+2;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 5].Value = "Non Grinding Time (sec)";
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 5].Merge = true;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 5].Style.Font.Bold = true;
                    drow++;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 1].Value = "Slide Forward (sec)";
                    exelDerivedParamworksheet.Cells[drow, 2, drow, 2].Value = slideforware;
                    exelDerivedParamworksheet.Cells[drow, 4, drow, 4].Value = "Program Read (sec)";
                    exelDerivedParamworksheet.Cells[drow, 5, drow, 5].Value = prgmread;
                    drow++;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 1].Value = "Flagging (sec)";
                    exelDerivedParamworksheet.Cells[drow, 2, drow, 2].Value = flag;
                    exelDerivedParamworksheet.Cells[drow, 4, drow, 4].Value = "Slide Return (sec)";
                    exelDerivedParamworksheet.Cells[drow, 5, drow, 5].Value = sldereturn;
                    drow++;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 1].Value = "Manual Loading/Unloading Time (sec)";
                    exelDerivedParamworksheet.Cells[drow, 2, drow, 2].Value = manualLoadUnload;
                    exelDerivedParamworksheet.Cells[drow, 4, drow, 4].Value = "Loading/Unloading Time (sec)";
                    exelDerivedParamworksheet.Cells[drow, 5, drow, 5].Value = loadunload;
                    drow++;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 1].Value = "Other (sec)";
                    exelDerivedParamworksheet.Cells[drow, 2, drow, 2].Value = other;
                    drow++;

                    drow++;
                    dcol = 1;
                    ListView lvderivedCalculate = lvderivedParameter.Items[i].FindControl("lvCalculatedPara") as ListView;
                    //Header
                    for (int j = 0; j < lvderivedCalculate.Items.Count; j++)
                    {
                        dcol = 1;
                        if (j == 0)
                        {
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Parameter";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Radial Depth of Cut (X) (mm/rev)";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Depth of Cut (Z) (mm/rev)";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "MRR'(X) (cu.mm/mm/sec)";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "MRR'(Z) (cu.mm/mm/sec)";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            //exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Total MRR' (cu.mm/sec)";
                            //exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            //dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Total MRR (cu.mm/sec)";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Grit Penetration Depth (X) (μm)";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Grit Penetration Depth (Z) (μm)";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Time (sec)";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Wheel Work Speed Ratio";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            dcol++;
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = "Wheel Work RPM Ratio";
                            exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Style.Font.Bold = true;
                            drow++;
                            
                        }
                        dcol = 1;
                        bool tanggoCologFlag = false;
                        string parameter = (lvderivedCalculate.Items[j].FindControl("parameter") as Label).Text;
                        //string mrr = (lvderivedCalculate.Items[j].FindControl("mrr") as Label).Text;
                        //string totalmrr = (lvderivedCalculate.Items[j].FindControl("totalmrr") as Label).Text;
                        //string gritpenetrationdepth = (lvderivedCalculate.Items[j].FindControl("gritpenetrationdepth") as Label).Text;
                        string time = (lvderivedCalculate.Items[j].FindControl("time") as Label).Text;
                        string workspeedratio = (lvderivedCalculate.Items[j].FindControl("workspeedratio") as Label).Text;
                        string workrpmratio = (lvderivedCalculate.Items[j].FindControl("workrpmratio") as Label).Text;
                        string mrrx = (lvderivedCalculate.Items[j].FindControl("mrrx") as Label).Text;
                        string mrrz = (lvderivedCalculate.Items[j].FindControl("mrrz") as Label).Text;
                       // string totalmrrx = (lvderivedCalculate.Items[j].FindControl("totalmrrx") as Label).Text;
                        string gpdx = (lvderivedCalculate.Items[j].FindControl("gpdx") as Label).Text;
                        string gpdz = (lvderivedCalculate.Items[j].FindControl("gpdz") as Label).Text;
                        string radialdocx = (lvderivedCalculate.Items[j].FindControl("radialDOCX") as Label).Text;
                        string radialdocz = (lvderivedCalculate.Items[j].FindControl("radialDOCZ") as Label).Text;
                        string totalmrr = (lvderivedCalculate.Items[j].FindControl("totalmrr") as Label).Text;
                        string tangoOD = (lvderivedInput.Items[j].FindControl("TangoFlagOD") as HiddenField).Value;
                        string tangoFace = (lvderivedInput.Items[j].FindControl("TangoFlagFace") as HiddenField).Value;
                        if (tangoOD == "1" || tangoFace == "1")
                        {
                            tanggoCologFlag = true;
                        }
                        if (tanggoCologFlag)
                        {
                            exelDerivedParamworksheet.Row(drow).Style.Font.Color.SetColor(Color.Red);
                        }
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = parameter;
                        dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = radialdocx;
                        dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = radialdocz;
                        dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = mrrx;
                        dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = mrrz;
                        dcol++;
                        //exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = totalmrrx;
                        //dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = totalmrr;
                        dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = gpdx;
                        dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = gpdz;
                        dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = time;
                        dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = workspeedratio;
                        dcol++;
                        exelDerivedParamworksheet.Cells[drow, dcol, drow, dcol].Value = workrpmratio;
                        drow++;
                    }

                    string equdia = (lvderivedParameter.Items[i].FindControl("txtEquivalentDia") as Label).Text;
                    string equdiaface = (lvderivedParameter.Items[i].FindControl("txtEquivalentDiaFace") as Label).Text;
                    string cuttingedge = (lvderivedParameter.Items[i].FindControl("txtCuttingEdge") as Label).Text;
                    string spartoutrev = (lvderivedParameter.Items[i].FindControl("txtSpartOutRev") as Label).Text;
                    string grindtime = (lvderivedParameter.Items[i].FindControl("txtGrindcycletime") as Label).Text;
                    string nongrindtime = (lvderivedParameter.Items[i].FindControl("txtNongrindingtime") as Label).Text;
                    string totalgrinding = (lvderivedParameter.Items[i].FindControl("txtTotalGrindingTime") as Label).Text;
                    string totalcycletime = (lvderivedParameter.Items[i].FindControl("txtTotalcycletime") as Label).Text;
                    string floortofloor = (lvderivedParameter.Items[i].FindControl("txtFloortoFloor") as Label).Text;

                    drow++;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 5].Value = "Other Calculations";
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 5].Merge = true;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 5].Style.Font.Bold = true;
                    drow++;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 1].Value = "Equivalent Dia for OD (De) (mm)";
                    exelDerivedParamworksheet.Cells[drow, 2, drow, 2].Value = equdia;
                    exelDerivedParamworksheet.Cells[drow, 4, drow, 4].Value = "Equivalent Dia Face (mm)";
                    exelDerivedParamworksheet.Cells[drow, 5, drow, 5].Value = equdiaface;
                    drow++;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 1].Value = "Cutting Edge Density (/sq.m.)";
                    exelDerivedParamworksheet.Cells[drow, 2, drow, 2].Value = cuttingedge;
                    exelDerivedParamworksheet.Cells[drow, 4, drow, 4].Value = "Spark Out Revolutions (rev)";
                    exelDerivedParamworksheet.Cells[drow, 5, drow, 5].Value = spartoutrev;
                    drow = drow + 2;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 5].Value = "Time Calculations";
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 5].Merge = true;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 5].Style.Font.Bold = true;
                    drow++;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 1].Value = "Grinding Time (sec)";
                    exelDerivedParamworksheet.Cells[drow, 2, drow, 2].Value = grindtime;
                    exelDerivedParamworksheet.Cells[drow, 4, drow, 4].Value = "Non Grinding Time (sec)";
                    exelDerivedParamworksheet.Cells[drow, 5, drow, 5].Value = nongrindtime;
                    drow++;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 1].Value = "Total Grinding Time (sec)";
                    exelDerivedParamworksheet.Cells[drow, 2, drow, 2].Value = totalgrinding;
                    exelDerivedParamworksheet.Cells[drow, 4, drow, 4].Value = "Total Cycle Time (sec)";
                    exelDerivedParamworksheet.Cells[drow, 5, drow, 5].Value = totalcycletime;
                    drow++;
                    exelDerivedParamworksheet.Cells[drow, 1, drow, 1].Value = "Floor to Floor Time (sec)";
                    exelDerivedParamworksheet.Cells[drow, 2, drow, 2].Value = floortofloor;
                    drow =drow+2;
                }

                exelDerivedParamworksheet.Cells[exelDerivedParamworksheet.Dimension.Address].AutoFitColumns();
            }
            #endregion

            #region ----Time Graph------
            string Plunge ="", Subcategory="";
            string prevPlunge = "", prevSubcategory = "";
            string FinalSdoc = "",FinalPlunge = "", FinalSubcategory = "";
            for (int i = 0; i < dcSdoclist.Items.Count; i++)
            {
                if (dcSdoclist.Items[i].Selected)
                {
                    var sdocid = dcSdoclist.Items[i].Text.Split('_');
                    FinalSdoc = sdocid[0].Replace("SDoc", "");
                    Plunge = sdocid[1];
                    Subcategory = sdocid[2];
                    if (Plunge != prevPlunge)
                    {
                        FinalPlunge = FinalPlunge + "'" + Plunge + "',";
                    }
                    if (Subcategory != prevSubcategory)
                    {
                        FinalSubcategory = FinalSubcategory + "'" + Subcategory + "',";
                    }
                    prevPlunge = Plunge;
                    prevSubcategory = Subcategory;
                }
            }
            if (FinalPlunge != "" && FinalSubcategory != "")
            {
                FinalPlunge = FinalPlunge.Substring(0, FinalPlunge.Length - 1);
                FinalSubcategory = FinalSubcategory.Substring(0, FinalSubcategory.Length - 1);

                var exelGrindTimeworksheet = Excel.Workbook.Worksheets.Add("Grinding Time");
                setWorkSheetSetting(exelGrindTimeworksheet);
                exelGrindTimeworksheet.Cells[1, 1, 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //Color backcolor = Color.FromArgb(224, 76, 76);
                exelGrindTimeworksheet.Cells[1, 1, 1, 8].Value = "Automation of Grinding Process Intelligence (AGI)";
                exelGrindTimeworksheet.Cells[1, 1, 1, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                exelGrindTimeworksheet.Cells[1, 1, 1, 8].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(231, 231, 231));
                exelGrindTimeworksheet.Cells[1, 1, 1, 8].Merge = true;
                exelGrindTimeworksheet.Cells[1, 1, 1, 8].Style.Font.Size = 18;
                exelGrindTimeworksheet.Cells[1, 1, 1, 8].Style.Font.Bold = true;
                exelGrindTimeworksheet.Cells[1, 1, 1, 8].Style.Font.Color.SetColor(Color.Red);


                exelGrindTimeworksheet.Cells[2, 1, 2, 1].Value = "Username: " + Session["EmpName"].ToString();
                exelGrindTimeworksheet.Cells[2, 2, 2, 2].Style.Font.Bold = true;
                exelGrindTimeworksheet.Cells[2, 1, 2, 1].Style.Font.Bold = true;
                exelGrindTimeworksheet.Cells[2, 2, 2, 2].Value = "DateTime: " + DateTime.Now;

                decimal totalGrindingTime = 0, totalNonGrindingTime = 0;



                //total cycle time
                exelGrindTimeworksheet.Cells[4, 1, 4, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                exelGrindTimeworksheet.Cells[4, 1, 4, 8].Value = "Total Cycle Time";
                exelGrindTimeworksheet.Cells[4, 1, 4, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                exelGrindTimeworksheet.Cells[4, 1, 4, 8].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(209, 242, 208));
                exelGrindTimeworksheet.Cells[4, 1, 4, 8].Merge = true;
                exelGrindTimeworksheet.Cells[4, 1, 4, 8].Style.Font.Size = 14;
                exelGrindTimeworksheet.Cells[4, 1, 4, 8].Style.Font.Bold = true;
                exelGrindTimeworksheet.Cells[4, 1, 4, 8].Style.Font.Color.SetColor(Color.FromArgb(78, 73, 73));
                int cellRowGrindTime = 5;
                List<string> grindTimeDetails = new List<string>();
                grindTimeDetails = DBAccess.omBindTotalCycleTime(FinalSdoc, FinalPlunge, FinalSubcategory);
                exelGrindTimeworksheet.Cells[cellRowGrindTime, 1].Value = "Item";
                exelGrindTimeworksheet.Cells[cellRowGrindTime, 1].Style.Font.Bold = true;
                exelGrindTimeworksheet.Cells[cellRowGrindTime, 2].Value = "Value";
                exelGrindTimeworksheet.Cells[cellRowGrindTime, 2].Style.Font.Bold = true;
                cellRowGrindTime++;
                for (int i = 0; i < grindTimeDetails.Count; i = i + 2)
                {
                    exelGrindTimeworksheet.Cells[cellRowGrindTime, 1].Value = grindTimeDetails[i];
                    if (grindTimeDetails[i + 1].ToString() != "")
                    {
                        exelGrindTimeworksheet.Cells[cellRowGrindTime, 2].Value = Convert.ToDecimal(grindTimeDetails[i + 1]);
                        totalGrindingTime = totalGrindingTime + Convert.ToDecimal(grindTimeDetails[i + 1]);
                    }
                    else
                    {
                        exelGrindTimeworksheet.Cells[cellRowGrindTime, 2].Value = grindTimeDetails[i + 1];
                    }
                    cellRowGrindTime++;
                }
                ExcelPieChart pieChartGrindTime = exelGrindTimeworksheet.Drawings.AddChart("pieChart", eChartType.Pie3D) as ExcelPieChart;
                pieChartGrindTime.Title.Text = "Grinding Time and Non Grinding Time";
                pieChartGrindTime.Title.Font.Size = 11;
                pieChartGrindTime.Series.Add(ExcelRange.GetAddress(6, 2, 7, 2), ExcelRange.GetAddress(6, 1, 7, 1));
                pieChartGrindTime.Legend.Position = eLegendPosition.Bottom;
                // pieChartGrindTime.DataLabel.ShowPercent = true;
                //pieChartGrindTime.DataLabel.ShowValue = true;
                pieChartGrindTime.DataLabel.ShowPercent = true;
                //  pieChartGrindTime.DataLabel.
                // pieChartGrindTime.DataLabel.ShowCategory = true;
                //pieChartGrindTime.DataLabel.ShowLeaderLines = true;
                //pieChartGrindTime.DataLabel.Separator = ";  ";
                //pieChartGrindTime.DataLabel. = eLabelPosition.BestFit;

                //var xdoc = pieChartGrindTime.ChartXml;
                //var nsuri = xdoc.DocumentElement.NamespaceURI;
                //var nsm = new XmlNamespaceManager(xdoc.NameTable);
                //nsm.AddNamespace("c", nsuri);

                ////Added the number format node via XML
                //var numFmtNode = xdoc.CreateElement("c:numFmt", nsuri);

                //var formatCodeAtt = xdoc.CreateAttribute("formatCode", nsuri);
                //formatCodeAtt.Value = "0.00%";
                //numFmtNode.Attributes.Append(formatCodeAtt);

                //var sourceLinkedAtt = xdoc.CreateAttribute("sourceLinked", nsuri);
                //sourceLinkedAtt.Value = "0";
                //numFmtNode.Attributes.Append(sourceLinkedAtt);

                //var dLblsNode = xdoc.SelectSingleNode("c:chartSpace/c:chart/c:plotArea/c:pieChart/c:ser/c:dLbls", nsm);
                //dLblsNode.AppendChild(numFmtNode);



                pieChartGrindTime.SetSize(500, 300);

                pieChartGrindTime.SetPosition(5, 0, 4, 0);

                ExcelPieChart excelLineChart1 = exelGrindTimeworksheet.Drawings.AddChart("pieChart1", eChartType.Pie3D) as ExcelPieChart;
                excelLineChart1.Title.Text = "Total Cycle Time";
                excelLineChart1.Title.Font.Size = 11;
                ExcelRange yvalues = exelGrindTimeworksheet.Cells["B6,B9"];
                ExcelRange xvalues = exelGrindTimeworksheet.Cells["A6,A9"];
                excelLineChart1.Series.Add(yvalues, xvalues);
                excelLineChart1.Legend.Position = eLegendPosition.Bottom;
                excelLineChart1.DataLabel.ShowPercent = true;
                excelLineChart1.SetSize(500, 300);
                excelLineChart1.SetPosition(5, 0, 13, 0);

                ExcelPieChart excelNongrindAcualgrindChart = exelGrindTimeworksheet.Drawings.AddChart("pieChart2", eChartType.Pie3D) as ExcelPieChart;
                excelNongrindAcualgrindChart.Title.Text = "Actual Grinding Time v/s Non Grinding Time";
                excelNongrindAcualgrindChart.Title.Font.Size = 11;
                ExcelRange yvalue = exelGrindTimeworksheet.Cells["B7,B9"];
                ExcelRange xvalue = exelGrindTimeworksheet.Cells["A7,A9"];
                excelNongrindAcualgrindChart.Series.Add(yvalue, xvalue);
                excelNongrindAcualgrindChart.Legend.Position = eLegendPosition.Bottom;
                excelNongrindAcualgrindChart.DataLabel.ShowPercent = true;
                excelNongrindAcualgrindChart.SetSize(500, 300);
                excelNongrindAcualgrindChart.SetPosition(5, 0, 22, 0);


                //grinding Time
                exelGrindTimeworksheet.Cells[23, 1, 23, 8].Value = "Grinding Time";
                exelGrindTimeworksheet.Cells[23, 1, 23, 8].Style.Font.Size = 14;
                exelGrindTimeworksheet.Cells[23, 1, 23, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                exelGrindTimeworksheet.Cells[23, 1, 23, 8].Style.Font.Bold = true;
                exelGrindTimeworksheet.Cells[23, 1, 23, 8].Merge = true;
                exelGrindTimeworksheet.Cells[23, 1, 23, 8].Style.Font.Color.SetColor(Color.FromArgb(78, 73, 73));
                exelGrindTimeworksheet.Cells[23, 1, 23, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                exelGrindTimeworksheet.Cells[23, 1, 23, 8].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(209, 242, 208));
                exelGrindTimeworksheet.Cells[24, 1].Value = "Item";
                exelGrindTimeworksheet.Cells[24, 1].Style.Font.Bold = true;
                exelGrindTimeworksheet.Cells[24, 2].Value = "Value";
                exelGrindTimeworksheet.Cells[24, 2].Style.Font.Bold = true;
                List<string> nongrindTimeDetails = new List<string>();
                nongrindTimeDetails = DBAccess.omBindGrindingTime(FinalSdoc, FinalPlunge, FinalSubcategory);
                int cellRownonGrindTime = 25;
                for (int i = 0; i < nongrindTimeDetails.Count; i = i + 2)
                {
                    exelGrindTimeworksheet.Cells[cellRownonGrindTime, 1].Value = nongrindTimeDetails[i];
                    if (nongrindTimeDetails[i + 1].ToString() != "")
                    {
                        exelGrindTimeworksheet.Cells[cellRownonGrindTime, 2].Value = Convert.ToDecimal(nongrindTimeDetails[i + 1]);
                        totalNonGrindingTime = totalNonGrindingTime + Convert.ToDecimal(nongrindTimeDetails[i + 1]);
                    }
                    else
                    {
                        exelGrindTimeworksheet.Cells[cellRownonGrindTime, 2].Value = nongrindTimeDetails[i + 1];
                    }
                    cellRownonGrindTime++;
                }
                ExcelPieChart pieChartnonGrindTime = exelGrindTimeworksheet.Drawings.AddChart("nonGrindingTimepieChart", eChartType.Pie3D) as ExcelPieChart;
                pieChartnonGrindTime.Title.Text = "Grinding Time";
                pieChartnonGrindTime.Title.Font.Size = 11;
                pieChartnonGrindTime.Series.Add(ExcelRange.GetAddress(25, 2, 30, 2), ExcelRange.GetAddress(25, 1, 30, 1));
                pieChartnonGrindTime.Legend.Position = eLegendPosition.Bottom;
                pieChartnonGrindTime.DataLabel.ShowPercent = true;
                pieChartnonGrindTime.SetSize(500, 300);
                pieChartnonGrindTime.SetPosition(24, 0, 4, 0);

                //Non grinding time
                exelGrindTimeworksheet.Cells[42, 1, 42, 8].Value = "Non Grinding Time";
                exelGrindTimeworksheet.Cells[42, 1, 42, 8].Style.Font.Size = 14;
                exelGrindTimeworksheet.Cells[42, 1, 42, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                exelGrindTimeworksheet.Cells[42, 1, 42, 8].Style.Font.Bold = true;
                exelGrindTimeworksheet.Cells[42, 1, 42, 8].Merge = true;
                exelGrindTimeworksheet.Cells[42, 1, 42, 8].Style.Font.Color.SetColor(Color.FromArgb(78, 73, 73));
                exelGrindTimeworksheet.Cells[42, 1, 42, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                exelGrindTimeworksheet.Cells[42, 1, 42, 8].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(209, 242, 208));
                exelGrindTimeworksheet.Cells[43, 1].Value = "Item";
                exelGrindTimeworksheet.Cells[43, 1].Style.Font.Bold = true;
                exelGrindTimeworksheet.Cells[43, 2].Value = "Value";
                exelGrindTimeworksheet.Cells[43, 2].Style.Font.Bold = true;

                List<string> totalcycleTimeDetails = new List<string>();
                totalcycleTimeDetails = DBAccess.omBindNonGrindingTime(FinalSdoc, FinalPlunge, FinalSubcategory);
                int cellRowtotalCycleTime = 44;
                for (int i = 0; i < totalcycleTimeDetails.Count; i = i + 2)
                {
                    exelGrindTimeworksheet.Cells[cellRowtotalCycleTime, 1].Value = totalcycleTimeDetails[i];
                    if (totalcycleTimeDetails[i + 1].ToString() != "")
                    {
                        exelGrindTimeworksheet.Cells[cellRowtotalCycleTime, 2].Value = Convert.ToDecimal(totalcycleTimeDetails[i + 1]);
                        totalNonGrindingTime = totalNonGrindingTime + Convert.ToDecimal(totalcycleTimeDetails[i + 1]);
                    }
                    else
                    {
                        exelGrindTimeworksheet.Cells[cellRowtotalCycleTime, 2].Value = totalcycleTimeDetails[i + 1];
                    }
                    cellRowtotalCycleTime++;
                }

                ExcelPieChart pieCharttotalCycleTime = exelGrindTimeworksheet.Drawings.AddChart("totalCycleTimepieChart", eChartType.Pie3D) as ExcelPieChart;
                pieCharttotalCycleTime.Title.Text = "Non Grinding Time";
                pieCharttotalCycleTime.Title.Font.Size = 11;
                pieCharttotalCycleTime.Series.Add(ExcelRange.GetAddress(44, 2, 49, 2), ExcelRange.GetAddress(44, 1, 49, 1));
                pieCharttotalCycleTime.Legend.Position = eLegendPosition.Bottom;
                pieCharttotalCycleTime.DataLabel.ShowPercent = true;
                pieCharttotalCycleTime.SetSize(500, 300);
                pieCharttotalCycleTime.SetPosition(43, 0, 4, 0);

                exelGrindTimeworksheet.Column(1).AutoFit();
            }

            var exelCalcParamworksheet = Excel.Workbook.Worksheets.Add("Calulated Parameter");
            setWorkSheetSetting(exelCalcParamworksheet);
            exelCalcParamworksheet.Cells[1, 1, 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //Color backcolor = Color.FromArgb(224, 76, 76);
            exelCalcParamworksheet.Cells[1, 1, 1, 8].Value = "Automation of Grinding Process Intelligence (AGI)";
            exelCalcParamworksheet.Cells[1, 1, 1, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
            exelCalcParamworksheet.Cells[1, 1, 1, 8].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(231, 231, 231));
            exelCalcParamworksheet.Cells[1, 1, 1, 8].Merge = true;
            exelCalcParamworksheet.Cells[1, 1, 1, 8].Style.Font.Size = 18;
            exelCalcParamworksheet.Cells[1, 1, 1, 8].Style.Font.Bold = true;
            exelCalcParamworksheet.Cells[1, 1, 1, 8].Style.Font.Color.SetColor(Color.Red);

            exelCalcParamworksheet.Cells[2, 1, 2, 1].Value = "Username: " + Session["EmpName"].ToString();
            exelCalcParamworksheet.Cells[2, 2, 2, 2].Style.Font.Bold = true;
            exelCalcParamworksheet.Cells[2, 1, 2, 1].Style.Font.Bold = true;
            exelCalcParamworksheet.Cells[2, 2, 2, 2].Value = "DateTime: " + DateTime.Now;

            exelCalcParamworksheet.Cells[4, 1, 4, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            exelCalcParamworksheet.Cells[4, 1, 4, 2].Value = "Calculated Parameter";
            exelCalcParamworksheet.Cells[4, 1, 4, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            exelCalcParamworksheet.Cells[4, 1, 4, 2].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(209, 242, 208));
            exelCalcParamworksheet.Cells[4, 1, 4, 2].Merge = true;
            exelCalcParamworksheet.Cells[4, 1, 4, 2].Style.Font.Size = 14;
            exelCalcParamworksheet.Cells[4, 1, 4, 2].Style.Font.Bold = true;
            exelCalcParamworksheet.Cells[4, 1, 4, 2].Style.Font.Color.SetColor(Color.FromArgb(78, 73, 73));
            int cellRowCalcParam = 5;
            List<string> calParamDetails = DBAccess.omBindCalculatedParam(sdocPlungeCat.Sdocname, sdocPlungeCat.Plunge, sdocPlungeCat.Category);
            int noOfRow = Convert.ToInt32(calParamDetails[0]);
            int noOfColumn = Convert.ToInt32(calParamDetails[1]);
            List<string> calcItemName = new List<string>();
            List<string> calcModuleName = new List<string>();
            List<string> calcValues = new List<string>();
            //for (int i = 3; i < calParamDetails.Count; i++)
            //{
            //    if (i > 2 && i < (noOfRow + 3))
            //    {
            //        calcItemName.Add(calParamDetails[i]);
            //    }
            //    else
            //    {
            //        calcValues.Add(calParamDetails[i]);
            //    }
            //}
            for (var i = 3; i < calParamDetails.Count; i++)
            {
                if (i > 2 && i < (noOfRow) + 3)
                {
                    calcModuleName.Add(calParamDetails[i]);
                }
                else if (i > (noOfRow+ 3) && i < ((2 * noOfRow) + 4))
                {
                    calcItemName.Add(calParamDetails[i]);
                }
                else if (i > ((2 * noOfRow) + 3))
                {
                    calcValues.Add(calParamDetails[i]);
                }
            }
            int calcSdocCount = 0;
            for (int i = 0; i < noOfColumn - 2; i++)
            {
                int tempRow = cellRowCalcParam;
                exelCalcParamworksheet.Cells[cellRowCalcParam, 1, cellRowCalcParam, 4].Value = calcValues[calcSdocCount];
                calcSdocCount++;
                exelCalcParamworksheet.Cells[cellRowCalcParam, 1, cellRowCalcParam, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                exelCalcParamworksheet.Cells[cellRowCalcParam, 1, cellRowCalcParam, 4].Style.Font.Size = 13;
                exelCalcParamworksheet.Cells[cellRowCalcParam, 1, cellRowCalcParam, 4].Merge = true;
                exelCalcParamworksheet.Cells[cellRowCalcParam, 1, cellRowCalcParam, 4].Style.Font.Bold = true;
                exelCalcParamworksheet.Cells[cellRowCalcParam, 1, cellRowCalcParam, 4].Style.Font.Color.SetColor(Color.OrangeRed);
                cellRowCalcParam++;
                //exelCalcParamworksheet.Cells[cellRowCalcParam, 1].Value = "Item";
                //exelCalcParamworksheet.Cells[cellRowCalcParam, 1].Style.Font.Bold = true;
                //exelCalcParamworksheet.Cells[cellRowCalcParam, 2].Value = "Value";
                //exelCalcParamworksheet.Cells[cellRowCalcParam, 2].Style.Font.Bold = true;
                //cellRowCalcParam++;
                int derivedflag = 0, dressingflag = 0;
                for (int j = 0; j < noOfRow; j++)
                {
                    

                    if (calcModuleName[j] == "Dervied Parameters")
                    {
                        if (derivedflag == 0)
                        {
                            derivedflag++;
                            exelCalcParamworksheet.Cells[cellRowCalcParam, 1, cellRowCalcParam, 2].Value = "Calculated Parameters";
                            exelCalcParamworksheet.Cells[cellRowCalcParam, 1, cellRowCalcParam, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            exelCalcParamworksheet.Cells[cellRowCalcParam, 1, cellRowCalcParam, 2].Style.Font.Size = 12;
                            exelCalcParamworksheet.Cells[cellRowCalcParam, 1, cellRowCalcParam, 2].Merge = true;
                            exelCalcParamworksheet.Cells[cellRowCalcParam, 1, cellRowCalcParam, 2].Style.Font.Bold = true;
                            cellRowCalcParam++;
                            exelCalcParamworksheet.Cells[cellRowCalcParam, 1].Value = "Item";
                            exelCalcParamworksheet.Cells[cellRowCalcParam, 1].Style.Font.Bold = true;
                            exelCalcParamworksheet.Cells[cellRowCalcParam, 2].Value = "Value";
                            exelCalcParamworksheet.Cells[cellRowCalcParam, 2].Style.Font.Bold = true;
                            cellRowCalcParam++;
                        }
                    }
                    if (calcModuleName[j] == "Dressing Parameters")
                    {
                        if (dressingflag == 0)
                        {
                            dressingflag++;
                            exelCalcParamworksheet.Cells[cellRowCalcParam, 1, cellRowCalcParam, 2].Value = "Dressing Parameters";
                            exelCalcParamworksheet.Cells[cellRowCalcParam, 1, cellRowCalcParam, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            exelCalcParamworksheet.Cells[cellRowCalcParam, 1, cellRowCalcParam, 2].Style.Font.Size = 12;
                            exelCalcParamworksheet.Cells[cellRowCalcParam, 1, cellRowCalcParam, 2].Merge = true;
                            exelCalcParamworksheet.Cells[cellRowCalcParam, 1, cellRowCalcParam, 2].Style.Font.Bold = true;
                            cellRowCalcParam++;
                            exelCalcParamworksheet.Cells[cellRowCalcParam, 1].Value = "Item";
                            exelCalcParamworksheet.Cells[cellRowCalcParam, 1].Style.Font.Bold = true;
                            exelCalcParamworksheet.Cells[cellRowCalcParam, 2].Value = "Value";
                            exelCalcParamworksheet.Cells[cellRowCalcParam, 2].Style.Font.Bold = true;
                            cellRowCalcParam++;
                        }
                    }
                    //appendString += '<tr><td>' + itemName[k] + '</td><td>' + calDetails[calDetailsCount] + '</td></tr></table></div>';
                    exelCalcParamworksheet.Cells[cellRowCalcParam, 1].Value = calcItemName[j];
                    if (calcValues[calcSdocCount].ToString() != "")
                    {
                        exelCalcParamworksheet.Cells[cellRowCalcParam, 2].Value = Convert.ToDecimal(calcValues[calcSdocCount]);
                    }
                    else
                    {
                        exelCalcParamworksheet.Cells[cellRowCalcParam, 2].Value = calcValues[calcSdocCount];
                    }
                    cellRowCalcParam++;
                    calcSdocCount++;

                }
                cellRowCalcParam++;
                ExcelPieChart pieChartCalcParam = exelCalcParamworksheet.Drawings.AddChart("CalculatedParameterPieChart" + i, eChartType.Pie3D) as ExcelPieChart;
                pieChartCalcParam.Title.Text = "Calculated Parameter";
                pieChartCalcParam.Title.Font.Size = 11;
                pieChartCalcParam.Series.Add(ExcelRange.GetAddress(tempRow + 3, 2, tempRow + 5, 2), ExcelRange.GetAddress(tempRow + 3, 1, tempRow + 5, 1));
                pieChartCalcParam.Legend.Position = eLegendPosition.Bottom;
                pieChartCalcParam.DataLabel.ShowPercent = true;
                pieChartCalcParam.SetSize(500, 250);
                pieChartCalcParam.SetPosition(tempRow - 1, 0, 5, 0);
            }
            for (int i = 1; i <= 2; i++)
            {
                exelCalcParamworksheet.Cells[4, i, (noOfRow * noOfColumn) + ((noOfColumn - 1) * 3), i].AutoFitColumns();
            }
            var exelSignalworksheet = Excel.Workbook.Worksheets.Add("Power Signal");
            setWorkSheetSetting(exelSignalworksheet);
            exelSignalworksheet.Cells[1, 1, 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //Color backcolor = Color.FromArgb(224, 76, 76);
            exelSignalworksheet.Cells[1, 1, 1, 8].Value = "Automation of Grinding Process Intelligence (AGI)";
            exelSignalworksheet.Cells[1, 1, 1, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
            exelSignalworksheet.Cells[1, 1, 1, 8].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(231, 231, 231));
            exelSignalworksheet.Cells[1, 1, 1, 8].Merge = true;
            exelSignalworksheet.Cells[1, 1, 1, 8].Style.Font.Size = 18;
            exelSignalworksheet.Cells[1, 1, 1, 8].Style.Font.Bold = true;
            exelSignalworksheet.Cells[1, 1, 1, 8].Style.Font.Color.SetColor(Color.Red);

            exelSignalworksheet.Cells[2, 1, 2, 1].Value = "Username: " + Session["EmpName"].ToString();
            exelSignalworksheet.Cells[2, 2, 2, 2].Style.Font.Bold = true;
            exelSignalworksheet.Cells[2, 1, 2, 1].Style.Font.Bold = true;
            exelSignalworksheet.Cells[2, 2, 2, 2].Value = "DateTime: " + DateTime.Now;

            exelSignalworksheet.Cells[4, 1, 4, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            exelSignalworksheet.Cells[4, 1, 4, 8].Value = "Inferences from Power Signal";
            exelSignalworksheet.Cells[4, 1, 4, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
            exelSignalworksheet.Cells[4, 1, 4, 8].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(209, 242, 208));
            exelSignalworksheet.Cells[4, 1, 4, 8].Merge = true;
            exelSignalworksheet.Cells[4, 1, 4, 8].Style.Font.Size = 14;
            exelSignalworksheet.Cells[4, 1, 4, 8].Style.Font.Bold = true;
            exelSignalworksheet.Cells[4, 1, 4, 8].Style.Font.Color.SetColor(Color.FromArgb(78, 73, 73));
            DataTable dtsignal = new DataTable();
            dtsignal = DBAccess.omBindInferenceSignal(sdocPlungeCat);
            int cellRowSignal = 5;
            for (int i = 0; i < dtsignal.Columns.Count; i++)
            {
                exelSignalworksheet.Cells[cellRowSignal, i + 1].Value = dtsignal.Columns[i].ColumnName.ToString();
                exelSignalworksheet.Cells[cellRowSignal, i + 1].Style.Font.Bold = true;
            }
            cellRowSignal++;
            for (int i = 0; i < dtsignal.Rows.Count; i++)
            {
                for (int j = 0; j < dtsignal.Columns.Count; j++)
                {
                    exelSignalworksheet.Cells[cellRowSignal, j + 1].Value = dtsignal.Rows[i][j].ToString();
                }
                cellRowSignal++;
            }
            for (int i = 1; i <= dtsignal.Columns.Count; i++)
            {
                exelSignalworksheet.Cells[4, i, dtsignal.Rows.Count + 1, i].AutoFitColumns();
            }

            #endregion

            #region ----Images----
            var exelImageworksheet = Excel.Workbook.Worksheets.Add("Images");
            setWorkSheetSetting(exelImageworksheet);
            exelImageworksheet.Cells[1, 1, 1, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //Color backcolor = Color.FromArgb(224, 76, 76);
            exelImageworksheet.Cells[1, 1, 1, 8].Value = "Automation of Grinding Process Intelligence (AGI)";
            exelImageworksheet.Cells[1, 1, 1, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
            exelImageworksheet.Cells[1, 1, 1, 8].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(231, 231, 231));
            exelImageworksheet.Cells[1, 1, 1, 8].Merge = true;
            exelImageworksheet.Cells[1, 1, 1, 8].Style.Font.Size = 18;
            exelImageworksheet.Cells[1, 1, 1, 8].Style.Font.Bold = true;
            exelImageworksheet.Cells[1, 1, 1, 8].Style.Font.Color.SetColor(Color.Red);

            exelImageworksheet.Cells[2, 1, 2, 1].Value = "Username: " + Session["EmpName"].ToString();
            exelImageworksheet.Cells[2, 2, 2, 2].Style.Font.Bold = true;
            exelImageworksheet.Cells[2, 1, 2, 1].Style.Font.Bold = true;
            exelImageworksheet.Cells[2, 2, 2, 2].Value = "DateTime: " + DateTime.Now;

            exelImageworksheet.Cells[4, 1, 4, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            exelImageworksheet.Cells[4, 1, 4, 8].Value = "Images";
            exelImageworksheet.Cells[4, 1, 4, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
            exelImageworksheet.Cells[4, 1, 4, 8].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(209, 242, 208));
            exelImageworksheet.Cells[4, 1, 4, 8].Merge = true;
            exelImageworksheet.Cells[4, 1, 4, 8].Style.Font.Size = 14;
            exelImageworksheet.Cells[4, 1, 4, 8].Style.Font.Bold = true;
            exelImageworksheet.Cells[4, 1, 4, 8].Style.Font.Color.SetColor(Color.FromArgb(78, 73, 73));

            List<SdocImages> listImage = new List<SdocImages>();
            listImage = DBAccess.omBindImages(sdocPlungeCat);
            int cellRowImages = 5;
            try
            {
                int k = 0;
                foreach (SdocImages data in listImage)
                {
                    exelImageworksheet.Cells[cellRowImages, 1].Value = data.SdocName;
                    exelImageworksheet.Cells[cellRowImages, 1].Style.Font.Size = 13;
                    exelImageworksheet.Cells[cellRowImages, 1].Style.Font.Bold = true;
                    exelImageworksheet.Cells[cellRowImages, 1].Style.Font.Color.SetColor(Color.OrangeRed);
                    cellRowImages++;
                    for (int j = 0; j < data.Values.Count; j++)
                    {
                        try
                        {
                            int col = (j * 5) + 1;
                            exelImageworksheet.Cells[cellRowImages, col, cellRowImages, col + 3].Value = data.Values[j].wpImageName;
                            exelImageworksheet.Cells[cellRowImages, col, cellRowImages, col + 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            exelImageworksheet.Cells[cellRowImages, col, cellRowImages, col + 3].Merge = true;

                            string path = data.Values[j].wpImagePath;
                            System.Drawing.Image img = System.Drawing.Image.FromFile(Server.MapPath(path));
                            ExcelPicture pic = exelImageworksheet.Drawings.AddPicture(data.Values[j].wpImageName + "_" + k + j, img);
                            pic.SetPosition(cellRowImages, 0, (j * 4) + (1 * j), 0);
                            pic.SetSize(200, 200);
                            exelImageworksheet.Protection.IsProtected = false;
                            exelImageworksheet.Protection.AllowSelectLockedCells = false;
                        }
                        catch (Exception e1)
                        { }
                    }
                    cellRowImages += 12;
                    k++;
                }

              
            }
            catch (Exception ex)
            { }
            #endregion
            DownloadFile(destination, Excel.GetAsByteArray());
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

        protected void ddlSdocID_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlChkPlunges.DataSource = DBAccess.getOMPlunges(ddlSdocID.SelectedItem.Text);
            ddlChkPlunges.DataBind();
            if(ddlChkPlunges.Items.Count>0)
            {
                ddlChkPlunges.SelectedIndex = 0;
            }
            ddlChkSubCatogery.DataSource = DBAccess.getOMSubCategoryBasedOnPlunges(ddlSdocID.SelectedItem.Text, ddlChkPlunges.SelectedItem.Text);
            ddlChkSubCatogery.DataBind();
            if(ddlChkSubCatogery.Items.Count>0)
            {
                ddlChkSubCatogery.SelectedIndex = 0;
            }
        }

        protected void ddlChkPlunges_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            foreach (System.Web.UI.WebControls.ListItem data in ddlChkPlunges.Items)
            {
                if (data.Selected)
                {
                    list.Add(data.Text);
                }
            }
            StringBuilder str = new StringBuilder();
            str.Append("");
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (i == list.Count - 1)
                    {
                        str.Append(list[i]);
                    }
                    else
                    {
                        str.Append(list[i] + ",");
                    }
                }
            }
            catch (Exception ex)
            { }
            ddlChkSubCatogery.DataSource = DBAccess.getOMSubCategoryBasedOnPlunges(ddlSdocID.SelectedItem.Text, str.ToString());
            ddlChkSubCatogery.DataBind();
            if(ddlChkSubCatogery.Items.Count>0)
            {
                ddlChkSubCatogery.SelectedIndex = 0;
            }
        }



        protected void viewBtn_Click(object sender, EventArgs e)
        {
            bindGeneralInfo(setSdocPlungeCategory(), "");
            bindQualityParameter(setSdocPlungeCategory(), "");
            bindDerivedParameters(setSdocPlungeCategory(), "");
            bindInferenceSignal(setSdocPlungeCategory());
            bindImages(setSdocPlungeCategory());
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
                DataTable dt = DBAccess.omBindGeneralInfo(setSdocPlungeCategory(), parameter.ToString(), "GeneralParmNonDefault");
                gvGeneralInfo.DataSource=removeBlankValuesFromGeneralInfoGrid(dt);
                gvGeneralInfo.DataBind();
                checkParameterToPanelList();
                //bindGeneralInfo(setSdocPlungeCategory(), parameter.ToString());
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
               

                DataTable dt=DBAccess.omBindGeneralInfo(setSdocPlungeCategory(), parameter.ToString(), "GeneralParmNonDefault");
                gvGeneralInfo.DataSource = removeBlankValuesFromGeneralInfoGrid(dt);
                gvGeneralInfo.DataBind();
                checkParameterToPanelList();
                // bindGeneralInfo(setSdocPlungeCategory(), parameter.ToString());
            }

            Session["GeneralInfoParameters"] = parameter.ToString();
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
                List<QualityParam> listQuality=DBAccess.omBindQualityParam(setSdocPlungeCategory(), parameter.ToString(), "QualityParmNonDefault");
                lvQualityParam.DataSource = removeBlankValuesFromQualityGrid(listQuality);
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
                List<QualityParam> listQuality = DBAccess.omBindQualityParam(setSdocPlungeCategory(), parameter.ToString(), "QualityParmNonDefault");
                lvQualityParam.DataSource = removeBlankValuesFromQualityGrid(listQuality);
                lvQualityParam.DataBind();
                checkQualityParameterToPanelList();
                //bindQualityParameter(setSdocPlungeCategory(), parameter.ToString());
            }
            Session["QualityParameters"] = parameter.ToString();
        }

        protected void btnPDF_Click(object sender, EventArgs e)
        {
            try
            {
                if(hfImageData.Value!="")
                {
                    string str =  hfImageData.Value.ToString();
                    string base64 = Request.Form[hfImageData.UniqueID].Split(',')[1];
                }
              
                Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);


                //// calling PDFFooter class to Include in document
                //pdfWriter.PageEvent = new PDFFooter();

                pdfDoc.Open();
                byte[] file;
                file = System.IO.File.ReadAllBytes(Server.MapPath("~/Images/microgrinding.png"));//ImagePath  
                iTextSharp.text.Image jpg1 = iTextSharp.text.Image.GetInstance(file);
                jpg1.ScaleToFit(50f, 20f);//Set width and height in float  
                                          // jpg1.Alignment = 2;                        // pdfTable1.AddCell(jpg1);
                                          // jpg1.SetAbsolutePosition(36, PdfWriter.ve(true));
                                          //pdfDoc.Add(jpg1);

                PdfPTable pdfTable1 = new PdfPTable(3);
                //pdfTable1.DefaultCell.Padding = 5;
                pdfTable1.WidthPercentage = 100;
                pdfTable1.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                pdfTable1.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                //pdfTable1.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170);
                pdfTable1.DefaultCell.BorderWidth = 0;

                PdfPCell cell = new PdfPCell(jpg1, false);
                cell.BorderWidth = 1;
                cell.Padding = 1;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //cell.Width = 50f;
                pdfTable1.AddCell(cell);

                Chunk c1 = new Chunk("TECHNICAL INFORMATION NOTE", FontFactory.GetFont("Times New Roman"));
                c1.Font.Color = new BaseColor(0, 0, 0);
                c1.Font.SetStyle(0);
                c1.Font.Size = 9;
                Phrase p1 = new Phrase();
                p1.Add(c1);
                PdfPCell cellHeader = new PdfPCell(p1);
                // cellHeader.Colspan = 2;
                cellHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                cellHeader.VerticalAlignment = Element.ALIGN_MIDDLE;
                cellHeader.BorderWidth = 1;
                pdfTable1.AddCell(cellHeader);

                Chunk date = new Chunk(DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt"));
                date.Font.SetStyle(0);
                date.Font.Size = 8;
                PdfPCell dateCell = new PdfPCell(new Phrase(date));
                dateCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                dateCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                dateCell.BorderWidth = 1;
                pdfTable1.AddCell(dateCell);
                pdfDoc.Add(pdfTable1);
                //// for line
                Paragraph lineSeparator = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                // Set gap between line paragraphs.
                lineSeparator.SetLeading(0.5F, 0.5F);
                pdfDoc.Add(lineSeparator);
                List<string> sdocDetails = new List<string>();
                int noOfSdoc = gvGeneralInfo.HeaderRow.Cells.Count - 6;
                if (noOfSdoc > 0)
                {
                    for (int i = 3; i < gvGeneralInfo.HeaderRow.Cells.Count-3; i++)
                    {
                        sdocDetails.Add(gvGeneralInfo.HeaderRow.Cells[i].Text);
                    }
                }
                else
                {
                    return;
                }
                int generalInfoCount = 3, inferenceCount = 1, imageCount = 0, qlyCount = 0, calParamCount = 1;
                //DataTable calcParamTable = (DataTable)Session["CalcParamGraphData"];
                //string calParamStr = hdnCalcParamGraph.Value.Trim().Remove(hdnCalcParamGraph.Value.Trim().Length - 1);
                //List<string> calcParamGraph = calParamStr.Split('$').ToList();
                //string s1 = hdnCalcParamGraph.Value;
                //List<string> calcParamGraphData = new List<string>();
                //for (int i = 0; i < calcParamGraph.Count; i++)
                //{
                //    calcParamGraphData.Add(calcParamGraph[i].Split(',')[1]);
                //}
                for (int sdocCount = 0; sdocCount < sdocDetails.Count; sdocCount++)
                {
                    //Paragraph SDocParam = new Paragraph(sdocDetails[sdocCount]);
                    //SDocParam.Alignment = Element.ALIGN_CENTER;
                    //pdfDoc.Add(SDocParam);
                    // calling PDFFooter class to Include in document
                    if(sdocCount>0)
                    {
                        //Session["OMSDoc"] = "";
                        //pdfWriter.PageEvent = new PDFFooter();
                        Paragraph p2 = new Paragraph();
                        pdfDoc.Add(p2);
                        pdfDoc.NewPage();
                    }
                    Session["OMSDoc"] = sdocDetails[sdocCount];
                   // Session["OMPrev"] = sdocDetails[sdocCount];
                    pdfWriter.PageEvent = new PDFFooter();

                    #region  ---- General Info -----
                    List<string> generalDetails = new List<string>();
                    List<string> machineDetails = new List<string>();
                    List<string> consumableDetails = new List<string>();
                    List<string> workpieceDetails = new List<string>();
                    List<string> operationalDetails = new List<string>();
                    int gCount = 0, mCount = 0, cCount = 0, wCount = 0, oCount = 0;
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
                                continue;
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
                                continue;
                            }
                            machineDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                            machineDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
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
                                continue;
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
                                continue;
                            }
                            if (gvGeneralInfo.Rows[i].Cells[2].Text == "Hardness")                            {                                string hardnessUnit = string.Empty;                                hardnessUnit = DBAccess.getHardnessUnit(sdocDetails[sdocCount]);                                workpieceDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);                                workpieceDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text + "  " + hardnessUnit);                                continue;                            }
                            if (gvGeneralInfo.Rows[i].Cells[2].Text == "Hardness Unit")                            {                                                                continue;                            }
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
                                continue;
                            }
                            operationalDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                            operationalDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                        }
                    }
                    generalInfoCount++;
                    PdfPTable mainTbl = new PdfPTable(2);
                    mainTbl.WidthPercentage = 100;
                    mainTbl.SpacingBefore = 7;
                    mainTbl.SpacingAfter = 7;
                    PdfPCell headingCell = getPdfCellWithBoldText("Input");
                    headingCell.Colspan = 2;
                    headingCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    mainTbl.AddCell(headingCell);
                    if (generalDetails.Count > 0)
                    {
                        PdfPCell mcell = new PdfPCell();
                        PdfPTable generalDetailsTbl = new PdfPTable(2);
                        generalDetailsTbl.WidthPercentage = 90;
                        generalDetailsTbl.SpacingBefore = 7;
                        generalDetailsTbl.SpacingAfter = 7;
                        for (int i = 0; i < generalDetails.Count; i++)
                        {
                            if (i == 0)
                            {
                                PdfPCell headerCell = getPdfCellWithBoldText(generalDetails[i]);
                                headerCell.Colspan = 2;
                                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                generalDetailsTbl.AddCell(headerCell);
                            }
                            else if ((i % 2) == 0)
                            {
                                generalDetailsTbl.AddCell(getPdfCell(generalDetails[i]));
                            }
                            else
                            {
                                generalDetailsTbl.AddCell(getPdfCell(generalDetails[i]));
                            }
                        }
                        mcell.AddElement(generalDetailsTbl);
                        mainTbl.AddCell(mcell);
                    }
                    if (machineDetails.Count > 0)
                    {
                        PdfPCell mcell = new PdfPCell();
                        PdfPTable machineDetailsTbl = new PdfPTable(2);
                        machineDetailsTbl.WidthPercentage = 90;
                        machineDetailsTbl.SpacingBefore = 7;
                        machineDetailsTbl.SpacingAfter = 7;
                        for (int i = 0; i < machineDetails.Count; i++)
                        {
                            if (i == 0)
                            {
                                PdfPCell headerCell = getPdfCellWithBoldText(machineDetails[i]);
                                headerCell.Colspan = 2;
                                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                machineDetailsTbl.AddCell(headerCell);
                            }
                            else if ((i % 2) == 0)
                            {
                                machineDetailsTbl.AddCell(getPdfCell(machineDetails[i]));
                            }
                            else
                            {
                                machineDetailsTbl.AddCell(getPdfCell(machineDetails[i]));
                            }
                        }
                        mcell.AddElement(machineDetailsTbl);
                        mainTbl.AddCell(mcell);
                    }
                    if (consumableDetails.Count > 0)
                    {
                        PdfPCell mcell = new PdfPCell();
                        PdfPTable DetailsTbl = new PdfPTable(2);
                        DetailsTbl.WidthPercentage = 90;
                        DetailsTbl.SpacingBefore = 7;
                        DetailsTbl.SpacingAfter = 7;
                        for (int i = 0; i < consumableDetails.Count; i++)
                        {
                            if (i == 0)
                            {
                                PdfPCell headerCell = getPdfCellWithBoldText(consumableDetails[i]);
                                headerCell.Colspan = 2;
                                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                DetailsTbl.AddCell(headerCell);
                            }
                            else if ((i % 2) == 0)
                            {
                                DetailsTbl.AddCell(getPdfCell(consumableDetails[i]));
                            }
                            else
                            {
                                DetailsTbl.AddCell(getPdfCell(consumableDetails[i]));
                            }
                        }
                        mcell.AddElement(DetailsTbl);
                        mainTbl.AddCell(mcell);
                    }
                    if (workpieceDetails.Count > 0)
                    {
                        PdfPCell mcell = new PdfPCell();
                        PdfPTable DetailsTbl = new PdfPTable(2);
                        DetailsTbl.WidthPercentage = 90;
                        DetailsTbl.SpacingBefore = 7;
                        DetailsTbl.SpacingAfter = 7;
                        for (int i = 0; i < workpieceDetails.Count; i++)
                        {
                            if (i == 0)
                            {
                                PdfPCell headerCell = getPdfCellWithBoldText(workpieceDetails[i]);
                                headerCell.Colspan = 2;
                                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                DetailsTbl.AddCell(headerCell);
                            }
                            else if ((i % 2) == 0)
                            {
                                DetailsTbl.AddCell(getPdfCell(workpieceDetails[i]));
                            }
                            else
                            {
                                DetailsTbl.AddCell(getPdfCell(workpieceDetails[i]));
                            }
                        }
                        mcell.AddElement(DetailsTbl);
                        mainTbl.AddCell(mcell);
                    }
                    if (operationalDetails.Count > 0)
                    {
                        PdfPCell mcell = new PdfPCell();
                        PdfPTable DetailsTbl = new PdfPTable(2);
                        DetailsTbl.WidthPercentage = 90;
                        DetailsTbl.SpacingBefore = 7;
                        DetailsTbl.SpacingAfter = 7;
                        for (int i = 0; i < operationalDetails.Count; i++)
                        {
                            if (i == 0)
                            {
                                PdfPCell headerCell = getPdfCellWithBoldText(operationalDetails[i]);
                                headerCell.Colspan = 2;
                                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                DetailsTbl.AddCell(headerCell);
                            }
                            else if ((i % 2) == 0)
                            {
                                DetailsTbl.AddCell(getPdfCell(operationalDetails[i]));
                            }
                            else
                            {
                                DetailsTbl.AddCell(getPdfCell(operationalDetails[i]));
                            }
                        }
                        mcell.AddElement(DetailsTbl);
                        mainTbl.AddCell(mcell);
                    }
                    if (gvInferenceSignal.Rows.Count > 0)
                    {
                        PdfPCell mcell = new PdfPCell();
                        //PdfPTable DetailsTbl = new PdfPTable(2);
                        //DetailsTbl.WidthPercentage = 90;
                        //DetailsTbl.SpacingBefore = 7;
                        //DetailsTbl.SpacingAfter = 7;
                        //PdfPCell headerCell = getPdfCellWithBoldText("Inferences from Power Signal");
                        //headerCell.Colspan = 2;
                        //headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        //DetailsTbl.AddCell(headerCell);
                        //for (int i = 0; i < gvInferenceSignal.Rows.Count; i++)
                        //{
                        //    if (gvInferenceSignal.Rows[i].Cells[inferenceCount].Text == "&nbsp;")
                        //    {
                        //        continue;
                        //    }
                        //    DetailsTbl.AddCell(getPdfCell(gvInferenceSignal.Rows[i].Cells[0].Text));
                        //    DetailsTbl.AddCell(getPdfCell(gvInferenceSignal.Rows[i].Cells[inferenceCount].Text));
                        //}
                        //inferenceCount++;
                        //mcell.AddElement(DetailsTbl);
                        mainTbl.AddCell(mcell);
                    }
                    pdfDoc.Add(mainTbl);
                    #endregion
                    #region  ------- Images --------------
                    PdfPTable imageDetails = new PdfPTable(2);
                    imageDetails.WidthPercentage = 100;
                    imageDetails.SpacingAfter = 7;
                    imageDetails.SpacingBefore = 7;
                    if (lvImages.Items.Count > 0)
                    {
                        PdfPCell imgheaderCell = getPdfCellWithBoldText("Process");
                        imgheaderCell.Colspan = 2;
                        imgheaderCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        imageDetails.AddCell(imgheaderCell);
                        ListView imageListview = lvImages.Items[imageCount].FindControl("lvImageDetails") as ListView;
                        imageCount++;
                        for (int i = 0; i < imageListview.Items.Count; i++)
                        {
                            try
                            {
                                if(i==0)
                                {
                                    PdfPCell imgCell = new PdfPCell();
                                    imgCell.Colspan = 2;
                                    PdfPTable imageDetails1 = new PdfPTable(1);
                                    imageDetails1.WidthPercentage = 90;
                                    imageDetails1.SpacingBefore = 3;
                                    imageDetails1.SpacingAfter = 3;
                                    imageDetails1.AddCell(getPdfCell((imageListview.Items[i].FindControl("imgName") as HtmlGenericControl).InnerText));
                                    string path = (imageListview.Items[i].FindControl("hdnImagePath") as HiddenField).Value;
                                    byte[] imgfile;
                                    imgfile = System.IO.File.ReadAllBytes(Server.MapPath(path));//ImagePath  
                                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgfile);
                                    PdfPCell imgCell1 = new PdfPCell(img, true);
                                    imageDetails1.AddCell(imgCell1);
                                    //img.ScaleToFit(50f, 30f);
                                    imgCell.AddElement(imageDetails1);
                                    imageDetails.AddCell(imgCell);
                                }
                                else
                                {
                                    PdfPCell imgCell = new PdfPCell();
                                    PdfPTable imageDetails1 = new PdfPTable(1);
                                    imageDetails1.WidthPercentage = 90;
                                    imageDetails1.SpacingBefore = 3;
                                    imageDetails1.SpacingAfter = 3;
                                    imageDetails1.AddCell(getPdfCell((imageListview.Items[i].FindControl("imgName") as HtmlGenericControl).InnerText));
                                    string path = (imageListview.Items[i].FindControl("hdnImagePath") as HiddenField).Value;
                                    byte[] imgfile;
                                    imgfile = System.IO.File.ReadAllBytes(Server.MapPath(path));//ImagePath  
                                    iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgfile);
                                    PdfPCell imgCell1 = new PdfPCell(img, true);
                                    imageDetails1.AddCell(imgCell1);
                                    //img.ScaleToFit(50f, 30f);
                                    imgCell.AddElement(imageDetails1);
                                    imageDetails.AddCell(imgCell);
                                }
                                
                            }
                            catch (Exception ex1)
                            { }
                        }
                        if (imageListview.Items.Count % 2 == 0)
                        {
                            PdfPCell imgCell = new PdfPCell();
                            imageDetails.AddCell(imgCell);
                        }

                    }

                    //byte[] bytes = Convert.FromBase64String(base64);
                    //iTextSharp.text.Image imggraph = iTextSharp.text.Image.GetInstance(bytes);
                    //PdfPCell imgCell2 = new PdfPCell(imggraph, true);
                    //imageDetails.AddCell(imgCell2);
                    //imageDetails.AddCell("Graph");

                    //string Html = "<img id='img1' src='"+hfImageData.Value+"' />";
                    //StringReader sr = new StringReader(Html);
                    //List<IElement> elements = HTMLWorker.ParseToList(sr, null);

                    //foreach (IElement e1 in elements)
                    //{
                    //    PdfPCell imgCell3 = new PdfPCell();
                    //    //Add those elements to the paragraph
                    //    imgCell3.AddElement(e1);
                    //    imageDetails.AddCell(imgCell3);
                    //}
                    pdfDoc.Add(imageDetails);
                    #endregion
                    #region  ----- Calculated Param Graph ---

                    //if (calcParamTable.Rows.Count > 0)
                    //{
                    //    PdfPTable calPramTbl = new PdfPTable(2);
                    //    calPramTbl.WidthPercentage = 100;
                    //    calPramTbl.SpacingAfter = 7;
                    //    calPramTbl.SpacingBefore = 7;
                    //    PdfPCell calParamCell1 = getPdfCellWithBoldText("Calculated Parameter");
                    //    calParamCell1.Colspan = 2;
                    //    calParamCell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    //    calPramTbl.AddCell(calParamCell1);
                    //    PdfPTable innerTbl = new PdfPTable(2);
                    //    innerTbl.AddCell(getPdfCellWithBoldText("Item"));
                    //    innerTbl.AddCell(getPdfCellWithBoldText("Value"));
                    //    for (int i = 0; i < calcParamTable.Rows.Count; i++)
                    //    {
                    //        innerTbl.AddCell(getPdfCell(calcParamTable.Rows[i][0].ToString()));
                    //        innerTbl.AddCell(getPdfCell(calcParamTable.Rows[i][calParamCount].ToString()));
                    //    }
                    //    calPramTbl.AddCell(innerTbl);
                    //    byte[] calcbytes = Convert.FromBase64String(calcParamGraphData[sdocCount]);
                    //    iTextSharp.text.Image calcimggraph = iTextSharp.text.Image.GetInstance(calcbytes);
                    //    PdfPCell calParamCell2 = new PdfPCell(calcimggraph, true);
                    //    calPramTbl.AddCell(calParamCell2);
                    //    calParamCount++;
                    //    pdfDoc.Add(calPramTbl);
                    //}

                    #endregion
                    #region  ----- Output ----------
                    if (lvQualityParam.Items.Count > 0)
                    {
                        int noOfQltItem = lvQualityParam.Items.Count;
                        
                        PdfPCell qlyCell = new PdfPCell();
                        PdfPTable qlyDetailsFinalTbl = new PdfPTable(2);
                        qlyDetailsFinalTbl.WidthPercentage = 100;
                        qlyDetailsFinalTbl.SpacingBefore = 7;
                        qlyDetailsFinalTbl.SpacingAfter = 7;
                        
                        PdfPCell qlyDetailsFinalCell = new PdfPCell();

                        PdfPTable qlyDetailsTbl = new PdfPTable(5);
                        qlyDetailsTbl.WidthPercentage = 100;
                        qlyDetailsTbl.SpacingBefore = 7;
                        qlyDetailsTbl.SpacingAfter = 7;
                       
                        PdfPTable qlyDetailsTbl2 = new PdfPTable(5);
                        qlyDetailsTbl2.WidthPercentage = 100;
                        qlyDetailsTbl2.SpacingBefore = 7;
                        qlyDetailsTbl2.SpacingAfter = 7;
                      
                        ListView qlyListview = (lvQualityParam.Items[qlyCount].FindControl("lvinnerQly") as ListView);
                        qlyCount++;
                        ListView dlyFinallistview = new ListView();

                        for (int i = 0; i < qlyListview.Items.Count; i++)
                        {
                            
                            
                            if (string.IsNullOrEmpty((qlyListview.Items[i].FindControl("lblTargetLower") as Label).Text) && string.IsNullOrEmpty((qlyListview.Items[i].FindControl("lblTargetUpper") as Label).Text) && string.IsNullOrEmpty((qlyListview.Items[i].FindControl("lblActualLower") as Label).Text) && string.IsNullOrEmpty((qlyListview.Items[i].FindControl("lblActualUpper") as Label).Text))
                            {
                                
                                continue;
                            }
                           else
                            {
                                dlyFinallistview.Items.Add(qlyListview.Items[i]);
                            //    table.Rows.Add((qlyListview.Items[i].FindControl("Qlyparam") as Label).Text, (qlyListview.Items[i].FindControl("lblTargetLower") as Label).Text, (qlyListview.Items[i].FindControl("lblTargetUpper") as Label).Text, (qlyListview.Items[i].FindControl("lblActualLower") as Label).Text, (qlyListview.Items[i].FindControl("lblActualUpper") as Label).Text);
                            }

                        }


                        if(dlyFinallistview.Items.Count==1)
                        {
                            PdfPCell qlyheaderCell = getPdfCellWithBoldText("Quality Parameters");
                            qlyheaderCell.Colspan = 2;
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
                        else if (dlyFinallistview.Items.Count > 1)
                        {
                            PdfPCell qlyheaderCell = getPdfCellWithBoldText("Quality Parameters");
                            qlyheaderCell.Colspan = 2;
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
                                PdfPCell qlyHeadercell2 = getPdfCellWithBoldText(name);
                                qlyHeadercell2.Colspan = colspanValue;
                                qlyHeadercell2.HorizontalAlignment = Element.ALIGN_CENTER;
                                qlyDetailsTbl2.AddCell(qlyHeadercell2);
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
                                PdfPCell qlyHeadercell2 = getPdfCellWithBoldText(name);
                                qlyHeadercell2.HorizontalAlignment = Element.ALIGN_CENTER;
                                qlyDetailsTbl2.AddCell(qlyHeadercell2);
                            }
                        }
                    
                        for (int j = 0; j < dlyFinallistview.Items.Count; j++)
                        {
                            if (j % 2 == 0)
                            {
                                //if (string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblTargetLower") as Label).Text) && string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblTargetUpper") as Label).Text) && string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblActualLower") as Label).Text) && string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblActualUpper") as Label).Text))
                                //{

                                //    continue;
                                //}
                                qlyDetailsTbl.AddCell(getPdfCellWithBoldText((dlyFinallistview.Items[j].FindControl("Qlyparam") as Label).Text));
                                qlyDetailsTbl.AddCell(getPdfCell((dlyFinallistview.Items[j].FindControl("lblTargetLower") as Label).Text));
                                qlyDetailsTbl.AddCell(getPdfCell((dlyFinallistview.Items[j].FindControl("lblTargetUpper") as Label).Text));
                                qlyDetailsTbl.AddCell(getPdfCell((dlyFinallistview.Items[j].FindControl("lblActualLower") as Label).Text));
                                qlyDetailsTbl.AddCell(getPdfCell((dlyFinallistview.Items[j].FindControl("lblActualUpper") as Label).Text));
                            }
                            else
                            {
                                //if (string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblTargetLower") as Label).Text) && string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblTargetUpper") as Label).Text) && string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblActualLower") as Label).Text) && string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblActualUpper") as Label).Text))
                                //{

                                //    continue;
                                //}
                                qlyDetailsTbl2.AddCell(getPdfCellWithBoldText((dlyFinallistview.Items[j].FindControl("Qlyparam") as Label).Text));
                                qlyDetailsTbl2.AddCell(getPdfCell((dlyFinallistview.Items[j].FindControl("lblTargetLower") as Label).Text));
                                qlyDetailsTbl2.AddCell(getPdfCell((dlyFinallistview.Items[j].FindControl("lblTargetUpper") as Label).Text));
                                qlyDetailsTbl2.AddCell(getPdfCell((dlyFinallistview.Items[j].FindControl("lblActualLower") as Label).Text));
                                qlyDetailsTbl2.AddCell(getPdfCell((dlyFinallistview.Items[j].FindControl("lblActualUpper") as Label).Text));
                            }
                        }

                        qlyDetailsFinalCell.AddElement(qlyDetailsTbl);
                        qlyDetailsFinalTbl.AddCell(qlyDetailsFinalCell);

                        qlyDetailsFinalCell = new PdfPCell();
                        qlyDetailsFinalCell.AddElement(qlyDetailsTbl2);
                        qlyDetailsFinalTbl.AddCell(qlyDetailsFinalCell);

                        pdfDoc.Add(qlyDetailsFinalTbl);
                    }
                    #endregion


                }
                #region  ------- Time Graph ------
                if(hfImageData.Value=="" || hfImageData.Value==null)
                {
                 
                }
                else
                {
                    PdfPTable totalCycleGraph = getGraphTable((List<string>)Session["TotalCycleTime"], hfImageData, "Total Cycle Time");
                    pdfDoc.Add(totalCycleGraph);
                }
                //if(hdnTimeGraph.Value=="" || hdnTimeGraph.Value ==null)
                //{
                //}
                //else
                //{
                //    PdfPTable grindTime = getGraphTable((List<string>)Session["GrindingTime"], hdnTimeGraph, "Grinding Time");
                //    pdfDoc.Add(grindTime);
                //}
                //if(hdnNonGrindTime.Value=="" || hdnNonGrindTime.Value == null)
                //{
                //}
                //else
                //{
                //    PdfPTable nonGrindTime = getGraphTable((List<string>)Session["NonGrindingTime"], hdnNonGrindTime, "Non Grinding Time");
                //    pdfDoc.Add(nonGrindTime);
                //}


                #endregion

                pdfWriter.CloseStream = false;
                pdfDoc.Close();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=AGIReport.pdf");
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
        public class PDFFooter : PdfPageEventHelper
        {
            // write on top of document
            //public override void OnOpenDocument(PdfWriter writer, Document document)
            //{
            //    base.OnOpenDocument(writer, document);
            //    PdfPTable tabFot = new PdfPTable(new float[] { 1F });
            //    tabFot.SpacingAfter = 10F;
            //    PdfPCell cell;
            //    tabFot.TotalWidth = 300F;
            //    cell = new PdfPCell(new Phrase("Header"));
            //    tabFot.AddCell(cell);
            //    tabFot.WriteSelectedRows(0, -1, 150, document.Top, writer.DirectContent);
            //}

            //// write on start of each page
            //public override void OnStartPage(PdfWriter writer, Document document)
            //{
            //    base.OnStartPage(writer, document);
            //}
               
          //  string SDoc = HttpContext.Current.Session["OMSDoc"].ToString();
            //OutputModules outputModules = new OutputModules();

            // write on end of each page
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                try
                {
                    base.OnEndPage(writer, document);
                    PdfPTable tabFot = new PdfPTable(1);
                    tabFot.DefaultCell.BorderWidth = 0;
                    tabFot.TotalWidth = 150F;
                    PdfPCell cell=new PdfPCell();
                    string SDoc = HttpContext.Current.Session["OMSDoc"].ToString();
                    Chunk chunk = new Chunk(SDoc);
                    chunk.Font.Color = BaseColor.BLACK;
                    chunk.Font.SetStyle(0);
                    chunk.Font.Size = 7;
                    Phrase phrase = new Phrase();
                    phrase.Add(chunk);
                    cell = new PdfPCell(phrase);
                    tabFot.AddCell(cell);
                    tabFot.WriteSelectedRows(0, -1, 430, document.Bottom, writer.DirectContent);
                }
                catch (Exception ex)
                {

                }
               
            }

            //write on close of document
            public override void OnCloseDocument(PdfWriter writer, Document document)
            {
                base.OnCloseDocument(writer, document);
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
            chunk.Font.Size = 7;
            Phrase phrase = new Phrase();
            phrase.Add(chunk);
            PdfPCell cell = new PdfPCell(phrase);
            cell.ExtraParagraphSpace = 3;
            cell.BorderColor = new BaseColor(70, 70, 70);
            return cell;
        }
        private PdfPTable getGraphTable(List<string> listData, HiddenField hdnField, string header)
        {
            PdfPTable timeGraph = new PdfPTable(2);
            timeGraph.WidthPercentage = 100;
            timeGraph.SpacingAfter = 7;
            timeGraph.SpacingBefore = 7;
            PdfPCell tblHeader = getPdfCellWithBoldText(header);
            tblHeader.Colspan = 2;
            tblHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            timeGraph.AddCell(tblHeader);
            try
            {
                PdfPCell timeGraphCell1 = new PdfPCell();
                PdfPTable timeGraphData = new PdfPTable(2);
                List<string> data = listData;
                if (data.Count > 0)
                {

                    timeGraphData.AddCell(getPdfCellWithBoldText("Item"));
                    timeGraphData.AddCell(getPdfCellWithBoldText("Value"));
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (i % 2 == 0)
                        {
                            timeGraphData.AddCell(getPdfCell(data[i]));
                        }
                        else
                        {
                            timeGraphData.AddCell(getPdfCell(data[i]));
                        }
                    }
                    timeGraph.AddCell(timeGraphData);
                   
                    string base64string = Request.Form[hdnField.UniqueID].Split(',')[1];
                    byte[] bytesData = Convert.FromBase64String(base64string);
                    iTextSharp.text.Image imggraph = iTextSharp.text.Image.GetInstance(bytesData);
                    PdfPCell timeGraphimg = new PdfPCell(imggraph, true);
                    timeGraphimg.Rowspan = 2;
                    timeGraph.AddCell(timeGraphimg);

                    timeGraph.AddCell(getPdfCell(""));
                }
            }
            catch (Exception ex)
            { }
            return timeGraph;
        }
        private PdfPTable getTable(List<string> listcalculatedcycletime,List<string> listactalcycletime, HiddenField hdnField, string header)
        {
            PdfPTable timeGraph = new PdfPTable(2);
            timeGraph.WidthPercentage = 100;
            timeGraph.SpacingAfter = 7;
            timeGraph.SpacingBefore = 7;
            PdfPCell tblHeader = getPdfCellWithBoldText(header);
            tblHeader.Colspan = 2;
            tblHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            timeGraph.AddCell(tblHeader);
            try
            {
                PdfPCell timeGraphCell1 = new PdfPCell();
                PdfPTable timeGraphData = new PdfPTable(2);
                List<string> data = listcalculatedcycletime;
                List<string> data2 = listactalcycletime;
                if (data.Count > 0)
                {
                    PdfPCell tblHeader1 = getPdfCellWithBoldText("Calculated Cycle Time");
                    tblHeader1.Colspan = 2;
                    tblHeader1.HorizontalAlignment = Element.ALIGN_CENTER;
                    timeGraphData.AddCell(tblHeader1);
                    timeGraphData.AddCell(getPdfCellWithBoldText("Item"));
                    timeGraphData.AddCell(getPdfCellWithBoldText("Value"));
                    for (int i = 0; i < data.Count; i++)
                    {
                        if (i % 2 == 0)
                        {
                            timeGraphData.AddCell(getPdfCell(data[i]));
                        }
                        else
                        {
                            timeGraphData.AddCell(getPdfCell(data[i]));
                        }
                    }
                    PdfPCell tblHeader3= new PdfPCell(new Phrase(""));
                    tblHeader3.Colspan = 2;
                    tblHeader3.HorizontalAlignment = Element.ALIGN_CENTER;
                    tblHeader3.Border = 0;
                    tblHeader3.BackgroundColor=BaseColor.WHITE;
                    timeGraphData.AddCell(tblHeader3);

                    PdfPCell tblHeader2 = getPdfCellWithBoldText("Actual Cycle Time");
                    tblHeader2.Colspan = 2;
                    tblHeader2.HorizontalAlignment = Element.ALIGN_CENTER;
                    timeGraphData.AddCell(tblHeader2);
                    timeGraphData.AddCell(getPdfCellWithBoldText("Item"));
                    timeGraphData.AddCell(getPdfCellWithBoldText("Value"));
                    for (int i = 0; i < data2.Count; i++)
                    {
                        if (i % 2 == 0)
                        {
                            timeGraphData.AddCell(getPdfCell(data2[i]));
                        }
                        else
                        {
                            timeGraphData.AddCell(getPdfCell(data2[i]));
                        }
                    }

                    timeGraph.AddCell(timeGraphData);

                    string base64string = Request.Form[hdnField.UniqueID].Split(',')[1];
                    byte[] bytesData = Convert.FromBase64String(base64string);
                    iTextSharp.text.Image imggraph = iTextSharp.text.Image.GetInstance(bytesData);
                    PdfPCell timeGraphimg = new PdfPCell(imggraph, true);
                    timeGraph.AddCell(timeGraphimg);

                }
            }
            catch (Exception ex)
            { }
            return timeGraph;
        }
      

       
        protected void pdfOK_ServerClick(object sender, EventArgs e)
        {
            if(defaultReport.Checked)
            {
                try
                {
                    if (hdActualTimeGraph.Value != "")
                    {
                        string str = hdActualTimeGraph.Value.ToString();
                        string base64 = Request.Form[hdActualTimeGraph.UniqueID].Split(',')[1];
                    }

                    Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);


                    //// calling PDFFooter class to Include in document
                    //pdfWriter.PageEvent = new PDFFooter();

                    pdfDoc.Open();
                    byte[] file;
                    file = System.IO.File.ReadAllBytes(Server.MapPath("~/Images/micromaticlogo.jpg"));//ImagePath  
                    iTextSharp.text.Image jpg1 = iTextSharp.text.Image.GetInstance(file);
                    jpg1.BackgroundColor = new iTextSharp.text.BaseColor(255,255,255);
                    jpg1.ScaleToFit(100f, 50f);//Set width and height in float  
                                              // jpg1.Alignment = 2;                        // pdfTable1.AddCell(jpg1);
                                              // jpg1.SetAbsolutePosition(36, PdfWriter.ve(true));
                                              //pdfDoc.Add(jpg1);

                    PdfPTable pdfTable1 = new PdfPTable(5);
                    //pdfTable1.DefaultCell.Padding = 5;
                    pdfTable1.WidthPercentage = 100;
                    pdfTable1.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfTable1.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    //pdfTable1.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170);
                    pdfTable1.DefaultCell.BorderWidth = 0;

                    PdfPCell cell = new PdfPCell(jpg1, false);
                    cell.BorderWidth = 1;
                    cell.Padding = 1;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    //cell.Width = 50f;
                    pdfTable1.AddCell(cell);

                    Chunk c1 = new Chunk("TECHNICAL INFORMATION NOTE", FontFactory.GetFont("Times New Roman"));
                    c1.Font.Color = new BaseColor(0, 0, 0);
                    c1.Font.SetStyle(0);
                    c1.Font.Size = 14;
                    Phrase p1 = new Phrase();
                    p1.Add(c1);
                    PdfPCell cellHeader = new PdfPCell(p1);
                    // cellHeader.Colspan = 2;
                    cellHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    cellHeader.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cellHeader.BorderWidth = 1;
                    cellHeader.Colspan = 3;
                    pdfTable1.AddCell(cellHeader);

                    Chunk date = new Chunk(DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt"));
                    date.Font.SetStyle(0);
                    date.Font.Size = 8;
                    PdfPCell dateCell = new PdfPCell(new Phrase(date));
                    dateCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    dateCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    dateCell.BorderWidth = 1;
                    pdfTable1.AddCell(dateCell);
                    pdfDoc.Add(pdfTable1);
                    //// for line
                    Paragraph lineSeparator = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                    // Set gap between line paragraphs.
                    lineSeparator.SetLeading(0.5F, 0.5F);
                    pdfDoc.Add(lineSeparator);
                    List<string> sdocDetails = new List<string>();
                    string Sdocid = "";
                    int noOfSdoc = gvGeneralInfo.HeaderRow.Cells.Count - 6;
                    if (noOfSdoc > 0)
                    {
                        for (int i = 3; i < gvGeneralInfo.HeaderRow.Cells.Count - 3; i++)
                        {
                            sdocDetails.Add(gvGeneralInfo.HeaderRow.Cells[i].Text);
                            if(sdocDetails.Count==1)
                            {
                                Sdocid = gvGeneralInfo.HeaderRow.Cells[i].Text;
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                    int generalInfoCount = 3, inferenceCount = 1, imageCount = 0, qlyCount = 0, calParamCount = 1;
                    //DataTable calcParamTable = (DataTable)Session["CalcParamGraphData"];
                    //string calParamStr = hdnCalcParamGraph.Value.Trim().Remove(hdnCalcParamGraph.Value.Trim().Length - 1);
                    //List<string> calcParamGraph = calParamStr.Split('$').ToList();
                    //string s1 = hdnCalcParamGraph.Value;
                    //List<string> calcParamGraphData = new List<string>();
                    //for (int i = 0; i < calcParamGraph.Count; i++)
                    //{
                    //    calcParamGraphData.Add(calcParamGraph[i].Split(',')[1]);
                    //}
                    for (int sdocCount = 0; sdocCount < sdocDetails.Count; sdocCount++)
                    {
                        //Paragraph SDocParam = new Paragraph(sdocDetails[sdocCount]);
                        //SDocParam.Alignment = Element.ALIGN_CENTER;
                        //pdfDoc.Add(SDocParam);
                        // calling PDFFooter class to Include in document
                        if (sdocCount > 0)
                        {
                            //Session["OMSDoc"] = "";
                            //pdfWriter.PageEvent = new PDFFooter();
                            Paragraph p2 = new Paragraph();
                            pdfDoc.Add(p2);
                            pdfDoc.NewPage();
                        }
                        Session["OMSDoc"] = sdocDetails[sdocCount];
                        // Session["OMPrev"] = sdocDetails[sdocCount];
                        pdfWriter.PageEvent = new PDFFooter();

                        #region  ---- General Info -----
                        List<string> generalDetails = new List<string>();
                        List<string> machineDetails = new List<string>();
                        List<string> consumableDetails = new List<string>();
                        List<string> workpieceDetails = new List<string>();
                        List<string> operationalDetails = new List<string>();
                        List<string> gaugingDetails = new List<string>();
                        List<string> coolantDetails = new List<string>();
                        int gCount = 0, mCount = 0, cCount = 0, wCount = 0, oCount = 0,gDCount=0,cDCount=0;
                        for (int i = 0; i < gvGeneralInfo.Rows.Count; i++)
                        {
                            string inputModule = gvGeneralInfo.Rows[i].Cells[1].Text;
                            if (inputModule == "General Information")
                            {
                                //if (gCount == 0)
                                //{
                                //    generalDetails.Add("General Information");
                                //    gCount++;
                                //}
                                if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                                {
                                    continue;
                                }

                                generalDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                                generalDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                            }

                            else if (inputModule == "Machine Tool")
                            {
                               
                                if(gvGeneralInfo.Rows[i].Cells[2].Text == "Steady Rest" || gvGeneralInfo.Rows[i].Cells[2].Text == "Flagging Type" || gvGeneralInfo.Rows[i].Cells[2].Text == "Flagging Make" || gvGeneralInfo.Rows[i].Cells[2].Text == "Wheel Balancer Type" || gvGeneralInfo.Rows[i].Cells[2].Text == "Wheel Balancer Make" || gvGeneralInfo.Rows[i].Cells[2].Text.Replace("&amp;","&") == "Gap & Crash Make")
                                {
                                    //if (gDCount == 0)
                                    //{
                                    //    gaugingDetails.Add("Gauging System");
                                    //    gDCount++;
                                    //}
                                    if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                                    {
                                        continue;
                                    }
                                    gaugingDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                                    gaugingDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                                }
                                else
                                {
                                    //if (mCount == 0)
                                    //{
                                    //    machineDetails.Add("Machine Tool");
                                    //    mCount++;
                                    //}
                                    if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                                    {
                                        continue;
                                    }
                                    machineDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                                    machineDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                                }
                            }
                            else if (inputModule == "Consumables Details")
                            {
                                if(gvGeneralInfo.Rows[i].Cells[2].Text == "Coolant Type" || gvGeneralInfo.Rows[i].Cells[2].Text == "Coolant Make")
                                {
                                    //if (cDCount == 0)
                                    //{
                                    //    coolantDetails.Add("Coolant Details");
                                    //    cDCount++;
                                    //}
                                    if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                                    {
                                        continue;
                                    }
                                    coolantDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                                    coolantDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                                }
                                else
                                {
                                    //if (cCount == 0)
                                    //{
                                    //    consumableDetails.Add("Consumables Details");
                                    //    cCount++;
                                    //}
                                    if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                                    {
                                        continue;
                                    }
                                    consumableDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                                    consumableDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                                }
                                   
                            }
                            else if (inputModule == "Workpiece Details")
                            {
                                //if (wCount == 0)
                                //{
                                //    workpieceDetails.Add("Workpiece Details");
                                //    wCount++;
                                //}
                                if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                                {
                                    continue;
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
                                if (gvGeneralInfo.Rows[i].Cells[2].Text == "Coolant Concentration %")
                                {
                                    //if (coolantDetails.Count<=0)
                                    //{
                                    //    if (cDCount == 0)
                                    //    {
                                    //        coolantDetails.Add("Coolant Details");
                                    //        cDCount++;
                                    //    }
                                    //}
                                   
                                    if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                                    {
                                        continue;
                                    }
                                    coolantDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                                    coolantDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                                }
                                else
                                {
                                    //if (oCount == 0)
                                    //{
                                    //    operationalDetails.Add("Operational Parameters");
                                    //    oCount++;
                                    //}
                                    if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                                    {
                                        continue;
                                    }
                                    operationalDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                                    operationalDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                                }
                               
                            }
                        }
                        generalInfoCount++;
                        PdfPTable mainTbl = new PdfPTable(2);
                        mainTbl.WidthPercentage = 100;
                        mainTbl.SpacingBefore = 7;
                        mainTbl.SpacingAfter = 7;
                        PdfPCell headingCell = getPdfCellWithBoldText("Input");
                        headingCell.Colspan = 2;
                        headingCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        mainTbl.AddCell(headingCell);
                        if (generalDetails.Count > 0)
                        {
                            PdfPCell mcell = new PdfPCell();
                            PdfPTable generalDetailsTbl = new PdfPTable(2);
                            generalDetailsTbl.WidthPercentage = 90;
                            generalDetailsTbl.SpacingBefore = 7;
                            generalDetailsTbl.SpacingAfter = 7;
                            for (int i = 0; i < generalDetails.Count; i++)
                            {
                                if (i == 0)
                                {
                                    PdfPCell headerCell = getPdfCellWithBoldText("General Information");
                                    headerCell.Colspan = 2;
                                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    generalDetailsTbl.AddCell(headerCell);
                                }
                                if ((i % 2) == 0)
                                {
                                    generalDetailsTbl.AddCell(getPdfCell(generalDetails[i].Replace("&amp;","&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                                else
                                {
                                    generalDetailsTbl.AddCell(getPdfCell(generalDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                            }
                            mcell.AddElement(generalDetailsTbl);
                            mainTbl.AddCell(mcell);
                        }
                        if (machineDetails.Count > 0)
                        {
                            PdfPCell mcell = new PdfPCell();
                            PdfPTable machineDetailsTbl = new PdfPTable(2);
                            machineDetailsTbl.WidthPercentage = 90;
                            machineDetailsTbl.SpacingBefore = 7;
                            machineDetailsTbl.SpacingAfter = 7;
                            for (int i = 0; i < machineDetails.Count; i++)
                            {
                                if (i == 0)
                                {
                                    PdfPCell headerCell = getPdfCellWithBoldText("Machine Tool");
                                    headerCell.Colspan = 2;
                                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    machineDetailsTbl.AddCell(headerCell);
                                }
                                if ((i % 2) == 0)
                                {
                                    machineDetailsTbl.AddCell(getPdfCell(machineDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                                else
                                {
                                    machineDetailsTbl.AddCell(getPdfCell(machineDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                            }
                            mcell.AddElement(machineDetailsTbl);
                            mainTbl.AddCell(mcell);
                        }
                        if (consumableDetails.Count > 0)
                        {
                            PdfPCell mcell = new PdfPCell();
                            PdfPTable DetailsTbl = new PdfPTable(2);
                            DetailsTbl.WidthPercentage = 90;
                            DetailsTbl.SpacingBefore = 7;
                            DetailsTbl.SpacingAfter = 7;
                            for (int i = 0; i < consumableDetails.Count; i++)
                            {
                                if (i == 0)
                                {
                                    PdfPCell headerCell = getPdfCellWithBoldText("Consumables Details");
                                    headerCell.Colspan = 2;
                                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    DetailsTbl.AddCell(headerCell);
                                }
                                if ((i % 2) == 0)
                                {
                                    DetailsTbl.AddCell(getPdfCell(consumableDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                                else
                                {
                                    DetailsTbl.AddCell(getPdfCell(consumableDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                            }
                            mcell.AddElement(DetailsTbl);
                            mainTbl.AddCell(mcell);
                        }
                        if (gaugingDetails.Count > 0)
                        {
                            PdfPCell mcell = new PdfPCell();
                            PdfPTable DetailsTbl = new PdfPTable(2);
                            DetailsTbl.WidthPercentage = 90;
                            DetailsTbl.SpacingBefore = 7;
                            DetailsTbl.SpacingAfter = 7;
                            for (int i = 0; i < gaugingDetails.Count; i++)
                            {
                                if (i == 0)
                                {
                                    PdfPCell headerCell = getPdfCellWithBoldText("Gauging System");
                                    headerCell.Colspan = 2;
                                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    DetailsTbl.AddCell(headerCell);
                                }
                                if ((i % 2) == 0)
                                {
                                    DetailsTbl.AddCell(getPdfCell(gaugingDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                                else
                                {
                                    DetailsTbl.AddCell(getPdfCell(gaugingDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                            }
                            mcell.AddElement(DetailsTbl);
                            mainTbl.AddCell(mcell);
                        }
                        if (coolantDetails.Count > 0)
                        {
                            PdfPCell mcell = new PdfPCell();
                            PdfPTable DetailsTbl = new PdfPTable(2);
                            DetailsTbl.WidthPercentage = 90;
                            DetailsTbl.SpacingBefore = 7;
                            DetailsTbl.SpacingAfter = 7;
                            for (int i = 0; i < coolantDetails.Count; i++)
                            {
                                if (i == 0)
                                {
                                    PdfPCell headerCell = getPdfCellWithBoldText("Coolant Details");
                                    headerCell.Colspan = 2;
                                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    DetailsTbl.AddCell(headerCell);
                                }
                                if ((i % 2) == 0)
                                {
                                    DetailsTbl.AddCell(getPdfCell(coolantDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                                else
                                {
                                    DetailsTbl.AddCell(getPdfCell(coolantDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                            }
                            mcell.AddElement(DetailsTbl);
                            mainTbl.AddCell(mcell);
                        }
                        if (workpieceDetails.Count > 0)
                        {
                            PdfPCell mcell = new PdfPCell();
                            PdfPTable DetailsTbl = new PdfPTable(2);
                            DetailsTbl.WidthPercentage = 90;
                            DetailsTbl.SpacingBefore = 7;
                            DetailsTbl.SpacingAfter = 7;
                            for (int i = 0; i < workpieceDetails.Count; i++)
                            {
                                if (i == 0)
                                {
                                    PdfPCell headerCell = getPdfCellWithBoldText("Workpiece Details");
                                    headerCell.Colspan = 2;
                                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    DetailsTbl.AddCell(headerCell);
                                }
                                if ((i % 2) == 0)
                                {
                                    DetailsTbl.AddCell(getPdfCell(workpieceDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                                else
                                {
                                    DetailsTbl.AddCell(getPdfCell(workpieceDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                            }
                            mcell.AddElement(DetailsTbl);
                            mainTbl.AddCell(mcell);
                        }
                        if (operationalDetails.Count > 0)
                        {
                            PdfPCell mcell = new PdfPCell();
                            PdfPTable DetailsTbl = new PdfPTable(2);
                            DetailsTbl.WidthPercentage = 90;
                            DetailsTbl.SpacingBefore = 7;
                            DetailsTbl.SpacingAfter = 7;
                            for (int i = 0; i < operationalDetails.Count; i++)
                            {
                                if (i == 0)
                                {
                                    PdfPCell headerCell = getPdfCellWithBoldText("Operational Parameters");
                                    headerCell.Colspan = 2;
                                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    DetailsTbl.AddCell(headerCell);
                                }
                                if ((i % 2) == 0)
                                {
                                    DetailsTbl.AddCell(getPdfCell(operationalDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                                else
                                {
                                    DetailsTbl.AddCell(getPdfCell(operationalDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                            }
                            mcell.AddElement(DetailsTbl);
                            mainTbl.AddCell(mcell);
                        }
                        if (gvInferenceSignal.Rows.Count > 0)
                        {
                            PdfPCell mcell = new PdfPCell();
                            //PdfPTable DetailsTbl = new PdfPTable(2);
                            //DetailsTbl.WidthPercentage = 90;
                            //DetailsTbl.SpacingBefore = 7;
                            //DetailsTbl.SpacingAfter = 7;
                            //PdfPCell headerCell = getPdfCellWithBoldText("Inferences from Power Signal");
                            //headerCell.Colspan = 2;
                            //headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            //DetailsTbl.AddCell(headerCell);
                            //for (int i = 0; i < gvInferenceSignal.Rows.Count; i++)
                            //{
                            //    if (gvInferenceSignal.Rows[i].Cells[inferenceCount].Text == "&nbsp;")
                            //    {
                            //        continue;
                            //    }
                            //    DetailsTbl.AddCell(getPdfCell(gvInferenceSignal.Rows[i].Cells[0].Text));
                            //    DetailsTbl.AddCell(getPdfCell(gvInferenceSignal.Rows[i].Cells[inferenceCount].Text));
                            //}
                            //inferenceCount++;
                            //mcell.AddElement(DetailsTbl);
                            mainTbl.AddCell(mcell);
                        }
                        pdfDoc.Add(mainTbl);
                        #endregion
                        #region  ------- Images --------------
                        PdfPTable imageDetails = new PdfPTable(2);
                        imageDetails.WidthPercentage = 100;
                        imageDetails.SpacingAfter = 7;
                        imageDetails.SpacingBefore = 7;
                        if (lvImages.Items.Count > 0)
                        {
                            try
                            {
                                if ((lvImages.Items[imageCount].FindControl("imgSdoc") as HiddenField).Value == sdocDetails[sdocCount].ToString())
                                {

                                    PdfPCell imgheaderCell = getPdfCellWithBoldText("Process");
                                    imgheaderCell.Colspan = 2;
                                    imgheaderCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    imageDetails.AddCell(imgheaderCell);
                                    ListView imageListview = lvImages.Items[imageCount].FindControl("lvImageDetails") as ListView;
                                    imageCount++;
                                    for (int i = 0; i < imageListview.Items.Count; i++)
                                    {
                                        try
                                        {
                                            if (i == 0)
                                            {
                                                PdfPCell imgCell = new PdfPCell();
                                                imgCell.Colspan = 2;
                                                PdfPTable imageDetails1 = new PdfPTable(1);
                                                imageDetails1.WidthPercentage = 90;
                                                imageDetails1.SpacingBefore = 3;
                                                imageDetails1.SpacingAfter = 3;
                                                imageDetails1.AddCell(getPdfCell((imageListview.Items[i].FindControl("imgName") as HtmlGenericControl).InnerText)).VerticalAlignment = Element.ALIGN_MIDDLE;
                                                string path = (imageListview.Items[i].FindControl("hdnImagePath") as HiddenField).Value;
                                                byte[] imgfile;
                                                imgfile = System.IO.File.ReadAllBytes(Server.MapPath(path));//ImagePath  
                                                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgfile);
                                                // img.ScaleAbsolute(500f, 159f);
                                                //img.ScaleToFit(800f, 200f);
                                                //float h = img.ScaledHeight;
                                                // var hp = img.ScalePercent;
                                                //if (img.ScaledWidth < img.ScaledHeight)
                                                //{
                                                //    // img.RotationDegrees = -270;
                                                //    img.Width = 400;
                                                //  //  img.Height = 400;
                                                //}
                                                PdfPCell imgCell1 = new PdfPCell(img, true);
                                                imgCell1.HorizontalAlignment = Element.ALIGN_CENTER;
                                                imgCell1.Padding = 2;
                                                imageDetails1.AddCell(imgCell1);
                                                //if (img.ScaledHeight > 200 ||(img.ScaledHeight==img.ScaledWidth))
                                                //{
                                              
                                                imgCell.FixedHeight = 200;
                                                //}
                                                //img.ScaleToFit(50f, 30f);

                                                // imgCell.Width = 500f;
                                                imgCell.AddElement(imageDetails1);
                                                imageDetails.AddCell(imgCell);
                                            }
                                            else
                                            {
                                                PdfPCell imgCell = new PdfPCell();
                                                PdfPTable imageDetails1 = new PdfPTable(1);
                                                imageDetails1.WidthPercentage = 90;
                                                imageDetails1.SpacingBefore = 3;
                                                imageDetails1.SpacingAfter = 3;
                                                imageDetails1.AddCell(getPdfCell((imageListview.Items[i].FindControl("imgName") as HtmlGenericControl).InnerText));
                                                string path = (imageListview.Items[i].FindControl("hdnImagePath") as HiddenField).Value;
                                                byte[] imgfile;
                                                imgfile = System.IO.File.ReadAllBytes(Server.MapPath(path));//ImagePath  
                                                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgfile);
                                                PdfPCell imgCell1 = new PdfPCell(img, true);
                                                imgCell1.Padding = 2;
                                                imageDetails1.AddCell(imgCell1);
                                                //img.ScaleToFit(50f, 30f);
                                                //if (img.ScaledHeight > 200)
                                                //{
                                               
                                                    imgCell.FixedHeight = 200;
                                                //}

                                                imgCell.AddElement(imageDetails1);
                                                imageDetails.AddCell(imgCell);
                                            }

                                        }
                                        catch (Exception ex1)
                                        { }
                                    }
                                    if (imageListview.Items.Count % 2 == 0)
                                    {
                                        PdfPCell imgCell = new PdfPCell();
                                        imageDetails.AddCell(imgCell);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {

                            }

                        }

                        //byte[] bytes = Convert.FromBase64String(base64);
                        //iTextSharp.text.Image imggraph = iTextSharp.text.Image.GetInstance(bytes);
                        //PdfPCell imgCell2 = new PdfPCell(imggraph, true);
                        //imageDetails.AddCell(imgCell2);
                        //imageDetails.AddCell("Graph");

                        //string Html = "<img id='img1' src='"+hfImageData.Value+"' />";
                        //StringReader sr = new StringReader(Html);
                        //List<IElement> elements = HTMLWorker.ParseToList(sr, null);

                        //foreach (IElement e1 in elements)
                        //{
                        //    PdfPCell imgCell3 = new PdfPCell();
                        //    //Add those elements to the paragraph
                        //    imgCell3.AddElement(e1);
                        //    imageDetails.AddCell(imgCell3);
                        //}
                        pdfDoc.Add(imageDetails);
                        #endregion
                        #region  ----- Calculated Param Graph ---

                        //if (calcParamTable.Rows.Count > 0)
                        //{
                        //    PdfPTable calPramTbl = new PdfPTable(2);
                        //    calPramTbl.WidthPercentage = 100;
                        //    calPramTbl.SpacingAfter = 7;
                        //    calPramTbl.SpacingBefore = 7;
                        //    PdfPCell calParamCell1 = getPdfCellWithBoldText("Calculated Parameter");
                        //    calParamCell1.Colspan = 2;
                        //    calParamCell1.HorizontalAlignment = Element.ALIGN_CENTER;
                        //    calPramTbl.AddCell(calParamCell1);
                        //    PdfPTable innerTbl = new PdfPTable(2);
                        //    innerTbl.AddCell(getPdfCellWithBoldText("Item"));
                        //    innerTbl.AddCell(getPdfCellWithBoldText("Value"));
                        //    for (int i = 0; i < calcParamTable.Rows.Count; i++)
                        //    {
                        //        innerTbl.AddCell(getPdfCell(calcParamTable.Rows[i][0].ToString()));
                        //        innerTbl.AddCell(getPdfCell(calcParamTable.Rows[i][calParamCount].ToString()));
                        //    }
                        //    calPramTbl.AddCell(innerTbl);
                        //    byte[] calcbytes = Convert.FromBase64String(calcParamGraphData[sdocCount]);
                        //    iTextSharp.text.Image calcimggraph = iTextSharp.text.Image.GetInstance(calcbytes);
                        //    PdfPCell calParamCell2 = new PdfPCell(calcimggraph, true);
                        //    calPramTbl.AddCell(calParamCell2);
                        //    calParamCount++;
                        //    pdfDoc.Add(calPramTbl);
                        //}

                        #endregion
                        #region  ----- Output ----------
                        if (lvQualityParam.Items.Count > 0)
                        {
                            int noOfQltItem = lvQualityParam.Items.Count;

                            PdfPCell qlyCell = new PdfPCell();
                            PdfPTable qlyDetailsFinalTbl = new PdfPTable(2);
                            qlyDetailsFinalTbl.WidthPercentage = 100;
                            qlyDetailsFinalTbl.SpacingBefore = 7;
                            qlyDetailsFinalTbl.SpacingAfter = 7;
                              qlyDetailsFinalTbl.SplitLate = false;

                            PdfPCell qlyDetailsFinalCell = new PdfPCell();

                            PdfPTable qlyDetailsTbl = new PdfPTable(5);
                            qlyDetailsTbl.WidthPercentage = 100;
                            qlyDetailsTbl.SpacingBefore = 7;
                            qlyDetailsTbl.SpacingAfter = 7;

                            PdfPTable qlyDetailsTbl2 = new PdfPTable(5);
                            qlyDetailsTbl2.WidthPercentage = 100;
                            qlyDetailsTbl2.SpacingBefore = 7;
                            qlyDetailsTbl2.SpacingAfter = 7;

                            ListView qlyListview = (lvQualityParam.Items[qlyCount].FindControl("lvinnerQly") as ListView);
                            qlyCount++;
                            ListView dlyFinallistview = new ListView();

                            for (int i = 0; i < qlyListview.Items.Count; i++)
                            {


                                if (string.IsNullOrEmpty((qlyListview.Items[i].FindControl("lblTargetLower") as Label).Text) && string.IsNullOrEmpty((qlyListview.Items[i].FindControl("lblTargetUpper") as Label).Text) && string.IsNullOrEmpty((qlyListview.Items[i].FindControl("lblActualLower") as Label).Text) && string.IsNullOrEmpty((qlyListview.Items[i].FindControl("lblActualUpper") as Label).Text))
                                {

                                    continue;
                                }
                                else
                                {
                                    dlyFinallistview.Items.Add(qlyListview.Items[i]);
                                    //    table.Rows.Add((qlyListview.Items[i].FindControl("Qlyparam") as Label).Text, (qlyListview.Items[i].FindControl("lblTargetLower") as Label).Text, (qlyListview.Items[i].FindControl("lblTargetUpper") as Label).Text, (qlyListview.Items[i].FindControl("lblActualLower") as Label).Text, (qlyListview.Items[i].FindControl("lblActualUpper") as Label).Text);
                                }

                            }


                            if (dlyFinallistview.Items.Count == 1)
                            {
                                PdfPCell qlyheaderCell = getPdfCellWithBoldText("Quality Parameters");
                                qlyheaderCell.Colspan = 2;
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
                            else if (dlyFinallistview.Items.Count > 1)
                            {
                                PdfPCell qlyheaderCell = getPdfCellWithBoldText("Quality Parameters");
                                qlyheaderCell.Colspan = 2;
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
                                    PdfPCell qlyHeadercell2 = getPdfCellWithBoldText(name);
                                    qlyHeadercell2.Colspan = colspanValue;
                                    qlyHeadercell2.HorizontalAlignment = Element.ALIGN_CENTER;
                                    qlyDetailsTbl2.AddCell(qlyHeadercell2);
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
                                    PdfPCell qlyHeadercell2 = getPdfCellWithBoldText(name);
                                    qlyHeadercell2.HorizontalAlignment = Element.ALIGN_CENTER;
                                    qlyDetailsTbl2.AddCell(qlyHeadercell2);
                                }
                            }

                            for (int j = 0; j < dlyFinallistview.Items.Count; j++)
                            {
                                if (j % 2 == 0)
                                {
                                    //if (string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblTargetLower") as Label).Text) && string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblTargetUpper") as Label).Text) && string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblActualLower") as Label).Text) && string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblActualUpper") as Label).Text))
                                    //{

                                    //    continue;
                                    //}
                                    qlyDetailsTbl.AddCell(getPdfCellWithBoldText(((dlyFinallistview.Items[j].FindControl("Qlyparam") as Label).Text).Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                    qlyDetailsTbl.AddCell(getPdfCell(((dlyFinallistview.Items[j].FindControl("lblTargetLower") as Label).Text).Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                    qlyDetailsTbl.AddCell(getPdfCell(((dlyFinallistview.Items[j].FindControl("lblTargetUpper") as Label).Text).Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                    qlyDetailsTbl.AddCell(getPdfCell(((dlyFinallistview.Items[j].FindControl("lblActualLower") as Label).Text).Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                    qlyDetailsTbl.AddCell(getPdfCell(((dlyFinallistview.Items[j].FindControl("lblActualUpper") as Label).Text).Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                                else
                                {
                                    //if (string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblTargetLower") as Label).Text) && string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblTargetUpper") as Label).Text) && string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblActualLower") as Label).Text) && string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblActualUpper") as Label).Text))
                                    //{

                                    //    continue;
                                    //}
                                    qlyDetailsTbl2.AddCell(getPdfCellWithBoldText(((dlyFinallistview.Items[j].FindControl("Qlyparam") as Label).Text).Replace("&amp;", "&"))).VerticalAlignment=Element.ALIGN_MIDDLE;
                                    qlyDetailsTbl2.AddCell(getPdfCell(((dlyFinallistview.Items[j].FindControl("lblTargetLower") as Label).Text).Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                    qlyDetailsTbl2.AddCell(getPdfCell(((dlyFinallistview.Items[j].FindControl("lblTargetUpper") as Label).Text).Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                    qlyDetailsTbl2.AddCell(getPdfCell(((dlyFinallistview.Items[j].FindControl("lblActualLower") as Label).Text).Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                    qlyDetailsTbl2.AddCell(getPdfCell(((dlyFinallistview.Items[j].FindControl("lblActualUpper") as Label).Text).Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                            }

                            qlyDetailsFinalCell.AddElement(qlyDetailsTbl);
                            qlyDetailsFinalTbl.AddCell(qlyDetailsFinalCell);

                            qlyDetailsFinalCell = new PdfPCell();
                            qlyDetailsFinalCell.AddElement(qlyDetailsTbl2);
                            qlyDetailsFinalTbl.AddCell(qlyDetailsFinalCell);

                            pdfDoc.Add(qlyDetailsFinalTbl);
                        }
                        #endregion


                    }
                    if (sdocDetails.Count > 1)
                    {
                        Paragraph p2 = new Paragraph();
                        pdfDoc.Add(p2);
                        pdfDoc.NewPage();
                        Session["OMSDoc"] = "";
                        pdfWriter.PageEvent = new PDFFooter();
                    }
                    #region  ------- Time Graph ------
                    if (hdActualTimeGraph.Value == "" || hdActualTimeGraph.Value == null)
                    {

                    }
                    else
                    {
                        List<string> totalCycleTime = (List<string>)Session["TotalCycleTime"];
                        List<System.Web.UI.WebControls.ListItem> listitem = new List<System.Web.UI.WebControls.ListItem>();
                        for(int i = 0; i < totalCycleTime.Count; i=i+2)
                        {
                            listitem.Add(new System.Web.UI.WebControls.ListItem(totalCycleTime[i].ToString(), totalCycleTime[i + 1].ToString()));
                        }
                        listitem = listitem.OrderBy(li => li.Text).ToList<System.Web.UI.WebControls.ListItem>();
                        totalCycleTime = new List<string>();
                        for(int i=0;i< listitem.Count; i++)
                        {
                            if(listitem[i].Text== "Actual Grinding Time (sec)" || listitem[i].Text == "Non Grinding Time (sec)" || listitem[i].Text == "Total Actual Grinding Time (sec)")
                            {
                                totalCycleTime.Add(listitem[i].Text);
                                totalCycleTime.Add(listitem[i].Value);
                            }
                           
                        }
                        PdfPTable totalCycleGraph = getGraphTable(totalCycleTime, hdActualTimeGraph, "Total Cycle Time");
                        if (totalCycleTime.Count > 0)
                        {
                            pdfDoc.Add(totalCycleGraph);
                        }
                 
                    }
                    //if(hdnTimeGraph.Value=="" || hdnTimeGraph.Value ==null)
                    //{
                    //}
                    //else
                    //{
                    //    PdfPTable grindTime = getGraphTable((List<string>)Session["GrindingTime"], hdnTimeGraph, "Grinding Time");
                    //    pdfDoc.Add(grindTime);
                    //}
                    //if(hdnNonGrindTime.Value=="" || hdnNonGrindTime.Value == null)
                    //{
                    //}
                    //else
                    //{
                    //    PdfPTable nonGrindTime = getGraphTable((List<string>)Session["NonGrindingTime"], hdnNonGrindTime, "Non Grinding Time");
                    //    pdfDoc.Add(nonGrindTime);
                    //}


                    #endregion

                    pdfWriter.CloseStream = false;
                    pdfDoc.Close();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename="+ Sdocid+"_Default Report.pdf");
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
            else if(customeReport.Checked)
            {
                try
                {
                    if (hdnTotalCycleTimeGraph.Value != "")
                    {
                        string str = hdnTotalCycleTimeGraph.Value.ToString();
                        string base64 = Request.Form[hdnTotalCycleTimeGraph.UniqueID].Split(',')[1];
                    }
                    if (hdnCalculatedTimeGraph.Value != "")
                    {
                        string str = hdnCalculatedTimeGraph.Value.ToString();
                        string base64 = Request.Form[hdnCalculatedTimeGraph.UniqueID].Split(',')[1];
                    }
                    if (hdActualTimeGraph.Value != "")
                    {
                        string str = hdActualTimeGraph.Value.ToString();
                        string base64 = Request.Form[hdActualTimeGraph.UniqueID].Split(',')[1];
                    }
                    Document pdfDoc = new Document(PageSize.A4, 15, 15, 25, 25);
                    PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDoc, Response.OutputStream);


                    //// calling PDFFooter class to Include in document
                    //pdfWriter.PageEvent = new PDFFooter();

                    pdfDoc.Open();
                    byte[] file;
                    file = System.IO.File.ReadAllBytes(Server.MapPath("~/Images/micromaticlogo.jpg"));//ImagePath  
                    iTextSharp.text.Image jpg1 = iTextSharp.text.Image.GetInstance(file);
                    jpg1.ScaleToFit(100f, 50f);//Set width and height in float  
                                               // jpg1.Alignment = 2;                        // pdfTable1.AddCell(jpg1);
                                               // jpg1.SetAbsolutePosition(36, PdfWriter.ve(true));
                                               //pdfDoc.Add(jpg1);

                    PdfPTable pdfTable1 = new PdfPTable(5);
                    //pdfTable1.DefaultCell.Padding = 5;
                    pdfTable1.WidthPercentage = 100;
                    pdfTable1.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfTable1.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    //pdfTable1.DefaultCell.BackgroundColor = new iTextSharp.text.BaseColor(64, 134, 170);
                    pdfTable1.DefaultCell.BorderWidth = 0;

                    PdfPCell cell = new PdfPCell(jpg1, false);
                    cell.BorderWidth = 1;
                    cell.Padding = 1;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    //cell.Width = 50f;
                    pdfTable1.AddCell(cell);

                    Chunk c1 = new Chunk("TECHNICAL INFORMATION NOTE", FontFactory.GetFont("Times New Roman"));
                    c1.Font.Color = new BaseColor(0, 0, 0);
                    c1.Font.SetStyle(0);
                    c1.Font.Size = 14;
                    Phrase p1 = new Phrase();
                    p1.Add(c1);
                    PdfPCell cellHeader = new PdfPCell(p1);
                    // cellHeader.Colspan = 2;
                    cellHeader.HorizontalAlignment = Element.ALIGN_CENTER;
                    cellHeader.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cellHeader.BorderWidth = 1;
                    cellHeader.Colspan = 3;
                    pdfTable1.AddCell(cellHeader);

                    Chunk date = new Chunk(DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt"));
                    date.Font.SetStyle(0);
                    date.Font.Size = 8;
                    PdfPCell dateCell = new PdfPCell(new Phrase(date));
                    dateCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    dateCell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    dateCell.BorderWidth = 1;
                    pdfTable1.AddCell(dateCell);
                    pdfDoc.Add(pdfTable1);
                    //// for line
                    Paragraph lineSeparator = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, iTextSharp.text.BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                    // Set gap between line paragraphs.
                    lineSeparator.SetLeading(0.5F, 0.5F);
                    pdfDoc.Add(lineSeparator);
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
                    int generalInfoCount = 3, inferenceCount = 1, imageCount = 0, qlyCount = 0, calParamCount = 2;
                    DataTable calcParamTable = (DataTable)Session["CalcParamGraphData"];
                    string calParamStr = hdnCalcParamGraph.Value.Trim().Remove(hdnCalcParamGraph.Value.Trim().Length - 1);
                    List<string> calcParamGraph = calParamStr.Split('$').ToList();
                    string s1 = hdnCalcParamGraph.Value;
                    List<string> calcParamGraphData = new List<string>();
                    for (int i = 0; i < calcParamGraph.Count; i++)
                    {
                        calcParamGraphData.Add(calcParamGraph[i].Split(',')[1]);
                    }
                    for (int sdocCount = 0; sdocCount < sdocDetails.Count; sdocCount++)
                    {
                        //Paragraph SDocParam = new Paragraph(sdocDetails[sdocCount]);
                        //SDocParam.Alignment = Element.ALIGN_CENTER;
                        //pdfDoc.Add(SDocParam);
                        // calling PDFFooter class to Include in document
                        if (sdocCount > 0)
                        {
                            //Session["OMSDoc"] = "";
                            //pdfWriter.PageEvent = new PDFFooter();
                            Paragraph p2 = new Paragraph();
                            pdfDoc.Add(p2);
                            pdfDoc.NewPage();
                        }
                        Session["OMSDoc"] = sdocDetails[sdocCount];
                        // Session["OMPrev"] = sdocDetails[sdocCount];
                        pdfWriter.PageEvent = new PDFFooter();

                        #region  ---- General Info -----
                        List<string> generalDetails = new List<string>();
                        List<string> machineDetails = new List<string>();
                        List<string> consumableDetails = new List<string>();
                        List<string> workpieceDetails = new List<string>();
                        List<string> operationalDetails = new List<string>();
                        List<string> gaugingDetails = new List<string>();
                        List<string> coolantDetails = new List<string>();
                        int gCount = 0, mCount = 0, cCount = 0, wCount = 0, oCount = 0, gDCount = 0, cDCount = 0;
                        for (int i = 0; i < gvGeneralInfo.Rows.Count; i++)
                        {
                            string inputModule = gvGeneralInfo.Rows[i].Cells[1].Text;
                            if (inputModule == "General Information")
                            {
                                //if (gCount == 0)
                                //{
                                //    generalDetails.Add("General Information");
                                //    gCount++;
                                //}
                                if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                                {
                                    continue;
                                }

                                generalDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                                generalDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                            }

                            else if (inputModule == "Machine Tool")
                            {

                                if (gvGeneralInfo.Rows[i].Cells[2].Text == "Steady Rest" || gvGeneralInfo.Rows[i].Cells[2].Text == "Flagging Type" || gvGeneralInfo.Rows[i].Cells[2].Text == "Flagging Make" || gvGeneralInfo.Rows[i].Cells[2].Text == "Wheel Balancer Type" || gvGeneralInfo.Rows[i].Cells[2].Text == "Wheel Balancer Make" || gvGeneralInfo.Rows[i].Cells[2].Text.Replace("&amp;", "&") == "Gap & Crash Make")
                                {
                                    //if (gDCount == 0)
                                    //{
                                    //    gaugingDetails.Add("Gauging System");
                                    //    gDCount++;
                                    //}
                                    if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                                    {
                                        continue;
                                    }
                                    gaugingDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                                    gaugingDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                                }
                                else
                                {
                                    //if (mCount == 0)
                                    //{
                                    //    machineDetails.Add("Machine Tool");
                                    //    mCount++;
                                    //}
                                    if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                                    {
                                        continue;
                                    }
                                    machineDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                                    machineDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                                }
                            }
                            else if (inputModule == "Consumables Details")
                            {
                                if (gvGeneralInfo.Rows[i].Cells[2].Text == "Coolant Type" || gvGeneralInfo.Rows[i].Cells[2].Text == "Coolant Make")
                                {
                                    //if (cDCount == 0)
                                    //{
                                    //    coolantDetails.Add("Coolant Details");
                                    //    cDCount++;
                                    //}
                                    if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                                    {
                                        continue;
                                    }
                                    coolantDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                                    coolantDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                                }
                                else
                                {
                                    //if (cCount == 0)
                                    //{
                                    //    consumableDetails.Add("Consumables Details");
                                    //    cCount++;
                                    //}
                                    if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                                    {
                                        continue;
                                    }
                                    consumableDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                                    consumableDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                                }

                            }
                            else if (inputModule == "Workpiece Details")
                            {
                                //if (wCount == 0)
                                //{
                                //    workpieceDetails.Add("Workpiece Details");
                                //    wCount++;
                                //}
                                if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                                {
                                    continue;
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
                                if (gvGeneralInfo.Rows[i].Cells[2].Text == "Coolant Concentration %")
                                {
                                    //if (coolantDetails.Count<=0)
                                    //{
                                    //    if (cDCount == 0)
                                    //    {
                                    //        coolantDetails.Add("Coolant Details");
                                    //        cDCount++;
                                    //    }
                                    //}

                                    if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                                    {
                                        continue;
                                    }
                                    coolantDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                                    coolantDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                                }
                                else
                                {
                                    //if (oCount == 0)
                                    //{
                                    //    operationalDetails.Add("Operational Parameters");
                                    //    oCount++;
                                    //}
                                    if (gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text == "&nbsp;")
                                    {
                                        continue;
                                    }
                                    operationalDetails.Add(gvGeneralInfo.Rows[i].Cells[0].Text);
                                    operationalDetails.Add(gvGeneralInfo.Rows[i].Cells[generalInfoCount].Text);
                                }

                            }
                        }
                        generalInfoCount++;
                        PdfPTable mainTbl = new PdfPTable(2);
                        mainTbl.WidthPercentage = 100;
                        mainTbl.SpacingBefore = 7;
                        mainTbl.SpacingAfter = 7;
                        PdfPCell headingCell = getPdfCellWithBoldText("Input");
                        headingCell.Colspan = 2;
                        headingCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        mainTbl.AddCell(headingCell);
                        if (generalDetails.Count > 0)
                        {
                            PdfPCell mcell = new PdfPCell();
                            PdfPTable generalDetailsTbl = new PdfPTable(2);
                            generalDetailsTbl.WidthPercentage = 90;
                            generalDetailsTbl.SpacingBefore = 7;
                            generalDetailsTbl.SpacingAfter = 7;
                            for (int i = 0; i < generalDetails.Count; i++)
                            {
                                if (i == 0)
                                {
                                    PdfPCell headerCell = getPdfCellWithBoldText("General Information");
                                    headerCell.Colspan = 2;
                                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    generalDetailsTbl.AddCell(headerCell);
                                }
                                if ((i % 2) == 0)
                                {
                                    generalDetailsTbl.AddCell(getPdfCell(generalDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                                else
                                {
                                    generalDetailsTbl.AddCell(getPdfCell(generalDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                            }
                            mcell.AddElement(generalDetailsTbl);
                            mainTbl.AddCell(mcell);
                        }
                        if (machineDetails.Count > 0)
                        {
                            PdfPCell mcell = new PdfPCell();
                            PdfPTable machineDetailsTbl = new PdfPTable(2);
                            machineDetailsTbl.WidthPercentage = 90;
                            machineDetailsTbl.SpacingBefore = 7;
                            machineDetailsTbl.SpacingAfter = 7;
                            for (int i = 0; i < machineDetails.Count; i++)
                            {
                                if (i == 0)
                                {
                                    PdfPCell headerCell = getPdfCellWithBoldText("Machine Tool");
                                    headerCell.Colspan = 2;
                                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    machineDetailsTbl.AddCell(headerCell);
                                }
                                if ((i % 2) == 0)
                                {
                                    machineDetailsTbl.AddCell(getPdfCell(machineDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                                else
                                {
                                    machineDetailsTbl.AddCell(getPdfCell(machineDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                            }
                            mcell.AddElement(machineDetailsTbl);
                            mainTbl.AddCell(mcell);
                        }
                        if (consumableDetails.Count > 0)
                        {
                            PdfPCell mcell = new PdfPCell();
                            PdfPTable DetailsTbl = new PdfPTable(2);
                            DetailsTbl.WidthPercentage = 90;
                            DetailsTbl.SpacingBefore = 7;
                            DetailsTbl.SpacingAfter = 7;
                            for (int i = 0; i < consumableDetails.Count; i++)
                            {
                                if (i == 0)
                                {
                                    PdfPCell headerCell = getPdfCellWithBoldText("Consumables Details");
                                    headerCell.Colspan = 2;
                                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    DetailsTbl.AddCell(headerCell);
                                }
                                if ((i % 2) == 0)
                                {
                                    DetailsTbl.AddCell(getPdfCell(consumableDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                                else
                                {
                                    DetailsTbl.AddCell(getPdfCell(consumableDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                            }
                            mcell.AddElement(DetailsTbl);
                            mainTbl.AddCell(mcell);
                        }
                        if (gaugingDetails.Count > 0)
                        {
                            PdfPCell mcell = new PdfPCell();
                            PdfPTable DetailsTbl = new PdfPTable(2);
                            DetailsTbl.WidthPercentage = 90;
                            DetailsTbl.SpacingBefore = 7;
                            DetailsTbl.SpacingAfter = 7;
                            for (int i = 0; i < gaugingDetails.Count; i++)
                            {
                                if (i == 0)
                                {
                                    PdfPCell headerCell = getPdfCellWithBoldText("Gauging System");
                                    headerCell.Colspan = 2;
                                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    DetailsTbl.AddCell(headerCell);
                                }
                                if ((i % 2) == 0)
                                {
                                    DetailsTbl.AddCell(getPdfCell(gaugingDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                                else
                                {
                                    DetailsTbl.AddCell(getPdfCell(gaugingDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                            }
                            mcell.AddElement(DetailsTbl);
                            mainTbl.AddCell(mcell);
                        }
                        if (coolantDetails.Count > 0)
                        {
                            PdfPCell mcell = new PdfPCell();
                            PdfPTable DetailsTbl = new PdfPTable(2);
                            DetailsTbl.WidthPercentage = 90;
                            DetailsTbl.SpacingBefore = 7;
                            DetailsTbl.SpacingAfter = 7;
                            for (int i = 0; i < coolantDetails.Count; i++)
                            {
                                if (i == 0)
                                {
                                    PdfPCell headerCell = getPdfCellWithBoldText("Coolant Details");
                                    headerCell.Colspan = 2;
                                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    DetailsTbl.AddCell(headerCell);
                                }
                                if ((i % 2) == 0)
                                {
                                    DetailsTbl.AddCell(getPdfCell(coolantDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                                else
                                {
                                    DetailsTbl.AddCell(getPdfCell(coolantDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                            }
                            mcell.AddElement(DetailsTbl);
                            mainTbl.AddCell(mcell);
                        }
                        if (workpieceDetails.Count > 0)
                        {
                            PdfPCell mcell = new PdfPCell();
                            PdfPTable DetailsTbl = new PdfPTable(2);
                            DetailsTbl.WidthPercentage = 90;
                            DetailsTbl.SpacingBefore = 7;
                            DetailsTbl.SpacingAfter = 7;
                            for (int i = 0; i < workpieceDetails.Count; i++)
                            {
                                if (i == 0)
                                {
                                    PdfPCell headerCell = getPdfCellWithBoldText("Workpiece Details");
                                    headerCell.Colspan = 2;
                                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    DetailsTbl.AddCell(headerCell);
                                }
                                if ((i % 2) == 0)
                                {
                                    DetailsTbl.AddCell(getPdfCell(workpieceDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                                else
                                {
                                    DetailsTbl.AddCell(getPdfCell(workpieceDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                            }
                            mcell.AddElement(DetailsTbl);
                            mainTbl.AddCell(mcell);
                        }
                        if (operationalDetails.Count > 0)
                        {
                            PdfPCell mcell = new PdfPCell();
                            PdfPTable DetailsTbl = new PdfPTable(2);
                            DetailsTbl.WidthPercentage = 90;
                            DetailsTbl.SpacingBefore = 7;
                            DetailsTbl.SpacingAfter = 7;
                            for (int i = 0; i < operationalDetails.Count; i++)
                            {
                                if (i == 0)
                                {
                                    PdfPCell headerCell = getPdfCellWithBoldText("Operational Parameters");
                                    headerCell.Colspan = 2;
                                    headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    DetailsTbl.AddCell(headerCell);
                                }
                                if ((i % 2) == 0)
                                {
                                    DetailsTbl.AddCell(getPdfCell(operationalDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                                else
                                {
                                    DetailsTbl.AddCell(getPdfCell(operationalDetails[i].Replace("&amp;", "&"))).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                            }
                            mcell.AddElement(DetailsTbl);
                            mainTbl.AddCell(mcell);
                        }
                        if (gvInferenceSignal.Rows.Count > 0)
                        {
                            PdfPCell mcell = new PdfPCell();
                            //PdfPTable DetailsTbl = new PdfPTable(2);
                            //DetailsTbl.WidthPercentage = 90;
                            //DetailsTbl.SpacingBefore = 7;
                            //DetailsTbl.SpacingAfter = 7;
                            //PdfPCell headerCell = getPdfCellWithBoldText("Inferences from Power Signal");
                            //headerCell.Colspan = 2;
                            //headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                            //DetailsTbl.AddCell(headerCell);
                            //for (int i = 0; i < gvInferenceSignal.Rows.Count; i++)
                            //{
                            //    if (gvInferenceSignal.Rows[i].Cells[inferenceCount].Text == "&nbsp;")
                            //    {
                            //        continue;
                            //    }
                            //    DetailsTbl.AddCell(getPdfCell(gvInferenceSignal.Rows[i].Cells[0].Text));
                            //    DetailsTbl.AddCell(getPdfCell(gvInferenceSignal.Rows[i].Cells[inferenceCount].Text));
                            //}
                            //inferenceCount++;
                            //mcell.AddElement(DetailsTbl);
                            mainTbl.AddCell(mcell);
                        }
                        pdfDoc.Add(mainTbl);
                        #endregion
                        #region  ------- Images --------------
                        PdfPTable imageDetails = new PdfPTable(2);
                        imageDetails.WidthPercentage = 100;
                        imageDetails.SpacingAfter = 7;
                        imageDetails.SpacingBefore = 7;
                        if (lvImages.Items.Count > 0)
                        {
                            try
                            {


                                if ((lvImages.Items[imageCount].FindControl("imgSdoc") as HiddenField).Value == sdocDetails[sdocCount].ToString())
                                {
                                    PdfPCell imgheaderCell = getPdfCellWithBoldText("Process");
                                    imgheaderCell.Colspan = 2;
                                    imgheaderCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                    imageDetails.AddCell(imgheaderCell);
                                    ListView imageListview = lvImages.Items[imageCount].FindControl("lvImageDetails") as ListView;
                                    imageCount++;
                                    for (int i = 0; i < imageListview.Items.Count; i++)
                                    {
                                        try
                                        {
                                            if (i == 0)
                                            {
                                                PdfPCell imgCell = new PdfPCell();
                                                imgCell.Colspan = 2;
                                                PdfPTable imageDetails1 = new PdfPTable(1);
                                                imageDetails1.WidthPercentage = 90;
                                                imageDetails1.SpacingBefore = 3;
                                                imageDetails1.SpacingAfter = 3;
                                                imageDetails1.AddCell(getPdfCell((imageListview.Items[i].FindControl("imgName") as HtmlGenericControl).InnerText)).VerticalAlignment = Element.ALIGN_MIDDLE;
                                                string path = (imageListview.Items[i].FindControl("hdnImagePath") as HiddenField).Value;
                                                byte[] imgfile;
                                                imgfile = System.IO.File.ReadAllBytes(Server.MapPath(path));//ImagePath  
                                                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgfile);
                                                //if (img.ScaledWidth < img.ScaledHeight)
                                                //{
                                                //    img.RotationDegrees = -270;
                                                //}
                                                PdfPCell imgCell1 = new PdfPCell(img, true);
                                                imgCell1.HorizontalAlignment = Element.ALIGN_CENTER;
                                                imgCell1.Padding = 2;
                                                imageDetails1.AddCell(imgCell1);
                                                //if (img.ScaledHeight > 200 || (img.ScaledHeight == img.ScaledWidth))
                                                //{
                                                    imgCell.FixedHeight = 200;
                                                //}
                                                imgCell.AddElement(imageDetails1);
                                                imageDetails.AddCell(imgCell);
                                            }
                                            else
                                            {
                                                PdfPCell imgCell = new PdfPCell();
                                                PdfPTable imageDetails1 = new PdfPTable(1);
                                                imageDetails1.WidthPercentage = 90;
                                                imageDetails1.SpacingBefore = 3;
                                                imageDetails1.SpacingAfter = 3;
                                                imageDetails1.AddCell(getPdfCell((imageListview.Items[i].FindControl("imgName") as HtmlGenericControl).InnerText));
                                                string path = (imageListview.Items[i].FindControl("hdnImagePath") as HiddenField).Value;
                                                byte[] imgfile;
                                                imgfile = System.IO.File.ReadAllBytes(Server.MapPath(path));//ImagePath  
                                                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgfile);
                                                PdfPCell imgCell1 = new PdfPCell(img, true);
                                                imgCell1.Padding = 2;
                                                imageDetails1.AddCell(imgCell1);
                                                //if (img.ScaledHeight > 500)
                                                //{
                                                    imgCell.FixedHeight = 200;
                                                //}
                                                //img.ScaleToFit(50f, 30f);
                                                imgCell.AddElement(imageDetails1);
                                                imageDetails.AddCell(imgCell);
                                            }

                                        }
                                        catch (Exception ex1)
                                        { }
                                    }
                                    if (imageListview.Items.Count % 2 == 0)
                                    {
                                        PdfPCell imgCell = new PdfPCell();
                                        imageDetails.AddCell(imgCell);
                                    }
                                }
                                else
                                {

                                }

                            }
                            catch (Exception ex)
                            {

                            }

                        }

                        //byte[] bytes = Convert.FromBase64String(base64);
                        //iTextSharp.text.Image imggraph = iTextSharp.text.Image.GetInstance(bytes);
                        //PdfPCell imgCell2 = new PdfPCell(imggraph, true);
                        //imageDetails.AddCell(imgCell2);
                        //imageDetails.AddCell("Graph");

                        //string Html = "<img id='img1' src='"+hfImageData.Value+"' />";
                        //StringReader sr = new StringReader(Html);
                        //List<IElement> elements = HTMLWorker.ParseToList(sr, null);

                        //foreach (IElement e1 in elements)
                        //{
                        //    PdfPCell imgCell3 = new PdfPCell();
                        //    //Add those elements to the paragraph
                        //    imgCell3.AddElement(e1);
                        //    imageDetails.AddCell(imgCell3);
                        //}
                        pdfDoc.Add(imageDetails);
                        #endregion
                        #region  ----- Output ----------
                        if (lvQualityParam.Items.Count > 0)
                        {
                            int noOfQltItem = lvQualityParam.Items.Count;

                            PdfPCell qlyCell = new PdfPCell();
                            PdfPTable qlyDetailsFinalTbl = new PdfPTable(2);
                            qlyDetailsFinalTbl.WidthPercentage = 100;
                            qlyDetailsFinalTbl.SpacingBefore = 7;
                            qlyDetailsFinalTbl.SpacingAfter = 7;
                            qlyDetailsFinalTbl.SplitLate = false;
                            PdfPCell qlyDetailsFinalCell = new PdfPCell();

                            PdfPTable qlyDetailsTbl = new PdfPTable(5);
                            qlyDetailsTbl.WidthPercentage = 100;
                            qlyDetailsTbl.SpacingBefore = 7;
                            qlyDetailsTbl.SpacingAfter = 7;

                            PdfPTable qlyDetailsTbl2 = new PdfPTable(5);
                            qlyDetailsTbl2.WidthPercentage = 100;
                            qlyDetailsTbl2.SpacingBefore = 7;
                            qlyDetailsTbl2.SpacingAfter = 7;

                            ListView qlyListview = (lvQualityParam.Items[qlyCount].FindControl("lvinnerQly") as ListView);
                            qlyCount++;
                            ListView dlyFinallistview = new ListView();

                            for (int i = 0; i < qlyListview.Items.Count; i++)
                            {


                                if (string.IsNullOrEmpty((qlyListview.Items[i].FindControl("lblTargetLower") as Label).Text) && string.IsNullOrEmpty((qlyListview.Items[i].FindControl("lblTargetUpper") as Label).Text) && string.IsNullOrEmpty((qlyListview.Items[i].FindControl("lblActualLower") as Label).Text) && string.IsNullOrEmpty((qlyListview.Items[i].FindControl("lblActualUpper") as Label).Text))
                                {

                                    continue;
                                }
                                else
                                {
                                    dlyFinallistview.Items.Add(qlyListview.Items[i]);
                                    //    table.Rows.Add((qlyListview.Items[i].FindControl("Qlyparam") as Label).Text, (qlyListview.Items[i].FindControl("lblTargetLower") as Label).Text, (qlyListview.Items[i].FindControl("lblTargetUpper") as Label).Text, (qlyListview.Items[i].FindControl("lblActualLower") as Label).Text, (qlyListview.Items[i].FindControl("lblActualUpper") as Label).Text);
                                }

                            }


                            if (dlyFinallistview.Items.Count == 1)
                            {
                                PdfPCell qlyheaderCell = getPdfCellWithBoldText("Quality Parameters");
                                qlyheaderCell.Colspan = 2;
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
                            else if (dlyFinallistview.Items.Count > 1)
                            {
                                PdfPCell qlyheaderCell = getPdfCellWithBoldText("Quality Parameters");
                                qlyheaderCell.Colspan = 2;
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
                                    PdfPCell qlyHeadercell2 = getPdfCellWithBoldText(name);
                                    qlyHeadercell2.Colspan = colspanValue;
                                    qlyHeadercell2.HorizontalAlignment = Element.ALIGN_CENTER;
                                    qlyDetailsTbl2.AddCell(qlyHeadercell2);
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
                                    PdfPCell qlyHeadercell2 = getPdfCellWithBoldText(name);
                                    qlyHeadercell2.HorizontalAlignment = Element.ALIGN_CENTER;
                                    qlyDetailsTbl2.AddCell(qlyHeadercell2);
                                }
                            }

                            for (int j = 0; j < dlyFinallistview.Items.Count; j++)
                            {
                                if (j % 2 == 0)
                                {
                                    //if (string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblTargetLower") as Label).Text) && string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblTargetUpper") as Label).Text) && string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblActualLower") as Label).Text) && string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblActualUpper") as Label).Text))
                                    //{

                                    //    continue;
                                    //}
                                    qlyDetailsTbl.AddCell(getPdfCellWithBoldText((dlyFinallistview.Items[j].FindControl("Qlyparam") as Label).Text)).VerticalAlignment = Element.ALIGN_MIDDLE;
                                    qlyDetailsTbl.AddCell(getPdfCell((dlyFinallistview.Items[j].FindControl("lblTargetLower") as Label).Text)).VerticalAlignment = Element.ALIGN_MIDDLE;
                                    qlyDetailsTbl.AddCell(getPdfCell((dlyFinallistview.Items[j].FindControl("lblTargetUpper") as Label).Text)).VerticalAlignment = Element.ALIGN_MIDDLE;
                                    qlyDetailsTbl.AddCell(getPdfCell((dlyFinallistview.Items[j].FindControl("lblActualLower") as Label).Text)).VerticalAlignment = Element.ALIGN_MIDDLE;
                                    qlyDetailsTbl.AddCell(getPdfCell((dlyFinallistview.Items[j].FindControl("lblActualUpper") as Label).Text)).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                                else
                                {
                                    //if (string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblTargetLower") as Label).Text) && string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblTargetUpper") as Label).Text) && string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblActualLower") as Label).Text) && string.IsNullOrEmpty((dlyFinallistview.Items[j].FindControl("lblActualUpper") as Label).Text))
                                    //{

                                    //    continue;
                                    //}
                                    qlyDetailsTbl2.AddCell(getPdfCellWithBoldText((dlyFinallistview.Items[j].FindControl("Qlyparam") as Label).Text)).VerticalAlignment = Element.ALIGN_MIDDLE;
                                    qlyDetailsTbl2.AddCell(getPdfCell((dlyFinallistview.Items[j].FindControl("lblTargetLower") as Label).Text)).VerticalAlignment = Element.ALIGN_MIDDLE;
                                    qlyDetailsTbl2.AddCell(getPdfCell((dlyFinallistview.Items[j].FindControl("lblTargetUpper") as Label).Text)).VerticalAlignment = Element.ALIGN_MIDDLE;
                                    qlyDetailsTbl2.AddCell(getPdfCell((dlyFinallistview.Items[j].FindControl("lblActualLower") as Label).Text)).VerticalAlignment = Element.ALIGN_MIDDLE;
                                    qlyDetailsTbl2.AddCell(getPdfCell((dlyFinallistview.Items[j].FindControl("lblActualUpper") as Label).Text)).VerticalAlignment = Element.ALIGN_MIDDLE;
                                }
                            }

                            qlyDetailsFinalCell.AddElement(qlyDetailsTbl);
                            qlyDetailsFinalTbl.AddCell(qlyDetailsFinalCell);

                            qlyDetailsFinalCell = new PdfPCell();
                            qlyDetailsFinalCell.AddElement(qlyDetailsTbl2);
                            qlyDetailsFinalTbl.AddCell(qlyDetailsFinalCell);

                            pdfDoc.Add(qlyDetailsFinalTbl);
                        }
                        #endregion
                        #region  ----- Calculated Param Graph ---

                        if (calcParamTable.Rows.Count > 0)
                        {
                            PdfPTable calPramTbl = new PdfPTable(2);
                            calPramTbl.WidthPercentage = 100;
                            calPramTbl.SpacingAfter = 7;
                            calPramTbl.SpacingBefore = 7;
                            //calPramTbl.SplitLate = false;
                            PdfPCell calParamCell1 = getPdfCellWithBoldText("Calculated Parameters");
                            calParamCell1.Colspan = 2;
                            calParamCell1.HorizontalAlignment = Element.ALIGN_CENTER;
                            calPramTbl.AddCell(calParamCell1);
                            PdfPTable innerTbl = new PdfPTable(2);
                            int derivedflag = 0, dresserflag = 0;
                            for (int i = 0; i < calcParamTable.Rows.Count; i++)
                            {
                                //PdfPCell pdfPCell = new PdfPCell();
                                if (calcParamTable.Rows[i][0].ToString() == "Dervied Parameters")
                                {
                                    if (derivedflag == 0)
                                    {
                                        derivedflag++;
                                        PdfPCell pdfPCell = getPdfCellWithBoldText("Calculated Parameters");
                                        pdfPCell.Colspan = 2;
                                        pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                        innerTbl.AddCell(pdfPCell);
                                        innerTbl.AddCell(getPdfCellWithBoldText("Item"));
                                        innerTbl.AddCell(getPdfCellWithBoldText("Value"));
                                    }
                                }
                                if (calcParamTable.Rows[i][0].ToString() == "Dressing Parameters")
                                {
                                    if (dresserflag == 0)
                                    {
                                        dresserflag++;
                                        PdfPCell pdfPCell = getPdfCellWithBoldText("Dressing Parameters");
                                        pdfPCell.Colspan = 2;
                                        pdfPCell.HorizontalAlignment = Element.ALIGN_CENTER;
                                        innerTbl.AddCell(pdfPCell);
                                        innerTbl.AddCell(getPdfCellWithBoldText("Item"));
                                        innerTbl.AddCell(getPdfCellWithBoldText("Value"));
                                    }
                                }

                                innerTbl.AddCell(getPdfCell(calcParamTable.Rows[i][1].ToString())).VerticalAlignment = Element.ALIGN_MIDDLE;
                                innerTbl.AddCell(getPdfCell(calcParamTable.Rows[i][calParamCount].ToString())).VerticalAlignment = Element.ALIGN_MIDDLE;
                            }
                            calPramTbl.AddCell(innerTbl);
                            byte[] calcbytes = Convert.FromBase64String(calcParamGraphData[sdocCount]);
                            iTextSharp.text.Image calcimggraph = iTextSharp.text.Image.GetInstance(calcbytes);
                            PdfPCell calParamCell2 = new PdfPCell(calcimggraph, true);
                            calPramTbl.AddCell(calParamCell2);
                            calParamCount++;
                            pdfDoc.Add(calPramTbl);
                        }

                        #endregion

                       

                    }
                    if (sdocDetails.Count > 1)
                    {
                        Paragraph p = new Paragraph();
                        pdfDoc.Add(p);
                        pdfDoc.NewPage();
                        Session["OMSDoc"] = "";
                        pdfWriter.PageEvent = new PDFFooter();
                    }
                    #region  ------- Time Graph ------
                    if (hdnTotalCycleTimeGraph.Value == "" || hdnTotalCycleTimeGraph.Value == null)
                    {

                    }
                    else
                    {
                        List<string> listTotalcycletime = (List<string>)Session["TotalCycleTime"];

                        List<System.Web.UI.WebControls.ListItem> listitem = new List<System.Web.UI.WebControls.ListItem>();
                        for (int i = 0; i < listTotalcycletime.Count; i = i + 2)
                        {
                            listitem.Add(new System.Web.UI.WebControls.ListItem(listTotalcycletime[i].ToString(), listTotalcycletime[i + 1].ToString()));
                        }
                        listitem = listitem.OrderBy(li => li.Text).ToList<System.Web.UI.WebControls.ListItem>();
                        List<string> listcalculatedtotalcycletime = new List<string>();
                        List<string> listactualtotalcycletime = new List<string>();
                        for (int i = 0; i < listitem.Count; i++)
                        {
                            if (listitem[i].ToString() == "Non Grinding Time (sec)" || listitem[i].ToString() == "Actual Grinding Time (sec)" || listitem[i].ToString() == "Total Actual Grinding Time (sec)")
                            {
                                listactualtotalcycletime.Add(listitem[i].Text);
                                listactualtotalcycletime.Add(listitem[i].Value);
                            }
                            if (listitem[i].ToString() == "Non Grinding Time (sec)" || listitem[i].ToString() == "Grinding Time (sec)" || listitem[i].ToString() == "Total Grinding Time (sec)")
                            {
                                listcalculatedtotalcycletime.Add(listitem[i].Text);
                                listcalculatedtotalcycletime.Add(listitem[i].Value);
                            }
                        }

                        PdfPTable totalCycleGraph = getTable(listcalculatedtotalcycletime, listactualtotalcycletime, hdnTotalCycleTimeGraph, "Total Cycle Time");
                        //  PdfPTable totalCycleGraph = getGraphTable((List<string>)Session["TotalCycleTime"], hdnTotalCycleTimeGraph, "Total Cycle Time");

                        pdfDoc.Add(totalCycleGraph);
                    }
                    if (hdnCalculatedTimeGraph.Value == "" || hdnCalculatedTimeGraph.Value == null)
                    {

                    }
                    else
                    {
                        List<string> calculatetimeDetails= (List<string>)Session["GrindingTime"];
                        PdfPTable calculatedtimegraph = getGraphTable((List<string>)Session["GrindingTime"], hdnCalculatedTimeGraph, "Calculated Time");
                        if (calculatetimeDetails.Count > 0)
                        {
                            pdfDoc.Add(calculatedtimegraph);

                        }
                      
                    }
                    #region ------Actual Time Graph-----
                    if (hdActualTimeGraph.Value == "" || hdActualTimeGraph.Value == null)
                    {

                    }
                    else
                    {
                        List<string> listTotalcycletime = (List<string>)Session["TotalCycleTime"];

                        List<System.Web.UI.WebControls.ListItem> listitem = new List<System.Web.UI.WebControls.ListItem>();
                        for (int i = 0; i < listTotalcycletime.Count; i = i + 2)
                        {
                            listitem.Add(new System.Web.UI.WebControls.ListItem(listTotalcycletime[i].ToString(), listTotalcycletime[i + 1].ToString()));
                        }
                        listitem = listitem.OrderBy(li => li.Text).ToList<System.Web.UI.WebControls.ListItem>();
                        List<string> listactualtime = new List<string>();
                        for (int i = 0; i < listitem.Count; i++)
                        {
                            if (listitem[i].ToString() == "Non Grinding Time (sec)" || listitem[i].ToString() == "Actual Grinding Time (sec)")
                            {
                                listactualtime.Add(listitem[i].Text);
                                listactualtime.Add(listitem[i].Value);
                            }
                        }

                        PdfPTable actualtimeGraph = getGraphTable(listactualtime, hdActualTimeGraph, "Actual Time");
                        if (listactualtime.Count > 0)
                        {
                            pdfDoc.Add(actualtimeGraph);
                        }

                    }

                    #endregion
                    //if(hdnTimeGraph.Value=="" || hdnTimeGraph.Value ==null)
                    //{
                    //}
                    //else
                    //{
                    //    PdfPTable grindTime = getGraphTable((List<string>)Session["GrindingTime"], hdnTimeGraph, "Grinding Time");
                    //    pdfDoc.Add(grindTime);
                    //}
                    //if(hdnNonGrindTime.Value=="" || hdnNonGrindTime.Value == null)
                    //{
                    //}
                    //else
                    //{
                    //    PdfPTable nonGrindTime = getGraphTable((List<string>)Session["NonGrindingTime"], hdnNonGrindTime, "Non Grinding Time");
                    //    pdfDoc.Add(nonGrindTime);
                    //}


                    #endregion

                    pdfWriter.CloseStream = false;
                    pdfDoc.Close();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=" + Sdocid + "_Custom Report.pdf");
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
        }

        protected void Comparison_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SDocComparisonPage.aspx");
        }
    }
}