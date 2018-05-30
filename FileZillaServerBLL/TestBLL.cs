using FileZillaServerDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerBLL
{
    public class TestBLL
    {
        TestDAL tDal = new TestDAL();

        public bool Add()
        {
            return tDal.Add();
        }
    }
}
