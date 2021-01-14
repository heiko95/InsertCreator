using Liedeinblendung.Model;
using Liedeinblendung.ViewModel;
using System;
using System.IO;
using System.Windows;

namespace Liedeinblendung
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            string path = $"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator";
            FadeInWriter fadeInWriter = new FadeInWriter();

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            FileCreate("FadeText.txt", path);
            FileCreate("FadeTextMeta.txt", path);
            FileCreate("Ministry.json", path);
            fadeInWriter.LoadImages();

            var vm = new WindowViewModel();
            var window = new MainWindow(vm);
            window.Show();
        }

        private void FileCreate(string filename, string path)
        {
            var fullpath = $"{path}/{filename}";

            if (!File.Exists(fullpath))
            {
                var file = File.Create(fullpath, 1024);
                file.Close();
            }
        }
    }
}