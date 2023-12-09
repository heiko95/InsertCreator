using HgSoftware.InsertCreator.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class BibleViewModel : ObservableObject, INotifyDataErrorInfo
    {
        #region Private Fields

        private readonly BibleTextService _bibleTextService = new BibleTextService($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Luther2017.csv");
        private readonly BibleValidationViewModel _bibleValidationViewModel;
        private readonly FadeInWriter _fadeInWriter;
        private readonly HistoryViewModel _historyViewModel;
        private bool _studioMode;

        #endregion Private Fields

        #region Public Constructors

        public BibleViewModel(List<BibleBook> bible, FadeInWriter fadeInWriter, HistoryViewModel historyViewModel)
        {
            Bible = new ObservableCollection<BibleBook>(bible);
            _fadeInWriter = fadeInWriter;
            _historyViewModel = historyViewModel;
            _bibleValidationViewModel = new BibleValidationViewModel(bible);
            _bibleValidationViewModel.ErrorsChanged += ErrorViewModel_ErrorChanged;

            _bibleTextService.ReadBible();
        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        #endregion Public Events

        #region Public Properties

        public ObservableCollection<BibleBook> Bible { get; set; }

        public string BibleText
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                if (!string.IsNullOrEmpty(value))
                    TextBlockNotEmpty = true;
                else
                    TextBlockNotEmpty = false;

                if (!_bibleValidationViewModel.ValidateBibleText(value, GetVerselist(SelectedVerses), nameof(BibleText)))
                    return;
                OnPropertyChanged(nameof(ButtonsEnable));
                OnPropertyChanged(nameof(ResearchEnable));
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

        public bool ButtonsEnable
        {
            get
            {
                if (_bibleValidationViewModel.PropertyHasError(nameof(BibleText)) || !ResearchEnable)
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

        public bool HasErrors => _bibleValidationViewModel.HasErrors;

        public ICommand LeftButtonCommand => new RelayCommand(OnButtonLeft);

        public ICommand OnLoadBibletext => new RelayCommand(LoadBibleText);
        public ICommand RemoveText => new RelayCommand(OnRemoveText);

        public bool ResearchEnable
        {
            get
            {
                if (PropertyIsEmptyOrHasError(nameof(SelectedChapter), SelectedChapter) ||
                    PropertyIsEmptyOrHasError(nameof(SelectedBook), SelectedBook) ||
                    PropertyIsEmptyOrHasError(nameof(SelectedVerses), SelectedVerses))

                    return false;
                return true;
            }
        }

        public ICommand RightButtonCommand => new RelayCommand(OnButtonRight);

        public string SelectedBook
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                if (!_bibleValidationViewModel.ValidateBook(value, nameof(SelectedBook)))
                    return;

                if (!string.IsNullOrEmpty(SelectedChapter))
                    if (_bibleValidationViewModel.ValidateChapter(value, SelectedChapter, nameof(SelectedChapter)))
                        if (!string.IsNullOrEmpty(SelectedVerses) && !_bibleValidationViewModel.PropertyHasError(SelectedChapter))
                            _bibleValidationViewModel.ValidateVerse(value, SelectedChapter, SelectedVerses, nameof(SelectedVerses));

                OnPropertyChanged(nameof(ButtonsEnable));
                OnPropertyChanged(nameof(ResearchEnable));
                OnPropertyChanged(nameof(EnableChapter));
                OnPropertyChanged(nameof(EnableVerse));
            }
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
                OnPropertyChanged(nameof(ResearchEnable));
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
                {
                    _bibleValidationViewModel.ValidateBibleText(BibleText, GetVerselist(SelectedVerses), nameof(BibleText));
                    OnPropertyChanged(nameof(ButtonsEnable));
                    OnPropertyChanged(nameof(ResearchEnable));
                    return;
                }
                if (!string.IsNullOrEmpty(value))
                {
                    _bibleValidationViewModel.ValidateBibleText(BibleText, GetVerselist(SelectedVerses), nameof(BibleText));
                }
                OnPropertyChanged(nameof(ButtonsEnable));
                OnPropertyChanged(nameof(ResearchEnable));
                OnPropertyChanged(nameof(EnableChapter));
                OnPropertyChanged(nameof(EnableVerse));
            }
        }

        public bool TextBlockNotEmpty
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        #endregion Public Properties

        #region Public Methods

        public IEnumerable GetErrors(string propertyName)
        {
            return _bibleValidationViewModel.GetErrors(propertyName);
        }

        public void UpdateButtons(bool studioMode)
        {
            _studioMode = studioMode;
            OnPropertyChanged("ButtonLeft");
            OnPropertyChanged("ButtonRight");
        }

        #endregion Public Methods

        #region Private Methods

        private void ClearView()
        {
            SelectedVerses = string.Empty;
            SelectedChapter = string.Empty;
            SelectedBook = string.Empty;

            BibleText = string.Empty;

            _bibleValidationViewModel.ClearErrors(SelectedBook);
            _bibleValidationViewModel.ClearErrors(SelectedChapter);
            _bibleValidationViewModel.ClearErrors(SelectedVerses);
        }

        private void ErrorViewModel_ErrorChanged(object sender, DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
            OnPropertyChanged(nameof(ButtonsEnable));
            OnPropertyChanged(nameof(ResearchEnable));
            OnPropertyChanged(nameof(EnableChapter));
            OnPropertyChanged(nameof(EnableVerse));
        }

        private List<int> GetVerselist(string verses)
        {
            var result = new List<int>();

            if (string.IsNullOrEmpty(verses) || _bibleValidationViewModel.PropertyHasError(nameof(SelectedVerses)))
                return result;

            var sections = verses.Split(';').ToList();

            foreach (var section in sections)
            {
                var innerSections = section.Split('.').ToList();

                foreach (var innerSection in innerSections)
                {
                    var inner = innerSection.Split('-').ToList();

                    if (inner.Count == 1)
                        result.Add(int.Parse(inner[0]));
                    else
                    {
                        var first = int.Parse(inner[0]);
                        var second = int.Parse(inner[1]);
                        var count = second - first;
                        var state = first;
                        while (count >= 0)
                        {
                            result.Add(state);
                            state++;
                            count--;
                        }
                    }
                }
            }

            return result;
        }

        private void LoadBibleText(object obj)
        {
            var sb = new StringBuilder();
            foreach (var verse in GetVerselist(SelectedVerses))
            {
                var bibleText = _bibleTextService.GetBibleText(SelectedBook, Convert.ToInt32(SelectedChapter), verse);
                if (string.IsNullOrEmpty(bibleText)) continue;
                sb.Append(verse);
                sb.Append(" ");
                sb.Append(bibleText);
                sb.Append(" ");
            }
            var text = sb.ToString();

            if (!string.IsNullOrEmpty(text))
            {
                BibleText = text;
                return;
            }

            var url = $"{Properties.Settings.Default.BibleServer}/{Properties.Settings.Default.BibleTranslation}/{SelectedBook}{SelectedChapter},{SelectedVerses}";
            System.Diagnostics.Process.Start(url);
        }

        private void OnButtonLeft(object obj)
        {
            if (_studioMode)
            {
                var bibleword = new BibleData(SelectedBook, SelectedChapter, SelectedVerses, BibleText);
                _historyViewModel.AddToHistory(bibleword);
                ClearView();
                return;
            }
            _fadeInWriter.ResetFade();
            _historyViewModel.SelectedIndex = -1;
        }

        private void OnButtonRight(object obj)
        {
            var bibleword = new BibleData(SelectedBook, SelectedChapter, SelectedVerses, BibleText);

            if (!_studioMode)
            {
                _historyViewModel.AddToHistory(bibleword);
            }
            _fadeInWriter.WriteFade(bibleword);
            _historyViewModel.SelectFade(bibleword);
            ClearView();
        }

        private void OnRemoveText(object obj)
        {
            BibleText = "";
        }

        private bool PropertyIsEmptyOrHasError(string propertyName, string value)
        {
            if (string.IsNullOrEmpty(value) || _bibleValidationViewModel.PropertyHasError(propertyName))
                return true;
            return false;
        }

        #endregion Private Methods
    }
}