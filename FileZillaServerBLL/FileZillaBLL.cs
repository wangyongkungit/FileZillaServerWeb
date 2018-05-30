using FileZillaServerCommonHelper;
using FileZillaServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerBLL
{
    public class FileZillaBLL
    {
        FileZillaDAL fzDal = new FileZillaDAL();

        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="operateType">操作类型</param>
        /// <param name="content">记录内容</param>
        /// <param name="startDateTime">开始时间</param>
        /// <param name="endDateTime">结束时间</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public DataTable GetFileZilla(string userID, string operateType, string content, string startDateTime, string endDateTime, string fileName, int pageIndex, int pageSize, out int totalAmount)
        {
            return fzDal.GetFileZilla(userID, operateType, content, startDateTime, endDateTime, fileName, pageIndex, pageSize, out totalAmount);
        }

        public int Add(string userID, FileZillaOperateType operateType, string content, DateTime dateTime, string fileName)
        {
            return fzDal.Add(userID, operateType, content, dateTime, fileName);
        }

        /// <summary>
        /// 根据起止时间删除日志
        /// </summary>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public int Delete(string startDate, string endDate)
        {
            return fzDal.Delete(startDate, endDate);
        }
    }
}
