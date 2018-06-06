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
            string[] parametersRequired = { "projectId", "categoryId"};
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
                string message = ErrorCode.GetCodeMessage(errorCode);
                List<SubTabs> subTabs = new List<SubTabs>();
                for (int i = 0; i < fileCategories.Rows.Count; i++)
                {
                    SubTabs sub = new SubTabs();
                    sub.Id = fileCategories.Rows[i]["ID"].ToString();
                    sub.hasParent = fileCategories.Rows[i]["hasParent"].ToString() == "1";
                    sub.categoryId = fileCategories.Rows[i]["categoryId"].ToString();
                    sub.Title = fileCategories.Rows[i]["title"].ToString();
                    subTabs.Add(sub);
                }
                JsonResult<SubTabs> result = new JsonResult<SubTabs> { Code = errorCode, Message = message, Rows = 0, Result = subTabs };
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