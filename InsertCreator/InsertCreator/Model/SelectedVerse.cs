using Liedeinblendung.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liedeinblendung.Model
{
    public class SelectedVerse : ObservableObject
    {
        public SelectedVerse(Verse verse)
        {
            Verse = verse;
            IsSelected = false;
        }

        public bool IsSelected
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public Verse Verse
        {
            get { return GetValue<Verse>(); }
            set { SetValue(value); }
        }


    }
}
