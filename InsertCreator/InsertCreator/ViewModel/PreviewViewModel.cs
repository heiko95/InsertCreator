using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class PreviewViewModel : ObservableObject
    {
        #region Public Constructors

        public PreviewViewModel(Bitmap startimage)
        {
            PreviewImage = BitmapToImageSource(startimage);
        }

        #endregion Public Constructors

        #region Public Properties

        public ImageSource PreviewImage
        {
            get { return GetValue<ImageSource>(); }
            set
            {
                SetValue(string.Empty);
                SetValue(value);
            }
        }

        public ImageSource OldPreviewImage
        {
            get { return GetValue<ImageSource>(); }
            set
            {
                SetValue(string.Empty);
                SetValue(value);
            }
        }

        #endregion Public Properties

        #region Public Methods

        public void SetPreview(Bitmap image)
        {
            PreviewImage = BitmapToImageSource(image);
        }

        #endregion Public Methods

        #region Private Methods

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

        #endregion Private Methods
    }
}