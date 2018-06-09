using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel.Interface
{
    public class FilesForTab
    {
        public string categoryId { get; set; }

        public string fileHistoryId { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string fileName { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public string fileExt { get; set; }

        public string description { get; set; }
    }
}
