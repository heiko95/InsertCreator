using Liedeinblendung.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Liedeinblendung.ViewModel
{
    public class WindowViewModel : ObservableObject
    {
        public WindowViewModel()
        {
            _gbData = new MainViewModel(_hymnalJsonReader.LoadHymnalData(($"{Directory.GetCurrentDirectory()}/DataSource/GB_Data.json")), "Gesangbuch");
            _cbData = new MainViewModel(_hymnalJsonReader.LoadHymnalData(($"{Directory.GetCurrentDirectory()}/DataSource/CB_Data.json")), "Chorbuch");

            string path = $"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
               
                File.Create($"{path}/FadeText.txt", 1024);
                File.Create($"{path}/FadeTextMeta.txt", 1024);                      
            }
               
            CurrentData = _gbData;
            License = File.ReadAllText(($"{Directory.GetCurrentDirectory()}/License.txt"));

        }



        public string License
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public int Selected
        {
            get { return GetValue<int>(); }
            set 
            { 
                SetValue(value);

                if (value == 1)
                {
                    CurrentData = _cbData;
                    return;
                }
                CurrentData = _gbData; 
            }
        }

        public MainViewModel CurrentData 
        { 
            get { return GetValue<MainViewModel>(); }
            set { SetValue(value); }

        }

        private readonly MainViewModel _gbData;
        private readonly MainViewModel _cbData;
        private readonly HymnalJsonReader _hymnalJsonReader = new HymnalJsonReader();










    }
}
