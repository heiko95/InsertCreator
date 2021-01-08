using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Liedeinblendung.Model
{
    class FadeInWriter
    {

        public void WriteFade(HymnalData hymnalData)
        {
            CreateTextfiles(hymnalData);
            CreateInsertPicture(hymnalData);

        }


        private void CreateTextfiles(HymnalData hymnalData)
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


        public void CreateInsertPicture(HymnalData hymnalData)
        {
            var tempFileNamePath = $"{Directory.GetCurrentDirectory()}/DataSource/InsertFrame.png";
            Bitmap image = new Bitmap(tempFileNamePath);

            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
          




        }


    }
}
