using FileZillaServerBLL;
using FileZillaServerCommonHelper;
using FileZillaServerProfile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb
{
    public partial class PasswordModify : WebPageHelper
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 确定按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOK_Click(object sender, EventArgs e)
        {
            ValidatePassword();
        }

        /// <summary>
        /// 验证密码输入
        /// </summary>
        private void ValidatePassword()
        {
            EmployeeBLL eBll = new EmployeeBLL();
            //用户ID
            string ID = UserProfile.GetInstance().ID;
            //员工编号
            string employeeNO = UserProfile.GetInstance().EmployeeNO;
            //加密密码
            string passWord = AES.Encrypt(txtFormerPwd.Text.Trim(), "0512000000000512");
            //得到User
            DataTable dtUser = eBll.GetUser(employeeNO, passWord);
            //未找到用户
            if (dtUser == null || dtUser.Rows.Count == 0)
            {
                rfvFormerPwd.ErrorMessage = "原密码不正确";
                rfvFormerPwd.IsValid = false;
            }
            //找到用户
            else
            {
                string newPassword = txtNewPwdAgain.Text.Trim();
                //密码加密
                newPassword = AES.Encrypt(newPassword, "0512000000000512");
                //加密后更新
                bool updateResult = eBll.UpdatePassword(ID, newPassword);
                string alertMsg = string.Empty;
                //修改成功
                if (updateResult)
                {
                    alertMsg = "密码修改成功，请使用新密码重新登录！";
                }
                //修改失败
                else
                {
                    alertMsg = "密码修改失败！";
                }
                //弹出提示
                ExecuteScript(string.Format("AlertDialog('{0}','{1}');", alertMsg, updateResult));
            }
        }
    }
}