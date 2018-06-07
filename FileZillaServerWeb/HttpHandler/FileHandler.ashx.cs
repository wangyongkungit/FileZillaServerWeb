using FileZillaServerBLL;
using FileZillaServerModel;
using FileZillaServerModel.Interface;
using Jil;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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

        /// <summary>
        /// 获取指定 tab 下的文件列表
        /// </summary>
        public void GetFilesByTab()
        {
            string returnMsg = string.Empty;
            int errorCode = 0;
            // 校验参数
            string[] parametersRequired = { "categoryId" };
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
                    file.fileName = item.FILENAME;
                    file.filePath = item.FILEFULLNAME;
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
            List<FileOperationLog> fileOperationLogs = foBll.GetFileOperateLogs(context, out errorCode);
            // 获取到结果
            if (fileOperationLogs != null && fileOperationLogs.Count() > 0)
            {
                List<SubFileOperateLog> fileOperateLogs = new List<SubFileOperateLog>();
                foreach (var item in fileOperationLogs)
                {
                    SubFileOperateLog file = new SubFileOperateLog();
                    file.operateDate = DateTimeHelper.ConvertDateTimeInt(item.OPERATEDATE ?? DateTime.MinValue);
                    file.operateUser = item.OPERATEUSER;
                    file.operateContent = item.OPERATECONTENT;
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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}