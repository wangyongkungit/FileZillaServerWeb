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

        /// <summary>
        /// 判断任务完成稿是否提醒，在用
        /// </summary>
        /// <param name="userIDorEmpNo">userID</param>
        /// <param name="folder">目录名（即任务编号）</param>
        /// <param name="modifyFolder">修改任务名</param>
        /// <param name="taskType">任务类型：0，新任务；1，售后；2，倒计时1小时；3，新任务待分配</param>
        /// <param name="toUserType">发送到的用户的类型</param>
        /// <returns></returns>
        public bool IsExist(string userIDorEmpNo, string folder, string modifyFolder, string taskType, string toUserType)
        {
            string sql = string.Empty;
            //1是客服人员
            if (toUserType == "1")
            {
                sql = string.Format(@"SELECT count(*) from taskreminding where enteringperson = '{0}' and folder = '{1}'  and taskType = '{2}' AND TOUSERTYPE = {3}", userIDorEmpNo, folder, taskType, toUserType);
                //是修改任务的
                if (!string.IsNullOrEmpty(modifyFolder))
                {
                    sql += string.Format(" and modifyFolder = '{0}'", modifyFolder);
                }
            }
            else if (toUserType == "2")  //造价员
            {
                sql = string.Format(@"SELECT count(*) from taskreminding where employeeNo = '{0}' and folder = '{1}' and taskType = '{2}' AND TOUSERTYPE = {3}", userIDorEmpNo, folder, taskType, toUserType);
                //是修改任务的
                if (!string.IsNullOrEmpty(modifyFolder))
                {
                    sql += string.Format(" and modifyFolder = '{0}'", modifyFolder);
                }
            }
            else if (toUserType == "0") //管理员
            {
                sql = string.Format(@"SELECT COUNT(*) FROM TASKREMINDING WHERE FOLDER = '{0}' AND TOUSERTYPE = '{1}'", folder, toUserType);
            }
            else
            {
                return false;
            }
            DataSet ds = MySqlHelper.GetDataSet(sql);
            int amount = 0;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                amount = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            return amount > 0;
        }

        /// <summary>
        /// 添加TaskReminding
        /// </summary>
        /// <param name="userNO">员工编号</param>
        /// <param name="enteringPerson">录入人</param>
        /// <param name="folder">任务编号（即任务目录名）</param>
        /// <param name="modifyFolder"></param>
        /// <param name="isReminded">是否已提醒，新增时固定为0，表示未提醒</param>
        /// <param name="createDate">创建时间</param>
        /// <param name="expireDate">截止时间</param>
        /// <param name="isFinished">是否完成</param>
        /// <param name="taskType">任务类型</param>
        /// <param name="toUserType">需发送到的用户类型</param>
        /// <returns></returns>
        public int Add(string userNO, string enteringPerson, string folder, string modifyFolder, string isReminded, string createDate, string expireDate, string isFinished, string taskType, string toUserType)
        {
            modifyFolder = modifyFolder == null ? "null" : string.Format("'{0}'", modifyFolder);
            string insertSql = string.Format(@"insert into taskreminding(ID,EMPLOYEENO,ENTERINGPERSON,FOLDER,MODIFYFOLDER,ISREMINDED,CREATEDATE,EXPIREDATE,ISFINISHED,TASKTYPE,TOUSERTYPE)
                                              values('{0}','{1}','{2}','{3}', {4}, '{5}', '{6}', {7}, '{8}', '{9}', '{10}')",
                                             Guid.NewGuid(), userNO, enteringPerson, folder, modifyFolder, isReminded, createDate, expireDate == null ? "null" : string.Format("'{0}'", expireDate), isFinished, taskType, toUserType);
            int rows = MySqlHelper.ExecuteNonQuery(insertSql);
            return rows;
        }
    }
}
