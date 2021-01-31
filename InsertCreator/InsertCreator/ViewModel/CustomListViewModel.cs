using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class CustomListViewModel : ObservableObject
    {

        public CustomListViewModel()
        {
            TextLaneOne = "";
            TextLaneTwo = "";
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






    }
}
