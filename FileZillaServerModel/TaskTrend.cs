using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel
{
    /// <summary>
    /// TaskTrend:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class TaskTrend
    {
        public TaskTrend()
        { }
        #region Model
        private string _id;
        private string _projectid;
        private string _employeeid;
        private string _description;
        private DateTime? _createdate;
        private bool _readstatus;
        private int? _type;
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
        public string DESCRIPTION
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>
        public DateTime? CREATEDATE
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool READSTATUS
        {
            set { _readstatus = value; }
            get { return _readstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TYPE
        {
            set { _type = value; }
            get { return _type; }
        }
        #endregion Model

    }
}
