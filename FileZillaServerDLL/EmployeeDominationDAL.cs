using FileZillaServerModel;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerDAL
{
    public class EmployeeDominationDAL
    {
        public EmployeeDominationDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from employeedomination");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,36)			};
            parameters[0].Value = ID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsByParentIDAndChildID(string parentID, string childID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from employeedomination");
            strSql.Append(" where PARENTEMPLOYEEID=@parentID AND CHILDEMPLOYEEID = @childID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@parentID", MySqlDbType.VarChar,36),
                    new MySqlParameter("@childID", MySqlDbType.VarChar,36) };
            parameters[0].Value = parentID;
            parameters[1].Value = childID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(EmployeeDomination model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into employeedomination(");
            strSql.Append("ID,PARENTEMPLOYEEID,CHILDEMPLOYEEID)");
            strSql.Append(" values (");
            strSql.Append("@ID,@PARENTEMPLOYEEID,@CHILDEMPLOYEEID)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,36),
					new MySqlParameter("@PARENTEMPLOYEEID", MySqlDbType.VarChar,36),
					new MySqlParameter("@CHILDEMPLOYEEID", MySqlDbType.VarChar,36)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.PARENTEMPLOYEEID;
            parameters[2].Value = model.CHILDEMPLOYEEID;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(EmployeeDomination model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update employeedomination set ");
            strSql.Append("PARENTEMPLOYEEID=@PARENTEMPLOYEEID,");
            strSql.Append("CHILDEMPLOYEEID=@CHILDEMPLOYEEID");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@PARENTEMPLOYEEID", MySqlDbType.VarChar,36),
					new MySqlParameter("@CHILDEMPLOYEEID", MySqlDbType.VarChar,36),
					new MySqlParameter("@ID", MySqlDbType.VarChar,36)};
            parameters[0].Value = model.PARENTEMPLOYEEID;
            parameters[1].Value = model.CHILDEMPLOYEEID;
            parameters[2].Value = model.ID;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from employeedomination ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,36)			};
            parameters[0].Value = ID;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteByParentIDAndChildID(string parentID, string childID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from employeedomination ");
            strSql.Append(" where PARENTEMPLOYEEID=@parentID AND CHILDEMPLOYEEID = @childID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@parentID", MySqlDbType.VarChar,36),
                    new MySqlParameter("@childID", MySqlDbType.VarChar,36) };
            parameters[0].Value = parentID;
            parameters[1].Value = childID;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from employeedomination ");
            strSql.Append(" where ID in (" + IDlist + ")  ");
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EmployeeDomination GetModel(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,PARENTEMPLOYEEID,CHILDEMPLOYEEID from employeedomination ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,36)			};
            parameters[0].Value = ID;

            EmployeeDomination model = new EmployeeDomination();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public EmployeeDomination DataRowToModel(DataRow row)
        {
            EmployeeDomination model = new EmployeeDomination();
            if (row != null)
            {
                if (row["ID"] != null)
                {
                    model.ID = row["ID"].ToString();
                }
                if (row["PARENTEMPLOYEEID"] != null)
                {
                    model.PARENTEMPLOYEEID = row["PARENTEMPLOYEEID"].ToString();
                }
                if (row["CHILDEMPLOYEEID"] != null)
                {
                    model.CHILDEMPLOYEEID = row["CHILDEMPLOYEEID"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,PARENTEMPLOYEEID,CHILDEMPLOYEEID ");
            strSql.Append(" FROM employeedomination ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListDistinctParentEmpID(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT DISTINCT parentEmployeeID, e.EMPLOYEENO, e.`NAME`, e.toRegularDate from employeeDomination ed, employee e");
            strSql.Append(" WHERE ed.PARENTEMPLOYEEID = e.ID AND e.AVAILABLE = 1");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" AND " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetListChildEmployee(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT parentEmployeeId, childEmployeeId, e.EMPLOYEENO, e.`NAME`, e.toRegularDate FROM employeedomination ed, employee e ");
            strSql.Append(" WHERE ed.CHILDEMPLOYEEID = e.ID AND e.AVAILABLE = 1");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" AND " + strWhere);
            }
            strSql.Append(" ORDER BY e.employeeNo ");
            return DbHelperMySQL.Query(strSql.ToString());
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.ID desc");
            }
            strSql.Append(")AS Row, T.*  from employeedomination T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
