using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel
{
    public partial class Project
    {
        public Project()
        { }
        #region Model
        private string _id;
        private string _taskno;
        private string _projectname;
        private DateTime? _orderdate;
        private DateTime? _expiredate;
        private decimal? _timeneeded;
        private string _shop;
        private double? _orderamount;
        private decimal? _proportion;
        private string _valuatemode;
        private string _province;
        private string _modelingsoftware;
        private string _valuatesoftware;
        private string _specialtycategory;
        private string _specialtycategoryminor;
        private string _wangwangname;
        private string _email;
        private decimal? _floors;
        private double? _constructionarea;
        private string _structureform;
        private string _buildingtype;
        private string _transactionstatus;
        private double? _refund;
        private string _mobilephone;
        private string _qq;
        private string _paymentmethod;
        private string _extrarequirement;
        private string _referrer;
        private decimal? _cashback;
        private string _remarks;
        private string _assignmentbook;
        private decimal? _isfinished=0M;
        private string _finishedperson;
        private string _enteringperson;
        private DateTime? _createdate;
        private decimal? _iscreatedfolder;
        private int? _materialisupload;
        private decimal? _isdeleted;
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
        public string TASKNO
        {
            set { _taskno = value; }
            get { return _taskno; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PROJECTNAME
        {
            set { _projectname = value; }
            get { return _projectname; }
        }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>
        public DateTime? ORDERDATE
        {
            set { _orderdate = value; }
            get { return _orderdate; }
        }
        /// <summary>
        /// on update CURRENT_TIMESTAMP
        /// </summary>
        public DateTime? EXPIREDATE
        {
            set { _expiredate = value; }
            get { return _expiredate; }
        }
        /// <summary>
        /// 所需时间
        /// </summary>
        public decimal? TIMENEEDED
        {
            set { _timeneeded = value; }
            get { return _timeneeded; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SHOP
        {
            set { _shop = value; }
            get { return _shop; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? ORDERAMOUNT
        {
            set { _orderamount = value; }
            get { return _orderamount; }
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
        public string VALUATEMODE
        {
            set { _valuatemode = value; }
            get { return _valuatemode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PROVINCE
        {
            set { _province = value; }
            get { return _province; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MODELINGSOFTWARE
        {
            set { _modelingsoftware = value; }
            get { return _modelingsoftware; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string VALUATESOFTWARE
        {
            set { _valuatesoftware = value; }
            get { return _valuatesoftware; }
        }
        /// <summary>
        /// 专业类别
        /// </summary>
        public string SPECIALTYCATEGORY
        {
            get { return _specialtycategory; }
            set { _specialtycategory = value; }
        }
        /// <summary>
        /// 专业类别
        /// </summary>
        public string SPECIALTYCATEGORYMINOR
        {
            get { return _specialtycategoryminor; }
            set { _specialtycategoryminor = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WANGWANGNAME
        {
            set { _wangwangname = value; }
            get { return _wangwangname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EMAIL
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? FLOORS
        {
            set { _floors = value; }
            get { return _floors; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? CONSTRUCTIONAREA
        {
            set { _constructionarea = value; }
            get { return _constructionarea; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STRUCTUREFORM
        {
            set { _structureform = value; }
            get { return _structureform; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BUILDINGTYPE
        {
            set { _buildingtype = value; }
            get { return _buildingtype; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TRANSACTIONSTATUS
        {
            set { _transactionstatus = value; }
            get { return _transactionstatus; }
        }
        /// <summary>
        /// 
        /// </summary>
        public double? REFUND
        {
            set { _refund = value; }
            get { return _refund; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MOBILEPHONE
        {
            set { _mobilephone = value; }
            get { return _mobilephone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string QQ
        {
            set { _qq = value; }
            get { return _qq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PAYMENTMETHOD
        {
            set { _paymentmethod = value; }
            get { return _paymentmethod; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EXTRAREQUIREMENT
        {
            set { _extrarequirement = value; }
            get { return _extrarequirement; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string REMARKS
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ASSIGNMENTBOOK
        {
            set { _assignmentbook = value; }
            get { return _assignmentbook; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string REFERRER
        {
            set { _referrer = value; }
            get { return _referrer; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? CASHBACK
        {
            set { _cashback = value; }
            get { return _cashback; }
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
        /// 完成人
        /// </summary>
        public string FINISHEDPERSON
        {
            set { _finishedperson = value; }
            get { return _finishedperson; }
        }
        /// <summary>
        /// 录入人
        /// </summary>
        public string ENTERINGPERSON
        {
            set { _enteringperson = value; }
            get { return _enteringperson; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? CREATEDATE
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ISCREATEDFOLDER
        {
            set { _iscreatedfolder = value; }
            get { return _iscreatedfolder; }
        }
        /// <summary>
        /// 资料是否已上传
        /// </summary>
        public int? MATERIALISUPLOAD
        {
            set { _materialisupload = value; }
            get { return _materialisupload; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ISDELETED
        {
            set { _isdeleted = value; }
            get { return _isdeleted; }
        }
        #endregion Model
    }
}
