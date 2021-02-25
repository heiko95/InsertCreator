using HgSoftware.InsertCreator.Extensions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;

namespace HgSoftware.InsertCreator.Model
{
    public class HymnalData : IInsertData
    {
        #region Public Constructors

        public HymnalData(string book, string number, string name, string textAutor, string melodieAutor, ObservableCollection<SelectedVerse> selectedVerses)
        {
            Book = book;
            Number = number;
            Name = name;
            TextAutor = textAutor;
            MelodieAutor = melodieAutor;
            SongVerses = WriteInputVers(selectedVerses);
        }

        #endregion Public Constructors

        #region Public Properties

        public string Book { get; set; }

        public string FirstLine => $"{Book} {Number}";
        public string MelodieAutor { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }

        public string SecondLine => Name.LimitString(20);
        public string SongVerses { get; set; }
        public string TextAutor { get; set; }

        #endregion Public Properties

        #region Private Methods

        private string WriteInputVers(ObservableCollection<SelectedVerse> verses)
        {
            StringBuilder state = new StringBuilder("");

            string inputVerse = "";

            foreach (var verse in verses)
            {
                if (verse.IsSelected)
                    state.Append("T");
                else
                    state.Append("F");
            }

            List<Match> matches = new List<Match>();

            foreach (Match match in Regex.Matches(state.ToString(), "[T]{1,}"))
            {
                matches.Add(match);
            }

            if (matches.Count > 0)
            {
                inputVerse = " / ";

                foreach (var match in matches)
                {
                    switch (match.Length)
                    {
                        case 1:
                            inputVerse = $"{inputVerse}{match.Index + 1}";
                            break;

                        case 2:
                            inputVerse = $"{inputVerse}{match.Index + 1}-{match.Index + match.Length}";
                            break;

                        default:
                            inputVerse = $"{inputVerse}{match.Index + 1}-{match.Index + match.Length}";
                            break;
                    }

                    inputVerse = $"{inputVerse} + ";
                }

                char[] charsToTrim = { ' ', '+' };
                inputVerse = inputVerse.TrimEnd(charsToTrim);
                return inputVerse;
            }
            return inputVerse;
        }

        #endregion Private Methods
    }
}