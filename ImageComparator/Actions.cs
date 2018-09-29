using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageComparator
{
    public class Actions
    {
        public static Bitmap GrayscaleImage(Bitmap source)
        {
            Bitmap grayScaledImage = new Bitmap(source.Width, source.Height);
            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {
                    Color pixelColor = source.GetPixel(j, i);
                    //https://en.wikipedia.org/wiki/Grayscale from thread https://stackoverflow.com/questions/2265910/convert-an-image-to-grayscale
                    int grayScale = (int)((pixelColor.R * 0.299) + (pixelColor.G * 0.587) + (pixelColor.B * 0.114));
                    Color grayColor = Color.FromArgb(pixelColor.A, grayScale, grayScale, grayScale);
                    grayScaledImage.SetPixel(j, i, grayColor);
                }
            }
            return grayScaledImage;
        }

        public static List<double> GetBrightness(Bitmap source)
        {
            List<double> results = new List<double>();

            for (int i = 0; i < source.Width; i++)
            {
                for (int j = 0; j < source.Height; j++)
                {
                    results.Add(source.GetPixel(j, i).GetBrightness());
                }
            }
            return results;
        }
    }
}
