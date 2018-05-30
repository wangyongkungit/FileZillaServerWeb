using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerDAL
{
    /// <summary>
    /// **************************************
    /// 描    述：Project数据访问层
    /// 作    者：Yongkun Wang
    /// 创建时间：2017-04-13
    /// 修改历史：2017-04-13 Yongkun Wang 创建
    /// **************************************
    /// </summary>
    public class SystemLogDALex
    {
        public DataTable GetSystemLog(Dictionary<string, string> dicCondition, int pageIndex, int pageSize, out int totalAmount)
        {
            totalAmount = 0;
            StringBuilder sbSql = new StringBuilder();
            sbSql.Append(@"SELECT e.employeeno,l.createdate,l.ipaddress, l.operatecontent from systemlog l
                         INNER JOIN employee e
                         ON l.employeeid = e.id
                         WHERE 1 = 1 ");
            if (dicCondition.ContainsKey("employeeNO"))
            {
                sbSql.AppendFormat(" AND employeeNO LIKE '%{0}%'", dicCondition["employeeNO"]);
            }
            if (dicCondition.ContainsKey("operateDateStart"))
            {
                sbSql.AppendFormat(" AND createDate >= '{0}'", dicCondition["operateDateStart"]);
            }
            if (dicCondition.ContainsKey("operateDateEnd"))
            {
                sbSql.AppendFormat(" AND createDate <= '{0}'", dicCondition["operateDateEnd"]);
            }
            if (dicCondition.ContainsKey("operateType"))
            {
                sbSql.AppendFormat(" AND operateType = '{0}'", dicCondition["operateType"]);
            }
            DataSet dsRowsCount = MySqlHelper.GetDataSet(sbSql.ToString());
            if (dsRowsCount != null && dsRowsCount.Tables.Count > 0)
            {
                totalAmount = dsRowsCount.Tables[0].Rows.Count;
            }
            sbSql.AppendFormat(" ORDER BY l.createdate DESC LIMIT {0},{1}", (pageIndex - 1) * pageSize, pageSize);
            DataSet ds = DbHelperMySQL.Query(sbSql.ToString());
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
    }
}
