using Liedeinblendung.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liedeinblendung.ViewModel
{
    public class MinistryViewModel : ObservableObject
    {

        public MinistryViewModel()
        {
            Ministries.Add(new Ministry(){Function = "Di", ForeName =  "Horst", SureName = "Häußermann" });
            Ministries.Add(new Ministry() { Function = "U.Di", ForeName = "Mischa", SureName = "Häußermann" });
        }

        public ObservableCollection<Ministry> Ministries { get; set; } = new ObservableCollection<Ministry>();

    }
}
