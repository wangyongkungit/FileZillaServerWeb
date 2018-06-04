using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel
{
    /// <summary>
    /// FileOperationLog:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class FileOperationLog
    {
        public FileOperationLog()
        { }
        #region Model
        private string _id;
        private string _projectid;
        private string _employeeid;
        private string _filename;
        private int? _operatetype;
        private DateTime? _operatedate;
        private string _operateuser;
        private string _operatecontent;
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PROJECTID
        {
            set { _projectid = value; }
            get { return _projectid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EMPLOYEEID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FILENAME
        {
            set { _filename = value; }
            get { return _filename; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OPERATETYPE
        {
            set { _operatetype = value; }
            get { return _operatetype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OPERATEDATE
        {
            set { _operatedate = value; }
            get { return _operatedate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OPERATEUSER
        {
            set { _operateuser = value; }
            get { return _operateuser; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OPERATECONTENT
        {
            set { _operatecontent = value; }
            get { return _operatecontent; }
        }
        #endregion Model

    }
}
