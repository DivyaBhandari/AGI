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
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                            dbDate = sdr["ScriptDate"].ToString();
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
            Session["EmpName"] = null;
            if (Session["AdminName"] != null )
            {
                loggedUser.Text = Session["AdminName"].ToString();
                return;
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
           
            //if (Session["EmpName"]!=null)
            //{
            //    string role = DBAccess.CheckRole(Session["EmpName"].ToString());
            //    if(role=="Operator")
            //    {
            //        Response.Redirect("LoginPage.aspx");
            //    }
            //}
        }
    }
}