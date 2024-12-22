namespace HgSoftware.InsertCreator.Model
{
    public class BibleVerse
    {
        #region Public Constructors

        public BibleVerse(string book,
                          int chapter,
                          int verse,
                          string text)
        {
            Book = book;
            Chapter = chapter;
            Text = text;
            Verse = verse;
        }

        #endregion Public Constructors

        #region Public Properties

        public string Book { get; }
        public int Chapter { get; }
        public string Text { get; }
        public int Verse { get; }

        #endregion Public Properties
    }
}