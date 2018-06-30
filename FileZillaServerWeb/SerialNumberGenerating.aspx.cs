using FileZillaServerBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FileZillaServerCommonHelper;
using FileZillaServerModel;
using FileZillaServerProfile;
using System.IO;
using System.Text;
using System.Configuration;
using FileZillaServerDAL;
using System.Runtime.InteropServices;

namespace FileZillaServerWeb
{
    /// <summary>
    /// 描    述：任务生成
    /// 作    者：Yongkun Wang
    /// 创建时间：2017-03-02
    /// 修改历史：2017-03-02 Yongkun Wang 创建
    /// </summary>
    public partial class SerialNumberGenerating : WebPageHelper
    {
        #region Field & Some BLLObject
        /// <summary>
        /// 任务ID
        /// </summary>
        private string projectID
        {
            get
            {
                return Request.QueryString["projectID"];
            }
            set
            {
                projectID = value;
            }
        }

        private ProjectBLL pBll = new ProjectBLL();
        private AttachmentBLL aBll = new AttachmentBLL();
        private ProjectModifyBLL pmBll = new ProjectModifyBLL();
        EmployeeBLL eBll = new EmployeeBLL();
        #endregion

        //protected override void Render(HtmlTextWriter writer)
        //{
        //    System.IO.StringWriter sw = new System.IO.StringWriter();
        //    HtmlTextWriter htw = new HtmlTextWriter(sw);
        //    base.Render(writer);
        //    writer.Write(sw.ToString().Replace("<body", "<body onload=\"WinOnLoad();\""));
        //}

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidatePermission(Request.Url.LocalPath);
                //string rawUrl = Request.Url.LocalPath.TrimStart('/');
                //UserProfile user = UserProfile.GetInstance();
                //bool flag = false;
                //for (int i = 0; i < user.Menu.Count; i++)
                //{
                //    if (user.Menu[i].Path.Contains(rawUrl))
                //    {
                //        flag = true;
                //        break;
                //    }
                //}
                //if (!flag)
                //{
                //    Response.Redirect("Tip.html", true);
                //    //Response.Write("<h3>对不起，页面无法呈现给你！可能原因如下：<br>1、你不具备此页面的访问权限；<br>2、会话已过期，请尝试重新<a href=\"Login.aspx\">登录</a>。</h3>");
                //    Response.End();
                //}

                DropDownListDataBind();
                LoadEmployee();
                if (!string.IsNullOrEmpty(projectID))
                {
                    Page.Title = "任务维护";
                    Session["projectID"] = null;
                    hidProjectID.Value = projectID;
                    hidProjectID2.Value = projectID;
                    FormDataFill();
                }
                else
                {
                    Page.Title = "任务生成";
                }
            }
        }
        #endregion

        #region btnGeneratingClickEvent
        /// <summary>
        /// 生成按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGenerating_Click(object sender, EventArgs e)
        {
            GeneratingTask();
        }

        /// <summary>
        /// 生成任务
        /// </summary>
        private void GeneratingTask()
        {
            try
            {
                #region 变量声明
                //期限时间
                string strExpireDate = txtExpireDate.Text.Trim();
                //DateTime dtExpireDate = Convert.ToDateTime(strExpireDate + (string.IsNullOrEmpty(projectID) ? ":00:00" : string.Empty));
                DateTime dtExpireDate = Convert.ToDateTime(strExpireDate + ":00:00");
                string expireDate = dtExpireDate.ToString("yyyyMMddHH");
                //下单时间

                DateTime dtOrderDate = DateTime.Now;
                try
                {
                    dtOrderDate = Convert.ToDateTime(txtOrderDate.Text.Trim());
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLine("下单时间错误：" + ex.Message + ex.StackTrace);
                    ExecuteScript("AlertDialogOrderErr('请输入或选择正确的下单时间！');");
                    return;
                }
                string orderDate = dtOrderDate.ToString("yyMMddHHmm");
                //店铺编号
                string shopID = ddlShop.SelectedValue;
                double orderAmount = Convert.ToDouble(txtOrderAmount.Text);//订单金额
                string taskFolderName = string.Format("{0}-{1}{2}", expireDate, shopID, orderDate);//任务名
                DataSet ds = pBll.GetList(string.Format(" AND taskno = '{0}'", taskFolderName));
                if (string.IsNullOrEmpty(projectID) && ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    LogHelper.WriteLine("任务编号存在：" + taskFolderName);
                    Alert("根据输入信息生成的任务编号已存在，请核对！");
                    return;
                }

                string projectName = txtProjectName.Text.Trim();//工程名称
                string valuateMode = ddlValuateMode.SelectedValue;//计价模式
                string province = ddlProvince.SelectedValue; //Request.Form["ddlProvince"];//ddlProvince.SelectedValue;//省份
                string modelingSoftware = ddlModelingSoftware.SelectedValue;//建模软件
                string valuateSoftware = txtValuateSoftware.Text.Trim();// ddlValuateMode.SelectedValue;//计价软件
                //string specialtyCategory = ddlSpecialtyCategory.SelectedValue;//专业类别
                string specialtyCategory = string.Empty;//专业类别
                string specialtyCategoryMinor = string.Empty;// ddlSpecialtyCategoryMinor.SelectedValue;//专业类别小类
                string wangwangName = txtWangwangName.Text.Trim();//客户旺旺号
                string email = txtEmail.Text.Trim();//电子邮箱
                string floors = txtFloors.Text.Trim();//层数
                string constructionArea = txtConstructionArea.Text.Trim();//建筑面积
                string structureForm = ddlStructureForm.SelectedValue;//结构类型
                string buildingType = ddlBuildingType.SelectedValue;//建筑类型
                string transactionStatue = ddlTransactionStatus.SelectedValue;//交易状态
                string refund = txtRefund.Text.Trim();//退款金额
                string mobilePhone = txtMobilePhone.Text.Trim();//手机号
                string qq = txtQQ.Text.Trim();//QQ
                string paymentMethod = ddlPaymentMethod.SelectedValue;//支付方式
                string referrer = txtReferrer.Text.Trim();//推荐人
                string cashBack = txtCashBack.Text.Trim();//返现金额
                string remarks = txtRemarks.Text.Trim();//备注
                int? timeNeeded = null;
                if (!string.IsNullOrEmpty(txtTimeNeeded.Text.Trim()))
                {
                    if (ddlTimeNeeded.SelectedValue == "H")
                    {
                        timeNeeded = Convert.ToInt16(txtTimeNeeded.Text.Trim());
                    }
                    else if (ddlTimeNeeded.SelectedValue == "d")
                    {
                        timeNeeded = Convert.ToInt16(txtTimeNeeded.Text.Trim()) * 24;
                    }
                }
                string extraRequirement = txtExtraRequirement.Text.Trim();//其他要求
                int? materialIsUpload = Convert.ToInt32(ddlMaterialIsUpload.SelectedValue);
                if (UserProfile.GetInstance() == null)
                {
                    ExecuteScript("alert('会话已过期，请重新登录！');window.location.href='Login.aspx';");
                    return;
                }
                string enteringPerson = UserProfile.GetInstance().ID;
                #endregion

                #region 更新
                ProjectSpecialtyBLL pspBll = new ProjectSpecialtyBLL();
                specialtyCategory = Request["ctl00$ContentPlaceHolder1$ddlSpecialtyCategory"] ?? string.Empty;
                specialtyCategoryMinor = Request["ctl00$ContentPlaceHolder1$ddlSpecialtyCategoryMinor"] ?? string.Empty;
                //更新
                if (!string.IsNullOrEmpty(projectID))
                {
                    Project project = pBll.GetModel(projectID);
                    List<FileZillaServerProfile.Menu> lstMenu = UserProfile.GetInstance().Menu;
                    bool flag = false;
                    for (int i = 0; i < lstMenu.Count; i++)
                    {
                        if (lstMenu[i].Name == "任务维护")
                        {
                            flag = true;
                        }
                    }
                    #region 定义更新日志内容的字符串
                    //定义更新日志内容的字符串
                    StringBuilder sbLogContent = new StringBuilder("更新");
                    sbLogContent.AppendFormat("[{0}]：", project.TASKNO);
                    if (project.PROJECTNAME != txtProjectName.Text.Trim())
                    {
                        sbLogContent.AppendFormat("工程名称：{0}┃{1}，", project.PROJECTNAME, txtProjectName.Text.Trim());
                    }
                    //if (project.TASKNO != txtTaskName.Text.Trim())  //任务编号
                    //{
                    //    sbLogContent.AppendFormat("任务编号：{0}┃{1}，", project.TASKNO, txtTaskName.Text.Trim());
                    //}
                    if (project.ORDERDATE != dtOrderDate)
                    {
                        sbLogContent.AppendFormat("下单时间：{0}┃{1}，", project.ORDERDATE, dtOrderDate);
                    }
                    if (Convert.ToString(project.ORDERAMOUNT) != txtOrderAmount.Text.Trim())
                    {
                        sbLogContent.AppendFormat("订单金额：{0}┃{1}，", project.ORDERAMOUNT, txtOrderAmount.Text.Trim());
                    }
                    if (project.SHOP != ddlShop.SelectedValue)
                    {
                        sbLogContent.AppendFormat("店铺：{0}┃{1}，", ddlShop.Items.FindByValue(project.SHOP).Text, ddlShop.SelectedItem.Text);
                    }
                    if (project.PROVINCE != ddlProvince.SelectedValue)
                    {
                        sbLogContent.AppendFormat("省份：{0}┃{1}，", ddlShop.Items.FindByValue(project.PROVINCE).Text, ddlProvince.SelectedItem.Text);
                    }
                    if (project.WANGWANGNAME != txtWangwangName.Text.Trim())
                    {
                        sbLogContent.AppendFormat("旺旺号：{0}┃{1}，", project.WANGWANGNAME, txtWangwangName.Text.Trim());
                    }
                    if (project.PAYMENTMETHOD != ddlPaymentMethod.SelectedValue)
                    {
                        sbLogContent.AppendFormat("支付方式：{0}┃{1}，", ddlPaymentMethod.Items.FindByValue(project.PAYMENTMETHOD).Text, ddlPaymentMethod.SelectedItem.Text);
                    }
                    if (project.TRANSACTIONSTATUS != ddlTransactionStatus.SelectedValue)
                    {
                        sbLogContent.AppendFormat("交易状态：{0}┃{1}，", ddlTransactionStatus.Items.FindByValue(project.TRANSACTIONSTATUS).Text, ddlTransactionStatus.SelectedItem.Text);
                    }
                    if (!string.IsNullOrEmpty(refund) && project.REFUND != Convert.ToDouble(txtRefund.Text.Trim()))
                    {
                        sbLogContent.AppendFormat("退款金额：{0}┃{1}，", project.REFUND, txtRefund.Text.Trim());
                    }
                    #endregion
                    //管理员权限，可维护全部信息
                    if (flag)
                    {
                        project.PROJECTNAME = projectName;
                        //project.TASKNO = taskFolderName;
                        project.PROJECTNAME = projectName;
                        project.ORDERDATE = dtOrderDate;
                        project.EXPIREDATE = dtExpireDate;
                        project.TIMENEEDED = timeNeeded;
                        project.SHOP = shopID;
                        project.ORDERAMOUNT = orderAmount;
                        project.VALUATEMODE = valuateMode;
                        project.PROVINCE = province;
                        project.MODELINGSOFTWARE = modelingSoftware;
                        project.VALUATESOFTWARE = valuateSoftware;
                        project.SPECIALTYCATEGORY = null; //specialtyCategory;
                        project.SPECIALTYCATEGORYMINOR = null; // specialtyCategoryMinor;
                        project.WANGWANGNAME = wangwangName;
                        project.EMAIL = email;
                        if (!string.IsNullOrEmpty(floors))
                        {
                            project.FLOORS = Convert.ToInt16(floors);
                        }
                        if (!string.IsNullOrEmpty(constructionArea))
                        {
                            project.CONSTRUCTIONAREA = Convert.ToInt32(constructionArea);
                        }
                        project.STRUCTUREFORM = structureForm;
                        project.BUILDINGTYPE = buildingType;
                        project.TRANSACTIONSTATUS = transactionStatue;
                        if (!string.IsNullOrEmpty(refund))
                        {
                            project.REFUND = Convert.ToDouble(refund);
                        }
                        project.MOBILEPHONE = mobilePhone;
                        project.QQ = qq;
                        project.PAYMENTMETHOD = paymentMethod;
                        project.EXTRAREQUIREMENT = extraRequirement;
                        project.REFERRER = referrer;
                        if (!string.IsNullOrEmpty(cashBack))
                        {
                            project.CASHBACK = Convert.ToDecimal(cashBack);
                        }
                        project.REMARKS = remarks;
                        project.MATERIALISUPLOAD = materialIsUpload;
                    }
                    //客服权限，仅能维护交易状态
                    else
                    {
                        project.ORDERAMOUNT = orderAmount;
                        project.TRANSACTIONSTATUS = ddlTransactionStatus.SelectedValue;
                        project.MATERIALISUPLOAD = materialIsUpload;
                        project.REFUND = Convert.ToInt16(refund);
                    }
                    bool modifyFlag = pBll.Update(project);
                    if (modifyFlag)
                    {
                        if (!string.IsNullOrEmpty(specialtyCategory))
                        {
                            pspBll.Add(projectID, specialtyCategory, "0");
                        }
                        if (!string.IsNullOrEmpty(specialtyCategoryMinor))
                        {
                            pspBll.Add(projectID, specialtyCategoryMinor, "1");
                        }
                        try
                        {
                            SystemLog log = new SystemLog();
                            log.ID = Guid.NewGuid().ToString();
                            log.EMPLOYEEID = UserProfile.GetInstance().ID;
                            log.OPERATETYPE = null;
                            log.OPERATECONTENT = sbLogContent.ToString().TrimEnd('，');
                            log.CREATEDATE = DateTime.Now;
                            //string mac = null;
                            string ip = null;
                            GetIP(out ip);
                            log.IPADDRESS = ip;
                            //log.PHYSICALADDRESS = mac;
                            SystemLogDAL slogDal = new SystemLogDAL();
                            slogDal.Add(log);
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLine(ex.Message);
                        }
                        string currentPath = Request.RawUrl;
                        ExecuteScript("alert('编辑成功！');window.location.href='" + currentPath + "';");
                    }
                    else
                    {
                        Alert("编辑失败！");
                        return;
                    }
                }
                #endregion
                #region 添加
                //添加
                else
                {
                    FileZillaServerModel.Project project = new Project();
                    project.ID = Guid.NewGuid().ToString();
                    project.TASKNO = taskFolderName;
                    project.PROJECTNAME = projectName;
                    project.ORDERDATE = dtOrderDate;
                    project.EXPIREDATE = dtExpireDate;
                    project.TIMENEEDED = timeNeeded;
                    project.SHOP = shopID;
                    project.ORDERAMOUNT = orderAmount;
                    project.VALUATEMODE = valuateMode;
                    project.PROVINCE = province;
                    project.MODELINGSOFTWARE = modelingSoftware;
                    project.VALUATESOFTWARE = valuateSoftware;
                    project.SPECIALTYCATEGORY = null;// specialtyCategory;
                    project.SPECIALTYCATEGORYMINOR = null;// specialtyCategoryMinor;
                    project.WANGWANGNAME = wangwangName;
                    project.EMAIL = email;
                    if (!string.IsNullOrEmpty(floors))
                    {
                        project.FLOORS = Convert.ToInt16(floors);
                    }
                    if (!string.IsNullOrEmpty(constructionArea))
                    {
                        project.CONSTRUCTIONAREA = Convert.ToInt32(constructionArea);
                    }
                    project.STRUCTUREFORM = structureForm;
                    project.BUILDINGTYPE = buildingType;
                    project.TRANSACTIONSTATUS = transactionStatue;
                    project.MOBILEPHONE = mobilePhone;
                    project.QQ = qq;
                    project.PAYMENTMETHOD = paymentMethod;
                    project.EXTRAREQUIREMENT = extraRequirement;
                    project.REFERRER = referrer;
                    if (!string.IsNullOrEmpty(cashBack))
                    {
                        project.CASHBACK = Convert.ToDecimal(cashBack);
                    }
                    project.REMARKS = remarks;
                    project.ASSIGNMENTBOOK = txtAssignmentBook.Text.Trim();
                    project.ISFINISHED = 0;
                    project.MATERIALISUPLOAD = materialIsUpload;
                    project.ENTERINGPERSON = enteringPerson;
                    project.CREATEDATE = DateTime.Now;
                    project.ISCREATEDFOLDER = 0;
                    project.ISDELETED = 0;
                    bool success = pBll.Add(project);
                    if (success)
                    {
                        if (!string.IsNullOrEmpty(specialtyCategory))
                        {
                            pspBll.Add(project.ID, specialtyCategory, "0");
                        }
                        if (!string.IsNullOrEmpty(specialtyCategoryMinor))
                        {
                            pspBll.Add(project.ID, specialtyCategoryMinor, "1");
                        }

                        txtTaskName.Text = taskFolderName;
                        //如果选择了同步创建目录
                        if (true)//   (Request.Form["sync"] != null && Request.Form["sync"] == "rdbSync")
                        {
                            //新任务上传目录
                            string taskAllotmentPath = ConfigurationManager.AppSettings["taskAllotmentPath"].ToString().TrimEnd('\\');
                            //任务书内容
                            string assignmentBookText = txtAssignmentBook.Text.Trim();
                            //新任务的路径
                            string newTaskPath = string.Format("{0}\\{1}", taskAllotmentPath, taskFolderName);
                            //新任务的任务书的路径
                            string assignmentBookDirectoryName = string.Format("{0}\\{1}", newTaskPath, "任务书");
                            if (!Directory.Exists(assignmentBookDirectoryName))
                            {
                                Directory.CreateDirectory(assignmentBookDirectoryName);
                            }
                            //任务书文本文件路径
                            string txtFileName = string.Format("{0}\\{1}", assignmentBookDirectoryName, "任务书.txt");
                            using (StreamWriter sw = new StreamWriter(txtFileName, false, Encoding.UTF8))
                            {
                                sw.Write(assignmentBookText);
                            }
                        }
                        Session["projectID"] = project.ID;
                        lblGenerateSuccess.Visible = true;
                        //hidProjectID2.Value = project.ID;
                        //ExecuteScript("AlertDialog('生成成功！', 'InvokeCreateFolder', '" + project.ID + "');");
                        ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "AlertDialog('生成成功！', 'InvokeCreateFolder', '" + project.ID + "');", true);
                        //ExecuteScript("alert('生成成功！');window.location.href='SerialNumberGenerating.aspx?projectID=" + project.ID + "';");
                        return;
                    }
                    else
                    {
                        ExecuteScript("AlertDialog('生成失败！', null, null);");
                        return;
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + ex.StackTrace);
                ExecuteScript("AlertDialog('操作失败！', null, null);");
            }
        }
        #endregion

        #region DropDownListDataBind
        /// <summary>
        /// 集中绑定DropDownList
        /// </summary>
        public void DropDownListDataBind()
        {
            try
            {
                System.Data.DataTable dtConfig = null;
                if (Cache["dtConfig"] == null)
                {
                    dtConfig = new ConfigureBLL().GetConfig();
                    Cache.Insert("dtConfig", dtConfig, null, DateTime.Now.AddHours(1.5), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                dtConfig = (DataTable)Cache["dtConfig"];
                DropDownListDataBind(ddlShop, dtConfig, ConfigTypeName.店铺编号名称, "-请选择-");
                DropDownListDataBind(ddlValuateMode, dtConfig, ConfigTypeName.计价模式, "-请选择-");
                DropDownListProvinceBind();
                DropDownListDataBind(ddlModelingSoftware, dtConfig, ConfigTypeName.建模软件, "-请选择-");
                //DropDownListDataBind(ddlValuateSoftware, dtConfig, ConfigTypeName.计价软件, string.Empty);
                DropDownListDataBind(ddlPaymentMethod, dtConfig, ConfigTypeName.支付方式, "-请选择-");
                DropDownListDataBind(ddlStructureForm, dtConfig, ConfigTypeName.建筑类型, "-请选择-");
                DropDownListDataBind(ddlBuildingType, dtConfig, ConfigTypeName.结构类型, "-请选择-");
                DropDownListDataBind(ddlSpecialtyCategory, dtConfig, ConfigTypeName.专业类别, "请选择");
                //DropDownListDataBind(ddlSpecialtyCategoryMinor, dtConfig, ConfigTypeName.专业类别小类, "请选择");
                DropDownListDataBind(ddlTransactionStatus, dtConfig, ConfigTypeName.交易状态, "-请选择-");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 绑定DropDownList的方法
        /// </summary>
        /// <param name="dropDownList"></param>
        /// <param name="configTypeName"></param>
        /// <param name="tipString"></param>
        protected void DropDownListDataBind(DropDownList dropDownList, DataTable dtConfig, ConfigTypeName configTypeName, string tipString)
        {
            try
            {
                //DataTable dt = new ConfigureBLL().GetConfig(configTypeName.ToString());
                DataTable dtNew = dtConfig.Clone();
                DataRow[] drArray = dtConfig.Select("configtypename='" + configTypeName + "'");
                for (int i = 0; i < drArray.Length; i++)
                {
                    dtNew.ImportRow(drArray[i]);
                }
                dropDownList.DataSource = dtNew;
                dropDownList.DataTextField = "configvalue";
                dropDownList.DataValueField = "configkey";
                dropDownList.DataBind();
                dtNew = null;
                if (configTypeName != ConfigTypeName.支付方式)
                {
                    dropDownList.Items.Insert(0, new ListItem(tipString, string.Empty));
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + ex.StackTrace);
                ExecuteScript("AlertDialog('程序出错！', null);");
            }
        }

        /// <summary>
        /// 绑定省份
        /// </summary>
        private void DropDownListProvinceBind()
        {
            try
            {
                DataTable dt = null;
                if (Cache["dtProvince"] == null)
                {
                    dt = new ConfigureBLL().GetProvince();
                    Cache.Insert("dtProvince", dt, null, DateTime.Now.AddDays(5), System.Web.Caching.Cache.NoSlidingExpiration);
                }
                dt = Cache["dtProvince"] as DataTable;
                ddlProvince.DataSource = dt;
                ddlProvince.DataTextField = "Name";
                ddlProvince.DataValueField = "Code";
                ddlProvince.DataBind();
                dt = null;
                ddlProvince.Items.Insert(0, new ListItem("-请选择-", string.Empty));
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// dropdownlist加载员工编号和姓名信息
        /// </summary>
        protected void LoadEmployee()
        {
            DataTable dtEmp = eBll.GetListUnionNoAndNameForDemonation(string.Empty, "EMPLOYEENO").Tables[0];
            ddlAssignTo.DataSource = dtEmp;
            ddlAssignTo.DataTextField = "NOANDNAME";
            ddlAssignTo.DataValueField = "ID";
            ddlAssignTo.DataBind();
        }
        #endregion

        #region FillForm
        /// <summary>
        /// 填充表单
        /// </summary>
        private void FormDataFill()
        {
            try
            {
                //填充任务表单
                Project project = pBll.GetModel(projectID);
                if (project != null)
                {
                    //要转换成与My97DatePicker控件一样的格式
                    txtExpireDate.Text = project.EXPIREDATE == null ? string.Empty : project.EXPIREDATE.Value.ToString("yyyy-MM-dd HH");
                    txtOrderDate.Text = project.ORDERDATE == null ? string.Empty : project.ORDERDATE.Value.ToString("yyyy-MM-dd HH:mm:ss");
                    if (project.TIMENEEDED % 24 == 0)
                    {
                        txtTimeNeeded.Text = (project.TIMENEEDED / 24).ToString();
                        ddlTimeNeeded.SelectedValue = "d";
                    }
                    else
                    {
                        txtTimeNeeded.Text = project.TIMENEEDED.ToString();
                        ddlTimeNeeded.SelectedValue = "H";
                    }
                    ddlShop.SelectedValue = project.SHOP;
                    txtOrderAmount.Text = project.ORDERAMOUNT.ToString();
                    txtProjectName.Text = project.PROJECTNAME;
                    ddlValuateMode.SelectedValue = project.VALUATEMODE;
                    ddlProvince.SelectedValue = project.PROVINCE;
                    ddlModelingSoftware.SelectedValue = project.MODELINGSOFTWARE;
                    //ddlValuateSoftware.SelectedValue = project.VALUATESOFTWARE;
                    txtValuateSoftware.Text = project.VALUATESOFTWARE;
                    //ddlSpecialtyCategory.SelectedValue = project.SPECIALTYCATEGORY;
                    //ddlSpecialtyCategoryMinor.SelectedValue = project.SPECIALTYCATEGORYMINOR;
                    txtWangwangName.Text = project.WANGWANGNAME;
                    txtEmail.Text = project.EMAIL;
                    txtFloors.Text = project.FLOORS.ToString();
                    txtConstructionArea.Text = project.CONSTRUCTIONAREA.ToString();
                    ddlStructureForm.SelectedValue = project.STRUCTUREFORM;
                    ddlBuildingType.SelectedValue = project.BUILDINGTYPE;
                    ddlTransactionStatus.SelectedValue = project.TRANSACTIONSTATUS;
                    txtRefund.Text = project.REFUND.ToString();
                    txtMobilePhone.Text = project.MOBILEPHONE;
                    txtQQ.Text = project.QQ;
                    ddlPaymentMethod.SelectedValue = project.PAYMENTMETHOD;
                    txtReferrer.Text = project.REFERRER;
                    txtCashBack.Text = project.CASHBACK.ToString();
                    ddlMaterialIsUpload.SelectedValue = Convert.ToString(project.MATERIALISUPLOAD) ?? "0";
                    txtRemarks.Text = project.REMARKS;
                    txtExtraRequirement.Text = project.EXTRAREQUIREMENT;
                    txtTaskName.Text = project.TASKNO;
                    //设置网页标题
                    Page.Title = string.Format("{0} - {1}", project.TASKNO, "任务维护");
                }
                //完成人需要进行联合查询获取并显示
                //txtFinishedPerson.Text = pBll.GetFinishedPerson(projectID);

                //2017-04-05需求变更，完成人有时不止一个
                StringBuilder finishedPerson = new StringBuilder();
                ProjectSharingBLL psBll = new ProjectSharingBLL();
                DataTable dtFinishedPerson = psBll.GetListInnerJoinEmployee(string.Format(" PROJECTID = '{0}'", projectID)).Tables[0];
                if (dtFinishedPerson != null && dtFinishedPerson.Rows.Count > 0)
                {
                    for (int i = 0; i < dtFinishedPerson.Rows.Count; i++)
                    {
                        finishedPerson.Append(string.Format("{0}│", dtFinishedPerson.Rows[i]["employeeNo"]));
                    }
                    txtFinishedPerson.Text = finishedPerson.ToString().TrimEnd('│');
                }
                else
                {
                    divAssign.Visible = true;
                }

                ProjectSpecialtyBLL pspBll = new ProjectSpecialtyBLL();
                DataTable dtSpecialtyList = pspBll.GetSpecialtyInnerJoinProject(projectID, string.Empty).Tables[0];
                string specialtyList = string.Empty;
                DataRow[] drSpcList = dtSpecialtyList.Select(" type = '0'");
                for (int i = 0; i < drSpcList.Length; i++)
                {
                    specialtyList += drSpcList[i]["specialtyid"].ToString() + ",";
                }
                specialtyList = specialtyList.TrimEnd(',');
                hidDdlSpecialtySelectedValue.Value = specialtyList;

                StringBuilder specialtyMinorList = new StringBuilder();
                DataRow[] drSpcMinorList = dtSpecialtyList.Select(" type = '1'");
                for (int i = 0; i < drSpcMinorList.Length; i++)
                {
                    specialtyMinorList.Append(drSpcMinorList[i]["specialtyid"].ToString() + ",");
                }
                hidDdlSpecialtyMinorSelectedValue.Value = specialtyMinorList.ToString().TrimEnd(',');

                lblOperateType.Text = "维护";
                btnGenerating.Text = "确定修改";
                btnSetIsFinished.Visible = project.ISFINISHED != 1;

                //菜单验证
                List<FileZillaServerProfile.Menu> lstMenu = new List<FileZillaServerProfile.Menu>();
                lstMenu = UserProfile.instance.Menu;
                bool modifyFlag = false;
                for (int i = 0; i < lstMenu.Count; i++)
                {
                    if (lstMenu[i].Name == "任务维护")
                    {
                        modifyFlag = true;
                        break;
                    }
                }

                //角色验证
                List<Role> lstRole = new List<Role>();
                lstRole = UserProfile.instance.Role;
                if (lstRole.Any(item => item.RoleName == "超级管理员"))
                {
                    if (!string.IsNullOrEmpty(projectID))
                    {
                        hidIsSuperAdmin.Value = "0";
                    }
                }

                ////普通任务的压缩及下载
                //DataTable dt = pBll.GetFinalScript(projectID);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    imgFinalFolder.Visible = true;
                //    lblFinalScript.Text = string.Format("{0}", dt.Rows[0]["taskno"].ToString());
                //    hidProjectOrModifyID.Value = projectID;
                //    StringBuilder sbProject = new StringBuilder();
                //    sbProject.AppendFormat("<input type=\"button\" id=\"btnProjectCompress\" name=\"btnProjectCompress\" title=\"压缩\" value=\"压缩\" class=\"btnCompress\" onclick=\"Compress('0','{0}');\" />", projectID);
                //    sbProject.AppendFormat(
                //        "<input type=\"button\" id=\"btnProjectDownload\" name=\"btnProjectDownload\" value=\"下载\" onclick=\"Download('0','{0}')\" title=\"请先压缩再下载\" class=\"btnDownload\" disabled=\"true\" />",
                //        projectID);
                //    divProject.InnerHtml = sbProject.ToString();
                //}

                //任务资料的列表绑定
                //GridViewTaskDataBind(projectID, project.FINISHEDPERSON);
                //修改资料的列表绑定
                //GridViewModifyTaskDataBind(projectID);
                ////填充修改任务的表单
                //FillModifyTaskForm();

                //如果没有任务维护的权限，禁用以下控件
                if (!modifyFlag)
                {
                    txtExpireDate.ReadOnly = true;
                    txtOrderDate.ReadOnly = true;
                    txtTimeNeeded.ReadOnly = true;
                    ddlTimeNeeded.Enabled = false;
                    ddlShop.Enabled = false;
                    txtOrderAmount.ReadOnly = true;
                    txtProjectName.ReadOnly = true;
                    ddlValuateMode.Enabled = false;
                    ddlProvince.Enabled = false;
                    ddlModelingSoftware.Enabled = false;
                    //ddlValuateSoftware.Enabled = false;
                    txtValuateSoftware.Enabled = false;
                    ddlSpecialtyCategory.Enabled = false;
                    //ddlSpecialtyCategoryMinor.Enabled = false;
                    txtWangwangName.ReadOnly = true;
                    txtEmail.ReadOnly = true;
                    txtFloors.ReadOnly = true;
                    txtConstructionArea.ReadOnly = true;
                    ddlStructureForm.Enabled = false;
                    ddlBuildingType.Enabled = false;
                    txtMobilePhone.ReadOnly = true;
                    txtQQ.ReadOnly = true;
                    ddlPaymentMethod.Enabled = false;
                    txtReferrer.Enabled = false;
                    txtCashBack.Enabled = false;
                    txtRemarks.ReadOnly = true;
                    txtExtraRequirement.ReadOnly = true;
                    txtTaskName.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + ex.StackTrace);
            }
        }

        #region GridViewDataBind
        ///// <summary>
        ///// 任务资料数据绑定
        ///// </summary>
        ///// <param name="projectID"></param>
        ///// <param name="finishedPerson">完成人</param>
        //private void GridViewTaskDataBind(string projectID, string finishedPerson)
        //{
        //    try
        //    {
        //        projectID = projectID == null ? Convert.ToString(Session["projectID"]) : projectID;
        //        //如果完成人不为空，说明任务已分配，不再绑定并展示任务资料的数据，同时隐藏上传任务资料按钮
        //        if (string.IsNullOrEmpty(finishedPerson))
        //        {
        //            //绑定已上传的任务资料
        //            List<Attachment> lstAtt = aBll.GetModelList(string.Format(" TASKTYPE = 0 AND TASKID = '{0}'", projectID));
        //            gvTaskData.DataSource = lstAtt;
        //            gvTaskData.DataBind();
        //        }
        //        else
        //        {
        //            lblTaskDataUploadTip.Visible = false;
        //            //fupTaskData.Visible = false;
        //            InputFile1.Visible = false;
        //            btnUploadTaskData.Visible = false;
        //        }

        //        //绑定完成稿
        //        DataTable dt = new ProjectBLL().GetFinalScript(projectID);
        //        gvFinalData.DataSource = dt;
        //        gvFinalData.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteLine(ex.Message);
        //    }
        //}

        /// <summary>
        /// 修改资料数据绑定
        /// </summary>
        /// <param name="projectID"></param>
        //private void GridViewModifyTaskDataBind(string projectID)
        //{
        //    try
        //    {
        //        //绑定修改稿
        //        DataTable dtModify = pBll.GetProjectModifyByPrjID(projectID);
        //        gvModify.DataSource = dtModify;
        //        gvModify.DataBind();

        //        //lstAtt = null;
        //        //lstAtt = new List<Attachment>();
        //        //lstAtt = aBll.GetModelList(string.Format(" TASKTYPE = 1 AND TASKID = '{0}'", projectID));
        //        //gvModify.DataSource = lstAtt;
        //        //gvModify.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteLine(ex.Message);
        //    }
        //}
        #endregion

        #region 填充修改任务表单    //discarded
        /// <summary>
        /// 填充修改任务表单（这里把它单独拉出来，是为了在删除任务后刷新修改任务的列表）
        /// </summary>
        //private void FillModifyTaskForm()
        //{
        //    try
        //    {
        //        //售后修改任务的压缩及下载
        //        DataTable dtModify = pBll.GetProjectModifyByPrjID(projectID);
        //        if (dtModify != null && dtModify.Rows.Count > 0)
        //        {
        //            StringBuilder sbInsertHtml = new StringBuilder();
        //            for (int i = 0; i < dtModify.Rows.Count; i++)
        //            {
        //                sbInsertHtml.Append("<div style=\"width:450px; height:36px; float:left;\">");
        //                sbInsertHtml.Append("<img src=\"Images/folder.png\" width=\"22\" height=\"22\" />");
        //                sbInsertHtml.AppendFormat("<lable>{0}</lable>", dtModify.Rows[i]["folderName"]);
        //                sbInsertHtml.Append("</div>");

        //                sbInsertHtml.AppendFormat("<div style=\"width:400px; height:36px; float:left;\">", dtModify.Rows[i]["ID"]);
        //                sbInsertHtml.AppendFormat("<input id=\"btnCmprs{0}\" name=\"btnCmprs{0}\" type=\"button\" value=\"压缩\" class=\"btnCompress\" onclick=\"Compress('1','{1}','{2}');\" />",
        //                    i, dtModify.Rows[i]["ID"], i);
        //                sbInsertHtml.AppendFormat(
        //                    "<input id=\"btnModifyDownload{0}\" name=\"btnModifyDownload{0}\" type=\"button\" value=\"下载\" class=\"btnDownload\" onclick=\"Download('1','{1}');\" title=\"请先压缩再下载\" disabled=\"true\" />",
        //                    i, dtModify.Rows[i]["ID"]);
        //                if (dtModify.Rows[i]["isfinished"].ToString() == "0")
        //                {
        //                    sbInsertHtml.AppendFormat(
        //                        "<input id=\"btnDeleteModifyTask{0}\" name=\"btnDeleteModifyTask{0}\" type=\"button\" value=\"删除\" onclick=\"Delete('{1}')\"", i, dtModify.Rows[i]["ID"]);
        //                }
        //                sbInsertHtml.Append("</div>");
        //            }
        //            divModifyList.InnerHtml = sbInsertHtml.ToString();
        //        }
        //        else
        //        {
        //            divModifyList.InnerHtml = "暂无修改记录";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        LogHelper.WriteLine(ex.Message + ex.StackTrace);
        //    }
        //}
        #endregion
        #endregion

        #region Remarks   //discarded
        //protected void btnCompress_Click(object sender, EventArgs e)
        //{

        //}

        //protected void btnDownload_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string DownloadFileName = @"F:\Temporary\Cache\Dictionary\员工\004\2017052401-817022711523020完成.zip";//文件路径
        //        string filepath = DownloadFileName; //Server.MapPath(DownloadFileName);
        //        //string filename = Path.GetFileName(filepath);
        //        //FileInfo file = new FileInfo(filepath);
        //        //Response.Clear();
        //        //Response.ContentType = "application/octet-stream";
        //        //Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));

        //        //Response.AddHeader("Content-length", file.Length.ToString());
        //        //Response.Flush();
        //        //Response.WriteFile(filepath);
        //        //Response.End();

        //        System.IO.FileInfo fileInfo = new System.IO.FileInfo(filepath);

        //        if (fileInfo.Exists == true)
        //        {
        //            const long ChunkSize = 102400;//100K 每次读取文件，只读取100Ｋ，这样可以缓解服务器的压力
        //            byte[] buffer = new byte[ChunkSize];

        //            Response.Clear();
        //            System.IO.FileStream iStream = System.IO.File.OpenRead(filepath);
        //            long dataLengthToRead = iStream.Length;//获取下载的文件总大小
        //            Response.ContentType = "application/octet-stream";
        //            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(filepath));
        //            while (dataLengthToRead > 0 && Response.IsClientConnected)
        //            {
        //                int lengthRead = iStream.Read(buffer, 0, Convert.ToInt32(ChunkSize));//读取的大小
        //                Response.OutputStream.Write(buffer, 0, lengthRead);
        //                Response.Flush();
        //                dataLengthToRead = dataLengthToRead - lengthRead;
        //            }
        //            Response.Close();
        //        }
        //    }
        //    catch
        //    {
        //        Response.Write("<script>alert('没有找到下载的源文件')</script>");
        //    }
        //}
        #endregion

        #region DownloadButtonClickEvent   //discarded
        /*/// <summary>
        /// 下载按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string employeeRootPath = ConfigurationManager.AppSettings["employeePath"];

                string taskType = this.hidTaskType.Value;//任务类型：0，普通任务；1，售后。
                string projectOrModifyID = hidProjectOrModifyID.Value;//任务或者售后的ID
                //普通任务
                if (taskType == "0")
                {
                    DataTable dt = new ProjectBLL().GetFinalScript(projectOrModifyID);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string taskNo = dt.Rows[0]["taskno"].ToString();
                        string employeeNo = dt.Rows[0]["EMPLOYEENO"].ToString();
                        string currentEmpPath = string.Format("{0}{1}", employeeRootPath, employeeNo);
                        foreach (DirectoryInfo taskFolder in new DirectoryInfo(currentEmpPath).GetDirectories())
                        {
                            if (taskFolder.Name.StartsWith(taskNo))
                            {
                                string fileName = taskFolder.FullName + ".zip";
                                DownLoad(fileName);
                            }
                        }
                    }
                }
                //修改记录
                else if (taskType == "1")
                {
                    DataTable dt = new ProjectBLL().GetProjectModifyByModifyID(projectOrModifyID);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string taskNo = dt.Rows[0]["taskNo"].ToString();
                        string employeeNo = dt.Rows[0]["employeeno"].ToString();
                        string currentEmpPath = string.Format("{0}{1}", employeeRootPath, employeeNo);
                        foreach (DirectoryInfo taskFolder in new DirectoryInfo(currentEmpPath).GetDirectories())
                        {
                            if (taskFolder.Name.StartsWith(taskNo))
                            {
                                //存放修改记录的文件夹名
                                string modifyRecordFolder = ConfigurationManager.AppSettings["modifyRecordFolderName"].ToString();
                                foreach (DirectoryInfo taskFolderChild in taskFolder.GetDirectories())
                                {
                                    //如果当前目录是修改记录的目录
                                    if (taskFolderChild.Name.Trim() == modifyRecordFolder)
                                    {
                                        //遍历修改目录的子目录
                                        foreach (DirectoryInfo modifyFolders in taskFolderChild.GetDirectories())
                                        {
                                            string modifyFolderName = modifyFolders.Name;
                                            string dtModifyFolderName = dt.Rows[0]["foldername"].ToString();
                                            //找到需要的目录名下载之
                                            if (modifyFolderName.Contains("完成") && modifyFolderName.StartsWith(dtModifyFolderName))
                                            {
                                                string destinationFileName = modifyFolders.FullName + ".zip";
                                                DownLoad(destinationFileName);
                                            }
                                            else
                                            {
                                                ExecuteScript("AlertDialog('修改任务暂未完成或文件夹名称已被改动，无法下载。', null);");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + ex.StackTrace);
                ExecuteScript("AlertDialog('下载出错！', null);");
            }
        }
        */
        #endregion

        #region 下载公共方法
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="fileName"></param>
        private void DownLoad(string fileName)
        {
            if (File.Exists(fileName))
            {
                System.IO.Stream iStream = null;
                // Buffer to read 10K bytes in chunk:
                byte[] buffer = new Byte[10000];
                // Length of the file:
                int length;
                // Total bytes to read.
                long dataToRead;
                // Identify the file to download including its path.
                string filepath = fileName;
                // Identify the file name.
                string filename = System.IO.Path.GetFileName(filepath);
                try
                {
                    // Open the file.
                    iStream = new System.IO.FileStream(filepath, System.IO.FileMode.Open,
                                System.IO.FileAccess.Read, System.IO.FileShare.Read);
                    // Total bytes to read.
                    dataToRead = iStream.Length;
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.ClearContent();
                    Response.ContentType = "text/plain"; // Set the file type
                    Response.AddHeader("Content-Length", dataToRead.ToString());
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + filename + "");

                    // Read the bytes.
                    while (dataToRead > 0)
                    {
                        // Verify that the client is connected.
                        if (Response.IsClientConnected)
                        {
                            // Read the data in buffer.
                            length = iStream.Read(buffer, 0, 10000);

                            // Write the data to the current output stream.
                            Response.OutputStream.Write(buffer, 0, length);

                            // Flush the data to the HTML output.
                            Response.Flush();

                            buffer = new Byte[10000];
                            dataToRead = dataToRead - length;
                        }
                        else
                        {
                            // Prevent infinite loop if user disconnects
                            dataToRead = -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Trap the error, if any.
                    Response.Write("Error : " + ex.Message);
                }
                finally
                {
                    if (iStream != null)
                    {
                        //Close the file.
                        iStream.Close();
                    }
                    Response.End();
                }
            }
        }
        #endregion

        #region ModifyTaskUpload    //discarded
        /// <summary>
        /// 上传修改任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddModifyTask_Click(object sender, EventArgs e)
        {
            //UploadModifyTask();
        }

        ///// <summary>
        ///// 上传任务或售后
        ///// </summary>
        //private void UploadModifyTask()
        //{
        //    if (fupd.HasFile)//如果用户选择了文件
        //    {
        //        #region Remarks
        //        //LogHelper.WriteLine("进入开头");
        //        //if (Path.GetExtension(fupd.FileName).ToLower() != ".zip")
        //        //{
        //        //    Alert("请上传“.zip”后缀的压缩文件！");
        //        //    return;
        //        //}
        //        //string employeeRootPath = ConfigurationManager.AppSettings["employeePath"];

        //        ////string taskType = this.hidTaskType.Value;//任务类型：0，普通任务；1，售后。
        //        //string projectOrModifyID = hidProjectOrModifyID.Value;//任务或者售后的ID
        //        //try
        //        //{
        //        //    //获得修改稿的dt
        //        //    DataTable dt = new ProjectBLL().GetProjectModifyByPrjID(projectID);
        //        //    //LogHelper.WriteLine("获得修改稿的dtCount " + dt.Rows.Count);
        //        //    LogHelper.WriteLine("dt is null:" + (dt == null));
        //        //    if (dt != null && dt.Rows.Count > 0)
        //        //    {
        //        //        //任务目录（任务编号）
        //        //        string taskNo = dt.Rows[0]["taskNo"].ToString();
        //        //        //员工编号
        //        //        string employeeNo = dt.Rows[0]["employeeno"].ToString();
        //        //        //当前员工的文件存储目录
        //        //        string currentEmpPath = string.Format("{0}{1}", employeeRootPath, employeeNo);
        //        //        foreach (DirectoryInfo taskFolder in new DirectoryInfo(currentEmpPath).GetDirectories())
        //        //        {
        //        //            //找到当前修改记录对应的任务目录
        //        //            if (taskFolder.Name.StartsWith(taskNo))
        //        //            {
        //        //                //存放修改记录的文件夹名
        //        //                string modifyRecordFolder = ConfigurationManager.AppSettings["modifyRecordFolderName"].ToString();
        //        //                LogHelper.WriteLine("modifyRecordFolder:" + modifyRecordFolder);
        //        //                int modifyTaskAmount = 0;
        //        //                foreach (DirectoryInfo taskFolderChild in taskFolder.GetDirectories())
        //        //                {
        //        //                    //如果当前目录是修改记录的目录
        //        //                    if (taskFolderChild.Name.Trim() == modifyRecordFolder)
        //        //                    {
        //        //                        //遍历修改目录的子目录
        //        //                        foreach (DirectoryInfo modifyFolders in taskFolderChild.GetDirectories())
        //        //                        {
        //        //                            string modifyFolderName = modifyFolders.Name;
        //        //                            //string dtModifyFolderName = dt.Rows[0]["foldername"].ToString();
        //        //                            ////找到需要的目录名下载之
        //        //                            //if (modifyFolderName.Contains("完成") && modifyFolderName.StartsWith(dtModifyFolderName))
        //        //                            //{
        //        //                            //    string destinationFileName = modifyFolders.FullName + ".zip";
        //        //                            //    DownLoad(destinationFileName);
        //        //                            //}
        //        //                            //不包含“完成”二字的目录，作为修改记录的数量
        //        //                            if (!modifyFolderName.Contains("完成"))
        //        //                            {
        //        //                                modifyTaskAmount++;
        //        //                            }
        //        //                        }
        //        //                    }
        //        //                }
        //        //                //当前任务的修改目录全名
        //        //                string modifyFolderFullName = string.Format("{0}\\{1}\\{2}", currentEmpPath, taskFolder.Name, modifyRecordFolder);
        //        //                //如果修改记录数量为0，那么就要判断当前任务有没有“修改记录”这个子目录了
        //        //                if (modifyTaskAmount == 0)
        //        //                {
        //        //                    LogHelper.WriteLine("如果当前任务还没有创建“修改记录”目录的话，就创建一个，顺便把“修改1”也创建了。PS：他爸都没有，怎么可能有儿子？");
        //        //                    //如果当前任务还没有创建“修改记录”目录的话，就创建一个，顺便把“修改1”也创建了。PS：他爸都没有，怎么可能有儿子？
        //        //                    if (!Directory.Exists(modifyFolderFullName))
        //        //                    {
        //        //                        string modifyFolderNameOne = modifyFolderFullName + "\\修改1\\";
        //        //                        Directory.CreateDirectory(modifyFolderNameOne);
        //        //                        fupd.SaveAs(modifyFolderNameOne + fupd.FileName);
        //        //                        string zipedFileName = modifyFolderNameOne + fupd.FileName;
        //        //                        string upZipDirectory = modifyFolderNameOne + fupd.FileName.Replace(Path.GetExtension(fupd.FileName), string.Empty);
        //        //                        ZipHelper.UnZipFiles(zipedFileName, upZipDirectory, string.Empty);
        //        //                        File.Delete(zipedFileName);
        //        //                        FillModifyTaskForm();
        //        //                        Alert("上传修改任务成功！");
        //        //                        return;
        //        //                    }
        //        //                }
        //        //                //如果已经有过修改记录，就自增1作为新的目录名
        //        //                else
        //        //                {
        //        //                    LogHelper.WriteLine("如果已经有过修改记录，就自增1作为新的目录名");
        //        //                    //新的修改任务的目录序号
        //        //                    int modifySequence = modifyTaskAmount + 1;
        //        //                    //新的修改任务的目录名
        //        //                    string addModifyFolderName = string.Format("{0}{1}", "修改", modifySequence);
        //        //                    //待添加的任务目录的全名
        //        //                    string addModifyFolderFullName = string.Format("{0}\\{1}", modifyFolderFullName, addModifyFolderName);
        //        //                    //如果待添加的任务目录名不存在
        //        //                    LogHelper.WriteLine("//如果待添加的任务目录名不存在");
        //        //                    if (!Directory.Exists(addModifyFolderFullName))
        //        //                    {
        //        //                        Directory.CreateDirectory(addModifyFolderFullName);
        //        //                    }
        //        //                    //定义上载到服务器的文件路径
        //        //                    string uploadPath = string.Format("{0}\\{1}", addModifyFolderFullName, fupd.FileName);
        //        //                    LogHelper.WriteLine("//定义上载到服务器的文件路径");
        //        //                    //进行上载
        //        //                    fupd.SaveAs(uploadPath);
        //        //                    string modifyFolderNameOne = modifyFolderFullName + "\\修改1\\";
        //        //                    string zipedFileName = modifyFolderNameOne + fupd.FileName;
        //        //                    string upZipDirectory = modifyFolderNameOne + fupd.FileName.Replace(Path.GetExtension(fupd.FileName), string.Empty);
        //        //                    ZipHelper.UnZipFiles(zipedFileName, upZipDirectory, string.Empty);
        //        //                    File.Delete(zipedFileName);
        //        //                    FillModifyTaskForm();
        //        //                    Alert("上传修改任务成功！");
        //        //                    return;
        //        //                }
        //        //            }
        //        //        }
        //        //    }
        //        //    //“修改记录”目录没有文件的话
        //        //    else
        //        //    {
        //        //        DataTable dt0 = new ProjectBLL().GetEmployeeNoAndTaskNo(projectID);
        //        //        LogHelper.WriteLine("dt0 is null:" + (dt0 == null));
        //        //        if (dt0 != null && dt0.Rows.Count > 0)
        //        //        {
        //        //            //任务目录（任务编号）
        //        //            string taskNo = dt0.Rows[0]["taskNo"].ToString();
        //        //            //员工编号
        //        //            string employeeNo = dt0.Rows[0]["employeeno"].ToString();
        //        //            //当前员工的文件存储目录
        //        //            string currentEmpPath = string.Format("{0}{1}", employeeRootPath, employeeNo);
        //        //            LogHelper.WriteLine("currentEmpPath:" + currentEmpPath);
        //        //            foreach (DirectoryInfo taskFolder in new DirectoryInfo(currentEmpPath).GetDirectories())
        //        //            {
        //        //                if (taskFolder.Name.StartsWith(taskNo))
        //        //                {
        //        //                    //存放修改记录的文件夹名
        //        //                    string modifyRecordFolder = ConfigurationManager.AppSettings["modifyRecordFolderName"].ToString();
        //        //                    LogHelper.WriteLine("modifyRecordFolder:" + modifyRecordFolder);
        //        //                    //当前任务的修改目录全名
        //        //                    string modifyFolderFullName = string.Format("{0}\\{1}\\{2}", currentEmpPath, taskFolder.Name, modifyRecordFolder);
        //        //                    if (!Directory.Exists(modifyFolderFullName))
        //        //                    {
        //        //                        Directory.CreateDirectory(modifyFolderFullName);
        //        //                        //如果当前任务还没有创建“修改记录”目录的话，就创建一个，顺便把“修改1”也创建了。PS：他爸都没有，怎么可能有儿子？
        //        //                    }
        //        //                    LogHelper.WriteLine("修改1目录:");
        //        //                    //修改1目录
        //        //                    string modifyFolderNameOne = modifyFolderFullName + "\\修改1\\";
        //        //                    LogHelper.WriteLine("modifyFolderNameOne:" + modifyFolderNameOne);
        //        //                    if (!Directory.Exists(modifyFolderNameOne))
        //        //                    {
        //        //                        LogHelper.WriteLine("Directory.Exists(modifyFolderNameOne)");
        //        //                        Directory.CreateDirectory(modifyFolderNameOne);
        //        //                        fupd.SaveAs(modifyFolderNameOne +  fupd.FileName);
        //        //                        string zipedFileName = modifyFolderNameOne +  fupd.FileName;
        //        //                        string upZipDirectory = modifyFolderNameOne +  fupd.FileName.Replace(Path.GetExtension(fupd.FileName), string.Empty);
        //        //                        ZipHelper.UnZipFiles(zipedFileName, upZipDirectory, string.Empty);
        //        //                        File.Delete(zipedFileName);
        //        //                        FillModifyTaskForm();
        //        //                        Alert("上传修改任务成功！");
        //        //                        return; 
        //        //                    }
        //        //                }
        //        //            }
        //        //        }
        //        //    }
        //        //}
        //        //catch (Exception ex)
        //        //{
        //        //    LogHelper.WriteLine(ex.Message);
        //        //    string msg = ex.Message.Replace("'", "'") + "<br />" + ex.StackTrace;
        //        //    Alert("程序出错！<br />" + msg);
        //        //}
        //        #endregion
        //        LogHelper.WriteLine("进入开头");
        //        //if (Path.GetExtension(fupd.FileName).ToLower() != ".zip")
        //        //{
        //        //    Alert("请上传扩展名为“.zip”的压缩文件！");
        //        //    return;
        //        //}
        //        //所有员工的根目录
        //        string employeeRootPath = ConfigurationManager.AppSettings["employeePath"];

        //        //任务类型
        //        string taskType = hidTaskType.Value;
        //        //新任务
        //        if (taskType == "0")
        //        {
        //            /* 2017-03-09，修改该IF下的代码。需求变更，改为直接上传到FTP服务器的“未分配”目录下，并自动生成任务书。*/

        //            //根据projectID获得任务实体
        //            Project project = pBll.GetModel(projectID);
        //            if (project != null)
        //            {
        //                LogHelper.WriteLine("project is null:" + (project == null));
        //                //任务目录（即任务编号）
        //                string taskNo = project.TASKNO;

        //                //新任务分配目录 
        //                string taskAllotmentPath = ConfigurationManager.AppSettings["taskAllotmentPath"];
        //                //任务书内容
        //                string assignmentBookText = project.ASSIGNMENTBOOK;// dt.Rows[0]["assignmentbook"].ToString();
        //                //新任务的路径
        //                string newTaskPath = string.Format("{0}\\{1}", taskAllotmentPath, taskNo);
        //                if (!Directory.Exists(newTaskPath))
        //                {
        //                    Directory.CreateDirectory(newTaskPath);
        //                }
        //                //上传文件的保存路径
        //                string savePath = string.Format("{0}\\{1}", newTaskPath, fupd.FileName);
        //                try
        //                {
        //                    //保存文件到服务器
        //                    fupd.SaveAs(savePath);
        //                    //this.InputFile1.MoveTo(savePath, Brettle.Web.NeatUpload.MoveToOptions.Overwrite);
        //                    //创建任务书存放目录
        //                    string assignmentBookDirectory = string.Format("{0}\\{1}", newTaskPath, "任务书");
        //                    if (!Directory.Exists(assignmentBookDirectory))
        //                    {
        //                        Directory.CreateDirectory(assignmentBookDirectory);
        //                    }
        //                    //创建任务书文件存放路径
        //                    string assignmentBookFilePath = string.Format("{0}\\{1}", assignmentBookDirectory, "任务书.txt");
        //                    using (StreamWriter sw = new StreamWriter(assignmentBookFilePath, false))
        //                    {
        //                        sw.Write(assignmentBookText);
        //                    }
        //                    Alert("上传成功！");
        //                }
        //                catch (Exception ex)
        //                {
        //                    LogHelper.WriteLine(ex.Message);
        //                    Alert("上传失败！");
        //                }
        //            }
        //        }
        //        //售后修改任务
        //        else if (taskType == "1")
        //        {

        //        }
        //        //string taskType = this.hidTaskType.Value;//任务类型：0，普通任务；1，售后。
        //        string projectOrModifyID = hidProjectOrModifyID.Value;//任务或者售后的ID
        //        try
        //        {
        //            //获得修改稿的dt
        //            DataTable dt = new ProjectBLL().GetProjectModifyByPrjID(projectID);
        //            //LogHelper.WriteLine("获得修改稿的dtCount " + dt.Rows.Count);
        //            LogHelper.WriteLine("dt is null:" + (dt == null));
        //            if (dt != null && dt.Rows.Count > 0)
        //            {
        //                //任务目录（任务编号）
        //                string taskNo = dt.Rows[0]["taskNo"].ToString();
        //                //员工编号
        //                string employeeNo = dt.Rows[0]["employeeno"].ToString();
        //                if (taskType == "0")
        //                {
        //                    //2017-03-09，修改该IF下的代码，需求变更，改为直接上传到FTP服务器的“未分配”目录下，并自动生成任务书
        //                    //新任务上传目录
        //                    string taskAllotmentPath = ConfigurationManager.AppSettings["taskAllotmentPath"].TrimEnd('\\');
        //                    //任务书内容
        //                    string assignmentBookText = dt.Rows[0]["assignmentbook"].ToString();
        //                    //新任务的路径
        //                    string newTaskPath = string.Format("{0}\\{1}", taskAllotmentPath, taskNo);
        //                    if (!Directory.Exists(newTaskPath))
        //                    {
        //                        Directory.CreateDirectory(newTaskPath);
        //                    }
        //                    string savePath = string.Format("{0}\\{1}", newTaskPath, fupd.FileName);
        //                    try
        //                    {
        //                        fupd.SaveAs(savePath);
        //                        Alert("上传成功！");
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        LogHelper.WriteLine(ex.Message);
        //                        Alert("上传失败！");
        //                    }
        //                }
        //                else if (taskType == "1")
        //                {
        //                    //当前员工的文件存储目录
        //                    string currentEmpPath = string.Format("{0}{1}", employeeRootPath, employeeNo);
        //                    foreach (DirectoryInfo taskFolder in new DirectoryInfo(currentEmpPath).GetDirectories())
        //                    {
        //                        //找到当前修改记录对应的任务目录
        //                        if (taskFolder.Name.StartsWith(taskNo))
        //                        {
        //                            //存放修改记录的文件夹名
        //                            string modifyRecordFolder = ConfigurationManager.AppSettings["modifyRecordFolderName"].ToString();
        //                            LogHelper.WriteLine("modifyRecordFolder:" + modifyRecordFolder);
        //                            int modifyTaskAmount = 0;
        //                            foreach (DirectoryInfo taskFolderChild in taskFolder.GetDirectories())
        //                            {
        //                                //如果当前目录是修改记录的目录
        //                                if (taskFolderChild.Name.Trim() == modifyRecordFolder)
        //                                {
        //                                    //遍历修改目录的子目录
        //                                    foreach (DirectoryInfo modifyFolders in taskFolderChild.GetDirectories())
        //                                    {
        //                                        string modifyFolderName = modifyFolders.Name;
        //                                        //string dtModifyFolderName = dt.Rows[0]["foldername"].ToString();
        //                                        ////找到需要的目录名下载之
        //                                        //if (modifyFolderName.Contains("完成") && modifyFolderName.StartsWith(dtModifyFolderName))
        //                                        //{
        //                                        //    string destinationFileName = modifyFolders.FullName + ".zip";
        //                                        //    DownLoad(destinationFileName);
        //                                        //}
        //                                        //不包含“完成”二字的目录，作为修改记录的数量
        //                                        if (!modifyFolderName.Contains("完成"))
        //                                        {
        //                                            modifyTaskAmount++;
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            //当前任务的修改目录全名
        //                            string modifyFolderFullName = string.Format("{0}\\{1}\\{2}", currentEmpPath, taskFolder.Name, modifyRecordFolder);
        //                            //如果修改记录数量为0，那么就要判断当前任务有没有“修改记录”这个子目录了
        //                            if (modifyTaskAmount == 0)
        //                            {
        //                                LogHelper.WriteLine("如果当前任务还没有创建“修改记录”目录的话，就创建一个，顺便把“修改1”也创建了。PS：他爸都没有，怎么可能有儿子？");
        //                                //如果当前任务还没有创建“修改记录”目录的话，就创建一个，顺便把“修改1”也创建了。PS：他爸都没有，怎么可能有儿子？
        //                                if (!Directory.Exists(modifyFolderFullName))
        //                                {
        //                                    string modifyFolderNameOne = modifyFolderFullName + "\\修改1\\";
        //                                    Directory.CreateDirectory(modifyFolderNameOne);
        //                                    fupd.SaveAs(modifyFolderNameOne + fupd.FileName);
        //                                    string zipedFileName = modifyFolderNameOne + fupd.FileName;
        //                                    string upZipDirectory = modifyFolderNameOne + fupd.FileName.Replace(Path.GetExtension(fupd.FileName), string.Empty);
        //                                    ZipHelper.UnZipFiles(zipedFileName, upZipDirectory, string.Empty);
        //                                    File.Delete(zipedFileName);
        //                                    FillModifyTaskForm();
        //                                    Alert("上传修改任务成功！");
        //                                    return;
        //                                }
        //                            }
        //                            //如果已经有过修改记录，就自增1作为新的目录名
        //                            else
        //                            {
        //                                LogHelper.WriteLine("如果已经有过修改记录，就自增1作为新的目录名");
        //                                //新的修改任务的目录序号
        //                                int modifySequence = modifyTaskAmount + 1;
        //                                //新的修改任务的目录名
        //                                string addModifyFolderName = string.Format("{0}{1}", "修改", modifySequence);
        //                                //待添加的任务目录的全名
        //                                string addModifyFolderFullName = string.Format("{0}\\{1}", modifyFolderFullName, addModifyFolderName);
        //                                //如果待添加的任务目录名不存在
        //                                LogHelper.WriteLine("//如果待添加的任务目录名不存在");
        //                                if (!Directory.Exists(addModifyFolderFullName))
        //                                {
        //                                    Directory.CreateDirectory(addModifyFolderFullName);
        //                                }
        //                                //定义上载到服务器的文件路径
        //                                string uploadPath = string.Format("{0}\\{1}", addModifyFolderFullName, fupd.FileName);
        //                                LogHelper.WriteLine("//定义上载到服务器的文件路径");
        //                                //进行上载
        //                                fupd.SaveAs(uploadPath);
        //                                string modifyFolderNameOne = modifyFolderFullName + "\\修改1\\";
        //                                string zipedFileName = modifyFolderNameOne + fupd.FileName;
        //                                string upZipDirectory = modifyFolderNameOne + fupd.FileName.Replace(Path.GetExtension(fupd.FileName), string.Empty);
        //                                ZipHelper.UnZipFiles(zipedFileName, upZipDirectory, string.Empty);
        //                                File.Delete(zipedFileName);
        //                                FillModifyTaskForm();
        //                                Alert("上传修改任务成功！");
        //                                return;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //            //“修改记录”目录没有文件的话
        //            else
        //            {
        //                DataTable dt0 = new ProjectBLL().GetEmployeeNoAndTaskNo(projectID);
        //                LogHelper.WriteLine("dt0 is null:" + (dt0 == null));
        //                if (dt0 != null && dt0.Rows.Count > 0)
        //                {
        //                    //任务目录（任务编号）
        //                    string taskNo = dt0.Rows[0]["taskNo"].ToString();
        //                    //员工编号
        //                    string employeeNo = dt0.Rows[0]["employeeno"].ToString();
        //                    //当前员工的文件存储目录
        //                    string currentEmpPath = string.Format("{0}{1}", employeeRootPath, employeeNo);
        //                    LogHelper.WriteLine("currentEmpPath:" + currentEmpPath);
        //                    foreach (DirectoryInfo taskFolder in new DirectoryInfo(currentEmpPath).GetDirectories())
        //                    {
        //                        if (taskFolder.Name.StartsWith(taskNo))
        //                        {
        //                            //存放修改记录的文件夹名
        //                            string modifyRecordFolder = ConfigurationManager.AppSettings["modifyRecordFolderName"].ToString();
        //                            LogHelper.WriteLine("modifyRecordFolder:" + modifyRecordFolder);
        //                            //当前任务的修改目录全名
        //                            string modifyFolderFullName = string.Format("{0}\\{1}\\{2}", currentEmpPath, taskFolder.Name, modifyRecordFolder);
        //                            if (!Directory.Exists(modifyFolderFullName))
        //                            {
        //                                Directory.CreateDirectory(modifyFolderFullName);
        //                                //如果当前任务还没有创建“修改记录”目录的话，就创建一个，顺便把“修改1”也创建了。PS：他爸都没有，怎么可能有儿子？
        //                            }
        //                            LogHelper.WriteLine("修改1目录:");
        //                            //修改1目录
        //                            string modifyFolderNameOne = modifyFolderFullName + "\\修改1\\";
        //                            LogHelper.WriteLine("modifyFolderNameOne:" + modifyFolderNameOne);
        //                            if (!Directory.Exists(modifyFolderNameOne))
        //                            {
        //                                LogHelper.WriteLine("Directory.Exists(modifyFolderNameOne)");
        //                                Directory.CreateDirectory(modifyFolderNameOne);
        //                                fupd.SaveAs(modifyFolderNameOne + fupd.FileName);
        //                                string zipedFileName = modifyFolderNameOne + fupd.FileName;
        //                                string upZipDirectory = modifyFolderNameOne + fupd.FileName.Replace(Path.GetExtension(fupd.FileName), string.Empty);
        //                                ZipHelper.UnZipFiles(zipedFileName, upZipDirectory, string.Empty);
        //                                File.Delete(zipedFileName);
        //                                FillModifyTaskForm();
        //                                Alert("上传修改任务成功！");
        //                                return;
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            LogHelper.WriteLine(ex.Message);
        //            string msg = ex.Message.Replace("'", "'") + "<br />" + ex.StackTrace;
        //            Alert("程序出错！" + msg);
        //        }
        //    }
        //}
        #endregion

        #region 删除售后任务   //discarded
        /// <summary>
        /// 售后任务删除按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnModifyDelete_Click(object sender, EventArgs e)
        {
            //ModifyTaskDetete();
        }

        ///// <summary>
        ///// 删除修改任务
        ///// </summary>
        //private void ModifyTaskDetete()
        //{
        //    string prjModifyID = hidDeleteID.Value;
        //    string employeeRootPath = ConfigurationManager.AppSettings["employeePath"];
        //    //获得修改稿的dt
        //    DataTable dt = new ProjectBLL().GetProjectModifyByModifyID(prjModifyID);
        //    //删除文件目录
        //    bool bDelFolder = false;
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        //任务目录（任务编号）
        //        string taskNo = dt.Rows[0]["taskNo"].ToString();
        //        //员工编号
        //        string employeeNo = dt.Rows[0]["employeeno"].ToString();
        //        //修改目录的子目录名，如“修改1”、“修改2”
        //        string modifyTaskItem = dt.Rows[0]["folderName"].ToString();
        //        //当前员工的文件存储目录
        //        string currentEmpPath = string.Format("{0}{1}", employeeRootPath, employeeNo);
        //        foreach (DirectoryInfo taskFolder in new DirectoryInfo(currentEmpPath).GetDirectories())
        //        {
        //            //找到当前修改记录对应的任务目录
        //            if (taskFolder.Name.StartsWith(taskNo))
        //            {
        //                //存放修改记录的文件夹名
        //                string modifyRecordFolder = ConfigurationManager.AppSettings["modifyRecordFolderName"].ToString();
        //                //int modifyTaskAmount = 0;
        //                foreach (DirectoryInfo taskFolderChild in taskFolder.GetDirectories())
        //                {
        //                    //如果当前目录是修改记录的目录
        //                    if (taskFolderChild.Name.Trim() == modifyRecordFolder)
        //                    {
        //                        //遍历修改目录的子目录
        //                        foreach (DirectoryInfo modifyFolders in taskFolderChild.GetDirectories())
        //                        {
        //                            string modifyFolderName = modifyFolders.Name;
        //                            //string dtModifyFolderName = dt.Rows[0]["foldername"].ToString();
        //                            ////找到需要的目录名下载之
        //                            //if (modifyFolderName.Contains("完成") && modifyFolderName.StartsWith(dtModifyFolderName))
        //                            //{
        //                            //    string destinationFileName = modifyFolders.FullName + ".zip";
        //                            //    DownLoad(destinationFileName);
        //                            //}
        //                            //不包含“完成”二字的目录，作为修改记录的数量
        //                            //if (!modifyFolderName.Contains("完成"))
        //                            //{
        //                            //    modifyTaskAmount++;
        //                            //}
        //                            if (modifyFolderName.StartsWith(modifyTaskItem))
        //                            {
        //                                if (Directory.Exists(modifyFolders.FullName))
        //                                {
        //                                    Directory.Delete(modifyFolders.FullName, true);
        //                                    bDelFolder = true;
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    //删除数据库数据
        //    bool bDelData = false;
        //    //如果成功删除了修改记录的目录
        //    if (bDelFolder)
        //    {
        //        bDelData = new ProjectBLL().DeleteProjectModifyTask(prjModifyID);
        //    }
        //    else
        //    {
        //        Alert("目录删除失败！");
        //        return;
        //    }
        //    //成功删除了数据库数据
        //    if (!bDelData)
        //    {
        //        Alert("数据库删除失败！");
        //        return;
        //    }
        //    //如果目录和数据库均删除成功
        //    if (bDelFolder && bDelData)
        //    {
        //        FillModifyTaskForm();
        //        Alert("删除成功！");
        //    }
        //}
        #endregion

        #region 删除任务
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (hidIsSuperAdmin.Value != "0") // 0，指示超级管理员，超管才可删除任务
            {
                return;
            }
            bool delFlag = pBll.LogicDelete(projectID);
            if (delFlag)
            {
                ExecuteScript("AlertDialog('删除成功！', null);window.close();");
            }
            else
            {
                ExecuteScript("AlertDialog('删除失败！', null);");
            }
            return;
        }
        #endregion

        #region 上传资料
        ///// <summary>
        ///// 上传资料按钮点击事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnUploadTaskData_Click(object sender, EventArgs e)
        //{
        //    if (string.IsNullOrEmpty(projectID) && Session["projectID"] == null)
        //    {
        //        ExecuteScript("AlertDialog('请先生成任务！', null);");
        //        return;
        //    }
        //    Button btnSender = sender as Button;
        //    UploadTaskData(btnSender);
        //}

        ///// <summary>
        ///// 上传任务资料
        ///// </summary>
        ///// <param name="button">事件发送者</param>
        //private void UploadTaskData(Button button)
        //{
        //    //1.如果是任务资料上传按钮
        //    if (button.ID == "btnUploadTaskData")
        //    {
        //        //if (fupTaskData.HasFile)//如果有文件
        //        if (InputFile1.HasFile)
        //        {
        //            /* 2017-03-09，修改该段代码。需求变更，改为直接上传到FTP服务器的“未分配”目录下。*/

        //            //根据projectID获得任务实体
        //            string projectID0 = projectID == null ? Convert.ToString(Session["projectID"]) : projectID;
        //            Project project = pBll.GetModel(projectID0);
        //            if (project != null)
        //            {
        //                if (!string.IsNullOrEmpty(project.FINISHEDPERSON))
        //                {
        //                    ExecuteScript("AlertDialog('当前任务已分配，不再支持上传！', null);");
        //                    return;
        //                }
        //                LogHelper.WriteLine("project is null:" + (project == null));
        //                //任务目录（即任务编号）
        //                string taskNo = project.TASKNO;

        //                //新任务分配目录
        //                string taskAllotmentPath = ConfigurationManager.AppSettings["taskAllotmentPath"];
        //                //任务书内容
        //                string assignmentBookText = project.ASSIGNMENTBOOK;// dt.Rows[0]["assignmentbook"].ToString();
        //                //新任务的路径
        //                string newTaskPath = string.Format("{0}\\{1}", taskAllotmentPath, taskNo);
        //                //任务资料是上传到“任务书”的文件目录里的
        //                string newAssignmentBookPath = string.Format("{0}\\{1}", newTaskPath, "任务书");
        //                //首次上传目录应该是不存在的，故先生成
        //                if (!Directory.Exists(newAssignmentBookPath))
        //                {
        //                    Directory.CreateDirectory(newAssignmentBookPath);
        //                }
        //                //上传文件的保存路径
        //                string savePath = string.Format("{0}\\{1}", newAssignmentBookPath, /*fupTaskData.FileName*/ InputFile1.FileName);
        //                try
        //                {
        //                    //保存文件到服务器
        //                    //fupTaskData.SaveAs(savePath);
        //                    this.InputFile1.MoveTo(savePath, Brettle.Web.NeatUpload.MoveToOptions.Overwrite);

        //                    //fupd.SaveAs(Server.MapPath("~\\Upload\\") + fupTaskData.FileName);    //！！！！！这个不用管

        //                    //fupd.PostedFile.SaveAs(savePath);
        //                    //fupd.SaveAs("f:\\D850E6D413BE_20161214083735.jpg");
        //                    //相对路径
        //                    string relativePath = string.Format("{0}\\{1}\\{2}", taskNo, "任务书", /*fupTaskData.FileName*/InputFile1.FileName);
        //                    //文件名
        //                    string fileName = /*fupTaskData.FileName*/ InputFile1.FileName;
        //                    //扩展名
        //                    string extension = Path.GetExtension(fileName);
        //                    Attachment att = new Attachment();
        //                    att.ID = Guid.NewGuid().ToString();
        //                    att.TASKID = projectID0;
        //                    att.TASKTYPE = 0;
        //                    att.FILENAME = fileName;
        //                    att.FILEFULLNAME = relativePath;
        //                    att.EXTENSION = extension;
        //                    bool isAdd = aBll.Add(att);
        //                    if (isAdd)
        //                    {
        //                        GridViewTaskDataBind(projectID0, string.Empty);
        //                        GridViewModifyTaskDataBind(projectID);

        //                        string url = "UpLoad/" + relativePath; //文件保存的路径  
        //                        float FileSize = (float)System.Math.Round((float)InputFile1.ContentLength / 1024000, 1); //获取文件大小并保留小数点后一位,单位是M

        //                        Alert("上传成功！");
        //                    }
        //                    else
        //                    {
        //                        //数据添加失败，则删除文件
        //                        if (File.Exists(savePath))
        //                        {
        //                            File.Delete(savePath);
        //                        }
        //                        Alert("上传失败！");
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    LogHelper.WriteLine(ex.Message);
        //                    Alert("上传失败！");
        //                }
        //            }
        //        }
        //    }
        //    //2.如果是修改记录上传按钮
        //    else if (button.ID == "btnUploadModifyData")
        //    {
        //        string projectID0 = projectID == null ? Convert.ToString(Session["projectID"]) : projectID;
        //        //任务尚未分配，不允许上传
        //        DataTable dt = pBll.GetEmployeeNoAndTaskNo(projectID0);
        //        if (dt == null || dt.Rows.Count == 0)
        //        {
        //            ExecuteScript("AlertDialog('当前任务暂未分配，不允许上传修改任务！', null);");
        //            return;
        //        }
        //        ////有待审核的修改任务，不允许上传
        //        //bool isExist = pBll.IsExistModifyTaskWaitforReview(projectID0);
        //        //if (isExist)
        //        //{
        //        //    ExecuteScript("AlertDialog('当前有待审核的修改任务，不允许上传新的修改任务！', null);");
        //        //    return;
        //        //}
        //        if (fupModifyData.HasFile)
        //        {
        //            //修改任务分配目录
        //            string modifyTaskAllotmentPath = ConfigurationManager.AppSettings["modifyTaskAllotmentPath"].TrimEnd('\\');
        //            string employeeNo = dt.Rows[0]["employeeno"].ToString();
        //            string taskNo = dt.Rows[0]["taskno"].ToString();
        //            //修改任务存放目录，形如F:\Temporary\Cache\Dictionary\客服\修改未分配\004-2017031411-31703111500
        //            string newModifyPath = string.Format("{0}\\{1}-{2}", modifyTaskAllotmentPath, employeeNo, taskNo);
        //            if (!Directory.Exists(newModifyPath))
        //            {
        //                Directory.CreateDirectory(newModifyPath);
        //            }
        //            StringBuilder modifyFolder = new StringBuilder();
        //            //修改任务的序号
        //            int times = pmBll.GetModifyTaskCount(projectID0) + 1;
        //            //为0的话，需要将父级目录“修改记录”文件夹一同创建
        //            if (times == 1)
        //            {
        //                string modifyRecordFolderName = ConfigurationManager.AppSettings["modifyRecordFolderName"].TrimEnd('\\');
        //                modifyFolder.AppendFormat("{0}\\修改1", modifyRecordFolderName);
        //            }
        //            //否则直接创建新序号修改目录
        //            else
        //            {
        //                modifyFolder.AppendFormat("修改{0}", times);
        //            }
        //            //完整的修改任务项目目录
        //            string modifyTaskItemPath = string.Format("{0}\\{1}", newModifyPath, modifyFolder);
        //            //不存在则创建
        //            if (!Directory.Exists(modifyTaskItemPath))
        //            {
        //                Directory.CreateDirectory(modifyTaskItemPath);
        //            }
        //            //保存路径
        //            //F:\Temporary\Cache\Dictionary\客服\修改未分配\004-2017031411-31703111500\修改记录\修改1\修改要求.zip
        //            string modifyFileSavePath = string.Format("{0}\\{1}", modifyTaskItemPath, fupModifyData.FileName);
        //            try
        //            {
        //                //上传
        //                fupModifyData.SaveAs(modifyFileSavePath);

        //                ProjectModify prjMdy = new ProjectModify();
        //                bool addPrjMdyFlag = true;
        //                //获取是否有未审核的修改任务
        //                DataSet dsPm = pmBll.GetList(string.Format(" PROJECTID='{0}' AND REVIEWSTATUS=0 ", projectID0));
        //                //存在待审核的，就不添加记录了
        //                if (dsPm != null && dsPm.Tables.Count > 0 && dsPm.Tables[0].Rows.Count > 0)
        //                {

        //                }
        //                //否则，就添加一条修改任务记录
        //                else
        //                {
        //                    prjMdy.ID = Guid.NewGuid().ToString();
        //                    prjMdy.PROJECTID = projectID0;
        //                    prjMdy.FOLDERNAME = modifyFolder.ToString();
        //                    prjMdy.ISUPLOADATTACH = 0;
        //                    prjMdy.ISFINISHED = 0;
        //                    prjMdy.REVIEWSTATUS = 0;
        //                    prjMdy.CREATEDATE = DateTime.Now;
        //                    addPrjMdyFlag = pmBll.Add(prjMdy);
        //                }
        //                if (addPrjMdyFlag)
        //                {
        //                    Attachment att = new Attachment();
        //                    att.ID = Guid.NewGuid().ToString();
        //                    att.TASKID = projectID0;
        //                    att.TASKTYPE = 1;
        //                    att.FILENAME = fupModifyData.FileName;
        //                    att.FILEFULLNAME = string.Format("{0}-{1}\\{2}\\{3}", employeeNo, taskNo, modifyFolder.ToString(), fupModifyData.FileName);
        //                    att.EXTENSION = Path.GetExtension(fupModifyData.FileName);
        //                    att.CREATEDATE = DateTime.Now;
        //                    bool addAttFlag = aBll.Add(att);
        //                    //上传成功
        //                    if (addAttFlag)
        //                    {
        //                        GridViewTaskDataBind(projectID0, string.Empty);
        //                        GridViewModifyTaskDataBind(projectID);
        //                        ExecuteScript("AlertDialog('上传成功！', null);");
        //                        return;
        //                    }
        //                    //上传失败
        //                    else
        //                    {
        //                        pmBll.Delete(prjMdy.ID);
        //                        if (File.Exists(modifyFileSavePath))
        //                        {
        //                            File.Delete(modifyFileSavePath);
        //                        }
        //                        ExecuteScript("AlertDialog('上传失败！', null);");
        //                        return;
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                LogHelper.WriteLine("上传修改任务失败\r\n" + ex.Message + ex.StackTrace);
        //                ExecuteScript("AlertDialog('上传失败！', null);");
        //                return;
        //            }
        //        }
        //    }
        //}
        #endregion

        //protected void btnFinalDataDownload_Click(object sender, EventArgs e)
        //{

        //}

        #region 所有GridView的RowCommand
        ///// <summary>
        ///// 所有附件GridView的RowCommand
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void gvTaskData_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    GridView gv = sender as GridView;
        //    //判断sender
        //    #region 1.任务资料
        //    //===================================== 1.任务资料 =====================================
        //    if (gv.ID == "gvTaskData")
        //    {
        //        if (!string.IsNullOrEmpty(e.CommandName) && e.CommandName == "del")
        //        {
        //            string attID = Convert.ToString(e.CommandArgument);
        //            Attachment att = aBll.GetModel(attID);
        //            if (att != null)
        //            {
        //                //新任务分配目录
        //                string taskAllotmentPath = ConfigurationManager.AppSettings["taskAllotmentPath"].TrimEnd('\\');
        //                string attFullName = string.Format("{0}\\{1}", taskAllotmentPath, att.FILEFULLNAME);
        //                if (File.Exists(attFullName))
        //                {
        //                    try
        //                    {
        //                        File.Delete(attFullName);
        //                        bool delFlag = aBll.Delete(attID);
        //                        if (delFlag)
        //                        {
        //                            string projectID0 = projectID == null ? Convert.ToString(Session["projectID"]) : projectID;
        //                            GridViewTaskDataBind(projectID0, string.Empty);
        //                            ExecuteScript("AlertDialog('删除成功！',null);");
        //                        }
        //                        else
        //                        {
        //                            ExecuteScript("AlertDialog('删除失败！',null);");
        //                            return;
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        LogHelper.WriteLine("任务资料删除失败，" + ex.Message + ex.StackTrace);
        //                        ExecuteScript("AlertDialog('删除失败！',null);");
        //                        return;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                ExecuteScript("AlertDialog('对象不存在，删除失败！',null);");
        //                return;
        //            }
        //        }
        //    }
        //    #endregion
        //    #region 2.完成稿
        //    //===================================== 2.完成稿 =====================================
        //    else if (gv.ID == "gvFinalData")
        //    {
        //        if (e.CommandName == "download")
        //        {
        //            try
        //            {
        //                string employeeRootPath = ConfigurationManager.AppSettings["employeePath"];
        //                string projectID = Convert.ToString(e.CommandArgument.ToString().Split('|')[0]);
        //                string finishedPerson = Convert.ToString(e.CommandArgument.ToString().Split('|')[1]);
        //                DataTable dt = null;
        //                if (string.IsNullOrEmpty(finishedPerson))
        //                {
        //                    dt = new ProjectBLL().GetFinalScript(projectID);
        //                }
        //                else
        //                {
        //                    dt = new ProjectBLL().GetFinalScript(projectID, finishedPerson);
        //                }
        //                if (dt != null && dt.Rows.Count > 0)
        //                {
        //                    string taskNo = dt.Rows[0]["taskno"].ToString();
        //                    string employeeNo = dt.Rows[0]["EMPLOYEENO"].ToString();
        //                    string currentEmpPath = string.Format("{0}{1}", employeeRootPath, employeeNo);
        //                    foreach (DirectoryInfo taskFolder in new DirectoryInfo(currentEmpPath).GetDirectories())
        //                    {
        //                        if (taskFolder.Name.StartsWith(taskNo))
        //                        {
        //                            foreach (DirectoryInfo taskFolderChild in new DirectoryInfo(taskFolder.FullName).GetDirectories())
        //                            {
        //                                if (taskFolderChild.Name == "完成稿")
        //                                {
        //                                    string fileName = taskFolderChild.FullName + ".zip";
        //                                    if (File.Exists(fileName))
        //                                    {
        //                                        DownLoad(fileName);
        //                                    }
        //                                    else
        //                                    {
        //                                        ExecuteScript("AlertDialog('文件不存在或已被移动！',null);");
        //                                        return;
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    ExecuteScript("AlertDialog('未查询到相应的数据库记录！',null);");
        //                    return;
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                LogHelper.WriteLine(string.Format("{0}\r\n{1}", "下载出错", ex.Message));
        //                ExecuteScript("AlertDialog('下载出错！',null);");
        //                return;
        //            }
        //        }
        //    }
        //    #endregion
        //    #region 3.修改稿
        //    //===================================== 3.修改稿 =====================================
        //    else if (gv.ID == "gvModify")
        //    {
        //        //========================= (1)上传 =========================
        //        if (e.CommandName == "upload")
        //        {
        //            string projectID0 = projectID == null ? Convert.ToString(Session["projectID"]) : projectID;
        //            //任务尚未分配，不允许上传
        //            DataTable dt = pBll.GetEmployeeNoAndTaskNo(projectID0);
        //            //修改任务分配目录
        //            string modifyTaskAllotmentPath = ConfigurationManager.AppSettings["modifyTaskAllotmentPath"].TrimEnd('\\');
        //            string employeeNo = dt.Rows[0]["employeeno"].ToString();
        //            string taskNo = dt.Rows[0]["taskno"].ToString();
        //            string newModifyPath = string.Format("{0}\\{1}-{2}", modifyTaskAllotmentPath, employeeNo, taskNo);//F:\Temporary\Cache\Dictionary\客服\修改未分配\004-2017031411-31703111500
        //            StringBuilder modifyFolder = new StringBuilder();

        //            ProjectModify pmModel = new ProjectModify();
        //            pmModel = pmBll.GetModel("");
        //            string modifyFoderName = pmModel.FOLDERNAME;
        //            string modifyFullName = string.Format("{0}\\{1}", newModifyPath, modifyFoderName);

        //            if (Directory.Exists(modifyFullName))
        //            {
        //                DirectoryInfo dirInfo = new DirectoryInfo(modifyFullName);
        //                foreach (System.IO.FileSystemInfo fsi in dirInfo.GetFileSystemInfos())
        //                {
        //                    //如果是文件夹
        //                    if (fsi.Attributes == FileAttributes.Directory)
        //                    {
        //                        if (Directory.Exists(fsi.FullName))
        //                        {
        //                            Directory.Delete(fsi.FullName);
        //                        }
        //                    }
        //                    //文件
        //                    else
        //                    {
        //                        if (File.Exists(fsi.FullName))
        //                        {
        //                            File.Delete(fsi.FullName);
        //                        }
        //                    }
        //                }
        //            }

        //            //修改任务的序号
        //            int times = pmBll.GetModifyTaskCount(projectID0) + 1;
        //            //为0的话，需要将父级目录“修改记录”文件夹一同创建
        //            if (times == 1)
        //            {
        //                string modifyRecordFolderName = ConfigurationManager.AppSettings["modifyRecordFolderName"].TrimEnd('\\');
        //                modifyFolder.AppendFormat("{0}\\修改1", modifyRecordFolderName);
        //            }
        //            //否则直接创建新序号修改目录
        //            else
        //            {
        //                modifyFolder.AppendFormat("修改{0}", times);
        //            }
        //            //完整的修改任务项目目录
        //            string modifyTaskItemPath = string.Format("{0}\\{1}", newModifyPath, modifyFolder);
        //            //不存在则创建
        //            if (!Directory.Exists(modifyTaskItemPath))
        //            {
        //                Directory.CreateDirectory(modifyTaskItemPath);
        //            }
        //            //保存路径
        //            //F:\Temporary\Cache\Dictionary\客服\修改未分配\004-2017031411-31703111500\修改记录\修改1\修改要求.zip
        //            string modifyFileSavePath = string.Format("{0}\\{1}", modifyTaskItemPath, fupModifyData.FileName);
        //            try
        //            {
        //                //上传
        //                fupModifyData.SaveAs(modifyFileSavePath);

        //                ProjectModify prjMdy = new ProjectModify();
        //                bool addPrjMdyFlag = true;
        //                //获取是否有未审核的修改任务
        //                DataSet dsPm = pmBll.GetList(string.Format(" PROJECTID='{0}' AND REVIEWSTATUS=0 ", projectID0));
        //                //存在待审核的，就不添加记录了
        //                if (dsPm != null && dsPm.Tables.Count > 0 && dsPm.Tables[0].Rows.Count > 0)
        //                {

        //                }
        //                //否则，就添加一条修改任务记录
        //                else
        //                {
        //                    prjMdy.ID = Guid.NewGuid().ToString();
        //                    prjMdy.PROJECTID = projectID0;
        //                    prjMdy.FOLDERNAME = modifyFolder.ToString();
        //                    prjMdy.ISFINISHED = 0;
        //                    prjMdy.REVIEWSTATUS = 0;
        //                    prjMdy.CREATEDATE = DateTime.Now;
        //                    addPrjMdyFlag = pmBll.Add(prjMdy);
        //                }
        //                if (addPrjMdyFlag)
        //                {
        //                    Attachment att = new Attachment();
        //                    att.ID = Guid.NewGuid().ToString();
        //                    att.TASKID = projectID0;
        //                    att.TASKTYPE = 1;
        //                    att.FILENAME = fupModifyData.FileName;
        //                    att.FILEFULLNAME = string.Format("{0}-{1}\\{2}\\{3}", employeeNo, taskNo, modifyFolder.ToString(), fupModifyData.FileName);
        //                    att.EXTENSION = Path.GetExtension(fupModifyData.FileName);
        //                    att.CREATEDATE = DateTime.Now;
        //                    bool addAttFlag = aBll.Add(att);
        //                    //上传成功
        //                    if (addAttFlag)
        //                    {
        //                        GridViewModifyTaskDataBind(projectID0);
        //                        ExecuteScript("AlertDialog('上传成功！', null);");
        //                        return;
        //                    }
        //                    //上传失败
        //                    else
        //                    {
        //                        pmBll.Delete(prjMdy.ID);
        //                        if (File.Exists(modifyFileSavePath))
        //                        {
        //                            File.Delete(modifyFileSavePath);
        //                        }
        //                        ExecuteScript("AlertDialog('上传失败！', null);");
        //                        return;
        //                    }
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                LogHelper.WriteLine("上传修改任务失败\r\n" + ex.Message + ex.StackTrace);
        //                ExecuteScript("AlertDialog('上传失败！', null);");
        //                return;
        //            }
        //        }
        //        // ========================= (2)删除 =========================
        //        else if (e.CommandName == "del")
        //        {
        //            Attachment att = aBll.GetModel(e.CommandArgument.ToString());
        //            if (att != null)
        //            {
        //                //修改任务分配目录
        //                string modifyTaskAllotmentPath = ConfigurationManager.AppSettings["modifyTaskAllotmentPath"].TrimEnd('\\');
        //                string attFullName = string.Format("{0}\\{1}", modifyTaskAllotmentPath, att.FILEFULLNAME);
        //                if (File.Exists(attFullName))
        //                {
        //                    try
        //                    {
        //                        File.Delete(attFullName);
        //                        bool delFlag = aBll.Delete(e.CommandArgument.ToString());
        //                        if (delFlag)
        //                        {
        //                            string projectID0 = projectID == null ? Convert.ToString(Session["projectID"]) : projectID;
        //                            GridViewModifyTaskDataBind(projectID0);
        //                            ExecuteScript("AlertDialog('删除成功！',null);");
        //                        }
        //                        else
        //                        {
        //                            ExecuteScript("AlertDialog('删除失败！',null);");
        //                            return;
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        LogHelper.WriteLine("任务资料删除失败，" + ex.Message + ex.StackTrace);
        //                        ExecuteScript("AlertDialog('删除失败！',null);");
        //                        return;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                ExecuteScript("AlertDialog('数据已被删除！',null);");
        //                return;
        //            }
        //        }
        //        //========================= (3)下载 =========================
        //        else if (e.CommandName == "download")
        //        {
        //            ////修改任务分配目录
        //            //string modifyTaskAllotmentPath = ConfigurationManager.AppSettings["modifyTaskAllotmentPath"].TrimEnd('\\');
        //            //string prjMdyID = e.CommandArgument.ToString();
        //            //DataSet ds = aBll.GetList(string.Format(" taskid='{0}'", prjMdyID));
        //            //DataTable dt = new DataTable();
        //            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            //{
        //            //    dt = ds.Tables[0];
        //            //    string fileFullName = dt.Rows[0]["FILEFULLNAME"].ToString();
        //            //    fileFullName = string.Format("{0}\\{1}", modifyTaskAllotmentPath, fileFullName);
        //            //    if (File.Exists(fileFullName))
        //            //    {
        //            //        DownLoad(fileFullName);
        //            //    }
        //            //    else
        //            //    {
        //            //        ExecuteScript("AlertDialog('文件已被移动或删除，无法下载！',null);");
        //            //        return;
        //            //    }
        //            //}
        //            //else
        //            //{
        //            //    ExecuteScript("AlertDialog('文件已被移动或删除，无法下载！',null);");
        //            //    return;
        //            //}

        //            string employeeRootPath = ConfigurationManager.AppSettings["employeePath"];

        //            //string projectID0 = projectID == null ? Convert.ToString(Session["projectID"]) : projectID;
        //            DataTable dt = new ProjectBLL().GetProjectModifyByModifyID(e.CommandArgument.ToString());
        //            string employeeNo = dt.Rows[0]["employeeNo"].ToString();//员工编号
        //            string taskNo = dt.Rows[0]["taskNo"].ToString();//任务目录
        //            string currentEmpPath = string.Format("{0}{1}", employeeRootPath, employeeNo);//员工目录
        //            //遍历员工目录，即找出各个任务的目录
        //            foreach (DirectoryInfo taskFolder in new DirectoryInfo(currentEmpPath).GetDirectories())
        //            {
        //                //如果目录名是以任务名打头的，说明就是它了，因为员工会在原文件夹名后面加上乱七八糟的东西。如果前面他也敢动，那我也没办法了。
        //                if (taskFolder.Name.StartsWith(taskNo))
        //                {
        //                    //修改记录目录
        //                    string modifyRecordFolder = ConfigurationManager.AppSettings["modifyRecordFolderName"].ToString();
        //                    //遍历单个任务目录下的文件夹
        //                    foreach (DirectoryInfo taskFolderChild in taskFolder.GetDirectories())
        //                    {
        //                        //如果目录名是“修改记录”
        //                        if (taskFolderChild.Name == modifyRecordFolder)
        //                        {
        //                            //进一步遍历每次修改记录目录
        //                            foreach (DirectoryInfo modifyFolder in taskFolderChild.GetDirectories())
        //                            {
        //                                //每次修改记录产生的文件夹
        //                                string modifyFolderName = modifyFolder.Name;
        //                                //数据库中存储的修改记录文件夹，是最初的名称，不带“完成”
        //                                string dtFolderName = dt.Rows[0]["folderName"].ToString();
        //                                //如果目录名包含“完成”并且是当前售后任务打头的，那么就是它了
        //                                if (modifyFolderName == dtFolderName)
        //                                {
        //                                    if (Directory.Exists(modifyFolder.FullName))
        //                                    {
        //                                        string destinationFileName = modifyFolder.FullName + ".zip";
        //                                        //ZipHelper.CreateZipFile(modifyFolder.FullName, destinationFileName);
        //                                        if (File.Exists(destinationFileName))
        //                                        {
        //                                            DownLoad(destinationFileName);
        //                                            break;
        //                                        }
        //                                        else
        //                                        {
        //                                            ExecuteScript("AlertDialog('下载失败，文件已被删除或移动！',null);");
        //                                            return;
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            //break;
        //                        }
        //                    }
        //                }
        //                //break;
        //            }
        //        }
        //    }
        //    #endregion
        //}
        #endregion

        #region 文件上传或替换
        ///// <summary>
        ///// 文件上传或替换
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnFileReplace_Click(object sender, EventArgs e)
        //{
        //    if (Request.Files.Count > 0)
        //    {
        //        string prjMdyID = hidFileReplaceId.Value;
        //        string fileName = Path.GetFileName(hidFileReplaceName.Value);
        //        string projectID0 = projectID == null ? Convert.ToString(Session["projectID"]) : projectID;
        //        for (int i = 0; i < Request.Files.Count; i++)
        //        {
        //            string requestFileName = Path.GetFileName(Request.Files[i].FileName);
        //            if (requestFileName == fileName)
        //            {
        //                HttpPostedFile hpFile = Request.Files[i];
        //                //任务尚未分配，不允许上传
        //                DataTable dt = pBll.GetEmployeeNoAndTaskNo(projectID0);
        //                //修改任务分配目录
        //                string modifyTaskAllotmentPath = ConfigurationManager.AppSettings["modifyTaskAllotmentPath"].TrimEnd('\\');
        //                string employeeNo = dt.Rows[0]["employeeno"].ToString();
        //                string taskNo = dt.Rows[0]["taskno"].ToString();
        //                //形如“F:\Temporary\Cache\Dictionary\客服\修改未分配\004-2017031411-31703111500”
        //                string newModifyPath = string.Format("{0}\\{1}-{2}", modifyTaskAllotmentPath, employeeNo, taskNo);
        //                StringBuilder modifyFolder = new StringBuilder();

        //                ProjectModify pmModel = new ProjectModify();
        //                pmModel = pmBll.GetModel(prjMdyID);
        //                string modifyFoderName = pmModel.FOLDERNAME;
        //                string modifyFullName = string.Format("{0}\\{1}", newModifyPath, modifyFoderName);

        //                //如果存在目录
        //                if (Directory.Exists(modifyFullName))
        //                {
        //                    //定义目录信息并遍历目录下的所有文件及文件夹
        //                    DirectoryInfo dirInfo = new DirectoryInfo(modifyFullName);
        //                    foreach (System.IO.FileSystemInfo fsi in dirInfo.GetFileSystemInfos())
        //                    {
        //                        //删除文件夹
        //                        if (fsi.Attributes == FileAttributes.Directory)
        //                        {
        //                            if (Directory.Exists(fsi.FullName))
        //                            {
        //                                Directory.Delete(fsi.FullName);
        //                            }
        //                        }
        //                        //删除文件
        //                        else
        //                        {
        //                            if (File.Exists(fsi.FullName))
        //                            {
        //                                File.Delete(fsi.FullName);
        //                            }
        //                        }
        //                    }
        //                }
        //                try
        //                {
        //                    string fileSavePath = string.Format("{0}\\{1}", modifyFullName, requestFileName);
        //                    hpFile.SaveAs(fileSavePath);
        //                    pmModel.ISUPLOADATTACH = 1;
        //                    bool updateFlag = pmBll.Update(pmModel);
        //                    if (!updateFlag)
        //                    {
        //                        LogHelper.WriteLine(string.Format("更新ID为[{0}]的修改任务的附件上传状态失败", prjMdyID));
        //                    }
        //                    break;
        //                }
        //                catch (Exception ex)
        //                {
        //                    LogHelper.WriteLine(ex.Message + ex.StackTrace);
        //                }
        //            }
        //        }
        //        GridViewModifyTaskDataBind(projectID0);
        //        ExecuteScript("AlertDialog('文件上传成功！', null);");
        //        return;
        //    }
        //}
        #endregion

        #region 修改任务创建
        ///// <summary>
        ///// 创建修改任务按钮单击事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnCreateModifyTask_Click(object sender, EventArgs e)
        //{
        //    CreateModifyTask();
        //}

        ///// <summary>
        ///// 创建新的修改任务
        ///// </summary>
        //private void CreateModifyTask()
        //{
        //    string projectID0 = projectID == null ? Convert.ToString(Session["projectID"]) : projectID;
        //    //如果projectID和Session["projectID"]均没有值，则可判断任务尚未生成，不允许创建修改任务
        //    if (string.IsNullOrEmpty(projectID0))
        //    {
        //        ExecuteScript("AlertDialog('请先生成任务！', null);");
        //        return;
        //    }
        //    //任务尚未分配，不允许上传
        //    DataTable dt = pBll.GetEmployeeNoAndTaskNo(projectID0);
        //    if (dt == null || dt.Rows.Count == 0)
        //    {
        //        ExecuteScript("AlertDialog('当前任务暂未分配，不允许上传修改任务！', null);");
        //        return;
        //    }
        //    //修改任务分配目录
        //    string modifyTaskAllotmentPath = ConfigurationManager.AppSettings["modifyTaskAllotmentPath"].TrimEnd('\\');
        //    string employeeNo = dt.Rows[0]["employeeno"].ToString();
        //    string taskNo = dt.Rows[0]["taskno"].ToString();
        //    string newModifyPath = string.Format("{0}\\{1}-{2}", modifyTaskAllotmentPath, employeeNo, taskNo);//F:\Temporary\Cache\Dictionary\客服\修改未分配\004-2017031411-31703111500
        //    if (!Directory.Exists(newModifyPath))
        //    {
        //        Directory.CreateDirectory(newModifyPath);
        //    }
        //    StringBuilder modifyFolder = new StringBuilder();
        //    StringBuilder modifyFolderFullName = new StringBuilder();
        //    //修改任务的序号
        //    int times = pmBll.GetModifyTaskCount(projectID0) + 1;
        //    //为0的话，需要将父级目录“修改记录”文件夹一同创建
        //    if (times == 1)
        //    {
        //        string modifyRecordFolderName = ConfigurationManager.AppSettings["modifyRecordFolderName"].TrimEnd('\\');
        //        modifyFolder.Append("修改1");
        //        modifyFolderFullName.AppendFormat("{0}\\修改1", modifyRecordFolderName);
        //    }
        //    //否则直接创建新序号修改目录
        //    else
        //    {
        //        modifyFolder.AppendFormat("修改{0}", times);
        //        modifyFolderFullName.AppendFormat("修改{0}", times);
        //    }
        //    //完整的修改任务项目目录
        //    string modifyTaskItemPath = string.Format("{0}\\{1}", newModifyPath, modifyFolderFullName);
        //    //不存在则创建
        //    //形如 F:\Temporary\Cache\Dictionary\客服\修改未分配\004-2017031411-31703111500\修改记录\修改1
        //    if (!Directory.Exists(modifyTaskItemPath))
        //    {
        //        Directory.CreateDirectory(modifyTaskItemPath);
        //    }
        //    ////保存路径
        //    ////F:\Temporary\Cache\Dictionary\客服\修改未分配\004-2017031411-31703111500\修改记录\修改1\修改要求.zip
        //    //string modifyFileSavePath = string.Format("{0}\\{1}", modifyTaskItemPath, fupModifyData.FileName);
        //    //try
        //    //{
        //    ////上传
        //    //fupModifyData.SaveAs(modifyFileSavePath);

        //    ProjectModify prjMdy = new ProjectModify();
        //    bool addPrjMdyFlag = true;
        //    ////获取是否有未审核的修改任务
        //    //DataSet dsPm = pmBll.GetList(string.Format(" PROJECTID='{0}' AND REVIEWSTATUS=0 ", projectID0));
        //    ////存在待审核的，就不添加记录了
        //    //if (dsPm != null && dsPm.Tables.Count > 0 && dsPm.Tables[0].Rows.Count > 0)
        //    //{

        //    //}
        //    ////否则，就添加一条修改任务记录
        //    //else
        //    //{
        //    prjMdy.ID = Guid.NewGuid().ToString();
        //    prjMdy.PROJECTID = projectID0;
        //    prjMdy.FOLDERNAME = modifyFolder.ToString();
        //    prjMdy.FOLDERFULLNAME = modifyFolderFullName.ToString();
        //    prjMdy.ISFINISHED = 0;
        //    prjMdy.REVIEWSTATUS = 0;
        //    prjMdy.CREATEDATE = DateTime.Now;
        //    addPrjMdyFlag = pmBll.Add(prjMdy);
        //    //}
        //    //if (addPrjMdyFlag)
        //    //{
        //    //Attachment att = new Attachment();
        //    //att.ID = Guid.NewGuid().ToString();
        //    //att.TASKID = projectID0;
        //    //att.TASKTYPE = 1;
        //    //att.FILENAME = fupModifyData.FileName;
        //    //att.FILEFULLNAME = string.Format("{0}-{1}\\{2}\\{3}", employeeNo, taskNo, modifyFolder.ToString(), fupModifyData.FileName);
        //    //att.EXTENSION = Path.GetExtension(fupModifyData.FileName);
        //    //att.CREATEDATE = DateTime.Now;
        //    //bool addAttFlag = aBll.Add(att);
        //    ////上传成功
        //    //if (addAttFlag)
        //    //{
        //    GridViewModifyTaskDataBind(projectID0);
        //    ExecuteScript("AlertDialog('创建成功！', null);");
        //    return;
        //    //}
        //    ////上传失败
        //    //else
        //    //{
        //    //    pmBll.Delete(prjMdy.ID);
        //    //    if (File.Exists(modifyFileSavePath))
        //    //    {
        //    //        File.Delete(modifyFileSavePath);
        //    //    }
        //    //    ExecuteScript("AlertDialog('上传失败！', null);");
        //    //    return;
        //    //}
        //    //}
        //    //}
        //    //catch
        //    //{ } 
        //}
        #endregion

        //[DllImport("Iphlpapi.dll")]
        //private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        //获取MAC地址
        private void GetIP(out string userIP)
        {
            userIP = string.Empty;
            // 在此处放置用户代码以初始化页面
            try
            {
                string userip = Request.UserHostAddress;
                string strClientIP = Request.UserHostAddress.ToString().Trim();
                Int32 ldest = inet_addr(strClientIP); //目的地的ip 
                Int32 lhost = inet_addr("");   //本地服务器的ip 
                Int64 macinfo = new Int64();
                //Int32 len = 6;
                //int res = SendARP(ldest, 0, ref macinfo, ref len);
                string mac_src = macinfo.ToString("X");
                if (mac_src == "0")
                {
                    if (userip == "127.0.0.1")
                    {
                    }
                    else
                    {
                    }
                    userIP = userip;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine("获取IP地址失败。\r\n" + ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 设置任务为已完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSetIsFinished_Click(object sender, EventArgs e)
        {
            Project project = pBll.GetModel(projectID);
            project.ISFINISHED = 1;
            bool updFlag = pBll.Update(project);
            if (updFlag)
            {
                ExecuteScript("AlertDialog('任务已设置为已完成！', null);");
            }
            else
            {
                ExecuteScript("AlertDialog('设置失败！', null);");
                LogHelper.WriteLine("任务置为完成状态失败");
            }
        }

        /// <summary>
        /// 分配任务
        /// 修改记录：2018-06-30，添加方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAssign_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlAssignTo.SelectedValue))
            {
                return;
            }
            try
            {
                // 需要转移对象的 empId
                string employeeID = ddlAssignTo.SelectedValue;
                string transferToEmpNo = eBll.GetModel(employeeID).EMPLOYEENO;
                int errorCode = 0;
                string actPath = string.Empty;
                string taskRootFolder = string.Empty;
                string taskFolderWithoutEmpNo = string.Empty;
                bool flag = new FileCategoryBLL().GetFilePathByProjectId(projectID, string.Empty, string.Empty, false, out actPath, out taskRootFolder, out taskFolderWithoutEmpNo, out errorCode);
                string sourceDirectory = taskRootFolder;
                string destinctDirectory = string.Format(taskFolderWithoutEmpNo, transferToEmpNo);

                try
                {
                    Directory.Move(sourceDirectory, destinctDirectory);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLine("|" + sourceDirectory + "|" + destinctDirectory);
                    LogHelper.WriteLine(ex.Message + ex.StackTrace);
                    ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('目录移动失败！');", true);
                    return;
                }

                // 更新任务完成人
                ProjectSharing ps = new ProjectSharing();
                ProjectSharingBLL psBll = new ProjectSharingBLL();
                ps.ID = Guid.NewGuid().ToString();
                ps.PROJECTID = projectID;
                ps.FINISHEDPERSON = employeeID;
                ps.CREATEDATE = DateTime.Now;
                if (psBll.Add(ps))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('分配成功！');window.location.href='SerialNumberGenerating.aspx?projectID=" + projectID + "';", true);
                    return;
                }

                ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('新增完成人失败！');window.location.href='SerialNumberGenerating.aspx?projectID=" + projectID + "';", true);
                return;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message);
                ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('分配异常！');", true);
                return;
            }
        }
    }
}