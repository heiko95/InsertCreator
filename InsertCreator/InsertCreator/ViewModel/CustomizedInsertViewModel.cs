using HgSoftware.InsertCreator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace HgSoftware.InsertCreator.ViewModel
{
   public class CustomizedInsertViewModel : ObservableObject
    {
        #region Private Fields

        private FadeInWriter _fadeInWriter;

        private CustomInsertJasonReaderWriter _readerWriter = new CustomInsertJasonReaderWriter($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.json");

        #endregion Private Fields

        #region Public Constructors

        public CustomizedInsertViewModel(FadeInWriter fadeInWriter)
        {
            _fadeInWriter = fadeInWriter;
            CustomInserts = new List<CustomListViewModel>();
            LoadInserts();            
            CustomInsertViewSource.Source = CustomInserts;
        }

        public void LoadInserts()
        {
            CustomInserts = _readerWriter.LoadMinistryData();
        }

        public void SaveInserts()
        {
            _readerWriter.WriteMinistryData(CustomInserts);
        }


        #endregion Public Constructors

        #region Public Properties

        public ICommand AcceptCommand => new RelayCommand(OnAcceptPressed);



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

        public ICommand ListKeyDownCommand => new RelayCommand(OnListKeyDown);

        public ICommand ResetCommand => new RelayCommand(OnResetPressed);

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
            }
        }

        public string TextLaneTwo
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
            }
        }

        #endregion Public Properties

        #region Internal Properties

        internal CollectionViewSource CustomInsertViewSource { get; set; } = new CollectionViewSource();

        #endregion Internal Properties

        #region Private Methods

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

        private void OnAcceptPressed(object obj)
        {
            _fadeInWriter.WriteCustom(TextLaneOne, TextLaneTwo);

            if (SelectedItem != null)
            {
                SelectedItem.TextLaneOne = TextLaneOne;
                SelectedItem.TextLaneTwo = TextLaneTwo;
                CustomInsertView.Refresh();
                TextLaneOne = "";
                TextLaneTwo = "";
                return;
            }

            AddToList();


        }
        private void OnListKeyDown(object obj)
        {
            if (SelectedItem != null)
            {
                CustomInserts.Remove(SelectedItem);
                CustomInsertView.Refresh();
                TextLaneOne = "";
                TextLaneTwo = "";                
            }
        }
        private void OnResetPressed(object obj)
        {
            _fadeInWriter.ResetFade();
        }

        #endregion Private Methods
    }
}
