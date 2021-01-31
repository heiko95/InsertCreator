using HgSoftware.InsertCreator.ViewModel;
using System;
using System.Drawing;
using System.IO;

namespace HgSoftware.InsertCreator.Model
{
    public class FadeInWriter
    {
        #region Public Constructors

        public FadeInWriter()
        {
            PositionData _positionData = new PositionData();

            CurrentFade = LoadFrame(!Properties.Settings.Default.UseGreenscreen, Properties.Settings.Default.LogoAsCornerlogo);


        }

        #endregion Public Constructors

        #region Public Events

        public event EventHandler<Bitmap> OnInsertUpdate;

        #endregion Public Events

        #region Public Properties

        public Bitmap CurrentFade { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public void LoadImages()
        {
            Bitmap image = LoadFrame(!Properties.Settings.Default.UseGreenscreen, Properties.Settings.Default.LogoAsCornerlogo);
            var drawingTool = Graphics.FromImage(image);
            DrawLogo(drawingTool);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/HymnalInsert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/MinistryInsert.png", System.Drawing.Imaging.ImageFormat.Png);
        }

        public void ResetFade()
        {
            Bitmap image = LoadFrame(!Properties.Settings.Default.UseGreenscreen, Properties.Settings.Default.LogoAsCornerlogo);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/HymnalInsert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/MinistryInsert.png", System.Drawing.Imaging.ImageFormat.Png);
            CurrentFade = image;
            OnInsertUpdate?.Invoke(this, image);
        }

        public float TextPositionX()
        {
            if (Properties.Settings.Default.LogoOnLefthand)
                return 280;
            return 70;
        }

        public void WriteCustom(string textLaneOne, string textLaneTwo)
        {
            var greenScreen = !Properties.Settings.Default.UseGreenscreen;
            var cornerbug = Properties.Settings.Default.LogoAsCornerlogo;

            if (String.IsNullOrEmpty(textLaneOne))
            {
                CreateCustomInsertSingle(textLaneTwo, greenScreen, cornerbug);
            }

            if (String.IsNullOrEmpty(textLaneTwo))
            {
                CreateCustomInsertSingle(textLaneOne, greenScreen, cornerbug);
            }

            CreateCustomInsertDouble(textLaneOne, textLaneTwo, greenScreen, cornerbug);

        }



        public void WriteHymnalFade(HymnalData hymnalData)
        {
            var greenScreen = !Properties.Settings.Default.UseGreenscreen;
            var cornerbug = Properties.Settings.Default.LogoAsCornerlogo;

            if (Properties.Settings.Default.ShowComponistAndAutor)
                CreateHymnalInsertPictureMeta(hymnalData, greenScreen, cornerbug);
            else
                CreateHymnalInsertPicture(hymnalData, greenScreen, cornerbug);
        }

        public void WriteMinistryFade(MinistryGridViewModel ministry)
        {
            var greenScreen = !Properties.Settings.Default.UseGreenscreen;
            var cornerbug = Properties.Settings.Default.LogoAsCornerlogo;

            CreateMinistrieInsert(ministry, greenScreen, cornerbug);
        }

        #endregion Public Methods

        #region Private Methods

        private void CreateHymnalInsertPicture(HymnalData hymnalData, bool transparent = true, bool useCornerBug = false)
        {
            Bitmap image = LoadFrame(transparent, useCornerBug);

            var drawingTool = Graphics.FromImage(image);

            DrawRectangle(drawingTool);

            drawingTool.DrawString(
             $"{hymnalData.Book} {hymnalData.Number}{hymnalData.SongVerses}",
             new Font("Calibri", 48, FontStyle.Bold, GraphicsUnit.Pixel),
             new SolidBrush(Color.Black), new PointF(TextPositionX(), 830));

            drawingTool.DrawString(
                hymnalData.Name,
                new Font("Calibri", 44, GraphicsUnit.Pixel),
                new SolidBrush(Color.Black), new PointF(TextPositionX(), 900));

            DrawLogo(drawingTool);

            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/HymnalInsert.png", System.Drawing.Imaging.ImageFormat.Png);
            CurrentFade = image;
            OnInsertUpdate?.Invoke(this, image);
        }

        private void CreateHymnalInsertPictureMeta(HymnalData hymnalData, bool transparent, bool useCornerBug)
        {
            Bitmap image = LoadFrame(transparent, useCornerBug);

            var drawingTool = Graphics.FromImage(image);

            DrawRectangle(drawingTool);

            drawingTool.DrawString(
             $"{hymnalData.Book} {hymnalData.Number}{hymnalData.SongVerses}",
             new Font("Calibri", 44, FontStyle.Bold, GraphicsUnit.Pixel),
             new SolidBrush(Color.Black), new PointF(TextPositionX(), 820));

            drawingTool.DrawString(
                hymnalData.Name,
                new Font("Calibri", 40, GraphicsUnit.Pixel),
                new SolidBrush(Color.Black), new PointF(TextPositionX(), 868));

            drawingTool.DrawString(
               hymnalData.TextAutor,
               new Font("Calibri", 24, GraphicsUnit.Pixel),
               new SolidBrush(Color.Black), new PointF(TextPositionX(), 917));

            drawingTool.DrawString(
               hymnalData.MelodieAutor,
               new Font("Calibri", 24, GraphicsUnit.Pixel),
               new SolidBrush(Color.Black), new PointF(TextPositionX(), 947));

            DrawLogo(drawingTool);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/HymnalInsert.png", System.Drawing.Imaging.ImageFormat.Png);
            CurrentFade = image;
            OnInsertUpdate?.Invoke(this, image);
        }

        private void CreateCustomInsertDouble(string textLaneOne, string textLaneTwo, bool transparent = true, bool useCornerBug = false)
        {
            Bitmap image = LoadFrame(transparent, useCornerBug);
            var drawingTool = Graphics.FromImage(image);
            DrawRectangle(drawingTool);

            drawingTool.DrawString(
            textLaneOne,
            new Font("Calibri", 48, FontStyle.Bold, GraphicsUnit.Pixel),
            new SolidBrush(Color.Black), new PointF(TextPositionX(), 830));

            drawingTool.DrawString(
             textLaneTwo,
             new Font("Calibri", 44, GraphicsUnit.Pixel),
             new SolidBrush(Color.Black), new PointF(TextPositionX(), 900));

            DrawLogo(drawingTool);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/CustomInsert.png", System.Drawing.Imaging.ImageFormat.Png);
            CurrentFade = image;
            OnInsertUpdate?.Invoke(this, image);
        }

        private void CreateCustomInsertSingle(string text, bool transparent = true, bool useCornerBug = false)
        {
            Bitmap image = LoadFrame(transparent, useCornerBug);
            var drawingTool = Graphics.FromImage(image);
            DrawRectangle(drawingTool);

            drawingTool.DrawString(
            text,
            new Font("Calibri", 48, FontStyle.Bold, GraphicsUnit.Pixel),
            new SolidBrush(Color.Black), new PointF(TextPositionX(), 830));
          

            DrawLogo(drawingTool);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/CustomInsert.png", System.Drawing.Imaging.ImageFormat.Png);
            CurrentFade = image;
            OnInsertUpdate?.Invoke(this, image);
        }

        private void CreateMinistrieInsert(MinistryGridViewModel ministry, bool transparent = true, bool useCornerBug = false)
        {
            Bitmap image = LoadFrame(transparent, useCornerBug);

            var drawingTool = Graphics.FromImage(image);
            DrawRectangle(drawingTool);

            // TODO Pos Anpassen
            drawingTool.DrawString(
             $"{ministry.ForeName} {ministry.SureName}",
             new Font("Calibri", 48, FontStyle.Bold, GraphicsUnit.Pixel),
             new SolidBrush(Color.Black), new PointF(TextPositionX(), 830));

            drawingTool.DrawString(
             ministry.Function,
             new Font("Calibri", 44, GraphicsUnit.Pixel),
             new SolidBrush(Color.Black), new PointF(TextPositionX(), 900));

            DrawLogo(drawingTool);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/MinistryInsert.png", System.Drawing.Imaging.ImageFormat.Png);
            CurrentFade = image;
            OnInsertUpdate?.Invoke(this, image);
        }

        private void DrawLogo(Graphics drawingTool)
        {
            if (File.Exists($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/LogoSmall.png"))
            {
                var image = Image.FromFile($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/LogoSmall.png");

                int y = 820;
                int x = 1520;

                if (Properties.Settings.Default.LogoOnLefthand)
                    x = 20;

                const int size = 160;
                LogoWriter(drawingTool, image, x, y, size);
            }
        }

        private void DrawRectangle(Graphics drawingTool)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            drawingTool.FillRectangle(myBrush, new Rectangle(0, 800, 1700, 200));
            myBrush.Dispose();
        }

        private Bitmap LoadFrame(bool transparent, bool useCornerBug)
        {
            var transparentFrame = $"{Directory.GetCurrentDirectory()}/DataSource/InsertFrameTrans.png";
            var greenFrame = $"{Directory.GetCurrentDirectory()}/DataSource/InsertFrameGreen.png";
            Bitmap image;

            if (transparent)
                image = new Bitmap(transparentFrame);
            else
                image = new Bitmap(greenFrame);

            if (useCornerBug && File.Exists($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/LogoTiny.png"))
            {
                var drawingTool = Graphics.FromImage(image);
                var logoImage = Image.FromFile($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/LogoTiny.png");
                LogoWriter(drawingTool, logoImage, 1800, 20, 100);
            }
            return image;
        }

        private void LogoWriter(Graphics drawingTool, Image image, float x, float y, float size)
        {
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

        #endregion Private Methods
    }
}