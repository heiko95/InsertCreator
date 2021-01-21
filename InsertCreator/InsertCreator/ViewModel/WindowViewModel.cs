using HgSoftware.InsertCreator.Model;
using HgSoftware.InsertCreator.View;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Windows.Input;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class WindowViewModel : ObservableObject
    {
        #region Public Fields


        #endregion Public Fields

        #region Private Fields

        private readonly HymnalInputViewModel _cbData;

        private readonly HymnalInputViewModel _gbData;

        private readonly HymnalJsonReader _hymnalJsonReader = new HymnalJsonReader();

        private readonly BibleJsonReader _bibleJsonReader = new BibleJsonReader();

        private readonly FadeInWriter _fadeInWriter = new FadeInWriter();

        private readonly PreviewViewModel _previewViewModel = new PreviewViewModel();

        private readonly AppSettingReaderWriter _appSetting = new AppSettingReaderWriter();

        private readonly PreviewWindowController _previewWindow;


        

        #endregion Private Fields

        #region Public Constructors

        public WindowViewModel()
        {
            _gbData = new HymnalInputViewModel(_hymnalJsonReader.LoadHymnalData(($"{Directory.GetCurrentDirectory()}/DataSource/GB_Data.json")), "Gesangbuch", _fadeInWriter);
            _cbData = new HymnalInputViewModel(_hymnalJsonReader.LoadHymnalData(($"{Directory.GetCurrentDirectory()}/DataSource/CB_Data.json")), "Chorbuch", _fadeInWriter);
            BibleViewModel = new BibleViewModel(_bibleJsonReader.LoadBibleData(($"{Directory.GetCurrentDirectory()}/DataSource/Bible_Data.json")), _fadeInWriter);
            HymnalInputVisible = true;
            BibleInputVisible = false;
            MinistryViewModel = new MinistryViewModel(_fadeInWriter);
            ConfigViewModel.OnLoadMinistries += UpdateMinistries;
            ConfigViewModel.OnUpdatePreviewMode += UpdatePreviewMode;
            _fadeInWriter.OnInsertUpdate += UpdatePreview;
            CurrentHymnalViewModel = _gbData;
            _previewWindow = new PreviewWindowController(_previewViewModel);

            if (Convert.ToBoolean(_appSetting.ReadSetting(KeyName.ShowPreviewPicture)))
                _previewWindow.Show();

        }


       



    private void UpdatePreview(object sender, Bitmap e)
        {
            _previewViewModel.SetPreview(e);
        }

        private void UpdatePreviewMode(object sender, bool e)
        {
            _previewWindow.Update(e);           
        }

        #endregion Public Constructors

        #region Public Properties

        public HymnalInputViewModel CurrentHymnalViewModel
        {
            get { return GetValue<HymnalInputViewModel>(); }
            set { SetValue(value); }
        }


        public ConfigViewModel ConfigViewModel { get; set; } = new ConfigViewModel();

        public InfoViewModel InfoViewModel { get; set; } = new InfoViewModel();

        public BibleViewModel BibleViewModel { get; set; }


        public bool HymnalInputVisible
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public bool BibleInputVisible
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }


        
        public MinistryViewModel MinistryViewModel
        {
            get { return GetValue<MinistryViewModel>(); }
            set { SetValue(value); }
        }

        public ICommand OnShowConfig => new RelayCommand(OpenConfigDialog);

        public ICommand OnShowInfo => new RelayCommand(OpenInfoDialog);

        public ICommand CloseCommand => new RelayCommand(OnCloseWindow);
        
        private void OnCloseWindow(object obj)
        {
            _previewWindow.Close(); 
        }

        public int Selected
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);

                if (value == 3)
                {
                    HymnalInputVisible = false;
                    BibleInputVisible = true;
                    return;
                }

                if (value == 2)
                {
                    HymnalInputVisible = false;
                    BibleInputVisible = false;
                    return;
                }

                HymnalInputVisible = true;
                BibleInputVisible = false;

                if (value == 1)
                {
                    CurrentHymnalViewModel = _cbData;
                    return;
                }
                CurrentHymnalViewModel = _gbData;
            }
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

        #endregion Private Methods
    }
}