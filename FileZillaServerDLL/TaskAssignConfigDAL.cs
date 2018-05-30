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
    public partial class TaskAssignConfigDAL
    {
        public TaskAssignConfigDAL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from taskassignconfig");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
            parameters[0].Value = ID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool ExistsByEmployeeID(string employeeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from taskassignconfig");
            strSql.Append(" where EMPLOYEEID=@employeeID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@employeeID", MySqlDbType.VarChar,40)			};
            parameters[0].Value = employeeID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TaskAssignConfig model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into taskassignconfig(");
            strSql.Append("ID,EMPLOYEEID,AVAILABLE,TARGETAMOUNT,TIMEMULTIPLE)");
            strSql.Append(" values (");
            strSql.Append("@ID,@EMPLOYEEID,@AVAILABLE,@TARGETAMOUNT,@TIMEMULTIPLE)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40),
					new MySqlParameter("@EMPLOYEEID", MySqlDbType.VarChar,40),
					new MySqlParameter("@AVAILABLE", MySqlDbType.Decimal,1),
					new MySqlParameter("@TARGETAMOUNT", MySqlDbType.Decimal,8),
					new MySqlParameter("@TIMEMULTIPLE", MySqlDbType.Int32,2)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.EMPLOYEEID;
            parameters[2].Value = model.AVAILABLE;
            parameters[3].Value = model.TARGETAMOUNT;
            parameters[4].Value = model.TIMEMULTIPLE;

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
        public bool Update(TaskAssignConfig model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update taskassignconfig set ");
            strSql.Append("EMPLOYEEID=@EMPLOYEEID,");
            strSql.Append("AVAILABLE=@AVAILABLE,");
            strSql.Append("TARGETAMOUNT=@TARGETAMOUNT,");
            strSql.Append("TIMEMULTIPLE=@TIMEMULTIPLE");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@EMPLOYEEID", MySqlDbType.VarChar,40),
					new MySqlParameter("@AVAILABLE", MySqlDbType.Decimal,1),
					new MySqlParameter("@TARGETAMOUNT", MySqlDbType.Decimal,8),
					new MySqlParameter("@TIMEMULTIPLE", MySqlDbType.Int32,2),
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)};
            parameters[0].Value = model.EMPLOYEEID;
            parameters[1].Value = model.AVAILABLE;
            parameters[2].Value = model.TARGETAMOUNT;
            parameters[3].Value = model.TIMEMULTIPLE;
            parameters[4].Value = model.ID;

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
            strSql.Append("delete from taskassignconfig ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from taskassignconfig ");
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
        public TaskAssignConfig GetModel(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,EMPLOYEEID,AVAILABLE,TARGETAMOUNT,TIMEMULTIPLE from taskassignconfig ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
            parameters[0].Value = ID;

            TaskAssignConfig model = new TaskAssignConfig();
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
        public TaskAssignConfig DataRowToModel(DataRow row)
        {
            TaskAssignConfig model = new TaskAssignConfig();
            if (row != null)
            {
                if (row["ID"] != null)
                {
                    model.ID = row["ID"].ToString();
                }
                if (row["EMPLOYEEID"] != null)
                {
                    model.EMPLOYEEID = row["EMPLOYEEID"].ToString();
                }
                if (row["AVAILABLE"] != null && row["AVAILABLE"].ToString() != "")
                {
                    model.AVAILABLE = decimal.Parse(row["AVAILABLE"].ToString());
                }
                if (row["TARGETAMOUNT"] != null && row["TARGETAMOUNT"].ToString() != "")
                {
                    model.TARGETAMOUNT = decimal.Parse(row["TARGETAMOUNT"].ToString());
                }
                if (row["TIMEMULTIPLE"] != null && row["TIMEMULTIPLE"].ToString() != "")
                {
                    model.TIMEMULTIPLE = int.Parse(row["TIMEMULTIPLE"].ToString());
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
            strSql.Append("select ID,EMPLOYEEID,AVAILABLE,TARGETAMOUNT ,TIMEMULTIPLE ");
            strSql.Append(" FROM taskassignconfig ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListJoinEmp(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT tac.ID, e.ID employeeID, e.EMPLOYEENO, e.`NAME`, tac.available, tac.TIMEMULTIPLE FROm taskassignconfig tac
                         INNER JOIN employee e
                         ON tac.EMPLOYEEID = e.ID AND e.AVAILABLE = 1");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by e.employeeNo ");
            return DbHelperMySQL.Query(strSql.ToString());
        }

        ///// <summary>
        ///// 获取记录总数
        ///// </summary>
        //public int GetRecordCount(string strWhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select count(1) FROM taskassignconfig ");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    object obj = DbHelperSQL.GetSingle(strSql.ToString());
        //    if (obj == null)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return Convert.ToInt32(obj);
        //    }
        //}
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
            strSql.Append(")AS Row, T.*  from taskassignconfig T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMySQL.Query(strSql.ToString());
        }
        #endregion  BasicMethod
    }
}
