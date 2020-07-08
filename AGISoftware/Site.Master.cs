using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AGISoftware.DataBaseAccess;
using AGISoftware.Model;

namespace AGISoftware
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Page.Form.Attributes.Add("enctype", "multipart/form-data");
           
            if (!IsPostBack)
            {
                hdSignalProcess.Value = Utility.SignalProcessPath;

                hideshowList();

                string dbDate = "", dbVNo = "";
              
                SqlConnection con = ConnectionManager.GetConnection();
                SqlDataReader sdr = null;
                SqlCommand cmd = null;
                List<string> list = new List<string>();
                try
                {
                    cmd = new SqlCommand("select ScriptDate,DbVersionNumber from DatabaseVersion where ScriptDate=(select  max(ScriptDate) from DatabaseVersion)", con);
                    cmd.CommandType = CommandType.Text;
                    sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            dbDate= sdr["ScriptDate"].ToString();
                            dbVNo = sdr["DbVersionNumber"].ToString();
                        }
                    }

                }
                catch (Exception ex)
                {
                    Logger.WriteDebugLog("While getting latest DB version number" + ex.Message);
                }
                finally
                {
                    if (con != null) con.Close(); if (sdr != null) sdr.Close();
                }
                dbVersionNo.InnerText = dbVNo;
                dbReleaseDate.InnerText = dbDate == "" ? "" : Convert.ToDateTime(dbDate).ToString("dd/MM/yyyy");
            }
           
          //  Session["AdminName"] = null;
            if (Session["EmpName"] !=null)
            {
                loggedUser.Text = Session["EmpName"].ToString();
                return;
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
           
        }

        private void hideshowList()
        {
            List<RoleAccessRight> pageList = (List<RoleAccessRight>)Session["PageList"];
            if(pageList!=null)
            {
                ApplicationToolKit.Visible = pageList.Where(ss => ss.Page.Equals("ApplicationToolKit", StringComparison.OrdinalIgnoreCase)).Select(ss => ss.visibilty).FirstOrDefault();
                DataInputModule.Visible = pageList.Where(ss => ss.Page.Equals("DataInputModule", StringComparison.OrdinalIgnoreCase)).Select(ss => ss.visibilty).FirstOrDefault();
                DerivedParameters.Visible = pageList.Where(ss => ss.Page.Equals("DerivedParameters", StringComparison.OrdinalIgnoreCase)).Select(ss => ss.visibilty).FirstOrDefault();
                OutputModules.Visible = pageList.Where(ss => ss.Page.Equals("OutputModules", StringComparison.OrdinalIgnoreCase)).Select(ss => ss.visibilty).FirstOrDefault();
                SignalProcess.Visible = pageList.Where(ss => ss.Page.Equals("SignalProcess", StringComparison.OrdinalIgnoreCase)).Select(ss => ss.visibilty).FirstOrDefault();
                MasterData.Visible = pageList.Where(ss => ss.Page.Equals("MasterData", StringComparison.OrdinalIgnoreCase)).Select(ss => ss.visibilty).FirstOrDefault();
                ParameterMaster.Visible = pageList.Where(ss => ss.Page.Equals("ParameterMaster", StringComparison.OrdinalIgnoreCase)).Select(ss => ss.visibilty).FirstOrDefault();
                OperatorDetailsMaster.Visible = pageList.Where(ss => ss.Page.Equals("OperatorDetailsMaster", StringComparison.OrdinalIgnoreCase)).Select(ss => ss.visibilty).FirstOrDefault();
                ParametersRelationshipMaster.Visible = pageList.Where(ss => ss.Page.Equals("ParametersRelationshipMaster", StringComparison.OrdinalIgnoreCase)).Select(ss => ss.visibilty).FirstOrDefault();
                AssignValueForDependency.Visible = pageList.Where(ss => ss.Page.Equals("AssignValueForDependency", StringComparison.OrdinalIgnoreCase)).Select(ss => ss.visibilty).FirstOrDefault();
                DeleteSDoc.Visible = pageList.Where(ss => ss.Page.Equals("DeleteSDoc", StringComparison.OrdinalIgnoreCase)).Select(ss => ss.visibilty).FirstOrDefault();
                UnlockSdocID.Visible = pageList.Where(ss => ss.Page.Equals("UnlockSdocID", StringComparison.OrdinalIgnoreCase)).Select(ss => ss.visibilty).FirstOrDefault();
                InputModuleMasterView.Visible = pageList.Where(ss => ss.Page.Equals("InputModuleMasterView", StringComparison.OrdinalIgnoreCase)).Select(ss => ss.visibilty).FirstOrDefault();
                AssignPagesforUser.Visible = pageList.Where(ss => ss.Page.Equals("AssignPagesforUser", StringComparison.OrdinalIgnoreCase)).Select(ss => ss.visibilty).FirstOrDefault();
                ParameterDependenacyList.Visible = pageList.Where(ss => ss.Page.Equals("ParameterDependenacyList", StringComparison.OrdinalIgnoreCase)).Select(ss => ss.visibilty).FirstOrDefault();
                if (ApplicationToolKit.Visible==false && DataInputModule.Visible==false&&DerivedParameters.Visible==false&&OutputModules.Visible==false && SignalProcess.Visible==false)
                {
                    showTransactions.Visible = false;

                }
                else
                {
                    showTransactions.Visible = true;
                }
                if (MasterData.Visible == false && ParameterMaster.Visible == false && OperatorDetailsMaster.Visible == false && ParametersRelationshipMaster.Visible == false && AssignValueForDependency.Visible == false && DeleteSDoc.Visible == false && InputModuleMasterView.Visible == false && AssignPagesforUser.Visible == false && UnlockSdocID.Visible==false)
                {
                    showMasters.Visible = false;
                }
                else
                {
                    showMasters.Visible = true;
                }
            }
           
        }
    }
}