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
            new System.IO.StreamWriter($"{Directory.GetCurrentDirectory()}/DataSource/FadeText.txt"))
            {
                file.WriteLine($"{hymnalData.Number} {hymnalData.SongVerses}");
                file.WriteLine(hymnalData.Name);
            }

        }

    }
}
