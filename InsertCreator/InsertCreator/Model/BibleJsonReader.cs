using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;

namespace HgSoftware.InsertCreator.Model
{
    internal class BibleJsonReader
    {
        #region Public Constructors

        public BibleJsonReader()
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public ObservableCollection<BibleBook> LoadBibleData(string path)
        {
            var biblebooks = new ObservableCollection<BibleBook>();

            var o1 = File.ReadAllText(path);

            biblebooks = JsonConvert.DeserializeObject<ObservableCollection<BibleBook>>(o1);

            return biblebooks;
        }

        #endregion Public Methods
    }
}