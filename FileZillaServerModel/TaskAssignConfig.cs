﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel
{
    public class TaskAssignConfig
    {
        public TaskAssignConfig()
        { }
        #region Model
        private string _id;
        private string _employeeid;
        private decimal? _available;
        private decimal? _targetamount;
        private int? _timemultiple = 1;
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
        public decimal? AVAILABLE
        {
            set { _available = value; }
            get { return _available; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? TARGETAMOUNT
        {
            set { _targetamount = value; }
            get { return _targetamount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? TIMEMULTIPLE
        {
            set { _timemultiple = value; }
            get { return _timemultiple; }
        }
        #endregion Model
    }
}
