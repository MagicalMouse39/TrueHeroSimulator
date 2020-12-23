using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrueHeroSimulator
{
    internal static class Extensions
    {
        public static Bitmap Resize(this Bitmap image, int width, int height)
        {
            var bmp = new Bitmap(width, height);
            var g = Graphics.FromImage(bmp);
            g.InterpolationMode = InterpolationMode.High;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            float scale = Math.Min(width / image.Width, height / image.Height);
            int scaleWidth = (int)(image.Width * scale);
            int scaleHeight = (int)(image.Height * scale);
            g.FillRectangle(Brushes.Black, new RectangleF(0, 0, width, height));
            g.DrawImage(image, (width - scaleWidth) / 2, (height - scaleHeight) / 2, scaleWidth, scaleHeight);
            return bmp;
        }

        public static GraphicsPath Round(this Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();

            if (radius == 0)
            {
                path.AddRectangle(bounds);
                return path;
            }

            // top left arc  
            path.AddArc(arc, 180, 90);

            // top right arc  
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);

            // bottom right arc  
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // bottom left arc 
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);

            path.CloseFigure();
            return path;
        }
    }
}