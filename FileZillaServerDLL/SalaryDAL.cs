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
	/// 数据访问类:Salary
	/// </summary>
	public partial class SalaryDAL
    {
        public SalaryDAL()
		{}
        #region
        /// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from salary");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
			parameters[0].Value = ID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(FileZillaServerModel.Salary model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into salary(");
            strSql.Append("ID,EMPLOYEEID,MONTHDATE,BASESALARY,CONTRACTSALARY,PIECEWAGE,PIECEPENALTY,FULLATTEND,OVERTIMEWAGE,AGEWAGE,ACCOMMODATION_ALLOWANCE,MEAL_ALLOWANCE,ATTENDANCEPENALTY,OTHERWAGE,TOTALORDERAMOUNT,SOCIALSECURITY_INDIVIDUAL,SOCIALSECURITY_COMPANY,HOUSINGPROVIDENTFUND_INDIVIDUAL,HOUSINGPROVIDENTFUND_COMPANY,TOTALINCOME,REALSALARY,REALSALARY_COMPANY)");
            strSql.Append(" values (");
            strSql.Append("@ID,@EMPLOYEEID,@MONTHDATE,@BASESALARY,@CONTRACTSALARY,@PIECEWAGE,@PIECEPENALTY,@FULLATTEND,@OVERTIMEWAGE,@AGEWAGE,@ACCOMMODATION_ALLOWANCE,@MEAL_ALLOWANCE,@ATTENDANCEPENALTY,@OTHERWAGE,@TOTALORDERAMOUNT,@SOCIALSECURITY_INDIVIDUAL,@SOCIALSECURITY_COMPANY,@HOUSINGPROVIDENTFUND_INDIVIDUAL,@HOUSINGPROVIDENTFUND_COMPANY,@TOTALINCOME,@REALSALARY,@REALSALARY_COMPANY)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40),
					new MySqlParameter("@EMPLOYEEID", MySqlDbType.VarChar,40),
					new MySqlParameter("@MONTHDATE", MySqlDbType.Date),
					new MySqlParameter("@BASESALARY", MySqlDbType.Decimal,10),
					new MySqlParameter("@CONTRACTSALARY", MySqlDbType.Decimal,8),
					new MySqlParameter("@PIECEWAGE", MySqlDbType.Decimal,8),
					new MySqlParameter("@PIECEPENALTY", MySqlDbType.Decimal,8),
					new MySqlParameter("@FULLATTEND", MySqlDbType.Decimal,8),
					new MySqlParameter("@OVERTIMEWAGE", MySqlDbType.Decimal,8),
					new MySqlParameter("@AGEWAGE", MySqlDbType.Decimal,8),
					new MySqlParameter("@ACCOMMODATION_ALLOWANCE", MySqlDbType.Decimal,8),
					new MySqlParameter("@MEAL_ALLOWANCE", MySqlDbType.Decimal,8),
					new MySqlParameter("@ATTENDANCEPENALTY", MySqlDbType.Decimal,8),
					new MySqlParameter("@OTHERWAGE", MySqlDbType.Decimal,8),
					new MySqlParameter("@TOTALORDERAMOUNT", MySqlDbType.Decimal,8),
					new MySqlParameter("@SOCIALSECURITY_INDIVIDUAL", MySqlDbType.Decimal,8),
					new MySqlParameter("@SOCIALSECURITY_COMPANY", MySqlDbType.Decimal,8),
					new MySqlParameter("@HOUSINGPROVIDENTFUND_INDIVIDUAL", MySqlDbType.Decimal,8),
					new MySqlParameter("@HOUSINGPROVIDENTFUND_COMPANY", MySqlDbType.Decimal,8),
					new MySqlParameter("@TOTALINCOME", MySqlDbType.Decimal,8),
					new MySqlParameter("@REALSALARY", MySqlDbType.Decimal,10),
					new MySqlParameter("@REALSALARY_COMPANY", MySqlDbType.Decimal,10)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.EMPLOYEEID;
            parameters[2].Value = model.MONTHDATE;
            parameters[3].Value = model.BASESALARY;
            parameters[4].Value = model.CONTRACTSALARY;
            parameters[5].Value = model.PIECEWAGE;
            parameters[6].Value = model.PIECEPENALTY;
            parameters[7].Value = model.FULLATTEND;
            parameters[8].Value = model.OVERTIMEWAGE;
            parameters[9].Value = model.AGEWAGE;
            parameters[10].Value = model.ACCOMMODATION_ALLOWANCE;
            parameters[11].Value = model.MEAL_ALLOWANCE;
            parameters[12].Value = model.ATTENDANCEPENALTY;
            parameters[13].Value = model.OTHERWAGE;
            parameters[14].Value = model.TOTALORDERAMOUNT;
            parameters[15].Value = model.SOCIALSECURITY_INDIVIDUAL;
            parameters[16].Value = model.SOCIALSECURITY_COMPANY;
            parameters[17].Value = model.HOUSINGPROVIDENTFUND_INDIVIDUAL;
            parameters[18].Value = model.HOUSINGPROVIDENTFUND_COMPANY;
            parameters[19].Value = model.TOTALINCOME;
            parameters[20].Value = model.REALSALARY;
            parameters[21].Value = model.REALSALARY_COMPANY;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Update(FileZillaServerModel.Salary model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update salary set ");
			strSql.Append("EMPLOYEEID=@EMPLOYEEID,");
			strSql.Append("MONTHDATE=@MONTHDATE,");
			strSql.Append("BASESALARY=@BASESALARY,");
			strSql.Append("CONTRACTSALARY=@CONTRACTSALARY,");
			strSql.Append("PIECEWAGE=@PIECEWAGE,");
			strSql.Append("PIECEPENALTY=@PIECEPENALTY,");
			strSql.Append("FULLATTEND=@FULLATTEND,");
			strSql.Append("OVERTIMEWAGE=@OVERTIMEWAGE,");
			strSql.Append("AGEWAGE=@AGEWAGE,");
			strSql.Append("ACCOMMODATION_ALLOWANCE=@ACCOMMODATION_ALLOWANCE,");
			strSql.Append("MEAL_ALLOWANCE=@MEAL_ALLOWANCE,");
			strSql.Append("ATTENDANCEPENALTY=@ATTENDANCEPENALTY,");
            strSql.Append("OTHERWAGE=@OTHERWAGE,");
            strSql.Append("TOTALORDERAMOUNT=@TOTALORDERAMOUNT,");
			strSql.Append("SOCIALSECURITY_INDIVIDUAL=@SOCIALSECURITY_INDIVIDUAL,");
			strSql.Append("SOCIALSECURITY_COMPANY=@SOCIALSECURITY_COMPANY,");
			strSql.Append("HOUSINGPROVIDENTFUND_INDIVIDUAL=@HOUSINGPROVIDENTFUND_INDIVIDUAL,");
            strSql.Append("HOUSINGPROVIDENTFUND_COMPANY=@HOUSINGPROVIDENTFUND_COMPANY,");
            strSql.Append("TOTALINCOME=@TOTALINCOME,");
			strSql.Append("REALSALARY=@REALSALARY,");
			strSql.Append("REALSALARY_COMPANY=@REALSALARY_COMPANY");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@EMPLOYEEID", MySqlDbType.VarChar,40),
					new MySqlParameter("@MONTHDATE", MySqlDbType.Date),
					new MySqlParameter("@BASESALARY", MySqlDbType.Decimal,10),
					new MySqlParameter("@CONTRACTSALARY", MySqlDbType.Decimal,8),
					new MySqlParameter("@PIECEWAGE", MySqlDbType.Decimal,8),
					new MySqlParameter("@PIECEPENALTY", MySqlDbType.Decimal,8),
					new MySqlParameter("@FULLATTEND", MySqlDbType.Decimal,8),
					new MySqlParameter("@OVERTIMEWAGE", MySqlDbType.Decimal,8),
					new MySqlParameter("@AGEWAGE", MySqlDbType.Decimal,8),
					new MySqlParameter("@ACCOMMODATION_ALLOWANCE", MySqlDbType.Decimal,8),
					new MySqlParameter("@MEAL_ALLOWANCE", MySqlDbType.Decimal,8),
					new MySqlParameter("@ATTENDANCEPENALTY", MySqlDbType.Decimal,8),
					new MySqlParameter("@OTHERWAGE", MySqlDbType.Decimal,8),
					new MySqlParameter("@TOTALORDERAMOUNT", MySqlDbType.Decimal,8),
					new MySqlParameter("@SOCIALSECURITY_INDIVIDUAL", MySqlDbType.Decimal,8),
					new MySqlParameter("@SOCIALSECURITY_COMPANY", MySqlDbType.Decimal,8),
					new MySqlParameter("@HOUSINGPROVIDENTFUND_INDIVIDUAL", MySqlDbType.Decimal,8),
					new MySqlParameter("@HOUSINGPROVIDENTFUND_COMPANY", MySqlDbType.Decimal,8),
					new MySqlParameter("@TOTALINCOME", MySqlDbType.Decimal,8),
					new MySqlParameter("@REALSALARY", MySqlDbType.Decimal,10),
					new MySqlParameter("@REALSALARY_COMPANY", MySqlDbType.Decimal,10),
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)};
			parameters[0].Value = model.EMPLOYEEID;
			parameters[1].Value = model.MONTHDATE;
			parameters[2].Value = model.BASESALARY;
			parameters[3].Value = model.CONTRACTSALARY;
			parameters[4].Value = model.PIECEWAGE;
			parameters[5].Value = model.PIECEPENALTY;
			parameters[6].Value = model.FULLATTEND;
			parameters[7].Value = model.OVERTIMEWAGE;
			parameters[8].Value = model.AGEWAGE;
			parameters[9].Value = model.ACCOMMODATION_ALLOWANCE;
			parameters[10].Value = model.MEAL_ALLOWANCE;
			parameters[11].Value = model.ATTENDANCEPENALTY;
            parameters[12].Value = model.OTHERWAGE;
            parameters[13].Value = model.TOTALORDERAMOUNT;
            parameters[14].Value = model.SOCIALSECURITY_INDIVIDUAL;
            parameters[15].Value = model.SOCIALSECURITY_COMPANY;
            parameters[16].Value = model.HOUSINGPROVIDENTFUND_INDIVIDUAL;
            parameters[17].Value = model.HOUSINGPROVIDENTFUND_COMPANY;
            parameters[18].Value = model.TOTALINCOME;
            parameters[19].Value = model.REALSALARY;
            parameters[20].Value = model.REALSALARY_COMPANY;
            parameters[21].Value = model.ID;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from salary ");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
			parameters[0].Value = ID;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from salary ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString());
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
		public FileZillaServerModel.Salary GetModel(string ID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select ID,EMPLOYEEID,MONTHDATE,BASESALARY,CONTRACTSALARY,PIECEWAGE,PIECEPENALTY,FULLATTEND,OVERTIMEWAGE,AGEWAGE,ACCOMMODATION_ALLOWANCE,MEAL_ALLOWANCE,ATTENDANCEPENALTY,OTHERWAGE,SOCIALSECURITY_INDIVIDUAL,SOCIALSECURITY_COMPANY,HOUSINGPROVIDENTFUND_INDIVIDUAL,HOUSINGPROVIDENTFUND_COMPANY,TOTALINCOME,REALSALARY,REALSALARY_COMPANY from salary ");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
			parameters[0].Value = ID;

			FileZillaServerModel.Salary model=new FileZillaServerModel.Salary();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public FileZillaServerModel.Salary DataRowToModel(DataRow row)
		{
            FileZillaServerModel.Salary model = new FileZillaServerModel.Salary();
			if (row != null)
			{
				if(row["ID"]!=null)
				{
					model.ID=row["ID"].ToString();
				}
				if(row["EMPLOYEEID"]!=null)
				{
					model.EMPLOYEEID=row["EMPLOYEEID"].ToString();
				}
				if(row["MONTHDATE"]!=null && row["MONTHDATE"].ToString()!="")
				{
					model.MONTHDATE=DateTime.Parse(row["MONTHDATE"].ToString());
				}
				if(row["BASESALARY"]!=null && row["BASESALARY"].ToString()!="")
				{
					model.BASESALARY=decimal.Parse(row["BASESALARY"].ToString());
				}
				if(row["CONTRACTSALARY"]!=null && row["CONTRACTSALARY"].ToString()!="")
				{
					model.CONTRACTSALARY=decimal.Parse(row["CONTRACTSALARY"].ToString());
				}
				if(row["PIECEWAGE"]!=null && row["PIECEWAGE"].ToString()!="")
				{
					model.PIECEWAGE=decimal.Parse(row["PIECEWAGE"].ToString());
				}
				if(row["PIECEPENALTY"]!=null && row["PIECEPENALTY"].ToString()!="")
				{
					model.PIECEPENALTY=decimal.Parse(row["PIECEPENALTY"].ToString());
				}
				if(row["FULLATTEND"]!=null && row["FULLATTEND"].ToString()!="")
				{
					model.FULLATTEND=decimal.Parse(row["FULLATTEND"].ToString());
				}
				if(row["OVERTIMEWAGE"]!=null && row["OVERTIMEWAGE"].ToString()!="")
				{
					model.OVERTIMEWAGE=decimal.Parse(row["OVERTIMEWAGE"].ToString());
				}
				if(row["AGEWAGE"]!=null && row["AGEWAGE"].ToString()!="")
				{
					model.AGEWAGE=decimal.Parse(row["AGEWAGE"].ToString());
				}
				if(row["ACCOMMODATION_ALLOWANCE"]!=null && row["ACCOMMODATION_ALLOWANCE"].ToString()!="")
				{
					model.ACCOMMODATION_ALLOWANCE=decimal.Parse(row["ACCOMMODATION_ALLOWANCE"].ToString());
				}
				if(row["MEAL_ALLOWANCE"]!=null && row["MEAL_ALLOWANCE"].ToString()!="")
				{
					model.MEAL_ALLOWANCE=decimal.Parse(row["MEAL_ALLOWANCE"].ToString());
				}
				if(row["ATTENDANCEPENALTY"]!=null && row["ATTENDANCEPENALTY"].ToString()!="")
				{
					model.ATTENDANCEPENALTY=decimal.Parse(row["ATTENDANCEPENALTY"].ToString());
				}
				if(row["OTHERWAGE"]!=null && row["OTHERWAGE"].ToString()!="")
				{
					model.OTHERWAGE=decimal.Parse(row["OTHERWAGE"].ToString());
				}
				if(row["SOCIALSECURITY_INDIVIDUAL"]!=null && row["SOCIALSECURITY_INDIVIDUAL"].ToString()!="")
				{
					model.SOCIALSECURITY_INDIVIDUAL=decimal.Parse(row["SOCIALSECURITY_INDIVIDUAL"].ToString());
				}
				if(row["SOCIALSECURITY_COMPANY"]!=null && row["SOCIALSECURITY_COMPANY"].ToString()!="")
				{
					model.SOCIALSECURITY_COMPANY=decimal.Parse(row["SOCIALSECURITY_COMPANY"].ToString());
				}
				if(row["HOUSINGPROVIDENTFUND_INDIVIDUAL"]!=null && row["HOUSINGPROVIDENTFUND_INDIVIDUAL"].ToString()!="")
				{
					model.HOUSINGPROVIDENTFUND_INDIVIDUAL=decimal.Parse(row["HOUSINGPROVIDENTFUND_INDIVIDUAL"].ToString());
				}
				if(row["HOUSINGPROVIDENTFUND_COMPANY"]!=null && row["HOUSINGPROVIDENTFUND_COMPANY"].ToString()!="")
				{
					model.HOUSINGPROVIDENTFUND_COMPANY=decimal.Parse(row["HOUSINGPROVIDENTFUND_COMPANY"].ToString());
                }
                if (row["TOTALINCOME"] != null && row["TOTALINCOME"].ToString() != "")
                {
                    model.TOTALINCOME = decimal.Parse(row["TOTALINCOME"].ToString());
                }
				if(row["REALSALARY"]!=null && row["REALSALARY"].ToString()!="")
				{
					model.REALSALARY=decimal.Parse(row["REALSALARY"].ToString());
				}
				if(row["REALSALARY_COMPANY"]!=null && row["REALSALARY_COMPANY"].ToString()!="")
				{
					model.REALSALARY_COMPANY=decimal.Parse(row["REALSALARY_COMPANY"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select ID,EMPLOYEEID,MONTHDATE,BASESALARY,CONTRACTSALARY,PIECEWAGE,PIECEPENALTY,FULLATTEND,OVERTIMEWAGE,AGEWAGE,ACCOMMODATION_ALLOWANCE,MEAL_ALLOWANCE,ATTENDANCEPENALTY,OTHERWAGE,SOCIALSECURITY_INDIVIDUAL,SOCIALSECURITY_COMPANY,HOUSINGPROVIDENTFUND_INDIVIDUAL,HOUSINGPROVIDENTFUND_COMPANY,TOTALINCOME,REALSALARY,REALSALARY_COMPANY ");
			strSql.Append(" FROM salary ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySQL.Query(strSql.ToString());
		}

		/*/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM salary ");
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from salary T ");
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
			parameters[0].Value = "salary";
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
        /// <param name="name"></param>
        /// <param name="employeeNo"></param>
        /// <param name="monthDate"></param>
        /// <returns></returns>
        public DataSet GetListUnionEmp(string name, string employeeNo, string monthDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select s.ID,EMPLOYEEID,EMPLOYEENO,MONTHDATE,
                            IFNULL(BASESALARY,0) BASESALARY,
                            IFNULL(CONTRACTSALARY,0) CONTRACTSALARY,
                            IFNULL(PIECEWAGE,0) PIECEWAGE,
                            IFNULL(PIECEPENALTY,0) PIECEPENALTY,
                            IFNULL(FULLATTEND,0) FULLATTEND,
                            IFNULL(OVERTIMEWAGE,0) OVERTIMEWAGE,
                            IFNULL(AGEWAGE,0) AGEWAGE,
                            IFNULL(ACCOMMODATION_ALLOWANCE,0) ACCOMMODATION_ALLOWANCE,
                            IFNULL(MEAL_ALLOWANCE,0) MEAL_ALLOWANCE,
                            IFNULL(ATTENDANCEPENALTY,0) ATTENDANCEPENALTY,
                            IFNULL(OTHERWAGE,0) OTHERWAGE,
                            IFNULL(SOCIALSECURITY_INDIVIDUAL,0) SOCIALSECURITY_INDIVIDUAL,
                            IFNULL(SOCIALSECURITY_COMPANY,0) SOCIALSECURITY_COMPANY,
                            IFNULL(HOUSINGPROVIDENTFUND_INDIVIDUAL,0) HOUSINGPROVIDENTFUND_INDIVIDUAL,
                            IFNULL(HOUSINGPROVIDENTFUND_COMPANY,0) HOUSINGPROVIDENTFUND_COMPANY,
                            IFNULL(REALSALARY,0) REALSALARY,
                            IFNULL(REALSALARY_COMPANY,0) REALSALARY_COMPANY ,

							
                            TOTALORDERAMOUNT,
                            TOTALINCOME");
            strSql.Append(" FROM salary S INNER JOIN EMPLOYEE E ON e.ID = S.employeeid where 1 = 1 ");
            if (!string.IsNullOrEmpty(name))
            {
                strSql.Append(" AND name LIKE  '%" + name + "%'");
            }
            if (!string.IsNullOrEmpty(employeeNo))
            {
                strSql.Append(" AND employeeNo LIKE  '%" + employeeNo + "%'");
            }
            if (!string.IsNullOrEmpty(monthDate))
            {
                strSql.Append(" AND DATE_FORMAT(MONTHDATE,'%Y-%m') =  '" + monthDate + "'");
            }
            strSql.Append(" ORDER BY EMPLOYEENO,MONTHDATE DESC ");
            return DbHelperMySQL.Query(strSql.ToString());
        }

		#endregion  ExtensionMethod
    }
}
