using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HgSoftware.InsertCreator.Model;
using AutoCompleteTextBox.Editors;
using System.IO;

namespace HgSoftware.InsertCreator.Providers
{
    public class BibleSuggestionProvider : IComboSuggestionProvider
    {
        public IEnumerable<BibleBook> BibleBooks { get; set; }

        private BibleJsonReader _bibleJsonReader = new BibleJsonReader();

        public BibleBook GetExactSuggestion(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter)) return null;
            return
                BibleBooks
                    .FirstOrDefault(BibleBook => string.Equals(BibleBook.Name, filter, StringComparison.CurrentCultureIgnoreCase));
        }

        public IEnumerable<BibleBook> GetSuggestions(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter)) return null;
            System.Threading.Thread.Sleep(1000);

            var test = BibleBooks
                    .Where(BibleBook => BibleBook.Name.IndexOf(filter, StringComparison.CurrentCultureIgnoreCase) > -1)
                    .ToList();

            return test;                
        }      


        System.Collections.IEnumerable IComboSuggestionProvider.GetSuggestions(string filter)
        {
            return GetSuggestions(filter);
        }

        public System.Collections.IEnumerable GetFullCollection()
        {
            return BibleBooks.ToList();
        }

        public BibleSuggestionProvider()
        {

            var books = _bibleJsonReader.LoadBibleData(($"{Directory.GetCurrentDirectory()}/DataSource/Bible_Data.json"));
            BibleBooks = books;
        }
    }
}
