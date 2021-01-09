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
        private AppSettingReaderWriter _appSetting = new AppSettingReaderWriter();


        public void WriteFade(HymnalData hymnalData)
        {
            CreateTextfiles(hymnalData);

            var greenScreen = !Convert.ToBoolean(_appSetting.ReadSetting(ConfigSectionName.hymnalInsertOptions, KeyName.UseGreenscreen));

            if (Convert.ToBoolean(_appSetting.ReadSetting(ConfigSectionName.hymnalInsertOptions, KeyName.ShowComponistAndAutor)))
                CreateHymnalInsertPictureMeta(hymnalData, greenScreen);
            else
                CreateHymnalInsertPicture(hymnalData, greenScreen);

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

        public void LoadImages()
        {            
            Bitmap image = LoadFrame(!Convert.ToBoolean(_appSetting.ReadSetting(ConfigSectionName.hymnalInsertOptions, KeyName.UseGreenscreen)));
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/HymnalInsert.png", System.Drawing.Imaging.ImageFormat.Png);

        }

        private void CreateMinistrieInsert(Ministry ministry, bool transparent = true)
        {
            Bitmap image = LoadFrame(transparent);

            var drawingTool = Graphics.FromImage(image);

            // TODO Pos Anpassen
            drawingTool.DrawString(
             $"{ministry.Function} {ministry.ForeName}{ministry.SureName}",
             new Font("Arial", 48, FontStyle.Bold, GraphicsUnit.Pixel),
             new SolidBrush(Color.Black), new PointF(90, 840));
        }


        private void CreateHymnalInsertPicture(HymnalData hymnalData, bool transparent = true)
        {
           
            Bitmap image = LoadFrame(transparent);         

            var drawingTool = Graphics.FromImage(image);

            drawingTool.DrawString(
             $"{hymnalData.Book} {hymnalData.Number}{hymnalData.SongVerses}",
             new Font("Arial", 48, FontStyle.Bold, GraphicsUnit.Pixel),
             new SolidBrush(Color.Black), new PointF(90, 840));

            drawingTool.DrawString(
                hymnalData.Name,
                new Font("Arial", 44, GraphicsUnit.Pixel),
                new SolidBrush(Color.Black), new PointF(90, 910));

            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/HymnalInsert.png", System.Drawing.Imaging.ImageFormat.Png);

        }

        private void CreateHymnalInsertPictureMeta(HymnalData hymnalData, bool transparent)
        {
            Bitmap image = LoadFrame(transparent);

            var drawingTool = Graphics.FromImage(image);

            // TODO Pos Anpassen
            drawingTool.DrawString(
             $"{hymnalData.Book} {hymnalData.Number}{hymnalData.SongVerses}",
             new Font("Arial", 48, FontStyle.Bold, GraphicsUnit.Pixel),
             new SolidBrush(Color.Black), new PointF(90, 840));

            drawingTool.DrawString(
                hymnalData.Name,
                new Font("Arial", 44, GraphicsUnit.Pixel),
                new SolidBrush(Color.Black), new PointF(90, 910));

            drawingTool.DrawString(
               $"{hymnalData.TextAutor}{hymnalData.MelodieAutor}",
               new Font("Arial", 16, GraphicsUnit.Pixel),
               new SolidBrush(Color.Black), new PointF(90, 975));

            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/HymnalInsert.png", System.Drawing.Imaging.ImageFormat.Png);

        }

        private Bitmap LoadFrame(bool transparent)
        {
            var transparentFrame = $"{Directory.GetCurrentDirectory()}/DataSource/InsertFrameTrans.png";
            var greenFrame = $"{Directory.GetCurrentDirectory()}/DataSource/InsertFrameGreen.png";
            Bitmap image;

            if (transparent)
                image = new Bitmap(transparentFrame);
            else
                image = new Bitmap(greenFrame);

            return image;

        }


    }
}
