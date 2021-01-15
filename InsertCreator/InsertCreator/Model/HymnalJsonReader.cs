using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace HgSoftware.InsertCreator.Model
{
    internal class HymnalJsonReader
    {
        #region Public Methods

        public List<Song> LoadHymnalData(string path)
        {
            var songs = new List<Song>();

            var o1 = File.ReadAllText(path);

            songs = JsonConvert.DeserializeObject<List<Song>>(o1);

            return songs;
        }

        #endregion Public Methods
    }
}