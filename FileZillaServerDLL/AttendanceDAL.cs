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
    public class AttendanceDAL
    {
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,DINGTALKID,GROUPID,PLANID,RECORDID,WORKDATE,D_USERID,USERID,CHECKTYPE,TIMERESULT,LOCATIONRESULT,APPROVEID,BASECHECKTIME,USERCHECKTIME,DEDUCTMONEY,SCORE,DEVIATIONMINUTES ");
            strSql.Append(" FROM attendance ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表（关联员工表）
        /// </summary>
        public DataSet GetListUnionEmp(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select employeeno,name, WORKDATE,D_USERID,USERID,
                            case checkType
                            WHEN 'OnDuty' THEN '上班'
                            WHEN 'OffDuty' THEN '下班'
                            END CHECKTYPE,
                            case TIMERESULT 
                             WHEN 'Normal' THEN '正常'
                             WHEN 'Early' THEN '早退'
                             WHEN 'Late' THEN '迟到'
                             WHEN 'SeriousLate' THEN '严重迟到'
                             WHEN 'NotSigned' THEN '未打卡'
                             end timeresult,TIMERESULT,BASECHECKTIME,
                             USERCHECKTIME,DEDUCTMONEY,SCORE,DEVIATIONMINUTES
                             FROM attendance a
                             LEFT JOIN 
                             employee e
                             on a.userid=e.id
                             WHERE 1=1 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(strWhere);
            }
            strSql.Append(" ORDER BY employeeno, basechecktime");
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(Attendance model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into attendance(");
            strSql.Append("ID,DINGTALKID,GROUPID,PLANID,RECORDID,WORKDATE,D_USERID,USERID,CHECKTYPE,TIMERESULT,LOCATIONRESULT,APPROVEID,BASECHECKTIME,USERCHECKTIME,DEDUCTMONEY,SCORE,DEVIATIONMINUTES)");
            strSql.Append(" values (");
            strSql.Append("@ID,@DINGTALKID,@GROUPID,@PLANID,@RECORDID,@WORKDATE,@D_USERID,@USERID,@CHECKTYPE,@TIMERESULT,@LOCATIONRESULT,@APPROVEID,@BASECHECKTIME,@USERCHECKTIME,@DEDUCTMONEY,@SCORE,@DEVIATIONMINUTES)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.VarChar,40),
					new MySqlParameter("@DINGTALKID", MySqlDbType.VarChar,40),
					new MySqlParameter("@GROUPID", MySqlDbType.VarChar,40),
					new MySqlParameter("@PLANID", MySqlDbType.VarChar,40),
					new MySqlParameter("@RECORDID", MySqlDbType.VarChar,40),
					new MySqlParameter("@WORKDATE", MySqlDbType.DateTime),
					new MySqlParameter("@D_USERID", MySqlDbType.VarChar,40),
					new MySqlParameter("@USERID", MySqlDbType.VarChar,40),
					new MySqlParameter("@CHECKTYPE", MySqlDbType.VarChar,255),
					new MySqlParameter("@TIMERESULT", MySqlDbType.VarChar,30),
					new MySqlParameter("@LOCATIONRESULT", MySqlDbType.VarChar,30),
					new MySqlParameter("@APPROVEID", MySqlDbType.VarChar,40),
					new MySqlParameter("@BASECHECKTIME", MySqlDbType.DateTime),
					new MySqlParameter("@USERCHECKTIME", MySqlDbType.DateTime),
					new MySqlParameter("@DEDUCTMONEY", MySqlDbType.Decimal,8),
					new MySqlParameter("@SCORE", MySqlDbType.Decimal,6),
					new MySqlParameter("@DEVIATIONMINUTES", MySqlDbType.Decimal,6)};
            parameters[0].Value = model.ID;
            parameters[1].Value = model.DINGTALKID;
            parameters[2].Value = model.GROUPID;
            parameters[3].Value = model.PLANID;
            parameters[4].Value = model.RECORDID;
            parameters[5].Value = model.WORKDATE;
            parameters[6].Value = model.D_USERID;
            parameters[7].Value = model.USERID;
            parameters[8].Value = model.CHECKTYPE;
            parameters[9].Value = model.TIMERESULT;
            parameters[10].Value = model.LOCATIONRESULT;
            parameters[11].Value = model.APPROVEID;
            parameters[12].Value = model.BASECHECKTIME;
            parameters[13].Value = model.USERCHECKTIME;
            parameters[14].Value = model.DEDUCTMONEY;
            parameters[15].Value = model.SCORE;
            parameters[16].Value = model.DEVIATIONMINUTES;

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
