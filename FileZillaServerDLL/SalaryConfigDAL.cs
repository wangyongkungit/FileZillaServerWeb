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
    /// 数据访问类:SalaryConfig
    /// </summary>
    public partial class SalaryConfigDAL
    {
        public SalaryConfigDAL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from salaryconfig");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
            parameters[0].Value = ID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(SalaryConfig model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into salaryconfig(");
            strSql.Append("ID,EMPLOYEEID,BASESALARY,AGEWAGE,ACCOMMODATION_ALLOWANCE,MEAL_ALLOWANCE,OTHERWAGE,SOCIALSECURITY_INDIVIDUAL,SOCIALSECURITY_COMPANY,HOUSINGPROVIDENTFUND_INDIVIDUAL,HOUSINGPROVIDENTFUND_COMPANY,PAYOFF_TYPE,COMMISSION)");
            strSql.Append(" values (");
            strSql.Append("@ID,@EMPLOYEEID,@BASESALARY,@AGEWAGE,@ACCOMMODATION_ALLOWANCE,@MEAL_ALLOWANCE,@OTHERWAGE,@SOCIALSECURITY_INDIVIDUAL,@SOCIALSECURITY_COMPANY,@HOUSINGPROVIDENTFUND_INDIVIDUAL,@HOUSINGPROVIDENTFUND_COMPANY,@PAYOFF_TYPE,@COMMISSION)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40),
					new MySqlParameter("@EMPLOYEEID", MySqlDbType.VarChar,40),
					new MySqlParameter("@BASESALARY", MySqlDbType.Decimal,10),
					new MySqlParameter("@AGEWAGE", MySqlDbType.Decimal,8),
					new MySqlParameter("@ACCOMMODATION_ALLOWANCE", MySqlDbType.Decimal,8),
					new MySqlParameter("@MEAL_ALLOWANCE", MySqlDbType.Decimal,8),
					new MySqlParameter("@OTHERWAGE", MySqlDbType.Decimal,8),
					new MySqlParameter("@SOCIALSECURITY_INDIVIDUAL", MySqlDbType.Decimal,8),
					new MySqlParameter("@SOCIALSECURITY_COMPANY", MySqlDbType.Decimal,8),
					new MySqlParameter("@HOUSINGPROVIDENTFUND_INDIVIDUAL", MySqlDbType.Decimal,8),
					new MySqlParameter("@HOUSINGPROVIDENTFUND_COMPANY", MySqlDbType.Decimal,8),
					new MySqlParameter("@PAYOFF_TYPE", MySqlDbType.Decimal,1),
					new MySqlParameter("@COMMISSION", MySqlDbType.Decimal,3)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.EMPLOYEEID;
            parameters[2].Value = model.BASESALARY;
            parameters[3].Value = model.AGEWAGE;
            parameters[4].Value = model.ACCOMMODATION_ALLOWANCE;
            parameters[5].Value = model.MEAL_ALLOWANCE;
            parameters[6].Value = model.OTHERWAGE;
            parameters[7].Value = model.SOCIALSECURITY_INDIVIDUAL;
            parameters[8].Value = model.SOCIALSECURITY_COMPANY;
            parameters[9].Value = model.HOUSINGPROVIDENTFUND_INDIVIDUAL;
            parameters[10].Value = model.HOUSINGPROVIDENTFUND_COMPANY;
            parameters[11].Value = model.PAYOFF_TYPE;
            parameters[12].Value = model.COMMISSION;

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
        public bool Update(SalaryConfig model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update salaryconfig set ");
            strSql.Append("EMPLOYEEID=@EMPLOYEEID,");
            strSql.Append("BASESALARY=@BASESALARY,");
            strSql.Append("AGEWAGE=@AGEWAGE,");
            strSql.Append("ACCOMMODATION_ALLOWANCE=@ACCOMMODATION_ALLOWANCE,");
            strSql.Append("MEAL_ALLOWANCE=@MEAL_ALLOWANCE,");
            strSql.Append("OTHERWAGE=@OTHERWAGE,");
            strSql.Append("SOCIALSECURITY_INDIVIDUAL=@SOCIALSECURITY_INDIVIDUAL,");
            strSql.Append("SOCIALSECURITY_COMPANY=@SOCIALSECURITY_COMPANY,");
            strSql.Append("HOUSINGPROVIDENTFUND_INDIVIDUAL=@HOUSINGPROVIDENTFUND_INDIVIDUAL,");
            strSql.Append("HOUSINGPROVIDENTFUND_COMPANY=@HOUSINGPROVIDENTFUND_COMPANY,");
            strSql.Append("PAYOFF_TYPE=@PAYOFF_TYPE,");
            strSql.Append("COMMISSION=@COMMISSION");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@EMPLOYEEID", MySqlDbType.VarChar,40),
					new MySqlParameter("@BASESALARY", MySqlDbType.Decimal,10),
					new MySqlParameter("@AGEWAGE", MySqlDbType.Decimal,8),
					new MySqlParameter("@ACCOMMODATION_ALLOWANCE", MySqlDbType.Decimal,8),
					new MySqlParameter("@MEAL_ALLOWANCE", MySqlDbType.Decimal,8),
					new MySqlParameter("@OTHERWAGE", MySqlDbType.Decimal,8),
					new MySqlParameter("@SOCIALSECURITY_INDIVIDUAL", MySqlDbType.Decimal,8),
					new MySqlParameter("@SOCIALSECURITY_COMPANY", MySqlDbType.Decimal,8),
					new MySqlParameter("@HOUSINGPROVIDENTFUND_INDIVIDUAL", MySqlDbType.Decimal,8),
					new MySqlParameter("@HOUSINGPROVIDENTFUND_COMPANY", MySqlDbType.Decimal,8),
					new MySqlParameter("@PAYOFF_TYPE", MySqlDbType.Decimal,1),
					new MySqlParameter("@COMMISSION", MySqlDbType.Decimal,3),
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)};
            parameters[0].Value = model.EMPLOYEEID;
            parameters[1].Value = model.BASESALARY;
            parameters[2].Value = model.AGEWAGE;
            parameters[3].Value = model.ACCOMMODATION_ALLOWANCE;
            parameters[4].Value = model.MEAL_ALLOWANCE;
            parameters[5].Value = model.OTHERWAGE;
            parameters[6].Value = model.SOCIALSECURITY_INDIVIDUAL;
            parameters[7].Value = model.SOCIALSECURITY_COMPANY;
            parameters[8].Value = model.HOUSINGPROVIDENTFUND_INDIVIDUAL;
            parameters[9].Value = model.HOUSINGPROVIDENTFUND_COMPANY;
            parameters[10].Value = model.PAYOFF_TYPE;
            parameters[11].Value = model.COMMISSION;
            parameters[12].Value = model.ID;

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
            strSql.Append("delete from salaryconfig ");
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
            strSql.Append("delete from salaryconfig ");
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
        public SalaryConfig GetModel(string ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,EMPLOYEEID,BASESALARY,AGEWAGE,ACCOMMODATION_ALLOWANCE,MEAL_ALLOWANCE,OTHERWAGE,SOCIALSECURITY_INDIVIDUAL,SOCIALSECURITY_COMPANY,HOUSINGPROVIDENTFUND_INDIVIDUAL,HOUSINGPROVIDENTFUND_COMPANY,PAYOFF_TYPE,COMMISSION from salaryconfig ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
            parameters[0].Value = ID;

            SalaryConfig model = new SalaryConfig();
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
        public SalaryConfig DataRowToModel(DataRow row)
        {
            SalaryConfig model = new SalaryConfig();
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
                if (row["BASESALARY"] != null && row["BASESALARY"].ToString() != "")
                {
                    model.BASESALARY = decimal.Parse(row["BASESALARY"].ToString());
                }
                if (row["AGEWAGE"] != null && row["AGEWAGE"].ToString() != "")
                {
                    model.AGEWAGE = decimal.Parse(row["AGEWAGE"].ToString());
                }
                if (row["ACCOMMODATION_ALLOWANCE"] != null && row["ACCOMMODATION_ALLOWANCE"].ToString() != "")
                {
                    model.ACCOMMODATION_ALLOWANCE = decimal.Parse(row["ACCOMMODATION_ALLOWANCE"].ToString());
                }
                if (row["MEAL_ALLOWANCE"] != null && row["MEAL_ALLOWANCE"].ToString() != "")
                {
                    model.MEAL_ALLOWANCE = decimal.Parse(row["MEAL_ALLOWANCE"].ToString());
                }
                if (row["OTHERWAGE"] != null && row["OTHERWAGE"].ToString() != "")
                {
                    model.OTHERWAGE = decimal.Parse(row["OTHERWAGE"].ToString());
                }
                if (row["SOCIALSECURITY_INDIVIDUAL"] != null && row["SOCIALSECURITY_INDIVIDUAL"].ToString() != "")
                {
                    model.SOCIALSECURITY_INDIVIDUAL = decimal.Parse(row["SOCIALSECURITY_INDIVIDUAL"].ToString());
                }
                if (row["SOCIALSECURITY_COMPANY"] != null && row["SOCIALSECURITY_COMPANY"].ToString() != "")
                {
                    model.SOCIALSECURITY_COMPANY = decimal.Parse(row["SOCIALSECURITY_COMPANY"].ToString());
                }
                if (row["HOUSINGPROVIDENTFUND_INDIVIDUAL"] != null && row["HOUSINGPROVIDENTFUND_INDIVIDUAL"].ToString() != "")
                {
                    model.HOUSINGPROVIDENTFUND_INDIVIDUAL = decimal.Parse(row["HOUSINGPROVIDENTFUND_INDIVIDUAL"].ToString());
                }
                if (row["HOUSINGPROVIDENTFUND_COMPANY"] != null && row["HOUSINGPROVIDENTFUND_COMPANY"].ToString() != "")
                {
                    model.HOUSINGPROVIDENTFUND_COMPANY = decimal.Parse(row["HOUSINGPROVIDENTFUND_COMPANY"].ToString());
                }
                if (row["PAYOFF_TYPE"] != null && row["PAYOFF_TYPE"].ToString() != "")
                {
                    model.PAYOFF_TYPE = decimal.Parse(row["PAYOFF_TYPE"].ToString());
                }
                if (row["COMMISSION"] != null && row["COMMISSION"].ToString() != "")
                {
                    model.COMMISSION = decimal.Parse(row["COMMISSION"].ToString());
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
            strSql.Append("select ID,EMPLOYEEID,BASESALARY,AGEWAGE,ACCOMMODATION_ALLOWANCE,MEAL_ALLOWANCE,OTHERWAGE,SOCIALSECURITY_INDIVIDUAL,SOCIALSECURITY_COMPANY,HOUSINGPROVIDENTFUND_INDIVIDUAL,HOUSINGPROVIDENTFUND_COMPANY,PAYOFF_TYPE,COMMISSION ");
            strSql.Append(" FROM salaryconfig ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表，关联员工表
        /// <param name="strWhere"></param>
        /// </summary>
        public DataSet GetListUnionEmp(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sc.ID,EMPLOYEEID, EMPLOYEENO, BASESALARY,AGEWAGE,ACCOMMODATION_ALLOWANCE,MEAL_ALLOWANCE,OTHERWAGE,SOCIALSECURITY_INDIVIDUAL,SOCIALSECURITY_COMPANY,HOUSINGPROVIDENTFUND_INDIVIDUAL,HOUSINGPROVIDENTFUND_COMPANY,PAYOFF_TYPE,sc.COMMISSION ");
            strSql.Append(" FROM salaryconfig sc INNER JOIN employee e on sc.employeeid = e.id");
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
            StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) FROM salaryconfig ");
            if(strWhere.Trim()!="")
            {
                strSql.Append(" where "+strWhere);
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
        }*/
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
            strSql.Append(")AS Row, T.*  from salaryconfig T ");
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
            parameters[0].Value = "salaryconfig";
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

        #endregion  ExtensionMethod
    }
}
