using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HgSoftware.InsertCreator.Model
{
    public class BibleData : IInsertData
    {
        public BibleData(string bibleBook, string bibleChapter, string bibleVerse, string bibleText)
        {
            BibleBook = bibleBook;
            BibleChapter = bibleChapter.Replace(" ", "");
            BibleVerse = bibleVerse.Replace("; ", ";").Replace(";", "; ");
            BibleText = bibleText;
        }

        public string BibleBook { get; set; }
        public string BibleChapter { get; set; }
        public string BibleVerse { get; set; }

        public string BibleText { get; set; }

        public string FirstLine
        {
            get
            {
                if (string.IsNullOrEmpty(BibleText))
                    return "Bibelwort";
                return "Bibeltext";
            }
        }

        public string SecondLine => $"{BibleBook} {BibleChapter}, {BibleVerse}";
    }
}