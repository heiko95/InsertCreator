using HgSoftware.InsertCreator.ViewModel;
using System;
using System.Drawing;
using System.IO;

namespace HgSoftware.InsertCreator.Model
{
    internal class FadeInWriter
    {
        #region Private Fields

        private AppSettingReaderWriter _appSetting = new AppSettingReaderWriter();

        #endregion Private Fields

        #region Public Methods

        public void LoadImages()
        {
            Bitmap image = LoadFrame(!Convert.ToBoolean(_appSetting.ReadSetting(KeyName.UseGreenscreen)));
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/HymnalInsert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/MinistryInsert.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        public void WriteHymnalFade(HymnalData hymnalData)
        {
            var greenScreen = !Convert.ToBoolean(_appSetting.ReadSetting(KeyName.UseGreenscreen));

            if (Convert.ToBoolean(_appSetting.ReadSetting(KeyName.ShowComponistAndAutor)))
                CreateHymnalInsertPictureMeta(hymnalData, greenScreen);
            else
                CreateHymnalInsertPicture(hymnalData, greenScreen);
        }

        public void WriteMinistryFade(MinistryGridViewModel ministry)
        {
            var greenScreen = !Convert.ToBoolean(_appSetting.ReadSetting(KeyName.UseGreenscreen));

            if (Convert.ToBoolean(_appSetting.ReadSetting(KeyName.ShowComponistAndAutor)))
                CreateMinistrieInsert(ministry, greenScreen);
            else
                CreateMinistrieInsert(ministry, greenScreen);
        }

        #endregion Public Methods

        #region Private Methods

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

            DrawLogo(drawingTool);

            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/HymnalInsert.png", System.Drawing.Imaging.ImageFormat.Png);
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

            DrawLogo(drawingTool);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/HymnalInsert.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        private void CreateMinistrieInsert(MinistryGridViewModel ministry, bool transparent = true)
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

            DrawLogo(drawingTool);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/MinistryInsert.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        private void DrawLogo(Graphics drawingTool)
        {
            if (File.Exists($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/LogoSmall.png"))
            {
                var image = Image.FromFile($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/LogoSmall.png");

                const int y = 825;
                const int x = 1425;
                const int size = 150;

                if (image.Width == image.Height)
                {
                    drawingTool.DrawImage(image, new PointF(x, y));
                    return;
                }
                if (image.Width > image.Height)
                {
                    drawingTool.DrawImage(image, new PointF(x, ((size - image.Height) / 2) + y));
                    return;
                }
                if (image.Width < image.Height)
                {
                    drawingTool.DrawImage(image, new PointF(((size - image.Width) / 2) + x, y));
                    return;
                }
            }
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

        #endregion Private Methods
    }
}