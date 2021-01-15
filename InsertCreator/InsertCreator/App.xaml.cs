using System;
using System.IO;
using System.Windows;
using HgSoftware.InsertCreator.Model;
using HgSoftware.InsertCreator.ViewModel;

namespace HgSoftware.InsertCreator
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