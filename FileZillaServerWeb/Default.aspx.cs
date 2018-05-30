using FileZillaServerCommonHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            string result = AES.Encrypt("666666", "0512000000000512");
            string jiemi = AES.Decrypt(result, "0512000000000512");
        }
    }
}