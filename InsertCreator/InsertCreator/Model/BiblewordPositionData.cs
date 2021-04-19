using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HgSoftware.InsertCreator.Model
{
    public class BiblewordPositionData : IPositionData
    {
        #region Private Fields

        [JsonProperty("Schriftsatz Überschrift")]
        private Font _fontTextHeadline;

        [JsonProperty("Schriftsatz Text")]
        private Font _fontTextBody;

        [JsonProperty("Position Einblendefeld")]
        private Point _rectanglePosition = new Point();

        [JsonProperty("Position Logo")]
        private PointF _logoPosition = new PointF();

        [JsonProperty("1.Position Überschrift")]
        private Point _headlineTextFirstLine = new Point();

        [JsonProperty("2.Position Überschrift")]
        private Point _headlineTextSecondLine = new Point();

        [JsonProperty("1.Position Versnummer")]
        private Point _versenumberFirstLine = new Point();

        [JsonProperty("2.Position Versnummer")]
        private Point _versenumberSecondLine = new Point();

        [JsonProperty("3.Position Versnummer")]
        private Point _versenumberThirdLine = new Point();

        [JsonProperty("4.Position Versnummer")]
        private Point _versenumberFourthLine = new Point();

        [JsonProperty("5.Position Versnummer")]
        private Point _versenumberFifthLine = new Point();

        [JsonProperty("6.Position Versnummer")]
        private Point _versenumberSixthLine = new Point();

        [JsonProperty("7.Position Versnummer")]
        private Point _versenumberSeventhLine = new Point();

        [JsonProperty("8.Position Versnummer")]
        private Point _versenumberEighthLine = new Point();

        [JsonProperty("1.Position Text")]
        private Point _textFirstLine = new Point();

        [JsonProperty("2.Position Text")]
        private Point _textSecondLine = new Point();

        [JsonProperty("3.Position Text")]
        private Point _textThirdLine = new Point();

        [JsonProperty("4.Position Text")]
        private Point _textFourthLine = new Point();

        [JsonProperty("5.Position Text")]
        private Point _textFifthLine = new Point();

        [JsonProperty("6.Position Text")]
        private Point _textSixthLine = new Point();

        [JsonProperty("7.Position Text")]
        private Point _textSeventhLine = new Point();

        [JsonProperty("8.Position Text")]
        private Point _textEighthLine = new Point();

        [JsonProperty("Objektgröße Einblendefeld")]
        private Size _sizeRectangle = new Size();

        [JsonProperty("Objektgröße Logo")]
        private int _sizeLogo;

        [JsonProperty("Transparenz Einblendefeld")]
        private int _transparencyRectangle;

        [JsonIgnore]
        private Point _maxTextPosition = new Point();


        #endregion Private Fields

        #region Public Constructor

        public BiblewordPositionData()
        {
            _fontTextHeadline = new Font("Calibri", 64, FontStyle.Bold, GraphicsUnit.Pixel);

            _fontTextBody = new Font("Calibri", 56, FontStyle.Regular, GraphicsUnit.Pixel); ;

            _rectanglePosition.X = 69;
            _rectanglePosition.Y = 45;

            _logoPosition.X = 1560;
            _logoPosition.Y = 95;

            _headlineTextFirstLine.X = 150;
            _headlineTextFirstLine.Y = 150;

            _headlineTextSecondLine.X = 150;
            _headlineTextSecondLine.Y = 230;

            _versenumberFirstLine.X = 150;
            _versenumberFirstLine.Y = 391;

            _versenumberSecondLine.X = 150;
            _versenumberSecondLine.Y = 468;

            _versenumberThirdLine.X = 150;
            _versenumberThirdLine.Y = 545;

            _versenumberFourthLine.X = 150;
            _versenumberFourthLine.Y = 622;

            _versenumberFifthLine.X = 150;
            _versenumberFifthLine.Y = 699;

            _versenumberSixthLine.X = 150;
            _versenumberSixthLine.Y = 776;

            _versenumberSeventhLine.X = 150;
            _versenumberSeventhLine.Y = 853;

            _versenumberEighthLine.X = 150;
            _versenumberEighthLine.Y = 930;

            _textFirstLine.X = 235;
            _textFirstLine.Y = 391;

            _textSecondLine.X = 235;
            _textSecondLine.Y = 468;

            _textThirdLine.X = 235;
            _textThirdLine.Y = 545;

            _textFourthLine.X = 235;
            _textFourthLine.Y = 622;

            _textFifthLine.X = 235;
            _textFifthLine.Y = 699;

            _textSixthLine.X = 235;
            _textSixthLine.Y = 776;

            _textSeventhLine.X = 235;
            _textSeventhLine.Y = 853;

            _textEighthLine.X = 235;
            _textEighthLine.Y = 930;

            _sizeRectangle.Height = 990;
            _sizeRectangle.Width = 1782;

            _sizeLogo = 240;

            _transparencyRectangle = 200;

            _maxTextPosition.X = 1920 - _textFirstLine.X;
            _maxTextPosition.Y = 1080 - _headlineTextFirstLine.Y;
        }

        #endregion Public Constructor

        #region Public Properties

        [JsonIgnore]
        public Font FontTextHeadline
        {
            get { return _fontTextHeadline; }
        }

        [JsonIgnore]
        public Font FontTextBody
        {
            get { return _fontTextBody; }
        }

        [JsonIgnore]
        public Point RectanglePosition
        {
            get { return _rectanglePosition; }
        }

        [JsonIgnore]
        public PointF LogoPosition
        {
            get { return _logoPosition; }
        }

        [JsonIgnore]
        public Point HeadlineTextFirstLine
        {
            get { return _headlineTextFirstLine; }
        }

        [JsonIgnore]
        public Point HeadlineTextSecondLine
        {
            get { return _headlineTextSecondLine; }
        }

        [JsonIgnore]
        public Point VersenumberFirstLine
        {
            get { return _versenumberFirstLine; }
        }

        [JsonIgnore]
        public Point VersenumberSecondLine
        {
            get { return _versenumberSecondLine; }
        }

        [JsonIgnore]
        public Point VersenumberThirdLine
        {
            get { return _versenumberThirdLine; }
        }

        [JsonIgnore]
        public Point VersenumberFourthLine
        {
            get { return _versenumberFourthLine; }
        }

        [JsonIgnore]
        public Point VersenumberFifthLine
        {
            get { return _versenumberFifthLine; }
        }

        [JsonIgnore]
        public Point VersenumberSixthLine
        {
            get { return _versenumberSixthLine; }
        }

        [JsonIgnore]
        public Point VersenumberSeventhLine
        {
            get { return _versenumberSeventhLine; }
        }

        [JsonIgnore]
        public Point VersenumberEighthLine
        {
            get { return _versenumberEighthLine; }
        }

        [JsonIgnore]
        public Point TextFirstLine
        {
            get { return _textFirstLine; }
        }

        [JsonIgnore]
        public Point TextSecondLine
        {
            get { return _textSecondLine; }
        }

        [JsonIgnore]
        public Point TextThirdLine
        {
            get { return _textThirdLine; }
        }

        [JsonIgnore]
        public Point TextFourthLine
        {
            get { return _textFourthLine; }
        }

        [JsonIgnore]
        public Point TextFifthLine
        {
            get { return _textFifthLine; }
        }

        [JsonIgnore]
        public Point TextSixthLine
        {
            get { return _textSixthLine; }
        }

        [JsonIgnore]
        public Point TextSeventhLine
        {
            get { return _textSeventhLine; }
        }

        [JsonIgnore]
        public Point TextEighthLine
        {
            get { return _textEighthLine; }
        }

        [JsonIgnore]
        public Size SizeRectangle
        {
            get { return _sizeRectangle; }
        }

        [JsonIgnore]
        public int SizeLogo
        {
            get { return _sizeLogo; }
        }

        [JsonIgnore]
        public int TransparencyRectangle
        {
            get { 
                if (Properties.Settings.Default.UseGreenscreen)
                    return 255;
                return _transparencyRectangle;
            }
                
        }

        [JsonIgnore]
        public Point MaxTextPosition
        {
            get { return _maxTextPosition; }
        }

        #endregion Public Properties
    }
}