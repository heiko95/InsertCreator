using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HgSoftware.InsertCreator.ViewModel
{
    public class BibleBrowserViewModel : ObservableObject
    {
        public BibleBrowserViewModel()
        {
            BibleLink = "https://www.die-bibel.de/bibeln/online-bibeln/lesen/LU17/MAT.1/Matth%C3%A4us-1";
        }

        /// <summary>
        /// Softwarelicence
        /// </summary>
        public string BibleLink
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}