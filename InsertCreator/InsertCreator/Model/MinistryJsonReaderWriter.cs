using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Liedeinblendung.ViewModel;
using Newtonsoft.Json;

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

            ministries = JsonConvert.DeserializeObject<ObservableCollection<MinistryGridViewModel>>(o1);

            return ministries;
        }


        public void WriteMinistryData(ObservableCollection<MinistryGridViewModel> ministries)
        {
            File.WriteAllText(_path, JsonConvert.SerializeObject(ministries));            
        }


    }
}
