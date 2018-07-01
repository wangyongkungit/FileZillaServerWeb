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

        /// <summary>
        /// 判断是否已添加任务提醒，在用
        /// </summary>
        /// <param name="enteringPerson">录入人ID</param>
        /// <param name="folder">目录名（即任务编号）</param>
        /// <param name="modifyFolder">修改任务名</param>
        /// <param name="taskType">任务类型：0，新任务；1，售后；2，倒计时3小时；3，新任务待分配</param>
        /// <param name="toUserType">发送到的用户的类型</param>
        /// <returns></returns>
        public bool IsExist(string enteringPerson, string folder, string modifyFolder, string taskType, string toUserType)
        {
            return trDal.IsExist(enteringPerson, folder, modifyFolder, taskType, toUserType);
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
            return trDal.Add(userNO, enteringPerson, folder, modifyFolder, isReminded, createDate, expireDate, isFinished, taskType, toUserType);
        }
    }
}
