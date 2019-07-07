﻿using FileZillaServerDAL;
using FileZillaServerModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerBLL
{
    public class TaskAssignConfigDetailsBLL
    {
        private readonly TaskAssignConfigDetailsDAL dal=new TaskAssignConfigDetailsDAL();
		public TaskAssignConfigDetailsBLL()
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
		public bool Add(TaskAssignConfigDetails model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(TaskAssignConfigDetails model)
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
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(IDlist );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public TaskAssignConfigDetails GetModel(string ID)
		{
			
			return dal.GetModel(ID);
		}

        ///// <summary>
        ///// 得到一个对象实体，从缓存中
        ///// </summary>
        //public TaskAssignConfigDetails GetModelByCache(string ID)
        //{
			
        //    string CacheKey = "TaskAssignConfigDetailsModel-" + ID;
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
        //    return (TaskAssignConfigDetails)objModel;
        //}

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
		public List<TaskAssignConfigDetails> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetTaskAssignDetails(string employeeID, string specialtyType)
        {
            EmployeeDAL eDal = new EmployeeDAL();
            Employee emp = eDal.GetModel(employeeID);
            return dal.GetTaskAssignDetails(employeeID, emp.EMPLOYEENO, specialtyType);
        }

        public DataSet GetEmpNoSpecicaltyNameAndTaskAcountByEmpID()
        {
            return dal.GetEmpNoSpecicaltyNameAndTaskAcountByEmpID();
        }

        public DataTable GetSpecialtyConfig(string employeeID)
        {
            EmployeeDAL eDal = new EmployeeDAL();
            Employee emp = eDal.GetModel(employeeID);
            return dal.GetSpecialtyConfig(employeeID, emp.EMPLOYEENO);
        }
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TaskAssignConfigDetails> DataTableToList(DataTable dt)
		{
			List<TaskAssignConfigDetails> modelList = new List<TaskAssignConfigDetails>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TaskAssignConfigDetails model;
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

        ///// <summary>
        ///// 分页获取数据列表
        ///// </summary>
        //public int GetRecordCount(string strWhere)
        //{
        //    return dal.GetRecordCount(strWhere);
        //}
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

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
    }
}
