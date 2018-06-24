using FileZillaServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerProfile
{
    public class GetDataSet
    {
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="employeeNO">员工编号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public static DataTable GetUesrProfileByUserIDandPwd(string employeeNO, string password)
        {
            string strSql = string.Format(@"SELECT count(*)
                                 from employee emp
                                 INNER JOIN employeerole emprole
                                 on emp.id=emprole.employeeid
                                 INNER JOIN role r
                                 on emprole.roleid=r.id
                                 INNER JOIN rolemenumap rmm
                                 ON r.ID=rmm.ROLEID
                                 INNER JOIN menu m
                                 ON rmm.MENUID=m.ID
	                             where EMPLOYEENO='{0}' and `PASSWORD`='{1}'", employeeNO, password);
            DataSet ds = MySqlHelper.GetDataSet(strSql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="employeeNO">员工编号</param>
        /// <returns></returns>
        public static DataTable GetUesrProfileByUserID(string employeeNO)
        {
            string strSql = string.Format(@"SELECT emp.ID,emp.EMPLOYEENO,emp.`NAME`,emp.isBranchLeader,
                                 r.ID roleID,r.ROLENAME,
                                 m.MENUNAME,m.MENUPATH,m.PARENTID,m.REMARKS
                                 from employee emp
                                 INNER JOIN employeerole emprole
                                 on emp.id=emprole.employeeid
                                 INNER JOIN role r
                                 on emprole.roleid=r.id
                                 INNER JOIN rolemenumap rmm
                                 ON r.ID=rmm.ROLEID
                                 INNER JOIN menu m
                                 ON rmm.MENUID=m.ID
	                             where EMPLOYEENO='{0}'", employeeNO);
            DataSet ds = MySqlHelper.GetDataSet(strSql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="employeeNO">员工编号</param>
        /// <returns></returns>
        public static DataTable GetUesrProfileByUserIDandPwdNew(string employeeNO, string passWord)
        {
            string strSql = string.Format(@"SELECT emp.ID,emp.EMPLOYEENO,emp.`NAME`,
                                 r.ID roleID,r.ROLENAME,
                                 m.MENUNAME,m.MENUPATH,m.PARENTID,m.REMARKS
                                 from employee emp
                                 INNER JOIN employeerole emprole
                                 on emp.id=emprole.employeeid
                                 INNER JOIN role r
                                 on emprole.roleid=r.id
                                 INNER JOIN rolemenumap rmm
                                 ON r.ID=rmm.ROLEID
                                 INNER JOIN menu m
                                 ON rmm.MENUID=m.ID
	                             WHERE available = 1 AND EMPLOYEENO='{0}' AND PASSWORD = '{1}'", employeeNO, passWord);
            DataSet ds = MySqlHelper.GetDataSet(strSql);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
    }
}
