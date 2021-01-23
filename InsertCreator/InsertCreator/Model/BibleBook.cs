using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HgSoftware.InsertCreator.Model
{
    public class BibleBook
    {   
        public string Name { get; set; }
        public List<BibleChapter> Chapters { get; set; } = new List<BibleChapter>();




    }
}
