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

        private readonly HymnalInputViewModel _cbData;

        //public BibleBrowserViewModel BibleBrowserViewModel { get; set; } = new BibleBrowserViewModel();

        public BibleViewModel BibleViewModel { get; private set; }

        private readonly HymnalInputViewModel _gbData;

        private readonly PreviewWindowController _previewWindow;

        #endregion Private Fields

        #region Public Constructors

        public WindowViewModel(PositionData positionData)
        {
            var fadeInWriter = new FadeInWriter(positionData);
            HistoryViewModel = new HistoryViewModel(fadeInWriter);

            _log.Info("Load Data");
            _gbData = new HymnalInputViewModel(HymnalJsonReader.LoadHymnalData($"{Directory.GetCurrentDirectory()}/DataSource/GB_Data.json"), "Gesangbuch", fadeInWriter, HistoryViewModel);
            _cbData = new HymnalInputViewModel(HymnalJsonReader.LoadHymnalData($"{Directory.GetCurrentDirectory()}/DataSource/CB_Data.json"), "Chorbuch", fadeInWriter, HistoryViewModel);

            HymnalInputVisible = true;
            BibleInputVisible = false;
            CustomInputVisible = false;
            MinistryInputVisible = false;

            _log.Info("Load Ministry");
            MinistryViewModel = new MinistryViewModel(fadeInWriter, HistoryViewModel);
            CustomizedViewModel = new CustomizedInsertViewModel(fadeInWriter, HistoryViewModel);

            _log.Info("Load PreviewMode");
            PreviewViewModel = new PreviewViewModel(fadeInWriter.CurrentFade);

            ConfigViewModel.OnLoadMinistries += UpdateMinistries;
            ConfigViewModel.OnResetMinistries += ResetMinistries;
            ConfigViewModel.OnUpdateFullscreenMode += UpdateFullscreenMode;
            ConfigViewModel.OnUpdatePreview += UpdatePreviewMode;
            fadeInWriter.OnInsertUpdate += UpdatePreview;

            CurrentHymnalViewModel = _gbData;

            _previewWindow = new PreviewWindowController(PreviewViewModel);

            _log.Info("ReadBible");
            BibleViewModel = new BibleViewModel(BibleJsonReader.LoadBibleData($"{Directory.GetCurrentDirectory()}/DataSource/Bible_Data.json"), fadeInWriter, HistoryViewModel);
            //BibleViewModel.OpenBibleBrowser += OnOpenBibleBrowser;

            _log.Info("Create Preview");
            SetPreview(Properties.Settings.Default.ShowPreviewPicture);

            if (Properties.Settings.Default.ShowInsertInFullscreen)
                _previewWindow.Show();
        }

        //private async void OnOpenBibleBrowser(object sender, string e)
        //{
        //    await MaterialDesignThemes.Wpf.DialogHost.Show(BibleBrowserViewModel, "MainWindow");
        //}

        #endregion Public Constructors

        #region Public Properties

        public bool BibleInputVisible
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public ICommand CloseCommand => new RelayCommand(OnCloseWindow);
        public ConfigViewModel ConfigViewModel { get; set; } = new ConfigViewModel();

        public HymnalInputViewModel CurrentHymnalViewModel
        {
            get { return GetValue<HymnalInputViewModel>(); }
            set { SetValue(value); }
        }

        public bool CustomInputVisible
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public bool MinistryInputVisible
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public CustomizedInsertViewModel CustomizedViewModel { get; set; }

        public string Hight
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public HistoryViewModel HistoryViewModel { get; set; }

        public bool HymnalInputVisible
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public InfoViewModel InfoViewModel { get; set; } = new InfoViewModel();

        public MinistryViewModel MinistryViewModel
        {
            get { return GetValue<MinistryViewModel>(); }
            set { SetValue(value); }
        }

        public ICommand OnShowConfig => new RelayCommand(OpenConfigDialog);
        public ICommand OnShowInfo => new RelayCommand(OpenInfoDialog);
        public PreviewViewModel PreviewViewModel { get; set; }

        public bool PrviewVisibleFlag
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public int Selected
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);

                if (value == 4)
                {
                    HymnalInputVisible = false;
                    BibleInputVisible = false;
                    CustomInputVisible = true;
                    MinistryInputVisible = false;
                    return;
                }

                if (value == 3)
                {
                    HymnalInputVisible = false;
                    BibleInputVisible = true;
                    CustomInputVisible = false;
                    MinistryInputVisible = false;
                    return;
                }

                if (value == 2)
                {
                    HymnalInputVisible = false;
                    BibleInputVisible = false;
                    CustomInputVisible = false;
                    MinistryInputVisible = true;
                    return;
                }

                HymnalInputVisible = true;
                BibleInputVisible = false;
                CustomInputVisible = false;
                MinistryInputVisible = false;

                if (value == 1)
                {
                    CurrentHymnalViewModel = _cbData;
                    return;
                }
                CurrentHymnalViewModel = _gbData;
            }
        }

        public string With
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        #endregion Public Properties

        #region Private Methods

        private void OnCloseWindow(object obj)
        {
            CustomizedViewModel.SaveInserts();
            Properties.Settings.Default.Save();
            _previewWindow.Close();
        }

        async private void OpenConfigDialog(object obj)
        {
            await MaterialDesignThemes.Wpf.DialogHost.Show(ConfigViewModel, "MainWindow");
        }

        async private void OpenInfoDialog(object obj)
        {
            await MaterialDesignThemes.Wpf.DialogHost.Show(InfoViewModel, "MainWindow");
        }

        private void ResetMinistries(object sender, EventArgs e)
        {
            MinistryViewModel.Reset();
        }

        private void SetPreview(bool state)
        {
            _gbData.UpdateButtons(state);
            _cbData.UpdateButtons(state);
            BibleViewModel.UpdateButtons(state);
            CustomizedViewModel.UpdateButtons(state);
            MinistryViewModel.UpdateButtons(state);

            if (state)
            {
                With = "1200";
                Hight = "790";
                PrviewVisibleFlag = true;
                OnPropertyChanged("With");
                OnPropertyChanged("Hight");
                OnPropertyChanged("PrviewVisibleFlag");
                return;
            }
            PrviewVisibleFlag = false;
            With = "550";
            Hight = "550";
            OnPropertyChanged("With");
            OnPropertyChanged("Hight");
            OnPropertyChanged("PrviewVisibleFlag");
        }

        private void UpdateFullscreenMode(object sender, bool e)
        {
            _previewWindow.Update(e);
        }

        private void UpdateMinistries(object sender, ObservableCollection<MinistryGridViewModel> e)
        {
            MinistryViewModel.UpdateMinistries(e);
        }

        private void UpdatePreview(object sender, Bitmap e)
        {
            PreviewViewModel.SetPreview(e);
        }

        private void UpdatePreviewMode(object sender, bool e)
        {
            SetPreview(e);
        }

        #endregion Private Methods
    }
}