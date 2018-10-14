using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageComparatorLibrary;

namespace ImageComparatorApp
{
    public partial class Form1 : Form
    {
        string supportedImagesTypes = "All Supported|*.jpg;*.jpeg;*.png|JPEG|*.jpg;*jpeg|Portable Network Graphic|*.png";
        ImageComparator image1Comp;
        ImageComparator image2Comp;
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Invalidate();
            OpenFileDialog image1 = new OpenFileDialog
            {
                Title = "Open Image 1",
                Filter = supportedImagesTypes
            };

            if (image1.ShowDialog() == DialogResult.OK)
            {
                image1Comp = new ImageComparator(image1.FileName);
                pictureBox1.Image = image1Comp.Source;
                pictureBox3.Image = image1Comp.imageGrayscaled;
                if (image2Comp != null)
                {
                    image1Comp.DrawDifference(image1Comp.CompareImagesList(image2Comp.GetBrightness()), image1.FileName, 664, 12, this.CreateGraphics());
                    richTextBox1.AppendText("\nResult: " + Math.Round(image1Comp.CompareImages(image2Comp.GetBrightness()) / 256, 2).ToString() + "%");
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Invalidate();
            OpenFileDialog image2 = new OpenFileDialog
            {
                Title = "Open Image 2",
                Filter = supportedImagesTypes
            };

            if (image2.ShowDialog() == DialogResult.OK)
            {
                image2Comp = new ImageComparator(image2.FileName);
                pictureBox2.Image = image2Comp.Source;
                pictureBox4.Image = image2Comp.imageGrayscaled;
                if (image1Comp != null)
                {
                    image2Comp.DrawDifference(image2Comp.CompareImagesList(image1Comp.GetBrightness()), image2.FileName, 664, 12, this.CreateGraphics());
                    richTextBox1.AppendText("\nResult: " + Math.Round(image1Comp.CompareImages(image2Comp.GetBrightness()) / 256, 2).ToString() + "%");
                }
            }
        }
    }
}
