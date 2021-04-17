using HgSoftware.InsertCreator.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            var isBookValid = (book == "" || _bible.Exists(x => x.Name == book));
            if (!isBookValid)
            {
                AddError(propertyName, "Ungültiges Buch");
                return false;
            }
            return true;
        }

        public bool ValidateChapter(string book, string chapter, string propertyName)
        {
            ClearErrors(propertyName);

            var isChapterValid = (int.TryParse(chapter, out int chapterNumber) && _bible.Exists(x => x.Name == book));
            if (isChapterValid)
            {
                var bibleBook = _bible.Find(x => x.Name == book);

                if (chapterNumber > 0 && chapterNumber <= bibleBook.Chapters.Count)
                {
                    return true;
                }
            }

            if (book == "" && chapter == "")
                return true;

            AddError(propertyName, "Ungültiges Kapitel");
            return false;
        }

        public bool ValidateVerse(string book, string chapter, string verse, string propertyName)
        {
            ClearErrors(propertyName);

            if (book == "" && chapter == "" && verse == "")
                return true;

            if (!IsVerseSyntaxValid(verse) || !IsVerseValueValid(verse, book, chapter))
            {
                AddError(propertyName, "Ungültiger Vers");
                return false;
            }
            return true;
        }

        public bool IsVerseSyntaxValid(string verse)
        {
            Match match = Regex.Match(verse, "([0-9]+([.-][0-9]+)?)(;([0-9]+([.-][0-9]+)?))*");
            if (match.Success && match.Value.Length == verse.Length)
            {
                return true;
            }
            return false;
        }

        public bool IsVerseValueValid(string verse, string book, string chapter)
        {
            var numbers = verse.Replace(".", ";").Replace("-", ";").Split(';').Select(int.Parse).ToList();

            var maxVersesinChapter = _bible.Find(x => x.Name == book).Chapters.Find(x => x.Number.ToString().Equals(chapter)).Versecount;
            if (maxVersesinChapter < numbers.Max())
                return false;

            if (numbers.Count != numbers.Distinct().Count())
                return false;

            return true;
        }
    }
}