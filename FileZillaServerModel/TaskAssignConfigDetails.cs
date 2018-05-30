using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel
{
    public class TaskAssignConfigDetails
    {
        public TaskAssignConfigDetails()
        { }
        #region Model
        private string _id;
        private string _employeeid;
        private string _specialtycategory;
        private decimal? _qualityscore;
        private int? _available;
        private int? _timemultiple;
        private int? _specialtytype;
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
        public string EMPLOYEEID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SPECIALTYCATEGORY
        {
            set { _specialtycategory = value; }
            get { return _specialtycategory; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? QUALITYSCORE
        {
            set { _qualityscore = value; }
            get { return _qualityscore; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? AVAILABLE
        {
            set { _available = value; }
            get { return _available; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TIMEMULTIPLE
        {
            set { _timemultiple = value; }
            get { return _timemultiple; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? SPECIALTYTYPE
        {
            set { _specialtytype = value; }
            get { return _specialtytype; }
        }
        #endregion Model
    }
}
