using System;
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

                    Bitmap image = new Bitmap(openFileDialog.FileName);
                    var scaledBitmap = ScaleBitmap(image);
                    scaledBitmap.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Logo.png", System.Drawing.Imaging.ImageFormat.Png);
                    scaledBitmap.Dispose();
                    image.Dispose();
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

        public Bitmap ScaleBitmap(Bitmap image)
        {
            var size = Math.Max(image.Width, image.Height);

            var resultPicture = new Bitmap(size, size);

            using (var g = Graphics.FromImage(resultPicture))
            {
                if (image.Width > image.Height)
                {
                    var offsetY = (size - image.Height) / 2;
                    g.DrawImage(image, 0, offsetY, image.Width, image.Height);
                    return resultPicture;
                }

                if (image.Width < image.Height)
                {
                    var offsetX = (size - image.Width) / 2;
                    g.DrawImage(image, offsetX, 0, image.Width, image.Height);
                    return resultPicture;
                }

                g.DrawImage(image, 0, 0, image.Width, image.Height);
                return resultPicture;
            }
        }

        #endregion Public Methods
    }
}