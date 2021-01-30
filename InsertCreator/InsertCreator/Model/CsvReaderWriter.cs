using HgSoftware.InsertCreator.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

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
            using var reader = new StreamReader(filepath);
            var ministries = new ObservableCollection<MinistryGridViewModel>();

            string headerLine = reader.ReadLine();

            string line;

            if (headerLine.StartsWith("Textbox40"))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(line))
                        break;
                    var values = line.Split(',');
                    var function = values[1].Split(' ')[0];
                    var surename = values[4].TrimStart('"');
                    var forename = values[5].TrimStart(' ').Split(' ')[0];
                    ministries.Add(new MinistryGridViewModel() { ForeName = forename, SureName = surename, Function = function });
                }
            }
            else
            {
                while ((line = reader.ReadLine()) != null)
                {
                    var values = line.Split(',');
                    ministries.Add(new MinistryGridViewModel() { ForeName = values[2], SureName = values[1], Function = values[0] });
                }
            }

            return ministries;
        }

        private void SaveCsv(ObservableCollection<MinistryGridViewModel> ministries, string filepath)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Path.GetFileNameWithoutExtension(filepath));
            sb.Append("\r\n");

            var count = 0;

            for (int i = 0; i < ministries.Count; i++)
            {
                string[] ministry = new string[] { ministries[i].Function, ministries[i].SureName, ministries[i].ForeName };

                for (int j = 0; j < ministry.Length; j++)
                {
                    //Append data with separator.
                    sb.Append(ministry[j] + ",");
                }

                //Append new line character.
                sb.Append("\r\n");
                count++;
            }
            File.WriteAllBytes(filepath, Encoding.UTF8.GetBytes(sb.ToString()));

            if (count == 1)
                MessageBox.Show($"{count} Eintrag wurde exportiert", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show($"{count} Einträge wurden exportiert", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion Private Methods
    }
}