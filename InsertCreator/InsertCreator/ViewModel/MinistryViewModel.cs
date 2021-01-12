using Liedeinblendung.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Liedeinblendung.ViewModel
{
    public class MinistryViewModel : ObservableObject
    {
        private MinistryJsonReaderWriter _readerWriter = new MinistryJsonReaderWriter($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Ministry.json");
        private FadeInWriter _fadeInWriter = new FadeInWriter();


        public ICommand AcceptCommand => new RelayCommand(OnAcceptPressed);

        private void OnAcceptPressed(object obj)
        {
            if (SelectedItem != null)
                _fadeInWriter.CreateMinistrieInsert(SelectedItem);
        }

        public MinistryViewModel()
        {
            Ministries  = new ObservableCollection<MinistryGridViewModel>();

            Ministries = _readerWriter.LoadMinistryData();

            foreach (var ministry in Ministries)
            {
                if (!UsedFunctions.Contains(ministry.Function))
                    UsedFunctions.Add(ministry.Function);
            }


            MinistryViewSource.Source = Ministries;
            Ministries.CollectionChanged += CollectionChanged();
            MinistryViewSource.Filter += MinistryViewSource_Filter;
        }

        private NotifyCollectionChangedEventHandler CollectionChanged()
        {
            return new NotifyCollectionChangedEventHandler(CollectionChangeEvents);
        }

        private void CollectionChangeEvents(object sender, NotifyCollectionChangedEventArgs e)
        {
            _readerWriter.WriteMinistryData(Ministries);
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (MinistryGridViewModel item in e.NewItems)
                {
                    //Added items
                    item.OnUpdateFunction += UpdateFunctionList;
                }               
                return;
            }
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (MinistryGridViewModel item in e.OldItems)
                {
                    //Removed items
                    item.OnUpdateFunction -= UpdateFunctionList;
                }                
                return;
            }         

        }

        private void UpdateFunctionList(object sender, string e)
        {
            var newFunction = e;
            if (!UsedFunctions.Contains(newFunction))
            {
                UsedFunctions.Add(newFunction);
            }
            UsedFunctions.Sort();
            InvokePropertyChanged("UsedFunctions");
            //MinistryView.Refresh();
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
                if (p.SureName.ToLower().StartsWith(FilterText.ToLower()) || p.ForeName.ToLower().StartsWith(FilterText.ToLower()))
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
        }

        public string FilterText
        {
            get { return GetValue<string>(); }
            set { SetValue(value);
                MinistryView.Refresh();
            }
        }


        public MinistryGridViewModel SelectedItem
        {
            get { return GetValue<MinistryGridViewModel>(); }
            set { SetValue(value); }
        }




        /// <summary>
        /// Filtered List
        /// </summary>
        internal CollectionViewSource MinistryViewSource { get; set; } = new CollectionViewSource();

        /// <summary>
        /// Filtered List
        /// </summary>
        public ICollectionView MinistryView
        {
            get { return MinistryViewSource.View; }
        }



        private ListChangedEventHandler OnCollectionChanged()
        {
            return new ListChangedEventHandler(change);
        }

        void change(object sender, ListChangedEventArgs e)
        {
            //var newFunction = Ministries[e.NewIndex].Function;
            //if (!UsedFunctions.Contains(newFunction))
            //{
            //    UsedFunctions.Add(newFunction);
            //}
            //UsedFunctions.Sort();
            //InvokePropertyChanged("UsedFunctions");
            //MinistryView.Refresh();
        }


        //private void RemoveUnusedFunctions()
        //{
        //    List<string> tmpList = new List<string>();
        //    List<string> usedFunctions = new List<string>();
        //    foreach (var ministry in Ministries)
        //    {
        //        tmpList.Add(ministry.Function);
        //    }
        //    foreach (var function in UsedFunctions)
        //    {
        //        if (tmpList.Contains(function))
        //        {
        //            usedFunctions.Add(function);
        //        }
        //    }


        //    UsedFunctions.Clear();
        //    UsedFunctions = usedFunctions;

        //}

        public ObservableCollection<MinistryGridViewModel> Ministries 
        {
            get { return GetValue<ObservableCollection<MinistryGridViewModel>>(); }
            set { SetValue(value); 
            }
        } 
                  

        public List<string> UsedFunctions { get; set; } = new List<string>();
    }
}
