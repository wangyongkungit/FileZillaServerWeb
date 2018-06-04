using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yiliangyijia.Comm
{
    public class JsonResult<T>
    {
        public int Code { get; set; }

        public string Message { get; set; }

        public int Rows { get; set; }

        public List<T> Result { get; set; }
    }
}
