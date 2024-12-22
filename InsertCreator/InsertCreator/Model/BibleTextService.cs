using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace HgSoftware.InsertCreator.Model
{
    public class BibleTextService
    {
        #region Private Fields

        private readonly string _filePath;
        private IList<BibleVerse> _bibleVerses = new List<BibleVerse>();

        #endregion Private Fields

        #region Public Constructors

        public BibleTextService(string filePath)
        {
            _filePath = filePath;
        }

        #endregion Public Constructors

        #region Public Methods

        public string GetBibleText(string book, int chapter, int verse)
            => _bibleVerses.FirstOrDefault(x => x.Book == book && x.Chapter == chapter && x.Verse == verse)?.Text ?? string.Empty;

        public void ReadBible()
        {
            if (!File.Exists(_filePath)) return;
            _bibleVerses.Clear();

            using var reader = new StreamReader(_filePath);
            var configuration = new CsvConfiguration(new CultureInfo("de-DE"))
            {
                Encoding = Encoding.UTF8,
                Delimiter = "~",
                HasHeaderRecord = false
            };

            var csv = new CsvReader(reader, configuration);
            var headerRecord = csv.Read(); // Read Header Row
            while (csv.Read())
            {
                _bibleVerses.Add(new BibleVerse(csv.GetField(0)!,
                                                csv.GetField<int>(1),
                                                csv.GetField<int>(2)!,
                                                csv.GetField(3)!));
            }
        }

        #endregion Public Methods
    }
}