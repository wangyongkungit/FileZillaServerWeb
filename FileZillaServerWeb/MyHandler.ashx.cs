using FileZillaServerBLL;
using FileZillaServerCommonHelper;
using FileZillaServerModel;
using FileZillaServerModel.Interface;
using Jil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Yiliangyijia.Comm;
using Yiliangyijia.Comm.Taobao;

namespace FileZillaServerWeb
{
    /// <summary>
    /// MyHandler 的摘要说明
    /// </summary>
    public class MyHandler : IHttpHandler
    {
        EmployeeAccountBLL empAcctBll = new EmployeeAccountBLL();
        FileHistoryBLL fileHistoryBll = new FileHistoryBLL();
        FileCategoryBLL fcBll = new FileCategoryBLL();
        ProjectBLL prjBll = new ProjectBLL();

        public void ProcessRequest(HttpContext context)
        {
            string method = context.Request.Params["method"];
            string jsonResult = string.Empty;
            switch (method)
            {
                // 获取员工账户
                case "GetEmployeeAccount":
                    jsonResult = GetEmployeeAccount(context);
                    break;
                // 取现
                case "Withdraw":
                    jsonResult = Withdraw(context);
                    break;
                // 获取文件完整路径
                case "GetFilepath":
                    jsonResult = GetFilepath(context);
                    break;
                // 获取任务动态
                case "GetTrends":
                    jsonResult = GetTrends(context);
                    break;
                // 获取交易状态列表
                case "GetAllTransactionStatus":
                    jsonResult = GetAllTransactionStatus();
                    break;
                // 更新交易状态
                case "UpdateTransactionStatusByProjectId":
                    jsonResult = UpdateTransactionStatusByProjectId(context);
                    break;
                // 获取淘宝订单信息
                case "GetTaobaoOrderInfo":
                    jsonResult = GetTaobaoOrderInfo(context);
                    break;
                default:
                    break;
            }
            context.Response.ContentType = "text/json";
            context.Response.Write(jsonResult);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        #region 获取员工账户
        private string GetEmployeeAccount(HttpContext context)
        {
            StringBuilder sbJsonResult = new StringBuilder();
            string employeeID = context.Request.Params["employeeID"];
            EmployeeAccount empAcct = empAcctBll.GetModelList(" employeeID = '" + employeeID + "'").FirstOrDefault();
            TransactionDetailsBLL transactionDetailsBll = new TransactionDetailsBLL();

            //账户余额
            decimal amount = 0m;
            //decimal surplus = 0m;
            // 已发
            decimal paid = 0m;
            // 奖罚，获取交易记录中奖励和处罚之和
            decimal rewardAndAmercement = transactionDetailsBll.GetRewardAndAmercementAmount(employeeID);
            decimal others = transactionDetailsBll.GetOtherAmount(employeeID);
            if (empAcct != null)
            {
                amount = empAcct.AMOUNT ?? 0m;
                //surplus = empAcct.SURPLUSAMOUNT ?? 0m;
                // 已发，取员工账户表中已发的值
                paid = empAcct.PAIDAMOUNT ?? 0m;
                //rewardAndAmercement = empAcct.REWARDANDAMERCEMENTAMOUNT ?? 0m;
                // 其他
                //others = empAcct.OTHERSAMOUNT ?? 0m;
            }
            StringBuilder sbEmpAcct = new StringBuilder();
            sbEmpAcct.Append("[");
            sbEmpAcct.Append("{\"value\":" + amount + ",\"name\":\"剩余\"},");
            sbEmpAcct.Append("{\"value\":" + paid + ",\"name\":\"已发\"},");
            sbEmpAcct.Append("{\"value\":" + Math.Abs(rewardAndAmercement) + ",\"name\":\"奖罚\"},");
            sbEmpAcct.Append("{\"value\":" + Math.Abs(others) + ",\"name\":\"其他\"}");
            sbEmpAcct.Append("]");
            sbJsonResult.Append(sbEmpAcct);
            return sbJsonResult.ToString();
        }
        #endregion

        #region 取现
        private string Withdraw(HttpContext context)
        {
            StringBuilder sbJsonResult = new StringBuilder();
            decimal? withdrawAmount = Convert.ToDecimal(context.Request.Params["withdrawAmount"]);
            string employeeID = context.Request.Params["employeeID"];
            WithdrawDetails withdraw = new WithdrawDetails();
            withdraw.ID = Guid.NewGuid().ToString();
            withdraw.EMPLOYEEID = employeeID;
            withdraw.WITHDRAWAMOUNT = withdrawAmount;
            withdraw.CREATEDATE = DateTime.Now;
            withdraw.ISCONFIRMED = false;
            if (new WithdrawDetailsBLL().Add(withdraw))
            {
                sbJsonResult.Append("{\"result\":\"1\"}");
            }
            else
            {
                sbJsonResult.Append("{\"result\":\"0\"}");
            }
            return sbJsonResult.ToString();
        }
        #endregion

        #region 获取文件路径
        private string GetFilepath(HttpContext context)
        {
            StringBuilder sbJsonResult = new StringBuilder();
            string fileHistoryId = context.Request.Params["fileHistoryId"];
            FileHistory fileHistory = fileHistoryBll.GetModel(fileHistoryId);
            string fileCategoryId = fileHistory.PARENTID;
            FileCategory category = fcBll.GetModel(fileCategoryId);
            string folderName = category.FOLDERNAME;

            string taskNo = string.Empty;
            string returnFileName = string.Empty;
            // 根据任务ID获取任务编号和员工编号，如果有记录说明任务已分配，则需要到员工目录下寻找该文件
            DataTable dtTaskNoAndEmpNo = fileHistoryBll.GetTaskNoAndEmpNoByPrjId(category.PROJECTID).Tables[0];
            // 任务已分配
            if (dtTaskNoAndEmpNo != null && dtTaskNoAndEmpNo.Rows.Count > 0)
            {
                string rootPath = ConfigurationManager.AppSettings["employeePath"].ToString();

                string empNo = dtTaskNoAndEmpNo.AsEnumerable().Select(item => Convert.ToString(item["employeeNo"])).FirstOrDefault();
                string empNoFullPath = Path.Combine(rootPath, empNo);

                taskNo = dtTaskNoAndEmpNo.AsEnumerable().Select(item => Convert.ToString(item["taskNo"])).FirstOrDefault();
                string taskNoFinalFolder = Directory.GetFiles(empNoFullPath, taskNo, SearchOption.AllDirectories).FirstOrDefault();
                if (!string.IsNullOrEmpty(taskNoFinalFolder))
                {
                    returnFileName = Path.Combine(rootPath, empNo, taskNo, folderName);
                }
            }
            // 任务未分配
            else
            {
                Project prj = prjBll.GetModel(category.PROJECTID);
                taskNo = prj.TASKNO;
                string rootPath = ConfigurationManager.AppSettings["taskAllotmentPath"];
                string taskNoFinalFolder = Directory.GetFiles(rootPath, taskNo, SearchOption.TopDirectoryOnly).FirstOrDefault();
                if (!string.IsNullOrEmpty(taskNoFinalFolder))
                {
                    returnFileName = Path.Combine(rootPath, taskNo, folderName);
                }
            }
            using (var output = new StringWriter())
            {
                JSON.Serialize(
                    new
                    {
                        code = 0,
                        msg = "success",
                        filename = returnFileName
                    },
                    output
                );
                sbJsonResult.Append(output.ToString());
            }
            return sbJsonResult.ToString();
        }
        #endregion

        #region 根据员工ID获取任务动态
        private string GetTrends(HttpContext context)
        {
            StringBuilder sbJsonResult = new StringBuilder();
            string employeeID = context.Request.Params["employeeID"];
            List<TaskTrend> lstTrend = new TaskTrendBLL().GetTop10ModelList(" employeeID = '" + employeeID + "' AND type = 1");
            List<TaskTrendInterface> lstResult = new List<TaskTrendInterface>();
            foreach (var item in lstTrend)
            {
                TaskTrendInterface taskTrendInterface = new TaskTrendInterface();
                taskTrendInterface.CreateDate = item.CREATEDATE ?? DateTime.Now;
                taskTrendInterface.FriendlyDate = DateTimeHelper.ChangeTime(item.CREATEDATE ?? DateTime.MinValue);
                taskTrendInterface.TrendContent = item.DESCRIPTION;
                lstResult.Add(taskTrendInterface);
            }
            sbJsonResult.Append(JsonConvert.SerializeObject(lstResult));
            return sbJsonResult.ToString();
        }
        #endregion

        #region 获取所有的交易状态值
        /// <summary>
        /// 获取所有的交易状态值
        /// </summary>
        /// <returns></returns>
        private string GetAllTransactionStatus()
        {
            StringBuilder sbJsonResult = new StringBuilder();
            ConfigureBLL cBll = new ConfigureBLL();
            DataTable dtTransactionStatus = cBll.GetConfig(ConfigTypeName.交易状态.ToString());
            if (dtTransactionStatus != null && dtTransactionStatus.Rows.Count > 0)
            {
                sbJsonResult.Append("[");
                for (int i = 0; i < dtTransactionStatus.Rows.Count; i++)
                {
                    sbJsonResult.Append("{\"key\":\"" + dtTransactionStatus.Rows[i]["configKey"].ToString() + "\",\"value\":\"" + dtTransactionStatus.Rows[i]["configValue"].ToString() + "\"}");
                    if (i != dtTransactionStatus.Rows.Count - 1)
                    {
                        sbJsonResult.Append(",");
                    }
                }
                sbJsonResult.Append("]");
            }
            return sbJsonResult.ToString();
        }
        #endregion

        #region 更新交易状态
        /// <summary>
        /// 更新交易状态
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string UpdateTransactionStatusByProjectId(HttpContext context)
        {
            StringBuilder sbJsonResult = new StringBuilder();
            ProjectBLL prjBll = new ProjectBLL();
            string projectId = context.Request.Params["projectId"];
            string newStatus = context.Request.Params["newStatus"];
            Project project = prjBll.GetModel(projectId);
            project.TRANSACTIONSTATUS = newStatus;
            if (prjBll.Update(project))
            {
                sbJsonResult.Append("{\"success\":\"true\"}");
            }
            else
            {
                sbJsonResult.Append("{\"success\":\"false\"}");
            }
            return sbJsonResult.ToString();
        }
        #endregion

        #region 获取淘宝订单信息
        /// <summary>
        /// 获取淘宝订单信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string GetTaobaoOrderInfo(HttpContext context)
        {
            StringBuilder sbJsonResult = new StringBuilder();
            DataTable dtShop = new ConfigureBLL().GetShopKeys();
            string AccessKey = string.Empty;//AK
            string SecretKey = string.Empty;//SK
            string tid = context.Request.Params["tid"].Trim();
            if (dtShop != null && dtShop.Rows.Count > 0)
            {
                for (int i = 0; i < dtShop.Rows.Count; i++)
                {
                    AccessKey = dtShop.Rows[i]["accessKey"]?.ToString();
                    SecretKey = dtShop.Rows[i]["SecretKey"]?.ToString();
                    if (!string.IsNullOrWhiteSpace(AccessKey) && !string.IsNullOrWhiteSpace(SecretKey))
                    {
                        SignatureEncryption SE = new SignatureEncryption();
                        Get G = new Get();
                        string requestId = Guid.NewGuid().ToString();//请求的UUID，每个请求都要求使用不同的UUID，服务端会拦截UUID重复的请求
                        string timeStamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss+0000");//当前的时间戳，UTC时间，带时区，例如（20160619160421+0000)

                        Dictionary<string, string> Parameter = new Dictionary<string, string>();
                        Parameter.Add("fields", "tid,pay_time,total_fee, payment,received_payment, buyer_nick,shop_pick");//  type,status,payment,orders,promotion_details,,
                        Parameter.Add("tid", tid);  // close 416046851854648869             有效的：293410926387038206    ，  274050855711040792

                        string PostUrl = SE.ProduceUrl("trade", "TradeFullinfoGetRequest", Parameter);//生成Url链接  Step1: 构造 HTTP请求：设置访问地址 URL
                        string HeadField = SE.HeadField(timeStamp, requestId, PostUrl);//生成头域字符串        Step2: 添加必须的头域
                        string WaitForSignString = SE.GetStringA();//生成待签名字符串   Step3: 将上述请求转换为"待签名字符串 A"
                        string Sign = SE.GetSignString(AccessKey, requestId, WaitForSignString);//签名

                        string Authorization = SE.Ty(AccessKey, Sign);//添加鉴权头域 Authorization  这个是没用的 错误的 鉴权头域
                        string Authorization2 = SE.GetAuthorizationHeader("GET", PostUrl, timeStamp, requestId, AccessKey, SecretKey);//添加鉴权头域 Authorization2    正确的鉴权头域  
                        string GetPutDBInfo = G.GetResponseData(PostUrl, timeStamp, requestId, Authorization2);
                        if (GetPutDBInfo.Contains("trade_fullinfo_get_response"))
                        {
                            sbJsonResult.Append(GetPutDBInfo.Replace("}}", ",\"shopid\":\"" + dtShop.Rows[i]["shopId"].ToString() + "\"}}"));
                            break;
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(sbJsonResult.ToString()))
            {
                return sbJsonResult.ToString();
            }
            return sbJsonResult.Append("{\"success\":\"false\"}").ToString();
        }
        #endregion
    }
}