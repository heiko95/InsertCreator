using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liedeinblendung.Model
{
    class HymnalJsonReader
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
