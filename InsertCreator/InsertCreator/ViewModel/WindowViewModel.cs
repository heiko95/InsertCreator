using Liedeinblendung.Extensions;
using Liedeinblendung.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
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


               
            CurrentData = _gbData;
            License = File.ReadAllText(($"{Directory.GetCurrentDirectory()}/License.txt"));

        }


        public string Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string PublishDate
        {
            get
            {
                return File.GetCreationTime(Assembly.GetExecutingAssembly().Location).ToString().Split(' ')[0];
            }
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
