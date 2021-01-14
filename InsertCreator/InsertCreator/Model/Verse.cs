using System.Collections.Generic;

namespace Liedeinblendung.Model
{
    public class Verse
    {
        public int Number { get; set; }

        public List<string> Lines { get; set; } = new List<string>();
    }
}