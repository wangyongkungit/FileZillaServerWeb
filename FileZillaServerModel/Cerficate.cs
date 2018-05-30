using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel
{
    public class Cerficate
    {
        public Cerficate()
		{}
		#region Model
        private string _id;
        private string _employeeid;
        private string _certificatename;
        private string _filepath;
        private string _description;
        private bool _ismain = false;
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
		public string CERTIFICATENAME
		{
			set{ _certificatename=value;}
			get{return _certificatename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FILEPATH
		{
			set{ _filepath=value;}
			get{return _filepath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DESCRIPTION
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool ISMAIN
		{
			set{ _ismain=value;}
			get{return _ismain;}
		}
		#endregion Model
    }
}
