using System.IO;
using System.Reflection;

namespace Liedeinblendung.ViewModel
{
    public class InfoViewModel : ObservableObject
    {
        #region Public Constructors

        public InfoViewModel()
        {
            License = File.ReadAllText(($"{Directory.GetCurrentDirectory()}/License.txt"));
        }

        #endregion Public Constructors

        #region Public Properties

        public string License
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public string PublishDate
        {
            get
            {
                return File.GetCreationTime(Assembly.GetExecutingAssembly().Location).ToString().Split(' ')[0];
            }
        }

        public string Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        #endregion Public Properties
    }
}