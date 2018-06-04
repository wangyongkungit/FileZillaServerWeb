using FileZillaServerProfile;
using System;
using System.Web;
using System.Web.SessionState;

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

}