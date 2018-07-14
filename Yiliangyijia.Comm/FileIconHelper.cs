using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yiliangyijia.Comm
{
    public static class FileIconHelper
    {
        public static string GetFileIcon(string fileExtName)
        {
            string iconName = string.Empty;
            switch(fileExtName.ToLower())
            {
                case ".doc":
                    iconName = "doc-ico.png";
                    break;
                case ".docx":
                    iconName = "docx-ico.png";
                    break;
                case ".xls":
                    iconName = "xls-ico.png";
                    break;
                case ".xlsx":
                    iconName = "xlsx-ico.png";
                    break;
                case ".ppt":
                    iconName = "ppt-ico.png";
                    break;
                case ".pptx":
                    iconName = "pptx-ico.png";
                    break;
                case ".txt":
                    iconName = "txt-ico.png";
                    break;
                case ".flv":
                    iconName = "-ico.png";
                    break;
                case ".xml":
                    iconName = "xml-ico.png";
                    break;
                case ".zip":
                    iconName = "zip-ico.png";
                    break;
                case ".rar":
                    iconName = "rar-ico.png";
                    break;
                case ".pdf":
                    iconName = "pdf-ico.png";
                    break;
                case ".7z":
                    iconName = "zip-ico.png";
                    break;
                case ".jpg":
                case ".png":
                case ".gif":
                    iconName = "jpg-ico.png";
                    break;
                default:
                    iconName = "unknown-ico.png";
                    break;
            }
            return iconName;
        }
    }
}
