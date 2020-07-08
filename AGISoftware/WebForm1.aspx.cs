using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AGISoftware.DataBaseAccess;
using AGISoftware.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Drawing.Chart;
using Spire.Xls;
using System.IO;
using System.Text;

namespace AGISoftware
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                // Create a new DataTable.    
                DataTable custTable = new DataTable("Customers");
                DataColumn dtColumn;
                DataRow myDataRow;

                // Create id column  
                dtColumn = new DataColumn();
                dtColumn.ColumnName = "id"; 
                custTable.Columns.Add(dtColumn);


                for(int i=0;i<3;i++)
                {
                    myDataRow = custTable.NewRow();
                    myDataRow["id"] = "SDoc000001";
                    custTable.Rows.Add(myDataRow);
                }
                DataTable dt = DBAccess.getSystemDocumentData();
                gv.DataSource = custTable;
                gv.DataBind();
            }
        }

        protected void export_Click(object sender, EventArgs e)
        {
            try
            {
                string templatefile = string.Empty;
                string Filename = "exceTopdf.xlsx";
                string Source = string.Empty;
                Source = GetReportPath(Filename);
                string Template = string.Empty;
                Template = "exceTopdf_" + DateTime.Now + ".xlsx";
                string destination = string.Empty;
                destination = Path.Combine(appPath, "Temp", SafeFileName(Template));
                if (!File.Exists(Source))
                {
                    Logger.WriteDebugLog("exceTopdf- \n " + Source);
                }
                FileInfo newFile = new FileInfo(Source);
                ExcelPackage Excel = new ExcelPackage();
                //var ws = Excel.Workbook.Worksheets.Add("Data");
                //Excel.Workbook.Worksheets.Delete("Sheet1");

                var exelworksheet = Excel.Workbook.Worksheets.Add("Data");
                exelworksheet.Cells[6, 1, 6, 5].Value = "Automation of Grinding Process Intelligence (AGI)";
                Excel.SaveAs(newFile);
                Workbook workbook = new Workbook();
                workbook.LoadFromFile(newFile.FullName);
                workbook.SaveToFile("exceTopdf.pdf", Spire.Xls.FileFormat.PDF);
                System.Diagnostics.Process.Start("exceTopdf.pdf");
            }
            catch (Exception ex)
            {

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
    }
}