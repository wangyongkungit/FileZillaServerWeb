using FileZillaServerProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MenuListDataBind();
        }

        public List<FileZillaServerProfile.Menu> lstMenu { get; set; }

        protected void MenuListDataBind()
        {
            lstMenu = UserProfile.GetInstance().Menu.Where(d => d.ParentID == string.Empty).ToList();
        }
    }
}