using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel
{
    public class EmployeeRole
    {
        public EmployeeRole()
        { }
        #region Model
        private string _id;
        private string _employeeid;
        private string _roleid;
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
        public string ROLEID
        {
            set { _roleid = value; }
            get { return _roleid; }
        }
        #endregion Model
    }
}
