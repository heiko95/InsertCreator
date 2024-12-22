using System.Collections.Generic;

namespace HgSoftware.InsertCreator.Model
{
    public class BibleTextRepository
    {
        #region Public Properties

        public IList<BibleVerse> BibleVerses { get; set; } = new List<BibleVerse>();

        #endregion Public Properties
    }
}