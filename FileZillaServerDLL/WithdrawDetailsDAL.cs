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
    /// <summary>
    /// 数据访问类:WithdrawDetails
    /// </summary>
    public partial class WithdrawDetailsDAL
    {
        public WithdrawDetailsDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from withdrawdetails");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,36)			};
            parameters[0].Value = ID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(WithdrawDetails model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into withdrawdetails(");
            strSql.Append("ID,EMPLOYEEID,WITHDRAWAMOUNT,CREATEDATE,ISCONFIRMED,OPERATEPERSON,ISDELETED)");
            strSql.Append(" values (");
            strSql.Append("@ID,@EMPLOYEEID,@WITHDRAWAMOUNT,@CREATEDATE,@ISCONFIRMED,@OPERATEPERSON,@ISDELETED)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,36),
					new MySqlParameter("@EMPLOYEEID", MySqlDbType.VarChar,36),
					new MySqlParameter("@WITHDRAWAMOUNT", MySqlDbType.Decimal,12),
					new MySqlParameter("@CREATEDATE", MySqlDbType.DateTime),
					new MySqlParameter("@ISCONFIRMED", MySqlDbType.Bit),
					new MySqlParameter("@OPERATEPERSON", MySqlDbType.VarChar,36),
					new MySqlParameter("@ISDELETED", MySqlDbType.Bit)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.EMPLOYEEID;
            parameters[2].Value = model.WITHDRAWAMOUNT;
            parameters[3].Value = model.CREATEDATE;
            parameters[4].Value = model.ISCONFIRMED;
            parameters[5].Value = model.OPERATEPERSON;
            parameters[6].Value = model.ISDELETED;

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
        public bool Update(WithdrawDetails model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update withdrawdetails set ");
            strSql.Append("EMPLOYEEID=@EMPLOYEEID,");
            strSql.Append("WITHDRAWAMOUNT=@WITHDRAWAMOUNT,");
            strSql.Append("CREATEDATE=@CREATEDATE,");
            strSql.Append("ISCONFIRMED=@ISCONFIRMED,");
            strSql.Append("OPERATEPERSON=@OPERATEPERSON,");
            strSql.Append("ISDELETED=@ISDELETED");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@EMPLOYEEID", MySqlDbType.VarChar,36),
					new MySqlParameter("@WITHDRAWAMOUNT", MySqlDbType.Decimal,12),
					new MySqlParameter("@CREATEDATE", MySqlDbType.DateTime),
					new MySqlParameter("@ISCONFIRMED", MySqlDbType.Bit),
					new MySqlParameter("@OPERATEPERSON", MySqlDbType.VarChar,36),
					new MySqlParameter("@ISDELETED", MySqlDbType.Bit),
					new MySqlParameter("@ID", MySqlDbType.VarChar,36)};
            parameters[0].Value = model.EMPLOYEEID;
            parameters[1].Value = model.WITHDRAWAMOUNT;
            parameters[2].Value = model.CREATEDATE;
            parameters[3].Value = model.ISCONFIRMED;
            parameters[4].Value = model.OPERATEPERSON;
            parameters[5].Value = model.ISDELETED;
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
            strSql.Append("delete from withdrawdetails ");
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
            strSql.Append("delete from withdrawdetails ");
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
        public WithdrawDetails GetModel(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,EMPLOYEEID,WITHDRAWAMOUNT,CREATEDATE,ISCONFIRMED,OPERATEPERSON,ISDELETED from withdrawdetails ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,36)			};
            parameters[0].Value = ID;

            WithdrawDetails model = new WithdrawDetails();
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
        public WithdrawDetails DataRowToModel(DataRow row)
        {
            WithdrawDetails model = new WithdrawDetails();
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
                if (row["WITHDRAWAMOUNT"] != null && row["WITHDRAWAMOUNT"].ToString() != "")
                {
                    model.WITHDRAWAMOUNT = decimal.Parse(row["WITHDRAWAMOUNT"].ToString());
                }
                if (row["CREATEDATE"] != null && row["CREATEDATE"].ToString() != "")
                {
                    model.CREATEDATE = DateTime.Parse(row["CREATEDATE"].ToString());
                }
                if (row["ISCONFIRMED"] != null && row["ISCONFIRMED"].ToString() != "")
                {
                    if ((row["ISCONFIRMED"].ToString() == "1") || (row["ISCONFIRMED"].ToString().ToLower() == "true"))
                    {
                        model.ISCONFIRMED = true;
                    }
                    else
                    {
                        model.ISCONFIRMED = false;
                    }
                }
                if (row["OPERATEPERSON"] != null)
                {
                    model.OPERATEPERSON = row["OPERATEPERSON"].ToString();
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
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,EMPLOYEEID,WITHDRAWAMOUNT,CREATEDATE,ISCONFIRMED,OPERATEPERSON,ISDELETED ");
            strSql.Append(" FROM withdrawdetails ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM withdrawdetails ");
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
            strSql.Append(")AS Row, T.*  from withdrawdetails T ");
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
            parameters[0].Value = "withdrawdetails";
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetListUnionEmployee(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select wd.ID,EMPLOYEEID,e.employeeNo,WITHDRAWAMOUNT,wd.CREATEDATE,wd.ISCONFIRMED,e2.employeeNo operatePerson,wd.ISDELETED
                         FROM withdrawdetails wd
                         INNER JOIN employee e
                         on wd.EMPLOYEEID = e.ID
                        LEFT JOIN employee e2
                        ON wd.operatePerson = e2.ID ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by createDate desc ");
            return DbHelperMySQL.Query(strSql.ToString());
        }
        #endregion  ExtensionMethod
    }
}
