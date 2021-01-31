using HgSoftware.InsertCreator.Model;
using HgSoftware.InsertCreator.ViewModel;
using System;
using System.IO;
using System.Windows;

namespace HgSoftware.InsertCreator
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Logfield
        /// </summary>
        private static readonly log4net.ILog _log = LogHelper.GetLogger();

        protected override void OnStartup(StartupEventArgs e)
        {
            string path = $"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator";
            string positionPath = $"{Environment.GetEnvironmentVariable("userprofile")}/InsertCreator/Position.Json";

            PositionJsonReaderWriter positionDatajsonReaderWriter = new PositionJsonReaderWriter(positionPath);
            
            PositionData positionData = new PositionData();

            if (!File.Exists(positionPath))
            {
                var file = File.Create(positionPath, 1024);
                file.Close();
                positionDatajsonReaderWriter.WritePositionData(positionData);
            }
            else
                positionData = positionDatajsonReaderWriter.LoadPositionData();

            FadeInWriter fadeInWriter = new FadeInWriter(positionData);


            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            _log.Info("Create Ministry.json");
            FileCreate("Ministry.json", path);
            FileCreate("Insert.json", path);

            _log.Info("Load Images");
            fadeInWriter.LoadImages();

            _log.Info("Create ViewModel");
            var vm = new WindowViewModel(positionData);

            _log.Info("Open Window");
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