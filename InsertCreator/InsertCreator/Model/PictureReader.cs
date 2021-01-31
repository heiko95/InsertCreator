using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HgSoftware.InsertCreator.Model
{
    public class PictureReader
    {
        #region Public Methods

        public void LoadPicture()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetEnvironmentVariable("userprofile");
                openFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png;";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    RemovePicture();

                    Bitmap bigImage = new Bitmap(openFileDialog.FileName);
                    bigImage.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Logo.png", System.Drawing.Imaging.ImageFormat.Png);
               
                    Properties.Settings.Default.UseLogo = true;                   
                }
            }
        }

        public void RemovePicture()
        {
            if (File.Exists($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Logo.png"))
            {
                File.Delete($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Logo.png");
            }
            Properties.Settings.Default.UseLogo = false; 
        }

        #endregion Public Methods

        #region Private Methods

        public Bitmap ResizePicture(Bitmap image, int size)
        {
            if (image.Width == image.Height)
            {
                return new Bitmap(image, size, size);
            }
            double ratio;

            if (image.Width < image.Height)
            {
                ratio = (double)size / image.Height;
                return new Bitmap(image, Convert.ToInt32(image.Width * ratio), size);
            }
            ratio = (double)size / image.Width;
            return new Bitmap(image, size, Convert.ToInt32(image.Height * ratio));
        }

        #endregion Private Methods
    }
}