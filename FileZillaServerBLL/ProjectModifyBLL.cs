using FileZillaServerDAL;
using FileZillaServerModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerBLL
{
    /// <summary>
    /// 描    述：ProjectModify业务逻辑层
    /// 作    者：Yongkun Wang
    /// 创建时间：2016-03-02
    /// 修改历史：2017-03-02 Yongkun Wang 创建
    /// </summary>
    public class ProjectModifyBLL
    {
        private readonly ProjectModifyDAL dal=new ProjectModifyDAL();
		public ProjectModifyBLL()
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
		public bool Add(ProjectModify model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(ProjectModify model)
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
		/*/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(Maticsoft.Common.PageValidate.SafeLongFilter(IDlist,0) );
		}*/

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public ProjectModify GetModel(string ID)
		{			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			return dal.GetList(strWhere);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ProjectModify> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<ProjectModify> DataTableToList(DataTable dt)
		{
			List<ProjectModify> modelList = new List<ProjectModify>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				ProjectModify model;
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

        /*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			return dal.GetRecordCount(strWhere);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}
        */

		#endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 获取指定projectID的修改任务数量
        /// </summary>
        /// <param name="projectID"></param>
        /// <returns></returns>
        public int GetModifyTaskCount(string projectID)
        {
            return dal.GetModifyTaskCount(projectID);
        }
		#endregion  ExtensionMethod
    }
}
