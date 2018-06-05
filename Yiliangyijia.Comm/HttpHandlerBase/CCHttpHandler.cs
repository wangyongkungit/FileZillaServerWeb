using FileZillaServerProfile;
using Jil;
using System;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Yiliangyijia.Comm;

/// <summary>
/// CCHttpHandler 的摘要说明
/// </summary>
public class CCHttpHandler : IHttpHandler, IRequiresSessionState
{
    public CCHttpHandler()
    {
    }

    public HttpContext context { get; set; }

    public UserProfile objUserInfo
    {
        get
        {
            if (HttpContext.Current.Session["Profile"] != null)
                return (UserProfile)HttpContext.Current.Session["Profile"];
            HttpContext.Current.Session["Profile"] = (object)new UserProfile();
            return (UserProfile)HttpContext.Current.Session["Profile"];
        }
        set
        {
            HttpContext.Current.Session["Profile"] = (object)value;
        }
    }

    bool IHttpHandler.IsReusable => throw new NotImplementedException();

    void IHttpHandler.ProcessRequest(HttpContext context)
    {
        this.context = context;
        Type type = this.GetType();
        try
        {
            if (context.Request["FuncName"] == null)
                return;
            string funcName = context.Request["FuncName"].ToString();
            var methodInfo = type.GetMethod(funcName);
            if (methodInfo != null)
            {
                methodInfo.Invoke(this, null);
            }
            else
            {
                context.Response.Write("No Such Method");
            }
        }
        catch (Exception ec)
        {
            Console.WriteLine(ec.ToString());
        }
    }

    public void GenerateJson()
    {
        context.Response.Write(@"{""FirstName"":""Toby"", ""LastName"":""Yang"", 
               ""Blog"":""y976362357@163.com""}");
    }

    public void GenerateJson(object result)
    {
        string callback = context.Request["callback"];
        if (String.IsNullOrEmpty(callback))
        {
            context.Response.Write(JSON.Serialize(result));
        }
        else
        {
            context.Response.Write(callback + "(" + JSON.Serialize(result) + ")");
        }
    }

    public bool CheckProjectid()
    {
        var projectid = context.Request["projectid"].ToString();
        if (String.IsNullOrEmpty(projectid))
        {

            context.Response.Write("no projectid");
            return false;
        }
        return true;
    }

    /// <summary>
    /// 校验必输参数
    /// </summary>
    /// <param name="paramsRequired">必输参数数组</param>
    /// <param name="returnMsg">返回消息</param>
    /// <returns>是否通过校验</returns>
    public bool CheckParamsRequired(string[] paramsRequired, out string returnMsg)
    {
        returnMsg = string.Empty;
        string[] parametersGet = context.Request.Params.AllKeys;
        string param = paramsRequired.Except(parametersGet).FirstOrDefault();
        if (!string.IsNullOrEmpty(param))
        {
            returnMsg = string.Format("{0}:{1}", ErrorCode.GetCodeMessage(2004), param);
            return false;
        }
        return true;
    }

    public bool CheckProjectId(string projectId)
    {
        if (true) // select exist, prjBll
        {
            return true;
        }
        return false;
    }

    public bool CheckFileCategoryKey(string fileCategoryKey)
    {
        if (true) //
        {
            return true;
        }
        return false;
    }
}