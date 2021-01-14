using System.IO;
using System.Reflection;

namespace Liedeinblendung.ViewModel
{
    public class InfoViewModel : ObservableObject
    {
        public InfoViewModel()
        {
            License = File.ReadAllText(($"{Directory.GetCurrentDirectory()}/License.txt"));
        }

        public string Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string PublishDate
        {
            get
            {
                return File.GetCreationTime(Assembly.GetExecutingAssembly().Location).ToString().Split(' ')[0];
            }
        }

        public string License
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
    }
}