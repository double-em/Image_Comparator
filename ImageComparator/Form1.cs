using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageComparator
{
    public partial class Form1 : Form
    {
        string supportedImagesTypes = "All Supported|*.jpg;*.jpeg;*.png|JPEG|*.jpg;*jpeg|Portable Network Graphic|*.png";
        List<double> brightnessImage1 = new List<double>();
        List<double> brightnessImage2 = new List<double>();
        double difference = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog image1 = new OpenFileDialog
            {
                Title = "Open Image 1",
                Filter = supportedImagesTypes
            };

            if (image1.ShowDialog() == DialogResult.OK)
            {
                Bitmap image1Bmp =  new Bitmap(image1.FileName);
                Bitmap image1Resized = new Bitmap(image1Bmp, new Size(16, 16));

                pictureBox1.Image = image1Bmp;
                Bitmap grayscaledImage1 = Actions.GrayscaleImage(image1Resized);
                pictureBox3.Image = grayscaledImage1;
                brightnessImage1 = Actions.GetBrightness(grayscaledImage1);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog image2 = new OpenFileDialog
            {
                Title = "Open Image 2",
                Filter = supportedImagesTypes
            };

            if (image2.ShowDialog() == DialogResult.OK)
            {
                Bitmap image2Bmp = new Bitmap(image2.FileName);
                Bitmap image2Resized = new Bitmap(image2Bmp, new Size(16, 16));

                pictureBox2.Image = image2Bmp;
                Bitmap grayscaledImage2 = Actions.GrayscaleImage(image2Resized);
                pictureBox4.Image = grayscaledImage2;
                brightnessImage2 = Actions.GetBrightness(grayscaledImage2);

                Image image2Img = Image.FromFile(image2.FileName);
                Graphics graphicsObj = Graphics.FromImage(image2Bmp);
                graphicsObj = this.CreateGraphics();
                Pen pen1 = new Pen(Color.Black, 1);
                int offsetX = 664;
                int offsetY = 12;
                int counter = 0;
                for (int j = 0; j < 16; j++)
                {
                    for (int i = 0; i < 16; i++)
                    {
                        double result = Math.Sqrt(Math.Pow(brightnessImage1[counter] - brightnessImage2[counter], 2));
                        //double result = brightnessImage1[counter];
                        Rectangle rectangle1 = new Rectangle((offsetX + (i * 20)), offsetY, 20, 20);
                        SolidBrush brush1 = new SolidBrush(Color.FromArgb(0, 255, 255, 255));
                        SolidBrush brushText = new SolidBrush(Color.FromArgb(0, 0, 0));

                        result *= 100;
                        if (result > 1)
                        {
                            brush1 = new SolidBrush(Color.FromArgb((int)Math.Round(result, 0), 255, 0, 0));
                            richTextBox1.AppendText(Math.Round(result, 2).ToString() + "%, ");
                            difference += result;
                        }

                        Font font1 = new Font("Arial", 10);
                        graphicsObj.DrawString(Math.Round(result, 0).ToString(), font1, brushText, rectangle1);
                        graphicsObj.FillRectangle(brush1, rectangle1);
                        counter++;
                    }
                    offsetY += 20;
                }
                
                richTextBox1.AppendText("\nResult: " + Math.Round(((difference/256)), 2).ToString() + "%");
            }
        }
    }
}
