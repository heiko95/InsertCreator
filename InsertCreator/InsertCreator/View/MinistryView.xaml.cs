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
            this.MGrid.MouseDoubleClick += GridMouseDoubleDown;
        }

        private void GridMouseDoubleDown(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                }
            }
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
    }
}