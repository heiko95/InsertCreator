using Liedeinblendung.Model;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;

namespace Liedeinblendung.ViewModel
{
    public class MinistryViewModel : ObservableObject
    {
        #region Private Fields

        private FadeInWriter _fadeInWriter = new FadeInWriter();
        private MinistryJsonReaderWriter _readerWriter = new MinistryJsonReaderWriter($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Ministry.json");

        #endregion Private Fields

        #region Public Constructors

        public MinistryViewModel()
        {
            Ministries = new ObservableCollection<MinistryGridViewModel>();

            if (_readerWriter.LoadMinistryData() != null)
                Ministries = _readerWriter.LoadMinistryData();

            foreach (var ministry in Ministries)
            {
                if (!UsedFunctions.Contains(ministry.Function))
                    UsedFunctions.Add(ministry.Function);
            }

            MinistryViewSource.Source = Ministries;
            Ministries.CollectionChanged += CollectionChanged;
            MinistryViewSource.Filter += MinistryViewSource_Filter;
        }

        #endregion Public Constructors

        #region Public Properties

        public ICommand AcceptCommand => new RelayCommand(OnAcceptPressed);

        public string FilterText
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                if (MinistryView != null)
                {
                    try
                    {
                        MinistryView.Refresh();
                        MinistryView.MoveCurrentToFirst();
                    }
                    catch
                    {
                    }
                }
            }
        }

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

        public MinistryGridViewModel SelectedItem
        {
            get { return GetValue<MinistryGridViewModel>(); }
            set
            {
                SetValue(value);
            }
        }

        //}
        public ObservableCollection<string> UsedFunctions { get; set; } = new ObservableCollection<string>();

        #endregion Public Properties

        #region Internal Properties

        /// <summary>
        /// Filtered List
        /// </summary>
        internal CollectionViewSource MinistryViewSource { get; set; } = new CollectionViewSource();

        #endregion Internal Properties

        #region Internal Methods

        internal void UpdateMinistries(ObservableCollection<MinistryGridViewModel> ministryList)
        {
            var count = 0;
            foreach (var item in ministryList)
            {
                if (!Ministries.Contains(item))
                {
                    Ministries.Add(item);
                    count++;
                }
            }
            MessageBox.Show($"{count} Einträge wurden zum Verzeichnis hinzugefügt", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            MinistryViewSource.Source = Ministries;
            MinistryView.Refresh();
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
                }

                return;
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

                if (!string.IsNullOrEmpty(p.ForeName) && !string.IsNullOrEmpty(p.SureName))
                {
                    if (p.SureName.ToLower().StartsWith(FilterText.ToLower())
                        || p.ForeName.ToLower().StartsWith(FilterText.ToLower())
                        || p.FullName.ToLower().StartsWith(FilterText.ToLower())
                        || p.FullName2.ToLower().StartsWith(FilterText.ToLower())
                        || p.FullName2.ToLower().Replace(",", "").StartsWith(FilterText.ToLower())
                        )
                    {
                        e.Accepted = true;
                        return;
                    }
                }
                e.Accepted = false;
            }
        }

        private void OnAcceptPressed(object obj)
        {
            if (SelectedItem != null)
            {
                _fadeInWriter.WriteMinistryFade(SelectedItem);
                FilterText = "";
            }
        }

        private void UpdateElement(object sender, string e)
        {
            var newFunction = e;
            if (!UsedFunctions.Contains(newFunction))
            {
                UsedFunctions.Add(newFunction);
            }
            _readerWriter.WriteMinistryData(Ministries);
        }

        #endregion Private Methods
    }
}