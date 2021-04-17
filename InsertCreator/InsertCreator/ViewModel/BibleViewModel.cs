﻿using HgSoftware.InsertCreator.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design;
using System.Windows.Input;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class BibleViewModel : ObservableObject, INotifyDataErrorInfo
    {
        #region Private Fields

        private FadeInWriter _fadeInWriter;
        private HistoryViewModel _historyViewModel;

        private readonly BibleValidationViewModel _bibleValidationViewModel;

        //public event EventHandler<string> OpenBibleBrowser;

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        #endregion Private Fields

        #region Public Constructors

        public BibleViewModel(List<BibleBook> bible, FadeInWriter fadeInWriter, HistoryViewModel historyViewModel)
        {
            Bible = new ObservableCollection<BibleBook>(bible);
            _fadeInWriter = fadeInWriter;
            _historyViewModel = historyViewModel;
            _bibleValidationViewModel = new BibleValidationViewModel(bible);
            _bibleValidationViewModel.ErrorsChanged += ErrorViewModel_ErrorChanged;
        }

        private void ErrorViewModel_ErrorChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(ButtonsEnable));
            OnPropertyChanged(nameof(EnableChapter));
            OnPropertyChanged(nameof(EnableVerse));
        }

        public bool TextBlockNotEmpty
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public bool ButtonsEnable
        {
            get
            {
                if (string.IsNullOrEmpty(SelectedChapter) ||
                    string.IsNullOrEmpty(SelectedBook) ||
                    string.IsNullOrEmpty(SelectedVerses) ||
                    HasErrors)
                    return false;
                return true;
            }
        }

        public bool EnableChapter
        {
            get
            {
                if (string.IsNullOrEmpty(SelectedBook) ||
                    _bibleValidationViewModel.PropertyHasError(nameof(SelectedBook)))
                    return false;
                return true;
            }
        }

        public bool EnableVerse
        {
            get
            {
                if (string.IsNullOrEmpty(SelectedBook) ||
                    _bibleValidationViewModel.PropertyHasError(nameof(SelectedBook)) ||
                    string.IsNullOrEmpty(SelectedChapter) ||
                    _bibleValidationViewModel.PropertyHasError(nameof(SelectedChapter)))

                    return false;
                return true;
            }
        }

        public string BibleText
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                if (!string.IsNullOrEmpty(value))
                {
                    TextBlockNotEmpty = true;
                    return;
                }
                TextBlockNotEmpty = false;
            }
        }

        private bool _studioMode;

        #endregion Public Constructors

        #region Public Properties

        public ObservableCollection<BibleBook> Bible { get; set; }

        #endregion Public Properties

        public ICommand RightButtonCommand => new RelayCommand(OnButtonRight);
        public ICommand LeftButtonCommand => new RelayCommand(OnButtonLeft);

        public ICommand ViewOnline => new RelayCommand(OpenBrowserDialog);
        public ICommand RemoveText => new RelayCommand(OnRemoveText);

        private void OnRemoveText(object obj)
        {
            BibleText = "";
        }

        private void OpenBrowserDialog(object obj)
        {
            var url = $"{Properties.Settings.Default.BibleServer}/{Properties.Settings.Default.BibleTranslation}/{SelectedBook}{SelectedChapter},{SelectedVerses}";
            System.Diagnostics.Process.Start(url);
            //OpenBibleBrowser?.Invoke(this, string.Empty);
        }

        public string SelectedChapter
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                if (!_bibleValidationViewModel.ValidateChapter(SelectedBook, value, nameof(SelectedChapter)))
                    return;

                if (!string.IsNullOrEmpty(SelectedVerses))
                    _bibleValidationViewModel.ValidateVerse(SelectedBook, value, SelectedVerses, nameof(SelectedVerses));

                OnPropertyChanged(nameof(ButtonsEnable));
                OnPropertyChanged(nameof(EnableChapter));
                OnPropertyChanged(nameof(EnableVerse));
            }
        }

        public string SelectedVerses
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                if (!_bibleValidationViewModel.ValidateVerse(SelectedBook, SelectedChapter, value, nameof(SelectedVerses)))
                    return;

                OnPropertyChanged(nameof(ButtonsEnable));
                OnPropertyChanged(nameof(EnableChapter));
                OnPropertyChanged(nameof(EnableVerse));
            }
        }

        public string SelectedBook
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                if (!_bibleValidationViewModel.ValidateBook(value, nameof(SelectedBook)))
                    return;

                if (!string.IsNullOrEmpty(SelectedChapter))
                    _bibleValidationViewModel.ValidateChapter(value, SelectedChapter, nameof(SelectedChapter));

                if (!string.IsNullOrEmpty(SelectedVerses))
                    _bibleValidationViewModel.ValidateVerse(value, SelectedChapter, SelectedVerses, nameof(SelectedVerses));

                OnPropertyChanged(nameof(ButtonsEnable));
                OnPropertyChanged(nameof(EnableChapter));
                OnPropertyChanged(nameof(EnableVerse));
            }
        }

        public string ButtonLeft
        {
            get
            {
                if (_studioMode) return "Erstellen";
                else return "Zurücksetzen";
            }
        }

        public string ButtonRight
        {
            get
            {
                if (_studioMode) return "Anzeigen";
                else return "Erstellen und Anzeigen";
            }
        }

        public bool HasErrors => _bibleValidationViewModel.HasErrors;

        public void UpdateButtons(bool studioMode)
        {
            _studioMode = studioMode;
            OnPropertyChanged("ButtonLeft");
            OnPropertyChanged("ButtonRight");
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _bibleValidationViewModel.GetErrors(propertyName);
        }

        private void OnButtonLeft(object obj)
        {
            if (_studioMode)
            {
                var bibleword = new BibleData();
                _historyViewModel.AddToHistory(bibleword);
                ClearView();
                return;
            }
            _fadeInWriter.ResetFade();
            _historyViewModel.SelectedIndex = -1;
        }

        private void ClearView()
        {
            SelectedVerses = string.Empty;
            SelectedBook = string.Empty;
            SelectedChapter = string.Empty;
            BibleText = string.Empty;

            _bibleValidationViewModel.ClearErrors(SelectedBook);
            _bibleValidationViewModel.ClearErrors(SelectedChapter);
            _bibleValidationViewModel.ClearErrors(SelectedVerses);
        }

        private void OnButtonRight(object obj)
        {
            var bibleword = new BibleData();

            if (!_studioMode)
            {
                _historyViewModel.AddToHistory(bibleword);
            }
            _fadeInWriter.WriteFade(bibleword);
            _historyViewModel.SelectFade(bibleword);
            ClearView();
        }
    }
}