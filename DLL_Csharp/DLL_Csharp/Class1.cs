using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RotateHL
{
    public static class AddClass
    {
        public static int RotateX(int x,int y, double degrees,
                                      int offsetX, int offsetY)
        {
            return (int)(Math.Round(Math.Cos(degrees)*(x - offsetX) - Math.Sin(degrees)*(y - offsetY) + offsetX));
        }

        public static int RotateY(int x, int y, double degrees,
                              int offsetX, int offsetY)
        {
            return (int)(Math.Round(Math.Sin(degrees) * (x - offsetX) + Math.Cos(degrees) * (y - offsetY) + offsetY));
        }

        public static Bitmap RotateImage(this Bitmap sourceBitmap,
                                       double angle)
        {

            //Convert to Radians 
            angle = angle / 57.29577951308;


            //Calculate new heaight and width
            int sourceWidth = sourceBitmap.Width;
            int sourceHeight = sourceBitmap.Height;

            int newWidth, newHeight;

            if (angle < Math.PI)
            {
                newWidth = (int)Math.Round(sourceWidth * Math.Cos(angle) + sourceHeight * Math.Sin(angle));
                newHeight = (int)Math.Round(sourceWidth * Math.Sin(angle) + sourceHeight * Math.Cos(angle));
            }
            else if( angle == Math.PI)
            {
                newWidth = sourceWidth;
                newHeight = sourceHeight;
            }
            else 
            {
                double tmp = angle - Math.PI / 2;
                newWidth = (int)Math.Round(sourceHeight * Math.Cos(angle) + sourceWidth * Math.Sin(angle));
                newHeight = (int)Math.Round(sourceHeight * Math.Sin(angle) + sourceWidth * Math.Cos(angle));
            }


            newWidth = Math.Abs(newWidth);
            newHeight = Math.Abs(newHeight);

            Bitmap resultBitmap = new Bitmap(newWidth, newHeight);

            int moveX = (int)(resultBitmap.Width - sourceBitmap.Width) / 2;
            int moveY = (int)(resultBitmap.Height - sourceBitmap.Height) / 2;

            //Calculate Offset in order to rotate on image middle 
            int xOffset = (int)(sourceBitmap.Width / 2.0);
            int yOffset = (int)(sourceBitmap.Height / 2.0);

            for (int x = 0; x < resultBitmap.Width; x++)
            {
                for (int y = 0; y < resultBitmap.Height; y++)
                {
                    //swaping pixels
                    int newX = RotateX(x, y, -angle, xOffset, yOffset);
                    int newY = RotateY(x, y, -angle, xOffset, yOffset);

                    if (newX < sourceBitmap.Width && newY < sourceBitmap.Height && newX > 0 && newY > 0)
                    {
                        Color color = sourceBitmap.GetPixel(newX, newY);
                        resultBitmap.SetPixel(x, y, color);
                    }
                }
            }
            return resultBitmap;
        }
        public static void bytes(Bitmap bmp, PaintEventArgs e)
        {
            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

            // Set every third value to 255. A 24bpp bitmap will look red.  
            for (int counter = 2; counter < rgbValues.Length; counter += 3)
                rgbValues[counter] = 255;

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);

            // Draw the modified image.
            e.Graphics.DrawImage(bmp, 0, 150);
        }
    }
}