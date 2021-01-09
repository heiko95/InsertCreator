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

            var greenScreen = !Convert.ToBoolean(_appSetting.ReadSetting(KeyName.UseGreenscreen));

            if (Convert.ToBoolean(_appSetting.ReadSetting(KeyName.ShowComponistAndAutor)))
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
            Bitmap image = LoadFrame(!Convert.ToBoolean(_appSetting.ReadSetting(KeyName.UseGreenscreen)));
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/HymnalInsert.png", System.Drawing.Imaging.ImageFormat.Png);

        }

        private void CreateMinistrieInsert(Ministry ministry, bool transparent = true)
        {
            Bitmap image = LoadFrame(transparent);

            var drawingTool = Graphics.FromImage(image);

            // TODO Pos Anpassen
            drawingTool.DrawString(
             $"{ministry.ForeName} {ministry.SureName}",
             new Font("Calibri", 48, FontStyle.Bold, GraphicsUnit.Pixel),
             new SolidBrush(Color.Black), new PointF(90, 840));

            drawingTool.DrawString(
             ministry.Function,
             new Font("Calibri", 44, GraphicsUnit.Pixel),
             new SolidBrush(Color.Black), new PointF(90, 910));
        }


        private void CreateHymnalInsertPicture(HymnalData hymnalData, bool transparent = true)
        {
           
            Bitmap image = LoadFrame(transparent);         

            var drawingTool = Graphics.FromImage(image);

            drawingTool.DrawString(
             $"{hymnalData.Book} {hymnalData.Number}{hymnalData.SongVerses}",
             new Font("Calibri", 48, FontStyle.Bold, GraphicsUnit.Pixel),
             new SolidBrush(Color.Black), new PointF(90, 840));

            drawingTool.DrawString(
                hymnalData.Name,
                new Font("Calibri", 44, GraphicsUnit.Pixel),
                new SolidBrush(Color.Black), new PointF(90, 910));

            drawingTool.DrawImage(
                GetLogo(), new PointF(1425,825));

            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/HymnalInsert.png", System.Drawing.Imaging.ImageFormat.Png);

        }

        private Image GetLogo()
        {
            return Image.FromFile($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/LogoSmall.png");
        }

        private void CreateHymnalInsertPictureMeta(HymnalData hymnalData, bool transparent)
        {
            Bitmap image = LoadFrame(transparent);

            var drawingTool = Graphics.FromImage(image);

            drawingTool.DrawString(
             $"{hymnalData.Book} {hymnalData.Number}{hymnalData.SongVerses}",
             new Font("Calibri", 44, FontStyle.Bold, GraphicsUnit.Pixel),
             new SolidBrush(Color.Black), new PointF(90, 810));
            
            drawingTool.DrawString(
                hymnalData.Name,
                new Font("Calibri", 40, GraphicsUnit.Pixel),
                new SolidBrush(Color.Black), new PointF(90, 865));

            drawingTool.DrawString(
               hymnalData.TextAutor,
               new Font("Calibri", 24, GraphicsUnit.Pixel),
               new SolidBrush(Color.Black), new PointF(90, 926));

            drawingTool.DrawString(
               hymnalData.MelodieAutor,
               new Font("Calibri", 24, GraphicsUnit.Pixel),
               new SolidBrush(Color.Black), new PointF(90, 956));

            drawingTool.DrawImage(
                GetLogo(), new PointF(1425, 825));

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
