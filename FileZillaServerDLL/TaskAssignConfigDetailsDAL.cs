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
    public class TaskAssignConfigDetailsDAL
    {
        public TaskAssignConfigDetailsDAL()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from taskassignconfigdetails");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
			parameters[0].Value = ID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(TaskAssignConfigDetails model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into taskassignconfigdetails(");
            strSql.Append("ID,EMPLOYEEID,SPECIALTYCATEGORY,QUALITYSCORE,AVAILABLE,TIMEMULTIPLE,SPECIALTYTYPE)");
            strSql.Append(" values (");
            strSql.Append("@ID,@EMPLOYEEID,@SPECIALTYCATEGORY,@QUALITYSCORE,@AVAILABLE,@TIMEMULTIPLE,@SPECIALTYTYPE)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40),
					new MySqlParameter("@EMPLOYEEID", MySqlDbType.VarChar,40),
					new MySqlParameter("@SPECIALTYCATEGORY", MySqlDbType.VarChar,2),
					new MySqlParameter("@QUALITYSCORE", MySqlDbType.Decimal,3),
					new MySqlParameter("@AVAILABLE", MySqlDbType.Int32,1),
					new MySqlParameter("@TIMEMULTIPLE", MySqlDbType.Int32,2),
					new MySqlParameter("@SPECIALTYTYPE", MySqlDbType.Int32,1)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.EMPLOYEEID;
            parameters[2].Value = model.SPECIALTYCATEGORY;
            parameters[3].Value = model.QUALITYSCORE;
            parameters[4].Value = model.AVAILABLE;
            parameters[5].Value = model.TIMEMULTIPLE;
            parameters[6].Value = model.SPECIALTYTYPE;

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
		public bool Update(TaskAssignConfigDetails model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update taskassignconfigdetails set ");
            strSql.Append("EMPLOYEEID=@EMPLOYEEID,");
            strSql.Append("SPECIALTYCATEGORY=@SPECIALTYCATEGORY,");
            strSql.Append("QUALITYSCORE=@QUALITYSCORE,");
            strSql.Append("AVAILABLE=@AVAILABLE,");
            strSql.Append("TIMEMULTIPLE=@TIMEMULTIPLE,");
            strSql.Append("SPECIALTYTYPE=@SPECIALTYTYPE");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@EMPLOYEEID", MySqlDbType.VarChar,40),
					new MySqlParameter("@SPECIALTYCATEGORY", MySqlDbType.VarChar,2),
					new MySqlParameter("@QUALITYSCORE", MySqlDbType.Decimal,3),
					new MySqlParameter("@AVAILABLE", MySqlDbType.Int32,1),
					new MySqlParameter("@TIMEMULTIPLE", MySqlDbType.Int32,2),
					new MySqlParameter("@SPECIALTYTYPE", MySqlDbType.Int32,1),
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)};
            parameters[0].Value = model.EMPLOYEEID;
            parameters[1].Value = model.SPECIALTYCATEGORY;
            parameters[2].Value = model.QUALITYSCORE;
            parameters[3].Value = model.AVAILABLE;
            parameters[4].Value = model.TIMEMULTIPLE;
            parameters[5].Value = model.SPECIALTYTYPE;
            parameters[6].Value = model.ID;

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
			strSql.Append("delete from taskassignconfigdetails ");
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
			strSql.Append("delete from taskassignconfigdetails ");
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
		public TaskAssignConfigDetails GetModel(string ID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select ID,EMPLOYEEID,SPECIALTYCATEGORY,QUALITYSCORE,AVAILABLE,TIMEMULTIPLE,SPECIALTYTYPE from taskassignconfigdetails ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40)			};
            parameters[0].Value = ID;

			TaskAssignConfigDetails model=new TaskAssignConfigDetails();
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
		public TaskAssignConfigDetails DataRowToModel(DataRow row)
		{
            TaskAssignConfigDetails model = new TaskAssignConfigDetails();
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
				if(row["SPECIALTYCATEGORY"]!=null)
				{
					model.SPECIALTYCATEGORY=row["SPECIALTYCATEGORY"].ToString();
				}
				if(row["QUALITYSCORE"]!=null && row["QUALITYSCORE"].ToString()!="")
				{
					model.QUALITYSCORE=decimal.Parse(row["QUALITYSCORE"].ToString());
				}
				if(row["AVAILABLE"]!=null && row["AVAILABLE"].ToString()!="")
				{
					model.AVAILABLE=int.Parse(row["AVAILABLE"].ToString());
                }
                if (row["TIMEMULTIPLE"] != null && row["TIMEMULTIPLE"].ToString() != "")
                {
                    model.TIMEMULTIPLE = int.Parse(row["TIMEMULTIPLE"].ToString());
                }
                if (row["SPECIALTYTYPE"] != null && row["SPECIALTYTYPE"].ToString() != "")
                {
                    model.SPECIALTYTYPE = int.Parse(row["SPECIALTYTYPE"].ToString());
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
            strSql.Append("select ID,EMPLOYEEID,SPECIALTYCATEGORY,QUALITYSCORE,AVAILABLE,TIMEMULTIPLE,SPECIALTYTYPE ");
			strSql.Append(" FROM taskassignconfigdetails ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetTaskAssignDetails(string employeeID, string employeeNo, string specialtyType)
        {
            string configName = specialtyType == "0" ? "专业类别" : "专业类别小类";
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT spc.*, tacd.ID, EMPLOYEEID,tacd.SPECIALTYCATEGORY, IFNULL(QUALITYSCORE, 0) QUALITYSCORE, IFNULL(AVAILABLE, 0) AVAILABLE, IFNULL(TIMEMULTIPLE, 0) TIMEMULTIPLE,SPECIALTYTYPE, '" + employeeNo + @"' employeeNo FROM (SELECT cv.configvalue specialtyName, cv.configKey specialtyKey from configvalue cv
                             INNER JOIN configtype ct
                             on cv.CONFIGTYPEID = ct.CONFIGTYPEID
                             WHERE ct.CONFIGTYPENAME = '" + configName + @"') spc
                             LEFT JOIN taskassignconfigdetails tacd
                             ON spc.specialtyKey = tacd.SPECIALTYCATEGORY
                             AND tacd.specialtyType = '" + specialtyType + @"'
                             AND tacd.employeeID = @employeeID ORDER BY spc.specialtyKey");
            MySqlParameter[] parameters = {
					new MySqlParameter("@employeeID", MySqlDbType.VarChar,40)};
            parameters[0].Value = employeeID;
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            return ds;
        }

        public DataTable GetSpecialtyConfig(string employeeID, string employeeNo)
        {
            StringBuilder strSql = new StringBuilder(@"
                select '" + employeeID + @"' employeeID, '" + employeeNo + @"' employeeNo, 0 qualityScore, configkey specialtyKey,configvalue specialtyName, NULL available from configvalue cv
                 left join configtype ct
                 on cv.configtypeid=ct.configtypeid
                WHERE ct.configtypename='专业类别'");
            DataSet ds = MySqlHelper.GetDataSet(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }

        ///// <summary>
        ///// 获取记录总数
        ///// </summary>
        //public int GetRecordCount(string strWhere)
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select count(1) FROM taskassignconfigdetails ");
        //    if(strWhere.Trim()!="")
        //    {
        //        strSql.Append(" where "+strWhere);
        //    }
        //    object obj = DbHelperSQL.GetSingle(strSql.ToString());
        //    if (obj == null)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return Convert.ToInt32(obj);
        //    }
        //}
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
			strSql.Append(")AS Row, T.*  from taskassignconfigdetails T ");
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
			parameters[0].Value = "taskassignconfigdetails";
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
