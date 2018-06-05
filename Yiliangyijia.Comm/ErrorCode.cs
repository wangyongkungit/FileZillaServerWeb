using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yiliangyijia.Comm
{
    public class ErrorCode
    {
        public static string GetCodeMessage(int code)
        {
            string msg = string.Empty;
            switch (code)
            {
                case 0:
                    msg = "调用成功";
                    break;
                case 1:
                    msg = "调用失败";
                    break;
                case 2001:
                    msg = "不合法的字符值";
                    break;
                case 2002:
                    msg = "请求的方法不存在";
                    break;
                case 2003:
                    msg = "请求的参数不正确";
                    break;
                case 2004:
                    msg = "缺少参数";
                    break;
                case 2011:
                    msg = "无效的projectId";
                    break;
                case 2016:
                    msg = "无效的faileCategoryId";
                    break;
                case 2021:
                    msg = "无效的fileHistory.parentId";
                    break;
                case 2026:
                    msg = "无效的fileHistoryId";
                    break;
                case 2101:
                    msg = "AddFile数据库记录添加失败";
                    break;
                case 3001:
                    msg = "无效的uesrId";
                    break;
                case 3002:
                    msg = "无效的roleId";
                    break;
                case 3021:
                    msg = "无效的deptId";
                    break;
                case 6001:
                    msg = "文件不存在";
                    break;
                case 6002:
                    msg = "不合法的文件类型";
                    break;
                case 6003:
                    msg = "不合法的文件大小";
                    break;
                case 6004:
                    msg = "文件复制失败";
                    break;
                case 6005:
                    msg = "文件粘贴失败";
                    break;
                case 6006:
                    msg = "文件删除失败";
                    break;
                case 6011:
                    msg = "文件上传异常";
                    break;
                case 6021:
                    msg = "文件下载异常";
                    break;
                case 6501:
                    msg = "目录不存在";
                    break;
                case 6502:
                    msg = "没有目录操作权限";
                    break;
                case 6503:
                    msg = "文件复制失败";
                    break;
                case 6504:
                    msg = "目录移动失败";
                    break;
                case 6505:
                    msg = "目录删除失败";
                    break;
                default:
                    break;
            }
            return msg;
        }
    }
}
