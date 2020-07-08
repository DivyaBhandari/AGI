using Elmah;
//using AGISoftware.DataBaseAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Text;
using AGISoftware.Model;
using System.Web.UI.WebControls;

namespace AGISoftware.DataBaseAccess
{
    public class DBAccess
    {
        #region --------get Formula list---
        internal static DataTable getFormulaList(string Sdocid, out DataTable derivedLowerddt, out DataTable InputModuledt)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            DataTable dt = new DataTable();
            derivedLowerddt = new DataTable();
            InputModuledt = new DataTable();
            SqlCommand cmd = new SqlCommand("[dbo].[S_Get_CalculationFormulaLog]", con);
            cmd.Parameters.AddWithValue("@SDocId", Sdocid);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sdr = cmd.ExecuteReader();
            InputModuledt.Load(sdr);
            dt.Load(sdr);
            derivedLowerddt.Load(sdr);

            con.Close();
            sdr.Close();
            return dt;
        }
        #endregion

        #region --------Login-------------

        internal static string CheckSupLoginData(string empname, string password)
        {

            string adminData = string.Empty;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "select Role from Employee_Information where Employeeid=@employeename and upassword=@Upassword";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@employeename", empname);
                cmd.Parameters.AddWithValue("@Upassword", password);
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    sdr.Read();
                    {
                        adminData = sdr["Role"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);//(ex.ToString());
                throw;
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }
            return adminData;
        }

        internal static List<RoleAccessRight> getPagesforRole(string role)
        {

            List<RoleAccessRight> listPage = new List<RoleAccessRight>();
            RoleAccessRight roleAccessRight = null;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "select PageName from RoleAccessRights where Role=@role and Visibility=1";
                cmd = new SqlCommand(sqlQuery, conn);
             
                cmd.Parameters.AddWithValue("@role", role);
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while(sdr.Read())
                    {
                        roleAccessRight = new RoleAccessRight();
                        roleAccessRight.Page = sdr["PageName"].ToString();
                        roleAccessRight.visibilty = true;
                       listPage.Add(roleAccessRight);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);//(ex.ToString());
                throw;
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }
            return listPage;
        }

        internal static string CheckRole(string empid)
        {

            string adminData = string.Empty;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "select Role from Employee_Information where Employeeid=@employeename";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@employeename", empid);

                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    sdr.Read();
                    {
                        adminData = sdr["Role"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);//(ex.ToString());
                throw;
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }
            return adminData;
        }
        #endregion

        #region --------Employeee Info----------


        internal static int deleteEmployeeDetails(string empid)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<EmployeeDetails> listemp = new List<EmployeeDetails>();
            EmployeeDetails emp = null;
            int success = 0;
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_EmployeeDetailsSaveUpdate]", con);
                cmd.Parameters.AddWithValue("@empid", empid);
                cmd.Parameters.AddWithValue("@Param", "Delete");
                cmd.CommandType = CommandType.StoredProcedure;
                success = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    success = -2;
                }
                else
                {
                    Logger.WriteErrorLog("Error while delete Emp data - " + ex.Message);
                    success = 0;
                }
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return success;
        }
        internal static int saveEmployeeDetails(EmployeeDetails empdata, string parameter)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<EmployeeDetails> listemp = new List<EmployeeDetails>();
            EmployeeDetails emp = null;
            int success = 0;
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_EmployeeDetailsSaveUpdate]", con);
                cmd.Parameters.AddWithValue("@empid", empdata.id);
                cmd.Parameters.AddWithValue("@Name", empdata.Name);
                cmd.Parameters.AddWithValue("@email", empdata.email);
                cmd.Parameters.AddWithValue("@mblno", empdata.mblno);
                cmd.Parameters.AddWithValue("@password", empdata.password);
                cmd.Parameters.AddWithValue("@role", empdata.role);
                cmd.Parameters.AddWithValue("@Param", parameter);
                //cmd.Parameters.AddWithValue("@Param", empdata.AdminName );
                cmd.CommandType = CommandType.StoredProcedure;
                success = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    success = -2;
                }
                else
                {
                    Logger.WriteErrorLog("Error while save Emp data - " + ex.Message);
                    success = 0;
                }
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return success;
        }
        internal static List<EmployeeDetails> getEmployeeDetails(string empid)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<EmployeeDetails> listemp = new List<EmployeeDetails>();
            EmployeeDetails emp = null;
            int success = 0;
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_EmployeeDetailsSaveUpdate]", con);
                cmd.Parameters.AddWithValue("@empid", empid);
                cmd.Parameters.AddWithValue("@Param", "View");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        emp = new EmployeeDetails();
                        emp.id = sdr["Employeeid"].ToString();
                        emp.Name = sdr["Name"].ToString();
                        emp.email = sdr["Email"].ToString();
                        emp.mblno = sdr["MobileNo"].ToString();
                        emp.password = sdr["upassword"].ToString();
                        emp.role = sdr["Role"].ToString();
                        listemp.Add(emp);
                    }

                }

            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    success = -2;
                }
                else
                {
                    Logger.WriteErrorLog("Error while view Emp data - " + ex.Message);
                    success = 0;
                }
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return listemp;
        }

        #endregion

        #region --SDoc Autogenerate------


        internal static string getSdocname(string columnname)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            string SDoc = "";
            List<string> list = new List<string>();
            try
            {


                cmd = new SqlCommand("select max(" + columnname + ") as Sdoc from SystemDocTransaction", con);
                //     cmd.Parameters.AddWithValue("@columnname", columnname);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (sdr["Sdoc"].ToString() == "")
                        {
                            SDoc = "-1";
                        }
                        else
                        {
                            SDoc = sdr["Sdoc"].ToString();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog(" Error while get SDoc name (Auto generate)"+ ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return SDoc;
        }
        internal static string getSystemDocumentName(long docid, long plunge, long subcategory, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            string SDoc = "";
            List<string> list = new List<string>();
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_SystemDocument]", con);
                cmd.Parameters.AddWithValue("@docid", docid);
                cmd.Parameters.AddWithValue("@plunge", plunge);
                cmd.Parameters.AddWithValue("@subcategory", subcategory);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (sdr["output"].ToString() == "")
                        {
                            SDoc = "-1";
                        }
                        else
                        {
                            SDoc = sdr["output"].ToString();
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
            return SDoc;
        }

        //internal static Int64 getSdocname()
        //{
        //    SqlConnection con = ConnectionManager.GetConnection();
        //    SqlDataReader sdr = null;
        //    SqlCommand cmd = null;
        //    Int64 SDoc = 0;
        //    List<string> list = new List<string>();
        //    try
        //    {


        //        cmd = new SqlCommand("select max(SDocName) from SystemDocTransaction", con);
        //        // cmd.Parameters.AddWithValue("@columnname", columnname);
        //        cmd.CommandType = CommandType.Text;
        //        sdr = cmd.ExecuteReader();
        //        if (sdr.HasRows)
        //        {
        //            while (sdr.Read())
        //            {
        //                SDoc = Convert.ToInt64(sdr["SDocName"].ToString());
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteDebugLog(ex.Message);
        //    }
        //    finally
        //    {
        //        if (con != null) con.Close();
        //        if (sdr != null) sdr.Close();
        //    }
        //    return SDoc;
        //}
        #endregion




        #region-----------Data Input Module--------------

        internal static List<ParameterDependency> getDependencyParameterList()
        {

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            List<ParameterDependency> listParameterDependencies = new List<ParameterDependency>();
            ParameterDependency parameterDependency = null;
            try
            {
                cmd = new SqlCommand("select * from ParameterDependencyDetails", conn);
                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        parameterDependency = new ParameterDependency();
                        parameterDependency.Parameter1 = rdr["Parameter1"].ToString();
                        parameterDependency.ParameterId1 = rdr["ParameterID1"].ToString();
                        parameterDependency.Parameter2 = rdr["Parameter2"].ToString();
                        parameterDependency.ParameterId2 = rdr["ParameterID2"].ToString();
                        parameterDependency.Parameter2Value = rdr["ParameterValue2"].ToString();
                        parameterDependency.LSL = rdr["LSL"].ToString();
                        parameterDependency.USL = rdr["USL"].ToString();
                        listParameterDependencies.Add(parameterDependency);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("While getting Paramter dependency list" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }
            return listParameterDependencies;
        }
        internal static List<ParameterDependency> IMgetDependencyParametervalue(string param2id, string param2value, out string param1,out string param1Id)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<ParameterDependency> listParameter = new List<ParameterDependency>();
            ParameterDependency parameter = null;
            param1 = ""; param1Id="";

            try
            {
                cmd = new SqlCommand("select ParameterID1,Parameter1,ParameterValue1 from InputParameterLinkDetails where ParameterID2=@paramid2 and ParameterValue2=@paramvalue2", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@paramid2", param2id);
                cmd.Parameters.AddWithValue("@paramvalue2", param2value);
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        parameter = new ParameterDependency();
                        parameter.ParameterId1 = sdr["ParameterID1"].ToString();
                        param1Id= sdr["ParameterID1"].ToString();
                        parameter.Parameter1 = sdr["Parameter1"].ToString();
                        param1= sdr["Parameter1"].ToString();
                        parameter.Parameter1Value = sdr["ParameterValue1"].ToString();
                        listParameter.Add(parameter);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("Input Module- get Dependency Parameter value " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return listParameter;
        }

        internal static List<ParameterDependency> IMgetDependencyParameter(string param2id, string param2value)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<ParameterDependency> listParameter = new List<ParameterDependency>();
            ParameterDependency parameter = null;
           

            try
            {
                cmd = new SqlCommand("select ParameterID1,Parameter1 from InputParameterLinkMaster where ParameterID2=@paramid2", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@paramid2", param2id);
                cmd.Parameters.AddWithValue("@paramvalue2", param2value);
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        parameter = new ParameterDependency();
                        parameter.ParameterId1 = sdr["ParameterID1"].ToString();
                        parameter.Parameter1 = sdr["Parameter1"].ToString();
                        parameter.Parameter1Value = null;
                        listParameter.Add(parameter);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("Input Module-get Dependency Parameter " + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return listParameter;
        }


        internal static string getFormulaList(string parametername)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<DataInputModuleParameter> listParameter = new List<DataInputModuleParameter>();
            DataInputModuleParameter parameter = null;
            string formula = "";
            try
            {
                cmd = new SqlCommand("select * from ParameterFormulaDetails where InputFlag=1 and Parameter=@parametername", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@parametername", parametername);
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        formula = sdr["Formula"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("Input Module- get FormulaList "+ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return formula;
        }


        internal static int RemoveImage(string SDoc, string imgName, string imgPath)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<string> list = new List<string>();
            int success = 0;
            try
            {
                cmd = new SqlCommand("delete from SDocImageDetails where SDocID=@SDoc and ImageName=@name and ImageFileName=@path", con);
                cmd.Parameters.AddWithValue("@SDoc", SDoc);
                cmd.Parameters.AddWithValue("@name", imgName);
                cmd.Parameters.AddWithValue("@path", imgPath);
                cmd.CommandType = CommandType.Text;
                success = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    success = -2;
                }
                else
                {
                    Logger.WriteErrorLog("Error while remove image from workpiece - " + ex.Message);
                    success = 0;
                }
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return success;
        }


        internal static List<DataInputModuleParameter> getDataInputModuleOpearationalParamData(string id, string InputModule, string SubInputModule)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<DataInputModuleParameter> listParameter = new List<DataInputModuleParameter>();
            DataInputModuleParameter parameter = null;
            try
            {

                //cmd = new SqlCommand("select * from InputModuleParameterDetails as M left join SystemDocTransaction as T on(M.Id=T.ParameterID) where InputModule=@inputModule and SubInputModule=@subinputModule", con);
                cmd = new SqlCommand("[dbo].[S_Get_SystemDocDataSaveUpdate]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SDoc", id);
                cmd.Parameters.AddWithValue("@InputModule", InputModule);
                cmd.Parameters.AddWithValue("@SubInputModule", SubInputModule);
                cmd.Parameters.AddWithValue("@Param", "View");
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(sdr);
                    string tempBachid = "";
                    List<string> listOfBachid = new List<string>();
                    foreach (DataRow item in dt.Rows)
                    {
                        string s = item["BatchID"].ToString();
                        if (item["BatchID"].ToString() != "")
                        {
                            if (!listOfBachid.Contains(item["BatchID"].ToString()))
                            {
                                listOfBachid.Add(item["BatchID"].ToString());
                            }
                        }

                        //  tempBachid = item["BatchID"].ToString();
                    }

                    List<string> rlistOfBachid = new List<string>();
                    List<string> slistOfBachid = new List<string>();
                    List<string> flistOfBachid = new List<string>();
                    for (int r=0;r<listOfBachid.Count;r++)
                    {
                        if(listOfBachid[r].ToString().StartsWith("R"))
                        {
                            rlistOfBachid.Add(listOfBachid[r].ToString());
                        }
                        if (listOfBachid[r].ToString().StartsWith("S"))
                        {
                            slistOfBachid.Add(listOfBachid[r].ToString());
                        }
                        if (listOfBachid[r].ToString().StartsWith("F"))
                        {
                            flistOfBachid.Add(listOfBachid[r].ToString());
                        }
                    }
                    rlistOfBachid.Sort();
                    slistOfBachid.Sort();
                    flistOfBachid.Sort();
                    listOfBachid = rlistOfBachid.Concat(slistOfBachid).Concat(flistOfBachid).ToList();

                    foreach (string param in listOfBachid)
                    {
                        parameter = new DataInputModuleParameter();

                        if (param.StartsWith("R"))
                        {
                            parameter.Prameter = "Roughing" + param.Replace("R", " ");
                        }
                        else if (param.StartsWith("S"))
                        {
                            parameter.Prameter = "Semi Finishing" + param.Replace("S", " ");
                        }
                        else if (param.StartsWith("F"))
                        {
                            parameter.Prameter = "Finishing" + param.Replace("F", " ");
                        }
                        foreach (DataRow item in dt.Rows)
                        {
                            if (item["BatchID"].ToString() == param)
                            {
                                if (item["BatchName"].ToString().Contains("Feed"))
                                {
                                    parameter.FeedRate = item["Value"].ToString();
                                    parameter.ParameterFeedRate = item["Parameter"].ToString();
                                    parameter.ParameterIDFeedRate = item["ParameterID"].ToString();
                                    parameter.EntryTypeFeedRate = item["Entry_Type"].ToString();
                                    parameter.DataTypeFeedRate = item["DataType"].ToString();
                                    parameter.RecommendationFeedRate = item["Recommendation"].ToString();
                                    parameter.LslUslFeedRate = item["LSL"].ToString() + ";" + item["USL"].ToString();
                                    parameter.DependancyFeedRate = item["Dependency"].ToString();
                                    parameter.IndependentParameterFeedRate = item["IndependentParameter"].ToString();
                                    parameter.MandatoryFeedRate = item["MandatoryFlag"].ToString();
                                    parameter.CustomenameFeedRate = item["Customname"].ToString();
                                    if (item["LimitImage"].ToString() == "")
                                    {
                                        parameter.ImageFeedRate = "";
                                    }
                                    else
                                    {
                                        byte[] bytes = (byte[])item["LimitImage"];
                                        string strBase64 = Convert.ToBase64String(bytes);
                                        parameter.ImageFeedRate = "data:Image/png;base64," + strBase64;
                                    }
                                    // parameter.ImageFeedRate = item["LimitImage"].ToString();
                                }
                                else if (item["BatchName"].ToString().Contains("DOC"))
                                {
                                    parameter.DOC = item["Value"].ToString();
                                    parameter.ParameterIDDOC = item["ParameterID"].ToString();
                                    parameter.ParameterDOC = item["Parameter"].ToString();
                                    parameter.EntryTypeDOC = item["Entry_Type"].ToString();
                                    parameter.DataTypeDOC = item["DataType"].ToString();
                                    parameter.RecommendationDOC = item["Recommendation"].ToString();
                                    parameter.LslUslDOC = item["LSL"].ToString() + ";" + item["USL"].ToString();
                                    //parameter.ImageDOC = item["LimitImage"].ToString();
                                    parameter.DependancyDoc = item["Dependency"].ToString();
                                    parameter.IndependentParameterDoc = item["IndependentParameter"].ToString();
                                    parameter.MandatoryDoc = item["MandatoryFlag"].ToString();
                                    parameter.CustomenameDoc = item["Customname"].ToString();
                                    if (item["LimitImage"].ToString() == "")
                                    {
                                        parameter.ImageDOC = "";
                                    }
                                    else
                                    {
                                        byte[] bytes = (byte[])item["LimitImage"];
                                        string strBase64 = Convert.ToBase64String(bytes);
                                        parameter.ImageDOC = "data:Image/png;base64," + strBase64;
                                    }
                                }
                                else if (item["BatchName"].ToString().Contains("Face"))
                                {
                                    parameter.Face = item["Value"].ToString();
                                    parameter.ParameterIDFace = item["ParameterID"].ToString();
                                    parameter.ParameterFace = item["Parameter"].ToString();
                                    parameter.EntryTypeFace = item["Entry_Type"].ToString();
                                    parameter.DataTypeFace = item["DataType"].ToString();
                                    parameter.RecommendationFace = item["Recommendation"].ToString();
                                    parameter.LslUslFace = item["LSL"].ToString() + ";" + item["USL"].ToString();
                                    parameter.DependancyFace = item["Dependency"].ToString();
                                    parameter.IndependentParameterFace = item["IndependentParameter"].ToString();
                                    parameter.MandatoryFace = item["MandatoryFlag"].ToString();
                                    parameter.CustomenameFace = item["Customname"].ToString();
                                    //parameter.ImageDOC = item["LimitImage"].ToString();
                                    if (item["LimitImage"].ToString() == "")
                                    {
                                        parameter.ImageFace = "";
                                    }
                                    else
                                    {
                                        byte[] bytes = (byte[])item["LimitImage"];
                                        string strBase64 = Convert.ToBase64String(bytes);
                                        parameter.ImageFace = "data:Image/png;base64," + strBase64;
                                    }
                                }
                                else if (item["BatchName"].ToString() == "Work Speed (Nw) (rpm)")
                                {
                                    parameter.WorkRPM = item["Value"].ToString();
                                    parameter.ParameterIDWorkRPM = item["ParameterID"].ToString();
                                    parameter.ParameterWorkRPM = item["Parameter"].ToString();
                                    parameter.EntryTypeWorkRPM = item["Entry_Type"].ToString();
                                    parameter.DataTypeWorkRPM = item["DataType"].ToString();
                                    parameter.RecommendationWorkRPM = item["Recommendation"].ToString();
                                    parameter.LslUslWorkRPM = item["LSL"].ToString() + ";" + item["USL"].ToString();
                                    parameter.DependancyWorkRPM = item["Dependency"].ToString();
                                    parameter.IndependentParameterWorkRPM = item["IndependentParameter"].ToString();
                                    parameter.MandatoryWorkRPM = item["MandatoryFlag"].ToString();
                                    parameter.CustomenameWorkRPM = item["Customname"].ToString();
                                    //parameter.ImageWorkRPM = item["LimitImage"].ToString();
                                    if (item["LimitImage"].ToString() == "")
                                    {
                                        parameter.ImageWorkRPM = "";
                                    }
                                    else
                                    {
                                        byte[] bytes = (byte[])item["LimitImage"];
                                        string strBase64 = Convert.ToBase64String(bytes);
                                        parameter.ImageWorkRPM = "data:Image/png;base64," + strBase64;
                                    }
                                }
                                else if (item["BatchName"].ToString() == "Wheel Speed (Vs) (m/s)")
                                {
                                    parameter.Wheelms = item["Value"].ToString();
                                    parameter.ParameterIDWheelms = item["ParameterID"].ToString();
                                    parameter.ParameterWheelms = item["Parameter"].ToString();
                                    parameter.EntryTypeWheelms = item["Entry_Type"].ToString();
                                    parameter.DataTypeWheelms = item["DataType"].ToString();
                                    parameter.RecommendationWheelms = item["Recommendation"].ToString();
                                    parameter.LslUslWheelms = item["LSL"].ToString() + ";" + item["USL"].ToString();
                                    parameter.DependancyWheelms = item["Dependency"].ToString();
                                    parameter.IndependentParameterWheelms = item["IndependentParameter"].ToString();
                                    parameter.MandatoryWheelms = item["MandatoryFlag"].ToString();
                                    parameter.CustomenameWheelms = item["Customname"].ToString();
                                    //parameter.ImageWheelms = item["LimitImage"].ToString();
                                    if (item["LimitImage"].ToString() == "")
                                    {
                                        parameter.ImageWheelms = "";
                                    }
                                    else
                                    {
                                        byte[] bytes = (byte[])item["LimitImage"];
                                        string strBase64 = Convert.ToBase64String(bytes);
                                        parameter.ImageWheelms = "data:Image/png;base64," + strBase64;
                                    }
                                }
                                else if (item["BatchName"].ToString() == "Work Speed (Vw) (m/s)")
                                {
                                    parameter.Workms = item["Value"].ToString();
                                    parameter.ParameterIDWorkms = item["ParameterID"].ToString();
                                    parameter.ParameterWorkms = item["Parameter"].ToString();
                                    parameter.EntryTypeWorkms = item["Entry_Type"].ToString();
                                    parameter.DataTypeWorkms = item["DataType"].ToString();
                                    parameter.RecommendationWorkms = item["Recommendation"].ToString();
                                    parameter.LslUslWorkms = item["LSL"].ToString() + ";" + item["USL"].ToString();
                                    parameter.DependancyWorkms = item["Dependency"].ToString();
                                    parameter.IndependentParameterWorkms = item["IndependentParameter"].ToString();
                                    parameter.MandatoryWorkms = item["MandatoryFlag"].ToString();
                                    parameter.CustomenameWorkms = item["Customname"].ToString();
                                    //parameter.ImageWorkms = item["LimitImage"].ToString();
                                    if (item["LimitImage"].ToString() == "")
                                    {
                                        parameter.ImageWorkms = "";
                                    }
                                    else
                                    {
                                        byte[] bytes = (byte[])item["LimitImage"];
                                        string strBase64 = Convert.ToBase64String(bytes);
                                        parameter.ImageWorkms = "data:Image/png;base64," + strBase64;
                                    }
                                    parameter.GrindingProcess= item["GrindingProcess"].ToString(); ;
                                }
                                else if (item["BatchName"].ToString() == "Wheel Speed (Ns) (rpm)")
                                {
                                    parameter.WheelRPM = item["Value"].ToString();
                                    parameter.ParameterIDWheelRPM = item["ParameterID"].ToString();
                                    parameter.ParameterWheelRPM = item["Parameter"].ToString();
                                    parameter.EntryTypeWheelRPM = item["Entry_Type"].ToString();
                                    parameter.DataTypeWheelRPM = item["DataType"].ToString();
                                    parameter.RecommendationWheelRPM = item["Recommendation"].ToString();
                                    parameter.LslUslWheelRPM = item["LSL"].ToString() + ";" + item["USL"].ToString();
                                    parameter.DependancyWheelRPM = item["Dependency"].ToString();
                                    parameter.IndependentParameterWheelRPM = item["IndependentParameter"].ToString();
                                    parameter.MandatoryWheelRPM = item["MandatoryFlag"].ToString();
                                    parameter.CustomenameWheelRPM = item["Customname"].ToString();
                                    //parameter.ImageWheelRPM = item["LimitImage"].ToString();
                                    if (item["LimitImage"].ToString() == "")
                                    {
                                        parameter.ImageWheelRPM = "";
                                    }
                                    else
                                    {
                                        byte[] bytes = (byte[])item["LimitImage"];
                                        string strBase64 = Convert.ToBase64String(bytes);
                                        parameter.ImageWheelRPM = "data:Image/png;base64," + strBase64;
                                    }
                                }
                            }
                        }
                        listParameter.Add(parameter);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("InputModule - getDataInputModuleOpearationalParamData"+ ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return listParameter;
        }




        internal static List<DataInputModuleParameter> getDataInputModuleQualityParamData(string id, string InputModule, string SubInputModule)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<DataInputModuleParameter> listParameter = new List<DataInputModuleParameter>();
            DataInputModuleParameter parameter = null;
            try
            {

                //cmd = new SqlCommand("select * from InputModuleParameterDetails as M left join SystemDocTransaction as T on(M.Id=T.ParameterID) where InputModule=@inputModule and SubInputModule=@subinputModule", con);
                cmd = new SqlCommand("[dbo].[S_Get_SystemDocDataSaveUpdate]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SDoc", id);
                cmd.Parameters.AddWithValue("@InputModule", InputModule);
                cmd.Parameters.AddWithValue("@SubInputModule", SubInputModule);
                cmd.Parameters.AddWithValue("@Param", "View");
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(sdr);
                    string tempParameter = "";
                    List<string> listOfParam = new List<string>();
                    foreach (DataRow item in dt.Rows)
                    {
                        if (item["ParameterName"].ToString() != tempParameter)
                        {
                            listOfParam.Add(item["ParameterName"].ToString());
                        }
                        tempParameter = item["ParameterName"].ToString();
                    }
                    foreach (string param in listOfParam)
                    {

                        parameter = new DataInputModuleParameter();
                        //parameter.Prameter = param;
                        string customName = "";
                        foreach (DataRow item in dt.Rows)
                        {
                            if (item["ParameterName"].ToString() == param)
                            {
                                customName = item["Customname"].ToString().Substring(0, item["Customname"].ToString().LastIndexOf(';'));

                                if (item["SubParameterName"].ToString().Trim() == "Lower Target")
                                {
                                    parameter.TargetLower = item["Value"].ToString();
                                    parameter.ParamIdTargetLower = item["ParameterID"].ToString();
                                    parameter.EntryTypeTL = item["Entry_Type"].ToString();
                                    parameter.DataTypeTL = item["DataType"].ToString();
                                    parameter.RecommendationTL = item["Recommendation"].ToString();
                                    parameter.LslUslTargetLower = item["LSL"].ToString() + ";" + item["USL"].ToString();
                                    //parameter.ImageRecommandationTL= item["LimitImage"].ToString();
                                    parameter.DependencyTargetLower = item["Dependency"].ToString();
                                    parameter.IndependentParameterTargetLower = item["IndependentParameter"].ToString();
                                    parameter.ParameterLowerTarget = item["Parameter"].ToString();
                                    parameter.MandatoryTargetLower = item["MandatoryFlag"].ToString();
                                    parameter.CustomenameLowerTarget = item["Customname"].ToString();
                                    if (item["LimitImage"].ToString() == "")
                                    {
                                        parameter.ImageRecommandationTL = "";
                                    }
                                    else
                                    {
                                        byte[] bytes = (byte[])item["LimitImage"];
                                        string strBase64 = Convert.ToBase64String(bytes);
                                        parameter.ImageRecommandationTL = "data:Image/png;base64," + strBase64;
                                    }
                                }
                                else if (item["SubParameterName"].ToString().Trim() == "Upper Target")
                                {
                                    parameter.TargetUpper = item["Value"].ToString();
                                    parameter.ParamIdTargetUpper = item["ParameterID"].ToString();
                                    parameter.EntryTypeTU = item["Entry_Type"].ToString();
                                    parameter.DataTypeTU = item["DataType"].ToString();
                                    parameter.RecommendationTU = item["Recommendation"].ToString();
                                    parameter.LslUslTargetUpper = item["LSL"].ToString() + ";" + item["USL"].ToString();
                                    // parameter.ImageRecommandationTU = item["LimitImage"].ToString();
                                    parameter.DependencyTargetUpper = item["Dependency"].ToString();
                                    parameter.IndependentParameterTargetUpper = item["IndependentParameter"].ToString();
                                    parameter.ParameterTargetUpper = item["Parameter"].ToString();
                                    parameter.MandatoryTargetUpper = item["MandatoryFlag"].ToString();
                                    parameter.CustomenameTargetUpper = item["Customname"].ToString();
                                    if (item["LimitImage"].ToString() == "")
                                    {
                                        parameter.ImageRecommandationTU = "";
                                    }
                                    else
                                    {
                                        byte[] bytes = (byte[])item["LimitImage"];
                                        string strBase64 = Convert.ToBase64String(bytes);
                                        parameter.ImageRecommandationTU = "data:Image/png;base64," + strBase64;
                                    }
                                }
                                else if (item["SubParameterName"].ToString().Trim() == "Lower Actual")
                                {
                                    parameter.ActualLower = item["Value"].ToString();
                                    parameter.ParamIdActualLower = item["ParameterID"].ToString();
                                    parameter.EntryTypeAL = item["Entry_Type"].ToString();
                                    parameter.DataTypeAL = item["DataType"].ToString();
                                    parameter.RecommendationlAL = item["Recommendation"].ToString();
                                    parameter.LslUslActualLower = item["LSL"].ToString() + ";" + item["USL"].ToString();
                                    // parameter.ImageRecommandationAL = item["LimitImage"].ToString();
                                    parameter.DependencyActualLower = item["Dependency"].ToString();
                                    parameter.IndependentParameterActualLower = item["IndependentParameter"].ToString();
                                    parameter.ParameterActualLower = item["Parameter"].ToString();
                                    parameter.MandatoryActualLower = item["MandatoryFlag"].ToString();
                                    parameter.CustomenameActualLower = item["Customname"].ToString();
                                    if (item["LimitImage"].ToString() == "")
                                    {
                                        parameter.ImageRecommandationAL = "";
                                    }
                                    else
                                    {
                                        byte[] bytes = (byte[])item["LimitImage"];
                                        string strBase64 = Convert.ToBase64String(bytes);
                                        parameter.ImageRecommandationAL = "data:Image/png;base64," + strBase64;
                                    }
                                }
                                else if (item["SubParameterName"].ToString().Trim() == "Upper Actual")
                                {
                                    parameter.ActualUpper = item["Value"].ToString();
                                    parameter.ParamIdActualUpper = item["ParameterID"].ToString();
                                    parameter.EntryTypeAU = item["Entry_Type"].ToString();
                                    parameter.DataTypeAU = item["DataType"].ToString();
                                    parameter.RecommendationAU = item["Recommendation"].ToString();
                                    parameter.LslUslActualUpper = item["LSL"].ToString() + ";" + item["USL"].ToString();
                                    //parameter.ImageRecommandationAU = item["LimitImage"].ToString();
                                    parameter.DependencyActualUpper = item["Dependency"].ToString();
                                    parameter.IndependentParameterActualUpper = item["IndependentParameter"].ToString();
                                    parameter.ParameterActualUpper = item["Parameter"].ToString();
                                    parameter.MandatoryActualUpper = item["MandatoryFlag"].ToString();
                                    parameter.CustomenameActualUpper = item["Customname"].ToString();
                                    if (item["LimitImage"].ToString() == "")
                                    {
                                        parameter.ImageRecommandationAU = "";
                                    }
                                    else
                                    {
                                        byte[] bytes = (byte[])item["LimitImage"];
                                        string strBase64 = Convert.ToBase64String(bytes);
                                        parameter.ImageRecommandationAU = "data:Image/png;base64," + strBase64;
                                    }
                                }
                            }
                        }

                        parameter.Prameter = customName;
                        listParameter.Add(parameter);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("Input Module -getDataInputModuleQualityParamData" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return listParameter;
        }
        internal static List<DataInputModuleParameter> getDataInputModuleData(string id, string InputModule, string SubInputModule)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<DataInputModuleParameter> listParameter = new List<DataInputModuleParameter>();
            DataInputModuleParameter parameter = null;
            try
            {

                //cmd = new SqlCommand("select * from InputModuleParameterDetails as M left join SystemDocTransaction as T on(M.Id=T.ParameterID) where InputModule=@inputModule and SubInputModule=@subinputModule", con);
                cmd = new SqlCommand("[dbo].[S_Get_SystemDocDataSaveUpdate]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SDoc", id);
                cmd.Parameters.AddWithValue("@InputModule", InputModule);
                cmd.Parameters.AddWithValue("@SubInputModule", SubInputModule);
                cmd.Parameters.AddWithValue("@Param", "View");
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        parameter = new DataInputModuleParameter();
                        if (SubInputModule == "Grinding Parameters")
                        {
                            if (sdr["BatchID"].ToString() == "")
                            {
                                parameter.PrameterId = sdr["ParameterID"].ToString();
                                parameter.Prameter = sdr["Parameter"].ToString();
                                parameter.CustomeName = sdr["Customname"].ToString();
                                parameter.Value = sdr["Value"].ToString();
                                parameter.Datatype = sdr["DataType"].ToString();
                                parameter.ObjectType = sdr["Entry_Type"].ToString();
                                parameter.ImageName = sdr["SubInputModule"].ToString();
                                parameter.CalculatedFlag = sdr["CalculateFlag"].ToString();
                                parameter.Recommendation = sdr["Recommendation"].ToString();
                                parameter.LimitRange = sdr["LSL"].ToString() + ";" + sdr["USL"].ToString();
                                //listParameter.ListofValue = sdr["ListOfValue"].ToString();

                                parameter.DependancyFlag = sdr["DependencyFlag"].ToString();
                                parameter.Dependancy = sdr["Dependency"].ToString();
                                parameter.IndependentParameter = sdr["IndependentParameter"].ToString();
                                parameter.Mandatory = sdr["MandatoryFlag"].ToString();
                                if (sdr["LimitImage"].ToString() == "")
                                {
                                    parameter.Image = "";
                                }
                                else
                                {
                                    byte[] bytes = (byte[])sdr["LimitImage"];
                                    string strBase64 = Convert.ToBase64String(bytes);
                                    parameter.Image = "data:Image/png;base64," + strBase64;
                                }
                                listParameter.Add(parameter);

                            }

                        }
                        else
                        {
                            if (sdr["Parameter"].ToString() == "Hardness Unit")
                            {
                                string s= sdr["Value"].ToString();
                                HttpContext.Current.Session["hardnessUnit"] = sdr["Value"].ToString();
                                continue;
                            }

                            parameter.PrameterId = sdr["ParameterID"].ToString();
                            parameter.Prameter = sdr["Parameter"].ToString();
                            parameter.CustomeName = sdr["Customname"].ToString();
                            parameter.Value = sdr["Value"].ToString();
                            parameter.Datatype = sdr["DataType"].ToString();
                            parameter.ObjectType = sdr["Entry_Type"].ToString();
                            parameter.ImageName = sdr["SubInputModule"].ToString();
                            parameter.CalculatedFlag = sdr["CalculateFlag"].ToString();
                            parameter.Recommendation = sdr["Recommendation"].ToString();
                            parameter.LimitRange = sdr["LSL"].ToString() + ";" + sdr["USL"].ToString();
                            //listParameter.ListofValue = sdr["ListOfValue"].ToString();

                            parameter.DependancyFlag = sdr["DependencyFlag"].ToString();
                            parameter.Dependancy = sdr["Dependency"].ToString();
                            parameter.IndependentParameter = sdr["IndependentParameter"].ToString();
                            parameter.Mandatory= sdr["MandatoryFlag"].ToString();

                            if (sdr["LimitImage"].ToString() == "")
                            {
                                parameter.Image = "";
                            }
                            else
                            {
                                byte[] bytes = (byte[])sdr["LimitImage"];
                                string strBase64 = Convert.ToBase64String(bytes);
                                parameter.Image = "data:Image/png;base64," + strBase64;
                            }
                            listParameter.Add(parameter);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("Input Module - getDataInputModuleData" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return listParameter;
        }

        #endregion
        internal static List<string> getGIObjective(string inputtext)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<string> list = new List<string>();
            try
            {


                cmd = new SqlCommand("select * from Objective where Item like '@input%'", con);
                cmd.Parameters.AddWithValue("@input", inputtext);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            list.Add(sdr["Item"].ToString());

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
            return list;
        }
        internal static List<InputModuleParameter> getInputModuleParameterDetails(string InputModuleName, string Parameterid)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<InputModuleParameter> list = new List<InputModuleParameter>();
            InputModuleParameter parameter = null;
            try
            {


                cmd = new SqlCommand("[dbo].[S_Get_SystemDocDataSaveUpdate]", con);
                cmd.Parameters.AddWithValue("@InputModule", InputModuleName);
                cmd.Parameters.AddWithValue("@ParameterId", Parameterid);
                cmd.Parameters.AddWithValue("@Param", "ValueList");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        try
                        {
                            //  string listvalue = sdr["ListValue"].ToString();
                            parameter = new InputModuleParameter();
                            string listvalue = sdr["ListValue"].ToString();
                            parameter.ParameterDetails = listvalue;
                            parameter.Limitrange = sdr["LSL"].ToString() + "," + sdr["USL"].ToString() + "," + listvalue;
                            list.Add(parameter);
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteDebugLog("get InputModule ParameterDetails"+ex.Message);
                        }

                    }

                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("get InputModule ParameterDetails"+ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }

        internal static int InsertUpdateInputModule(string SDoc, DataInputModuleParameter data, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<string> list = new List<string>();
            if(data.GrindingProcess=="" || data.GrindingProcess==null)
            {
                data.GrindingProcess = "";
            }
            int success = 0;
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_SystemDocDataSaveUpdate]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SDoc", SDoc);
                cmd.Parameters.AddWithValue("@ParameterId", data.PrameterId);
                cmd.Parameters.AddWithValue("@value", data.Value);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.Parameters.AddWithValue("@SDocName", data.SDOcName);
                cmd.Parameters.AddWithValue("@PlungeId", data.PlungeId);
                cmd.Parameters.AddWithValue("@SubCategoryId", data.SubcategoryId);
                cmd.Parameters.AddWithValue("@UserId", data.Username);
                cmd.Parameters.AddWithValue("@FormulaStr", data.CalculatedFormula);
                cmd.Parameters.AddWithValue("@GrindingProcess", data.GrindingProcess);
                cmd.CommandTimeout = 120;
                success = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    success = -2;
                }
                else
                {
                    Logger.WriteErrorLog("Error while save or update Input Module - " + ex.Message);
                    success = 0;
                }
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return success;
        }

        internal static void DeleteSDocID(string SDoc)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<string> list = new List<string>();
            try
            {
                cmd = new SqlCommand("delete from SystemDocTransaction where SDocId=@SDoc", con);
                cmd.Parameters.AddWithValue("@SDoc", SDoc);
                cmd.CommandType = CommandType.Text;
                 cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
               
                    Logger.WriteErrorLog("Error while delete SdocId - " + ex.Message);
                   
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
           
        }

        internal static int InsertUpdateInputModuleForImage(string SDoc, DataInputModuleParameter data, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<string> list = new List<string>();
            int success = 0;
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_SystemDocDataSaveUpdate]", con);
                cmd.Parameters.AddWithValue("@SDoc", SDoc);
                cmd.Parameters.AddWithValue("@ParameterId", data.PrameterId);
                cmd.Parameters.AddWithValue("@value", data.Value);
                cmd.Parameters.AddWithValue("@SubInputModule", data.ImagePath);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.Parameters.AddWithValue("@UserId", data.Username);
                cmd.CommandType = CommandType.StoredProcedure;
                success = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    success = -2;
                }
                else
                {
                    Logger.WriteErrorLog("Error while save or update for Input Module  Image- " + ex.Message);
                    success = 0;
                }
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return success;
        }







        #region  ------------ Application Tool Kit --------------------------
        public static DataTable getAllTabDetails(string tabname, string query)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[S_Get_SystemDocAnalysis]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InputModule", tabname);
                cmd.Parameters.AddWithValue("@Query", query);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                Logger.WriteDebugLog("ATK Get All Tab Details" +e.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return dt;
        }
        internal static List<string> getATKTabParameterValueScript(string colname, string query)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            List<string> list = new List<string>();
            SqlDataReader sdr = null;
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[S_Get_SystemDocAnalysis]", con);
                cmd.Parameters.AddWithValue("@Parameter", colname);
                cmd.Parameters.AddWithValue("@Query", query);
                cmd.Parameters.AddWithValue("@Param", "ValueList");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        //string str = sdr["Value"].ToString().Replace('\n', ' ');
                        string str = sdr["Value"].ToString();
                        list.Add(str);
                    }
                }


            }
            catch (Exception e)
            {
                Logger.WriteDebugLog("ATK get Tab Parameter Value Script"+ e.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();

            }
            return list;
        }
        internal static List<string> ddlSdocPlungeCatIndexChange(string param, string query)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            List<string> list = new List<string>();
            try
            {
                if (query.Trim() != "")
                {
                    cmd = new SqlCommand("select distinct " + param + " from SystemDocTransaction where isnull(" + param + ",'')<>'' and " + query + " order by " + param, con);
                }
                else
                {
                    cmd = new SqlCommand("select distinct " + param + " from SystemDocTransaction where isnull(" + param + ",'')<>'' order by " + param, con);
                }
                sdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(sdr);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i][0].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog(" ATK - ddl Sdoc Plunge Cat Index Change" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }
        internal static List<ListItem> getATKGraphValueFrequency(string param, string query)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            List<ListItem> list = new List<ListItem>();
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_SystemDocAnalysis]", con);
                cmd.Parameters.AddWithValue("@Query", query);
                cmd.Parameters.AddWithValue("@Parameter", param);
                cmd.Parameters.AddWithValue("@Param", "Statistics");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(new ListItem
                        {
                            Value = sdr["Value"].ToString(),
                            Text = sdr["frequency"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("ATK - get ATKGraph Value Frequency"+ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }

        internal static List<string> getQueryList()
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<string> queryList = new List<string>();
            try
            {
                cmd = new SqlCommand("select * from AnalysisQueryHistory  where ISNULL(Query,'')<>'' order by UpdatedTime desc", con);
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        queryList.Add(sdr["Query"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("ATK - getQueryList" +ex.Message);
            }
            finally
            {
                if (con != null) con.Close(); if (sdr != null) sdr.Close();
            }
            return queryList;
        }
        internal static void saveQueryInfo(string query, string updatedby)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_AnalysisQueryHistorySave]", con);
                cmd.Parameters.AddWithValue("@Query", query);
                cmd.Parameters.AddWithValue("@UpdatedBy", updatedby);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("ATK - save Query Info"+ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
            }
        }
        internal static List<CustomColumn> getCustomColumnName()        {            SqlConnection con = ConnectionManager.GetConnection();            SqlDataReader sdr = null;            SqlCommand cmd = null;            List<CustomColumn> list = new List<CustomColumn>();            CustomColumn parameter = null;            try            {                cmd = new SqlCommand("[dbo].[S_Get_InputParameterDeatils]", con);                cmd.CommandType = CommandType.StoredProcedure;                sdr = cmd.ExecuteReader();                if (sdr.HasRows)                {                    while (sdr.Read())                    {
                        parameter = new CustomColumn();
                        string param = sdr["Parameter"].ToString();
                        if (param != "SDoc ID")
                        {
                            parameter.ColumnName = param;
                            parameter.CustomName = sdr["CustomName"].ToString();
                            list.Add(parameter);
                        }                    }                }            }            catch (Exception ex)            {                Logger.WriteDebugLog("ATK - get Custom ColumnName"+ex.Message);            }            finally            {                if (con != null) con.Close();                if (sdr != null) sdr.Close();            }            return list;        }
        public static DataTable getSystemDocumentData()
        {
            SqlConnection con = ConnectionManager.GetConnection();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[S_Get_SystemDocAnalysis]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InputModule", "General Information");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                Logger.WriteDebugLog("ATK - get SystemDocument Data" + e.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return dt;
        }
        public static DataTable getMachineToolData()
        {
            SqlConnection con = ConnectionManager.GetConnection();
            DataTable dt = new DataTable();
            try
            {

                SqlCommand cmd = new SqlCommand("[dbo].[S_Get_SystemDocAnalysis]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InputModule", "Machine Tool");

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                Logger.WriteDebugLog("ATK - get MachineTool Data" + e.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return dt;
        }
        public static DataTable getWheelData()
        {
            SqlConnection con = ConnectionManager.GetConnection();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[S_Get_SystemDocAnalysis]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InputModule", "Consumables Details");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                Logger.WriteDebugLog("ATK - get Wheel Data" + e.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return dt;
        }
        public static DataTable getWorkPieceData()
        {
            SqlConnection con = ConnectionManager.GetConnection();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[S_Get_SystemDocAnalysis]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InputModule", "Workpiece Details");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                Logger.WriteDebugLog("ATK - get WorkPiece Data" + e.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return dt;
        }
        public static DataTable getOperationalParameterData()
        {
            SqlConnection con = ConnectionManager.GetConnection();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[S_Get_SystemDocAnalysis]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InputModule", "Operational Parameters");
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                Logger.WriteDebugLog("ATK - get OperationalParameter Data" + e.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return dt;
        }
        public static DataTable getTargetQualityData()
        {
            SqlConnection con = ConnectionManager.GetConnection();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[S_Get_SystemDocAnalysis]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InputModule", "Quality Parameters");

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }
            catch (Exception e)
            {
                Logger.WriteDebugLog("ATK - get TargetQuality Data" + e.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
            }
            return dt;
        }
        public static List<CustomColumn> getATKTabParameter(string input)
        {
            List<CustomColumn> listData = new List<CustomColumn>();
            CustomColumn data = null;
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            
            try
            {
                //SqlCommand cmd = new SqlCommand("select Customname,Column_Name from InputModuleParameterDetails where InputModule=@tabname", con);
                //cmd.Parameters.AddWithValue("@tabname", tabname);
                SqlCommand cmd = new SqlCommand("[dbo].[S_Get_InputParameterDeatils]", con);
                cmd.Parameters.AddWithValue("@Param", input);                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        //if (tabname != "General Information")
                        //{
                        //    if (sdr["Parameter"].ToString() == "SDoc ID")
                        //    {
                        //        data = new CustomColumn();
                        //        data.ColumnName = sdr["Column_Name"].ToString();
                        //        data.CustomName = sdr["CustomName"].ToString();
                        //        listData.Add(data);
                        //    }
                        //    else
                        //    {
                        //        if (sdr["InputModule"].ToString() == tabname)
                        //        {
                        //            data = new CustomColumn();
                        //            data.ColumnName = sdr["Column_Name"].ToString();
                        //            data.CustomName = sdr["CustomName"].ToString();
                        //            listData.Add(data);
                        //        }
                        //    }
                        //}
                        //else
                        //{


                        //    if (sdr["InputModule"].ToString() == tabname)
                        //    {
                        data = new CustomColumn();
                        data.ColumnName = sdr["Parameter"].ToString();
                        data.CustomName = sdr["CustomName"].ToString();
                        if (input == "Statistics")
                        {
                            data.DerivedFlag = sdr["DerivedFlag"].ToString();
                          
                            data.InputModule= sdr["InputModule"].ToString();

                        }
                        listData.Add(data);
                        //    }
                        //}
                        //string colname = sdr["Column_Name"].ToString();                        //string custname = sdr["Customname"].ToString();                        //if (colname.Contains(';'))                        //{                        //    string[] collist = colname.Split(';');                        //    string[] customlist = custname.Split(';');                        //    for (int i = 0; i < collist.Length; i++)                        //    {                        //        data = new CustomColumn();                        //        data.ColumnName = collist[i];                        //        data.CustomName = customlist[i];                        //        listData.Add(data);                        //    }                        //}                        //else                        //{                        //    data = new CustomColumn();                        //    data.ColumnName = sdr["Column_Name"].ToString();                        //    data.CustomName = sdr["Customname"].ToString();                        //    listData.Add(data);                        //}
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteDebugLog("ATK - get ATK TabParameter" + e.Message);
                
            }
            finally
            {
                if (con != null) { con.Close(); }
                if (sdr != null) { sdr.Close(); }
            }
            return listData;
        }

        public static List<ADKParameterMinMaxAvg> getATKParameterMinMaxAvg(string query, string column, string sort, out int success)        {            SqlConnection con = ConnectionManager.GetConnection();            List<ADKParameterMinMaxAvg> listdata = new List<ADKParameterMinMaxAvg>();            SqlDataReader sdr = null;            ADKParameterMinMaxAvg data = null;            success = 0;            try            {                SqlCommand cmd = new SqlCommand("[dbo].[S_Get_SystemDocAnalysis]", con);                cmd.Parameters.AddWithValue("@Query", query);                cmd.Parameters.AddWithValue("@Column", column);                cmd.Parameters.AddWithValue("@OrderBy", sort);                cmd.Parameters.AddWithValue("@Param", "Statistics");                cmd.CommandType = CommandType.StoredProcedure;                sdr = cmd.ExecuteReader();                if (sdr.HasRows)                {                    while (sdr.Read())                    {                        data = new ADKParameterMinMaxAvg();                        data.Parameter = sdr["Parameter"].ToString();                        data.Min = sdr["Min"].ToString();                        data.Max = sdr["Max"].ToString();                        data.Avg = sdr["Avg"].ToString();
                        data.DerivedFlag = sdr["DerivedFlag"].ToString();                        listdata.Add(data);                    }                }
                //SqlCommand cmd = new SqlCommand("[dbo].[S_Get_GrindingProcessStatistics]", con);
                //cmd.Parameters.AddWithValue("@SortOrder", sort);
                //cmd.CommandType = CommandType.StoredProcedure;
                //sdr = cmd.ExecuteReader();
                //if (sdr.HasRows)
                //{
                //    while (sdr.Read())
                //    {
                //        data = new ADKParameterMinMaxAvg();
                //        data.Parameter = sdr["Parameter"].ToString();
                //        data.Min = sdr["Min"].ToString();
                //        data.Max = sdr["Max"].ToString();
                //        data.Avg = sdr["Avg"].ToString();
                //        listdata.Add(data);
                //    }
                //}
            }            catch (Exception e)            {                success = 1;
                Logger.WriteDebugLog("ATK - get ATKParameter MinMaxAvg" + e.Message);            }            finally            {                if (con != null)                {                    con.Close();                }                if (sdr != null)                {                    sdr.Close();                }            }            return listdata;        }
        public static List<ADKParameter> getATKDDLGraphParameter()
        {
            SqlConnection con = ConnectionManager.GetConnection();
            List<ADKParameter> listdata = new List<ADKParameter>();
            SqlDataReader sdr = null;
            ADKParameter data = null;
            try
            {
                //SqlCommand cmd = new SqlCommand("select distinct Parameter from OperationalParameterDetails", con);
                SqlCommand cmd = new SqlCommand("[dbo].[S_Get_GrindingProcessStatistics]", con);                cmd.Parameters.AddWithValue("@SortOrder", "");                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        data = new ADKParameter();
                        data.Parameter = sdr["Parameter"].ToString();
                        listdata.Add(data);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteDebugLog("ATK - get ATK DDL GraphParameter" + e.Message);
            }
            finally
            {
                if (con != null) { con.Close(); }
                if (sdr != null) { sdr.Close(); }
            }

            return listdata;
        }

        //public static List<ADKParameter> getATKTabParameterValue(string colname)
        //{
        //    SqlConnection con = ConnectionManager.GetConnection();
        //    SqlDataReader sdr = null;
        //    List<ADKParameter> listdata = new List<ADKParameter>();
        //    ADKParameter data = null;
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand("select @colname from System_Documents_", con);
        //        cmd.Parameters.AddWithValue("@colname" , colname);
        //        sdr = cmd.ExecuteReader();
        //        if(sdr.HasRows)
        //        {
        //            while(sdr.Read())
        //            {
        //                data = new ADKParameter();
        //                data.Parameter = sdr[colname].ToString();
        //                listdata.Add(data);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //    }
        //    finally
        //    {
        //        if (con != null)
        //        {
        //            con.Close();
        //        }
        //        if (sdr != null)
        //        {
        //            sdr.Close();
        //        }
        //    }
        //    return listdata;
        //}
        public static DataTable getATKTabParameterValue(string colname)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            DataTable dt = new DataTable();
            try
            {
                SqlCommand cmd = new SqlCommand("[dbo].[S_Get_SystemDocAnalysis]", con);
                cmd.Parameters.AddWithValue("@Parameter", colname);
                cmd.Parameters.AddWithValue("@Param", "ValueList");
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);


            }
            catch (Exception e)
            {
                Logger.WriteDebugLog("ATK - get ATK TabParameter Value" + e.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }

            }
            return dt;
        }
        public static DataTable getQueryData(string query, string column, string inputmodule, out string error)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            DataTable dt = new DataTable();
            error = "";
            SqlDataReader sdr = null;
            try
            {
                //SqlCommand cmd = new SqlCommand(query, con);
                SqlCommand cmd = new SqlCommand("[dbo].[S_Get_SystemDocAnalysis]", con);
                cmd.Parameters.AddWithValue("@Query", query);
                cmd.Parameters.AddWithValue("@Column", column);
                cmd.Parameters.AddWithValue("@InputModule", inputmodule);
                cmd.Parameters.AddWithValue("@SubInputModule", "");
                cmd.Parameters.AddWithValue("@Parameter", "");
                cmd.Parameters.AddWithValue("@Param", "");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    dt.Load(sdr);
                }
                //SqlDataAdapter sda = new SqlDataAdapter(cmd);

                //sda.Fill(dt);
            }
            catch (Exception e)
            {
                error = "Error";
                Logger.WriteDebugLog("ATK - get Query Data" + e.Message);
            }
            finally
            {
                if (con != null)
                {
                    con.Close();
                }
                if (sdr != null)
                {
                    sdr.Close();
                }
            }
            return dt;
        }

        public static string getATKDDLMeanValue(string param)        {            SqlConnection con = ConnectionManager.GetConnection();            SqlDataReader sdr = null;            string mean = "";            try            {                SqlCommand cmd = new SqlCommand("select Avg from OperationalParameterDetails where Parameter=@param", con);                cmd.Parameters.AddWithValue("@param", param);                sdr = cmd.ExecuteReader();                if (sdr.HasRows)                {                    while (sdr.Read())                    {                        mean = sdr["Avg"].ToString();                    }                }            }            catch (Exception e)            {
                Logger.WriteDebugLog("ATK - get ATK DDL MeanValue" + e.Message);            }            finally            {                if (con != null) { con.Close(); }                if (sdr != null) { sdr.Close(); }            }            return mean;        }
        #endregion


        #region  -----Derived Parameter -----

        internal static int setSparkandTangoRelief(string SDoc,  string sparkout, string userid, string traverseFedd, string slideForward, string prgmRead, string flagging, string slideReturn, string other, string loadUnload, string chipwidthratio, string wheeltiltangle, string otherDescription, string manualLoadUnload, string remarks)
        {
            //string othertimedescription
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<string> list = new List<string>();
            int success = 0;
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_InputProcessEngineeringData]", con);
                cmd.Parameters.AddWithValue("@SDocId", SDoc);
              //  cmd.Parameters.AddWithValue("@SparkOut", tango);
                cmd.Parameters.AddWithValue("@SparkOut", sparkout);
                cmd.Parameters.AddWithValue("@UserId", userid);

                cmd.Parameters.AddWithValue("@TraverseFeedGrinding", traverseFedd);
                cmd.Parameters.AddWithValue("@SlideForward", slideForward);
                cmd.Parameters.AddWithValue("@ProgramRead", prgmRead);
                cmd.Parameters.AddWithValue("@Flagging", flagging);
                cmd.Parameters.AddWithValue("@Slide", slideReturn);
                cmd.Parameters.AddWithValue("@Others", other);
               cmd.Parameters.AddWithValue("@OthersDescription", otherDescription);
                cmd.Parameters.AddWithValue("@LoadingUnloading", loadUnload);

                cmd.Parameters.AddWithValue("@Chipwidththickness_ratio", chipwidthratio);
                cmd.Parameters.AddWithValue("@wheeltilt_angles", wheeltiltangle);
                cmd.Parameters.AddWithValue("@ManualLoadingUnloading", manualLoadUnload);
                cmd.Parameters.AddWithValue("@Remark", remarks);

                cmd.Parameters.AddWithValue("@Param", "Save");
                cmd.CommandType = CommandType.StoredProcedure;
                success = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    success = -2;
                }
                else
                {
                    Logger.WriteErrorLog("Derived Parameter - Error while set Spark and tango relief time - " + ex.Message);
                    success = 0;
                }
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return success;
        }

        //, byte[] signalfilepath
        internal static int setSignalFileName(string SDoc, string signalfilename,string  filepath, string userid)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<string> list = new List<string>();
            int success = 0;
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_InputProcessEngineeringData]", con);
                cmd.Parameters.AddWithValue("@SDocId", SDoc);
                cmd.Parameters.AddWithValue("@FileName", signalfilename);
                cmd.Parameters.AddWithValue("@File", filepath);
                cmd.Parameters.AddWithValue("@UserId", userid);
                cmd.Parameters.AddWithValue("@Param", "Save");
                cmd.CommandType = CommandType.StoredProcedure;
                success = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    success = -2;
                }
                else
                {
                    Logger.WriteErrorLog("Derived Parameter - Error while save signal file path name - " + ex.Message);
                    success = 0;
                }
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return success;
        }

        internal static int removeSignalFileName(string SDoc, string filename)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<string> list = new List<string>();
            int success = 0;
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_InputProcessEngineeringData]", con);
                cmd.Parameters.AddWithValue("@SDocId", SDoc);
                cmd.Parameters.AddWithValue("@FileName", filename);
                cmd.Parameters.AddWithValue("@Param", "Remove");
                cmd.CommandType = CommandType.StoredProcedure;
                success = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    success = -2;
                }
                else
                {
                    Logger.WriteErrorLog("Derived Parameter - Error while remove signal file path name - " + ex.Message);
                    success = 0;
                }
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return success;
        }


        internal static List<DerivedInputParameter> getDerivedInputParameter(string id, out DerivedOutputParameter dop, out int success, out List<DerivedOutputParameter> listsignalprocess, out string Sdocstatus)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<DerivedInputParameter> list = new List<DerivedInputParameter>();
            listsignalprocess = new List<DerivedOutputParameter>();
            DerivedOutputParameter signalProcess = null;
            DerivedInputParameter parameter = null;
            dop = new DerivedOutputParameter();
            Sdocstatus = "";
            success = 0;
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_InputProcessEngineeringData]", con);
                cmd.Parameters.AddWithValue("@SDocId", id);
                cmd.Parameters.AddWithValue("@Param", "");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        Sdocstatus = sdr["SDocStatus"].ToString();
                    }
                    sdr.NextResult();
                    while (sdr.Read())
                    {
                        parameter = new DerivedInputParameter();
                        parameter.Point = sdr["Point"].ToString();
                        parameter.Identifier = sdr["Identifier"].ToString();
                        parameter.Diameter = sdr["Dia"].ToString();
                        parameter.DOC = sdr["DOC"].ToString();
                        parameter.StockonFace = sdr["StockonFace"].ToString();
                        parameter.InFeed = sdr["InFeed"].ToString();
                       // parameter.WorkSpeed = sdr["WorkSpeed"].ToString();
                        parameter.ODWidth = sdr["GrindingODWidth"].ToString();
                        parameter.FeedAngle = sdr["FeedAngle"].ToString();
                        parameter.WorkSpeedOD = sdr["WorkSpeedOD"].ToString();
                        parameter.WorkSpeedFace = sdr["WorkSpeedFace"].ToString();
                        parameter.XFeed = sdr["X_FEED"].ToString();
                        parameter.ZFeed = sdr["Z_FEED"].ToString();
                        parameter.TangoFlagOD = sdr["TANGOFlagOD"].ToString();
                        parameter.TangoFlagFace = sdr["TANGOFlagFace"].ToString();
                        if (parameter.TangoFlagOD == "1" || parameter.TangoFlagFace == "1")
                        {
                            parameter.TangoColor = "redColor";
                        }
                        else
                        {
                            parameter.TangoColor = "blackColor";
                        }
                        list.Add(parameter);

                    }
                    sdr.NextResult();
                    while (sdr.Read())
                    {
                        dop.Sparkouttime = sdr["SparkOutTime"].ToString();
                        dop.Targetrelieftime = sdr["TangoReliefTime"].ToString();
                        dop.signalfilename = sdr["FileName"].ToString();
                        dop.TraverseSpeed = sdr["TraverseFeedGrindingTime"].ToString();
                        dop.SlideForward = sdr["SlideForwardTime"].ToString();
                        dop.ProgramRead = sdr["ProgramReadTime"].ToString();
                        dop.Flagging = sdr["FlaggingTime"].ToString();
                        dop.SlideReturn = sdr["SlideTime"].ToString();
                        dop.Others = sdr["OthersTime"].ToString();
                        dop.OthersTimeDescription = sdr["OthersDescription"].ToString();
                        dop.LoadUnloadTime = sdr["LoadingUnloadingTime"].ToString();
                        dop.Chipwidthratio = sdr["Chipwidththicknessratio"].ToString();
                        dop.Wheeltiltangle = sdr["wheeltiltangles"].ToString();
                        dop.ManualLoadingUnloading = sdr["ManualLoadingUnloading"].ToString();
                        dop.remarks = sdr["Remarks"].ToString();
                        dop.DressingCycleTime = sdr["Dressing_Cycle_Time"].ToString();
                    }
                    sdr.NextResult();
                    while (sdr.Read())
                    {
                        signalProcess = new DerivedOutputParameter();
                        signalProcess.signalfilename = sdr["FileName"].ToString();
                        signalProcess.signalfilepath = sdr["FilePath"].ToString();
                        listsignalprocess.Add(signalProcess);
                    }

                }

            }
            catch (Exception ex)
            {
                success = 1;
                Logger.WriteDebugLog("Derived Parameter - get DerivedInputParameters" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return list;
        }

        internal static string getSDocIdStatus(string Sdocid)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            string result = "UnLocked";
            try
            {
                cmd = new SqlCommand("select * from SystemDocMasterDetails where SDocId=@SDocId and Status='Locked'", con);
                cmd.Parameters.AddWithValue("@SDocId", Sdocid);
                cmd.CommandType = CommandType.Text;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        result = "Locked";

                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("Derived Parameter - get SDocIdStatus" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return result;
        }

        internal static List<DerivedCalculateParameter> getDerivedCalculateParameter(string id, out DerivedOutputParameter dop, out int success)        {            SqlConnection con = ConnectionManager.GetConnection();            SqlDataReader sdr = null;            SqlCommand cmd = null;            List<DerivedCalculateParameter> list = new List<DerivedCalculateParameter>();            DerivedCalculateParameter parameter = null;            dop = new DerivedOutputParameter();            int i = 0;
            success = 0;            try            {                cmd = new SqlCommand("[dbo].[S_Get_InputProcessEngineeringData]", con);                cmd.Parameters.AddWithValue("@SDocId", id);                cmd.Parameters.AddWithValue("@Param", "Calculated");                cmd.CommandType = CommandType.StoredProcedure;                sdr = cmd.ExecuteReader();                if (sdr.HasRows)                {                    while (sdr.Read())                    {                        parameter = new DerivedCalculateParameter();
                        parameter.Point = sdr["Point"].ToString();
                        parameter.RadialDOCX = sdr["RadialDepthOfCutX"].ToString();
                        parameter.RadialDOCZ = sdr["RadialDepthOfCutZ"].ToString();
                        parameter.MRRX = sdr["MRR_X"].ToString();
                        parameter.MRRZ = sdr["MRR_Z"].ToString();
                        parameter.TotalMRRX = sdr["TotalMRR_X"].ToString();
                        parameter.ToralMRR = sdr["TotalMRR"].ToString();
                        parameter.GritPenetrationDepthX = sdr["GritPenetrationDepth_X"].ToString();
                        parameter.GritPenetrationDepthZ = sdr["GritPenetrationDepth_Z"].ToString();                        parameter.Time = sdr["Time"].ToString();
                        parameter.WorkSpeedRatio = sdr["WheelWorkSpeedRatio"].ToString();
                        parameter.WorkRPMRatio = sdr["WheelWorkRPMRatio"].ToString();
                        parameter.TangoFlagOD = sdr["TANGOFlagOD"].ToString();
                        parameter.TangoFlagFace = sdr["TANGOFlagFace"].ToString();
                        if (parameter.TangoFlagOD == "1" || parameter.TangoFlagFace == "1")
                        {
                            parameter.TangoColor = "redColor";
                        }
                        else
                        {
                            parameter.TangoColor = "blackColor";
                        }                        list.Add(parameter);

                    }                    sdr.NextResult();                    while (sdr.Read())                    {
                        dop.EquivalentDia = sdr["EquivalentDiaOD"].ToString();                        dop.EquivalentDiaFace = sdr["EquivalentDiaFace"].ToString();
                        dop.CuttingEdgeDensity = sdr["Cuttingedgedensity"].ToString();
                        dop.SparkOutRevolutions = sdr["SparkOutRevolutions"].ToString();                        dop.GrindingCycletime = sdr["GrindingCycleTime"].ToString();                        dop.NongrindingCycleTime = sdr["NonGrindingCycleTime"].ToString();
                        dop.TotalGrindingTime = sdr["TotalGrindingCycleTime"].ToString();
                        dop.TotalCycletime = sdr["TotalCycleTime"].ToString();
                        dop.FloorToFloor = sdr["FloorToFloor"].ToString();
                    }

                }

            }            catch (Exception ex)            {
                success = 1;                Logger.WriteDebugLog("Derived Parameter - get DerivedCalculateParameter" + ex.Message);

            }            finally            {                if (con != null) con.Close();                if (sdr != null) sdr.Close();            }            return list;        }



        internal static void setSpartAndTango(string spark, string tango, string id)        {            SqlConnection con = ConnectionManager.GetConnection();            SqlDataReader sdr = null;            SqlCommand cmd = null;            try            {                cmd = new SqlCommand("update System_Documents set SparkOutTime=@spark, TargetreliefTime=@tango where ID=@sdocid", con);                cmd.Parameters.AddWithValue("@spark", spark);                cmd.Parameters.AddWithValue("@tango", tango);                cmd.Parameters.AddWithValue("@sdocid", id);                cmd.CommandType = CommandType.Text;                cmd.ExecuteNonQuery();            }            catch (Exception ex)            {                Logger.WriteDebugLog("Derived Parameter - set SpartAndTango" + ex.Message);            }            finally            {                if (con != null) con.Close();                if (sdr != null) sdr.Close();            }        }

        internal static int setSDocLockStatus(string Sdocid,string username)        {            SqlConnection con = ConnectionManager.GetConnection();            SqlDataReader sdr = null;            SqlCommand cmd = null;            int success = 0;            try            {                cmd = new SqlCommand("[dbo].[S_Get_SystemDocStatusSaveView]", con);                cmd.Parameters.AddWithValue("@SDocId", Sdocid);                cmd.Parameters.AddWithValue("@User", username);                cmd.Parameters.AddWithValue("@Param", "Save");
                cmd.Parameters.AddWithValue("@Status", "Locked");                cmd.CommandType = CommandType.StoredProcedure;               success= cmd.ExecuteNonQuery();            }            catch (Exception ex)            {                Logger.WriteDebugLog("Derived Parameter - set SDocLock Status" + ex.Message);                success = 0;            }            finally            {                if (con != null) con.Close();                if (sdr != null) sdr.Close();            }            return success;        }



        #endregion

        #region ------------------ Output Module ---------------
        internal static string getHardnessUnit(string sdocid)        {            SqlConnection con = ConnectionManager.GetConnection();            SqlDataReader sdr = null;            SqlCommand cmd = null;            string unit = string.Empty;            try            {                cmd = new SqlCommand("select SDocId,Value,Parameter from SystemDocTransaction t join InputModuleParameterDetails m on t.ParameterID=m.ParameterID where Parameter='Hardness Unit' and SDocId=@Sdoc", con);                cmd.Parameters.AddWithValue("@Sdoc", sdocid);                cmd.CommandType = CommandType.Text;                sdr = cmd.ExecuteReader();                if (sdr.HasRows)                {                    while (sdr.Read())                    {                        try                        {                            unit = sdr["Value"].ToString();                        }                        catch (Exception ex)                        {                            Logger.WriteDebugLog("Output Module - getHardnessunit" + ex.Message);                        }                    }                }            }            catch (Exception ex)            {                Logger.WriteDebugLog("Output Module - "+ex.Message);            }            finally            {                if (con != null) con.Close();                if (sdr != null) sdr.Close();            }            return unit;        }

        public static List<string> getOMSubCategoryBasedOnPlunges(string sdoc, string plunge)
        {
            List<string> listData = new List<string>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            try
            {

                SqlCommand cmd = new SqlCommand("select distinct SubCategoryId from SystemDocTransaction where SDocName=@id and PlungeId in (" + plunge + ") order by SubCategoryId", con);
                cmd.Parameters.AddWithValue("@id", sdoc);
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        listData.Add(sdr["SubCategoryId"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteDebugLog("Output Module - get OutputModule SubCategoryBasedOnPlunges" + e.Message);
            }
            finally
            {
                if (con != null) { con.Close(); }
                if (sdr != null) { sdr.Close(); }
            }
            return listData;
        }
        public static List<string> getOMSdocname()
        {
            List<string> listData = new List<string>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            try
            {

                SqlCommand cmd = new SqlCommand("select distinct SDocName from SystemDocTransaction where ISNULL(SDocName,'')<>'' order by SDocName ", con);
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        listData.Add(sdr["SDocName"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteDebugLog("Output Module - get OutputModule Sdocname" + e.Message);
            }
            finally
            {
                if (con != null) { con.Close(); }
                if (sdr != null) { sdr.Close(); }
            }
            return listData;
        }
        public static List<string> getOMPlunges(string Sdocname)
        {
            List<string> listData = new List<string>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            try
            {

                SqlCommand cmd = new SqlCommand("select distinct PlungeId from SystemDocTransaction where SDocName=@id order by PlungeId", con);
                cmd.Parameters.AddWithValue("@id", Sdocname);
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        listData.Add(sdr["PlungeId"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteDebugLog("Output Module - get OutputModule Plunges" + e.Message);
            }
            finally
            {
                if (con != null) { con.Close(); }
                if (sdr != null) { sdr.Close(); }
            }
            return listData;
        }
        public static List<string> getOMSubCategory(string Sdocname)
        {
            List<string> listData = new List<string>();
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            try
            {

                SqlCommand cmd = new SqlCommand("select distinct SubCategoryId from SystemDocTransaction where where SDocName=@id", con);
                cmd.Parameters.AddWithValue("@id", Sdocname);
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        listData.Add(sdr["SubCategoryId"].ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteDebugLog("Output Module - get OutputModule SubCategory" + e.Message);
            }
            finally
            {
                if (con != null) { con.Close(); }
                if (sdr != null) { sdr.Close(); }
            }
            return listData;
        }
        public static List<CustomColumn> getOMGeneralInfoParamForFilter()
        {
            List<CustomColumn> listData = new List<CustomColumn>();
            CustomColumn data = null;
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            try
            {

                SqlCommand cmd = new SqlCommand("[dbo].[S_Get_InputParameterDeatils]", con);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        string inputModule = sdr["InputModule"].ToString();
                        if (inputModule != "Quality Parameters" && inputModule != "Calculated Parameters")
                        {
                            data = new CustomColumn();
                            data.ColumnName = sdr["Parameter"].ToString();
                            data.CustomName = sdr["CustomName"].ToString();
                            listData.Add(data);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteDebugLog("Output Module - get OutputModule GeneralInfoParamForFilter" + e.Message);
            }
            finally
            {
                if (con != null) { con.Close(); }
                if (sdr != null) { sdr.Close(); }
            }
            return listData;
        }
        public static List<CustomColumn> getOMQualityParamForFilter()
        {
            List<CustomColumn> listData = new List<CustomColumn>();
            CustomColumn data = null;
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            List<string> param = new List<string>();
            List<string> Custom = new List<string>();
            List<string> CustomName = new List<string>();
            try
            {

                SqlCommand cmd = new SqlCommand("[dbo].[S_Get_InputParameterDeatils]", con);
                cmd.Parameters.AddWithValue("@Param", "OutputModuleQuality");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        param.Add(sdr["ParameterName"].ToString());
                        Custom.Add(sdr["Parameter"].ToString());
                        CustomName.Add(sdr["Customname"].ToString());
                    }
                    string tempName = "";
                    List<string> distParam = new List<string>();
                    for (int i = 0; i < param.Count; i++)
                    {

                        if (param[i] != tempName)
                        {
                            distParam.Add(param[i].ToString());
                        }
                        tempName = param[i].ToString();
                    }
                    for (int i = 0; i < distParam.Count; i++)
                    {
                        data = new CustomColumn();
                        // data.CustomName = distParam[i];
                        string custName = distParam[i];
                        StringBuilder str = new StringBuilder();
                        int count = 0;
                        for (int j = 0; j < param.Count; j++)
                        {
                            if (param[j] == distParam[i])
                            {
                                if (count > 0)
                                {
                                    str.Append(",'" + Custom[j] + "'");
                                }
                                else
                                {
                                    str.Append("'" + Custom[j] + "'");
                                    count++;
                                    custName = CustomName[j].Substring(0, CustomName[j].LastIndexOf(";"));
                                }

                            }
                        }
                        data.CustomName = custName;
                        data.ColumnName = str.ToString();
                        listData.Add(data);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.WriteDebugLog("Output Module - get OutputModule QualityParamForFilter" + e.Message);
            }
            finally
            {
                if (con != null) { con.Close(); }
                if (sdr != null) { sdr.Close(); }
            }
            return listData;
        }

        internal static DataTable omBindGeneralInfo(SdocPlungCat name, string selectedParam, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<OutputModuleParam> list = new List<OutputModuleParam>();
            OutputModuleParam parameter = null;
            DataTable dt = new DataTable();
            try
            {


                //cmd = new SqlCommand("select ID,Customer,Machine_Model,Component_Name,Material,Hardness,Wheel_Make,Wheel_Specs,Dresser_Type,Dresser_Specs,Wheel_Speed_Vs_ms,Work_Speed_Nw_rpm  from System_Documents where ID=@id", con);
                //cmd.Parameters.AddWithValue("@id", id);
                cmd = new SqlCommand("[dbo].[S_Get_OutPutModuleCalculatedParameter]", con);
                cmd.Parameters.AddWithValue("@SDocId", name.Sdocname);
                cmd.Parameters.AddWithValue("@PlungeID", name.Plunge);
                cmd.Parameters.AddWithValue("@SubCategoryID", name.Category);
                cmd.Parameters.AddWithValue("@Parameter", selectedParam);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    dt.Load(sdr);
                    dt.Columns["Customname"].ColumnName = "Items";
                    dt.Columns["Recommendation"].ColumnName = "Recommendation";
                    dt.Columns["Lower Limit"].ColumnName = "Lower Limit";
                    dt.Columns["Upper Limit"].ColumnName = "Upper Limit";
                    dt.Columns.Remove("ParameterID");
                 
                    //while (sdr.Read())
                    //{
                    //    try
                    //    {

                    //parameter = new OutputModuleParam();
                    //parameter.Parameter= sdr["Parameter"].ToString();
                    //parameter.Value = sdr["Value"].ToString();

                    //list.Add(parameter);

                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Logger.WriteDebugLog(ex.Message);
                    //    }
                    //}
                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("Output Module - OutputModule Bind GeneralInfo" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close(); if (sdr != null) sdr.Close();
            }
            return dt;
        }

        internal static OMDerivedParameter omBindDerivedParameter(SdocPlungCat name, string selectedParam, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<OMDerivedParameter> list = new List<OMDerivedParameter>();
            List<DerivedInputParameter> listderivedInputParameters = new List<DerivedInputParameter>();
            List<DerivedCalculateParameter> listderivedCalculateParameters = new List<DerivedCalculateParameter>();
            List<DerivedOutputParameter> listderivedInputParametersOuput = new List<DerivedOutputParameter>();
            List<DerivedOutputParameter> listderivedCalculateParametersOutput = new List<DerivedOutputParameter>();
            OMDerivedParameter parameter = null;
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_OutPutModuleCalculatedParameter]", con);
                cmd.Parameters.AddWithValue("@SDocId", name.Sdocname);
                cmd.Parameters.AddWithValue("@PlungeID", name.Plunge);
                cmd.Parameters.AddWithValue("@SubCategoryID", name.Category);
                cmd.Parameters.AddWithValue("@Parameter", "");
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    parameter = new OMDerivedParameter();

                    DerivedInputParameter derivedInputParameter = null;
                    while (sdr.Read())
                    {
                        derivedInputParameter  = new DerivedInputParameter();
                        derivedInputParameter.SDocId = sdr["SDocID"].ToString();
                        derivedInputParameter.Parameter = sdr["Parameter"].ToString();
                        derivedInputParameter.Diameter = sdr["Dia"].ToString();
                        derivedInputParameter.DOC = sdr["DOC"].ToString();
                        derivedInputParameter.StockonFace = sdr["StockonFace"].ToString();
                        derivedInputParameter.InFeed = sdr["InFeed"].ToString();
                        //derivedInputParameter.WorkSpeed = sdr["WorkSpeed"].ToString();
                        derivedInputParameter.ODWidth = sdr["GrindingODWidth"].ToString();

                        derivedInputParameter.FeedAngle = sdr["FeedAngle"].ToString();
                        derivedInputParameter.WorkSpeedOD = sdr["WorkSpeedOD"].ToString();
                        derivedInputParameter.WorkSpeedFace = sdr["WorkSpeedFace"].ToString();
                        derivedInputParameter.XFeed = sdr["X_FEED"].ToString();
                        derivedInputParameter.ZFeed = sdr["Z_FEED"].ToString();
                        derivedInputParameter.TangoFlagOD = sdr["TANGOFlagOD"].ToString();
                        derivedInputParameter.TangoFlagFace = sdr["TANGOFlagFace"].ToString();
                        if (derivedInputParameter.TangoFlagOD == "1" || derivedInputParameter.TangoFlagFace == "1")
                        {
                            derivedInputParameter.TangoColor = "redColor";
                        }
                        else
                        {
                            derivedInputParameter.TangoColor = "blackColor";
                        }
                        listderivedInputParameters.Add(derivedInputParameter);
                    }
                    parameter.derivedInputParameters = listderivedInputParameters;

                    sdr.NextResult();

                    DerivedOutputParameter derivedOutputParameter = null;
                    while (sdr.Read())
                    {
                        derivedOutputParameter = new DerivedOutputParameter();
                        derivedOutputParameter.SDocId = sdr["SDocID"].ToString();
                        derivedOutputParameter.Sparkouttime = sdr["SparkOutTime"].ToString();
                        derivedOutputParameter.Targetrelieftime = sdr["TangoReliefTime"].ToString();
                        derivedOutputParameter.TraverseSpeed = sdr["TraverseFeedGrindingTime"].ToString();
                        derivedOutputParameter.SlideForward = sdr["SlideForwardTime"].ToString();
                        derivedOutputParameter.ProgramRead = sdr["ProgramReadTime"].ToString();
                        derivedOutputParameter.Flagging = sdr["FlaggingTime"].ToString();
                        derivedOutputParameter.SlideReturn = sdr["SlideTime"].ToString();
                        derivedOutputParameter.Others = sdr["othersTime"].ToString();
                        //derivedOutputParameter.OthersTimeDescription = sdr["othersTime"].ToString();
                        derivedOutputParameter.LoadUnloadTime = sdr["LoadingUnloadingTime"].ToString();
                        derivedOutputParameter.Chipwidthratio = sdr["ChipWidthThicknessRatio"].ToString();
                        derivedOutputParameter.Wheeltiltangle = sdr["WheelTiltAngles"].ToString();
                        derivedOutputParameter.ManualLoadingUnloading = sdr["ManualLoadingUnloading"].ToString();
                        derivedOutputParameter.remarks = sdr["Remarks"].ToString();

                        listderivedInputParametersOuput.Add(derivedOutputParameter);
                    }
                    parameter.derivedInputParametersOutput = listderivedInputParametersOuput;

                    sdr.NextResult();

                    DerivedCalculateParameter derivedCalculateParameter = null;
                    while (sdr.Read())
                    {
                        derivedCalculateParameter = new DerivedCalculateParameter();
                        derivedCalculateParameter.SDocId = sdr["SDocID"].ToString();
                        derivedCalculateParameter.Parameter = sdr["Parameter"].ToString();
                        derivedCalculateParameter.RadialDOCX = sdr["RadialDepthOfCutX"].ToString();
                        derivedCalculateParameter.RadialDOCZ = sdr["RadialDepthOfCutZ"].ToString();
                        derivedCalculateParameter.MRRX = sdr["MRR_X"].ToString();
                        derivedCalculateParameter.MRRZ = sdr["MRR_Z"].ToString();
                        derivedCalculateParameter.TotalMRRX = sdr["TotalMRR_X"].ToString();
                        derivedCalculateParameter.ToralMRR = sdr["TotalMRR"].ToString();                        derivedCalculateParameter.GritPenetrationDepthX = sdr["GritPenetrationDepth_X"].ToString();
                        derivedCalculateParameter.GritPenetrationDepthX = sdr["GritPenetrationDepth_Z"].ToString();                        derivedCalculateParameter.Time = sdr["Time"].ToString();
                        derivedCalculateParameter.WorkSpeedRatio = sdr["WheelWorkSpeedRatio"].ToString();
                        derivedCalculateParameter.WorkRPMRatio = sdr["WheelWorkRPMRatio"].ToString();
                        derivedCalculateParameter.TangoFlagOD = sdr["TANGOFlagOD"].ToString();
                        derivedCalculateParameter.TangoFlagFace = sdr["TANGOFlagFace"].ToString();
                        if (derivedCalculateParameter.TangoFlagOD == "1" || derivedCalculateParameter.TangoFlagFace == "1")
                        {
                            derivedCalculateParameter.TangoColor = "redColor";
                        }
                        else
                        {
                            derivedCalculateParameter.TangoColor = "blackColor";
                        }
                        listderivedCalculateParameters.Add(derivedCalculateParameter);
                    }
                    parameter.derivedcalculatedtParameters = listderivedCalculateParameters;

                    sdr.NextResult();

                     derivedOutputParameter = null;
                    while (sdr.Read())
                    {
                        derivedOutputParameter = new DerivedOutputParameter();
                        derivedOutputParameter.SDocId = sdr["SDocID"].ToString();
                        derivedOutputParameter.EquivalentDia = sdr["EquivalentDiaOD"].ToString();
                        derivedOutputParameter.EquivalentDiaFace = sdr["EquivalentDiaForFace"].ToString();
                        derivedOutputParameter.CuttingEdgeDensity = sdr["CuttingEdgeDensity"].ToString();
                        derivedOutputParameter.SparkOutRevolutions = sdr["SparkOutRevolutions"].ToString();                        derivedOutputParameter.GrindingCycletime = sdr["GrindingCycleTime"].ToString();                        derivedOutputParameter.NongrindingCycleTime = sdr["NonGrindingCycleTime"].ToString();
                        derivedOutputParameter.TotalGrindingTime = sdr["TotalGrindingCycleTime"].ToString();
                        derivedOutputParameter.TotalCycletime = sdr["TotalCycleTime"].ToString();
                        derivedOutputParameter.FloorToFloor = sdr["FloorToFloor"].ToString();
                        listderivedCalculateParametersOutput.Add(derivedOutputParameter);
                    }
                    parameter.derivedCalculateParametersOutput = listderivedCalculateParametersOutput;
                    //list.Add(parameter);
                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("OutputModule - Bind Derived Parameter" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close(); if (sdr != null) sdr.Close();
            }
            return parameter;
        }
        internal static List<SdocImages> omBindImages(SdocPlungCat name)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<SdocImages> listSdocImage = new List<SdocImages>();
            SdocImages data = null;
            DataTable dt = new DataTable();
            List<string> DistSdoc = new List<string>();
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_OutPutModuleCalculatedParameter]", con);
                cmd.Parameters.AddWithValue("@SDocId", name.Sdocname);
                cmd.Parameters.AddWithValue("@PlungeID", name.Plunge);
                cmd.Parameters.AddWithValue("@SubCategoryID", name.Category);
                cmd.Parameters.AddWithValue("@Param", "IMAGES");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    dt.Load(sdr);
                    string tempSdocName = "";
                    foreach (DataRow item in dt.Rows)
                    {

                        if (item["SDOCid"].ToString() != tempSdocName)
                        {
                            DistSdoc.Add(item["SDOCid"].ToString());
                        }
                        tempSdocName = item["SDOCid"].ToString();
                    }
                    foreach (string Sdoc in DistSdoc)
                    {
                        data = new SdocImages();
                        WorkpieceImage imageDetail = null;
                        data.SdocName = Sdoc;
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            if (dataRow["SDOCid"].ToString() == Sdoc)
                            {
                                imageDetail = new WorkpieceImage();
                                if (dataRow["ImageFileName"].ToString().StartsWith("~/UploadImages"))
                                {
                                    imageDetail.wpImageName = dataRow["ImageName"].ToString();
                                    imageDetail.wpImagePath = dataRow["ImageFileName"].ToString();
                                    data.Values.Add(imageDetail);
                                }
                               
                            }
                        }
                        listSdocImage.Add(data);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("OutputModule- BindImages" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close(); if (sdr != null) sdr.Close();
            }
            return listSdocImage;
        }
        internal static List<QualityParam> omBindQualityParam(SdocPlungCat name, string selectedParam, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<QualityParam> listQlydetails = new List<QualityParam>();
            QualityParam data = null;
            DataTable dt = new DataTable();
            List<string> distSdoc = new List<string>();
            List<string> itemname = new List<string>();
            List<string> Sdocid = new List<string>();
            List<string> value = new List<string>();
            List<string> parameterName = new List<string>();
            List<string> subParameterName = new List<string>();
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_OutPutModuleCalculatedParameter]", con);
                cmd.Parameters.AddWithValue("@SDocId", name.Sdocname);
                cmd.Parameters.AddWithValue("@PlungeID", name.Plunge);
                cmd.Parameters.AddWithValue("@SubCategoryID", name.Category);
                cmd.Parameters.AddWithValue("@Parameter", selectedParam);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        distSdoc.Add(sdr["SDocId"].ToString());
                    }
                    sdr.NextResult();
                    while (sdr.Read())
                    {
                        itemname.Add(sdr["Customname"].ToString());
                        Sdocid.Add(sdr["SDocId"].ToString());
                        value.Add(sdr["Value"].ToString());
                        parameterName.Add(sdr["ParameterName"].ToString());
                        subParameterName.Add(sdr["SubParameterName"].ToString());
                    }
                    for (int i = 0; i < distSdoc.Count; i++)
                    {
                        List<string> tempSodc = new List<string>();
                        List<string> tempItemName = new List<string>();
                        List<string> tempValue = new List<string>();
                        List<string> tempParamName = new List<string>();
                        List<string> tempSubParamName = new List<string>();
                        data = new QualityParam();
                        DataInputModuleParameter details = null;
                        data.SdocName = distSdoc[i];
                        for (int j = 0; j < Sdocid.Count; j++)
                        {
                            if (distSdoc[i] == Sdocid[j])
                            {
                                tempSodc.Add(Sdocid[j]);
                                tempItemName.Add(itemname[j]);
                                tempValue.Add(value[j]);
                                tempParamName.Add(parameterName[j]);
                                tempSubParamName.Add(subParameterName[j]);
                            }
                        }
                        List<string> DistinctParameter = new List<string>();
                        string tempparam = "";
                        for (int d = 0; d < tempParamName.Count; d++)
                        {
                            if (tempParamName[d] != tempparam)
                            {
                                DistinctParameter.Add(tempParamName[d]);
                            }
                            tempparam = tempParamName[d];
                        }
                        for (int l = 0; l < DistinctParameter.Count; l++)
                        {

                            details = new DataInputModuleParameter();
                            // details.Prameter = DistinctParameter[l];
                            string custName = DistinctParameter[l];
                            for (var m = 0; m < tempSodc.Count; m++)
                            {
                                if (tempParamName[m] == DistinctParameter[l])
                                {
                                    if (tempSubParamName[m] == "Lower Target")
                                    {
                                        details.TargetLower = tempValue[m];
                                       
                                    }
                                    else if (tempSubParamName[m] == "Upper Target")
                                    {
                                        details.TargetUpper = tempValue[m];
                                    }
                                    else if (tempSubParamName[m] == "Lower Actual")
                                    {
                                        details.ActualLower = tempValue[m];
                                    }
                                    else if (tempSubParamName[m] == "Upper Actual")
                                    {
                                        details.ActualUpper = tempValue[m];
                                    }
                                    custName = tempItemName[m].Substring(0, tempItemName[m].LastIndexOf(";"));
                                }
                            }
                            details.Prameter = custName;
                            data.Values.Add(details);
                        }
                        listQlydetails.Add(data);
                    }

                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("OutputModule - Bind QualityParam" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close(); if (sdr != null) sdr.Close();
            }
            return listQlydetails;
        }
        internal static DataTable omBindInferenceSignal(SdocPlungCat name)        {            SqlConnection con = ConnectionManager.GetConnection();            SqlDataReader sdr = null;            SqlCommand cmd = null;            DataTable dt = new DataTable();            try            {                cmd = new SqlCommand("[dbo].[S_Get_OutPutModuleCalculatedParameter]", con);                cmd.Parameters.AddWithValue("@SDocId", name.Sdocname);                cmd.Parameters.AddWithValue("@PlungeID", name.Plunge);                cmd.Parameters.AddWithValue("@SubCategoryID", name.Category);                cmd.Parameters.AddWithValue("@Param", "SignalParm");                cmd.CommandType = CommandType.StoredProcedure;                sdr = cmd.ExecuteReader();                if (sdr.HasRows)                {                    dt.Load(sdr);
                    dt.Columns.Remove("ParameterID");
                }            }            catch (Exception ex)            {                Logger.WriteDebugLog("OutputModule - BindInferenceSignal"+ex.Message);            }            finally            {                if (con != null) con.Close(); if (sdr != null) sdr.Close();            }            return dt;        }
        internal static List<string> omBindQualityParameter(string id, string plunge, string subcat, string selectedParam, out List<string> DistinctSdoc)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            DistinctSdoc = new List<string>();
            List<string> details = new List<string>();
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_OutPutModuleCalculatedParameter]", con);
                cmd.Parameters.AddWithValue("@SDocId", id);
                cmd.Parameters.AddWithValue("@PlungeID", plunge);
                cmd.Parameters.AddWithValue("@SubCategoryID", subcat);
                cmd.Parameters.AddWithValue("@Parameter", selectedParam);
                cmd.Parameters.AddWithValue("@Param", "QualityParm");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        DistinctSdoc.Add(sdr["SDocId"].ToString());
                    }
                    sdr.NextResult();
                    while (sdr.Read())
                    {
                        details.Add(sdr["Customname"].ToString());
                        details.Add(sdr["SDocId"].ToString());
                        details.Add(sdr["Value"].ToString());
                        details.Add(sdr["ParameterName"].ToString());
                        details.Add(sdr["SubParameterName"].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("Output Module - Bind QualityParameter"+ex.Message);
            }
            finally
            {
                if (con != null) con.Close(); if (sdr != null) sdr.Close();
            }
            return details;
        }

        internal static List<string> omBindGrindingTime(string id, string plunge, string subcat)
        {

            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<string> list = new List<string>();
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_OutPutModuleCalculatedParameter]", con);
                cmd.Parameters.AddWithValue("@SDocId", id);
                cmd.Parameters.AddWithValue("@PlungeID", plunge);
                cmd.Parameters.AddWithValue("@SubCategoryID", subcat);
                cmd.Parameters.AddWithValue("@Param", "GrindingParm");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(sdr["Parameter"].ToString());
                        list.Add(sdr["Value"].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("Output Module - Bind GrindingTime"+ex.Message);
            }
            finally
            {
                if (con != null) con.Close(); if (sdr != null) sdr.Close();
            }
            return list;
        }



        internal static List<string> omBindNonGrindingTime(string id, string plunge, string subcat)
        {

            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<string> list = new List<string>();
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_OutPutModuleCalculatedParameter]", con);
                cmd.Parameters.AddWithValue("@SDocId", id);
                cmd.Parameters.AddWithValue("@PlungeID", plunge);
                cmd.Parameters.AddWithValue("@SubCategoryID", subcat);
                cmd.Parameters.AddWithValue("@Param", "NonGrindingParm");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(sdr["Parameter"].ToString());
                        list.Add(sdr["Value"].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("OutputModule - Bind NonGrindingTime"+ ex.Message);
            }
            finally
            {
                if (con != null) con.Close(); if (sdr != null) sdr.Close();
            }
            return list;
        }

        internal static List<string> omBindTotalCycleTime(string id, string plunge, string subcat)
        {

            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<string> list = new List<string>();
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_OutPutModuleCalculatedParameter]", con);
                cmd.Parameters.AddWithValue("@SDocId", id);
                cmd.Parameters.AddWithValue("@PlungeID", plunge);
                cmd.Parameters.AddWithValue("@SubCategoryID", subcat);
                cmd.Parameters.AddWithValue("@Param", "TotalGrindingParm");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        list.Add(sdr["Parameter"].ToString());
                        list.Add(sdr["Value"].ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("Output Module - Bind TotalCycleTime" +ex.Message);
            }
            finally
            {
                if (con != null) con.Close(); if (sdr != null) sdr.Close();
            }
            return list;
        }
  
        internal static List<string> omBindCalculatedParam(string Sdoc, string Plunge, string Subcat)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            List<string> calcParamDetails = new List<string>();
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_OutPutModuleCalculatedParameter]", con);
                cmd.Parameters.AddWithValue("@SDocId", Sdoc);
                cmd.Parameters.AddWithValue("@PlungeID", Plunge);
                cmd.Parameters.AddWithValue("@SubCategoryID", Subcat);
                cmd.Parameters.AddWithValue("@Param", "CalculatedParm");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    dt.Load(sdr);
                    DataTable dtCopy = dt.Copy();
                    HttpContext.Current.Session["CalcParamGraphData"] = dtCopy;
                    calcParamDetails.Add(dt.Rows.Count.ToString());
                    calcParamDetails.Add(dt.Columns.Count.ToString());
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        calcParamDetails.Add(dt.Columns[i].ColumnName);
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            calcParamDetails.Add(dt.Rows[j][i].ToString());
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("Output Module - Bind CalculatedParam"+ex.Message);
            }
            finally
            {
                if (con != null) con.Close(); if (sdr != null) sdr.Close();
            }
            return calcParamDetails;
        }
        #endregion

        #region Master Data---------------

        internal static List<string> GetAllInputModules()
        {
            List<string> values = new List<string>();
            string query = @"select distinct InputModule from ParameterListDetails";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    values.Add(rdr["InputModule"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Mater Data - GetAllInputModules" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }
            return values;
        }
        internal static List<string> GetSubInputModule(string input)
        {
            string query;

            List<string> values = new List<string>();
            //values.Add("");

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                if (input == "ALL")
                {
                    input = "";
                }
                cmd = new SqlCommand("select distinct SubInputModule from ParameterListDetails where InputModule=@input", conn);
                cmd.Parameters.AddWithValue("@input", input);
                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    values.Add(rdr["SubInputModule"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Mater Data - GetSubInputModule"+ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }
            return values;
        }
        internal static List<ParameterNameID> getParameterDetails(string input, string subinput)
        {
            SqlConnection conn = ConnectionManager.GetConnection();

            List<ParameterNameID> list = new List<ParameterNameID>();
            ParameterNameID data = null;
            SqlDataReader rdr = null;
            try
            {
                SqlCommand cmd = null;
                if (input == "ALL")
                {
                    input = "";
                }
                cmd = new SqlCommand("select distinct  ParameterID,Parameter from ParameterListDetails where InputModule=@input and SubInputModule=@subinput", conn);
                cmd.Parameters.AddWithValue("@input", input);
                cmd.Parameters.AddWithValue("@subinput", subinput);
                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    data = new ParameterNameID();
                    data.ParameterID = rdr["ParameterID"].ToString();
                    data.Parameter = rdr["Parameter"].ToString();
                    list.Add(data);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Mater Data -  getParameterDetails"+ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }
            return list;
        }

        internal static string getDatatype(string paramid)
        {
            SqlConnection conn = ConnectionManager.GetConnection();

            string datatype = "Varchar";
            SqlDataReader rdr = null;
            try
            {
                SqlCommand cmd = null;
              
                cmd = new SqlCommand("select DataType from InputModuleParameterDetails where ParameterID=@paramid", conn);
                cmd.Parameters.AddWithValue("@paramid", paramid);
                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                   datatype = rdr["DataType"].ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Mater Data -  getParameterDetails" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }
            return datatype;
        }
        internal static bool DeleteParameterListValue(string parameter, string value)
        {
            bool success = false;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string query = @"[S_Get_ParameterListSaveUpdateDelete]";
            int parameterid = Convert.ToInt32(parameter);
            SqlDataReader sdr = null;
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", "Delete");
                cmd.Parameters.AddWithValue("@Value", value);
                cmd.Parameters.AddWithValue("@ParameterID", parameterid);
                cmd.Parameters.AddWithValue("@id", 0);
                cmd.Parameters.AddWithValue("@InputModule", "");
                cmd.Parameters.AddWithValue("@SubInputModule", "");
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (sdr["DeleteFlag"].ToString() == "True")
                        {
                            success = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Mater Data - Delete Parameter Listvalue"+ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return success;
        }
        internal static List<string> GetAllSubInputModules_MasterData(string ipModule)
        {
            List<string> values = new List<string>();
            string query = @"select distinct SubInputModule from InputModuleParameterDetails where  ISNULL(SubInputModule,'')<>'' and InputModule=@InputModule";
            //string query = "[dbo].[S_Get_InputParameterInsertUpdate]";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                cmd = new SqlCommand(query, conn);
                //  cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InputModule", ipModule);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!rdr["SubInputModule"].ToString().Equals(string.Empty))
                        values.Add(rdr["SubInputModule"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }
            return values;
        }
        internal static List<ParameterNameID> GetAllParameters_MasterData(string ipModule, string sipModule)
        {

            List<ParameterNameID> list = new List<ParameterNameID>();
            ParameterNameID data = null;
            string query = @"select distinct Parameter,ParameterID,DataType from InputModuleParameterDetails where InputModule = @InputModule and SubInputModule = @SubInputModule and Entry_Type='Drop Down'";
            //  string query = "[dbo].[S_Get_InputParameterInsertUpdate]";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                cmd = new SqlCommand(query, conn);
                //cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InputModule", ipModule);
                cmd.Parameters.AddWithValue("@SubInputModule", sipModule);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    data = new ParameterNameID();
                    data.Parameter = rdr["Parameter"].ToString();
                    data.ParameterID = rdr["ParameterID"].ToString()+";"+ rdr["DataType"].ToString();
                    list.Add(data);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (rdr != null) rdr.Close();
            }
            return list;
        }






        ///////////////////////////


        internal static List<string> GetAllNewInputModules()
        {
            List<string> values = new List<string>();
            string query = @"select distinct InputModule from InputModuleParameterDetails";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    values.Add(rdr["InputModule"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Mater Data - GetAllNewInputModules"+ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return values;
        }
        internal static DataTable GetParameterListValue(string inputModuleValue, string subInputModuleValue, string parameterValue)
        {
            DataTable dt = new DataTable();
            string query = "[S_Get_ParameterListSaveUpdateDelete]";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            int parameterID;
            if (parameterValue == "")
            {
                parameterID = 0;
            }
            else
            {
                parameterID = Convert.ToInt32(parameterValue);
            }
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", "View");
                cmd.Parameters.AddWithValue("@InputModule", inputModuleValue);
                cmd.Parameters.AddWithValue("@SubInputModule", subInputModuleValue);
                cmd.Parameters.AddWithValue("@ParameterID", parameterID);
                cmd.Parameters.AddWithValue("@id", 0);
                cmd.Parameters.AddWithValue("@columnName", "");
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    dt.Load(rdr);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Mater Data - GetParameterListValue"+ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return dt;
        }




        internal static List<string> GetAllSipModules()
        {
            List<string> val = new List<string>();
            val.Add("");
            return val;
        }

        internal static List<string> GetAllMasterParameters()
        {
            List<string> val = new List<string>();
            val.Add("");
            return val;
        }

        internal static bool DeleteInputModuleData(int id)
        {
            bool success = false;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string query = @"delete from InputModuleDetails where Id=@id";
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                int val = cmd.ExecuteNonQuery();
                if (val > 0)
                    success = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return success;
        }

        internal static bool SaveInputModuleData(string inputModule, string subInputModule)
        {
            bool success = false;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string query = @"insert into InputModuleDetails(InputModule,SubInputModule) values (@iModule,@sModule)";
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@iModule", inputModule);
                cmd.Parameters.AddWithValue("@sModule", subInputModule);
                int val = cmd.ExecuteNonQuery();
                if (val > 0)
                    success = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return success;
        }
        internal static List<string> GetAllSubInputModules(string ipModule)
        {
            List<string> values = new List<string>();
            string query = @"select distinct SubInputModule from ParameterListDetails where InputModule=@inputMod";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@inputMod", ipModule);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!rdr["SubInputModule"].ToString().Equals(string.Empty))
                        values.Add(rdr["SubInputModule"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return values;
        }

        internal static List<string> GetAllParameters(string ipModule, string sipModule)
        {

            List<string> values = new List<string>();
            string query = @"select distinct Parameter from ParameterListDetails where InputModule = @inputMod and SubInputModule = @subIM";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@inputMod", ipModule);
                cmd.Parameters.AddWithValue("@subIM", sipModule);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    values.Add(rdr["Parameter"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return values;
        }

        internal static bool InsertParameterListValue(string ipModule, string sipModule, string param, string lstValue, string paramid, string adminid)
        {
            bool success = false;
            int parameterid = 0;
            if (paramid != "")
            {
                parameterid = Convert.ToInt32(paramid);
            }
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string query = @"[S_Get_ParameterListSaveUpdateDelete]";
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", "Save");
                cmd.Parameters.AddWithValue("@InputModule", ipModule);
                cmd.Parameters.AddWithValue("@SubInputModule", sipModule);
                cmd.Parameters.AddWithValue("@parameter", param);
                cmd.Parameters.AddWithValue("@ParameterID", parameterid);
                cmd.Parameters.AddWithValue("@Value", lstValue);
                cmd.Parameters.AddWithValue("@id", 0);
                cmd.Parameters.AddWithValue("@columnName", "");
                cmd.Parameters.AddWithValue("@UserId", adminid);
                int val = cmd.ExecuteNonQuery();
                if (val > 0)
                    success = true;
            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return success;
        }

        internal static bool UpdateParameterListValue(int id, string listValue, string adminid)
        {
            bool success = false;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string query = @"[S_Get_ParameterListSaveUpdateDelete]";
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", "Update");
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@InputModule", "");
                cmd.Parameters.AddWithValue("@SubInputModule", "");
                cmd.Parameters.AddWithValue("@Value", listValue);
                cmd.Parameters.AddWithValue("@columnName", "");
                cmd.Parameters.AddWithValue("@UserId", adminid);
                int val = cmd.ExecuteNonQuery();
                if (val > 0)
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return success;
        }


        #endregion

        #region ------------Input Module Master------------
        internal static DataTable GetInputModuleDetails()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string query = @"[S_Get_InputModuleInsertUpdate]";
            SqlDataReader rdr = null;
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InputModule", "");
                cmd.Parameters.AddWithValue("@SubInputModule", "");
                cmd.Parameters.AddWithValue("@Param", "");
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    dt.Load(rdr);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return dt;
        }

        internal static int UpdateInputModuleRename(string inputId, string rename,string sortorder, string param)
        {

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            List<string> deleteSDoc = new List<string>();
            int success = 0;
            try
            {
                cmd = new SqlCommand("[S_Get_InputModuleInsertUpdate]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InputmoduleId", inputId);
                cmd.Parameters.AddWithValue("@Rename", rename);
                cmd.Parameters.AddWithValue("@SortOrder", sortorder);
                cmd.Parameters.AddWithValue("@Param", param);
               success= cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Whil rename Input module tab" + ex.Message);
                success = 0;
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return success;
        }
        #endregion





        #region ----------------------Parameter Master----------------

        internal static List<string> GetAllParameters_ParameterMaster(string ipModule, string sipModule)
        {

            List<string> values = new List<string>();
            //  string query = @"select distinct Parameter from InputModuleParameterDetails where InputModule = @inputMod and SubInputModule = @subIM";
            string query = "[dbo].[S_Get_InputParameterInsertUpdate]";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@InputModule", ipModule);
                cmd.Parameters.AddWithValue("@SubInputModule", sipModule);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    values.Add(rdr["Parameter"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return values;
        }

        internal static List<string> GetAllSubInputModules_ParameterMaster(string ipModule)
        {
            List<string> values = new List<string>();
            //string query = @"select distinct SubInputModule from InputModuleParameterDetails where InputModule=@inputMod";
            //string query = "[dbo].[S_Get_InputParameterInsertUpdate]";
            string query = @"select distinct SubInputModule from InputModuleParameterDetails where  ISNULL(SubInputModule,'')<>'' and InputModule=@InputModule";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@InputModule", ipModule);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (!rdr["SubInputModule"].ToString().Equals(string.Empty))
                        values.Add(rdr["SubInputModule"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return values;
        }
        internal static List<string> GetAllInputModules_ParameterMaster()
        {
            List<string> values = new List<string>();
            string query = @"select distinct InputModule from InputModuleParameterDetails";
            //string query = "[dbo].[S_Get_InputParameterInsertUpdate]";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    values.Add(rdr["InputModule"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return values;
        }

        internal static List<ParameterDetails> GetParameters(string inputModuleValue, string subInputModuleValue, string parameterValue)
        {
            DataTable dt = new DataTable();
            string query = "[dbo].[S_Get_InputParameterInsertUpdate]";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;

            List<ParameterDetails> listParameter = new List<ParameterDetails>();
            ParameterDetails parameter = null;
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", "");
                cmd.Parameters.AddWithValue("@InputModule", inputModuleValue);
                cmd.Parameters.AddWithValue("@SubInputModule", subInputModuleValue);
                cmd.Parameters.AddWithValue("@parameter", parameterValue);

                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        parameter = new ParameterDetails();
                        parameter.Id = rdr["ParameterID"].ToString();
                        parameter.InputModule = rdr["InputModule"].ToString();
                        parameter.SubInputModule = rdr["SubInputModule"].ToString();
                        parameter.Parameter = rdr["Parameter"].ToString();
                        parameter.LSL = rdr["LSL"].ToString();
                        parameter.USL = rdr["USL"].ToString();
                        parameter.Reccomandation = rdr["Recommendation"].ToString();
                        if (rdr["EnableFlag"] == DBNull.Value)
                        {
                            parameter.Enableflag = false;

                        }
                        else
                        {
                            parameter.Enableflag = Convert.ToBoolean(rdr["EnableFlag"].ToString());
                        }

                        parameter.IsNumeric = rdr["ISNumeric"].ToString();
                        parameter.SortOrder = rdr["SortOrder"].ToString();
                        if (Convert.ToInt32(parameter.IsNumeric) != 1)
                        {
                            parameter.LSL = "NA";
                            parameter.USL = "NA";
                        }

                        if (rdr["LimitImage"].ToString() == "")
                        {
                            parameter.Image = "";
                        }
                        else
                        {
                            byte[] bytes = (byte[])rdr["LimitImage"];
                            parameter.ImageLimit = bytes;
                            string strBase64 = Convert.ToBase64String(bytes);
                            parameter.Image = "data:Image/png;base64," + strBase64;
                        }

                        if (rdr["DefaultParameterFlag"] == DBNull.Value)
                        {
                            parameter.DefaultParam = false;

                        }
                        else
                        {
                            parameter.DefaultParam = Convert.ToBoolean(rdr["DefaultParameterFlag"].ToString());
                        }
                        parameter.EntryType = rdr["Entry_Type"].ToString();
                        parameter.DataType= rdr["DataType"].ToString();
                        parameter.Deletableflag = rdr["DeletableFlag"].ToString();
                        parameter.Mandatoryflag = rdr["MandatoryFlag"].ToString();
                        
                        if (rdr["DependencyFlag"] == DBNull.Value)
                        {
                            parameter.Dependencyflag = false;
                        }
                        else
                        {
                            parameter.Dependencyflag = Convert.ToBoolean(rdr["DependencyFlag"].ToString());
                        }
                        parameter.IndependentParameter = rdr["IndependentParameter"].ToString();
                        parameter.Customname = rdr["Customname"].ToString();
                        listParameter.Add(parameter);
                    }
                    //  dt.Load(rdr);
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("GetParameters"+ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return listParameter;
        }

        internal static bool UpdateParametersLimitImage(ParameterDetails parameter)
        {
            bool success = false;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string query = @"[S_Get_InputParameterInsertUpdate]";
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", "Update");
                cmd.Parameters.AddWithValue("@InputModule", parameter.InputModule);
                cmd.Parameters.AddWithValue("@SubInputModule", parameter.SubInputModule);
                cmd.Parameters.AddWithValue("@parameter", parameter.Parameter);
                cmd.Parameters.AddWithValue("@Recommendation", parameter.Reccomandation);
                // cmd.Parameters.AddWithValue("@Recommendation", parameter.AdminName);
                if (parameter.LSL == "" || parameter.LSL == "NA")
                {
                    cmd.Parameters.AddWithValue("@LSL", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LSL", parameter.LSL);
                }
                if (parameter.USL == "" || parameter.USL == "NA")
                {
                    cmd.Parameters.AddWithValue("@USL", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@USL", parameter.USL);
                }

                cmd.Parameters.AddWithValue("@EnableFlag", parameter.Enableflag);
                cmd.Parameters.AddWithValue("@SortOrder", parameter.SortOrder);
                cmd.Parameters.AddWithValue("@LimitImage", System.Data.SqlTypes.SqlBinary.Null);

                int val = cmd.ExecuteNonQuery();
                if (val > 0)
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Parameter Master - UpdateParametersLimitImage" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return success;
        }

       

        internal static bool UpdateParameters(ParameterDetails parameter,string param)
        {
            bool success = false;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string query = @"[S_Get_InputParameterInsertUpdate]";
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", "Update");
                cmd.Parameters.AddWithValue("@InputModule", parameter.InputModule);
                cmd.Parameters.AddWithValue("@SubInputModule", parameter.SubInputModule);
                cmd.Parameters.AddWithValue("@parameter", parameter.Parameter);
                cmd.Parameters.AddWithValue("@Recommendation", parameter.Reccomandation);
                cmd.Parameters.AddWithValue("@Customname", parameter.Customname);
                // cmd.Parameters.AddWithValue("@Recommendation", parameter.AdminName);
                if (parameter.LSL == "" || parameter.LSL == "NA")
                {
                    cmd.Parameters.AddWithValue("@LSL", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LSL", parameter.LSL);
                }
                if (parameter.USL == "" || parameter.USL == "NA")
                {
                    cmd.Parameters.AddWithValue("@USL", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@USL", parameter.USL);
                }

                cmd.Parameters.AddWithValue("@EnableFlag", parameter.Enableflag);
                cmd.Parameters.AddWithValue("@SortOrder", parameter.SortOrder);
                cmd.Parameters.AddWithValue("@DefaultParameterFlag", parameter.DefaultParam);
                if (param == "UpdateLimitImage")
                {
                    cmd.Parameters.AddWithValue("@LimitImage", System.Data.SqlTypes.SqlBinary.Null);
                }
                else
                {
                    if (parameter.ImageLimit != null)
                    {
                        cmd.Parameters.AddWithValue("@LimitImage", parameter.ImageLimit);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@LimitImage", System.Data.SqlTypes.SqlBinary.Null);
                    }
                }


                cmd.Parameters.AddWithValue("@MandatoryFlag", parameter.Mandatoryflag);
                cmd.Parameters.AddWithValue("@DependencyFlag", parameter.Dependencyflag);
                cmd.Parameters.AddWithValue("@IndependentParameter", parameter.IndependentParameter);
                int val = cmd.ExecuteNonQuery();
                if (val > 0)
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Parameter Master - UpdateParameters" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return success;
        }

        internal static string DeleteParameter(string parameterName, string parameterID, string inputModule)
        {
            string success = "";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            string query = @"[S_Get_InputParameterInsertUpdate]";
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", "Delete");
                cmd.Parameters.AddWithValue("@parameter", parameterName);
                cmd.Parameters.AddWithValue("@ParameterID", parameterID);
                cmd.Parameters.AddWithValue("@InputModule", inputModule);
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        success = sdr["DeleteFlag"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Parameter Master - Deleting Parameter" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
                if (sdr != null) sdr.Close();
            }
            return success;
        }

        internal static bool removeLimitImage(string paramname, string paramid)
        {
            bool success = false;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string query = @"update InputModuleParameterDetails set LimitImage=@LimitImage where Parameter=@parameter";
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@parameter", paramname);
                cmd.Parameters.AddWithValue("@LimitImage", System.Data.SqlTypes.SqlBinary.Null);
                int val = cmd.ExecuteNonQuery();
                if (val > 0)
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Parameter Master - removeImage" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return success;
        }

        internal static bool SaveNewParameters(ParameterDetails parameter)
        {
            bool success = false;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            string query = @"[S_Get_InputParameterInsertUpdate]";
            try
            {
                cmd = new SqlCommand(query, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", "Save");
                cmd.Parameters.AddWithValue("@InputModule", parameter.InputModule);
                cmd.Parameters.AddWithValue("@SubInputModule", parameter.SubInputModule);
                cmd.Parameters.AddWithValue("@parameter", parameter.Parameter);
               // cmd.Parameters.AddWithValue("@Customname", parameter.Parameter);
                cmd.Parameters.AddWithValue("@Recommendation", parameter.Reccomandation);
                // cmd.Parameters.AddWithValue("@Recommendation", parameter.AdminName);
                cmd.Parameters.AddWithValue("@Customname", parameter.Customname==""?parameter.Parameter: parameter.Customname);
                if (parameter.LSL == "" || parameter.LSL == "NA")
                {
                    cmd.Parameters.AddWithValue("@LSL", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LSL", parameter.LSL);
                }
                if (parameter.USL == "" || parameter.USL == "NA")
                {
                    cmd.Parameters.AddWithValue("@USL", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@USL", parameter.USL);
                }

                cmd.Parameters.AddWithValue("@EnableFlag", parameter.Enableflag);
                cmd.Parameters.AddWithValue("@SortOrder", parameter.SortOrder);
                cmd.Parameters.AddWithValue("@DefaultParameterFlag", parameter.DefaultParam);
                if (parameter.ImageLimit != null)
                {
                    cmd.Parameters.AddWithValue("@LimitImage", parameter.ImageLimit);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@LimitImage", System.Data.SqlTypes.SqlBinary.Null);
                }
                cmd.Parameters.AddWithValue("@User", parameter.AdminName);
                cmd.Parameters.AddWithValue("@Entry_Type", parameter.EntryType);
                cmd.Parameters.AddWithValue("@DataType", parameter.DataType);
                cmd.Parameters.AddWithValue("@DependencyFlag", parameter.Dependencyflag);
                cmd.Parameters.AddWithValue("@IndependentParameter", parameter.IndependentParameter);
                cmd.Parameters.AddWithValue("@MandatoryFlag", parameter.Mandatoryflag);
                int val = cmd.ExecuteNonQuery();
                if (val > 0)
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Parameter Master - Save new parameters"+ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return success;
        }


        //internal static List<string> GetAllSubInputModules(string inputModule)
        //{
        //    List<string> values = new List<string>();

        //    SqlConnection conn = ConnectionManager.GetConnection();
        //    SqlCommand cmd = null;
        //    SqlDataReader rdr = null;
        //    try
        //    {
        //        cmd = new SqlCommand("[dbo].[S_Get_InputParameterInsertUpdate]", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Parameters.AddWithValue("@InputModule", inputModule);
        //        // cmd.Parameters.AddWithValue("@subIM", sipModule);
        //        rdr = cmd.ExecuteReader();
        //        while (rdr.Read())
        //        {
        //            values.Add(rdr["Parameter"].ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteErrorLog(ex.Message);
        //    }
        //    finally
        //    {
        //        if (conn != null) conn.Close();
        //    }
        //    return values;
        //}
        #endregion


        #region ---- DElete SDoc---
        internal static List<string> getSDocForDelete(string param,string param1)
        {
            
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            List<string> deleteSDoc = new List<string>();
            try
            {
                cmd = new SqlCommand("[S_Get_SystemDocHistoryDetails]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.Parameters.AddWithValue("@Param1", param1);
                rdr = cmd.ExecuteReader();
                if(rdr.HasRows)
                {
                    while(rdr.Read())
                    {
                        deleteSDoc.Add(rdr["SdocID"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("While getting SDocList for delete" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return deleteSDoc;
        }


        internal static string getRoleOfEmp(string empname)
        {

            string adminData = string.Empty;
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader sdr = null;
            string sqlQuery = string.Empty;
            try
            {
                sqlQuery = "select Role from Employee_Information where Employeeid=@employeename";
                cmd = new SqlCommand(sqlQuery, conn);
                cmd.Parameters.AddWithValue("@employeename", empname);
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    sdr.Read();
                    {
                        adminData = sdr["Role"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorSignal.FromCurrentContext().Raise(ex);//(ex.ToString());
                throw;
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return adminData;
        }
        internal static int DeleteSDoc(string SDoc, string User, string param)
        {

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            List<string> deleteSDoc = new List<string>();
            int success = 0;
            try
            {
                cmd = new SqlCommand("[S_Get_SystemDocHistoryDetails]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SDocId", "'"+ SDoc + "'");
                cmd.Parameters.AddWithValue("@User", User);
                cmd.Parameters.AddWithValue("@Param", param);
               success= cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Whil delete or restore SDoc" + ex.Message);
                success = 0;
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return success;
        }

        internal static List<DeletedSDocDetails> getDeletedSDocDetails(string SDocid, string adminname, string param)
        {

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            List<DeletedSDocDetails> listDeletedSdocDetails = new List<DeletedSDocDetails>();
            DeletedSDocDetails deletedSDocDetails = null;
            try
            {
                cmd = new SqlCommand("[S_Get_SystemDocHistoryDetails]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SDocId", SDocid);
                cmd.Parameters.AddWithValue("@User", adminname);
                cmd.Parameters.AddWithValue("@Param", param);

                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        deletedSDocDetails = new DeletedSDocDetails();
                        deletedSDocDetails.InputModule = rdr["InputModule"].ToString();
                        deletedSDocDetails.SubInputModule = rdr["SubInputModule"].ToString();
                        deletedSDocDetails.Parameter = rdr["Parameter"].ToString();
                        deletedSDocDetails.SDocId = rdr["SDocId"].ToString();
                        deletedSDocDetails.Value = rdr["Value"].ToString();
                        deletedSDocDetails.DeletedBy = rdr["DeletedBy"].ToString();
                        deletedSDocDetails.DeletedDate = rdr["DeleteDate"].ToString();
                        listDeletedSdocDetails.Add(deletedSDocDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("While getting deleted SDoc details" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return listDeletedSdocDetails;
        }
        #endregion

        #region  -----   Unlock SDocId-------------
        internal static List<string> getSDocForUnlock(string param)
        {

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            List<string> listLockedSDoc = new List<string>();
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_SystemDocStatusSaveView]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", param);
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listLockedSDoc.Add(rdr["SdocID"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("While getting SDocList for Unlock" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return listLockedSDoc;
        }

        internal static List<DeletedSDocDetails> getLockedSDocDetails(string SDocid, string adminname, string param)
        {

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            List<DeletedSDocDetails> listLockedSdocDetails = new List<DeletedSDocDetails>();
            DeletedSDocDetails lockedSDocDetails = null;
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_SystemDocStatusSaveView]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SDocId", SDocid);
                cmd.Parameters.AddWithValue("@User", adminname);
                cmd.Parameters.AddWithValue("@Param", param);

                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        lockedSDocDetails = new DeletedSDocDetails();
                        lockedSDocDetails.InputModule = rdr["InputModule"].ToString();
                        lockedSDocDetails.SubInputModule = rdr["SubInputModule"].ToString();
                        lockedSDocDetails.Parameter = rdr["Parameter"].ToString();
                        lockedSDocDetails.SDocId = rdr["SDocId"].ToString();
                        lockedSDocDetails.Value = rdr["Value"].ToString();
                        lockedSDocDetails.DeletedBy = rdr["UpdatedBy"].ToString();
                        lockedSDocDetails.DeletedDate = rdr["UpdatedTS"].ToString();
                        listLockedSdocDetails.Add(lockedSDocDetails);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("While getting deleted SDoc details" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return listLockedSdocDetails;
        }
        internal static int UnlockSDoc(string SDoc, string User, string param)
        {

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            int success = 0;
            List<string> deleteSDoc = new List<string>();
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_SystemDocStatusSaveView]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SDocId",SDoc);
                cmd.Parameters.AddWithValue("@User", User);
                cmd.Parameters.AddWithValue("@Status", "UnLocked");
                cmd.Parameters.AddWithValue("@Param", param);
                success=cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("While Unlocked the SDocID" + ex.Message);
                success = 0;
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return success;
        }
        #endregion


        #region -------Depemdency Master
        internal static List<ParameterDependency> getParameter1List(string param1, string param2)
        {

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            List<ParameterDependency> listDependentParameter = new List<ParameterDependency>();
            ParameterDependency parameterDependency = null;
            try
            {
                cmd = new SqlCommand("[S_Get_InputParameterLinkDetailsSaveAndView]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param1", param1);
                cmd.Parameters.AddWithValue("@Param2", param2);
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        parameterDependency = new ParameterDependency();
                        parameterDependency.InputModule = rdr["InputModule"].ToString();
                        parameterDependency.Parameter1 = rdr["Parameter"].ToString();
                        listDependentParameter.Add(parameterDependency);
                        // listDependentParameter.Add(rdr["Parameter"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Depemdency Master - While getting ParamterList for dependency master" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return listDependentParameter;
        }
        internal static int saveParameterDependency(string param1, string param2, string inpuModule)
        {

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            List<string> deleteSDoc = new List<string>();
            int success = 0;
            try
            {
                cmd = new SqlCommand("[S_Get_InputParameterLinkDetailsSaveAndView]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Parameter1", param1);
                cmd.Parameters.AddWithValue("@Parameter2", param2);
                cmd.Parameters.AddWithValue("@InputputModule1", inpuModule);
                cmd.Parameters.AddWithValue("@InputputModule2", inpuModule);
                cmd.Parameters.AddWithValue("@Param1", "Master");
                cmd.Parameters.AddWithValue("@Param2", "Save");
                success=cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Depemdency Master - While saving Parameter Dependency" +   "" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return success;
        }
        internal static List<ParameterDependency> getDependencyParameter()
        {

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            List<ParameterDependency> listParameterDependencies = new List<ParameterDependency>();
            ParameterDependency parameterDependency = null;
            try
            {
                cmd = new SqlCommand("[S_Get_InputParameterLinkDetailsSaveAndView]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param1", "Master");
                cmd.Parameters.AddWithValue("@Param2", "View");
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        parameterDependency = new ParameterDependency();
                        parameterDependency.ParameterId1 = rdr["ParameterID1"].ToString();
                        parameterDependency.Parameter1 = rdr["Parameter1"].ToString();
                        parameterDependency.ParameterId1 = rdr["ParameterID2"].ToString();
                        parameterDependency.Parameter2 = rdr["Parameter2"].ToString();
                        parameterDependency.InputModule = rdr["InputputModule1"].ToString();
                        listParameterDependencies.Add(parameterDependency);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Depemdency Master - While getting Paramter dependency list" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return listParameterDependencies;
        }
        #endregion


        #region ------Dependency Transaction---
        internal static List<string> TgetParameter1List(string param1, string param2, out List<ParameterDependency> listparameterDependency)
        {

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            List<string> listParam1 = new List<string>();
            listparameterDependency = new List<ParameterDependency>();
            ParameterDependency parameterDependency = null;
            try
            {
                cmd = new SqlCommand("[S_Get_InputParameterLinkDetailsSaveAndView]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param1", param1);
                cmd.Parameters.AddWithValue("@Param2", param2);
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        listParam1.Add(rdr["Parameter1"].ToString());
                        parameterDependency = new ParameterDependency();
                        parameterDependency.Parameter1 = rdr["Parameter1"].ToString();
                        parameterDependency.Parameter2 = rdr["Parameter2"].ToString();
                        listparameterDependency.Add(parameterDependency);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Dependency Transaction - TgetParameter1List" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return listParam1;
        }

        internal static List<ParameterDependency> getParameterDependencyValues(string param1, out List<string> param2ddlList)
        {

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            param2ddlList = new List<string>();
            List<ParameterDependency> listparameterDependency = new List<ParameterDependency>();
            ParameterDependency parameterDependency = null;
            try
            {
                cmd = new SqlCommand("[S_Get_InputParameterLinkDetailsSaveAndView]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Parameter1", param1);
                cmd.Parameters.AddWithValue("@Param1", "Detail");
                cmd.Parameters.AddWithValue("@Param2", "View");
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        param2ddlList.Add(rdr["ListValue"].ToString());
                    }
                    rdr.NextResult();
                    while(rdr.Read())
                    {
                        parameterDependency = new ParameterDependency();
                        parameterDependency.Parameter1 = rdr["Parameter"].ToString();
                        parameterDependency.Parameter1Value= rdr["ListValue"].ToString();
                        parameterDependency.Parameter2 = rdr["Parameter2"].ToString();
                        parameterDependency.Parameter2Value = rdr["Parametervalue2"].ToString();
                        listparameterDependency.Add(parameterDependency);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Dependency Transaction -  getParameterDependencyValues" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return listparameterDependency;
        }

        internal static int saveParameterDependencyValue(ParameterDependency parameterDependency)
        {

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            List<string> deleteSDoc = new List<string>();
            int success = 0;
            try
            {
                cmd = new SqlCommand("[S_Get_InputParameterLinkDetailsSaveAndView]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Parameter1", parameterDependency.Parameter1);
                cmd.Parameters.AddWithValue("@Parameter2", parameterDependency.Parameter2);
                cmd.Parameters.AddWithValue("@ParameterValue1", parameterDependency.Parameter1Value);
                cmd.Parameters.AddWithValue("@ParameterValue2", parameterDependency.Parameter2Value);
                cmd.Parameters.AddWithValue("@Param1", "Detail");
                cmd.Parameters.AddWithValue("@Param2", "Save");
                success=cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("Dependency Transaction -  saveParameterDependencyValue" +
                    "" + ex.Message);
                success = 0;
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return success;

        }
        #endregion

        #region --------RoleAssignRights-----

        internal static int InsertUpdateRoleAccessRight(string role,string page,int visibility)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<EmployeeDetails> listemp = new List<EmployeeDetails>();
            EmployeeDetails emp = null;
            int success = 0;
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_RoleAccessRightsInsertUpdate]", con);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@PageName", page);
                cmd.Parameters.AddWithValue("@Visibility", visibility);
                cmd.CommandType = CommandType.StoredProcedure;
                success = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("PRIMARY KEY"))
                {
                    success = -2;
                }
                else
                {
                    Logger.WriteErrorLog("Error while Insert Update the Page to Normal User - " + ex.Message);
                    success = 0;
                }
            }
            finally
            {
                if (con != null) con.Close();
                if (sdr != null) sdr.Close();
            }
            return success;
        }

        internal static List<RoleAccessRight> GetRoleAccessRight(string empid)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader rdr = null;
            SqlCommand cmd = null;
            List<RoleAccessRight> listRoleAccess = new List<RoleAccessRight>();
            RoleAccessRight roleAccess = null;
            int success = 0;
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_RoleAccessRightsInsertUpdate]", con);
                cmd.Parameters.AddWithValue("@Role", empid);
                cmd.Parameters.AddWithValue("@param", "View");
                cmd.CommandType = CommandType.StoredProcedure;
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        roleAccess = new RoleAccessRight();
                        roleAccess.Role = rdr["Role"].ToString();
                        roleAccess.Page = rdr["PageName"].ToString();
                        listRoleAccess.Add(roleAccess);
                    }
                }
            }
            catch (Exception ex)
            {
                //if (ex.Message.Contains("PRIMARY KEY"))
                //{
                //    success = -2;
                //}
                //else
                //{
                   Logger.WriteErrorLog("RoleAssignRights- Error while Get RoleAccessRight - " + ex.Message);
                //    success = 0;
                //}
            }
            finally
            {
                if (con != null) con.Close();
                if (rdr != null) rdr.Close();
            }
            return listRoleAccess;
        }
        internal static List<RoleAccessRight> GetPageListForRole(string role,out string empname)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader rdr = null;
            SqlCommand cmd = null;
            List<RoleAccessRight> listPage = new List<RoleAccessRight>();
            RoleAccessRight roleAccess = null;
             empname = "";
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_RoleAccessRightsInsertUpdate]", con);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@param", "PageList");
                cmd.CommandType = CommandType.StoredProcedure;
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        roleAccess = new RoleAccessRight();
                        roleAccess.Page = rdr["PageName"].ToString();
                        roleAccess.visibilty = true;
                        listPage.Add(roleAccess);
                    }
                }
                rdr.NextResult();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        empname = rdr["Name"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                //if (ex.Message.Contains("PRIMARY KEY"))
                //{
                //    success = -2;
                //}
                //else
                //{
                Logger.WriteErrorLog("RoleAssignRights - GetPageListForRole- " + ex.Message);
                //    success = 0;
                //}
            }
            finally
            {
                if (con != null) con.Close();
                if (rdr != null) rdr.Close();
            }
            return listPage;
        }

        internal static List<string> GetEmployeeID()
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader rdr = null;
            SqlCommand cmd = null;
            List<string> listEmpId = new List<string>();
            RoleAccessRight empid = null;
            int success = 0;
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_RoleAccessRightsInsertUpdate]", con);
                cmd.Parameters.AddWithValue("@param", "EmpList");
                cmd.CommandType = CommandType.StoredProcedure;
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        //empid = new RoleAccessRight();
                        //empid.EmpId = rdr["Employeeid"].ToString();
                        //empid.EmpName= rdr["Name"].ToString();
                        listEmpId.Add(rdr["Employeeid"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                //if (ex.Message.Contains("PRIMARY KEY"))
                //{
                //    success = -2;
                //}
                //else
                //{
                Logger.WriteErrorLog("RoleAssignRights - GetEmployeeID - " + ex.Message);
                //    success = 0;
                //}
            }
            finally
            {
                if (con != null) con.Close();
                if (rdr != null) rdr.Close();
            }
            return listEmpId;
        }
        #endregion


        #region ------ParameterRelationship------

        internal static List<ParameterDependency> getParameterRelationList(string dependentparam, string dependentparamID, string independentparam)
        {

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            List<ParameterDependency> listParameterDependencies = new List<ParameterDependency>();
            ParameterDependency parameterDependency = null;
            try
            {
                cmd = new SqlCommand("[S_Get_ParameterDependencySaveView]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", "View");
                cmd.Parameters.AddWithValue("@ParameterID1", dependentparamID);
                cmd.Parameters.AddWithValue("@Parameter1", dependentparam);
                cmd.Parameters.AddWithValue("@Parameter2", independentparam);
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        parameterDependency = new ParameterDependency();
                        parameterDependency.Parameter1 = rdr["Parameter1"].ToString();
                        parameterDependency.ParameterId1 = rdr["ParameterID1"].ToString();
                        parameterDependency.Parameter2 = rdr["Parameter2"].ToString();
                        parameterDependency.ParameterId2 = rdr["ParameterID2"].ToString();
                        parameterDependency.Parameter2Value = rdr["ParameterValue2"].ToString();
                        parameterDependency.LSL = rdr["LSL"].ToString();
                        parameterDependency.USL = rdr["USL"].ToString();
                        listParameterDependencies.Add(parameterDependency);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("ParameterRelationship - getParameterRelationList" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return listParameterDependencies;
        }
        internal static List<ParameterDependency> getParameterRelationData()
        {

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            List<ParameterDependency> listParameterDependencies = new List<ParameterDependency>();
            ParameterDependency parameterDependency = null;
            try
            {
                cmd = new SqlCommand("[S_Get_ParameterDependencySaveView]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Param", "ParameterList");
                cmd.Parameters.AddWithValue("@ParameterID1", "");
                cmd.Parameters.AddWithValue("@Parameter1", "");
                cmd.Parameters.AddWithValue("@Parameter2", "");
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        parameterDependency = new ParameterDependency();
                        parameterDependency.Parameter1 = rdr["Parameter"].ToString();
                        parameterDependency.ParameterId1 = rdr["ParameterID"].ToString();
                        parameterDependency.Parameter2Value = rdr["parameter2"].ToString();
                        listParameterDependencies.Add(parameterDependency);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("ParameterRelationship - getParameterRelationData" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return listParameterDependencies;
        }
        internal static string saveUpdateParameterRelationshipData(ParameterDependency parmdependency)
        {

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = null;
            SqlDataReader rdr = null;
            string Output = "";
            try
            {
                cmd = new SqlCommand("[S_Get_ParameterDependencySaveView]", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Parameter1", parmdependency.Parameter1);
                cmd.Parameters.AddWithValue("@Parameter2", parmdependency.Parameter2);
                cmd.Parameters.AddWithValue("@ParameterID1", parmdependency.ParameterId1);
                cmd.Parameters.AddWithValue("@ParameterID2", parmdependency.ParameterId2);
                cmd.Parameters.AddWithValue("@ParameterValue2", parmdependency.Parameter2Value);
                cmd.Parameters.AddWithValue("@LSL", parmdependency.LSL);
                cmd.Parameters.AddWithValue("@USL", parmdependency.USL);
                cmd.Parameters.AddWithValue("@Param", "Save");
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        Output = rdr["Flag"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteErrorLog("ParameterRelationship -saveUpdateParameterRelationshipData" +
                    "" + ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return Output;

        }
        #endregion

        #region -------- SdocId Comparison------
        internal static DataTable cumiBindGeneralInfo(string SDocid1, string SDocid2,string selectedParameter, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<OutputModuleParam> list = new List<OutputModuleParam>();
            OutputModuleParam parameter = null;
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_SystemDocComparisonReport]", con);
                cmd.Parameters.AddWithValue("@SDocId1", SDocid1);
                cmd.Parameters.AddWithValue("@SDocId2", SDocid2);
                cmd.Parameters.AddWithValue("@Parameter", selectedParameter);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    dt.Load(sdr);
                    dt.Columns["Customname"].ColumnName = "Items";
                    dt.Columns["Recommendation"].ColumnName = "Recommendation";
                    dt.Columns["Lower Limit"].ColumnName = "Lower Limit";
                    dt.Columns["Upper Limit"].ColumnName = "Upper Limit";
                    dt.Columns.Remove("ParameterID");

                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("Cumi Bind GeneralInfo" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close(); if (sdr != null) sdr.Close();
            }
            return dt;
        }

        internal static List<QualityParam> cumiBindQualityParam(string SDocid1, string SDocid2, string selectedParameter, string param)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<QualityParam> listQlydetails = new List<QualityParam>();
            QualityParam data = null;
            DataTable dt = new DataTable();
            List<string> distSdoc = new List<string>();
            List<string> itemname = new List<string>();
            List<string> Sdocid = new List<string>();
            List<string> value = new List<string>();
            List<string> parameterName = new List<string>();
            List<string> subParameterName = new List<string>();
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_SystemDocComparisonReport]", con);
                cmd.Parameters.AddWithValue("@SDocId1", SDocid1);
                cmd.Parameters.AddWithValue("@SDocId2", SDocid2);
                cmd.Parameters.AddWithValue("@Parameter", selectedParameter);
                cmd.Parameters.AddWithValue("@Param", param);
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        distSdoc.Add(sdr["SDocId"].ToString());
                    }
                    sdr.NextResult();
                    while (sdr.Read())
                    {
                        itemname.Add(sdr["Customname"].ToString());
                        Sdocid.Add(sdr["SDocId"].ToString());
                        value.Add(sdr["Value"].ToString());
                        parameterName.Add(sdr["ParameterName"].ToString());
                        subParameterName.Add(sdr["SubParameterName"].ToString());
                    }
                    for (int i = 0; i < distSdoc.Count; i++)
                    {
                        List<string> tempSodc = new List<string>();
                        List<string> tempItemName = new List<string>();
                        List<string> tempValue = new List<string>();
                        List<string> tempParamName = new List<string>();
                        List<string> tempSubParamName = new List<string>();
                        data = new QualityParam();
                        DataInputModuleParameter details = null;
                        data.SdocName = distSdoc[i];
                        for (int j = 0; j < Sdocid.Count; j++)
                        {
                            if (distSdoc[i] == Sdocid[j])
                            {
                                tempSodc.Add(Sdocid[j]);
                                tempItemName.Add(itemname[j]);
                                tempValue.Add(value[j]);
                                tempParamName.Add(parameterName[j]);
                                tempSubParamName.Add(subParameterName[j]);
                            }
                        }
                        List<string> DistinctParameter = new List<string>();
                        string tempparam = "";
                        for (int d = 0; d < tempParamName.Count; d++)
                        {
                            if (tempParamName[d] != tempparam)
                            {
                                DistinctParameter.Add(tempParamName[d]);
                            }
                            tempparam = tempParamName[d];
                        }
                        for (int l = 0; l < DistinctParameter.Count; l++)
                        {

                            details = new DataInputModuleParameter();
                            // details.Prameter = DistinctParameter[l];
                            string custName = DistinctParameter[l];
                            for (var m = 0; m < tempSodc.Count; m++)
                            {
                                if (tempParamName[m] == DistinctParameter[l])
                                {
                                    if (tempSubParamName[m] == "Lower Target")
                                    {
                                        details.TargetLower = tempValue[m];
                                    }
                                    else if (tempSubParamName[m] == "Upper Target")
                                    {
                                        details.TargetUpper = tempValue[m];
                                    }
                                    else if (tempSubParamName[m] == "Lower Actual")
                                    {
                                        details.ActualLower = tempValue[m];
                                    }
                                    else if (tempSubParamName[m] == "Upper Actual")
                                    {
                                        details.ActualUpper = tempValue[m];
                                    }
                                    custName = tempItemName[m].Substring(0, tempItemName[m].LastIndexOf(";"));
                                }
                            }
                            details.Prameter = custName;
                            data.Values.Add(details);
                        }
                        listQlydetails.Add(data);
                    }

                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("Cumi - Bind QualityParam" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close(); if (sdr != null) sdr.Close();
            }
            return listQlydetails;
        }

        internal static List<string> cumiBindCalculatedParam(string SDocid1, string SDocid2)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            DataTable dt = new DataTable();
            List<string> calcParamDetails = new List<string>();
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_SystemDocComparisonReport]", con);
                cmd.Parameters.AddWithValue("@SDocId1", SDocid1);
                cmd.Parameters.AddWithValue("@SDocId2", SDocid2);
                cmd.Parameters.AddWithValue("@Param", "CalculatedParm");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    dt.Load(sdr);
                    DataTable dtCopy = dt.Copy();
                    HttpContext.Current.Session["CalcParamGraphData"] = dtCopy;
                    calcParamDetails.Add(dt.Rows.Count.ToString());
                    calcParamDetails.Add(dt.Columns.Count.ToString());
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        calcParamDetails.Add(dt.Columns[i].ColumnName);
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            calcParamDetails.Add(dt.Rows[j][i].ToString());
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("CUMI - Bind CalculatedParam" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close(); if (sdr != null) sdr.Close();
            }
            return calcParamDetails;
        }
        internal static List<TotalCycleTimeGrpah> cumiBindTotalCycleTime(string SDocid1, string SDocid2)
        {

            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
           // List<string> listtotoalcycletime = new List<string>();
            List<TotalCycleTimeGrpah> listtotalCycleTimeGrpahs = new List<TotalCycleTimeGrpah>();
            TotalCycleTimeGrpah totalCycleTimeGrpah = null;
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_SystemDocComparisonReport]", con);
                cmd.Parameters.AddWithValue("@SDocId1", SDocid1);
                cmd.Parameters.AddWithValue("@SDocId2", SDocid2);
                cmd.Parameters.AddWithValue("@Param", "TotalGrindingParm");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                        dt.Load(sdr);
                        DataTable dtCopy = dt.Copy();
                        HttpContext.Current.Session["TotalCycleTime"] = dtCopy;
                        for(int i=2;i<dt.Columns.Count;i++)
                        {
                            totalCycleTimeGrpah = new TotalCycleTimeGrpah();
                            totalCycleTimeGrpah.SDocid = dt.Columns[i].ColumnName;
                            List<TotalCycleTimeGrpahValue> listtotalCycleTimeGrpahValues = new List<TotalCycleTimeGrpahValue>();
                            TotalCycleTimeGrpahValue totalCycleTimeGrpahValue = null;
                            for (int j=0;j<dt.Rows.Count;j++)
                            {
                                totalCycleTimeGrpahValue = new TotalCycleTimeGrpahValue();
                                totalCycleTimeGrpahValue.Parameter = dt.Rows[j][1].ToString();
                                totalCycleTimeGrpahValue.Value = dt.Rows[j][i].ToString();
                                listtotalCycleTimeGrpahValues.Add(totalCycleTimeGrpahValue);
                            }
                            totalCycleTimeGrpah.values = listtotalCycleTimeGrpahValues;
                            listtotalCycleTimeGrpahs.Add(totalCycleTimeGrpah);
                        }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("CUMI - Bind TotalCycleTime" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close(); if (sdr != null) sdr.Close();
            }
            return listtotalCycleTimeGrpahs;
        }

        internal static List<TotalCycleTimeGrpah> cumiBindGrindingTime(string SDocid1, string SDocid2)
        {

            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            // List<string> listtotoalcycletime = new List<string>();
            List<TotalCycleTimeGrpah> listtotalCycleTimeGrpahs = new List<TotalCycleTimeGrpah>();
            TotalCycleTimeGrpah totalCycleTimeGrpah = null;
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_SystemDocComparisonReport]", con);
                cmd.Parameters.AddWithValue("@SDocId1", SDocid1);
                cmd.Parameters.AddWithValue("@SDocId2", SDocid2);
                cmd.Parameters.AddWithValue("@Param", "GrindingParm");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                        dt.Load(sdr);
                        DataTable dtCopy = dt.Copy();
                        HttpContext.Current.Session["GrindingTime"] = dtCopy;
                        for (int i = 2; i < dt.Columns.Count; i++)
                        {
                            totalCycleTimeGrpah = new TotalCycleTimeGrpah();
                            totalCycleTimeGrpah.SDocid = dt.Columns[i].ColumnName;
                            List<TotalCycleTimeGrpahValue> listtotalCycleTimeGrpahValues = new List<TotalCycleTimeGrpahValue>();
                            TotalCycleTimeGrpahValue totalCycleTimeGrpahValue = null;
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                totalCycleTimeGrpahValue = new TotalCycleTimeGrpahValue();
                                totalCycleTimeGrpahValue.Parameter = dt.Rows[j][1].ToString();
                                totalCycleTimeGrpahValue.Value = dt.Rows[j][i].ToString();
                                listtotalCycleTimeGrpahValues.Add(totalCycleTimeGrpahValue);
                            }
                            totalCycleTimeGrpah.values = listtotalCycleTimeGrpahValues;
                            listtotalCycleTimeGrpahs.Add(totalCycleTimeGrpah);
                        }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("CUMI - Bind GrindingTime" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close(); if (sdr != null) sdr.Close();
            }
            return listtotalCycleTimeGrpahs;
        }
        internal static List<TotalCycleTimeGrpah> cumiBindNonGrindingTime(string SDocid1, string SDocid2)
        {

            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            // List<string> listtotoalcycletime = new List<string>();
            List<TotalCycleTimeGrpah> listtotalCycleTimeGrpahs = new List<TotalCycleTimeGrpah>();
            TotalCycleTimeGrpah totalCycleTimeGrpah = null;
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_SystemDocComparisonReport]", con);
                cmd.Parameters.AddWithValue("@SDocId1", SDocid1);
                cmd.Parameters.AddWithValue("@SDocId2", SDocid2);
                cmd.Parameters.AddWithValue("@Param", "NonGrindingParm");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                        dt.Load(sdr);
                        DataTable dtCopy = dt.Copy();
                        HttpContext.Current.Session["NonGrindingTime"] = dtCopy;
                        for (int i = 2; i < dt.Columns.Count; i++)
                        {
                            totalCycleTimeGrpah = new TotalCycleTimeGrpah();
                            totalCycleTimeGrpah.SDocid = dt.Columns[i].ColumnName;
                            List<TotalCycleTimeGrpahValue> listtotalCycleTimeGrpahValues = new List<TotalCycleTimeGrpahValue>();
                            TotalCycleTimeGrpahValue totalCycleTimeGrpahValue = null;
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                totalCycleTimeGrpahValue = new TotalCycleTimeGrpahValue();
                                totalCycleTimeGrpahValue.Parameter = dt.Rows[j][1].ToString();
                                totalCycleTimeGrpahValue.Value = dt.Rows[j][i].ToString();
                                listtotalCycleTimeGrpahValues.Add(totalCycleTimeGrpahValue);
                            }
                            totalCycleTimeGrpah.values = listtotalCycleTimeGrpahValues;
                            listtotalCycleTimeGrpahs.Add(totalCycleTimeGrpah);
                        }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("CUMI - Bind NonGrindingTime" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close(); if (sdr != null) sdr.Close();
            }
            return listtotalCycleTimeGrpahs;
        }

        internal static DataTable cumiBindInferenceSignal(string SDocid1, string SDocid2)        {            SqlConnection con = ConnectionManager.GetConnection();            SqlDataReader sdr = null;            SqlCommand cmd = null;            DataTable dt = new DataTable();            try            {                cmd = new SqlCommand("[dbo].[S_Get_SystemDocComparisonReport]", con);                cmd.Parameters.AddWithValue("@SDocId1", SDocid1);
                cmd.Parameters.AddWithValue("@SDocId2", SDocid2);                cmd.Parameters.AddWithValue("@Param", "SignalParm");                cmd.CommandType = CommandType.StoredProcedure;                sdr = cmd.ExecuteReader();                if (sdr.HasRows)                {                    dt.Load(sdr);
                    dt.Columns.Remove("ParameterID");
                }            }            catch (Exception ex)            {                Logger.WriteDebugLog("CUMI - BindInferenceSignal" + ex.Message);            }            finally            {                if (con != null) con.Close(); if (sdr != null) sdr.Close();            }            return dt;        }

        internal static List<SdocImages> cumiBindImages(string SDocid1, string SDocid2)
        {
            SqlConnection con = ConnectionManager.GetConnection();
            SqlDataReader sdr = null;
            SqlCommand cmd = null;
            List<SdocImages> listSdocImage = new List<SdocImages>();
            SdocImages data = null;
            DataTable dt = new DataTable();
            List<string> DistSdoc = new List<string>();
            try
            {
                cmd = new SqlCommand("[dbo].[S_Get_SystemDocComparisonReport]", con);
                cmd.Parameters.AddWithValue("@SDocId1", SDocid1);
                cmd.Parameters.AddWithValue("@SDocId2", SDocid2);
                cmd.Parameters.AddWithValue("@Param", "IMAGES");
                cmd.CommandType = CommandType.StoredProcedure;
                sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    dt.Load(sdr);
                    string tempSdocName = "";
                    foreach (DataRow item in dt.Rows)
                    {

                        if (item["SDOCid"].ToString() != tempSdocName)
                        {
                            DistSdoc.Add(item["SDOCid"].ToString());
                        }
                        tempSdocName = item["SDOCid"].ToString();
                    }
                    foreach (string Sdoc in DistSdoc)
                    {
                        data = new SdocImages();
                        WorkpieceImage imageDetail = null;
                        data.SdocName = Sdoc;
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            if (dataRow["SDOCid"].ToString() == Sdoc)
                            {
                                imageDetail = new WorkpieceImage();
                                if (dataRow["ImageFileName"].ToString().StartsWith("~/UploadImages"))
                                {
                                    imageDetail.wpImageName = dataRow["ImageName"].ToString();
                                    imageDetail.wpImagePath = dataRow["ImageFileName"].ToString();
                                    data.Values.Add(imageDetail);
                                }
                            }
                        }
                        listSdocImage.Add(data);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteDebugLog("Cumi- BindImages" + ex.Message);
            }
            finally
            {
                if (con != null) con.Close(); if (sdr != null) sdr.Close();
            }
            return listSdocImage;
        }
        #endregion
    }
}