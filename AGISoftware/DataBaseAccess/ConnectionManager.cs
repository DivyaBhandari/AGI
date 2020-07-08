using Elmah;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Configuration;

namespace AGISoftware.DataBaseAccess
{
    public class ConnectionManager
    {


        #region "Create Connection String---"
        //  static string conString = WebConfigurationManager.ConnectionStrings["ConnString"].ToString();
       
       
        public static bool timeOut = false;
        public static SqlConnection GetConnection()
        {
            string conString="";
            if (HttpContext.Current.Session["LevelType"] ==null)
            {
               HttpContext.Current.Response.Redirect("~/LevelAccesssPage.aspx");
            }else
            {
                conString = WebConfigurationManager.ConnectionStrings[HttpContext.Current.Session["LevelType"].ToString()].ToString();
            }
             
            bool writeDown = false;
            DateTime dt = DateTime.Now;
            SqlConnection conn = new SqlConnection(conString);
            do
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    if (writeDown == false)
                    {
                        dt = DateTime.Now.AddSeconds(60);
                        Logger.WriteErrorLog(ex.Message);
                        ErrorSignal.FromCurrentContext().Raise(ex);
                        writeDown = true;

                    }
                    if (dt < DateTime.Now)
                    {
                        Logger.WriteErrorLog(ex.Message);
                        ErrorSignal.FromCurrentContext().Raise(ex);
                        throw;
                    }
                    Thread.Sleep(1000);
                }

            } while (conn.State != ConnectionState.Open);
            return conn;
        }
        #endregion
    }
}
