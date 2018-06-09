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
    public class FileOperationLogDAL
    {
        public FileOperationLogDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from fileoperationlog");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ID", MySqlDbType.VarChar,36)           };
            parameters[0].Value = ID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(FileOperationLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into fileoperationlog(");
            strSql.Append("ID,PROJECTID,EMPLOYEEID,FILENAME,OPERATETYPE,OPERATEDATE,OPERATEUSER,OPERATECONTENT)");
            strSql.Append(" values (");
            strSql.Append("@ID,@PROJECTID,@EMPLOYEEID,@FILENAME,@OPERATETYPE,@OPERATEDATE,@OPERATEUSER,@OPERATECONTENT)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ID", MySqlDbType.VarChar,36),
                    new MySqlParameter("@PROJECTID", MySqlDbType.VarChar,36),
                    new MySqlParameter("@EMPLOYEEID", MySqlDbType.VarChar,36),
                    new MySqlParameter("@FILENAME", MySqlDbType.VarChar,255),
                    new MySqlParameter("@OPERATETYPE", MySqlDbType.Int32,2),
                    new MySqlParameter("@OPERATEDATE", MySqlDbType.DateTime),
                    new MySqlParameter("@OPERATEUSER", MySqlDbType.VarChar,36),
                    new MySqlParameter("@OPERATECONTENT", MySqlDbType.VarChar,300)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.PROJECTID;
            parameters[2].Value = model.EMPLOYEEID;
            parameters[3].Value = model.FILENAME;
            parameters[4].Value = model.OPERATETYPE;
            parameters[5].Value = model.OPERATEDATE;
            parameters[6].Value = model.OPERATEUSER;
            parameters[7].Value = model.OPERATECONTENT;

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
        public bool Update(FileOperationLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update fileoperationlog set ");
            strSql.Append("PROJECTID=@PROJECTID,");
            strSql.Append("EMPLOYEEID=@EMPLOYEEID,");
            strSql.Append("FILENAME=@FILENAME,");
            strSql.Append("OPERATETYPE=@OPERATETYPE,");
            strSql.Append("OPERATEDATE=@OPERATEDATE,");
            strSql.Append("OPERATEUSER=@OPERATEUSER,");
            strSql.Append("OPERATECONTENT=@OPERATECONTENT");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PROJECTID", MySqlDbType.VarChar,36),
                    new MySqlParameter("@EMPLOYEEID", MySqlDbType.VarChar,36),
                    new MySqlParameter("@FILENAME", MySqlDbType.VarChar,255),
                    new MySqlParameter("@OPERATETYPE", MySqlDbType.Int32,2),
                    new MySqlParameter("@OPERATEDATE", MySqlDbType.DateTime),
                    new MySqlParameter("@OPERATEUSER", MySqlDbType.VarChar,36),
                    new MySqlParameter("@OPERATECONTENT", MySqlDbType.VarChar,300),
                    new MySqlParameter("@ID", MySqlDbType.VarChar,36)};
            parameters[0].Value = model.PROJECTID;
            parameters[1].Value = model.EMPLOYEEID;
            parameters[2].Value = model.FILENAME;
            parameters[3].Value = model.OPERATETYPE;
            parameters[4].Value = model.OPERATEDATE;
            parameters[5].Value = model.OPERATEUSER;
            parameters[6].Value = model.OPERATECONTENT;
            parameters[7].Value = model.ID;

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
            strSql.Append("delete from fileoperationlog ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ID", MySqlDbType.VarChar,36)           };
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
            strSql.Append("delete from fileoperationlog ");
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
        public FileOperationLog GetModel(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,PROJECTID,EMPLOYEEID,FILENAME,OPERATETYPE,OPERATEDATE,OPERATEUSER,OPERATECONTENT from fileoperationlog ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ID", MySqlDbType.VarChar,36)           };
            parameters[0].Value = ID;

            FileOperationLog model = new FileOperationLog();
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
        public FileOperationLog DataRowToModel(DataRow row)
        {
            FileOperationLog model = new FileOperationLog();
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
                if (row["FILENAME"] != null)
                {
                    model.FILENAME = row["FILENAME"].ToString();
                }
                if (row["OPERATETYPE"] != null && row["OPERATETYPE"].ToString() != "")
                {
                    model.OPERATETYPE = int.Parse(row["OPERATETYPE"].ToString());
                }
                if (row["OPERATEDATE"] != null && row["OPERATEDATE"].ToString() != "")
                {
                    model.OPERATEDATE = DateTime.Parse(row["OPERATEDATE"].ToString());
                }
                if (row["OPERATEUSER"] != null)
                {
                    model.OPERATEUSER = row["OPERATEUSER"].ToString();
                }
                if (row["OPERATECONTENT"] != null)
                {
                    model.OPERATECONTENT = row["OPERATECONTENT"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, string orderBy)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,PROJECTID,EMPLOYEEID,FILENAME,OPERATETYPE,OPERATEDATE,OPERATEUSER,OPERATECONTENT ");
            strSql.Append(" FROM fileoperationlog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                strSql.Append(orderBy);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM fileoperationlog ");
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
            strSql.Append(")AS Row, T.*  from fileoperationlog T ");
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
			parameters[0].Value = "fileoperationlog";
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
        /// 获取操作记录，join Employee 表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public DataSet GetListJoinEmployee(string strWhere, string orderBy)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select f.ID,f.PROJECTID,f.EMPLOYEEID,f.FILENAME,f.OPERATETYPE,f.OPERATEDATE,f.OPERATEUSER operateUserId, f.OPERATECONTENT, e.EMPLOYEENO operateUser
                         FROM fileoperationlog f
                         LEFT JOIN employee e
                         ON f.EMPLOYEEID = e.ID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (!string.IsNullOrEmpty(orderBy))
            {
                strSql.Append(orderBy);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }
        #endregion  ExtensionMethod
    }
}
