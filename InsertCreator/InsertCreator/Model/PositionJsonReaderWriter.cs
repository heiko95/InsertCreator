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

        private readonly string _path;

        #endregion Private Fields

        #region Public Constructors

        public PositionJsonReaderWriter(string path)
        {
            _path = path;
        }

        #endregion Public Constructors

        #region Public Methods

        public void LoadPositionData<T>(ref T positionData) where T : IPositionData
        {
            _log.Info("Load Position Data");

            var positionListText = File.ReadAllText(_path);

            if (!string.IsNullOrEmpty(positionListText))
            {
                positionData = JsonConvert.DeserializeObject<T>(positionListText);
            }
        }

        public void WritePositionData(IPositionData positionData)
        {
            _log.Info("WritePositionData");
            File.WriteAllText(_path, JsonConvert.SerializeObject(positionData));
        }

        #endregion Public Methods
    }
}