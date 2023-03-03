using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class Program
{
    static void Main(string[] args)
    {
        
        Bitmap image1 = new Bitmap("image1.jpg");
        Bitmap image2 = new Bitmap("image2.jpg");

        Func<Bitmap, Bitmap> grayScaleFilter = bitmap =>
        {
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int gray = (int)(color.R * 0.3 + color.G * 0.59 + color.B * 0.11);
                    newBitmap.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            }

            return newBitmap;
        };

        Func<Bitmap, Bitmap> brightenFilter = bitmap =>
        {
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int r = color.R + 50;
                    int g = color.G + 50;
                    int b = color.B + 50;
                    r = Math.Min(r, 255);
                    g = Math.Min(g, 255);
                    b = Math.Min(b, 255);
                    newBitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            return newBitmap;
        };

        Func<Bitmap, Bitmap> invertFilter = bitmap =>
        {
            Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int r = 255 - color.R;
                    int g = 255 - color.G;
                    int b = 255 - color.B;
                    newBitmap.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }

            return newBitmap;
        };

        
        Action<Bitmap> displayImage = bitmap =>
        {
            string fileName = $"{Path.GetFileNameWithoutExtension(Path.GetRandomFileName())}.jpg";
            bitmap.Save(fileName, ImageFormat.Jpeg);
            Console.WriteLine($"Processed image saved to {fileName}");
        };

        Bitmap processedImage1 = grayScaleFilter(image1);
        displayImage(processedImage1);

        Bitmap processedImage2 = brightenFilter(image2);
        displayImage(processedImage2);

        Bitmap processedImage3 = invertFilter(image1);
        displayImage(processedImage3);
    }
}
