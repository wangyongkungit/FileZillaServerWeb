using FileZillaServerDAL;
using FileZillaServerModel;
using FileZillaServerModel.Config;
using FileZillaServerModel.Interface;
using Jil;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FileZillaServerBLL
{
    public class FileCategoryBLL
    {
        private readonly FileCategoryDAL dal = new FileCategoryDAL();
        public FileCategoryBLL()
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
        public bool Add(FileCategory model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(FileCategory model)
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
        public FileCategory GetModel(string ID)
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
        public List<FileCategory> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FileCategory> DataTableToList(DataTable dt)
        {
            List<FileCategory> modelList = new List<FileCategory>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                FileCategory model;
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
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
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
        /// 根据proejctId和category获取orderSort
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="category"></param>
        /// <returns></returns>
        public int GetOrderSort(string projectId, string category)
        {
            return dal.GetOrderSort(projectId, category);
        }

        /// <summary>
        /// 获取回复tab列表
        /// </summary>
        /// <param name="categoryId">配置表中的categoryId</param>
        /// <param name="parentId">针对哪个记录进行的回复</param>
        /// <returns></returns>
        public DataSet GetReplayToTabList(string categoryId)
        {
            DataSet ds = dal.GetReplayToList(categoryId);
            return ds;
        }

        public bool AddFileCategory(HttpContext context, out int errCode)
        {
            errCode = 0;
            ConfigureBLL configBll = new ConfigureBLL();
            DataTable dtConfig = configBll.GetConfig("文件分类小类");
            string projectId = context.Request["projectId"];
            string categoryId = context.Request["categoryId"];
            string parentId = context.Request["parentId"];
            string description = context.Request["description"];
            string title = dtConfig.AsEnumerable().Where(item => Convert.ToString(item["configKey"]) == categoryId).Select(item => item.Field<string>("configValue")).FirstOrDefault();
            string rootPath = ConfigurationManager.AppSettings["taskAllotmentPath"];
            FileCategory fileCategory = new FileCategory();
            fileCategory.ID = Guid.NewGuid().ToString();
            fileCategory.PROJECTID = projectId;
            fileCategory.CATEGORY = categoryId;
            fileCategory.DESCRIPTION = description;
            fileCategory.ORDERSORT = GetOrderSort(projectId, categoryId);
            title += Convert.ToString(fileCategory.ORDERSORT);
            fileCategory.TITLE = title;
            fileCategory.FOLDERNAME = title;
            fileCategory.CREATEDATE = DateTime.Now;
            fileCategory.PARENTID = parentId;
            fileCategory.CLASSSORT = GlobalConfig.dicMap[categoryId];
            fileCategory.DIVISIONSORT = Convert.ToInt32(categoryId);
            // 添加数据库记录
            if (this.Add(fileCategory))
            {
                // 创建任务目录
                if (!GetFilePathByProjectId(projectId, title, out errCode))
                {
                    // 目录创建失败时，需要将已添加到数据库的记录删除
                    this.Delete(fileCategory.ID);
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 根据任务ID和title创建目录
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public bool GetFilePathByProjectId(string projectId, string folderName, out int errCode)
        {
            errCode = 0;
            FileHistoryBLL fileHistoryBll = new FileHistoryBLL();
            string taskNo = string.Empty;
            string returnFileName = string.Empty;
            try
            {
                // 根据任务ID获取任务编号和员工编号，如果有记录说明任务已分配，则需要到员工目录下寻找该文件
                DataTable dtTaskNoAndEmpNo = fileHistoryBll.GetTaskNoAndEmpNoByPrjId(projectId).Tables[0];
                // 任务已分配
                if (dtTaskNoAndEmpNo != null && dtTaskNoAndEmpNo.Rows.Count > 0)
                {
                    string finishedPerson = Convert.ToString((dtTaskNoAndEmpNo.Rows[0]["FINISHEDPERSON"]));
                    if (!string.IsNullOrEmpty(finishedPerson))
                    {
                        string rootPath = ConfigurationManager.AppSettings["employeePath"].ToString();

                        string empNo = dtTaskNoAndEmpNo.AsEnumerable().Select(item => Convert.ToString(item["employeeNo"])).FirstOrDefault();
                        string empNoFullPath = Path.Combine(rootPath, empNo);

                        taskNo = dtTaskNoAndEmpNo.AsEnumerable().Select(item => Convert.ToString(item["taskNo"])).FirstOrDefault();
                        string taskNoFinalFolder = Directory.GetDirectories(empNoFullPath, taskNo, SearchOption.AllDirectories).FirstOrDefault();
                        if (!string.IsNullOrEmpty(taskNoFinalFolder))
                        {
                            returnFileName = Path.Combine(rootPath, empNo, taskNo, folderName);
                        }
                    }
                    // 任务未分配
                    else
                    {
                        Project prj = new ProjectBLL().GetModel(projectId);
                        taskNo = prj.TASKNO;
                        string rootPath = ConfigurationManager.AppSettings["taskAllotmentPath"];
                        string taskNoFinalFolder = Directory.GetDirectories(rootPath, taskNo, SearchOption.TopDirectoryOnly).FirstOrDefault();
                        if (!string.IsNullOrEmpty(taskNoFinalFolder))
                        {
                            returnFileName = Path.Combine(rootPath, taskNo, folderName);
                        }
                    }
                }
                if (!Directory.Exists(returnFileName))
                {
                    try
                    {
                        Directory.CreateDirectory(returnFileName);
                    }
                    catch (Exception ex)
                    {
                        errCode = 1;
                    }
                    finally
                    {
                        if (!Directory.Exists(returnFileName))
                        {
                            errCode = 6503;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                errCode = 1;
            }
            return errCode == 0;
        }
        public DataSet GetReplyToTab(HttpContext context, out int errCode)
        {
            errCode = 0;
            string categoryId = context.Request["categoryId"];
            //string parentId = context.Request["parentId"];
            DataSet ds = this.GetReplayToTabList(categoryId);
            return ds;
        }
        #endregion  ExtensionMethod
    }
}
