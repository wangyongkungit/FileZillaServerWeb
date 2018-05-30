using FileZillaServerDAL;
using FileZillaServerModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerBLL
{
    public class AttendanceBLL
    {
        AttendanceDAL dal = new AttendanceDAL();

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得数据列表（关联员工表）
        /// </summary>
        public DataSet GetListUnionEmp(string strWhere)
        {
            return dal.GetListUnionEmp(strWhere);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Attendance model)
        {
            return dal.Add(model);
        }
    }
}
