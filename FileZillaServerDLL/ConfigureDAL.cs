using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerDAL
{
    public class ConfigureDAL
    {
        /// <summary>
        /// 根据配置名称获取配置
        /// </summary>
        /// <returns></returns>
        public DataTable GetConfig()
        {
            String strSql = @"select configkey,configvalue,configtypename from configvalue cv
                 left join configtype ct
                 on cv.configtypeid=ct.configtypeid";
            DataSet ds = MySqlHelper.GetDataSet(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }

        /// <summary>
        /// 根据配置名称获取配置
        /// </summary>
        /// <param name="configType">配置名称</param>
        /// <returns></returns>
        public DataTable GetConfig(string configType)
        {
            StringBuilder strSql = new StringBuilder(@"
                select configkey,configvalue from configvalue cv
                 left join configtype ct
                 on cv.configtypeid=ct.configtypeid
                WHERE ct.configtypename='" + configType + "'");
            DataSet ds = MySqlHelper.GetDataSet(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }

        /// <summary>
        /// 获取默认提成比例
        /// </summary>
        /// <returns></returns>
        public DataTable GetDefaultProportionConfig()
        {
            StringBuilder strSql = new StringBuilder(@"
                SELECT ct.ID, cv.CONFIGVALUE from configvalue cv
                 INNER JOIN configtype ct
                 on cv.CONFIGTYPEID = ct.CONFIGTYPEID
                 WHERE ct.CONFIGTYPENAME = '任务默认提成比例';");
            DataSet ds = MySqlHelper.GetDataSet(strSql.ToString());
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <returns></returns>
        public DataTable GetProvince()
        {
            String strSql = @"SELECT `CODE`,`NAME` from region order by `code`";
            DataSet ds = MySqlHelper.GetDataSet(strSql);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }

        /// <summary>
        /// 获取任务目标值
        /// </summary>
        /// <returns></returns>
        public DataTable GetTaskObjectiveValue()
        {
            string sql = @"SELECT t.*,employeeno from taskobjectivevalue t
                            INNER JOIN employee e
                            on t.employeeid=e.id order by employeeno";
            DataSet ds = MySqlHelper.GetDataSet(sql);
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;
        }

        /// <summary>
        /// 更新目标值
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="d_value"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateObjectiveValue(int objValue, int d_value, string id)
        {
            string sql = string.Format(@"UPDATE taskobjectivevalue SET objectiveValue = {0}, d_value = {1} WHERE ID = '{2}'", objValue, d_value, id);
            int rows = DbHelperMySQL.ExecuteSql(sql);
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
        /// 更新默认任务提成比例
        /// </summary>
        /// <returns></returns>
        public bool UpdateDefaultProportion(decimal newProportion)
        {
            string sql = string.Format(@"UPDATE configvalue SET configvalue = '{0}' WHERE CONFIGTYPEID = '9C349F71-7032-4771-890D-54C9DE73D8C0'", newProportion);
            int rows = DbHelperMySQL.ExecuteSql(sql);
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
