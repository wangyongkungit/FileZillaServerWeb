using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel
{
    /// <summary>
    /// SalaryConfig:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class SalaryConfig
    {
        public SalaryConfig()
        { }
        #region Model
        private string _id;
        private string _employeeid;
        private decimal? _basesalary;
        private decimal? _agewage;
        private decimal? _accommodation_allowance;
        private decimal? _meal_allowance;
        private decimal? _otherwage;
        private decimal? _socialsecurity_individual;
        private decimal? _socialsecurity_company;
        private decimal? _housingprovidentfund_individual;
        private decimal? _housingprovidentfund_company;
        private decimal? _payoff_type;
        private decimal? _commission;
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
        public decimal? BASESALARY
        {
            set { _basesalary = value; }
            get { return _basesalary; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? AGEWAGE
        {
            set { _agewage = value; }
            get { return _agewage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ACCOMMODATION_ALLOWANCE
        {
            set { _accommodation_allowance = value; }
            get { return _accommodation_allowance; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? MEAL_ALLOWANCE
        {
            set { _meal_allowance = value; }
            get { return _meal_allowance; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? OTHERWAGE
        {
            set { _otherwage = value; }
            get { return _otherwage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SOCIALSECURITY_INDIVIDUAL
        {
            set { _socialsecurity_individual = value; }
            get { return _socialsecurity_individual; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? SOCIALSECURITY_COMPANY
        {
            set { _socialsecurity_company = value; }
            get { return _socialsecurity_company; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? HOUSINGPROVIDENTFUND_INDIVIDUAL
        {
            set { _housingprovidentfund_individual = value; }
            get { return _housingprovidentfund_individual; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? HOUSINGPROVIDENTFUND_COMPANY
        {
            set { _housingprovidentfund_company = value; }
            get { return _housingprovidentfund_company; }
        }
        /// <summary>
        /// 工资发放方式：0，客服；1，庆元；2，办公楼
        /// </summary>
        public decimal? PAYOFF_TYPE
        {
            set { _payoff_type = value; }
            get { return _payoff_type; }
        }
        /// <summary>
        /// 任务提成
        /// </summary>
        public decimal? COMMISSION
        {
            set { _commission = value; }
            get { return _commission; }
        }
        #endregion Model

    }
}
