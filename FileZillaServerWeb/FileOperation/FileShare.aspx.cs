using FileZillaServerBLL;
using FileZillaServerCommonHelper;
using FileZillaServerModel;
using FileZillaServerProfile;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb.FileOperation
{
    public partial class FileShare : System.Web.UI.Page
    {
        FileHistoryBLL fhBll = new FileHistoryBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void LoadData()
        {
            string fileHistoryId = Request.QueryString["fileHistoryId"];
            if (string.IsNullOrEmpty(fileHistoryId))
            {
                return;
            }
            string prjId = fhBll.GetProjectIdById(fileHistoryId);
            Project prj = new ProjectBLL().GetModel(prjId);
            DataTable dtShopName = new ConfigureBLL().GetConfig("店铺编号名称");
            lblShopName.Text = dtShopName.AsEnumerable().Where(item => item.Field<string>("configKey") == prj.SHOP).Select(item2 => item2.Field<string>("configValue")).FirstOrDefault();
        }

        protected void btnGetFile_Click(object sender, EventArgs e)
        {
            ValidatePassword();
        }

        private void ValidatePassword()
        {
            string fileHistoryId = Request.QueryString["fileHistoryId"];
            if (string.IsNullOrEmpty(fileHistoryId))
            {
                return;
            }
            string taskNo = fhBll.GetTaskNoById(fileHistoryId);
            string password = taskNo.Substring(taskNo.Length - 4, 4);
            char[] arr = password.ToCharArray().Reverse().ToArray();
            password = new string(arr);
            if (txtGetPassword.Text.Trim() != password)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('提取码不正确！')", true);
                return;
            }
            DownloadFile(fileHistoryId);
        }

        protected void DownloadFile(string fileHistoryId)
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
                string userId = UserProfile.GetInstance()?.ID;
                string operateTypeName = "下载";
                int operateTypeKey = new ConfigureBLL().GetConfig(ConfigTypeName.文件操作类型.ToString()).AsEnumerable().Where(item => item["configValue"].ToString() == operateTypeName).Select(item => Convert.ToInt32(item["configKey"])).FirstOrDefault();
                FileOperationLog fileOperationLog = new FileOperationLog();
                fileOperationLog.ID = Guid.NewGuid().ToString();
                fileOperationLog.PROJECTID = projectId;
                fileOperationLog.EMPLOYEEID = userId;
                fileOperationLog.FILENAME = fileHistory.FILENAME;
                fileOperationLog.OPERATETYPE = operateTypeKey;
                fileOperationLog.OPERATEDATE = DateTime.Now;
                fileOperationLog.OPERATECONTENT = operateTypeName + fileHistory.FILENAME;
                fileOperationLog.OPERATEUSER = userId;
                new FileOperationLogBLL().Add(fileOperationLog);

                System.IO.Stream iStream = null;
                // Buffer to read 10K bytes in chunk:
                byte[] buffer = new Byte[10000];
                // Length of the file:
                int length;
                // Total bytes to read.
                long dataToRead;
                // Identify the file to download including its path.
                string filepath = physicalFileName;
                // Identify the file name.
                string filename = System.IO.Path.GetFileName(filepath);
                try
                {
                    // Open the file.
                    iStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open,
                                System.IO.FileAccess.Read, System.IO.FileShare.Read);
                    // Total bytes to read.
                    dataToRead = iStream.Length;
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ClearContent();
                    Response.ContentType = "text/plain"; // Set the file type
                    Response.AddHeader("Content-Length", dataToRead.ToString());
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + "");

                    // Read the bytes.
                    while (dataToRead > 0)
                    {
                        // Verify that the client is connected.
                        if (Response.IsClientConnected)
                        {
                            // Read the data in buffer.
                            length = iStream.Read(buffer, 0, 10000);

                            // Write the data to the current output stream.
                            Response.OutputStream.Write(buffer, 0, length);

                            // Flush the data to the HTML output.
                            Response.Flush();

                            buffer = new Byte[10000];
                            dataToRead = dataToRead - length;
                        }
                        else
                        {
                            // Prevent infinite loop if user disconnects
                            dataToRead = -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Trap the error, if any.
                    Response.Write("Error : " + ex.Message);
                }
                finally
                {
                    if (iStream != null)
                    {
                        //Close the file.
                        iStream.Close();
                    }
                    Response.End();
                }
            }
        }
    }
}