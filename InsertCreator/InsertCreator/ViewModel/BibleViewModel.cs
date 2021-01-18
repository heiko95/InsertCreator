using HgSoftware.InsertCreator.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class BibleViewModel : ObservableObject
    {

        public ObservableCollection<BibleBook> Biblebooks { get; set; } = new ObservableCollection<BibleBook>();

        public List<String> BiblebookNames { get; set; } = new List<String>();

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
            
        }

        private void queries_CurrentChanged(object sender, EventArgs e)
        {
            BibleBook currentQuery = (BibleBook)BibleBookView.CurrentItem;
        }





        public CollectionView BibleBookView { get; private set; }


        private void MinistryViewSource_Filter(object sender, FilterEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SelectedValue))
            {
                //no filter when no search text is entered
                e.Accepted = true;
            }
            else
            {
                BibleBook p = (BibleBook)e.Item;
                               
                    if (p.Name.ToLower().Contains(SelectedValue.ToLower()))          
                    {
                        e.Accepted = true;
                        return;
                    }
                
                e.Accepted = false;
            }
        }

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

        public int SelectedChapter
        {
            get { return GetValue<int>(); }
            set { SetValue(value); }
        }

        public string SelectedVerses
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }



    



    }
}
