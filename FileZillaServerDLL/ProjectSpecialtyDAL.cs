using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerDAL
{
    public class ProjectSpecialtyDAL
    {
        /// <summary>
        /// 获得任务专业
        /// </summary>
        public DataSet GetSpecialtyInnerJoinProject(string projectID, string type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT p.id pid, ps.id psid, ps.specialtyid, ps.type from project p
                             LEFT JOIN projectspecialty ps
                             on p.id = ps.projectid
                             where p.id = '" + projectID + "'");
            if (!string.IsNullOrEmpty(type))
            {
                strSql.Append(" and ps.type = '" + type + "'");
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 判断专业是否已设置
        /// </summary>
        public bool GetSpecialtyExistsByProjectIDAndSpecialtyCate(string projectID, string specialtyCategory, string type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT count(*) cnt from project p
                             LEFT JOIN projectspecialty ps
                             on p.id = ps.projectid
                             where p.id = '" + projectID + "' and specialtyID = '" + specialtyCategory + "' AND type ='" + type + "'");
            bool exists = DbHelperMySQL.Exists(strSql.ToString());
            return exists;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteByProjectID(string projectID, string type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from projectspecialty ");
            strSql.Append(" where projectID=@projectID and type=@type ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@projectID", MySqlDbType.VarChar,40),
                    new MySqlParameter("@type", MySqlDbType.Int32,1)};
            parameters[0].Value = projectID;
            parameters[1].Value = type;
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
        /// 增加一条数据
        /// </summary>
        public bool Add(string projectID, string specialtyID, string type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into projectspecialty(");
            strSql.Append("ID,PROJECTID,SPECIALTYID,TYPE)");
            strSql.Append(" values (");
            strSql.Append("@ID,@PROJECTID,@SPECIALTYID,@TYPE)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40),
					new MySqlParameter("@PROJECTID", MySqlDbType.VarChar,40),
					new MySqlParameter("@SPECIALTYID", MySqlDbType.VarChar,40),
                                          new MySqlParameter("@TYPE", MySqlDbType.Int32,1)};
            parameters[0].Value = Guid.NewGuid().ToString();
            parameters[1].Value = projectID;
            parameters[2].Value = specialtyID;
            parameters[3].Value = type;

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
    }
}
