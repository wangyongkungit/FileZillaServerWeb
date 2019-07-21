using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerProfile
{
    public class Menu
    {
        public string Name { get; set; }

        public string Path { get; set; }

        public string ParentID { get; set; }

        public string Remarks { get; set; }

        public bool Available { get; set; }
    }
}
