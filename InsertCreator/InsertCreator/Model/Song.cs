using System.Collections.Generic;

namespace Liedeinblendung.Model
{
    public class Song
    {
        public string Number { get; set; }

        public string Title { get; set; }

        public int Versecount { get; set; }

        public List<Verse> Verses { get; set; } = new List<Verse>();

        public List<Metadata> Metadata { get; set; } = new List<Metadata>();
    }
}