﻿using HgSoftware.InsertCreator.Behaviors;
using HgSoftware.InsertCreator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Windows.Input;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class HistoryViewModel : ObservableObject
    {
        #region Private Fields

        private readonly FadeInWriter _fadeInWriter;

        #endregion Private Fields

        /// <summary>
        /// Returns the type of the item that can be dropped
        /// </summary>

        #region Public Constructors

        public HistoryViewModel(FadeInWriter fadeInWriter)
        {
            _fadeInWriter = fadeInWriter;
            HistoryViewSource.Source = History;
        }

        #endregion Public Constructors

        #region Public Properties

        public ICommand CreateCommand => new RelayCommand(OnCreateCommand);
        public ICommand DeleteCommand => new RelayCommand(OnDeleteElement);
        public ICommand DoubleClick => new RelayCommand(OnCreateCommand);

        public ObservableCollection<IInsertData> History { get; private set; } = new ObservableCollection<IInsertData>();

        /// <summary>
        /// Filtered Itemlist
        /// </summary>
        public ICollectionView HistoryView
        {
            get { return HistoryViewSource.View; }
        }

        public int ItemWith
        {
            get
            {
                if (History.Count > 12)
                    return 159;
                return 162;
            }
        }

        public ICommand ListKeyDownCommand => new RelayCommand(OnDeleteElement);
        public ICommand ResetCommand => new RelayCommand(OnReset);
        public ICommand SaveCommand => new RelayCommand(OnSaveElement);

        public int SelectedIndex
        {
            get { return GetValue<int>(); }
            set
            {
                SetValue(value);
            }
        }

        public IInsertData SelectedItem
        {
            get { return GetValue<IInsertData>(); }
            set
            {
                SetValue(value);
                OnPropertyChanged("ValidFlag");
            }
        }

        public bool ValidFlag
        {
            get
            {
                if (SelectedItem != null)
                {
                    return true;
                }
                return false;
            }
        }

        #endregion Public Properties

        #region Internal Properties

        /// <summary>
        /// Filtered Itemlist
        /// </summary>
        internal CollectionViewSource HistoryViewSource { get; set; } = new CollectionViewSource();

        #endregion Internal Properties

        #region Public Methods

        public void AddToHistory(IInsertData insertData)
        {
            OnPropertyChanged("ItemWith");

            if (!AlreadyExists(insertData))
            {
                History.Add(insertData);
                HistoryView.Refresh();
                SelectedItem = insertData;
            }
            else
            {
                SelectedItem = History.First(x => x.FirstLine == insertData.FirstLine && x.SecondLine == insertData.SecondLine);
                HistoryView.Refresh();
            }
        }

        public void SelectFade(IInsertData insert)
        {
            if (AlreadyExists(insert))
            {
                SelectedItem = History.First(x => x.FirstLine == insert.FirstLine && x.SecondLine == insert.SecondLine);
                HistoryView.Refresh();
                return;
            }
            SelectedIndex = -1;
        }

        #endregion Public Methods

        #region Private Methods

        private bool AlreadyExists(IInsertData insertData)
        {
            if (History.Any(x => x.FirstLine == insertData.FirstLine) && History.Any(x => x.SecondLine == insertData.SecondLine))
                return true;
            return false;
        }

        private void OnCreateCommand(object obj)
        {
            if (SelectedItem != null)
                _fadeInWriter.WriteFade(SelectedItem);
        }

        private void OnDeleteElement(object obj)
        {
            if (SelectedItem != null)
            {
                History.Remove(SelectedItem);
                HistoryView.Refresh();
                SelectedIndex = -1;
            }
        }

        private void OnReset(object obj)
        {
            _fadeInWriter.ResetFade();
            SelectedIndex = -1;
        }

        private void OnSaveElement(object obj)
        {
            _fadeInWriter.SaveFade(SelectedItem);
        }

        #endregion Private Methods
    }
}