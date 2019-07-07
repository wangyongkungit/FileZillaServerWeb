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
    public class TaskTrendDAL
    {
        public TaskTrendDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from tasktrend");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,36)			};
            parameters[0].Value = ID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TaskTrend model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into tasktrend(");
            strSql.Append("ID,PROJECTID,EMPLOYEEID,DESCRIPTION,CREATEDATE,READSTATUS,TYPE)");
            strSql.Append(" values (");
            strSql.Append("@ID,@PROJECTID,@EMPLOYEEID,@DESCRIPTION,@CREATEDATE,@READSTATUS,@TYPE)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ID", MySqlDbType.VarChar,36),
                    new MySqlParameter("@PROJECTID", MySqlDbType.VarChar,36),
                    new MySqlParameter("@EMPLOYEEID", MySqlDbType.VarChar,36),
                    new MySqlParameter("@DESCRIPTION", MySqlDbType.VarChar,255),
                    new MySqlParameter("@CREATEDATE", MySqlDbType.DateTime),
                    new MySqlParameter("@READSTATUS", MySqlDbType.Bit),
                    new MySqlParameter("@TYPE", MySqlDbType.Int32,1)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.PROJECTID;
            parameters[2].Value = model.EMPLOYEEID;
            parameters[3].Value = model.DESCRIPTION;
            parameters[4].Value = model.CREATEDATE;
            parameters[5].Value = model.READSTATUS;
            parameters[6].Value = model.TYPE;

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
        public bool Update(TaskTrend model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update tasktrend set ");
            strSql.Append("PROJECTID=@PROJECTID,");
            strSql.Append("EMPLOYEEID=@EMPLOYEEID,");
            strSql.Append("DESCRIPTION=@DESCRIPTION,");
            strSql.Append("CREATEDATE=@CREATEDATE,");
            strSql.Append("READSTATUS=@READSTATUS,");
            strSql.Append("TYPE=@TYPE");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PROJECTID", MySqlDbType.VarChar,36),
                    new MySqlParameter("@EMPLOYEEID", MySqlDbType.VarChar,36),
                    new MySqlParameter("@DESCRIPTION", MySqlDbType.VarChar,255),
                    new MySqlParameter("@CREATEDATE", MySqlDbType.DateTime),
                    new MySqlParameter("@READSTATUS", MySqlDbType.Bit),
                    new MySqlParameter("@TYPE", MySqlDbType.Int32,1),
                    new MySqlParameter("@ID", MySqlDbType.VarChar,36)};
            parameters[0].Value = model.PROJECTID;
            parameters[1].Value = model.EMPLOYEEID;
            parameters[2].Value = model.DESCRIPTION;
            parameters[3].Value = model.CREATEDATE;
            parameters[4].Value = model.READSTATUS;
            parameters[5].Value = model.TYPE;
            parameters[6].Value = model.ID;

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
            strSql.Append("delete from tasktrend ");
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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from tasktrend ");
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
        public TaskTrend GetModel(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,PROJECTID,EMPLOYEEID,DESCRIPTION,CREATEDATE,READSTATUS,TYPE from tasktrend ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,36)			};
            parameters[0].Value = ID;

            TaskTrend model = new TaskTrend();
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
        public TaskTrend DataRowToModel(DataRow row)
        {
            TaskTrend model = new TaskTrend();
            if (row != null)
            {
                if (row["ID"] != null)
                {
                    model.ID = row["ID"].ToString();
                }
                if (row["PROJECTID"] != null)
                {
                    model.PROJECTID = row["PROJECTID"].ToString();
                }
                if (row["EMPLOYEEID"] != null)
                {
                    model.EMPLOYEEID = row["EMPLOYEEID"].ToString();
                }
                if (row["DESCRIPTION"] != null)
                {
                    model.DESCRIPTION = row["DESCRIPTION"].ToString();
                }
                if (row["CREATEDATE"] != null && row["CREATEDATE"].ToString() != "")
                {
                    model.CREATEDATE = DateTime.Parse(row["CREATEDATE"].ToString());
                }
                if (row["READSTATUS"] != null && row["READSTATUS"].ToString() != "")
                {
                    if ((row["READSTATUS"].ToString() == "1") || (row["READSTATUS"].ToString().ToLower() == "true"))
                    {
                        model.READSTATUS = true;
                    }
                    else
                    {
                        model.READSTATUS = false;
                    }
                }
                if (row["TYPE"] != null && row["TYPE"].ToString() != "")
                {
                    model.TYPE = int.Parse(row["TYPE"].ToString());
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
            strSql.Append("select ID,PROJECTID,EMPLOYEEID,DESCRIPTION,CREATEDATE,READSTATUS,TYPE ");
            strSql.Append(" FROM tasktrend ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" ORDER BY CREATEDATE DESC LIMIT 0, 5");
            return DbHelperMySQL.Query(strSql.ToString());
        }

        public DataSet GetListPage(string strWhere, int pageIndex, int pageSize, out int totalRecordCount)
        {
            totalRecordCount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,PROJECTID,EMPLOYEEID,DESCRIPTION,CREATEDATE,READSTATUS,TYPE ");
            strSql.Append(" FROM tasktrend ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            string sqlCount = "SELECT COUNT(*) FROM taskTrend where 1 = 1 ";
            sqlCount += (" And " + strWhere);
            totalRecordCount = Convert.ToInt32( DbHelperMySQL.GetSingle(sqlCount));

            strSql.AppendFormat(" ORDER BY CREATEDATE DESC LIMIT {0}, {1}", (pageIndex - 1) * pageSize, pageSize);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM tasktrend ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperMySQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
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
            strSql.Append(")AS Row, T.*  from tasktrend T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        {
            MySqlParameter[] parameters = {
                    new MySqlParameter("@tblName", MySqlDbType.VarChar, 255),
                    new MySqlParameter("@fldName", MySqlDbType.VarChar, 255),
                    new MySqlParameter("@PageSize", MySqlDbType.Int32),
                    new MySqlParameter("@PageIndex", MySqlDbType.Int32),
                    new MySqlParameter("@IsReCount", MySqlDbType.Bit),
                    new MySqlParameter("@OrderType", MySqlDbType.Bit),
                    new MySqlParameter("@strWhere", MySqlDbType.VarChar,1000),
                    };
            parameters[0].Value = "tasktrend";
            parameters[1].Value = "ID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;
            parameters[5].Value = 0;
            parameters[6].Value = strWhere;	
            return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
        }*/

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 根据任务编号获取任务ID
        /// </summary>
        public string GetProjectIDByTaskNo(string taskNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ID FROM project WHERE TASKNO = '" + taskNo + "'");
            return DbHelperMySQL.GetSingle(strSql.ToString()).ToString();
        }

        /// <summary>
        /// 根据员工编号获取员工ID
        /// </summary>
        public string GetEmployeeIDByEmployeeNo(string employeeNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT ID FROM employee WHERE employeeNo = '" + employeeNo + "')");
            return DbHelperMySQL.GetSingle(strSql.ToString()).ToString();
        }
        #endregion  ExtensionMethod
    }
}
