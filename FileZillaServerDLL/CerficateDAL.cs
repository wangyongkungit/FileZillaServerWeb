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
    public class CerficateDAL
    {
        public CerficateDAL()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from cerficate");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,36)			};
			parameters[0].Value = ID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(Cerficate model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into cerficate(");
            strSql.Append("ID,EMPLOYEEID,CERTIFICATENAME,FILEPATH,DESCRIPTION,ISMAIN)");
            strSql.Append(" values (");
            strSql.Append("@ID,@EMPLOYEEID,@CERTIFICATENAME,@FILEPATH,@DESCRIPTION,@ISMAIN)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,36),
					new MySqlParameter("@EMPLOYEEID", MySqlDbType.VarChar,36),
					new MySqlParameter("@CERTIFICATENAME", MySqlDbType.VarChar,100),
					new MySqlParameter("@FILEPATH", MySqlDbType.VarChar,255),
					new MySqlParameter("@DESCRIPTION", MySqlDbType.VarChar,150),
					new MySqlParameter("@ISMAIN", MySqlDbType.Bit)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.EMPLOYEEID;
            parameters[2].Value = model.CERTIFICATENAME;
            parameters[3].Value = model.FILEPATH;
            parameters[4].Value = model.DESCRIPTION;
            parameters[5].Value = model.ISMAIN;

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
		public bool Update(Cerficate model)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update cerficate set ");
            strSql.Append("EMPLOYEEID=@EMPLOYEEID,");
            strSql.Append("CERTIFICATENAME=@CERTIFICATENAME,");
            strSql.Append("FILEPATH=@FILEPATH,");
            strSql.Append("DESCRIPTION=@DESCRIPTION,");
            strSql.Append("ISMAIN=@ISMAIN");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@EMPLOYEEID", MySqlDbType.VarChar,36),
					new MySqlParameter("@CERTIFICATENAME", MySqlDbType.VarChar,100),
					new MySqlParameter("@FILEPATH", MySqlDbType.VarChar,255),
					new MySqlParameter("@DESCRIPTION", MySqlDbType.VarChar,150),
					new MySqlParameter("@ISMAIN", MySqlDbType.Bit),
					new MySqlParameter("@ID", MySqlDbType.VarChar,36)};
            parameters[0].Value = model.EMPLOYEEID;
            parameters[1].Value = model.CERTIFICATENAME;
            parameters[2].Value = model.FILEPATH;
            parameters[3].Value = model.DESCRIPTION;
            parameters[4].Value = model.ISMAIN;
            parameters[5].Value = model.ID;

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
        /// 设置指定 ID 以外的证书图片为非主图
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool SetIsMainToFalseExceptID(string ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update cerficate set ");
            strSql.Append("ISMAIN=@ISMAIN");
            strSql.Append(" where ID <> @ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ISMAIN", MySqlDbType.Bit),
					new MySqlParameter("@ID", MySqlDbType.VarChar,36)};
            parameters[0].Value = false;
            parameters[1].Value = ID;
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
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from cerficate ");
			strSql.Append(" where ID=@ID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,36)			};
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
			strSql.Append("delete from cerficate ");
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
		public Cerficate GetModel(string ID)
		{

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,EMPLOYEEID,CERTIFICATENAME,FILEPATH,DESCRIPTION,ISMAIN from cerficate ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,36)			};
            parameters[0].Value = ID;

			Cerficate model=new Cerficate();
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
		public Cerficate DataRowToModel(DataRow row)
		{
			Cerficate model=new Cerficate();
			if (row != null)
			{
				if(row["ID"]!=null)
				{
					model.ID=row["ID"].ToString();
				}
                if (row["EMPLOYEEID"] != null)
                {
                    model.EMPLOYEEID = row["EMPLOYEEID"].ToString();
                }
				if(row["CERTIFICATENAME"]!=null)
				{
					model.CERTIFICATENAME=row["CERTIFICATENAME"].ToString();
				}
				if(row["FILEPATH"]!=null)
				{
					model.FILEPATH=row["FILEPATH"].ToString();
				}
				if(row["DESCRIPTION"]!=null)
				{
					model.DESCRIPTION=row["DESCRIPTION"].ToString();
				}
				if(row["ISMAIN"]!=null && row["ISMAIN"].ToString()!="")
				{
					if((row["ISMAIN"].ToString()=="1")||(row["ISMAIN"].ToString().ToLower()=="true"))
					{
						model.ISMAIN=true;
					}
					else
					{
						model.ISMAIN=false;
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
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select ID,EMPLOYEEID,CERTIFICATENAME,FILEPATH,DESCRIPTION,ISMAIN ");
			strSql.Append(" FROM cerficate ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySQL.Query(strSql.ToString());
		}
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
			strSql.Append(")AS Row, T.*  from cerficate T ");
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
			parameters[0].Value = "cerficate";
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
