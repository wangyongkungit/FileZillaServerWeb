using FileZillaServerBLL;
using FileZillaServerCommonHelper;
using FileZillaServerModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FileZillaServerWeb
{
    public partial class EmployeeAdd : WebPageHelper
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["UserId"]))
                {
                    Page.Title = "员工添加";
                    EmployeeNoCreate();
                }
                else
                {
                    lblNewOrEdit.Text = "编辑";
                    Page.Title = "员工编辑";
                    DataLoad();
                }
            }
        }

        /// <summary>
        /// 加载员工信息
        /// </summary>
        private void DataLoad()
        {
            string userId = Request.QueryString["UserId"];
            btnOK.Text = "编辑";
            try
            {
                Employee emp = eBll.GetModel(userId);
                if (emp != null)
                {
                    //为0是总经办
                    if (emp.TYPE == 0)
                    {
                        rdbGMO.Checked = true;
                    }
                    //1是客服
                    else if (emp.TYPE == 1)
                    {
                        rdbCustomerService.Checked = true;
                    }
                    //2是造价员
                    else if (emp.TYPE == 2)
                    {
                        rdbEmployee.Checked = true;
                    }
                    txtEmployeeNo.Text = emp.EMPLOYEENO;
                    txtEmployeeNo.ReadOnly = true;
                    txtName.Text = emp.NAME;
                    txtMobilePhone.Text = emp.MOBILEPHONE;
                    if (emp.SEX)
                    {
                        rdbMale.Checked = true;
                    }
                    else
                    {
                        rdbFemale.Checked = true;
                    }
                    txtBirthDate.Text = emp.BIRTHDATE == null ? string.Empty : emp.BIRTHDATE.Value.ToString("yyyy-MM-dd");
                    if (emp.ISBRANCHLEADER)
                    {
                        rblIsBranchLeader.SelectedValue = "1";
                    }
                    //如果钉钉UserId未关联，则在相应文本框中进行提示
                    if (string.IsNullOrEmpty(emp.DINGTALKUSERID))
                    {
                        txtDingtalkUserId.Text = string.Empty;
                        txtDingtalkUserId.ForeColor = System.Drawing.Color.Red;
                        txtDingtalkUserId.Attributes["placeholder"] = "钉钉UserId尚未关联，请尽快关联。";
                    }
                    //已关联钉钉，则显示钉钉UserId并隐藏关联按钮
                    else
                    {
                        txtDingtalkUserId.Text = emp.DINGTALKUSERID;
                        txtDingtalkUserId.ReadOnly = true;
                        btnAddDingtalkUserId.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message);
            }
        }

        EmployeeBLL eBll = new EmployeeBLL();

        #region 生成编号
        /// <summary>
        /// 生成编号
        /// </summary>
        protected void EmployeeNoCreate()
        {
            //用户类型，0是普通员工，1是客服
            string userType = string.Empty;
            string prefix = string.Empty;
            //总经办
            if (rdbGMO.Checked)
            {
                userType = "0";
                prefix = "A";
            }
            //客服
            else if (rdbCustomerService.Checked)
            {
                userType = "1";
                prefix = "B";
            }
            //造价员
            else if (rdbEmployee.Checked)
            {
                userType = "2";
                prefix = "C";
            }
            else
            {
                return;
            }
            string maxNo = eBll.GetMaxEmployeeNO(userType);
            try
            {
                //进行编号
                //if (userType == "0")
                //{
                    if (Convert.ToInt16(maxNo) == 999)
                    {
                        LogHelper.WriteLine("编号已溢出。");
                        txtEmployeeNo.Text = "3位数员工编号已溢出。";
                        btnOK.Enabled = false;
                        btnOK.Style["cursor"] = "not-allowed";
                        return;
                    }
                    //获取相应编号的下一个编号
                    maxNo = (Convert.ToInt16(maxNo) + 1).ToString().PadLeft(3, '0');
                    txtEmployeeNo.Text = prefix+ maxNo;
                //}
                ////客服是用英文字母编号的
                //else
                //{
                //    if (maxNo == "z")
                //    {
                //        LogHelper.WriteLine("客服编号已溢出。");
                //        txtEmployeeNo.Text = "按26个英文字母编排的客服编号已溢出。";
                //        btnOK.Enabled = false;
                //        btnOK.Style["cursor"] = "not-allowed";
                //        return;
                //    }
                //    //获取相应字母的下一个编号
                //    byte[] array = System.Text.Encoding.ASCII.GetBytes(maxNo);
                //    int asciiCode = (int)array[0];
                //    byte[] arrayNew = new byte[1];
                //    arrayNew[0] = (byte)(Convert.ToInt32(asciiCode + 1));
                //    maxNo = Convert.ToString(System.Text.Encoding.ASCII.GetString(arrayNew));
                //    txtEmployeeNo.Text = maxNo;
                //}
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine("员工编号加载出错。" + ex.Message);
                txtEmployeeNo.Text = string.Empty;
                txtEmployeeNo.ForeColor = System.Drawing.Color.Red;
                txtEmployeeNo.Attributes["placeholder"] = "自动编号出错，请联系系统运维人员或手工输入编号。";
            }
        }
        #endregion

        /// <summary>
        /// 单选按钮发生改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rdbUserType_CheckedChanged(object sender, EventArgs e)
        {
            EmployeeNoCreate();
        }

        #region 员工添加和编辑
        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Request.QueryString["UserId"]))
            {
                AddEmployee();
            }
            else
            {
                EditEmployee();
            }
        }

        /// <summary>
        /// 添加员工
        /// </summary>
        private void AddEmployee()
        {
            string employeeNO = txtEmployeeNo.Text.Trim();  //员工编号
            try
            {
                //判断编号是否已经使用
                bool isExist = eBll.IsExist(employeeNO);
                if (isExist)
                {
                    ExecuteScript("AlertDialog('员工编号已存在！', null);");
                    return;
                }
                string name = txtName.Text.Trim();              //员工姓名
                int gender = 0;                                 //员工性别
                if (rdbFemale.Checked)
                {
                    gender = 1;
                }
                //出生日期
                string birthdate = txtBirthDate.Text.Trim();

                Employee emp = new Employee();
                emp.ID = Guid.NewGuid().ToString();
                emp.EMPLOYEENO = employeeNO;
                //密码加密
                emp.PASSWORD = AES.Encrypt("666666", "0512000000000512");
                //姓名
                emp.NAME = name == null ? null : name;
                //性别
                emp.SEX = gender == 0;
                if (!string.IsNullOrEmpty(birthdate))
                {
                    emp.BIRTHDATE = Convert.ToDateTime(birthdate);
                }
                //手机号码
                emp.MOBILEPHONE = txtMobilePhone.Text.Trim();
                //员工类型，0：总经办，1：客服，2：造价员
                decimal userType = 0;
                //总经办
                if (rdbGMO.Checked)
                {
                    userType = 0;
                }
                //客服
                else if (rdbCustomerService.Checked)
                {
                    userType = 1;
                }
                //造价员
                else if (rdbEmployee.Checked)
                {
                    userType = 2;
                }
                emp.TYPE = userType;
                emp.ISBRANCHLEADER = rblIsBranchLeader.SelectedValue == "1";
                //启用状态
                emp.AVAILABLE = 1;
                bool flag = eBll.Add(emp);
                //本系统添加成功
                if (flag)
                {
                    // 2018-05-19 添加
                    // 添加员工的任务分成配置
                    EmployeeProportionBLL epBll = new EmployeeProportionBLL();
                    EmployeeProportion empProportion = new EmployeeProportion();
                    empProportion.ID = Guid.NewGuid().ToString();
                    empProportion.EMPLOYEEID = emp.ID;
                    empProportion.PROPORTION = 0.3m;
                    empProportion.PARENTEMPLOYEEID = null;
                    empProportion.ISBRANCHLEADER = emp.ISBRANCHLEADER;
                    epBll.Add(empProportion);
                    // 任务分成配置结束

                    // 2018-05-20 添加
                    // 添加员工的默认账户
                    EmployeeAccount empAcct = new EmployeeAccount();
                    empAcct.ID = Guid.NewGuid().ToString();
                    empAcct.EMPLOYEEID = emp.ID;
                    empAcct.AMOUNT = 0m;
                    empAcct.PAIDAMOUNT = 0m;
                    empAcct.SURPLUSAMOUNT = 0m;
                    empAcct.OTHERSAMOUNT = 0m;
                    empAcct.CREATEDATE = DateTime.Now;
                    empAcct.LASTUPDATEDATE = DateTime.Now;
                    EmployeeAccountBLL empAcctBll = new EmployeeAccountBLL();
                    empAcctBll.Add(empAcct);
                    // 员工账户结束
                    #region 调用钉钉接口，添加员工到钉钉
                    //如果选择了钉钉同步创建
                    if (Request.Form["sync"] == "rdbSync")
                    {
                        string accessTokenResult = DingTalkHelper.GetAccessToken();
                        JObject jObj = JObject.Parse(accessTokenResult);
                        string errcode = jObj["errcode"].ToString();
                        string accessToken = string.Empty;
                        if (errcode == "0")
                        {
                            accessToken = jObj["access_token"].ToString();
                            string postUrl = string.Format("https://oapi.dingtalk.com/user/create?access_token={0}", accessToken);
                            //岗位
                            string position = string.Empty;
                            //钉钉系统中对应的部门ID
                            string departmentid = string.Empty;
                            //总经办
                            if (rdbGMO.Checked)
                            {
                                position = rdbGMO.Text;
                                departmentid = "37037510";
                            }
                            //客服
                            else if (rdbCustomerService.Checked)
                            {
                                position = rdbCustomerService.Text;
                                departmentid = "36958636";
                            }
                            //造价员
                            else if (rdbEmployee.Checked)
                            {
                                position = rdbEmployee.Text;
                                departmentid = "36961466";
                            }

                            //\"orderInDepts\":\"{1:10}\",  \"remark\":\"\",    \"tel\":\"010-12333\",  \"workPlace\":\"\",  \"isHide\":false,  \"isSenior\":false
                            string param = "{\"name\":\"" + emp.NAME + "\",    \"department\":[" + departmentid + "],  \"position\":\"" + position + "\",  \"mobile\":\"" + emp.MOBILEPHONE +
                                "\",  \"email\":\"" + emp.EMAIL + "\",  \"jobnumber\":\"" + emp.EMPLOYEENO + "\",}";
                            object userCreateResult = WebServiceHelper.Post(postUrl, param);
                            JObject jCreateResult = JObject.Parse(userCreateResult.ToString());
                            //返回码
                            errcode = jCreateResult["errcode"].ToString();
                            //返回消息
                            string errmsg = jCreateResult["errmsg"].ToString();
                            //返回码为0，成功
                            if (errcode == "0")
                            {
                                string dingTalkUserId = jCreateResult["userid"].ToString();
                                emp.DINGTALKUSERID = dingTalkUserId;
                                try
                                {
                                    //关联钉钉UserId
                                    bool uptFlag = eBll.Update(emp);
                                    //关联成功
                                    if (uptFlag)
                                    {
                                        //弹出提示并刷新当前页面
                                        ExecuteScript("alert('添加成功！');window.location.href='EmployeeAdd?UserId=" + emp.ID + "'");
                                    }
                                    else
                                    {
                                        ExecuteScript("AlertDialog('关联钉钉UserId失败！', null);");
                                    }
                                }
                                //本地OA更新钉钉UserId失败
                                catch (Exception ex)
                                {
                                    LogHelper.WriteLine("自动关联钉钉UserId失败。" + ex.Message + ex.StackTrace);
                                    ExecuteScript("AlertDialog('操作失败！', null)");
                                }
                            }
                            else
                            {
                                LogHelper.WriteLine("用户在OA系统创建成功，但在钉钉系统创建失败，原因：" + errmsg);
                                //ExecuteScript("AlertDialog('OA系统已成功创建用户，但该用户在钉钉系统中创建失败，请人工在钉钉系统中添加。<br />钉钉系统中添加成功后，需要将其系统中的UserId填写并关联到本系统中。', null)");
                                ExecuteScript("alert('OA系统已成功创建用户，但该用户在钉钉系统中创建失败，请人工在钉钉系统中添加。钉钉系统中添加成功后，需要将其系统中的UserId填写并关联到本系统中。');window.location.href='EmployeeAdd?UserId=" + emp.ID + "';");
                            }
                        }
                        else
                        {
                            ExecuteScript("AlertDialog('用户已在本系统中成功创建，但获取钉钉token失败，并未在钉钉系统中成功添加用户，请人工添加！', null)");
                        }
                    }
                    else
                    {
                        ExecuteScript("alert('本系统添加成功但尚未关联钉钉UserId，请尽快关联！');window.location.href='EmployeeAdd?UserId=" + emp.ID + "';");
                    }
                    #endregion
                }
                //添加失败
                else
                {
                    ExecuteScript("AlertDialog('添加失败！', null)");
                }
            }
            catch (Exception ex)
            {
                string exMsg = ex.Message;
                LogHelper.WriteLine(string.Format("员工添加出错。" + exMsg));  //写入文本日志
                string script = string.Format("AlertDialog('服务器错误，请联系管理员！<p>{0}</p>'), null", exMsg);
                ExecuteScript(script);
            }
        }

        /// <summary>
        /// 员工编辑
        /// </summary>
        protected void EditEmployee()
        {
            Employee emp = new Employee();
            string userId = Request.QueryString["UserId"];
            emp = eBll.GetModel(userId);
            if (emp != null)
            {
                string name = txtName.Text.Trim();
                string mobilePhone = txtMobilePhone.Text.Trim();
                string email = txtEmail.Text.Trim();
                emp.NAME = name;
                emp.MOBILEPHONE = mobilePhone;
                emp.EMAIL = email;
                emp.ISBRANCHLEADER = rblIsBranchLeader.SelectedValue == "1";
                bool editFlag = eBll.Update(emp);
                if (editFlag)
                {
                    ExecuteScript("alert('编辑成功！');window.location.href='EmployeeAdd.aspx?UserId=" + emp.ID + "';");
                }
                else
                {
                    LogHelper.WriteLine("员工编辑失败,编号为：" + emp.EMPLOYEENO);
                }
            }
        }

        #endregion

        protected void btnInvokeWebService_Click(object sender, EventArgs e)
        {
            Employee emp=new Employee();
            emp.NAME = "李泓霖";
            emp.EMPLOYEENO = "009";
            emp.MOBILEPHONE = "18862323333";
            string accessTokenResult = DingTalkHelper.GetAccessToken();
            JObject jObj = JObject.Parse(accessTokenResult);
            string errcode = jObj["errcode"].ToString();
            string accessToken = string.Empty;
            if (errcode == "0")
            {
                accessToken = jObj["access_token"].ToString();
                //object[] param ={ (object)"{\"name\":\"" + emp.NAME + "\",\"orderInDepts\":\"{1:10,2:20}\",\"department\":[1,2],\"position\":\"产品经理\",\"mobile\":\"" + emp.MOBILEPHONE +
                //    "\",\"tel\":\"010-12333\",\"workPlace\":\"\",\"remark\":\"\",\"email\":\"\",\"jobnumber\":\"" + emp.EMPLOYEENO + "\",\"isHide\":false,\"isSenior\":false}"};
                //object userCreateResult = WebServiceHelper.InvokeWebService(postUrl, null, param);
                //JObject jCreateResult = JObject.Parse(userCreateResult.ToString());
                //errcode = jCreateResult["errcode"].ToString();
                //if (errcode == "0")
                //{
                //    string dingTalkUserId = jCreateResult["userid"].ToString();
                //}
                //else
                //{
 
                //}

                string position = string.Empty;
                //总经办
                if (rdbGMO.Checked)
                {
                    position = rdbGMO.Text;
                }
                //客服
                else if (rdbCustomerService.Checked)
                {
                    position = rdbCustomerService.Text;
                }
                //造价员
                else if (rdbEmployee.Checked)
                {
                    position = rdbEmployee.Text;
                }

                string postUrl = string.Format("https://oapi.dingtalk.com/user/create?access_token={0}", accessToken);
                string param = "{\"name\":\"" + emp.NAME + "\",  \"orderInDepts\":\"{1:10}\",  \"department\":[1],  \"position\":\"" + position + "\",  \"mobile\":\"" + emp.MOBILEPHONE +
                    "\",  \"tel\":\"010-12333\",  \"workPlace\":\"\",  \"remark\":\"\",  \"email\":\"\",  \"jobnumber\":\"" + emp.EMPLOYEENO + "\",  \"isHide\":false,  \"isSenior\":false}";
                object userCreateResult = WebServiceHelper.Post(postUrl, param);
                JObject jCreateResult = JObject.Parse(userCreateResult.ToString());
                errcode = jCreateResult["errcode"].ToString();
                if (errcode == "0")
                {
                    string dingTalkUserId = jCreateResult["userid"].ToString();
                    emp.DINGTALKUSERID = dingTalkUserId;
                    try
                    {
                        //更新钉钉UserId
                        bool uptFlag = eBll.Update(emp);
                        //更新成功
                        if (uptFlag)
                        {
                            ExecuteScript("AlertDialog('成功关联钉钉UserId！', null)");
                            return;
                        }
                        //更新失败
                        else
                        {
                            ExecuteScript("AlertDialog('关联钉钉UserId失败！', null)");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLine("关联钉钉UserId失败。" + ex.Message + ex.StackTrace);
                        ExecuteScript("AlertDialog('操作失败！', null)");
                        return;
                    }
                }
                else
                {

                }
            }
            else
            {
 
            }
        }

        /// <summary>
        /// 关联钉钉UserId按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddDingtalkUserId_Click(object sender, EventArgs e)
        {
            AddDingtalkUserId();
        }

        /// <summary>
        /// 关联钉钉UserId
        /// </summary>
        protected void AddDingtalkUserId()
        {
            string dingtalkUserId = txtDingtalkUserId.Text.Trim();
            //未填写钉钉UserId
            if (string.IsNullOrEmpty(dingtalkUserId))
            {
                ExecuteScript("AlertDialog('请填写钉钉UserId！(2)', null)");
                return;
            }
            string userId = Request.QueryString["UserId"];
            //如果userId为空，直接返回
            if (string.IsNullOrEmpty(userId))
            {
                return;
            }
            Employee emp = eBll.GetModel(userId);
            //emp为null，直接返回
            if (emp == null)
            {
                return;
            }
            emp.DINGTALKUSERID = dingtalkUserId;
            try
            {
                //更新钉钉UserId
                bool uptFlag = eBll.Update(emp);
                //更新成功，刷新当前页面
                if (uptFlag)
                {
                    string rawUrl = Request.RawUrl;
                    ExecuteScript("alert('成功关联钉钉UserId！');window.location.href='" + rawUrl + "';");
                    //ExecuteScript("AlertDialog('成功关联钉钉UserId！', null)");
                    return;
                }
                //更新失败
                else
                {
                    ExecuteScript("AlertDialog('关联钉钉UserId失败！', null)");
                    LogHelper.WriteLine("关联钉钉UserId失败。");
                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine("关联钉钉UserId失败。" + ex.Message + ex.StackTrace);
                ExecuteScript("AlertDialog('操作失败！', null)");
                return;
            }
        }
    }
}