using FileZillaServerBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb
{
    public partial class ShopAddEdit : System.Web.UI.Page
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
            DataTable dtShop = new ConfigureBLL().GetShopKeys();
            string shopId = Request.QueryString["shopid"];
            if (!string.IsNullOrEmpty(shopId))
            {
                txtShopId.Text = shopId;
                txtShopId.Enabled = false;
                txtShopName.Text = dtShop.AsEnumerable().Where(item => item.Field<string>("shopid") == shopId).Select(t => t.Field<string>("configValue")).FirstOrDefault();
                txtShopName.Enabled = false;
                txtAccessKey.Text= dtShop.AsEnumerable().Where(item => item.Field<string>("shopid") == shopId).Select(t => t.Field<string>("AccessKey")).FirstOrDefault();
                txtSecretKey.Text= dtShop.AsEnumerable().Where(item => item.Field<string>("shopid") == shopId).Select(t => t.Field<string>("SecretKey")).FirstOrDefault();
            }            
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            string shopId = Request.QueryString["shopid"];
            string accessKey = txtAccessKey.Text.Trim();
            string secretKey = txtSecretKey.Text.Trim();
            if (!string.IsNullOrEmpty(shopId))
            {
                if (new ConfigureBLL().UpdateShopKeys(shopId, accessKey, secretKey))
                {
                    // Update cache
                    UpdateConfigCache();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('编辑成功！');", true);
                    return;
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('编辑失败！');", true);
                    return;
                }
            }
            else
            {
                shopId = txtShopId.Text.Trim();
                string shopName = txtShopName.Text.Trim();
                if (new ConfigureBLL().InsertShop(shopId, shopName, accessKey, secretKey))
                {
                    // Update cache
                    UpdateConfigCache();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('添加成功！');", true);
                    return;
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), Guid.NewGuid().ToString(), "alert('添加失败！');", true);
                    return;
                }
            }
        }

        private void UpdateConfigCache()
        {
            System.Data.DataTable dtConfig = null;
            if (Cache["dtConfig"] != null)
            {
                HttpRuntime.Cache.Remove("dtConfig");
            }
            dtConfig = new ConfigureBLL().GetConfig();
            Cache.Insert("dtConfig", dtConfig, null, DateTime.Now.AddHours(1.5), System.Web.Caching.Cache.NoSlidingExpiration);
        }
    }
}