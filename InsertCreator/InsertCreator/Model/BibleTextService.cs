using CsvHelper;
using CsvHelper.Configuration;
using HgSoftware.InsertCreator.ViewModel;
using System;
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
            ReadBible();
        }

        public List<int> GetVerseList(string verses)
        {
            var result = new List<int>();

            if (string.IsNullOrEmpty(verses))
                return result;

            var sections = verses.Split(';').ToList();

            foreach (var section in sections)
            {
                var innerSections = section.Split('.').ToList();

                foreach (var innerSection in innerSections)
                {
                    var inner = innerSection.Split('-').ToList();

                    if (inner.Count == 1)
                        result.Add(int.Parse(inner[0]));
                    else
                    {
                        var first = int.Parse(inner[0]);
                        var second = int.Parse(inner[1]);
                        var count = second - first;
                        var state = first;
                        while (count >= 0)
                        {
                            result.Add(state);
                            state++;
                            count--;
                        }
                    }
                }
            }

            return result;
        }


        #endregion Public Constructors

        #region Public Methods

        public string GetBibleText(string book, int chapter, List<int> verses)
        {
            var sb = new StringBuilder();
            foreach (var verse in verses)
            {
                var bibleText = _bibleVerses.FirstOrDefault(x => x.Book == book && x.Chapter == chapter && x.Verse == verse)?.Text ?? string.Empty;
                if (string.IsNullOrEmpty(bibleText)) continue;
                sb.Append(verse);
                sb.Append(" ");
                sb.Append(bibleText);
                sb.Append(" ");
            }
            return  sb.ToString();
        }

       

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