using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace HgSoftware.InsertCreator.Model
{
    internal static class BibleJsonReader
    {
        #region Public Methods

        public static List<BibleBook> LoadBibleData(string path)
        {
            List<BibleBook> biblebooks;

            var o1 = File.ReadAllText(path);

            biblebooks = JsonConvert.DeserializeObject<List<BibleBook>>(o1);

            return biblebooks;
        }

        #endregion Public Methods
    }
}