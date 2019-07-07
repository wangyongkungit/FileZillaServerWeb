using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FileZillaServerDAL;
using System.IO;
using FileZillaServerCommonHelper;
using FileZillaServerBLL;

namespace FileZillaServerWeb
{
    public partial class FileZillaLogs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (Session["userID"] == null)
                //{
                //    Response.Redirect("Login.aspx");
                //}
                //else
                //{
                    LoadOperateType();
                    GridViewDataBind();
                //}
            }
        }

        FileZillaBLL fzBll = new FileZillaBLL();

        protected void LoadOperateType()
        {
            DataTable dt = new ConfigureBLL().GetConfig("日志记录操作类型");
            ddlOperateType.DataSource = dt;
            ddlOperateType.DataTextField = "configvalue";
            ddlOperateType.DataValueField = "configkey";
            ddlOperateType.DataBind();
            ddlOperateType.Items.Insert(0, new ListItem("--", string.Empty));
        }

        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            GridViewDataBind();
        }

        /// <summary>
        /// 同步按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSync_Click(object sender, EventArgs e)
        {
            LoadLogTxt();
        }

        /// <summary>
        /// 查询按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GridViewDataBind();
        }

        protected void GridViewDataBind()
        {
            string userID = txtUserName.Text.Trim();
            string operateType = ddlOperateType.SelectedValue;
            string content = txtContent.Text.Trim();
            string startDate = txtStartDate.Text.Trim();
            string endDate = txtEndDate.Text.Trim();
            string fileName = txtFileName.Text.Trim();
            AspNetPager.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            int totalAmount = 0;
            DataTable dt = fzBll.GetFileZilla(userID, operateType, content, startDate, endDate, fileName, AspNetPager.CurrentPageIndex, AspNetPager.PageSize, out totalAmount);
            AspNetPager.RecordCount = totalAmount;
            gvData.DataSource = dt;
            gvData.DataBind();
        }

        #region Remarks_LoadLogTxt
        protected void LoadLogTxt()
        {
            #region Old Code,2017-02-22 Remarks
            ////定义日志配置路径
            //string dictionaryPath = ConfigurationManager.AppSettings["txtLogPath"];
            ////获取日志配置目录
            //DirectoryInfo theFolder = new DirectoryInfo(dictionaryPath);
            ////遍历目录下的所有文件
            //foreach (FileInfo file in theFolder.GetFiles())
            //{
            //    //定义副本文件名称，之后都是针对副本进行操作的
            //    string fileFullNameCopy = file.FullName.Substring(0, file.FullName.LastIndexOf(".log")) + "_bak.log";
            //    //如果存在则删除
            //    if (File.Exists(fileFullNameCopy))
            //    {
            //        File.Delete(fileFullNameCopy);
            //    }
            //    //不存在复制一份副本
            //    else
            //    {
            //        File.Copy(file.FullName, fileFullNameCopy);
            //    }
            //    System.IO.StreamReader sr = new System.IO.StreamReader(fileFullNameCopy, System.Text.Encoding.Default);
            //    string line;
            //    while ((line = sr.ReadLine()) != null)
            //    {
            //        //获得时间字符串的起始索引
            //        int startDateCharIndex = line.IndexOf(')') + 1;
            //        //获得时间字符串的结束索引
            //        int endDateCharIndex = line.IndexOf('-');
            //        //存在时间字符串则进行进一步操作
            //        if (startDateCharIndex >= 0 && endDateCharIndex > -1)
            //        {
            //            DateTime dateTime = new DateTime();
            //            string strDate = line.Substring(startDateCharIndex, endDateCharIndex - startDateCharIndex);
            //            bool convertResult = DateTime.TryParse(strDate, out dateTime);
            //            if (convertResult)
            //            {
            //                //登录
            //                if (line.Contains("> 230 Logged on"))
            //                {
            //                    int startUserStrintIndex = endDateCharIndex;
            //                    int endUserStringIndex = Common.GetIndexOfCharAppearAmount2(line, '(', 2);
            //                    string userID = line.Substring(startUserStrintIndex + 1, endUserStringIndex - (startUserStrintIndex + 1)).Trim();
            //                    string content = string.Format("【{0}】在【{1}】登录系统", userID, dateTime);
            //                    int rows = fzBll.Add(userID, FileZillaOperateType.Login, content, dateTime, null);
            //                }
            //                //上传、下载和删除
            //                else if (line.Contains("> STOR") || line.Contains("> RETR") || line.Contains("> DELE"))
            //                {
            //                    string operateCHN = string.Empty;
            //                    string operateEng = string.Empty;
            //                    FileZillaOperateType fzot = FileZillaOperateType.Default;
            //                    if (line.Contains("> STOR"))
            //                    {
            //                        operateCHN = "上传";
            //                        operateEng = "> STOR";
            //                        fzot = FileZillaOperateType.Upload;
            //                    }
            //                    else if (line.Contains("> RETR"))
            //                    {
            //                        operateCHN = "下载";
            //                        operateEng = "> RETR";
            //                        fzot = FileZillaOperateType.Download;
            //                    }
            //                    else if (line.Contains("> DELE"))
            //                    {
            //                        operateCHN = "删除";
            //                        operateEng = "> DELE";
            //                        fzot = FileZillaOperateType.Delete;
            //                    }
            //                    int startUserStringIndex = endDateCharIndex;
            //                    int endUserStringIndex = Common.GetIndexOfCharAppearAmount2(line, '(', 2);
            //                    string userID = line.Substring(startUserStringIndex + 1, endUserStringIndex - (startUserStringIndex + 1)).Trim();
            //                    string fileName = line.Substring(line.IndexOf(operateEng, 0) + 6 + 1).Trim();
            //                    string content = string.Format("【{0}】在【{1}】{2}了文件【{3}】", userID, dateTime, operateCHN, fileName);
            //                    int rows = fzBll.Add(userID, fzot, content, dateTime, fileName);
            //                }
            //            }
            //        }
            //    }
            //    //关闭流
            //    if (sr != null)
            //    {
            //        sr.Close();
            //    }
            //    //删除文件
            //    if (File.Exists(fileFullNameCopy))
            //    {
            //        File.Delete(fileFullNameCopy);
            //    }
            //}
            #endregion

            //获取并定义日志配置路径
            try
            {
                string dictionaryPath = ConfigurationManager.AppSettings["txtLogPath"];

                string logFileName = string.Format("fzs-{0}.log", DateTime.Now.ToString("yyyy-MM-dd"));
                string fullName = string.Format("{0}{1}", dictionaryPath, logFileName);
                if (File.Exists(fullName))
                {
                    string startTime = string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd"), "00:00:00");
                    string endTime = string.Format("{0} {1}", DateTime.Now.ToString("yyyy-MM-dd"), "23:59:59");
                    int rowsDeleted = fzBll.Delete(startTime, endTime);
                    //定义副本文件名称，之后都是针对副本进行操作的
                    string fileFullNameCopy = fullName.Substring(0, fullName.LastIndexOf(".log")) + "_bak.log";
                    //如果存在则删除
                    if (File.Exists(fileFullNameCopy))
                    {
                        File.Delete(fileFullNameCopy);
                    }
                    //不存在复制一份副本
                    else
                    {
                        File.Copy(fullName, fileFullNameCopy);
                    }
                    System.IO.StreamReader sr = new System.IO.StreamReader(fileFullNameCopy, System.Text.Encoding.UTF8);
                    bool isFolder = false;//是否进入文件夹上传状态的标识
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        //当不是文件夹时才继续遍历
                        if (isFolder && line.Contains("> disconnected."))
                        {
                            isFolder = false;
                        }
                        if (!isFolder)
                        {
                            //获得时间字符串的起始索引
                            int startDateCharIndex = line.IndexOf(')') + 1;
                            //获得时间字符串的结束索引
                            int endDateCharIndex = line.IndexOf('-');
                            //存在时间字符串则进行进一步操作
                            if (startDateCharIndex >= 0 && endDateCharIndex > -1)
                            {
                                DateTime dateTime = new DateTime();
                                string strDate = line.Substring(startDateCharIndex, endDateCharIndex - startDateCharIndex);
                                bool convertResult = DateTime.TryParse(strDate, out dateTime);
                                if (convertResult)
                                {
                                    //登录
                                    if (line.Contains("> 230 Logged on"))
                                    {
                                        isFolder = false;//将是否是文件夹的标识置为false
                                        int startUserStrintIndex = endDateCharIndex;
                                        int endUserStringIndex = Common.GetIndexOfCharAppearAmount2(line, '(', 2);
                                        string userID = line.Substring(startUserStrintIndex + 1, endUserStringIndex - (startUserStrintIndex + 1)).Trim();
                                        string content = string.Format("{0} 登录系统", userID);
                                        int rows = fzBll.Add(userID, FileZillaOperateType.Login, content, dateTime, null);
                                    }
                                    //上传、下载和删除
                                    else if (line.Contains("> STOR ") || line.Contains("> RETR ") || line.Contains("> DELE "))
                                    {
                                        isFolder = false;//将是否是文件夹的标识置为false
                                        string operateCHN = string.Empty;
                                        string operateEng = string.Empty;
                                        FileZillaOperateType fzot = FileZillaOperateType.Default;
                                        if (line.Contains("> STOR "))
                                        {
                                            operateCHN = "上传";
                                            operateEng = "> STOR";
                                            fzot = FileZillaOperateType.Upload;
                                        }
                                        else if (line.Contains("> RETR "))
                                        {
                                            operateCHN = "下载";
                                            operateEng = "> RETR";
                                            fzot = FileZillaOperateType.Download;
                                        }
                                        else if (line.Contains("> DELE "))
                                        {
                                            operateCHN = "删除";
                                            operateEng = "> DELE";
                                            fzot = FileZillaOperateType.Delete;
                                        }
                                        int startUserStringIndex = endDateCharIndex;
                                        int endUserStringIndex = Common.GetIndexOfCharAppearAmount2(line, '(', 2);
                                        string userID = line.Substring(startUserStringIndex + 1, endUserStringIndex - (startUserStringIndex + 1)).Trim();
                                        string fileName = line.Substring(line.IndexOf(operateEng, 0) + 6).Trim();
                                        string content = string.Format("{0} {1}文件【{2}】", userID, operateCHN, fileName);
                                        int rows = fzBll.Add(userID, fzot, content, dateTime, fileName);
                                    }
                                    else if (line.Contains("> MKD "))
                                    {
                                        isFolder = true;//将是否是文件夹的标识置为true
                                        int startUserStrintIndex = endDateCharIndex;
                                        int endUserStringIndex = Common.GetIndexOfCharAppearAmount2(line, '(', 2);
                                        string userID = line.Substring(startUserStrintIndex + 1, endUserStringIndex - (startUserStrintIndex + 1)).Trim();
                                        string folderName = line.Substring(line.IndexOf("> MKD ", 0) + 6).Trim();
                                        string content = string.Format("{0} 上传文件夹【{1}】", userID, folderName);
                                        int rows = fzBll.Add(userID, FileZillaOperateType.Upload, content, dateTime, folderName);
                                    }
                                }
                            }
                        }
                    }
                    //关闭流
                    if (sr != null)
                    {
                        sr.Close();
                    }
                    //删除文件
                    if (File.Exists(fileFullNameCopy))
                    {
                        File.Delete(fileFullNameCopy);
                    }
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Replace("'", "''") + ex.StackTrace.Replace("'", "''");
                ClientScript.RegisterClientScriptBlock(this.GetType(), string.Empty, "alert('" + msg + "')", true);
            }
        }
        #endregion
    }
}