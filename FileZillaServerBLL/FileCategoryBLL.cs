using FileZillaServerDAL;
using FileZillaServerModel;
using FileZillaServerModel.Config;
using FileZillaServerModel.Interface;
using FileZillaServerProfile;
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
        public DataSet GetList(string strWhere, string orderBy)
        {
            return dal.GetList(strWhere, orderBy);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<FileCategory> GetModelList(string strWhere, string orderBy)
        {
            DataSet ds = dal.GetList(strWhere, orderBy);
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
            return GetList(string.Empty, string.Empty);
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

        #endregion  BasicMethod
        #region  ExtensionMethod
        /// <summary>
        /// 根据 fileHistoryId 获取 projectId
        /// </summary>
        public DataSet GetProjectIdByFileHistoryId(string fileHistoryId)
        {
            return dal.GetProjectIdByFileHistoryId(fileHistoryId);
        }

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
        /// 根据 parentId 获取序号
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public int GetOrderSortForChildTab(string parentId)
        {
            return dal.GetOrderSortForChildTab(parentId);
        }

        #region 获取回复tab列表
        /// <summary>
        /// 获取回复tab列表
        /// </summary>
        /// <param name="categoryId">配置表中的categoryId</param>
        /// <param name="parentId">针对哪个记录进行的回复</param>
        /// <returns></returns>
        public DataSet GetReplayToTabList(string projectId, string categoryId)
        {
            DataSet ds = dal.GetReplayToList(projectId, categoryId);
            return ds;
        }
        #endregion


        #region 添加文件类别
        public bool AddFileCategory(HttpContext context, out int errCode)
        {
            errCode = 0;
            string returnFolderName = string.Empty;
            string taskRootFolder = string.Empty;
            string taskFolderWithoutEmpNo = string.Empty;
            ConfigureBLL configBll = new ConfigureBLL();
            DataTable dtConfig = configBll.GetConfig("文件分类小类");
            string projectId = context.Request["projectId"];
            string categoryId = context.Request["categoryId"];
            string parentId = context.Request["parentId"];
            string description = context.Request["description"];
            string expireDate = context.Request["expiredate"];
            string title = dtConfig.AsEnumerable().Where(item => Convert.ToString(item["configKey"]) == categoryId).Select(item => item.Field<string>("configValue")).FirstOrDefault();
            string rootPath = ConfigurationManager.AppSettings["taskAllotmentPath"];
            FileCategory fileCategory = new FileCategory();
            fileCategory.ID = Guid.NewGuid().ToString();
            fileCategory.PROJECTID = projectId;
            fileCategory.CATEGORY = categoryId;
            fileCategory.DESCRIPTION = description;
            // 新的修改或者疑问
            if (parentId == "0")
            {
                fileCategory.ORDERSORT = GetOrderSort(projectId, categoryId);
            }
            // 修改完成或者疑问答复
            else
            {
                fileCategory.ORDERSORT = GetOrderSortForChildTab(parentId);
            }
            if (categoryId != "1" && categoryId != "2")
            {
                title += Convert.ToString(fileCategory.ORDERSORT);
            }
            fileCategory.TITLE = title;
            fileCategory.FOLDERNAME = title;
            fileCategory.CREATEDATE = DateTime.Now;
            fileCategory.CREATEUSER = UserProfile.GetInstance().ID;
            DateTime dtExpire = IsDate(expireDate) ? Convert.ToDateTime(expireDate) : DateTime.MinValue;
            fileCategory.EXPIREDATE = dtExpire;
            fileCategory.PARENTID = parentId;
            fileCategory.CLASSSORT = GlobalConfig.dicMap[categoryId];
            fileCategory.DIVISIONSORT = Convert.ToInt32(categoryId);
            //FileCategory fileCategoryExist = this.GetModelList("", "").FirstOrDefault();
            // 添加数据库记录
            if (this.Add(fileCategory))
            {
                if (fileCategory.CATEGORY == "2")
                {

                }
                // 创建任务目录
                if (!GetFilePathByProjectId(projectId, fileCategory.CATEGORY, fileCategory.FOLDERNAME, true, out returnFolderName, out taskRootFolder, out taskFolderWithoutEmpNo, out errCode))
                {
                    // 目录创建失败时，需要将已添加到数据库的记录删除
                    this.Delete(fileCategory.ID);
                    return false;
                }
                return true;
            }
            return false;
        }

        #region 创建目录
        /// <summary>
        /// 根据任务ID和title创建目录
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="folderName"></param>
        /// <param name="errCode">返回码</param>
        /// <returns></returns>
        public bool GetFilePathByProjectId(string projectId, string fileCategory, string folderName, bool isCreateFolder, out string returnFolderName, out string taskRootFolder, out string taskFolderWithoutEmpNo, out int errCode)
        {
            errCode = 0;
            returnFolderName = string.Empty;
            // 任务的根目录（通常就是任务编号命名的目录）
            taskRootFolder = string.Empty;
            // 不带员工编号的任务目录名，此处主要为任务转移模块服务
            taskFolderWithoutEmpNo = string.Empty;
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
                    // 修改记录文件夹
                    string modifyRecordFolderName = Convert.ToString(ConfigurationManager.AppSettings["modifyRecordFolderName"]);
                    // 疑问记录文件夹
                    string questionFolderName = Convert.ToString(ConfigurationManager.AppSettings["questionFolderName"]);
                    string finishedPerson = Convert.ToString((dtTaskNoAndEmpNo.Rows[0]["FINISHEDPERSON"]));
                    if (!string.IsNullOrEmpty(finishedPerson))
                    {
                        string rootPath = Convert.ToString(ConfigurationManager.AppSettings["employeePath"]);

                        string empNo = dtTaskNoAndEmpNo.AsEnumerable().Select(item => Convert.ToString(item["employeeNo"])).FirstOrDefault();
                        string empNoFullPath = Path.Combine(rootPath, empNo);

                        taskNo = dtTaskNoAndEmpNo.AsEnumerable().Select(item => Convert.ToString(item["taskNo"])).FirstOrDefault();
                        string taskNoFinalFolder = Directory.GetDirectories(empNoFullPath, taskNo + "*", SearchOption.AllDirectories).FirstOrDefault();
                        string lastFolder = string.Empty;
                        string[] folders = taskNoFinalFolder.Split('\\');
                        lastFolder = folders[folders.Length - 1];
                        taskFolderWithoutEmpNo = string.Format("{0}{1}\\{2}", rootPath, "{0}", lastFolder);
                        if (!string.IsNullOrEmpty(taskNoFinalFolder))
                        {
                            taskRootFolder = taskNoFinalFolder;
                            // 任务书和完成稿
                            if (fileCategory == "1" || fileCategory == "2")
                            {
                                returnFileName = Path.Combine(taskNoFinalFolder, folderName);
                            }
                            // 修改和修改完成
                            else if (fileCategory == "3" || fileCategory == "4")
                            {
                                returnFileName = Path.Combine(taskNoFinalFolder, modifyRecordFolderName, folderName);
                            }
                            // 疑问和疑问答复
                            else if (fileCategory == "5" || fileCategory == "6")
                            {
                                returnFileName = Path.Combine(taskNoFinalFolder, questionFolderName, folderName);
                            }
                            returnFolderName = returnFileName;
                        }
                    }
                    // 任务未分配
                    else
                    {
                        Project prj = new ProjectBLL().GetModel(projectId);
                        taskNo = prj.TASKNO;
                        string rootPath = Convert.ToString(ConfigurationManager.AppSettings["taskAllotmentPath"]);
                        string taskNoFinalFolder = Directory.GetDirectories(rootPath, taskNo + "*", SearchOption.TopDirectoryOnly).FirstOrDefault();
                        if (!string.IsNullOrEmpty(taskNoFinalFolder))
                        {
                            taskRootFolder = taskNoFinalFolder;
                            if (fileCategory == "1" || fileCategory == "2")
                            {
                                returnFileName = Path.Combine(taskNoFinalFolder, folderName);
                            }
                            else if (fileCategory == "3" || fileCategory == "4")
                            {
                                returnFileName = Path.Combine(taskNoFinalFolder, modifyRecordFolderName, folderName);
                            }
                            else if (fileCategory == "5" || fileCategory == "6")
                            {
                                returnFileName = Path.Combine(taskNoFinalFolder, questionFolderName, folderName);
                            }
                            returnFolderName = returnFileName;
                        }
                    }
                }
                if (!Directory.Exists(returnFileName))
                {
                    try
                    {
                        if (isCreateFolder)
                        {
                            Directory.CreateDirectory(returnFileName);
                        }
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
        #endregion
        #endregion

        #region 获取修改完成或者疑问答复的tab
        /// <summary>
        /// 获取修改完成或者疑问答复的tab
        /// </summary>
        /// <param name="context"></param>
        /// <param name="errCode"></param>
        /// <returns></returns>
        public DataSet GetReplyToTab(HttpContext context, out int errCode)
        {
            errCode = 0;
            string projectId = context.Request["projectId"];
            string categoryId = context.Request["categoryId"];
            //categoryId = GlobalConfig.dicMapForSubTab[categoryId].ToString();
            DataSet ds = this.GetReplayToTabList(projectId, categoryId);
            return ds;
        }
        #endregion

        #region 获取 fileCategory tab 列表
        /// <summary>
        /// 获取 fileCategory tab 列表
        /// </summary>
        /// <param name="context"></param>
        /// <param name="errCode"></param>
        /// <returns></returns>
        public List<FileCategory> GetCategories(HttpContext context, out int errCode)
        {
            errCode = 0;
            string projectId = context.Request["projectId"];
            string where = string.Format(" projectId = '{0}'", projectId);
            // 加入排序字段
            string orderBy = string.Format(" ORDER BY CLASSSORT, ORDERSORT, DIVISIONSORT");
            List<FileCategory> categories = this.GetModelList(where, orderBy);
            return categories;
        }

        public DataSet GetExpireDateByProjectId(string projectId)
        {
            return dal.GetExpireDateByProjectId(projectId);
        }
        #endregion

        public static bool IsDate(string strDate)
        {
            try
            {
                DateTime.Parse(strDate);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion  ExtensionMethod
    }
}
