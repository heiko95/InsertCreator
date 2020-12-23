using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Liedeinblendung.Model
{
    public class Verse
    {
        public int Number { get; set; }

        public List<string> Lines { get; set; } = new List<string>();
    }
}
