using FileZillaServerBLL;
using FileZillaServerModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace FileZillaServerWeb
{
    /// <summary>
    /// MyHandler 的摘要说明
    /// </summary>
    public class MyHandler : IHttpHandler
    {
        EmployeeAccountBLL empAcctBll = new EmployeeAccountBLL();

        public void ProcessRequest(HttpContext context)
        {
            string method = context.Request.Params["method"];
            string employeeID = context.Request.Params["employeeID"];
            StringBuilder sbJsonResult = new StringBuilder();
            switch (method)
            {
                case "GetEmployeeAccount":
                    EmployeeAccount empAcct = empAcctBll.GetModelList(" employeeID = '" + employeeID + "'").FirstOrDefault();
                    decimal amount = 0m;
                    decimal surplus = 0m;
                    decimal paid = 0m;
                    decimal others = 0m;
                    if (empAcct != null)
                    {
                        amount = empAcct.AMOUNT ?? 0m;
                        surplus = empAcct.SURPLUSAMOUNT ?? 0m;
                        paid = empAcct.PAIDAMOUNT ?? 0m;
                        others = empAcct.OTHERSAMOUNT ?? 0m;
                    }
                    StringBuilder sbEmpAcct = new StringBuilder();
                    sbEmpAcct.Append("[");
                    sbEmpAcct.Append("{\"value\":" + amount + ",\"name\":\"剩余\"},");
                    sbEmpAcct.Append("{\"value\":" + paid + ",\"name\":\"已发\"},");
                    sbEmpAcct.Append("{\"value\":" + surplus + ",\"name\":\"奖罚\"},");
                    sbEmpAcct.Append("{\"value\":" + others + ",\"name\":\"其他\"}");
                    sbEmpAcct.Append("]");
                    sbJsonResult.Append(sbEmpAcct);
                    break;
                case "Withdraw":
                    decimal? withdrawAmount = Convert.ToDecimal(context.Request.Params["withdrawAmount"]);
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
                    break;
                default:
                    break;
            }
            context.Response.ContentType = "text/json";
            context.Response.Write(sbJsonResult.ToString());
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}