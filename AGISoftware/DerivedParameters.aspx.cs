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
using OfficeOpenXml;
using OfficeOpenXml.Style;


namespace AGISoftware
{
    public partial class DerivedParameters : System.Web.UI.Page
    {
        public static string removeFilename="", removeFilepath="";
        protected void Page_Load(object sender, EventArgs e)
        {
         
            if (!IsPostBack)
            {
                bindSdocID();
                if (Session["ATKSDocID"] != null)
                {
         
                    txtsdocid.Text = Session["ATKSDocID"].ToString();
                    DerivedOutputParameter dop = new DerivedOutputParameter();
                    List<DerivedOutputParameter> listSignalProcess = new List<DerivedOutputParameter>();
                    string SDocstatus;
                    int success;
                    Session["ATKSDocID"] = txtsdocid.Text;
                    string id = txtsdocid.Text;
                    lvInputParam.DataSource = DBAccess.getDerivedInputParameter(id, out dop, out success, out listSignalProcess, out SDocstatus);
                    lvInputParam.DataBind();

                    sdocCompleteStatus.InnerText = SDocstatus;
                    txtSparkOutTime.Text = dop.Sparkouttime;
                    txtTangotTime.Text = dop.Targetrelieftime;
                    txtChipwidthratio.Text = dop.Chipwidthratio;
                    txtWheeltiltangle.Text = dop.Wheeltiltangle;
                    txtFeedGrindTime.Text = dop.TraverseSpeed;
                    txtSlideForward.Text = dop.SlideForward;
                    txtPrgmRead.Text = dop.ProgramRead;
                    txtFlagging.Text = dop.Flagging;
                    txtSlideRetuen.Text = dop.SlideReturn;
                    txtOther.Text = dop.Others;
                    txtothertimediscription.Text = dop.OthersTimeDescription;
                    txtLoadUnload.Text = dop.LoadUnloadTime;
                    txtManualLoadUnload.Text = dop.ManualLoadingUnloading;
                    txtRemarks.Text = dop.remarks;
                    txtDressingCycleTime.Text = dop.DressingCycleTime;

                    bindSignalProcessfile();
                 
                    if(success==1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('System Doc has incorrect datatype value. Please check input module data.');", true);
                        return;
                    }
                    
                    if (txtSparkOutTime.Text!="" || txtTangotTime.Text!="" || txtFeedGrindTime.Text != "" || txtSlideForward.Text != "" || txtPrgmRead.Text != "" || txtSlideRetuen.Text != "" || txtFlagging.Text != "" || txtOther.Text != "" || txtothertimediscription.Text != "" || txtLoadUnload.Text != "" || txtWheeltiltangle.Text!="" || txtChipwidthratio.Text!="" || txtManualLoadUnload.Text!="")
                    {
                        int calculatSuccess;
                        lvCalculatedPara.DataSource = DBAccess.getDerivedCalculateParameter(id, out dop,out calculatSuccess);
                        lvCalculatedPara.DataBind();
                        txtEquivalentDia.Text = dop.EquivalentDia;
                        txtEquivalentDiaFace.Text = dop.EquivalentDiaFace;
                        txtCuttingEdge.Text = dop.CuttingEdgeDensity;
                        txtSpartOutRev.Text = dop.SparkOutRevolutions;
                        txtGrindcycletime.Text = dop.GrindingCycletime;
                        txtNongrindingtime.Text = dop.NongrindingCycleTime;
                        txtTotalGrindingTime.Text = dop.TotalGrindingTime;
                        txtTotalcycletime.Text = dop.TotalCycletime;
                        txtFloortoFloor.Text = dop.FloorToFloor;
                        if(success==1)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('System Doc has incorrect datatype value. Please check input module data.');", true);
                            return;
                        }
                    }
                    checkSdocLockOrUnlock();
                    
                }
                else
                {
                    cbLockUnlock.Visible = false;
                }

                
            }
          
            selectSignalfile.Attributes["onchange"] = "UploadSignalFile(this)";
            if (Session["EmpName"] == null)
            {
                Response.Redirect("LoginPage.aspx");
            }
          
        }

        private void checkSdocLockOrUnlock()
        {
            string result = DBAccess.getSDocIdStatus(Session["ATKSDocID"].ToString());
            if(result=="Locked")
            {
                cbLockUnlock.Visible = true;
                cbLockUnlock.Text = result;
                cbLockUnlock.Checked = true;
                cbLockUnlock.Attributes["onclick"] = "return false";
                txtChipwidthratio.ReadOnly = true;
                txtWheeltiltangle.ReadOnly = true;
                txtSparkOutTime.ReadOnly = true;
                txtFeedGrindTime.ReadOnly = true;
                txtSlideForward.ReadOnly = true;
                txtPrgmRead.ReadOnly = true;
                txtFlagging.ReadOnly = true;
                txtSlideRetuen.ReadOnly = true;
                txtOther.ReadOnly = true;
                txtothertimediscription.ReadOnly = true;
                txtLoadUnload.ReadOnly = true;
                txtManualLoadUnload.ReadOnly = true;
                txtRemarks.ReadOnly = true;
                selectSignalfile.Enabled = false;
                for(int i=0;i<gvSignalProcessfiles.Rows.Count;i++)
                {
                    (gvSignalProcessfiles.Rows[i].FindControl("removeSignalProcessfile") as LinkButton).Enabled = false;
                }
            }
            else
            {
                cbLockUnlock.Visible = true;
                cbLockUnlock.Text = result;
                cbLockUnlock.Checked = false;
                cbLockUnlock.Attributes["onclick"] = "return true";
                txtChipwidthratio.ReadOnly = false;
                txtWheeltiltangle.ReadOnly = false;
                txtSparkOutTime.ReadOnly = false;
                txtFeedGrindTime.ReadOnly = false;
                txtSlideForward.ReadOnly = false;
                txtPrgmRead.ReadOnly = false;
                txtFlagging.ReadOnly = false;
                txtSlideRetuen.ReadOnly = false;
                txtOther.ReadOnly = false;
                txtothertimediscription.ReadOnly = false;
                txtLoadUnload.ReadOnly = false;
                txtManualLoadUnload.ReadOnly = false;
                txtRemarks.ReadOnly = false;
                selectSignalfile.Enabled = true;
                for (int i = 0; i < gvSignalProcessfiles.Rows.Count; i++)
                {
                    (gvSignalProcessfiles.Rows[i].FindControl("removeSignalProcessfile") as LinkButton).Enabled = true;
                }
            }

        }
        private void bindSdocID()
        {
            List<string> sdoclist = null;            sdoclist = DBAccess.getSDocForDelete("Delete", "SDocList");
            sdoclist.Insert(0, "");            var builder = new System.Text.StringBuilder();            if (sdoclist.Count > 0)            {                for (int i = 0; i < sdoclist.Count; i++)                {                    if (i == 0)                    {                        txtsdocid.Text = sdoclist[i].ToString();                    }                    builder.Append(String.Format("<option style='font-weight:unset' value='{0}'>", sdoclist[i].ToString()));                }            }            else            {                txtsdocid.Text = "";            }            SdocList.InnerHtml = builder.ToString();
        }
        protected void load_Click(object sender, EventArgs e)        {
            int success;
            DerivedOutputParameter dop = new DerivedOutputParameter();
            List<DerivedOutputParameter> listSignalProcess = new List<DerivedOutputParameter>();
            string SDocstatus="";
            Session["ATKSDocID"] = txtsdocid.Text;
            string id = txtsdocid.Text;
            lvInputParam.DataSource = DBAccess.getDerivedInputParameter(id, out dop,out success, out listSignalProcess, out SDocstatus);
            lvInputParam.DataBind();
            sdocCompleteStatus.InnerText = SDocstatus;
            txtSparkOutTime.Text = dop.Sparkouttime;
            txtTangotTime.Text = dop.Targetrelieftime;
            txtChipwidthratio.Text = dop.Chipwidthratio;
            txtWheeltiltangle.Text = dop.Wheeltiltangle;
            txtFeedGrindTime.Text = dop.TraverseSpeed;
            txtSlideForward.Text = dop.SlideForward;
            txtPrgmRead.Text = dop.ProgramRead;
            txtFlagging.Text = dop.Flagging;
            txtSlideRetuen.Text = dop.SlideReturn;
            txtOther.Text = dop.Others;
            txtothertimediscription.Text = dop.OthersTimeDescription;
            txtLoadUnload.Text = dop.LoadUnloadTime;
            txtManualLoadUnload.Text = dop.ManualLoadingUnloading;
            txtRemarks.Text = dop.remarks;
            txtDressingCycleTime.Text = dop.DressingCycleTime;
            bindSignalProcessfile();

            if (success == 1)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('System Doc has incorrect datatype value. Please check input module data.');", true);
                return;
            }
            //lvCalculatedPara.DataSource = DBAccess.getDerivedCalculateParameter(Session["ATKSDocID"].ToString(), out dop);
            //lvCalculatedPara.DataBind();
            checkSdocLockOrUnlock();

        }

        protected void calculateBtn_Click(object sender, EventArgs e)
        {
            int success;
            DerivedOutputParameter dop = new DerivedOutputParameter();
            if (Session["ATKSDocID"] != null)
            {
                if (cbLockUnlock.Text.Trim() == "UnLocked")
                {
                    int result = DBAccess.setSparkandTangoRelief(Session["ATKSDocID"].ToString(), txtSparkOutTime.Text,  Session["EmpName"].ToString(), txtFeedGrindTime.Text, txtSlideForward.Text, txtPrgmRead.Text, txtFlagging.Text, txtSlideRetuen.Text, txtOther.Text, txtLoadUnload.Text, txtChipwidthratio.Text, txtWheeltiltangle.Text, txtothertimediscription.Text, txtManualLoadUnload.Text,txtRemarks.Text);

                }
            }
            if (Session["ATKSDocID"] != null)
            {
                string id = txtsdocid.Text;
                
                if (id == Session["ATKSDocID"].ToString())
                {
                    lvCalculatedPara.DataSource = DBAccess.getDerivedCalculateParameter(id, out dop,out success);
                    lvCalculatedPara.DataBind();
                    txtEquivalentDia.Text = dop.EquivalentDia;
                    txtEquivalentDiaFace.Text = dop.EquivalentDiaFace;
                    txtCuttingEdge.Text = dop.CuttingEdgeDensity;
                    txtSpartOutRev.Text = dop.SparkOutRevolutions;
                    txtGrindcycletime.Text = dop.GrindingCycletime;
                    txtNongrindingtime.Text = dop.NongrindingCycleTime;
                    txtTotalGrindingTime.Text = dop.TotalGrindingTime;
                    txtTotalcycletime.Text = dop.TotalCycletime;
                    txtFloortoFloor.Text = dop.FloorToFloor;
                    if (success == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('System Doc has incorrect datatype value. Please check input module data.');", true);
                        return;
                    }
                }

            }
        }

        protected void outputModule_Click(object sender, EventArgs e)
        {
            Response.Redirect("OutputModules.aspx");
        }

        protected void SaveInputParameter_Click(object sender, EventArgs e)
        {
            if (Session["ATKSDocID"] != null || Session["ATKSDocID"].ToString()!="")
            {
              // int result= DBAccess.setSparkandTangoRelief(Session["ATKSDocID"].ToString(), txtSparkOutTime.Text,txtTangotTime.Text, Session["EmpName"].ToString());
            }
        }

        protected void calculateParameter_Click(object sender, EventArgs e)
        {
            DerivedCalculateParameter dcp = new DerivedCalculateParameter();
            for (int i = 0; i < lvCalculatedPara.Items.Count; i++)
            {
                //dip.Point =  (lvInputParam.Items[i].FindControl("point") as Label).Text;
                dcp.MRR = (lvCalculatedPara.Items[i].FindControl("mrr") as Label).Text;
                dcp.ToralMRR = (lvCalculatedPara.Items[i].FindControl("totalmrr") as Label).Text;
                dcp.GritPenetrationDepth = (lvCalculatedPara.Items[i].FindControl("gritpenetrationdepth") as Label).Text;
                dcp.Time = (lvCalculatedPara.Items[i].FindControl("time") as Label).Text;
                dcp.RadialDepthofCut = (lvCalculatedPara.Items[i].FindControl("radialdoc") as Label).Text;
                // int success= DBAccess.setDerivedParameters();
            }
        }

        protected void SaveSignalfile_Click(object sender, EventArgs e)
        {
            try
            {

                //string fileName = "";
                //byte[] filePath= { };
                //if (selectSignalfile.PostedFile != null)
                //{
                //    if (selectSignalfile.HasFile)
                //    {
                //        if (selectSignalfile.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                //        {
                //            fileName = selectSignalfile.FileName;
                //            BinaryReader br = new BinaryReader(selectSignalfile.PostedFile.InputStream);
                //            filePath = br.ReadBytes(selectSignalfile.PostedFile.ContentLength);
                //            Session["filepath"] = filePath;
                //            if (Session["ATKSDocID"] != null)
                //            {
                //                int result = DBAccess.setSignalFileName(Session["ATKSDocID"].ToString(), fileName, filePath, Session["EmpName"].ToString());
                //                signalfilename.InnerText = fileName;
                //                if (fileName == "")
                //                {
                //                    removeSignalfilepath.Visible = false;
                //                    lbtnviewExcel.Visible = false;
                //                }
                //                else
                //                {
                //                    removeSignalfilepath.Visible = true;
                //                    lbtnviewExcel.Visible = true;
                //                }
                //                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('Signalfile saved successfully!')</script>", false);
                //            }
                //            else
                //            {
                //                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('System Doc required.');", true);
                //            }
                //        }
                //        else
                //        {
                //            ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('Only xlsx file can upload.');", true);
                //        }

                //    }

                //}
                bool nonExcelfile = false;
                //foreach (HttpPostedFile postedFile in selectSignalfile.PostedFiles)
                //{ 
                //    if (Path.GetExtension(postedFile.FileName) == ".xls" || Path.GetExtension(postedFile.FileName) == ".xlsx")
                //    {

                //    }
                //    else
                //    {
                //        nonExcelfile = true;
                //    }
                //}

                foreach (HttpPostedFile postedFile in selectSignalfile.PostedFiles)
                {
                    //if (postedFile.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) || postedFile.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                    //{
                        string fileName = Path.GetFileName(postedFile.FileName);
                        string currentDateTime = System.DateTime.Now.ToString("ddMMyyhhmmssfffff");
                        string file = currentDateTime + "_" + fileName;
                        string filepath = "~/UploadSignalProcess/" + file;
                        postedFile.SaveAs(Server.MapPath("~/UploadSignalProcess/") + file);

                        if (Session["ATKSDocID"] != null)
                        {
                            int result = DBAccess.setSignalFileName(Session["ATKSDocID"].ToString(), fileName, filepath, Session["EmpName"].ToString());
                            // ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('Signalfile saved successfully!')</script>", false);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('System Doc required.');", true);
                        }
                    //}
                    //else
                    //{
                    //    nonExcelfile = true;
                    //}
                }
                //if (nonExcelfile)
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('Only xlsx file can upload.');", true);
                //}

                bindSignalProcessfile();
              

                //string filePath, fileName = "";
                //if (selectSignalfile.PostedFile != null)
                //{
                //    filePath = selectSignalfile.PostedFile.FileName;
                //    fileName = selectSignalfile.FileName;

                //}

                //if (Session["ATKSDocID"] != null)
                //{
                //    int result = DBAccess.setSignalFileName(Session["ATKSDocID"].ToString(), fileName, Session["EmpName"].ToString());
                //    signalfilename.InnerText = fileName;
                //    if (fileName == "")
                //    {
                //        removeSignalfilepath.Visible = false;
                //    }
                //    else
                //    {
                //        removeSignalfilepath.Visible = true;
                //    }
                //    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('Signalfile saved successfully!')</script>", false);
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('System Doc required');", true);
                //}




            }
            catch (Exception ex)
            {

            }
           
        }
       
        private void bindSignalProcessfile()
        {
            DerivedOutputParameter dop = new DerivedOutputParameter();
            List<DerivedOutputParameter> listSignalProcess = new List<DerivedOutputParameter>();
            string SDocstatus = "";
            int success;
            Session["ATKSDocID"] = txtsdocid.Text;
            string id = txtsdocid.Text;
            List<DerivedInputParameter> listderivedInputParameters =DBAccess.getDerivedInputParameter(id, out dop, out success, out listSignalProcess,out SDocstatus);
            
            gvSignalProcessfiles.DataSource = listSignalProcess;
            gvSignalProcessfiles.DataBind();
        }


        //[System.Web.Services.WebMethod(EnableSession = true)]
        //public static void setSignalFilepath()
        //{
        //    string filePath, fileName = "";
        //    if (selectSignalfile.PostedFile != null)
        //    {
        //        filePath = selectSignalfile.PostedFile.FileName; // file name with path.
        //        fileName = selectSignalfile.FileName;// Only file name.
        //    }

        //    if (Session["ATKSDocID"] != null || Session["ATKSDocID"].ToString() != "")
        //    {
        //        int result = DBAccess.setSignalFileName(Session["ATKSDocID"].ToString(), fileName, Session["EmpName"].ToString());
        //    }
        //    signalfilename.InnerText = fileName;
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('Signalfile saved successfully!')</script>", false);
        //}


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

        protected void removeImageConfimYes_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (Session["ATKSDocID"] != null)
                {
                    int result = DBAccess.removeSignalFileName(Session["ATKSDocID"].ToString(), removeFilename);
                   
                }

                string Source = string.Empty;
                Source = GetSignalProcessPath(removeFilepath.Replace("~/UploadSignalProcess/", ""));
                if (File.Exists(Source))
                {
                    File.Delete(Source);
                    //Logger.WriteDebugLog("Derived Parameter Signal Process- \n " + Source);
                }
                
                bindSignalProcessfile();


            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("remove Signal File");
            }
           
        }

        protected void viewFormulaa_Click(object sender, EventArgs e)
        {
            string emprole = DBAccess.getRoleOfEmp(Session["EmpName"] == null ? "" : Session["EmpName"].ToString());
            if (emprole != "Admin")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('Only Admin can see the Calculation Log.');", true);
                return;
            }
            DataTable dt, derivedLowerdt, inputModuedt;
            if (Session["ATKSDocID"] != null)
            {
                dt = DBAccess.getFormulaList(Session["ATKSDocID"].ToString(), out derivedLowerdt, out inputModuedt);
            }
            else
            {
                return;
            }
          
            string templatefile = string.Empty;
            string Filename = "Formula Sheet.xlsx";
            string Source = string.Empty;
            Source = GetReportPath(Filename);
            string Template = string.Empty;
            Template = "FormulaList_" + DateTime.Now + ".xlsx";
            string destination = string.Empty;
            destination = Path.Combine(appPath, "Temp", SafeFileName(Template));
            if (!File.Exists(Source))
            {
                Logger.WriteDebugLog("Application Tool Kit- \n " + Source);
            }
            if (dt.Rows.Count > 0)
            {
                FileInfo newFile = new FileInfo(Source);
                ExcelPackage Excel = new ExcelPackage(newFile, true);
                Excel.Workbook.Worksheets.Delete("Sheet1");

                var exelworksheet = Excel.Workbook.Worksheets.Add("Formula List");
                int cellRow = 1;
                exelworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                exelworksheet.Cells[cellRow, cellRow, cellRow, 10].Value = "Automation of Grinding Process Intelligence (AGI)";
                exelworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                exelworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(231, 231, 231));
                exelworksheet.Cells[cellRow, cellRow, cellRow, 10].Merge = true;
                exelworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                exelworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.Font.Size = 18;
                exelworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.Font.Bold = true;
                exelworksheet.Cells[cellRow, cellRow, cellRow, 10].Style.Font.Color.SetColor(Color.Red);

                cellRow = cellRow + 1;
                exelworksheet.Cells[cellRow, 1, cellRow, 1].Value = "Username: " + Session["EmpName"].ToString();
                exelworksheet.Cells[cellRow, 2, cellRow, 2].Style.Font.Bold = true;
                exelworksheet.Cells[cellRow, 1, cellRow, 1].Style.Font.Bold = true;
                exelworksheet.Cells[cellRow, 2, cellRow, 2].Value = "DateTime: " + DateTime.Now;


                cellRow = cellRow + 2;

                exelworksheet.Cells[cellRow, 1, cellRow, 10].Value = "Calculation Log";
                exelworksheet.Cells[cellRow, 1, cellRow, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                exelworksheet.Cells[cellRow, 1, cellRow, 10].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(209, 242, 208));
                exelworksheet.Cells[cellRow, 1, cellRow, 10].Merge = true;
                exelworksheet.Cells[cellRow, 1, cellRow, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                exelworksheet.Cells[cellRow, 1, cellRow, 10].Style.Font.Size = 14;
                exelworksheet.Cells[cellRow, 1, cellRow, 10].Style.Font.Bold = true;
                exelworksheet.Cells[cellRow, 1, cellRow, 10].Style.Font.Color.SetColor(Color.FromArgb(78, 73, 73));
                cellRow++;

                List<string> distinctSDoc = new List<string>();
                string previousSDoc = "";

                DataView dv = new DataView(dt);
                DataTable SDocdt = dv.ToTable(true, "SDocId");
                //  int cellColumn = 1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int flag = 0;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string str = dt.Rows[i][j].ToString();
                        if (str != "")
                        {
                            exelworksheet.Cells[cellRow, 1].Value = str;
                            exelworksheet.Cells[cellRow, 1].Style.Font.Color.SetColor(Color.FromArgb(245, 95, 69));
                            exelworksheet.Cells[cellRow, 1].Style.Font.Bold = true;
                            exelworksheet.Cells[cellRow, 1].Style.Font.Size = 12;

                            flag = 1;
                            break;
                        }
                    }
                    if (flag == 1)
                    {
                        break;
                    }
                }

                cellRow++;

                exelworksheet.Cells[cellRow, 1].Value = "Data Input Module";
                exelworksheet.Cells[cellRow, 1].Style.Font.Color.SetColor(Color.FromArgb(78, 73, 73));
                exelworksheet.Cells[cellRow, 1].Style.Font.Bold = true;
                exelworksheet.Cells[cellRow, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                exelworksheet.Cells[cellRow, 1].Style.Fill.BackgroundColor.SetColor(Color.Yellow);
                cellRow++;

                for (int i = 1; i <= 2; i++)
                {
                    if (i == 1)
                    {
                        exelworksheet.Cells[cellRow, i].Value = "Identifier";
                    }
                    if (i == 2)
                    {
                        exelworksheet.Cells[cellRow, i].Value = "Value";
                    }
                    exelworksheet.Cells[cellRow, i].Style.Font.Bold = true;
                }
                cellRow++;

                inputModuedt.Columns.Remove("SDocId");
                for (int i = 0; i < inputModuedt.Rows.Count; i++)
                {
                    int cellColumn = 1;
                    for (int j = 0; j < inputModuedt.Columns.Count; j++)
                    {
                        if (inputModuedt.Rows[i][j].ToString() != "")
                        {
                            try
                            {
                                if (j == 1)
                                {
                                    string[] result;
                                    result = inputModuedt.Rows[i][j].ToString().Split(';');
                                    string s = result[4];
                                    if (result[4] == " ")
                                    {
                                        result[4] = "0";
                                    }
                                    exelworksheet.Cells[cellRow, cellColumn].Value = result[2] + " ; " + result[3] + " ; " + result[4];
                                    cellColumn++;
                                }
                                else
                                {
                                    exelworksheet.Cells[cellRow, cellColumn].Value = inputModuedt.Rows[i][j].ToString();
                                    cellColumn++;
                                }

                            }
                            catch(Exception ex)
                            {

                            }
                           

                        }
                        else
                        {
                            exelworksheet.Cells[cellRow, cellColumn].Value = inputModuedt.Rows[i][j].ToString();
                            cellColumn++;
                        }
                    }
                    cellRow++;
                }

                cellRow++;

                exelworksheet.Cells[cellRow, 1].Value = "Derived Parameters";
                exelworksheet.Cells[cellRow, 1].Style.Font.Bold = true;
                exelworksheet.Cells[cellRow, 1].Style.Font.Color.SetColor(Color.FromArgb(78, 73, 73));
                exelworksheet.Cells[cellRow, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                exelworksheet.Cells[cellRow, 1].Style.Fill.BackgroundColor.SetColor(Color.Yellow);

                cellRow++;


                for (int j = 1; j <= 29; j++)
                {
                    if (j == 1)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Identifier";
                    }
                    else if (j == 2)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Dia (x) (mm)";
                    }
                    else if (j == 3)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Stock Diametrically (mm)";
                    }
                    else if (j == 4)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Stock on Face (mm)";
                    }
                    else if (j == 5)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "In Feed (mm/min)";
                    }
                    //else if (j == 6)
                    //{
                    //    exelworksheet.Cells[cellRow, j].Value = "Work Speed (m/sec)";
                    //}
                    else if (j == 6)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Grinding OD Width (mm)";
                    }
                    else if (j == 7)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Feed Angle";
                    }
                    else if (j == 8)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Work Speed (m/min) OD";
                    }
                    else if (j == 9)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Work Speed (m/min) Face";
                    }
                    else if (j == 10)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "X-Feed (mm/min)";
                    }
                    else if (j == 11)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Z-Feed (mm/min)";
                    }
                    else if (j == 12)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "MRR (X) (cu.mm/sec)";
                    }
                    else if (j == 13)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "MRR (Z) (cu.mm/sec)";
                    }
                    else if (j == 14)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "MRR'(X) (cu.mm/mm/sec)";
                    }
                    else if (j == 15)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "MRR'(Z) (cu.mm/mm/sec)";
                    }
                    else if (j == 16)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Total MRR' (cu.mm/sec)";
                    }
                    else if (j == 17)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Total MRR (cu.mm/sec)";
                    }
                    else if (j == 18)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Wheel Work Speed Ratio";
                    }
                    else if (j == 19)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Wheel Work RPM Ratio";
                    }
                    else if (j == 20)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Grit Penetration Depth (X) (μm)";
                    }
                    else if (j == 21)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Grit Penetration Depth (Z) (μm)";
                    }
                    else if (j == 22)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Time X (sec)";
                    }
                    else if (j == 23)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Time Z (sec)"; 
                    }
                    else if (j == 24)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Time (sec)";
                    }
                    else if (j == 25)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Radial Depth of Cut (X) (mm/rev)";
                    }
                    else if (j == 26)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Depth of Cut (Z) (mm/rev)";
                    }
                    else if (j == 27)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Tango Time X (sec)";
                    }
                    else if (j == 28)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Tango Time Z (sec)";
                    }
                    else if (j == 29)
                    {
                        exelworksheet.Cells[cellRow, j].Value = "Tango Time (sec)";
                    }

                    exelworksheet.Cells[cellRow, j].Style.Font.Bold = true;
                }
                cellRow++;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int cellColumn = 1;
                    bool tangoColoChange = false;
                    for (int j = 1; j < dt.Columns.Count; j++)
                    {

                        if (j == 1)
                        {
                            if (dt.Rows[i][j].ToString() != "")
                            {
                                try
                                {
                                    string[] parameter;
                                    parameter = dt.Rows[i][j].ToString().Split(':');
                                    exelworksheet.Cells[cellRow, cellColumn].Value = parameter[0];
                                    cellColumn++;
                                    string[] result;
                                    result = dt.Rows[i][j].ToString().Split(';');
                                    exelworksheet.Cells[cellRow, cellColumn].Value = result[2] + " ; " + result[3] + " ; " + result[4];
                                    cellColumn++;
                                }
                                catch(Exception ex)
                                {

                                }
                               
                            }
                            else
                            {
                                exelworksheet.Cells[cellRow, cellColumn].Value = dt.Rows[i][j].ToString();
                                cellColumn++;
                                exelworksheet.Cells[cellRow, cellColumn].Value = dt.Rows[i][j].ToString();
                                cellColumn++;
                            }

                            continue;
                        }
                        //Dia
                        if (j == 2 || j==3)
                        {
                            if (dt.Rows[i][j].ToString() != "")
                            {
                                try
                                {
                                    string[] results;
                                    results = dt.Rows[i][j].ToString().Split(';');
                                    exelworksheet.Cells[cellRow, cellColumn].Value = results[2] + " ; " + results[3] + " ; " + results[4];
                                    if (results[4]!="" )
                                    {
                                        if (results[4].Contains('-'))
                                        {
                                            tangoColoChange = true;
                                        }
                                    }
                                    cellColumn++;
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                            else
                            {
                                exelworksheet.Cells[cellRow, cellColumn].Value = dt.Rows[i][j].ToString();
                                cellColumn++;
                            }
                            continue;
                        }
                        

                        if (j == 23)
                        {
                            if (dt.Rows[i][j].ToString() != "")
                            {
                                try
                                {
                                    string[] results;
                                    results = dt.Rows[i][j].ToString().Split(';');
                                    exelworksheet.Cells[cellRow, cellColumn].Value = "t" + i + ": " + results[2] + " ; " + results[3] + " ; " + results[4];
                                    cellColumn++;
                                }
                                catch ( Exception ex)
                                {
                                    exelworksheet.Cells[cellRow, cellColumn].Value = "t" + i + ": " + "" + " ; " + "" + " ; " + "";
                                    cellColumn++;
                                }
                              
                            }
                            else
                            {
                                exelworksheet.Cells[cellRow, cellColumn].Value = "t" + i + ": " + dt.Rows[i][j].ToString();
                                cellColumn++;
                            }

                            continue;
                        }

                        if (j > 1)
                        {
                            if (dt.Rows[i][j].ToString() != "")
                            {
                                try
                                {
                                    string[] results;
                                    results = dt.Rows[i][j].ToString().Split(';');
                                    exelworksheet.Cells[cellRow, cellColumn].Value = results[2] + " ; " + results[3] + " ; " + results[4];
                                    cellColumn++;
                                }
                                catch(Exception ex)
                                {

                                }
                              
                            }
                            else
                            {
                                exelworksheet.Cells[cellRow, cellColumn].Value = dt.Rows[i][j].ToString();
                                cellColumn++;
                            }

                        }
                    }
                    if (tangoColoChange)
                    {
                        exelworksheet.Row(cellRow).Style.Font.Color.SetColor(Color.Red);
                    }
                    cellRow++;
                }
                for (int i = 1; i <= 29; i++)
                {
                    exelworksheet.Cells[3, i, cellRow, i].AutoFitColumns();
                }
                cellRow++;
                for (int i = 1; i <= 2; i++)
                {
                    if (i == 1)
                    {
                        exelworksheet.Cells[cellRow, i].Value = "Identifier";
                    }
                    if (i == 2)
                    {
                        exelworksheet.Cells[cellRow, i].Value = "Value";
                    }
                    exelworksheet.Cells[cellRow, i].Style.Font.Bold = true;
                }
                cellRow++;
                for (int i = 0; i < derivedLowerdt.Rows.Count; i++)
                {

                    for (int j = 1; j < derivedLowerdt.Columns.Count; j++)
                    {
                        int cellColumn = 1;
                        string[] result;

                        if (derivedLowerdt.Rows[i][j].ToString() != "")
                        {
                            try
                            {
                                result = derivedLowerdt.Rows[i][j].ToString().Split(';');
                                //exelworksheet.Cells[cellRow, cellColumn].Value = result[0];
                                exelworksheet.Cells[cellRow, cellColumn].Value = derivedLowerdt.Columns[j].ColumnName.ToString();
                                cellColumn++;
                                exelworksheet.Cells[cellRow, cellColumn].Value = result[2] + " ; " + result[3] + " ; " + result[4];
                                cellRow++;

                            }
                            catch(Exception ex)
                            {
                                exelworksheet.Cells[cellRow, cellColumn].Value = "" + " ; " + "" + " ; " + "";
                                cellRow++;
                            }
                          
                        }
                        else
                        {
                            exelworksheet.Cells[cellRow, cellColumn].Value = derivedLowerdt.Columns[j].ColumnName.ToString();
                            cellColumn++;
                            exelworksheet.Cells[cellRow, cellColumn].Value = derivedLowerdt.Rows[i][j].ToString();
                            cellRow++;
                        }

                    }

                }

                //for(int d=0;d<SDocdt.Rows.Count;d++)
                //{
                //    for (int i = 1; i < dt.Rows.Count; i++)
                //    {
                //        int cellColumn = 1;
                //        for (int j = 1; j < dt.Columns.Count; j++)
                //        {
                //            if(SDocdt.Rows[d][d].ToString()==dt.Rows[i][1].ToString())
                //            {
                //                string[] result;
                //                result = dt.Rows[i][j].ToString().Split(';');
                //                exelworksheet.Cells[cellRow, cellColumn].Value = result[0];
                //                exelworksheet.Cells[cellRow, cellColumn + 1].Value = result[1];
                //                exelworksheet.Cells[cellRow, cellColumn + 2].Value = result[2];
                //                exelworksheet.Cells[cellRow, cellColumn + 3].Value = result[3];
                //                if (result[4].ToString() == "")
                //                {
                //                    exelworksheet.Cells[cellRow, cellColumn + 4].Value = result[4];
                //                }
                //                else
                //                {
                //                    exelworksheet.Cells[cellRow, cellColumn + 4].Value = Convert.ToDecimal(result[4]);
                //                }
                //            }
                //            else
                //            {
                //                cellRow++;
                //            }
                //        }
                //        cellRow++;
                //    }
                //}
                //for (int i = 0; i < SDocdt.Rows.Count; i++)
                //{
                //    for (int j = 0; j < SDocdt.Columns.Count; j++)
                //    {
                //        if(dt.Rows[i][j].ToString()== previousSDoc)
                //        {
                //            continue;
                //        }else
                //        {
                //            distinctSDoc.Add(dt.Rows[i][j].ToString());
                //        }
                //        previousSDoc = dt.Rows[i][j].ToString();
                //    }

                //}
                //for (int j = 0; j < 5; j++)
                //{
                //    if(j==0)
                //    {
                //        exelworksheet.Cells[cellRow, j + 1].Value = "Parameter Name";
                //    }else if (j == 1)
                //    {
                //        exelworksheet.Cells[cellRow, j + 1].Value = "SDoc ID";
                //    }else if (j == 2)
                //    {
                //        exelworksheet.Cells[cellRow, j + 1].Value = "Formula";
                //    }
                //    else if (j == 3)
                //    {
                //        exelworksheet.Cells[cellRow, j + 1].Value = "Formula String";
                //    }
                //    else if (j == 4)
                //    {
                //        exelworksheet.Cells[cellRow, j + 1].Value = "Output";
                //    }
                //    exelworksheet.Cells[cellRow, j + 1].Style.Font.Bold = true;
                //}

                //cellRow++;
                //for (int i = 1; i < dt.Rows.Count; i++)
                //{
                //    int cellColumn = 1;
                //    for (int j = 2; j < dt.Columns.Count; j++)
                //    {
                //        string[] result;
                //        result = dt.Rows[i][j].ToString().Split(';');
                //        exelworksheet.Cells[cellRow, cellColumn].Value = result[0];
                //        exelworksheet.Cells[cellRow, cellColumn + 1].Value = result[1];
                //        exelworksheet.Cells[cellRow, cellColumn + 2].Value = result[2];
                //        exelworksheet.Cells[cellRow, cellColumn + 3].Value = result[3];
                //        if(result[4].ToString()=="")
                //        {
                //            exelworksheet.Cells[cellRow, cellColumn + 4].Value = result[4];
                //        }
                //        else
                //        {
                //            exelworksheet.Cells[cellRow, cellColumn + 4].Value = Convert.ToDecimal(result[4]);
                //        }

                //    }
                //    cellRow++;
                //}

                DownloadFile(destination, Excel.GetAsByteArray());
            }
        }

        protected void lbtnviewExcel_Click(object sender, EventArgs e)
        {
            //try
            //{
   
            //    string fileextension = "";
            //    if(Session["filepath"] !=null)
            //    {
            //        byte[] signalfileinbinary = (byte[]) Session["filepath"];
            //        if (signalfilename.InnerText.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
            //        {
            //            //Response.Buffer = true;
            //            //Response.Charset = "";
            //            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //            //Response.ContentType = "application/xlsx";
            //            //Response.BinaryWrite(signalfileinbinary);
            //            //Response.Flush();
            //            //Response.End();
            //            HttpContext.Current.Response.Clear();
            //            HttpContext.Current.Response.Charset = "";
            //            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + signalfilename.InnerText + "\"");
            //            HttpContext.Current.Response.OutputStream.Write(signalfileinbinary, 0, signalfileinbinary.Length);
            //            HttpContext.Current.Response.Flush();
            //            HttpContext.Current.Response.SuppressContent = true;
            //            HttpContext.Current.ApplicationInstance.CompleteRequest();
            //        }

            //    }

            //}
            //catch (Exception ex)
            //{
            //    Logger.WriteErrorLog(ex.Message);
            //}

        }


        protected void gvSignalProcessfiles_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //gvParameterList.EditIndex = e.NewEditIndex;
            string filepath = (gvSignalProcessfiles.Rows[e.NewEditIndex].FindControl("hdfilepath") as HiddenField).Value;
            string filename= (gvSignalProcessfiles.Rows[e.NewEditIndex].FindControl("lbfilename") as LinkButton).Text;
            string file = filepath.Replace("~/UploadSignalProcess/", "");
            try
            {

                string fileextension = "";
                if (filepath != null || filepath !="")
                {
                    string Source = string.Empty;
                    Source = GetSignalProcessPath(file);
                    if (!File.Exists(Source))
                    {
                        Logger.WriteDebugLog("Derived Parameter Signal Process- \n " + Source);
                        ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('Uploaded file not found.');", true);
                    }
                    string destination = string.Empty;
                    destination = Path.Combine(appPath, "UploadSignalProcess", SafeFileName(file));
                    string contentType = string.Empty;
                    contentType = "application/" + Path.GetExtension(filename);
                    DownloadSignalProcessFile(filename, destination, contentType);

                    //if (filename.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                    //{
                    //    DownloadSignalProcessFile(filename, destination, contentType);

                    //}
                    //else

                    //if (filename.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase) || filename.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                    //{
                    //    DownloadSignalProcessFile(filename, destination, contentType);
                    //    // FileInfo newFile = new FileInfo(Source);
                    //    //ExcelPackage Excel = new ExcelPackage(newFile, true);
                    //    //DownloadSignalProcessFile(filename, Excel.GetAsByteArray());
                    //}
                }

            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }

        }
        public static string GetSignalProcessPath(string reportName)
        {
            string src;
            if (HttpContext.Current.Session["Language"] == null)
                src = Path.Combine(appPath, "UploadSignalProcess", reportName);
            else
            {
                if (HttpContext.Current.Session["Language"].ToString() != "en")
                    src = Path.Combine(appPath, "UploadSignalProcess-" + HttpContext.Current.Session["Language"].ToString() + "", reportName);
                else
                    src = Path.Combine(appPath, "UploadSignalProcess", reportName);
            }
            return src;
        }
        private static void DownloadSignalProcessFile(string filename, string destination, string contetType)
        {
            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.Charset = "";
            //HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
            //HttpContext.Current.Response.OutputStream.Write(bytearray, 0, bytearray.Length);
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.SuppressContent = true;
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
            try
            {
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.ContentType = contetType;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=\"" + filename + "\"");
                HttpContext.Current.Response.TransmitFile(destination);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.SuppressContent = true;
                HttpContext.Current.ApplicationInstance.CompleteRequest();

            }
            catch(Exception ex)
            {

            }
           

        }

        protected void lockConfirmationYes_ServerClick(object sender, EventArgs e)
        {
            if(Session["ATKSDocID"]!=null)
            {
                int result=DBAccess.setSDocLockStatus(Session["ATKSDocID"].ToString(), Session["EmpName"].ToString());
                if (result.Equals(0))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openErrorModal('Failed to lock the SDocID');", true);
                    return;
                }
                checkSdocLockOrUnlock();
                ScriptManager.RegisterStartupScript(this, typeof(Page), "Success" + 1, "<script>showpop5('SDocID locked successfully!')</script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "openWarningModal('System Doc required.');", true);
            }
            
        }

        protected void lvInputParam_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListViewDataItem dataItem = (ListViewDataItem)e.Item;

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                string tangoOD = (e.Item.FindControl("TangoFlagOD") as HiddenField).Value;
                string tangoFlag = (e.Item.FindControl("TangoFlagFace") as HiddenField).Value;
                if(tangoOD=="1" || tangoFlag == "1")
                {
                    HtmlTableRow row = e.Item.FindControl("inputtblrow") as HtmlTableRow;
                   //// row.FindControl
                   //row.ad = "Green";
                   //(e.Item.FindControl("") as Label)
                }
            }

        }

        protected void gvSignalProcessfiles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            removeFilepath = (gvSignalProcessfiles.Rows[e.RowIndex].FindControl("hdfilepath") as HiddenField).Value;
             removeFilename = (gvSignalProcessfiles.Rows[e.RowIndex].FindControl("lbfilename") as LinkButton).Text;
            //removeFilename = removeFilename.Replace("~/UploadSignalProcess/", "");
            ScriptManager.RegisterStartupScript(this, GetType(), "RecordsTextopenModaladded", "removeSignalFilepathConfirm('Are you sure, you want to remove this Signal file path?');", true);
        }
    }
}