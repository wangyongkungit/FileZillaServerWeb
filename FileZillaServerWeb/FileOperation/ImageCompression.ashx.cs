using FileZillaServerBLL;
using FileZillaServerModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using Yiliangyijia.Comm;

namespace FileZillaServerWeb.FileOperation
{
    /// <summary>
    /// ImageCompression 的摘要说明
    /// </summary>
    public class ImageCompression : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string employeeID = context.Request.Params["empID"];
            GetCertificate(context);
        }

        public void GetCertificate(HttpContext context)
        {
            string employeeID = context.Request.QueryString["empID"];
            Cerficate cerficate = new CerficateBLL().GetModelList(" employeeID = '" + employeeID + "' AND isMain = 1 ").FirstOrDefault();
            if (cerficate != null)
            {
                System.Web.HttpServerUtility server = System.Web.HttpContext.Current.Server;
                string filePath = server.MapPath(string.Format("~/{0}/{1}", Convert.ToString(ConfigurationManager.AppSettings["fileSavePath"]), cerficate.FILEPATH));
                System.Drawing.Image img = new Bitmap(filePath, true);
                //aFile.Close();
                System.Drawing.Image img2 = new ImageHelper().GetHvtThumbnail(img, 400, 0);


                //将Image转换成流数据，并保存为byte[]   

                MemoryStream mstream = new MemoryStream();
                img2.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] byData = new Byte[mstream.Length];
                mstream.Position = 0;
                mstream.Read(byData, 0, byData.Length); mstream.Close();
                if (byData.Length > 0)
                {
                    MemoryStream ms = new MemoryStream(byData);
                    context.Response.Clear();
                    context.Response.ContentType = "image/jpg";
                    context.Response.OutputStream.Write(byData, 0, byData.Length);
                    context.Response.End();
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}