using FileZillaServerProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb
{
    public partial class WebPageRedirect : WebPageHelper
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //UserProfile user = UserProfile.GetInstance();
                RepeaterDataBind();
            }
        }

        protected void RepeaterDataBind()
        {
            rptMenu.DataSource = UserProfile.GetInstance().Menu.Where(d =>d.ParentID == string.Empty);
            rptMenu.DataBind();
        }
    }
}