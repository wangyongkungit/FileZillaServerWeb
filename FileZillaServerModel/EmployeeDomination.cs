using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel
{
    public class EmployeeDomination
    {
        public EmployeeDomination()
        { }
        #region Model
        private string _id;
        private string _parentemployeeid;
        private string _childemployeeid;
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
        public string PARENTEMPLOYEEID
        {
            set { _parentemployeeid = value; }
            get { return _parentemployeeid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CHILDEMPLOYEEID
        {
            set { _childemployeeid = value; }
            get { return _childemployeeid; }
        }
        #endregion Model
    }
}
