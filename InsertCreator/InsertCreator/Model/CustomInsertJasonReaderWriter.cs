using HgSoftware.InsertCreator.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HgSoftware.InsertCreator.Model
{
    class CustomInsertJasonReaderWriter
    {
        #region Private Fields

        private string _path;

        #endregion Private Fields

        #region Public Constructors

        public CustomInsertJasonReaderWriter(string path)
        {
            _path = path;
        }

        #endregion Public Constructors

        #region Public Methods

        public List<CustomListViewModel> LoadMinistryData()
        {
            var insertList = new List<CustomListViewModel>();

            var insertListtext = File.ReadAllText(_path);

            if (!string.IsNullOrEmpty(insertListtext))
            {
                var tmpInsert = JsonConvert.DeserializeObject<List<CustomListViewModel>>(insertListtext);

                foreach (var insert in tmpInsert)
                {     
                    if (!(String.IsNullOrEmpty(insert.TextLaneOne) && String.IsNullOrEmpty(insert.TextLaneTwo)))
                        insertList.Add(insert);                 
                }
            }
            return insertList;
        }


        public void WriteMinistryData(List<CustomListViewModel> inserts)
        {
            File.WriteAllText(_path, JsonConvert.SerializeObject(inserts));
        }


        #endregion Public Methods
    }
}

