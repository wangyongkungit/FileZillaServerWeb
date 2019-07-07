using FileZillaServerCommonHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerDAL
{
    public class FileZillaDAL
    {
        /// <summary>
        /// 获取日志列表
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="operateType">操作类型</param>
        /// <param name="content">记录内容</param>
        /// <param name="startDateTime">开始时间</param>
        /// <param name="endDateTime">结束时间</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public DataTable GetFileZilla(string userID, string operateType, string content, string startDateTime, string endDateTime, string fileName, int pageIndex, int pageSize, out int totalAmount)
        {
            totalAmount = 0;
            StringBuilder strSql = new StringBuilder(@"
           /*  SELECT fz.OPERATEUSERID,fz.content,fz.operatedate,fz.filename,fz.ipaddress,cfg.configvalue operatetype FROM fileZillaLog fz 
            LEFT JOIN 
            (
                select configkey,configvalue from configvalue cv
                 left join configtype ct
                 on cv.configtypeid=ct.configtypeid
            ) cfg
            ON fz.operatetype=cfg.configkey
            WHERE 1=1  */

SELECT fz.OPERATEUSERID,fz.content,fz.operatedate,fz.filename,fz.ipaddress,cfg.configvalue operatetype FROM fileZillaLog fz 
            LEFT JOIN 
            (
                select configkey,configvalue from configvalue cv
                 left join configtype ct
                 on cv.configtypeid=ct.configtypeid
            ) cfg
            ON fz.operatetype=cfg.configkey
            WHERE fz.ID  in
 (    SELECT Id FROM
(
SELECT  ID 
 FROM fileZillaLog fz   
WHERE 1 = 1 
");

            StringBuilder sbWhere = new StringBuilder();
            if (!string.IsNullOrEmpty(userID))
            {
                sbWhere.AppendFormat(" AND OPERATEUSERID='{0}'", userID);
            }
            if (!string.IsNullOrEmpty(operateType))
            {
                sbWhere.AppendFormat(" AND OPERATETYPE='{0}'", operateType);
            }
            if (!string.IsNullOrEmpty(content))
            {
                //sbWhere.AppendFormat(" AND CONTENT LIKE '%{0}%'", content);
                sbWhere.AppendFormat(" AND LOCATE('{0}', CONTENT) > 0 ", content);
            }
            if (!string.IsNullOrEmpty(startDateTime))
            {
                sbWhere.AppendFormat(" AND OPERATEDATE>='{0}'", startDateTime);
            }
            if (!string.IsNullOrEmpty(endDateTime))
            {
                sbWhere.AppendFormat(" AND OPERATEDATE<='{0}'", endDateTime);
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                //sbWhere.AppendFormat(" AND FILENAME LIKE '%{0}%'", fileName);
                sbWhere.AppendFormat(" AND LOCATE('{0}', FILENAME) > 0 ", fileName);
            }
            //DataSet dsRowsCount = MySqlHelper.GetDataSet(strSql.ToString());
            //if (dsRowsCount != null && dsRowsCount.Tables.Count > 0)
            //{
            //    totalAmount = dsRowsCount.Tables[0].Rows.Count;
            //}
            totalAmount = GetTotalCount(sbWhere.ToString());
            //strSql.AppendFormat(" ORDER BY OPERATEDATE DESC LIMIT {0},{1}", (pageIndex - 1) * pageSize, pageSize);
            strSql.Append(sbWhere).AppendFormat(" ORDER BY operatedate DESC LIMIT {0},{1}) as aa ) ", (pageIndex - 1) * pageSize, pageSize);
            DataSet ds = MySqlHelper.GetDataSet(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            else
                return null;
        }

        private int GetTotalCount(string where)
        {
            string strSql = "SELECT COUNT(*) FROM filezillalog WHERE 1 = 1 " + where;
            string connectionString = ConfigurationManager.ConnectionStrings["MySqlConnection"].ToString();
            CommandType cmdType = CommandType.Text;
            int cnt = Convert.ToInt32(MySqlHelper.ExecuteScalar(connectionString, cmdType, strSql, null));
            return cnt;
        }

        /// <summary>
        /// 新增FileZilla日志
        /// </summary>
        /// <param name="userID">用户编号</param>
        /// <param name="operateType">操作类型</param>
        /// <param name="content">日志内容</param>
        /// <param name="dateTime">记录时间</param>
        /// <param name="fileName">操作的文件名</param>
        /// <returns></returns>
        public int Add(string userID, FileZillaOperateType operateType, string content, DateTime dateTime, string fileName)
        {
            string insertSql = string.Format(@"insert into filezillalog(ID,OPERATEUSERID,OPERATETYPE,CONTENT,OPERATEDATE,FILENAME)
                                              values('{0}','{1}','{2}','{3}','{4}'," + (fileName != null ? "'{5}'" : "null") + ")",
                                              Guid.NewGuid(), userID, operateType, content, dateTime, fileName);
            int rows = MySqlHelper.ExecuteNonQuery(insertSql);
            return rows;
        }

        /// <summary>
        /// 删除FileZilla日志
        /// </summary>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public int Delete(string startDate, string endDate)
        {
            string strSql = string.Format(string.Format(@"DELETE FROM filezillalog WHERE OPERATEDATE>='{0}' AND OPERATEDATE<='{1}'", startDate, endDate));
            int rows = MySqlHelper.ExecuteNonQuery(strSql);
            return rows;
        }
    }
}
