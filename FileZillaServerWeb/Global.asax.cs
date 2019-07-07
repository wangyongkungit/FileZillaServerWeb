﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using FileZillaServerWeb;

namespace FileZillaServerWeb
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            //BundleTable.EnableOptimizations = false;
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            //routeCollection.MapPageRoute("RouteForCustomer", "Customer/{Id}", "~/Customer.aspx")
            RouteTable.Routes.MapPageRoute("empHome1", "h/{*employeeid}", "~/Pages/Account/employeeHome.aspx");
            //RouteTable.Routes.MapPageRoute("empHome2", "home/employeeId.html", "~/Pages/Account/employeeHome.aspx");
        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码

        }
    }
}
