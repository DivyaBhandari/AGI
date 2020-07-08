using System;using System.Collections.Generic;using System.Linq;using System.Web;using System.IO;using System.Web.SessionState;

namespace AGISoftware
{
    /// <summary>
    /// Summary description for fileUploader
    /// </summary>
    public class fileUploader : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)        {            context.Response.ContentType = "text/plain";            try            {                string dirFullPath = HttpContext.Current.Server.MapPath("~/UploadImages/");                string[] files;                files = System.IO.Directory.GetFiles(dirFullPath);                string str_image = "";                foreach (string s in context.Request.Files)                {                    HttpPostedFile file = context.Request.Files[s];                    string currentDateTime = System.DateTime.Now.ToString("ddMMyyhhmmssfffff");
                    string fileName = currentDateTime + "_" + file.FileName;                    string fileExtension = file.ContentType;                    if (!string.IsNullOrEmpty(fileName))                    {                        fileExtension = Path.GetExtension(fileName);
                        //str_image = "MyPHOTO_" + numFiles.ToString() + fileExtension;
                        //str_image = "MyPHOTO_" + fileName + fileExtension;

                       
                        str_image =  fileName;                        string pathToSave_100 = HttpContext.Current.Server.MapPath("~/UploadImages/") + str_image;                        if (HttpContext.Current.Session["imagesDetails"] != null)                        {

                            List<string> list = (List<string>)HttpContext.Current.Session["imagesDetails"];
                          
                            list.Add("~/UploadImages/" + fileName);
                            //list.Add(fileName);
                            HttpContext.Current.Session["imagesDetails"] = list;                        }                        else                        {                            List<string> list = new List<string>();
                        
                            list.Add("~/UploadImages/" + fileName);
                            //list.Add(fileName);
                            HttpContext.Current.Session["imagesDetails"] = list;                        }                        file.SaveAs(pathToSave_100);                    }                }
                //  database record update logic here  ()

                context.Response.Write(str_image);            }            catch (Exception ac)            {            }        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}