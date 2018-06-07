using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel.Interface
{
    public class SubFileOperateLog
    {
        public int operateDate { get; set; }

        public string operateUser { get; set; }

        public string operateContent { get; set; }
    }
}
