using Newtonsoft.Json;
using System.IO;

namespace HgSoftware.InsertCreator.Model
{
    public class PositionJsonReaderWriter
    {
        #region Private Fields

        /// <summary>
        /// Logfield
        /// </summary>
        private static readonly log4net.ILog _log = LogHelper.GetLogger();

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
            _log.Info("Load Position Data");
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
            _log.Info("WritePositionData");
            File.WriteAllText(_path, JsonConvert.SerializeObject(positionData));
        }

        #endregion Public Methods
    }
}