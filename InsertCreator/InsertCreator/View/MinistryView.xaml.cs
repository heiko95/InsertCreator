using System.Windows.Controls;
using System.Windows.Input;

namespace Liedeinblendung.View
{
    /// <summary>
    /// Interaction logic for MinistryView.xaml
    /// </summary>
    public partial class MinistryView : UserControl
    {
        public MinistryView()
        {
            InitializeComponent();
            this.MGrid.PreviewKeyDown += MoveCellOnEnterKey;
        }

        private void MoveCellOnEnterKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Cancel [Enter] key event.
                e.Handled = true;
                // Press [Tab] key programatically.
                var tabKeyEvent = new KeyEventArgs(
                  e.KeyboardDevice, e.InputSource, e.Timestamp, Key.Tab);
                tabKeyEvent.RoutedEvent = Keyboard.KeyDownEvent;
                InputManager.Current.ProcessInput(tabKeyEvent);
            }
        }

        private void MGrid_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            foreach (var column in MGrid.Columns)
            {
                column.IsReadOnly = false;
            }
        }

        private void MGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            foreach (var column in MGrid.Columns)
            {
                column.IsReadOnly = true;
            }
        }
    }
}