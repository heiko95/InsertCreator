using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liedeinblendung.Model
{
    class FadeInWriter
    {

        public void WriteFade(HymnalData hymnalData)
        {



            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/FadeText.txt"))
            {
                file.WriteLine($"{hymnalData.Book} {hymnalData.Number}{hymnalData.SongVerses}");
                file.WriteLine(hymnalData.Name);
            }

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/FadeTextMeta.txt"))
            {
                file.WriteLine($"{hymnalData.TextAutor}{hymnalData.MelodieAutor}");
            }

        }

    }
}
