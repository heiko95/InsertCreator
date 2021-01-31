using Newtonsoft.Json;
using System.Drawing;

namespace HgSoftware.InsertCreator.Model
{
    public class PositionData
    {
        #region Private Fields

        [JsonProperty]
        private PointF _cornerbugPosition = new PointF();

        [JsonProperty]
        private Point _rectanglePosition = new Point();

        [JsonProperty]
        private PointF _leftTextFourRowFirstLine = new PointF();

        [JsonProperty]
        private PointF _leftTextFourRowFourthLine = new PointF();

        [JsonProperty]
        private PointF _leftTextFourRowSecondLine = new PointF();

        [JsonProperty]
        private PointF _leftTextFourRowThirdLine = new PointF();

        [JsonProperty]
        private PointF _leftTextOneRowFirstLine = new PointF();

        [JsonProperty]
        private PointF _leftTextTwoRowFirstLine = new PointF();

        [JsonProperty]
        private PointF _leftTextTwoRowSecondLine = new PointF();

        [JsonProperty]
        private PointF _leftLogoPosition = new PointF();

        [JsonProperty]
        private PointF _rightLogoPosition = new PointF();

        [JsonProperty]
        private PointF _rightTextFourRowFirstLine = new PointF();

        [JsonProperty]
        private PointF _rightTextFourRowFourthLine = new PointF();

        [JsonProperty]
        private PointF _rightTextFourRowSecondLine = new PointF();

        [JsonProperty]
        private PointF _rightTextFourRowThirdLine = new PointF();

        [JsonProperty]
        private PointF _rightTextOneRowFirstLine = new PointF();

        [JsonProperty]
        private PointF _rightTextTwoRowFirstLine = new PointF();

        [JsonProperty]
        private PointF _rightTextTwoRowSecondLine = new PointF();

        public PositionData()
        {
            _cornerbugPosition.X = 1766;
            _cornerbugPosition.Y = 44;

            _rectanglePosition.X = 0;
            _rectanglePosition.Y = 782;

            /// Logo on Lefthand Side

            _leftLogoPosition.X = 50;
            _leftLogoPosition.Y = 802;        

            _leftTextOneRowFirstLine.X = 310;
            _leftTextOneRowFirstLine.Y = 853;

            _leftTextTwoRowFirstLine.X = 310;
            _leftTextTwoRowFirstLine.Y = 808;

            _leftTextTwoRowSecondLine.X = 310;
            _leftTextTwoRowSecondLine.Y = 899;

            _leftTextFourRowFirstLine.X = 310;
            _leftTextFourRowFirstLine.Y = 806;

            _leftTextFourRowSecondLine.X = 310;
            _leftTextFourRowSecondLine.Y = 855;
            
            _leftTextFourRowThirdLine.X = 310;
            _leftTextFourRowThirdLine.Y = 916;

            _leftTextFourRowFourthLine.X = 310;
            _leftTextFourRowFourthLine.Y = 946;

            /// Logo on Righthand Side

            _rightLogoPosition.X = 1450;
            _rightLogoPosition.Y = 802;

            _rightTextOneRowFirstLine.X = 70;
            _rightTextOneRowFirstLine.Y = 853;

            _rightTextTwoRowFirstLine.X = 70;
            _rightTextTwoRowFirstLine.Y = 808;

            _rightTextTwoRowSecondLine.X = 70;
            _rightTextTwoRowSecondLine.Y = 899;

            _rightTextFourRowFirstLine.X = 70;
            _rightTextFourRowFirstLine.Y = 806;

            _rightTextFourRowSecondLine.X = 70;
            _rightTextFourRowSecondLine.Y = 855;

            _rightTextFourRowThirdLine.X = 70;
            _rightTextFourRowThirdLine.Y = 916;

            _rightTextFourRowFourthLine.X = 70;
            _rightTextFourRowFourthLine.Y = 946;
        }

        [JsonIgnore]
        public PointF CornerbugPosition
        {
            get { return _cornerbugPosition; } 
        }

        [JsonIgnore]
        public Point RectanglePosition
        {            
            get { return _rectanglePosition; }
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
        public PointF TextFourRowFourthLinePosition
        {
            get
            {
                if (Properties.Settings.Default.LogoOnLefthand)
                    return _leftTextFourRowFourthLine;
                return _rightTextFourRowFourthLine;
            }
        }
        #endregion Private Fields
    }
}