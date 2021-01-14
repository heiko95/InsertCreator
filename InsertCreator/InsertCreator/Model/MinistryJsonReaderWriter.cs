using Liedeinblendung.ViewModel;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace Liedeinblendung.Model
{
    public class MinistryJsonReaderWriter
    {
        private string _path;

        public MinistryJsonReaderWriter(string path)
        {
            _path = path;
        }

        public ObservableCollection<MinistryGridViewModel> LoadMinistryData()
        {
            var ministries = new ObservableCollection<MinistryGridViewModel>();

            var o1 = File.ReadAllText(_path);

            var tmpMinistries = JsonConvert.DeserializeObject<ObservableCollection<MinistryGridViewModel>>(o1);

            foreach (var ministrie in tmpMinistries)
            {
                if (ministrie.SureName != null && ministrie.ForeName != null)
                {
                    ministries.Add(ministrie);
                }
            }

            return ministries;
        }

        public void WriteMinistryData(ObservableCollection<MinistryGridViewModel> ministries)
        {
            File.WriteAllText(_path, JsonConvert.SerializeObject(ministries));
        }
    }
}