using Liedeinblendung.Model;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Liedeinblendung.ViewModel
{
    public class ConfigViewModel : ObservableObject
    {
        #region Private Fields

        private readonly AppSettingReaderWriter _appSetting = new AppSettingReaderWriter();

        private readonly CsvReaderWriter _csvReaderWriter = new CsvReaderWriter();

        private readonly PictureReader _pictureReader = new PictureReader();

        #endregion Private Fields

        #region Public Constructors

        public ConfigViewModel()
        {
            UseGreenScreen = Convert.ToBoolean(_appSetting.ReadSetting(KeyName.UseGreenscreen));
            ShowMetaData = Convert.ToBoolean(_appSetting.ReadSetting(KeyName.ShowComponistAndAutor));
            if (File.Exists($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/LogoBig.png"))
            {
                Bitmap image = new Bitmap($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/LogoBig.png");
                PreviewLogo = BitmapToImageSource(image);
            }
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<ObservableCollection<MinistryGridViewModel>> OnLoadMinistries;

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
                    _appSetting.WriteAppSetting(KeyName.ShowComponistAndAutor, value.ToString());
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
                    _appSetting.WriteAppSetting(KeyName.UseGreenscreen, value.ToString());
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