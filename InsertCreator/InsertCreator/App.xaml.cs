using Liedeinblendung.Model;
using Liedeinblendung.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
                File.Create($"{path}/FadeText.txt", 1024);
                File.Create($"{path}/FadeTextMeta.txt", 1024);
                File.Create($"{path}/Ministry.json", 1024);

                
            }
            fadeInWriter.LoadImages();




            var vm = new WindowViewModel();
            var window = new MainWindow(vm);
            window.Show();



        }
    }
}
