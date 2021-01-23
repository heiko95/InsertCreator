using HgSoftware.InsertCreator.Model;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class ConfigViewModel : ObservableObject
    {
        #region Private Fields

        private readonly CsvReaderWriter _csvReaderWriter = new CsvReaderWriter();

        private readonly PictureReader _pictureReader = new PictureReader();

        #endregion Private Fields

        #region Public Constructors

        public ConfigViewModel()
        {
            UseGreenScreen = Properties.Settings.Default.UseGreenscreen;
            ShowMetaData = Properties.Settings.Default.ShowComponistAndAutor;
            ShowInsertInFullscreen = Properties.Settings.Default.ShowInsertInFullscreen;
            ShowPreviewPicture = Properties.Settings.Default.ShowPreviewPicture;
            if (File.Exists($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/LogoBig.png"))
            {
                Bitmap image = new Bitmap($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/LogoBig.png");
                PreviewLogo = BitmapToImageSource(image);
            }
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<ObservableCollection<MinistryGridViewModel>> OnLoadMinistries;

        public event EventHandler<bool> OnUpdateFullscreenMode;

        public event EventHandler<bool> OnUpdatePreview;

        #endregion Public Events

        #region Public Properties

        public ICommand OnLoadCsv => new RelayCommand(LoadCSV);

        public ICommand OnReset => new RelayCommand(RemoveLogo);

        public ICommand OnSaveCsv => new RelayCommand(SaveCSV);

        public ICommand OnUpload => new RelayCommand(LoadLogo);

        public ImageSource PreviewLogo
        {
            get { return GetValue<ImageSource>(); }
            set
            {
                SetValue(string.Empty);
                SetValue(value);
            }
        }

        public bool ShowMetaData
        {
            get { return GetValue<bool>(); }
            set
            {
                if (ShowMetaData != value)
                {
                    SetValue(value);
                    Properties.Settings.Default.ShowComponistAndAutor = value;
                    //_appSetting.WriteAppSetting(KeyName.ShowComponistAndAutor, value.ToString());
                }
            }
        }

        public bool UseGreenScreen
        {
            get { return GetValue<bool>(); }
            set
            {
                if (UseGreenScreen != value)
                {
                    SetValue(value);
                    Properties.Settings.Default.UseGreenscreen = value;
                    //_appSetting.WriteAppSetting(KeyName.UseGreenscreen, value.ToString());
                }
            }
        }

        public bool ShowInsertInFullscreen
        {
            get { return GetValue<bool>(); }
            set
            {
                if (ShowInsertInFullscreen != value)
                {
                    SetValue(value);
                    Properties.Settings.Default.ShowInsertInFullscreen = value;
                    //_appSetting.WriteAppSetting(KeyName.ShowInsertInFullscreen, value.ToString());
                    OnUpdateFullscreenMode?.Invoke(this, value);
                }
            }
        }

        public bool ShowPreviewPicture
        {
            get { return GetValue<bool>(); }
            set
            {
                if (ShowPreviewPicture != value)
                {
                    SetValue(value);
                    Properties.Settings.Default.ShowPreviewPicture = value;
                    //_appSetting.WriteAppSetting(KeyName.ShowPreviewPicture, value.ToString());
                    OnUpdatePreview?.Invoke(this, value);
                }
            }
        }

        #endregion Public Properties

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

        private void LoadCSV(object obj)
        {
            var loadedMinisries = _csvReaderWriter.ImportCsv();
            if (loadedMinisries != null)
                OnLoadMinistries?.Invoke(this, loadedMinisries);
        }

        private void LoadLogo(object obj)
        {
            _pictureReader.LoadPicture();

            if (File.Exists($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/LogoBig.png"))
            {
                Bitmap image = new Bitmap($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/LogoBig.png");
                PreviewLogo = BitmapToImageSource(image);
            }
        }

        private void RemoveLogo(object obj)
        {
            PreviewLogo = null;
            _pictureReader.RemovePicture();
        }

        private void SaveCSV(object obj)
        {
            _csvReaderWriter.SaveCsv();
        }

        #endregion Private Methods
    }
}