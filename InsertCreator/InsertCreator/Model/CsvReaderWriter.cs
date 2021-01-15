using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using HgSoftware.InsertCreator.ViewModel;

namespace HgSoftware.InsertCreator.Model
{
    public class CsvReaderWriter
    {
        #region Private Fields

        private MinistryJsonReaderWriter _ministryJsonReaderWriter = new MinistryJsonReaderWriter($"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Ministry.json");

        #endregion Private Fields

        #region Public Methods

        public ObservableCollection<MinistryGridViewModel> ImportCsv()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetEnvironmentVariable("userprofile");
                openFileDialog.Filter = "csv files (*.csv)|*.csv";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = false;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        return ReadCsv(openFileDialog.FileName);
                    }
                    catch
                    {
                        MessageBox.Show("Fehler beim Import", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return null;
        }

        public void SaveCsv()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "csv files (*.csv)|*.csv";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.InitialDirectory = Environment.GetEnvironmentVariable("userprofile");
            saveFileDialog1.FileName = $"Export_{DateTime.UtcNow.ToString("u").Split(' ')[0]}";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var ministries = _ministryJsonReaderWriter.LoadMinistryData();
                SaveCsv(ministries, saveFileDialog1.FileName);
            }
        }

        #endregion Public Methods

        #region Private Methods

        private ObservableCollection<MinistryGridViewModel> ReadCsv(string filepath)
        {
            using (var reader = new StreamReader(filepath))
            {
                var ministries = new ObservableCollection<MinistryGridViewModel>();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    ministries.Add(new MinistryGridViewModel() { ForeName = values[1], SureName = values[0], Function = values[2] });
                }

                return ministries;
            }
        }

        private void SaveCsv(ObservableCollection<MinistryGridViewModel> ministries, string filepath)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ministries.Count; i++)
            {
                string[] ministry = new string[] { ministries[i].SureName, ministries[i].ForeName, ministries[i].Function };
                for (int j = 0; j < ministry.Length; j++)
                {
                    //Append data with separator.
                    sb.Append(ministry[j] + ';');
                }
                //Append new line character.
                sb.Append("\r\n");
            }
            File.WriteAllBytes(filepath, Encoding.UTF8.GetBytes(sb.ToString()));
        }

        #endregion Private Methods
    }
}