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
    public class FileHistoryDAL
    {
        public FileHistoryDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from filehistory");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ID", MySqlDbType.VarChar,36)           };
            parameters[0].Value = ID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(FileHistory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into filehistory(");
            strSql.Append("ID,PARENTID,FILENAME,FILEEXTENSION,FILEFULLNAME,DESCRIPTION,OPERATEDATE,OPERATEUSER,ISDELETED)");
            strSql.Append(" values (");
            strSql.Append("@ID,@PARENTID,@FILENAME,@FILEEXTENSION,@FILEFULLNAME,@DESCRIPTION,@OPERATEDATE,@OPERATEUSER,@ISDELETED)");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ID", MySqlDbType.VarChar,36),
                    new MySqlParameter("@PARENTID", MySqlDbType.VarChar,36),
                    new MySqlParameter("@FILENAME", MySqlDbType.VarChar,255),
                    new MySqlParameter("@FILEEXTENSION", MySqlDbType.VarChar,20),
                    new MySqlParameter("@FILEFULLNAME", MySqlDbType.VarChar,255),
                    new MySqlParameter("@DESCRIPTION", MySqlDbType.VarChar,255),
                    new MySqlParameter("@OPERATEDATE", MySqlDbType.DateTime),
                    new MySqlParameter("@OPERATEUSER", MySqlDbType.VarChar,36),
                    new MySqlParameter("@ISDELETED", MySqlDbType.Bit)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.PARENTID;
            parameters[2].Value = model.FILENAME;
            parameters[3].Value = model.FILEEXTENSION;
            parameters[4].Value = model.FILEFULLNAME;
            parameters[5].Value = model.DESCRIPTION;
            parameters[6].Value = model.OPERATEDATE;
            parameters[7].Value = model.OPERATEUSER;
            parameters[8].Value = model.ISDELETED;

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
        public bool Update(FileHistory model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update filehistory set ");
            strSql.Append("PARENTID=@PARENTID,");
            strSql.Append("FILENAME=@FILENAME,");
            strSql.Append("FILEEXTENSION=@FILEEXTENSION,");
            strSql.Append("FILEFULLNAME=@FILEFULLNAME,");
            strSql.Append("DESCRIPTION=@DESCRIPTION,");
            strSql.Append("OPERATEDATE=@OPERATEDATE,");
            strSql.Append("OPERATEUSER=@OPERATEUSER,");
            strSql.Append("ISDELETED=@ISDELETED");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@PARENTID", MySqlDbType.VarChar,36),
                    new MySqlParameter("@FILENAME", MySqlDbType.VarChar,255),
                    new MySqlParameter("@FILEEXTENSION", MySqlDbType.VarChar,20),
                    new MySqlParameter("@FILEFULLNAME", MySqlDbType.VarChar,255),
                    new MySqlParameter("@DESCRIPTION", MySqlDbType.VarChar,255),
                    new MySqlParameter("@OPERATEDATE", MySqlDbType.DateTime),
                    new MySqlParameter("@OPERATEUSER", MySqlDbType.VarChar,36),
                    new MySqlParameter("@ISDELETED", MySqlDbType.Bit),
                    new MySqlParameter("@ID", MySqlDbType.VarChar,36)};
            parameters[0].Value = model.PARENTID;
            parameters[1].Value = model.FILENAME;
            parameters[2].Value = model.FILEEXTENSION;
            parameters[3].Value = model.FILEFULLNAME;
            parameters[4].Value = model.DESCRIPTION;
            parameters[5].Value = model.OPERATEDATE;
            parameters[6].Value = model.OPERATEUSER;
            parameters[7].Value = model.ISDELETED;
            parameters[8].Value = model.ID;

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
            strSql.Append("delete from filehistory ");
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
            strSql.Append("delete from filehistory ");
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
        public FileHistory GetModel(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,PARENTID,FILENAME,FILEEXTENSION,FILEFULLNAME,DESCRIPTION,OPERATEDATE,OPERATEUSER,ISDELETED from filehistory ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@ID", MySqlDbType.VarChar,36)           };
            parameters[0].Value = ID;

            FileHistory model = new FileHistory();
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
        public FileHistory DataRowToModel(DataRow row)
        {
            FileHistory model = new FileHistory();
            if (row != null)
            {
                if (row["ID"] != null)
                {
                    model.ID = row["ID"].ToString();
                }
                if (row["PARENTID"] != null)
                {
                    model.PARENTID = row["PARENTID"].ToString();
                }
                if (row["FILENAME"] != null)
                {
                    model.FILENAME = row["FILENAME"].ToString();
                }
                if (row["FILEEXTENSION"] != null)
                {
                    model.FILEEXTENSION = row["FILEEXTENSION"].ToString();
                }
                if (row["FILEFULLNAME"] != null)
                {
                    model.FILEFULLNAME = row["FILEFULLNAME"].ToString();
                }
                if (row["DESCRIPTION"] != null)
                {
                    model.DESCRIPTION = row["DESCRIPTION"].ToString();
                }
                if (row["OPERATEDATE"] != null && row["OPERATEDATE"].ToString() != "")
                {
                    model.OPERATEDATE = DateTime.Parse(row["OPERATEDATE"].ToString());
                }
                if (row["OPERATEUSER"] != null)
                {
                    model.OPERATEUSER = row["OPERATEUSER"].ToString();
                }
                if (row["ISDELETED"] != null && row["ISDELETED"].ToString() != "")
                {
                    if ((row["ISDELETED"].ToString() == "1") || (row["ISDELETED"].ToString().ToLower() == "true"))
                    {
                        model.ISDELETED = true;
                    }
                    else
                    {
                        model.ISDELETED = false;
                    }
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
            strSql.Append("select ID,PARENTID,FILENAME,FILEEXTENSION,FILEFULLNAME,DESCRIPTION,OPERATEDATE,OPERATEUSER,ISDELETED ");
            strSql.Append(" FROM filehistory  where IsDeleted = 0 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            if (!String.IsNullOrEmpty(orderBy))
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
            strSql.Append("select count(1) FROM filehistory ");
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
            strSql.Append(")AS Row, T.*  from filehistory T ");
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
			parameters[0].Value = "filehistory";
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
        public string GetTaskNoById(string fileHistoryId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT TASKNO FROM project
                            WHERE ID IN 
                            ( SELECT projectId FROM filecategory WHERE ID IN
                            (SELECT parentId FROM filehistory WHERE ID = '"+ fileHistoryId + "') )");
            object obj = DbHelperMySQL.GetSingle(strSql.ToString());
            return Convert.ToString(obj);
        }

        /// <summary>
        /// 判断是否已经设置了完成人，即是否已经分配
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public DataSet GetTaskNoAndEmpNoByPrjId(string projectID)
        {
            string sql = string.Format(@"SELECT p.TASKNO, e.EMPLOYEENO, ps.FINISHEDPERSON FROM project p
                                         LEFT JOIN projectsharing ps
                                         ON p.id = ps.PROJECTID 
                                         LEFT JOIN employee e
                                         ON ps.FINISHEDPERSON = e.ID
                                         WHERE p.ID = '{0}'", projectID);
            DataSet ds = DbHelperMySQL.Query(sql);
            return ds;
        }
        #endregion  ExtensionMethod
    }
}
