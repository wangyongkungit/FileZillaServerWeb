using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yiliangyijia.Comm
{
    public class DateTimeHelper
    {
        /// <summary>    
        /// Unix时间戳转为C#格式时间    
        /// </summary>    
        /// <param name="timeStamp">Unix时间戳格式,例如1482115779</param>    
        /// <returns>C#格式时间</returns>    
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }


        /// <summary>    
        /// DateTime时间格式转换为Unix时间戳格式    
        /// </summary>    
        /// <param name="time"> DateTime时间格式</param>    
        /// <returns>Unix时间戳格式</returns>    
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        public static bool IsDate(string strDate)
        {
            try
            {
                DateTime.Parse(strDate);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string ChangeTime(DateTime dt)
        {
            TimeSpan ts = DateTime.Now - dt;
            if (ts.TotalDays > 365)
            {
                return Math.Floor(ts.TotalDays / 365) + "年前";
            }
            else if (ts.TotalDays > 30)
            {
                return Math.Floor(ts.TotalDays / 30) + "个月前";
            }
            else if (ts.TotalDays > 7)
            {
                return Math.Floor(ts.TotalDays / 7) + "周前";
            }
            else if (ts.TotalDays > 1)
            {
                return Math.Floor(ts.TotalDays) + "天前";
            }
            else if (ts.TotalHours > 1)
            {
                return Math.Floor(ts.TotalHours) + "小时前";
            }
            else if (ts.TotalMinutes > 1)
            {
                return Math.Floor(ts.TotalMinutes) + "分钟前";
            }
            //else if (ts.TotalSeconds > 10)
            //{
            //    return Math.Floor(ts.TotalSeconds) + "秒前";
            //}
            else
            {
                return "刚刚";
            }
        }
    }
}
