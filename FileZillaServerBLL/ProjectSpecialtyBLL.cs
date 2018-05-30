using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileZillaServerDAL;

namespace FileZillaServerBLL
{
    public class ProjectSpecialtyBLL
    {
        ProjectSpecialtyDAL pspDal = new ProjectSpecialtyDAL();
        /// <summary>
        /// 获得任务专业
        /// </summary>
        public DataSet GetSpecialtyInnerJoinProject(string projectID, string type)
        {
            return pspDal.GetSpecialtyInnerJoinProject(projectID, type);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(string projectID, string specialtyID, string type)
        {
            string[] specialtys = specialtyID.Split(',');
            bool delFlag = pspDal.DeleteByProjectID(projectID, type);
            int succCount = 0;
            for (int i = 0; i < specialtys.Length; i++)
            {
                //if (!pspDal.GetSpecialtyExistsByProjectIDAndSpecialtyCate(projectID, specialtys[i]))
                //{
                    pspDal.Add(projectID, specialtys[i], type);
                    succCount++;
                //}
            }
            if (specialtys.Length == succCount)
            {
                return true;
            }
            return false;
        }
    }
}
