using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel
{
/// <summary>
	/// WithdrawDetails:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class WithdrawDetails
	{
		public WithdrawDetails()
		{}
		#region Model
		private string _id;
		private string _employeeid;
		private decimal? _withdrawamount=0.00M;
		private DateTime? _createdate;
        private bool _isconfirmed;
        private string _operateperson;
		private bool _isdeleted;
		/// <summary>
		/// 
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EMPLOYEEID
		{
			set{ _employeeid=value;}
			get{return _employeeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? WITHDRAWAMOUNT
		{
			set{ _withdrawamount=value;}
			get{return _withdrawamount;}
		}
		/// <summary>
		/// on update CURRENT_TIMESTAMP
		/// </summary>
		public DateTime? CREATEDATE
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool ISCONFIRMED
		{
			set{ _isconfirmed=value;}
			get{return _isconfirmed;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string OPERATEPERSON
        {
            set { _operateperson = value; }
            get { return _operateperson; }
        }
		/// <summary>
		/// 
		/// </summary>
		public bool ISDELETED
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}
		#endregion Model
    }
}
