using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileZillaServerCommonHelper
{
    public class ZipHelper
    {
              /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="destinationZipFilePath"></param>
        public static void CreateZip(string sourceFilePath, string destinationZipFilePath)
        {
            if (sourceFilePath[sourceFilePath.Length - 1] != System.IO.Path.DirectorySeparatorChar)
                sourceFilePath += System.IO.Path.DirectorySeparatorChar;
            ZipOutputStream zipStream = new ZipOutputStream(File.Create(destinationZipFilePath));
            zipStream.SetLevel(6);  // 压缩级别 0-9
            CreateZipFiles(sourceFilePath, zipStream);
            zipStream.Finish();
            zipStream.Close();
        }
        /// <summary>
        /// 递归压缩文件
        /// </summary>
        /// <param name="sourceFilePath">待压缩的文件或文件夹路径</param>
        /// <param name="zipStream">打包结果的zip文件路径（类似 D:\WorkSpace\a.zip）,全路径包括文件名和.zip扩展名</param>
        /// <param name="staticFile"></param>
        private static void CreateZipFiles(string sourceFilePath, ZipOutputStream zipStream)
        {
            Crc32 crc = new Crc32();
            string[] filesArray = Directory.GetFileSystemEntries(sourceFilePath);
            foreach (string file in filesArray)
            {
                if (Directory.Exists(file))                     //如果当前是文件夹，递归
                {
                    CreateZipFiles(file, zipStream);
                }
                else                                            //如果是文件，开始压缩
                {
                    FileStream fileStream = File.OpenRead(file);
                    byte[] buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, buffer.Length);
                    string tempFile = file.Substring(sourceFilePath.LastIndexOf("\\") + 1);
                    ZipEntry entry = new ZipEntry(tempFile);
                    entry.DateTime = DateTime.Now;
                    entry.Size = fileStream.Length;
                    fileStream.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    zipStream.PutNextEntry(entry);
                    zipStream.Write(buffer, 0, buffer.Length);
                }
            }
        }


        public static void CreateZipFile(string filesPath, string zipFilePath)
        {
            if (!Directory.Exists(filesPath))
            {
                return;
            }
            ZipOutputStream stream = new ZipOutputStream(File.Create(zipFilePath));
            stream.SetLevel(0); // 压缩级别 0-9  
            byte[] buffer = new byte[4096]; //缓冲区大小  
            string[] filenames = Directory.GetFiles(filesPath, "*.*", SearchOption.AllDirectories);
            foreach (string file in filenames)
            {
                ZipEntry entry = new ZipEntry(file.Replace(filesPath, ""));
                entry.DateTime = DateTime.Now;
                stream.PutNextEntry(entry);
                using (FileStream fs = File.OpenRead(file))
                {
                    int sourceBytes;
                    do
                    {
                        sourceBytes = fs.Read(buffer, 0, buffer.Length);
                        stream.Write(buffer, 0, sourceBytes);
                    } while (sourceBytes > 0);
                }
            }
            stream.Finish();
            stream.Close();
        }

        //#region 解压缩包（单个文件解压缩）
        ///// <summary>/// 解压缩包（单个文件解压缩）/// </summary>
        ///// <param name="zipFileName">压缩文件</param>/// 
        ///// <param name="unzipFileName">解压缩文件</param>/// 
        ///// <param name="zipEnum">压缩算法枚举</param>///
        ///// <returns>压缩成功标志</returns>
        // public static bool UnZipFile(string zipFileName, string unzipFileName, ZipEnum zipEnum)
        //{
        //     bool flag = true; 
        //     try 
        //     {
        //         switch (zipEnum) 
        //         {
        //             case ZipEnum.BZIP2: 
        //             FileStream inStream = File.OpenRead(zipFileName); 
        //             FileStream outStream = File.Open(unzipFileName, FileMode.Create);
        //             ICSharpCode.SharpZipLib.BZip2.Decompress(inStream, outStream, true);
        //             break; 
        //             case ZipEnum.GZIP:
        //                 GZipInputStream zipFile = new GZipInputStream(File.OpenRead(zipFileName));
        //                 FileStream destFile = File.Open(unzipFileName, FileMode.Create);
        //                 int bufferSize = 2048 * 2; byte[] fileData = new byte[bufferSize]; 
        //                 while (bufferSize > 0) { bufferSize = zipFile.Read(fileData, 0, bufferSize); 
        //                     zipFile.Write(fileData, 0, bufferSize); } 
        //                     destFile.Close(); zipFile.Close(); 
        //                     break; default: break;
        //         } 
        //     } 
        //     catch 
        //     {
        //         flag = false; 
        //     }
        //     return flag; 
        // }
        //#endregion

        #region 解压缩包（将压缩包解压到指定目录）
        /// <summary>
        /// 解压缩包（将压缩包解压到指定目录）
        /// </summary>
        /// <param name="zipedFileName">压缩包名称</param>
        /// <param name="unZipDirectory">解压缩目录</param>
        /// <param name="password">密码</param>
        public static void UnZipFiles(string zipedFileName, string unZipDirectory, string password)
        {
            using (ZipInputStream zis = new ZipInputStream(File.Open(zipedFileName, FileMode.OpenOrCreate)))
            {
                if (!string.IsNullOrEmpty(password))
                {
                    zis.Password = password;//有加密文件的，可以设置密码解压
                } ZipEntry zipEntry;
                while ((zipEntry = zis.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(unZipDirectory);
                    string pathName = Path.GetDirectoryName(zipEntry.Name);
                    string fileName = Path.GetFileName(zipEntry.Name);
                    pathName = pathName.Replace(".", "$");
                    directoryName += "\\" + pathName;
                    if (!Directory.Exists(directoryName))
                    {
                        Directory.CreateDirectory(directoryName);
                    }
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        FileStream fs = File.Create(Path.Combine(directoryName, fileName));
                        int size = 2048;
                        byte[] bytes = new byte[2048];
                        while (true)
                        {
                            size = zis.Read(bytes, 0, bytes.Length);
                            if (size > 0)
                            {
                                fs.Write(bytes, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        fs.Close();
                    }
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// 压缩枚举
    /// </summary>
    public enum ZipEnum
    { 
        //压缩时间长，压缩率高  
        BZIP2,
        //压缩效率高，压缩率低
        GZIP
    }
}
