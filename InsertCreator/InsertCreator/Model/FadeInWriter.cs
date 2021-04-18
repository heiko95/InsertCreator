using HgSoftware.InsertCreator.ViewModel;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace HgSoftware.InsertCreator.Model
{
    public class FadeInWriter
    {
        #region Private Fields

        private readonly PictureReader _pictureReader = new PictureReader();
        private readonly PositionData _positionData;
        private readonly BiblewordPositionData _biblewordPositionData;

        #endregion Private Fields

        #region Public Constructors

        public FadeInWriter(PositionData positionData, BiblewordPositionData biblewordPositionData)
        {
            _positionData = positionData;
            _biblewordPositionData = biblewordPositionData;
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
        }

        public void ResetFade()
        {
            Bitmap image = LoadFrame(!Properties.Settings.Default.UseGreenscreen, Properties.Settings.Default.LogoAsCornerlogo);
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            CurrentFade = image;
            OnInsertUpdate?.Invoke(this, image);
        }

        //public float TextPositionX()
        //{
        //    if (Properties.Settings.Default.LogoOnLefthand)
        //        return 280;
        //    return 70;
        //}

        public void WriteFade(IInsertData insert)
        {
            Bitmap image = SelectFadeWriter(insert);
            if (image != null)
            {
                CreateInsert(image);
            }
        }

        public void SaveFade(IInsertData insert)
        {
            Bitmap image = SelectFadeWriter(insert);
            if (image != null)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "png files (*.png)|*.png";
                saveFileDialog1.FilterIndex = 2;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.InitialDirectory = Environment.GetEnvironmentVariable("userprofile");
                saveFileDialog1.FileName = $"{insert.FirstLine}_{insert.SecondLine}";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        #endregion Public Methods

        #region Private Methods

        private Bitmap SelectFadeWriter(IInsertData insert)
        {
            var greenScreen = !Properties.Settings.Default.UseGreenscreen;
            var cornerbug = Properties.Settings.Default.LogoAsCornerlogo;

            switch (insert)
            {
                case CustomInsert _:
                    return WriteCustom(insert as CustomInsert, greenScreen, cornerbug);

                case HymnalData _:
                    return WriteHymnalFade(insert as HymnalData, greenScreen, cornerbug);

                case MinistryGridViewModel _:
                    return WriteMinistryFade(insert as MinistryGridViewModel, greenScreen, cornerbug);

                case BibleData _:
                    return WriteBibleFade(insert as BibleData, greenScreen, cornerbug);
            }

            return null;
        }

        private void CreateInsert(Bitmap image)
        {
            image.Save($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Insert.png", System.Drawing.Imaging.ImageFormat.Png);
            CurrentFade = image;
            OnInsertUpdate?.Invoke(this, image);
        }

        private Bitmap CreateCustomInsertDouble(string textLaneOne, string textLaneTwo, bool transparent = true, bool useCornerBug = false)
        {
            Bitmap image = LoadFrame(transparent, useCornerBug);
            var drawingTool = Graphics.FromImage(image);
            DrawRectangle(drawingTool);

            drawingTool.DrawString(
            textLaneOne,
            _positionData.FontTextTwoRowFirstLine,
            new SolidBrush(Color.Black), _positionData.TextTwoRowFirstLinePosition);

            drawingTool.DrawString(
             textLaneTwo,
             _positionData.FontTextTwoRowSecondLine,
             new SolidBrush(Color.Black), _positionData.TextTwoRowSecondLinePosition);

            DrawLogo(drawingTool);

            return image;
        }

        private Bitmap CreateCustomInsertSingle(string text, bool transparent = true, bool useCornerBug = false)
        {
            Bitmap image = LoadFrame(transparent, useCornerBug);
            var drawingTool = Graphics.FromImage(image);
            DrawRectangle(drawingTool);

            drawingTool.DrawString(
            text,
            _positionData.FontTextOneRowFirstLine,
            new SolidBrush(Color.Black), _positionData.TextOneRowFirstLinePosition);

            DrawLogo(drawingTool);

            return image;
        }

        private Bitmap CreateBibleInsert(BibleData bibleData, bool transparent = true, bool useCornerBug = false)
        {
            Bitmap image = LoadFrame(transparent, useCornerBug);
            var drawingTool = Graphics.FromImage(image);
            DrawRectangle(drawingTool);

            drawingTool.DrawString(
             "Textwort",
            _positionData.FontTextTwoRowFirstLine,
             new SolidBrush(Color.Black), _positionData.TextTwoRowFirstLinePosition);

            drawingTool.DrawString(
             $"{bibleData.BibleBook} {bibleData.BibleChapter}, {bibleData.BibleVerse}",
             _positionData.FontTextTwoRowSecondLine,
             new SolidBrush(Color.Black), _positionData.TextTwoRowSecondLinePosition);

            DrawLogo(drawingTool);

            return image;
        }

        private Bitmap CreateHymnalInsertPicture(HymnalData hymnalData, bool transparent = true, bool useCornerBug = false)
        {
            Bitmap image = LoadFrame(transparent, useCornerBug);

            var drawingTool = Graphics.FromImage(image);

            DrawRectangle(drawingTool);

            drawingTool.DrawString(
             $"{hymnalData.Book} {hymnalData.Number}{hymnalData.SongVerses}",
             _positionData.FontTextTwoRowFirstLine,
             new SolidBrush(Color.Black), _positionData.TextTwoRowFirstLinePosition);

            drawingTool.DrawString(
                hymnalData.Name,
                _positionData.FontTextTwoRowSecondLine,
                new SolidBrush(Color.Black), _positionData.TextTwoRowSecondLinePosition);

            DrawLogo(drawingTool);

            return image;
        }

        private Bitmap CreateHymnalInsertPictureMeta(HymnalData hymnalData, bool transparent, bool useCornerBug)
        {
            Bitmap image = LoadFrame(transparent, useCornerBug);

            var drawingTool = Graphics.FromImage(image);

            DrawRectangle(drawingTool);

            drawingTool.DrawString(
             $"{hymnalData.Book} {hymnalData.Number}{hymnalData.SongVerses}",
             _positionData.FontTextFourRowFirstLine,
             new SolidBrush(Color.Black), _positionData.TextFourRowFirstLinePosition);

            drawingTool.DrawString(
                hymnalData.Name,
                _positionData.FontTextFourRowSecondLine,
                new SolidBrush(Color.Black), _positionData.TextFourRowSecondLinePosition);

            drawingTool.DrawString(
               hymnalData.TextAutor,
               _positionData.FontTextFourRowThirdLine,
               new SolidBrush(Color.Black), _positionData.TextFourRowThirdLinePosition);

            drawingTool.DrawString(
               hymnalData.MelodieAutor,
               _positionData.FontTextFourRowFourthLine,
               new SolidBrush(Color.Black), _positionData.TextFourRowFourthLinePosition);

            DrawLogo(drawingTool);

            return image;
        }

        private Bitmap CreateMinistrieInsert(MinistryGridViewModel ministry, bool transparent = true, bool useCornerBug = false)
        {
            Bitmap image = LoadFrame(transparent, useCornerBug);

            var drawingTool = Graphics.FromImage(image);
            DrawRectangle(drawingTool);

            drawingTool.DrawString(
             $"{ministry.ForeName} {ministry.SureName}",
             _positionData.FontTextTwoRowFirstLine,
             new SolidBrush(Color.Black), _positionData.TextTwoRowFirstLinePosition);

            drawingTool.DrawString(
             ministry.Function,
             _positionData.FontTextTwoRowSecondLine,
             new SolidBrush(Color.Black), _positionData.TextTwoRowSecondLinePosition);

            DrawLogo(drawingTool);
            return image;
        }

        private void DrawLogo(Graphics drawingTool)
        {
            if (File.Exists($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Logo.png"))
            {
                var image = _pictureReader.ResizePicture(new Bitmap($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Logo.png"), _positionData.SizeLogo);
                LogoWriter(drawingTool, image, _positionData.LogoPosition, _positionData.SizeLogo);
            }
        }

        private void DrawRectangle(Graphics drawingTool)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
            drawingTool.FillRectangle(myBrush, new Rectangle(_positionData.RectanglePosition.X, _positionData.RectanglePosition.Y, _positionData.SizeRectangle.Width, _positionData.SizeRectangle.Height));
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

            if (useCornerBug && File.Exists($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Logo.png"))
            {
                var drawingTool = Graphics.FromImage(image);

                var logoImage = _pictureReader.ResizePicture(new Bitmap($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Logo.png"), _positionData.SizeCornerbug);
                LogoWriter(drawingTool, logoImage, _positionData.CornerbugPosition, _positionData.SizeCornerbug);
            }
            return image;
        }

        private void LogoWriter(Graphics drawingTool, Image image, PointF position, float size)
        {
            if (image.Width == image.Height)
            {
                drawingTool.DrawImage(image, new PointF(position.X, position.Y));
                return;
            }
            if (image.Width > image.Height)
            {
                drawingTool.DrawImage(image, new PointF(position.X, ((size - image.Height) / 2) + position.Y));
                return;
            }
            if (image.Width < image.Height)
            {
                drawingTool.DrawImage(image, new PointF(((size - image.Width) / 2) + position.X, position.Y));
            }
        }

        private Bitmap WriteCustom(CustomInsert insert, bool greenScreen, bool cornerbug)
        {
            if (String.IsNullOrEmpty(insert.LineOne))
            {
                return CreateCustomInsertSingle(insert.LineTwo, greenScreen, cornerbug);
            }

            if (String.IsNullOrEmpty(insert.LineTwo))
            {
                return CreateCustomInsertSingle(insert.LineOne, greenScreen, cornerbug);
            }
            return CreateCustomInsertDouble(insert.LineOne, insert.LineTwo, greenScreen, cornerbug);
        }

        private Bitmap WriteHymnalFade(HymnalData hymnalData, bool greenScreen, bool cornerbug)
        {
            if (Properties.Settings.Default.ShowComponistAndAutor)
                return CreateHymnalInsertPictureMeta(hymnalData, greenScreen, cornerbug);
            else
                return CreateHymnalInsertPicture(hymnalData, greenScreen, cornerbug);
        }

        private Bitmap WriteMinistryFade(MinistryGridViewModel ministry, bool greenScreen, bool cornerbug)
        {
            return CreateMinistrieInsert(ministry, greenScreen, cornerbug);
        }

        private Bitmap WriteBibleFade(BibleData bibleData, bool greenScreen, bool cornerbug)
        {
            if (string.IsNullOrEmpty(bibleData.BibleText))
                return CreateBibleInsert(bibleData, greenScreen, cornerbug);
            return CreateFullscreenBibleInsert(bibleData, greenScreen);
        }

        private Bitmap CreateFullscreenBibleInsert(BibleData bibleData, bool greenScreen)
        {
            Bitmap image = LoadFrame(!greenScreen, false);
            var drawingTool = Graphics.FromImage(image);
            DrawBibleRectangle(drawingTool, greenScreen);

            //drawingTool.DrawString(
            // "Textwort",
            //_positionData.FontTextTwoRowFirstLine,
            // new SolidBrush(Color.Black), _positionData.TextTwoRowFirstLinePosition);

            //drawingTool.DrawString(
            // $"{bibleData.BibleBook} {bibleData.BibleChapter}, {bibleData.BibleVerse}",
            // _positionData.FontTextTwoRowSecondLine,
            // new SolidBrush(Color.Black), _positionData.TextTwoRowSecondLinePosition);

            DrawLogo(drawingTool);

            return image;
        }

        private void DrawBibleRectangle(Graphics drawingTool, bool greenScreen)
        {
            var transparency = 255;
            if (!greenScreen)
                transparency = 200;
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(Color.FromArgb(transparency, 255, 255, 255));
            drawingTool.FillRectangle(myBrush, new Rectangle(_biblewordPositionData.RectanglePosition.X, _biblewordPositionData.RectanglePosition.Y, _biblewordPositionData.SizeRectangle.Width, _biblewordPositionData.SizeRectangle.Height));
            myBrush.Dispose();
        }

        #endregion Private Methods
    }
}