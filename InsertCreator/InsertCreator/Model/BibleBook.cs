using System.Collections.Generic;

namespace HgSoftware.InsertCreator.Model
{
    public class BibleBook
    {
        public string Name { get; set; }
        public List<BibleChapter> Chapters { get; set; } = new List<BibleChapter>();
    }
}