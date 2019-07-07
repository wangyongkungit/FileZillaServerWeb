using FileZillaServerDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerBLL
{
    public class ConfigureBLL
    {
        ConfigureDAL cDal = new ConfigureDAL();

        /// <summary>
        /// 根据配置名称获取配置
        /// </summary>
        /// <returns></returns>
        public DataTable GetConfig()
        {
            return cDal.GetConfig();
        }

        /// <summary>
        /// 根据配置名称获取配置
        /// </summary>
        /// <param name="configType">配置名称</param>
        /// <returns></returns>
        public DataTable GetConfig(string configType)
        {
            return cDal.GetConfig(configType);
        }

        /// <summary>
        /// 获取默认提成比例
        /// </summary>
        /// <returns></returns>
        public DataTable GetDefaultProportionConfig()
        {
            return cDal.GetDefaultProportionConfig();
        }

        /// <summary>
        /// 获取省份
        /// </summary>
        /// <returns></returns>
        public DataTable GetProvince()
        {
            return cDal.GetProvince();
        }

        /// <summary>
        /// 获取任务目标值
        /// </summary>
        /// <returns></returns>
        public DataTable GetTaskObjectiveValue()
        {
            return cDal.GetTaskObjectiveValue();
        }

        /// <summary>
        /// 更新目标值
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="d_value"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateObjectiveValue(int objValue, int d_value, string id)
        {
            return cDal.UpdateObjectiveValue(objValue, d_value, id);
        }

        /// <summary>
        /// 更新默认任务提成比例
        /// </summary>
        /// <returns></returns>
        public bool UpdateDefaultProportion(decimal newProportion)
        {
            return cDal.UpdateDefaultProportion(newProportion);
        }

        public bool InsertShop(string shopId, string shopName, string accessKey, string secretKey)
        {
            return cDal.InsertShop(shopId, shopName, accessKey, secretKey);
        }

        public DataTable GetShopKeys()
        {
            return cDal.GetShopKeys();
        }

        public bool UpdateShopKeys(string Id, string accessKey, string secretKey)
        {
            return cDal.UpdateShopKeys(Id, accessKey, secretKey);
        }

        public bool DisableShop(string ID)
        {
            return cDal.DisableShop(ID);
        }
    }
}
