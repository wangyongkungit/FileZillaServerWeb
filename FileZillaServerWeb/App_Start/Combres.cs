[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(FileZillaServerWeb.App_Start.Combres), "PreStart")]
namespace FileZillaServerWeb.App_Start {
	using System.Web.Routing;
	using global::Combres;
	
    public static class Combres {
        public static void PreStart() {
            RouteTable.Routes.AddCombresRoute("Combres");
        }
    }
}