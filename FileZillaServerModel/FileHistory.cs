using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel
{
    /// <summary>
    /// FileHistory:文件历史实体类
    /// </summary>
    [Serializable]
    public partial class FileHistory
    {
        public FileHistory()
        { }
        #region Model
        private string _id;
        private string _parentid;
        private string _filename;
        private string _fileextension;
        private string _filefullname;
        private string _description;
        private DateTime? _operatedate;
        private string _operateuser;
        private bool _isdeleted = false;
        /// <summary>
        /// 主键
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 父级目录
        /// </summary>
        public string PARENTID
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 文件名（包含扩展名但不包含路径）
        /// </summary>
        public string FILENAME
        {
            set { _filename = value; }
            get { return _filename; }
        }
        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string FILEEXTENSION
        {
            set { _fileextension = value; }
            get { return _fileextension; }
        }
        /// <summary>
        /// 包含文件路径的文件名
        /// </summary>
        public string FILEFULLNAME
        {
            set { _filefullname = value; }
            get { return _filefullname; }
        }
        /// <summary>
        /// 描述，针对该文件的描述信息
        /// </summary>
        public string DESCRIPTION
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime? OPERATEDATE
        {
            set { _operatedate = value; }
            get { return _operatedate; }
        }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public string OPERATEUSER
        {
            set { _operateuser = value; }
            get { return _operateuser; }
        }
        /// <summary>
        /// 是否删除（false：否，true：是）
        /// </summary>
        public bool ISDELETED
        {
            set { _isdeleted = value; }
            get { return _isdeleted; }
        }
        #endregion Model

    }
}
