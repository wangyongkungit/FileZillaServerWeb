using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel
{
    /// <summary>
    /// EmployeeProportion:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class EmployeeProportion
    {
        public EmployeeProportion()
        { }
        #region Model
        private string _id;
        private string _employeeid;
        private decimal? _proportion;
        private string _parentemployeeid;
        private bool _isbranchleader = false;
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
        public decimal? PROPORTION
        {
            set { _proportion = value; }
            get { return _proportion; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PARENTEMPLOYEEID
        {
            set { _parentemployeeid = value; }
            get { return _parentemployeeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool ISBRANCHLEADER
        {
            set { _isbranchleader = value; }
            get { return _isbranchleader; }
        }
        #endregion Model

    }
}
