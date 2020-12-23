using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liedeinblendung.Model
{
    public class Song
    {
        public string Number { get; set; }

        public string Title { get; set; }

        public int Versecount { get; set; }

        public List<Verse> Verses { get; set; } = new List<Verse>();

        public List<Metadata> Metadatas { get; set; } = new List<Metadata>();
    }
}
