using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel
{
    /// <summary>
    /// attendance:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Attendance
    {
        public Attendance()
        { }
        #region Model
        private string _id;
        private string _dingtalkid;
        private string _groupid;
        private string _planid;
        private string _recordid;
        private DateTime? _workdate;
        private string _d_userid;
        private string _userid;
        private string _checktype;
        private string _timeresult;
        private string _locationresult;
        private string _approveid;
        private DateTime? _basechecktime;
        private DateTime? _userchecktime;
        private decimal? _deductmoney;
        private decimal? _score;
        private decimal? _deviationminutes;
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
        public string DINGTALKID
        {
            set { _dingtalkid = value; }
            get { return _dingtalkid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GROUPID
        {
            set { _groupid = value; }
            get { return _groupid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PLANID
        {
            set { _planid = value; }
            get { return _planid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RECORDID
        {
            set { _recordid = value; }
            get { return _recordid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? WORKDATE
        {
            set { _workdate = value; }
            get { return _workdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string D_USERID
        {
            set { _d_userid = value; }
            get { return _d_userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string USERID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CHECKTYPE
        {
            set { _checktype = value; }
            get { return _checktype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TIMERESULT
        {
            set { _timeresult = value; }
            get { return _timeresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LOCATIONRESULT
        {
            set { _locationresult = value; }
            get { return _locationresult; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string APPROVEID
        {
            set { _approveid = value; }
            get { return _approveid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? BASECHECKTIME
        {
            set { _basechecktime = value; }
            get { return _basechecktime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? USERCHECKTIME
        {
            set { _userchecktime = value; }
            get { return _userchecktime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? DEDUCTMONEY
        {
            set { _deductmoney = value; }
            get { return _deductmoney; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SCORE
        {
            set { _score = value; }
            get { return _score; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? DEVIATIONMINUTES
        {
            set { _deviationminutes = value; }
            get { return _deviationminutes; }
        }
        #endregion Model
    }
}
