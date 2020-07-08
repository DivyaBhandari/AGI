using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using AGISoftware.DataBaseAccess;
using AGISoftware.Model;

namespace AGISoftware
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Session["PageList"] = null;
                if(Session["LevelType"]==null || Session["LevelTypeName"] ==null)
                {
                    Response.Redirect("~/LevelAccesssPage.aspx", false);
                }
                else
                {
                    leveltype.InnerText = Session["LevelTypeName"].ToString();
                }
            }
         
        }
        protected void loginBtn_ServerClick(object sender, EventArgs e)
        {

            try
            {

                if (!string.IsNullOrEmpty(txtUsername.Value) && !string.IsNullOrEmpty(txtPassword.Value))
                {

                    string UserRole = DBAccess.CheckSupLoginData(txtUsername.Value, txtPassword.Value);
                    if (Session["LevelType"].ToString() == "level1" || Session["LevelType"].ToString() == "level2")
                    {
                        if (UserRole == "Normal User")
                        {
                            List<RoleAccessRight> listPage = new List<RoleAccessRight>();
                            listPage = DBAccess.getPagesforRole(txtUsername.Value);
                            FormsAuthentication.SetAuthCookie(this.txtUsername.Value.Trim(), true);
                            FormsAuthenticationTicket ticket1 = new FormsAuthenticationTicket(1, this.txtUsername.Value.Trim(),
                                DateTime.Now, DateTime.Now.AddMinutes(480), true, txtUsername.Value);
                            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket1));
                            Response.Cookies.Add(cookie1);
                            if (listPage.Count > 0)
                            {
                                Session["EmpName"] = txtUsername.Value;
                                Session["PageList"] = listPage;
                                Response.Redirect("~/" + listPage[0].Page.ToString() + ".aspx", false);
                            }

                            else
                            {
                                errorMsg.InnerText = "Not assigned any pages for this Employee";
                                errorMsg.Visible = true;
                                txtUsername.Value = "";
                            }

                        }
                        else if (UserRole == "Master User")
                        {
                            List<RoleAccessRight> listPage = new List<RoleAccessRight>();
                            RoleAccessRight roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "MasterData";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "ParameterMaster";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "OperatorDetailsMaster";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "ParametersRelationshipMaster";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "AssignValueForDependency";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "DeleteSDoc";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "InputModuleMasterView";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "AssignPagesforUser";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "ParameterDependenacyList";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "UnlockSdocID";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);

                            FormsAuthentication.SetAuthCookie(this.txtUsername.Value.Trim(), true);
                            FormsAuthenticationTicket ticket1 = new FormsAuthenticationTicket(1, this.txtUsername.Value.Trim(),
                                DateTime.Now, DateTime.Now.AddMinutes(480), true, txtUsername.Value);
                            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket1));
                            Response.Cookies.Add(cookie1);
                            if (listPage.Count > 0)
                            {
                                Session["EmpName"] = txtUsername.Value;
                                Session["PageList"] = listPage;
                                Response.Redirect("~/" + listPage[0].Page.ToString() + ".aspx", false);
                            }
                            else
                            {
                                errorMsg.InnerText = "Not assigned any pages for this Employee";
                                errorMsg.Visible = true;
                                txtUsername.Value = "";
                            }

                        }
                        else if(UserRole == "Admin" || UserRole == "Operator")
                        {
                            errorMsg.InnerText = "Only Normal user and Master Page user can login";
                            errorMsg.Visible = true;
                            txtUsername.Value = "";
                        }
                        else
                        {

                            errorMsg.InnerText = "Invalid user id or password";
                            errorMsg.Visible = true;
                            txtUsername.Value = "";
                        }
                    }
                    else
                    {
                        if (UserRole == "Admin")
                        {
                            List<RoleAccessRight> listPage = new List<RoleAccessRight>();
                            RoleAccessRight roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "MasterData";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "ParameterMaster";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "OperatorDetailsMaster";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "ParametersRelationshipMaster";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "AssignValueForDependency";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "DeleteSDoc";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "InputModuleMasterView";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "AssignPagesforUser";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "ParameterDependenacyList";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "UnlockSdocID";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "ApplicationToolKit";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "DataInputModule";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "DerivedParameters";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "OutputModules";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "SignalProcess";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);

                            FormsAuthentication.SetAuthCookie(this.txtUsername.Value.Trim(), true);
                            FormsAuthenticationTicket ticket1 = new FormsAuthenticationTicket(1, this.txtUsername.Value.Trim(),
                                DateTime.Now, DateTime.Now.AddMinutes(480), true, txtUsername.Value);
                            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket1));
                            Response.Cookies.Add(cookie1);
                            if (listPage.Count > 0)
                            {
                                Session["EmpName"] = txtUsername.Value;
                                Session["PageList"] = listPage;
                                Response.Redirect("~/" + listPage[0].Page.ToString() + ".aspx", false);
                            }
                            else
                            {
                                errorMsg.InnerText = "Not assigned any pages for this Employee";
                                errorMsg.Visible = true;
                                txtUsername.Value = "";
                            }
                            //Session["EmpName"] = txtUsername.Value;
                            //FormsAuthentication.SetAuthCookie(this.txtUsername.Value.Trim(), true);
                            //FormsAuthenticationTicket ticket1 = new FormsAuthenticationTicket(1, this.txtUsername.Value.Trim(),
                            //    DateTime.Now, DateTime.Now.AddMinutes(480), true, txtUsername.Value);
                            //HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket1));
                            //Response.Cookies.Add(cookie1);
                            //Response.Redirect("~/MasterData.aspx", false);
                        }
                        else if (UserRole == "Operator")
                        {
                            List<RoleAccessRight> listPage = new List<RoleAccessRight>();
                            RoleAccessRight roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "ApplicationToolKit";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "DataInputModule";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "DerivedParameters";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "OutputModules";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "SignalProcess";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);

                            FormsAuthentication.SetAuthCookie(this.txtUsername.Value.Trim(), true);
                            FormsAuthenticationTicket ticket1 = new FormsAuthenticationTicket(1, this.txtUsername.Value.Trim(),
                                DateTime.Now, DateTime.Now.AddMinutes(480), true, txtUsername.Value);
                            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket1));
                            Response.Cookies.Add(cookie1);
                            if (listPage.Count > 0)
                            {
                                Session["EmpName"] = txtUsername.Value;
                                Session["PageList"] = listPage;
                                Response.Redirect("~/" + listPage[0].Page.ToString() + ".aspx", false);
                            }
                            else
                            {
                                errorMsg.InnerText = "Not assigned any pages for this Employee";
                                errorMsg.Visible = true;
                                txtUsername.Value = "";
                            }
                        }
                        else if (UserRole == "Master User")
                        {
                            List<RoleAccessRight> listPage = new List<RoleAccessRight>();
                            RoleAccessRight roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "MasterData";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "ParameterMaster";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "OperatorDetailsMaster";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "ParametersRelationshipMaster";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "AssignValueForDependency";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "DeleteSDoc";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "InputModuleMasterView";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "AssignPagesforUser";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "ParameterDependenacyList";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);
                            roleAccessRight = new RoleAccessRight();
                            roleAccessRight.Page = "UnlockSdocID";
                            roleAccessRight.visibilty = true;
                            listPage.Add(roleAccessRight);

                            FormsAuthentication.SetAuthCookie(this.txtUsername.Value.Trim(), true);
                            FormsAuthenticationTicket ticket1 = new FormsAuthenticationTicket(1, this.txtUsername.Value.Trim(),
                                DateTime.Now, DateTime.Now.AddMinutes(480), true, txtUsername.Value);
                            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket1));
                            Response.Cookies.Add(cookie1);
                            if (listPage.Count > 0)
                            {
                                Session["EmpName"] = txtUsername.Value;
                                Session["PageList"] = listPage;
                                Response.Redirect("~/" + listPage[0].Page.ToString() + ".aspx", false);
                            }
                            else
                            {
                                errorMsg.InnerText = "Not assigned any pages for this Employee";
                                errorMsg.Visible = true;
                                txtUsername.Value = "";
                            }

                        }
                        else if (UserRole == "Normal User")
                        {
                            List<RoleAccessRight> listPage = new List<RoleAccessRight>();
                            listPage = DBAccess.getPagesforRole(txtUsername.Value);
                            FormsAuthentication.SetAuthCookie(this.txtUsername.Value.Trim(), true);
                            FormsAuthenticationTicket ticket1 = new FormsAuthenticationTicket(1, this.txtUsername.Value.Trim(),
                                DateTime.Now, DateTime.Now.AddMinutes(480), true, txtUsername.Value);
                            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket1));
                            Response.Cookies.Add(cookie1);
                            if (listPage.Count > 0)
                            {
                                Session["EmpName"] = txtUsername.Value;
                                Session["PageList"] = listPage;
                                Response.Redirect("~/" + listPage[0].Page.ToString() + ".aspx", false);
                            }

                            else
                            {
                                errorMsg.InnerText = "Not assigned any pages for this Employee";
                                errorMsg.Visible = true;
                                txtUsername.Value = "";
                            }

                        }
                        else
                        {

                            errorMsg.InnerText = "Invalid user id or password";
                            errorMsg.Visible = true;
                            txtUsername.Value = "";
                        }

                    }


                }
                else
                {
                    txtUsername.Value = "";
                    errorMsg.InnerText = "Invalid user id or password";
                    errorMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);
                Logger.WriteErrorLog(ex.ToString());
            }

            //try
            //{

            //    if (!string.IsNullOrEmpty(txtUsername.Value) && !string.IsNullOrEmpty(txtPassword.Value))
            //    {
            //        //if(DBAccess.WindowAuthentication)
            //        string UserRole = DBAccess.CheckSupLoginData(txtUsername.Value, txtPassword.Value);
            //        if (!string.IsNullOrWhiteSpace(UserRole))
            //        {
            //            if (UserRole == "Admin")
            //            {

            //                Session["AdminName"] = txtUsername.Value;
            //                FormsAuthentication.SetAuthCookie(this.txtUsername.Value.Trim(), true);
            //                FormsAuthenticationTicket ticket1 = new FormsAuthenticationTicket(1, this.txtUsername.Value.Trim(),
            //                    DateTime.Now, DateTime.Now.AddMinutes(480), true, txtUsername.Value);
            //                HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket1));
            //                Response.Cookies.Add(cookie1);
            //                Response.Redirect("~/MasterData.aspx", false);
            //            }

            //            else if (UserRole == "Operator")
            //            {

            //                Session["EmpName"] = txtUsername.Value;

            //                FormsAuthentication.SetAuthCookie(this.txtUsername.Value.Trim(), true);
            //                FormsAuthenticationTicket ticket1 = new FormsAuthenticationTicket(1, this.txtUsername.Value.Trim(),
            //                    DateTime.Now, DateTime.Now.AddMinutes(480), true, txtUsername.Value);
            //                HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket1));
            //                Response.Cookies.Add(cookie1);
            //                Response.Redirect("~/ApplicationToolKit.aspx", false);
            //            }

            //            else
            //            {

            //                errorMsg.InnerText = "Invalid user id or password";
            //                errorMsg.Visible = true;
            //                txtUsername.Value = "";
            //            }
            //        }
            //        else
            //        {
            //            txtUsername.Value = "";
            //            errorMsg.InnerText = "Invalid user id or password";
            //            errorMsg.Visible = true;
            //        }
            //    }
            //    else
            //    {
            //        txtUsername.Value = "";
            //        errorMsg.InnerText = "Invalid user id or password";
            //        errorMsg.Visible = true;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ErrorSignal.FromCurrentContext().Raise(ex);
            //    Logger.WriteErrorLog(ex.ToString());
            //}

        }
    }
}