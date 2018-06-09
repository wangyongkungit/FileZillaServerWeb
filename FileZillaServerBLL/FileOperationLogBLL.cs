using FileZillaServerDAL;
using FileZillaServerModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FileZillaServerBLL
{
    public class FileOperationLogBLL
    {
        private readonly FileOperationLogDAL dal = new FileOperationLogDAL();
        public FileOperationLogBLL()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string ID)
        {
            return dal.Exists(ID);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(FileOperationLog model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(FileOperationLog model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string ID)
        {

            return dal.Delete(ID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string IDlist)
        {
            return dal.DeleteList(IDlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public FileOperationLog GetModel(string ID)
        {

            return dal.GetModel(ID);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, string orderBy)
        {
            return dal.GetList(strWhere, orderBy);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FileOperationLog> GetModelList(string strWhere, string orderBy)
        {
            DataSet ds = dal.GetList(strWhere, orderBy);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FileOperationLog> DataTableToList(DataTable dt)
        {
            List<FileOperationLog> modelList = new List<FileOperationLog>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                FileOperationLog model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("", "");
        }
        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 获取操作记录，join Employee 表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public DataSet GetListJoinEmployee(string strWhere, string orderBy)
        {
            return dal.GetListJoinEmployee(strWhere, orderBy);
        }

        public DataSet GetFileOperateLogs(HttpContext context, out int errCode)
        {
            errCode = 0;
            string parentId = context.Request["projectId"];
            string where = string.Format(" projectId = '{0}'", parentId);
            // 加入排序字段
            string orderBy = string.Format(" ORDER BY operateDate");
            DataSet fileOperateLogs = this.GetListJoinEmployee(where, orderBy);
            return fileOperateLogs;
        }
        #endregion  ExtensionMethod
    }
}
