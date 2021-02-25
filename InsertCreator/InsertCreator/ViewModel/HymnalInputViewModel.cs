using HgSoftware.InsertCreator.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class HymnalInputViewModel : ObservableObject
    {
        #region Private Fields

        private readonly string _bookname;

        private readonly List<Song> _currentHymnal;
        private readonly FadeInWriter _fadeInWriter;
        private readonly HistoryViewModel _historyViewModel;

        private bool _studioMode;

        #endregion Private Fields

        #region Public Constructors

        public HymnalInputViewModel(List<Song> hymnalList, string bookname, FadeInWriter fadeInWriter, HistoryViewModel historyViewModel)
        {
            VerseList = new ObservableCollection<SelectedVerse>();
            _historyViewModel = historyViewModel;
            _fadeInWriter = fadeInWriter;
            _currentHymnal = hymnalList;
            _bookname = bookname;
        }

        #endregion Public Constructors

        #region Public Properties

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

        public string InputNumber
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                CheckValid(value);
            }
        }

        public string InputText
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string InputVers
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public ICommand LeftButtonCommand => new RelayCommand(OnButtonLeft);

        public string MelodieAutor
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public bool NumberValidFlag
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public ICommand RightButtonCommand => new RelayCommand(OnButtonRight);

        public List<int> SelectedVerses
        {
            get { return GetValue<List<int>>(); }
            set { SetValue(value); }
        }

        public string TextAutor
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public bool ValidFlag
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public ObservableCollection<SelectedVerse> VerseList
        {
            get { return GetValue<ObservableCollection<SelectedVerse>>(); }
            set { SetValue(value); }
        }

        #endregion Public Properties

        #region Public Methods

        public void UpdateButtons(bool studioMode)
        {
            _studioMode = studioMode;
            OnPropertyChanged("ButtonLeft");
            OnPropertyChanged("ButtonRight");
        }

        #endregion Public Methods

        #region Private Methods

        private void CheckValid(string number)
        {
            if (_currentHymnal.Exists(x => x.Number == number))
            {
                NumberValidFlag = true;
                ValidFlag = true;

                CreateFade(number);
                return;
            }

            if (_currentHymnal.Exists(x => x.Number == $"{number}a"))
            {
                NumberValidFlag = true;
                ValidFlag = true;
                InputNumber = $"{number}a";
                CreateFade(InputNumber);
                return;
            }

            ValidFlag = !_studioMode;
            NumberValidFlag = false;
            InputText = "";
            VerseList.Clear();
            MelodieAutor = "";
            TextAutor = "";
        }

        private void ClearView()
        {
            InputNumber = "";
            InputVers = "";
        }

        private void CreateFade(string number)
        {
            Song current = _currentHymnal.Find(x => x.Number == number);

            VerseList.Clear();
            InputText = current.Title;

            foreach (var verse in current.Verses)
            {
                VerseList.Add(new SelectedVerse(verse));
            }

            if (current.Metadata.Exists(x => x.Key == "Text"))
                TextAutor = $" Text: {current.Metadata.Find(x => x.Key == "Text").Value}";
            else
                TextAutor = "";

            if (current.Metadata.Exists(x => x.Key == "Melodie"))
                MelodieAutor = $" Melodie: {current.Metadata.Find(x => x.Key == "Melodie").Value}";
            else if
                 (current.Metadata.Exists(x => x.Key == "Musik"))
                MelodieAutor = $" Musik: {current.Metadata.Find(x => x.Key == "Musik").Value}";
            else
                MelodieAutor = "";
        }

        private void OnButtonLeft(object obj)
        {
            if (_studioMode)
            {
                var hymnal = new HymnalData(_bookname, InputNumber, InputText, MelodieAutor, TextAutor, VerseList);
                _historyViewModel.AddToHistory(hymnal);
                ClearView();
                return;
            }
            _fadeInWriter.ResetFade();
            _historyViewModel.SelectedIndex = -1;
        }

        private void OnButtonRight(object obj)
        {
            var hymnal = new HymnalData(_bookname, InputNumber, InputText, MelodieAutor, TextAutor, VerseList);

            if (!_studioMode)
            {
                _historyViewModel.AddToHistory(hymnal);
            }
            _fadeInWriter.WriteFade(hymnal);
            _historyViewModel.SelectFade(hymnal);
            ClearView();
        }

        #endregion Private Methods
    }
}