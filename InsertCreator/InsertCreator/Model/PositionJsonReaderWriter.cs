using Newtonsoft.Json;
using System;
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

        #endregion Private Fields

        #region Public Constructors

        public PositionJsonReaderWriter()
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public void LoadPositionData<T>(ref T positionData, string path) where T : IPositionData
        {
            _log.Info("Load Position Data");

            var positionListText = File.ReadAllText(path);

            if (!string.IsNullOrEmpty(positionListText))
            {
                positionData = JsonConvert.DeserializeObject<T>(positionListText);
            }
        }

        public void WritePositionData(IPositionData positionData, string path)
        {
            _log.Info("WritePositionData");
            File.WriteAllText(path, JsonConvert.SerializeObject(positionData));
        }

        #endregion Public Methods
    }
}