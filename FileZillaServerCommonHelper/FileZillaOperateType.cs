using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerCommonHelper
{
    public enum FileZillaOperateType
    {
        Default=0,
        Login = 1,
        Logout = 2,
        View=11,
        Upload = 21,
        Download = 22,
        Delete = 21,
    };
}
