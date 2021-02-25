using Newtonsoft.Json;
using System.IO;

namespace HgSoftware.InsertCreator.Model
{
    public class PositionJsonReaderWriter
    {
        #region Private Fields

        private readonly string _path;

        #endregion Private Fields

        #region Public Constructors

        public PositionJsonReaderWriter(string path)
        {
            _path = path;
        }

        #endregion Public Constructors

        #region Public Methods

        public PositionData LoadPositionData()
        {
            var positionData = new PositionData();

            var positionListText = File.ReadAllText(_path);

            if (!string.IsNullOrEmpty(positionListText))
            {
                positionData = JsonConvert.DeserializeObject<PositionData>(positionListText);
            }

            return positionData;
        }

        public void WritePositionData(PositionData positionData)
        {
            File.WriteAllText(_path, JsonConvert.SerializeObject(positionData));
        }

        #endregion Public Methods
    }
}