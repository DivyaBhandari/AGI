﻿using System;

namespace AGISoftware
{
    /// <summary>
    /// Summary description for fileUploader
    /// </summary>
    public class fileUploader : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
                    string fileName = currentDateTime + "_" + file.FileName;
                        //str_image = "MyPHOTO_" + numFiles.ToString() + fileExtension;
                        //str_image = "MyPHOTO_" + fileName + fileExtension;

                       
                        str_image =  fileName;

                            List<string> list = (List<string>)HttpContext.Current.Session["imagesDetails"];
                          
                            list.Add("~/UploadImages/" + fileName);
                            //list.Add(fileName);
                            HttpContext.Current.Session["imagesDetails"] = list;
                        
                            list.Add("~/UploadImages/" + fileName);
                            //list.Add(fileName);
                            HttpContext.Current.Session["imagesDetails"] = list;
                //  database record update logic here  ()

                context.Response.Write(str_image);
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}