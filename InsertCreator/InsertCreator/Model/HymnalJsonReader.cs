using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Liedeinblendung.Model
{
    internal class HymnalJsonReader
    {
        public List<Song> LoadHymnalData(string path)
        {
            var songs = new List<Song>();

            var o1 = File.ReadAllText(path);

            songs = JsonConvert.DeserializeObject<List<Song>>(o1);

            return songs;
        }
    }
}