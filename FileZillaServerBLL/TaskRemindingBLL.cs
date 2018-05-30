using FileZillaServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerBLL
{
    public class TaskRemindingBLL
    {
        TaskRemindingDAL trDal = new TaskRemindingDAL();

        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="employeeNo">员工编号</param>
        /// <param name="taskName">任务名称（即文件夹名称）</param>
        /// <param name="isFinished">是否完成</param>
        /// <param name="taskType">任务类型</param>
        /// <param name="pageIndex">页码索引</param>
        /// <param name="pageSize">页码规格</param>
        /// <param name="totalAmount">记录总数</param>
        /// <returns></returns>
        public DataTable GetTaskList(string employeeNo, string taskName, string isFinished, string taskType, int pageIndex, int pageSize, out int totalAmount)
        {
            return trDal.GetTaskList(employeeNo, taskName, isFinished, taskType, pageIndex, pageSize, out totalAmount);
        }
    }
}
