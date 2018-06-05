using FileZillaServerBLL;
using FileZillaServerModel;
using FileZillaServerModel.Interface;
using Jil;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public void AddFileCategory()
        {
            string returnMsg = string.Empty;
            int errCode = 0;
            string[] parametersRequired = { "projectId", "categoryId", "description" };
            if (!CheckParamsRequired(parametersRequired, out returnMsg))
            {
                context.Response.Write(returnMsg);
                return;
            }
            FileCategoryBLL fcBll = new FileCategoryBLL();
            if (fcBll.AddFileCategory(context, out errCode))
            {
                string message = errCode == 0 ? "添加成功" : ErrorCode.GetCodeMessage(errCode);
                JsonResult<string> result = new JsonResult<string> { Code = 0, Message = message, Rows = 1, Result = null };
                context.Response.Write(JSON.Serialize(result));
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