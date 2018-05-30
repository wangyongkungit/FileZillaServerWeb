using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerDAL
{
    public class TaskRemindingDAL
    {
        public DataTable GetTaskList(string employeeNo, string taskName, string isFinished, string taskType, int pageIndex, int pageSize, out int totalAmount)
        {
            totalAmount = 0;
            StringBuilder strSql = new StringBuilder(@"select employeeNO,folder,case isfinished when '0' then '未完成' else '已完成' END isFinished,
                                        CASE taskType WHEN '0' THEN '普通任务' ELSE '售后' END taskType,expiredate                                                 
                                        from taskreminding tr where 1=1 ");
            if (!string.IsNullOrEmpty(employeeNo))
            {
                strSql.AppendFormat(" and employeeno like '%{0}%'", employeeNo);
            }
            if (!string.IsNullOrEmpty(taskName))
            {
                strSql.AppendFormat(" and folder like '%{0}%'", taskName);
            }
            if (!string.IsNullOrEmpty(isFinished))
            {
                strSql.AppendFormat(" and isfinished='{0}'", isFinished);
            }
            if (!string.IsNullOrEmpty(taskType))
            {
                strSql.AppendFormat(" and tasktype='{0}'", taskType);
            }
            strSql.AppendFormat(" order by employeeno,folder,expiredate desc LIMIT {0},{1}", (pageIndex - 1) * pageSize, pageSize);
            DataSet ds = MySqlHelper.GetDataSet(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                totalAmount = ds.Tables[0].Rows.Count;
                return ds.Tables[0];
            }
            return null;
        }
    }
}
