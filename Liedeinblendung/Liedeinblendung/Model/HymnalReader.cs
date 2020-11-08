using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liedeinblendung.Model
{
    class HymnalReader
    {

        public List<HymnalData> LoadHymnals ()
        {

            return File.ReadAllLines($"{Directory.GetCurrentDirectory()}/DataSource/liste-neues-gesangbuch-20041209.csv")
                                           .Select(v => FromCsv(v))
                                           .ToList();
        }


        private HymnalData FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(';');
            var hymnal = new HymnalData();
            hymnal.Number = values[0];
            hymnal.Name = values[1];

            return hymnal;
        }

    }
}
