using System.Windows;

namespace HgSoftware.InsertCreator.View
{
    /// <summary>
    /// Interaction logic for PreView.xaml
    /// </summary>
    public partial class PreView : Window
    {
        public PreView()
        {
            InitializeComponent();
        }

        private void Window_loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }
    }
}