using FileZillaServerBLL;
using FileZillaServerModel;
using Jil;
using System;
using System.Collections.Generic;
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
            string projectId = context.Request["projectId"];
            string categoryId = context.Request["categoryId"];
            string description = context.Request["description"];
            FileCategory fileCategory = new FileCategory();
            FileCategoryBLL fcBll = new FileCategoryBLL();
            fileCategory.ID = Guid.NewGuid().ToString();
            fileCategory.CATEGORY = categoryId;
            fileCategory.DESCRIPTION = description;
            fileCategory.TITLE = "";
            fileCategory.FOLDERNAME = "";
            if (fcBll.Add(fileCategory))
            {
                JsonResult<string> result = new JsonResult<string> { Code = 0, Message = "add success", Rows = 1, Result = null };
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