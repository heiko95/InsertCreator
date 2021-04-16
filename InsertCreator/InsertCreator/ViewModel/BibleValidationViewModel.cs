using HgSoftware.InsertCreator.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class BibleValidationViewModel : BaseErrorViewModel
    {
        private List<BibleBook> _bible;

        public BibleValidationViewModel(List<BibleBook> Bible) : base()
        {
            _bible = Bible;
        }

        public bool ValidateBook(string book, string propertyName)
        {
            ClearErrors(propertyName);
            if (!_bible.Exists(x => x.Name == book))
            {
                AddError(propertyName, "Ungültiges Buch");
                return false;
            }
            return true;
        }

        public bool ValidateChapter(string book, string chapter, string propertyName)
        {
            ClearErrors(propertyName);

            if (int.TryParse(chapter, out int chapterNumber) && _bible.Exists(x => x.Name == book))
            {
                var bibleBook = _bible.Find(x => x.Name == book);

                if (chapterNumber > 0 && chapterNumber <= bibleBook.Chapters.Count)
                {
                    return true;
                }
            }
            AddError(propertyName, "Ungültiges Kapitel");
            return false;
        }

        public bool ValidateVerse(string book, string chapter, string verse)
        {
            return true;
        }
    }
}