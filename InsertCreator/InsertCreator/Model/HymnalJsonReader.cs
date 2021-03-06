﻿using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace HgSoftware.InsertCreator.Model
{
    internal static class HymnalJsonReader
    {
        #region Public Methods

        public static List<Song> LoadHymnalData(string path)
        {
            var o1 = File.ReadAllText(path);

            var songs = JsonConvert.DeserializeObject<List<Song>>(o1);

            return songs;
        }

        #endregion Public Methods
    }
}