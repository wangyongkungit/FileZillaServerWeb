using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel
{
    /// <summary>
	/// Salary:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Salary
    {
        public Salary()
        { }
        #region Model
        private string _id;
        private string _employeeid;
        private DateTime? _monthdate;
        private decimal? _basesalary;
        private decimal? _contractsalary;
        private decimal? _piecewage;
        private decimal? _piecepenalty;
        private decimal? _fullattend;
        private decimal? _overtimewage;
        private decimal? _agewage;
        private decimal? _accommodation_allowance;
        private decimal? _meal_allowance;
        private decimal? _attendancepenalty;
        private decimal? _otherwage;
        private decimal? _totalorderamount = 0.00M;
        private decimal? _socialsecurity_individual;
        private decimal? _socialsecurity_company;
        private decimal? _housingprovidentfund_individual;
        private decimal? _housingprovidentfund_company;
        private decimal? _totalincome = 0.00M;
        private decimal? _realsalary;
        private decimal? _realsalary_company;
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 员工ID
        /// </summary>
        public string EMPLOYEEID
        {
            set { _employeeid = value; }
            get { return _employeeid; }
        }
        /// <summary>
        /// 工资年月
        /// </summary>
        public DateTime? MONTHDATE
        {
            set { _monthdate = value; }
            get { return _monthdate; }
        }
        /// <summary>
        /// 基本工资
        /// </summary>
        public decimal? BASESALARY
        {
            set { _basesalary = value; }
            get { return _basesalary; }
        }
        /// <summary>
        /// 合同工资
        /// </summary>
        public decimal? CONTRACTSALARY
        {
            set { _contractsalary = value; }
            get { return _contractsalary; }
        }
        /// <summary>
        /// 计件工资
        /// </summary>
        public decimal? PIECEWAGE
        {
            set { _piecewage = value; }
            get { return _piecewage; }
        }
        /// <summary>
        /// 计件扣款
        /// </summary>
        public decimal? PIECEPENALTY
        {
            set { _piecepenalty = value; }
            get { return _piecepenalty; }
        }
        /// <summary>
        /// 全勤工资
        /// </summary>
        public decimal? FULLATTEND
        {
            set { _fullattend = value; }
            get { return _fullattend; }
        }
        /// <summary>
        /// 其他收入
        /// </summary>
        public decimal? OVERTIMEWAGE
        {
            set { _overtimewage = value; }
            get { return _overtimewage; }
        }
        /// <summary>
        /// 工龄工资
        /// </summary>
        public decimal? AGEWAGE
        {
            set { _agewage = value; }
            get { return _agewage; }
        }
        /// <summary>
        /// 住宿补贴
        /// </summary>
        public decimal? ACCOMMODATION_ALLOWANCE
        {
            set { _accommodation_allowance = value; }
            get { return _accommodation_allowance; }
        }
        /// <summary>
        /// 伙食补贴
        /// </summary>
        public decimal? MEAL_ALLOWANCE
        {
            set { _meal_allowance = value; }
            get { return _meal_allowance; }
        }
        /// <summary>
        /// 考勤扣款
        /// </summary>
        public decimal? ATTENDANCEPENALTY
        {
            set { _attendancepenalty = value; }
            get { return _attendancepenalty; }
        }
        /// <summary>
        /// 其他收入
        /// </summary>
        public decimal? OTHERWAGE
        {
            set { _otherwage = value; }
            get { return _otherwage; }
        }
        /// <summary>
        /// 订单总额
        /// </summary>
        public decimal? TOTALORDERAMOUNT
        {
            set { _totalorderamount = value; }
            get { return _totalorderamount; }
        }
        /// <summary>
        /// 个人社保
        /// </summary>
        public decimal? SOCIALSECURITY_INDIVIDUAL
        {
            set { _socialsecurity_individual = value; }
            get { return _socialsecurity_individual; }
        }
        /// <summary>
        /// 企业社保
        /// </summary>
        public decimal? SOCIALSECURITY_COMPANY
        {
            set { _socialsecurity_company = value; }
            get { return _socialsecurity_company; }
        }
        /// <summary>
        /// 个人公积金
        /// </summary>
        public decimal? HOUSINGPROVIDENTFUND_INDIVIDUAL
        {
            set { _housingprovidentfund_individual = value; }
            get { return _housingprovidentfund_individual; }
        }
        /// <summary>
        /// 企业公积金
        /// </summary>
        public decimal? HOUSINGPROVIDENTFUND_COMPANY
        {
            set { _housingprovidentfund_company = value; }
            get { return _housingprovidentfund_company; }
        }
        /// <summary>
        /// 总收入
        /// </summary>
        public decimal? TOTALINCOME
        {
            set { _totalincome = value; }
            get { return _totalincome; }
        }
        /// <summary>
        /// 实发工资（个人）
        /// </summary>
        public decimal? REALSALARY
        {
            set { _realsalary = value; }
            get { return _realsalary; }
        }
        /// <summary>
        /// 实发工资（企业）
        /// </summary>
        public decimal? REALSALARY_COMPANY
        {
            set { _realsalary_company = value; }
            get { return _realsalary_company; }
        }
        #endregion Model
    }
}
