using HgSoftware.InsertCreator.Model;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Windows.Input;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class WindowViewModel : ObservableObject
    {
        #region Private Fields

        /// <summary>
        /// Logfield
        /// </summary>
        private static readonly log4net.ILog _log = LogHelper.GetLogger();

        private readonly BibleJsonReader _bibleJsonReader = new BibleJsonReader();
        private readonly HymnalInputViewModel _cbData;

        private readonly FadeInWriter _fadeInWriter = new FadeInWriter();
        private readonly HymnalInputViewModel _gbData;

        private readonly HymnalJsonReader _hymnalJsonReader = new HymnalJsonReader();
        private readonly PreviewWindowController _previewWindow;
        public PreviewViewModel PreviewViewModel { get; set; }

        #endregion Private Fields

        #region Public Constructors

        public WindowViewModel()
        {
            _log.Info("Create Preview");
            SetPreview(Properties.Settings.Default.ShowPreviewPicture);

            _log.Info("Load Data");
            _gbData = new HymnalInputViewModel(_hymnalJsonReader.LoadHymnalData(($"{Directory.GetCurrentDirectory()}/DataSource/GB_Data.json")), "Gesangbuch", _fadeInWriter);
            _cbData = new HymnalInputViewModel(_hymnalJsonReader.LoadHymnalData(($"{Directory.GetCurrentDirectory()}/DataSource/CB_Data.json")), "Chorbuch", _fadeInWriter);
            BibleViewModel = new BibleViewModel(_bibleJsonReader.LoadBibleData(($"{Directory.GetCurrentDirectory()}/DataSource/Bible_Data.json")), _fadeInWriter);

            HymnalInputVisible = true;
            BibleInputVisible = false;

            _log.Info("Load Ministry");
            MinistryViewModel = new MinistryViewModel(_fadeInWriter);
            CustomizedViewModel = new CustomizedInsertViewModel(_fadeInWriter);

            _log.Info("Load PreviewMode");
            PreviewViewModel = new PreviewViewModel(_fadeInWriter.CurrentFade);

            ConfigViewModel.OnLoadMinistries += UpdateMinistries;
            ConfigViewModel.OnResetMinistries += ResetMinistries;
            ConfigViewModel.OnUpdateFullscreenMode += UpdateFullscreenMode;
            ConfigViewModel.OnUpdatePreview += UpdatePreviewMode;
            _fadeInWriter.OnInsertUpdate += UpdatePreview;

            CurrentHymnalViewModel = _gbData;

            _previewWindow = new PreviewWindowController(PreviewViewModel);

            if (Properties.Settings.Default.ShowInsertInFullscreen)
                _previewWindow.Show();
        }



        public bool PrviewVisibleFlag
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public string With
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        private void SetPreview(bool state)
        {
            if (state)
            {
                With = "1200";
                PrviewVisibleFlag = true;
                OnPropertyChanged("With");
                OnPropertyChanged("PrviewVisibleFlag");
                return;
            }
            PrviewVisibleFlag = false;
            With = "550";
            OnPropertyChanged("With");
            OnPropertyChanged("PrviewVisibleFlag");
            return;
        }

        private void UpdateFullscreenMode(object sender, bool e)
        {
            _previewWindow.Update(e);
        }

        private void UpdatePreview(object sender, Bitmap e)
        {
            PreviewViewModel.SetPreview(e);
        }

        private void UpdatePreviewMode(object sender, bool e)
        {
            SetPreview(e);
        }

        #endregion Public Constructors

        #region Public Properties

        public bool BibleInputVisible
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public BibleViewModel BibleViewModel { get; set; }

        public ICommand CloseCommand => new RelayCommand(OnCloseWindow);

        public ConfigViewModel ConfigViewModel { get; set; } = new ConfigViewModel();

        public HymnalInputViewModel CurrentHymnalViewModel
        {
            get { return GetValue<HymnalInputViewModel>(); }
            set { SetValue(value); }
        }

        public bool HymnalInputVisible
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public bool CustomInputVisible
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public CustomizedInsertViewModel CustomizedViewModel { get; set; } 

        public InfoViewModel InfoViewModel { get; set; } = new InfoViewModel();

        public MinistryViewModel MinistryViewModel
        {
            get { return GetValue<MinistryViewModel>(); }
            set { SetValue(value); }
        }

        public ICommand OnShowConfig => new RelayCommand(OpenConfigDialog);

        public ICommand OnShowInfo => new RelayCommand(OpenInfoDialog);

        public int Selected
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);

                //if (value == 4)
                //{
                //    HymnalInputVisible = false;
                //    BibleInputVisible = true;
                //    CustomInputVisible = false;
                //    return;
                //}

                if (value == 3)
                {
                    HymnalInputVisible = false;
                    BibleInputVisible = false;
                    CustomInputVisible = true;
                    return;
                }

                if (value == 2)
                {
                    HymnalInputVisible = false;
                    BibleInputVisible = false;
                    CustomInputVisible = false;
                    return;
                }

                HymnalInputVisible = true;
                BibleInputVisible = false;
                CustomInputVisible = false;

                if (value == 1)
                {
                    CurrentHymnalViewModel = _cbData;
                    return;
                }
                CurrentHymnalViewModel = _gbData;
            }
        }

        private void OnCloseWindow(object obj)
        {
            CustomizedViewModel.SaveInserts();
            Properties.Settings.Default.Save();
            _previewWindow.Close();
        }

        #endregion Public Properties

        #region Private Methods

        async private void OpenConfigDialog(object obj)
        {
            await MaterialDesignThemes.Wpf.DialogHost.Show(ConfigViewModel, "MainWindow");
        }

        async private void OpenInfoDialog(object obj)
        {
            await MaterialDesignThemes.Wpf.DialogHost.Show(InfoViewModel, "MainWindow");
        }

        private void UpdateMinistries(object sender, ObservableCollection<MinistryGridViewModel> e)
        {
            MinistryViewModel.UpdateMinistries(e);
        }

        private void ResetMinistries(object sender, EventArgs e)
        {
            MinistryViewModel.Reset();
        }

        #endregion Private Methods
    }
}