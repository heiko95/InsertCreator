using Newtonsoft.Json;
using System.Drawing;

namespace HgSoftware.InsertCreator.Model
{
    public class PositionData : IPositionData
    {
        #region Private Fields

        [JsonProperty("Position Cornerbug")]
        private Point _cornerbugPosition = new Point();

        [JsonProperty("Schriftsatz 1.Position Text-vierrehig")]
        private Font _fontTextFourRowFirstLine;

        [JsonProperty("Schriftsatz 4.Position Text-vierrehig")]
        private Font _fontTextFourRowFourthLine;

        [JsonProperty("Schriftsatz 2.Position Text-vierrehig")]
        private Font _fontTextFourRowSecondLine;

        [JsonProperty("Schriftsatz 3.Position Text-vierrehig")]
        private Font _fontTextFourRowThirdLine;

        [JsonProperty("Schriftsatz 1.Position Text-einrehig")]
        private Font _fontTextOneRowFirstLine;

        [JsonProperty("Schriftsatz 1.Position Text-zweirehig")]
        private Font _fontTextTwoRowFirstLine;

        [JsonProperty("Schriftsatz 2.Position Text-zweirehig")]
        private Font _fontTextTwoRowSecondLine;

        [JsonProperty("Position Logo Links")]
        private Point _leftLogoPosition = new Point();

        [JsonProperty("1.Position Text-vierrehig (Logo Links)")]
        private Point _leftTextFourRowFirstLine = new Point();

        [JsonProperty("4.Position Text-vierrehig (Logo Links)")]
        private Point _leftTextFourRowFourthLine = new Point();

        [JsonProperty("2.Position Text-vierrehig (Logo Links)")]
        private Point _leftTextFourRowSecondLine = new Point();

        [JsonProperty("3.Position Text-vierrehig (Logo Links)")]
        private Point _leftTextFourRowThirdLine = new Point();

        [JsonProperty("1.Position Text-einrehig (Logo Links)")]
        private Point _leftTextOneRowFirstLine = new Point();

        [JsonProperty("1.Position Text-zweirehig (Logo Links)")]
        private Point _leftTextTwoRowFirstLine = new Point();

        [JsonProperty("2.Position Text-zweirehig (Logo Links)")]
        private Point _leftTextTwoRowSecondLine = new Point();

        [JsonProperty("Position Einblendefeld")]
        private Point _rectanglePosition = new Point();

        [JsonProperty("Position Logo Rechts")]
        private Point _rightLogoPosition = new Point();

        [JsonProperty("1.Position Text-vierrehig (Logo Rechts)")]
        private Point _rightTextFourRowFirstLine = new Point();

        [JsonProperty("4.Position Text-vierrehig (Logo Rechts)")]
        private Point _rightTextFourRowFourthLine = new Point();

        [JsonProperty("2.Position Text-vierrehig (Logo Rechts)")]
        private Point _rightTextFourRowSecondLine = new Point();

        [JsonProperty("3.Position Text-vierrehig (Logo Rechts)")]
        private Point _rightTextFourRowThirdLine = new Point();

        [JsonProperty("1.Position Text-einrehig (Logo Rechts)")]
        private Point _rightTextOneRowFirstLine = new Point();

        [JsonProperty("1.Position Text-zweirehig (Logo Rechts)")]
        private Point _rightTextTwoRowFirstLine = new Point();

        [JsonProperty("2.Position Text-zweirehig (Logo Rechts)")]
        private Point _rightTextTwoRowSecondLine = new Point();

        [JsonProperty("Objektgröße Cornerbug")]
        private int _sizeCornerbug;

        [JsonProperty("Objektgröße Logo")]
        private int _sizeLogo;

        [JsonProperty("Objektgröße Einblendefeld")]
        private Size _sizeRectangle = new Size();

        [JsonProperty("Transparenz Einblendefeld")]
        private int _transparencyRectangle;

        #endregion Private Fields

        #region Public Constructors

        public PositionData()
        {
            //Positions

            _cornerbugPosition.X = 1766;
            _cornerbugPosition.Y = 44;

            _rectanglePosition.X = 0;
            _rectanglePosition.Y = 782;

            // Logo on Lefthand Side

            _leftLogoPosition.X = 50;
            _leftLogoPosition.Y = 802;

            _leftTextOneRowFirstLine.X = 310;
            _leftTextOneRowFirstLine.Y = 845;

            _leftTextTwoRowFirstLine.X = 310;
            _leftTextTwoRowFirstLine.Y = 800;

            _leftTextTwoRowSecondLine.X = 310;
            _leftTextTwoRowSecondLine.Y = 891;

            _leftTextFourRowFirstLine.X = 310;
            _leftTextFourRowFirstLine.Y = 798;

            _leftTextFourRowSecondLine.X = 310;
            _leftTextFourRowSecondLine.Y = 847;

            _leftTextFourRowThirdLine.X = 310;
            _leftTextFourRowThirdLine.Y = 908;

            _leftTextFourRowFourthLine.X = 310;
            _leftTextFourRowFourthLine.Y = 938;

            // Logo on Righthand Side

            _rightLogoPosition.X = 1450;
            _rightLogoPosition.Y = 802;

            _rightTextOneRowFirstLine.X = 70;
            _rightTextOneRowFirstLine.Y = 845;

            _rightTextTwoRowFirstLine.X = 70;
            _rightTextTwoRowFirstLine.Y = 800;

            _rightTextTwoRowSecondLine.X = 70;
            _rightTextTwoRowSecondLine.Y = 891;

            _rightTextFourRowFirstLine.X = 70;
            _rightTextFourRowFirstLine.Y = 798;

            _rightTextFourRowSecondLine.X = 70;
            _rightTextFourRowSecondLine.Y = 847;

            _rightTextFourRowThirdLine.X = 70;
            _rightTextFourRowThirdLine.Y = 908;

            _rightTextFourRowFourthLine.X = 70;
            _rightTextFourRowFourthLine.Y = 938;

            _fontTextOneRowFirstLine = new Font("Calibri", 76, FontStyle.Bold, GraphicsUnit.Pixel);
            _fontTextTwoRowFirstLine = new Font("Calibri", 76, FontStyle.Bold, GraphicsUnit.Pixel);
            _fontTextTwoRowSecondLine = new Font("Calibri", 72, FontStyle.Regular, GraphicsUnit.Pixel);
            _fontTextFourRowFirstLine = new Font("Calibri", 48, FontStyle.Bold, GraphicsUnit.Pixel);
            _fontTextFourRowSecondLine = new Font("Calibri", 48, FontStyle.Regular, GraphicsUnit.Pixel);
            _fontTextFourRowThirdLine = new Font("Calibri", 24, FontStyle.Regular, GraphicsUnit.Pixel);
            _fontTextFourRowFourthLine = new Font("Calibri", 24, FontStyle.Regular, GraphicsUnit.Pixel);

            _sizeCornerbug = 110;

            _sizeLogo = 180;

            _sizeRectangle.Height = 220;
            _sizeRectangle.Width = 1650;

            _transparencyRectangle = 255;
        }

        #endregion Public Constructors

        #region Public Properties

        [JsonIgnore]
        public PointF CornerbugPosition
        {
            get { return _cornerbugPosition; }
        }

        [JsonIgnore]
        public Font FontTextFourRowFirstLine
        {
            get { return _fontTextFourRowFirstLine; }
        }

        [JsonIgnore]
        public Font FontTextFourRowFourthLine
        {
            get { return _fontTextFourRowFourthLine; }
        }

        [JsonIgnore]
        public Font FontTextFourRowSecondLine
        {
            get { return _fontTextFourRowSecondLine; }
        }

        [JsonIgnore]
        public Font FontTextFourRowThirdLine
        {
            get { return _fontTextFourRowThirdLine; }
        }

        [JsonIgnore]
        public Font FontTextOneRowFirstLine
        {
            get { return _fontTextOneRowFirstLine; }
        }

        [JsonIgnore]
        public Font FontTextTwoRowFirstLine
        {
            get { return _fontTextTwoRowFirstLine; }
        }

        [JsonIgnore]
        public Font FontTextTwoRowSecondLine
        {
            get { return _fontTextTwoRowSecondLine; }
        }

        [JsonIgnore]
        public PointF LogoPosition
        {
            get
            {
                if (Properties.Settings.Default.LogoOnLefthand)
                    return _leftLogoPosition;
                return _rightLogoPosition;
            }
        }

        [JsonIgnore]
        public Point RectanglePosition
        {
            get { return _rectanglePosition; }
        }

        [JsonIgnore]
        public int SizeCornerbug
        {
            get { return _sizeCornerbug; }
        }

        [JsonIgnore]
        public int SizeLogo
        {
            get { return _sizeLogo; }
        }

        [JsonIgnore]
        public Size SizeRectangle
        {
            get { return _sizeRectangle; }
        }

        [JsonIgnore]
        public PointF TextFourRowFirstLinePosition
        {
            get
            {
                if (Properties.Settings.Default.LogoOnLefthand)
                    return _leftTextFourRowFirstLine;
                return _rightTextFourRowFirstLine;
            }
        }

        [JsonIgnore]
        public PointF TextFourRowFourthLinePosition
        {
            get
            {
                if (Properties.Settings.Default.LogoOnLefthand)
                    return _leftTextFourRowFourthLine;
                return _rightTextFourRowFourthLine;
            }
        }

        [JsonIgnore]
        public PointF TextFourRowSecondLinePosition
        {
            get
            {
                if (Properties.Settings.Default.LogoOnLefthand)
                    return _leftTextFourRowSecondLine;
                return _rightTextFourRowSecondLine;
            }
        }

        [JsonIgnore]
        public PointF TextFourRowThirdLinePosition
        {
            get
            {
                if (Properties.Settings.Default.LogoOnLefthand)
                    return _leftTextFourRowThirdLine;
                return _rightTextFourRowThirdLine;
            }
        }

        [JsonIgnore]
        public PointF TextOneRowFirstLinePosition
        {
            get
            {
                if (Properties.Settings.Default.LogoOnLefthand)
                    return _leftTextOneRowFirstLine;
                return _rightTextOneRowFirstLine;
            }
        }

        [JsonIgnore]
        public PointF TextTwoRowFirstLinePosition
        {
            get
            {
                if (Properties.Settings.Default.LogoOnLefthand)
                    return _leftTextTwoRowFirstLine;
                return _rightTextTwoRowFirstLine;
            }
        }

        [JsonIgnore]
        public PointF TextTwoRowSecondLinePosition
        {
            get
            {
                if (Properties.Settings.Default.LogoOnLefthand)
                    return _leftTextTwoRowSecondLine;
                return _rightTextTwoRowSecondLine;
            }
        }

        [JsonIgnore]
        public int TransparencyRectangle
        {
            get { return _transparencyRectangle; }
        }

        #endregion Public Properties
    }
}