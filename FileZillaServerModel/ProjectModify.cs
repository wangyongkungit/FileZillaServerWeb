using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel
{
    /// <summary>
    /// ProjectModify:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class ProjectModify
    {
        public ProjectModify()
        { }
        #region Model
        private string _id;
        private string _projectid;
        private string _foldername;
        private string _folderfullname;
        private decimal? _isuploadattach;
        private decimal? _isfinished = 0M;
        private decimal? _reviewstatus;
        private DateTime? _createdate;
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
        public string FOLDERNAME
        {
            set { _foldername = value; }
            get { return _foldername; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FOLDERFULLNAME
        {
            set { _folderfullname = value; }
            get { return _folderfullname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ISUPLOADATTACH
        {
            set { _isuploadattach = value; }
            get { return _isuploadattach; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ISFINISHED
        {
            set { _isfinished = value; }
            get { return _isfinished; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? REVIEWSTATUS
        {
            set { _reviewstatus = value; }
            get { return _reviewstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? CREATEDATE
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        #endregion Model
    }
}
