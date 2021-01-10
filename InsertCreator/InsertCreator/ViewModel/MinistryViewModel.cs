using Liedeinblendung.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
            MinistrieNames.Add("Horst Peterson");
            MinistrieNames.Add("ff sgsd");

        }

        public ObservableCollection<Ministry> Ministries { get; set; } = new ObservableCollection<Ministry>();

        public ObservableCollection<string> MinistrieNames { get; set; } = new ObservableCollection<string>();
    }
}
