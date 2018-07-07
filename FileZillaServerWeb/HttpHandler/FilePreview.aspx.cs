using FileZillaServerBLL;
using FileZillaServerCommonHelper;
using FileZillaServerModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb.HttpHandler
{
    public partial class FilePreview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PreviewFile();
            }
        }

        private void PreviewFile()
        {
            string fileHistoryId = Request["fileHistoryId"];
            try
            {
                int errorCode = 0;
                string actPath = string.Empty;
                string taskRootFolder = string.Empty;
                string taskFolderWithoutEmpNo = string.Empty;
                // 根据 ID 获取到 fileHistory 对象
                FileHistory fileHistory = new FileHistoryBLL().GetModel(fileHistoryId);
                DataTable dtPrjIdAndCategory = new FileCategoryBLL().GetProjectIdByFileHistoryId(fileHistoryId).Tables[0];
                string projectId = Convert.ToString(dtPrjIdAndCategory.Rows[0]["PROJECTID"]);
                string category = Convert.ToString(dtPrjIdAndCategory.Rows[0]["category"]);
                string folderName = Convert.ToString(dtPrjIdAndCategory.Rows[0]["folderName"]);
                bool flag = new FileCategoryBLL().GetFilePathByProjectId(projectId, category, folderName, false, out actPath, out taskRootFolder, out taskFolderWithoutEmpNo, out errorCode);
                string physicalFileName = Path.Combine(actPath, fileHistory.FILENAME);
                if (File.Exists(physicalFileName))
                {
                    string extName = Path.GetExtension(physicalFileName);
                    switch (extName)
                    {
                        case ".pdf":
                            PreviewPdf(physicalFileName);
                            break;
                        case ".txt":
                            PreviewText(physicalFileName);
                            break;
                        case ".jpg":
                        case ".png":
                        case ".bmp":
                        case ".jpeg":
                        case ".gif":
                            PreviewImage(physicalFileName);
                            break;
                        default:
                            return;
                    }
                }
                Response.Write("<h3>文件不存在！</h3>");
                Response.End();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine($"文件{fileHistoryId}预览出错" + ex.Message + ex.StackTrace);
                Response.Write("<h3>ERROR</h3>");
                Response.End();
            }
        }

        private void PreviewPdf(string physicalFileName)
        {
            Response.ContentType = "Application/pdf";
            Response.AddHeader("content-disposition", "filename=" + physicalFileName);
            Response.WriteFile(physicalFileName);
            Response.End();
        }

        public void PreviewText(string physicalFileName)
        {
            Response.ContentType = "text/plain";
            Response.ContentEncoding = Encoding.GetEncoding("gb2312");
            Response.AddHeader("content-disposition", "filename=" + physicalFileName);
            Response.WriteFile(physicalFileName);
            Response.End();
        }

        private void PreviewImage(string physicalFileName)
        {
            imgPreview.ImageUrl = physicalFileName;
            // 以二进制方式读文件
            FileStream aFile = new FileStream(physicalFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            // 创建一个二进制数据流读入器，和打开的文件关联
            BinaryReader brMyfile = new BinaryReader(aFile);
            // 把文件指针重新定位到文件的开始
            brMyfile.BaseStream.Seek(0, SeekOrigin.Begin);
            //获取照片的字节数组
            byte[] photo = brMyfile.ReadBytes(Convert.ToInt32(aFile.Length.ToString()));
            // 关闭以上new的各个对象
            brMyfile.Close();
            Response.Write(photo);
            if (photo.Length > 0)
            {
                MemoryStream ms = new MemoryStream(photo);
                Response.Clear();
                Response.ContentType = "image/gif";
                Response.OutputStream.Write(photo, 0, photo.Length);
                Response.End();
            }
            Response.End();
        }
    }
}