using System.Collections.Generic;

namespace HgSoftware.InsertCreator.Model
{
    public class Song
    {
        #region Public Properties

        public List<Metadata> Metadata { get; set; } = new List<Metadata>();

        public string Number { get; set; }

        public string Title { get; set; }

        public int Versecount { get; set; }

        public List<Verse> Verses { get; set; } = new List<Verse>();

        #endregion Public Properties
    }
}