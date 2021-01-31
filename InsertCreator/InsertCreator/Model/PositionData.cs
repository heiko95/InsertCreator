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
        private PointF _logoPositionLeft = new PointF();

        [JsonProperty]
        private PointF _logoPositionRight = new PointF();

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
            _cornerbugPosition.X = 1;
            _cornerbugPosition.Y = 1;

            _leftTextFourRowFirstLine.X = 1;
            _leftTextFourRowFirstLine.Y = 1;

            _leftTextFourRowFourthLine.X = 1;
            _leftTextFourRowFourthLine.Y = 1;

            _leftTextFourRowSecondLine.X = 1;
            _leftTextFourRowSecondLine.Y = 1;

            _leftTextFourRowThirdLine.X = 1;
            _leftTextFourRowThirdLine.Y = 1;

            _leftTextOneRowFirstLine.X = 1;
            _leftTextOneRowFirstLine.Y = 1;

            _leftTextTwoRowFirstLine.X = 1;
            _leftTextTwoRowFirstLine.Y = 1;

            _leftTextTwoRowSecondLine.X = 1;
            _leftTextTwoRowSecondLine.Y = 1;

            _logoPositionLeft.X = 1;
            _logoPositionLeft.Y = 1;

            _logoPositionRight.X = 1;
            _logoPositionRight.Y = 1;

            _rightTextFourRowFirstLine.X = 1;
            _rightTextFourRowFirstLine.Y = 1;

            _rightTextFourRowFourthLine.X = 1;
            _rightTextFourRowFourthLine.Y = 1;

            _rightTextFourRowSecondLine.X = 1;
            _rightTextFourRowSecondLine.Y = 1;

            _rightTextFourRowThirdLine.X = 1;
            _rightTextFourRowThirdLine.Y = 1;

            _rightTextOneRowFirstLine.X = 1;
            _rightTextOneRowFirstLine.Y = 1;

            _rightTextTwoRowFirstLine.X = 1;
            _rightTextTwoRowFirstLine.Y = 1;

            _rightTextTwoRowSecondLine.X = 1;
            _rightTextTwoRowSecondLine.Y = 1;
        }

        [JsonIgnore]
        public PointF CornerbugPosition
        {
            get { return _cornerbugPosition; } 
        }

        [JsonIgnore]
        public PointF LogoPosition
        {
            get 
            {
                if (Properties.Settings.Default.LogoOnLefthand)
                    return _logoPositionLeft;
                return _logoPositionRight;
            }           
        }

     


        #endregion Private Fields
    }
}