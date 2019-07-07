using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FileZillaServerModel;
using FileZillaServerBLL;
using System.Data;
using System.Configuration;
using FileZillaServerCommonHelper;
using FileZillaServerProfile;
using System.Text;

namespace FileZillaServerWeb
{
    /// <summary>
    /// **************************************
    /// 描    述：任务列表
    /// 作    者：Yongkun Wang
    /// 创建时间：2016-03-02
    /// 修改历史：2017-03-02 Yongkun Wang 创建
    /// **************************************
    /// </summary>
    public partial class TaskList : WebPageHelper
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Title = "任务查看";
            //首次绑定
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
                //    Response.End();
                //}

                RepeaterDataBind(false);
                DropDownListDataBind();
            }
            //doPostBack触发的页面回传，以此进行排序
            else
            {
                //EventTarget为Sort时才进行排序
                if (Request.Form["__EVENTTARGET"] == "Sort")
                {
                    string orderField = Request.Form["__EVENTARGUMENT"];
                    string orderDirection = sortOrder.Value;
                    DataView dv = new DataView();
                    //排序字段为空或者Session["dtTaskListExport"]为空，均无排序必要
                    if (!string.IsNullOrEmpty(orderField) && Session["dtTaskListExport"] != null)
                    {
                        System.Data.DataTable dtTemp = (System.Data.DataTable)Session["dtTaskListExport"];
                        dv = dtTemp.AsDataView();
                        dv.Sort = string.Format("{0} {1}", orderField, orderDirection);
                        rptData.DataSource = dv.ToTable();
                        rptData.DataBind();
                    }
                }
            }
        }

        ProjectBLL pBll = new ProjectBLL();

        /// <summary>
        /// 绑定Repeater数据
        /// </summary>
        protected void RepeaterDataBind(bool isExport)
        {
            Dictionary<string, string> dicCondition = new Dictionary<string, string>();
            string taskNo = txtTaskNo.Text.Trim();//任务编号
            string projectName = txtProjectName.Text.Trim();//工程名称
            string shop = ddlShop.SelectedValue;//店铺
            string expireDataStart = txtExpireDateStart.Text.Trim();//截止时间范围开始
            string expireDateEnd = txtExpireDateEnd.Text.Trim();//截止时间范围结束
            string expireDateMonth = txtExpireDateMonth.Text.Trim();//截止时间月份
            //如果截止时间月份，这个快速选择项不为空，那么以这个值为主
            if (!string.IsNullOrEmpty(expireDateMonth))
            {
                expireDataStart = string.Format("{0}-{1}", expireDateMonth, "01 00:00:00");
                DateTime dtExpire = Convert.ToDateTime(expireDataStart);
                int daysInMonth = DateTime.DaysInMonth(dtExpire.Year, dtExpire.Month);
                expireDateEnd = string.Format("{0}-{1}{2}", expireDateMonth, daysInMonth.ToString().PadLeft(2, '0'), " 23:59:59");
            }
            string orderDateStart = txtOrderDateStart.Text.Trim();//下单时间开始
            string orderDateEnd = txtOrderDateEnd.Text.Trim();//下单时间结束
            string orderDateMonth = txtOrderDateMonth.Text.Trim();//截止时间月份
            //如果订单时间月份，这个快速选择项不为空，那么以这个值为主
            if (!string.IsNullOrEmpty(orderDateMonth))
            {
                orderDateStart = string.Format("{0}-{1}", orderDateMonth, "01 00:00:00");
                DateTime dtOrder = Convert.ToDateTime(orderDateStart);
                int daysInMonth = DateTime.DaysInMonth(dtOrder.Year, dtOrder.Month);
                orderDateEnd = string.Format("{0}-{1}{2}", orderDateMonth, daysInMonth.ToString().PadLeft(2, '0'), " 23:59:59");
            }
            string orderAmountMin = txtOrderAmountMin.Text.Trim();//金额最小值
            string orderAmountMax = txtOrderAmountMax.Text.Trim();//金额最大值
            string buildingType = ddlBuildingType.SelectedValue;//建筑类型
            string structureForm = ddlStructureForm.SelectedValue;//结构类型
            string constructionAreaMin = txtConstructionAreaMin.Text.Trim();//建筑面积范围开始
            string constructionAreaMax = txtConstructionAreaMax.Text.Trim();//建筑面积范围结束
            string floorsMin = txtFloorsMin.Text.Trim();//层数起始
            string floorsMax = txtFloorsMax.Text.Trim();//层数结束
            string province = ddlProvince.SelectedValue; // Request.Form["ddlProvince"];//省份
            string transactionStatus = ddlTransactionStatus.SelectedValue;//交易状态
            string wangwangName = txtWangwangName.Text.Trim();//旺旺名
            string mobilePhone = txtMobilePhone.Text.Trim();//手机号码
            string qq = txtQQ.Text.Trim();//QQ
            string modelingSoftware = ddlModelingSoftware.SelectedValue;//计价软件
            string valuateSoftware = txtValuateSoftware.Text.Trim();//建模软件
            string finishedPerson = txtFinishedPerson.Text.Trim();//完成人
            string isFinished = ddlFinishedStatus.SelectedValue;//完成状态
            string enteringPerson = txtEnteringPerson.Text.Trim();//录入人（即客服）

            if (!string.IsNullOrEmpty(taskNo))
            {
                dicCondition.Add("taskNo", taskNo);
            }
            if (!string.IsNullOrEmpty(projectName))
            {
                dicCondition.Add("projectName", projectName);
            }
            if (!string.IsNullOrEmpty(shop))
            {
                dicCondition.Add("shop", shop);
            }
            if (!string.IsNullOrEmpty(expireDataStart))
            {
                dicCondition.Add("expireDateStart", expireDataStart);
            }
            if (!string.IsNullOrEmpty(expireDateEnd))
            {
                dicCondition.Add("expireDateEnd", expireDateEnd);
            }
            if (!string.IsNullOrEmpty(orderDateStart))
            {
                dicCondition.Add("orderDateStart", orderDateStart);
            }
            if (!string.IsNullOrEmpty(orderDateEnd))
            {
                dicCondition.Add("orderDateEnd", orderDateEnd);
            }
            if (!string.IsNullOrEmpty(orderAmountMin))
            {
                dicCondition.Add("orderAmountMin", orderAmountMin);
            }
            if (!string.IsNullOrEmpty(orderAmountMax))
            {
                dicCondition.Add("orderAmountMax", orderAmountMax);
            }
            if (!string.IsNullOrEmpty(buildingType))
            {
                dicCondition.Add("buildingType", buildingType);
            }
            if (!string.IsNullOrEmpty(structureForm))
            {
                dicCondition.Add("structureForm", structureForm);
            }
            if (!string.IsNullOrEmpty(constructionAreaMin))
            {
                dicCondition.Add("constructionAreaMin", constructionAreaMin);
            }
            if (!string.IsNullOrEmpty(constructionAreaMax))
            {
                dicCondition.Add("constructionAreaMax", constructionAreaMax);
            }
            if (!string.IsNullOrEmpty(floorsMin))
            {
                dicCondition.Add("floorsMin", floorsMin);
            }
            if (!string.IsNullOrEmpty(floorsMax))
            {
                dicCondition.Add("floorsMax", floorsMax);
            }
            if (!string.IsNullOrEmpty(province))
            {
                dicCondition.Add("province", province);
            }
            if (!string.IsNullOrEmpty(transactionStatus))
            {
                dicCondition.Add("transactionStatus", transactionStatus);
            }
            if (!string.IsNullOrEmpty(wangwangName))
            {
                dicCondition.Add("wangwangName", wangwangName);
            }
            if (!string.IsNullOrEmpty(mobilePhone))
            {
                dicCondition.Add("mobilePhone", mobilePhone);
            }
            if (!string.IsNullOrEmpty(qq))
            {
                dicCondition.Add("qq", qq);
            }
            if (!string.IsNullOrEmpty(modelingSoftware))
            {
                dicCondition.Add("modelingSoftware", modelingSoftware);
            }
            if (!string.IsNullOrEmpty(valuateSoftware))
            {
                dicCondition.Add("valuateSoftware", valuateSoftware);
            }
            if (!string.IsNullOrEmpty(finishedPerson))
            {
                dicCondition.Add("finishedPerson", finishedPerson);
            }
            if (!string.IsNullOrEmpty(isFinished))
            {
                dicCondition.Add("isfinished", isFinished);
            }
            //if (!string.IsNullOrEmpty(enteringPerson))
            //{
            //    dicCondition.Add("enteringPerson",enteringPerson);
            //}
            //是否是管理员
            bool isAdmin = false;
            bool isCustomerService = false;
            List<Role> lstRole = UserProfile.GetInstance().Role;
            for (int i = 0; i < lstRole.Count; i++)
            {
                //如果角色名称中包含管理员，则判定是管理员
                if (lstRole[i].RoleName.Contains("管理员"))
                {
                    isAdmin = true;
                    break;
                }
                else if (lstRole[i].RoleName == "客服")
                {
                    isCustomerService = true;
                }
            }
            //不是管理员的话，只能看当前登录人录入的信息
            if (isCustomerService)
            {
                dicCondition.Add("enteringPerson", UserProfile.GetInstance().EmployeeNO);
            }
            //如果是管理员并且输入了录入人编号，则加入录入人的筛选条件
            else if (isAdmin && !string.IsNullOrEmpty(enteringPerson))
            {
                dicCondition.Add("enteringPerson", enteringPerson);
            }
            AspNetPager1.PageSize = Convert.ToInt32(ConfigurationManager.AppSettings["pageSize"]);
            int totalAmount = 0;//返回的记录数

            //待用======================================================
            StringBuilder sbSortExpression = new StringBuilder();
            /*//如果EventTarget为Sort，才进行排序
            if (Request.Form["__EVENTTARGET"] == "Sort")
            {
                string orderField = Request.Form["__EVENTARGUMENT"];
                string orderDirection = sortOrder.Value;

                if (!string.IsNullOrEmpty(orderField))
                {
                    sbSortExpression.AppendFormat("{0} {1}", orderField, orderDirection);
                }
            }
            //待用======================================================
             * */

            DataSet ds = new DataSet();
            //导出需要的
            if (isExport)
            {
                ds = pBll.GetListUnion(dicCondition, sbSortExpression.ToString(), 0, 0, out totalAmount);
                Session["dtTaskListExport"] = ds.Tables[0];
            }
            //不是导出需要的
            else
            {
                ds = pBll.GetListUnion(dicCondition, sbSortExpression.ToString(), AspNetPager1.CurrentPageIndex, AspNetPager1.PageSize, out totalAmount);
                AspNetPager1.RecordCount = totalAmount;
                lblTotalRecordAmount.Text = totalAmount.ToString();
                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    rptData.DataSource = dt;
                    //遍历DataTable，将ID赋给sbIDs字符串
                    StringBuilder sbIDs = new StringBuilder();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sbIDs.AppendFormat("'{0}'{1}", dt.Rows[i]["ID"], ",");
                    }
                    if (dt.Rows.Count == 0)
                    {
                        sbIDs.Append("'',");
                    }
                    string sIDs = sbIDs.ToString().TrimEnd(',');
                    //查出任务的完成人
                    ProjectSharingBLL psBll = new ProjectSharingBLL();
                    DataTable dtFinishedPerson = psBll.GetListInnerJoinEmployee(string.Format("PROJECTID IN ({0})", sIDs)).Tables[0];
                    Session["dtFinishedPerson"] = dtFinishedPerson;
                }
                rptData.DataBind();
                double totalOrderAmount = 0;
                double totalSalary = 0;
                double totalCommission = 0;
                double totalRefund = 0;
                CountSalary(false, isCustomerService, out totalOrderAmount, out totalSalary, out totalCommission, out totalRefund);
                lblTotalOrderAmount.Text = totalOrderAmount.ToString();
                lblTotalRefund.Text = totalRefund.ToString();
                lblSalary.Text = totalSalary.ToString();
                //登录身份非客服并且录入人文本框输入查询条件时（也就是说管理员想查询某个客服录入的数据时），就显示出客服薪资
                lblCsSalary.Text = !isCustomerService && string.IsNullOrEmpty(enteringPerson) ? string.Empty : totalCommission.ToString();
            }
        }

        /// <summary>
        /// 集中绑定DropDownList
        /// </summary>
        public void DropDownListDataBind()
        {
            System.Data.DataTable dtConfig = null;
            if (Cache["dtConfig"] == null)
            {
                dtConfig = new ConfigureBLL().GetConfig();
                Cache.Insert("dtConfig", dtConfig, null, DateTime.Now.AddHours(1.5), System.Web.Caching.Cache.NoSlidingExpiration);
            }
            dtConfig = (DataTable)Cache["dtConfig"];
            DropDownListDataBind(ddlShop, dtConfig, ConfigTypeName.店铺编号名称, "-请选择-");
            DropDownListDataBind(ddlStructureForm, dtConfig, ConfigTypeName.建筑类型, "-请选择-");
            DropDownListDataBind(ddlBuildingType, dtConfig, ConfigTypeName.结构类型, "-请选择-");
            DropDownListProvinceBind();
            DropDownListDataBind(ddlTransactionStatus, dtConfig, ConfigTypeName.交易状态, "-请选择-");
            DropDownListDataBind(ddlModelingSoftware, dtConfig, ConfigTypeName.建模软件, "-请选择-");
        }

        /*/// <summary>
        /// 绑定DropDownList的方法
        /// </summary>
        /// <param name="dropDownList"></param>
        /// <param name="configTypeName"></param>
        /// <param name="tipString"></param>
        protected void DropDownListDataBind(DropDownList dropDownList, ConfigTypeName configTypeName, string tipString)
        {
            System.Data.DataTable dt = new ConfigureBLL().GetConfig(configTypeName.ToString());
            dropDownList.DataSource = dt;
            dropDownList.DataTextField = "configvalue";
            dropDownList.DataValueField = "configkey";
            dropDownList.DataBind();
            dropDownList.Items.Insert(0, new ListItem(tipString, string.Empty));
        }*/

        /// <summary>
        /// 绑定DropDownList的方法
        /// </summary>
        /// <param name="dropDownList"></param>
        /// <param name="configTypeName"></param>
        /// <param name="tipString"></param>
        protected void DropDownListDataBind(DropDownList dropDownList, System.Data.DataTable dtConfig, ConfigTypeName configTypeName, string tipString)
        {
            try
            {
                //DataTable dt = new ConfigureBLL().GetConfig(configTypeName.ToString());
                System.Data.DataTable dtNew = dtConfig.Clone();
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
                dropDownList.Items.Insert(0, new ListItem(tipString, string.Empty));
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
                LogHelper.WriteLine("省份绑定出错！" + ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// 页索引发生改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager_PageChanged(object sender, EventArgs e)
        {
            RepeaterDataBind(false);
        }

        /// <summary>
        /// 页码跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGoPage_Click(object sender, EventArgs e)
        {
            try
            {
                int pageindex = int.Parse(tb_pageindex.Text);
                AspNetPager1.CurrentPageIndex = pageindex;
            }
            catch (FormatException)
            {
                lbl_error.Text = "输入的页索引格式不正确";
            }
        }

        /// <summary>
        /// 查询按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RepeaterDataBind(false);
        }

        /// <summary>
        /// 导出按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            double totalOrderAmount;
            double totalSalary;
            double totalCommission;
            double totalRefund = 0;
            //RepeaterDataBind(true);
            CountSalary(true, false, out totalOrderAmount, out totalSalary, out totalCommission, out totalRefund);
        }

        /// <summary>
        /// 指定条件下应得金额的汇总
        /// </summary>
        /// <param name="isExport">是否是导出</param>
        /// <param name="isCustomerService">是否是客服</param>
        /// <param name="totalOrderAmount">订单总额</param>
        /// <param name="totalSalary">理论薪资</param>
        /// <param name="totalCommission">客服薪资</param>
        private void CountSalary(bool isExport, bool isCustomerService, out double totalOrderAmount, out double totalSalary, out double totalCommission, out double totalRefund)
        {
            //Dictionary<string, string> dicCondition = new Dictionary<string, string>();
            ////是否是管理员
            //bool isAdmin = false;
            //List<Role> lstRole = UserProfile.instance.Role;
            //for (int i = 0; i < lstRole.Count; i++)
            //{
            //    //如果角色名称中包含管理员，则判定是管理员
            //    if (lstRole[i].RoleName == "管理员")
            //    {
            //        isAdmin = true;
            //    }
            //}
            ////不是管理员的话，只能看当前登录人录入的信息
            //if (!isAdmin)
            //{
            //    dicCondition.Add("enteringPerson", UserProfile.instance.ID);
            //}
            //int totalAmount = 0;
            //DataSet ds = pBll.GetListUnion(dicCondition, AspNetPager1.CurrentPageIndex, 0, out totalAmount);
            //System.Data.DataTable dt = ds.Tables[0];
            totalOrderAmount = 0;
            totalSalary = 0;
            totalCommission = 0;
            totalRefund = 0;

            RepeaterDataBind(true);
            if (Session["dtTaskListExport"] == null)
            {
                ExecuteScript("alert('数据获取失败！您可点击“查询”重新获取数据后再试。')");
                return;
            }
            System.Data.DataTable dt = (System.Data.DataTable)Session["dtTaskListExport"];
            System.Data.DataTable dtExport = dt.Copy();
            dtExport.Columns.Add("理论应得", Type.GetType("System.Double"));

            DataTable dtFinishedPerson = Session["dtFinishedPerson"] as DataTable;

            for (int i = 0; i < dtExport.Rows.Count; i++)
            {
                double orderAmount = Convert.ToDouble(dtExport.Rows[i]["ORDERAMOUNT"]);



                //定义分成比例，默认是100%，如果是多任务的，需要设置为对应的比例
                double proportion = 1;
                if (!string.IsNullOrEmpty(txtFinishedPerson.Text.Trim()))
                {
                    if (dtFinishedPerson != null)
                    {
                        try
                        {
                            DataRow[] drFinishedPerson = dtFinishedPerson.Select(string.Format("PROJECTID = '{0}'", dt.Rows[i]["ID"]));
                            if (drFinishedPerson.Length > 1)
                            {
                                DataRow[] drCurrent = dtFinishedPerson.Select(string.Format("PROJECTID = '{0}' AND EMPLOYEENO = '{1}'", dt.Rows[i]["ID"], txtFinishedPerson.Text.Trim()));
                                proportion = Convert.ToDouble(drCurrent[0]["PROPORTION"]);
                            }
                        }
                        catch (Exception ex)
                        {
                            LogHelper.WriteLine(ex.Message + ex.StackTrace);
                        }
                    }
                }

                double refund = Convert.ToDouble(!string.IsNullOrEmpty(Convert.ToString(dtExport.Rows[i]["REFUND"])) ? dtExport.Rows[i]["REFUND"] : 0);

                #region 计算员工薪资
                //订单总额
                totalOrderAmount += orderAmount;

                totalRefund += refund;
                //先减去退款金额
                orderAmount = orderAmount - refund;
                //订单总额计算完后，就可以对下单金额进行分成计算了。因为订单总额totalOrderAmount是显示在页面上的提示性文字，还有后面的客服薪资，也需要用到这个原始的数据。
                orderAmount = orderAmount * proportion;

                string shop = dtExport.Rows[i]["SHOP_"].ToString();
                string paymentMethod = dtExport.Rows[i]["PAYMENTMETHOD_"].ToString();
                string transactionStatus = dtExport.Rows[i]["TRANSACTIONSTATUS_"].ToString();
                //返现金额为空，赋为0
                double cashBack = !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["CASHBACK"])) ? Convert.ToDouble(dt.Rows[i]["CASHBACK"]) : 0;
                double subtract = 0;
                //支付方式不是支付宝，也就是说是信用卡或花呗
                if (paymentMethod != "0")
                {
                    //店铺是邻宝，3.5%
                    if (shop == "9")
                    {
                        subtract += orderAmount * 0.035;//店铺9是邻宝,3.5%
                    }
                    //其他店铺，1%
                    else
                    {
                        subtract += orderAmount * 0.01;
                    }
                }
                //店铺是邻宝，额外加收2.5%
                if (shop == "9")
                {
                    subtract += orderAmount * 0.025;
                }
                //返现金额
                if (cashBack > 0)
                {
                    subtract += cashBack;
                }
                //提成
                double salesCommission = 0;
                //如果是退款，提成为0
                if (transactionStatus == "8")
                {
                    salesCommission = 0;
                }
                //否则是30%
                else
                {
                    salesCommission = (orderAmount - subtract) * 0.3;
                }
                double salary = salesCommission;
                dtExport.Rows[i]["理论应得"] = salary;
                totalSalary += salary;
                #endregion
            }

            #region 计算客服薪资
            double basesalary = 0;//底薪
            //double commission = 0;//提成
            if (!string.IsNullOrEmpty(txtEnteringPerson.Text.Trim()) || isCustomerService)  //如果不是客服身份，并且输入了录入人，说明是管理员想查询客服的薪资，故显示客服薪资
            {
                spnCsSalary.Visible = true;
                basesalary = 1820;//底薪
                double rate = 0;//提成比例

                if (totalOrderAmount >= 50000)
                {
                    rate = 0.12;
                }
                else if (totalOrderAmount >= 45000)
                {
                    rate = 0.1;
                }
                else if (totalOrderAmount >= 40000)
                {
                    rate = 0.08;
                }
                else if (totalOrderAmount >= 35000)
                {
                    rate = 0.06;
                }
                else if (totalOrderAmount >= 30000)
                {
                    rate = 0.04;
                }
                else if (totalOrderAmount >= 25000)
                {
                    rate = 0.02;
                }
                else if (totalOrderAmount >= 20000)
                {
                    rate = 0.01;
                }
                else
                {
                    rate = 0;
                }
                //客服薪资 = 底薪 + 提成
                totalCommission = basesalary + totalOrderAmount * rate;
                #region Remarks
                //if (totalOrderAmount >= 50000)
                //{
                //    commission = totalOrderAmount * 0.12;
                //}
                //else if (totalOrderAmount >= 45000)
                //{
                //    commission = totalOrderAmount * 0.1;
                //}
                //else if (totalOrderAmount >= 40000)
                //{
                //    commission = totalOrderAmount * 0.08;
                //}
                //else if (totalOrderAmount >= 35000)
                //{
                //    commission = totalOrderAmount * 0.06;
                //}
                //else if (totalOrderAmount >= 30000)
                //{
                //    commission = totalOrderAmount * 0.04;
                //}
                //else if (totalOrderAmount >= 25000)
                //{
                //    commission = totalOrderAmount * 0.02;
                //}
                //else if (totalOrderAmount >= 20000)
                //{
                //    commission = totalOrderAmount * 0.01;
                //}
                //else
                //{
                //    commission = 0;
                //}
                //totalCommission = basesalary + commission;
                #endregion
            }
            #endregion

            if (isExport)
            {
                //定义不需要导出的列并从DataTable中移除
                string[] colNameRemove = { "ID", "SHOP_", "TRANSACTIONSTATUS_", "PAYMENTMETHOD_", "REFERRER", "CASHBACK" };
                for (int i = 0; i < colNameRemove.Length; i++)
                {
                    dtExport.Columns.Remove(colNameRemove[i]);
                }
                //ExcelOperate.ExportExcel(dt);
                ExcelHelper.ExportByWeb(dtExport, "任务列表", "任务报表.xls");
            }
        }

        /// <summary>
        /// Repeater ItemDataBound事件，设置任务剩余时间并提醒、绑定完成人（因为有时一个任务的完成人不止一个）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptData_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                try
                {
                    DataRowView drv = (DataRowView)e.Item.DataItem;

                    #region 2017-04-05需求变更，完成人有时不止一个
                    //2017-04-05需求变更，完成人有时不止一个
                    //获取当前Item的ID
                    string projectID = Convert.ToString(drv["ID"]);
                    StringBuilder finishedPerson = new StringBuilder();
                    //取出之前存储的Session["dtFinishedPerson"]
                    DataTable dtFinishedPerson = Session["dtFinishedPerson"] as DataTable;
                    //从dtFinishedPerson中找出当前PROJECTID对应的记录
                    DataRow[] drFinishedPerson = dtFinishedPerson.Select(string.Format("PROJECTID = '{0}'", projectID));
                    //完成人Label
                    Label lblFinishedPerson = (Label)e.Item.FindControl("lblFinishedPerson");
                    //没有完成人，显示未分配
                    if (drFinishedPerson.Length == 0)
                    {
                        if (lblFinishedPerson != null)
                        {
                            lblFinishedPerson.Text = "未分配";//"&nbsp;--";
                            lblFinishedPerson.ToolTip = "未分配";
                        }

                        //是否创建了任务目录
                        string isCreatedTaskFloder = Convert.ToString(drv["ISCREATEDFOLDER"]);
                        //LogHelper.WriteLine("isCreatedTaskFloder:  " + isCreatedTaskFloder);
                        if (/*!string.IsNullOrEmpty(isCreatedTaskFloder) &&*/ isCreatedTaskFloder != "1")
                        {
                            if (lblFinishedPerson != null)
                            {
                                lblFinishedPerson.Text = "无资料";
                                lblFinishedPerson.ToolTip = "无资料";
                            }
                        }
                    }
                    else
                    {
                        //遍历DataRow数组，追加给字符串变量
                        foreach (DataRow dr in drFinishedPerson)
                        {
                            finishedPerson.Append(string.Format("{0}│", dr["EMPLOYEENO"]));
                        }
                        //给完成人label赋值
                        if (lblFinishedPerson != null)
                        {
                            lblFinishedPerson.Text = finishedPerson.ToString().TrimEnd('│');
                            lblFinishedPerson.ToolTip = lblFinishedPerson.Text;
                        }
                    }
                    
                    Control hl = e.Item.FindControl("hlTaskProportion");
                    hl.Visible = lblFinishedPerson.Text.Contains('│');
                    #endregion

                    DateTime dtExpire = Convert.ToDateTime(drv["expireDate"]);

                    Label lblTip = (Label)e.Item.FindControl("lblRemainTime");

                    //是否完成标识
                    bool isFinished = drv["isfinished"].ToString() == "1";
                    //未完成的，才显示剩余时间
                    if (lblTip != null && !isFinished)
                    {
                        if (DateTime.Now < dtExpire)
                        {
                            TimeSpan ts = dtExpire - DateTime.Now;

                            //设置提醒label文本
                            lblTip.Text = string.Format("{0}小时", Math.Floor(ts.TotalHours));
                            if (ts.TotalHours <= 3)
                            {
                                lblTip.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");//不足3小时，红色
                                lblTip.Font.Bold = true;
                            }
                            else if (ts.TotalHours <= 6)
                            {
                                lblTip.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF8800");//不足6小时，橙色
                                lblTip.Font.Bold = true;
                            }
                            else if (ts.TotalHours <= 12)
                            {
                                lblTip.ForeColor = System.Drawing.ColorTranslator.FromHtml("#EEEE00");//不足12小时，黄色
                                lblTip.Font.Bold = true;
                            }
                            else if (ts.TotalHours <= 24)
                            {
                                lblTip.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF77FF");//不足24小时，洋红色
                            }
                            else if (ts.TotalHours <= 48)
                            {
                                lblTip.ForeColor = System.Drawing.ColorTranslator.FromHtml("#57C600");//48小时以上，酸橙色(浅绿)
                            }
                            else
                            {
                                lblTip.Text = "&gt;2天";
                            }
                        }
                        else if (dtExpire < DateTime.Now)
                        {
                            TimeSpan ts = DateTime.Now - dtExpire;
                            //e.Row.Cells[2].Text += "（已逾期" + Math.Floor( ts.TotalHours) + "小时）";
                            lblTip.Text += string.Format("逾期{0}", Common.TransformTimeSpan(ts));
                            lblTip.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
                catch(Exception ex)
                {
                    LogHelper.WriteLine(ex.Message + ex.StackTrace);
                }
            }
        }
    }
}