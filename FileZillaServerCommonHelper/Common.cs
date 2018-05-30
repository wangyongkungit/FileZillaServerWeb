using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FileZillaServerCommonHelper
{
    public class Common
    {
        public static int GetIndexOfCharAppearAmount(string str, char chrFind, int appearAmount)
        {
            int count = 0;
            char chrTemp;
            for (int i = 0; i < str.Length; i++)
            {
                chrTemp = str[i];
                if (chrTemp == chrFind)
                {
                    count++;
                }
            }
            int index=0;
            //int n = 0;
            for (int j = 0; j <= count; j++)
            {
                index = str.IndexOf(chrFind, index);
                if (j == appearAmount)
                {
                    break;
                }
                else
                {
                    index = str.IndexOf(chrFind, index + 1);
                }
            }
            return index;
        }

        public static int GetIndexOfCharAppearAmount2(string str, char charFind, int appearTimes)
        {
            Console.WriteLine("Please input your STRING:");
            //string a = Console.ReadLine();
            char c;
            //char find_c;
            //Console.WriteLine("Please input the CHARACTER in your string:");
            //find_c = Convert.ToChar(Console.ReadLine());
            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                c = str[i];
                if (c == charFind)//求得a中包含该字符的个数，以便遍历   
                {
                    count++;
                }
            }
            //Console.WriteLine("There are total {0} of char '{1}' in your input string.", count, charFind);
            int index = 0;
            //int n;//第n个find_c  
            //Console.WriteLine("Please input the SEQUENCE of the char '{0}' in your input string:", charFind);
            //n = Convert.ToInt32(Console.ReadLine());
            if (appearTimes > count)
            {
                //Console.WriteLine("Error:The Num must be less than or equal to {0}.", count);
                //Console.ReadKey();
            }
            for (int j = 1; j <= count; j++)
            {
                index = str.IndexOf(charFind, index);
                if (j == appearTimes)
                {
                    break;
                }
                else
                {
                    index = str.IndexOf(charFind, index + 1);
                }

            }
            //Console.WriteLine("The Index of the No.{0} char '{1}' in your input string is {2}.", appearTimes, charFind, index);  
            return index;
        }

        public static string TransformTimeSpan(TimeSpan timeSpan)
        {
            if (timeSpan.TotalHours > 24)
            {
                if (timeSpan.Hours == 0)
                {
                    return string.Format("{0}天", timeSpan.Days);
                }
                return string.Format("{0}天{1}小时", timeSpan.Days, timeSpan.Hours);
            }
            else
            {
                return string.Format("{0}小时", timeSpan.Hours);
            }
        }

        /// <summary>
        /// 将xml文档转成集合
        /// </summary>
        /// <param name="xmlstring">xml文档</param>
        /// <param name="SecretCode">最外层标签</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetNodelist(string xmlstring, string StrTopA)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlstring);
            XmlNode xmllist = xmldoc.SelectNodes(StrTopA)[0];
            foreach (XmlNode xn in xmllist)
            {
                dic.Add(xn.Name, xn.InnerText);
            }
            return dic;
        }

        ///<summary>
        /// 根据截取ipconfig /all命令的输出流获取网卡Mac
        ///</summary>
        ///<returns></returns>
        public static List<string> GetMacByIPConfig()
        {
            List<string> macs = new List<string>();

            ProcessStartInfo startInfo = new ProcessStartInfo("ipconfig", "/all");
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.CreateNoWindow = true;
            Process p = Process.Start(startInfo);
            //截取输出流
            StreamReader reader = p.StandardOutput;
            string line = reader.ReadLine();

            while (!reader.EndOfStream)
            {
                if (!string.IsNullOrEmpty(line))
                {
                    line = line.Trim();

                    if (line.StartsWith("Physical Address") || line.StartsWith("物理地址"))
                    {
                        line = line.Substring(line.IndexOf(':') + 1, line.Length - (line.IndexOf(':') + 1)).Trim();
                        macs.Add(line);
                    }
                }
                line = reader.ReadLine();
            }

            //等待程序执行完退出进程
            p.WaitForExit();
            p.Close();
            reader.Close();

            return macs;
        }
    }
}
