using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HgSoftware.InsertCreator.Model
{
    public class BibleData : IInsertData
    {
        public string BibleBook { get; set; }
        public string BibleChapter { get; set; }
        public string BibleVerse { get; set; }
        public string FirstLine => "Textwort";
        public string SecondLine => $"{BibleBook} {BibleChapter}, {BibleVerse}";
    }
}