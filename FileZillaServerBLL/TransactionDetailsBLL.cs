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
    public class TransactionDetailsBLL
    {
        private readonly TransactionDetailsDAL dal = new TransactionDetailsDAL();
        public TransactionDetailsBLL()
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
		public bool Add(TransactionDetails model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(TransactionDetails model)
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
		public TransactionDetails GetModel(string ID)
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
		public List<TransactionDetails> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<TransactionDetails> DataTableToList(DataTable dt)
		{
			List<TransactionDetails> modelList = new List<TransactionDetails>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				TransactionDetails model;
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
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetListJoinEmpAndPrj(Dictionary<string,string> dicConditon, Dictionary<string, bool> dicSelectFlag, string transactionType, int pageIndex, int pageSize, out int totalAmount, out int sumAmount, out DataTable exportDataTable)
        {
            return dal.GetListJoinEmpAndPrj(dicConditon, dicSelectFlag, transactionType, pageIndex, pageSize, out totalAmount, out sumAmount, out exportDataTable);
        }

        /// <summary>
        /// 获取奖罚
        /// </summary>
        public decimal GetRewardAndAmercementAmount(string employeeId)
        {
            return dal.GetRewardAndAmercementAmount(employeeId);
        }

        /// <summary>
        /// 获取其他
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public decimal GetOtherAmount(string employeeId)
        {
            return dal.GetOtherAmount(employeeId);
        }
        #endregion  ExtensionMethod
    }
}
