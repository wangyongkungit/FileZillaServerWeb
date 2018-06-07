using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel.Interface
{
    public class CategoryTabs
    {
        /// <summary>
        /// FileCategory Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// tab 的标题
        /// </summary>
        public string title { get; set; }

        /// <summary>
        /// tab 的描述
        /// </summary>
        public string description { get; set; }
    }
}
