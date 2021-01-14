using System.Collections.Generic;

namespace Liedeinblendung.Model
{
    public class Verse
    {
        #region Public Properties

        public List<string> Lines { get; set; } = new List<string>();
        public int Number { get; set; }

        #endregion Public Properties
    }
}