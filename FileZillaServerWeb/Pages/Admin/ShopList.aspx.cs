using FileZillaServerBLL;
using FileZillaServerCommonHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb
{
    public partial class ShopList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        protected void LoadData()
        {
            DataTable dtShops = new ConfigureBLL().GetShopKeys();
            gvShop.DataSource = dtShops;
            gvShop.DataBind();
        }

        protected void gvShop_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Disable")
            {
                string ID = e.CommandArgument.ToString();
                bool flag = new ConfigureBLL().DisableShop(ID);
                if (flag)
                {
                    LoadData();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('更新成功！');", true);
                    return;
                }
                ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('更新失败！');", true);
                return;
            }
        }
    }
}