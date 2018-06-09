using FileZillaServerBLL;
using FileZillaServerCommonHelper;
using FileZillaServerModel;
using FileZillaServerModel.Interface;
using FileZillaServerProfile;
using Jil;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using Yiliangyijia.Comm;

namespace FileZillaServerWeb.HttpHandler
{
    /// <summary>
    /// FileHandler 的摘要说明
    /// </summary>
    public class FileHandler : CCHttpHandler
    {
        string fileUploadTempFolder = Convert.ToString(ConfigurationManager.AppSettings["fileUploadTempFolder"]);

        #region AddFileCategory
        public void AddFileCategory()
        {
            string returnMsg = string.Empty;
            int errorCode = 0;
            string[] parametersRequired = { "projectId", "categoryId", "description" };
            if (!CheckParamsRequired(parametersRequired, out errorCode, out returnMsg))
            {
                JsonResult<string> result = new JsonResult<string> { Code = errorCode, Message = returnMsg, Rows = 0, Result = null, Req_Date = DateTime.Now };
                GenerateJson(result);
                return;
            }
            FileCategoryBLL fcBll = new FileCategoryBLL();
            if (fcBll.AddFileCategory(context, out errorCode))
            {
                string message = ErrorCode.GetCodeMessage(errorCode);
                JsonResult<string> result = new JsonResult<string> { Code = errorCode, Message = message, Rows = 0, Result = null, Req_Date = DateTime.Now };
                GenerateJson(result);
                return;
            }
        }
        #endregion

        #region GetReplyToTab
        /// <summary>
        /// 根据选择的tal获取回复tab列表
        /// </summary>
        public void GetReplyToTab()
        {
            string returnMsg = string.Empty;
            int errorCode = 0;
            // 校验参数
            string[] parametersRequired = { "projectId", "categoryId" };
            if (!CheckParamsRequired(parametersRequired, out errorCode, out returnMsg))
            {
                JsonResult<string> result = new JsonResult<string> { Code = errorCode, Message = returnMsg, Rows = 0, Result = null };
                GenerateJson(result);
                return;
            }
            // 获取列表
            FileCategoryBLL fcBll = new FileCategoryBLL();
            DataTable fileCategories = fcBll.GetReplyToTab(context, out errorCode).Tables[0];
            if (fileCategories != null && fileCategories.Rows.Count > 0)
            {
                List<SubTabs> subTabs = new List<SubTabs>();
                for (int i = 0; i < fileCategories.Rows.Count; i++)
                {
                    SubTabs sub = new SubTabs();
                    sub.Id = fileCategories.Rows[i]["ID"].ToString();
                    //sub.hasParent = fileCategories.Rows[i]["hasParent"].ToString() == "1";
                    sub.categoryId = fileCategories.Rows[i]["categoryId"].ToString();
                    sub.Title = fileCategories.Rows[i]["title"].ToString();
                    subTabs.Add(sub);
                }
                string message = ErrorCode.GetCodeMessage(errorCode);
                JsonResult<SubTabs> result = new JsonResult<SubTabs> { Code = errorCode, Message = message, Rows = subTabs.Count(), Result = subTabs };
                GenerateJson(result);
                return;
            }
            else
            {
                string message = ErrorCode.GetCodeMessage(errorCode);
                JsonResult<string> result = new JsonResult<string> { Code = errorCode, Message = message, Rows = 0, Result = null };
                GenerateJson(result);
                return;
            }
        }
        #endregion

        #region 获取 fileCategory 列表
        /// <summary>
        /// 获取 fileCategory 列表
        /// </summary>
        public void GetCategories()
        {
            string returnMsg = string.Empty;
            int errorCode = 0;
            // 校验参数
            string[] parametersRequired = { "projectId" };
            if (!CheckParamsRequired(parametersRequired, out errorCode, out returnMsg))
            {
                JsonResult<string> result = new JsonResult<string> { Code = errorCode, Message = returnMsg, Rows = 0, Result = null };
                GenerateJson(result);
                return;
            }

            FileCategoryBLL fcBll = new FileCategoryBLL();
            List<FileCategory> categories = fcBll.GetCategories(context, out errorCode);
            // 获取到结果
            if (categories != null && categories.Count() > 0)
            {
                List<CategoryTabs> categoryTabs = new List<CategoryTabs>();
                foreach (var item in categories)
                {
                    CategoryTabs tab = new CategoryTabs();
                    tab.Id = item.ID;
                    tab.title = item.TITLE;
                    tab.description = item.DESCRIPTION;
                    categoryTabs.Add(tab);
                }
                string message = ErrorCode.GetCodeMessage(errorCode);
                JsonResult<CategoryTabs> result = new JsonResult<CategoryTabs> { Code = errorCode, Message = message, Rows = categoryTabs.Count, Result = categoryTabs };
                GenerateJson(result);
                return;
            }
            // 结果为空
            else
            {
                string message = ErrorCode.GetCodeMessage(errorCode);
                JsonResult<string> result = new JsonResult<string> { Code = errorCode, Message = message, Rows = 0, Result = null };
                GenerateJson(result);
                return;
            }
        }
        #endregion

        public void AddFile()
        {
            string returnMsg = string.Empty;
            int errorCode = 0;
            // 校验参数
            string[] parametersRequired = { "parentId", "description" };
            if (!CheckParamsRequired(parametersRequired, out errorCode, out returnMsg))
            {
                JsonResult<string> result = new JsonResult<string> { Code = errorCode, Message = returnMsg, Rows = 0, Result = null };
                GenerateJson(result);
                return;
            }
            // 上传文件

        }

        #region 获取指定 project 下的文件列表
        /// <summary>
        /// 获取指定 project 下的文件列表
        /// </summary>
        public void GetFilesByProject()
        {
            string returnMsg = string.Empty;
            int errorCode = 0;
            // 校验参数
            string[] parametersRequired = { "projectId" };
            if (!CheckParamsRequired(parametersRequired, out errorCode, out returnMsg))
            {
                JsonResult<string> result = new JsonResult<string> { Code = errorCode, Message = returnMsg, Rows = 0, Result = null };
                GenerateJson(result);
                return;
            }

            FileHistoryBLL fileHistoryBLL = new FileHistoryBLL();
            List<FileHistory> fileHistories = fileHistoryBLL.GetFileHistories(context, out errorCode);
            // 如果有结果
            if (fileHistories != null && fileHistories.Count() > 0)
            {
                List<FilesForTab> filesForTabs = new List<FilesForTab>();
                foreach (var item in fileHistories)
                {
                    FilesForTab file = new FilesForTab();
                    file.categoryId = item.PARENTID;
                    file.fileHistoryId = item.ID;
                    file.fileName = item.FILENAME;
                    file.fileExt = item.FILEEXTENSION;
                    file.description = item.DESCRIPTION;
                    filesForTabs.Add(file);
                }
                string message = ErrorCode.GetCodeMessage(errorCode);
                JsonResult<FilesForTab> result = new JsonResult<FilesForTab> { Code = errorCode, Message = message, Rows = filesForTabs.Count, Result = filesForTabs };
                GenerateJson(result);
                return;
            }
            // 结果集为空
            else
            {
                string message = ErrorCode.GetCodeMessage(errorCode);
                JsonResult<string> result = new JsonResult<string> { Code = errorCode, Message = message, Rows = 0, Result = null };
                GenerateJson(result);
                return;
            }
        }
        #endregion

        #region 获取操作日志
        public void GetProjectOperationLogs()
        {
            string returnMsg = string.Empty;
            int errorCode = 0;
            // 校验参数
            string[] parametersRequired = { "projectId" };
            if (!CheckParamsRequired(parametersRequired, out errorCode, out returnMsg))
            {
                JsonResult<string> result = new JsonResult<string> { Code = errorCode, Message = returnMsg, Rows = 0, Result = null };
                GenerateJson(result);
                return;
            }

            FileOperationLogBLL foBll = new FileOperationLogBLL();
            DataSet dsFileOperationLogs = foBll.GetFileOperateLogs(context, out errorCode);
            // 获取到结果
            if (dsFileOperationLogs != null && dsFileOperationLogs.Tables.Count > 0 && dsFileOperationLogs.Tables[0].Rows.Count > 0)
            {
                List<SubFileOperateLog> fileOperateLogs = new List<SubFileOperateLog>();
                DataTable dt = dsFileOperationLogs.Tables[0];
                int rowsCount = dt.Rows.Count;
                for (int i = 0; i < rowsCount; i++)
                {
                    SubFileOperateLog file = new SubFileOperateLog();
                    file.operateDate = DateTimeHelper.ConvertDateTimeInt(Convert.ToDateTime(dt.Rows[i]["operateDate"]));
                    file.operateUser = Convert.ToString(dt.Rows[i]["operateUser"]);
                    file.operateContent = Convert.ToString(dt.Rows[i]["operateContent"]);
                    fileOperateLogs.Add(file);
                }
                string message = ErrorCode.GetCodeMessage(errorCode);
                JsonResult<SubFileOperateLog> result = new JsonResult<SubFileOperateLog> { Code = errorCode, Message = message, Rows = fileOperateLogs.Count, Result = fileOperateLogs };
                GenerateJson(result);
                return;
            }
            // 结果集为空
            else
            {
                string message = ErrorCode.GetCodeMessage(errorCode);
                JsonResult<string> result = new JsonResult<string> { Code = errorCode, Message = message, Rows = 0, Result = null };
                GenerateJson(result);
                return;
            }
        }
        #endregion

        #region 文件上传并添加记录到数据库
        public void UpadlodPart()
        {
            string taskid = context.Request["taskid"];
            var receiveFile = context.Request.Files[0];
            var chunknum = context.Request["chunk"];
            var filename = string.Concat(receiveFile.FileName, "_", taskid, "_", chunknum);
            string accFullFilename = Path.Combine(fileUploadTempFolder, filename);

            var accFullFileInfo = new FileInfo(accFullFilename);
            if (accFullFileInfo.Exists && accFullFileInfo.Length == receiveFile.ContentLength)
                return;
            receiveFile.SaveAs(accFullFilename);
        }

        public void UploadSuccess()
        {
            string returnMsg = string.Empty;
            int errorCode = 0;
            string projectId = string.Empty;
            string folder = string.Empty;
            string userId = string.Empty;
            string physicalFileName = string.Empty;
            // 校验参数
            string[] parametersRequired = { "parentId", "description", "taskid", "filename" };
            if (!CheckParamsRequired(parametersRequired, out errorCode, out returnMsg))
            {
                JsonResult<string> resultChkPar = new JsonResult<string> { Code = errorCode, Message = returnMsg, Rows = 0, Result = null };
                GenerateJson(resultChkPar);
                return;
            }
            string taskid = context.Request["taskid"].ToString();
            string filename = context.Request["filename"].ToString();
            string parentId = Convert.ToString(context.Request["parentId"]);
            string description = Convert.ToString(context.Request["description"]);
            FileCategory fileCategory = new FileCategoryBLL().GetModel(parentId);
            if (fileCategory != null)
            {
                projectId = fileCategory.PROJECTID;
                folder = fileCategory.FOLDERNAME;
                bool flag = MergeSplitFile(taskid, projectId, folder, filename, out physicalFileName, out errorCode);
                if (flag)
                {
                    userId = UserProfile.GetInstance()?.ID;
                    bool addFileHisFlag = new FileHistoryBLL().AddFileHistory(parentId, filename, description, userId);
                    if (addFileHisFlag)
                    {
                        string operateTypeName = "上传";
                        int operateTypeKey = new ConfigureBLL().GetConfig(ConfigTypeName.文件操作类型.ToString()).AsEnumerable().Where(item => item["configValue"].ToString() == operateTypeName).Select(item => Convert.ToInt32(item["configKey"])).FirstOrDefault();
                        FileOperationLog fileOperationLog = new FileOperationLog();
                        fileOperationLog.ID = Guid.NewGuid().ToString();
                        fileOperationLog.PROJECTID = projectId;
                        fileOperationLog.EMPLOYEEID = userId;
                        fileOperationLog.FILENAME = filename;
                        fileOperationLog.OPERATETYPE = operateTypeKey;
                        fileOperationLog.OPERATEDATE = DateTime.Now;
                        fileOperationLog.OPERATECONTENT = operateTypeName + filename;
                        fileOperationLog.OPERATEUSER = userId;
                        new FileOperationLogBLL().Add(fileOperationLog);
                        JsonResult<string> resultOprLog = new JsonResult<string> { Code = errorCode, Message = returnMsg, Rows = 0, Result = null };
                        GenerateJson(resultOprLog);
                        return;
                    }
                    // 添加失败时，需删除已上传的文件
                    else
                    {
                        if (File.Exists(physicalFileName))
                        {
                            File.Delete(physicalFileName);
                        }
                        JsonResult<string> resultAddDb2 = new JsonResult<string> { Code = errorCode, Message = "数据库记录添加失败", Rows = 0, Result = null };
                        GenerateJson(resultAddDb2);
                        return;
                    }
                }
                returnMsg = ErrorCode.GetCodeMessage(errorCode);
                JsonResult<string> resultAddDb = new JsonResult<string> { Code = errorCode, Message = returnMsg, Rows = 0, Result = null };
                GenerateJson(resultAddDb);
                return;
            }
            JsonResult<string> result = new JsonResult<string> { Code = errorCode, Message = "未找到 projectId 对应的记录", Rows = 0, Result = null };
            GenerateJson(result);
            return;
        }

        /// <summary>
        /// 合并文件
        /// </summary>
        /// <param name="taskid"></param>
        /// <param name="projectId">项目ID</param>
        /// <param name="title">文件保存的直接父目录名，如修改1、修改1完成</param>
        /// <param name="filename">文件名</param>
        /// <param name="errorCode">错误码</param>
        /// <returns>是否成功</returns>
        private bool MergeSplitFile(string taskid, string projectId, string title, string filename, out string physicalFileName, out int errorCode)
        {
            string actPath = string.Empty;
            physicalFileName = string.Empty;
            bool flag = new FileCategoryBLL().GetFilePathByProjectId(projectId, title, false, out actPath, out errorCode);
            if (Directory.Exists(actPath))
            {
                string actFileName = Path.Combine(actPath, filename);
                if (File.Exists(actFileName))
                {
                    errorCode = 6007;
                    return false;
                }
                physicalFileName = actFileName;
                DirectoryInfo savePathInfo = new DirectoryInfo(fileUploadTempFolder);
                var allSplitFiles = savePathInfo.EnumerateFiles().Where(file => file.Name.StartsWith(filename) && file.Name.Contains(taskid));
                using (FileStream fileStream = File.Create(actFileName, 10 * 1024 * 1024))
                {
                    foreach (var file in allSplitFiles)
                    {
                        using (FileStream splitFile = File.Open(file.FullName, FileMode.Open))
                        {
                            splitFile.CopyTo(fileStream);
                        }
                        file.Delete();
                    }
                }
                return true;
            }
            errorCode = 6501;
            return false;
        }
        #endregion

        #region 删除文件
        public void DeleteFile()
        {
            string returnMsg = string.Empty;
            int errorCode = 0;
            // 校验参数
            string[] parametersRequired = { "fileHistoryId" };
            if (!CheckParamsRequired(parametersRequired, out errorCode, out returnMsg))
            {
                JsonResult<string> result = new JsonResult<string> { Code = errorCode, Message = returnMsg, Rows = 0, Result = null };
                GenerateJson(result);
                return;
            }

            string fileHistoryId = context.Request["fileHistoryId"];
            // 根据 ID 获取到 fileHistory 对象
            FileHistory fileHistory = new FileHistoryBLL().GetModel(fileHistoryId);
            if (fileHistoryId != null)
            {
                fileHistory.ISDELETED = true;
                bool delFlag = new FileHistoryBLL().Update(fileHistory);
                // 逻辑删除成功
                if (delFlag)
                {
                    // 记录操作日志
                    string operateTypeName = "删除";
                    int operateTypeKey = new ConfigureBLL().GetConfig(ConfigTypeName.文件操作类型.ToString()).AsEnumerable().Where(item => item["configValue"].ToString() == operateTypeName).Select(item => Convert.ToInt32(item["configKey"])).FirstOrDefault();
                    FileOperationLog fileOperationLog = new FileOperationLog();
                    fileOperationLog.ID = Guid.NewGuid().ToString();
                    fileOperationLog.PROJECTID = fileHistory.PARENTID;
                    fileOperationLog.EMPLOYEEID = UserProfile.GetInstance()?.ID;
                    fileOperationLog.FILENAME = fileHistory.FILENAME;
                    fileOperationLog.OPERATETYPE = operateTypeKey;
                    fileOperationLog.OPERATEDATE = DateTime.Now;
                    fileOperationLog.OPERATECONTENT = operateTypeName + fileHistory.FILENAME;
                    fileOperationLog.OPERATEUSER = UserProfile.GetInstance()?.ID;
                    new FileOperationLogBLL().Add(fileOperationLog);
                }
                else
                {
                    errorCode = 1;
                }
                JsonResult<string> result = new JsonResult<string> { Code = errorCode, Message = returnMsg, Rows = 0, Result = null };
                GenerateJson(result);
                return;
            }
            JsonResult<string> resultFinal = new JsonResult<string> { Code = errorCode, Message = returnMsg, Rows = 0, Result = null };
            GenerateJson(resultFinal);
            return;
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}