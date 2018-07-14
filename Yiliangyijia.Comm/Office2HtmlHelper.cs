using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MyWord = Microsoft.Office.Interop.Word;

namespace Yiliangyijia.Comm
{
    public class Office2HtmlHelper
    {
        /// <summary>
        /// Word转成Html
        /// </summary>
        /// <param name="path">要转换的文档的路径</param>
        /// <param name="savePath">转换成html的保存路径</param>
        /// <param name="wordFileName">转换成html的文件名字</param>
        public static void Word2Html(string path, string savePath, string wordFileName)
        {
            MyWord.Application word = new MyWord.Application();
            Type wordType = word.GetType();
            MyWord.Documents docs = word.Documents;
            Type docsType = docs.GetType();
            MyWord.Document doc = (MyWord.Document)docsType.InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, docs, new Object[] { (object)path, true, true });
            try
            {
                Type docType = doc.GetType();
                string strSaveFileName = System.IO.Path.Combine(savePath, $"{wordFileName}.html");
                object saveFileName = (object)strSaveFileName;
                docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, doc, new object[] { saveFileName, MyWord.WdSaveFormat.wdFormatFilteredHTML });
                docType.InvokeMember("Close", System.Reflection.BindingFlags.InvokeMethod, null, doc, null);
                wordType.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, word, null);
            }
            catch
            { }
            finally
            {
                doc = null;
                docs = null;
                word = null;
            }
        }
        /// <summary>
        /// Excel转成Html
        /// </summary>
        /// <param name="path">要转换的文档的路径</param>
        /// <param name="savePath">转换成html的保存路径</param>
        /// <param name="wordFileName">转换成html的文件名字</param>
        public static void Excel2Html(string path, string savePath, string wordFileName)
        {
            string str = string.Empty;
            Microsoft.Office.Interop.Excel.Application repExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = null;
            Microsoft.Office.Interop.Excel.Worksheet worksheet = null;
            try
            {
                workbook = repExcel.Application.Workbooks.Open(path, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];
                object htmlFile = System.IO.Path.Combine(savePath, $"{wordFileName}.html");
                object ofmt = Microsoft.Office.Interop.Excel.XlFileFormat.xlHtml;
                workbook.SaveAs(htmlFile, ofmt, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                object osave = false;
                workbook.Close(osave, Type.Missing, Type.Missing);
                repExcel.Quit();
            }
            catch
            {

            }
            finally
            {
                if (repExcel != null)
                {
                    Kill(new IntPtr(repExcel.Hwnd), "excel");
                }
                worksheet = null;
                workbook = null;
                repExcel = null;
            }
        }
        /// <summary>
        /// ppt转成Html
        /// </summary>
        /// <param name="path">要转换的文档的路径</param>
        /// <param name="savePath">转换成html的保存路径</param>
        /// <param name="wordFileName">转换成html的文件名字</param>
        public static void PPT2Html(string path, string savePath, string wordFileName)
        {
            //Microsoft.Office.Interop.PowerPoint.Application ppApp = new Microsoft.Office.Interop.PowerPoint.Application();
            //string strSourceFile = path;
            //string strDestinationFile = savePath + wordFileName + ".html";
            //Microsoft.Office.Interop.PowerPoint.Presentation prsPres = ppApp.Presentations.Open(strSourceFile, Microsoft.Office.Core.MsoTriState.msoTrue, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoFalse);

            //prsPres.SaveAs(strDestinationFile, Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsHTML, MsoTriState.msoTrue);
            //prsPres.Close();
            //ppApp.Quit();
        }

        #region 结束Excel进程
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

        /// <summary>
        /// 强制关闭当前Excel进程
        /// </summary>
        public static void Kill(IntPtr intPtr, string processName)
        {
            try
            {
                Process[] ps = Process.GetProcesses();
                int processID = 0;
                GetWindowThreadProcessId(intPtr, out processID); //得到本进程唯一标志k
                foreach (Process p in ps)
                {
                    if (p.ProcessName.ToLower().Equals(processName))
                    {
                        if (p.Id == processID)
                        {
                            p.Kill();
                        }
                    }
                }
            }
            catch
            { }
        }
        #endregion
    }
}