using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HgSoftware.InsertCreator.Model
{
    class BibleJsonReader
    {
        #region Private Fields



        #endregion Private Fields

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
