using FileZillaServerProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb.UserControl
{
    public partial class SiteMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMenu();
            }
        }

        protected void LoadMenu()
        {
            UserProfile up = UserProfile.GetInstance();
            if (up == null)
            {
                return;
            }
            rptMenu.DataSource = up.Menu.Where(d => d.ParentID == string.Empty);
            rptMenu.DataBind();
        }
    }
}