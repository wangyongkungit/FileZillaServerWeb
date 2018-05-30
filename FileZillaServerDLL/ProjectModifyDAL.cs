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
    public class ProjectModifyDAL
    {
        public ProjectModifyDAL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from projectmodify");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
            parameters[0].Value = ID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ProjectModify model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into projectmodify(");
            strSql.Append("ID,PROJECTID,FOLDERNAME,FOLDERFULLNAME,ISUPLOADATTACH,ISFINISHED,REVIEWSTATUS,createdate)");
            strSql.Append(" values (");
            strSql.Append("@ID,@PROJECTID,@FOLDERNAME,@FOLDERFULLNAME,@ISUPLOADATTACH,@ISFINISHED,@REVIEWSTATUS,@createdate)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40),
					new MySqlParameter("@PROJECTID", MySqlDbType.VarChar,40),
					new MySqlParameter("@FOLDERNAME", MySqlDbType.VarChar,200),
					new MySqlParameter("@FOLDERFULLNAME", MySqlDbType.VarChar,400),
					new MySqlParameter("@ISUPLOADATTACH", MySqlDbType.Decimal,1),
					new MySqlParameter("@ISFINISHED", MySqlDbType.Decimal,1),
					new MySqlParameter("@REVIEWSTATUS", MySqlDbType.Decimal,1),
					new MySqlParameter("@createdate", MySqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.PROJECTID;
            parameters[2].Value = model.FOLDERNAME;
            parameters[3].Value = model.FOLDERFULLNAME;
            parameters[4].Value = model.ISUPLOADATTACH;
            parameters[5].Value = model.ISFINISHED;
            parameters[6].Value = model.REVIEWSTATUS;
            parameters[7].Value = model.CREATEDATE;

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
        public bool Update(ProjectModify model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update projectmodify set ");
            strSql.Append("PROJECTID=@PROJECTID,");
            strSql.Append("FOLDERNAME=@FOLDERNAME,");
            strSql.Append("FOLDERFULLNAME=@FOLDERFULLNAME,");
            strSql.Append("ISUPLOADATTACH=@ISUPLOADATTACH,");
            strSql.Append("ISFINISHED=@ISFINISHED,");
            strSql.Append("REVIEWSTATUS=@REVIEWSTATUS,");
            strSql.Append("createdate=@createdate");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@PROJECTID", MySqlDbType.VarChar,40),
					new MySqlParameter("@FOLDERNAME", MySqlDbType.VarChar,200),
					new MySqlParameter("@FOLDERFULLNAME", MySqlDbType.VarChar,400),
					new MySqlParameter("@ISUPLOADATTACH", MySqlDbType.Decimal,1),
					new MySqlParameter("@ISFINISHED", MySqlDbType.Decimal,1),
					new MySqlParameter("@REVIEWSTATUS", MySqlDbType.Decimal,1),
					new MySqlParameter("@createdate", MySqlDbType.DateTime),
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)};
            parameters[0].Value = model.PROJECTID;
            parameters[1].Value = model.FOLDERNAME;
            parameters[2].Value = model.FOLDERFULLNAME;
            parameters[3].Value = model.ISUPLOADATTACH;
            parameters[4].Value = model.ISFINISHED;
            parameters[5].Value = model.REVIEWSTATUS;
            parameters[6].Value = model.CREATEDATE;
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
            strSql.Append("delete from projectmodify ");
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
            strSql.Append("delete from projectmodify ");
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
        public ProjectModify GetModel(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,PROJECTID,FOLDERNAME,FOLDERFULLNAME,ISUPLOADATTACH,ISFINISHED,REVIEWSTATUS,createdate from projectmodify ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
            parameters[0].Value = ID;

            ProjectModify model = new ProjectModify();
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
        public ProjectModify DataRowToModel(DataRow row)
        {
            ProjectModify model = new ProjectModify();
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
                if (row["FOLDERNAME"] != null)
                {
                    model.FOLDERNAME = row["FOLDERNAME"].ToString();
                }
                if (row["FOLDERFULLNAME"] != null)
                {
                    model.FOLDERFULLNAME = row["FOLDERFULLNAME"].ToString();
                }
                if (row["ISUPLOADATTACH"] != null && row["ISUPLOADATTACH"].ToString() != "")
                {
                    model.ISUPLOADATTACH = decimal.Parse(row["ISUPLOADATTACH"].ToString());
                }
                if (row["ISFINISHED"] != null && row["ISFINISHED"].ToString() != "")
                {
                    model.ISFINISHED = decimal.Parse(row["ISFINISHED"].ToString());
                }
                if (row["REVIEWSTATUS"] != null && row["REVIEWSTATUS"].ToString() != "")
                {
                    model.REVIEWSTATUS = decimal.Parse(row["REVIEWSTATUS"].ToString());
                }
                if (row["CREATEDATE"] != null && row["CREATEDATE"].ToString() != "")
                {
                    model.CREATEDATE = DateTime.Parse(row["CREATEDATE"].ToString());
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
            strSql.Append("SELECT ID,PROJECTID,FOLDERNAME,FOLDERFULLNAME,ISUPLOADATTACH,ISFINISHED,REVIEWSTATUS,CREATEDATE ");
            strSql.Append(" FROM PROJECTMODIFY ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" WHERE " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /*
        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM projectmodify ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        */

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 获取指定projectID的修改任务数量
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public int GetModifyTaskCount(string projectID)
        {
            string sql = string.Format("SELECT count(*) cnt from projectmodify pm WHERE projectid='{0}' GROUP BY projectid", projectID);
            int cnt = Convert.ToInt16(DbHelperMySQL.GetSingle(sql));
            return cnt;
        }
        #endregion  ExtensionMethod
    }
}
