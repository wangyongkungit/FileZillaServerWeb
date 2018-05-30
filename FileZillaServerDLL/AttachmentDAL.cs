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
    public class AttachmentDAL
    {
        public AttachmentDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from attachment");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
            parameters[0].Value = ID;
            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Attachment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into attachment(");
            strSql.Append("ID,TASKID,TASKTYPE,FILENAME,EXTENSION,FILEFULLNAME,CREATEDATE)");
            strSql.Append(" values (");
            strSql.Append("@ID,@TASKID,@TASKTYPE,@FILENAME,@EXTENSION,@FILEFULLNAME,@CREATEDATE)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40),
					new MySqlParameter("@TASKID", MySqlDbType.VarChar,40),
					new MySqlParameter("@TASKTYPE", MySqlDbType.Decimal,1),
					new MySqlParameter("@FILENAME", MySqlDbType.VarChar,255),
					new MySqlParameter("@EXTENSION", MySqlDbType.VarChar,10),
					new MySqlParameter("@FILEFULLNAME", MySqlDbType.VarChar,300),
					new MySqlParameter("@CREATEDATE", MySqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.TASKID;
            parameters[2].Value = model.TASKTYPE;
            parameters[3].Value = model.FILENAME;
            parameters[4].Value = model.EXTENSION;
            parameters[5].Value = model.FILEFULLNAME;
            parameters[6].Value = model.CREATEDATE;

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
        public bool Update(Attachment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update attachment set ");
            strSql.Append("TASKID=@TASKID,");
            strSql.Append("TASKTYPE=@TASKTYPE,");
            strSql.Append("FILENAME=@FILENAME,");
            strSql.Append("EXTENSION=@EXTENSION,");
            strSql.Append("FILEFULLNAME=@FILEFULLNAME"); ;
            strSql.Append("CREATEDATE=@CREATEDATE");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@TASKID", MySqlDbType.VarChar,40),
					new MySqlParameter("@TASKTYPE", MySqlDbType.Decimal,1),
					new MySqlParameter("@FILENAME", MySqlDbType.VarChar,255),
					new MySqlParameter("@EXTENSION", MySqlDbType.VarChar,10),
					new MySqlParameter("@FILEFULLNAME", MySqlDbType.VarChar,300),
					new MySqlParameter("@CREATEDATE", MySqlDbType.DateTime),
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)};
            parameters[0].Value = model.TASKID;
            parameters[1].Value = model.TASKTYPE;
            parameters[2].Value = model.FILENAME;
            parameters[3].Value = model.EXTENSION;
            parameters[4].Value = model.FILEFULLNAME;
            parameters[5].Value = model.CREATEDATE;
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
            strSql.Append("delete from attachment ");
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
            strSql.Append("delete from attachment ");
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
        public Attachment GetModel(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,TASKID,TASKTYPE,FILENAME,EXTENSION,FILEFULLNAME,CREATEDATE from attachment ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
            parameters[0].Value = ID;

            Attachment model = new Attachment();
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
        public Attachment DataRowToModel(DataRow row)
        {
            Attachment model = new Attachment();
            if (row != null)
            {
                if (row["ID"] != null)
                {
                    model.ID = row["ID"].ToString();
                }
                if (row["TASKID"] != null)
                {
                    model.TASKID = row["TASKID"].ToString();
                }
                if (row["TASKTYPE"] != null && row["TASKTYPE"].ToString() != "")
                {
                    model.TASKTYPE = decimal.Parse(row["TASKTYPE"].ToString());
                }
                if (row["FILENAME"] != null)
                {
                    model.FILENAME = row["FILENAME"].ToString();
                }
                if (row["EXTENSION"] != null)
                {
                    model.EXTENSION = row["EXTENSION"].ToString();
                }
                if (row["FILEFULLNAME"] != null)
                {
                    model.FILEFULLNAME = row["FILEFULLNAME"].ToString();
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
            strSql.Append("select ID,TASKID,TASKTYPE,FILENAME,EXTENSION,FILEFULLNAME,CREATEDATE ");
            strSql.Append(" FROM attachment ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
