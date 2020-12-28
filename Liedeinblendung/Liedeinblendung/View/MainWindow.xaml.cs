using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Liedeinblendung
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private object _selected;



        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (List.SelectedItem != null)
            {
                _selected = List.SelectedItem;
                return;
            }
            List.SelectedItem = _selected;

        }


    }
}
