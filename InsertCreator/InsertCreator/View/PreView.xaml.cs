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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }
    }
}