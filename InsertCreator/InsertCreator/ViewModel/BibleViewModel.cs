using HgSoftware.InsertCreator.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using HgSoftware.InsertCreator.View.ValidationRules;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class BibleViewModel : ObservableObject
    {
       
        public ObservableCollection<BibleBook> Biblebooks { get; set; } = new ObservableCollection<BibleBook>();

        public List<string> BiblebookNames { get; set; } = new List<string>();

        public BibleViewModel(ObservableCollection<BibleBook> biblebooks)
        {
            Biblebooks = biblebooks;

            foreach (var item in Biblebooks)
            {
                BiblebookNames.Add(item.Name);

            }

            BibleBookView = new CollectionView(Biblebooks);
            BibleBookView.MoveCurrentTo(Biblebooks[0]);
            BibleBookView.CurrentChanged += new EventHandler(queries_CurrentChanged);

            ErrorsChanged += BibleViewModel_ErrorsChanged;


        }

        private void BibleViewModel_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
           
        }

        private void queries_CurrentChanged(object sender, EventArgs e)
        {
            BibleBook currentQuery = (BibleBook)BibleBookView.CurrentItem;
        }

        public CollectionView BibleBookView { get; private set; }


        public string SelectedValue
        {
            get { return GetValue<string>(); }
            set
            {
                SetValue(value);
                BibleBookView.Refresh();
            }
        }

        public BibleBook Selectedbook
        {
            get { return GetValue<BibleBook>(); }
            set { SetValue(value); }
        }


       
        public string SelectedChapter
        {
            get { return GetValue<string>(); }
            set 
            {
                ClearErrors(nameof(SelectedChapter));
                if (Convert.ToInt32(value) > 50)
                {
                    AddError(nameof(SelectedChapter), "Invalid value. The max product price is $50.00.");
                }


                SetValue(value);                
            }
        }


        public string SelectedVerses
        {

            get { return GetValue<string>(); }
            set 
            {
                //ValidateProperty(value);
                SetValue(value); 
            }
        }









    }
}
