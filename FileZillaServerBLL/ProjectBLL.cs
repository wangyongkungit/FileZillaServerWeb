using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileZillaServerDAL;
using FileZillaServerModel;
using System.Data;

namespace FileZillaServerBLL
{
    /// <summary>
    /// 描    述：Project业务逻辑层
    /// 作    者：Yongkun Wang
    /// 创建时间：2016-03-02
    /// 修改历史：2017-03-02 Yongkun Wang 创建
    /// </summary>
    public class ProjectBLL
    {
        private readonly ProjectDAL dal = new ProjectDAL();
		public ProjectBLL()
		{}
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
		public bool Add(Project model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Project model)
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
        ///// <summary>
        ///// 删除一条数据
        ///// </summary>
        //public bool DeleteList(string IDlist )
        //{
        //    return dal.DeleteList(Maticsoft.Common.PageValidate.SafeLongFilter(IDlist,0) );
        //}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Project GetModel(string ID)
		{
			
			return dal.GetModel(ID);
		}

        ///// <summary>
        ///// 得到一个对象实体，从缓存中
        ///// </summary>
        //public Project GetModelByCache(string ID)
        //{
			
        //    string CacheKey = "projectModel-" + ID;
        //    object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
        //    if (objModel == null)
        //    {
        //        try
        //        {
        //            objModel = dal.GetModel(ID);
        //            if (objModel != null)
        //            {
        //                int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
        //                Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
        //            }
        //        }
        //        catch{}
        //    }
        //    return (Maticsoftproject)objModel;
        //}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
        /// <summary>
        /// 获得数据列表（关联）
        /// </summary>
        public DataSet GetListUnion(Dictionary<string, string> dicCondition, string sortExpression, int pageIndex, int pageSize, out int totalAmount)
        {
            return dal.GetListUnion(dicCondition, sortExpression, pageIndex, pageSize, out totalAmount);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Project> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Project> DataTableToList(DataTable dt)
		{
			List<Project> modelList = new List<Project>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Project model;
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
			return GetList("");
		}

		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/*/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
        public DataSet GetList(int PageSize, int PageIndex, string strWhere)
        {
            return dal.GetList(PageSize, PageIndex, strWhere);
        }*/

		#endregion  BasicMethod
		#region  ExtensionMethod

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool LogicDelete(string ID)
        {
            return dal.LogicDelete(ID);
        }

        /// <summary>
        /// 获取完成人
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public string GetFinishedPerson(string projectID)
        {
            return dal.GetFinishedPerson(projectID);
        }

        /// <summary>
        /// 获取完成稿
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public DataTable GetFinalScript(string projectID)
        {
            return dal.GetFinalScript(projectID);
        }

        /// <summary>
        /// 根据任务ID和完成人ID获取完成稿
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="finishedPerson"></param>
        /// <returns></returns>
        public DataTable GetFinalScript(string projectID, string finishedPerson)
        {
            return dal.GetFinalScript(projectID, finishedPerson);
        }

        /// <summary>
        /// 根据任务ID获取员工编号和任务名
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public DataTable GetEmployeeNoAndTaskNo(string projectID)
        {
            return dal.GetEmployeeNoAndTaskNo(projectID);
        }

        /// <summary>
        /// 获取修改稿
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public DataTable GetProjectModifyByPrjID(string projectID)
        {
            return dal.GetProjectModifyByPrjID(projectID);
        }

        /// <summary>
        /// 根据修改记录的ID获取修改记录稿
        /// </summary>
        /// <param name="projectModifyID"></param>
        /// <returns></returns>
        public DataTable GetProjectModifyByModifyID(string projectModifyID)
        {
            return dal.GetProjectModifyByModifyID(projectModifyID);
        }

        /// <summary>
        /// 删除售后任务
        /// </summary>
        /// <param name="ID">售后任务ID</param>
        /// <returns></returns>
        public bool DeleteProjectModifyTask(string ID)
        {
            return dal.DeleteProjectModifyTask(ID);
        }

        /// <summary>
        /// 判断待审核的修改任务是否存在
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public bool IsExistModifyTaskWaitforReview(string projectID)
        {
            return dal.IsExistModifyTaskWaitforReview(projectID);
        }

        /// <summary>
        /// 根据修改记录的ID获取修改记录稿
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public DataTable GetProjectForEmployeeHome(string employeeID, string where, int pageIndex, int pageSize, out int totalAmount)
        {
            return dal.GetProjectForEmployeeHome(employeeID, where, pageIndex, pageSize, out totalAmount);
        }

        /// <summary>
        /// 根据任务编号获取任务ID
        /// </summary>
        /// <param name="taskNo"></param>
        /// <returns></returns>
        public string GetPrjIDByTaskNo(string taskNo)
        {
            return dal.GetPrjIDByTaskNo(taskNo);
        }

        /// <summary>
        /// 判断完成稿是否存在
        /// </summary>
        /// <param name="projectID"></param>
        /// <param name="modifyFolderName"></param>
        /// <returns></returns>
        public bool IsExistFinalModifyScript(string projectID, string modifyFolderName)
        {
            return dal.IsExistFinalModifyScript(projectID, modifyFolderName);
        }

        /// <summary>
        /// 添加一条修改任务记录
        /// </summary>
        /// <param name="projectID">对应的普通任务的ID</param>
        /// <param name="folderName">目录名</param>
        /// <param name="isFinished">是否完成</param>
        /// <param name="reviewStatus">是否审核通过</param>
        /// <param name="dtCreate">创建时间</param>
        /// <returns></returns>
        public bool AddProjectModify(string projectID, string folderName, int isFinished, int reviewStatus, DateTime dtCreate)
        {
            return dal.AddProjectModify(projectID, folderName, isFinished, reviewStatus, dtCreate);
        }
        #endregion  ExtensionMethod
    }
}
