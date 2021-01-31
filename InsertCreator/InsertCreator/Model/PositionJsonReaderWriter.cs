using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HgSoftware.InsertCreator.Model
{
    public class PositionJsonReaderWriter
    {
        #region Private Fields

        private string _path;

        #endregion Private Fields

        #region Public Constructors

        public PositionJsonReaderWriter(string path)
        {
            _path = path;
        }

        #endregion Public Constructors

        #region Public Methods

        public PositionData LoadMinistryData()
        {
            var positionData = new PositionData();

            var positionListText = File.ReadAllText(_path);

            if (!string.IsNullOrEmpty(positionListText))
            {
                 positionData = JsonConvert.DeserializeObject<PositionData>(positionListText);                              
            }

            return positionData;
        }


        public void WriteMinistryData(PositionData positionData)
        {
            File.WriteAllText(_path, JsonConvert.SerializeObject(positionData));
        }


        #endregion Public Methods

    }
}
