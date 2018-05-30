using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileZillaServerModel;

namespace FileZillaServerDAL
{
    public partial class ProjectSharingDAL
    {
        public ProjectSharingDAL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from projectsharing");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
            parameters[0].Value = ID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(ProjectSharing model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into projectsharing(");
            strSql.Append("ID,PROJECTID,FINISHEDPERSON,PROPORTION,CREATEDATE)");
            strSql.Append(" values (");
            strSql.Append("@ID,@PROJECTID,@FINISHEDPERSON,@PROPORTION,@CREATEDATE)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40),
					new MySqlParameter("@PROJECTID", MySqlDbType.VarChar,40),
					new MySqlParameter("@FINISHEDPERSON", MySqlDbType.VarChar,40),
					new MySqlParameter("@PROPORTION", MySqlDbType.Double,3),
					new MySqlParameter("@CREATEDATE", MySqlDbType.DateTime)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.PROJECTID;
            parameters[2].Value = model.FINISHEDPERSON;
            parameters[3].Value = model.PROPORTION;
            parameters[4].Value = model.CREATEDATE;

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
        public bool Update(ProjectSharing model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update projectsharing set ");
            strSql.Append("PROJECTID=@PROJECTID,");
            strSql.Append("FINISHEDPERSON=@FINISHEDPERSON,");
            strSql.Append("PROPORTION=@PROPORTION,");
            strSql.Append("CREATEDATE=@CREATEDATE");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@PROJECTID", MySqlDbType.VarChar,40),
					new MySqlParameter("@FINISHEDPERSON", MySqlDbType.VarChar,40),
					new MySqlParameter("@PROPORTION", MySqlDbType.Double,3),
					new MySqlParameter("@CREATEDATE", MySqlDbType.DateTime),
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)};
            parameters[0].Value = model.PROJECTID;
            parameters[1].Value = model.FINISHEDPERSON;
            parameters[2].Value = model.PROPORTION;
            parameters[3].Value = model.CREATEDATE;
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
            strSql.Append("delete from projectsharing ");
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
            strSql.Append("delete from projectsharing ");
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
        public ProjectSharing GetModel(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,PROJECTID,FINISHEDPERSON,PROPORTION,CREATEDATE from projectsharing ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
            parameters[0].Value = ID;

            ProjectSharing model = new ProjectSharing();
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
        public ProjectSharing DataRowToModel(DataRow row)
        {
            ProjectSharing model = new ProjectSharing();
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
                if (row["FINISHEDPERSON"] != null)
                {
                    model.FINISHEDPERSON = row["FINISHEDPERSON"].ToString();
                }
                //model.PROPORTION=row["PROPORTION"].ToString();
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
            strSql.Append("select ID,PROJECTID,FINISHEDPERSON,PROPORTION,CREATEDATE ");
            strSql.Append(" FROM projectsharing ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /*/// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM projectsharing ");
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
            strSql.Append(")AS Row, T.*  from projectsharing T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMySQL.Query(strSql.ToString());
        }*/

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
            parameters[0].Value = "projectsharing";
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
        /// 获得完成人
        /// </summary>
        public DataSet GetListInnerJoinEmployee(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select PS.ID,PROJECTID,E.EMPLOYEENO,PS.FINISHEDPERSON,PS.PROPORTION,PS.CREATEDATE,P.TASKNO FROM PROJECTSHARING PS INNER JOIN EMPLOYEE E ON PS.FINISHEDPERSON = E.ID
                            INNER JOIN PROJECT P
                            ON PS.PROJECTID = P.ID");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }
        #endregion  ExtensionMethod
    }
}
