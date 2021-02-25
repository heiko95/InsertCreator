using HgSoftware.InsertCreator.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class CustomizedInsertViewModel : ObservableObject
    {
        #region Private Fields

        private readonly FadeInWriter _fadeInWriter;

        private readonly HistoryViewModel _historyViewModel;
        private readonly CustomInsertJasonReaderWriter _readerWriter = new CustomInsertJasonReaderWriter($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.json");
        private bool _studioMode;

        #endregion Private Fields

        #region Public Constructors

        public CustomizedInsertViewModel(FadeInWriter fadeInWriter, HistoryViewModel historyViewModel)
        {
            _fadeInWriter = fadeInWriter;
            _historyViewModel = historyViewModel;
            CustomInserts = new List<CustomListViewModel>();
            LoadInserts();
            CustomInsertViewSource.Source = CustomInserts;
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

        public List<CustomListViewModel> CustomInserts
        {
            get
            {
                return GetValue<List<CustomListViewModel>>();
            }
            set
            {
                SetValue(value);
            }
        }

        /// <summary>
        /// Filtered List
        /// </summary>
        public ICollectionView CustomInsertView
        {
            get { return CustomInsertViewSource.View; }
        }

        public ICommand LeftButtonCommand => new RelayCommand(OnButtonLeft);

        public ICommand ListKeyDownCommand => new RelayCommand(OnListKeyDown);

        public ICommand RightButtonCommand => new RelayCommand(OnButtonRight);

        public int SelectedIndex
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }

        public CustomListViewModel SelectedItem
        {
            get { return GetValue<CustomListViewModel>(); }
            set
            {
                SetValue(value);

                if (value != null)
                {
                    TextLaneOne = value.TextLaneOne;
                    TextLaneTwo = value.TextLaneTwo;
                }
            }
        }

        public string TextLaneOne
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged("ValidFlagLeft");
                OnPropertyChanged("ValidFlagRight");
            }
        }

        public string TextLaneTwo
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged("ValidFlagLeft");
                OnPropertyChanged("ValidFlagRight");
            }
        }

        public bool ValidFlagLeft
        {
            get
            {
                if (!_studioMode)
                {
                    return true;
                }
                if (string.IsNullOrEmpty(TextLaneOne) && string.IsNullOrEmpty(TextLaneTwo))
                    return false;
                return true;
            }
        }

        public bool ValidFlagRight
        {
            get
            {
                if (string.IsNullOrEmpty(TextLaneOne) && string.IsNullOrEmpty(TextLaneTwo))
                    return false;
                return true;
            }
        }

        #endregion Public Properties

        #region Internal Properties

        internal CollectionViewSource CustomInsertViewSource { get; set; } = new CollectionViewSource();

        #endregion Internal Properties

        #region Public Methods

        public void LoadInserts()
        {
            CustomInserts = _readerWriter.LoadMinistryData();
        }

        public void SaveInserts()
        {
            _readerWriter.WriteMinistryData(CustomInserts);
        }

        public void UpdateButtons(bool studioMode)
        {
            _studioMode = studioMode;
            OnPropertyChanged("ButtonLeft");
            OnPropertyChanged("ButtonRight");
            OnPropertyChanged("ValidFlagLeft");
            OnPropertyChanged("ValidFlagRight");
        }

        #endregion Public Methods

        #region Private Methods

        private void AddFade()
        {
            if (SelectedItem != null)
            {
                SelectedItem.TextLaneOne = TextLaneOne;
                SelectedItem.TextLaneTwo = TextLaneTwo;
                CustomInsertView.Refresh();
                TextLaneOne = "";
                TextLaneTwo = "";
                SelectedIndex = -1;
                return;
            }
            AddToList();
        }

        private void AddToList()
        {
            if (!(CustomInserts.Exists(x => x.TextLaneOne == TextLaneOne) && CustomInserts.Exists(y => y.TextLaneTwo == TextLaneTwo)))
            {
                CustomInserts.Add(new CustomListViewModel() { TextLaneOne = TextLaneOne, TextLaneTwo = TextLaneTwo });
                CustomInsertView.Refresh();
                SelectedIndex = -1;
            }
            TextLaneOne = "";
            TextLaneTwo = "";
        }

        private void OnButtonLeft(object obj)
        {
            if (_studioMode)
            {
                var fade = new CustomInsert(TextLaneOne, TextLaneTwo);
                _historyViewModel.AddToHistory(fade);
                AddFade();
                return;
            }

            _fadeInWriter.ResetFade();
            SelectedIndex = -1;
            _historyViewModel.SelectedIndex = -1;
            TextLaneOne = "";
            TextLaneTwo = "";
        }

        private void OnButtonRight(object obj)
        {
            var fade = new CustomInsert(TextLaneOne, TextLaneTwo);

            if (!_studioMode)
            {
                _historyViewModel.AddToHistory(fade);
            }
            _fadeInWriter.WriteFade(fade);
            _historyViewModel.SelectFade(fade);

            AddFade();
        }

        private void OnListKeyDown(object obj)
        {
            if (SelectedItem != null)
            {
                CustomInserts.Remove(SelectedItem);
                CustomInsertView.Refresh();
                SelectedIndex = -1;
                TextLaneOne = "";
                TextLaneTwo = "";
            }
        }

        #endregion Private Methods
    }
}