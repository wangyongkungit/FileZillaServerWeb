using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel
{
    [Serializable]
    public partial class ProjectSharing
    {
        public ProjectSharing()
        { }
        #region Model
        private string _id;
        private string _projectid;
        private string _finishedperson;
        private double? _proportion;
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
        public string FINISHEDPERSON
        {
            set { _finishedperson = value; }
            get { return _finishedperson; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? PROPORTION
        {
            set { _proportion = value; }
            get { return _proportion; }
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
