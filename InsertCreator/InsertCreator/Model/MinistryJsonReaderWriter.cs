using System.Collections.ObjectModel;
using System.IO;
using HgSoftware.InsertCreator.ViewModel;
using Newtonsoft.Json;

namespace HgSoftware.InsertCreator.Model
{
    public class MinistryJsonReaderWriter
    {
        #region Private Fields

        private string _path;

        #endregion Private Fields

        #region Public Constructors

        public MinistryJsonReaderWriter(string path)
        {
            _path = path;
        }

        #endregion Public Constructors

        #region Public Methods

        public ObservableCollection<MinistryGridViewModel> LoadMinistryData()
        {
            var ministries = new ObservableCollection<MinistryGridViewModel>();

            var ministrytext = File.ReadAllText(_path);

            if (!string.IsNullOrEmpty(ministrytext))
            {
                var tmpMinistries = JsonConvert.DeserializeObject<ObservableCollection<MinistryGridViewModel>>(ministrytext);

                foreach (var ministrie in tmpMinistries)
                {
                    if (ministrie.SureName != null && ministrie.ForeName != null)
                    {
                        ministries.Add(ministrie);
                    }
                }
            }

            return ministries;
        }

        public void WriteMinistryData(ObservableCollection<MinistryGridViewModel> ministries)
        {
            File.WriteAllText(_path, JsonConvert.SerializeObject(ministries));
        }

        #endregion Public Methods
    }
}