using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liedeinblendung.ViewModel;

namespace Liedeinblendung.ViewModel
{    
    public class MinistryGridViewModel : ObservableObject
    {

        public event EventHandler<string> OnUpdateFunction; 


        public string Function 
        {
            get { return GetValue<string>(); }
            set 
            {
                SetValue(value);
                OnUpdateFunction?.Invoke(this, value);
            }                           
        }

        public string ForeName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string SureName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
       
    }
}
