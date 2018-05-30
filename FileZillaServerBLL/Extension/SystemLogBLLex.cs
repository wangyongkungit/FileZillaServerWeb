using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileZillaServerDAL;
using System.Data;

namespace FileZillaServerBLL
{
    public class SystemLogBLLex
    {
        /// <summary>
        /// 获取日志
        /// </summary>
        /// <param name="dicCondition">条件集合</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">分页规格</param>
        /// <param name="totalAmount">记录总数</param>
        /// <returns></returns>
        public DataTable GetSystemLog(Dictionary<string, string> dicCondition, int pageIndex, int pageSize, out int totalAmount)
        {
            return new SystemLogDALex().GetSystemLog(dicCondition, pageIndex, pageSize, out totalAmount);
        }
    }
}
