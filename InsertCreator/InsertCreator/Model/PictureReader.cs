using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Liedeinblendung.Model
{
    public class PictureReader
    {
        private AppSettingReaderWriter _appSetting = new AppSettingReaderWriter();

        public void LoadPicture()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetEnvironmentVariable("userprofile");
                openFileDialog.Filter = "png files (*.png)|*.png|jpg files (*.jpg)|*.jpg";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    RemovePicture();
                    Bitmap smallImage = ResizePicture(new Bitmap(openFileDialog.FileName), 150);
                    smallImage.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/LogoSmall.png", System.Drawing.Imaging.ImageFormat.Png);

                    Bitmap bigImage = ResizePicture(new Bitmap(openFileDialog.FileName), 240);
                    bigImage.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/LogoBig.png", System.Drawing.Imaging.ImageFormat.Png);

                    _appSetting.WriteAppSetting(KeyName.UseLogo, "true");
                }
            }
        }

        public void RemovePicture()
        {
            List<string> files = new List<string>()
            {
                $"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/LogoBig.png",
                $"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/LogoSmall.png"
            };

            _appSetting.WriteAppSetting(KeyName.UseLogo, "false");

            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }
        }

        private Bitmap ResizePicture(Bitmap image, int size)
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
    }
}