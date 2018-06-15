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
    /// 数据访问类:TransactionDetails
    /// </summary>
    public partial class TransactionDetailsDAL
    {
        public TransactionDetailsDAL()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from transactiondetails");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,36)			};
            parameters[0].Value = ID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(TransactionDetails model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into transactiondetails(");
            strSql.Append("ID,TRANSACTIONAMOUNT,TRANSACTIONDESCRIPTION,TRANSACTIONDATE,TRANSACTIONTYPE,EMPLOYEEID,PROJECTID,CREATEDATE,ISDELETED)");
            strSql.Append(" values (");
            strSql.Append("@ID,@TRANSACTIONAMOUNT,@TRANSACTIONDESCRIPTION,@TRANSACTIONDATE,@TRANSACTIONTYPE,@EMPLOYEEID,@PROJECTID,@CREATEDATE,@ISDELETED)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,36),
					new MySqlParameter("@TRANSACTIONAMOUNT", MySqlDbType.Decimal,12),
					new MySqlParameter("@TRANSACTIONDESCRIPTION", MySqlDbType.VarChar,255),
					new MySqlParameter("@TRANSACTIONDATE", MySqlDbType.DateTime),
					new MySqlParameter("@TRANSACTIONTYPE", MySqlDbType.Int32,1),
					new MySqlParameter("@EMPLOYEEID", MySqlDbType.VarChar,36),
					new MySqlParameter("@PROJECTID", MySqlDbType.VarChar,36),
					new MySqlParameter("@CREATEDATE", MySqlDbType.DateTime),
					new MySqlParameter("@ISDELETED", MySqlDbType.Bit)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.TRANSACTIONAMOUNT;
            parameters[2].Value = model.TRANSACTIONDESCRIPTION;
            parameters[3].Value = model.TRANSACTIONDATE;
            parameters[4].Value = model.TRANSACTIONTYPE;
            parameters[5].Value = model.EMPLOYEEID;
            parameters[6].Value = model.PROJECTID;
            parameters[7].Value = model.CREATEDATE;
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
        public bool Update(TransactionDetails model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update transactiondetails set ");
            strSql.Append("TRANSACTIONAMOUNT=@TRANSACTIONAMOUNT,");
            strSql.Append("TRANSACTIONDESCRIPTION=@TRANSACTIONDESCRIPTION,");
            strSql.Append("TRANSACTIONDATE=@TRANSACTIONDATE,");
            strSql.Append("TRANSACTIONTYPE=@TRANSACTIONTYPE,");
            strSql.Append("EMPLOYEEID=@EMPLOYEEID,");
            strSql.Append("PROJECTID=@PROJECTID,");
            strSql.Append("CREATEDATE=@CREATEDATE,");
            strSql.Append("ISDELETED=@ISDELETED");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@TRANSACTIONAMOUNT", MySqlDbType.Decimal,12),
					new MySqlParameter("@TRANSACTIONDESCRIPTION", MySqlDbType.VarChar,255),
					new MySqlParameter("@TRANSACTIONDATE", MySqlDbType.DateTime),
					new MySqlParameter("@TRANSACTIONTYPE", MySqlDbType.Int32,1),
					new MySqlParameter("@EMPLOYEEID", MySqlDbType.VarChar,36),
					new MySqlParameter("@PROJECTID", MySqlDbType.VarChar,36),
					new MySqlParameter("@CREATEDATE", MySqlDbType.DateTime),
					new MySqlParameter("@ISDELETED", MySqlDbType.Bit),
					new MySqlParameter("@ID", MySqlDbType.VarChar,36)};
            parameters[0].Value = model.TRANSACTIONAMOUNT;
            parameters[1].Value = model.TRANSACTIONDESCRIPTION;
            parameters[2].Value = model.TRANSACTIONDATE;
            parameters[3].Value = model.TRANSACTIONTYPE;
            parameters[4].Value = model.EMPLOYEEID;
            parameters[5].Value = model.PROJECTID;
            parameters[6].Value = model.CREATEDATE;
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
            strSql.Append("delete from transactiondetails ");
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
            strSql.Append("delete from transactiondetails ");
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
        public TransactionDetails GetModel(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,TRANSACTIONAMOUNT,TRANSACTIONDESCRIPTION,TRANSACTIONDATE,TRANSACTIONTYPE,EMPLOYEEID,PROJECTID,CREATEDATE,ISDELETED from transactiondetails ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,36)			};
            parameters[0].Value = ID;

            TransactionDetails model = new TransactionDetails();
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
        public TransactionDetails DataRowToModel(DataRow row)
        {
            TransactionDetails model = new TransactionDetails();
            if (row != null)
            {
                if (row["ID"] != null)
                {
                    model.ID = row["ID"].ToString();
                }
                if (row["TRANSACTIONAMOUNT"] != null && row["TRANSACTIONAMOUNT"].ToString() != "")
                {
                    model.TRANSACTIONAMOUNT = decimal.Parse(row["TRANSACTIONAMOUNT"].ToString());
                }
                if (row["TRANSACTIONDESCRIPTION"] != null)
                {
                    model.TRANSACTIONDESCRIPTION = row["TRANSACTIONDESCRIPTION"].ToString();
                }
                if (row["TRANSACTIONDATE"] != null && row["TRANSACTIONDATE"].ToString() != "")
                {
                    model.TRANSACTIONDATE = DateTime.Parse(row["TRANSACTIONDATE"].ToString());
                }
                if (row["TRANSACTIONTYPE"] != null && row["TRANSACTIONTYPE"].ToString() != "")
                {
                    model.TRANSACTIONTYPE = int.Parse(row["TRANSACTIONTYPE"].ToString());
                }
                if (row["EMPLOYEEID"] != null)
                {
                    model.EMPLOYEEID = row["EMPLOYEEID"].ToString();
                }
                if (row["PROJECTID"] != null)
                {
                    model.PROJECTID = row["PROJECTID"].ToString();
                }
                if (row["CREATEDATE"] != null && row["CREATEDATE"].ToString() != "")
                {
                    model.CREATEDATE = DateTime.Parse(row["CREATEDATE"].ToString());
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
            strSql.Append("select ID,TRANSACTIONAMOUNT,TRANSACTIONDESCRIPTION,TRANSACTIONDATE,TRANSACTIONTYPE,EMPLOYEEID,PROJECTID,CREATEDATE,ISDELETED ");
            strSql.Append(" FROM transactiondetails ");
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
            strSql.Append("select count(1) FROM transactiondetails ");
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
            strSql.Append(")AS Row, T.*  from transactiondetails T ");
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
            parameters[0].Value = "transactiondetails";
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
        public DataSet GetListJoinEmpAndPrj(Dictionary<string,string> dic, int pageIndex, int pageSize, out int totalAmount)
        {
            totalAmount = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select td.ID,TRANSACTIONAMOUNT,TRANSACTIONDESCRIPTION,TRANSACTIONDATE,TRANSACTIONTYPE,td.EMPLOYEEID,td.PROJECTID,td.CREATEDATE,td.ISDELETED,
                         e.EMPLOYEENO, e.`NAME`, p.TASKNO
                         FROM transactiondetails td
                         LEFT JOIN employee e
                         ON td.EMPLOYEEID = e.ID
                         LEFT JOIN project p
                         ON td.PROJECTID = p.ID
                         WHERE 1 = 1 ");
            StringBuilder sbWhere = new StringBuilder();
            if (dic.ContainsKey("employeeId"))
            {
                sbWhere.AppendFormat(" and employeeId = '{0}'", dic["employeeId"]);
            }
            if (dic.ContainsKey("transacType"))
            {
                sbWhere.AppendFormat(" and transactionType = '{0}'", dic["transacType"]);
            }
            if (dic.ContainsKey("amountFrom"))
            {
                sbWhere.AppendFormat(" and TRANSACTIONAMOUNT >= {0}", dic["amountFrom"]);
            }
            if (dic.ContainsKey("amountTo"))
            {
                sbWhere.AppendFormat(" and TRANSACTIONAMOUNT <= {0}", dic["amountTo"]);
            }
            if (dic.ContainsKey("dateFrom"))
            {
                sbWhere.AppendFormat(" and transactiondate >= '{0}'", dic["dateFrom"]);
            }
            if (dic.ContainsKey("dateTo"))
            {
                sbWhere.AppendFormat(" and transactiondate <= '{0}'", dic["dateTo"]);
            }
            strSql.AppendFormat(" ORDER BY orderDate DESC LIMIT {0}, {1}", (pageIndex - 1) * pageSize, pageSize);
            return DbHelperMySQL.Query(strSql.ToString());
        }

        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        //public DataSet GetList(string strWhere)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select ID,TRANSACTIONAMOUNT,TRANSACTIONDESCRIPTION,TRANSACTIONDATE,TRANSACTIONTYPE,EMPLOYEEID,PROJECTID,CREATEDATE,ISDELETED ");
        //    strSql.Append(" FROM transactiondetails ");
        //    if (strWhere.Trim() != "")
        //    {
        //        strSql.Append(" where " + strWhere);
        //    }
        //    return DbHelperMySQL.Query(strSql.ToString());
        //}

        /// <summary>
        /// 获取奖罚
        /// </summary>
        public decimal GetRewardAndAmercementAmount(string employeeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT IFNULL(SUM(IFNULL(TRANSACTIONAMOUNT,0)),0) JF FROM transactiondetails 
                            WHERE EMPLOYEEID = '"+employeeId + @"' AND TRANSACTIONTYPE = 1 OR TRANSACTIONTYPE = 2 ");
            object obj = DbHelperMySQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0.0m;
            }
            else
            {
                return Convert.ToDecimal(obj);
            }
        }
        #endregion  ExtensionMethod
    }
}
