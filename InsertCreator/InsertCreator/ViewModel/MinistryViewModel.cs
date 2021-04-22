using HgSoftware.InsertCreator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class MinistryViewModel : ObservableObject
    {
        #region Private Fields

        private readonly FadeInWriter _fadeInWriter;
        private readonly HistoryViewModel _historyViewModel;
        private readonly MinistryJsonReaderWriter _readerWriter = new MinistryJsonReaderWriter($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Ministry.json");
        private bool _studioMode;

        #endregion Private Fields

        #region Public Constructors

        public MinistryViewModel(FadeInWriter fadeInWriter, HistoryViewModel historyViewModel)
        {
            _historyViewModel = historyViewModel;
            _fadeInWriter = fadeInWriter;
            Ministries = new ObservableCollection<MinistryGridViewModel>();

            if (_readerWriter.LoadMinistryData() != null)
                Ministries = _readerWriter.LoadMinistryData();

            UpdateFunctionList();

            MinistryViewSource.Source = Ministries;
            Ministries.CollectionChanged += CollectionChanged;
            MinistryViewSource.Filter += MinistryViewSource_Filter;
        }

        #endregion Public Constructors

        #region Public Properties

        public bool ValidFlagRight
        {
            get
            {
                if (SelectedItem == null)
                    return false;
                return true;
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
                if (SelectedItem == null)
                    return false;
                return true;
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

        public string FilterText
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                if (MinistryView != null)
                {
                    MinistryView.Refresh();
                    MinistryView.MoveCurrentToFirst();
                }
            }
        }

        public ICommand LeftButtonCommand => new RelayCommand(OnButtonLeft);

        public ObservableCollection<MinistryGridViewModel> Ministries
        {
            get
            {
                return GetValue<ObservableCollection<MinistryGridViewModel>>();
            }
            set
            {
                SetValue(value);
            }
        }

        /// <summary>
        /// Filtered List
        /// </summary>
        public ICollectionView MinistryView
        {
            get { return MinistryViewSource.View; }
        }

        public ICommand RightButtonCommand => new RelayCommand(OnButtonRight);
        public ICommand RowEditEndCommand => new RelayCommand(OnEditRowEnd);

        public MinistryGridViewModel SelectedItem
        {
            get { return GetValue<MinistryGridViewModel>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged("ValidFlagLeft");
                OnPropertyChanged("ValidFlagRight");
            }
        }

        public ObservableCollection<string> UsedFunctions { get; set; } = new ObservableCollection<string>();

        #endregion Public Properties

        #region Internal Properties

        /// <summary>
        /// Filtered List
        /// </summary>
        internal CollectionViewSource MinistryViewSource { get; set; } = new CollectionViewSource();

        #endregion Internal Properties

        #region Public Methods

        public void UpdateButtons(bool studioMode)
        {
            _studioMode = studioMode;
            OnPropertyChanged("ButtonLeft");
            OnPropertyChanged("ButtonRight");
            OnPropertyChanged("ValidFlagLeft");
            OnPropertyChanged("ValidFlagRight");
        }

        public void SaveMinistries()
        {
            _readerWriter.WriteMinistryData(Ministries);
        }


        #endregion Public Methods

        #region Internal Methods

        internal void Reset()
        {
            Ministries.Clear();
            MinistryViewSource.Source = Ministries;
            MinistryView.Refresh();
            _readerWriter.WriteMinistryData(Ministries);
        }

        internal void UpdateMinistries(ObservableCollection<MinistryGridViewModel> ministryList)
        {
            var addCount = 0;
            var updatedCount = 0;
            string addMessage = "";
            string updatedMessage = "";

            var tmpMinistryList = new List<MinistryGridViewModel>();
            tmpMinistryList.AddRange(Ministries);

            foreach (var item in ministryList)
            {
                if (!tmpMinistryList.Exists(x => x.FullName == item.FullName))
                {
                    Ministries.Add(item);
                    addCount++;
                }
                if (tmpMinistryList.Exists(x => x.FullName == item.FullName))
                {
                    var oldFunction = tmpMinistryList.Find(x => x.FullName == item.FullName).Function;

                    if (oldFunction != item.Function)
                    {
                        tmpMinistryList.Find(x => x.FullName == item.FullName).Function = item.Function;
                        updatedCount++;
                    }
                }
            }

            if (addCount == 1)
                addMessage = $"{addCount} Eintrag wurde zum Verzeichnis hinzugefügt";
            else
                addMessage = $"{addCount} Einträge wurden zum Verzeichnis hinzugefügt";

            if (updatedCount == 1)
                updatedMessage = $"{updatedCount} Eintrag wurde im Verzeichnis aktualisiert";
            else
                updatedMessage = $"{updatedCount} Einträge wurden im Verzeichnis aktualisiert";

            MessageBox.Show(addMessage + Environment.NewLine + updatedMessage, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //if (addCount == 1)
            //    MessageBox.Show($"{addCount} Eintrag wurde zum Verzeichnis hinzugefügt", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //else
            //    MessageBox.Show($"{addCount} Einträge wurden zum Verzeichnis hinzugefügt", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            UpdateFunctionList();
            MinistryViewSource.Source = Ministries;
            MinistryView.Refresh();
            _readerWriter.WriteMinistryData(Ministries);
        }

        #endregion Internal Methods

        #region Private Methods

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (MinistryGridViewModel item in e.NewItems)
                {
                    //Added items
                    item.OnUpdateFunction += UpdateElement;
                }

                return;
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (MinistryGridViewModel item in e.OldItems)
                {
                    //Removed items
                    item.OnUpdateFunction -= UpdateElement;
                    _readerWriter.WriteMinistryData(Ministries);
                }

                UpdateFunctionList();
                MinistryView.Refresh();
            }
        }

        private void MinistryViewSource_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                //no filter when no search text is entered
                e.Accepted = true;
            }
            else
            {
                MinistryGridViewModel p = (MinistryGridViewModel)e.Item;

                if (!string.IsNullOrEmpty(p.ForeName) && !string.IsNullOrEmpty(p.SureName) &&
                    p.SureName.ToLower().StartsWith(FilterText.ToLower())
                        || p.ForeName.ToLower().StartsWith(FilterText.ToLower())
                        || p.FullName.ToLower().StartsWith(FilterText.ToLower())
                        || p.FullName2.ToLower().StartsWith(FilterText.ToLower())
                        || p.FullName2.ToLower().Replace(",", "").StartsWith(FilterText.ToLower())
                        )
                {
                    e.Accepted = true;
                    return;
                }

                e.Accepted = false;
            }
        }

        private void OnButtonLeft(object obj)
        {
            if (_studioMode)
            {
                _historyViewModel.AddToHistory(SelectedItem);
                FilterText = "";
                SelectedItem = null;
                return;
            }

            _fadeInWriter.ResetFade();
            _historyViewModel.SelectedIndex = -1;
            SelectedItem = null;
        }

        private void OnButtonRight(object obj)
        {
            if (SelectedItem != null)
            {
                if (!_studioMode)
                {
                    _historyViewModel.AddToHistory(SelectedItem);
                }
                _fadeInWriter.WriteFade(SelectedItem);
                _historyViewModel.SelectFade(SelectedItem);
                FilterText = "";
                SelectedItem = null;
            }
        }

        private void OnEditRowEnd(object obj)
        {
            var item = obj as MinistryGridViewModel;
            if (item.ForeName == "" && item.SureName == "")
            {
                Ministries.Remove(item);
            }
        }

        private void UpdateElement(object sender, string e)
        {
            var newFunction = e;

            if (!string.IsNullOrEmpty(e) && !UsedFunctions.Contains(newFunction))
            {
                UsedFunctions.Add(newFunction);
            }
            
        }

        private void UpdateFunctionList()
        {
            UsedFunctions.Clear();
            foreach (var ministry in Ministries)
            {
                if (!string.IsNullOrEmpty(ministry.Function) && !UsedFunctions.Contains(ministry.Function))
                    UsedFunctions.Add(ministry.Function);
            }
        }

        #endregion Private Methods
    }
}