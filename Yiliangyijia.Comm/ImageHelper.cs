using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yiliangyijia.Comm
{
    public class ImageHelper
    {
        /// <summary> 
        /// 为图片生成缩略图 by 何问起
        /// </summary> 
        /// <param name="phyPath">原图片的路径</param> 
        /// <param name="width">缩略图宽</param> 
        /// <param name="height">缩略图高</param> 
        /// <returns></returns> 
        public System.Drawing.Image GetHvtThumbnail(System.Drawing.Image image, int width, int height)
        {
            // 如果缩略图的宽度或者高度小于等于当前图片的宽度或者高度，则判断为不需要缩略，直接返回 image 对象
            if (image.Width <= width || image.Height <= height)
            {
                return image;
            }

            int min_width, min_height;

            // 如果高度为0，则根据长宽比计算出高度
            if (height == 0)
            {
                min_width = width;
                min_height = (int)((float)image.Height * ((float)width / (float)image.Width));
            }
            // 如果宽度为0，则根据长宽比计算出宽度
            else if (width == 0)
            {
                min_height = height;
                min_width = (int)((float)image.Width * ((float)height / (float)image.Height));
            }
            // 否则，设置为指定的宽度和高度
            else
            {
                min_width = width;
                min_height = height;
            }

            //代码是从开源项目HoverTreeCMS中获取的
            Bitmap m_hovertreeBmp = new Bitmap(min_width, min_height);
            //从Bitmap创建一个System.Drawing.Graphics 
            Graphics m_HvtGr = Graphics.FromImage(m_hovertreeBmp);
            //设置  
            m_HvtGr.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            //下面这个也设成高质量 
            m_HvtGr.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            //下面这个设成High 
            m_HvtGr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //把原始图像绘制成上面所设置宽高的缩小图 
            Rectangle rectDestination = new Rectangle(0, 0, min_width, min_height);

            m_HvtGr.DrawImage(image, rectDestination, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            //image.Save("", System.Drawing.Imaging.ImageFormat.Jpeg);

            return m_hovertreeBmp;
        }
    }
}
