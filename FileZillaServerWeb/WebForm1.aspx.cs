using FileZillaServerBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Upload_Click1(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string FileName = this.AttachFile.FileName;//获取上传文件的全路径  
                string ExtenName = System.IO.Path.GetExtension(FileName);//获取扩展名  
                string SaveFileName = System.IO.Path.Combine(Request.PhysicalApplicationPath + "\\Upload", FileName);//合并两个路径为上传到服务器上的全路径
                if (this.AttachFile.ContentLength > 0)
                {
                    try
                    {
                        this.AttachFile.MoveTo(SaveFileName, Brettle.Web.NeatUpload.MoveToOptions.Overwrite);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('Success');", true);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        protected void Upload_Click(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void btnSync_Click(object sender, EventArgs e)
        {
            TestBLL tBll = new TestBLL();
            tBll.Add();
        }  
    }
}