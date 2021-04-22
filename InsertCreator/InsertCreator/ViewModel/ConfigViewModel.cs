using HgSoftware.InsertCreator.Model;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
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
            LogoAsCornerbug = Properties.Settings.Default.LogoAsCornerlogo;

            if (Properties.Settings.Default.LogoOnLefthand)
                LogoPositionSelection = 0;
            else
                LogoPositionSelection = 1;

            if (File.Exists($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Logo.png"))
            {
                Bitmap image = new Bitmap($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Logo.png");
                PreviewLogo = BitmapToImageSource(image);
            }
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<ObservableCollection<MinistryGridViewModel>> OnLoadMinistries;

        public event EventHandler<EventArgs> OnResetMinistries;

        public event EventHandler<bool> OnUpdateFullscreenMode;

        public event EventHandler<bool> OnUpdatePreview;

        public event EventHandler<EventArgs> OnSaveMinistries;

        #endregion Public Events

        #region Public Properties

        public bool LogoAsCornerbug
        {
            get { return GetValue<bool>(); }
            set
            {
                if (LogoAsCornerbug != value)
                {
                    SetValue(value);
                    Properties.Settings.Default.LogoAsCornerlogo = value;
                }
            }
        }

        public int LogoPositionSelection
        {
            get { return GetValue<int>(); }
            set
            {
                if (LogoPositionSelection != value)
                {
                    SetValue(value);
                    if (value == 1)
                    {
                        Properties.Settings.Default.LogoOnLefthand = false;
                        return;
                    }
                    Properties.Settings.Default.LogoOnLefthand = true;
                }
            }
        }

        public ICommand OnDeleteList => new RelayCommand(DeleteMinistryList);
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

        public bool ShowInsertInFullscreen
        {
            get { return GetValue<bool>(); }
            set
            {
                if (ShowInsertInFullscreen != value)
                {
                    SetValue(value);
                    Properties.Settings.Default.ShowInsertInFullscreen = value;
                    OnUpdateFullscreenMode?.Invoke(this, value);
                }
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
                    OnUpdatePreview?.Invoke(this, value);
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

        private void DeleteMinistryList(object obj)
        {
            var result = MessageBox.Show("Möchten sie die Amtsträgerliste unwiderruflich löschen", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                OnResetMinistries?.Invoke(this, new EventArgs());
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

            if (File.Exists($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Logo.png"))
            {
                Bitmap image = new Bitmap($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Logo.png");
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
            OnSaveMinistries?.Invoke(this, EventArgs.Empty);
            _csvReaderWriter.SaveCsv();
        }

        #endregion Private Methods
    }
}