using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerModel.Config
{
    public static class GlobalConfig
    {
        public static Dictionary<string, int> dicMap = new Dictionary<string, int>();

        static GlobalConfig()
        {
            dicMap.Add("1", 1);
            dicMap.Add("2", 2);
            dicMap.Add("3", 3);
            dicMap.Add("4", 3);
            dicMap.Add("5", 4);
            dicMap.Add("6", 4);
        }

    }
}
