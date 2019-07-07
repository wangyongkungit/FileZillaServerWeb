using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel.Interface
{
    public class TaskTrendInterface
    {
        public string ID { get; set; }

        public string ProjectId { get; set; }
        public DateTime CreateDate { get; set; }
        public string FriendlyDate { get; set; }
        public string TrendContent { get; set; }

        public bool ReadStatus { get; set; }
    }
}
