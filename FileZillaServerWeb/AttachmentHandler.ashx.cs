using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using FileZillaServerCommonHelper;
using System.Data;
using FileZillaServerBLL;
using System.Configuration;
using System.IO;
using System.Web.Services;
using System.Text;
using FileZillaServerModel;
using FileZillaServerDAL;

namespace FileZillaServerWeb
{
    /// <summary>
    /// 描    述：响应ajax请求，进行文件夹的压缩以及学生任务未分配目录的扫描
    /// 作    者：Wang Yongkun
    /// 创建时间：2016-03-08
    /// 修改历史：2017-03-08 wyk添加Compress方法
    ///           2017-03-16 wyk添加GetStudentTask()方法
    /// </summary>
    public class AttachmentHandler : IHttpHandler
    {
        private HttpContext context;

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            this.context = context;
            if (context.Request["RequestType"] == "AjaxRequest")
            {
                if (context.Request["Method"] == "GetProgress")
                {
                    //context.Response.Clear();
                    //context.Response.Write(this.GetProgress());
                    //context.Response.End();
                }
                //压缩
                else if (context.Request["Method"] == "Compress")
                {
                    string type = context.Request["type"];
                    string projectID = context.Request["ID"];
                    string finishedPerson = context.Request["FinishedPerson"];
                    LogHelper.WriteLine("finishedPerson: " + finishedPerson);
                    context.Response.Clear();
                    context.Response.Write(this.Compress(type, projectID, finishedPerson));
                    context.Response.End();
                }
                //获取任务列表
                else if (context.Request["Method"] == "GetStudentTask")
                {
                    context.Response.Clear();
                    context.Response.Write(this.GetStudentTask());
                    context.Response.End();
                }
                /*else
                {
                    this.DoWork();
                }*/
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /*/// <summary>
        /// 开始工作
        /// </summary>
        private void DoWork()
        {
            for (int i = 0; i < 100; i++)
            {
                // 记录进度
                // 实际应用中需要进一步控制（利用用户信息、cookies等），防止并发造成混乱
                this.context.Application["progress"] = i + 1;
                Random r = new Random();
                Thread.Sleep(r.Next(10, 100));
            }
            // 完成后释放资源
            this.context.Application["progress"] = null;
        }

        /// <summary>
        /// 查询进度
        /// </summary>
        /// <returns>进度</returns>
        private int GetProgress()
        {
            if (this.context.Application["progress"] != null)
            {
                return (int)this.context.Application["progress"];
            }
            else
            {
                return -1;
            }
        }*/

        /// <summary>
        /// 压缩
        /// </summary>
        /// <param name="type">任务类型</param>
        /// <param name="projectID">任务ID</param>
        /// <param name="finishedPerson">完成人ID</param>
        /// <returns></returns>
        public string Compress(string type, string projectID, string finishedPerson)
        {
            string result = "{\"result\":\"0\"}";
            try
            {
                string employeeRootPath = ConfigurationManager.AppSettings["employeePath"];
                //===================================== type为0，普通任务 =====================================
                if (type == "0")
                {
                    DataTable dt = new ProjectBLL().GetFinalScript(projectID, finishedPerson);

                    //string finishedPerson = context.Request["FinishedPerson"];

                    LogHelper.WriteLine("finishedPerson: " + finishedPerson);

                    //DataRow[] dr = dt.Select(string.Format("FINISHEDPERSON = '{0}'", finishedPerson));

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string employeeNo = dt.Rows[0]["EMPLOYEENO"].ToString();//员工编号
                        string taskNo = dt.Rows[0]["taskno"].ToString();//任务目录

                        LogHelper.WriteLine("employeeNo: " + employeeNo);
                        LogHelper.WriteLine("taskNo: " + taskNo);

                        string currentEmpPath = string.Format("{0}{1}", employeeRootPath, employeeNo);//员工目录
                        LogHelper.WriteLine("currentEmpPath: " + currentEmpPath);
                        //遍历员工目录，即找出各个任务的目录
                        foreach (DirectoryInfo taskFolder in new DirectoryInfo(currentEmpPath).GetDirectories())
                        {
                            LogHelper.WriteLine("如果目录名是以任务名打头的，   上面    taskNo :   " + taskNo);
                            //如果目录名是以任务名打头的，说明就是它了，因为员工会在原文件夹名后面加上乱七八糟的东西。如果前面他也敢动，那我也没办法了。
                            if (taskFolder.Name.StartsWith(taskNo))
                            {
                                foreach (DirectoryInfo taskFolderChild in new DirectoryInfo(taskFolder.FullName).GetDirectories())
                                {
                                    if (taskFolderChild.Name == "完成稿")
                                    {
                                        string destinationFileName = taskFolderChild.FullName + ".zip";
                                        if (!File.Exists(destinationFileName))
                                        {
                                            //ZipHelper.CreateZip(taskFolder.FullName, taskFolder.FullName + ".zip");
                                            ZipHelper.CreateZipFile(taskFolder.FullName + "\\" + taskFolderChild.Name, destinationFileName);
                                            result = "{\"result\":\"1\"}";
                                            break;
                                        }
                                        else
                                        {
                                            result = "{\"result\":\"2\"}";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //===================================== type为1，售后任务 =====================================
                else if (type == "1")
                {
                    DataTable dt = new ProjectBLL().GetProjectModifyByModifyID(projectID);
                    string employeeNo = dt.Rows[0]["employeeNo"].ToString();//员工编号
                    string taskNo = dt.Rows[0]["taskNo"].ToString();//任务目录
                    string currentEmpPath = string.Format("{0}{1}", employeeRootPath, employeeNo);//员工目录
                    //遍历员工目录，即找出各个任务的目录
                    foreach (DirectoryInfo taskFolder in new DirectoryInfo(currentEmpPath).GetDirectories())
                    {
                        //如果目录名是以任务名打头的，说明就是它了，因为员工会在原文件夹名后面加上乱七八糟的东西。如果前面他也敢动，那我也没办法了。
                        if (taskFolder.Name.StartsWith(taskNo))
                        {
                            //修改记录目录
                            string modifyRecordFolder = ConfigurationManager.AppSettings["modifyRecordFolderName"].ToString();
                            //遍历单个任务目录下的文件夹
                            foreach (DirectoryInfo taskFolderChild in taskFolder.GetDirectories())
                            {
                                //如果目录名是“修改记录”
                                if (taskFolderChild.Name == modifyRecordFolder)
                                {
                                    //进一步遍历每次修改记录目录
                                    foreach (DirectoryInfo modifyFolder in taskFolderChild.GetDirectories())
                                    {
                                        //每次修改记录产生的文件夹
                                        string modifyFolderName = modifyFolder.Name;
                                        //数据库中存储的修改记录文件夹
                                        string dtFolderName = dt.Rows[0]["folderName"].ToString();
                                        //如果目录名包含“完成”并且是当前售后任务打头的，那么就是它了
                                        //if (modifyFolderName.Contains("完成") && modifyFolderName.StartsWith(dtFolderName))
                                        LogHelper.WriteLine("modifyFolderName == dtFolderName   :" + modifyFolderName + "----" + dtFolderName);
                                        if (modifyFolderName == dtFolderName)
                                        {
                                            if (Directory.Exists(modifyFolder.FullName))
                                            {
                                                string destinationFileName = modifyFolder.FullName + ".zip";
                                                if (!File.Exists(destinationFileName))
                                                {
                                                    ZipHelper.CreateZipFile(modifyFolder.FullName, destinationFileName);
                                                    result = "{\"result\":\"1\"}";
                                                    break;
                                                }
                                                else
                                                {
                                                    result = "{\"result\":\"2\"}";
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                        //break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine("压缩失败！\r\n" + ex.Message);
                return "{\"result\":\"0\"}";
            }
            return result;
        }

        /// <summary>
        /// 获取学生类任务（毕业设计）
        /// </summary>
        /// <returns></returns>
        public string GetStudentTask()
        {
            try
            {
                //记录访问日志
                SystemLog log = new SystemLog();
                log.ID = Guid.NewGuid().ToString();
                log.EMPLOYEEID = "unknown";
                log.OPERATETYPE = null;
                log.OPERATECONTENT = "GetStudentTaskList";
                log.CREATEDATE = DateTime.Now;
                log.IPADDRESS = null;
                log.PHYSICALADDRESS = null;
                SystemLogDAL slogDal = new SystemLogDAL();
                slogDal.Add(log);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLine(ex.Message + ex.StackTrace);
            }

            //遍历学生类任务的分配目录，并以Json形式返回
            string studentTaskAllotmentPath = ConfigurationManager.AppSettings["studentTaskAllotmentPath"];
            DirectoryInfo dirInfo = new DirectoryInfo(studentTaskAllotmentPath);
            StringBuilder jsonResult = new StringBuilder("{\"listFolder\":[");
            if (dirInfo.Exists)
            {
                int i = 0;
                foreach (DirectoryInfo folders in dirInfo.GetDirectories())
                {
                    jsonResult.Append("{\"No\":\"" + (++i) + "\",\"Folder\":\"" + folders.Name + "\"},");
                }
            }
            jsonResult.ToString().TrimEnd(',');
            jsonResult.Append("]}");
            return jsonResult.ToString();
        }
    }
}