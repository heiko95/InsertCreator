﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class PreviewViewModel : ObservableObject
    {

        public PreviewViewModel(Bitmap startimage)
        {
            PreviewImage = BitmapToImageSource(startimage);
        }

        public void SetPreview(Bitmap image)
        {           
          PreviewImage = BitmapToImageSource(image);      
        }

        public ImageSource PreviewImage 
        {
            get { return GetValue<ImageSource>(); }
            set
            {
                SetValue(string.Empty);
                SetValue(value);
            }
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }


    }
}
