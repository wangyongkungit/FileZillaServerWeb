using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerDAL
{
    public class TestDAL
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add()
        {
            StringBuilder strSql01 = new StringBuilder();
            strSql01.Append("select ID,SPECIALTYCATEGORY from project");
            DataSet ds = DbHelperMySQL.Query(strSql01.ToString());
            DataTable dt = ds.Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into projectspecialty(");
                strSql.Append("ID,PROJECTID,SPECIALTYID)");
                strSql.Append(" values (");
                strSql.Append("@ID,@PROJECTID,@SPECIALTYID)");
                MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40),
					new MySqlParameter("@PROJECTID", MySqlDbType.VarChar,40),
					new MySqlParameter("@SPECIALTYID", MySqlDbType.VarChar,40)};
                parameters[0].Value = Guid.NewGuid().ToString();
                parameters[1].Value = dt.Rows[i]["ID"].ToString();
                parameters[2].Value = dt.Rows[i]["SPECIALTYCATEGORY"].ToString();

                int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
                if (rows > 0)
                {
                    
                }
                else
                {
                   
                }
            }
            return true;
        }
    }
}
