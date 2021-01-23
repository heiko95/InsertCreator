using HgSoftware.InsertCreator.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace HgSoftware.InsertCreator
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(WindowViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}